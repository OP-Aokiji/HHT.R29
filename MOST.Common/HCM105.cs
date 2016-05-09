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
using MOST.Common.CommonResult;
using MOST.Common.Utility;
using Framework.Controls.UserControls;
using MOST.Client.Proxy.CommonProxy;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.Common
{
    public partial class HCM105 : TDialog, IPopupWindow
    {
        #region Local Variable
        private DriverListResult m_result;
        private int m_rowsCnt;
        private readonly String HEADER_DRIVER_ID = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM105_0003");
        private readonly String HEADER_DRIVER_NM = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM105_0004");
        #endregion

        public HCM105()
        {
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
        }

        private void InitializeDataGrid()
        {
            btnOk.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0013");
            btnCancel.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0014");
            btnSearch.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0015");
            this.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM105_0001");
            lblStaffName.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM105_0002");
            txtStaffName.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM105_0002");

            String[,] header = { { HEADER_DRIVER_ID, "105" }, { HEADER_DRIVER_NM, "250" } };
            grdDriverList.setHeader(header);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            MOST.Common.CommonParm.DriverListParm blParm = (MOST.Common.CommonParm.DriverListParm)parm;
            txtStaffName.Text = blParm.EmpId;
            F_Search();
            this.ShowDialog();

            return m_result;
        }

        private void F_Search()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Request Webservice
                ICommonProxy proxy = new CommonProxy();
                Framework.Service.Provider.WebService.Provider.InternalStaffMngParm parm = new Framework.Service.Provider.WebService.Provider.InternalStaffMngParm();
                if (!String.IsNullOrEmpty(txtStaffName.Text))
                {
                    parm.userName = txtStaffName.Text;
                }
                parm.viewType = "staffcombo";

                ResponseInfo info = proxy.getInternalStaffMngList(parm);
                #endregion

                #region Display Data
                grdDriverList.Clear();

                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is InternalStaffMngItem)
                    {
                        InternalStaffMngItem item = (InternalStaffMngItem)info.list[i];
                        DataRow newRow = grdDriverList.NewRow();
                        newRow[HEADER_DRIVER_ID] = item.empId;
                        newRow[HEADER_DRIVER_NM] = item.engNm;
                        grdDriverList.Add(newRow);
                    }
                }
                grdDriverList.Refresh();

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

        private void ReturnDriverInfo()
        {
            int currRowIndex = grdDriverList.CurrentRowIndex;
            string strEmpId = grdDriverList.DataTable.Rows[currRowIndex][HEADER_DRIVER_ID].ToString();
            string strEngName = grdDriverList.DataTable.Rows[currRowIndex][HEADER_DRIVER_NM].ToString();

            m_result = new DriverListResult();
            m_result.EmpId = strEmpId;
            m_result.EmpName = strEngName;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int currRowIndex = grdDriverList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnDriverInfo();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void grdDriverList_DoubleClick(object sender, EventArgs e)
        {
            int currRowIndex = grdDriverList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnDriverInfo();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.validations(this.Controls))
            {
                F_Search();

                if (m_rowsCnt <= 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0058"));
                    txtStaffName.Focus();
                    txtStaffName.SelectAll();
                }
            }
        }

        private void HCM105_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                MessageBox.Show("Screen ID : " + this.Name + "\n\rScreen caption: " + this.Text, "Screen Information !");
            }
        }
    }
}