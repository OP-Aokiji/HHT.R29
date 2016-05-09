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
    public partial class HCM118 : TDialog, IPopupWindow
    {
        #region Local Variable
        private const string CONST_CD = "CD";   // Search by CD
        private const string CONST_NM = "NM";   // Search by Name

        private readonly String HEADER_CODE = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM110_0001");
        private readonly String HEADER_NAME = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM110_0002");
        private readonly String HEADER_TYPE = "Type";
        private readonly String HEADER_PHONE = "Phone";
        private readonly String HEADER_FAX = "Fax";
        private int m_rowsCnt;
        private RequesterListResult m_result;
        #endregion

        public HCM118()
        {
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            InitializeData();
        }

        private void InitializeDataGrid()
        {
            btnOk.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0013");
            btnCancel.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0014");
            btnSearch.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0015");

            String[,] header = { { HEADER_CODE, "70" }, { HEADER_NAME, "70" }, { HEADER_TYPE, "70" }, { HEADER_PHONE, "70" }, { HEADER_FAX, "120" } };
            grdList.setHeader(header);
        }

        private void InitializeData()
        {   
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // Options: Code/Name. Default option is Code.
                cboOption.Items.Clear();
                cboOption.Items.Add(new ComboboxValueDescriptionPair(CONST_CD, "Code"));
                cboOption.Items.Add(new ComboboxValueDescriptionPair(CONST_NM, "Name"));
                cboOption.SelectedIndex = 0;

                // Type {Forwarding, Shipping Agent, ...}
                ICommonProxy proxy = new CommonProxy();
                PartnerCodeParm partnerParm = new PartnerCodeParm();
                partnerParm.searchType = "HHT_PartnerCodeType";
                partnerParm.searchModule = "MT";
                partnerParm.ptyCd = txtSearch.Text.Trim();
                partnerParm.engPtyNm = txtSearch.Text.Trim();
                partnerParm.tyCd = CommonUtility.GetComboboxSelectedValue(cboOption);
                partnerParm.ptyDivCd = CommonUtility.GetComboboxSelectedValue(cboType);

                ResponseInfo info = proxy.getPartnerCodeTypeList(partnerParm);
                CommonUtility.InitializeCombobox(cboType);
                if (info != null)
                {
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is PartnerCodeItem)
                        {
                            PartnerCodeItem item = (PartnerCodeItem)info.list[i];
                            cboType.Items.Add(new ComboboxValueDescriptionPair(item.ptyCd, item.engPtyNm));
                        }
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
                #region Request Webservice
                Cursor.Current = Cursors.WaitCursor;

                ICommonProxy proxy = new CommonProxy();
                PartnerCodeParm partnerParm = new PartnerCodeParm();
                partnerParm.searchType = "HHT_PartnerCodeList";
                partnerParm.searchModule = "MT";
                partnerParm.ptyCd = txtSearch.Text.Trim();
                partnerParm.engPtyNm = txtSearch.Text.Trim();
                partnerParm.tyCd = CommonUtility.GetComboboxSelectedValue(cboOption);
                partnerParm.ptyDivCd = CommonUtility.GetComboboxSelectedValue(cboType);

                ResponseInfo info = proxy.getPartnerCodeTypeList(partnerParm);
                #endregion

                #region Display Data
                grdList.Clear();
                if (info != null)
                {   
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is PartnerCodeItem)
                        {
                            PartnerCodeItem item = (PartnerCodeItem)info.list[i];
                            DataRow newRow = grdList.NewRow();
                            newRow[HEADER_CODE] = item.ptyCd;
                            newRow[HEADER_NAME] = item.engPtyNm;
                            newRow[HEADER_TYPE] = item.ptyDivName;
                            newRow[HEADER_PHONE] = item.telNo;
                            newRow[HEADER_FAX] = item.faxNo;
                            grdList.Add(newRow);
                        }
                    }
                }

                if (info!= null && info.list != null)
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

        private void ReturnInfo()
        {
            int currRowIndex = grdList.CurrentRowIndex;
            m_result = new RequesterListResult();

            m_result.Code = grdList.DataTable.Rows[currRowIndex][HEADER_CODE].ToString();
            m_result.Name = grdList.DataTable.Rows[currRowIndex][HEADER_NAME].ToString();
            m_result.Type = grdList.DataTable.Rows[currRowIndex][HEADER_TYPE].ToString();
            m_result.TelNo = grdList.DataTable.Rows[currRowIndex][HEADER_PHONE].ToString();
            m_result.FaxNo = grdList.DataTable.Rows[currRowIndex][HEADER_FAX].ToString();
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            MOST.Common.CommonParm.RequesterListParm vsParm = (MOST.Common.CommonParm.RequesterListParm)parm;
            txtSearch.Text = vsParm.SearchItem;
            if (!string.IsNullOrEmpty(vsParm.Option))
            {
                CommonUtility.SetComboboxSelectedItem(cboOption, vsParm.Option);
            }
            F_Search();
            this.ShowDialog();

            return m_result;
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