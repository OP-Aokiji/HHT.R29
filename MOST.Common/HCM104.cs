using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using MOST.Client.Proxy.CommonProxy;
using Framework.Service.Provider.WebService.Provider;
using System.Collections;
using Framework.Common.PopupManager;
using Framework.Common.ExceptionHandler;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.Common.Utility;
using Framework.Controls.UserControls;
using Framework.Common.Constants;

namespace MOST.Common
{
    public partial class HCM104 : TDialog, IPopupWindow
    {
        #region Local Variable
        private BLListParm m_parm;
        private BLListResult m_result;
        private int m_rowsCnt;
        private readonly String HEADER_JPVC = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM104_0002");
        private readonly String HEADER_BL = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM104_0004");
        private readonly String HEADER_SHPRCONS = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM104_0010");
        private readonly String HEADER_STATUS = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM104_0009");
        private readonly String HEADER_DO = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM104_0005");
        private readonly String HEADER_FA = "F/A";
        private readonly String HEADER_MT = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM104_0006");
        private readonly String HEADER_M3 = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM104_0007");
        private readonly String HEADER_QTY = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM104_0008");
        private const String    HEADER_CGTPCD = "_CGTPCD";        // Cargo Type
        private const String HEADER_FNLOPEYN = "_FNLOPEYN";        // Final Dicharging
        private const String HEADER_FNLDELVYN = "_FNLDELVYN";        // Final H/O

        /**
         * lv.dat
         * add local val for paging and status filter
         */
        private int iTotalPage = 0;
        private int iCurrentPage = 0;
        private int iNumbPerPage = 5;
        private string sStat = "";
        private int iFlag = 1; // 1 - load using blno 2 - load when chose status

        // now make this val to be local 
        Framework.Service.Provider.WebService.Provider.CargoImportParm parm;

        #endregion

        public HCM104()
        {
            InitializeComponent();
            InitializeDataGrid();
            this.initialFormSize();
        }

        private void InitializeDataGrid()
        {
            btnOk.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0013");
            btnCancel.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0014");
            btnSearch.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0015");
            this.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM104_0001");
            lblJPVC.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM104_0002");
            lblBLNo.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM104_0003");
            txtJPVC.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM104_0002");
            txtBLNo.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM104_0003");

            String[,] header = { { HEADER_JPVC, "85" }, { HEADER_BL, "85" }, {HEADER_SHPRCONS, "85"}, { HEADER_STATUS, "80" }
                                ,{ HEADER_DO, "85" },{ HEADER_FA, "60" }, { HEADER_MT, "90" }, { HEADER_M3, "90" }, { HEADER_QTY, "75" }
                                , { HEADER_CGTPCD, "0" }, { HEADER_FNLOPEYN, "0" }, { HEADER_FNLDELVYN, "0" } };
            grdBLList.setHeader(header);
        }

        private void InitializeCboStatus()
        {
            try
            {
                int iIndexDefault = 0;
                ICommonProxy proxy = new CommonProxy();
                CommonUtility.InitializeCombobox(cboStatus);

                CargoExportParm cgExpParm = new CargoExportParm();

                ResponseInfo info = proxy.getStatusList(cgExpParm);

                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CodeMasterListItem)
                    {
                        CodeMasterListItem item = (CodeMasterListItem)info.list[i];
                        cboStatus.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));

                        if (item.scdNm.Equals("Reserved"))
                            iIndexDefault = i;
                    }
                    else if (info.list[i] is CodeMasterListItem1)
                    {
                        CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                        cboStatus.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));

                        if (item.scdNm.Equals("Reserved"))
                            iIndexDefault = i;
                    }
                }

                cboStatus.SelectedIndex = iIndexDefault + 1;
                this.sStat = cboStatus.Text;
                this.cboStatus.SelectedIndexChanged += new System.EventHandler(this.cboStatus_SelectedIndexChanged);
            }
            catch (Framework.Common.Exception.BusinessException ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private void InitializeCboPaging()
        {
            InitializeParam();

            ICommonProxy proxy = new CommonProxy();
            ResponseInfo numbInfo = proxy.getCargoImportListNumbPage(parm);
            this.iTotalPage = getNumbPage((int)numbInfo.list[0]);

            cboPaging.Items.Clear();
            cboPaging.Text = "";

            for (int i = 1; i <= this.iTotalPage; i++)
                this.cboPaging.Items.Add(i);

            if (this.iTotalPage > 0)
            {
                cboPaging.SelectedIndex = 0;
            }
            else
                grdBLList.Clear();
        }

        private void InitializeParam()
        {
            parm = new Framework.Service.Provider.WebService.Provider.CargoImportParm();

            if (!String.IsNullOrEmpty(txtJPVC.Text))
            {
                parm.vslCallId = txtJPVC.Text;
            }
            if (!String.IsNullOrEmpty(txtBLNo.Text))
            {
                parm.blNo = txtBLNo.Text;
            }
            if (m_parm.ExcludeFnlDischarging)
            {
                parm.hhtFnlMode = Constants.FINAL_MODE_DSFN;
            }
            else if (m_parm.ExcludeFnlHandlingOut)
            {
                parm.hhtFnlMode = Constants.FINAL_MODE_HOFN;
            }
            //if(iFlag == 2)
            //    parm.stat = this.sStat;

            //parm.numbPerPage = this.iNumbPerPage;
            //parm.pageType = "BL";

            //parm.currentPage = this.iCurrentPage;
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (MOST.Common.CommonParm.BLListParm)parm;
            txtJPVC.Text = m_parm.Jpvc;
            
            if (!string.IsNullOrEmpty(txtJPVC.Text))
            {
                if (!string.IsNullOrEmpty(m_parm.BlNo))
                {
                    this.txtBLNo.Text = m_parm.BlNo;
                }
                
                F_Search();
            }

            /**
             * lv.dat
             * init for data paging
             */
            //InitializeCboStatus();

            //InitializeCboPaging();

            this.ShowDialog();
            return m_result;
        }

        private void F_Search()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Request Webservice
                InitializeParam();
                ICommonProxy proxy = new CommonProxy();
                //parm.currentPage = this.iCurrentPage;

                ResponseInfo info = proxy.getCargoImportList(parm);
                #endregion

                #region Display Data
                grdBLList.Clear();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CargoImportItem)
                    {
                        CargoImportItem item = (CargoImportItem)info.list[i];
                        DataRow newRow = grdBLList.NewRow();
                        newRow[HEADER_JPVC] = item.vslCallId;
                        newRow[HEADER_BL] = item.blNo;
                        newRow[HEADER_SHPRCONS] = item.cngShp;
                        newRow[HEADER_STATUS] = item.statNm;
                        newRow[HEADER_DO] = item.doNo;
                        newRow[HEADER_FA] = item.fwrAgnt;
                        newRow[HEADER_MT] = item.docMt;
                        newRow[HEADER_M3] = item.docM3;
                        newRow[HEADER_QTY] = item.docQty;
                        newRow[HEADER_CGTPCD] = item.cgTpCd;
                        newRow[HEADER_FNLOPEYN] = item.fnlOpeYn;
                        newRow[HEADER_FNLDELVYN] = item.fnlDelvYn;
                        grdBLList.Add(newRow);

                        //if (!String.IsNullOrEmpty(txtBLNo.Text) && iFlag == 1)
                        //{
                        //    this.cboStatus.SelectedIndexChanged -= this.cboStatus_SelectedIndexChanged;
                        //    cboStatus.SelectedIndex = 0;
                        //    this.cboStatus.SelectedIndexChanged += this.cboStatus_SelectedIndexChanged;
                        //}
                    }
                }
                grdBLList.Refresh();

                if (info.list != null)
                {
                    m_rowsCnt = info.list.Length;
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

        private void ReturnBLInfo()
        {
            int currRowIndex = grdBLList.CurrentRowIndex;
            string strJpvc = grdBLList.DataTable.Rows[currRowIndex][HEADER_JPVC].ToString();
            string strBl = grdBLList.DataTable.Rows[currRowIndex][HEADER_BL].ToString();
            string strDo = grdBLList.DataTable.Rows[currRowIndex][HEADER_DO].ToString();
            string strFA = grdBLList.DataTable.Rows[currRowIndex][HEADER_FA].ToString();
            string strMt = grdBLList.DataTable.Rows[currRowIndex][HEADER_MT].ToString();
            string strM3 = grdBLList.DataTable.Rows[currRowIndex][HEADER_M3].ToString();
            string strQty = grdBLList.DataTable.Rows[currRowIndex][HEADER_QTY].ToString();
            string strCgTpCd = grdBLList.DataTable.Rows[currRowIndex][HEADER_CGTPCD].ToString();
            string strFnlOpeYn = grdBLList.DataTable.Rows[currRowIndex][HEADER_FNLOPEYN].ToString();
            string strFnlDelvYn = grdBLList.DataTable.Rows[currRowIndex][HEADER_FNLDELVYN].ToString();

            m_result = new BLListResult();
            m_result.VslCallId = strJpvc;
            m_result.Bl = strBl;
            m_result.DoNo = strDo;
            m_result.FwrAgnt = strFA;
            m_result.Mt = strMt;
            m_result.M3 = strM3;
            m_result.Qty = strQty;
            m_result.CgTpCd = strCgTpCd;
            m_result.FnlOpeYn = strFnlOpeYn;
            m_result.FnlDelvYn = strFnlDelvYn;
        }

        /**
         * lv.dat
         * add method for paging
         */

        private int getNumbPage(int iNumbRow)
        {
            if (iNumbRow == 0)
                return iNumbRow;

            int iNumbPage = iNumbRow / this.iNumbPerPage;
            if ((iNumbPage == 0 && iNumbRow > 0) || iNumbRow % this.iNumbPerPage != 0)
                iNumbPage++;

            return iNumbPage;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.validations(this.Controls) &&
                (!string.IsNullOrEmpty(txtJPVC.Text) || !string.IsNullOrEmpty(txtBLNo.Text)))
            {
                F_Search();
                //if (!String.IsNullOrEmpty(txtBLNo.Text))
                //    iFlag = 1;

                //InitializeCboPaging();
                if (m_rowsCnt <= 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0058"));
                    txtJPVC.Focus();
                    txtJPVC.SelectAll();
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int currRowIndex = grdBLList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnBLInfo();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void grdBLList_DoubleClick(object sender, EventArgs e)
        {
            int currRowIndex = grdBLList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnBLInfo();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.sStat = ((ComboBox)sender).Text;
            parm.stat = this.sStat;

            iFlag = 2;
            InitializeCboPaging();
        }

        private void cboPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.iCurrentPage = int.Parse(((ComboBox)sender).Text);

            F_Search();
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

        private void HCM104_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                MessageBox.Show("Screen ID : " + this.Name + "\n\rScreen caption: " + this.Text, "Screen Information !");
            }
        }
    }
}