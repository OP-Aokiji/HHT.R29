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
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.Common.Utility;
using Framework.Common.Constants;
using Framework.Controls.UserControls;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;

namespace MOST.Common
{
    public partial class HCM117 : TDialog, IPopupWindow
    {
        #region Local Variable
        private readonly String HEADER_GR = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0005");
        private readonly String HEADER_JPVC = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0002");
        private readonly String HEADER_SN = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0003");
        private readonly String HEADER_LORRY = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0006");
        private readonly String HEADER_QTY = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0007");
        private readonly String HEADER_MT = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0008");
        private readonly String HEADER_M3 = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0009");
        // Only display in case of P/S
        private readonly String HEADER_SPCGCHK = "Spare";
        private readonly String HEADER_DELVTPNM = "Delv Tp";
        // Hidden values
        private const String HEADER_HID_TSPTR = "_TSPTR";         // Transporter Company Code
        private const String HEADER_HID_TSPTCOMPNM = "_TSPTCOMPNM";    // Transporter Company Name
        private const String HEADER_HID_CGTPCD = "_CGTPCD";        // Cargo Type
        
        private const String HEADER_HID_DGYN = "_DGYN";
        private const String HEADER_HID_DGSTATCD = "_DGSTATCD";

        //added by William (2015/08/04 - HHT) Mantis issue 49799
        //private const String HEADER_HID_DELVTPCD = "_DELVTPCD";
        //private const String HEADER_HID_CMDT_CD = "CMDTCD";
        private const String HEADER_CMDT_CD = "CMDTCD";
        private const String HEADER_DELVTPCD = "DELVTPCD";      // DELVTPCD
        private int m_rowsCnt;
        private bool m_initializedSN;
        private GRListParm m_parm;
        private GRListResult m_result;
        private SearchJPVCResult m_jpvcResult;

        //added by William (2015/07/21 - HHT) Mantis issue 49799
        private const string IS_HHT = "Y";
        private const string SEARCH_TYPE_GR = "grNo";

        //added by William (2015/08/04 - HHT) Mantis issue 49799
        private string stMode = "";
        private const string GATE_IM_TIME = "GATE IN TIME";
        private const string GATE_OUT_MODE = "GO";

        //Added by Chris 2015-11-18
        private const String HEADER_HID_DRIVERID = "DRIVER ID";
        private const String HEADER_HID_LICSNO = "LICSNO";
        private const String HEADER_HID_EXPIRE = "EXPIRE";

        #endregion

        public HCM117()
        {
            m_initializedSN = false;
            InitializeComponent();
            this.initialFormSize();
            
            /*
                Authority Check
             */
            this.authorityCheck();

            EnableJpvc();
            InitializeDataGrid();
        }

        //added by William (2015/08/04 - HHT) Mantis issue 49799
        public HCM117(string stMode)
        {
            m_initializedSN = false;
            InitializeComponent();
            this.initialFormSize();

            /*
                Authority Check
             */
            this.authorityCheck();

            EnableJpvc();
            this.stMode = stMode;

            InitializeDataGrid();
        }


        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (MOST.Common.CommonParm.GRListParm)parm;
            

            //added by William (2015/07/22 - HHT) Mantis issue 49799
            if (m_parm != null && !string.IsNullOrEmpty(m_parm.GrNo))
            {
                txtGRNo.Text = m_parm.GrNo;
            }

            if (Constants.NONCALLID.Equals(m_parm.Jpvc))
            {
                rbtnNonJPVC.Checked = true;

                //commented by William (2015/07/21 - HHT) Mantis issue 49799
                //txtArvlDateFrom.Enabled = true;
                //txtArvlDateTo.Enabled = true;
            }
            else
            {
                //Commented by Chris 2015-09-17
                //rbtnJPVC.Checked = true;

                //commented by William (2015/07/21 - HHT) Mantis issue 49799
                //txtJPVC.Text = m_parm.Jpvc;
                //txtArvlDateFrom.Enabled = false;
                //txtArvlDateTo.Enabled = false;
            }

            //added by William (2015/07/21 - HHT) Mantis issue 49799

            string fromEstArr = UserInfo.getInstance().Workdate;
            string toEstArr = CommonUtility.ParseYMD(fromEstArr).AddDays(7).ToString("dd/MM/yyyy");
            CommonUtility.SetDTPValueDMY(txtArvlDateFrom, fromEstArr);
            CommonUtility.SetDTPValueDMY(txtArvlDateTo, toEstArr);

            GetShippingNoteList();

            // In case JPVC is not empty
            if (!string.IsNullOrEmpty(GetVslCallId()))
            {
                InitializeCboSNNo(GetVslCallId());
            }

            //commented by William (2015/07/21 - HHT) Mantis issue 49799
            //if (!string.IsNullOrEmpty(GetVslCallId()))
            //{
            //    F_Retrieve();
            //}

            //Added by Chris 2015-09-17 for 49799
            if (!string.IsNullOrEmpty(txtGRNo.Text.Trim()))
            {
                F_Retrieve();
            }
            

            this.ShowDialog();
            return m_result;
        }

        private void EnableJpvc()
        {
            txtArvlDateFrom.CustomFormat = TDateTimePicker.FORMAT_DDMMYYYY;
            txtArvlDateTo.CustomFormat = TDateTimePicker.FORMAT_DDMMYYYY;
            CommonUtility.SetDTPValueBlank(txtArvlDateFrom);
            CommonUtility.SetDTPValueBlank(txtArvlDateTo);

            rbtnJPVC.Enabled = true;
            rbtnNonJPVC.Enabled = true;
            btnF1.Visible = true;
            txtJPVC.Enabled = true;
        }

        private void InitializeDataGrid()
        {
            btnOk.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0013");
            btnCancel.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0014");
            btnRetrieve.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0062");
            btnF1.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0017");
            this.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0001");
            lblSNNo.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0003");
            lblGRNo.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0004");
            txtJPVC.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0002");
            cboSNNo.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0003");
            txtGRNo.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0004");

            //modified by William (2015/08/04 - HHT) Mantis issue 49799
            string[,] header;
            if (!stMode.Equals(GATE_OUT_MODE))
            {
                header = new string[,]{ 
                                { HEADER_GR, "85" },
                                { HEADER_CMDT_CD, "100" },
                                { HEADER_MT, "60" }, 
                                { HEADER_M3, "60" }, 
                                { HEADER_QTY, "50" },  
                                { HEADER_SN, "85" }, 
                                { HEADER_JPVC, "85" }, 
                                { HEADER_DELVTPCD, "100" },

                                { HEADER_DELVTPNM, "0" },
                                { HEADER_SPCGCHK, "0" }, 
                                //{ HEADER_LORRY, "0" }, 
                                { HEADER_HID_TSPTR, "0" }, 
                                { HEADER_HID_TSPTCOMPNM, "0" },  
                                { HEADER_HID_CGTPCD, "0" }, 
                                { HEADER_HID_DGYN, "0" }, 
                                { HEADER_HID_DGSTATCD, "0" },
                                
                                //Added by Chris 2015-11-18
                                { HEADER_HID_DRIVERID, "0" },
                                { HEADER_HID_LICSNO, "0" },
                                { HEADER_HID_EXPIRE, "0" }
                               };
            }
            else
            {
                header = new string[,]{ 
                                { HEADER_GR, "85" },
                                { HEADER_CMDT_CD, "100" },
                                { HEADER_MT, "60" }, 
                                { HEADER_M3, "60" }, 
                                { HEADER_QTY, "50" },  
                                { HEADER_SN, "85" }, 
                                { HEADER_JPVC, "85" }, 
                                { HEADER_DELVTPCD, "100" },
                                { GATE_IM_TIME, "100" },

                                { HEADER_DELVTPNM, "0" },
                                { HEADER_SPCGCHK, "0" }, 
                                //{ HEADER_LORRY, "0" }, 
                                { HEADER_HID_TSPTR, "0" }, 
                                { HEADER_HID_TSPTCOMPNM, "0" },  
                                { HEADER_HID_CGTPCD, "0" }, 
                                { HEADER_HID_DGYN, "0" }, 
                                { HEADER_HID_DGSTATCD, "0" },

                                //Added by Chris 2015-11-18
                                { HEADER_HID_DRIVERID, "0" },
                                { HEADER_HID_LICSNO, "0" },
                                { HEADER_HID_EXPIRE, "0" }
                               

                        };
            }
            this.grdGRList.setHeader(header);
        }

        private void InitializeCboSNNo(String argJpvc)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ResponseInfo info;  
                ICommonProxy proxy = new CommonProxy();
                CommonUtility.InitializeCombobox(cboSNNo);

                // In case user input JPVC directly (don't choose from pop up):
                // return if JPVC is not correct
                //if (rbtnJPVC.Checked && m_jpvcResult == null && CommonUtility.IsValidJPVC(argJpvc, ref m_jpvcResult) == false)
                //{
                //    CommonUtility.InitializeCombobox(cboSNNo);
                //    return;
                //}

                if (rbtnJPVC.Checked == false && rbtnNonJPVC.Checked == false)
                {
                    Framework.Service.Provider.WebService.Provider.ShippingNoteParm parm = new Framework.Service.Provider.WebService.Provider.ShippingNoteParm();
                    parm.arrvDtFm = txtArvlDateFrom.Text.Substring(0, 10);
                    parm.arrvDtTo = txtArvlDateTo.Text.Substring(0, 10);
                    parm.lorryNo = m_parm.LorryNo;
                    parm.searchTypeCboSN = "B";
                    info = proxy.getShippingNoteComboList(parm);
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is ShippingNoteItem)
                        {
                            ShippingNoteItem item = (ShippingNoteItem)info.list[i];
                            cboSNNo.Items.Add(item.shipgNoteNo);
                        }
                    }
                    
                }
                cboSNNo.Refresh();
                
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
                m_initializedSN = true;
                Cursor.Current = Cursors.Default;
            }
        }

        //Added by Chris 2015-09-24 for 49799
        private void InitializeCboSNNo2(String argJpvc)
        {
            try
            {
                cboSNNo.Items.Clear();
                Cursor.Current = Cursors.WaitCursor;
                ResponseInfo info;
                ICommonProxy proxy = new CommonProxy();
                CommonUtility.InitializeCombobox(cboSNNo);
                Framework.Service.Provider.WebService.Provider.ShippingNoteParm parm = new Framework.Service.Provider.WebService.Provider.ShippingNoteParm();
                // In case user input JPVC directly (don't choose from pop up):
                // return if JPVC is not correct
                if (rbtnJPVC.Checked && m_jpvcResult == null && CommonUtility.IsValidJPVC(argJpvc, ref m_jpvcResult) == false)
                {
                    CommonUtility.InitializeCombobox(cboSNNo);
                    return;
                }

                if (!rbtnJPVC.Checked && !rbtnNonJPVC.Checked)
                {  
                    parm.arrvDtFm = txtArvlDateFrom.Text.Substring(0, 10);
                    parm.arrvDtTo = txtArvlDateTo.Text.Substring(0, 10);
                    parm.lorryNo = m_parm.LorryNo;
                    parm.searchTypeCboSN = "B";
                    info = proxy.getShippingNoteComboList(parm);
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is ShippingNoteItem)
                        {
                            ShippingNoteItem item = (ShippingNoteItem)info.list[i];
                            cboSNNo.Items.Add(item.shipgNoteNo);
                        }
                    }
                }
                if (rbtnJPVC.Checked)
                {
                    parm.arrvDtFm = txtArvlDateFrom.Text.Substring(0, 10);
                    parm.arrvDtTo = txtArvlDateTo.Text.Substring(0, 10);
                    parm.lorryNo = m_parm.LorryNo;
                    parm.vslCallId = txtJPVC.Text;
                    parm.searchTypeCboSN = "J";
                    info = proxy.getShippingNoteComboList(parm);
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is ShippingNoteItem)
                        {
                            ShippingNoteItem item = (ShippingNoteItem)info.list[i];
                            cboSNNo.Items.Add(item.shipgNoteNo);
                        }
                    }
                }
                if (rbtnNonJPVC.Checked)
                {
                    parm.arrvDtFm = txtArvlDateFrom.Text.Substring(0, 10);
                    parm.arrvDtTo = txtArvlDateTo.Text.Substring(0, 10);
                    parm.lorryNo = m_parm.LorryNo;
                    parm.searchTypeCboSN = "N";
                    info = proxy.getShippingNoteComboList(parm);
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is ShippingNoteItem)
                        {
                            ShippingNoteItem item = (ShippingNoteItem)info.list[i];
                            cboSNNo.Items.Add(item.shipgNoteNo);
                        }
                    }
                }

                
                cboSNNo.Refresh();

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
                m_initializedSN = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private string GetVslCallId()
        {
            string vslCallId = string.Empty;
            if (!rbtnNonJPVC.Checked && !rbtnJPVC.Checked)
            {
                vslCallId = "";
            }
            else if (rbtnJPVC.Checked)
            {
                vslCallId = txtJPVC.Text.Trim();
            }
            else if (rbtnNonJPVC.Checked)
            {
                vslCallId = Constants.NONCALLID;
            }
            return vslCallId;
        }

        private void F_Retrieve()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ICommonProxy proxy = new CommonProxy();
                Framework.Service.Provider.WebService.Provider.GoodsReceiptParm parm = new Framework.Service.Provider.WebService.Provider.GoodsReceiptParm();

                //added by William (2015/07/21 - HHT) Mantis issue 49799
                parm.arrvDtFm = txtArvlDateFrom.Value.ToString("dd/MM/yyyy");
                parm.arrvDtTo = txtArvlDateTo.Value.ToString("dd/MM/yyyy");
                if (m_parm != null)
                {
                    parm.gateInOut = m_parm.GateInOut;
                    parm.lorryNo = m_parm.LorryNo;
                    parm.driverId = m_parm.DriverId;
                }
                parm.vslCallId = GetVslCallId();

                //added by William (2015/07/21 - HHT) Mantis issue 49799
                string strShipgNoteNo = CommonUtility.GetComboboxSelectedValue(cboSNNo);
                if (!String.IsNullOrEmpty(strShipgNoteNo))
                {
                    parm.shipgNoteNo = strShipgNoteNo;
                }
                if (!String.IsNullOrEmpty(txtGRNo.Text))
                {
                    parm.gdsRecvNo = txtGRNo.Text;
                }

                parm.searchType = SEARCH_TYPE_GR;
                ResponseInfo info = proxy.GetGoodsReceiptList(parm);

                grdGRList.Clear();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is GoodsReceiptItem)
                    {
                        GoodsReceiptItem item = (GoodsReceiptItem)info.list[i];
                        DataRow newRow = grdGRList.NewRow();
                        newRow[HEADER_GR] = item.gdsRecvNo;
                        newRow[HEADER_JPVC] = item.vslCallId;
                        newRow[HEADER_SN] = item.shipgNoteNo;

                        //commented by William (2015/08/04 - HHT) Mantis issue 49799
                        //newRow[HEADER_LORRY] = item.lorryId;
                        if (stMode.Equals(GATE_OUT_MODE))
                            newRow[GATE_IM_TIME] = item.gateInDt;

                        newRow[HEADER_QTY] = item.grQty;
                        newRow[HEADER_MT] = item.grWgt;
                        newRow[HEADER_M3] = item.grMsrmt;
                        newRow[HEADER_SPCGCHK] = item.spCargoChk;
                        newRow[HEADER_DELVTPNM] = item.delvTpNm;
                        newRow[HEADER_HID_TSPTR] = item.tsptr;
                        newRow[HEADER_HID_TSPTCOMPNM] = item.tsptCompNm;
                        newRow[HEADER_HID_CGTPCD] = item.cgTpCd;
                        newRow[HEADER_DELVTPCD] = item.delvTpCd;
                        newRow[HEADER_HID_DGYN] = item.dgYn;
                        newRow[HEADER_HID_DGSTATCD] = item.dgStatCd;
                        newRow[HEADER_CMDT_CD] = item.cmdtCd;
                        //Added by Chris 2015-11-18
                        newRow[HEADER_HID_DRIVERID] = item.driverId;
                        newRow[HEADER_HID_LICSNO] = item.licsNo;
                        newRow[HEADER_HID_EXPIRE] = item.licsExprYmd;

                        grdGRList.Add(newRow);
                    }

                    cboSNNo.Refresh();
                }
                grdGRList.Refresh();

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

        private void GetShippingNoteList()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (!string.IsNullOrEmpty(txtArvlDateFrom.Text) &&
                    !string.IsNullOrEmpty(txtArvlDateTo.Text))
                {
                    ICommonProxy proxy = new CommonProxy();
                    CargoMasterParm parm = new CargoMasterParm();
                    parm.vslCallId = GetVslCallId();
                    parm.arrvDtFm = txtArvlDateFrom.Text;
                    parm.arrvDtTo = txtArvlDateTo.Text;
                    ResponseInfo info = proxy.getCargoMasterComboList(parm);

                    CommonUtility.InitializeCombobox(cboSNNo);
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is ShippingNoteItem)
                        {
                            ShippingNoteItem item = (ShippingNoteItem)info.list[i];
                            string descr = item.shipgNoteNo;
                            if (!string.IsNullOrEmpty(item.catgCdNm))
                            {
                                descr += "(" + item.catgCdNm + ")";
                            }
                            cboSNNo.Items.Add(new ComboboxValueDescriptionPair(item.shipgNoteNo, descr));
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

        private void ReturnGRInfo()
        {
            int currRowIndex = grdGRList.CurrentRowIndex;
            string strGr = grdGRList.DataTable.Rows[currRowIndex][HEADER_GR].ToString();
            string strJpvc = grdGRList.DataTable.Rows[currRowIndex][HEADER_JPVC].ToString();
            string strSn = grdGRList.DataTable.Rows[currRowIndex][HEADER_SN].ToString();

            //commented by William (2015/08/04 - HHT) Mantis issue 49799
            //string strLorry = grdGRList.DataTable.Rows[currRowIndex][HEADER_LORRY].ToString();

            string strQty = grdGRList.DataTable.Rows[currRowIndex][HEADER_QTY].ToString();
            string strMt = grdGRList.DataTable.Rows[currRowIndex][HEADER_MT].ToString();
            string strM3 = grdGRList.DataTable.Rows[currRowIndex][HEADER_M3].ToString();
            string strSpCgChk = grdGRList.DataTable.Rows[currRowIndex][HEADER_SPCGCHK].ToString();
            string strTsptr = grdGRList.DataTable.Rows[currRowIndex][HEADER_HID_TSPTR].ToString();
            string strTsptrCompNm = grdGRList.DataTable.Rows[currRowIndex][HEADER_HID_TSPTCOMPNM].ToString();
            string strCgTpCd = grdGRList.DataTable.Rows[currRowIndex][HEADER_HID_CGTPCD].ToString();
            string strDelvTpCd = grdGRList.DataTable.Rows[currRowIndex][HEADER_DELVTPCD].ToString();
            string strDelvTpNm = grdGRList.DataTable.Rows[currRowIndex][HEADER_DELVTPNM].ToString();
            string strDgYn = grdGRList.DataTable.Rows[currRowIndex][HEADER_HID_DGYN].ToString();
            string strDgStatCd = grdGRList.DataTable.Rows[currRowIndex][HEADER_HID_DGSTATCD].ToString();
            string strCmdtCd = grdGRList.DataTable.Rows[currRowIndex][HEADER_CMDT_CD].ToString();

            //Added by Chris 2015/11/18
            string strDriverID = grdGRList.DataTable.Rows[currRowIndex][HEADER_HID_DRIVERID].ToString();
            string strLiscNo = grdGRList.DataTable.Rows[currRowIndex][HEADER_HID_LICSNO].ToString();
            string strLisExpire = grdGRList.DataTable.Rows[currRowIndex][HEADER_HID_EXPIRE].ToString();

            m_result = new GRListResult();
            m_result.GrNo = strGr;
            m_result.VslCallId = strJpvc;
            m_result.ShipgNoteNo = strSn;

            //commented by William (2015/08/04 - HHT) Mantis issue 49799
            //m_result.Lorry = strLorry;

            m_result.Qty = strQty;
            m_result.Mt = strMt;
            m_result.M3 = strM3;
            m_result.SpCargoChk = strSpCgChk;
            m_result.Tsptr = strTsptr;
            m_result.TsptrCompNm = strTsptrCompNm;
            m_result.CgTpCd = strCgTpCd;
            m_result.DelvTpCd = strDelvTpCd;
            m_result.DelvTpNm = strDelvTpNm;
            m_result.DgYn = strDgYn;
            m_result.DgStatCd = strDgStatCd;
            m_result.CmdtCd = strCmdtCd;
            //m_result.SpYn = strSpYn;
            //m_result.FnlOpeYn = strFnlOpeYn;
            //m_result.HiFnlYn = strHiFnlYn;
            //m_result.RhdlMode = strRhdlMode;
            //m_result.DmgYn = strDmgYn;
            //m_result.ShuYn = strShuYn;

            //Added by Chris 2015/11/18
            m_result.DriverId = strDriverID;
            m_result.Lic = strLiscNo;
            m_result.Expire = strLisExpire;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int currRowIndex = grdGRList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnGRInfo();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_result = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void grdGRList_DoubleClick(object sender, EventArgs e)
        {
            int currRowIndex = grdGRList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnGRInfo();

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
                m_jpvcResult = jpvcResult;

                //Commented by Chris 2015-09-23
                //InitializeCboSNNo(jpvcResult.Jpvc);
            }
        }

        private void RadiobuttonListener(object sender, EventArgs e)
        {
            RadioButton mybutton = (RadioButton)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "rbtnJPVC":
                case "rbtnNonJPVC":
                    OnCheckRadioButton();
                    //Commented by Chris 2015-09-24 for 49799
                    //InitializeCboSNNo(GetVslCallId());
                    break;
            }
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            if (this.validations(this.Controls) )
            {
                //added by William (2015/07/21 - HHT) Mantis issue: 49799
                if (rbtnJPVC.Checked && string.IsNullOrEmpty(txtJPVC.Text))
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM117_0001"));
                    return;
                }

                F_Retrieve();
                InitializeCboSNNo2(GetVslCallId());

                if (m_rowsCnt <= 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0058"));
                    txtJPVC.Focus();
                    txtJPVC.SelectAll();
                }
            }
        }

        private void OnCheckRadioButton()
        {
            if (rbtnJPVC.Checked)
            {
                //added by William (2015/07/21 - HHT) Mantis issue: 49799
                txtJPVC.Enabled = true;
                btnF1.Enabled = true;
                //txtArvlDateFrom.Enabled = false;
                //txtArvlDateTo.Enabled = false;
                //txtArvlDateFrom.isMandatory = false;
                //txtArvlDateTo.isMandatory = false;
            }
            else if (rbtnNonJPVC.Checked)
            {
                //added by William (2015/07/21 - HHT) Mantis issue: 49799
                txtJPVC.Enabled = false;
                btnF1.Enabled = false;
                //txtArvlDateFrom.Enabled = true;
                //txtArvlDateTo.Enabled = true;
                //txtArvlDateFrom.isMandatory = true;
                //txtArvlDateTo.isMandatory = true;
            }
        }

        private void txtJPVC_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJPVC.Text) && !m_initializedSN)
            {
                InitializeCboSNNo(txtJPVC.Text);
            }
        }

        private void txtJPVC_KeyPress(object sender, KeyPressEventArgs e)
        {
            m_initializedSN = false;

            // if key = Enter then get vessel schedule detail
            if (e.KeyChar.Equals((char)Keys.Enter))
            {
                InitializeCboSNNo(txtJPVC.Text);
            }
        }

        private void txtJPVC_TextChanged(object sender, EventArgs e)
        {
            m_initializedSN = false;
        }

        private void EstDateLostFocus(object sender, EventArgs e)
        {
            string fromEstArr;
            string toEstArr;
            DateTimePicker mybutton = (DateTimePicker)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "txtArvlDateFrom":
                    fromEstArr = txtArvlDateFrom.Text;
                    toEstArr = CommonUtility.ParseYMD(fromEstArr).AddDays(7).ToString("dd/MM/yyyy");
                    CommonUtility.SetDTPValueDMY(txtArvlDateFrom, fromEstArr);
                    CommonUtility.SetDTPValueDMY(txtArvlDateTo, toEstArr);
                    //Commented by Chris 2015-09-23
                    //GetShippingNoteList();
                    break;
                case "txtArvlDateTo":
                    toEstArr = txtArvlDateTo.Text;
                    fromEstArr = CommonUtility.ParseYMD(toEstArr).AddDays(-7).ToString("dd/MM/yyyy");
                    CommonUtility.SetDTPValueDMY(txtArvlDateFrom, fromEstArr);
                    CommonUtility.SetDTPValueDMY(txtArvlDateTo, toEstArr);
                    //Commented by Chris 2015-09-23
                    //GetShippingNoteList();
                    break;
            }
            
            
        }
    }
}