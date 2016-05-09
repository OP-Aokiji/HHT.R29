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
using MOST.Client.Proxy.CommonProxy;
using MOST.Client.Proxy.WHCheckerProxy;
using MOST.Common;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.WHChecker;
using MOST.WHChecker.Parm;
using MOST.WHChecker.Result;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.WHChecker
{
    public partial class HWC106 : TForm, IPopupWindow
    {
        private const string DECREASE = "DC";
        private const string INCREASE = "IC";

        private const string HEADER_ITEM_STATUS = "_ITEM_STATUS";
        private const string HEADER_WS_NM = "Status";
        private const string HEADER_WH = "WH";
        private const string HEADER_BLGR = "BL/GR";
        private const string HEADER_MT = "MT";
        private const string HEADER_M3 = "M3";
        private const string HEADER_QTY = "Qty";
        private const string HEADER_DATETIME = "Date";
        private const string HEADER_TO_MT = "Amd MT";
        private const string HEADER_TO_M3 = "Amd M3";
        private const string HEADER_TO_QTY = "Amd Qty";
        private const string HEADER_JPVC = "_vslCallId";
        private const string HEADER_CHGWGT = "_chgWgt";
        private const string HEADER_CHGMSRMT = "_chgMsrmt";
        private const string HEADER_CHGPKGQTY = "_chgPkgQty";
        private const string HEADER_RCCOCD = "_rcCoCd";
        private const string HEADER_RCCONM = "RC.Cond";
        private const string HEADER_LOCID = "_locId";

        private HWC106Parm m_parm;
        private ArrayList m_arrGridData;

        public HWC106()
        {
            m_arrGridData = new ArrayList();
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            btnReconcile.Enabled = false;
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HWC106Parm)parm;
            if (m_parm != null)
            {
                txtSNBL.Text = m_parm.SnBlNo;
                txtSelCgCond.Text = m_parm.WhTpNm;
                txtSelMT.Text = m_parm.Wgt;
                txtSelM3.Text = m_parm.Msrmt;
                txtSelQty.Text = m_parm.PkgQty;

                GetWHRecnList();
            }
            CommonUtility.SetDTPValueDMYHM(txtDatetime, CommonUtility.GetCurrentServerTime());
            this.ShowDialog();
            return null;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_ITEM_STATUS, "0" }, { HEADER_WS_NM, "40" }, { HEADER_WH, "40" }, { HEADER_BLGR, "100" }, { HEADER_MT, "40" }, { HEADER_M3, "40" }, { HEADER_QTY, "40" } 
            , { HEADER_RCCONM, "70" } , { HEADER_DATETIME, "90" } , { HEADER_TO_MT, "40" }, { HEADER_TO_M3, "40" }, { HEADER_TO_QTY, "40" }
            , { HEADER_JPVC, "0" }, { HEADER_CHGWGT, "0" } , { HEADER_CHGMSRMT, "0" }, { HEADER_CHGPKGQTY, "0" }, { HEADER_RCCOCD, "0" }, { HEADER_LOCID, "0" }};
            this.grdData.setHeader(header);
        }

        private void GetWHRecnList()
        {
            try
            {
                #region Grid Data
                IWHCheckerProxy proxy = new WHCheckerProxy();
                WHReconciliationParm parm = new WHReconciliationParm();
                parm.searchType = "whrecndtl";
                parm.vslCallId = m_parm.VslCallId;
                parm.whTpCd = m_parm.WhTpCd;
                parm.cgNo = m_parm.CgNo;
                ResponseInfo info = proxy.getWHRecnList(parm);

                CommonUtility.InitializeCombobox(cboChangeCgCondition);
                grdData.Clear();
                if (info != null)
                {
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is CodeMasterListItem)
                        {
                            CodeMasterListItem item = (CodeMasterListItem)info.list[i];
                            cboChangeCgCondition.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                        }
                        else if (info.list[i] is CodeMasterListItem1)
                        {
                            CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                            cboChangeCgCondition.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                        }
                        else if (info.list[i] is WHReconciliationItem)
                        {
                            WHReconciliationItem item = (WHReconciliationItem)info.list[i];
                            DataRow newRow = grdData.NewRow();
                            newRow[HEADER_ITEM_STATUS] = Constants.ITEM_OLD;
                            newRow[HEADER_WS_NM] = Constants.WS_NM_INITIAL;
                            newRow[HEADER_WH] = item.locId;
                            newRow[HEADER_BLGR] = item.cgNo;
                            newRow[HEADER_MT] = item.wgt;
                            newRow[HEADER_M3] = item.msrmt;
                            newRow[HEADER_QTY] = item.pkgQty;
                            newRow[HEADER_DATETIME] = item.rcDt;
                            newRow[HEADER_TO_MT] = item.amdWgt;
                            newRow[HEADER_TO_M3] = item.amdMsrmt;
                            newRow[HEADER_TO_QTY] = item.amdPkgQty;

                            newRow[HEADER_JPVC] = item.vslCallId;
                            newRow[HEADER_CHGWGT] = item.chgWgt;
                            newRow[HEADER_CHGMSRMT] = item.chgMsrmt;
                            newRow[HEADER_CHGPKGQTY] = item.chgPkgQty;
                            newRow[HEADER_RCCOCD] = item.rcCoCd;
                            newRow[HEADER_RCCONM] = item.rcCoNm;
                            newRow[HEADER_LOCID] = item.locId;

                            grdData.Add(newRow);
                            m_arrGridData.Add(item);
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
            }
        }

        private bool ProcessWHReconciliationItems()
        {
            bool result = false;
            try
            {
                // Ref: CT213001
                Cursor.Current = Cursors.WaitCursor;

                IWHCheckerProxy proxy = new WHCheckerProxy();
                System.Collections.ArrayList arrObj = new System.Collections.ArrayList();

                if (grdData != null)
                {
                    for (int i = 0; i < grdData.DataTable.Rows.Count; i++)
                    {
                        // Process only items that are modified.
                        string crud = CommonUtility.GetCRUDFromName(grdData.DataTable.Rows[i][HEADER_WS_NM].ToString());
                        if (!Constants.WS_INITIAL.Equals(crud))
                        {
                            string strToLocid = grdData.DataTable.Rows[i][HEADER_WH].ToString();
                            string[] arrToLocid = strToLocid.Split('-');
                            strToLocid = arrToLocid[0] + "(" + arrToLocid[1] + ",1)";

                            WHReconciliationItem item = new WHReconciliationItem();
                            item.cgNo = grdData.DataTable.Rows[i][HEADER_BLGR].ToString();
                            item.toLocId = strToLocid;
                            item.opeClassCd = m_parm.OpeClassCd;
                            item.jobCoCd = m_parm.WhTpCd;
                            item.wgt = grdData.DataTable.Rows[i][HEADER_MT].ToString();
                            item.msrmt = grdData.DataTable.Rows[i][HEADER_M3].ToString();
                            item.pkgQty = grdData.DataTable.Rows[i][HEADER_QTY].ToString();
                            item.amdWgt = CommonUtility.ToStringNumber(grdData.DataTable.Rows[i][HEADER_TO_MT].ToString());
                            item.amdMsrmt = CommonUtility.ToStringNumber(grdData.DataTable.Rows[i][HEADER_TO_M3].ToString());
                            item.amdPkgQty = CommonUtility.ToStringNumber(grdData.DataTable.Rows[i][HEADER_TO_QTY].ToString());
                            item.vslCallId = grdData.DataTable.Rows[i][HEADER_JPVC].ToString();
                            item.chgWgt = grdData.DataTable.Rows[i][HEADER_CHGWGT].ToString();
                            item.chgMsrmt = grdData.DataTable.Rows[i][HEADER_CHGMSRMT].ToString();
                            item.chgPkgQty = grdData.DataTable.Rows[i][HEADER_CHGPKGQTY].ToString();
                            item.rcCoCd = grdData.DataTable.Rows[i][HEADER_RCCOCD].ToString();
                            item.rcCoNm = grdData.DataTable.Rows[i][HEADER_RCCONM].ToString();
                            item.locId = grdData.DataTable.Rows[i][HEADER_LOCID].ToString();
                            item.rcDt = grdData.DataTable.Rows[i][HEADER_DATETIME].ToString();
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

                    proxy.processWHReconciliationItems(dataCollection);
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

        private decimal GetChangeAmount(string orgData, string amdData)
        {
            decimal orgAmt = 0;
            decimal amdAmt = 0;
            decimal chgAmt = 0;

            if (!string.IsNullOrEmpty(amdData))
            {
                amdAmt = CommonUtility.ParseDecimal(amdData);
            }

            if (!string.IsNullOrEmpty(orgData))
            {
                orgAmt = CommonUtility.ParseDecimal(orgData);
                if (amdAmt != 0) chgAmt = amdAmt - orgAmt;
            }
            else
            {
                if (amdAmt != 0) chgAmt = orgAmt - amdAmt;
            }

            return chgAmt;
        }

        private string GetRecnCondition(decimal chgWgt, decimal chgMsrmt, decimal chgPkgQty)
        {
            string rtnValue = "";
            if (chgMsrmt > 0) rtnValue = INCREASE;
            if (chgMsrmt < 0) rtnValue = DECREASE;
            if (chgPkgQty > 0) rtnValue = INCREASE;
            if (chgPkgQty < 0) rtnValue = DECREASE;
            if (chgWgt > 0) rtnValue = INCREASE;
            if (chgWgt < 0) rtnValue = DECREASE;

            return rtnValue;
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
                        if (ProcessWHReconciliationItems())
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            this.Close();
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
                            ProcessWHReconciliationItems();
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

                case "btnReconcile":
                    if (Validate())
                    {
                        UpdateItem();
                    }
                    break;
            }
        }

        private void grdData_CurrentCellChanged(object sender, EventArgs e)
        {
            int index = grdData.CurrentRowIndex;
            if (index > -1 && index < grdData.DataTable.Rows.Count)
            {
                string strMT = grdData.DataTable.Rows[index][HEADER_MT].ToString();
                string strM3 = grdData.DataTable.Rows[index][HEADER_M3].ToString();
                string strQty = grdData.DataTable.Rows[index][HEADER_QTY].ToString();

                txtSelMT.Text = strMT;
                txtSelM3.Text = strM3;
                txtSelQty.Text = strQty;

                btnReconcile.Enabled = true;
            }
        }

        private void UpdateItem()
        {
            int index = grdData.CurrentRowIndex;
            if (index > -1 && index < grdData.DataTable.Rows.Count)
            {
                // Set Readonly False to update column data.
                DataTable dataTable = grdData.DataTable;
                int colCnt = dataTable.Columns.Count;
                for (int i = 0; i < colCnt; i++)
                {
                    dataTable.Columns[i].ReadOnly = false;
                }

                // Update columns data
                DataRow row = grdData.DataTable.Rows[index];
                if (Constants.ITEM_OLD.Equals(grdData.DataTable.Rows[index][HEADER_ITEM_STATUS].ToString()))
                {
                    row[HEADER_WS_NM] = Constants.WS_NM_UPDATE;
                }
                row[HEADER_DATETIME] = txtDatetime.Text;
                row[HEADER_TO_MT] = CommonUtility.ToStringNumber(txtChangeMT.Text);
                row[HEADER_TO_M3] = CommonUtility.ToStringNumber(txtChangeM3.Text);
                row[HEADER_TO_QTY] = CommonUtility.ToStringNumber(txtChangeQty.Text);

                
                // Check if condition is changed or not.
                bool isCondChanged = false;
                string strChgCgCond = CommonUtility.GetComboboxSelectedValue(cboChangeCgCondition);
                if (cboChangeCgCondition.SelectedIndex > 0 && 
                    (m_parm != null && !strChgCgCond.Equals(m_parm.WhTpCd)))
                {
                    isCondChanged = true;
                }

                // Condition is changed     ==> NORMAL/SHUTOUT/DAMAGE case
                // Condition is NOT changed ==> INCREASE/DECREASE case
                //if (cboChangeCgCondition.SelectedIndex > 0)
                if (isCondChanged)
                {
                    // NORMAL/SHUTOUT/DAMAGE case
                    decimal subMt = GetChangeAmount("", txtChangeMT.Text);
                    decimal subM3 = GetChangeAmount("", txtChangeM3.Text);
                    decimal subQty = GetChangeAmount("", txtChangeQty.Text);

                    row[HEADER_CHGWGT] = subMt.ToString();
                    row[HEADER_CHGMSRMT] = subM3.ToString();
                    row[HEADER_CHGPKGQTY] = subQty.ToString();
                    row[HEADER_RCCOCD] = strChgCgCond;
                    row[HEADER_RCCONM] = CommonUtility.GetComboboxSelectedDescription(cboChangeCgCondition);
                }
                else
                {
                    // INCREASE/DECREASE case
                    decimal subMt = GetChangeAmount(txtSelMT.Text, txtChangeMT.Text);
                    decimal subM3 = GetChangeAmount(txtSelM3.Text, txtChangeM3.Text);
                    decimal subQty = GetChangeAmount(txtSelQty.Text, txtChangeQty.Text);

                    row[HEADER_CHGWGT] = subMt.ToString();
                    row[HEADER_CHGMSRMT] = subM3.ToString();
                    row[HEADER_CHGPKGQTY] = subQty.ToString();
                    string rcCocd = GetRecnCondition(subMt, subM3, subQty);
                    row[HEADER_RCCOCD] = rcCocd;
                    if (INCREASE.Equals(rcCocd))
                    {
                        row[HEADER_RCCONM] = "Increase";
                    }
                    else
                    {
                        row[HEADER_RCCONM] = "Decrease";
                    }
                }

                grdData.DataTable.AcceptChanges();
                grdData.Refresh();

                // Reset original Readonly
                for (int i = 0; i < colCnt; i++)
                {
                    dataTable.Columns[i].ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// Validation of W/H reconcilation.
        /// </summary>
        /// <returns></returns>
        private bool Validate()
        {
            // Validate empty value
            if (string.IsNullOrEmpty(txtChangeMT.Text) &&
                string.IsNullOrEmpty(txtChangeQty.Text))
            {
                CommonUtility.AlertMessage("Please input Change Amount.");
                txtChangeMT.Focus();
                return false;
            }

            // Validate positive MT/QTY
            decimal chgMt = CommonUtility.ParseDecimal(txtChangeMT.Text);
            decimal chgQty = CommonUtility.ParseDecimal(txtChangeQty.Text);
            if (chgMt <= 0 && chgQty <= 0)
            {
                CommonUtility.AlertMessage("Please input positive MT/QTY.");
                txtChangeMT.SelectAll();
                txtChangeMT.Focus();
                return false;
            }

            // Check if condition is changed or not.
            bool isCondChanged = false;
            string strChgCgCond = CommonUtility.GetComboboxSelectedValue(cboChangeCgCondition);
            if (cboChangeCgCondition.SelectedIndex > 0 && 
                (m_parm != null && !strChgCgCond.Equals(m_parm.WhTpCd)))
            {
                isCondChanged = true;
            }

            // Check if amount is changed or not
            bool isAmntChanged = false;
            int index = grdData.CurrentRowIndex;
            if (index > -1 && index < grdData.DataTable.Rows.Count)
            {
                string orgMt = grdData.DataTable.Rows[index][HEADER_MT].ToString();
                string orgQty = grdData.DataTable.Rows[index][HEADER_QTY].ToString();

                if (CommonUtility.ParseDecimal(orgMt) != chgMt ||
                    CommonUtility.ParseDecimal(orgQty) != chgQty)
                {
                    isAmntChanged = true;
                }
            }

            if (!isCondChanged && !isAmntChanged)
            {
                CommonUtility.AlertMessage("Please input a different MT/QTY or change cargo condition.");
                return false;
            }

            return true;
        }
    }
}