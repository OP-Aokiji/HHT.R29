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
using MOST.Client.Proxy.VesselOperatorProxy;
using MOST.Client.Proxy.ApronCheckerProxy;

namespace MOST.VesselOperator
{
    public partial class HVO104 : TForm, IPopupWindow
    {
        private const string HEADER_ITEM_STATUS = "ITEM_STATUS";
        private const string HEADER_WS_NM = "Status";
        private const string HEADER_BULK = "Bulk";
        private const string HEADER_APFP = "AP/FP";
        private const string HEADER_HATCH = "Hatch";
        private const string HEADER_EQU_CD = "_EQU";
        private const string HEADER_EQU_NM = "EQU";
        private const string HEADER_FAC_CD = "_FAC";
        private const string HEADER_FAC_NM = "FAC";
        private const string HEADER_STTIME = "StartTime";
        private const string HEADER_ENDTIME = "EndTime";
        private const string HEADER_REMARK = "Remark";
        private const string HEADER_SEQ = "_SEQ";

        private HVO104Parm m_parm;
        private HVO104Result m_result;
        private ArrayList m_arrGridData;

        public HVO104()
        {
            m_arrGridData = new ArrayList();
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            rbtnBreak.Checked = true;

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            cboFac.Enabled = false;
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HVO104Parm)parm;
            txtJPVC.Text = m_parm.JpvcInfo.Jpvc;
            InitializeData();
            this.ShowDialog();
            return m_result;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_ITEM_STATUS, "0" }, { HEADER_WS_NM, "40" }, { HEADER_BULK, "30" }
                        , { HEADER_HATCH, "35" },{ HEADER_APFP, "35" }, { HEADER_EQU_CD, "0" }, { HEADER_EQU_NM, "50" }
                        , { HEADER_FAC_CD, "0" }, { HEADER_FAC_NM, "50" }, { HEADER_STTIME, "95" }, { HEADER_ENDTIME, "95" }
                        , { HEADER_REMARK, "60" }, { HEADER_SEQ, "0" } };
            this.grdData.setHeader(header);
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Equipment, Facility
                GetVORDryBreakBulkCommonCd();
                #endregion

                #region Hatch, Grid Data
                GetVORDryBreakBulk();
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
                CommonUtility.InitializeCombobox(cboHatch);
                if (info != null)
                {
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is VORDryBreakBulkItem)
                        {
                            // Grid Data
                            VORDryBreakBulkItem item = (VORDryBreakBulkItem) info.list[i];
                            DataRow newRow = grdData.NewRow();
                            newRow[HEADER_ITEM_STATUS] = Constants.ITEM_OLD;
                            newRow[HEADER_WS_NM] = Constants.WS_NM_INITIAL;
                            newRow[HEADER_BULK] = item.cgTpCd;
                            newRow[HEADER_APFP] = item.hatchDrtCd;
                            newRow[HEADER_HATCH] = item.hatchNo;
                            newRow[HEADER_EQU_CD] = item.eqFacNo;
                            newRow[HEADER_EQU_NM] = item.eqFacNm;
                            newRow[HEADER_FAC_CD] = item.facility;
                            newRow[HEADER_FAC_NM] = item.facilityName;
                            newRow[HEADER_STTIME] = item.workStDt;
                            newRow[HEADER_ENDTIME] = item.workEndDt;
                            newRow[HEADER_REMARK] = item.remark;
                            newRow[HEADER_SEQ] = item.seq;
                            grdData.Add(newRow);
                            m_arrGridData.Add(item);

                            // Hatch info
                            cboHatch.Items.Add(new ComboboxValueDescriptionPair(item.hatchNo, item.hatchNo));
                        }
                    }
                }

                grdData.IsDirty = false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private void GetVORDryBreakBulkCommonCd()
        {
            try
            {
                // Equipment
                IVesselOperatorProxy acProxy = new VesselOperatorProxy();
                VORDryBreakBulkParm parm = new VORDryBreakBulkParm();
                parm.searchType = "equipment";
                parm.vslCallId = txtJPVC.Text;
                parm.workYmd = UserInfo.getInstance().Workdate;
                parm.shift = UserInfo.getInstance().Shift;
                ResponseInfo info = acProxy.getVORDryBreakBulkCommonCd(parm);
                CommonUtility.InitializeCombobox(cboEQU);
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is VesselOperationReportItem)
                    {
                        VesselOperationReportItem item = (VesselOperationReportItem) info.list[i];
                        cboEQU.Items.Add(new ComboboxValueDescriptionPair(item.eqFacNo, item.eqFacNm));
                    }
                }

                // Facility
                parm = new VORDryBreakBulkParm();
                parm.searchType = "HHTfacility";
                parm.vslCallId = txtJPVC.Text;
                parm.workYmd = UserInfo.getInstance().Workdate;
                parm.shift = UserInfo.getInstance().Shift;
                info = acProxy.getVORDryBreakBulkCommonCd(parm);
                CommonUtility.InitializeCombobox(cboFac);
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is VesselOperationReportItem)
                    {
                        VesselOperationReportItem item = (VesselOperationReportItem) info.list[i];
                        cboFac.Items.Add(new ComboboxValueDescriptionPair(item.eqFacNo, item.eqFacNm));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
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
                string eqFacNo = CommonUtility.GetComboboxSelectedValue(cboEQU);
                string eqFacNm = CommonUtility.GetComboboxSelectedDescription(cboEQU);
                if (rbtnDry.Checked &&
                    String.IsNullOrEmpty((eqFacNm ?? String.Empty).Trim()))  // Fix issue 0031427 
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO104_0001"));
                    return;
                }
                string facCd = CommonUtility.GetComboboxSelectedValue(cboFac);
                string facNm = CommonUtility.GetComboboxSelectedDescription(cboFac);

                VORDryBreakBulkItem item = (VORDryBreakBulkItem)m_arrGridData[currRowIndex];
                item.cgTpCd = rbtnBreak.Checked ? "BBK" : "DBK";
                item.hatchNo = hatchNo;
                item.eqFacNo = eqFacNo;
                item.eqFacNm = eqFacNm;
                item.facility = facCd;
                item.facilityName = facNm;
                item.rsDivCd = "EQ";
                item.eqTpCd = rbtnDry.Checked ? "FC" : "";
                item.remark = txtRemark.Text;
                item.workYmd = UserInfo.getInstance().Workdate;
                item.userId = UserInfo.getInstance().UserId;
                item.shftId = UserInfo.getInstance().Shift;
                item.CRUD = Constants.WS_UPDATE;

                // tnkytn: for testing. Will be deleted after server's change. Ref: QA_20090623_2
                if (!string.IsNullOrEmpty(item.workStDt))
                {
                    item.workStDt = item.workStDt.Trim();
                }
                if (!string.IsNullOrEmpty(item.workEndDt))
                {
                    item.workEndDt = item.workEndDt.Trim();
                }


                DataRow row = grdData.DataTable.Rows[currRowIndex];
                if (Constants.ITEM_OLD.Equals(grdData.DataTable.Rows[currRowIndex][HEADER_ITEM_STATUS].ToString()))
                {
                    row[HEADER_WS_NM] = Constants.WS_NM_UPDATE;
                }
                row[HEADER_BULK] = rbtnBreak.Checked ? "BBK" : "DBK";
                row[HEADER_HATCH] = hatchNo;
                row[HEADER_EQU_CD] = eqFacNo;
                row[HEADER_EQU_NM] = eqFacNm;
                row[HEADER_FAC_CD] = facCd;
                row[HEADER_FAC_NM] = facNm;
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

        private void ProcessVORDryBreakBulkCUD()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                ArrayList arrObj = new ArrayList();

                if (m_arrGridData != null)
                {
                    for (int i = 0; i < m_arrGridData.Count; i++)
                    {
                        VORDryBreakBulkItem item = (VORDryBreakBulkItem)m_arrGridData[i];

                        // Process only items that were modified.
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
            m_result = new HVO104Result();

            // Returned equipments, eg: "BG1,BG2,LL1"
            string strEquipments = "";
            if (m_arrGridData != null)
            {
                for (int i = 0; i < m_arrGridData.Count; i++)
                {
                    VORDryBreakBulkItem item = (VORDryBreakBulkItem)m_arrGridData[i];

                    // Get returned equipments, eg: "BG1,BG2,LL1"
                    if (!Constants.WS_DELETE.Equals(item.CRUD))
                    {
                        string eqFacNm = item.eqFacNm;
                        if (!string.IsNullOrEmpty(eqFacNm) && !string.IsNullOrEmpty(eqFacNm.Trim()))
                        {
                            if (!string.IsNullOrEmpty(strEquipments))
                            {
                                strEquipments = strEquipments + "," + eqFacNm;
                            }
                            else
                            {
                                strEquipments = strEquipments + eqFacNm;
                            }
                        }
                    }
                }
            }
            m_result.Equipments = strEquipments;
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

                case "btnUpdate":
                    if (this.validations(this.Controls))
                    {
                        if (!validationATB()) return;
                        UpdateItem();
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
            int currRowIndex = grdData.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_arrGridData.Count)
            {
                string strBulk = grdData.DataTable.Rows[currRowIndex][HEADER_BULK].ToString();
                string strHatch = grdData.DataTable.Rows[currRowIndex][HEADER_HATCH].ToString();
                string strEqu = grdData.DataTable.Rows[currRowIndex][HEADER_EQU_CD].ToString();
                string strFac = grdData.DataTable.Rows[currRowIndex][HEADER_FAC_CD].ToString();
                string strRmk = grdData.DataTable.Rows[currRowIndex][HEADER_REMARK].ToString();

                txtRemark.Text = strRmk;
                if ("BBK".Equals(strBulk))
                {
                    rbtnBreak.Checked = true;
                }
                else if ("DBK".Equals(strBulk))
                {
                    rbtnDry.Checked = true;
                }
                CommonUtility.SetComboboxSelectedItem(cboHatch, strHatch);
                CommonUtility.SetComboboxSelectedItem(cboEQU, strEqu);
                CommonUtility.SetComboboxSelectedItem(cboFac, strFac);

                // Disable UPDATE, DELETE buttons if this row was marked as deleted.
                string strWorkingStatus = grdData.DataTable.Rows[currRowIndex][HEADER_WS_NM].ToString();
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

        private void RadioListener(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            String radioBtnName = radioButton.Name;
            switch (radioBtnName)
            {
                case "rbtnBreak":
                    cboFac.Enabled = false;
                    break;

                case "rbtnDry":
                    cboFac.Enabled = true;
                    break;
            }
        }
    }
}