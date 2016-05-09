using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using Framework.Common.Constants;
using Framework.Common.PopupManager;
using MOST.Common.Utility;
using MOST.VesselOperator.Parm;
using MOST.VesselOperator.Result;
using MOST.Client.Proxy.CommonProxy;
using MOST.Common;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using Framework.Service.Provider.WebService.Provider;
using MOST.Client.Proxy.VesselOperatorProxy;

namespace MOST.VesselOperator
{
    public partial class HVO105 : TForm, IPopupWindow
    {
        private int m_workingStatus;
        private HVO105Parm m_parm;
        private HVO105Result m_result;
        private ArrayList m_arrBerthItems;

        string prevAtb = string.Empty;
        string Atw = string.Empty;
        string Atc = string.Empty;
        string prevAtu = string.Empty;
        string currAtu = string.Empty;
        string transferMode = string.Empty;
        string currWharfMarkFm = string.Empty;
        string currWharfMarkTo = string.Empty;
        string currWharf = string.Empty;

        
        public HVO105(int workingStatus)
        {
            m_workingStatus = workingStatus;
            InitializeComponent();
            this.initialFormSize();

            // For checking if form is dirty or not
            List<string> controlNames = new List<string>();
            controlNames.Add(txtATB.Name);
            controlNames.Add(txtATU.Name);
            controlNames.Add(txtMooring.Name);
            controlNames.Add(txtTug.Name);
            controlNames.Add(txtWharfFrom.Name);
            controlNames.Add(txtWharfTo.Name);
            controlNames.Add(txtRequester.Name);
            controlNames.Add(cboNewWharf.Name);
            controlNames.Add(cboPosition.Name);
            controlNames.Add(cboReason.Name);
            controlNames.Add(chkPilot.Name);
            this.AddOnChangeHandlerToInputControls(this.Controls, controlNames);

            m_arrBerthItems = new ArrayList();
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HVO105Parm)parm;
            transferMode = m_parm.transferMode;
            InitializeData();
            DisplayData();

            this.ShowDialog();
            return m_result;
        }

        private bool CheckNewAtuAtb(string atu, string atb)
        {
            bool isVal = true;
            if (!string.IsNullOrEmpty(atu) && !string.IsNullOrEmpty(atb))
            {
                DateTime Atb = CommonUtility.ParseYMDHM(atb);
                DateTime Atu = CommonUtility.ParseYMDHM(atu);
                if (Atu.CompareTo(Atb) < 0) //New Atb < New Atu
                    isVal = false;
            }
            return isVal;
        }

        private void DisplayData()
        {
            SftDblBankingParm vorParm = new SftDblBankingParm();
            vorParm.vslCallId = m_parm.JpvcInfo.Jpvc;
            if (transferMode.Equals(Constants.TSM_UPDATE))
                vorParm.vslShiftingSeq = m_parm.SftDblBankingItem.seq;
            else vorParm.vslShiftingSeq = "";

            IVesselOperatorProxy vslProxy = new VesselOperatorProxy();
            ResponseInfo infoResult = vslProxy.getCurrAtbAtu(vorParm);

            if ((infoResult != null) && (infoResult.list != null) && (infoResult.list.Length == 1) && (infoResult.list[0] is SftDblBankingItem))
            {
                prevAtb = ((SftDblBankingItem)infoResult.list[0]).atb;
                currWharfMarkFm = ((SftDblBankingItem)infoResult.list[0]).wharfMarkFm.ToString();
                currWharfMarkTo = ((SftDblBankingItem)infoResult.list[0]).wharfMarkTo.ToString();
                currWharf = ((SftDblBankingItem)infoResult.list[0]).currWharf.ToString();
                if (transferMode.Equals(Constants.TSM_INSERT))
                {
                    Atw = string.Empty;
                    Atc = string.Empty;
                }
                else
                {
                    Atw = ((SftDblBankingItem)infoResult.list[0]).atw;
                    Atc = ((SftDblBankingItem)infoResult.list[0]).atc;
                }
                if (!string.IsNullOrEmpty(((SftDblBankingItem)infoResult.list[0]).vslShiftingSeq))
                {
                    currAtu = ((SftDblBankingItem)infoResult.list[0]).atu;
                    //prevAtu = ((SftDblBankingItem)infoResult.list[0]).prevAtu;
                }
                else // get atu from vsl schedule
                    currAtu = ((SftDblBankingItem)infoResult.list[0]).atu;
            }


            CommonUtility.SetDTPValueBlank(txtATB);
            CommonUtility.SetDTPValueBlank(txtATU);

            txtJPVC.Text = m_parm.JpvcInfo.Jpvc;
            txtRequester.Text = UserInfo.getInstance().UserId;

            if (m_workingStatus == Constants.MODE_UPDATE && m_parm.SftDblBankingItem != null)
            {
                SftDblBankingItem item = m_parm.SftDblBankingItem;
                txtRequester.Text = item.reqr;
                
                txtWharfFrom.Text = item.wharfMarkFm;
                txtWharfTo.Text = item.wharfMarkTo;
                txtMooring.Text = item.mooring;
                txtTug.Text = item.tug;
                chkPilot.Checked = false;
                if ("Y".Equals(item.pilotYn))
                {
                    chkPilot.Checked = true;
                }

                //For (R24) Vsl Shifting CR issus  18 March, 2015. William
                //string atuDt = string.IsNullOrEmpty(item.prevAtu) ? m_parm.JpvcInfo.Atu : item.prevAtu;
                //CommonUtility.SetDTPValueDMYHM(txtATU, atuDt);
                CommonUtility.SetDTPValueDMYHM(txtATU, item.atuDt);
                
                CommonUtility.SetComboboxSelectedItem(cboNewWharf, item.nxBerthNo);
                CommonUtility.SetComboboxSelectedItem(cboPosition, item.berthAlongside);
                CommonUtility.SetComboboxSelectedItem(cboReason, item.rsnCd);

                if (string.IsNullOrEmpty(item.svcId))
                {
                    // Item which was inputted from MPTS

                    txtCurWharf.Text = item.currWharf;
                    CommonUtility.SetDTPValueDMYHM(txtATB, item.atbDt);

                    btnF1.Enabled = true;
                    txtRequester.Enabled = true;
                    txtRequester.isMandatory = true;
                }
                else
                {
                    // Item which is requested from MSS.

                    if (string.IsNullOrEmpty(item.atbDt.Trim()))
                    {
                        IVesselOperatorProxy proxy = new VesselOperatorProxy();
                        SftDblBankingParm parm = new SftDblBankingParm();
                        parm.vslCallId = txtJPVC.Text;
                        parm.searchType = "HHT_IF_VS_CW";
                        ResponseInfo info = proxy.getSftDblBankingList(parm);
                        if (info.list[0] is SftDblBankingItem)
                        {
                            SftDblBankingItem itemMSS = (SftDblBankingItem)info.list[0];
                            CommonUtility.SetDTPValueDMYHM(txtATB, itemMSS.prevAtu);
                            txtCurWharf.Text = itemMSS.currWharf;
                        }
                    }
                    else
                    {
                        txtCurWharf.Text = item.currWharf;
                        CommonUtility.SetDTPValueDMYHM(txtATB, item.atbDt);
                    }

                    btnF1.Enabled = false;
                    txtRequester.Enabled = false;
                    txtRequester.isMandatory = false;
                }
            }
            else
            {   //For (R24) Vsl Shifting CR issus ,  18 March, 2015. William
                //Current ATB = Pre ATU and get Current Loc

                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                SftDblBankingParm parm = new SftDblBankingParm();
                parm.vslCallId = txtJPVC.Text;
                parm.searchType = "HHT_IF_VS_CW";
                ResponseInfo info = proxy.getSftDblBankingList(parm);
                if (info.list[0] is SftDblBankingItem)
                {
                    SftDblBankingItem item = (SftDblBankingItem)info.list[0];
                    CommonUtility.SetDTPValueDMYHM(txtATB,item.prevAtu );
                    txtCurWharf.Text = item.currWharf;
                }

                
            }
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Current Wharf
                if (m_parm != null && m_parm.JpvcInfo != null)
                {
                    txtCurWharf.Text = m_parm.JpvcInfo.BerthLocation;
                }
                #endregion

                #region New Wharf
                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                SftDblBankingParm parm = new SftDblBankingParm();
                parm.searchType = "HHT_CB_NW";
                ResponseInfo info = proxy.getSftDblBankingList(parm);

                CommonUtility.InitializeCombobox(cboNewWharf);
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is BerthInfoItem)
                    {
                        BerthInfoItem item = (BerthInfoItem)info.list[i];
                        cboNewWharf.Items.Add(new ComboboxValueDescriptionPair(item.berthCd, item.berthCd));
                        m_arrBerthItems.Add(item);
                    }
                }
                #endregion

                #region Position
                parm = new SftDblBankingParm();
                parm.searchType = "HHT_CB_SP";
                info = proxy.getSftDblBankingList(parm);

                CommonUtility.InitializeCombobox(cboPosition);
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CodeMasterListItem)
                    {
                        CodeMasterListItem item = (CodeMasterListItem)info.list[i];
                        cboPosition.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                    else if (info.list[i] is CodeMasterListItem1)
                    {
                        CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                        cboPosition.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                }
                #endregion

                #region Reason
                parm = new SftDblBankingParm();
                parm.searchType = "HHT_CB_RS";
                info = proxy.getSftDblBankingList(parm);

                CommonUtility.InitializeCombobox(cboReason);
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CodeMasterListItem)
                    {
                        CodeMasterListItem item = (CodeMasterListItem)info.list[i];
                        cboReason.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                    else if (info.list[i] is CodeMasterListItem1)
                    {
                        CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                        cboReason.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                }
                #endregion

                
            }
            catch (Framework.Common.Exception.BusinessException ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void ReturnInfo()
        {
          
            m_result = new HVO105Result();
            SftDblBankingItem item = null;

            //item.atw = item.atw.Trim();
            //item.atc = item.atc.Trim();

            // ref: CT209 & CT105007
            if (m_workingStatus == Constants.MODE_ADD)
            {
                item = new SftDblBankingItem();
                item.CRUD = Constants.WS_INSERT;

                item.currWharfMakrFm = currWharfMarkFm;
                item.currWharfMakrTo = currWharfMarkTo;
                item.prevAtb = prevAtb.Trim() ;
                item.prevAtu = currAtu.Trim();

            }
            else if (m_workingStatus == Constants.MODE_UPDATE)
            {
                item = m_parm.SftDblBankingItem;
                // If Working Status is not INSERT (exist item), change it to UPDATE
                // Else (new item), remain status.
                if (!Constants.WS_INSERT.Equals(item.CRUD))
                {
                    item.CRUD = Constants.WS_UPDATE;
                }

                if (!string.IsNullOrEmpty(item.svcId))
                {
                    //MSS CASE 31, MARCH 2015 - WILLIAM
                    //If Vsl Shifting requested from MSS, in case first update: save pre Loc Mark, Pre Atb and pre Atu
                    //In case second update: don't need to set infor for pre Loc Mark
                    //If vslShiftingSeq = null/''/ < 2 then consider this vsl has not Shifting, PreInfo will be get from vsl schedule.
                    //Other case, pre info will be get from pre Shift ( vslShiftingSeq send to server will bill minus 1).
                    //
                    SftDblBankingParm preInfo = new SftDblBankingParm();
                    preInfo.vslCallId = m_parm.JpvcInfo.Jpvc;
                    if (transferMode.Equals(Constants.TSM_UPDATE))
                        preInfo.vslShiftingSeq = m_parm.SftDblBankingItem.seq;
                    else preInfo.vslShiftingSeq = "";

                    IVesselOperatorProxy preInfoProxy = new VesselOperatorProxy();
                    ResponseInfo preInfoResult = preInfoProxy.getPrevShiftInfo(preInfo);

                    if ((preInfoResult != null) && (preInfoResult.list != null) && (preInfoResult.list.Length == 1) && (preInfoResult.list[0] is SftDblBankingItem))
                    {
                        SftDblBankingItem sftDblBankingItem = ((SftDblBankingItem)preInfoResult.list[0]);
                        item.currWharfMakrFm = sftDblBankingItem.wharfMarkFm.ToString();
                        item.currWharfMakrTo = sftDblBankingItem.wharfMarkTo.ToString();
                        item.prevAtb = sftDblBankingItem.atb.Trim();
                        item.prevAtu = sftDblBankingItem.atu.Trim();
                        if (string.IsNullOrEmpty(item.prevAtu))
                        {
                            //MessageBox.Show("Can not proceed! You have to unberth at old location first.", "ALTERT", MessageBoxButtons.OK, MessageBoxIcon.Hand,MessageBoxDefaultButton.Button1);
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO105_1111"));
                            return;
                        }
                    }
                }
            }

            if (item != null)
            {
                item.searchType = "vesselShifting";
                item.vslCallId = m_parm.JpvcInfo.Jpvc;
                item.reqr = txtRequester.Text;

                //For (R24) Vsl Shifting CR issus ,  18 March, 2015. William
                item.atuDt = txtATU.Text.Trim();

                item.atbDt = txtATB.Text.Trim();
                item.currWharf = txtCurWharf.Text;
                item.nxBerthNo = CommonUtility.GetComboboxSelectedValue(cboNewWharf);
                item.wharfMarkFm = txtWharfFrom.Text;
                item.wharfMarkTo = txtWharfTo.Text;
                item.berthAlongside = CommonUtility.GetComboboxSelectedValue(cboPosition);
                item.berthAlongsideNm = CommonUtility.GetComboboxSelectedDescription(cboPosition);
                item.rsnCd = CommonUtility.GetComboboxSelectedValue(cboReason);
                item.rsnNm = CommonUtility.GetComboboxSelectedDescription(cboReason);
                item.pilotYn = chkPilot.Checked ? "Y" : "N";
                item.mooring = txtMooring.Text;
                item.tug = txtTug.Text;
                item.userId = UserInfo.getInstance().UserId;

                //Vessel Shifting CR, 24 March 2015, William
                //Comment
                /*if (m_parm.JpvcInfo != null)
                {
                    item.currWharf = m_parm.JpvcInfo.BerthLocation;
                }*/
                m_result.SftDblBankingItem = item;
            }
        }

        private bool Validate()
        {
            if (!ValidateWharfFromTo())
            {
                return false;
            }

            return true;
        }

        private bool ValidateWharfFromTo()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (m_parm != null && m_parm.JpvcInfo != null &&
                    !Constants.VSLTYPECD_LIQUID.Equals(m_parm.JpvcInfo.VslTp))
                {
                    double wharfFm = CommonUtility.ParseDouble(txtWharfFrom.Text);
                    double wharfTo = CommonUtility.ParseDouble(txtWharfTo.Text);

                    // Wharf Mark End must be greater than Wharf Mark Start
                    if (wharfTo < wharfFm)
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO105_0004"));
                        txtWharfTo.Focus();
                        txtWharfTo.SelectAll();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return true;
        }


        //Validate Datetime
        private bool validateTime()
        {
            //add new shifting, dont care atw and atc
            bool isVal = true;
            if (transferMode.Equals(Constants.TSM_UPDATE))
            {
                DateTime ATUDate = CommonUtility.ParseYMDHM(txtATU.Text);
                DateTime ATCDate = CommonUtility.ParseYMDHM(Atc);
                DateTime NewATBDate = CommonUtility.ParseYMDHM(txtATB.Text);
                DateTime ATWDate = CommonUtility.ParseYMDHM(Atw);

                if (!string.IsNullOrEmpty(Atc) && !string.IsNullOrEmpty(Atw))
                {
                    if (!string.IsNullOrEmpty(txtATU.Text))
                        if (string.IsNullOrEmpty(txtATB.Text))
                            isVal = false;
                        else if ((ATUDate < ATCDate) || (ATUDate < ATWDate) || (ATUDate < NewATBDate))
                            isVal = false;


                    if (!string.IsNullOrEmpty(Atc) && !string.IsNullOrEmpty(Atw))
                        if (NewATBDate > ATWDate || NewATBDate > ATCDate)
                            isVal = false;
                }
            }
            return isVal;
        }


        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnF1":
                    PartnerCodeListParm reqrParm = new PartnerCodeListParm();
                    reqrParm.Option = "CD";
                    PartnerCodeListResult reqrRes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_REQUESTER), reqrParm);
                    if (reqrRes != null)
                    {
                        txtRequester.Text = reqrRes.Code;
                    }
                    break;

                case "btnOk":
                    if (this.IsDirty)
                    {   
                        if (this.validations(this.Controls) && Validate())
                        {
                           
                            //William, March 16, 2015.
                            //For (R24) Vsl Shifting CR issus 
                            //Check vessel Unberth ?

                            if (transferMode.Equals(Constants.TSM_INSERT))
                            {
                                if (string.IsNullOrEmpty(currAtu))
                                {
                                    //MessageBox.Show("Can not proceed! You have to unberth at old location first.", "ALTERT", MessageBoxButtons.OK, MessageBoxIcon.Hand,MessageBoxDefaultButton.Button1);
                                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO105_1111"));
                                    return;
                                }
                            }

                            if (!validateTime())
                            {
                                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO105_1112"));
                                return;
                            }
                            else
                            {
                                if (transferMode.Equals(Constants.TSM_INSERT))
                                {
                                    if (!string.IsNullOrEmpty(txtATB.Text))
                                    {
                                        if (!string.IsNullOrEmpty(currAtu))
                                        {
                                            DateTime ATUDate = CommonUtility.ParseYMDHM(currAtu);
                                            DateTime newATB = CommonUtility.ParseYMDHM(txtATB.Text);
                                            if (newATB < ATUDate)
                                            {
                                                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO105_1112"));
                                                return;
                                            }
                                        }
                                    }
                                }
                            }

                            if (!CheckNewAtuAtb(txtATU.Text, txtATB.Text))
                            {
                                CommonUtility.AlertMessage("Please input ATB < ATU","WARNING !!!");
                                return;
                            }


                            //If User inputs ATB 
                            //then New Wharf, Wharf Mark and Position 
                            //are mandatory field.
                            string mandatoryField = string.Empty;
                            if (!string.IsNullOrEmpty(txtATB.Text))
                            {
                                if (string.IsNullOrEmpty(CommonUtility.GetComboboxSelectedValue(cboNewWharf)))
                                    mandatoryField += "New Wharf is mandatory.\r\n";
                                if (string.IsNullOrEmpty(txtWharfFrom.Text))
                                    mandatoryField += "Wharf Mark is mandatory.\r\n";
                                if (string.IsNullOrEmpty(CommonUtility.GetComboboxSelectedValue(cboPosition)))
                                    mandatoryField += "Position is mandatory.";
                            }

                            if (!string.IsNullOrEmpty(mandatoryField))
                            {
                                CommonUtility.AlertMessage(mandatoryField);
                                return;
                            }

                            ReturnInfo();
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            this.Close();
                        }
                    }
                    else
                    {
                        this.Close();
                    }
                    break;

                case "btnCancel":
                    if (this.IsDirty)
                    {
                        DialogResult dr = CommonUtility.ConfirmMsgSaveChances();
                        if (dr == DialogResult.Yes)
                        {
                            if (this.validations(this.Controls))
                            {
                                ReturnInfo();
                                this.Close();
                            }
                        }
                        else if (dr == DialogResult.No)
                        {
                            m_result = null;
                            this.Close();
                        }
                    }
                    else
                    {
                        m_result = null;
                        this.Close();
                    }
                    break;
            }
        }

        private void txtWharfFrom_LostFocus(object sender, EventArgs e)
        {
            double motherVslLoa = 0;
            if (m_parm != null && m_parm.JpvcInfo != null)
            {
                motherVslLoa = CommonUtility.ParseDouble(m_parm.JpvcInfo.Loa);
            }
            double wharfFm = CommonUtility.ParseDouble(txtWharfFrom.Text);
            double wharfTo = wharfFm + motherVslLoa;

            txtWharfTo.Text = wharfTo.ToString();
        }

        private void cboNewWharf_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pstSta = "0";
            string pstEnd = "0";
            int currIndex = cboNewWharf.SelectedIndex;
            if (m_arrBerthItems != null && 0 < currIndex && currIndex <= m_arrBerthItems.Count)
            {
                BerthInfoItem item = (BerthInfoItem)m_arrBerthItems[currIndex];
                pstSta = item.pstSta;
                pstEnd = item.pstEnd;
            }
            txtBerthWharfS.Text = pstSta;
            txtBerthWharfE.Text = pstEnd;
        }
    }
}