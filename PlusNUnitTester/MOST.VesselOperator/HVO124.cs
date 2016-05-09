using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MOST.Client.Proxy.VesselOperatorProxy;
using MOST.Common;
using MOST.Common.Utility;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.VesselOperator.Parm;
using Framework.Common.UserInformation;
using Framework.Common.PopupManager;
using Framework.Controls.Container;
using Framework.Common.Constants;
using Framework.Common.ExceptionHandler;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.VesselOperator
{
    public partial class HVO124 : TForm, IPopupWindow
    {
        #region Local Variable
        private const string HEADER_ITEM_STATUS = "_ITEM_STATUS";
        private const string HEADER_WS_NM = "Status";
        private const string HEADER_HATCH = "Hatch";
        private const string HEADER_EQU = "EQU";
        private const string HEADER_LD20 = "Loading 20'";
        private const string HEADER_LD40 = "Loading 40";
        private const string HEADER_DS20 = "Discharging 20";
        private const string HEADER_DS40 = "Discharging 40";
        private const string HEADER_STTIME = "StartTime";
        private const string HEADER_ENDTIME = "EndTime";
        private const string HEADER_SEQ = "_SEQ";
        #endregion

        public HVO124()
        {
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            btnDelete.Enabled = false;
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            HVO124Parm hvo124Parm = (HVO124Parm)parm;
            if (hvo124Parm != null && hvo124Parm.JpvcInfo != null)
            {
                txtJPVC.Text = hvo124Parm.JpvcInfo.Jpvc;
                F_Retrieve();
            }
            this.ShowDialog();
            return null;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_ITEM_STATUS, "0" }, { HEADER_WS_NM, "40" }, { HEADER_HATCH, "30" }, { HEADER_EQU, "80" }, { HEADER_LD20, "30" }, { HEADER_LD40, "30" }, { HEADER_DS20, "30" }, { HEADER_DS40, "30" }, { HEADER_STTIME, "100" }, { HEADER_ENDTIME, "100" }, { HEADER_SEQ, "0" } };
            this.grdData.setHeader(header);
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnF1":
                    MOST.Common.CommonParm.SearchJPVCParm jpvcParm = new MOST.Common.CommonParm.SearchJPVCParm();
                    jpvcParm.Jpvc = txtJPVC.Text;
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
                        if (ProcessContainerProcessItems())
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
                            ProcessContainerProcessItems();
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

                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                ContainerProcessParm parm = new ContainerProcessParm();
                parm.searchType = "HHT_CTNR_PROC";
                parm.vslCallId = txtJPVC.Text;
                parm.workYmd = UserInfo.getInstance().Workdate;
                parm.shftId = UserInfo.getInstance().Shift;
                ResponseInfo info = proxy.getContainerProcessList(parm);

                grdData.Clear();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is ContainerProcessItem)
                    {
                        ContainerProcessItem item = (ContainerProcessItem)info.list[i];
                        DataRow newRow = grdData.NewRow();
                        newRow[HEADER_ITEM_STATUS] = Constants.ITEM_OLD;
                        newRow[HEADER_WS_NM] = Constants.WS_NM_INITIAL;
                        newRow[HEADER_HATCH] = item.hatchNo;
                        newRow[HEADER_EQU] = item.eqNm;
                        newRow[HEADER_LD20] = item.l20;
                        newRow[HEADER_LD40] = item.l40;
                        newRow[HEADER_DS20] = item.d20;
                        newRow[HEADER_DS40] = item.d40;
                        newRow[HEADER_STTIME] = item.stDt;
                        newRow[HEADER_ENDTIME] = item.endDt;
                        newRow[HEADER_SEQ] = item.seq;
                        grdData.Add(newRow);
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

        // process deleted items
        private bool ProcessContainerProcessItems()
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
                            // ref: CT212
                            ContainerProcessItem item = new ContainerProcessItem();
                            item.vslCallId = txtJPVC.Text;
                            item.seq = grdData.DataTable.Rows[i][HEADER_SEQ].ToString();
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

                    proxy.processContainerProcessItems(dataCollection);
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
                btnDelete.Enabled = !Constants.WS_NM_DELETE.Equals(strWorkingStatus);
            }
        }
    }
}