using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Common.UserInformation;
using Framework.Controls.Container;
using MOST.Client.Proxy.CommonProxy;
using Framework.Service.Provider.WebService.Provider;
using System.Collections;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;
using MOST.Common.Utility;
using Framework.Controls.UserControls;
using Framework.Common.ExceptionHandler;

namespace MOST.Common
{
    public partial class HCM111 : TDialog, IPopupWindow
    {
        #region Local Variable
        public const int TYPE_GATEPASS  = 1;
        public const int TYPE_LORRY     = 2;
        private GatePassListResult m_result;
        private int m_rowsCnt;
        private int m_type;
        private readonly String HEADER_GATEPASS = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM120_0001");
        private readonly String HEADER_VESSEL_ID = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM120_0002");
        private readonly String HEADER_CG_NO = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM120_0003");
        private readonly String HEADER_LORRY_NO = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM120_0004");
        private const string HEADER_TSPTR = "Tsptr";
        private const string HEADER_WGT         = "WGT";
        private const string HEADER_MSRMT       = "MSRMT";
        private const string HEADER_PKGQTY      = "PKGQTY";
        private const string HEADER_CGINOUTCD   = "CGINOUTCD";
        private const string HEADER_CATGCD = "CATGCD";
        private const string HEADER_SEQ = "SEQ";
        private const string HEADER_CMDTCD = "CMDTCD";

        //added by William (2015/07/21 - HHT) Mantis issue 49799
        private const string HEADER_GATE_IN_TIME = "GATE IN TIME";

        private string shiftId;
        #endregion

        public HCM111(int type)
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
            
            if (m_type == HCM111.TYPE_GATEPASS)
            {
                this.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM120_0007");
                lbGP.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM120_0005");
                txtSearchItem.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM120_0005");
                String[,] header = { {HEADER_GATEPASS, "105"}, 
                                {HEADER_VESSEL_ID, "120"},
                                {HEADER_CG_NO, "105"},
                                {HEADER_LORRY_NO, "100"},
                                {HEADER_TSPTR, "50"},
                                //added by William (2015/07/21 - HHT) Mantis issue 49799
                                {HEADER_GATE_IN_TIME,"100"},
                                {HEADER_CGINOUTCD, "0"},
                                {HEADER_WGT, "0"},
                                {HEADER_MSRMT, "0"},
                                {HEADER_CMDTCD, "0"},
                                {HEADER_PKGQTY, "0"},
                                {HEADER_CATGCD, "0"},
                                {HEADER_SEQ, "0"}};
                grdDataList.setHeader(header);
            }
            else if (m_type == HCM111.TYPE_LORRY)
            {
                this.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM120_0008");
                lbGP.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM120_0006");
                txtSearchItem.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM120_0006");
                String[,] header = { {HEADER_LORRY_NO, "105"}, 
                                 {HEADER_VESSEL_ID, "120"},
                                 {HEADER_CG_NO, "105"},
                                 {HEADER_CGINOUTCD, "0"}};
                grdDataList.setHeader(header);
            }
            
        }

        private void F_Search()
        {
            try
            {
                #region Request Webservice

                Cursor.Current = Cursors.WaitCursor;

                ResponseInfo info = null;
                ICommonProxy proxy = new CommonProxy();
                CargoGatePassParm parm = new CargoGatePassParm();

                if (m_type == HCM111.TYPE_GATEPASS)
                {
                    parm.hhtFlag = "Y";     // To exlude GPs that were already gated-out.
                    parm.gatePassNo = txtSearchItem.Text;
                    parm.lorryNo = txtLorryNo.Text;
                    if (string.IsNullOrEmpty(txtLorryNo.Text) && string.IsNullOrEmpty(txtSearchItem.Text))
                    {
                        parm.startDt = txtStartDt.Value.ToString("dd/MM/yyyy");
                        parm.endDt = txtEndDt.Value.ToString("dd/MM/yyyy");
                    }
                    
                    parm.shftId = this.shiftId;
                    info = proxy.getCargoGatePassNo(parm);
                }
                else if (m_type == HCM111.TYPE_LORRY)
                {
                    parm.lorryNo = txtSearchItem.Text;
                    info = proxy.getCargoGateLorryNo(parm);
                }


                #endregion

                #region Display Data

                grdDataList.Clear();
                if (info != null && info.list != null)
                {
                    foreach (CargoGatePassItem item in info.list)
                    {
                        DataRow newRow = grdDataList.NewRow();
                        if (m_type == HCM111.TYPE_GATEPASS)
                        {
                            newRow[HEADER_GATEPASS] = item.gatePassNo;
                            newRow[HEADER_VESSEL_ID] = item.vslCallId;
                            newRow[HEADER_CG_NO] = item.cgNo;
                            newRow[HEADER_LORRY_NO] = item.lorryNo;
                            newRow[HEADER_TSPTR] = item.tsptr;
                            newRow[HEADER_CGINOUTCD] = item.cgInOutCd;
                            newRow[HEADER_WGT] = item.wgt;
                            newRow[HEADER_MSRMT] = item.msrmt;
                            newRow[HEADER_PKGQTY] = item.pkgQty;
                            newRow[HEADER_CATGCD] = item.catgCd;
                            newRow[HEADER_SEQ] = item.seq;
                            newRow[HEADER_CMDTCD] = item.cmdtCd;
                            //added by William (2015/07/21 - HHT) Mantis issue 49799
                            newRow[HEADER_GATE_IN_TIME] = item.gateInDt;
                        }
                        else if (m_type == HCM111.TYPE_LORRY)
                        {
                            newRow[HEADER_LORRY_NO] = item.lorryNo;
                            newRow[HEADER_VESSEL_ID] = item.vslCallId;
                            newRow[HEADER_CG_NO] = item.cgNo;
                            newRow[HEADER_CGINOUTCD] = item.cgInOutCd;
                        }
                        grdDataList.Add(newRow);
                    }
                    grdDataList.Refresh();
                        
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
            int currRowIndex = grdDataList.CurrentRowIndex;
            m_result = new GatePassListResult();
            if (m_type == HCM111.TYPE_GATEPASS)
            {
                m_result.GatePass = grdDataList.DataTable.Rows[currRowIndex][HEADER_GATEPASS].ToString();
                m_result.VslCallId = grdDataList.DataTable.Rows[currRowIndex][HEADER_VESSEL_ID].ToString();
                m_result.CgNo = grdDataList.DataTable.Rows[currRowIndex][HEADER_CG_NO].ToString();
                m_result.LorryNo = grdDataList.DataTable.Rows[currRowIndex][HEADER_LORRY_NO].ToString();
                m_result.Tsptr = grdDataList.DataTable.Rows[currRowIndex][HEADER_TSPTR].ToString();
                m_result.CgInOutCd = grdDataList.DataTable.Rows[currRowIndex][HEADER_CGINOUTCD].ToString();
                m_result.Wgt = grdDataList.DataTable.Rows[currRowIndex][HEADER_WGT].ToString();
                m_result.Mrsmt = grdDataList.DataTable.Rows[currRowIndex][HEADER_MSRMT].ToString();
                m_result.PkgQty = grdDataList.DataTable.Rows[currRowIndex][HEADER_PKGQTY].ToString();
                m_result.Catgcd = grdDataList.DataTable.Rows[currRowIndex][HEADER_CATGCD].ToString();
                m_result.Seq = grdDataList.DataTable.Rows[currRowIndex][HEADER_SEQ].ToString();
                m_result.CmdtCd = grdDataList.DataTable.Rows[currRowIndex][HEADER_CMDTCD].ToString();
            }
            else if (m_type == HCM111.TYPE_LORRY)
            {
                m_result.LorryNo = grdDataList.DataTable.Rows[currRowIndex][HEADER_LORRY_NO].ToString();
                m_result.VslCallId = grdDataList.DataTable.Rows[currRowIndex][HEADER_VESSEL_ID].ToString();
                m_result.CgNo = grdDataList.DataTable.Rows[currRowIndex][HEADER_CG_NO].ToString();
                m_result.CgInOutCd = grdDataList.DataTable.Rows[currRowIndex][HEADER_CGINOUTCD].ToString();
            }
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            MOST.Common.CommonParm.GatePassListParm gpParm = (MOST.Common.CommonParm.GatePassListParm)parm;
            if (m_type == HCM111.TYPE_GATEPASS)
            {
                txtSearchItem.Text = gpParm.GatePass;
                txtLorryNo.Text = string.IsNullOrEmpty(gpParm.LorryNo) ? string.Empty : gpParm.LorryNo;
            }
            else if (m_type == HCM111.TYPE_LORRY)
            {
                txtSearchItem.Text = gpParm.LorryNo;
            }
            DateTime endDate = string.IsNullOrEmpty(gpParm.WordDate) ?
               CommonUtility.ParseYMD(UserInfo.getInstance().Workdate)
               : CommonUtility.ParseYMD(gpParm.WordDate);
            DateTime startDate = endDate;
            txtStartDt.Value = startDate;
            txtEndDt.Value = endDate;

            //commented by William (2015/08/04 - HHT) Mantis issue 49799
            //this.shiftId = string.IsNullOrEmpty(gpParm.ShiftId) ? string.Empty : gpParm.ShiftId;

            if (!string.IsNullOrEmpty(txtSearchItem.Text)
                || !string.IsNullOrEmpty(txtLorryNo.Text))
            {
                F_Search();
            }
            this.ShowDialog();

            return m_result;
        }

        #region Event Handler

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.validations(this.Controls) && ValidationDateTime())
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

        private Boolean ValidationDateTime()
        {
            if (txtStartDt.Text == "" || txtEndDt.Text == "")
            {
                CommonUtility.AlertMessage("Please input Start Date and End Date for Gate In Date");
                return false;
            }
            DateTime startDate = txtStartDt.Value;
            DateTime endDate = txtEndDt.Value;
            DateTime minEndDate = startDate.AddDays(90);
            if (endDate >  minEndDate)
            {
                CommonUtility.AlertMessage("Date Range should be within 3 months");
                return false;
            }
            return true;
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