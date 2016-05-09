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
using MOST.Common.CommonParm;
using MOST.Common.Utility;
using Framework.Controls.UserControls;
using Framework.Common.ExceptionHandler;

namespace MOST.Common
{
    public partial class HCM114 : TDialog, IPopupWindow
    {
        #region Local Variable
        private GRBLListParm m_parm;
        private GRBLListResult m_result;
        private int m_rowsCnt;
        private readonly String HEADER_VESSEL_ID = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM114_0003");
        private readonly String HEADER_CG_NO = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM114_0004");
        private readonly String HEADER_STATUS = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM114_0008");
        private readonly String HEADER_MT = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM114_0005");
        private readonly String HEADER_M3 = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM114_0006");
        private readonly String HEADER_QTY = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM114_0007");
        private const string HEADER_CGTPCD      = "_CGTPCD";
        #endregion

        public HCM114()
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

            this.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM114_0001");
            lbGP.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM114_0002");
            txtSearchItem.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM114_0002");
            String[,] header = { {HEADER_VESSEL_ID, "95"},
                             {HEADER_CG_NO, "80"},
                            {HEADER_STATUS, "70"},
                             {HEADER_CGTPCD, "0"},
                            {HEADER_MT, "70"},
                            {HEADER_M3, "70"},
                            {HEADER_QTY, "70"},};
            grdDataList.setHeader(header);
        }

        private void F_Search()
        {
            try
            {
                #region Request Webservice
                Cursor.Current = Cursors.WaitCursor;
                ICommonProxy proxy = new CommonProxy();

                // GR
                Framework.Service.Provider.WebService.Provider.CargoExportParm grParm = new Framework.Service.Provider.WebService.Provider.CargoExportParm();
                if (m_parm != null && !string.IsNullOrEmpty(m_parm.VslCallId))
                {
                    grParm.vslCallId = m_parm.VslCallId;
                }
                grParm.cgNo = txtSearchItem.Text;
                ResponseInfo grInfo = proxy.getCargoExportList(grParm);

                // BL
                Framework.Service.Provider.WebService.Provider.CargoImportParm blParm = new Framework.Service.Provider.WebService.Provider.CargoImportParm();
                if (m_parm != null && !string.IsNullOrEmpty(m_parm.VslCallId))
                {
                    blParm.vslCallId = m_parm.VslCallId;
                }
                blParm.blNo = txtSearchItem.Text;
                ResponseInfo blInfo = proxy.getCargoImportList(blParm);
                #endregion

                #region Display Data
                grdDataList.Clear();
                DataRow newRow;
                for (int i = 0; i < grInfo.list.Length; i++)
                {
                    if (grInfo.list[i] is CargoExportItem)
                    {
                        CargoExportItem item = (CargoExportItem)grInfo.list[i];
                        newRow = grdDataList.NewRow();
                        newRow[HEADER_VESSEL_ID] = item.vslCallId;
                        newRow[HEADER_CG_NO] = item.grNo;
                        newRow[HEADER_STATUS] = item.statNm;
                        newRow[HEADER_QTY] = item.docQty;
                        newRow[HEADER_MT] = item.docMt;
                        newRow[HEADER_M3] = item.docM3;
                        newRow[HEADER_CGTPCD] = item.cgTpCd;
                        grdDataList.Add(newRow);
                    }
                }

                for (int i = 0; i < blInfo.list.Length; i++)
                {
                    if (blInfo.list[i] is CargoImportItem)
                    {
                        CargoImportItem item = (CargoImportItem)blInfo.list[i];
                        newRow = grdDataList.NewRow();
                        newRow[HEADER_VESSEL_ID] = item.vslCallId;
                        newRow[HEADER_CG_NO] = item.blNo;
                        newRow[HEADER_STATUS] = item.statNm;
                        newRow[HEADER_MT] = item.docMt;
                        newRow[HEADER_M3] = item.docM3;
                        newRow[HEADER_QTY] = item.docQty;
                        newRow[HEADER_CGTPCD] = item.cgTpCd;
                        grdDataList.Add(newRow);
                    }
                }
                grdDataList.Refresh();
                #endregion

                if (grInfo.list != null)
                {
                    m_rowsCnt = grInfo.list.Length;
                }
                if (blInfo.list != null)
                {
                    m_rowsCnt = m_rowsCnt + blInfo.list.Length;
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

        private void ReturnInfo()
        {
            int currRowIndex = grdDataList.CurrentRowIndex;
            m_result = new GRBLListResult();
            m_result.VslCallId = grdDataList.DataTable.Rows[currRowIndex][HEADER_VESSEL_ID].ToString();
            m_result.CgNo = grdDataList.DataTable.Rows[currRowIndex][HEADER_CG_NO].ToString();
            m_result.CgTpCd = grdDataList.DataTable.Rows[currRowIndex][HEADER_CGTPCD].ToString();
            m_result.Mt = grdDataList.DataTable.Rows[currRowIndex][HEADER_MT].ToString();
            m_result.M3 = grdDataList.DataTable.Rows[currRowIndex][HEADER_M3].ToString();
            m_result.Qty = grdDataList.DataTable.Rows[currRowIndex][HEADER_QTY].ToString();
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (GRBLListParm)parm;
            txtSearchItem.Text = m_parm != null ? m_parm.CgNo : string.Empty;
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
                    txtSearchItem.Focus();
                    txtSearchItem.SelectAll();
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int currRowIndex = grdDataList.CurrentRowIndex;
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

        private void grdGPList_DoubleClick(object sender, EventArgs e)
        {
            int currRowIndex = grdDataList.CurrentRowIndex;
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