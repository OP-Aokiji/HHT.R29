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
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.Common.Utility;

namespace MOST.Common
{
    public partial class HCM106 : TDialog, IPopupWindow
    {
        #region Local Variable
        private ContractorListResult m_result;
        private ContractorListParm m_parm;
        private int m_rowsCnt;
        private readonly String HEADER_ROLE = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM106_0003");
        private readonly String HEADER_EMPID = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM106_0004");
        private readonly String HEADER_NAME = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM106_0005");
        #endregion

        public HCM106()
        {
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            InitializeRole();
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            MOST.Common.CommonParm.ContractorListParm vsParm = (MOST.Common.CommonParm.ContractorListParm)parm;
            m_parm = vsParm;
            CommonUtility.SetComboboxSelectedItem(cboRole, vsParm.RoleCd);
            if (cboRole.SelectedIndex > 0)
            {
                F_Search();
            }
            this.ShowDialog();
            return m_result;
        }

        private void InitializeDataGrid()
        {   
            btnOk.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0013");
            btnCancel.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0014");
            this.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM106_0001");
            lblRole.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM106_0002");
            cboRole.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM106_0002");

            String[,] header = { { HEADER_ROLE, "30" }, { HEADER_EMPID, "70" }, { HEADER_NAME, "200" } };
            grdContractorList.setHeader(header);
        }

        private void InitializeRole()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ICommonProxy proxy = new CommonProxy();
                CheckListVSRParm parm = new CheckListVSRParm();
                parm.searchType = "roleCd";
                ResponseInfo info = proxy.getVSRList(parm);
                CommonUtility.InitializeCombobox(cboRole);
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CodeMasterListItem)
                    {
                        CodeMasterListItem item = (CodeMasterListItem)info.list[i];
                        cboRole.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                    else if (info.list[i] is CodeMasterListItem1)
                    {
                        CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                        cboRole.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
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

        private void F_Search()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ICommonProxy proxy = new CommonProxy();
                CheckListVSRParm parm = new CheckListVSRParm();
                parm.workYmd = m_parm.ShftDt;
                parm.shftId = m_parm.ShftId;
                parm.roleCd = CommonUtility.GetComboboxSelectedValue(cboRole);
                parm.searchType = "EmpId";
                ResponseInfo info = proxy.getVSRList(parm);

                grdContractorList.Clear();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CheckListVSRItem)
                    {
                        CheckListVSRItem item = (CheckListVSRItem)info.list[i];
                        DataRow newRow = grdContractorList.NewRow();
                        newRow[HEADER_ROLE] = item.roleCd;
                        newRow[HEADER_EMPID] = item.empId;
                        newRow[HEADER_NAME] = item.empNm;
                        grdContractorList.Add(newRow);
                    }
                }
                grdContractorList.Refresh();

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

        private void btnOk_Click(object sender, EventArgs e)
        {
            int currRowIndex = grdContractorList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnContractList();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void grdContractorList_DoubleClick(object sender, EventArgs e)
        {
            int currRowIndex = grdContractorList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnContractList();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void ReturnContractList()
        {
            int currRowIndex = grdContractorList.CurrentRowIndex;
            //Role: display value (in grid): "CO,FD,OS"; return value: only one role (get from Role combobox).
            string strRoleCd = CommonUtility.GetComboboxSelectedValue(cboRole);
            string strEmpId = grdContractorList.DataTable.Rows[currRowIndex][HEADER_EMPID].ToString();
            string strEmpNm = grdContractorList.DataTable.Rows[currRowIndex][HEADER_NAME].ToString();

            m_result = new ContractorListResult();
            m_result.RoleCd = strRoleCd;
            m_result.EmpId = strEmpId;
            m_result.EmpNm = strEmpNm;
        }

        private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.validations(this.Controls) && m_parm != null)
                {
                    F_Search();

                    if (m_rowsCnt <= 0)
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0058"));
                        cboRole.Focus();
                    }
                }
            }
            catch (Framework.Common.Exception.BusinessException ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}