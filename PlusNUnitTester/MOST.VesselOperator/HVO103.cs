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
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using Framework.Service.Provider.WebService.Provider;
using Framework.Controls.UserControls;
using MOST.Client.Proxy.VesselOperatorProxy;

namespace MOST.VesselOperator
{
    public partial class HVO103 : TForm, IPopupWindow
    {
        private const string HEADER_ITEM_STATUS = "ITEM_STATUS";
        private const string HEADER_WS_NM = "Status";
        private const string HEADER_HATCH = "Hatch";
        private const string HEADER_APFP = "AP/FP";
        private const string HEADER_BULK = "Bulk";
        private const string HEADER_TOPCLEAN = "Top/Clean";
        private const string HEADER_STTIME = "StartTime";
        private const string HEADER_ENDTIME = "EndTime";
        private const string HEADER_SEQ = "_SEQ";

        private HVO103Parm m_parm;
        private HVO103Result m_result;
        private ArrayList m_arrGridData;

        public HVO103()
        {
            
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            ClearForm();
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HVO103Parm)parm;
            if (m_parm != null && m_parm.JpvcInfo != null)
            {
                txtJPVC.Text = m_parm.JpvcInfo.Jpvc;
            }
            InitializeData();
            this.ShowDialog();
            return m_result;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_ITEM_STATUS, "0" }, { HEADER_WS_NM, "40" }, { HEADER_BULK, "30" }, { HEADER_HATCH, "40" }, { HEADER_APFP, "40" }, { HEADER_TOPCLEAN, "40" }, { HEADER_STTIME, "95" }, { HEADER_ENDTIME, "95" }, { HEADER_SEQ, "0" } };
            this.grdData.setHeader(header);
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region StartTime, EndTime
                // Start,End Time auto displays according to Workdate & shift.
                CommonUtility.SetDTPWithinShift(txtStartTime, txtEndTime);
                #endregion

                #region AP/FP
                CommonUtility.SetHatchDirectionAPFP(cboAPFP);
                #endregion

                #region Hatch
                CommonUtility.SetHatchInfo(cboHatch);
                #endregion

                #region Grid
                GetVORDryBreakBulk();
                #endregion

                #region Top/Clean
                CommonUtility.InitializeCombobox(cboTopClean, "Select");
                cboTopClean.Items.Add(new ComboboxValueDescriptionPair("TOP", "Top"));
                cboTopClean.Items.Add(new ComboboxValueDescriptionPair("CLEAN", "Clean"));
                #endregion
            }
            catch (Framework.Common.Exception.BusinessException ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        // Check if Start Time and End Time are within Login WorkDate and Login Shift
        public static bool ValidateVORDryBreakBulkItem(TDateTimePicker argFromCtrl, TDateTimePicker argToCtrl)
        {
            try
            {
                bool result = false;
                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                VORDryBreakBulkParm parm = new VORDryBreakBulkParm();
                parm.workYmd = UserInfo.getInstance().Workdate;
                parm.shift = UserInfo.getInstance().Shift;
                parm.strDate = argFromCtrl.Text;
                parm.endDate = string.IsNullOrEmpty(argToCtrl.Text) ? argFromCtrl.Text : argToCtrl.Text;
                ResponseInfo info = proxy.validateVORDryBreakBulkItem(parm);
                if (info != null)
                {
                    VORDryBreakBulkItem item = (VORDryBreakBulkItem) info.list[0];
                    if (string.IsNullOrEmpty(item.toHhMm) && string.IsNullOrEmpty(item.fmHhMm))
                    {
                        CommonUtility.AlertMessage(
                            Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0059"));
                        argFromCtrl.Focus();
                    }
                    else
                    {
                        result = true;
                    }
                }
                return result;
            }
            catch (Exception)
            {
                
            }
            return false;
        }

        private void GetVORDryBreakBulk()
        {
            try
            {
                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                VORDryBreakBulkParm parm = new VORDryBreakBulkParm();
                parm.searchType = "info";
                parm.rsDivCd = "EQ";
                parm.vslCallId = txtJPVC.Text;
                parm.workYmd = UserInfo.getInstance().Workdate;
                parm.shift = UserInfo.getInstance().Shift;
                ResponseInfo info = proxy.getVORDryBreakBulk(parm);
                grdData.Clear();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is VORDryBreakBulkItem)
                    {
                        VORDryBreakBulkItem item = (VORDryBreakBulkItem) info.list[i];
                        DataRow newRow = grdData.NewRow();
                        newRow[HEADER_ITEM_STATUS] = Constants.ITEM_OLD;
                        newRow[HEADER_WS_NM] = Constants.WS_NM_INITIAL;
                        newRow[HEADER_HATCH] = item.hatchNo;
                        newRow[HEADER_APFP] = item.hatchDrtCd;
                        newRow[HEADER_BULK] = item.cgTpCd;
                        newRow[HEADER_TOPCLEAN] = item.topClean;
                        newRow[HEADER_STTIME] = item.workStDt;
                        newRow[HEADER_ENDTIME] = item.workEndDt;
                        newRow[HEADER_SEQ] = item.seq;

                        grdData.Add(newRow);
                        m_arrGridData.Add(item);
                    }
                }

                grdData.IsDirty = false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private void ProcessVORDryBreakBulkCUD()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // ref: CT207
                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                System.Collections.ArrayList arrObj = new System.Collections.ArrayList();

                if (m_arrGridData != null)
                {
                    for (int i = 0; i < m_arrGridData.Count; i++)
                    {
                        VORDryBreakBulkItem item = (VORDryBreakBulkItem)m_arrGridData[i];

                        // Process only items that are modified.
                        if (Constants.WS_INSERT.Equals(item.CRUD) ||
                            Constants.WS_UPDATE.Equals(item.CRUD) ||
                            Constants.WS_DELETE.Equals(item.CRUD))
                        {
                            arrObj.Add(item);
                        }
                    }
                }

                if (arrObj.Count > 0)
                {
                    Object[] obj = arrObj.ToArray();
                    DataItemCollection dataCollection = new DataItemCollection();
                    dataCollection.collection = obj;

                    proxy.processVORDryBreakBulkCUD(dataCollection);
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

        private bool IsExistAlready(int mode)
        {
            // Make sure HATCH to be unique.
            string strHatchCbo = CommonUtility.GetComboboxSelectedValue(cboHatch);
            string strApfpCbo = CommonUtility.GetComboboxSelectedValue(cboAPFP);
            string strStartTm = txtStartTime.Text;
            string strEndTm = txtEndTime.Text;

            if (grdData != null && grdData.DataTable.Rows.Count > 0)
            {
                int count = grdData.DataTable.Rows.Count;
                int index = count - 1;
                string hatchNo = "";
                string apfpNo = "";
                string startTm = "";
                string endTm = "";
                if (mode != Constants.MODE_UPDATE || index != grdData.CurrentRowIndex)
                {
                    hatchNo = grdData.DataTable.Rows[index][HEADER_HATCH].ToString();
                    apfpNo = grdData.DataTable.Rows[index][HEADER_APFP].ToString();
                    startTm = grdData.DataTable.Rows[index][HEADER_STTIME].ToString();
                    endTm = grdData.DataTable.Rows[index][HEADER_ENDTIME].ToString();
                }
                
                
                while (!strHatchCbo.Equals(hatchNo) || !strApfpCbo.Equals(apfpNo) || !CheckOverlap(strStartTm, strEndTm, startTm, endTm))
                {
                    index = index - 1;
                    if (index < 0)
                    {
                        return false;
                    }
                    if (strHatchCbo.Equals(hatchNo) && strApfpCbo.Equals("") && CheckOverlap(strStartTm, strEndTm, startTm, endTm))
                    {
                        return true;
                    }
                    if (mode == Constants.MODE_ADD)
                    {
                        hatchNo = grdData.DataTable.Rows[index][HEADER_HATCH].ToString();
                        apfpNo = grdData.DataTable.Rows[index][HEADER_APFP].ToString();
                        startTm = grdData.DataTable.Rows[index][HEADER_STTIME].ToString();
                        endTm = grdData.DataTable.Rows[index][HEADER_ENDTIME].ToString();
                    }
                    else if (mode == Constants.MODE_UPDATE)
                    {
                        if (index != grdData.CurrentRowIndex)
                        {
                            hatchNo = grdData.DataTable.Rows[index][HEADER_HATCH].ToString();
                            apfpNo = grdData.DataTable.Rows[index][HEADER_APFP].ToString();
                            startTm = grdData.DataTable.Rows[index][HEADER_STTIME].ToString();
                            endTm = grdData.DataTable.Rows[index][HEADER_ENDTIME].ToString();
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

        private bool CheckOverlap(String strStartTm1, String strEndTm1, String strStartTm2, String strEndTm2)
        {
            DateTime startTm1 = CommonUtility.ParseYMDHM(strStartTm1);
            DateTime endTm1 = CommonUtility.ParseYMDHM(strEndTm1);
            DateTime startTm2 = CommonUtility.ParseYMDHM(strStartTm2);
            DateTime endTm2 = CommonUtility.ParseYMDHM(strEndTm2);
            if (startTm1 >= endTm2 || startTm2 >= endTm1)
            {
                return false;
            }
            return true;   
        }

        private bool Validate()
        {
            if (!CommonUtility.CheckDateStartEnd(txtStartTime, txtEndTime))
            {
                return false;
            }

            if (!ValidateVORDryBreakBulkItem(txtStartTime, txtEndTime))
            {
                return false;
            }

            if (!ValidateAtbExist())
            {
                return false;
            }

            if (!ValidateTheLatestAtb())
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// If there is no ATB in Vessel Schedule, alert message.
        /// </summary>
        /// <returns></returns>
        private bool ValidateAtbExist()
        {
            bool result = true;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string[] cols = { txtJPVC.Text };
                result = CommonUtility.ValidateFunc("atbExist", cols);
                if (!result)
                {
                    CommonUtility.AlertMessage("It can't input because there is no ATB.");
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

        /// <summary>
        /// If there exists Shifting data, system allows to input Time greater than the latest ATB in Shifting info.
        /// </summary>
        /// <returns></returns>
        private bool ValidateTheLatestAtb()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                DateTime curAtb = DateTime.MinValue;
                DateTime startTime = DateTime.MinValue;
                DateTime endTime = DateTime.MinValue;

                if (m_parm != null && m_parm.JpvcInfo != null)
                {
                    curAtb = CommonUtility.ParseYMDHM(m_parm.JpvcInfo.CurAtb);

                    string msg;
                    if (!string.IsNullOrEmpty(txtStartTime.Text))
                    {
                        startTime = CommonUtility.ParseYMDHM(txtStartTime.Text);
                        if (startTime.CompareTo(curAtb) < 0)
                        {
                            msg = "You should input hatch start time later than the latest ATB({0}). Please try again.";
                            msg = string.Format(msg, m_parm.JpvcInfo.CurAtb);
                            CommonUtility.AlertMessage(msg);
                            return false;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtEndTime.Text))
                    {
                        endTime = CommonUtility.ParseYMDHM(txtEndTime.Text);
                        if (endTime.CompareTo(curAtb) < 0)
                        {
                            msg = "You should input hatch end time later than the latest ATB({0}). Please try again.";
                            msg = string.Format(msg, m_parm.JpvcInfo.CurAtb);
                            CommonUtility.AlertMessage(msg);
                            return false;
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
            return true;
        }

        private void AddItem()
        {
            string hatchNo = CommonUtility.GetComboboxSelectedValue(cboHatch);
            string hatchDrtCd = CommonUtility.GetComboboxSelectedValue(cboAPFP);
            string topClean = CommonUtility.GetComboboxSelectedValue(cboTopClean);

            VORDryBreakBulkItem item = new VORDryBreakBulkItem();
            item.hatchNo = hatchNo;
            item.hatchDrtCd = hatchDrtCd;
            item.topClean = topClean;
            item.workStDt = txtStartTime.Text;
            item.workEndDt = txtEndTime.Text;
            item.rsDivCd = "EQ";
            item.vslCallId = txtJPVC.Text;
            item.cgTpCd = rbtnBreak.Checked ? "BBK" : "DBK";
            item.workYmd = UserInfo.getInstance().Workdate;
            item.userId = UserInfo.getInstance().UserId;
            item.shftId = UserInfo.getInstance().Shift;
            item.CRUD = Constants.WS_INSERT;
            m_arrGridData.Add(item);

            DataRow newRow = grdData.NewRow();
            newRow[HEADER_ITEM_STATUS] = Constants.ITEM_NEW;
            newRow[HEADER_WS_NM] = Constants.WS_NM_INSERT;
            newRow[HEADER_HATCH] = hatchNo;
            newRow[HEADER_APFP] = hatchDrtCd;
            newRow[HEADER_BULK] = rbtnBreak.Checked ? "BBK" : "DBK";
            newRow[HEADER_TOPCLEAN] = topClean;
            newRow[HEADER_STTIME] = txtStartTime.Text;
            newRow[HEADER_ENDTIME] = txtEndTime.Text;
            grdData.Add(newRow);
            grdData.Refresh();
        }

        private void UpdateItem()
        {
            int currRowIndex = grdData.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_arrGridData.Count)
            {
                // Set Readonly False to update column data.
                DataTable dataTable = grdData.DataTable;
                int colCnt = dataTable.Columns.Count;
                for (int i = 0; i < colCnt; i++)
                {
                    dataTable.Columns[i].ReadOnly = false;
                }

                // Update columns data
                string hatchNo = CommonUtility.GetComboboxSelectedValue(cboHatch);
                string hatchDrtCd = CommonUtility.GetComboboxSelectedValue(cboAPFP);
                string topClean = CommonUtility.GetComboboxSelectedValue(cboTopClean);

                VORDryBreakBulkItem item = (VORDryBreakBulkItem)m_arrGridData[currRowIndex];
                item.hatchNo = hatchNo;
                item.hatchDrtCd = hatchDrtCd;
                item.topClean = topClean;
                item.workStDt = txtStartTime.Text;
                item.workEndDt = txtEndTime.Text;
                item.rsDivCd = "EQ";
                item.vslCallId = txtJPVC.Text;
                item.cgTpCd = rbtnBreak.Checked ? "BBK" : "DBK";
                item.workYmd = UserInfo.getInstance().Workdate;
                item.userId = UserInfo.getInstance().UserId;
                item.shftId = UserInfo.getInstance().Shift;
                if (Constants.ITEM_OLD.Equals(grdData.DataTable.Rows[currRowIndex][HEADER_ITEM_STATUS].ToString()))
                {
                    item.CRUD = Constants.WS_UPDATE;
                }

                DataRow row = grdData.DataTable.Rows[currRowIndex];
                if (Constants.ITEM_OLD.Equals(grdData.DataTable.Rows[currRowIndex][HEADER_ITEM_STATUS].ToString()))
                {
                    row[HEADER_WS_NM] = Constants.WS_NM_UPDATE;
                }
                row[HEADER_HATCH] = hatchNo;
                row[HEADER_APFP] = hatchDrtCd;
                row[HEADER_BULK] = rbtnBreak.Checked ? "BBK" : "DBK";
                row[HEADER_TOPCLEAN] = topClean;
                row[HEADER_STTIME] = txtStartTime.Text;
                row[HEADER_ENDTIME] = txtEndTime.Text;
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
            int currRowIndex = grdData.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_arrGridData.Count)
            {
                // In case item status is NEW: remove this row from grid.
                // In case item status is OLD: change WORKING STATUS of this row.
                string itemStatus = grdData.DataTable.Rows[currRowIndex][HEADER_ITEM_STATUS].ToString();
                if (Constants.ITEM_NEW.Equals(itemStatus))
                {
                    m_arrGridData.RemoveAt(currRowIndex);

                    grdData.DataTable.AcceptChanges();
                    grdData.DataTable.Rows.RemoveAt(currRowIndex);
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

                    // Change WORKING STATUS of this row.
                    VORDryBreakBulkItem item = (VORDryBreakBulkItem)m_arrGridData[currRowIndex];
                    item.CRUD = Constants.WS_DELETE;

                    DataRow row = grdData.DataTable.Rows[currRowIndex];
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

        // Get returned hatches, eg: "H1,H2,H3"
        private void ReturnInfo()
        {
            m_result = new HVO103Result();
            string strHatches = "";
            if (m_arrGridData != null)
            {
                // Returned hatches, eg: "H1,H2,H3"
                for (int i = 0; i < m_arrGridData.Count; i++)
                {
                    VORDryBreakBulkItem item = (VORDryBreakBulkItem)m_arrGridData[i];
                    if (!Constants.WS_DELETE.Equals(item.CRUD))
                    {
                        string hatchNo = item.hatchNo;
                        if (!string.IsNullOrEmpty(strHatches))
                        {
                            strHatches = strHatches + "," + hatchNo;
                        }
                        else
                        {
                            strHatches = strHatches + hatchNo;
                        }
                    }
                }
            }
            m_result.Hatches = strHatches;
        }

        private void ClearForm()
        {
            grdData.IsDirty = false;

            m_arrGridData = new ArrayList();
            m_result = null;
            rbtnBreak.Checked = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
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
                        ProcessVORDryBreakBulkCUD();
                        ReturnInfo();
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
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
                            ProcessVORDryBreakBulkCUD();
                            ReturnInfo();
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
                    if (this.validations(this.Controls) && 
                        this.Validate())
                    {
                        // Check constraints: HATCH
                        if (!IsExistAlready(Constants.MODE_ADD))
                        {
                            AddItem();
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO103_0001"));
                            cboHatch.Focus();
                        }
                    }
                    break;

                case "btnUpdate":
                    if (this.validations(this.Controls) &&
                        this.Validate())
                    {
                        // Check constraints: HATCH
                        if (!IsExistAlready(Constants.MODE_UPDATE))
                        {
                            UpdateItem();
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO103_0001"));
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

        private void grdData_CurrentCellChanged(object sender, EventArgs e)
        {
            int index = grdData.CurrentRowIndex;
            if (index > -1 && index < grdData.DataTable.Rows.Count)
            {
                string strBulk = grdData.DataTable.Rows[index][HEADER_BULK].ToString();
                string strTopClean = grdData.DataTable.Rows[index][HEADER_TOPCLEAN].ToString();
                string strHatch = grdData.DataTable.Rows[index][HEADER_HATCH].ToString();
                string strAPFP = grdData.DataTable.Rows[index][HEADER_APFP].ToString();
                string strStartTime = grdData.DataTable.Rows[index][HEADER_STTIME].ToString();
                string strEndTime = grdData.DataTable.Rows[index][HEADER_ENDTIME].ToString();

                if ("BBK".Equals(strBulk))
                {
                    rbtnBreak.Checked = true;
                }
                else if ("DBK".Equals(strBulk))
                {
                    rbtnDry.Checked = true;
                }
                CommonUtility.SetComboboxSelectedItem(cboTopClean, strTopClean);
                CommonUtility.SetComboboxSelectedItem(cboHatch, strHatch);
                CommonUtility.SetComboboxSelectedItem(cboAPFP, strAPFP);
                CommonUtility.SetDTPValueDMYHM(txtStartTime, strStartTime);
                CommonUtility.SetDTPValueDMYHM(txtEndTime, strEndTime);

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

        private void RadiobuttonAction(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            String radioBtnName = radioButton.Name;
            switch (radioBtnName)
            {
                case "rbtnBreak":
                    cboTopClean.Enabled = false;
                    break;

                case "rbtnDry":
                    cboTopClean.Enabled = true;
                    break;
            }
        }
    }
}