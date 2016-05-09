using System;
using System.Collections;
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
using MOST.VesselOperator.Result;
using Framework.Common.PopupManager;
using Framework.Controls.Container;
using Framework.Common.Constants;
using Framework.Common.ExceptionHandler;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.VesselOperator
{
    public partial class HVO114 : TForm, IPopupWindow
    {
        #region Local Variable
        private const string HEADER_ITEM_STATUS = "_ITEM_STATUS";
        private const string HEADER_WS_NM = "Status";
        private const string HEADER_TYPE_NM = "Type";
        private const string HEADER_TYPE_CD = "_Type";
        private const string HEADER_ST_TIME = "ST Time";
        private const string HEADER_ED_TIME = "End Time";
        private const string HEADER_2ND_JPVC = "2nd JPVC";
        private const string HEADER_2ND_LOA = "2nd LOA";
        private const string HEADER_2ND_ATB = "2nd ATB";
        private const string HEADER_2ND_ATU = "2nd ATU";
        private const string HEADER_3RD_JPVC = "3rd JPVC";
        private const string HEADER_3RD_LOA = "3rd LOA";
        private const string HEADER_3RD_ATB = "3rd ATB";
        private const string HEADER_3RD_ATU = "3rd ATU";
        private const string HEADER_SEQ = "_SEQ";

        private ArrayList m_arrGridData;
        HVO114Parm m_parm;
        #endregion

        public HVO114()
        {   
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();

            m_arrGridData = new ArrayList();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
        
        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HVO114Parm)parm;
            if (m_parm != null && m_parm.JpvcInfo != null)
            {
                txtJPVC.Text = m_parm.JpvcInfo.Jpvc;
                F_Retrieve();
            }
            this.ShowDialog();
            return null;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_ITEM_STATUS, "0" }, { HEADER_WS_NM, "40" }, { HEADER_TYPE_NM, "70" }, { HEADER_TYPE_CD, "0" }, { HEADER_ST_TIME, "100" }, { HEADER_ED_TIME, "100" }, { HEADER_2ND_JPVC, "85" }, { HEADER_2ND_LOA, "45" }, { HEADER_2ND_ATB, "100" }, { HEADER_2ND_ATU, "100" }, { HEADER_3RD_JPVC, "85" }, { HEADER_3RD_LOA, "45" }, { HEADER_3RD_ATB, "100" }, { HEADER_3RD_ATU, "100" }, { HEADER_SEQ, "0" } };
            this.grdData.setHeader(header);
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnAdd":
                    AddItem();
                    break;

                case "btnUpdate":
                    UpdateItem();
                    break;

                case "btnDelete":
                    if (ValidateOnDelete())
                    {
                        DeleteItem();
                    }
                    break;

                case "btnOk":
                    if (grdData.IsDirty)
                    {
                        if (ProcessgetSftDblBankingItem())
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
                            ProcessgetSftDblBankingItem();
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
                SftDblBankingParm parm = new SftDblBankingParm();
                parm.vslCallId = txtJPVC.Text;
                parm.searchType = "HHT_IF_DB";
                ResponseInfo info = proxy.getSftDblBankingList(parm);

                m_arrGridData.Clear();
                grdData.Clear();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is SftDblBankingItem)
                    {
                        SftDblBankingItem item = (SftDblBankingItem)info.list[i];
                        DataRow newRow = grdData.NewRow();
                        newRow[HEADER_ITEM_STATUS] = Constants.ITEM_OLD;
                        newRow[HEADER_WS_NM] = Constants.WS_NM_INITIAL;
                        newRow[HEADER_TYPE_NM] = item.dblBnkDivCdNm;
                        newRow[HEADER_TYPE_CD] = item.dblBnkDivCd;
                        newRow[HEADER_ST_TIME] = item.stDt;
                        newRow[HEADER_ED_TIME] = item.endDt;
                        newRow[HEADER_2ND_JPVC] = item.dblBnkShip1;
                        newRow[HEADER_2ND_LOA] = item.ship1Loa;
                        newRow[HEADER_2ND_ATB] = item.ship1Atb;
                        newRow[HEADER_2ND_ATU] = item.ship1Atu;
                        newRow[HEADER_3RD_JPVC] = item.dblBnkShip2;
                        newRow[HEADER_3RD_LOA] = item.ship2Loa;
                        newRow[HEADER_3RD_ATB] = item.ship2Atb;
                        newRow[HEADER_3RD_ATU] = item.ship2Atu;
                        newRow[HEADER_SEQ] = item.seq;
                        grdData.Add(newRow);

                        m_arrGridData.Add(item);
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

        private void AddItem()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                HVO108Parm parm = new HVO108Parm();
                parm.JpvcInfo = m_parm.JpvcInfo;
                HVO108Result result = (HVO108Result)PopupManager.instance.ShowPopup(new HVO108(Constants.MODE_ADD), parm);
                if (result != null)
                {
                    SftDblBankingItem item = result.SftDblBankingItem;
                    m_arrGridData.Add(item);

                    DataRow newRow = grdData.NewRow();
                    newRow[HEADER_ITEM_STATUS] = Constants.ITEM_NEW;
                    newRow[HEADER_WS_NM] = Constants.WS_NM_INSERT;
                    newRow[HEADER_TYPE_NM] = item.dblBnkDivCdNm;
                    newRow[HEADER_TYPE_CD] = item.dblBnkDivCd;
                    newRow[HEADER_ST_TIME] = item.stDt;
                    newRow[HEADER_ED_TIME] = item.endDt;
                    newRow[HEADER_2ND_JPVC] = item.dblBnkShip1;
                    newRow[HEADER_2ND_LOA] = item.ship1Loa;
                    newRow[HEADER_2ND_ATB] = item.ship1Atb;
                    newRow[HEADER_2ND_ATU] = item.ship1Atu;
                    newRow[HEADER_3RD_JPVC] = item.dblBnkShip2;
                    newRow[HEADER_3RD_LOA] = item.ship2Loa;
                    newRow[HEADER_3RD_ATB] = item.ship2Atb;
                    newRow[HEADER_3RD_ATU] = item.ship2Atu;
                    newRow[HEADER_SEQ] = item.seq;
                    grdData.Add(newRow);
                    grdData.Refresh();
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

        private void UpdateItem()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                int currRowIndex = grdData.CurrentRowIndex;
                if (-1 < currRowIndex && currRowIndex < m_arrGridData.Count)
                {
                    HVO108Parm parm = new HVO108Parm();
                    parm.JpvcInfo = m_parm.JpvcInfo;
                    parm.SftDblBankingItem = (SftDblBankingItem)m_arrGridData[currRowIndex];
                    HVO108Result result = (HVO108Result)PopupManager.instance.ShowPopup(new HVO108(Constants.MODE_UPDATE), parm);
                    if (result != null && result.SftDblBankingItem != null)
                    {
                        SftDblBankingItem item = result.SftDblBankingItem;
                        m_arrGridData[currRowIndex] = item;

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
                        row[HEADER_TYPE_NM] = item.dblBnkDivCdNm;
                        row[HEADER_TYPE_CD] = item.dblBnkDivCd;
                        row[HEADER_ST_TIME] = item.stDt;
                        row[HEADER_ED_TIME] = item.endDt;
                        row[HEADER_2ND_JPVC] = item.dblBnkShip1;
                        row[HEADER_2ND_LOA] = item.ship1Loa;
                        row[HEADER_2ND_ATB] = item.ship1Atb;
                        row[HEADER_2ND_ATU] = item.ship1Atu;
                        row[HEADER_3RD_JPVC] = item.dblBnkShip2;
                        row[HEADER_3RD_LOA] = item.ship2Loa;
                        row[HEADER_3RD_ATB] = item.ship2Atb;
                        row[HEADER_3RD_ATU] = item.ship2Atu;
                        row[HEADER_SEQ] = item.seq;
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
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
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
                    SftDblBankingItem item = (SftDblBankingItem)m_arrGridData[currRowIndex];

                    if (!string.IsNullOrEmpty(item.ship1Atw) ||
                        !string.IsNullOrEmpty(item.ship1Atc))
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO114_0007"));
                        return;
                    }

                    item.searchType = "doubleBanking";
                    item.CRUD = Constants.WS_DELETE;

                    // Set Readonly False to update column data.
                    DataTable dataTable = grdData.DataTable;
                    int colCnt = dataTable.Columns.Count;
                    for (int i = 0; i < colCnt; i++)
                    {
                        dataTable.Columns[i].ReadOnly = false;
                    }

                    // Change WORKING STATUS of this row to DELETE.
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

        private bool ValidateOnDelete()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (DialogResult.Yes == CommonUtility.ConfirmMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0046")))
                {
                    if (!Validate1())
                    {
                        return false;
                    }

                    if (!Validate2())
                    {
                        return false;
                    }

                    if (!Validate3())
                    {
                        return false;
                    }

                    if (!Validate4())
                    {
                        return false;
                    }

                    if (!Validate5())
                    {
                        return false;
                    }

                    if (!Validate6())
                    {
                        return false;
                    }
                }
                else
                {
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

        private bool Validate1()
        {
            bool result = true;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                int currRowIndex = grdData.CurrentRowIndex;
                if (-1 < currRowIndex && currRowIndex < m_arrGridData.Count)
                {
                    // You can't delete it because there is STS info.
                    SftDblBankingItem item = (SftDblBankingItem)m_arrGridData[currRowIndex];
                    string[] cols = { item.vslCallId };
                    result = CommonUtility.ValidateFunc("stsValid", cols);
                    if (!result)
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO114_0001"));
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
            return result;
        }

        private bool Validate2()
        {
            bool result = true;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                int currRowIndex = grdData.CurrentRowIndex;
                if (-1 < currRowIndex && currRowIndex < m_arrGridData.Count)
                {
                    // You can't delete it because there is double banking info later than this.
                    SftDblBankingItem item = (SftDblBankingItem)m_arrGridData[currRowIndex];
                    string[] cols = { item.dblBnkShip1 };
                    result = CommonUtility.ValidateFunc("stsValid", cols);
                    if (!result)
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO114_0002"));
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
            return result;
        }

        private bool Validate3()
        {
            bool result = true;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                int currRowIndex = grdData.CurrentRowIndex;
                if (-1 < currRowIndex && currRowIndex < m_arrGridData.Count)
                {
                    // You can't delete it because there is equipment setting later than this.
                    SftDblBankingItem item = (SftDblBankingItem)m_arrGridData[currRowIndex];
                    string[] cols = { item.vslCallId, item.atb };
                    result = CommonUtility.ValidateFunc("laterEQValid", cols);
                    if (!result)
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO114_0003"));
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
            return result;
        }

        private bool Validate4()
        {
            bool result = true;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                int currRowIndex = grdData.CurrentRowIndex;
                if (-1 < currRowIndex && currRowIndex < m_arrGridData.Count)
                {
                    // You can't delete it because there is equipment setting later than this.
                    SftDblBankingItem item = (SftDblBankingItem)m_arrGridData[currRowIndex];
                    string[] cols = { item.dblBnkShip1, item.ship1Atb };
                    result = CommonUtility.ValidateFunc("laterEQValid", cols);
                    if (!result)
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO114_0004"));
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
            return result;
        }

        private bool Validate5()
        {
            bool result = true;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                int currRowIndex = grdData.CurrentRowIndex;
                if (-1 < currRowIndex && currRowIndex < m_arrGridData.Count)
                {
                    // You can't delete it because there is equipment setting later than this.
                    SftDblBankingItem item = (SftDblBankingItem)m_arrGridData[currRowIndex];
                    string[] cols = { item.vslCallId, item.atb };
                    result = CommonUtility.ValidateFunc("shftValid", cols);
                    if (!result)
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO114_0005"));
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
            return result;
        }

        private bool Validate6()
        {
            bool result = true;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                int currRowIndex = grdData.CurrentRowIndex;
                if (-1 < currRowIndex && currRowIndex < m_arrGridData.Count)
                {
                    // You can't delete it because there is equipment setting later than this.
                    SftDblBankingItem item = (SftDblBankingItem)m_arrGridData[currRowIndex];
                    string[] cols = { item.dblBnkShip1, item.ship1Atb };
                    result = CommonUtility.ValidateFunc("shftValid", cols);
                    if (!result)
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO114_0006"));
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
            return result;
        }

        private bool ProcessgetSftDblBankingItem()
        {
            // ref: CT209 & CT105007
            bool result = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                IVesselOperatorProxy proxy = new VesselOperatorProxy();

                // Process only items that are modified.
                ArrayList arrObj = new ArrayList();
                if (m_arrGridData != null)
                {
                    for (int i = 0; i < m_arrGridData.Count; i++)
                    {
                        SftDblBankingItem item = (SftDblBankingItem)m_arrGridData[i];

                        // Process only items that are modified.
                        if (Constants.WS_INSERT.Equals(item.CRUD) ||
                            Constants.WS_UPDATE.Equals(item.CRUD) ||
                            Constants.WS_DELETE.Equals(item.CRUD))
                        {
                            arrObj.Add(item);
                        }
                    }
                }

                Object[] obj = arrObj.ToArray();
                DataItemCollection dataCollection = new DataItemCollection();
                dataCollection.collection = obj;

                proxy.processgetSftDblBankingItem(dataCollection);

                if (arrObj.Count > 0)
                {
                    result = true;
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
            return result;
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            int index = grdData.CurrentRowIndex;
            if (index > -1 && index < grdData.DataTable.Rows.Count)
            {
                // Proceed update processing if this row was NOT marked as deleted.
                string strWorkingStatus = grdData.DataTable.Rows[index][HEADER_WS_NM].ToString();
                if (!Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                {
                    UpdateItem();
                }
            }
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
    }
}