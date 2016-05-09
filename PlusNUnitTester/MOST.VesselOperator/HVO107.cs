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
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using Framework.Service.Provider.WebService.Provider;
using MOST.Client.Proxy.VesselOperatorProxy;

namespace MOST.VesselOperator
{
    public partial class HVO107 : TForm, IPopupWindow
    {
        private const string HEADER_ITEM_STATUS = "_ITEM_STATUS";
        private const string HEADER_WS_NM = "Status";
        private const string HEADER_HATCH = "Hatch";
        private const string HEADER_APFP = "AP/FP";
        private const string HEADER_EQU = "EQU";
        private const string HEADER_DLY_CD = "DelayCode";
        private const string HEADER_ACCEPTEDDLY = "Accepted Delay";
        private const string HEADER_STARTTIME = "StartTime";
        private const string HEADER_ENDTIME = "EndTime";
        private const string HEADER_REMARK = "Remark";
        private const string HEADER_INPTDT = "_INPTDT";
        private const string HEADER_SEQ = "_SEQ";

        private HVO107Parm m_parm;

        public HVO107()
        {
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
            m_parm = (HVO107Parm)parm;
            txtJPVC.Text = m_parm.JpvcInfo.Jpvc;
            InitializeData();
            this.ShowDialog();
            return null;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_ITEM_STATUS, "0" }, { HEADER_WS_NM, "40" }, { HEADER_HATCH, "40" }, { HEADER_APFP, "30" }, { HEADER_EQU, "40" }, { HEADER_DLY_CD, "40" }, { HEADER_ACCEPTEDDLY, "40" }, { HEADER_STARTTIME, "110" }, { HEADER_ENDTIME, "110" }, { HEADER_REMARK, "60" }, { HEADER_INPTDT, "0" }, { HEADER_SEQ, "0" } };
            this.grdData.setHeader(header);
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Hatch
                CommonUtility.SetHatchInfo(cboHatch);
                #endregion

                #region AP/FP
                CommonUtility.SetHatchDirectionAPFP(cboAPFP);
                #endregion

                #region Equipment
                // Get declared equipment
                CommonUtility.GetDeclaredEqu(txtJPVC.Text, cboEqu);
                #endregion

                #region Grid Data
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
                VesselDelayRecordParm delayParm = new VesselDelayRecordParm();
                delayParm.searchType = "HHT_DelayRecordList";
                delayParm.vslCallId = txtJPVC.Text;
                delayParm.stDt = UserInfo.getInstance().Workdate;
                delayParm.shftId = UserInfo.getInstance().Shift;
                ResponseInfo delayInfo = delayProxy.getDelayRecordList(delayParm);

                grdData.Clear();
                if (delayInfo != null)
                {
                    for (int i = 0; i < delayInfo.list.Length; i++)
                    {
                        if (delayInfo.list[i] is VesselDelayRecordItem)
                        {
                            VesselDelayRecordItem item = (VesselDelayRecordItem)delayInfo.list[i];
                            DataRow newRow = grdData.NewRow();
                            newRow[HEADER_ITEM_STATUS] = Constants.ITEM_OLD;
                            newRow[HEADER_WS_NM] = Constants.WS_NM_INITIAL;
                            newRow[HEADER_HATCH] = item.hatchNo;
                            newRow[HEADER_APFP] = item.hatchDrtCd;
                            newRow[HEADER_EQU] = item.eqNo;
                            newRow[HEADER_DLY_CD] = item.rsnCd;
                            newRow[HEADER_ACCEPTEDDLY] = item.acptYN;
                            newRow[HEADER_STARTTIME] = item.stDt;
                            newRow[HEADER_ENDTIME] = item.endDt;
                            newRow[HEADER_REMARK] = item.rmk;
                            newRow[HEADER_INPTDT] = item.inptDt;
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

        private bool ProcessDelayRecordItems()
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
                            // ref: CT101
                            VesselDelayRecordItem item = new VesselDelayRecordItem();
                            item.vslCallId = txtJPVC.Text;
                            item.hatchNo = grdData.DataTable.Rows[i][HEADER_HATCH].ToString();
                            item.hatchDrtCd = grdData.DataTable.Rows[i][HEADER_APFP].ToString();
                            item.eqNo = grdData.DataTable.Rows[i][HEADER_EQU].ToString();
                            item.rsnCd = grdData.DataTable.Rows[i][HEADER_DLY_CD].ToString();
                            item.acptYN = chkAcceptDelay.Checked ? "Y" : "N";
                            item.stDt = grdData.DataTable.Rows[i][HEADER_STARTTIME].ToString();
                            item.endDt = grdData.DataTable.Rows[i][HEADER_ENDTIME].ToString();
                            item.rmk = grdData.DataTable.Rows[i][HEADER_REMARK].ToString();
                            item.inptDt = grdData.DataTable.Rows[i][HEADER_INPTDT].ToString();
                            //item.acptYN
                            //item.totalHRS
                            //item.rmk
                            //item.rsnCdNm
                            if (!string.IsNullOrEmpty(grdData.DataTable.Rows[i][HEADER_SEQ].ToString()))
                            {
                                item.seq = CommonUtility.ParseInt(grdData.DataTable.Rows[i][HEADER_SEQ].ToString());
                            }


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

                    proxy.processDelayRecordItems(dataCollection);
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
            newRow[HEADER_HATCH] = CommonUtility.GetComboboxSelectedValue(cboHatch);
            newRow[HEADER_APFP] = CommonUtility.GetComboboxSelectedValue(cboAPFP);
            newRow[HEADER_EQU] = CommonUtility.GetComboboxSelectedValue(cboEqu);
            newRow[HEADER_DLY_CD] = txtDelayCode.Text;
            newRow[HEADER_ACCEPTEDDLY] = chkAcceptDelay.Checked ? "Y" : "N";
            newRow[HEADER_STARTTIME] = txtStartTime.Text;
            newRow[HEADER_ENDTIME] = txtEndTime.Text;
            newRow[HEADER_REMARK] = txtRemark.Text;
            newRow[HEADER_INPTDT] = UserInfo.getInstance().Workdate;
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
                row[HEADER_HATCH] = CommonUtility.GetComboboxSelectedValue(cboHatch);
                row[HEADER_APFP] = CommonUtility.GetComboboxSelectedValue(cboAPFP);
                row[HEADER_EQU] = CommonUtility.GetComboboxSelectedValue(cboEqu);
                row[HEADER_DLY_CD] = txtDelayCode.Text;
                row[HEADER_ACCEPTEDDLY] = chkAcceptDelay.Checked ? "Y" : "N";
                row[HEADER_STARTTIME] = txtStartTime.Text;
                row[HEADER_ENDTIME] = txtEndTime.Text;
                row[HEADER_REMARK] = txtRemark.Text;
                row[HEADER_INPTDT] = UserInfo.getInstance().Workdate;
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

        private bool Validate()
        {
            try
            {   
                // Check if user does input hatch or EQU
                if (cboHatch.SelectedIndex < 1 && cboEqu.SelectedIndex < 1)
                {
                    CommonUtility.AlertMessage("Please input Hatch or EQU");
                    cboHatch.Focus();
                    return false;
                }

                // Validate inputted delay code
                ICommonProxy proxy = new CommonProxy();
                CommonCodeParm commonParm = new CommonCodeParm();
                commonParm.searchType = "DLYCD";
                commonParm.lcd = "BULK";
                commonParm.tyCd = "CD";
                commonParm.cd = txtDelayCode.Text;
                ResponseInfo info = proxy.getCommonCodeList(commonParm);
                if (info == null || info.list == null || info.list.Length < 1 || 
                    !(info.list[0] is CodeMasterListItem1 || info.list[0] is CodeMasterListItem))
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

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnF1":
                    DelayCodeListParm delayParm = new DelayCodeListParm();
                    DelayCodeListResult delayResult = (DelayCodeListResult)PopupManager.instance.ShowPopup(new MOST.Common.HCM102(MOST.Common.HCM102.TYPE_BULK), delayParm);
                    if (delayResult != null)
                    {
                        txtDelayCode.Text = delayResult.Code;
                        chkAcceptDelay.Checked = "Y".Equals(delayResult.AcceptedDelay);
                    }
                    break;

                case "btnOk":
                    if (grdData.IsDirty)
                    {
                        if (ProcessDelayRecordItems())
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
                            ProcessDelayRecordItems();
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
                        AddItem();
                    }
                    break;

                case "btnUpdate":
                    if (this.validations(this.Controls) && this.Validate())
                    {
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
            int index = grdData.CurrentRowIndex;
            if (index > -1 && index < grdData.DataTable.Rows.Count)
            {
                string strHatch = grdData.DataTable.Rows[index][HEADER_HATCH].ToString();
                string strApfp = grdData.DataTable.Rows[index][HEADER_APFP].ToString();
                string strEqu = grdData.DataTable.Rows[index][HEADER_EQU].ToString();
                string strDlyCd = grdData.DataTable.Rows[index][HEADER_DLY_CD].ToString();
                string strAcceptedDelay = grdData.DataTable.Rows[index][HEADER_ACCEPTEDDLY].ToString();
                string strStartTime = grdData.DataTable.Rows[index][HEADER_STARTTIME].ToString();
                string strEndTime = grdData.DataTable.Rows[index][HEADER_ENDTIME].ToString();
                string strRemark = grdData.DataTable.Rows[index][HEADER_REMARK].ToString();

                txtDelayCode.Text = strDlyCd;
                txtRemark.Text = strRemark;
                CommonUtility.SetComboboxSelectedItem(cboHatch, strHatch);
                CommonUtility.SetComboboxSelectedItem(cboAPFP, strApfp);
                CommonUtility.SetComboboxSelectedItem(cboEqu, strEqu);
                CommonUtility.SetDTPValueDMYHM(txtStartTime, strStartTime);
                CommonUtility.SetDTPValueDMYHM(txtEndTime, strEndTime);
                if ("Y".Equals(strAcceptedDelay) || "true".Equals(strAcceptedDelay))
                {
                    chkAcceptDelay.Checked = true;
                }
                else if ("N".Equals(strAcceptedDelay) || "false".Equals(strAcceptedDelay))
                {
                    chkAcceptDelay.Checked = false;
                }


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
                    commonParm.lcd = "BULK";
                    commonParm.tyCd = "CD";
                    commonParm.cd = txtDelayCode.Text;
                    ResponseInfo info = proxy.getCommonCodeList(commonParm);

                    if (info != null && info.list != null && info.list.Length > 0)
                    {
                        if (info.list[0] is CodeMasterListItem)
                            info.list[0] = CommonUtility.ToCodeMasterListItem1(info.list[0] as CodeMasterListItem);
                        if (info.list[0] is CodeMasterListItem1)
                        {
                            CodeMasterListItem1 item = (CodeMasterListItem1)info.list[0];
                            txtDelayCode.Text = item.scd;
                            if (info.list.Length == 1)
                            {
                                chkAcceptDelay.Checked = "Y".Equals(item.acptYN);
                            }
                        }
                    } else
                    {
                        CommonUtility.AlertMessage("Invalid Delay Code");
                        txtDelayCode.Text = "";
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
            cboHatch.SelectedIndex = 0;
            cboAPFP.SelectedIndex = 0;
            cboEqu.SelectedIndex = 0;
            txtDelayCode.Text = string.Empty;
            txtRemark.Text = string.Empty;
            chkAcceptDelay.Checked = false;
            CommonUtility.SetDTPValueBlank(txtStartTime);
            CommonUtility.SetDTPValueBlank(txtEndTime);

            this.IsDirty = false;
        }
    }
}