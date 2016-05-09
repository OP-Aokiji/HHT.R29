using System;
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
    public partial class HVO120 : TForm, IPopupWindow
    {
        private const string HEADER_ITEM_STATUS = "_ITEM_STATUS";
        private const string HEADER_WS_NM = "Status";
        private const string HEADER_CGTP = "Cg Type";
        private const string HEADER_DLY_CD = "DelayCode";
        private const string HEADER_DLY_NM = "Delay Code Name";
        private const string HEADER_ACCEPTDLY = "Accepted delay";
        private const string HEADER_STARTTIME = "StartTime";
        private const string HEADER_ENDTIME = "EndTime";
        private const string HEADER_REMARK = "Remark";
        private const string HEADER_SEQ = "_SEQ";

        private string m_opeTp;
        private string m_curDelayCodeDesc = "";
        private HVO120Parm m_parm;
        private VORLiquidBulkItem m_liquidBulkItem;

        public HVO120(string opeTp)
        {
            m_opeTp = opeTp;
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            CommonUtility.SetDTPValueBlank(txtStartTime);
            CommonUtility.SetDTPValueBlank(txtEndTime);
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HVO120Parm)parm;
            txtJPVC.Text = m_parm.JpvcInfo.Jpvc;
            InitializeData();
            this.ShowDialog();
            return null;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_ITEM_STATUS, "0" }, { HEADER_WS_NM, "40" }, { HEADER_CGTP, "40" }, { HEADER_DLY_CD, "40" }, { HEADER_DLY_NM, "0" }, { HEADER_ACCEPTDLY, "40" }, { HEADER_STARTTIME, "90" }, { HEADER_ENDTIME, "90" }, { HEADER_REMARK, "80" }, { HEADER_SEQ, "0" } };
            this.grdData.setHeader(header);
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Hatch
                CommonUtility.InitializeCombobox(cboCgTp, "Select");
                cboCgTp.Items.Add(new ComboboxValueDescriptionPair("LQE", "Edible"));
                cboCgTp.Items.Add(new ComboboxValueDescriptionPair("LQN", "Non Edible"));
                #endregion

                #region Grid Data
                GetLiquidBulkHoseQty();
                GetDelayRecordList();
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

        private void GetDelayRecordList()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Grid Data
                IVesselOperatorProxy delayProxy = new VesselOperatorProxy();
                VORLiquidBulkParm delayParm = new VORLiquidBulkParm();
                delayParm.jpvcNo = txtJPVC.Text;
                delayParm.workYmd = UserInfo.getInstance().Workdate;
                delayParm.shift = UserInfo.getInstance().Shift;
                ResponseInfo delayInfo = delayProxy.getVORLiquidDelay(delayParm);

                grdData.Clear();
                if (delayInfo != null)
                {
                    for (int i = 0; i < delayInfo.list.Length; i++)
                    {
                        if (delayInfo.list[i] is VORLiquidBulkItem)
                        {
                            VORLiquidBulkItem item = (VORLiquidBulkItem)delayInfo.list[i];
                            DataRow newRow = grdData.NewRow();
                            newRow[HEADER_ITEM_STATUS] = Constants.ITEM_OLD;
                            newRow[HEADER_WS_NM] = Constants.WS_NM_INITIAL;
                            newRow[HEADER_CGTP] = item.cgTpCd;
                            newRow[HEADER_DLY_CD] = item.delayCode;
                            newRow[HEADER_DLY_NM] = item.delayDesc;
                            newRow[HEADER_ACCEPTDLY] = item.accDelay;
                            newRow[HEADER_STARTTIME] = item.startTime;
                            newRow[HEADER_ENDTIME] = item.endTime;
                            newRow[HEADER_REMARK] = item.remark;
                            newRow[HEADER_SEQ] = item.seq;

                            grdData.Add(newRow);
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

        /// <summary>
        /// Get hose quantity
        /// </summary>
        private void GetLiquidBulkHoseQty()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                VORLiquidBulkParm parm = new VORLiquidBulkParm();
                parm.opeTp = m_opeTp;
                parm.jpvcNo = txtJPVC.Text;
                parm.workYmd = UserInfo.getInstance().Workdate;
                parm.shift = UserInfo.getInstance().Shift;
                parm.searchType = "HHT_LiquidBulk";
                ResponseInfo info = proxy.getVORLiquidBulk(parm);

                if (info != null && info.list.Length > 0 && info.list[0] is VORLiquidBulkItem)
                {
                    m_liquidBulkItem = (VORLiquidBulkItem)info.list[0];
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

        /// <summary>
        /// Update hose lines (and insert/update TMT_LQDCG_OPE_MST)
        /// </summary>
        private void UpdateVORLiquidHoseLines()
        {
            // ref: CT108 & CT108001
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (m_liquidBulkItem != null)
                {
                    IVesselOperatorProxy proxy = new VesselOperatorProxy();

                    m_liquidBulkItem.opeTp = m_opeTp;
                    m_liquidBulkItem.loadHoseQty = string.IsNullOrEmpty(m_liquidBulkItem.loadHoseQty) ? "0" : m_liquidBulkItem.loadHoseQty;
                    m_liquidBulkItem.dschHoseQty = string.IsNullOrEmpty(m_liquidBulkItem.dschHoseQty) ? "0" : m_liquidBulkItem.dschHoseQty;
                    m_liquidBulkItem.loadArmQty = string.IsNullOrEmpty(m_liquidBulkItem.loadArmQty) ? "0" : m_liquidBulkItem.loadArmQty;
                    m_liquidBulkItem.dschArmQty = string.IsNullOrEmpty(m_liquidBulkItem.dschArmQty) ? "0" : m_liquidBulkItem.dschArmQty;
                    m_liquidBulkItem.CRUD = Constants.WS_UPDATE;
                    m_liquidBulkItem.userId = UserInfo.getInstance().UserId;
                    m_liquidBulkItem.workYmd = UserInfo.getInstance().Workdate;
                    m_liquidBulkItem.shftId = UserInfo.getInstance().Shift;

                    Object[] obj = { m_liquidBulkItem };
                    DataItemCollection dataCollection = new DataItemCollection();
                    dataCollection.collection = obj;

                    proxy.updateVORLiquidHoseLines(dataCollection);
                }
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

        private bool ProcessVORLiquidDelayCUD()
        {
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
                            // ref: CT108 && CT108002
                            VORLiquidBulkItem item = new VORLiquidBulkItem();
                            item.vslCallId = txtJPVC.Text;
                            item.cgTpCd = grdData.DataTable.Rows[i][HEADER_CGTP].ToString();
                            item.delayCode = grdData.DataTable.Rows[i][HEADER_DLY_CD].ToString();
                            item.delayDesc = grdData.DataTable.Rows[i][HEADER_DLY_NM].ToString();
                            item.accDelay = grdData.DataTable.Rows[i][HEADER_ACCEPTDLY].ToString();
                            item.startTime = grdData.DataTable.Rows[i][HEADER_STARTTIME].ToString();
                            item.endTime = grdData.DataTable.Rows[i][HEADER_ENDTIME].ToString();
                            item.remark = grdData.DataTable.Rows[i][HEADER_REMARK].ToString();
                            if (!string.IsNullOrEmpty(grdData.DataTable.Rows[i][HEADER_SEQ].ToString()))
                            {
                                item.seq = grdData.DataTable.Rows[i][HEADER_SEQ].ToString();
                            }
                            item.opeTp = m_opeTp;
                            item.workYmd = UserInfo.getInstance().Workdate;
                            item.shftId = UserInfo.getInstance().Shift;
                            item.userId = UserInfo.getInstance().UserId;
                            item.CRUD = crud;

                            arrObj.Add(item);
                        }
                    }
                }

                if (arrObj.Count > 0)
                {
                    Object[] obj = arrObj.ToArray();
                    DataItemCollection dataCollection = new DataItemCollection();
                    dataCollection.collection = obj;

                    proxy.processVORLiquidDelayCUD(dataCollection);
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

        private void AddItem()
        {
            DataRow newRow = grdData.NewRow();
            newRow[HEADER_ITEM_STATUS] = Constants.ITEM_NEW;
            newRow[HEADER_WS_NM] = Constants.WS_NM_INSERT;
            newRow[HEADER_CGTP] = CommonUtility.GetComboboxSelectedValue(cboCgTp);
            newRow[HEADER_DLY_CD] = txtDelayCode.Text;
            newRow[HEADER_DLY_NM] = m_curDelayCodeDesc;
            newRow[HEADER_ACCEPTDLY] = chkAcceptDelay.Checked ? "Y" : "N";
            newRow[HEADER_STARTTIME] = txtStartTime.Text;
            newRow[HEADER_ENDTIME] = txtEndTime.Text;
            newRow[HEADER_REMARK] = txtRemark.Text;
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
                row[HEADER_CGTP] = CommonUtility.GetComboboxSelectedValue(cboCgTp);
                row[HEADER_DLY_CD] = txtDelayCode.Text;
                row[HEADER_DLY_NM] = m_curDelayCodeDesc;
                row[HEADER_ACCEPTDLY] = chkAcceptDelay.Checked ? "Y" : "N";
                row[HEADER_STARTTIME] = txtStartTime.Text;
                row[HEADER_ENDTIME] = txtEndTime.Text;
                row[HEADER_REMARK] = txtRemark.Text;
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
                case "btnF1":
                    DelayCodeListParm delayParm = new DelayCodeListParm();
                    DelayCodeListResult delayResult = (DelayCodeListResult)PopupManager.instance.ShowPopup(new HCM102(HCM102.TYPE_LIQUID), delayParm);
                    if (delayResult != null)
                    {
                        txtDelayCode.Text = delayResult.Code;
                        m_curDelayCodeDesc = delayResult.Description;
                        chkAcceptDelay.Checked = "Y".Equals(delayResult.AcceptedDelay);
                    }
                    break;

                case "btnOk":
                    if (grdData.IsDirty)
                    {
                        // Update hose lines (and insert/update TMT_LQDCG_OPE_MST)
                        UpdateVORLiquidHoseLines();
                        // Update grid data
                        if (ProcessVORLiquidDelayCUD())
                        {
                            ClearForm();
                            GetDelayRecordList();
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
                            // Update hose lines (and insert/update TMT_LQDCG_OPE_MST)
                            UpdateVORLiquidHoseLines();
                            // Update grid data
                            ProcessVORLiquidDelayCUD();
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
                    if (this.validations(this.Controls) && this.Validate())
                    {
                        // Check constraints: CgTp & Delay code & (StartTime or EndTime)
                        if (!IsExistAlready(Constants.MODE_ADD))
                        {
                            AddItem();
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
                        }
                    }
                    break;

                case "btnUpdate":
                    if (this.validations(this.Controls) && this.Validate())
                    {
                        // Check constraints: CgTp & Delay code & (StartTime or EndTime)
                        if (!IsExistAlready(Constants.MODE_UPDATE))
                        {
                            UpdateItem();
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
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

        private void grdData_CurrentCellChanged(object sender, EventArgs e)
        {
            int index = grdData.CurrentRowIndex;
            if (index > -1 && index < grdData.DataTable.Rows.Count)
            {
                string strCgTp = grdData.DataTable.Rows[index][HEADER_CGTP].ToString();
                string strDlyCd = grdData.DataTable.Rows[index][HEADER_DLY_CD].ToString();
                string strDlyNm = grdData.DataTable.Rows[index][HEADER_DLY_NM].ToString();
                string strAcceptDly = grdData.DataTable.Rows[index][HEADER_ACCEPTDLY].ToString();
                string strStartTime = grdData.DataTable.Rows[index][HEADER_STARTTIME].ToString();
                string strEndTime = grdData.DataTable.Rows[index][HEADER_ENDTIME].ToString();
                string strRemark = grdData.DataTable.Rows[index][HEADER_REMARK].ToString();

                m_curDelayCodeDesc = strDlyNm;
                txtDelayCode.Text = strDlyCd;
                txtRemark.Text = strRemark;
                CommonUtility.SetComboboxSelectedItem(cboCgTp, strCgTp);
                CommonUtility.SetDTPValueDMYHM(txtStartTime, strStartTime);
                CommonUtility.SetDTPValueDMYHM(txtEndTime, strEndTime);
                chkAcceptDelay.Checked = "Y".Equals(strAcceptDly);

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

        private void txtDelayCode_LostFocus(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (!string.IsNullOrEmpty(txtDelayCode.Text))
                {
                    ICommonProxy proxy = new CommonProxy();
                    CommonCodeParm commonParm = new CommonCodeParm();
                    commonParm.searchType = "DLYCD";
                    commonParm.lcd = "LIQUIQ";
                    commonParm.tyCd = "CD";
                    commonParm.cd = txtDelayCode.Text;
                    ResponseInfo info = proxy.getCommonCodeList(commonParm);

                    if (info != null && info.list != null)
                    {
                        for (int i = 0; i < info.list.Length; i++)
                        {
                            if (info.list.Length == 1 && (info.list[0] is CodeMasterListItem1 || info.list[0] is CodeMasterListItem))
                            {
                                if (info.list[0] is CodeMasterListItem)
                                    info.list[0] = CommonUtility.ToCodeMasterListItem1(info.list[0] as CodeMasterListItem);
                                CodeMasterListItem1 item = (CodeMasterListItem1) info.list[0];
                                chkAcceptDelay.Checked = "Y".Equals(item.acptYN);
                            }
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
                Cursor.Current = Cursors.Default;
            }
        }

        private void ClearForm()
        {
            cboCgTp.SelectedIndex = 0;
            txtDelayCode.Text = "";
            txtRemark.Text = "";
            chkAcceptDelay.Checked = false;
            CommonUtility.SetDTPValueBlank(txtStartTime);
            CommonUtility.SetDTPValueBlank(txtEndTime);

            this.IsDirty = false;
        }

        private bool Validate()
        {
            try
            {
                // Validate inputted delay code
                ICommonProxy proxy = new CommonProxy();
                CommonCodeParm commonParm = new CommonCodeParm();
                commonParm.searchType = "DLYCD";
                commonParm.lcd = "LIQUIQ";
                commonParm.tyCd = "CD";
                commonParm.cd = txtDelayCode.Text;
                ResponseInfo info = proxy.getCommonCodeList(commonParm);
                if (info == null || info.list == null || info.list.Length < 1 ||
                    !(info.list[0] is CodeMasterListItem || info.list[0] is CodeMasterListItem1))
                {
                    CommonUtility.AlertMessage("Invalid delay code. Please check again.");
                    txtDelayCode.Focus();
                    txtDelayCode.SelectAll();
                    return false;
                }

                // Validate start, end time within workdate and shift
                if (!CommonUtility.ValidateStartEndDtWithinShift(txtStartTime, txtEndTime))
                {
                    return false;
                }

                // Validate start, end time
                if (!CommonUtility.CheckDateStartEnd(txtStartTime, txtEndTime))
                {
                    txtStartTime.Focus();
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
            string strNewCgTp = CommonUtility.GetComboboxSelectedValue(cboCgTp);
            string strNewDlyCd = txtDelayCode.Text;
            string strNewStDt = txtStartTime.Text;
            string strNewEndDt = txtEndTime.Text;

            if (grdData != null && grdData.DataTable.Rows.Count > 0)
            {
                int count = grdData.DataTable.Rows.Count;
                int index = count - 1;
                string strOldCgTp = grdData.DataTable.Rows[index][HEADER_CGTP].ToString();
                string strOldDlyCd = grdData.DataTable.Rows[index][HEADER_DLY_CD].ToString();
                string strOldStDt = grdData.DataTable.Rows[index][HEADER_STARTTIME].ToString();
                string strOldEndDt = grdData.DataTable.Rows[index][HEADER_ENDTIME].ToString();
                while (!strNewCgTp.Equals(strOldCgTp) ||
                    !strNewDlyCd.Equals(strOldDlyCd) ||
                    (!strNewStDt.Equals(strOldStDt) && !strNewEndDt.Equals(strOldEndDt)))
                {
                    index = index - 1;
                    if (index < 0)
                    {
                        return false;
                    }
                    if (mode == Constants.MODE_ADD)
                    {
                        strOldCgTp = grdData.DataTable.Rows[index][HEADER_CGTP].ToString();
                        strOldDlyCd = grdData.DataTable.Rows[index][HEADER_DLY_CD].ToString();
                        strOldStDt = grdData.DataTable.Rows[index][HEADER_STARTTIME].ToString();
                        strOldEndDt = grdData.DataTable.Rows[index][HEADER_ENDTIME].ToString();
                    }
                    else if (mode == Constants.MODE_UPDATE)
                    {
                        if (index != grdData.CurrentRowIndex)
                        {
                            strOldCgTp = grdData.DataTable.Rows[index][HEADER_CGTP].ToString();
                            strOldDlyCd = grdData.DataTable.Rows[index][HEADER_DLY_CD].ToString();
                            strOldStDt = grdData.DataTable.Rows[index][HEADER_STARTTIME].ToString();
                            strOldEndDt = grdData.DataTable.Rows[index][HEADER_ENDTIME].ToString();
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