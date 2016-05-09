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
        private const string HEADER_DPTAGENT = "dptAgent";
        private const string HEADER_VSL_SHF_SEQ = "vslShiftingSeq";
        private const string HEADER_SHFT = "SHFT";

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

            /*----------------------------*/
            //OGA CR - William - 2014.12.30
            setOGA(txtJPVC.Text);
            /*----------------------------*/

            this.ShowDialog();
            return m_result;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_ITEM_STATUS, "0" }, { HEADER_WS_NM, "40" }, { HEADER_SHFT, "50" }, { HEADER_BULK, "30" }, { HEADER_HATCH, "40" }, { HEADER_APFP, "40" }, { HEADER_TOPCLEAN, "40" }, { HEADER_STTIME, "95" }, { HEADER_ENDTIME, "95" }, { HEADER_DPTAGENT, "95" }, { HEADER_SEQ, "0" }, { HEADER_VSL_SHF_SEQ, "0" } };
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

                #region SA Change
                if (m_parm.JpvcInfo.DeprSaId != m_parm.JpvcInfo.ArrvSaId)
                {
                    cboSAChange.Items.Add(new ComboboxValueDescriptionPair(m_parm.JpvcInfo.DeprSaId, m_parm.JpvcInfo.DeprSaId));
                }
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
                    VORDryBreakBulkItem item = (VORDryBreakBulkItem)info.list[0];
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
                        VORDryBreakBulkItem item = (VORDryBreakBulkItem)info.list[i];
                        DataRow newRow = grdData.NewRow();

                        newRow[HEADER_ITEM_STATUS] = Constants.ITEM_OLD;
                        //added by William (2015 July 09) issue 32956
                        if (!string.IsNullOrEmpty(item.vslShiftingSeq) && Int32.Parse(item.vslShiftingSeq) > 0)
                            newRow[HEADER_SHFT] = "SHFT" + item.vslShiftingSeq;
                        else
                            newRow[HEADER_SHFT] = "";
                        newRow[HEADER_WS_NM] = Constants.WS_NM_INITIAL;
                        newRow[HEADER_HATCH] = item.hatchNo;
                        newRow[HEADER_APFP] = item.hatchDrtCd;
                        newRow[HEADER_BULK] = item.cgTpCd;
                        newRow[HEADER_TOPCLEAN] = item.topClean;
                        newRow[HEADER_STTIME] = item.workStDt;
                        newRow[HEADER_ENDTIME] = item.workEndDt;
                        newRow[HEADER_SEQ] = item.seq;
                        newRow[HEADER_DPTAGENT] = item.dptAgent;
                        
                        //Vsl Shifting CR 17 March, 2015
                        newRow[HEADER_VSL_SHF_SEQ] = item.vslShiftingSeq;

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

            //if (!ValidateAtbExist())
            //{
            //    return false;
            //}

            //if (!ValidateTheLatestAtb())
            //{
            //    return false;
            //}
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

        //added by William (2015 March 19) issue 32956
        // ref: CT207
        //In case already have ATB, Start time and End Time have to be greater than ATB
        private bool validCurAtb(string strAtb, string strStart, string strEnd)
        {
            bool result = true;
            DateTime curAtb = CommonUtility.ParseYMDHM(strAtb);
            DateTime startTime = DateTime.Parse(strStart);
            DateTime endTime = DateTime.Parse(strEnd);

            if (!string.IsNullOrEmpty(strStart.ToString()) && startTime.CompareTo(curAtb) < 0)
                result = false;

            if (!string.IsNullOrEmpty(strEnd.ToString()) && endTime.CompareTo(curAtb) < 0)
                result = false;

            return result;
        }

        //In case already have ATU, Start time and End Time have to be less than ATU

        private bool validCurAtu(string strAtu, string strStart, string strEnd)
        {
            bool result = true;
            DateTime curAtu = CommonUtility.ParseYMDHM(strAtu);
            DateTime startTime = DateTime.Parse(strStart);
            DateTime endTime = DateTime.Parse(strEnd);

            if (!string.IsNullOrEmpty(strAtu))
            {
                if (!string.IsNullOrEmpty(strStart.ToString()) && startTime.CompareTo(curAtu) > 0)
                    result = false;

                if (!string.IsNullOrEmpty(strEnd.ToString()) && endTime.CompareTo(curAtu) > 0)
                    result = false;
            }
            return result;
        }

        private void AddItem(string VslShiftingSeq)
        {
            string hatchNo = CommonUtility.GetComboboxSelectedValue(cboHatch);
            string hatchDrtCd = CommonUtility.GetComboboxSelectedValue(cboAPFP);
            string topClean = CommonUtility.GetComboboxSelectedValue(cboTopClean);
            string dptAgent = CommonUtility.GetComboboxSelectedValue(cboSAChange);
            

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
            item.dptAgent = dptAgent;
            item.vslShiftingSeq = VslShiftingSeq;
            m_arrGridData.Add(item);

            DataRow newRow = grdData.NewRow();
            newRow[HEADER_ITEM_STATUS] = Constants.ITEM_NEW;
            newRow[HEADER_WS_NM] = Constants.WS_NM_INSERT;


            if (!string.IsNullOrEmpty(VslShiftingSeq) && Int32.Parse(VslShiftingSeq) > 0)
                newRow[HEADER_SHFT] = "SHFT" + VslShiftingSeq;
            else
                newRow[HEADER_SHFT] = "";

            newRow[HEADER_HATCH] = hatchNo;
            newRow[HEADER_APFP] = hatchDrtCd;
            newRow[HEADER_BULK] = rbtnBreak.Checked ? "BBK" : "DBK";
            newRow[HEADER_TOPCLEAN] = topClean;
            newRow[HEADER_STTIME] = txtStartTime.Text;
            newRow[HEADER_ENDTIME] = txtEndTime.Text;
            //Vsl Shifting CR 17 March, 2015
            newRow[HEADER_DPTAGENT] = dptAgent;

            //Vsl Shifting CR 17 March, 2015
            newRow[HEADER_VSL_SHF_SEQ] = VslShiftingSeq;
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
                string dptAgent = CommonUtility.GetComboboxSelectedValue(cboSAChange);

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

                //Vsl Shifting CR 17 March, 2015
                //item.vslShiftingSeq = m_parm.JpvcInfo.VslShiftingSeq;

                item.dptAgent = dptAgent;
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
                row[HEADER_DPTAGENT] = dptAgent;

                //Vsl Shifting CR 17 March, 2015
                //row[HEADER_VSL_SHF_SEQ] = m_parm.JpvcInfo.VslShiftingSeq;

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

        private bool validationATB()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtJPVC.Text.Trim()))
                {
                    IVesselOperatorProxy proxy = new VesselOperatorProxy();
                    VesselScheduleParm parm = new VesselScheduleParm();
                    parm.jpvcNo = txtJPVC.Text.Trim();

                    ResponseInfo info = proxy.getVesselScheduleListDetail(parm);

                    if (info != null && info.list.Length > 0)
                    {
                        if (info.list[0] is VesselScheduleItem)
                        {
                            string ATB = ((VesselScheduleItem)info.list[0]).atb;
                            if (string.IsNullOrEmpty(ATB))
                            {
                                MessageBox.Show("Please update ATB for this vessel first !", "WARNING !!!");
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            return true;
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
                    
                    if (this.validations(this.Controls) && this.Validate())
                    {
                        // Check constraints: HATCH
                        if (!IsExistAlready(Constants.MODE_ADD))
                        {
                            //if (!validationATB()) return;

                            //added by William (2015 March 18) issue 32956

                            //Get Current ATB, ATU depend on vslShiftingSeq.
                            //If vslShiftingSeq != null, Atb and Atu will be get from vsl shifting
                            //else atb and atu will be get from vsl schedule
                            string currAtb = string.Empty;
                            string currAtu = string.Empty;
                            string VslShiftingSeq = string.Empty;

                            SftDblBankingParm vorParm = new SftDblBankingParm();
                            vorParm.vslCallId = txtJPVC.Text;
                            vorParm.vslShiftingSeq = "";
                            IVesselOperatorProxy vslProxy = new VesselOperatorProxy();
                            ResponseInfo infoResult = vslProxy.getCurrAtbAtu(vorParm);
                            if ((infoResult != null) && (infoResult.list != null) && (infoResult.list.Length == 1) && (infoResult.list[0] is SftDblBankingItem))
                            {
                                currAtb = ((SftDblBankingItem)infoResult.list[0]).atb;
                                currAtu = ((SftDblBankingItem)infoResult.list[0]).atu;
                                VslShiftingSeq = ((SftDblBankingItem)infoResult.list[0]).vslShiftingSeq;


                                //Vessel Shifting CR, 20 March, 2015, William
                                //Validate start time, end time, ATB, ATU
                                if (!string.IsNullOrEmpty(currAtb))
                                {
                                    if (!validCurAtb(currAtb, txtStartTime.Value.ToString(), txtEndTime.Value.ToString()))
                                    {
                                        if (!string.IsNullOrEmpty(VslShiftingSeq))
                                            CommonUtility.AlertMessage(String.Format("Start Time/ End Time must be greater than ATB: {0}. {1}.", currAtb, "Refer to Vessel Shifting screen for more detail"), "WARNING !");
                                        else
                                            CommonUtility.AlertMessage(String.Format("Start Time/ End Time must be greater than ATB: {0}. {1}.", currAtb, "Refer to Vessel Schedule screen for more detail"), "WARNING !");
                                        return;
                                    }

                                    if (!validCurAtu(currAtu, txtStartTime.Value.ToString(), txtEndTime.Value.ToString()))
                                    {
                                        if (!string.IsNullOrEmpty(VslShiftingSeq))
                                            CommonUtility.AlertMessage(String.Format("Start Time/ End Time must be less than ATU: {0}. {1}.", currAtu, "Refer to Vessel Shifting screen for more detail"), "WARNING !");
                                        else
                                            CommonUtility.AlertMessage(String.Format("Start Time/ End Time must be less than ATU: {0}. {1}.", currAtu, "Refer to Vessel Schedule screen for more detail"), "WARNING !");
                                        return;
                                    }

                                    //OGA CR
                                    if (_flag_showMsg != 0)
                                    {
                                        DialogResult result = MessageBox.Show(_msg_Detail, "OGA Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                        if (result == DialogResult.Yes)
                                        {
                                            //this.btnAdd.Enabled = true;
                                            grdData.IsDirty = true;
                                        }
                                        else
                                        {
                                            //this.btnAdd.Enabled = false;
                                            grdData.IsDirty = false;
                                            return;
                                        }
                                    }

                                    //Vsl Shifting CR 17 March, 2015
                                    if (!string.IsNullOrEmpty(VslShiftingSeq))
                                    {
                                        if (MessageBox.Show("You are doing operation for Vessel Shifting case. Do you want to continue?", "ALERT", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                        { AddItem(VslShiftingSeq); }
                                    }
                                    else
                                    {
                                        //Nomal case, 

                                        AddItem("");
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(VslShiftingSeq))
                                        CommonUtility.AlertMessage(String.Format("Please update ATB for this Vessel first. {0}.", "Refer to Vessel Shifting screen for more detail"), "WARNING !");
                                    else
                                        CommonUtility.AlertMessage(String.Format("Please update ATB for this Vessel first. {0}.", "Refer to Vessel Schedule screen for more detail"), "WARNING !");
                                    return;
                                }
                            }
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

                            //Vessel Shifting CR 18 March, 2015 - William

                            //Get Current ATB, ATU depend on vslShiftingSeq.
                            //If vslShiftingSeq != null, Atb and Atu will be get from vsl shifting
                            //else atb and atu will be get from vsl schedule
                            string currAtb = string.Empty;
                            string currAtu = string.Empty;
                            string VslShiftingSeq = string.Empty;
                            int currRowIndex = grdData.CurrentRowIndex;
                            SftDblBankingParm vorParm = new SftDblBankingParm();
                            vorParm.vslCallId = txtJPVC.Text;
                            if (grdData.DataTable.Rows[currRowIndex][HEADER_VSL_SHF_SEQ] != null)
                                vorParm.vslShiftingSeq = grdData.DataTable.Rows[currRowIndex][HEADER_VSL_SHF_SEQ].ToString();
                            else
                                vorParm.vslShiftingSeq = "";

                            IVesselOperatorProxy vslProxy = new VesselOperatorProxy();
                            ResponseInfo infoResult = vslProxy.getCurrAtbAtu(vorParm);
                            if ((infoResult != null) && (infoResult.list != null) && (infoResult.list.Length == 1) && (infoResult.list[0] is SftDblBankingItem))
                            {
                                currAtb = ((SftDblBankingItem)infoResult.list[0]).atb;
                                currAtu = ((SftDblBankingItem)infoResult.list[0]).atu;
                                VslShiftingSeq = ((SftDblBankingItem)infoResult.list[0]).vslShiftingSeq;
                            }
                            if (!validCurAtb(currAtb, txtStartTime.Value.ToString(), txtEndTime.Value.ToString()))
                            {
                                if (!string.IsNullOrEmpty(VslShiftingSeq))
                                    CommonUtility.AlertMessage(String.Format("Start Time/ End Time must be greater than ATB: {0}. {1}.", currAtb, "Refer to Vessel Shifting screen for more detail"), "WARNING !");
                                else
                                    CommonUtility.AlertMessage(String.Format("Start Time/ End Time must be greater than ATB: {0}. {1}.", currAtb, "Refer to Vessel Schedule screen for more detail"), "WARNING !");
                                return;
                            }

                            if (!validCurAtu(currAtu, txtStartTime.Value.ToString(), txtEndTime.Value.ToString()))
                            {
                                if (!string.IsNullOrEmpty(VslShiftingSeq))
                                    CommonUtility.AlertMessage(String.Format("Start Time/ End Time must be less than ATU: {0}. {1}.", currAtu, "Refer to Vessel Shifting screen for more detail"), "WARNING !");
                                else
                                    CommonUtility.AlertMessage(String.Format("Start Time/ End Time must be less than ATU: {0}. {1}.", currAtu, "Refer to Vessel Schedule screen for more detail"), "WARNING !");
                                return;
                            } 

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

        //William - OGA CR 2014.12.30

        private SearchJPVCResult m_jpvcResult;
        private int _flag_showMsg = 0;
        private String _msg_Detail = string.Empty;
        String _ogaStatus = "";
        String _ogaDateTime = "";
        String _ogaQuarantine = "";

        private void viewOGAStatus(String ogaStatus, String dateTime, string quarantine)
        {
            this.tLabelOGAStatus.Text = ogaStatus;
            this.tLabelDatetime.Text = dateTime;
            this.tLabelQuanrantine.Text = quarantine;
        }
        private void setMessage(int flag_showMsg, String msg_Detail)
        {
            _flag_showMsg = flag_showMsg;
            _msg_Detail = msg_Detail;
        }
        private void setOGA(String vslCallId)
        {
            if (CommonUtility.IsValidJPVC(vslCallId, ref m_jpvcResult))
            {
                _ogaStatus = m_jpvcResult.OgaStatus.Trim();

                if (string.IsNullOrEmpty(m_jpvcResult.OgaStatusDate))
                    _ogaDateTime = "_ _ _";
                else
                    _ogaDateTime = m_jpvcResult.OgaStatusDate.Trim();

                _ogaQuarantine = m_jpvcResult.OgaQuarantine.Trim();

                #region Scenario 1
                if (_ogaStatus.Equals("N/A"))
                {
                    viewOGAStatus("N/A", "_ _ _", "IN PROGRESS");
                    setMessage(1, "Application for health clerance is N/A.\nVessel is IN PROGRESS.\nDo you want to continue?");

                    return;
                }
                #endregion Scenario 1

                #region Scenario 2
                else if (_ogaStatus.Equals("IN PROGRESS") && (!_ogaQuarantine.Equals("QUARANTINE AT ANCHORAGE")) && (!_ogaQuarantine.Equals("QUARANTINE AT WHARF")))
                {
                    viewOGAStatus(_ogaStatus, _ogaDateTime, "IN PROGRESS");
                    setMessage(2, "Application for health clerance is IN PROGRESS.\nVessel is IN PROGRESS.\nDo you want to continue?");

                    return;
                }
                #endregion Scenario 2

                #region Scenario 3
                else if (_ogaStatus.Equals("IN PROGRESS") && (_ogaQuarantine.Equals("QUARANTINE AT WHARF")))
                {
                    viewOGAStatus(_ogaStatus, _ogaDateTime, _ogaQuarantine);
                    setMessage(3, "Application for health clerance is IN PROGRESS.\nVessel is QUARANTINE AT WHARF.\nDo you want to continue?");

                    return;
                }
                #endregion Scenario 3

                #region Scenario 4
                else if (_ogaStatus.Equals("IN PROGRESS") && (_ogaQuarantine.Equals("QUARANTINE AT ANCHORAGE")))
                {
                    viewOGAStatus(_ogaStatus, _ogaDateTime, _ogaQuarantine);
                    _flag_showMsg = 4;
                    setMessage(4, "Application for health clerance is IN PROGRESS.\nVessel is QUARANTINE AT ANCHORAGE.\nDo you want to continue?");

                    return;
                }
                #endregion Scenario 4

                #region Scenario 5
                else if (_ogaStatus.Equals("APPROVE")) // && (!_ogaQuarantine.Equals("QUARANTINE AT ANCHORAGE")) && (!_ogaQuarantine.Equals("QUARANTINE AT WHARF")))
                {
                    viewOGAStatus(_ogaStatus, _ogaDateTime, "");
                    setMessage(0, string.Empty);

                    return;
                }
                #endregion Scenario 5

                /*#region Scenario 6
                else if (_ogaStatus.Equals("APPROVE") && (_ogaQuarantine.Equals("QUARANTINE AT WHARF")))
                {
                    viewOGAStatus(_ogaStatus, _ogaDateTime, _ogaQuarantine);
                    setMessage(0, string.Empty);

                    return;
                }
                #endregion Scenario 6

                #region Scenario 7
                else if (_ogaStatus.Equals("APPROVE") && (_ogaQuarantine.Equals("QUARANTINE AT ANCHORAGE")))
                {
                    viewOGAStatus(_ogaStatus, _ogaDateTime, _ogaQuarantine);
                    setMessage(0, string.Empty);

                    return;
                }
                #endregion Scenario 7
                */
                #region Scenario 8
                else if (_ogaStatus.Equals("REJECT") && (!_ogaQuarantine.Equals("QUARANTINE AT ANCHORAGE")) && (!_ogaQuarantine.Equals("QUARANTINE AT WHARF")))
                {
                    viewOGAStatus(_ogaStatus, _ogaDateTime, "IN PROGRESS");
                    setMessage(8, "Application for health clerance is REJECT.\nVessel is IN PROGRESS.\nDo you want to continue?");

                    return;
                }
                #endregion Scenario 8

                #region Scenario 9
                else if (_ogaStatus.Equals("REJECT") && (_ogaQuarantine.Equals("QUARANTINE AT WHARF")))
                {
                    viewOGAStatus(_ogaStatus, _ogaDateTime, _ogaQuarantine);
                    setMessage(9, "Application for health clerance is REJECT.\nVessel is QUARANTINE AT WHARF.\nDo you want to continue?");

                    return;
                }
                #endregion Scenario 9

                #region Scenario 10
                else if (_ogaStatus.Equals("REJECT") && (_ogaQuarantine.Equals("QUARANTINE AT ANCHORAGE")))
                {
                    viewOGAStatus(_ogaStatus, _ogaDateTime, _ogaQuarantine);
                    setMessage(10, "Application for health clerance is REJECT.\nVessel is QUARANTINE AT ANCHORAGE.\nDo you want to continue?");

                    return;
                }
                #endregion Scenario 10

                #region Scenario 11
                else if (_ogaStatus.Equals("HOLD") && (!_ogaQuarantine.Equals("QUARANTINE AT ANCHORAGE")) && (!_ogaQuarantine.Equals("QUARANTINE AT WHARF")))
                {
                    viewOGAStatus(_ogaStatus, _ogaDateTime, "IN PROGRESS");
                    setMessage(11, "Application for health clerance is HOLD.\nVessel is IN PROGRESS.\nDo you want to continue?");

                    return;
                }
                #endregion Scenario 11

                #region Scenario 12
                else if (_ogaStatus.Equals("HOLD") && (_ogaQuarantine.Equals("QUARANTINE AT WHARF")))
                {
                    viewOGAStatus(_ogaStatus, _ogaDateTime, _ogaQuarantine);
                    setMessage(12, "Application for health clerance is HOLD.\nVessel QUARANTINE AT WHARF.\nDo you want to continue?");

                    return;
                }
                #endregion Scenario 12

                #region Scenario 13
                else if (_ogaStatus.Equals("HOLD") && (_ogaQuarantine.Equals("QUARANTINE AT ANCHORAGE")))
                {
                    viewOGAStatus(_ogaStatus, _ogaDateTime, _ogaQuarantine);
                    setMessage(13, "Application for health clerance is HOLD.\nVessel QUARANTINE AT ANCHORAGE.\nDo you want to continue?");

                    return;
                }
                #endregion Scenario 12
            }
            else
            {
                MessageBox.Show("Please check your JPVC !");
            }
        }
    }
}