using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
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
    public partial class HWC101002 : TForm, IPopupWindow
    {
        public const string WHTPCD_GENERAL  = "G";  // Normal cargo
        public const string WHTPCD_SHUTOUT  = "S";  // Shut-out cargo
        public const string WHTPCD_DAMAGE   = "D";  // Damage cargo

        private const String HEADER_CGNO = "Cg No";
        private const String HEADER_CELL = "W/H";
        private const String HEADER_MT = "MT";
        private const String HEADER_M3 = "M3";
        private const String HEADER_QTY = "QTY";
        private const String HEADER_WHTPCD = "_WHTPCD";

        private bool m_isSpareCg;
        private string m_whTpCd;
        private HWC101002Parm m_parm;
        private HWC101002Result m_result;
        private ArrayList m_whConfigItems;

        public HWC101002(bool isSpareCg)
        {
            m_isSpareCg = isSpareCg;
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            SetVisibleControl();
            m_whTpCd = string.Empty;
            m_whConfigItems = new ArrayList();
            m_result = new HWC101002Result();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        public HWC101002() : this(false)
        {
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HWC101002Parm)parm;
            txtCgNo.Text = m_parm.CgNo;
            txtTotMt.Text = string.IsNullOrEmpty(m_parm.TotMt) ? "0" : m_parm.TotMt;
            txtTotM3.Text = string.IsNullOrEmpty(m_parm.TotM3) ? "0" : m_parm.TotM3;
            txtTotQty.Text = string.IsNullOrEmpty(m_parm.TotQty) ? "0" : m_parm.TotQty;

            #region Title
            string strTitle = "W/C - UnSet Location";
            if (m_parm != null)
            {
                strTitle = strTitle + " - " + m_parm.CgNo;
            }
            this.Text = strTitle;
            #endregion

            if (m_isSpareCg)
            {
                InitializeSpareCg();
            }
            else
            {
                InitializeLocation(txtCgNo.Text);
            }
            grdData.IsDirty = false;

            this.ShowDialog();
            return m_result;
        }

        private void SetVisibleControl()
        {
            if (m_isSpareCg)
            {
                this.cboSpareCg.Visible = true;
                this.txtCgNo.Visible = false;

                this.cboSpareCg.isMandatory = true;
                this.txtCgNo.isMandatory = false;
            } 
            else
            {
                this.cboSpareCg.Visible = false;
                this.txtCgNo.Visible = true;

                this.cboSpareCg.isMandatory = false;
                this.txtCgNo.isMandatory = true;
            }
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_CGNO, "60" }, { HEADER_CELL, "50" }, { HEADER_MT, "55" }, { HEADER_M3, "55" }, { HEADER_QTY, "60" }, { HEADER_WHTPCD, "0" } };
            this.grdData.setHeader(header);
        }

        private void InitializeSpareCg()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                IWHCheckerProxy proxy = new WHCheckerProxy();
                CargoExportParm cgExpParm = new CargoExportParm();
                cgExpParm.sprYn = "Y";
                if (m_parm != null)
                {
                    cgExpParm.vslCallId = m_parm.VslCallId;
                    cgExpParm.shipgNoteNo = m_parm.ShipgNoteNo;
                }
                ResponseInfo info = proxy.getCargoExportList(cgExpParm);

                CommonUtility.InitializeCombobox(cboSpareCg);
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CargoExportItem)
                    {
                        CargoExportItem item = (CargoExportItem)info.list[i];
                        cboSpareCg.Items.Add(new ComboboxValueDescriptionPair(item.cgNo, item.cgNo));
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

        private void InitializeLocation(string strCgNo)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Location
                CommonUtility.InitializeCombobox(cboLocation, "Select");
                if (!string.IsNullOrEmpty(strCgNo))
                {
                    IWHCheckerProxy proxy = new WHCheckerProxy();
                    WhConfigurationParm parm = new WhConfigurationParm();
                    parm.cgNo = strCgNo;
                    if (m_parm != null)
                    {
                        parm.vslCallId = m_parm.VslCallId;
                    }
                    parm.whTpCd = PreprocessWhTpCd();
                    parm.searchType = "occupiedInfo";
                    if (m_isSpareCg || (m_parm != null && "S".Equals(m_parm.SpCaCoCd)))
                    {
                        parm.spCaCoCd = "S";
                    }
                    ResponseInfo info = proxy.getWhViewList(parm);
                    
                    m_whConfigItems.Clear();
                    if (info != null)
                    {
                        for (int i = 0; i < info.list.Length; i++)
                        {
                            if (info.list[i] is WhConfigurationItem)
                            {
                                WhConfigurationItem item = (WhConfigurationItem)info.list[i];
                                string strLoc = item.locId + "[" + item.whTpCdNm + "]";
                                cboLocation.Items.Add(new ComboboxValueDescriptionPair(item.locId, strLoc));

                                m_whConfigItems.Add(item);
                            }
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

        /// <summary>
        /// Pre-process whTpCd.
        /// </summary>
        /// <returns></returns>
        private string PreprocessWhTpCd()
        {
            // pre-process whTpCd as whTpCd = "D','S','G"
            string strWhTpCd = string.Empty;
            if (m_parm.IsGeneralCg)
            {
                if (string.IsNullOrEmpty(strWhTpCd))
                {
                    strWhTpCd = "G'";
                }
                else
                {
                    strWhTpCd += ",'G'";
                }
            }
            if (m_parm.IsDamageCg)
            {
                if (string.IsNullOrEmpty(strWhTpCd))
                {
                    strWhTpCd = "D'";
                }
                else
                {
                    strWhTpCd += ",'D'";
                }
            }
            if (m_parm.IsShutoutCg)
            {
                if (string.IsNullOrEmpty(strWhTpCd))
                {
                    strWhTpCd = "S'";
                }
                else
                {
                    strWhTpCd += ",'S'";
                }
            }

            // whTpCd = "D','S','G'"  --> whTpCd = "D','S','G"
            if (!string.IsNullOrEmpty(strWhTpCd) && strWhTpCd.Length > 1)
            {
                strWhTpCd = strWhTpCd.Substring(0, strWhTpCd.Length - 1);
            }

            return strWhTpCd;
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
                            if (this.validations(this.Controls) && ValidateConfirmAmount())
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
                    if (this.validations(this.Controls))
                    {
                        if (Validate(Constants.MODE_ADD))
                        {
                            AddWHInfo();
                        }
                    }
                    break;

                case "btnUpdate":
                    if (this.validations(this.Controls))
                    {
                        if (Validate(Constants.MODE_UPDATE))
                        {
                            UpdateWHInfo();
                        }
                    }
                    break;

                case "btnDelete":
                    DeleteWHInfo();
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
                    item.cgNo = grdData.DataTable.Rows[i][HEADER_CGNO].ToString();
                    item.locId = grdData.DataTable.Rows[i][HEADER_CELL].ToString();
                    item.vslCallId = m_parm != null ? m_parm.VslCallId : string.Empty;
                    item.spCaCoCd = (m_isSpareCg || (m_parm != null && "S".Equals(m_parm.SpCaCoCd))) ? "S" : "";
                    item.whTpCd = grdData.DataTable.Rows[i][HEADER_WHTPCD].ToString();
                    item.wgt = CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_MT].ToString());
                    item.msrmt = CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_M3].ToString());
                    item.pkgQty = grdData.DataTable.Rows[i][HEADER_QTY].ToString();
                    m_result.ArrWHLocation.Add(item);
                }

                if (rowsCnt > 0)
                {
                    string strTmp = grdData.DataTable.Rows[0][HEADER_CELL].ToString();
                    string strLocId = strTmp.Substring(0, (strTmp.LastIndexOf('-'))) + "(" + strTmp.Substring(strTmp.LastIndexOf('-') + 1) + "," + rowsCnt + ")";
                    m_result.LocId = strLocId;
                }
            }
        }

        private void AddWHInfo()
        {
            DataRow newRow = grdData.NewRow();
            if (m_isSpareCg || (m_parm != null && "S".Equals(m_parm.SpCaCoCd)))
            {
                newRow[HEADER_CGNO] = CommonUtility.GetComboboxSelectedValue(cboSpareCg);
            } 
            else
            {
                if (m_parm != null)
                {
                    newRow[HEADER_CGNO] = m_parm.CgNo;
                }
            }
            newRow[HEADER_CELL] = CommonUtility.GetComboboxSelectedValue(cboLocation);
            newRow[HEADER_MT] = txtMT.Text;
            newRow[HEADER_M3] = txtM3.Text;
            newRow[HEADER_QTY] = txtQty.Text;
            newRow[HEADER_WHTPCD] = m_whTpCd;
            grdData.Add(newRow);
            grdData.Refresh();
        }

        private void UpdateWHInfo()
        {
            int currRowIndex = grdData.CurrentRowIndex;
            if (currRowIndex > -1 && currRowIndex < grdData.DataTable.Rows.Count)
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
                if (m_isSpareCg || (m_parm != null && "S".Equals(m_parm.SpCaCoCd)))
                {
                    row[HEADER_CGNO] = CommonUtility.GetComboboxSelectedValue(cboSpareCg);
                }
                else
                {
                    if (m_parm != null)
                    {
                        row[HEADER_CGNO] = m_parm.CgNo;
                    }
                }
                row[HEADER_MT] = txtMT.Text;
                row[HEADER_M3] = txtM3.Text;
                row[HEADER_QTY] = txtQty.Text;
                row[HEADER_WHTPCD] = m_whTpCd;
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

        private bool Validate(int mode)
        {
            // Make sure LOCID to be unique.
            if (IsExistAlready(mode))
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
                return false;
            }

            // Validate amount
            if (!ValidateAmount(mode))
            {
                return false;
            }

            return true;
        }

        private bool ValidateAmount(int mode)
        {
            // Validate amount: cell amount <= actual amount
            double cellMt = CommonUtility.ParseDouble(txtCellMt.Text);
            double cellM3 = CommonUtility.ParseDouble(txtCellM3.Text);
            int cellQty = CommonUtility.ParseInt(txtCellQty.Text);
            double mt = CommonUtility.ParseDouble(txtMT.Text);
            double m3 = CommonUtility.ParseDouble(txtM3.Text);
            int qty = CommonUtility.ParseInt(txtQty.Text);
            if (cellMt < mt || cellM3 < m3 || cellQty < qty)
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HWC101002_0001"));
                return false;
            }

            // Validate amount: total actual amount <= inputted amount
            double inpMt = CommonUtility.ParseDouble(txtTotMt.Text);
            double inpM3 = CommonUtility.ParseDouble(txtTotM3.Text);
            int inpQty = CommonUtility.ParseInt(txtTotQty.Text);
            double actTotMt = mt;
            double actTotM3 = m3;
            int actTotQty = qty;
            if (grdData != null && grdData.DataTable != null && grdData.DataTable.Rows.Count > 0)
            {
                int rowCnt = grdData.DataTable.Rows.Count;
                for (int i = 0; i < rowCnt; i++ )
                {
                    if (mode != Constants.MODE_UPDATE || i != grdData.CurrentRowIndex)
                    {
                        actTotMt += CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_MT].ToString());
                        actTotM3 += CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_M3].ToString());
                        actTotQty += CommonUtility.ParseInt(grdData.DataTable.Rows[i][HEADER_QTY].ToString());
                    }
                }
            }
            if (inpMt < actTotMt || inpM3 < actTotM3 || inpQty < actTotQty)
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HWC101002_0002"));
                return false;
            }


            return true;
        }

        private bool ValidateConfirmAmount()
        {
            double inpMt = CommonUtility.ParseDouble(txtTotMt.Text);
            double inpM3 = CommonUtility.ParseDouble(txtTotM3.Text);
            int inpQty = CommonUtility.ParseInt(txtTotQty.Text);
            double actTotMt = 0;
            double actTotM3 = 0;
            int actTotQty = 0;
            if (grdData != null && grdData.DataTable != null && grdData.DataTable.Rows.Count > 0)
            {
                int rowCnt = grdData.DataTable.Rows.Count;
                for (int i = 0; i < rowCnt; i++)
                {
                    actTotMt += CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_MT].ToString());
                    actTotM3 += CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_M3].ToString());
                    actTotQty += CommonUtility.ParseInt(grdData.DataTable.Rows[i][HEADER_QTY].ToString());
                }
            }
            if (inpMt != actTotMt || inpM3 != actTotM3 || inpQty != actTotQty)
            {
                CommonUtility.AlertMessage("Please verify amount again");
                return false;
            }
            return true;
        }

        private void CalcActAmount(double dCellMt, double dCellM3, string strCellQty)
        {
            // Total amount
            double dTotMt = CommonUtility.ParseDouble(txtTotMt.Text);
            double dTotM3 = CommonUtility.ParseDouble(txtTotM3.Text);
            int iTotQty = CommonUtility.ParseInt(txtTotQty.Text);

            // Total of grid amount
            double dGrdMt = 0;
            double dGrdM3 = 0;
            int iGrdQty = 0;
            if (grdData != null)
            {   
                int rowsCnt = grdData.DataTable.Rows.Count;
                for (int i = 0; i < rowsCnt; i++)
                {
                    dGrdMt += CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_MT].ToString());
                    dGrdM3 += CommonUtility.ParseDouble(grdData.DataTable.Rows[i][HEADER_M3].ToString());
                    iGrdQty += CommonUtility.ParseInt(grdData.DataTable.Rows[i][HEADER_QTY].ToString());
                }
            }

            // Remained amount
            double dRemainedMt = dTotMt - dGrdMt;
            double dRemainedM3 = dTotM3 - dGrdM3;
            int iRemainedQty = iTotQty - iGrdQty;

            // Cell amount compares to remained amount. 
            // Display the smaller one at actual amount text box.
            txtMT.Text = CommonUtility.ToString(dCellMt > dRemainedMt ? dRemainedMt : dCellMt);
            txtM3.Text = CommonUtility.ToString(dCellM3 > dRemainedM3 ? dRemainedM3 : dCellM3);
            int iCellQty = CommonUtility.ParseInt(strCellQty);
            txtQty.Text = iCellQty > iRemainedQty ? CommonUtility.ToString(iRemainedQty) : strCellQty;
        }

        private bool IsExistAlready(int mode)
        {
            // Make sure LOCID to be unique.
            string strWHId = CommonUtility.GetComboboxSelectedValue(cboLocation);
            string strCgNo = string.Empty;
            if (m_isSpareCg || (m_parm != null && "S".Equals(m_parm.SpCaCoCd)))
            {
                strCgNo = CommonUtility.GetComboboxSelectedValue(cboSpareCg);
            }
            else
            {
                if (m_parm != null)
                {
                    strCgNo = m_parm.CgNo;
                }
            }

            if (grdData != null && grdData.DataTable.Rows.Count > 0)
            {
                int count = grdData.DataTable.Rows.Count;
                int index = count - 1;
                string cgNo = grdData.DataTable.Rows[index][HEADER_CGNO].ToString();
                string whId = grdData.DataTable.Rows[index][HEADER_CELL].ToString();
                while (!strCgNo.Equals(cgNo) || !strWHId.Equals(whId))
                {
                    index = index - 1;
                    if (index < 0)
                    {
                        return false;
                    }
                    if (mode == Constants.MODE_ADD)
                    {
                        cgNo = grdData.DataTable.Rows[index][HEADER_CGNO].ToString();
                        whId = grdData.DataTable.Rows[index][HEADER_CELL].ToString();
                    }
                    else if (mode == Constants.MODE_UPDATE)
                    {
                        if (index != grdData.CurrentRowIndex)
                        {
                            cgNo = grdData.DataTable.Rows[index][HEADER_CGNO].ToString();
                            whId = grdData.DataTable.Rows[index][HEADER_CELL].ToString();
                        }
                    }
                }
                if (mode == Constants.MODE_ADD)
                {
                    if (index >= 0)
                    {
                        //CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
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

        private void grdData_CurrentCellChanged(object sender, EventArgs e)
        {
            int index = grdData.CurrentRowIndex;
            if (index > -1 && index < grdData.DataTable.Rows.Count)
            {
                if (m_isSpareCg || (m_parm != null && "S".Equals(m_parm.SpCaCoCd)))
                {
                    string strCgNo = grdData.DataTable.Rows[index][HEADER_CGNO].ToString();
                    CommonUtility.SetComboboxSelectedItem(cboSpareCg, strCgNo);
                }
                string strWHId = grdData.DataTable.Rows[index][HEADER_CELL].ToString();
                string strMT = grdData.DataTable.Rows[index][HEADER_MT].ToString();
                string strM3 = grdData.DataTable.Rows[index][HEADER_M3].ToString();
                string strQty = grdData.DataTable.Rows[index][HEADER_QTY].ToString();

                
                CommonUtility.SetComboboxSelectedItem(cboLocation, strWHId);
                txtMT.Text = strMT;
                txtM3.Text = strM3;
                txtQty.Text = strQty;

                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void cboLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCellMt.Text = "0";
            txtCellM3.Text = "0";
            txtCellQty.Text = "0";
            txtMT.Text = "0";
            txtM3.Text = "0";
            txtQty.Text = "0";
            m_whTpCd = string.Empty;
            if (cboLocation.SelectedIndex > 0)
            {
                WhConfigurationItem item = (WhConfigurationItem)m_whConfigItems[cboLocation.SelectedIndex - 1];
                string strCellMt = CommonUtility.ToString(item.wgt);
                string strCellM3 = CommonUtility.ToString(item.msrmt);
                string strCellQty = item.pkgQty;
                txtCellMt.Text = strCellMt;
                txtCellM3.Text = strCellM3;
                txtCellQty.Text = strCellQty;
                m_whTpCd = item.whTpCd;
                CalcActAmount(item.wgt, item.msrmt, item.pkgQty);
            }
        }

        private void cboSpareCg_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeLocation(CommonUtility.GetComboboxSelectedValue(cboSpareCg));
        }
    }
}