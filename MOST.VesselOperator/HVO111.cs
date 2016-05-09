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
    public partial class HVO111 : TForm, IPopupWindow
    {
        // TMT_VSL_OPE_RPT_DTL.CW_DIV = {'Y', 'N'}
        //private const string CONST_SHIPCR_NM = "Ship's Crew";

        /*
            ITEMCD	FSG	    Fail to Supply Gang
            ITEMCD	FSGE	Fail to Supply Gears
            ITEMCD	FSH	    Fail to supply Hopperman/Hatchman
            ITEMCD	FSSI	Fail to supply Signalman
            ITEMCD	FSSU	Fail to supply Supervisor
            ITEMCD	FSM	    Fail to supply machine
            ITEMCD	NAP	    Not Achieving Productivity
            ITEMCD	SWL	    Start Work Late
            ITEMCD	SWE	    Stop Work Early
         */
        private const string CONST_FAIL_SUPPLY_GANG         = "FSG";
        private const string CONST_FAIL_SUPPLY_GEARS        = "FSGE";
        private const string CONST_FAIL_SUPPLY_HOPPER       = "FSH";
        private const string CONST_FAIL_SUPPLY_SIGNAL       = "FSSI";
        private const string CONST_FAIL_SUPPLY_SUPERVISOR   = "FSSU";
        private const string CONST_FAIL_SUPPLY_MACHINE      = "FSM";
        private const string CONST_NOT_PRODUCTIVITY         = "NAP";
        private const string CONST_START_WORK_LATE          = "SWL";
        private const string CONST_STOP_WORK_EARLY          = "SWE";

        private const string HEADER_ITEM_STATUS = "_ITEM_STATUS";
        private const string HEADER_WS_NM = "Status";
        private const string HEADER_HATCH = "Hatch";
        private const string HEADER_STARTTIME = "Start Time";
        private const string HEADER_ENDTIME = "End Time";
        private const string HEADER_DATE = "Date";
        private const string HEADER_CONTRACTOR = "Contractor";
        private const string HEADER_PARTICULAR = "Particulars";
        private const string HEADER_PARTICULAR_CODE = "_Particulars_Code";
        private const string HEADER_ROLE = "Role";
        private const string HEADER_ROLE_CODE = "_Role_Code";
        private const string HEADER_PENALTY = "Penalty";
        private const string HEADER_UNITPRC = "_UnitPrc";
        private const string HEADER_RATE = "Rate";
        private const string HEADER_TOTAL = "Total(RM)";
        private const string HEADER_DPR_NO = "_DPR_NO";

        private HVO111Parm m_parm;

        public HVO111()
        {
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            cboRole.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HVO111Parm)parm;
            InitializeData();
            this.ShowDialog();
            return null;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_ITEM_STATUS, "0" }, { HEADER_WS_NM, "40" }, { HEADER_HATCH, "30" }, { HEADER_DATE, "75" }, { HEADER_CONTRACTOR, "40" }, { HEADER_PARTICULAR, "120" }, { HEADER_PARTICULAR_CODE, "0" }, { HEADER_ROLE, "120" }, { HEADER_STARTTIME, "90" }, { HEADER_ENDTIME, "90" }, { HEADER_ROLE_CODE, "0" }, { HEADER_PENALTY, "120" }, { HEADER_UNITPRC, "0" }, { HEADER_RATE, "40" }, { HEADER_TOTAL, "50" }, { HEADER_DPR_NO, "0" } };
            this.grdData.setHeader(header);
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Date, Time
                txtDate.CustomFormat = Framework.Controls.UserControls.TDateTimePicker.FORMAT_DDMMYYYY;
                CommonUtility.SetDTPValueDMY(txtDate, UserInfo.getInstance().Workdate);
                CommonUtility.SetDTPValueBlank(txtStartTime);
                CommonUtility.SetDTPValueBlank(txtEndTime);
                #endregion

                #region Hatch
                // Get inputted hatches from Stevedore/Trimming screen.
                GetInputtedHatches();

                // Set Hatch, Contractor within selected item from Stevedore/Trimming screen.
                FetchDataFromPrevScreen();
                #endregion

                #region Particulars

                CommonProxy commonProxy = new CommonProxy();
               
                CommonCodeParm commonParm = new CommonCodeParm();
                commonParm.searchType = "COMM";
                commonParm.lcd = "MT";
                commonParm.divCd = "ITEMCD";
                ResponseInfo commonInfo = commonProxy.getCommonCodeList(commonParm);

                //CommonUtility.initializeCombobox(cboParticulars);
                cboParticulars.Items.Clear();
                cboParticulars.Items.Add(new ComboboxValueDescriptionPair("", ""));

                for (int i = 0; i < commonInfo.list.Length; i++)
                {
                    if (commonInfo.list[i] is CodeMasterListItem)
                    {
                        CodeMasterListItem item = (CodeMasterListItem)commonInfo.list[i];
                        cboParticulars.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                    else if (commonInfo.list[i] is CodeMasterListItem1)
                    {
                        CodeMasterListItem1 item = (CodeMasterListItem1)commonInfo.list[i];
                        cboParticulars.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                }
                #endregion

                #region Role
                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                DelayPenaltyReportParm parm = new DelayPenaltyReportParm();
                parm.searchType = "HHT_Role_comboList";
                ResponseInfo info = proxy.getDelayPenaltyReportList(parm);
                CommonUtility.InitializeCombobox(cboRole);
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CodeMasterListItem)
                    {
                        CodeMasterListItem item = (CodeMasterListItem)info.list[i];
                        cboRole.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                    else if (info.list[i] is CodeMasterListItem1)
                    {
                        CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                        cboRole.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                }
                #endregion

                #region Grid Data
                GetDelayPenaltyReportList();
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

        /// <summary>
        /// Get inputted hatches, excluding hatches that were handled by Ship's Crew
        /// </summary>
        private void GetInputtedHatches()
        {
            if (m_parm != null && m_parm.ArrGridData != null)
            {
                CommonUtility.InitializeCombobox(cboHatch);

                ArrayList grdTmp = m_parm.ArrGridData;
                for (int i = 0; i < grdTmp.Count ; i++)
                {
                    VORDryBreakBulkItem item = (VORDryBreakBulkItem)grdTmp[i];
                    if (!HVO110.CONST_SHIPCR_NM.Equals(item.workComp))
                    {
                        cboHatch.Items.Add(new ComboboxValueDescriptionPair(item.hatchNo, item.hatchNo));
                    }
                }
            }
        }

        /// <summary>
        /// Set Hatch, Contractor within selected item from Stevedore/Trimming screen.
        /// </summary>
        private void FetchDataFromPrevScreen()
        {
            if (m_parm != null && m_parm.ArrGridData != null &&
                0 <= m_parm.CurrIndex && m_parm.CurrIndex < m_parm.ArrGridData.Count)
            {
                VORDryBreakBulkItem item = (VORDryBreakBulkItem)m_parm.ArrGridData[m_parm.CurrIndex];
                CommonUtility.SetComboboxSelectedItem(cboHatch, item.hatchNo);
                CommonUtility.SetComboboxSelectedItem(cboContractor, item.workComp);
            }
        }

        private void GetPenaltyDescrList()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // ref: CT103
                if (cboParticulars.SelectedIndex > 0)
                {
                    string particular = CommonUtility.GetComboboxSelectedValue(cboParticulars);

                    IVesselOperatorProxy proxy = new VesselOperatorProxy();
                    Framework.Service.Provider.WebService.Provider.DelayPenaltyReportParm parm = new Framework.Service.Provider.WebService.Provider.DelayPenaltyReportParm();
                    parm.searchType = "penaltyDescr";
                    parm.itemCd = particular;

                    if (CONST_FAIL_SUPPLY_GANG.Equals(particular) ||
                        CONST_FAIL_SUPPLY_GEARS.Equals(particular) ||
                        CONST_FAIL_SUPPLY_HOPPER.Equals(particular) ||
                        CONST_FAIL_SUPPLY_SIGNAL.Equals(particular) ||
                        CONST_FAIL_SUPPLY_SUPERVISOR.Equals(particular) ||
                        CONST_FAIL_SUPPLY_MACHINE.Equals(particular) ||
                        CONST_NOT_PRODUCTIVITY.Equals(particular) )
                    {
                        parm.roleCd = "*";
                    }
                    else if (CONST_START_WORK_LATE.Equals(particular) ||
                            CONST_STOP_WORK_EARLY.Equals(particular))
                    {
                        parm.roleCd = CommonUtility.GetComboboxSelectedValue(cboRole);
                    }

                    ResponseInfo info = proxy.getDelayPenaltyReportList(parm);

                    // Display Data
                    if (info != null && info.list.Length > 0 && info.list[0] is DelayPenaltyReportItem)
                    {
                        DelayPenaltyReportItem item = (DelayPenaltyReportItem)info.list[0];
                        txtPntyDescr.Text = item.pntyDescr;
                        txtUnitPrc.Text = item.unitPrc.ToString();
                    }
                    else
                    {
                        txtPntyDescr.Text = "";
                        txtUnitPrc.Text = "";
                    }
                }
                else
                {
                    txtPntyDescr.Text = "";
                    txtUnitPrc.Text = "";
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

        private void GetDelayPenaltyReportList()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                IVesselOperatorProxy proxy = new VesselOperatorProxy();

                // ref: CT103
                DelayPenaltyReportParm parm = new DelayPenaltyReportParm();
                parm.searchType = "HHT_reportList";
                parm.vslCallID = m_parm.JpvcInfo.Jpvc;
                ResponseInfo info = proxy.getDelayPenaltyReportList(parm);

                grdData.Clear();
                if (info != null)
                {
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is DelayPenaltyReportItem)
                        {
                            DelayPenaltyReportItem item = (DelayPenaltyReportItem)info.list[i];
                            DataRow newRow = grdData.NewRow();
                            newRow[HEADER_ITEM_STATUS] = Constants.ITEM_OLD;
                            newRow[HEADER_WS_NM] = Constants.WS_NM_INITIAL;
                            newRow[HEADER_HATCH] = item.hatchNo;
                            newRow[HEADER_DATE] = item.pntyDt;
                            newRow[HEADER_CONTRACTOR] = item.contrator;
                            newRow[HEADER_PARTICULAR] = item.itemCdNm;
                            newRow[HEADER_PARTICULAR_CODE] = item.itemCd;
                            newRow[HEADER_ROLE] = item.roleCdNm;
                            newRow[HEADER_ROLE_CODE] = item.roleCd;
                            newRow[HEADER_STARTTIME] = item.pntyTime;
                            newRow[HEADER_ENDTIME] = item.pntyEndTime;
                            newRow[HEADER_PENALTY] = item.pntyDescr;
                            newRow[HEADER_UNITPRC] = item.unitPrc;
                            newRow[HEADER_RATE] = item.itemQty;
                            newRow[HEADER_TOTAL] = item.pntyAmt;
                            newRow[HEADER_DPR_NO] = item.dlyPntyRptNo;

                            grdData.Add(newRow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                grdData.IsDirty = false;
                Cursor.Current = Cursors.Default;
            }
        }

        private bool ProcessDelayPenaltyReportItem()
        {
            bool result = false;
            try
            {
                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                System.Collections.ArrayList arrObj = new System.Collections.ArrayList();

                if (grdData != null)
                {
                    for (int i = 0; i < grdData.DataTable.Rows.Count; i++)
                    {
                        // Process only items that are modified.
                        string crud = CommonUtility.GetCRUDFromName(grdData.DataTable.Rows[i][HEADER_WS_NM].ToString());
                        if (!Constants.WS_INITIAL.Equals(crud))
                        {
                            // ref: CT103
                            DelayPenaltyReportItem item = new DelayPenaltyReportItem();
                            item.vslCallID = m_parm.JpvcInfo.Jpvc;
                            item.hatchNo = grdData.DataTable.Rows[i][HEADER_HATCH].ToString();
                            item.pntyDt = grdData.DataTable.Rows[i][HEADER_DATE].ToString();
                            item.itemCd = grdData.DataTable.Rows[i][HEADER_PARTICULAR_CODE].ToString();
                            item.roleCd = grdData.DataTable.Rows[i][HEADER_ROLE_CODE].ToString();
                            item.pntyTime = grdData.DataTable.Rows[i][HEADER_STARTTIME].ToString();
                            item.pntyEndTime = grdData.DataTable.Rows[i][HEADER_ENDTIME].ToString();
                            item.contrator = grdData.DataTable.Rows[i][HEADER_CONTRACTOR].ToString();
                            item.unitPrc = CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_UNITPRC].ToString());
                            item.itemQty = CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_RATE].ToString());
                            item.pntyDescr = grdData.DataTable.Rows[i][HEADER_PENALTY].ToString();
                            item.pntyAmt = CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_TOTAL].ToString());
                            item.shftId = UserInfo.getInstance().Shift;
                            item.userId = UserInfo.getInstance().UserId;
                            item.CRUD = crud;

                            item.dlyPntyRptNo = grdData.DataTable.Rows[i][HEADER_DPR_NO].ToString();

                            arrObj.Add(item);
                        }
                    }
                }

                if (arrObj.Count > 0)
                {
                    Object[] obj = arrObj.ToArray();
                    DataItemCollection dataCollection = new DataItemCollection();
                    dataCollection.collection = obj;

                    proxy.processDelayPenaltyReportItem(dataCollection);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            return result;
        }

        private void AddItem()
        {
            DataRow newRow = grdData.NewRow();
            newRow[HEADER_ITEM_STATUS] = Constants.ITEM_NEW;
            newRow[HEADER_WS_NM] = Constants.WS_NM_INSERT;
            newRow[HEADER_PARTICULAR] = CommonUtility.GetComboboxSelectedDescription(cboParticulars);
            newRow[HEADER_PARTICULAR_CODE] = CommonUtility.GetComboboxSelectedValue(cboParticulars);
            newRow[HEADER_ROLE] = CommonUtility.GetComboboxSelectedDescription(cboRole);
            newRow[HEADER_ROLE_CODE] = CommonUtility.GetComboboxSelectedValue(cboRole);
            newRow[HEADER_STARTTIME] = txtStartTime.Text;
            newRow[HEADER_ENDTIME] = txtEndTime.Text;
            newRow[HEADER_PENALTY] = txtPntyDescr.Text;
            newRow[HEADER_UNITPRC] = txtUnitPrc.Text;
            newRow[HEADER_RATE] = txtRate.Text;
            newRow[HEADER_TOTAL] = txtTotal.Text;
            newRow[HEADER_HATCH] = CommonUtility.GetComboboxSelectedValue(cboHatch);
            newRow[HEADER_DATE] = txtDate.Text;
            newRow[HEADER_CONTRACTOR] = CommonUtility.GetComboboxSelectedValue(cboContractor);
            grdData.Add(newRow);
            grdData.Refresh();
        }

        private void UpdateItem()
        {
            if (grdData.CurrentRowIndex > -1 && grdData.CurrentRowIndex < grdData.DataTable.Rows.Count)
            {
                // Set Readonly False to update column data.
                DataTable dataTable = grdData.DataTable;
                int colCnt = dataTable.Columns.Count;
                for (int i = 0; i < colCnt; i++)
                {
                    dataTable.Columns[i].ReadOnly = false;
                }

                // Update columns data
                DataRow row = grdData.DataTable.Rows[grdData.CurrentRowIndex];
                if (Constants.ITEM_OLD.Equals(grdData.DataTable.Rows[grdData.CurrentRowIndex][HEADER_ITEM_STATUS].ToString()))
                {
                    row[HEADER_WS_NM] = Constants.WS_NM_UPDATE;
                }
                row[HEADER_PARTICULAR] = CommonUtility.GetComboboxSelectedDescription(cboParticulars);
                row[HEADER_PARTICULAR_CODE] = CommonUtility.GetComboboxSelectedValue(cboParticulars);
                row[HEADER_ROLE] = CommonUtility.GetComboboxSelectedDescription(cboRole);
                row[HEADER_ROLE_CODE] = CommonUtility.GetComboboxSelectedValue(cboRole);
                row[HEADER_STARTTIME] = txtStartTime.Text;
                row[HEADER_ENDTIME] = txtEndTime.Text;
                row[HEADER_PENALTY] = txtPntyDescr.Text;
                row[HEADER_UNITPRC] = txtUnitPrc.Text;
                row[HEADER_RATE] = txtRate.Text;
                row[HEADER_TOTAL] = txtTotal.Text;
                row[HEADER_HATCH] = CommonUtility.GetComboboxSelectedValue(cboHatch);
                row[HEADER_DATE] = txtDate.Text;
                row[HEADER_CONTRACTOR] = CommonUtility.GetComboboxSelectedValue(cboContractor);
                grdData.DataTable.AcceptChanges();
                grdData.Refresh();

                // Reset original Readonly
                for (int i = 0; i < colCnt; i++)
                {
                    dataTable.Columns[i].ReadOnly = true;
                }
            }
        }

        private void DeleteItem()
        {
            if (grdData.CurrentRowIndex > -1 && grdData.CurrentRowIndex < grdData.DataTable.Rows.Count)
            {
                // In case item status is NEW: remove this row from grid.
                // In case item status is OLD: change WORKING STATUS of this row.
                string itemStatus = grdData.DataTable.Rows[grdData.CurrentRowIndex][HEADER_ITEM_STATUS].ToString();
                if (Constants.ITEM_NEW.Equals(itemStatus))
                {
                    grdData.DataTable.AcceptChanges();
                    grdData.DataTable.Rows.RemoveAt(grdData.CurrentRowIndex);
                    grdData.Refresh();
                }
                else if (Constants.ITEM_OLD.Equals(itemStatus))
                {
                    // Set Readonly False to update column data.
                    DataTable dataTable = grdData.DataTable;
                    int colCnt = dataTable.Columns.Count;
                    for (int i = 0; i < colCnt; i++)
                    {
                        dataTable.Columns[i].ReadOnly = false;
                    }

                    // Delete row
                    DataRow row = grdData.DataTable.Rows[grdData.CurrentRowIndex];
                    row[HEADER_WS_NM] = Constants.WS_NM_DELETE;
                    grdData.DataTable.AcceptChanges();
                    grdData.Refresh();

                    // Reset original Readonly
                    for (int i = 0; i < colCnt; i++)
                    {
                        dataTable.Columns[i].ReadOnly = true;
                    }
                }
            }
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnOk":
                    if (grdData.IsDirty)
                    {
                        if (ProcessDelayPenaltyReportItem())
                        {
                            ClearForm();
                            GetDelayPenaltyReportList();
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
                        }
                    }
                    else
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                    }
                    break;

                case "btnCancel":
                    if (grdData.IsDirty)
                    {
                        DialogResult dr = CommonUtility.ConfirmMsgSaveChances();
                        if (dr == DialogResult.Yes)
                        {
                            ProcessDelayPenaltyReportItem();
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            this.Close();
                        }
                        else if (dr == DialogResult.No)
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            this.Close();
                        }
                    }
                    else
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                        this.Close();
                    }
                    break;

                case "btnAdd":
                    if (this.validations(this.Controls) && Validate())
                    {
                        if (!IsExistAlready(Constants.MODE_ADD))
                        {
                            AddItem();
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
                            cboHatch.Focus();
                        }
                    }
                    break;

                case "btnUpdate":
                    if (this.validations(this.Controls) && Validate())
                    {
                        if (!IsExistAlready(Constants.MODE_UPDATE))
                        {
                            UpdateItem();
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
                            cboHatch.Focus();
                        }
                    }
                    break;

                case "btnDelete":
                    if (DialogResult.Yes == CommonUtility.ConfirmMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0046")))
                    {
                        DeleteItem();
                    }
                    break;
            }
        }

        private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CONST_START_WORK_LATE.Equals(CommonUtility.GetComboboxSelectedValue(cboParticulars)) ||
                CONST_STOP_WORK_EARLY.Equals(CommonUtility.GetComboboxSelectedValue(cboParticulars)))
            {
                cboRole.Enabled = true;
                cboRole.isMandatory = true;

                GetPenaltyDescrList();
            }
            txtRate_LostFocus(sender, e);
        }

        private void txtRate_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRate.Text) || string.IsNullOrEmpty(txtUnitPrc.Text))
            {
                txtTotal.Text = "0.0";
            }
            else
            {
                double total = CommonUtility.ParseDouble(txtUnitPrc.Text) * CommonUtility.ParseDouble(txtRate.Text);
                txtTotal.Text = String.Format("{0:0.00}", total);
            }
        }

        private void cboParticulars_SelectedIndexChanged(object sender, EventArgs e)
        {
            string particular = CommonUtility.GetComboboxSelectedValue(cboParticulars);

            if (CONST_FAIL_SUPPLY_GANG.Equals(particular) ||
                CONST_FAIL_SUPPLY_GEARS.Equals(particular) ||
                CONST_FAIL_SUPPLY_HOPPER.Equals(particular) ||
                CONST_FAIL_SUPPLY_SIGNAL.Equals(particular) ||
                CONST_FAIL_SUPPLY_SUPERVISOR.Equals(particular) ||
                CONST_FAIL_SUPPLY_MACHINE.Equals(particular) ||
                CONST_NOT_PRODUCTIVITY.Equals(particular) )
            {   
                cboRole.Enabled = false;
                cboRole.isMandatory = false;
                txtStartTime.Enabled = false;
                txtEndTime.Enabled = false;
                CommonUtility.SetDTPValueBlank(txtStartTime);
                CommonUtility.SetDTPValueBlank(txtEndTime);

                GetPenaltyDescrList();
            }
            else if (CONST_START_WORK_LATE.Equals(particular) ||
                CONST_STOP_WORK_EARLY.Equals(particular))
            {
                txtStartTime.Enabled = true;
                txtEndTime.Enabled = true;
                CommonUtility.SetDTPWithinShift(txtStartTime, txtEndTime);
                cboRole.Enabled = true;
                cboRole.isMandatory = true;

                if (cboRole.SelectedIndex > 0)
                {
                    GetPenaltyDescrList();
                }
                else
                {
                    txtPntyDescr.Text = string.Empty;
                    txtUnitPrc.Text = string.Empty;
                }
            }
            else
            {
                cboRole.SelectedIndex = 0;
                cboRole.Enabled = false;
                cboRole.isMandatory = false;

                txtPntyDescr.Text = string.Empty;
                txtUnitPrc.Text = string.Empty;

                txtStartTime.Enabled = false;
                txtEndTime.Enabled = false;
                CommonUtility.SetDTPValueBlank(txtStartTime);
                CommonUtility.SetDTPValueBlank(txtEndTime);
            }

            txtRate_LostFocus(sender, e);
        }

        private void grdData_CurrentCellChanged(object sender, EventArgs e)
        {
            int index = grdData.CurrentRowIndex;
            if (index > -1 && index < grdData.DataTable.Rows.Count)
            {
                string strHatch = grdData.DataTable.Rows[index][HEADER_HATCH].ToString();
                string strDate = grdData.DataTable.Rows[index][HEADER_DATE].ToString();
                string strContractor = grdData.DataTable.Rows[index][HEADER_CONTRACTOR].ToString();
                string strRoleCd = grdData.DataTable.Rows[index][HEADER_ROLE_CODE].ToString();
                string strStartTime = grdData.DataTable.Rows[index][HEADER_STARTTIME].ToString();
                string strEndTime = grdData.DataTable.Rows[index][HEADER_ENDTIME].ToString();
                string strParticularCd = grdData.DataTable.Rows[index][HEADER_PARTICULAR_CODE].ToString();
                string strPtnyDescr = grdData.DataTable.Rows[index][HEADER_PENALTY].ToString();
                string strUnitPrc = grdData.DataTable.Rows[index][HEADER_UNITPRC].ToString();
                string strRate = grdData.DataTable.Rows[index][HEADER_RATE].ToString();
                string strTotal = grdData.DataTable.Rows[index][HEADER_TOTAL].ToString();

                txtPntyDescr.Text = strPtnyDescr;
                txtUnitPrc.Text = strUnitPrc;
                txtRate.Text = strRate;
                txtTotal.Text = strTotal;
                CommonUtility.SetDTPValueDMY(txtDate, strDate);
                CommonUtility.SetDTPValueDMYHM(txtStartTime, strStartTime);
                CommonUtility.SetDTPValueDMYHM(txtEndTime, strEndTime);
                CommonUtility.SetComboboxSelectedItem(cboHatch, strHatch);
                CommonUtility.SetComboboxSelectedItem(cboContractor, strContractor);
                CommonUtility.SetComboboxSelectedItem(cboRole, strRoleCd);
                CommonUtility.SetComboboxSelectedItem(cboParticulars, strParticularCd);

                // Disable UPDATE, DELETE buttons if this row was marked as deleted.
                string strWorkingStatus = grdData.DataTable.Rows[index][HEADER_WS_NM].ToString();
                if (Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                {
                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                }
            }
        }

        private void cboHatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_parm != null && m_parm.ArrGridData != null)
            {
                CommonUtility.InitializeCombobox(cboContractor);

                ArrayList arrGridData = m_parm.ArrGridData;
                string hatchNo = CommonUtility.GetComboboxSelectedValue(cboHatch);
                for (int i = 0; i < arrGridData.Count; i++)
                {
                    VORDryBreakBulkItem item = (VORDryBreakBulkItem)arrGridData[i];
                    if (hatchNo.Equals(item.hatchNo) && !HVO110.CONST_SHIPCR_NM.Equals(item.workComp))
                    {
                        cboContractor.Items.Add(new ComboboxValueDescriptionPair(item.workComp, item.workComp));
                    }
                }
            }
        }

        private void ClearForm()
        {
            cboHatch.SelectedIndex = 0;
            cboParticulars.SelectedIndex = 0;
            cboRole.SelectedIndex = 0;
            cboContractor.SelectedIndex = 0;
            txtPntyDescr.Text = "";
            txtRate.Text = "";
            txtUnitPrc.Text = "";
            txtTotal.Text = "";
            CommonUtility.SetDTPValueBlank(txtDate);

            this.IsDirty = false;
        }

        private bool Validate()
        {
            try
            {
                // Check number of items > 0
                int rate = CommonUtility.ParseInt(txtRate.Text);
                if (rate <= 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO111_0001"));
                    txtRate.Focus();
                    txtRate.SelectAll();
                    return false;
                }

                // Validate start, end time within workdate and shift
                if (!CommonUtility.ValidateDateWithinWorkdate(txtDate))
                {
                    CommonUtility.AlertMessage("Date should be within Workdate.");
                    txtDate.Focus();
                    return false;
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

        private bool IsExistAlready(int mode)
        {
            // Make sure HATCH, STEVEDORE, PARTICULAR, DATE to be unique.
            string newHatch = CommonUtility.GetComboboxSelectedValue(cboHatch);
            string newParticular = CommonUtility.GetComboboxSelectedValue(cboParticulars);
            string newStv = CommonUtility.GetComboboxSelectedValue(cboContractor);
            string newDate = txtDate.Text;

            if (grdData != null && grdData.DataTable.Rows.Count > 0)
            {
                int count = grdData.DataTable.Rows.Count;
                int index = count - 1;
                string oldHatch = grdData.DataTable.Rows[index][HEADER_HATCH].ToString();
                string oldParticular = grdData.DataTable.Rows[index][HEADER_PARTICULAR_CODE].ToString();
                string oldStv = grdData.DataTable.Rows[index][HEADER_CONTRACTOR].ToString();
                string oldDate = grdData.DataTable.Rows[index][HEADER_DATE].ToString();
                while (!newHatch.Equals(oldHatch) || 
                    !newParticular.Equals(oldParticular) || 
                    !newStv.Equals(oldStv) || 
                    !newDate.Equals(oldDate) )
                {
                    index = index - 1;
                    if (index < 0)
                    {
                        return false;
                    }
                    if (mode == Constants.MODE_ADD)
                    {
                        oldHatch = grdData.DataTable.Rows[index][HEADER_HATCH].ToString();
                        oldParticular = grdData.DataTable.Rows[index][HEADER_PARTICULAR_CODE].ToString();
                        oldStv = grdData.DataTable.Rows[index][HEADER_CONTRACTOR].ToString();
                        oldDate = grdData.DataTable.Rows[index][HEADER_DATE].ToString();
                    }
                    else if (mode == Constants.MODE_UPDATE)
                    {
                        if (index != grdData.CurrentRowIndex)
                        {
                            oldHatch = grdData.DataTable.Rows[index][HEADER_HATCH].ToString();
                            oldParticular = grdData.DataTable.Rows[index][HEADER_PARTICULAR_CODE].ToString();
                            oldStv = grdData.DataTable.Rows[index][HEADER_CONTRACTOR].ToString();
                            oldDate = grdData.DataTable.Rows[index][HEADER_DATE].ToString();
                        }
                    }
                }
                if (mode == Constants.MODE_ADD)
                {
                    if (index >= 0)
                    {
                        return true;
                    }
                }
                else if (mode == Constants.MODE_UPDATE)
                {
                    if (index >= 0 && index != grdData.CurrentRowIndex)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}