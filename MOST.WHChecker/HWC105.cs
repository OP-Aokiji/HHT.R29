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
    public partial class HWC105 : TForm, IPopupWindow
    {
        /*
        Category:
        CATGTP	E	Export	
        CATGTP	I	Import	
        CATGTP	R	Rehandle	
        CATGTP	S	Storage	
        CATGTP	T	TransShipment	
        */
        private const string CATGTP_IMPORT = "I";
        private const string CATGTP_EXPORT = "E";
        private const string CATGTP_STORAGE = "S";
        private const string CATGTP_REHANDLE = "R";
        private const string CATGTP_TRANSHIPMENT = "T";

        private const string HEADER_ITEM_STATUS = "_ITEM_STATUS";
        private const string HEADER_WS_NM = "Status";
        private const string HEADER_CG_COND = "Cg.Condition";   // whTpNm
        private const string HEADER_CATEGORY = "Category";      // opeClassNm
        private const string HEADER_JPVC = "JPVC";
        private const string HEADER_SNBL = "SN/BL";
        private const string HEADER_GR = "GR";
        private const string HEADER_SPARE = "Spare";
        private const string HEADER_WH = "WH";
        private const string HEADER_MT = "MT";
        private const string HEADER_M3 = "M3";
        private const string HEADER_QTY = "Qty";
        private const string HEADER_CGNO = "_cgNo";
        private const string HEADER_WHTPCD = "_whTpCd";
        private const string HEADER_OPECLASSCD = "_opeClassCd";

        private bool m_reloadedCboWithinJpvc;
        private HWC105Parm m_parm;
        HWC106Parm m_detailParm;

        public HWC105()
        {
            m_reloadedCboWithinJpvc = false;
            m_detailParm = new HWC106Parm();
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            InitializeData();
            btnReconcile.Enabled = false;
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HWC105Parm)parm;
            if (m_parm != null)
            {
                txtJPVC.Text = m_parm.VslCallId;

                GetSNBLComboboxes(txtJPVC.Text);
                CommonUtility.SetComboboxSelectedItem(cboBL, m_parm.BlNo);
                CommonUtility.SetComboboxSelectedItem(cboSN, m_parm.SnNo);
                txtGR.Text = m_parm.GrNo;

                if (!string.IsNullOrEmpty(txtGR.Text))
                {
                    CommonUtility.SetComboboxSelectedItem(cboCategory, CATGTP_EXPORT);
                }
                else if (cboBL.SelectedIndex > 0)
                {
                    CommonUtility.SetComboboxSelectedItem(cboCategory, CATGTP_IMPORT);
                }

                if (!string.IsNullOrEmpty(txtJPVC.Text) ||
                    cboCategory.SelectedIndex > 0 ||
                    cboWH.SelectedIndex > 0)
                {
                    GetWHRecnList();
                }
            }
            this.ShowDialog();
            return null;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_ITEM_STATUS, "0" }, { HEADER_WS_NM, "40" }, { HEADER_CG_COND, "70" }, { HEADER_SPARE, "40" } , { HEADER_CATEGORY, "70" }, { HEADER_JPVC, "90" }, { HEADER_SNBL, "90" }, { HEADER_GR, "80" }
            , { HEADER_WH, "40" } , { HEADER_MT, "40" }, { HEADER_M3, "40" }, { HEADER_QTY, "40" }, { HEADER_CGNO, "0" }, { HEADER_WHTPCD, "0" }, { HEADER_OPECLASSCD, "0" } };
            this.grdData.setHeader(header);
        }

        private void SetDateVisible(bool bVisible)
        {
            if (bVisible)
            {
                // Set JPVC invisible
                txtJPVC.Visible = false;
                lblJPVC.Visible = false;
                btnF1.Visible = false;

                // Set date visible
                txtFromTime.Visible = true;
                txtToTime.Visible = true;
                lblDate.Visible = true;
                lblDate2.Visible = true;
            }
            else
            {
                // Set date invisible
                txtFromTime.Visible = false;
                txtToTime.Visible = false;
                lblDate.Visible = false;
                lblDate2.Visible = false;

                // Set JPVC visible
                txtJPVC.Visible = true;
                lblJPVC.Visible = true;
                btnF1.Visible = true;
            }
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Date
                txtFromTime.CustomFormat = Framework.Controls.UserControls.TDateTimePicker.FORMAT_DDMMYYYY;
                txtToTime.CustomFormat = Framework.Controls.UserControls.TDateTimePicker.FORMAT_DDMMYYYY;
                CommonUtility.SetDTPValueBlank(txtFromTime);
                CommonUtility.SetDTPValueBlank(txtToTime);
                SetDateVisible(false);
                #endregion

                #region W/H, Category
                IWHCheckerProxy proxy = new WHCheckerProxy();
                WHReconciliationParm parm = new WHReconciliationParm();
                parm.searchType = "whrecncombolist";
                ResponseInfo info = proxy.getWHRecnList(parm);

                CommonUtility.InitializeCombobox(cboWH);
                CommonUtility.InitializeCombobox(cboCategory);
                for (int i = 0; i < info.list.Length; i++)
                {
                    // Category type
                    if (info.list[i] is CodeMasterListItem)
                        info.list[i] = CommonUtility.ToCodeMasterListItem1(info.list[i] as CodeMasterListItem);
                    if (info.list[i] is CodeMasterListItem1)
                    {
                        // Add categories, excluding Rehandle 
                        CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                        if (!HWC105.CATGTP_REHANDLE.Equals(item.scd))
                        {
                            cboCategory.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                        }
                    }

                    // W/H Location
                    else if (info.list[i] is LocationCodeItem)
                    {
                        LocationCodeItem item = (LocationCodeItem)info.list[i];
                        cboWH.Items.Add(new ComboboxValueDescriptionPair(item.cd, item.cdNm));
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
                Cursor.Current = Cursors.Default;
            }
        }

        private void GetWHRecnList()
        {
            try
            {
                #region Grid Data
                string cgCategory = CommonUtility.GetComboboxSelectedValue(cboCategory);
                IWHCheckerProxy proxy = new WHCheckerProxy();
                WHReconciliationParm parm = new WHReconciliationParm();
                parm.searchType = "whrecnlist";
                parm.whLocId = CommonUtility.GetComboboxSelectedValue(cboWH);
                parm.category = cgCategory;
                if (CATGTP_STORAGE.Equals(cgCategory))
                {
                    parm.vslCallId = Constants.NONCALLID;
                    parm.estArrvDtFrom = txtFromTime.Text;
                    parm.estArrvDtTo = txtToTime.Text;
                }
                else if (CATGTP_IMPORT.Equals(cgCategory) ||
                    CATGTP_TRANSHIPMENT.Equals(cgCategory))
                {
                    parm.vslCallId = txtJPVC.Text;
                    parm.blNo = CommonUtility.GetComboboxSelectedValue(cboBL);
                }
                else if (CATGTP_EXPORT.Equals(cgCategory))
                {
                    parm.vslCallId = txtJPVC.Text;
                    parm.snNo = CommonUtility.GetComboboxSelectedValue(cboSN);
                    parm.grNo = txtGR.Text;
                }
                else
                {
                    parm.vslCallId = txtJPVC.Text;
                    parm.blNo = CommonUtility.GetComboboxSelectedValue(cboBL);
                    parm.snNo = CommonUtility.GetComboboxSelectedValue(cboSN);
                    parm.grNo = txtGR.Text;
                }
                ResponseInfo info = proxy.getWHRecnList(parm);

                grdData.Clear();
                if (info != null)
                {
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is WHReconciliationItem)
                        {
                            WHReconciliationItem item = (WHReconciliationItem)info.list[i];
                            DataRow newRow = grdData.NewRow();
                            newRow[HEADER_ITEM_STATUS] = Constants.ITEM_OLD;
                            newRow[HEADER_WS_NM] = Constants.WS_NM_INITIAL;
                            newRow[HEADER_CG_COND] = item.whTpNm;
                            newRow[HEADER_CATEGORY] = item.opeClassNm;
                            newRow[HEADER_JPVC] = item.vslCallId;
                            newRow[HEADER_SNBL] = item.snBlNo;
                            newRow[HEADER_GR] = item.grNo;
                            newRow[HEADER_SPARE] = item.spCaCoNm;
                            newRow[HEADER_WH] = item.locId;
                            newRow[HEADER_MT] = item.wgt;
                            newRow[HEADER_M3] = item.msrmt;
                            newRow[HEADER_QTY] = item.pkgQty;
                            newRow[HEADER_CGNO] = item.cgNo;
                            newRow[HEADER_WHTPCD] = item.whTpCd;
                            newRow[HEADER_OPECLASSCD] = item.opeClassCd;

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
            }
        }

        private bool ValidateSearchWhRecn()
        {
            bool result = true;

            if (string.IsNullOrEmpty(txtJPVC.Text) && cboWH.SelectedIndex <= 0 && cboCategory.SelectedIndex <= 0)
            {
                CommonUtility.AlertMessage("Please input at least one of the followings: JPVC, WH, Category");
                result = false;
            }
            return result;
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnOk":
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                    break;

                case "btnCancel":
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    break;

                case "btnF1":
                    MOST.Common.CommonParm.SearchJPVCParm jpvcParm = new MOST.Common.CommonParm.SearchJPVCParm();
                    jpvcParm.Jpvc = txtJPVC.Text;
                    SearchJPVCResult jpvcResultTmp = (SearchJPVCResult)PopupManager.instance.ShowPopup(new HCM101(), jpvcParm);
                    if (jpvcResultTmp != null)
                    {   
                        txtJPVC.Text = jpvcResultTmp.Jpvc;
                        GetSNBLComboboxes(txtJPVC.Text);
                    }
                    break;

                case "btnF2":
                    GRListParm grParm = new GRListParm();
                    grParm.Jpvc = txtJPVC.Text;
                    grParm.GrNo = txtGR.Text;
                    grParm.ShipgNoteNo = CommonUtility.GetComboboxSelectedValue(cboSN);
                    CargoExportResult grResultTmp = (CargoExportResult)PopupManager.instance.ShowPopup(new HCM103(), grParm);
                    if (grResultTmp != null)
                    {   
                        txtJPVC.Text = grResultTmp.VslCallId;
                        CommonUtility.SetComboboxSelectedItem(cboSN, grResultTmp.ShipgNoteNo);
                        txtGR.Text = grResultTmp.GrNo;
                    }
                    break;

                case "btnRetrieve":
                    if (this.validations(this.Controls) && ValidateSearchWhRecn())
                    {
                        GetWHRecnList();
                    }
                    break;

                case "btnReconcile":
                    PopupManager.instance.ShowPopup(new HWC106(), m_detailParm);
                    GetWHRecnList();
                    break;
            }
        }

        private void grdData_CurrentCellChanged(object sender, EventArgs e)
        {
            int index = grdData.CurrentRowIndex;
            if (index > -1 && index < grdData.DataTable.Rows.Count)
            {
                btnReconcile.Enabled = true;

                m_detailParm.VslCallId = txtJPVC.Text;
                m_detailParm.OpeClassCd = grdData.DataTable.Rows[index][HEADER_OPECLASSCD].ToString();
                m_detailParm.SnBlNo = grdData.DataTable.Rows[index][HEADER_SNBL].ToString();
                m_detailParm.WhTpCd = grdData.DataTable.Rows[index][HEADER_WHTPCD].ToString();
                m_detailParm.WhTpNm = grdData.DataTable.Rows[index][HEADER_CG_COND].ToString();
                m_detailParm.CgNo = grdData.DataTable.Rows[index][HEADER_CGNO].ToString();
                m_detailParm.Wgt = grdData.DataTable.Rows[index][HEADER_MT].ToString();
                m_detailParm.Msrmt = grdData.DataTable.Rows[index][HEADER_M3].ToString();
                m_detailParm.PkgQty = grdData.DataTable.Rows[index][HEADER_QTY].ToString();
            }
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDateVisible(false);

            //cargo category
            string cgCategory = CommonUtility.GetComboboxSelectedValue(cboCategory);
            if (CATGTP_STORAGE.Equals(cgCategory))
            {
                SetDateVisible(true);
                cboBL.Enabled = false;
                cboSN.Enabled = true;
                txtGR.Enabled = true;
                btnF2.Enabled = true;
            }
            else if (CATGTP_IMPORT.Equals(cgCategory) ||
                CATGTP_TRANSHIPMENT.Equals(cgCategory))
            {
                cboBL.Enabled = true;
                cboSN.Enabled = false;
                txtGR.Enabled = false;
                btnF2.Enabled = false;
            }
            else if (CATGTP_EXPORT.Equals(cgCategory))
            {
                cboBL.Enabled = false;
                cboSN.Enabled = true;
                txtGR.Enabled = true;
                btnF2.Enabled = true;
            }
            else
            {
                cboBL.Enabled = false;
                cboSN.Enabled = false;
                txtGR.Enabled = false;
                btnF2.Enabled = false;
            }
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            PopupManager.instance.ShowPopup(new HWC106(), m_detailParm);
        }

        private void txtJPVC_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJPVC.Text) && !m_reloadedCboWithinJpvc)
            {
                GetSNBLComboboxes(txtJPVC.Text);
            }
        }

        private void txtJPVC_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if key = Enter then get list of SN, BL
            if (e.KeyChar.Equals((char)Keys.Enter))
            {
                GetSNBLComboboxes(txtJPVC.Text);
            }
        }

        private void GetSNBLComboboxes(string vslCallId)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (!string.IsNullOrEmpty(vslCallId))
                {
                    // For handling event in case it occurs: KeyPress -> LostFocus
                    m_reloadedCboWithinJpvc = true;

                    // S/N
                    IWHCheckerProxy proxy = new WHCheckerProxy();
                    WHReconciliationParm parm = new WHReconciliationParm();
                    parm.searchType = "whrecnDoclist";
                    if (CATGTP_STORAGE.Equals(CommonUtility.GetComboboxSelectedValue(cboCategory)))
                    {
                        parm.vslCallId = Constants.NONCALLID;
                        parm.estArrvDtFrom = txtFromTime.Text;
                        parm.estArrvDtTo = txtToTime.Text;
                    }
                    else
                    {
                        parm.vslCallId = txtJPVC.Text;
                    }
                    parm.divCd = "SN";
                    ResponseInfo info = proxy.getWHRecnList(parm);
                    CommonUtility.InitializeCombobox(cboSN);
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is WHReconciliationItem)
                        {
                            WHReconciliationItem item = (WHReconciliationItem)info.list[i];
                            cboSN.Items.Add(new ComboboxValueDescriptionPair(item.docNo, item.docNo));
                        }
                    }

                    // B/L
                    parm.divCd = "BL";
                    info = proxy.getWHRecnList(parm);
                    CommonUtility.InitializeCombobox(cboBL);
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is WHReconciliationItem)
                        {
                            WHReconciliationItem item = (WHReconciliationItem)info.list[i];
                            cboBL.Items.Add(new ComboboxValueDescriptionPair(item.docNo, item.docNo));
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

        private void txtJPVC_TextChanged(object sender, EventArgs e)
        {
            m_reloadedCboWithinJpvc = false;
        }
    }
}