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
    public partial class HWC109 : TForm, IPopupWindow
    {
        private const string REQTPCD_MOVEMENT = "MOV";

        private const string HEADER_ITEM_STATUS = "_ITEM_STATUS";
        private const string HEADER_WS_NM = "Status";
        private const string HEADER_JPVC = "JPVC";
        private const string HEADER_CGNO = "Cargo No";
        private const string HEADER_FMMT = "Fm MT";
        private const string HEADER_FMM3 = "Fm M3";
        private const string HEADER_FMQTY = "Fm Qty";
        private const string HEADER_FMLOC = "Fm Loc";
        private const string HEADER_FMWHTPNM = "Fm WHTp";
        private const string HEADER_TOMT = "To MT";
        private const string HEADER_TOM3 = "To M3";
        private const string HEADER_TOQTY = "To Qty";
        private const string HEADER_TOLOC = "To Loc";
        private const string HEADER_FMWHTPCD = "_whTpCd";
        private const string HEADER_RHDLMODE = "_rhdlMode";
        private const string HEADER_SPCACOCD = "_spCaCoCd";
        private const string HEADER_TOWHID = "_to_whId";
        private const string HEADER_TOROW = "_to_row";
        private const string HEADER_TOBAY = "_to_bay";

        private HWC109Parm m_parm;
        private CargoMovementItem m_item;
        private ArrayList m_unusedCells;
        private ArrayList m_whConfigItems;

        public HWC109()
        {
            m_unusedCells = new ArrayList();
            m_whConfigItems = new ArrayList();
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();

            List<string> controlNames = new List<string>();
            controlNames.Add(txtTime.Name);
            controlNames.Add(txtToMT.Name);
            controlNames.Add(txtToM3.Name);
            controlNames.Add(txtToQty.Name);
            controlNames.Add(cboFmLoc.Name);
            controlNames.Add(cboToWH.Name);
            controlNames.Add(cboToRow.Name);
            controlNames.Add(cboToBay.Name);
            this.AddOnChangeHandlerToInputControls(this.Controls, controlNames);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HWC109Parm)parm;
            if (m_parm != null)
            {
                txtCargoNo.Text = m_parm.CgNo;
                GetCargoMovements();
                if (m_item == null || string.IsNullOrEmpty(m_item.cgNo))
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HWC109_0001"));
                    this.Close();
                }
                else
                {
                    InitializeData();
                    this.ShowDialog();
                }
            
            }
            return null;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_ITEM_STATUS, "0" }, { HEADER_WS_NM, "40" }, { HEADER_JPVC, "100" }, { HEADER_CGNO, "70" }, 
                                { HEADER_FMMT, "40" }, { HEADER_FMM3, "40" }, { HEADER_FMQTY, "45" } , { HEADER_FMLOC, "60" } , { HEADER_FMWHTPNM, "60" } , 
                                { HEADER_TOMT, "40" }, { HEADER_TOM3, "40" }, { HEADER_TOQTY, "45" } , { HEADER_TOLOC, "60" }, 
                                { HEADER_FMWHTPCD, "0" }, { HEADER_RHDLMODE, "0" }, { HEADER_SPCACOCD, "0" }, { HEADER_TOWHID, "0" }, { HEADER_TOROW, "0" }, { HEADER_TOBAY, "0" }};
            this.grdData.setHeader(header);
        }

        private void InitializeFromWH()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Location
                IWHCheckerProxy proxy = new WHCheckerProxy();
                WhConfigurationParm parm = new WhConfigurationParm();
                parm.cgNo = txtCargoNo.Text;
                ResponseInfo info = proxy.getHHTWhViewList(parm);
                CommonUtility.InitializeCombobox(cboFmLoc, "Select");
                if (info != null)
                {
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is WhConfigurationItem)
                        {
                            WhConfigurationItem item = (WhConfigurationItem)info.list[i];
                            string strLoc = item.locId + "[" + item.whTpCdNm + "]";
                            cboFmLoc.Items.Add(new ComboboxValueDescriptionPair(item.locId, strLoc));

                            m_whConfigItems.Add(item);
                        }
                    }
                }
                #endregion
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

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                CommonUtility.SetDTPValueBlank(txtTime);
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                
                InitializeToWH();
                InitializeFromWH();

                // Planned Location
                GetPlannedLocation();
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

        private void InitializeToWH()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region W/H Id
                IWHCheckerProxy proxy = new WHCheckerProxy();
                CargoMasterParm parm = new CargoMasterParm();
                parm.locDivCd = "WHO";
                ResponseInfo info = proxy.getWHComboList(parm);
                CommonUtility.InitializeCombobox(cboToWH, "Select");
                if (info != null)
                {
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is CargoMasterItem)
                        {
                            CargoMasterItem item = (CargoMasterItem)info.list[i];
                            cboToWH.Items.Add(new ComboboxValueDescriptionPair(item.locId, item.locId));
                        }
                    }
                }
                #endregion
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

        private void InitializeRowBay()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (cboToWH != null && cboToWH.SelectedIndex > 0)
                {
                    #region Row
                    string strWhId = CommonUtility.GetComboboxSelectedValue(cboToWH);
                    IWHCheckerProxy proxy = new WHCheckerProxy();

                    WhConfigurationParm parm = new WhConfigurationParm();
                    parm.whId = strWhId;
                    parm.locDivCd = "ROW";
                    ResponseInfo rowInfo = proxy.getWhConfigurationList(parm);
                    CommonUtility.InitializeCombobox(cboToRow, "Select");
                    if (rowInfo != null)
                    {
                        for (int i = 0; i < rowInfo.list.Length; i++)
                        {
                            if (rowInfo.list[i] is WhConfigurationItem)
                            {
                                WhConfigurationItem item = (WhConfigurationItem)rowInfo.list[i];
                                cboToRow.Items.Add(new ComboboxValueDescriptionPair(item.rowwId, item.rowwId));
                            }
                        }
                    }
                    #endregion

                    #region Bay
                    parm.locDivCd = "BAY";
                    ResponseInfo bayInfo = proxy.getWhConfigurationList(parm);
                    CommonUtility.InitializeCombobox(cboToBay, "Select");
                    if (bayInfo != null)
                    {
                        for (int i = 0; i < bayInfo.list.Length; i++)
                        {
                            if (bayInfo.list[i] is WhConfigurationItem)
                            {
                                WhConfigurationItem item = (WhConfigurationItem)bayInfo.list[i];
                                cboToBay.Items.Add(new ComboboxValueDescriptionPair(item.bayId, item.bayId));
                            }
                        }
                    }
                    #endregion

                    #region Unused Bays
                    // Unused cell: A1,B5,C2
                    m_unusedCells.Clear();
                    parm.locDivCd = "CEL";
                    parm.locUseYn = "N";
                    ResponseInfo unusedBayInfo = proxy.getWhConfigurationList(parm);
                    if (unusedBayInfo != null)
                    {
                        for (int i = 0; i < unusedBayInfo.list.Length; i++)
                        {
                            if (unusedBayInfo.list[i] is WhConfigurationItem)
                            {
                                WhConfigurationItem item = (WhConfigurationItem)unusedBayInfo.list[i];
                                m_unusedCells.Add(item.rowwId + item.bayId);
                            }
                        }
                    }
                    #endregion
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

        private void GetPlannedLocation()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // Request Webservice
                IWHCheckerProxy proxy = new WHCheckerProxy();
                WhConfigurationParm parm = new WhConfigurationParm();
                parm.searchType = "planInfo";
                parm.blSn = !string.IsNullOrEmpty(m_parm.ShipgNoteNo) ? m_parm.ShipgNoteNo : m_parm.CgNo;
                parm.vslCallId = m_parm.VslCallId;
                parm.reqTpCd = REQTPCD_MOVEMENT;    // Space(SPC)/ Movement(MOV)

                ResponseInfo info = proxy.getWhViewList(parm);
                string strPlannedLoc = string.Empty;
                if (info != null)
                {
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is WhConfigurationItem)
                        {
                            WhConfigurationItem item = (WhConfigurationItem)info.list[i];
                            if (i > 0)
                            {
                                strPlannedLoc += "," + item.locId;
                                if (!string.IsNullOrEmpty(item.whTpCdNm))
                                {
                                    strPlannedLoc += "[" + item.whTpCdNm + "]";
                                }
                            }
                            else
                            {
                                strPlannedLoc += item.locId;
                                if (!string.IsNullOrEmpty(item.whTpCdNm))
                                {
                                    strPlannedLoc += "[" + item.whTpCdNm + "]";
                                }
                            }
                        }
                    }
                }
                txtPlannedLoc.Text = strPlannedLoc;
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

        private void GetCargoMovements()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                IWHCheckerProxy proxy = new WHCheckerProxy();
                CargoMovementParm parm = new CargoMovementParm();
                parm.vslCallId = m_parm.VslCallId;
                parm.cgNo = txtCargoNo.Text;
                if ("IM".Equals(m_parm.CaTyCd))
                {
                    parm.searchType = "IM";
                }
                else if ("EX".Equals(m_parm.CaTyCd))
                {
                    parm.searchType = "EX";
                }

                ResponseInfo info = proxy.getCargoMovements(parm);
                if (info != null && info.list != null && info.list.Length > 0 && info.list[0] is CargoMovementItem)
                {
                    CargoMovementItem item = (CargoMovementItem)info.list[0];
                    m_item = item;
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

        private void AddItem()
        {
            if (0 < cboFmLoc.SelectedIndex && cboFmLoc.SelectedIndex <= m_whConfigItems.Count)
            {
                // Current item of selected cell
                WhConfigurationItem currWhConfigItem = (WhConfigurationItem)m_whConfigItems[cboFmLoc.SelectedIndex - 1];

                DataRow newRow = grdData.NewRow();
                newRow[HEADER_ITEM_STATUS] = Constants.ITEM_NEW;
                newRow[HEADER_WS_NM] = Constants.WS_NM_INSERT;
                newRow[HEADER_JPVC] = m_parm.VslCallId;
                newRow[HEADER_CGNO] = m_parm.CgNo;
                newRow[HEADER_FMMT] = currWhConfigItem.wgt;
                newRow[HEADER_FMM3] = currWhConfigItem.msrmt;
                newRow[HEADER_FMQTY] = currWhConfigItem.pkgQty;
                newRow[HEADER_FMLOC] = currWhConfigItem.locId;
                newRow[HEADER_FMWHTPNM] = currWhConfigItem.whTpCdNm;
                newRow[HEADER_TOMT] = string.IsNullOrEmpty(txtToMT.Text) ? "0" : txtToMT.Text;
                newRow[HEADER_TOM3] = string.IsNullOrEmpty(txtToM3.Text) ? "0" : txtToM3.Text;
                newRow[HEADER_TOQTY] = string.IsNullOrEmpty(txtToQty.Text) ? "0" : txtToQty.Text;
                newRow[HEADER_TOLOC] = GetToLocation();
                newRow[HEADER_FMWHTPCD] = currWhConfigItem.whTpCd;
                newRow[HEADER_SPCACOCD] = currWhConfigItem.spCaCoCd;
                newRow[HEADER_TOWHID] = CommonUtility.GetComboboxSelectedValue(cboToWH);
                newRow[HEADER_TOROW] = CommonUtility.GetComboboxSelectedValue(cboToRow);
                newRow[HEADER_TOBAY] = CommonUtility.GetComboboxSelectedValue(cboToBay);
                grdData.Add(newRow);
                grdData.Refresh();
            }
        }

        private void UpdateItem()
        {
            if (grdData.CurrentRowIndex > 0)
            {
                // Set Readonly False to update column data.
                DataTable dataTable = grdData.DataTable;
                int colCnt = dataTable.Columns.Count;
                for (int i = 0; i < colCnt; i++)
                {
                    dataTable.Columns[i].ReadOnly = false;
                }

                // Update columns data
                // Current item of selected cell
                WhConfigurationItem currWhConfigItem = (WhConfigurationItem)m_whConfigItems[cboFmLoc.SelectedIndex - 1];

                DataRow row = grdData.DataTable.Rows[grdData.CurrentRowIndex];
                row[HEADER_FMMT] = currWhConfigItem.wgt;
                row[HEADER_FMM3] = currWhConfigItem.msrmt;
                row[HEADER_FMQTY] = currWhConfigItem.pkgQty;
                row[HEADER_FMLOC] = currWhConfigItem.locId;
                row[HEADER_FMWHTPNM] = currWhConfigItem.whTpCdNm;
                row[HEADER_TOMT] = string.IsNullOrEmpty(txtToMT.Text) ? "0" : txtToMT.Text;
                row[HEADER_TOM3] = string.IsNullOrEmpty(txtToM3.Text) ? "0" : txtToM3.Text;
                row[HEADER_TOQTY] = string.IsNullOrEmpty(txtToQty.Text) ? "0" : txtToQty.Text;
                row[HEADER_TOLOC] = GetToLocation();
                row[HEADER_FMWHTPCD] = currWhConfigItem.whTpCd;
                row[HEADER_SPCACOCD] = currWhConfigItem.spCaCoCd;
                row[HEADER_TOWHID] = CommonUtility.GetComboboxSelectedValue(cboToWH);
                row[HEADER_TOROW] = CommonUtility.GetComboboxSelectedValue(cboToRow);
                row[HEADER_TOBAY] = CommonUtility.GetComboboxSelectedValue(cboToBay);
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
            if (-1 < currRowIndex)
            {
                // In case item status is NEW: remove this row from grid.
                // In case item status is OLD: change WORKING STATUS of this row.
                string itemStatus = grdData.DataTable.Rows[currRowIndex][HEADER_ITEM_STATUS].ToString();
                if (Constants.ITEM_NEW.Equals(itemStatus))
                {
                    grdData.DataTable.AcceptChanges();
                    grdData.DataTable.Rows.RemoveAt(currRowIndex);
                    grdData.Refresh();
                }
            }
        }

        private string GetToLocation()
        {
            // To location, eg: 4A-F9
            StringBuilder sbToLoc = new StringBuilder();
            sbToLoc.Append(CommonUtility.GetComboboxSelectedValue(cboToWH));
            sbToLoc.Append("-");
            sbToLoc.Append(CommonUtility.GetComboboxSelectedValue(cboToRow));
            sbToLoc.Append(CommonUtility.GetComboboxSelectedValue(cboToBay));
            return sbToLoc.ToString();
        }

        private bool IsExistAlready(int mode)
        {
            if (grdData != null && grdData.DataTable != null && grdData.DataTable.Rows.Count > 0)
            {
                // Make sure FROM LOCATION && TO LOCATION to be unique.
                string strToLocCbo = GetToLocation();
                string strFmLocCbo = CommonUtility.GetComboboxSelectedValue(cboFmLoc);
                string strFmWhTpCbo = string.Empty;

                int currIndex = cboFmLoc.SelectedIndex;
                if (currIndex > 0)
                {
                    WhConfigurationItem currWhConfigItem = (WhConfigurationItem)m_whConfigItems[currIndex - 1];
                    strFmWhTpCbo = currWhConfigItem.whTpCd;
                }

                int count = grdData.DataTable.Rows.Count;
                int index = count - 1;
                string strFmLoc = grdData.DataTable.Rows[index][HEADER_FMLOC].ToString();
                string strFmWhTp = grdData.DataTable.Rows[index][HEADER_FMWHTPCD].ToString();
                string strToLoc = grdData.DataTable.Rows[index][HEADER_TOLOC].ToString();
                while (!strFmLocCbo.Equals(strFmLoc) || !strFmWhTpCbo.Equals(strFmWhTp) || !strToLocCbo.Equals(strToLoc))
                {
                    index = index - 1;
                    if (index < 0)
                    {
                        return false;
                    }
                    if (mode == Constants.MODE_ADD)
                    {
                        strFmLoc = grdData.DataTable.Rows[index][HEADER_FMLOC].ToString();
                        strFmWhTp = grdData.DataTable.Rows[index][HEADER_FMWHTPCD].ToString();
                        strToLoc = grdData.DataTable.Rows[index][HEADER_TOLOC].ToString();
                    }
                    else if (mode == Constants.MODE_UPDATE)
                    {
                        if (index != grdData.CurrentRowIndex)
                        {
                            strFmLoc = grdData.DataTable.Rows[index][HEADER_FMLOC].ToString();
                            strFmWhTp = grdData.DataTable.Rows[index][HEADER_FMWHTPCD].ToString();
                            strToLoc = grdData.DataTable.Rows[index][HEADER_TOLOC].ToString();
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

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnOk":
                    if (grdData.IsDirty)
                    {
                        if (ProcessCargoMovementItem())
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
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
                            ProcessCargoMovementItem();
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
                    if (this.validations(this.Controls) && !IsUnusedCell() && ValidateAmount())
                    {
                        // Check constraints: TO LOCATION
                        if (!IsExistAlready(Constants.MODE_ADD))
                        {
                            AddItem();
                            CalcBalAmt();
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
                            cboFmLoc.Focus();
                        }
                    }
                    break;

                case "btnUpdate":
                    if (this.validations(this.Controls) && !IsUnusedCell() && ValidateAmount())
                    {
                        if (!IsExistAlready(Constants.MODE_UPDATE))
                        {
                            UpdateItem();
                            CalcBalAmt();
                        }
                    }
                    break;

                case "btnDelete":
                    if (DialogResult.Yes == CommonUtility.ConfirmMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0046")))
                    {
                        DeleteItem();
                        CalcBalAmt();
                    }
                    break;
            }
        }

        private void grdData_CurrentCellChanged(object sender, EventArgs e)
        {
            int index = grdData.CurrentRowIndex;
            if (index > -1 && index < grdData.DataTable.Rows.Count)
            {
                string strFmLoc = grdData.DataTable.Rows[index][HEADER_FMLOC].ToString();
                string strToMT = grdData.DataTable.Rows[index][HEADER_TOMT].ToString();
                string strToM3 = grdData.DataTable.Rows[index][HEADER_TOM3].ToString();
                string strToQty = grdData.DataTable.Rows[index][HEADER_TOQTY].ToString();
                string strToLoc = grdData.DataTable.Rows[index][HEADER_TOLOC].ToString();
                string strToRow = grdData.DataTable.Rows[index][HEADER_TOROW].ToString();
                string strToBay = grdData.DataTable.Rows[index][HEADER_TOBAY].ToString();

                txtToMT.Text = strToMT;
                txtToM3.Text = strToM3;
                txtToQty.Text = strToQty;
                CommonUtility.SetComboboxSelectedItem(cboFmLoc, strFmLoc);
                CommonUtility.SetComboboxSelectedItem(cboToWH, strToLoc);
                CommonUtility.SetComboboxSelectedItem(cboToRow, strToRow);
                CommonUtility.SetComboboxSelectedItem(cboToBay, strToBay);

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

        private bool ValidateAmount()
        {
            double balMt = CommonUtility.ParseDouble(txtBalMT.Text);
            double balM3 = CommonUtility.ParseDouble(txtBalM3.Text);
            int balQty = CommonUtility.ParseInt(txtBalQty.Text);

            double actMt = CommonUtility.ParseDouble(txtToMT.Text);
            double actM3 = CommonUtility.ParseDouble(txtToM3.Text);
            int actQty = CommonUtility.ParseInt(txtToQty.Text);

            if (actMt == 0 && actM3 == 0 && actQty == 0)
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HWC109_0002"));
                return false;
            }

            if (balQty < actQty || balMt < actMt || balM3 < actM3)
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HWC101001_0001"));
                return false;
            }

            return true;
        }

        private bool IsUnusedCell()
        {
            bool result = false;

            // Selected cell: A2
            string selectedCell = CommonUtility.GetComboboxSelectedValue(cboToRow) + CommonUtility.GetComboboxSelectedValue(cboToBay);
            if (m_unusedCells.Contains(selectedCell))
            {
                result = true;
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HWC101001_0002"));
            }

            return result;
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox mycbo = (ComboBox)sender;
            String cboName = mycbo.Name;
            switch (cboName)
            {
                case "cboFmLoc":
                    CalcBalAmt();
                    break;

                case "cboToWH":
                    InitializeRowBay();
                    break;
            }
        }

        /// <summary>
        /// Calculate balance amount of selected cell.
        /// Balance amount = (amount that was stacked into W/H before) - (total amount that was moved to other cells)
        /// </summary>
        private void CalcBalAmt()
        {

            try
            {
                string strFmWhTpNm = string.Empty;
                string strFmWhTpCd;
                string strFmLocId;
                double dblToMt = 0;
                double dblToM3 = 0;
                int intToQty = 0;
                double dblBalMt = 0;
                double dblBalM3 = 0;
                int intBalQty = 0;
                int currIndex = cboFmLoc.SelectedIndex;
                if (currIndex > 0)
                {
                    // Amount that was stacked into W/H before
                    WhConfigurationItem currWhConfigItem = (WhConfigurationItem)m_whConfigItems[currIndex - 1];
                    double dblFmMt = currWhConfigItem.wgt;
                    double dblFmM3 = currWhConfigItem.msrmt;
                    int intFmQty = CommonUtility.ParseInt(currWhConfigItem.pkgQty);
                    strFmWhTpNm = currWhConfigItem.whTpCdNm;
                    strFmWhTpCd = currWhConfigItem.whTpCd;
                    strFmLocId = currWhConfigItem.locId;

                    // Total amount that was moved to other cells (calculated from grid data)
                    int rowCnt = grdData.DataTable.Rows.Count;
                    for (int i = 0; i < rowCnt; i++)
                    {
                        string strMvWhTpCd = grdData.DataTable.Rows[i][HEADER_FMWHTPCD].ToString();
                        string strMvLocId = grdData.DataTable.Rows[i][HEADER_FMLOC].ToString();
                        if (strFmLocId.Equals(strMvLocId) && strFmWhTpCd.Equals(strMvWhTpCd))
                        {
                            dblToMt += CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_TOMT].ToString());
                            dblToM3 += CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_TOM3].ToString());
                            intToQty += CommonUtility.ParseInt(grdData.DataTable.Rows[i][HEADER_TOQTY].ToString());
                        }
                    }

                    // Calculate balance amount
                    dblBalMt = dblFmMt - dblToMt;
                    dblBalM3 = dblFmM3 - dblToM3;
                    intBalQty = intFmQty - intToQty;
                }
                txtBalMT.Text = dblBalMt.ToString();
                txtBalM3.Text = dblBalM3.ToString();
                txtBalQty.Text = intBalQty.ToString();
                txtFmWhTp.Text = strFmWhTpNm;

                txtToMT.Text = txtBalMT.Text;
                txtToM3.Text = txtBalM3.Text;
                txtToQty.Text = txtBalQty.Text;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private bool ProcessCargoMovementItem()
        {
            // ref: CT121010 && CT113006
            bool result = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (m_item != null)
                {

                    ArrayList whConfigList = new ArrayList();
                    IWHCheckerProxy proxy = new WHCheckerProxy();
                    CargoMovementItem item = m_item;
                    item.vslCallId = m_parm.VslCallId;

                    //item.jobNo
                    item.cgNo = txtCargoNo.Text;
                    item.stDt = txtTime.Text;
                    item.endDt = txtTime.Text;

                    string whPreFromName = CommonUtility.ToString(item.whId);
                    string whPreToName = CommonUtility.ToString(item.whId);
                    string fmTotalLoc = string.Empty;
                    string toTotalLoc = string.Empty;
                    if (grdData != null && grdData.DataTable != null && grdData.DataTable.Rows.Count > 0)
                    {
                        int rowCnt = grdData.DataTable.Rows.Count;

                        string strFromLocId = grdData.DataTable.Rows[0][HEADER_FMLOC].ToString();
                        string strToLocId = grdData.DataTable.Rows[0][HEADER_TOLOC].ToString();

                        if (!string.IsNullOrEmpty(strFromLocId))
                        {
                            whPreFromName = strFromLocId.Substring(0, strFromLocId.IndexOf("-", 0));
                            fmTotalLoc = whPreFromName + "(" + strFromLocId.Substring(strFromLocId.IndexOf("-", 0) + 1) + "," + rowCnt + ")";
                        }
                        if (!string.IsNullOrEmpty(strToLocId))
                        {
                            whPreToName = strToLocId.Substring(0, strToLocId.IndexOf("-", 0));
                            toTotalLoc = whPreToName + "(" + strToLocId.Substring(strToLocId.IndexOf("-", 0) + 1) + "," + rowCnt + ")";
                        }
                    }
                    item.fmLocId = fmTotalLoc;
                    item.toLocId = toTotalLoc;
                    item.allocateYN = "N";

                    string fmLoc = string.Empty; string toLoc = string.Empty; int iLoc = 0;
                    string fmDmgLoc = string.Empty; string toDmgLoc = string.Empty; int iDmgLoc = 0;
                    string fmShuLoc = string.Empty; string toShuLoc = string.Empty; int iShuLoc = 0;
                    string fmDmgRhdlCLoc = string.Empty; string toDmgRhdlCLoc = string.Empty; int iDmgRhdlCLoc = 0;
                    string fmDmgRhdlRLoc = string.Empty; string toDmgRhdlRLoc = string.Empty; int iDmgRhdlRLoc = 0;
                    string fmShuRhdlCLoc = string.Empty; string toShuRhdlCLoc = string.Empty; int iShuRhdlCLoc = 0;
                    string fmShuRhdlRLoc = string.Empty; string toShuRhdlRLoc = string.Empty; int iShuRhdlRLoc = 0;

                    string fmSpSLoc = string.Empty; string toSpSLoc = string.Empty; int iSpSLoc = 0;
                    string fmSpSDmgLoc = string.Empty; string toSpSDmgLoc = string.Empty; int iSpSDmgLoc = 0;
                    string fmSpSShuLoc = string.Empty; string toSpSShuLoc = string.Empty; int iSpSShuLoc = 0;
                    string fmSpSDmgRhdlCLoc = string.Empty; string toSpSDmgRhdlCLoc = string.Empty; int iSpSDmgRhdlCLoc = 0;
                    string fmSpSDmgRhdlRLoc = string.Empty; string toSpSDmgRhdlRLoc = string.Empty; int iSpSDmgRhdlRLoc = 0;
                    string fmSpSShuRhdlCLoc = string.Empty; string toSpSShuRhdlCLoc = string.Empty; int iSpSShuRhdlCLoc = 0;
                    string fmSpSShuRhdlRLoc = string.Empty; string toSpSShuRhdlRLoc = string.Empty; int iSpSShuRhdlRLoc = 0;

                    string fmSpOLoc = string.Empty; string toSpOLoc = string.Empty; int iSpOLoc = 0;
                    string fmSpODmgLoc = string.Empty; string toSpODmgLoc = string.Empty; int iSpODmgLoc = 0;
                    string fmSpOShuLoc = string.Empty; string toSpOShuLoc = string.Empty; int iSpOShuLoc = 0;
                    string fmSpODmgRhdlCLoc = string.Empty; string toSpODmgRhdlCLoc = string.Empty; int iSpODmgRhdlCLoc = 0;
                    string fmSpODmgRhdlRLoc = string.Empty; string toSpODmgRhdlRLoc = string.Empty; int iSpODmgRhdlRLoc = 0;
                    string fmSpOShuRhdlCLoc = string.Empty; string toSpOShuRhdlCLoc = string.Empty; int iSpOShuRhdlCLoc = 0;
                    string fmSpOShuRhdlRLoc = string.Empty; string toSpOShuRhdlRLoc = string.Empty; int iSpOShuRhdlRLoc = 0;

                    if (grdData != null && grdData.DataTable != null && grdData.DataTable.Rows.Count > 0)
                    {
                        int rowCnt = grdData.DataTable.Rows.Count;
                        for (int i = 0; i < rowCnt; i++)
                        {
                            // Add WhConfigurationItem items to collection
                            WhConfigurationItem whConfigItem = new WhConfigurationItem();
                            whConfigItem.vslCallId = grdData.DataTable.Rows[i][HEADER_JPVC].ToString();
                            whConfigItem.cgNo = grdData.DataTable.Rows[i][HEADER_CGNO].ToString();
                            whConfigItem.fmLocId = grdData.DataTable.Rows[i][HEADER_FMLOC].ToString();
                            whConfigItem.toLocId = grdData.DataTable.Rows[i][HEADER_TOLOC].ToString();
                            whConfigItem.wgt = CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_TOMT].ToString());
                            whConfigItem.msrmt = CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_TOM3].ToString());
                            whConfigItem.pkgQty = grdData.DataTable.Rows[i][HEADER_TOQTY].ToString();
                            whConfigItem.fmWgt = CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_FMMT].ToString());
                            whConfigItem.fmMsrmt = CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_FMM3].ToString());
                            whConfigItem.fmPkgQty = CommonUtility.ParseInt(grdData.DataTable.Rows[i][HEADER_FMQTY].ToString());
                            whConfigItem.whTpCd = grdData.DataTable.Rows[i][HEADER_FMWHTPCD].ToString();
                            whConfigItem.rhdlMode = grdData.DataTable.Rows[i][HEADER_RHDLMODE].ToString();
                            whConfigItem.spCaCoCd = grdData.DataTable.Rows[i][HEADER_SPCACOCD].ToString();
                            whConfigList.Add(whConfigItem);

                            string strWhTpCd = grdData.DataTable.Rows[i][HEADER_FMWHTPCD].ToString();
                            string strRhdlMode = grdData.DataTable.Rows[i][HEADER_RHDLMODE].ToString();
                            string strSpCaCoCd = grdData.DataTable.Rows[i][HEADER_SPCACOCD].ToString();

                            string _fmLoc = grdData.DataTable.Rows[i][HEADER_FMLOC].ToString();
                            string _toLoc = grdData.DataTable.Rows[i][HEADER_TOLOC].ToString();

                            // None of special cargo
                            if (string.IsNullOrEmpty(strSpCaCoCd))
                            {
                                // Normal cargo
                                if ("G".Equals(strWhTpCd))
                                {
                                    if (string.IsNullOrEmpty(fmLoc) || string.IsNullOrEmpty(toLoc))
                                    {
                                        fmLoc = _fmLoc;
                                        toLoc = _toLoc;
                                    }
                                    iLoc += 1;
                                }
                                // Damage cargo
                                else if ("D".Equals(strWhTpCd))
                                {
                                    // Damage
                                    if (string.IsNullOrEmpty(strRhdlMode))
                                    {
                                        if (string.IsNullOrEmpty(fmDmgLoc) || string.IsNullOrEmpty(toDmgLoc))
                                        {
                                            fmDmgLoc = _fmLoc;
                                            toDmgLoc = _toLoc;
                                        }
                                        iDmgLoc += 1;
                                    }
                                    // Damage cargo with re-handling with Mode = C
                                    else if ("C".Equals(strRhdlMode))
                                    {
                                        if (string.IsNullOrEmpty(fmDmgRhdlCLoc) || string.IsNullOrEmpty(toDmgRhdlCLoc))
                                        {
                                            fmDmgRhdlCLoc = _fmLoc;
                                            toDmgRhdlCLoc = _toLoc;
                                        }
                                        iDmgRhdlCLoc += 1;
                                    }
                                    // Damage cargo with re-handling with Mode = R
                                    else if ("R".Equals(strRhdlMode))
                                    {
                                        if (string.IsNullOrEmpty(fmDmgRhdlRLoc) || string.IsNullOrEmpty(toDmgRhdlRLoc))
                                        {
                                            fmDmgRhdlRLoc = _fmLoc;
                                            toDmgRhdlRLoc = _toLoc;
                                        }
                                        iDmgRhdlRLoc += 1;
                                    }
                                }
                                // Shut-out cargo
                                else if ("S".Equals(strWhTpCd))
                                {
                                    if (string.IsNullOrEmpty(strRhdlMode))
                                    {
                                        if (string.IsNullOrEmpty(fmShuLoc) || string.IsNullOrEmpty(toShuLoc))
                                        {
                                            fmShuLoc = _fmLoc;
                                            toShuLoc = _toLoc;
                                        }
                                        iShuLoc += 1;
                                    }
                                    else if ("C".Equals(strRhdlMode))
                                    {
                                        //Shut-out cargo with Mode = C
                                        if (string.IsNullOrEmpty(fmShuRhdlCLoc) || string.IsNullOrEmpty(toShuRhdlCLoc))
                                        {
                                            fmShuRhdlCLoc = _fmLoc;
                                            toShuRhdlCLoc = _toLoc;
                                        }
                                        iShuRhdlCLoc += 1;
                                    }
                                    else if ("R".Equals(strRhdlMode))
                                    {
                                        //Shut-out cargo with Mode = R
                                        if (string.IsNullOrEmpty(fmShuRhdlRLoc) || string.IsNullOrEmpty(toShuRhdlRLoc))
                                        {
                                            fmShuRhdlRLoc = _fmLoc;
                                            toShuRhdlRLoc = _toLoc;
                                        }
                                        iShuRhdlRLoc += 1;
                                    }
                                }
                            }
                            // Spare
                            else if ("S".Equals(strSpCaCoCd))
                            {
                                if ("G".Equals(strWhTpCd))
                                {
                                    //General cargo
                                    if (string.IsNullOrEmpty(fmSpSLoc) || string.IsNullOrEmpty(toSpSLoc))
                                    {
                                        fmSpSLoc = _fmLoc;
                                        toSpSLoc = _toLoc;
                                    }
                                    iSpSLoc += 1;
                                }
                                else if ("D".Equals(strWhTpCd))
                                {
                                    //Damage cargo
                                    if (string.IsNullOrEmpty(strRhdlMode))
                                    {
                                        if (string.IsNullOrEmpty(fmSpSDmgLoc) || string.IsNullOrEmpty(toSpSDmgLoc))
                                        {
                                            fmSpSDmgLoc = _fmLoc;
                                            toSpSDmgLoc = _toLoc;
                                        }
                                        iSpSDmgLoc += 1;
                                    }
                                    else if ("C".Equals(strRhdlMode))
                                    {
                                        //Damage cargo with re-handling with Mode = C
                                        if (string.IsNullOrEmpty(fmSpSDmgRhdlCLoc) || string.IsNullOrEmpty(toSpSDmgRhdlCLoc))
                                        {
                                            fmSpSDmgRhdlCLoc = _fmLoc;
                                            toSpSDmgRhdlCLoc = _toLoc;
                                        }
                                        iSpSDmgRhdlCLoc += 1;
                                    }
                                    else if ("R".Equals(strRhdlMode))
                                    {
                                        //Damage cargo with re-handling with Mode = L
                                        if (string.IsNullOrEmpty(fmSpSDmgRhdlRLoc) || string.IsNullOrEmpty(toSpSDmgRhdlRLoc))
                                        {
                                            fmSpSDmgRhdlRLoc = _fmLoc;
                                            toSpSDmgRhdlRLoc = _toLoc;
                                        }
                                        iSpSDmgRhdlRLoc += 1;
                                    }
                                }
                                else if ("S".Equals(strWhTpCd))
                                {
                                    //Shut-out cargo
                                    if (string.IsNullOrEmpty(strRhdlMode))
                                    {
                                        if (string.IsNullOrEmpty(fmSpSShuLoc) || string.IsNullOrEmpty(toSpSShuLoc))
                                        {
                                            fmSpSShuLoc = _fmLoc;
                                            toSpSShuLoc = _toLoc;
                                        }
                                        iSpSShuLoc += 1;
                                    }
                                    else if ("C".Equals(strRhdlMode))
                                    {
                                        //Shut-out cargo with Mode = C
                                        if (string.IsNullOrEmpty(fmSpSShuRhdlCLoc) || string.IsNullOrEmpty(toSpSShuRhdlCLoc))
                                        {
                                            fmSpSShuRhdlCLoc = _fmLoc;
                                            toSpSShuRhdlCLoc = _toLoc;
                                        }
                                        iSpSShuRhdlCLoc += 1;
                                    }
                                    else if ("R".Equals(strRhdlMode))
                                    {
                                        //Shut-out cargo with Mode = R
                                        if (string.IsNullOrEmpty(fmSpSShuRhdlRLoc) || string.IsNullOrEmpty(toSpSShuRhdlRLoc))
                                        {
                                            fmSpSShuRhdlRLoc = _fmLoc;
                                            toSpSShuRhdlRLoc = _toLoc;
                                        }
                                        iSpSShuRhdlRLoc += 1;
                                    }
                                }
                            }
                            // ???
                            else if ("O".Equals(strSpCaCoCd))
                            {
                                if ("G".Equals(strWhTpCd))
                                {
                                    //General cargo
                                    if (string.IsNullOrEmpty(fmSpOLoc) || string.IsNullOrEmpty(toSpOLoc))
                                    {
                                        fmSpOLoc = _fmLoc;
                                        toSpOLoc = _toLoc;
                                    }
                                    iSpOLoc += 1;
                                }
                                else if ("D".Equals(strWhTpCd))
                                {
                                    //Damage cargo
                                    if (string.IsNullOrEmpty(strRhdlMode))
                                    {
                                        if (string.IsNullOrEmpty(fmSpODmgLoc) || string.IsNullOrEmpty(toSpODmgLoc))
                                        {
                                            fmSpODmgLoc = _fmLoc;
                                            toSpODmgLoc = _toLoc;
                                        }
                                        iSpODmgLoc += 1;
                                    }
                                    else if ("C".Equals(strRhdlMode))
                                    {
                                        //Damage cargo with re-handling with Mode = C
                                        if (string.IsNullOrEmpty(fmSpODmgRhdlCLoc) || string.IsNullOrEmpty(toSpODmgRhdlCLoc))
                                        {
                                            fmSpODmgRhdlCLoc = _fmLoc;
                                            toSpODmgRhdlCLoc = _toLoc;
                                        }
                                        iSpODmgRhdlCLoc += 1;
                                    }
                                    else if ("R".Equals(strRhdlMode))
                                    {
                                        //Damage cargo with re-handling with Mode = R
                                        if (string.IsNullOrEmpty(fmSpODmgRhdlRLoc) || string.IsNullOrEmpty(toSpODmgRhdlRLoc))
                                        {
                                            fmSpODmgRhdlRLoc = _fmLoc;
                                            toSpODmgRhdlRLoc = _toLoc;
                                        }
                                        iSpODmgRhdlRLoc += 1;
                                    }
                                }
                                else if ("S".Equals(strWhTpCd))
                                {
                                    //Shut-out cargo
                                    if (string.IsNullOrEmpty(strRhdlMode))
                                    {
                                        if (string.IsNullOrEmpty(fmSpOShuLoc) || string.IsNullOrEmpty(toSpOShuLoc))
                                        {
                                            fmSpOShuLoc = _fmLoc;
                                            toSpOShuLoc = _toLoc;
                                        }
                                        iSpOShuLoc += 1;
                                    }
                                    else if ("C".Equals(strRhdlMode))
                                    {
                                        //Shut-out cargo with Mode = C
                                        if (string.IsNullOrEmpty(fmSpOShuRhdlCLoc) || string.IsNullOrEmpty(toSpOShuRhdlCLoc))
                                        {
                                            fmSpOShuRhdlCLoc = _fmLoc;
                                            toSpOShuRhdlCLoc = _toLoc;
                                        }
                                        iSpOShuRhdlCLoc += 1;
                                    }
                                    else if ("R".Equals(strRhdlMode))
                                    {
                                        //Shut-out cargo with Mode = R
                                        if (string.IsNullOrEmpty(fmSpOShuRhdlRLoc) || string.IsNullOrEmpty(toSpOShuRhdlRLoc))
                                        {
                                            fmSpOShuRhdlRLoc = _fmLoc;
                                            toSpOShuRhdlRLoc = _toLoc;
                                        }
                                        iSpOShuRhdlRLoc += 1;
                                    }
                                }
                            }

                            double dblMt = CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_TOMT].ToString());
                            double dblM3 = CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_TOM3].ToString());
                            int dblQty = CommonUtility.ParseInt(grdData.DataTable.Rows[i][HEADER_TOQTY].ToString());
                            item.wgt = dblMt;
                            item.msrmt = dblM3;
                            item.pkgQty = dblQty;

                            if (!string.IsNullOrEmpty(fmLoc))
                            {
                                item.fmLoc = whPreFromName + "(" + fmLoc.Substring(fmLoc.IndexOf("-", 0) + 1) + "," + iLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toLoc))
                            {
                                item.toLoc = whPreToName + "(" + toLoc.Substring(toLoc.IndexOf("-", 0) + 1) + "," + iLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmDmgLoc))
                            {
                                item.fmDmgLoc = whPreFromName + "(" + fmDmgLoc.Substring(fmDmgLoc.IndexOf("-", 0) + 1) + "," + iDmgLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toDmgLoc))
                            {
                                item.toDmgLoc = whPreToName + "(" + toDmgLoc.Substring(toDmgLoc.IndexOf("-", 0) + 1) + "," + iDmgLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmDmgRhdlCLoc))
                            {
                                item.fmDmgRhdlCLoc = whPreFromName + "(" + fmDmgRhdlCLoc.Substring(fmDmgRhdlCLoc.IndexOf("-", 0) + 1) + "," + iDmgRhdlCLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toDmgRhdlCLoc))
                            {
                                item.toDmgRhdlCLoc = whPreToName + "(" + toDmgRhdlCLoc.Substring(toDmgRhdlCLoc.IndexOf("-", 0) + 1) + "," + iDmgRhdlCLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmDmgRhdlRLoc))
                            {
                                item.fmDmgRhdlRLoc = whPreFromName + "(" + fmDmgRhdlRLoc.Substring(fmDmgRhdlRLoc.IndexOf("-", 0) + 1) + "," + iDmgRhdlRLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toDmgRhdlRLoc))
                            {
                                item.toDmgRhdlRLoc = whPreToName + "(" + toDmgRhdlRLoc.Substring(toDmgRhdlRLoc.IndexOf("-", 0) + 1) + "," + iDmgRhdlRLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmShuLoc))
                            {
                                item.fmShuLoc = whPreFromName + "(" + fmShuLoc.Substring(fmShuLoc.IndexOf("-", 0) + 1) + "," + iShuLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toShuLoc))
                            {
                                item.toShuLoc = whPreToName + "(" + toShuLoc.Substring(toShuLoc.IndexOf("-", 0) + 1) + "," + iShuLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmShuRhdlCLoc))
                            {
                                item.fmShuRhdlCLoc = whPreFromName + "(" + fmShuRhdlCLoc.Substring(fmShuRhdlCLoc.IndexOf("-", 0) + 1) + "," + iShuRhdlCLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toShuRhdlCLoc))
                            {
                                item.toShuRhdlCLoc = whPreToName + "(" + toShuRhdlCLoc.Substring(toShuRhdlCLoc.IndexOf("-", 0) + 1) + "," + iShuRhdlCLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmShuRhdlRLoc))
                            {
                                item.fmShuRhdlRLoc = whPreFromName + "(" + fmShuRhdlRLoc.Substring(fmShuRhdlRLoc.IndexOf("-", 0) + 1) + "," + iShuRhdlRLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toShuRhdlRLoc))
                            {
                                item.toShuRhdlRLoc = whPreToName + "(" + toShuRhdlRLoc.Substring(toShuRhdlRLoc.IndexOf("-", 0) + 1) + "," + iShuRhdlRLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmSpSLoc))
                            {
                                item.fmSpSLoc = whPreFromName + "(" + fmSpSLoc.Substring(fmSpSLoc.IndexOf("-", 0) + 1) + "," + iSpSLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toSpSLoc))
                            {
                                item.toSpSLoc = whPreToName + "(" + toSpSLoc.Substring(toSpSLoc.IndexOf("-", 0) + 1) + "," + iSpSLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmSpSDmgLoc))
                            {
                                item.fmSpSDmgLoc = whPreFromName + "(" + fmSpSDmgLoc.Substring(fmSpSDmgLoc.IndexOf("-", 0) + 1) + "," + iSpSDmgLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toSpSDmgLoc))
                            {
                                item.toSpSDmgLoc = whPreToName + "(" + toSpSDmgLoc.Substring(toSpSDmgLoc.IndexOf("-", 0) + 1) + "," + iSpSDmgLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmSpSDmgRhdlCLoc))
                            {
                                item.fmSpSDmgRhdlCLoc = whPreFromName + "(" + fmSpSDmgRhdlCLoc.Substring(fmSpSDmgRhdlCLoc.IndexOf("-", 0) + 1) + "," + iSpSDmgRhdlCLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toSpSDmgRhdlCLoc))
                            {
                                item.toSpSDmgRhdlCLoc = whPreToName + "(" + toSpSDmgRhdlCLoc.Substring(toSpSDmgRhdlCLoc.IndexOf("-", 0) + 1) + "," + iSpSDmgRhdlCLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmSpSDmgRhdlRLoc))
                            {
                                item.fmSpSDmgRhdlRLoc = whPreFromName + "(" + fmSpSDmgRhdlRLoc.Substring(fmSpSDmgRhdlRLoc.IndexOf("-", 0) + 1) + "," + iSpSDmgRhdlRLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toSpSDmgRhdlRLoc))
                            {
                                item.toSpSDmgRhdlRLoc = whPreToName + "(" + toSpSDmgRhdlRLoc.Substring(toSpSDmgRhdlRLoc.IndexOf("-", 0) + 1) + "," + iSpSDmgRhdlRLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmSpSShuLoc))
                            {
                                item.fmSpSShuLoc = whPreFromName + "(" + fmSpSShuLoc.Substring(fmSpSShuLoc.IndexOf("-", 0) + 1) + "," + iSpSShuLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toSpSShuLoc))
                            {
                                item.toSpSShuLoc = whPreToName + "(" + toSpSShuLoc.Substring(toSpSShuLoc.IndexOf("-", 0) + 1) + "," + iSpSShuLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmSpSShuRhdlCLoc))
                            {
                                item.fmSpSShuRhdlCLoc = whPreFromName + "(" + fmSpSShuRhdlCLoc.Substring(fmSpSShuRhdlCLoc.IndexOf("-", 0) + 1) + "," + iSpSShuRhdlCLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toSpSShuRhdlCLoc))
                            {
                                item.toSpSShuRhdlCLoc = whPreToName + "(" + toSpSShuRhdlCLoc.Substring(toSpSShuRhdlCLoc.IndexOf("-", 0) + 1) + "," + iSpSShuRhdlCLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmSpSShuRhdlRLoc))
                            {
                                item.fmSpSShuRhdlRLoc = whPreFromName + "(" + fmSpSShuRhdlRLoc.Substring(fmSpSShuRhdlRLoc.IndexOf("-", 0) + 1) + "," + iSpSShuRhdlRLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toSpSShuRhdlRLoc))
                            {
                                item.toSpSShuRhdlRLoc = whPreToName + "(" + toSpSShuRhdlRLoc.Substring(toSpSShuRhdlRLoc.IndexOf("-", 0) + 1) + "," + iSpSShuRhdlRLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmSpOLoc))
                            {
                                item.fmSpOLoc = whPreFromName + "(" + fmSpOLoc.Substring(fmSpOLoc.IndexOf("-", 0) + 1) + "," + iSpOLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toSpOLoc))
                            {
                                item.toSpOLoc = whPreToName + "(" + toSpOLoc.Substring(toSpOLoc.IndexOf("-", 0) + 1) + "," + iSpOLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmSpODmgLoc))
                            {
                                item.fmSpODmgLoc = whPreFromName + "(" + fmSpODmgLoc.Substring(fmSpODmgLoc.IndexOf("-", 0) + 1) + "," + iSpODmgLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toSpODmgLoc))
                            {
                                item.toSpODmgLoc = whPreToName + "(" + toSpODmgLoc.Substring(toSpODmgLoc.IndexOf("-", 0) + 1) + "," + iSpODmgLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmSpODmgRhdlCLoc))
                            {
                                item.fmSpODmgRhdlCLoc = whPreFromName + "(" + fmSpODmgRhdlCLoc.Substring(fmSpODmgRhdlCLoc.IndexOf("-", 0) + 1) + "," + iSpODmgRhdlCLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toSpODmgRhdlCLoc))
                            {
                                item.toSpODmgRhdlCLoc = whPreToName + "(" + toSpODmgRhdlCLoc.Substring(toSpODmgRhdlCLoc.IndexOf("-", 0) + 1) + "," + iSpODmgRhdlCLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmSpODmgRhdlRLoc))
                            {
                                item.fmSpODmgRhdlRLoc = whPreFromName + "(" + fmSpODmgRhdlRLoc.Substring(fmSpODmgRhdlRLoc.IndexOf("-", 0) + 1) + "," + iSpODmgRhdlRLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toSpODmgRhdlRLoc))
                            {
                                item.toSpODmgRhdlRLoc = whPreToName + "(" + toSpODmgRhdlRLoc.Substring(toSpODmgRhdlRLoc.IndexOf("-", 0) + 1) + "," + iSpODmgRhdlRLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmSpOShuLoc))
                            {
                                item.fmSpOShuLoc = whPreFromName + "(" + fmSpOShuLoc.Substring(fmSpOShuLoc.IndexOf("-", 0) + 1) + "," + iSpOShuLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toSpOShuLoc))
                            {
                                item.toSpOShuLoc = whPreToName + "(" + toSpOShuLoc.Substring(toSpOShuLoc.IndexOf("-", 0) + 1) + "," + iSpOShuLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmSpOShuRhdlCLoc))
                            {
                                item.fmSpOShuRhdlCLoc = whPreFromName + "(" + fmSpOShuRhdlCLoc.Substring(fmSpOShuRhdlCLoc.IndexOf("-", 0) + 1) + "," + iSpOShuRhdlCLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toSpOShuRhdlCLoc))
                            {
                                item.toSpOShuRhdlCLoc = whPreToName + "(" + toSpOShuRhdlCLoc.Substring(toSpOShuRhdlCLoc.IndexOf("-", 0) + 1) + "," + iSpOShuRhdlCLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(fmSpOShuRhdlRLoc))
                            {
                                item.fmSpOShuRhdlRLoc = whPreFromName + "(" + fmSpOShuRhdlRLoc.Substring(fmSpOShuRhdlRLoc.IndexOf("-", 0) + 1) + "," + iSpOShuRhdlRLoc + ")";
                            }
                            if (!string.IsNullOrEmpty(toSpOShuRhdlRLoc))
                            {
                                item.toSpOShuRhdlRLoc = whPreToName + "(" + toSpOShuRhdlRLoc.Substring(toSpOShuRhdlRLoc.IndexOf("-", 0) + 1) + "," + iSpOShuRhdlRLoc + ")";
                            }

                        } // end for
                    }

                    if (whConfigList.Count > 0)
                    {
                        item.collection = whConfigList.ToArray();
                    }
                    item.CRUD = Constants.WS_INSERT;
                    item.userId = UserInfo.getInstance().UserId;
                    item.shftDt = UserInfo.getInstance().Workdate;
                    item.shftId = UserInfo.getInstance().Shift;

                    Object[] obj = new Object[] { item };
                    DataItemCollection dataCollection = new DataItemCollection();
                    dataCollection.collection = obj;

                    proxy.processCargoMovementItem(dataCollection);
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
    }
}