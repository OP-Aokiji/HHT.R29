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
using Framework.Common.UserInformation;
using MOST.Common.Utility;

namespace MOST.Common
{
    public partial class HCM101 : TDialog, IPopupWindow
    {
        #region Local Variable
        private int m_rowsCnt;
        private SearchJPVCResult m_result;
        private readonly String HEADER_JPVC = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM101_0001");
        private readonly String HEADER_VESSEL_NM = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM101_0002");
        private readonly String HEADER_ETB = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM101_0003");
        private readonly String HEADER_BERTH_LOC = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM101_0004");
        private readonly String HEADER_LOA = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM101_0005");
        private readonly String HEADER_ATB = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM101_0006");
        private readonly String HEADER_ETW = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM101_0007");
        private readonly String HEADER_WHARF_START = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM101_0008");
        private readonly String HEADER_WHARF_END = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM101_0009");
        private readonly String HEADER_ATW = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM101_0010");
        private readonly String HEADER_ETC = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM101_0011");
        private readonly String HEADER_ATC = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM101_0012");
        private readonly String HEADER_ETU = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM101_0013");
        private readonly String HEADER_ATU = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM101_0014");
        private readonly String HEADER_VSLTP = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM101_0016");
        private readonly String HEADER_VSLTPNM = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM101_0017");
        private readonly String HEADER_PURPOFCALL = "Purp. of call";
        private readonly String HEADER_PURPOFCALLCD = "_PURPOFCALLCD";
        private readonly String HEADER_CURATB = "Cur. ATB";
        private String isWHChecker = "N";
        #endregion

        public HCM101()
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
            lbJPVC.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM101_0015");
            txtVslCdNm.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM101_0015");

            String[,] header = { {HEADER_JPVC, "105"}, 
                                 {HEADER_VESSEL_NM, "120"},
                                 {HEADER_ETB, "120"},
                                 {HEADER_BERTH_LOC, "0"},
                                 {HEADER_LOA, "0"},
                                 {HEADER_ATB, "0"},
                                 {HEADER_ETW, "0"},
                                 {HEADER_WHARF_START, "0"},
                                 {HEADER_WHARF_END, "0"},
                                 {HEADER_ATW, "0"},
                                 {HEADER_ETC, "0"},
                                 {HEADER_ATC, "0"},
                                 {HEADER_ETU, "0"},
                                 {HEADER_ATU, "0"},
                                {HEADER_VSLTP, "0"},
                                {HEADER_VSLTPNM, "0"},
                                {HEADER_PURPOFCALL, "0"},
                                {HEADER_PURPOFCALLCD, "0"},
                                {HEADER_CURATB, "0"}};
            grdJPVCList.setHeader(header);
        }

        private void F_Search()
        {
            try
            {
                #region Request Webservice

                Cursor.Current = Cursors.WaitCursor;

                ICommonProxy proxy = new CommonProxy();
                SearchJPVCParm parm = new SearchJPVCParm();
                parm.vslCallId = txtVslCdNm.Text;

                // Vessel should display vessel at berth and planned to come/berth only. 
                // But in W/H Checker case, It will display vessel already departure (ATD is not null)
                if (isWHChecker == "Y")
                {
                    parm.hhtYn = "N";
                }
                else if (isWHChecker == "VSL")
                {
                    parm.hhtYn = "VSL";
                }
                else
                {
                    parm.hhtYn = "Y";
                }
                parm.etaStart = UserInfo.getInstance().Workdate;
                
                /**
                 * add working date 
                 */

                parm.workDt = UserInfo.getInstance().Workdate;

                ResponseInfo info = proxy.getSearchJPVCList(parm);

                #endregion

                #region Display Data

                grdJPVCList.Clear();

                foreach (SearchJPVCItem item in info.list)
                {
                    DataRow newRow = grdJPVCList.NewRow();
                    newRow[HEADER_JPVC] = item.vslCallId;
                    newRow[HEADER_VESSEL_NM] = item.vslNm;
                    newRow[HEADER_BERTH_LOC] = item.berthLoc;
                    newRow[HEADER_LOA] = item.loa;
                    newRow[HEADER_WHARF_START] = item.wharfStart;
                    newRow[HEADER_WHARF_END] = item.wharfEnd;
                    newRow[HEADER_ETB] = item.etb;
                    newRow[HEADER_ATB] = item.atb;
                    newRow[HEADER_ETW] = item.etw;
                    newRow[HEADER_ATW] = item.atw;
                    newRow[HEADER_ETC] = item.etc;
                    newRow[HEADER_ATC] = item.atc;
                    newRow[HEADER_ETU] = item.etu;
                    newRow[HEADER_ATU] = item.atu;
                    newRow[HEADER_VSLTP] = item.vslTp;
                    newRow[HEADER_VSLTPNM] = item.vslTpNm;
                    newRow[HEADER_PURPOFCALL] = item.purpCall;
                    newRow[HEADER_PURPOFCALLCD] = item.purpCallCd;
                    newRow[HEADER_CURATB] = item.curAtb;

                    grdJPVCList.Add(newRow);
                }
                grdJPVCList.Refresh();

                if (info.list != null)
                {
                    m_rowsCnt = info.list.Length;
                }
                #endregion
            }
            catch (Framework.Common.Exception.BusinessException ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            } catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }

            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void ReturnJPVCInfo()
        {
            int currRowIndex = grdJPVCList.CurrentRowIndex;
            string strJPVC = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_JPVC].ToString();
            string strVesselName = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_VESSEL_NM].ToString();
            string strETB = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_ETB].ToString();
            string strBerthLocation = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_BERTH_LOC].ToString();
            string strLOA = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_LOA].ToString();
            string strATB = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_ATB].ToString();
            string strETW = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_ETW].ToString();
            string strWharfStart = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_WHARF_START].ToString();
            string strWharfEnd = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_WHARF_END].ToString();
            string strATW = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_ATW].ToString();
            string strETC = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_ETC].ToString();
            string strATC = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_ATC].ToString();
            string strETU = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_ETU].ToString();
            string strATU = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_ATU].ToString();
            string strVslTp = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_VSLTP].ToString();
            string strVslTpNm = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_VSLTPNM].ToString();
            string strPurpOfCall = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_PURPOFCALL].ToString();
            string strPurpOfCallCd = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_PURPOFCALLCD].ToString();
            string strCurAtb = grdJPVCList.DataTable.Rows[currRowIndex][HEADER_CURATB].ToString();
            
            m_result = new SearchJPVCResult();
            m_result.Jpvc = strJPVC;
            m_result.VesselName = strVesselName;
            m_result.BerthLocation = strBerthLocation;
            m_result.WharfStart = strWharfStart;
            m_result.WharfEnd = strWharfEnd;
            m_result.Loa = strLOA;
            m_result.VslTp = strVslTp;
            m_result.VslTpNm = strVslTpNm;
            m_result.Etb = strETB;
            m_result.Etw = strETW;
            m_result.Etc = strETC;
            m_result.Etu = strETU;
            m_result.Atb = strATB;
            m_result.Atw = strATW;
            m_result.Atc = strATC;
            m_result.Atu = strATU;
            m_result.PurpCall = strPurpOfCall;
            m_result.PurpCallCd = strPurpOfCallCd;
            m_result.CurAtb = strCurAtb;
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            MOST.Common.CommonParm.SearchJPVCParm vsParm = (MOST.Common.CommonParm.SearchJPVCParm)parm;
            txtVslCdNm.Text = vsParm.Jpvc;
            isWHChecker = vsParm.IsWHChecker;
            if (!string.IsNullOrEmpty(txtVslCdNm.Text) && txtVslCdNm.TextLength > 3)
            {
                F_Search();
            }
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
                    txtVslCdNm.Focus();
                    txtVslCdNm.SelectAll();
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int currRowIndex = grdJPVCList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt) {
                ReturnJPVCInfo();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void grdJPVCList_DoubleClick(object sender, EventArgs e)
        {
            int currRowIndex = grdJPVCList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnJPVCInfo();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        #endregion
    }
}