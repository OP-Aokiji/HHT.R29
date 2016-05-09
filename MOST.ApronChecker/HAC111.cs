using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using MOST.Client.Proxy.CommonProxy;
using MOST.ApronChecker;
using MOST.ApronChecker.Parm;
using Framework.Service.Provider.WebService.Provider;
using System.Collections;
using Framework.Common.PopupManager;
using MOST.Common.Utility;
using Framework.Common.Constants;
using Framework.Controls.UserControls;
using Framework.Common.ExceptionHandler;

namespace MOST.ApronChecker
{
    public partial class HAC111 : TDialog, IPopupWindow
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
        private const String HEADER_ORGVSLCALLID = "_ORGVSLCALLID";
        private const String HEADER_ORGCGNO = "_ORGCGNO";
        private const String HEADER_SPCACOCD = "_SPCACOCD";

        // Fix issue 33939
        private const string HEADER_OPECLASSCD = "_OPECLASSCD";
        private const string HEADER_RHDLGROUPNO = "_RHDLGROUPNO";
        private const string HEADER_ORGREFNO = "_ORGREFNO"; 
        #endregion

        public HAC111()
        {
            InitializeComponent();
            this.initialFormSize();

            /*
                Authority Check
             */
            this.authorityCheck();

            InitializeDataGrid();
            InitializeCboList();
            btnLoading.Enabled = false;
        }

        private void InitializeDataGrid()
        {
            btnExit.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0016");

            String[,] header = { { HEADER_CATG, "85" }, { HEADER_JPVC, "90" }, { HEADER_SNBL, "85" }, { HEADER_CGNO, "90" }, { HEADER_CGCOND, "75" },
                                 { HEADER_SPECGCOND, "80" }, { HEADER_RHDMODE, "90" }, { HEADER_RHDMT, "40" }, { HEADER_RHDM3, "40" }, { HEADER_RHDQTY, "40" }, 
                                 { HEADER_MT, "40" }, { HEADER_M3, "40" }, { HEADER_QTY, "40" }, { HEADER_STATUS, "90" }, { HEADER_CGTYPE, "90" }, 
                                 { HEADER_NXCGNO, "0" }, { HEADER_ORGRNO, "0" }, { HEADER_NXREFNO, "0" }, { HEADER_CGCOCD, "0" }, { HEADER_RHDLNO, "0" }, 
                                 { HEADER_ORGVSLCALLID, "0" }, { HEADER_ORGCGNO, "0" }, { HEADER_SPCACOCD, "0" },
                                 { HEADER_OPECLASSCD, "0" }, { HEADER_RHDLGROUPNO, "0" }, { HEADER_ORGREFNO, "0" } };
            this.grdData.setHeader(header);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            HAC111Parm rhdParm = (HAC111Parm)parm;
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
                F_Retrieve();
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

                Framework.Service.Provider.WebService.Provider.CargoRehandlingParm parm = new Framework.Service.Provider.WebService.Provider.CargoRehandlingParm();
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
                //parm.rhdlMode = "C";    // Change vessel
                //parm.hhtFnlMode = Constants.FINAL_MODE_RHLDFN;

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
                            newRow[HEADER_ORGVSLCALLID] = item.orgVslCallId;
                            newRow[HEADER_ORGCGNO] = item.orgCgNo;
                            newRow[HEADER_SPCACOCD] = item.spCaCoCd;

                            // Fix issue 33939
                            newRow[HEADER_OPECLASSCD] = item.opeClassCd;
                            newRow[HEADER_RHDLGROUPNO] = item.rhdlGroupNo;
                            newRow[HEADER_ORGREFNO] = item.orgRefNo;

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
                    F_Retrieve();
                    break;

                case "btnLoading":
                    int currRowIndex = grdData.CurrentRowIndex;
                    if (currRowIndex > -1 && currRowIndex < grdData.DataTable.Rows.Count)
                    {
                        HAC112Parm ldCnclParm = new HAC112Parm();
                        //ldCnclParm.VslCallId = grdData.DataTable.Rows[currRowIndex][HEADER_JPVC].ToString();
                        if (rbtnNonJPVC.Checked)
                        {
                            ldCnclParm.VslCallId = Constants.NONCALLID;
                        }
                        else if (rbtnJPVC.Checked)
                        {
                            ldCnclParm.VslCallId = txtJPVC.Text;
                        }
                        ldCnclParm.GrNo = grdData.DataTable.Rows[currRowIndex][HEADER_ORGRNO].ToString();
                        ldCnclParm.CgNo = grdData.DataTable.Rows[currRowIndex][HEADER_NXCGNO].ToString();
                        ldCnclParm.CgTpCd = grdData.DataTable.Rows[currRowIndex][HEADER_CGTYPE].ToString();
                        ldCnclParm.ShipgNoteNo = grdData.DataTable.Rows[currRowIndex][HEADER_NXREFNO].ToString();
                        ldCnclParm.JobCoCd = grdData.DataTable.Rows[currRowIndex][HEADER_CGCOCD].ToString();
                        ldCnclParm.RhdlNo = grdData.DataTable.Rows[currRowIndex][HEADER_RHDLNO].ToString();
                        ldCnclParm.OrgVslCallId = grdData.DataTable.Rows[currRowIndex][HEADER_ORGVSLCALLID].ToString();
                        ldCnclParm.OrgCgNo = grdData.DataTable.Rows[currRowIndex][HEADER_ORGCGNO].ToString();
                        ldCnclParm.CgCoCd = grdData.DataTable.Rows[currRowIndex][HEADER_CGCOCD].ToString();
                        ldCnclParm.SpCaCoCd = grdData.DataTable.Rows[currRowIndex][HEADER_SPCACOCD].ToString();

                        // Fix issue 33939
                        ldCnclParm.OpeClassCd = grdData.DataTable.Rows[currRowIndex][HEADER_OPECLASSCD].ToString();
                        ldCnclParm.RhdlGroupNo = grdData.DataTable.Rows[currRowIndex][HEADER_RHDLGROUPNO].ToString();
                        ldCnclParm.OrgRefNo = grdData.DataTable.Rows[currRowIndex][HEADER_ORGREFNO].ToString();

                        PopupManager.instance.ShowPopup(new HAC112(), ldCnclParm);
                        F_Retrieve();
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
                btnLoading.Enabled = true;
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

        private void HAC111_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q && e.Modifiers == Keys.Alt)
            {
                MessageBox.Show("Screen ID : " + this.Name + "\n\rScreen caption: " + this.Text, "Screen Information !");
            }
        }
    }
}