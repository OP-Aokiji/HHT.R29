using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using Framework.Common.PopupManager;
using Framework.Common.ExceptionHandler;
using MOST.Client.Proxy.CommonProxy;
using Framework.Service.Provider.WebService.Provider;
using MOST.Common.CommonResult;
using MOST.Common.Utility;

namespace MOST.Common
{
    public partial class HCM107 : TDialog, IPopupWindow
    {
        #region Local Variable
        public const int TYPE_HAC_LOADING = 1;
        public const int TYPE_HAC_DISCHARGING = 2;
        public const int TYPE_HPS = 3;
        private LorryListResult m_result;
        private SearchJPVCResult m_jpvcResult;
        private int m_rowsCnt;
        private int m_type; // 1: for AC loading, 2: for AC discharging, 3: for PS
        private readonly String HEADER_CONTRACTOR = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM107_0005");
        private readonly String HEADER_SNBL = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM107_0006");
        private readonly String HEADER_LORRY = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM107_0007");
        private readonly String HEADER_DRIVER = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM107_0008");
        #endregion

        public HCM107(int type)
        {
            this.m_type = type;
            InitializeComponent();
            this.initialFormSize();
            EnableJpvc();
            InitializeDataGrid();
        }

        private void EnableJpvc()
        {
            // HPS
            if (m_type == HCM107.TYPE_HPS)
            {
                this.btnF1.Visible = true;
                this.txtJPVC.Enabled = true;

                pnlAC.Visible = false;
                pnlPS.Visible = true;
                pnlPS.Top = 27;
                pnlPS.Left = 0;
            }
            // HAC
            else if (m_type == HCM107.TYPE_HAC_LOADING || m_type == HCM107.TYPE_HAC_DISCHARGING)
            {
                this.btnF1.Visible = false;
                this.txtJPVC.Enabled = false;

                pnlPS.Visible = false;
                pnlAC.Visible = true;
                pnlAC.Top = 27;
                pnlAC.Left = 0;
            }
        }

        private void InitializeDataGrid()
        {
            btnOk.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0013");
            btnCancel.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0014");
            btnSearch.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0015");
            btnF1.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0017");
            this.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM107_0001");
            lblJPVC.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM107_0002");
            lblSNBL.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM107_0003");
            lblSNBL2.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM107_0003");
            lblLorryNo.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM107_0004");
            txtJPVC.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM107_0002");
            cboBLNo.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM107_0003");
            txtLorryNo.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM107_0004");
            if (this.m_type == HCM107.TYPE_HAC_DISCHARGING)
            {
                String[,] header = { { HEADER_LORRY, "170" } };
                grdLorryList.setHeader(header);
            }
            else
            {
                String[,] header = { { HEADER_SNBL, "80" }, { HEADER_CONTRACTOR, "80" }, { HEADER_LORRY, "80" }, { HEADER_DRIVER, "120" } };
                grdLorryList.setHeader(header);
            }                     
        }

        private void InitializeCboSNBL(String argJpvc)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // In case user input JPVC directly (dont choose from popup):
                // return if JPVC is not correct
                if (m_type == HCM107.TYPE_HPS && m_jpvcResult == null && CommonUtility.IsValidJPVC(argJpvc, ref m_jpvcResult) == false)
                {
                    CommonUtility.InitializeCombobox(cboBLNo);
                    return;
                }

                #region BL No
                // Request web service
                ICommonProxy proxy2 = new CommonProxy();
                Framework.Service.Provider.WebService.Provider.DeliveryOrderParm parm2 = new Framework.Service.Provider.WebService.Provider.DeliveryOrderParm();
                parm2.vslCallId = argJpvc;
                parm2.searchType = "combo";

                ResponseInfo info2 = proxy2.getDeliveryOrderBLComboList(parm2);

                // Display data
                CommonUtility.InitializeCombobox(cboBLNo);
                for (int i = 0; i < info2.list.Length; i++)
                {
                    if (info2.list[i] is DeliveryOrderItem)
                    {
                        DeliveryOrderItem item = (DeliveryOrderItem)info2.list[i];
                        cboBLNo.Items.Add(item.blno);
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

        private void F_Search()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ICommonProxy proxy = new CommonProxy();
                AssignmentLorrysParm parm = new AssignmentLorrysParm();
                parm.searchType = !this.m_type.Equals(HCM107.TYPE_HAC_DISCHARGING) 
                    ? "onlyLorry" : "onlyLorryForDc";
                parm.vslCallId = txtJPVC.Text;

                if (m_type == HCM107.TYPE_HPS)
                {
                    if (cboBLNo.SelectedIndex >= 0)
                    {
                        parm.blNo = cboBLNo.SelectedItem.ToString();
                    }
                }
                else if (m_type == HCM107.TYPE_HAC_LOADING)
                {
                    parm.shipgNoteNo = txtSNBL.Text;
                }
                else if (m_type == HCM107.TYPE_HAC_DISCHARGING)
                {
                    parm.blNo = txtSNBL.Text;
                }
                if (!String.IsNullOrEmpty(txtLorryNo.Text))
                {
                    parm.lorryNo = txtLorryNo.Text;
                }

                ResponseInfo info = proxy.getAssignmentLorrysItems(parm);

                grdLorryList.Clear();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is AssignmentLorrysItem)
                    {
                        AssignmentLorrysItem item = (AssignmentLorrysItem)info.list[i];
                        DataRow newRow = grdLorryList.NewRow();

                        newRow[HEADER_LORRY] = item.lorryNo;
                        if (this.m_type != HCM107.TYPE_HAC_DISCHARGING)
                        {
                            newRow[HEADER_CONTRACTOR] = item.cdNm;
                            newRow[HEADER_SNBL] = item.snBlNo;
                            newRow[HEADER_DRIVER] = item.driverNm;
                        }
                        grdLorryList.Add(newRow);
                    }
                }

                if (info.list != null)
                {
                    m_rowsCnt = info.list.Length;
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

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            MOST.Common.CommonParm.LorryListParm vsParm = (MOST.Common.CommonParm.LorryListParm)parm;
            txtJPVC.Text = vsParm.Jpvc;
            txtLorryNo.Text = vsParm.LorryNo;
            if (m_type == HCM107.TYPE_HAC_LOADING)
            {
                txtSNBL.Text = vsParm.SnNo;
            }
            else if (m_type == HCM107.TYPE_HAC_DISCHARGING)
            {
                txtSNBL.Text = vsParm.BlNo;
            }
            
            // In case type is Post Safety
            if (m_type == HCM107.TYPE_HPS && !string.IsNullOrEmpty(txtJPVC.Text))
            {
                InitializeCboSNBL(txtJPVC.Text);
                CommonUtility.SetComboboxSelectedItem(this.cboBLNo, vsParm.BlNo);
            }

            F_Search();
            this.ShowDialog();

            return m_result;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.validations(this.Controls))
            {
                F_Search();

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
            int currRowIndex = grdLorryList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnLorryInfo();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void grdLorryList_DoubleClick(object sender, EventArgs e)
        {
            int currRowIndex = grdLorryList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnLorryInfo();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void ReturnLorryInfo()
        {
            int currRowIndex = grdLorryList.CurrentRowIndex;

            string strLorry = grdLorryList.DataTable.Rows[currRowIndex][HEADER_LORRY].ToString();
            string strContractor = string.Empty;
            string strSnbl = string.Empty;
            string strDriver = string.Empty; 
            if (this.m_type != HCM107.TYPE_HAC_DISCHARGING)
            {
                strContractor = grdLorryList.DataTable.Rows[currRowIndex][HEADER_CONTRACTOR].ToString();
                strSnbl = grdLorryList.DataTable.Rows[currRowIndex][HEADER_SNBL].ToString();
                strDriver = grdLorryList.DataTable.Rows[currRowIndex][HEADER_DRIVER].ToString();
            }           
            
            m_result = new LorryListResult();
            m_result.LorryNo = strLorry;
            if (this.m_type == HCM107.TYPE_HAC_DISCHARGING)
            {
                return;
            }
            m_result.Contractor = strContractor;
            m_result.SnblNo = strSnbl;            
            m_result.Driver = strDriver;
            m_result.JpvcInfo = m_jpvcResult;
        }

        private void btnF1_Click(object sender, EventArgs e)
        {
            MOST.Common.CommonParm.SearchJPVCParm jpvcParm = new MOST.Common.CommonParm.SearchJPVCParm();
            //jpvcParm.Jpvc = txtJPVC.Text;

            MOST.Common.CommonResult.SearchJPVCResult jpvcResult = (SearchJPVCResult)PopupManager.instance.ShowPopup(new MOST.Common.HCM101(), jpvcParm);
            if (jpvcResult != null)
            {
                txtJPVC.Text = jpvcResult.Jpvc;
                m_jpvcResult = jpvcResult;
                InitializeCboSNBL(jpvcResult.Jpvc);
            }
        }

        private void txtJPVC_LostFocus(object sender, EventArgs e)
        {
            // In case type is Post Safety
            if (m_type == HCM107.TYPE_HPS)
            {
                cboBLNo.SelectedIndex = -1;
                cboBLNo.Items.Clear();
                cboBLNo.Refresh();
                if (!String.IsNullOrEmpty(txtJPVC.Text))
                {
                    InitializeCboSNBL(txtJPVC.Text);
                }
            }
        }
    }
}