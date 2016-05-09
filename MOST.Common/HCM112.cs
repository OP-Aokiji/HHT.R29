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
    public partial class HCM112 : TDialog, IPopupWindow
    {
        #region Local Variable
        private EquipmentCodeResult m_result;
        private int m_rowsCnt;
        private readonly String HEADER_EQU_NM = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM112_0003");
        private readonly String HEADER_CAPA_DESCR = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM112_0004");
        private const string HEADER_EQU_CD = "_ROLE_CD";
        private const string HEADER_CAPA_CD = "_CAPA_CD";
        #endregion

        public HCM112()
        {
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            MOST.Common.CommonParm.EquipmentCodeParm vsParm = (MOST.Common.CommonParm.EquipmentCodeParm)parm;
            InitializeEquType(vsParm.EqIncludedList);
            this.ShowDialog();
            return m_result;
        }

        private void InitializeDataGrid()
        {   
            btnOk.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0013");
            btnCancel.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0014");
            this.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM112_0001");
            lblRole.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM112_0002");
            cboRole.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM112_0002");

            String[,] header = { { HEADER_EQU_NM, "70" }, { HEADER_EQU_CD, "0" }, { HEADER_CAPA_DESCR, "200" }, { HEADER_CAPA_CD, "0" } };
            grdData.setHeader(header);
        }

        private void InitializeEquType(string eqIncludedList)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ICommonProxy commonProxy = new CommonProxy();
                Framework.Service.Provider.WebService.Provider.EquipmentCodeParm commonParm = new Framework.Service.Provider.WebService.Provider.EquipmentCodeParm();
                commonParm.searchType = "mechanicalInitial";
                ResponseInfo commonInfo = commonProxy.getEquipmentCodeList(commonParm);
                CommonUtility.InitializeCombobox(cboRole, "Select");
                for (int i = 0; i < commonInfo.list.Length; i++)
                {
                    if (commonInfo.list[i] is CodeMasterListItem)
                        commonInfo.list[i] = CommonUtility.ToCodeMasterListItem1(commonInfo.list[i] as CodeMasterListItem);
                    if (commonInfo.list[i] is CodeMasterListItem1)
                    {
                        CodeMasterListItem1 item = (CodeMasterListItem1)commonInfo.list[i];

                        // Exclude Equipment Type <Forklift, Bulk Gantry, ...>
                        // --> Eliminate="FL,BG,LL,TX,CU,RC,PC,CP,MC,RL,SR,FC,GS,WP"
                        //string strEliminate = "FL,BG,LL,TX,CU,RC,PC,CP,MC,RL,SR,FC,GS,WP";
                        //if (strEliminate.IndexOf(item.scd) < 0)
                        //{
                        //    cboRole.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                        //}
                        if (!string.IsNullOrEmpty(eqIncludedList))
                        {
                            if (eqIncludedList.IndexOf(item.scd) >= 0)
                            {
                                cboRole.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                            }
                        }
                        else
                        {
                            cboRole.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                        }
                        
                    }
                }

                if (cboRole != null && cboRole.Items.Count == 2)
                {
                    cboRole.SelectedIndex = 1;
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
                Framework.Service.Provider.WebService.Provider.EquipmentCodeParm parm = new Framework.Service.Provider.WebService.Provider.EquipmentCodeParm();
                parm.searchType = "mechnicalCondition";
                parm.eqDivCdType = CommonUtility.GetComboboxSelectedValue(cboRole);
                parm.eqDivCd = CommonUtility.GetComboboxSelectedValue(cboRole);

                ResponseInfo info = proxy.getEquipmentCodeList(parm);

                grdData.Clear();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is EquipmentCodeItem)
                    {
                        EquipmentCodeItem item = (EquipmentCodeItem)info.list[i];
                        DataRow newRow = grdData.NewRow();
                        newRow[HEADER_EQU_NM] = item.eqDivCdNm;
                        newRow[HEADER_EQU_CD] = item.eqDivCd;
                        newRow[HEADER_CAPA_DESCR] = item.capaDescr;
                        newRow[HEADER_CAPA_CD] = item.capaCd;
                        grdData.Add(newRow);
                    }
                }
                grdData.Refresh();

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
            int currRowIndex = grdData.CurrentRowIndex;
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

        private void grdContractorList_DoubleClick(object sender, EventArgs e)
        {
            int currRowIndex = grdData.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnInfo();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void ReturnInfo()
        {
            string strEquNm = grdData.DataTable.Rows[grdData.CurrentRowIndex][HEADER_EQU_NM].ToString();
            string strEquCd = grdData.DataTable.Rows[grdData.CurrentRowIndex][HEADER_EQU_CD].ToString();
            string strCapaDescr = grdData.DataTable.Rows[grdData.CurrentRowIndex][HEADER_CAPA_DESCR].ToString();
            string strCapaCd = grdData.DataTable.Rows[grdData.CurrentRowIndex][HEADER_CAPA_CD].ToString();

            m_result = new EquipmentCodeResult();
            m_result.EquNm = strEquNm;
            m_result.EquCd = strEquCd;
            m_result.CapaDescr = strCapaDescr;
            m_result.CapaCd = strCapaCd;
        }

        private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.validations(this.Controls) && cboRole.SelectedIndex > 0)
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