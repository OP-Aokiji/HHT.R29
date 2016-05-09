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
    public partial class HCM102 : TDialog, IPopupWindow
    {
        #region Local Variable
        public const int TYPE_LIQUID    = 1;        // Delay Code for Liquid Bulk
        public const int TYPE_BULK      = 2;        // Delay Code for Break/Dry Bulk
        private int m_type;
        private int m_rowsCnt;
        private DelayCodeListResult m_result;
        private readonly String HEADER_CODE = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM102_0003");
        private readonly String HEADER_DESCR = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM102_0004");
        private readonly String HEADER_CHARGEABLE = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM102_0005");

        #endregion

        public HCM102(int type)
        {
            m_type = type;
            InitializeComponent();
            InitializeDataGrid();
            this.initialFormSize();
        }

        private void InitializeDataGrid()
        {
            btnOk.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0013");
            btnCancel.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0014");
            btnSearch.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0015");
            this.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM102_0001");
            lblDelayCode.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM102_0002");
            txtDelayCode.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM102_0002");

            String[,] header = { { HEADER_CODE, "40" }, { HEADER_DESCR, "150" }, { HEADER_CHARGEABLE, "30" } };
            grdDelayCodeList.setHeader(header);

            lstSearch.Items.Add("");
            int maxIndex = 0;
            if (m_type == HCM102.TYPE_BULK)
            {
                maxIndex = 84;
            }
            else if (m_type == HCM102.TYPE_LIQUID)
            {
                maxIndex = 91;
            }
            for (int i = 65; i < maxIndex; i++)
            {
                lstSearch.Items.Add(Convert.ToChar(i));
            }
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            MOST.Common.CommonParm.DelayCodeListParm vsParm = (MOST.Common.CommonParm.DelayCodeListParm)parm;
            txtDelayCode.Text = vsParm.DelayCode;

            this.ShowDialog();

            return m_result;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            F_Search(false);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int currRowIndex = grdDelayCodeList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnDelayCode();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void grdDelayCodeList_DoubleClick(object sender, EventArgs e)
        {
            int currRowIndex = grdDelayCodeList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnDelayCode();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void F_Search(bool isCategoryOnly)
        {
            try
            {
                if (this.validations(this.Controls))
                {
                    Cursor.Current = Cursors.WaitCursor;

                    ICommonProxy proxy = new CommonProxy();
                    CommonCodeParm commonParm = new CommonCodeParm();
                    commonParm.searchType = "DLYCD";
                    commonParm.lcd = m_type == HCM102.TYPE_LIQUID ? "LIQUIQ" : "BULK";
                    if (isCategoryOnly)
                    {
                        if (lstSearch.SelectedIndex > 0)
                        {
                            commonParm.col1 = lstSearch.SelectedItem.ToString();
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txtDelayCode.Text))
                        {
                            commonParm.tyCd = "CD";
                            commonParm.cd = txtDelayCode.Text;
                        }
                    }

                    ResponseInfo info = proxy.getCommonCodeList(commonParm);
                    
                    grdDelayCodeList.Clear();
                    // Fix issue 0032175
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is CodeMasterListItem)
                            info.list[i] = CommonUtility.ToCodeMasterListItem1((CodeMasterListItem)info.list[i]);

                        CodeMasterListItem1 item = info.list[i] as CodeMasterListItem1;
                        if (item != null)
                        {
                            DataRow newRow = grdDelayCodeList.NewRow();
                            newRow[HEADER_CODE] = item.scd;
                            newRow[HEADER_DESCR] = item.scdNm;
                            newRow[HEADER_CHARGEABLE] = item.acptYN;
                            grdDelayCodeList.Add(newRow);
                        }
                    }
                    grdDelayCodeList.Refresh();

                    if (info.list != null)
                    {
                        m_rowsCnt = info.list.Length;
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

        private void ReturnDelayCode()
        {
            int currRowIndex = grdDelayCodeList.CurrentRowIndex;
            string strCode = grdDelayCodeList.DataTable.Rows[currRowIndex][HEADER_CODE].ToString();
            string strDescription = grdDelayCodeList.DataTable.Rows[currRowIndex][HEADER_DESCR].ToString();
            string strChargeable = grdDelayCodeList.DataTable.Rows[currRowIndex][HEADER_CHARGEABLE].ToString();

            m_result = new DelayCodeListResult();
            m_result.Code = strCode;
            m_result.Description = strDescription;
            m_result.AcceptedDelay = strChargeable;
        }

        private void lstSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            F_Search(true);

            if (m_rowsCnt <= 0)
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0058"));
                txtDelayCode.Focus();
                txtDelayCode.SelectAll();
            }
        }

        private void HCM102_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                MessageBox.Show("Screen ID : " + this.Name + "\n\rScreen caption: " + this.Text, "Screen Information !");
            }
        }
    }
}