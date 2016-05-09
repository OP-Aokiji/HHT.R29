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
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.Client.Proxy.VesselOperatorProxy;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using Framework.Common.Helper;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.VesselOperator
{
    public partial class HVO108 : TForm, IPopupWindow
    {
        private HVO108Parm m_parm;
        private HVO108Result m_result;
        private SearchJPVCResult m_jpvc2Result;
        private SearchJPVCResult m_jpvc3Result;
        private bool m_isValidJPVC2;
        //private bool m_inputtedJPVC3;
        private int m_workingStatus;

        public HVO108(int workingStatus)
        {
            m_workingStatus = workingStatus;
            InitializeComponent();
            this.initialFormSize();
            m_isValidJPVC2 = false;
            //m_inputtedJPVC3 = false;

            List<string> controlNames = new List<string>();
            controlNames.Add(cboBType.Name);
            controlNames.Add(txtJPVC2.Name);
            controlNames.Add(txtLOA2.Name);
            controlNames.Add(txtATB2.Name);
            controlNames.Add(txtATU2.Name);
            controlNames.Add(txtATW2.Name);
            controlNames.Add(txtATC2.Name);
            controlNames.Add(txtJPVC3.Name);
            this.AddOnChangeHandlerToInputControls(this.Controls, controlNames);

            //////////////////////////////////////////////////////////////////////////
            // Making button text be multiline
            WndProcHooker.MakeButtonMultiline(this.btnJPVC2);
            //////////////////////////////////////////////////////////////////////////
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HVO108Parm)parm;
            InitializeData();
            DisplayData();
            this.ShowDialog();
            return m_result;
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Initialize data
                txtJPVC.Text = m_parm.JpvcInfo.Jpvc;
                CommonUtility.SetDTPValueBlank(txtATB2);
                CommonUtility.SetDTPValueBlank(txtATU2);
                CommonUtility.SetDTPValueBlank(txtATW2);
                CommonUtility.SetDTPValueBlank(txtATC2);
                #endregion

                #region Banking Type
                ICommonProxy commonProxy = new CommonProxy();
                CommonCodeParm commonParm = new CommonCodeParm();
                commonParm.searchType = "COMM";
                commonParm.lcd = "MT";
                commonParm.divCd = "DBLBNKDIV";
                ResponseInfo commonInfo = commonProxy.getCommonCodeList(commonParm);
                CommonUtility.InitializeCombobox(cboBType);
                for (int i = 0; i < commonInfo.list.Length; i++)
                {
                    if (commonInfo.list[i] is CodeMasterListItem)
                    {
                        CodeMasterListItem item = (CodeMasterListItem)commonInfo.list[i];
                        cboBType.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                    else if (commonInfo.list[i] is CodeMasterListItem1)
                    {
                        CodeMasterListItem1 item = (CodeMasterListItem1)commonInfo.list[i];
                        cboBType.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                }
                #endregion
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

        private void DisplayData()
        {
            if (m_workingStatus == Constants.MODE_UPDATE && m_parm.SftDblBankingItem != null)
            {
                SftDblBankingItem item = m_parm.SftDblBankingItem;
                CommonUtility.SetComboboxSelectedItem(cboBType, item.dblBnkDivCd);

                txtJPVC2.Text = item.dblBnkShip1;
                txtLOA2.Text = item.ship1Loa;
                CommonUtility.SetDTPValueDMYHM(txtATB2, item.ship1Atb);
                CommonUtility.SetDTPValueDMYHM(txtATW2, item.ship1Atw);
                CommonUtility.SetDTPValueDMYHM(txtATC2, item.ship1Atc);
                CommonUtility.SetDTPValueDMYHM(txtATU2, item.ship1Atu);
                if (!string.IsNullOrEmpty(txtJPVC2.Text))
                {
                    if (CommonUtility.IsValidJPVC(txtJPVC2.Text, ref m_jpvc2Result))
                    {
                        m_isValidJPVC2 = true;
                        btnJPVC2.Enabled = true;
                    }
                    else
                    {
                        m_isValidJPVC2 = false;
                    }
                }

                txtJPVC3.Text = item.dblBnkShip2;
                if (!string.IsNullOrEmpty(txtJPVC3.Text))
                {
                    if (CommonUtility.IsValidJPVC(txtJPVC3.Text, ref m_jpvc3Result))
                    {
                        //m_inputtedJPVC3 = true;
                        btnJPVC3.Enabled = true;
                    }
                    else
                    {
                        //m_inputtedJPVC3 = false;
                    }
                }
            }
        }

        private void ReturnInfo()
        {
            // ref: CT209 & CT105007
            m_result = new HVO108Result();
            SftDblBankingItem item = null;

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
                item.userId = UserInfo.getInstance().UserId;
                item.searchType = "doubleBanking";
                item.vslCallId = txtJPVC.Text;
                if (m_parm != null && m_parm.JpvcInfo != null)
                {
                    item.loa = m_parm.JpvcInfo.Loa;
                    item.atb = m_parm.JpvcInfo.Atb;
                    item.atw = m_parm.JpvcInfo.Atw;
                    item.atc = m_parm.JpvcInfo.Atc;
                    item.atu = m_parm.JpvcInfo.Atu;
                }

                item.dblBnkDivCd = CommonUtility.GetComboboxSelectedValue(cboBType);
                item.dblBnkDivCdNm = CommonUtility.GetComboboxSelectedDescription(cboBType);

                item.dblBnkShip1 = txtJPVC2.Text;
                item.ship1Loa = txtLOA2.Text;
                item.ship1Atb = txtATB2.Text;
                item.ship1Atw = txtATW2.Text;
                item.ship1Atc = txtATC2.Text;
                item.ship1Atu = txtATU2.Text;

                item.dblBnkShip2 = txtJPVC3.Text;
                if (m_jpvc3Result != null)
                {
                    item.ship2Loa = m_jpvc3Result.Loa;
                    item.ship2Atb = m_jpvc3Result.Atb;
                    item.ship2Atw = m_jpvc3Result.Atw;
                    item.ship2Atc = m_jpvc3Result.Atc;
                    item.ship2Atu = m_jpvc3Result.Atu;
                }

                item.stDt = CommonUtility.ToString(item.stDt).Trim();
                item.endDt = CommonUtility.ToString(item.endDt).Trim();
                item.atb = CommonUtility.ToString(item.atb).Trim();
                item.atw = CommonUtility.ToString(item.atw).Trim();
                item.atc = CommonUtility.ToString(item.atc).Trim();
                item.atu = CommonUtility.ToString(item.atu).Trim();
                item.ship1Atb = CommonUtility.ToString(item.ship1Atb).Trim();
                item.ship1Atw = CommonUtility.ToString(item.ship1Atw).Trim();
                item.ship1Atc = CommonUtility.ToString(item.ship1Atc).Trim();
                item.ship1Atu = CommonUtility.ToString(item.ship1Atu).Trim();
                item.ship2Atb = CommonUtility.ToString(item.ship2Atb).Trim();
                item.ship2Atw = CommonUtility.ToString(item.ship2Atw).Trim();
                item.ship2Atc = CommonUtility.ToString(item.ship2Atc).Trim();
                item.ship2Atu = CommonUtility.ToString(item.ship2Atu).Trim();

                m_result.SftDblBankingItem = item;
            }
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnF1":
                    MOST.Common.CommonParm.SearchJPVCParm jpvc2Parm = new MOST.Common.CommonParm.SearchJPVCParm();
                    jpvc2Parm.Jpvc = txtJPVC2.Text;
                    MOST.Common.CommonResult.SearchJPVCResult jpvc2Result = (SearchJPVCResult)PopupManager.instance.ShowPopup(new MOST.Common.HCM101(), jpvc2Parm);
                    if (jpvc2Result != null)
                    {
                        m_isValidJPVC2 = true;
                        m_jpvc2Result = jpvc2Result;
                        txtJPVC2.Text = m_jpvc2Result.Jpvc;
                        txtLOA2.Text = m_jpvc2Result.Loa;
                        CommonUtility.SetDTPValueDMYHM(txtATB2, m_jpvc2Result.Atb);
                        CommonUtility.SetDTPValueDMYHM(txtATU2, m_jpvc2Result.Atu);
                        CommonUtility.SetDTPValueDMYHM(txtATW2, m_jpvc2Result.Atw);
                        CommonUtility.SetDTPValueDMYHM(txtATC2, m_jpvc2Result.Atc);
                        btnJPVC2.Enabled = true;

                        // Banking Type
                        string strBType = CommonUtility.GetComboboxSelectedValue(cboBType);
                        switch (strBType)
                        {
                            // 'Barge/Tug'
                            case "BT":
                                txtJPVC3.Text = m_jpvc2Result.Jpvc;
                                break;
                        }
                    }
                    break;

                case "btnJPVC2":
                    HVO101001Parm dBankingParm = new HVO101001Parm();
                    dBankingParm.StsJpvcInfo = m_jpvc2Result;
                    dBankingParm.DblBankingVslCallId = txtJPVC.Text;
                    PopupManager.instance.ShowPopup(new HVO101001(), dBankingParm);
                    break;

                case "btnJPVC3":
                    HVO122Parm jpvc3Parm = new HVO122Parm();
                    jpvc3Parm.BankingType = CommonUtility.GetComboboxSelectedValue(cboBType);
                    jpvc3Parm.JpvcInfo = m_jpvc2Result;
                    m_jpvc3Result = (SearchJPVCResult)PopupManager.instance.ShowPopup(new HVO122(), jpvc3Parm);
                    if (m_jpvc3Result != null)
                    {
                        //m_inputtedJPVC3 = true;
                    }
                    break;

                //case "btnOk":
                //    if (this.IsDirty)
                //    {
                //        if (this.validations(this.Controls) && Validate())
                //        {
                //            if (ProcessgetSftDblBankingItem())
                //            {
                //                ClearForm();
                //                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
                //            }
                //        }
                //    }
                //    else
                //    {
                //        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                //        this.Close();
                //    }
                //    break;

                //case "btnCancel":
                //    if (this.IsDirty)
                //    {
                //        DialogResult dr = CommonUtility.ConfirmMsgSaveChances();
                //        if (dr == DialogResult.Yes)
                //        {
                //            if (this.validations(this.Controls) && Validate())
                //            {
                //                ProcessgetSftDblBankingItem();
                //                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                //                this.Close();
                //            }
                //        }
                //        else if (dr == DialogResult.No)
                //        {
                //            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                //            this.Close();
                //        }
                //    }
                //    else
                //    {
                //        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                //        this.Close();
                //    }
                //    break;

                case "btnOk":
                    if (this.IsDirty)
                    {
                        if (this.validations(this.Controls))
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

                case "btnList":
                    HVO108Parm listParm = new HVO108Parm();
                    if (m_parm != null && m_parm.JpvcInfo != null)
                    {
                        listParm.JpvcInfo = m_parm.JpvcInfo;
                    }
                    PopupManager.instance.ShowPopup(new HVO114(), listParm);
                    break;
            }
        }

        private void cboBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //m_inputtedJPVC3 = false;
            txtJPVC2.isMandatory = false;
            txtLOA2.isMandatory = false;
            txtATB2.isMandatory = false;
            txtATU2.isMandatory = false;
            txtATW2.isMandatory = false;
            txtATC2.isMandatory = false;
            txtJPVC2.Enabled = false;
            txtLOA2.Enabled = false;
            txtATB2.Enabled = false;
            txtATU2.Enabled = false;
            txtATW2.Enabled = false;
            txtATC2.Enabled = false;

            txtJPVC3.isMandatory = false;
            btnJPVC3.Enabled = false;
            btnJPVC2.Enabled = false;

            // Banking Type
            string strBType = CommonUtility.GetComboboxSelectedValue(cboBType);
            switch (strBType)
            {
                // 'Vessel'
                case "VL":
                    txtATB2.Enabled = true;
                    txtATU2.Enabled = true;
                    txtJPVC2.Enabled = true;
                    txtJPVC2.isMandatory = true;
                    break;

                // 'Barge/Tug'
                case "BT":
                    txtJPVC2.Enabled = true;
                    txtATB2.Enabled = true;
                    txtATU2.Enabled = true;
                    txtJPVC2.isMandatory = true;

                    btnJPVC3.Enabled = true;
                    txtJPVC3.isMandatory = true;
                    break;

                // 'Tug':
                case "TG":
                    btnJPVC2.Enabled = true;
                    txtLOA2.Enabled = true;
                    txtJPVC2.isMandatory = true;
                    txtLOA2.isMandatory = true;
                    break;

                // 'Tug Alongside'
                case "TA":
                    btnJPVC2.Enabled = true;
                    txtLOA2.Enabled = true;
                    txtATB2.Enabled = true;
                    txtATU2.Enabled = true;
                    txtATW2.Enabled = true;
                    txtATC2.Enabled = true;
                    txtJPVC2.isMandatory = true;
                    txtLOA2.isMandatory = true;
                    break;

                // 'Tug Replacement'
                case "TR":
                    btnJPVC2.Enabled = true;
                    btnJPVC3.Enabled = true;
                    txtLOA2.Enabled = true;
                    txtATU2.Enabled = true;

                    txtJPVC3.isMandatory = true;
                    txtLOA2.isMandatory = true;
                    txtATU2.isMandatory = true;
                    break;
            }

            ChangeData(true);
        }

        private void ChangeData(bool isClearData)
        {
            m_jpvc2Result = null;
            if (isClearData)
            {
                // 2nd vessel
                txtJPVC2.Text = "";
                txtLOA2.Text = "";
                CommonUtility.SetDTPValueBlank(txtATB2);
                CommonUtility.SetDTPValueBlank(txtATU2);
                CommonUtility.SetDTPValueBlank(txtATW2);
                CommonUtility.SetDTPValueBlank(txtATC2);

                // 3rd vessel
                txtJPVC3.Text = "";
            }

            // Banking Type
            string strBType = CommonUtility.GetComboboxSelectedValue(cboBType);
            switch (strBType)
            {
                // 'Vessel'
                case "VL":
                    btnJPVC2.Enabled = false;
                    break;

                // 'Barge/Tug'
                case "BT":
                    btnJPVC2.Enabled = false;
                    break;

                // 'Tug' or 'Tug Alongside'
                case "TG":
                case "TA":
                    if (m_parm != null && m_parm.JpvcInfo != null)
                    {
                        m_isValidJPVC2 = true;
                        m_jpvc2Result = m_parm.JpvcInfo;

                        txtJPVC2.Text = m_jpvc2Result.Jpvc;
                        CommonUtility.SetDTPValueDMYHM(txtATB2, m_jpvc2Result.Atb);
                        CommonUtility.SetDTPValueDMYHM(txtATU2, m_jpvc2Result.Atu);
                        CommonUtility.SetDTPValueDMYHM(txtATW2, m_jpvc2Result.Atw);
                        CommonUtility.SetDTPValueDMYHM(txtATC2, m_jpvc2Result.Atc);
                    }
                    break;

                // 'Tug Replacement'
                case "TR":
                    if (m_parm != null && m_parm.JpvcInfo != null)
                    {
                        m_isValidJPVC2 = true;
                        m_jpvc2Result = m_parm.JpvcInfo;

                        txtJPVC2.Text = m_jpvc2Result.Jpvc;
                        CommonUtility.SetDTPValueDMYHM(txtATB2, m_jpvc2Result.Atb);
                        CommonUtility.SetDTPValueDMYHM(txtATW2, m_jpvc2Result.Atw);

                        txtJPVC3.Text = m_jpvc2Result.Jpvc;
                    }
                    break;
            }
        }

        private void txtJPVC2_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJPVC2.Text) && !m_isValidJPVC2)
            {
                GetVesselScheduleDetail(txtJPVC2.Text);
            }
        }

        private void txtJPVC2_KeyPress(object sender, KeyPressEventArgs e)
        {
            m_isValidJPVC2 = false;
            //m_inputtedJPVC3 = false;
            ClearJPVC2();

            // if key = Enter then get vessel schedule detail
            if (e.KeyChar.Equals((char)Keys.Enter))
            {
                GetVesselScheduleDetail(txtJPVC2.Text);
            }
        }

        private void GetVesselScheduleDetail(string argJPVC)
        {
            try
            {
                if (!string.IsNullOrEmpty(argJPVC))
                {
                    Cursor.Current = Cursors.WaitCursor;

                    ClearJPVC2();

                    #region Request Webservice
                    IVesselOperatorProxy proxy = new VesselOperatorProxy();
                    VesselScheduleParm parm = new VesselScheduleParm();
                    parm.jpvcNo = argJPVC;
                    ResponseInfo info = proxy.getVesselScheduleListDetail(parm);
                    #endregion

                    #region Display Data
                    if (info != null && info.list.Length > 0)
                    {
                        // ResponseInfo info: VesselScheduleItem, BerthInfoItem, BerthInfoItem, ....
                        // Vessel Schedule
                        if (info.list[0] is VesselScheduleItem)
                        {
                            // For handling event in case it occurs: KeyPress -> LostFocus
                            m_isValidJPVC2 = true;

                            VesselScheduleItem item = (VesselScheduleItem)info.list[0];
                            m_jpvc2Result = new SearchJPVCResult();
                            m_jpvc2Result.Jpvc = item.jpvcNo;
                            m_jpvc2Result.VesselName = item.vslNm;
                            m_jpvc2Result.WharfStart = item.wharfMarkFrom;
                            m_jpvc2Result.WharfEnd = item.wharfMarkTo;
                            m_jpvc2Result.BerthLocation = item.berthLoc;
                            m_jpvc2Result.VslTp = item.vslTp;
                            m_jpvc2Result.VslTpNm = item.vslTpNm;
                            m_jpvc2Result.Loa = item.loa;
                            m_jpvc2Result.Etb = item.etb;
                            m_jpvc2Result.Etw = item.etw;
                            m_jpvc2Result.Etc = item.etc;
                            m_jpvc2Result.Etu = item.etu;
                            m_jpvc2Result.Atb = item.atb;
                            m_jpvc2Result.CurAtb = item.curAtb;
                            m_jpvc2Result.Atw = item.atw;
                            m_jpvc2Result.Atc = item.atc;
                            m_jpvc2Result.Atu = item.atu;
                            m_jpvc2Result.PurpCall = item.purpCall;
                            m_jpvc2Result.PurpCallCd = item.purpCallCd;

                            txtJPVC2.Text = m_jpvc2Result.Jpvc;
                            txtLOA2.Text = m_jpvc2Result.Loa;
                            CommonUtility.SetDTPValueDMYHM(txtATB2, m_jpvc2Result.Atb);
                            CommonUtility.SetDTPValueDMYHM(txtATW2, m_jpvc2Result.Atw);
                            CommonUtility.SetDTPValueDMYHM(txtATC2, m_jpvc2Result.Atc);
                            CommonUtility.SetDTPValueDMYHM(txtATU2, m_jpvc2Result.Atu);

                            // Banking Type
                            string strBType = CommonUtility.GetComboboxSelectedValue(cboBType);
                            switch (strBType)
                            {
                                // 'Vessel'
                                case "VL":
                                    btnJPVC2.Enabled = true;
                                    break;

                                // 'Barge/Tug'
                                case "BT":
                                    btnJPVC2.Enabled = true;
                                    txtJPVC3.Text = item.jpvcNo;
                                    break;
                            }
                        }
                    }
                    #endregion
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
        }

        private void ClearJPVC2()
        {
            txtLOA2.Text = "0";
            CommonUtility.SetDTPValueBlank(txtATB2);
            CommonUtility.SetDTPValueBlank(txtATU2);
            CommonUtility.SetDTPValueBlank(txtATW2);
            CommonUtility.SetDTPValueBlank(txtATC2);
        }

        private void txtJPVC2_EnabledChanged(object sender, EventArgs e)
        {
            btnF1.Enabled = false;
            if (txtJPVC2.Enabled)
            {
                btnF1.Enabled = true;
            }
        }

        //private bool Validate()
        //{
        //    try
        //    {
        //        Cursor.Current = Cursors.WaitCursor;

        //        // Check if 2nd vessel is different from 3rd vessel.
        //        if (!IsDifferentVessel())
        //        {
        //            return false;
        //        }

        //        // Validate JPVC2
        //        if (!ValidateJPVC2())
        //        {
        //            return false;
        //        }

        //        // Validate JPVC3
        //        if (!ValidateJPVC3())
        //        {
        //            return false;
        //        }

        //        // Validate 2nd ATU <= 3rd ATB in case of Tug Replacement
        //        if ("TR".Equals(CommonUtility.GetComboboxSelectedValue(cboBType)))
        //        {
        //            if (!string.IsNullOrEmpty(txtATU2.Text) &&
        //                m_jpvc3Result != null && !string.IsNullOrEmpty(m_jpvc3Result.Atb))
        //            {
        //                DateTime dtFrom = CommonUtility.ParseYMDHM(txtATU2.Text);
        //                DateTime dtTo = CommonUtility.ParseYMDHM(m_jpvc3Result.Atb);
        //                if (dtFrom.CompareTo(dtTo) > 0)
        //                {
        //                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO108_0002"));
        //                    txtATU2.Focus();
        //                    return false;
        //                }
        //            }
        //        }
        //    }
        //    catch (Framework.Common.Exception.BusinessException ex)
        //    {
        //        ExceptionHandler.ErrorHandler(this, ex);
        //    }
        //    finally
        //    {
        //        Cursor.Current = Cursors.Default;
        //    }

        //    return true;
        //}

        //private bool ValidateJPVC2()
        //{
        //    // Check if JPVC2 was inputted in case it's mandatory.
        //    if (txtJPVC2.isMandatory && !m_isValidJPVC2)
        //    {
        //        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0027"));
        //        txtJPVC2.SelectAll();
        //        txtJPVC2.Focus();
        //        return false;
        //    }

        //    // ATB <= ATW <= ATC <= ATU
        //    if (!CommonUtility.ValidateDateOrder(txtATB2, txtATW2, txtATC2, txtATU2))
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //private bool ValidateJPVC3()
        //{
        //    // Check if JPVC3 was inputted in case it's mandatory.
        //    if (txtJPVC3.isMandatory && !m_inputtedJPVC3)
        //    {
        //        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO108_0003"));
        //        btnJPVC3.Focus();
        //        return false;
        //    }
        //    return true;
        //}

        ///// <summary>
        ///// Check whether next vessel is different from mother vessel.
        ///// </summary>
        ///// <returns></returns>
        //private bool IsDifferentVessel()
        //{
        //    bool result = true;
        //    // Banking Type
        //    string strBType = CommonUtility.GetComboboxSelectedValue(cboBType);
        //    switch (strBType)
        //    {
        //        // 'Vessel'
        //        case "VL":
        //            if (txtJPVC2.Text.Equals(m_parm.JpvcInfo.Jpvc))
        //            {
        //                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO108_0001"));
        //                txtJPVC2.SelectAll();
        //                txtJPVC2.Focus();
        //                result = false;
        //            }
        //            break;
        //    }
            
        //    return result;
        //}
    }
}