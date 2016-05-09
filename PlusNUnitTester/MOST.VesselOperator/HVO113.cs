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
using Framework.Common.UserInformation;
using Framework.Common.ExceptionHandler;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.VesselOperator
{
    public partial class HVO113 : TForm, IPopupWindow
    {
        #region Local Variable
        // Operation type {Loading/Dicharging, Ship to ship, Transhipment}
        public const string CONST_OPETP_GENERAL         = "GEN";    // Operation type - General (Loading/Discharging)
        public const string CONST_OPETP_STS             = "STS";    // Purpose of Call - STS for liquid
        public const string CONST_OPETP_TRANSHIPMENT    = "TLS";    // Purpose of Call - Transhipment
        // Cargo operation type - Loading
        public const string CONST_CGOPETP_LOADING_GEN   = "LD";     // Cargo operation type - Loading - General
        public const string CONST_CGOPETP_LOADING_STS   = "SL";     // Cargo operation type - Loading - Ship to ship
        public const string CONST_CGOPETP_LOADING_TLS   = "TL";     // Cargo operation type - Loading - Transhipment
        // Cargo operation type - Discharging
        public const string CONST_CGOPETP_DISCHARGING_GEN = "DS";   // Cargo operation type - Discharging - General
        public const string CONST_CGOPETP_DISCHARGING_STS = "SD";   // Cargo operation type - Discharging - Ship to ship
        public const string CONST_CGOPETP_DISCHARGING_TLS = "TD";   // Cargo operation type - Discharging - Transhipment
        // Hose line type {Flexible hose, MLA}
        public const string CONST_LINETP_FLX            = "FLX";     // Hose line type - Flexible hose
        public const string CONST_LINETP_MLA            = "MLA";     // Hose line type - MLA

        private const string HEADER_ITEM_STATUS = "_ITEM_STATUS";
        private const string HEADER_WS_NM = "Status";
        private const string HEADER_TMNL_OPR = "Tmnl OPR";
        private const string HEADER_OPR_TYPE = "OPR Type";
        private const string HEADER_LINE_TYPE = "Line Type";
        private const string HEADER_LINE_NO = "Line No.";
        private const string HEADER_CARGO_TYPE = "Cargo Type";
        private const string HEADER_HOSEON_DT = "Hose On";
        private const string HEADER_COMMENCE_DT = "Commence";
        private const string HEADER_COMPLETE_DT = "Complete";
        private const string HEADER_HOSEOFF_DT = "Hose Off";
        private const string HEADER_TONNAGE = "Tonnage";
        private const string HEADER_PUMPING = "Pumping";
        private const string HEADER_PKGTP = "Pkg Tp";
        private const string HEADER_COMMODITY = "Commodity";
        private const string HEADER_SEQ = "_SEQ";

        private string m_opeTp;
        private HVO113Parm m_parm;
        private ArrayList m_arrGridData;
        private VORLiquidBulkItem m_liquidBulkItem;
        #endregion

        public HVO113(string opeTp)
        {
            m_opeTp = opeTp;
            m_arrGridData = new ArrayList();
            InitializeComponent();
            this.initialFormSize();

            // For checking if form is dirty or not
            List<string> controlNames = new List<string>();
            controlNames.Add(txtFHoseL.Name);
            controlNames.Add(txtFHoseD.Name);
            controlNames.Add(txtMLAL.Name);
            controlNames.Add(txtMLAD.Name);
            this.AddOnChangeHandlerToInputControls(this.Controls, controlNames);

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HVO113Parm)parm;
            if (m_parm != null && m_parm.JpvcInfo != null)
            {
                SetTitle();
                InitializeDataGrid();
                txtJPVC.Text = m_parm.JpvcInfo.Jpvc;

                // Get hose quantity
                GetLiquidBulkHoseQty();
                
                // Retrieve grid data
                F_Retrieve();
            }
            else
            {
                InitializeDataGrid();
            }
            this.ShowDialog();
            return null;
        }

        public void SetTitle()
        {
            string strTitle = "V/S - Jetty OPR List";
            switch (m_opeTp)
            {
                case HVO113.CONST_OPETP_GENERAL:
                    strTitle = "V/S - Jetty OPR List";
            	    break;
                case HVO113.CONST_OPETP_STS:
                    strTitle = "V/S - STS For Liquid";
                    break;
                case HVO113.CONST_OPETP_TRANSHIPMENT:
                    strTitle = "V/S - Transhipment List";
                    break;
            }
            this.Text = strTitle;
        }

        private void InitializeDataGrid()
        {
            if (HVO113.CONST_OPETP_STS.Equals(m_opeTp))
            {
                String[,] header = { { HEADER_ITEM_STATUS, "0" }, { HEADER_WS_NM, "40" }, { HEADER_OPR_TYPE, "70" }, { HEADER_LINE_TYPE, "70" }, { HEADER_LINE_NO, "50" }, { HEADER_CARGO_TYPE, "70" }, 
                                { HEADER_HOSEON_DT, "87" }, { HEADER_COMMENCE_DT, "82" }, { HEADER_COMPLETE_DT, "85" }, { HEADER_HOSEOFF_DT, "90" }, { HEADER_TONNAGE, "40" }, 
                                { HEADER_PUMPING, "40" }, { HEADER_PKGTP, "50" }, { HEADER_COMMODITY, "50" }, { HEADER_SEQ, "0" } };
                this.grdData.setHeader(header);
            }
            else
            {
                String[,] header = { { HEADER_ITEM_STATUS, "0" }, { HEADER_WS_NM, "40" }, { HEADER_TMNL_OPR, "70" }, { HEADER_OPR_TYPE, "70" }, { HEADER_LINE_TYPE, "70" }, { HEADER_LINE_NO, "50" }, { HEADER_CARGO_TYPE, "70" }, 
                                { HEADER_HOSEON_DT, "87" }, { HEADER_COMMENCE_DT, "82" }, { HEADER_COMPLETE_DT, "85" }, { HEADER_HOSEOFF_DT, "90" }, { HEADER_TONNAGE, "40" }, 
                                { HEADER_PUMPING, "40" }, { HEADER_PKGTP, "50" }, { HEADER_COMMODITY, "50" }, { HEADER_SEQ, "0" } };
                this.grdData.setHeader(header);
            }
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
                    if (DialogResult.Yes == CommonUtility.ConfirmMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0046")))
                    {
                        DeleteItem();
                    }
                    break;

                case "btnDelay":
                    HVO120Parm hvo120Parm = new HVO120Parm();
                    hvo120Parm.JpvcInfo = m_parm.JpvcInfo;
                    PopupManager.instance.ShowPopup(new HVO120(m_opeTp), hvo120Parm);
                    break;

                case "btnOk":
                    // Update hose lines (and insert/update TMT_LQDCG_OPE_MST)
                    UpdateVORLiquidHoseLines();

                    // Update grid data
                    if (grdData.IsDirty)
                    {
                        if (ProcessVORLiquidCargoCUD())
                        {
                            // Get hose quantity
                            GetLiquidBulkHoseQty();
                            // Retrieve grid data
                            F_Retrieve();
                            // Alert message "successfully"
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
                            if (ProcessVORLiquidCargoCUD())
                            {
                                //F_Retrieve();
                                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
                            }
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

                // Ref: CT109
                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                VORLiquidBulkParm parm = new VORLiquidBulkParm();
                parm.jpvcNo = txtJPVC.Text;
                parm.workYmd = UserInfo.getInstance().Workdate;
                parm.shift = UserInfo.getInstance().Shift;
                parm.opeTp = m_opeTp;
                ResponseInfo info = proxy.getVORLiquidCargo(parm);

                m_arrGridData.Clear();
                grdData.Clear();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is VORLiquidBulkItem)
                    {
                        VORLiquidBulkItem item = (VORLiquidBulkItem)info.list[i];
                        if (!string.IsNullOrEmpty(item.shftId))
                        {
                            DataRow newRow = grdData.NewRow();
                            newRow[HEADER_ITEM_STATUS] = Constants.ITEM_OLD;
                            newRow[HEADER_WS_NM] = Constants.WS_NM_INITIAL;
                            if (!HVO113.CONST_OPETP_STS.Equals(m_opeTp))
                            {
                                newRow[HEADER_TMNL_OPR] = item.tkOpr;
                            }
                            newRow[HEADER_OPR_TYPE] = item.jobTpCd;
                            newRow[HEADER_LINE_TYPE] = item.hoseTpCd;
                            newRow[HEADER_LINE_NO] = item.lineNo;
                            newRow[HEADER_CARGO_TYPE] = item.cgTpCd;
                            newRow[HEADER_COMMENCE_DT] = item.stDt;
                            newRow[HEADER_COMPLETE_DT] = item.endDt;
                            newRow[HEADER_HOSEON_DT] = item.hoseOnDt;
                            newRow[HEADER_HOSEOFF_DT] = item.hoseOffDt;
                            newRow[HEADER_TONNAGE] = item.tonHdlAmt;
                            newRow[HEADER_PUMPING] = item.pumpRate;
                            newRow[HEADER_PKGTP] = item.pkgTpCd;
                            newRow[HEADER_COMMODITY] = item.cmdtCd;
                            newRow[HEADER_SEQ] = item.seq;
                            grdData.Add(newRow);
                            m_arrGridData.Add(item);
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

        // Update number of hose lines for Add, Update, Delete process.
        private void RefreshHosesNo()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                int nFHoseLD = 0;   // F/Hose Loading
                int nFHoseDS = 0;   // F/Hose Discharging
                int nMlaLD = 0;     // MLA Loading
                int nMlaDS = 0;     // MLA Discharging

                if (m_arrGridData != null)
                {
                    for (int i = 0; i < m_arrGridData.Count; i++)
                    {
                        VORLiquidBulkItem item = (VORLiquidBulkItem)m_arrGridData[i];

                        if (!Constants.WS_DELETE.Equals(item.CRUD))
                        {
                            if (CONST_CGOPETP_LOADING_GEN.Equals(item.jobTpCd) || 
                                CONST_CGOPETP_LOADING_STS.Equals(item.jobTpCd) ||
                                CONST_CGOPETP_LOADING_TLS.Equals(item.jobTpCd))
                            {
                                if (CONST_LINETP_MLA.Equals(item.hoseTpCd))
                                {
                                    nMlaLD += CommonUtility.ParseInt(item.lineNo);
                                }
                                else if (CONST_LINETP_FLX.Equals(item.hoseTpCd))
                                {
                                    nFHoseLD += CommonUtility.ParseInt(item.lineNo);
                                }
                            }
                            else if (CONST_CGOPETP_DISCHARGING_GEN.Equals(item.jobTpCd) ||
                                CONST_CGOPETP_DISCHARGING_STS.Equals(item.jobTpCd) || 
                                CONST_CGOPETP_DISCHARGING_TLS.Equals(item.jobTpCd))
                            {
                                if (CONST_LINETP_MLA.Equals(item.hoseTpCd))
                                {
                                    nMlaDS += CommonUtility.ParseInt(item.lineNo);
                                }
                                else if (CONST_LINETP_FLX.Equals(item.hoseTpCd))
                                {
                                    nFHoseDS += CommonUtility.ParseInt(item.lineNo);
                                }
                            }
                        }
                    }
                }

                txtFHoseL.Text = nFHoseLD.ToString();
                txtFHoseD.Text = nFHoseDS.ToString();
                txtMLAL.Text = nMlaLD.ToString();
                txtMLAD.Text = nMlaDS.ToString();
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

        private void AddItem()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //RefreshBalAmt();

                HVO106Parm hvo106Parm = new HVO106Parm();
                hvo106Parm.JpvcInfo = m_parm.JpvcInfo;
                hvo106Parm.GridArrayList = m_arrGridData;
                HVO106Result hvo106Result = (HVO106Result)PopupManager.instance.ShowPopup(new HVO106(m_opeTp, Constants.MODE_ADD), hvo106Parm);
                if (hvo106Result != null)
                {
                    VORLiquidBulkItem item = hvo106Result.LiquidBulkItem;
                    m_arrGridData.Add(item);

                    DataRow newRow = grdData.NewRow();
                    newRow[HEADER_ITEM_STATUS] = Constants.ITEM_NEW;
                    newRow[HEADER_WS_NM] = Constants.WS_NM_INSERT;
                    if (!HVO113.CONST_OPETP_STS.Equals(m_opeTp))
                    {
                        newRow[HEADER_TMNL_OPR] = item.tkOpr;
                    }
                    newRow[HEADER_OPR_TYPE] = item.jobTpCd;
                    newRow[HEADER_LINE_TYPE] = item.hoseTpCd;
                    newRow[HEADER_LINE_NO] = item.lineNo;
                    newRow[HEADER_CARGO_TYPE] = item.cgTpCd;
                    newRow[HEADER_COMMENCE_DT] = item.stDt;
                    newRow[HEADER_COMPLETE_DT] = item.endDt;
                    newRow[HEADER_HOSEON_DT] = item.hoseOnDt;
                    newRow[HEADER_HOSEOFF_DT] = item.hoseOffDt;
                    newRow[HEADER_TONNAGE] = item.tonHdlAmt;
                    newRow[HEADER_PUMPING] = item.pumpRate;
                    newRow[HEADER_PKGTP] = item.pkgTpCd;
                    newRow[HEADER_COMMODITY] = item.cmdtCd;
                    grdData.Add(newRow);
                    grdData.Refresh();

                    RefreshHosesNo();
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

                //RefreshBalAmt();

                int currRowIndex = grdData.CurrentRowIndex;
                if (-1 < currRowIndex && currRowIndex < m_arrGridData.Count)
                {
                    HVO106Parm hvo106Parm = new HVO106Parm();
                    hvo106Parm.JpvcInfo = m_parm.JpvcInfo;
                    hvo106Parm.LiquidBulkItem = (VORLiquidBulkItem)m_arrGridData[currRowIndex];
                    hvo106Parm.GridArrayList = m_arrGridData;
                    HVO106Result hvo106Result = (HVO106Result)PopupManager.instance.ShowPopup(new HVO106(m_opeTp, Constants.MODE_UPDATE), hvo106Parm);
                    if (hvo106Result != null && hvo106Result.LiquidBulkItem != null)
                    {
                        VORLiquidBulkItem item = hvo106Result.LiquidBulkItem;
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
                        if (!HVO113.CONST_OPETP_STS.Equals(m_opeTp))
                        {
                            row[HEADER_TMNL_OPR] = item.tkOpr;
                        }
                        row[HEADER_OPR_TYPE] = item.jobTpCd;
                        row[HEADER_LINE_TYPE] = item.hoseTpCd;
                        row[HEADER_LINE_NO] = item.lineNo;
                        row[HEADER_CARGO_TYPE] = item.cgTpCd;
                        row[HEADER_COMMENCE_DT] = item.stDt;
                        row[HEADER_COMPLETE_DT] = item.endDt;
                        row[HEADER_HOSEON_DT] = item.hoseOnDt;
                        row[HEADER_HOSEOFF_DT] = item.hoseOffDt;
                        row[HEADER_TONNAGE] = item.tonHdlAmt;
                        row[HEADER_PUMPING] = item.pumpRate;
                        row[HEADER_PKGTP] = item.pkgTpCd;
                        row[HEADER_COMMODITY] = item.cmdtCd;
                        row[HEADER_SEQ] = item.seq;
                        grdData.DataTable.AcceptChanges();
                        grdData.Refresh();

                        // Reset original Readonly
                        for (int i = 0; i < colCnt; i++)
                        {
                            dataTable.Columns[i].ReadOnly = true;
                        }

                        RefreshHosesNo();
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
                    VORLiquidBulkItem item = (VORLiquidBulkItem)m_arrGridData[currRowIndex];
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

                RefreshHosesNo();
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

                txtFHoseL.Text = "0";
                txtFHoseD.Text = "0";
                txtMLAL.Text = "0";
                txtMLAD.Text = "0";

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
                    txtFHoseL.Text = CommonUtility.ToString(m_liquidBulkItem.totFlxLdQty);
                    txtFHoseD.Text = CommonUtility.ToString(m_liquidBulkItem.totFlxDsQty);
                    txtMLAL.Text = CommonUtility.ToString(m_liquidBulkItem.totArmLdQty);
                    txtMLAD.Text = CommonUtility.ToString(m_liquidBulkItem.totArmDsQty);
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
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (m_liquidBulkItem == null)
                {
                    m_liquidBulkItem = new VORLiquidBulkItem();
                }

                IVesselOperatorProxy proxy = new VesselOperatorProxy();

                // ref: CT108 & CT108001
                m_liquidBulkItem.vslCallId = txtJPVC.Text;
                m_liquidBulkItem.opeTp = m_opeTp;
                m_liquidBulkItem.loadHoseQty = string.IsNullOrEmpty(txtFHoseL.Text) ? "0" : txtFHoseL.Text;
                m_liquidBulkItem.dschHoseQty = string.IsNullOrEmpty(txtFHoseD.Text) ? "0" : txtFHoseD.Text;
                m_liquidBulkItem.loadArmQty = string.IsNullOrEmpty(txtMLAL.Text) ? "0" : txtMLAL.Text;
                m_liquidBulkItem.dschArmQty = string.IsNullOrEmpty(txtMLAD.Text) ? "0" : txtMLAD.Text;
                m_liquidBulkItem.CRUD = Constants.WS_UPDATE;
                m_liquidBulkItem.userId = UserInfo.getInstance().UserId;
                m_liquidBulkItem.workYmd = UserInfo.getInstance().Workdate;
                m_liquidBulkItem.shftId = UserInfo.getInstance().Shift;

                Object[] obj = { m_liquidBulkItem };
                DataItemCollection dataCollection = new DataItemCollection();
                dataCollection.collection = obj;

                proxy.updateVORLiquidHoseLines(dataCollection);
                //}
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

        private bool ProcessVORLiquidCargoCUD()
        {
            bool result = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                IVesselOperatorProxy proxy = new VesselOperatorProxy();

                // ref: CT108 & CT108003
                // Process only items that are modified.
                ArrayList arrObj = new ArrayList();
                if (m_arrGridData != null)
                {
                    for (int i = 0; i < m_arrGridData.Count; i++)
                    {
                        VORLiquidBulkItem item = (VORLiquidBulkItem)m_arrGridData[i];

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

                proxy.processVORLiquidCargoCUD(dataCollection);

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