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
using Framework.Common.ResourceManager;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using Framework.Service.Provider.WebService.Provider;
using MOST.Client.Proxy.VesselOperatorProxy;

namespace MOST.VesselOperator
{
    public partial class HVO106 : TForm, IPopupWindow
    {
        private const string CONST_CGTP_LIQUID_EDIBLE       = "LQE";
        private const string CONST_CGTP_LIQUID_NON_EDIBLE   = "LQN";

        private string m_opeTp;
        private int m_workingStatus;
        private HVO106Parm m_parm;
        private HVO106Result m_result;
        private HVO106001Result m_hoseDtResult;
        private double m_totHourE;   // Total delay time of Liquid Edible (LQE)
        private double m_totHourN;   // Total delay time of Liquid Non Edible (LQN)

        private double par_pln_load;
		private double par_pln_dis;
        private double par_act_load;
		private double par_act_dis;

        private double m_handledLDAmt;
        private double m_handledDSAmt;

        private ResponseInfo m_dlyInfo;

        // Original hose time (from database)
        private string m_orgHoseOnDt = string.Empty;
        private string m_orgCommenceDt = string.Empty;
        private string m_orgCompleteDt = string.Empty;
        private string m_orgHoseOffDt = string.Empty;

        public HVO106(string opeTp, int workingStatus)
        {
            m_opeTp = opeTp;
            m_workingStatus = workingStatus;
            m_hoseDtResult = new HVO106001Result();
            InitializeComponent();
            this.initialFormSize();

            // For checking if form is dirty or not
            List<string> controlNames = new List<string>();
            controlNames.Add(cboOPRType.Name);
            controlNames.Add(cboTmnlOPR.Name);
            controlNames.Add(cboCargoType.Name);
            controlNames.Add(cboLineType.Name);
            controlNames.Add(cboShprCnsne.Name);
            controlNames.Add(cboPkgTp.Name);
            controlNames.Add(cboCmdt.Name);
            controlNames.Add(txtLineNos.Name);
            controlNames.Add(txtTonnage.Name);
            controlNames.Add(txtPumping.Name);
            controlNames.Add(chkCompleted.Name);
            this.AddOnChangeHandlerToInputControls(this.Controls, controlNames);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HVO106Parm)parm;
            SetControls();
            InitializeData();
            DisplayUpdData();
            this.ShowDialog();
            return m_result;
        }

        private void SetControls()
        {
            pnlOPRType.Top = 0;
            pnlOPRType.Left = 0;
            pnlOPRType.Visible = true;

            // Cargo Operation (Liquid Bulk)
            if (HVO113.CONST_OPETP_GENERAL.Equals(m_opeTp))
            {
                this.Text = "V/S - Jetty OPR";

                pnlTmnlOPR.Top = 23;
                pnlTmnlOPR.Left = 0;
                pnlTmnlOPR.Visible = true;
                pnlMain.Top = 47;
                pnlMain.Left = 0;
                pnlMain.Visible = true;
            }
            // Transhipment
            else if (HVO113.CONST_OPETP_TRANSHIPMENT.Equals(m_opeTp))
            {
                this.Text = "V/S - Transhipment";

                pnlTmnlOPR.Top = 23;
                pnlTmnlOPR.Left = 0;
                pnlTmnlOPR.Visible = true;
                pnlMain.Top = 47;
                pnlMain.Left = 0;
                pnlMain.Visible = true;
            }
            // STS
            else if (HVO113.CONST_OPETP_STS.Equals(m_opeTp))
            {
                this.Text = "V/S - STS for Liquid";

                pnlTmnlOPR.Visible = false;
                pnlMain.Top = 23;
                pnlMain.Left = 0;
                pnlMain.Visible = true;
            }
        }

        private void DisplayUpdData()
        {
            chkCompleted.Checked = false;

            if (m_workingStatus == Constants.MODE_UPDATE && m_parm.LiquidBulkItem != null)
            {
                VORLiquidBulkItem item = m_parm.LiquidBulkItem;

                // Display updated data
                txtLineNos.Text = item.lineNo;
                txtTonnage.Text = item.tonHdlAmt;
                txtPumping.Text = item.pumpRate;
                CommonUtility.SetComboboxSelectedItem(cboOPRType, item.jobTpCd);
                CommonUtility.SetComboboxSelectedItem(cboLineType, item.hoseTpCd);
                CommonUtility.SetComboboxSelectedItem(cboCargoType, item.cgTpCd);
                if (HVO113.CONST_OPETP_GENERAL.Equals(m_opeTp) ||
                    HVO113.CONST_OPETP_TRANSHIPMENT.Equals(m_opeTp))
                {
                    CommonUtility.SetComboboxSelectedItem(cboTmnlOPR, item.tkOpr);    
                }
                if ("Y".Equals(item.jobCmplYn) || "Yes".Equals(item.jobCmplYn) || "true".Equals(item.jobCmplYn))
                {
                    chkCompleted.Checked = true;
                }
                CommonUtility.SetComboboxSelectedItem(cboCmdt, item.cmdtCd);
                CommonUtility.SetComboboxSelectedItem(cboShprCnsne, item.shprCnsne);
                CommonUtility.SetComboboxSelectedItem(cboPkgTp, item.pkgTpCd);

                // Backup original hose time
                m_orgHoseOnDt = item.hoseOnDt;
                m_orgCommenceDt = item.stDt;
                m_orgCompleteDt = item.endDt;
                m_orgHoseOffDt = item.hoseOffDt;
                
                // Set hose time
                m_hoseDtResult.HoseOnDt = item.hoseOnDt;
                m_hoseDtResult.HoseOffDt = item.hoseOffDt;
                m_hoseDtResult.CommenceDt = item.stDt;
                m_hoseDtResult.CompleteDt = item.endDt;

                //=========================== Trim text - for temporary - will be removed ======================================
                m_orgHoseOnDt = string.IsNullOrEmpty(m_orgHoseOnDt) ? "" : m_orgHoseOnDt.Trim();
                m_orgCommenceDt = string.IsNullOrEmpty(m_orgCommenceDt) ? "" : m_orgCommenceDt.Trim();
                m_orgCompleteDt = string.IsNullOrEmpty(m_orgCompleteDt) ? "" : m_orgCompleteDt.Trim();
                m_orgHoseOffDt = string.IsNullOrEmpty(m_orgHoseOffDt) ? "" : m_orgHoseOffDt.Trim();
                m_hoseDtResult.HoseOnDt = string.IsNullOrEmpty(m_hoseDtResult.HoseOnDt) ? "" : m_hoseDtResult.HoseOnDt.Trim();
                m_hoseDtResult.HoseOffDt = string.IsNullOrEmpty(m_hoseDtResult.HoseOffDt) ? "" : m_hoseDtResult.HoseOffDt.Trim();
                m_hoseDtResult.CommenceDt = string.IsNullOrEmpty(m_hoseDtResult.CommenceDt) ? "" : m_hoseDtResult.CommenceDt.Trim();
                m_hoseDtResult.CompleteDt = string.IsNullOrEmpty(m_hoseDtResult.CompleteDt) ? "" : m_hoseDtResult.CompleteDt.Trim();
                //==============================================================================================================
            }
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string strJpvcNo = string.Empty;
                if (m_parm != null && m_parm.JpvcInfo != null)
                {
                    strJpvcNo  = m_parm.JpvcInfo.Jpvc;
                }

                ResponseInfo info;
                VORLiquidBulkParm parm;
                IVesselOperatorProxy proxy = new VesselOperatorProxy();

                #region Terminal Operator
                if (HVO113.CONST_OPETP_GENERAL.Equals(m_opeTp) ||
                    HVO113.CONST_OPETP_TRANSHIPMENT.Equals(m_opeTp))
                {
                    parm = new VORLiquidBulkParm();
                    parm.jpvcNo = strJpvcNo;
                    parm.searchType = "HHT_TkOpr";
                    info = proxy.getVORLiquidBulk(parm);

                    CommonUtility.InitializeCombobox(cboTmnlOPR, "Select");
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is VORLiquidBulkItem)
                        {
                            VORLiquidBulkItem item = (VORLiquidBulkItem)info.list[i];
                            cboTmnlOPR.Items.Add(new ComboboxValueDescriptionPair(item.tkOpr, item.tkOpr));
                        }
                    }
                }
                #endregion

                #region Commodity
                parm = new VORLiquidBulkParm();
                parm.jpvcNo = strJpvcNo;
                parm.searchType = "HHT_Cmdt";
                info = proxy.getVORLiquidBulk(parm);

                CommonUtility.InitializeCombobox(cboCmdt, "Select");
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is VORLiquidBulkItem)
                    {
                        VORLiquidBulkItem item = (VORLiquidBulkItem)info.list[i];
                        cboCmdt.Items.Add(new ComboboxValueDescriptionPair(item.cmdtCd, item.cmdtCd));
                    }
                }
                #endregion

                #region Pkg Type
                parm = new VORLiquidBulkParm();
                parm.jpvcNo = strJpvcNo;
                parm.searchType = "HHT_PkgTp";
                info = proxy.getVORLiquidBulk(parm);

                CommonUtility.InitializeCombobox(cboPkgTp, "Select");
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is VORLiquidBulkItem)
                    {
                        VORLiquidBulkItem item = (VORLiquidBulkItem)info.list[i];
                        cboPkgTp.Items.Add(new ComboboxValueDescriptionPair(item.pkgTpCd, item.pkgTpCd));
                    }
                }
                #endregion

                #region Get delay time which is not accepted
                parm = new VORLiquidBulkParm();
                parm.opeTp = m_opeTp;
                parm.jpvcNo = strJpvcNo;
                parm.workYmd = UserInfo.getInstance().Workdate;
                parm.shift = UserInfo.getInstance().Shift;
                parm.searchType = "HHT_LiquidDelay";
                m_dlyInfo = proxy.getVORLiquidBulk(parm);
                #endregion

                #region Line Type
                parm = new VORLiquidBulkParm();
                parm.opeTp = m_opeTp;
                parm.searchType = "HHT_LineTp";
                info = proxy.getVORLiquidBulkComboList(parm);

                CommonUtility.InitializeCombobox(cboLineType, "Select");
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CodeMasterListItem)
                    {
                        CodeMasterListItem item = (CodeMasterListItem)info.list[i];
                        cboLineType.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                    else if (info.list[i] is CodeMasterListItem1)
                    {
                        CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                        cboLineType.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                }
                #endregion

                #region Get Operation Type and Cargo Type from ConfirmationSlip2
                ArrayList csOprTypeList = new ArrayList();
                ArrayList csCgTypeList = new ArrayList();
                ConfirmationSlipParm csparm = new ConfirmationSlipParm();
                csparm.vslCallId = strJpvcNo;
                info = proxy.getConfirmationSlipLiquidBulkList(csparm);

                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is ConfirmationSlipItem)
                    {
                        ConfirmationSlipItem item = (ConfirmationSlipItem)info.list[i];
                        csOprTypeList.Add(item.cgOptTpCd);
                        csCgTypeList.Add(item.cgTpCd);
                    }
                }
                #endregion

                #region Operation Type - fetched from Confirmation Slip 2
                parm = new VORLiquidBulkParm();
                parm.opeTp = m_opeTp;
                parm.searchType = "HHT_OpeTp";
                info = proxy.getVORLiquidBulkComboList(parm);

                CommonUtility.InitializeCombobox(cboOPRType, "Select");
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CodeMasterListItem)
                        info.list[i] = CommonUtility.ToCodeMasterListItem1(info.list[i] as CodeMasterListItem);
                    if (info.list[i] is CodeMasterListItem1)
                    {
                        CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                        if (csOprTypeList.Contains(item.scd))
                        {
                            cboOPRType.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                        }
                    }
                }
                #endregion

                #region Cargo Type - fetched from Confirmation Slip 2
                parm = new VORLiquidBulkParm();
                parm.searchType = "HHT_CgTp";
                info = proxy.getVORLiquidBulkComboList(parm);

                CommonUtility.InitializeCombobox(cboCargoType, "Select");
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CodeMasterListItem)
                        info.list[i] = CommonUtility.ToCodeMasterListItem1(info.list[i] as CodeMasterListItem);
                    if (info.list[i] is CodeMasterListItem1)
                    {
                        CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                        if (csCgTypeList.Contains(item.scd))
                        {
                            cboCargoType.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                        }
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
            // ref: CT108 & CT108003
            m_result = new HVO106Result();
            VORLiquidBulkItem item = null;
            
            if (m_workingStatus == Constants.MODE_ADD)
            {
                item = new VORLiquidBulkItem();
                item.CRUD = Constants.WS_INSERT;
            }
            else if (m_workingStatus == Constants.MODE_UPDATE)
            {
                item = m_parm.LiquidBulkItem;
                // If Working Status is not INSERT (exist item), change it to UPDATE
                // Else (new item), remain status.
                if (!Constants.WS_INSERT.Equals(item.CRUD))
                {
                    item.CRUD = Constants.WS_UPDATE;
                }
            }

            if (item != null)
            {
                item.vslCallId = m_parm.JpvcInfo.Jpvc;
                item.workYmd = UserInfo.getInstance().Workdate;
                item.shftId = UserInfo.getInstance().Shift;
                item.userId = UserInfo.getInstance().UserId;
                item.opeTp = m_opeTp;
                item.jobTpCd = CommonUtility.GetComboboxSelectedValue(cboOPRType);
                item.hoseTpCd = CommonUtility.GetComboboxSelectedValue(cboLineType);
                item.cgTpCd = CommonUtility.GetComboboxSelectedValue(cboCargoType);
                item.tonHdlAmt = txtTonnage.Text;
                item.pumpRate = txtPumping.Text;
                item.lineNo = string.IsNullOrEmpty(txtLineNos.Text) ? "0" : txtLineNos.Text;
                if (m_hoseDtResult != null)
                {
                    item.stDt = m_hoseDtResult.CommenceDt;
                    item.endDt = m_hoseDtResult.CompleteDt;
                    item.hoseOnDt = m_hoseDtResult.HoseOnDt;
                    item.hoseOffDt = m_hoseDtResult.HoseOffDt;
                }
                else
                {
                    item.stDt = string.Empty;
                    item.endDt = string.Empty;
                    item.hoseOnDt = string.Empty;
                    item.hoseOffDt = string.Empty;
                }
                item.jobCmplYn = chkCompleted.Checked ? "Y" : "N";
                item.cmdtCd = CommonUtility.GetComboboxSelectedValue(cboCmdt);
                item.shprCnsne = CommonUtility.GetComboboxSelectedValue(cboShprCnsne);
                item.pkgTpCd = CommonUtility.GetComboboxSelectedValue(cboPkgTp);
                if (HVO113.CONST_OPETP_GENERAL.Equals(m_opeTp) ||
                    HVO113.CONST_OPETP_TRANSHIPMENT.Equals(m_opeTp))
                {
                    item.tkOpr = CommonUtility.GetComboboxSelectedValue(cboTmnlOPR);
                }
            
                m_result.LiquidBulkItem = item;
            }
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnOk":
                    if (this.IsDirty)
                    {   
                        if (HVO113.CONST_OPETP_GENERAL.Equals(m_opeTp) ||
                            HVO113.CONST_OPETP_TRANSHIPMENT.Equals(m_opeTp))
                        {
                            if (this.validations(this.pnlOPRType.Controls) &&
                                this.validations(this.pnlTmnlOPR.Controls) &&
                                this.validations(this.pnlMain.Controls) && 
                                this.Validate())
                            {
                                ReturnInfo();
                                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                                this.Close();
                            }
                        }
                        else if (HVO113.CONST_OPETP_STS.Equals(m_opeTp))
                        {
                            if (this.validations(this.pnlOPRType.Controls) &&
                                this.validations(this.pnlMain.Controls) && 
                                this.Validate())
                            {
                                ReturnInfo();
                                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                    }
                    break;

                case "btnCancel":
                    if (this.IsDirty)
                    {
                        DialogResult dr = CommonUtility.ConfirmMsgSaveChances();
                        if (dr == DialogResult.Yes)
                        {
                            if (HVO113.CONST_OPETP_GENERAL.Equals(m_opeTp) ||
                                HVO113.CONST_OPETP_TRANSHIPMENT.Equals(m_opeTp))
                            {
                                if (this.validations(this.pnlTmnlOPR.Controls) &&
                                    this.validations(this.pnlMain.Controls) && this.Validate())
                                {
                                    ReturnInfo();
                                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                                    this.Close();
                                }
                            }
                            else if (HVO113.CONST_OPETP_STS.Equals(m_opeTp))
                            {
                                if (this.validations(this.pnlMain.Controls) && this.Validate())
                                {
                                    ReturnInfo();
                                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                                    this.Close();
                                }
                            }
                        }
                        else if (dr == DialogResult.No)
                        {
                            m_result = null;
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            this.Close();
                        }
                    }
                    else
                    {
                        m_result = null;
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                        this.Close();
                    }
                    break;

                case "btnInputDt":
                    HVO106001Parm hoseDtParm = new HVO106001Parm();
                    hoseDtParm.JpvcInfo = m_parm.JpvcInfo;
                    if (m_hoseDtResult != null)
                    {
                        hoseDtParm.PrevHoseOnDt = m_hoseDtResult.PrevHoseOnDt;
                        hoseDtParm.PrevHoseOffDt = m_hoseDtResult.PrevHoseOffDt;
                        hoseDtParm.PrevCommenceDt = m_hoseDtResult.PrevCommenceDt;
                        hoseDtParm.PrevCompleteDt = m_hoseDtResult.PrevCompleteDt;
                        hoseDtParm.HoseOnDt = m_hoseDtResult.HoseOnDt;
                        hoseDtParm.HoseOffDt = m_hoseDtResult.HoseOffDt;
                        hoseDtParm.CommenceDt = m_hoseDtResult.CommenceDt;
                        hoseDtParm.CompleteDt = m_hoseDtResult.CompleteDt;
                    }
                    if (m_workingStatus == Constants.MODE_UPDATE && m_parm.LiquidBulkItem != null)
                    {
                        VORLiquidBulkItem item = m_parm.LiquidBulkItem;
                        hoseDtParm.Seq = item.seq;
                        hoseDtParm.OrgHoseOnDt = m_orgHoseOnDt;
                        hoseDtParm.OrgHoseOffDt = m_orgHoseOffDt;
                        hoseDtParm.OrgCommenceDt = m_orgCommenceDt;
                        hoseDtParm.OrgCompleteDt = m_orgCompleteDt;
                    }

                    HVO106001Result hoseDtResult = (HVO106001Result)PopupManager.instance.ShowPopup(new HVO106001(m_opeTp, m_workingStatus), hoseDtParm);
                    if (hoseDtResult != null)
                    {
                        m_hoseDtResult = hoseDtResult;
                        this.IsDirty = true;

                        CalcPumpRate();
                    }
                    break;
            }
        }

        private void ComboboxListener(object sender, EventArgs e)
        {
            ComboBox mybutton = (ComboBox)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "cboTmnlOPR":
                case "cboOPRType":
                case "cboCargoType":
                    GetVORLiquidConfirmSlipInfo();
                    FetchHoseTimeFromPrevShift();
                    CalcPumpRate();
                    RefreshAmount();
                    GetShipperConsignee();
                    break;

                case "cboCmdt":
                    //FetchHoseTimeFromPrevShift();
                    GetShipperConsignee();
                    break;

                //case "cboLineType":
                //    FetchHoseTimeFromPrevShift();
                //    break;
            }
        }

        private bool ValidateDGConfirmedCmdt()
        {
            if (cboCmdt.SelectedIndex > 0)
            {
                string tmnlOpr = string.Empty;
                if (HVO113.CONST_OPETP_GENERAL.Equals(m_opeTp) || HVO113.CONST_OPETP_TRANSHIPMENT.Equals(m_opeTp))
                {
                    tmnlOpr = CommonUtility.GetComboboxSelectedValue(cboTmnlOPR);
                }
                string cmdtCd = CommonUtility.GetComboboxSelectedValue(cboCmdt);
                string oprTpCd = CommonUtility.GetComboboxSelectedValue(cboOPRType);
                string cgTpCd = CommonUtility.GetComboboxSelectedValue(cboCargoType);
                string[] cols = { m_parm.JpvcInfo.Jpvc, cmdtCd, oprTpCd, cgTpCd, tmnlOpr };

                bool result = CommonUtility.ValidateFunc("dgConfirmedCmdt", cols);
                if (!result)
                {
                    string strMsg = ResourceManager.getInstance().getString("HVO106_0002");
                    strMsg = string.Format(strMsg, cmdtCd);
                    DialogResult dr = CommonUtility.ConfirmMessage(strMsg);
                    if (dr == DialogResult.No)
                    {
                        cboCmdt.SelectedIndex = 0;
                        return false;
                    }
                }
            }

            return true;
        }

        private void FetchHoseTimeFromPrevShift()
        {
            try
            {
                m_hoseDtResult.PrevHoseOnDt = string.Empty;
                m_hoseDtResult.PrevHoseOffDt = string.Empty;
                m_hoseDtResult.PrevCommenceDt = string.Empty;
                m_hoseDtResult.PrevCompleteDt = string.Empty;

                if (cboOPRType.SelectedIndex > 0 &&
                    (HVO113.CONST_OPETP_STS.Equals(m_opeTp) ||
                     ((HVO113.CONST_OPETP_GENERAL.Equals(m_opeTp) || HVO113.CONST_OPETP_TRANSHIPMENT.Equals(m_opeTp)) &&
                      cboTmnlOPR.SelectedIndex > 0)) &&
                    cboCargoType.SelectedIndex > 0
                    //&&
                    //cboLineType.SelectedIndex > 0 &&
                    //cboCmdt.SelectedIndex > 0
                    )
                {
                    IVesselOperatorProxy proxy = new VesselOperatorProxy();
                    VORLiquidBulkParm parm = new VORLiquidBulkParm();
                    parm.searchType = "HHT_OprTimeSet";
                    parm.jpvcNo = m_parm.JpvcInfo.Jpvc;
                    parm.jobTpCd = CommonUtility.GetComboboxSelectedValue(cboOPRType);
                    parm.cgTpCd = CommonUtility.GetComboboxSelectedValue(cboCargoType);
                    //parm.hoseTpCd = CommonUtility.GetComboboxSelectedValue(cboLineType);
                    //parm.cmdtCd = CommonUtility.GetComboboxSelectedValue(cboCmdt);
                    if (HVO113.CONST_OPETP_GENERAL.Equals(m_opeTp) ||
                        HVO113.CONST_OPETP_TRANSHIPMENT.Equals(m_opeTp))
                    {
                        parm.tkOpr = CommonUtility.GetComboboxSelectedValue(cboTmnlOPR);
                    }
                    ResponseInfo info = proxy.getVORLiquidBulk(parm);

                    if (info != null && info.list != null && info.list.Length > 0 && info.list[0] is VORLiquidBulkItem)
                    {
                        VORLiquidBulkItem item = (VORLiquidBulkItem) info.list[0];
                        m_hoseDtResult.PrevHoseOnDt = item.hoseOnDt;
                        m_hoseDtResult.PrevHoseOffDt = item.hoseOffDt;
                        m_hoseDtResult.PrevCommenceDt = item.stDt;
                        m_hoseDtResult.PrevCompleteDt = item.endDt;
                    }


                    //=========================== Trim text - for temporary - will be removed ======================================
                    m_hoseDtResult.PrevHoseOnDt = string.IsNullOrEmpty(m_hoseDtResult.PrevHoseOnDt)
                                                      ? ""
                                                      : m_hoseDtResult.PrevHoseOnDt.Trim();
                    m_hoseDtResult.PrevHoseOffDt = string.IsNullOrEmpty(m_hoseDtResult.PrevHoseOffDt)
                                                       ? ""
                                                       : m_hoseDtResult.PrevHoseOffDt.Trim();
                    m_hoseDtResult.PrevCommenceDt = string.IsNullOrEmpty(m_hoseDtResult.PrevCommenceDt)
                                                        ? ""
                                                        : m_hoseDtResult.PrevCommenceDt.Trim();
                    m_hoseDtResult.PrevCompleteDt = string.IsNullOrEmpty(m_hoseDtResult.PrevCompleteDt)
                                                        ? ""
                                                        : m_hoseDtResult.PrevCompleteDt.Trim();
                    //==============================================================================================================
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private void GetVORLiquidConfirmSlipInfo()
        {
            try
            {
                par_pln_load = 0;
                par_pln_dis = 0;
                par_act_load = 0;
                par_act_dis = 0;
                m_handledLDAmt = 0;
                m_handledDSAmt = 0;

                double docLDAmt = 0;
                double docDSAmt = 0;
                double balLDAmt = 0;
                double balDSAmt = 0;
                if (m_parm != null && m_parm.JpvcInfo != null &&
                    cboOPRType.SelectedIndex > 0 &&
                    cboCargoType.SelectedIndex > 0 &&
                    (cboTmnlOPR.SelectedIndex > 0 || HVO113.CONST_OPETP_STS.Equals(m_opeTp)))
                {
                    string strCgTp = CommonUtility.GetComboboxSelectedValue(cboCargoType);
                    string strOprTp = CommonUtility.GetComboboxSelectedValue(cboOPRType);
                    string strTmnlOpr = CommonUtility.GetComboboxSelectedValue(cboTmnlOPR);

                    IVesselOperatorProxy proxy = new VesselOperatorProxy();
                    VORLiquidBulkParm parm = new VORLiquidBulkParm();
                    parm.jpvcNo = m_parm.JpvcInfo.Jpvc;
                    parm.workYmd = UserInfo.getInstance().Workdate;
                    parm.shift = UserInfo.getInstance().Shift;
                    parm.opeTp = m_opeTp;
                    parm.cgTpCd = strCgTp;
                    parm.jobTpCd = strOprTp;
                    parm.tkOpr = strTmnlOpr;
                    ResponseInfo info = proxy.getVORLiquidConfirmSlipInfo(parm);

                    if (info != null && info.list.Length > 0 && info.list[0] is VORLiquidBulkItem)
                    {
                        VORLiquidBulkItem returnItem = (VORLiquidBulkItem) info.list[0];

                        CommonUtility.SetComboboxSelectedItem(cboCmdt, returnItem.cmdtCd);
                        CommonUtility.SetComboboxSelectedItem(cboShprCnsne, returnItem.shprCnsne);
                        CommonUtility.SetComboboxSelectedItem(cboPkgTp, returnItem.pkgTpCd);

                        // Document amount
                        docLDAmt = CommonUtility.ParseDouble(returnItem.loadPlanMt);
                        docDSAmt = CommonUtility.ParseDouble(returnItem.disPlanMt);

                        // Handled amount: Calculate from grid data.
                        ArrayList gridData = m_parm.GridArrayList;
                        if (gridData != null && gridData.Count > 0)
                        {
                            for (int i = 0; i < gridData.Count; i++)
                            {
                                VORLiquidBulkItem item = (VORLiquidBulkItem) gridData[i];

                                // Check if grid item is not marked as DELETED
                                // and matches 3 keys: Operation Type, Cargo Type, Terminal Operator
                                if (!Constants.WS_DELETE.Equals(item.CRUD) &&
                                    (strOprTp.Equals(item.jobTpCd) && strTmnlOpr.Equals(item.tkOpr) &&
                                     strCgTp.Equals(item.cgTpCd)))
                                {
                                    if (HVO113.CONST_CGOPETP_LOADING_GEN.Equals(strOprTp) ||
                                        HVO113.CONST_CGOPETP_LOADING_STS.Equals(strOprTp) ||
                                        HVO113.CONST_CGOPETP_LOADING_TLS.Equals(strOprTp))
                                    {
                                        m_handledLDAmt += CommonUtility.ParseDouble(item.tonHdlAmt);
                                    }
                                    else if (HVO113.CONST_CGOPETP_DISCHARGING_GEN.Equals(strOprTp) ||
                                             HVO113.CONST_CGOPETP_DISCHARGING_STS.Equals(strOprTp) ||
                                             HVO113.CONST_CGOPETP_DISCHARGING_TLS.Equals(strOprTp))
                                    {
                                        m_handledDSAmt += CommonUtility.ParseDouble(item.tonHdlAmt);
                                    }
                                }
                            }
                        }

                        // In UPDATE mode, deduct handled amount by itselft tonnage amount
                        if (m_workingStatus == Constants.MODE_UPDATE && m_parm.LiquidBulkItem != null)
                        {
                            VORLiquidBulkItem item = m_parm.LiquidBulkItem;
                            string jobTpCd = item.jobTpCd;
                            if (HVO113.CONST_CGOPETP_LOADING_GEN.Equals(jobTpCd) ||
                                HVO113.CONST_CGOPETP_LOADING_STS.Equals(jobTpCd) ||
                                HVO113.CONST_CGOPETP_LOADING_TLS.Equals(jobTpCd))
                            {
                                m_handledLDAmt -= CommonUtility.ParseDouble(item.tonHdlAmt);
                            }
                            else if (HVO113.CONST_CGOPETP_DISCHARGING_GEN.Equals(jobTpCd) ||
                                     HVO113.CONST_CGOPETP_DISCHARGING_STS.Equals(jobTpCd) ||
                                     HVO113.CONST_CGOPETP_DISCHARGING_TLS.Equals(jobTpCd))
                            {
                                m_handledDSAmt -= CommonUtility.ParseDouble(item.tonHdlAmt);
                            }
                        }


                        //////////////////////////////////////////////////////////////////////////
                        // Handled amount: plus previous handled amount that is not in datagrid
                        par_pln_load = CommonUtility.ParseDouble(returnItem.loadPlanMt);
                        par_pln_dis = CommonUtility.ParseDouble(returnItem.disPlanMt);

                        par_act_load = CommonUtility.ParseDouble(returnItem.loadTotMt) -
                                       CommonUtility.ParseDouble(returnItem.loadActualMt);
                        par_act_dis = CommonUtility.ParseDouble(returnItem.disTotMt) -
                                      CommonUtility.ParseDouble(returnItem.disActualMt);

                        m_handledLDAmt += par_act_load;
                        m_handledDSAmt += par_act_dis;
                        //////////////////////////////////////////////////////////////////////////


                        // Balance amount
                        balLDAmt = docLDAmt - m_handledLDAmt;
                        balDSAmt = docDSAmt - m_handledDSAmt;
                    }
                }
                txtLDDoc.Text = CommonUtility.ToString(docLDAmt);
                txtDSDoc.Text = CommonUtility.ToString(docDSAmt);
                txtLDHandled.Text = CommonUtility.ToString(m_handledLDAmt);
                txtDSHandled.Text = CommonUtility.ToString(m_handledDSAmt);
                txtLDBal.Text = CommonUtility.ToString(balLDAmt);
                txtDSBal.Text = CommonUtility.ToString(balDSAmt);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private void GetShipperConsignee()
        {
            try
            {
                if (m_parm != null && m_parm.JpvcInfo != null &&
                    cboOPRType.SelectedIndex > 0 &&
                    cboCargoType.SelectedIndex > 0 &&
                    (cboTmnlOPR.SelectedIndex > 0 || HVO113.CONST_OPETP_STS.Equals(m_opeTp))
                    )
                {
                    string strCgTp = CommonUtility.GetComboboxSelectedValue(cboCargoType);
                    string strOprTp = CommonUtility.GetComboboxSelectedValue(cboOPRType);
                    string strTmnlOpr = CommonUtility.GetComboboxSelectedValue(cboTmnlOPR);
                    string strCmdt = CommonUtility.GetComboboxSelectedValue(cboCmdt);

                    IVesselOperatorProxy proxy = new VesselOperatorProxy();
                    VORLiquidBulkParm parm = new VORLiquidBulkParm();
                    parm.jpvcNo = m_parm.JpvcInfo.Jpvc;
                    parm.cgTpCd = strCgTp;
                    parm.jobTpCd = strOprTp;
                    parm.tkOpr = strTmnlOpr;
                    parm.cmdtCd = strCmdt;
                    parm.searchType = "HHT_ShprCnsne";
                    ResponseInfo info = proxy.getVORLiquidBulk(parm);

                    CommonUtility.InitializeCombobox(cboShprCnsne, "Select");
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is VORLiquidBulkItem)
                        {
                            VORLiquidBulkItem item = (VORLiquidBulkItem) info.list[i];
                            cboShprCnsne.Items.Add(new ComboboxValueDescriptionPair(item.shprCnsne, item.shprCnsne));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private bool Validate()
        {
            // Check hose on
            if (!ValidateHoseOn())
            {
                return false;
            }

            // Check if DG was already confirmed or not
            if (!ValidateDGConfirmedCmdt())
            {
                return false;
            }

            // Check if the operation is completed.
            if (!chkCompleted.Checked)
            {
                DialogResult dr;
                double balAmnt = 0;
                string strOprTp = CommonUtility.GetComboboxSelectedValue(cboOPRType);
                if (HVO113.CONST_CGOPETP_LOADING_GEN.Equals(strOprTp) || 
                    HVO113.CONST_CGOPETP_LOADING_STS.Equals(strOprTp) || 
                    HVO113.CONST_CGOPETP_LOADING_TLS.Equals(strOprTp))
                {
                    balAmnt = CommonUtility.ParseDouble(txtLDBal.Text);
                }
                else if (HVO113.CONST_CGOPETP_DISCHARGING_GEN.Equals(strOprTp) || 
                    HVO113.CONST_CGOPETP_DISCHARGING_STS.Equals(strOprTp) || 
                    HVO113.CONST_CGOPETP_DISCHARGING_TLS.Equals(strOprTp))
                {
                    balAmnt = CommonUtility.ParseDouble(txtDSBal.Text);
                }
                if (balAmnt <= 0)
                {
                    // Confirm message [Is this operation commpeted ?]
                    dr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HVO106_0003"));
                    if (dr == DialogResult.Yes)
                    {
                        chkCompleted.Checked = true;
                        chkCompleted.Focus();
                    }
                    else if (dr == DialogResult.No)
                    {
                        chkCompleted.Checked = false;
                        chkCompleted.Focus();
                    }
                }

                // Confirm message [You've inputted hose off. Is this operation completed ?]
                if ((m_hoseDtResult != null &&
                    !string.IsNullOrEmpty(m_hoseDtResult.HoseOffDt)))
                {
                    dr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HVO106_0007"));
                    if (dr == DialogResult.Yes)
                    {
                        chkCompleted.Checked = true;
                        chkCompleted.Focus();
                    }
                    else if (dr == DialogResult.No)
                    {
                        chkCompleted.Checked = false;
                        chkCompleted.Focus();
                    }
                }
            }

            // Input Hose On/Off Time when the operation is completed.
            if (chkCompleted.Checked)
            {
                if ((m_hoseDtResult == null ||
                    string.IsNullOrEmpty(m_hoseDtResult.HoseOffDt)))
                {
                    CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HVO106_0001"));
                    return false;
                }
            }

            return true;
        }

        private bool ValidateHoseOn()
        {
            if (m_hoseDtResult != null &&
                ((m_workingStatus == Constants.MODE_UPDATE && 
                string.IsNullOrEmpty(m_hoseDtResult.HoseOnDt) && 
                !string.IsNullOrEmpty(m_orgHoseOnDt)) ||
                (m_workingStatus == Constants.MODE_ADD && 
                string.IsNullOrEmpty(m_hoseDtResult.HoseOnDt) && 
                string.IsNullOrEmpty(m_hoseDtResult.PrevHoseOnDt))))
            {
                // Alert message [Please input Hose On time]
                CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HVO106_0004"));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Calculate pump rate.
        /// Pump rate = tonnage amount / time.
        /// Time is a difference of Commence time and Complete time 
        /// (or shift start time and shift end time in case of omitting of Commence/Complete time),
        /// excluding delay time within operation time.
        /// </summary>
        private void CalcPumpRate()
        {
            double pumpRate = 0;
            double tonAmnt = CommonUtility.ParseDouble(txtTonnage.Text);
            double countTime = 0;
            DateTime startTime = DateTime.MinValue;
            DateTime endTime = DateTime.MinValue;
            DateTime shiftStartTime = CommonUtility.GetShiftStartTime();
            DateTime shiftEndTime = CommonUtility.GetShiftEndTime();

            if (m_hoseDtResult != null)
            {
                if (!string.IsNullOrEmpty(m_hoseDtResult.CommenceDt))
                {
                    DateTime commenceDt = CommonUtility.ParseYMDHM(m_hoseDtResult.CommenceDt);
                    if (commenceDt.CompareTo(shiftStartTime) > 0 &&
                        commenceDt.CompareTo(shiftEndTime) < 0)
                    {
                        startTime = commenceDt;
                    }
                    else
                    {
                        startTime = shiftStartTime;
                    }
                }
                else
                {
                    startTime = shiftStartTime;
                }

                if (!string.IsNullOrEmpty(m_hoseDtResult.CompleteDt))
                {
                    DateTime completeDt = CommonUtility.ParseYMDHM(m_hoseDtResult.CompleteDt);
                    if (completeDt.CompareTo(shiftStartTime) > 0 &&
                        completeDt.CompareTo(shiftEndTime) < 0)
                    {
                        endTime = completeDt;
                    }
                    else
                    {
                        endTime = shiftEndTime;
                    }
                }
                else
                {
                    endTime = shiftEndTime;
                }
            }
            else
            {
                startTime = shiftStartTime;
                endTime = shiftEndTime;
            }

            if (endTime.CompareTo(startTime) > 0)
            {
                TimeSpan timespan = endTime.Subtract(startTime);
                countTime = timespan.TotalHours;

                GetDelayTimeWithinOprTime(startTime, endTime);
                if (CONST_CGTP_LIQUID_EDIBLE.Equals(CommonUtility.GetComboboxSelectedValue(cboCargoType)))
                {
                    countTime -= m_totHourE;
                }
                else if (CONST_CGTP_LIQUID_NON_EDIBLE.Equals(CommonUtility.GetComboboxSelectedValue(cboCargoType)))
                {
                    countTime -= m_totHourN;
                }
            }

		    if (countTime != 0) {
		        pumpRate = tonAmnt / countTime;
			}
            txtPumping.Text = CommonUtility.ToString(Math.Round(pumpRate, 3));
        }

        private void RefreshAmount()
        {
            bool isDS = false;
            string strOprTp = CommonUtility.GetComboboxSelectedValue(cboOPRType);
            if (HVO113.CONST_CGOPETP_DISCHARGING_GEN.Equals(strOprTp) || 
                HVO113.CONST_CGOPETP_DISCHARGING_STS.Equals(strOprTp) || 
                HVO113.CONST_CGOPETP_DISCHARGING_TLS.Equals(strOprTp))
            {
                isDS = true;
            }

            // Update values on text changed
            double curTonAmnt = CommonUtility.ParseDouble(txtTonnage.Text);
            double curBalAmnt;
            double curHandledAmnt;

            if (isDS)
            {
                curHandledAmnt = m_handledDSAmt + curTonAmnt;
                curBalAmnt = CommonUtility.ParseDouble(txtDSDoc.Text) - curHandledAmnt;
                
                txtDSHandled.Text = curHandledAmnt.ToString();
                txtDSBal.Text = curBalAmnt.ToString();
            }
            else
            {
                curHandledAmnt = m_handledLDAmt + curTonAmnt;
                curBalAmnt = CommonUtility.ParseDouble(txtLDDoc.Text) - curHandledAmnt;
                
                txtLDHandled.Text = curHandledAmnt.ToString();
                txtLDBal.Text = curBalAmnt.ToString();
            }
        }

        // Calculate delay hours within operation time
        private double CalcDelayHrsWithinOprTime(DateTime startTime, DateTime endTime, 
                                                DateTime delayStTime, DateTime delayEndTime)
        {
            double retValue = 0;
            DateTime start = startTime.CompareTo(delayStTime) > 0 ? startTime : delayStTime;
            DateTime end = endTime.CompareTo(delayEndTime) < 0 ? endTime : delayEndTime;

            if (endTime.CompareTo(startTime) > 0)
            {
                TimeSpan timespan = end.Subtract(start);
                retValue = timespan.TotalHours;
            }
            return retValue;
        }

        // Get delay hours within operation time
        private void GetDelayTimeWithinOprTime(DateTime startTime, DateTime endTime)
        {
            m_totHourE = 0;
            m_totHourN = 0;

            if (m_dlyInfo != null && m_dlyInfo.list != null) {
                foreach (VORLiquidBulkItem item in m_dlyInfo.list)
                {
                    DateTime dlyStTime = CommonUtility.ParseYMDHM(item.startTime);
                    DateTime dlyEndTime = CommonUtility.ParseYMDHM(item.endTime);

                    if (!"Y".Equals(item.accDelay))
                    {
                        if (CONST_CGTP_LIQUID_EDIBLE.Equals(item.cgTpCd))
                        {
                            m_totHourE += CalcDelayHrsWithinOprTime(startTime, endTime, dlyStTime, dlyEndTime);
                        }
                        else if (CONST_CGTP_LIQUID_NON_EDIBLE.Equals(item.cgTpCd))
                        {
                            m_totHourN += CalcDelayHrsWithinOprTime(startTime, endTime, dlyStTime, dlyEndTime);
                        }
                    }
                }
            }
        }

        private void txtTonnage_LostFocus(object sender, EventArgs e)
        {
            CalcPumpRate();
            RefreshAmount();
        }
    }
}