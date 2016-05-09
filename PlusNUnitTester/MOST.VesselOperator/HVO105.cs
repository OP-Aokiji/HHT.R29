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
            InitializeData();
            DisplayData();
            this.ShowDialog();
            return m_result;
        }

        private void DisplayData()
        {
            CommonUtility.SetDTPValueBlank(txtATB);
            CommonUtility.SetDTPValueBlank(txtATU);

            txtJPVC.Text = m_parm.JpvcInfo.Jpvc;
            txtRequester.Text = UserInfo.getInstance().UserId;

            if (m_workingStatus == Constants.MODE_UPDATE && m_parm.SftDblBankingItem != null)
            {
                SftDblBankingItem item = m_parm.SftDblBankingItem;
                txtRequester.Text = item.reqr;
                txtCurWharf.Text = item.currWharf;
                txtWharfFrom.Text = item.wharfMarkFm;
                txtWharfTo.Text = item.wharfMarkTo;
                txtMooring.Text = item.mooring;
                txtTug.Text = item.tug;
                chkPilot.Checked = false;
                if ("Y".Equals(item.pilotYn))
                {
                    chkPilot.Checked = true;
                }
                string atuDt = string.IsNullOrEmpty(item.prevAtu) ? m_parm.JpvcInfo.Atu : item.prevAtu;
                CommonUtility.SetDTPValueDMYHM(txtATU, atuDt);
                CommonUtility.SetDTPValueDMYHM(txtATB, item.atbDt);
                CommonUtility.SetComboboxSelectedItem(cboNewWharf, item.nxBerthNo);
                CommonUtility.SetComboboxSelectedItem(cboPosition, item.berthAlongside);
                CommonUtility.SetComboboxSelectedItem(cboReason, item.rsnCd);

                if (string.IsNullOrEmpty(item.svcId))
                {
                    // Item which was inputted from MPTS
                    btnF1.Enabled = true;
                    txtRequester.Enabled = true;
                    txtRequester.isMandatory = true;
                }
                else
                {
                    // Item which is requested from MSS.
                    btnF1.Enabled = false;
                    txtRequester.Enabled = false;
                    txtRequester.isMandatory = false;
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
            
            // ref: CT209 & CT105007
            if (m_workingStatus == Constants.MODE_ADD)
            {
                item = new SftDblBankingItem();
                item.CRUD = Constants.WS_INSERT;
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
            }

            if (item != null)
            {
                item.searchType = "vesselShifting";
                item.vslCallId = m_parm.JpvcInfo.Jpvc;
                item.reqr = txtRequester.Text;
                item.prevAtu = txtATU.Text;
                item.atbDt = txtATB.Text;
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
                if (m_parm.JpvcInfo != null)
                {
                    item.currWharf = m_parm.JpvcInfo.BerthLocation;
                }

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