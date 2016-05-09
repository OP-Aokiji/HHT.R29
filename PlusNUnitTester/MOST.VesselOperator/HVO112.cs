using System;
using System.Collections;
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
using MOST.Client.Proxy.VesselOperatorProxy;
using MOST.VesselOperator.Parm;
using MOST.VesselOperator.Result;
using Framework.Common.PopupManager;
using Framework.Controls.Container;
using Framework.Common.Constants;
using Framework.Common.ExceptionHandler;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.VesselOperator
{
    public partial class HVO112 : TForm, IPopupWindow
    {
        #region Local Variable
        private const string HEADER_ITEM_STATUS = "_ITEM_STATUS";
        private const string HEADER_WS_NM = "Status";
        private const string HEADER_REQ = "Requester";
        private const string HEADER_ATU = "ATU";
        private const string HEADER_ATB = "ATB";
        private const string HEADER_CUR_WHARF = "Prev. Loc";
        private const string HEADER_NEW_WHARF = "New Loc";
        private const string HEADER_NEW_WHR_FRM = "New W.From";
        private const string HEADER_NEW_WHR_TO = "New W.To";
        private const string HEADER_SF_POS_NM = "SF Pos";
        private const string HEADER_SF_POS_CD = "_SF Pos";
        private const string HEADER_REASON_NM = "Reason";
        private const string HEADER_REASON_CD = "_Reason";
        private const string HEADER_PILOT = "Pilot";
        private const string HEADER_MOORING = "Mooring";
        private const string HEADER_TUG = "Tug";
        private const string HEADER_SEQ = "_SEQ";

        private HVO112Parm m_parm;
        private ArrayList m_arrGridData;
        #endregion

        public HVO112()
        {
            m_arrGridData = new ArrayList();
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
        
        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HVO112Parm)parm;
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
            String[,] header = { { HEADER_ITEM_STATUS, "0" }, { HEADER_WS_NM, "40" }, { HEADER_REQ, "50" }, { HEADER_ATU, "100" }, { HEADER_ATB, "100" }, { HEADER_CUR_WHARF, "50" }, 
                                { HEADER_NEW_WHARF, "50" }, { HEADER_NEW_WHR_FRM, "50" }, { HEADER_NEW_WHR_TO, "50" }, { HEADER_SF_POS_NM, "50" }, { HEADER_SF_POS_CD, "0" }, 
                                { HEADER_REASON_NM, "120" }, { HEADER_REASON_CD, "0" }, { HEADER_PILOT, "20" }, { HEADER_MOORING, "20" }, { HEADER_TUG, "20" }, { HEADER_SEQ, "0" } };
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
                parm.searchType = "HHT_IF_VS";
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
                        newRow[HEADER_REQ] = item.reqr;
                        newRow[HEADER_ATU] = item.prevAtu;
                        newRow[HEADER_ATB] = item.atbDt;
                        newRow[HEADER_CUR_WHARF] = item.currWharf;
                        newRow[HEADER_NEW_WHARF] = item.nxBerthNo;
                        newRow[HEADER_NEW_WHR_FRM] = item.wharfMarkFm;
                        newRow[HEADER_NEW_WHR_TO] = item.wharfMarkTo;
                        newRow[HEADER_SF_POS_NM] = item.berthAlongsideNm;
                        newRow[HEADER_SF_POS_CD] = item.berthAlongside;
                        newRow[HEADER_REASON_NM] = item.rsnNm;
                        newRow[HEADER_REASON_CD] = item.rsnCd;
                        newRow[HEADER_PILOT] = item.pilotYn;
                        newRow[HEADER_MOORING] = item.mooring;
                        newRow[HEADER_TUG] = item.tug;
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
                HVO105Parm hvo105Parm = new HVO105Parm();
                hvo105Parm.JpvcInfo = m_parm.JpvcInfo;
                HVO105Result hvo105Result = (HVO105Result)PopupManager.instance.ShowPopup(new HVO105(Constants.MODE_ADD), hvo105Parm);
                if (hvo105Result != null)
                {
                    SftDblBankingItem item = hvo105Result.SftDblBankingItem;
                    m_arrGridData.Add(item);

                    DataRow newRow = grdData.NewRow();
                    newRow[HEADER_ITEM_STATUS] = Constants.ITEM_NEW;
                    newRow[HEADER_WS_NM] = Constants.WS_NM_INSERT;
                    newRow[HEADER_REQ] = item.reqr;
                    newRow[HEADER_ATU] = item.prevAtu;
                    newRow[HEADER_ATB] = item.atbDt;
                    newRow[HEADER_CUR_WHARF] = item.currWharf;
                    newRow[HEADER_NEW_WHARF] = item.nxBerthNo;
                    newRow[HEADER_NEW_WHR_FRM] = item.wharfMarkFm;
                    newRow[HEADER_NEW_WHR_TO] = item.wharfMarkTo;
                    newRow[HEADER_SF_POS_NM] = item.berthAlongsideNm;
                    newRow[HEADER_SF_POS_CD] = item.berthAlongside;
                    newRow[HEADER_REASON_NM] = item.rsnNm;
                    newRow[HEADER_REASON_CD] = item.rsnCd;
                    newRow[HEADER_PILOT] = item.pilotYn;
                    newRow[HEADER_MOORING] = item.mooring;
                    newRow[HEADER_TUG] = item.tug;
                    //newRow[HEADER_SEQ] = "";
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
                    HVO105Parm hvo105Parm = new HVO105Parm();
                    hvo105Parm.JpvcInfo = m_parm.JpvcInfo;
                    hvo105Parm.SftDblBankingItem = (SftDblBankingItem)m_arrGridData[currRowIndex];
                    HVO105Result hvo105Result = (HVO105Result)PopupManager.instance.ShowPopup(new HVO105(Constants.MODE_UPDATE), hvo105Parm);
                    if (hvo105Result != null && hvo105Result.SftDblBankingItem != null)
                    {
                        SftDblBankingItem item = hvo105Result.SftDblBankingItem;
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
                        row[HEADER_REQ] = item.reqr;
                        row[HEADER_ATU] = item.prevAtu;
                        row[HEADER_ATB] = item.atbDt;
                        row[HEADER_CUR_WHARF] = item.currWharf;
                        row[HEADER_NEW_WHARF] = item.nxBerthNo;
                        row[HEADER_NEW_WHR_FRM] = item.wharfMarkFm;
                        row[HEADER_NEW_WHR_TO] = item.wharfMarkTo;
                        row[HEADER_SF_POS_NM] = item.berthAlongsideNm;
                        row[HEADER_SF_POS_CD] = item.berthAlongside;
                        row[HEADER_REASON_NM] = item.rsnNm;
                        row[HEADER_REASON_CD] = item.rsnCd;
                        row[HEADER_PILOT] = item.pilotYn;
                        row[HEADER_MOORING] = item.mooring;
                        row[HEADER_TUG] = item.tug;
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
                    item.searchType = "vesselShifting";
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

        private bool ProcessgetSftDblBankingItem()
        {
            bool result = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                IVesselOperatorProxy proxy = new VesselOperatorProxy();

                // ref: CT209 & CT105007
                // Process only items that are modified.
                ArrayList arrObj = new ArrayList();
                if (m_arrGridData != null)
                {
                    for (int i = 0; i < m_arrGridData.Count; i++)
                    {
                        SftDblBankingItem item = (SftDblBankingItem)m_arrGridData[i];

                        // Process only items that are modified.
                        //string crud = CommonUtility.getCRUDFromName(grdData.DataTable.Rows[i][HEADER_WS_NM].ToString());
                        //if (!Constants.WS_INITIAL.Equals(crud))
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

        private bool ValidateOnDelete()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (!ValidateFromMss())
                {
                    return false;
                }

                if (DialogResult.Yes == CommonUtility.ConfirmMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0046")))
                {
                    if (!ValidateOfDBankingInfo())
                    {
                        return false;
                    }

                    if (!ValidateOfEquSettingsInfo())
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

        private bool ValidateFromMss()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                int currRowIndex = grdData.CurrentRowIndex;
                if (-1 < currRowIndex && currRowIndex < m_arrGridData.Count)
                {
                    SftDblBankingItem item = (SftDblBankingItem)m_arrGridData[currRowIndex];
                    if (!string.IsNullOrEmpty(item.svcId))
                    {
                        // You can't delete it because it's requested from MSS.
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO112_0001"));
                        return false;
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

        private bool ValidateOfDBankingInfo()
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
                    string[] cols = { item.vslCallId, item.atbDt };
                    result = CommonUtility.ValidateFunc("dblBnkValid", cols);
                    if (!result)
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO112_0002"));
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

        private bool ValidateOfEquSettingsInfo()
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
                    string[] cols = { item.vslCallId, item.atbDt };
                    result = CommonUtility.ValidateFunc("laterEQValid", cols);
                    if (!result)
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO112_0003"));
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