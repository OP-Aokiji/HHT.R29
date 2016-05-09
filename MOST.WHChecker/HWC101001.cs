using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using Framework.Common.Constants;
using Framework.Common.PopupManager;
using MOST.WHChecker.Parm;
using MOST.WHChecker.Result;
using MOST.Common.Utility;
using MOST.Client.Proxy.WHCheckerProxy;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.WHChecker
{
    public partial class HWC101001 : TForm, IPopupWindow
    {
        private bool m_displayOccupiedInfo;
        private string m_cachedWhLocId = string.Empty;  //Cached first whLocId
        private string m_cachedCell = string.Empty;     //Cached first cell
        private HWC101001Parm m_parm;
        private HWC101001Result m_result;
        private ArrayList m_unusedCells;
        private ArrayList m_rentalCells;

        private const String HEADER_WHID    = "W/H";
        private const String HEADER_BAY     = "Bay";
        private const String HEADER_ROW     = "Row";
        private const String HEADER_MT      = "MT";
        private const String HEADER_M3      = "M3";
        private const String HEADER_QTY     = "QTY";

        public HWC101001() : this(false)
        {
        }

        public HWC101001(bool displayOccupiedInfo)
        {
            m_displayOccupiedInfo = displayOccupiedInfo;
            m_unusedCells = new ArrayList();
            m_rentalCells = new ArrayList();
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            InitializeWHId();
            m_result = new HWC101001Result();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HWC101001Parm)parm;
            txtActMT.Text = string.IsNullOrEmpty(m_parm.TotMt) ? "0" : m_parm.TotMt;
            txtActM3.Text = string.IsNullOrEmpty(m_parm.TotM3) ? "0" : m_parm.TotM3;
            txtActQty.Text = string.IsNullOrEmpty(m_parm.TotQty) ? "0" : m_parm.TotQty;
            txtBalMT.Text = string.IsNullOrEmpty(m_parm.TotMt) ? "0" : m_parm.TotMt;
            txtBalM3.Text = string.IsNullOrEmpty(m_parm.TotM3) ? "0" : m_parm.TotM3;
            txtBalQty.Text = string.IsNullOrEmpty(m_parm.TotQty) ? "0" : m_parm.TotQty;

            #region Title
            string strTitle = "W/C - Set Location";
            if (m_parm != null)
            {
                strTitle = strTitle + " - " + m_parm.CgNo;
            }
            this.Text = strTitle;
            #endregion

            #region Planned/Occupied Info
            lblPlannedLoc.Text = m_displayOccupiedInfo ? "Occupied" : "Planned Loc";
            #endregion

            GetPlannedOccupiedLocation();
            grdData.IsDirty = false;

            this.ShowDialog();
            return m_result;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_WHID, "60" }, { HEADER_ROW, "40" }, { HEADER_BAY, "40" }, { HEADER_MT, "75" }, { HEADER_M3, "75" }, { HEADER_QTY, "90" } };
            this.grdData.setHeader(header);
        }

        private void InitializeWHId()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region W/H Id
                IWHCheckerProxy proxy = new WHCheckerProxy();
                CargoMasterParm parm = new CargoMasterParm();
                parm.locDivCd = "WHO";
                ResponseInfo info = proxy.getWHComboList(parm);
                CommonUtility.InitializeCombobox(cboWHNO, "Select");
                if (info != null)
                {
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is CargoMasterItem)
                        {
                            CargoMasterItem item = (CargoMasterItem)info.list[i];
                            cboWHNO.Items.Add(new ComboboxValueDescriptionPair(item.locId, item.locId));
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

                if (cboWHNO != null && cboWHNO.SelectedIndex > 0)
                {
                    #region Row
                    string strWhId = CommonUtility.GetComboboxSelectedValue(cboWHNO);
                    IWHCheckerProxy proxy = new WHCheckerProxy();

                    WhConfigurationParm parm = new WhConfigurationParm();
                    parm.whId = strWhId;
                    parm.locDivCd = "ROW";
                    ResponseInfo rowInfo = proxy.getWhConfigurationList(parm);
                    CommonUtility.InitializeCombobox(cboRow, "Select");
                    if (rowInfo != null)
                    {
                        for (int i = 0; i < rowInfo.list.Length; i++)
                        {
                            if (rowInfo.list[i] is WhConfigurationItem)
                            {
                                WhConfigurationItem item = (WhConfigurationItem)rowInfo.list[i];
                                cboRow.Items.Add(new ComboboxValueDescriptionPair(item.rowwId, item.rowwId));
                            }
                        }
                    }
                    #endregion

                    #region Bay
                    parm.locDivCd = "BAY";
                    ResponseInfo bayInfo = proxy.getWhConfigurationList(parm);
                    CommonUtility.InitializeCombobox(cboBay, "Select");
                    if (bayInfo != null)
                    {
                        for (int i = 0; i < bayInfo.list.Length; i++)
                        {
                            if (bayInfo.list[i] is WhConfigurationItem)
                            {
                                WhConfigurationItem item = (WhConfigurationItem)bayInfo.list[i];
                                cboBay.Items.Add(new ComboboxValueDescriptionPair(item.bayId, item.bayId));
                            }
                        }
                    }
                    #endregion

                    #region Unused Cells
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

                    #region Rental Cells
                    // Rental cell: 4A-B2
                    m_rentalCells.Clear();
                    parm = new WhConfigurationParm();
                    parm.searchType = "HHT";
                    parm.whId = strWhId;
                    ResponseInfo rentalInfo = proxy.getWhViewList(parm);
                    if (rentalInfo != null)
                    {
                        for (int i = 0; i < rentalInfo.list.Length; i++)
                        {
                            if (rentalInfo.list[i] is WhConfigurationItem)
                            {
                                WhConfigurationItem item = (WhConfigurationItem)rentalInfo.list[i];
                                m_rentalCells.Add(item.locId + item.locId);
                            }
                        }
                    }
                    #endregion

                    // Display cached occupied cell (in case of Loading Cancel)
                    if (m_displayOccupiedInfo && !string.IsNullOrEmpty(m_cachedCell) && m_cachedCell.Length > 1)
                    {
                        // m_cachedCell = "4A-B12" ==> row = "B", bay = "12"
                        string[] arrCellLoc = m_cachedCell.Split('-');
                        if (arrCellLoc != null && arrCellLoc.Length > 1)
                        {
                            // cell = "B12"
                            string cell = arrCellLoc[1];
                            if (!string.IsNullOrEmpty(cell) && cell.Length > 1)
                            {
                                string row = cell.Substring(0, 1);
                                string bay = cell.Substring(1, cell.Length - 1);
                                CommonUtility.SetComboboxSelectedItem(cboRow, row);
                                CommonUtility.SetComboboxSelectedItem(cboBay, bay);
                            }
                        }
                        
                    }
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

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnConfirm":
                    if (this.validations(this.Controls) && ValidateConfirmAmount())
                    {
                        ReturnWHInfo();
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
                            if (ValidateConfirmAmount())
                            {
                                ReturnWHInfo();
                                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                                this.Close(); 
                            }
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
                    if (this.validations(this.Controls) && !IsUnusedOrRentalCell() && ValidateAmount())
                    {
                        // Check constraints: WHID, BAY, ROW
                        // lv.dat add check MT M3 QTY
                        if (!IsExistAlready())
                        {
                            if (IsRealValues())
                                AddWHInfo();
                            else
                            {
                                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0073"));
                                cboWHNO.Focus();
                            }
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
                            cboWHNO.Focus();
                        }
                        CalcAmount();
                    }
                    break;

                case "btnUpdate":
                    if (this.validations(this.Controls) && !IsUnusedOrRentalCell() && ValidateAmount())
                    {
                        UpdateWHInfo();
                        CalcAmount();
                    }
                    break;

                case "btnDelete":
                    DeleteWHInfo();
                    CalcAmount();
                    break;

            }
        }

        private void ReturnWHInfo()
        {
            if (grdData != null)
            {
                int rowsCnt = grdData.DataTable.Rows.Count;
                for (int i = 0; i < rowsCnt; i++)
                {
                    WhConfigurationItem item = new WhConfigurationItem();
                    item.locId = grdData.DataTable.Rows[i][HEADER_WHID] + "-" + grdData.DataTable.Rows[i][HEADER_ROW] + grdData.DataTable.Rows[i][HEADER_BAY];
                    item.vslCallId = m_parm.VslCallId;
                    item.cgNo = m_parm.CgNo;
                    item.whTpCd = m_parm.WhTpCd;
                    item.wgt = CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_MT].ToString());
                    item.msrmt = CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_M3].ToString());
                    item.pkgQty = grdData.DataTable.Rows[i][HEADER_QTY].ToString();
                    m_result.ArrWHLocation.Add(item);
                }

                if (rowsCnt > 0)
                {
                    // Get cell format, eg: 4A-E2
                    string strLocId = grdData.DataTable.Rows[0][HEADER_WHID] + "(" + grdData.DataTable.Rows[0][HEADER_ROW] + grdData.DataTable.Rows[0][HEADER_BAY] + "," + rowsCnt + ")";
                    m_result.LocId = strLocId;
                    m_result.TotMt = grdData.DataTable.Rows[0][HEADER_MT].ToString();
                    m_result.TotM3 = grdData.DataTable.Rows[0][HEADER_M3].ToString();
                    m_result.TotQty = grdData.DataTable.Rows[0][HEADER_QTY].ToString();
                }
            }
        }

        private void GetPlannedOccupiedLocation()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // Request Webservice
                IWHCheckerProxy proxy = new WHCheckerProxy();
                WhConfigurationParm parm = new WhConfigurationParm();
                if (m_displayOccupiedInfo)
                {
                    parm.searchType = "occupiedInfo";
                    parm.cgNo = m_parm.CgNo;
                } 
                else
                {
                    // Display planned info of S/N or B/L
                    parm.searchType = "planInfo";
                    parm.whId = "";
                    parm.blSn = !string.IsNullOrEmpty(m_parm.ShipgNoteNo) ? m_parm.ShipgNoteNo : m_parm.CgNo;
                }
                parm.vslCallId = m_parm.VslCallId;

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
                                // Cache first whLocId and cell
                                m_cachedWhLocId = item.whLocId;
                                m_cachedCell = item.locId;
                            }
                        }
                    }
                }
                txtPlannedLoc.Text = strPlannedLoc;

                if (m_displayOccupiedInfo)
                {
                    // Display cached occupied WH ID and cell (in case of Loading Cancel)
                    // WH ID: displayed according to m_cachedWhLocId.
                    // Cell: will be displayed after event cboWHNO_SelectedIndexChanged is fired.
                    if (!string.IsNullOrEmpty(m_cachedWhLocId))
                    {
                        CommonUtility.SetComboboxSelectedItem(cboWHNO, m_cachedWhLocId);
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

        private bool ValidateAmount()
        {
            bool result = true;

            double balMt = CommonUtility.ParseDouble(txtBalMT.Text);
            double balM3 = CommonUtility.ParseDouble(txtBalM3.Text);
            int balQty = CommonUtility.ParseInt(txtBalQty.Text);

            double actMt = CommonUtility.ParseDouble(txtActMT.Text);
            double actM3 = CommonUtility.ParseDouble(txtActM3.Text);
            int actQty = CommonUtility.ParseInt(txtActQty.Text);

            if (balQty < actQty || balMt < actMt || balM3 < actM3)
            {
                result = false;
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HWC101001_0001"));
            }

            return result;
        }

        private bool ValidateConfirmAmount()
        {
            double balMt = CommonUtility.ParseDouble(txtBalMT.Text);
            double balM3 = CommonUtility.ParseDouble(txtBalM3.Text);
            int balQty = CommonUtility.ParseInt(txtBalQty.Text);
            
            if (balQty > 0 || balMt > 0 || balM3 > 0)
            {
                CommonUtility.AlertMessage("Please verify the amount");
                return false;
            }

            return true;
        }

        private bool IsUnusedOrRentalCell()
        {
            // Selected cell: A2
            string selectedCell = CommonUtility.GetComboboxSelectedValue(cboRow) + CommonUtility.GetComboboxSelectedValue(cboBay);
            if (m_unusedCells.Contains(selectedCell))
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HWC101001_0002"));
                return true;
            }

            // Selected cell: 4A-A2
            selectedCell = CommonUtility.GetComboboxSelectedValue(cboWHNO) + "-" + selectedCell;
            if (m_rentalCells.Contains(selectedCell))
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HWC101001_0003"));
                return true;
            }

            return false;
        }

        private void AddWHInfo()
        {
            DataRow newRow = grdData.NewRow();
            newRow[HEADER_WHID] = CommonUtility.GetComboboxSelectedValue(cboWHNO);
            newRow[HEADER_ROW] = CommonUtility.GetComboboxSelectedValue(cboRow);
            newRow[HEADER_BAY] = CommonUtility.GetComboboxSelectedValue(cboBay);
            newRow[HEADER_MT] = string.IsNullOrEmpty(txtActMT.Text) ? "0" : txtActMT.Text;
            newRow[HEADER_M3] = string.IsNullOrEmpty(txtActM3.Text) ? "0" : txtActM3.Text;
            newRow[HEADER_QTY] = string.IsNullOrEmpty(txtActQty.Text) ? "0" : txtActQty.Text;
            newRow[HEADER_MT] = txtActMT.Text;
            newRow[HEADER_M3] = txtActM3.Text;
            newRow[HEADER_QTY] = txtActQty.Text;
            grdData.Add(newRow);
            grdData.Refresh();
        }

        private void UpdateWHInfo()
        {
            int currRowIndex = grdData.CurrentRowIndex;
            if (currRowIndex > -1)
            {
                // Set Readonly False to update column data.
                DataTable dataTable = grdData.DataTable;
                int colCnt = dataTable.Columns.Count;
                for (int i = 0; i < colCnt; i++)
                {
                    dataTable.Columns[i].ReadOnly = false;
                }

                // Update columns data
                DataRow row = grdData.DataTable.Rows[currRowIndex];
                row[HEADER_MT] = string.IsNullOrEmpty(txtActMT.Text) ? "0" : txtActMT.Text;
                row[HEADER_M3] = string.IsNullOrEmpty(txtActM3.Text) ? "0" : txtActM3.Text;
                row[HEADER_QTY] = string.IsNullOrEmpty(txtActQty.Text) ? "0" : txtActQty.Text;
                grdData.DataTable.AcceptChanges();
                grdData.Refresh();

                // Reset original Readonly
                for (int i = 0; i < colCnt; i++)
                {
                    dataTable.Columns[i].ReadOnly = true;
                }
            }
        }

        private void DeleteWHInfo()
        {
            if (grdData.CurrentRowIndex > -1 && grdData.CurrentRowIndex < grdData.DataTable.Rows.Count)
            {
                grdData.DataTable.AcceptChanges();
                grdData.DataTable.Rows.RemoveAt(grdData.CurrentRowIndex);
                grdData.Refresh();
            }
        }

        private void CalcAmount()
        {   
            double mt = 0;
            double m3 = 0;
            long qty = 0;
            if (grdData != null && grdData.DataTable.Rows.Count > 0)
            {
                int rowNum = grdData.DataTable.Rows.Count;
                for (int i = 0; i < rowNum; i++)
                {
                    string curMT = grdData.DataTable.Rows[i][HEADER_MT].ToString();
                    string curM3 = grdData.DataTable.Rows[i][HEADER_M3].ToString();
                    string curQty = grdData.DataTable.Rows[i][HEADER_QTY].ToString();
                    mt = mt + CommonUtility.ParseDouble(CommonUtility.ToString(curMT));
                    m3 = m3 + CommonUtility.ParseDouble(CommonUtility.ToString(curM3));
                    qty = qty + CommonUtility.ParseLong(CommonUtility.ToString(curQty));
                }
            }

            string strMt = CommonUtility.ToString(CommonUtility.ParseDouble(m_parm.TotMt) - mt);
            string strM3 = CommonUtility.ToString(CommonUtility.ParseDouble(m_parm.TotM3) - m3);
            string strQty = CommonUtility.ToString(CommonUtility.ParseLong(m_parm.TotQty) - qty);
            txtActMT.Text = strMt;
            txtActM3.Text = strM3;
            txtActQty.Text = strQty;
            txtBalMT.Text = strMt;
            txtBalM3.Text = strM3;
            txtBalQty.Text = strQty;
        }

        private bool IsExistAlready()
        {
            // Make sure WHID, ROW, BAY to be unique.
            string strWHId = CommonUtility.GetComboboxSelectedValue(cboWHNO);
            string strRow = CommonUtility.GetComboboxSelectedValue(cboRow);
            string strBay = CommonUtility.GetComboboxSelectedValue(cboBay);

            if (grdData != null && grdData.DataTable.Rows.Count > 0)
            {
                int count = grdData.DataTable.Rows.Count;
                int index = count - 1;
                string whId = grdData.DataTable.Rows[index][HEADER_WHID].ToString();
                string row = grdData.DataTable.Rows[index][HEADER_ROW].ToString();
                string bay = grdData.DataTable.Rows[index][HEADER_BAY].ToString();
                while (
                    (!strWHId.Equals(whId) ||
                    !strRow.Equals(row) ||
                    !strBay.Equals(bay)))
                {
                    index = index - 1;
                    if (index < 0)
                    {
                        return false;
                    }
                    whId = grdData.DataTable.Rows[index][HEADER_WHID].ToString();
                    row = grdData.DataTable.Rows[index][HEADER_ROW].ToString();
                    bay = grdData.DataTable.Rows[index][HEADER_BAY].ToString();
                }
                if (index >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        /*
         * lv.dat add 20130628
         * check add real number of MT M3 QTY
         */ 
        private bool IsRealValues()
        {
            bool bIsReal = true;

            int iCount = 0;
            if (string.IsNullOrEmpty(txtActMT.Text) || "0".Equals(txtActMT.Text))
                iCount++;
            if (string.IsNullOrEmpty(txtActM3.Text) || "0".Equals(txtActM3.Text))
                iCount++;
            if (string.IsNullOrEmpty(txtActQty.Text) || "0".Equals(txtActQty.Text))
                iCount++;

            if (iCount == 3)
                bIsReal = false;

            return bIsReal;
        }

        private void cboWHNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeRowBay();
        }

        private void grdData_CurrentCellChanged(object sender, EventArgs e)
        {
            int index = grdData.CurrentRowIndex;
            if (index > -1 && index < grdData.DataTable.Rows.Count)
            {
                string strWHId = grdData.DataTable.Rows[index][HEADER_WHID].ToString();
                string strRow = grdData.DataTable.Rows[index][HEADER_ROW].ToString();
                string strBay = grdData.DataTable.Rows[index][HEADER_BAY].ToString();
                string strMT = grdData.DataTable.Rows[index][HEADER_MT].ToString();
                string strM3 = grdData.DataTable.Rows[index][HEADER_M3].ToString();
                string strQty = grdData.DataTable.Rows[index][HEADER_QTY].ToString();

                CommonUtility.SetComboboxSelectedItem(cboWHNO, strWHId);
                CommonUtility.SetComboboxSelectedItem(cboRow, strRow);
                CommonUtility.SetComboboxSelectedItem(cboBay, strBay);
                txtActMT.Text = strMT;
                txtActM3.Text = strM3;
                txtActQty.Text = strQty;

                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }
    }
}