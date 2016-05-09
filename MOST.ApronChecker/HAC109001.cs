using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MOST.Common;
using MOST.Common.Utility;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.ApronChecker.Parm;
using MOST.Client.Proxy.ApronCheckerProxy;
using Framework.Common.PopupManager;
using Framework.Controls.Container;
using Framework.Common.Constants;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.ApronChecker
{
    public partial class HAC109001 : TForm, IPopupWindow
    {
        #region Local Variable
        private const string HEADER_ITEM_STATUS = "_ITEM_STATUS";
        private const string HEADER_WS_NM = "Status";
        private const string HEADER_ROLE = "Role";
        private const string HEADER_STAFF_ID = "Staff Id";
        private const string HEADER_STAFF_NM = "Staff Name";
        private const string HEADER_WAREA = "W.Area";
        private const string HEADER_ST_TIME = "Start Time";
        private const string HEADER_ED_TIME = "End Time";
        private const string HEADER_SEQ = "_SEQ";
        private const string HEADER_JPVC = "_JPVC";
        #endregion

        public HAC109001()
        {
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            btnDelete.Enabled = false;
        }
        
        public IPopupResult ShowPopup(IPopupParm parm)
        {
            HAC109Parm _parm = (HAC109Parm)parm;
            if (_parm != null && _parm.JpvcInfo != null)
            {
                txtJPVC.Text = _parm.JpvcInfo.Jpvc;
                F_Retrieve();
            }
            this.ShowDialog();
            return null;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_ITEM_STATUS, "0" }, { HEADER_WS_NM, "40" }, { HEADER_ROLE, "50" }, { HEADER_STAFF_ID, "50" }, { HEADER_STAFF_NM, "100" }, { HEADER_WAREA, "100" }, { HEADER_ST_TIME, "100" }, { HEADER_ED_TIME, "100" }, { HEADER_SEQ, "0" }, { HEADER_JPVC, "0" } };
            this.grdData.setHeader(header);
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button _mybutton = (Button)sender;
            String _buttonName = _mybutton.Name;
            switch (_buttonName)
            {
                case "btnF1":
                    MOST.Common.CommonParm.SearchJPVCParm jpvcParm = new MOST.Common.CommonParm.SearchJPVCParm();
                    SearchJPVCResult jpvcResultTmp = (SearchJPVCResult)PopupManager.instance.ShowPopup(new HCM101(), jpvcParm);
                    if (jpvcResultTmp != null)
                    {
                        txtJPVC.Text = jpvcResultTmp.Jpvc;
                    }
                    break;

                case "btnRetrieve":
                    if (this.validations(this.Controls))
                    {
                        F_Retrieve();
                    }
                    break;

                case "btnDelete":
                    if (DialogResult.Yes == CommonUtility.ConfirmMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0046")))
                    {
                        DeleteItem();
                    }
                    break;

                case "btnOk":
                    if (grdData.IsDirty)
                    {
                        if (ProcessVSRListItem())
                        {
                            F_Retrieve();
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
                            ProcessVSRListItem();
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
            }
        }

        private void F_Retrieve()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // tnkytn: waiting for changes at server-side.
                IApronCheckerProxy proxy = new ApronCheckerProxy();
                CheckListVSRParm parm = new CheckListVSRParm();
                parm.searchType = "craneList";
                parm.vslCallID = txtJPVC.Text;
                parm.workYmd = UserInfo.getInstance().Workdate;
                parm.shftId = UserInfo.getInstance().Shift;
                ResponseInfo info = proxy.getVSRList(parm);

                grdData.Clear();
                DataRow newRow;
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CheckListVSRItem)
                    {
                        // tnkytn: waiting for changes at Server-side.
                        CheckListVSRItem item = (CheckListVSRItem)info.list[i];
                        if ("SD".Equals(item.divCd))
                        {
                            newRow = grdData.NewRow();
                            newRow[HEADER_ITEM_STATUS] = Constants.ITEM_OLD;
                            newRow[HEADER_WS_NM] = Constants.WS_NM_INITIAL;
                            newRow[HEADER_ROLE] = item.rsNm;
                            newRow[HEADER_STAFF_ID] = item.empId;
                            newRow[HEADER_STAFF_NM] = item.empNm;
                            newRow[HEADER_WAREA] = item.workLoc;
                            newRow[HEADER_ST_TIME] = item.workStDt;
                            newRow[HEADER_ED_TIME] = item.workEndDt;
                            newRow[HEADER_SEQ] = item.seq;
                            newRow[HEADER_JPVC] = item.vslCallID;
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

        private void DeleteItem()
        {
            if (grdData.CurrentRowIndex > -1 && grdData.CurrentRowIndex < grdData.DataTable.Rows.Count)
            {
                // In case item status is OLD: change WORKING STATUS of this row.
                string itemStatus = grdData.DataTable.Rows[grdData.CurrentRowIndex][HEADER_ITEM_STATUS].ToString();
                if (Constants.ITEM_OLD.Equals(itemStatus))
                {
                    // Set Readonly False to update column data.
                    DataTable dataTable = grdData.DataTable;
                    int colCnt = dataTable.Columns.Count;
                    for (int i = 0; i < colCnt; i++)
                    {
                        dataTable.Columns[i].ReadOnly = false;
                    }

                    // Change WORKING STATUS of this row to DELETE.
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

        private bool ProcessVSRListItem()
        {
            bool result = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                IApronCheckerProxy proxy = new ApronCheckerProxy();
                System.Collections.ArrayList arrObj = new System.Collections.ArrayList();

                if (grdData != null)
                {
                    for (int i = 0; i < grdData.DataTable.Rows.Count; i++)
                    {
                        // Process only items that are modified.
                        string crud = CommonUtility.GetCRUDFromName(grdData.DataTable.Rows[i][HEADER_WS_NM].ToString());
                        if (!Constants.WS_INITIAL.Equals(crud))
                        {
                            // ref: CT106 & CT106001
                            CheckListVSRItem item = new CheckListVSRItem();
                            item.vslCallID = grdData.DataTable.Rows[i][HEADER_JPVC].ToString();
                            if (!string.IsNullOrEmpty(grdData.DataTable.Rows[i][HEADER_SEQ].ToString()))
                            {
                                item.seq = CommonUtility.ParseInt(grdData.DataTable.Rows[i][HEADER_SEQ].ToString());
                            }
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

                    proxy.processVSRListItem(dataCollection);
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
                // Disable UPDATE, DELETE buttons if this row was marked as deleted.
                string strWorkingStatus = grdData.DataTable.Rows[index][HEADER_WS_NM].ToString();
                if (Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                {
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                }
            }
        }

        private void HAC109001_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q && e.Modifiers == Keys.Alt)
            {
                MessageBox.Show("Screen ID : " + this.Name + "\n\rScreen caption: " + this.Text, "Screen Information !");
            }
        }
    }
}