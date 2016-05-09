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
using MOST.Common.CommonResult;
using MOST.Common.Utility;
using Framework.Controls.UserControls;
using Framework.Common.Constants;
using Framework.Common.ExceptionHandler;

namespace MOST.Common
{
    public partial class HCM115 : TDialog, IPopupWindow
    {
        #region Local Variable
        private readonly string HEADER_LDDS = "LD/DS";
        private readonly string HEADER_SNBL = "SN/BL";
        private readonly string HEADER_GR = "GR";
        private readonly string HEADER_CGCOND = "Cg Cond.";
        private readonly string HEADER_REASON = "Reason";
        //lv.dat add 10-06-2013
        private readonly string HEADER_JOBSTRIP = "Job Trips";

        /**
         * lv.dat add paging 20130613
         */
        private int iTotalPage = 0;
        private int iCurrentPage = 0;
        private int iNumbPerPage = Constants.iNumbPerPage;

        Framework.Service.Provider.WebService.Provider.UnclosedOperationParm parm; 

        #endregion

        public HCM115()
        {
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            /*
                Authority Check
             */
            this.authorityCheck();
        }

        private void InitializeDataGrid()
        {
            btnExit.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0001");
            String[,] header = { { HEADER_LDDS, "85" }, { HEADER_SNBL, "85" }, { HEADER_GR, "70" }, { HEADER_CGCOND, "70" }, { HEADER_REASON, "140" }, { HEADER_JOBSTRIP, "80" } };
            this.grdData.setHeader(header);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            MOST.Common.CommonParm.UnclosedOperationParm unclosedOprParm = (MOST.Common.CommonParm.UnclosedOperationParm)parm;
            txtJPVC.Text = unclosedOprParm.VslCallId;
            if (!string.IsNullOrEmpty(txtJPVC.Text))
            {
                //F_Retrieve();
                InitializeCboPaging();
            }
            this.ShowDialog();
            return null;
        }

        private void F_Retrieve()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ICommonProxy proxy = new CommonProxy();
                /*Framework.Service.Provider.WebService.Provider.UnclosedOperationParm parm = new Framework.Service.Provider.WebService.Provider.UnclosedOperationParm();
                parm.searchType = "UnclosedOperationList";
                if (!string.IsNullOrEmpty(txtJPVC.Text))
                {
                    parm.vslCallId = txtJPVC.Text;
                }*/

                ResponseInfo info = proxy.getUnclosedOperationList(parm);

                grdData.Clear();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is UnclosedOperationItem)
                    {
                        UnclosedOperationItem item = (UnclosedOperationItem)info.list[i];
                        DataRow newRow = grdData.NewRow();
                        newRow[HEADER_LDDS] = item.cgOpTp;
                        newRow[HEADER_SNBL] = item.blSn;
                        newRow[HEADER_GR] = item.grNo;
                        newRow[HEADER_CGCOND] = item.cgCond;
                        newRow[HEADER_REASON] = item.rsn;
                        newRow[HEADER_JOBSTRIP] = item.jobStrip;
                        grdData.Add(newRow);
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

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnF1":
                    MOST.Common.CommonParm.SearchJPVCParm jpvcParm = new MOST.Common.CommonParm.SearchJPVCParm();
                    jpvcParm.Jpvc = txtJPVC.Text.Trim();
                    jpvcParm.IsWHChecker = "VSL";
                    MOST.Common.CommonResult.SearchJPVCResult jpvcResult = (SearchJPVCResult)PopupManager.instance.ShowPopup(new MOST.Common.HCM101(), jpvcParm);
                    if (jpvcResult != null)
                    {
                        txtJPVC.Text = jpvcResult.Jpvc;
                    }
                    break;

                case "btnRetrieve":
                    //F_Retrieve();
                    InitializeCboPaging();
                    break;

                case "btnExit":
                    this.Close();
                    break;
            }
        }

        /**
         * lv.dat add 20130613
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
            parm = new Framework.Service.Provider.WebService.Provider.UnclosedOperationParm();
            parm.searchType = "UnclosedOperationList";
            if (!string.IsNullOrEmpty(txtJPVC.Text))
            {
                parm.vslCallId = txtJPVC.Text;
            }

            parm.currentPage = this.iCurrentPage.ToString();
            parm.numbPerPage = this.iNumbPerPage.ToString();
            parm.pageType = "UOL";
        }

        private void InitializeCboPaging()
        {
            cboPaging.Items.Clear();
            cboPaging.Text = "";

            initializeParameter();

            ICommonProxy proxy = new CommonProxy();
            ResponseInfo numbInfo = proxy.getUnclosedOperationListNumbPage(parm);
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