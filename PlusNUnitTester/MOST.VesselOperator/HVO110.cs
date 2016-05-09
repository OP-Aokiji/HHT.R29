using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
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
using Framework.Common.Constants;

namespace MOST.VesselOperator
{
    public partial class HVO110 : TForm, IPopupWindow
    {
        private const int TAB_BBK = 0;
        private const int TAB_DBK = 1;
        private const string HEADER_ITEM_STATUS = "_ITEM_STATUS";
        private const string HEADER_WS_NM = "Status";
        private const string HEADER_BULK = "Break/Dry";
        private const string HEADER_HATCH = "Hatch";
        private const string HEADER_APFP = "AP/FP";
        private const string HEADER_CREW = "_CREW";
        private const string HEADER_COMPANY = "Comp.";
        private const string HEADER_WITHGEARS = "With Gears";
        private const string HEADER_SUPERVISOR = "Sup";
        private const string HEADER_WINCH = "Winch";
        private const string HEADER_SIGNAL = "Signal";
        private const string HEADER_DECKMEN = "Deck";
        private const string HEADER_HOPPER = "Hopper";
        private const string HEADER_GENERAL = "General";
        private const string HEADER_ADD_SUP = "Add.Sup";
        private const string HEADER_ADD_TONN = "Add.Tonn";
        private const string HEADER_COMPTPCD = "_compTpCd";

        // TMT_VSL_OPE_RPT_DTL.CW_DIV = {'Y', 'N'} - Y: Ship's Crew, N: Stevedore/Trimming company
        public const string CONST_SHIPCR_NM = "Ship's Crew";

        private HVO110Parm m_parm;
        private ArrayList m_arrGridData;

        public HVO110()
        {
            m_arrGridData = new ArrayList();
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();

            btnUpdate.Enabled = false;
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HVO110Parm)parm;
            txtDBJPVC.Text = m_parm.JpvcInfo.Jpvc;
            txtBBJPVC.Text = m_parm.JpvcInfo.Jpvc;
            InitializeData();
            this.ShowDialog();
            return null;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_ITEM_STATUS, "0" }, { HEADER_WS_NM, "40" }, { HEADER_BULK, "40" }, { HEADER_HATCH, "40" }, { HEADER_APFP, "40" }, { HEADER_CREW, "0" }, { HEADER_COMPANY, "40" }, { HEADER_WITHGEARS, "40" }, { HEADER_SUPERVISOR, "40" }, { HEADER_WINCH, "40" }, { HEADER_SIGNAL, "40" }, { HEADER_DECKMEN, "40" }, { HEADER_HOPPER, "40" }, { HEADER_GENERAL, "40" }, { HEADER_ADD_SUP, "40" }, { HEADER_ADD_TONN, "40" }, { HEADER_COMPTPCD, "0" } };
            this.grdData.setHeader(header);
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Grid Data
                GetVORDryBreakBulk();
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

        private void GetVORDryBreakBulk()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Grid Data
                IVesselOperatorProxy delayProxy = new VesselOperatorProxy();
                VORDryBreakBulkParm delayParm = new VORDryBreakBulkParm();
                delayParm.searchType = "infoSheet";
                delayParm.rsDivCd = "WC";
                delayParm.vslCallId = txtDBJPVC.Text;
                delayParm.workYmd = UserInfo.getInstance().Workdate;
                delayParm.shift = UserInfo.getInstance().Shift;
                ResponseInfo delayInfo = delayProxy.getVORDryBreakBulk(delayParm);

                grdData.Clear();
                m_arrGridData.Clear();
                if (delayInfo != null)
                {
                    for (int i = 0; i < delayInfo.list.Length; i++)
                    {
                        if (delayInfo.list[i] is VORDryBreakBulkItem)
                        {
                            VORDryBreakBulkItem item = (VORDryBreakBulkItem)delayInfo.list[i];

                            // To eliminate blank space. (refer: QA_20090714)
                            item.hatchNo = string.IsNullOrEmpty(item.hatchNo) ? string.Empty : item.hatchNo.Trim();
                            item.hatchDrtCd = string.IsNullOrEmpty(item.hatchDrtCd) ? string.Empty : item.hatchDrtCd.Trim();

                            DataRow newRow = grdData.NewRow();
                            newRow[HEADER_ITEM_STATUS] = Constants.ITEM_OLD;
                            newRow[HEADER_WS_NM] = Constants.WS_NM_INITIAL;
                            newRow[HEADER_BULK] = item.cgTpCd;
                            newRow[HEADER_HATCH] = item.hatchNo;
                            newRow[HEADER_APFP] = item.hatchDrtCd;
                            newRow[HEADER_CREW] = item.cwDiv;
                            newRow[HEADER_COMPANY] = item.workComp;
                            newRow[HEADER_WITHGEARS] = item.withGears;
                            newRow[HEADER_SUPERVISOR] = item.spr;
                            newRow[HEADER_WINCH] = item.winch;
                            newRow[HEADER_SIGNAL] = item.signal;
                            newRow[HEADER_DECKMEN] = item.deck;
                            newRow[HEADER_HOPPER] = item.hoper;
                            newRow[HEADER_GENERAL] = item.general;
                            newRow[HEADER_ADD_SUP] = item.supervisor;
                            newRow[HEADER_ADD_TONN] = item.nonworker;
                            newRow[HEADER_COMPTPCD] = item.compTpCd;

                            grdData.Add(newRow);
                            m_arrGridData.Add(item);
                        }
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
                grdData.IsDirty = false;
                Cursor.Current = Cursors.Default;
            }
        }

        private void UpdateItem()
        {
            int currRowIndex = grdData.CurrentRowIndex;
            if (currRowIndex > -1 && currRowIndex < grdData.DataTable.Rows.Count)
            {
                // Set Readonly False to update column data.
                DataTable dataTable = grdData.DataTable;
                int colCnt = dataTable.Columns.Count;
                for (int i = 0; i < colCnt; i++)
                {
                    dataTable.Columns[i].ReadOnly = false;
                }

                // Update columns data
                DataRow row = grdData.DataTable.Rows[currRowIndex];
                if (Constants.ITEM_OLD.Equals(grdData.DataTable.Rows[currRowIndex][HEADER_ITEM_STATUS].ToString()))
                {
                    row[HEADER_WS_NM] = Constants.WS_NM_UPDATE;
                }
                string cgTpCd = grdData.DataTable.Rows[currRowIndex][HEADER_BULK].ToString();
                if ("BBK".Equals(cgTpCd))
                {
                    if (rbtnBBCrew.Checked)
                    {
                        row[HEADER_CREW] = "Y";
                        row[HEADER_COMPANY] = CONST_SHIPCR_NM;
                        row[HEADER_WITHGEARS] = chkWithGears.Checked ? "Y" : "N";
                        row[HEADER_SUPERVISOR] = string.Empty;
                        row[HEADER_WINCH] = string.Empty;
                        row[HEADER_GENERAL] = string.Empty;
                        row[HEADER_ADD_SUP] = string.Empty;
                        row[HEADER_ADD_TONN] = string.Empty;
                        row[HEADER_COMPTPCD] = string.Empty;
                    }
                    else if (rbtnBBStvd.Checked)
                    {
                        row[HEADER_CREW] = "N";
                        row[HEADER_COMPANY] = txtBBStevedore.Text;
                        row[HEADER_WITHGEARS] = string.Empty;
                        row[HEADER_SUPERVISOR] = txtBBSupervisor.Text;
                        row[HEADER_WINCH] = txtBBWinch.Text;
                        row[HEADER_GENERAL] = txtBBGeneral.Text;
                        row[HEADER_ADD_SUP] = txtBBAddSup.Text;
                        row[HEADER_ADD_TONN] = txtBBAddTonn.Text;
                        row[HEADER_COMPTPCD] = string.IsNullOrEmpty(txtBBStevedore.Text) ? string.Empty : "STV";
                    }
                }
                else if ("DBK".Equals(cgTpCd) || "DBE".Equals(cgTpCd) || "DBN".Equals(cgTpCd))
                {
                    if (rbtnDBCrew.Checked)
                    {
                        row[HEADER_CREW] = "Y";
                        row[HEADER_COMPANY] = CONST_SHIPCR_NM;
                        row[HEADER_WITHGEARS] = string.Empty;
                        row[HEADER_SUPERVISOR] = string.Empty;
                        row[HEADER_SIGNAL] = string.Empty;
                        row[HEADER_DECKMEN] = string.Empty;
                        row[HEADER_HOPPER] = string.Empty;
                        row[HEADER_GENERAL] = string.Empty;
                        row[HEADER_ADD_SUP] = string.Empty;
                        row[HEADER_ADD_TONN] = string.Empty;
                        row[HEADER_COMPTPCD] = string.Empty;
                    }
                    else if (rbtnDBTrm.Checked)
                    {
                        row[HEADER_CREW] = "N";
                        row[HEADER_COMPANY] = txtDBTrimming.Text;
                        row[HEADER_WITHGEARS] = string.Empty;
                        row[HEADER_SUPERVISOR] = txtDBSupervisor.Text;
                        row[HEADER_SIGNAL] = txtDBSignal.Text;
                        row[HEADER_DECKMEN] = txtDBDeck.Text;
                        row[HEADER_HOPPER] = txtDBHopper.Text;
                        row[HEADER_GENERAL] = txtDBGeneral.Text;
                        row[HEADER_ADD_SUP] = txtDBAddSup.Text;
                        row[HEADER_ADD_TONN] = txtDBAddTonn.Text;
                        row[HEADER_COMPTPCD] = string.IsNullOrEmpty(txtDBTrimming.Text) ? string.Empty : "TRM";
                    }
                    
                }
                grdData.DataTable.AcceptChanges();
                grdData.Refresh();

                // Reset original Readonly
                for (int i = 0; i < colCnt; i++)
                {
                    dataTable.Columns[i].ReadOnly = true;
                }

                // Update array list
                VORDryBreakBulkItem item = (VORDryBreakBulkItem)m_arrGridData[currRowIndex];
                item.workComp = grdData.DataTable.Rows[currRowIndex][HEADER_COMPANY].ToString();
                item.withGears = grdData.DataTable.Rows[currRowIndex][HEADER_WITHGEARS].ToString();
                item.spr = grdData.DataTable.Rows[currRowIndex][HEADER_SUPERVISOR].ToString();
                item.winch = grdData.DataTable.Rows[currRowIndex][HEADER_WINCH].ToString();
                item.signal = grdData.DataTable.Rows[currRowIndex][HEADER_SIGNAL].ToString();
                item.deck = grdData.DataTable.Rows[currRowIndex][HEADER_DECKMEN].ToString();
                item.hoper = grdData.DataTable.Rows[currRowIndex][HEADER_HOPPER].ToString();
                item.general = grdData.DataTable.Rows[currRowIndex][HEADER_GENERAL].ToString();
                item.supervisor = grdData.DataTable.Rows[currRowIndex][HEADER_ADD_SUP].ToString();
                item.nonworker = grdData.DataTable.Rows[currRowIndex][HEADER_ADD_TONN].ToString();
                item.compTpCd = grdData.DataTable.Rows[currRowIndex][HEADER_COMPTPCD].ToString();
                item.cwDiv = grdData.DataTable.Rows[currRowIndex][HEADER_CREW].ToString();
                item.workYmd = UserInfo.getInstance().Workdate;
                item.shftId = UserInfo.getInstance().Shift;
                item.userId = UserInfo.getInstance().UserId;
                item.vslCallId = txtBBJPVC.Text;
                item.rsDivCd = "EQ";
                item.CRUD = Constants.WS_UPDATE;
            }
        }

        private void ClearCtrlValues()
        {
            if (tabMain.SelectedIndex == HVO110.TAB_BBK)
            {
                rbtnBBCrew.Checked = true;
                chkWithGears.Checked = false;
                txtBBStevedore.Text = string.Empty;
                txtBBSupervisor.Text = string.Empty;
                txtBBWinch.Text = string.Empty;
                txtBBGeneral.Text = string.Empty;
                txtBBAddSup.Text = string.Empty;
                txtBBAddTonn.Text = string.Empty;
                txtBBHatch.Text = string.Empty;
                txtBBWP.Text = string.Empty;
            }
            else if (tabMain.SelectedIndex == HVO110.TAB_DBK)
            {
                rbtnDBCrew.Checked = true;
                txtDBTrimming.Text = string.Empty;
                txtDBSupervisor.Text = string.Empty;
                txtDBSignal.Text = string.Empty;
                txtDBDeck.Text = string.Empty;
                txtDBHopper.Text = string.Empty;
                txtDBGeneral.Text = string.Empty;
                txtDBAddSup.Text = string.Empty;
                txtDBAddTonn.Text = string.Empty;
                txtDBHatch.Text = string.Empty;
            }
        }

        private void ClearAll()
        {
            // Beak Bulk
            rbtnBBCrew.Checked = true;
            chkWithGears.Checked = false;
            txtBBStevedore.Text = string.Empty;
            txtBBSupervisor.Text = string.Empty;
            txtBBWinch.Text = string.Empty;
            txtBBGeneral.Text = string.Empty;
            txtBBAddSup.Text = string.Empty;
            txtBBAddTonn.Text = string.Empty;
            txtBBHatch.Text = string.Empty;
            txtBBWP.Text = string.Empty;

            // Dry Bulk
            rbtnDBCrew.Checked = true;
            txtDBTrimming.Text = string.Empty;
            txtDBSupervisor.Text = string.Empty;
            txtDBSignal.Text = string.Empty;
            txtDBDeck.Text = string.Empty;
            txtDBHopper.Text = string.Empty;
            txtDBGeneral.Text = string.Empty;
            txtDBAddSup.Text = string.Empty;
            txtDBAddTonn.Text = string.Empty;
            txtDBHatch.Text = string.Empty;

            grdData.IsDirty = false;
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnBBF1":
                    MegaStvTrmParm stvParm = new MegaStvTrmParm();
                    stvParm.VslCallId = txtBBJPVC.Text;
                    stvParm.ShftId = UserInfo.getInstance().Shift;
                    stvParm.WorkYmd = UserInfo.getInstance().Workdate;
                    MegaStvTrmResult stvResTmp = (MegaStvTrmResult)PopupManager.instance.ShowPopup(new HCM109(HCM109.TYPE_STEVEDORE), stvParm);
                    if (stvResTmp != null)
                    {
                        txtBBStevedore.Text = stvResTmp.Code;
                    }
                    break;

                case "btnDBF1":
                    MegaStvTrmParm trmParm = new MegaStvTrmParm();
                    trmParm.VslCallId = txtDBJPVC.Text;
                    trmParm.ShftId = UserInfo.getInstance().Shift;
                    trmParm.WorkYmd = UserInfo.getInstance().Workdate;
                    MegaStvTrmResult trmResTmp = (MegaStvTrmResult)PopupManager.instance.ShowPopup(new HCM109(HCM109.TYPE_TRIMMING), trmParm);
                    if (trmResTmp != null)
                    {
                        txtDBTrimming.Text = trmResTmp.Code;
                    }
                    break;

                case "btnOk":
                    if (grdData.IsDirty)
                    {
                        if (ProcessVORDryBreakBulkForStevAndTrimCUD())
                        {
                            GetVORDryBreakBulk();
                            ClearAll();
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
                            ProcessVORDryBreakBulkForStevAndTrimCUD();
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

                case "btnPenalty":
                    HVO111Parm hvo111Parm = new HVO111Parm();
                    hvo111Parm.JpvcInfo = m_parm.JpvcInfo;
                    hvo111Parm.ArrGridData = m_arrGridData;
                    hvo111Parm.CurrIndex = grdData.CurrentRowIndex;
                    PopupManager.instance.ShowPopup(new HVO111(), hvo111Parm);
                    break;
                case "btnUpdate":
                    if (this.validations(this.Controls))
                    {
                        UpdateItem();
                        ClearCtrlValues();
                    }
                    break;
            }
        }

        private bool ProcessVORDryBreakBulkForStevAndTrimCUD()
        {
            // ref: CT208
            bool result = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

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
                            VORDryBreakBulkItem item = (VORDryBreakBulkItem)m_arrGridData[i];
                            arrObj.Add(item);
                        }
                    }
                }

                if (arrObj.Count > 0)
                {
                    Object[] obj = arrObj.ToArray();
                    DataItemCollection dataCollection = new DataItemCollection();
                    dataCollection.collection = obj;

                    proxy.processVORDryBreakBulkForStevAndTrimCUD(dataCollection);
                    result = true;
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
            return result;
        }

        private void grdData_CurrentCellChanged(object sender, EventArgs e)
        {
            int index = grdData.CurrentRowIndex;
            if (index > -1 && index < grdData.DataTable.Rows.Count)
            {
                string cgTpCd = grdData.DataTable.Rows[index][HEADER_BULK].ToString();
                if ("BBK".Equals(cgTpCd))
                {
                    // Set focus on Break Bulk tab.
                    tabMain.SelectedIndex = HVO110.TAB_BBK;

                    if ("Y".Equals(grdData.DataTable.Rows[index][HEADER_CREW].ToString()))
                    {
                        rbtnBBCrew.Checked = true;
                        string strWithGearsYn = grdData.DataTable.Rows[index][HEADER_WITHGEARS].ToString();
                        chkWithGears.Checked = "Y".Equals(strWithGearsYn);
                    }
                    else
                    {
                        rbtnBBStvd.Checked = true;
                        txtBBStevedore.Text = grdData.DataTable.Rows[index][HEADER_COMPANY].ToString();
                        txtBBSupervisor.Text = grdData.DataTable.Rows[index][HEADER_SUPERVISOR].ToString();
                        txtBBWinch.Text = grdData.DataTable.Rows[index][HEADER_WINCH].ToString();
                        txtBBGeneral.Text = grdData.DataTable.Rows[index][HEADER_GENERAL].ToString();
                        txtBBAddSup.Text = grdData.DataTable.Rows[index][HEADER_ADD_SUP].ToString();
                        txtBBAddTonn.Text = grdData.DataTable.Rows[index][HEADER_ADD_TONN].ToString();

                        txtBBHatch.Text = grdData.DataTable.Rows[index][HEADER_HATCH].ToString();
                        txtBBWP.Text = grdData.DataTable.Rows[index][HEADER_APFP].ToString();
                    }
                }
                else if ("DBK".Equals(cgTpCd) || "DBE".Equals(cgTpCd) || "DBN".Equals(cgTpCd))
                {
                    // Set focus on Dry Bulk tab.
                    tabMain.SelectedIndex = HVO110.TAB_DBK;

                    if ("Y".Equals(grdData.DataTable.Rows[index][HEADER_CREW].ToString()))
                    {
                        rbtnDBCrew.Checked = true;
                    }
                    else
                    {
                        rbtnDBTrm.Checked = true;

                        txtDBTrimming.Text = grdData.DataTable.Rows[index][HEADER_COMPANY].ToString();
                        txtDBSupervisor.Text = grdData.DataTable.Rows[index][HEADER_SUPERVISOR].ToString();
                        txtDBSignal.Text = grdData.DataTable.Rows[index][HEADER_SIGNAL].ToString();
                        txtDBDeck.Text = grdData.DataTable.Rows[index][HEADER_DECKMEN].ToString();
                        txtDBHopper.Text = grdData.DataTable.Rows[index][HEADER_HOPPER].ToString();
                        txtDBGeneral.Text = grdData.DataTable.Rows[index][HEADER_GENERAL].ToString();
                        txtDBAddSup.Text = grdData.DataTable.Rows[index][HEADER_ADD_SUP].ToString();
                        txtDBAddTonn.Text = grdData.DataTable.Rows[index][HEADER_ADD_TONN].ToString();

                        txtDBHatch.Text = grdData.DataTable.Rows[index][HEADER_HATCH].ToString();
                    }
                }

                // Disable UPDATE, DELETE buttons if this row was marked as deleted.
                string strWorkingStatus = grdData.DataTable.Rows[index][HEADER_WS_NM].ToString();
                btnUpdate.Enabled = !Constants.WS_NM_DELETE.Equals(strWorkingStatus);
            }
        }

        private void RadiobuttonAction(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            String rbtnName = radioButton.Name;
            switch (rbtnName)
            {
                case "rbtnBBCrew":
                case "rbtnBBStvd":
                case "rbtnDBCrew":
                case "rbtnDBTrm":
                    CheckRadioButton();
                    break;
            }
        }

        private void CheckRadioButton()
        {
            // BBK
            bool bEnable = false;
            if (rbtnBBStvd.Checked)
            {
                bEnable = true;
            }
            chkWithGears.Enabled = !bEnable;
            btnBBF1.Enabled = bEnable;
            txtBBStevedore.Enabled = bEnable;
            txtBBAddSup.Enabled = bEnable;
            txtBBAddTonn.Enabled = bEnable;
            txtBBGeneral.Enabled = bEnable;
            txtBBSupervisor.Enabled = bEnable;
            txtBBWinch.Enabled = bEnable;

            // DBK
            bEnable = false;
            if (rbtnDBTrm.Checked)
            {
                bEnable = true;
            }
            btnDBF1.Enabled = bEnable;
            txtDBTrimming.Enabled = bEnable;
            txtDBAddSup.Enabled = bEnable;
            txtDBAddTonn.Enabled = bEnable;
            txtDBDeck.Enabled = bEnable;
            txtDBGeneral.Enabled = bEnable;
            txtDBHopper.Enabled = bEnable;
            txtDBSignal.Enabled = bEnable;
            txtDBSupervisor.Enabled = bEnable;
        }
    }
}