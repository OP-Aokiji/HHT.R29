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
using Framework.Controls.UserControls;
using Framework.Common.ExceptionHandler;
using MOST.Common.Utility;

namespace MOST.Common
{
    public partial class HCM109 : TDialog, IPopupWindow
    {
        #region Local Variable
        public const int TYPE_TRIMMING = 1;    // Trimming POPUP
        public const int TYPE_STEVEDORE = 2;    // Stevedore POPUP
        private readonly String HEADER_CODE = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM110_0001");
        private readonly String HEADER_NAME = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM110_0002");
        private int m_type;
        private int m_rowsCnt;
        MOST.Common.CommonParm.MegaStvTrmParm m_parm;
        private MegaStvTrmResult m_result;
        #endregion

        public HCM109(int type)
        {
            this.m_type = type;
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            InitializeOthers();
        }

        private void InitializeDataGrid()
        {
            btnOk.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0013");
            btnCancel.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0014");
            btnSearch.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0015");

            String[,] header = { { HEADER_CODE, "70" }, { HEADER_NAME, "150" } };
            grdList.setHeader(header);
        }

        private void InitializeOthers()
        {
            // Set title
            switch (m_type)
            {
                case HCM109.TYPE_TRIMMING:
                    this.Text = "Trimming";
                    break;

                case HCM109.TYPE_STEVEDORE:
                    this.Text = "Stevedore";
                    break;
            }

            // Options: Code/Name. Default option is Code.
            cboOption.Items.Clear();
            cboOption.Items.Add(new ComboboxValueDescriptionPair("CD", "Code"));
            cboOption.Items.Add(new ComboboxValueDescriptionPair("NM", "Name"));
            cboOption.SelectedIndex = 0;
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (MOST.Common.CommonParm.MegaStvTrmParm)parm;
            txtSearch.Text = m_parm.SearchItem;
            if (!string.IsNullOrEmpty(m_parm.Option))
            {
                CommonUtility.SetComboboxSelectedItem(cboOption, m_parm.Option);
            }
            F_Search();
            this.ShowDialog();

            return m_result;
        }

        private void F_Search()
        {
            try
            {
                #region Request Webservice

                Cursor.Current = Cursors.WaitCursor;

                ICommonProxy proxy = new CommonProxy();

                MegaParm megaParm = new MegaParm();
                megaParm.vslCallId = m_parm.VslCallId;
                megaParm.shftId = m_parm.ShftId;
                megaParm.workYmd = m_parm.WorkYmd;
                if (m_type == HCM109.TYPE_STEVEDORE)
                {
                    megaParm.ptnrType = "STV";
                }
                else if (m_type == HCM109.TYPE_TRIMMING)
                {
                    megaParm.ptnrType = "TRM";
                }
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    string option = CommonUtility.GetComboboxSelectedValue(cboOption);
                    if ("CD".Equals(option))
                    {
                        megaParm.megaCompCd = txtSearch.Text;
                    }
                    else if ("NM".Equals(option))
                    {
                        megaParm.megaCompNm = txtSearch.Text;
                    }
                }
                ResponseInfo info = proxy.getMegaCompList(megaParm);
                #endregion

                #region Display Data
                grdList.Clear();
                if (info != null)
                {
                    if (m_type == HCM109.TYPE_STEVEDORE || m_type == HCM109.TYPE_TRIMMING)
                    {
                        for (int i = 0; i < info.list.Length; i++)
                        {
                            if (info.list[i] is MegaItem)
                            {
                                MegaItem item = (MegaItem)info.list[i];
                                DataRow newRow = grdList.NewRow();
                                newRow[HEADER_CODE] = item.megaCompCd;
                                newRow[HEADER_NAME] = item.megaCompNm;
                                grdList.Add(newRow);
                            }
                        }
                    }
                    grdList.Refresh();

                    if (info.list != null)
                    {
                        m_rowsCnt = info.list.Length;
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

        private void ReturnInfo()
        {
            int currRowIndex = grdList.CurrentRowIndex;
            m_result = new MegaStvTrmResult();
            m_result.Code = grdList.DataTable.Rows[currRowIndex][HEADER_CODE].ToString();
            m_result.Name = grdList.DataTable.Rows[currRowIndex][HEADER_NAME].ToString();
        }

        #region Event Handler
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.validations(this.Controls))
            {
                F_Search();

                if (m_rowsCnt <= 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0058"));
                    txtSearch.Focus();
                    txtSearch.SelectAll();
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int currRowIndex = grdList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnInfo();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void grdList_DoubleClick(object sender, EventArgs e)
        {
            int currRowIndex = grdList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnInfo();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        #endregion
    }
}