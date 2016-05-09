using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using MOST.Client.Proxy.CommonProxy;
using MOST.WHChecker;
using MOST.WHChecker.Parm;
using Framework.Service.Provider.WebService.Provider;
using System.Collections;
using Framework.Common.PopupManager;
using MOST.Common.Utility;
using Framework.Common.Constants;
using Framework.Controls.UserControls;
using Framework.Common.ExceptionHandler;

namespace MOST.WHChecker
{
    public partial class HWC107 : TDialog, IPopupWindow
    {
        #region Local Variable
        private const String HEADER_CATG = "Category";
        private const String HEADER_JPVC = "JPVC";
        private const String HEADER_SNBL = "SN/BL";
        private const String HEADER_CGNO = "GR";
        private const String HEADER_CGCOND = "Cg Cond.";
        private const String HEADER_SPECGCOND = "Sp Cg Cond.";
        private const String HEADER_RHDMODE = "Rhd Mode";
        private const String HEADER_RHDMT = "Rhd Mt";
        private const String HEADER_RHDM3 = "Rhd M3";
        private const String HEADER_RHDQTY = "Rhd Qty";
        private const String HEADER_MT = "MT";
        private const String HEADER_M3 = "M3";
        private const String HEADER_QTY = "Qty";
        private const String HEADER_STATUS = "Status";
        private const String HEADER_CGTYPE = "Cg Type";
        private const String HEADER_NXCGNO = "_NXCGNO";
        private const String HEADER_ORGRNO = "_ORGRNO";
        private const String HEADER_NXREFNO = "_NXREFNO";
        private const String HEADER_CGCOCD = "_CGCOCD";
        private const String HEADER_RHDLNO = "_RHDLNO";
        private const String HEADER_RHDLMODE = "_RHDLMODE";
        private const String HEADER_OPECLASSCD = "_OPECLASSCD";
        private const String HEADER_ORGBLSN = "_ORGBLSN";
        private const String HEADER_SPCACOCD = "_SPCACOCD";
        private const String HEADER_DELVTPCD = "_DELVTPCD";

        /*
         * lv.dat add paging 20130611
         */
        private int iTotalPage = 0;
        private int iCurrentPage = 0;
        private int iNumbPerPage = Constants.iNumbPerPage;

        Framework.Service.Provider.WebService.Provider.CargoRehandlingParm parm;

        #endregion

        public HWC107()
        {
            InitializeComponent();
            this.initialFormSize();

            /*
                Authority Check
             */
            this.authorityCheck();

            InitializeDataGrid();
            InitializeCboList();
            btnHO.Enabled = false;
        }

        private void InitializeDataGrid()
        {
            btnExit.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0016");

            String[,] header = { { HEADER_CATG, "85" }, { HEADER_JPVC, "90" }, { HEADER_SNBL, "85" }, { HEADER_CGNO, "90" }, { HEADER_CGCOND, "75" }, 
                                { HEADER_SPECGCOND, "80" }, { HEADER_RHDMODE, "90" }, { HEADER_RHDMT, "40" }, { HEADER_RHDM3, "40" }, { HEADER_RHDQTY, "40" }, 
                                { HEADER_MT, "40" }, { HEADER_M3, "40" }, { HEADER_QTY, "40" }, { HEADER_STATUS, "90" }, { HEADER_CGTYPE, "90" }, 
                                { HEADER_NXCGNO, "0" }, { HEADER_ORGRNO, "0" }, { HEADER_NXREFNO, "0" }, { HEADER_CGCOCD, "0" }, { HEADER_RHDLNO, "0" }
                                , { HEADER_RHDLMODE, "0" }, { HEADER_OPECLASSCD, "0" }, { HEADER_ORGBLSN, "0" }, { HEADER_SPCACOCD, "0" }, { HEADER_DELVTPCD, "0" }};
            this.grdData.setHeader(header);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            HWC107Parm rhdParm = (HWC107Parm)parm;
            if (Constants.NONCALLID.Equals(rhdParm.VslCallId))
            {
                rbtnNonJPVC.Checked = true;
            }
            else
            {
                txtJPVC.Text = rhdParm.VslCallId;
            }
            if (rbtnNonJPVC.Checked || 
                (rbtnJPVC.Checked && !string.IsNullOrEmpty(txtJPVC.Text)))
            {
                //F_Retrieve();
                InitializeCboPaging();
            }
            this.ShowDialog();
            return null;
        }

        private void InitializeCboList()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Category
                ICommonProxy commonProxy = new CommonProxy();

                CommonCodeParm commonParm = new CommonCodeParm();
                commonParm.searchType = "COMM";
                commonParm.lcd = "MT";
                commonParm.divCd = "CATGTP";
                ResponseInfo commonInfo = commonProxy.getCommonCodeList(commonParm);
                CommonUtility.InitializeCombobox(cboCatgTp);
                for (int i = 0; i < commonInfo.list.Length; i++)
                {
                    if (commonInfo.list[i] is CodeMasterListItem)
                    {
                        CodeMasterListItem item = (CodeMasterListItem)commonInfo.list[i];
                        cboCatgTp.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                    else if (commonInfo.list[i] is CodeMasterListItem1)
                    {
                        CodeMasterListItem1 item = (CodeMasterListItem1)commonInfo.list[i];
                        cboCatgTp.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                }
                #endregion

                #region Cargo condition
                commonParm = new CommonCodeParm();
                commonParm.searchType = "COMM";
                commonParm.lcd = "MT";
                commonParm.divCd = "CGCOCD";
                commonInfo = commonProxy.getCommonCodeList(commonParm);
                CommonUtility.InitializeCombobox(cboCgCoCd);
                for (int i = 0; i < commonInfo.list.Length; i++)
                {
                    if (commonInfo.list[i] is CodeMasterListItem)
                    {
                        CodeMasterListItem item = (CodeMasterListItem)commonInfo.list[i];
                        cboCgCoCd.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                    else if (commonInfo.list[i] is CodeMasterListItem1)
                    {
                        CodeMasterListItem1 item = (CodeMasterListItem1)commonInfo.list[i];
                        cboCgCoCd.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
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

        private void F_Retrieve()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ICommonProxy proxy = new CommonProxy();

                /*Framework.Service.Provider.WebService.Provider.CargoRehandlingParm parm = new Framework.Service.Provider.WebService.Provider.CargoRehandlingParm();
                parm.vslCallId = GetVslCallId();
                parm.col3 = "RH";
                if (cboCatgTp.SelectedIndex > 0)
                {
                    parm.opeClassCd = CommonUtility.GetComboboxSelectedValue(cboCatgTp);
                }
                if (cboCgCoCd.SelectedIndex > 0)
                {
                    parm.cgCoCd = CommonUtility.GetComboboxSelectedValue(cboCgCoCd);
                }
                parm.rhdlMode = "R";    // Return to shipper
                parm.hhtFnlMode = Constants.FINAL_MODE_RHHOFN;*/

                ResponseInfo info = proxy.getCargoRhdlOperation(parm);

                grdData.Clear();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CargoRehandlingItem)
                    {
                        CargoRehandlingItem item = (CargoRehandlingItem)info.list[i];
                        if (!string.IsNullOrEmpty(item.cgNo))
                        {
                            DataRow newRow = grdData.NewRow();
                            newRow[HEADER_CATG] = item.caTgNm;
                            newRow[HEADER_JPVC] = item.vslCallId;
                            newRow[HEADER_SNBL] = item.blSn;
                            newRow[HEADER_CGNO] = item.cgNo;
                            newRow[HEADER_CGCOND] = item.cgCoNm;
                            newRow[HEADER_SPECGCOND] = item.spCaCoNm;
                            newRow[HEADER_RHDMODE] = item.rhdlModeNm;
                            newRow[HEADER_RHDMT] = item.rhdlWgt;
                            newRow[HEADER_RHDM3] = item.rhdlMsrmt;
                            newRow[HEADER_RHDQTY] = item.rhdlPkgQty;
                            newRow[HEADER_MT] = item.wgt;
                            newRow[HEADER_M3] = item.msrmt;
                            newRow[HEADER_QTY] = item.pkgQty;
                            newRow[HEADER_STATUS] = item.statNm;
                            newRow[HEADER_CGTYPE] = item.cgTpCd;
                            newRow[HEADER_NXCGNO] = item.nxCgNo;
                            newRow[HEADER_ORGRNO] = item.orgGrNo;
                            newRow[HEADER_NXREFNO] = item.nxRefNo;
                            newRow[HEADER_CGCOCD] = item.cgCoCd;
                            newRow[HEADER_RHDLNO] = item.rhdlNo;
                            newRow[HEADER_RHDLMODE] = item.rhdlMode;
                            newRow[HEADER_OPECLASSCD] = item.opeClassCd;
                            newRow[HEADER_ORGBLSN] = item.orgBlSn;
                            newRow[HEADER_SPCACOCD] = item.spCaCoCd;
                            newRow[HEADER_DELVTPCD] = item.delvTpCd;
                            grdData.Add(newRow);
                        }
                    }
                }
                grdData.Refresh();
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

        private string GetVslCallId()
        {
            string vslCallId = string.Empty;
            if (rbtnJPVC.Checked)
            {
                vslCallId = txtJPVC.Text;
            }
            else if (rbtnNonJPVC.Checked)
            {
                vslCallId = Constants.NONCALLID;
            }
            return vslCallId;
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnF1":
                    MOST.Common.CommonParm.SearchJPVCParm jpvcParm = new MOST.Common.CommonParm.SearchJPVCParm();
                    MOST.Common.CommonResult.SearchJPVCResult jpvcResult = (MOST.Common.CommonResult.SearchJPVCResult)PopupManager.instance.ShowPopup(new MOST.Common.HCM101(), jpvcParm);
                    if (jpvcResult != null)
                    {
                        txtJPVC.Text = jpvcResult.Jpvc;
                    }
                    break;

                case "btnRetrieve":
                    //F_Retrieve();
                    InitializeCboPaging();
                    break;

                case "btnHO":
                    int currRowIndex = grdData.CurrentRowIndex;
                    if (currRowIndex > -1 && currRowIndex < grdData.DataTable.Rows.Count)
                    {
                        HWC108Parm rhdlParm = new HWC108Parm();
                        rhdlParm.VslCallId = grdData.DataTable.Rows[currRowIndex][HEADER_JPVC].ToString();
                        rhdlParm.CgNo = grdData.DataTable.Rows[currRowIndex][HEADER_CGNO].ToString();
                        rhdlParm.RhdlNo = grdData.DataTable.Rows[currRowIndex][HEADER_RHDLNO].ToString();
                        rhdlParm.RhdlMode = grdData.DataTable.Rows[currRowIndex][HEADER_RHDLMODE].ToString();
                        rhdlParm.CgCoCd = grdData.DataTable.Rows[currRowIndex][HEADER_CGCOCD].ToString();
                        rhdlParm.BlSn = grdData.DataTable.Rows[currRowIndex][HEADER_SNBL].ToString();
                        rhdlParm.CgTpCd = grdData.DataTable.Rows[currRowIndex][HEADER_CGTYPE].ToString();
                        rhdlParm.OpeClassCd = grdData.DataTable.Rows[currRowIndex][HEADER_OPECLASSCD].ToString();
                        rhdlParm.OrgBlSn = grdData.DataTable.Rows[currRowIndex][HEADER_ORGBLSN].ToString();
                        rhdlParm.SpCaCoCd = grdData.DataTable.Rows[currRowIndex][HEADER_SPCACOCD].ToString();
                        rhdlParm.OrgGrNo = grdData.DataTable.Rows[currRowIndex][HEADER_ORGRNO].ToString();
                        rhdlParm.DelvTpCd = grdData.DataTable.Rows[currRowIndex][HEADER_DELVTPCD].ToString();
                        PopupManager.instance.ShowPopup(new HWC108(), rhdlParm);
                        //F_Retrieve();
                        InitializeCboPaging();
                    }
                    break;

                case "btnExit":
                    this.Close();
                    break;
            }
        }

        private void grdData_CurrentCellChanged(object sender, EventArgs e)
        {
            if (grdData.CurrentRowIndex > -1 && grdData.CurrentRowIndex < grdData.DataTable.Rows.Count)
            {
                btnHO.Enabled = true;
            }
        }

        private void RadiobuttonListener(object sender, EventArgs e)
        {
            RadioButton mybutton = (RadioButton)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "rbtnJPVC":
                case "rbtnNonJPVC":
                    OnCheckRadioButton();
                    break;
            }
        }

        private void OnCheckRadioButton()
        {
            if (rbtnJPVC.Checked)
            {
                txtJPVC.Enabled = true;
                btnF1.Enabled = true;
            }
            else if (rbtnNonJPVC.Checked)
            {
                txtJPVC.Enabled = false;
                btnF1.Enabled = false;
            }
        }

        /**
         * lv.dat add 20130614
         */

        public int getNumbPage(int iNumbRow)
        {
            if (iNumbRow == 0)
                return iNumbRow;

            int iNumbPage = iNumbRow / this.iNumbPerPage;
            if ((iNumbPage == 0 && iNumbRow > 0) || iNumbRow % this.iNumbPerPage != 0)
                iNumbPage++;

            return iNumbPage;
        }

        private void initializeParameter()
        {
            parm = new Framework.Service.Provider.WebService.Provider.CargoRehandlingParm();
            parm.vslCallId = GetVslCallId();
            parm.col3 = "RH";
            if (cboCatgTp.SelectedIndex > 0)
            {
                parm.opeClassCd = CommonUtility.GetComboboxSelectedValue(cboCatgTp);
            }
            if (cboCgCoCd.SelectedIndex > 0)
            {
                parm.cgCoCd = CommonUtility.GetComboboxSelectedValue(cboCgCoCd);
            }
            parm.rhdlMode = "R";    // Return to shipper
            parm.hhtFnlMode = Constants.FINAL_MODE_RHHOFN;

            parm.currentPage = this.iCurrentPage.ToString();
            parm.numbPerPage = this.iNumbPerPage.ToString();
            parm.pageType = "CRO";
        }

        private void InitializeCboPaging()
        {
            cboPaging.Items.Clear();
            cboPaging.Text = "";

            initializeParameter();

            ICommonProxy proxy = new CommonProxy();
            ResponseInfo numbInfo = proxy.getCargoRhdlOperationNumbPage(parm);
            this.iTotalPage = getNumbPage((int)numbInfo.list[0]);

            for (int i = 1; i <= this.iTotalPage; i++)
                this.cboPaging.Items.Add(i);

            if (this.iTotalPage > 0)
            {
                cboPaging.SelectedIndex = 0;
            }
            else
                grdData.Clear();
        }

        private void cboPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.iCurrentPage = int.Parse(((ComboBox)sender).Text);
            parm.currentPage = this.iCurrentPage.ToString();

            F_Retrieve();
        }

        private void executePaging(object sender, EventArgs e)
        {
            Button btnHandle = (Button)sender;
            switch (btnHandle.Name)
            {
                case "btnPrev":
                    if (string.IsNullOrEmpty(cboPaging.Text) || cboPaging.SelectedIndex <= 0)
                        return;
                    cboPaging.SelectedIndex--;
                    break;
                case "btnNext":
                    if (string.IsNullOrEmpty(cboPaging.Text) || cboPaging.SelectedIndex >= this.iTotalPage - 1)
                        return;
                    cboPaging.SelectedIndex++;
                    break;
                default:
                    break;
            }
        }
    }
}