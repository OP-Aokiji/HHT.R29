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
using Framework.Common.ExceptionHandler;
using MOST.Common.CommonResult;
using MOST.Common.Utility;
using Framework.Controls.UserControls;
using Framework.Common.UserInformation;

namespace MOST.Common
{
    public partial class HCM108 : TDialog, IPopupWindow
    {
        #region Local Variable
        private DOListResult m_result;
        private int m_rowsCnt;

        private readonly String HEADER_JPVC = "JPVC";
        private readonly String HEADER_BL = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM108_0004");
        private readonly String HEADER_DO = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM108_0005");
        private readonly String HEADER_MT = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM108_0006");
        private readonly String HEADER_M3 = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM108_0007");
        private readonly String HEADER_QTY = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM108_0008");
        private const String HEADER_HID_TSPTR       = "_TSPTR";         // Transporter Company Code
        private const String HEADER_HID_TSPTCOMPNM  = "_TSPTCOMPNM";    // Transporter Company Name
        private const String HEADER_HID_DGYN = "_DGYN";
        private const String HEADER_HID_DGSTATCD = "_DGSTATCD";
        
        private const String HEADER_HID_DELVTPNM = "Delv Type";

        //Added by Chris 2015-11-18
        private const String HEADER_HID_DRIVERID = "DRIVER ID";
        private const String HEADER_HID_LICSNO = "LICSNO";
        private const String HEADER_HID_EXPIRE = "EXPIRE";

        //added by William (2015/07/21 - HHT) Mantis issue 49799
        MOST.Common.CommonParm.DOListParm doParm = null;

        //modified by William (2015/08/04 - HHT) Mantis issue 49799
        //private const String HEADER_HID_DELVTPCD = "_DELVTPCD";
        //private const String HEADER_HID_CMDT_CD = "CMDTCD";
        private const String HEADER_DELVTPCD = "DELVTPCD";
        private const String HEADER_CMDT_CD = "CMDTCD";

        #endregion

        public HCM108()
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
            btnF1.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0017");
            this.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM108_0001");
            lblJPVC.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM108_0002");
            lblDONo.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM108_0003");
            txtJPVC.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM108_0002");
            txtDONo.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM108_0003");

            //modified by William (2015/08/04 - HHT) Mantis issue 49799
            String[,] header = { 
                                    { HEADER_DO, "80" }, 
                                    { HEADER_CMDT_CD, "0" }, 
                                    { HEADER_MT, "90" }, 
                                    { HEADER_M3, "90" }, 
                                    { HEADER_QTY, "75" },
                                    { HEADER_BL, "90" },
                                    { HEADER_JPVC, "100" }, 
                                    { HEADER_DELVTPCD, "0" }, 
                                                                       
                                    { HEADER_HID_TSPTR, "0" }, 
                                    { HEADER_HID_TSPTCOMPNM, "0" }, 
                                    { HEADER_HID_DGYN, "0" }, 
                                    { HEADER_HID_DGSTATCD, "0" }, 
                                    { HEADER_HID_DELVTPNM, "0" }, 

                                    //Added by Chris 2015-11-18
                                    { HEADER_HID_DRIVERID, "0" }, 
                                    { HEADER_HID_LICSNO, "0" }, 
                                    { HEADER_HID_EXPIRE, "0" } 
                              };
            grdDOList.setHeader(header);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            doParm = (MOST.Common.CommonParm.DOListParm)parm;

            //commented by William (2015/07/21 - HHT) Mantis issue 49799

            //txtJPVC.Text = doParm.Jpvc;
            //txtDONo.Text = doParm.DoNo;

            //if (!string.IsNullOrEmpty(txtJPVC.Text.Trim()) ||
            //    !string.IsNullOrEmpty(txtDONo.Text.Trim()))
            //{
            //    F_Search();
            //}

            //added by William (2015/07/21 - HHT) Mantis issue 49799
            if (doParm != null && !string.IsNullOrEmpty(doParm.DoNo))
            {
                txtDONo.Text = doParm.DoNo;
            }

            //Commented by Chris 2015/09/23
            //string fromEstArr = CommonUtility.GetCurrentServerTime();
            //string toEstArr = CommonUtility.ParseYMD(fromEstArr).AddDays(7).ToString("dd/MM/yyyy");

            //Added By Chris Huynh 2015/09/23
            string fromEstArr = UserInfo.getInstance().Workdate;
            string toEstArr = CommonUtility.ParseYMD(fromEstArr).AddDays(7).ToString("dd/MM/yyyy");
            //-------------------------
            CommonUtility.SetDTPValueDMY(txtArvlDateFrom, fromEstArr.Substring(0, 10));
            CommonUtility.SetDTPValueDMY(txtArvlDateTo, toEstArr.Substring(0, 10));

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
                Framework.Service.Provider.WebService.Provider.DeliveryOrderParm parm = new Framework.Service.Provider.WebService.Provider.DeliveryOrderParm();
                if (!String.IsNullOrEmpty(txtJPVC.Text))
                {
                    parm.vslCallId = txtJPVC.Text;
                }
                if (!String.IsNullOrEmpty(txtDONo.Text))
                {
                    parm.dono = txtDONo.Text;
                }

                //added by William (2015/07/21 - HHT) Mantis issue 49799
                parm.arrvDtFm = txtArvlDateFrom.Value.ToString("dd/MM/yyyy");
                parm.arrvDtTo = txtArvlDateTo.Value.ToString("dd/MM/yyyy");
                if (doParm != null)
                {
                    parm.lorryNo = doParm.LorryNo;
                    parm.driverId = doParm.DriverId;
                }
                ResponseInfo info = proxy.GetDeliveryOrderList(parm);
                #endregion

                #region Display Data
                grdDOList.Clear();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is DeliveryOrderItem)
                    {
                        DeliveryOrderItem item = (DeliveryOrderItem)info.list[i];
                        DataRow newRow = grdDOList.NewRow();
                        newRow[HEADER_JPVC] = item.vslCallId;
                        newRow[HEADER_BL] = item.blno;
                        newRow[HEADER_DO] = item.dono;
                        newRow[HEADER_MT] = item.wgt;
                        newRow[HEADER_M3] = item.vol;
                        newRow[HEADER_QTY] = item.pkgqty;
                        newRow[HEADER_HID_TSPTR] = item.tsptr;
                        newRow[HEADER_HID_TSPTCOMPNM] = item.tsptCompNm;
                        newRow[HEADER_HID_DGYN] = item.dgYn;
                        newRow[HEADER_HID_DGSTATCD] = item.dgStatCd;
                        newRow[HEADER_DELVTPCD] = item.delvTpCd;
                        newRow[HEADER_HID_DELVTPNM] = item.delvTpNm;
                        newRow[HEADER_CMDT_CD] = item.cmdtcd;

                        //Added by Chris 2015-11-18
                        newRow[HEADER_HID_DRIVERID] = item.driverId;
                        newRow[HEADER_HID_LICSNO] = item.licsNo;
                        newRow[HEADER_HID_EXPIRE] = item.licsExprYmd;


                        grdDOList.Add(newRow);
                    }
                }
                grdDOList.Refresh();

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

        private void ReturnDOInfo()
        {
            int currRowIndex = grdDOList.CurrentRowIndex;
            string strBl = grdDOList.DataTable.Rows[currRowIndex][HEADER_BL].ToString();
            string strDo = grdDOList.DataTable.Rows[currRowIndex][HEADER_DO].ToString();
            string strMt = grdDOList.DataTable.Rows[currRowIndex][HEADER_MT].ToString();
            string strM3 = grdDOList.DataTable.Rows[currRowIndex][HEADER_M3].ToString();
            string strQty = grdDOList.DataTable.Rows[currRowIndex][HEADER_QTY].ToString();
            string strTsptr = grdDOList.DataTable.Rows[currRowIndex][HEADER_HID_TSPTR].ToString();
            string strVslCallId = grdDOList.DataTable.Rows[currRowIndex][HEADER_JPVC].ToString();
            string strTsptrCompNm = grdDOList.DataTable.Rows[currRowIndex][HEADER_HID_TSPTCOMPNM].ToString();
            string strDgYn = grdDOList.DataTable.Rows[currRowIndex][HEADER_HID_DGYN].ToString();
            string strDgStatCd = grdDOList.DataTable.Rows[currRowIndex][HEADER_HID_DGSTATCD].ToString();
            string strDelvTpCd = grdDOList.DataTable.Rows[currRowIndex][HEADER_DELVTPCD].ToString();
            string strDelvTpNm = grdDOList.DataTable.Rows[currRowIndex][HEADER_HID_DELVTPNM].ToString();
            string strCmdtCd = grdDOList.DataTable.Rows[currRowIndex][HEADER_CMDT_CD].ToString();

            //Added by Chris 2015/11/18
            string strDriverID = grdDOList.DataTable.Rows[currRowIndex][HEADER_HID_DRIVERID].ToString();
            string strLiscNo = grdDOList.DataTable.Rows[currRowIndex][HEADER_HID_LICSNO].ToString();
            string strLisExpire = grdDOList.DataTable.Rows[currRowIndex][HEADER_HID_EXPIRE].ToString();

            m_result = new DOListResult();
            m_result.Bl = strBl;
            m_result.DoNo = strDo;
            m_result.Mt = strMt;
            m_result.M3 = strM3;
            m_result.Qty = strQty;
            m_result.Tsptr = strTsptr;
            m_result.TsptrCompNm = strTsptrCompNm;
            m_result.VslCallId = strVslCallId;
            m_result.DgYn = strDgYn;
            m_result.DgStatCd = strDgStatCd;
            m_result.DelvTpCd = strDelvTpCd;
            m_result.DelvTpNm = strDelvTpNm;
            m_result.CmdtCd = strCmdtCd;

            //Added by Chris 2015/11/18
            m_result.DriverId = strDriverID;
            m_result.Lic = strLiscNo;
            m_result.Expire = strLisExpire;
            
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
            int currRowIndex = grdDOList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnDOInfo();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void grdDOList_DoubleClick(object sender, EventArgs e)
        {
            int currRowIndex = grdDOList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnDOInfo();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnF1_Click(object sender, EventArgs e)
        {
            MOST.Common.CommonParm.SearchJPVCParm jpvcParm = new MOST.Common.CommonParm.SearchJPVCParm();
            jpvcParm.Jpvc = txtJPVC.Text;

            MOST.Common.CommonResult.SearchJPVCResult jpvcResult = (SearchJPVCResult)PopupManager.instance.ShowPopup(new MOST.Common.HCM101(), jpvcParm);
            if (jpvcResult != null)
            {
                txtJPVC.Text = jpvcResult.Jpvc;
            }
        }

        private void txtArvlDateFrom_LostFocus(object sender, EventArgs e)
        {
            string fromEstArr = txtArvlDateFrom.Text.Substring(0,10);
            string toEstArr = CommonUtility.ParseYMD(fromEstArr).AddDays(7).ToString("dd/MM/yyyy");
            CommonUtility.SetDTPValueDMY(txtArvlDateFrom, fromEstArr.Substring(0, 10));
            CommonUtility.SetDTPValueDMY(txtArvlDateTo, toEstArr.Substring(0, 10));
        }

        private void txtArvlDateTo_LostFocus(object sender, EventArgs e)
        {
            string toEstArr = txtArvlDateTo.Text.Substring(0, 10);
            string fromEstArr = CommonUtility.ParseYMD(toEstArr).AddDays(-7).ToString("dd/MM/yyyy");
            CommonUtility.SetDTPValueDMY(txtArvlDateFrom, fromEstArr.Substring(0, 10));
            CommonUtility.SetDTPValueDMY(txtArvlDateTo, toEstArr.Substring(0, 10));
        }


    }
}