using System;
//using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Threading;

namespace MOST.Common
{
    public partial class HCM103 : TDialog, IPopupWindow
    {
        #region Local Variable
        private readonly String HEADER_GR = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0005");
        private readonly String HEADER_JPVC = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0002");
        private readonly String HEADER_SN = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0003");
        private readonly String HEADER_LORRY = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0006");
        private readonly String HEADER_DOCMT = "Doc MT";
        private readonly String HEADER_DOCM3 = "Doc M3";
        private readonly String HEADER_DOCQTY = "Doc Qty";
        private readonly String HEADER_MT = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0008");
        private readonly String HEADER_M3 = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0009");
        private readonly String HEADER_QTY = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0007");
        // Only display in case of A/C, W/C
        private readonly String HEADER_STATUS = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0010");
        private readonly String HEADER_CATGNM = "Catg";
        private readonly String HEADER_DELVTPNM = "Delv Tp";
        private readonly String HEADER_CGTPCDNM = "CgTp";        // Cargo Type
        private readonly String HEADER_FNLOPEYN = "LD Final";
        private readonly String HEADER_HIFNLYN = "HI Final";
        private readonly String HEADER_WHLOC = "Loc";
        private readonly String HEADER_RHDLMODE = "Rhdl";
        private readonly String HEADER_DMGYN = "Dmg";
        private readonly String HEADER_SHUYN = "Shut";
        private readonly String HEADER_SPRYN = "Spr";
        private readonly String HEADER_PKGNO = "PkgNo";
        // Hidden values
        private const String HEADER_HID_TSPTR = "_TSPTR";         // Transporter Company Code
        private const String HEADER_HID_TSPTCOMPNM = "_TSPTCOMPNM";    // Transporter Company Name
        private const String HEADER_HID_CGTPCD = "_CGTPCD";        // Cargo Type
        private const String HEADER_HID_DELVTPCD = "_DELVTPCD";      // DELVTPCD
        private const String HEADER_HID_DGYN = "_DGYN";
        private const String HEADER_HID_DGSTATCD = "_DGSTATCD";

        private int m_rowsCnt;
        private GRListParm m_parm;
        private CargoExportResult m_result;

        /**
         * lv.dat
         * not use this way any more
         */ 
        /*
        private double m_docMt;
        private double m_docM3;
        private int m_docQty;
        private double m_actMt;
        private double m_actM3;
        private int m_actQty;*/

        //QUANBTL 09-08-2012 fix G/R retrieve performance START

        private List<CargoExportItem> cargoItemList;
        private CargoExportParm cgExpParm;

        /*
        private NetworkAdapter[] adapters;
        private int adapterIndex;
        private NetworkMonitor monitor;
        

        NetworkInterface networkInterface;
        
        long lngBtyesReceived = 0;
        */
        private Thread oThread;
        private int isAlive;
        private string oldFromDate;
        private string oldToDate;
        private string oldGr;
        //QUANBTL 09-08-2012 fix G/R retrieve performance END

        /**
         * lv.dat
         * add local val for paging and status filter
         */
        private int iTotalPage = 0;
        private int iCurrentPage = 0;
        private int iNumbPerPage = Constants.iNumbPerPage;
        private string sStat = "NotFinal";
        private int iFlag = 2; // 1 - load using GR , 2 - load using status chose

        #endregion

        public HCM103()
        {
            InitializeComponent();
            this.initialFormSize();

            //QUANBTL 09-08-2012 fix G/R retrieve performance START
            this.Closing += new CancelEventHandler(this.HCM103_Closing);
            /*
            this.network.Location = new Point(this.network.Left,
                                    this.network.Top - this.network.Height);
            */
            //QUANBTL 09-08-2012 fix G/R retrieve performance END

            /*
                Authority Check
             */
            this.authorityCheck();

            InitializeDataGrid();
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (MOST.Common.CommonParm.GRListParm)parm;

            if (m_parm.GrNo != null && !String.Empty.Equals(m_parm.GrNo))
            {
                this.txtGRNo.Text = m_parm.GrNo.ToString();
                this.oldGr = this.txtGRNo.Text;
            }

            InitializeData();
            this.ShowDialog();
            return m_result;
        }

        private void InitializeData()
        {
            #region Title
            string strTitle = "GR List";
            if (m_parm != null && !string.IsNullOrEmpty(m_parm.Jpvc))
            {
                strTitle = strTitle + " - " + m_parm.Jpvc;
            }
            this.Text = strTitle;
            #endregion

            #region Estimate Arrival Date

            txtArvlDateFrom.CustomFormat = TDateTimePicker.FORMAT_DDMMYYYY;
            txtArvlDateTo.CustomFormat = TDateTimePicker.FORMAT_DDMMYYYY;
            if (m_parm != null && Constants.NONCALLID.Equals(m_parm.Jpvc))
            {
                txtArvlDateFrom.Value = DateTime.Now.AddDays(-30);
                txtArvlDateTo.Value = DateTime.Now.AddDays(30);
                txtArvlDateFrom.Text = DateTime.Now.AddDays(-30).ToString(TDateTimePicker.FORMAT_DDMMYYYY);
                txtArvlDateTo.Text = DateTime.Now.AddDays(30).ToString(TDateTimePicker.FORMAT_DDMMYYYY);
            }
            else
            {
                CommonUtility.SetDTPValueBlank(txtArvlDateFrom);
                CommonUtility.SetDTPValueBlank(txtArvlDateTo);
            }
            if (m_parm != null && Constants.NONCALLID.Equals(m_parm.Jpvc))
            {
                txtArvlDateFrom.Enabled = true;
                txtArvlDateTo.Enabled = true;
            }
            else
            {
                txtArvlDateFrom.Enabled = false;
                txtArvlDateTo.Enabled = false;
            }

            this.oldFromDate = txtArvlDateFrom.Text;
            this.oldToDate = txtArvlDateTo.Text;

            #endregion

            //QUANBTL 09-08-2012 fix G/R retrieve performance START

            #region internet connection
            /*
            monitor = new NetworkMonitor();
            this.adapters = monitor.Adapters;
            monitor.StopMonitoring();
            for (int i = 0; i < this.adapters.Length; i++)
            {
                if (adapters[i].Name.IndexOf(Constants.STRING_WIFI) > 0
                    || adapters[i].Name.IndexOf(Constants.STRING_WIRELESS) > 0)
                {
                    monitor.StartMonitoring(adapters[i]);
                    this.adapterIndex = i;
                    break;
                }
            }
            


            foreach (NetworkInterface currentNetworkInterface
                in NetworkInterface.GetAllNetworkInterfaces())
            {
                this.networkInterface = currentNetworkInterface;
            }
            */
            #endregion

            //QUANBTL 09-08-2012 fix G/R retrieve performance END      

            /**
             * lv.dat
             * add status 
             */
            //InitializeCboStatus();

            #region Shipping Note
            // In case JPVC is not empty
            //if (!string.IsNullOrEmpty(GetVslCallId()))
            //{
                InitializeCboSNNo();
            //}
            #endregion

            InitializeCboPaging();

            #region Grid data
            if (!string.IsNullOrEmpty(GetVslCallId()))
            {
                //F_Retrieve();
            }
            #endregion
        }

        private void InitializeDataGrid()
        {
            btnOk.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0013");
            btnCancel.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0014");
            btnRetrieve.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0062");
            this.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0001");
            lblSNNo.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0003");
            lblGRNo.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0004");
            cboSNNo.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0003");
            txtGRNo.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM103_0004");

            String[,] header = { { HEADER_CATGNM, "50" }, { HEADER_GR, "85" }, { HEADER_STATUS, "90" }, { HEADER_SPRYN, "40" }, { HEADER_JPVC, "85" }, { HEADER_SN, "85" }, { HEADER_LORRY, "60" }, 
                                { HEADER_DOCMT, "60" }, { HEADER_DOCM3, "60" }, { HEADER_DOCQTY, "50" }, { HEADER_MT, "60" }, { HEADER_M3, "60" }, { HEADER_QTY, "50" }, 
                                { HEADER_DELVTPNM, "50" }, { HEADER_CGTPCDNM, "50" }, { HEADER_FNLOPEYN, "60" }, { HEADER_HIFNLYN, "60" }, { HEADER_WHLOC, "60" }, { HEADER_RHDLMODE, "60" }, { HEADER_DMGYN, "50" }, { HEADER_SHUYN, "50" }, { HEADER_PKGNO, "70" },
                                { HEADER_HID_TSPTR, "0" }, { HEADER_HID_TSPTCOMPNM, "0" }, { HEADER_HID_CGTPCD, "0" }, { HEADER_HID_DELVTPCD, "0" }, { HEADER_HID_DGYN, "0" }, { HEADER_HID_DGSTATCD, "0" } };
            this.grdGRList.setHeader(header);
            
        }

        //QUANBTL 09-08-2012 fix G/R retrieve performance START

        private void InitializeCboSNNo()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ICommonProxy proxy = new CommonProxy();

                CommonUtility.InitializeCombobox(cboSNNo);

                #region "Query from DB"
                
                //getCargoItem(proxy);
                GetShippingNoteList();
                #endregion

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
                Cursor.Current = Cursors.Default;
            }
        }

        //private void InitializeCboStatus()
        //{
        //    try
        //    {
        //        int iIndexDefault = 0;
        //        ICommonProxy proxy = new CommonProxy();
        //        CommonUtility.InitializeCombobox(cboStatus);

        //        cgExpParm = new CargoExportParm();

        //        ResponseInfo info = proxy.getStatusList(cgExpParm);

        //        for (int i = 0; i < info.list.Length; i++)
        //        {
        //            if (info.list[i] is CodeMasterListItem)
        //            {
        //                CodeMasterListItem item = (CodeMasterListItem)info.list[i];
        //                cboStatus.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));

        //                if (item.scdNm.Equals("Reserved"))
        //                    iIndexDefault = i;
        //            }
        //            else if (info.list[i] is CodeMasterListItem1)
        //            {
        //                CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
        //                cboStatus.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));

        //                if (item.scdNm.Equals("Reserved"))
        //                    iIndexDefault = i;
        //            }
        //        }

        //        cboStatus.SelectedIndex = iIndexDefault + 1;
        //        this.sStat = cboStatus.Text;
        //        this.cboStatus.SelectedIndexChanged += new System.EventHandler(this.cboStatus_SelectedIndexChanged);
        //    }
        //    catch (Framework.Common.Exception.BusinessException ex)
        //    {
        //        ExceptionHandler.ErrorHandler(this, ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHandler.ErrorHandler(this, ex);
        //    }
        //}

        /**
         * lv.dat
         * to calculate MT , M3 , Qty 
         * TODO: will implement new object to take just 3 item
         */
        private void InitializeCalculate()
        {
            try
            {
                ICommonProxy proxy = new CommonProxy();

                ResponseInfo info = proxy.getSummary(cgExpParm);

                CargoExportItem item = (CargoExportItem) info.list[0];

                txtDocMt.Text = item.docMt.ToString();
                txtDocM3.Text = item.docM3.ToString();
                txtDocQty.Text = item.docQty.ToString();
                txtActMt.Text = item.accLoadMt.ToString();
                txtActM3.Text = item.accLoadM3.ToString();
                txtActQty.Text = item.accLoadQty.ToString();
            }
            catch (Framework.Common.Exception.BusinessException ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private void HCM103_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            abortThread();
        }

        /*
         * Add new row for datagrid
         * Data is 1 elem taken from cargoItemList
         */
        private void addnewRow(CargoExportItem item)
        {
            DataRow newRow = grdGRList.NewRow();
            newRow[HEADER_CATGNM] = item.catgNm;
            newRow[HEADER_GR] = item.grNo;
            newRow[HEADER_STATUS] = item.statNm;
            newRow[HEADER_JPVC] = item.vslCallId;
            newRow[HEADER_SN] = item.shipgNoteNo;
            newRow[HEADER_LORRY] = item.lorryId;
            newRow[HEADER_DOCMT] = item.docMt;
            newRow[HEADER_DOCM3] = item.docM3;
            newRow[HEADER_DOCQTY] = item.docQty;
            newRow[HEADER_MT] = item.wgt;
            newRow[HEADER_M3] = item.msrmt;
            newRow[HEADER_QTY] = item.pkgQty;
            newRow[HEADER_DELVTPNM] = item.delvTpNm;
            newRow[HEADER_CGTPCDNM] = item.cgTpCdNm;

            newRow[HEADER_FNLOPEYN] = item.fnlOpeYn;
            newRow[HEADER_HIFNLYN] = item.hiFnlYn;
            newRow[HEADER_WHLOC] = item.whLocIds;
            newRow[HEADER_RHDLMODE] = item.rhdlMode;
            newRow[HEADER_DMGYN] = item.dmgYn;
            newRow[HEADER_SHUYN] = item.shuYn;
            newRow[HEADER_PKGNO] = item.pkgNo;
            newRow[HEADER_SPRYN] = item.spYn;

            newRow[HEADER_HID_TSPTR] = string.Empty;
            newRow[HEADER_HID_TSPTCOMPNM] = string.Empty;
            newRow[HEADER_HID_CGTPCD] = item.cgTpCd;
            newRow[HEADER_HID_DELVTPCD] = item.delvTpCd;
            newRow[HEADER_HID_DGYN] = string.Empty;
            newRow[HEADER_HID_DGSTATCD] = string.Empty;
            grdGRList.Add(newRow);

            //not use this way to display summary
            /*m_docMt += item.docMt;
            m_docM3 += item.docM3;
            m_docQty += item.docQty;
            m_actMt += item.wgt;
            m_actM3 += item.msrmt;
            m_actQty += item.pkgQty;*/
        }

        /*
         * Reload G/R details for each time F_Retrieve is called
         */
        private void reloadGrDetail()
        {
            /*this.m_docMt = 0;
            this.m_docM3 = 0;
            this.m_docQty = 0;
            this.m_actMt = 0;
            this.m_actM3 = 0;
            this.m_actQty = 0;*/
        }

        private void changeControlEnabled(bool enableTF)
        {
            this.btnCancel.Enabled = enableTF;
            this.btnOk.Enabled = enableTF;
            this.btnRetrieve.Enabled = enableTF;
        }

        // For searching in object list
        #region Explicit predicate delegate

        private bool FindExcludeFnlLoading(CargoExportItem item)
        {
            if (Constants.STRING_NULL == item.loadEndDt
                || item.loadEndDt == null)
            {
                return true;
            }
            {
                return false;
            }
        }

        private bool FindExcludeFnlHI(CargoExportItem item)
        {
            if (Constants.STRING_NULL == item.hdlInEndDt
                || item.hdlInEndDt == null)
            {
                return true;
            }
            {
                return false;
            }
        }

        private bool FindShipgNoteNo(CargoExportItem item)
        {
            if (item.shipgNoteNo.Equals(CommonUtility.GetComboboxSelectedValue(cboSNNo)))
            {
                return true;
            }
            {
                return false;
            }
        }

        private bool FindGr(CargoExportItem item)
        {
            if (item.grNo.IndexOf(txtGRNo.Text) > -1)
            {
                return true;
            }
            {
                return false;
            }
        }

        #endregion

        private void F_Retrieve()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ICommonProxy proxy = new CommonProxy();
                Framework.Service.Provider.WebService.Provider.CargoExportParm cgExpParm
                    = new Framework.Service.Provider.WebService.Provider.CargoExportParm();
                string currentFromDate = txtArvlDateFrom.Text;
                string currentToDate = txtArvlDateTo.Text;

                cgExpParm.vslCallId = GetVslCallId();

                if (!this.oldFromDate.Equals(currentFromDate)
                    || !this.oldToDate.Equals(currentToDate))
                {
                    #region "Need to load data again from DB"

                    this.oldFromDate = currentFromDate;
                    this.oldToDate = currentToDate;

                    // make use of threading to load DB independently from main thread

                    this.changeControlEnabled(false);
                    oThread = new Thread(new ThreadStart(excute));
                    oThread.Start();
                    this.isAlive = 1;

                    #endregion
                }
                else if (this.oldGr != null && !String.Empty.Equals(oldGr)
                    && !this.oldGr.Equals(this.txtGRNo.Text))
                {

                    #region "Need to load data again from DB"

                    this.oldGr = this.txtGRNo.Text;

                    // make use of threading to load DB independently from main thread

                    this.changeControlEnabled(false);
                    oThread = new Thread(new ThreadStart(excute));
                    oThread.Start();
                    this.isAlive = 1;

                    #endregion
                }
                else
                {
                    #region "Query in existed array"

                    string shippingNoteNo = CommonUtility.GetComboboxSelectedValue(cboSNNo);
                    string grNo = "";

                    if (grNo != null)
                    {
                        grNo = txtGRNo.Text;
                    }


                    // existed data when init
                    if (cargoItemList != null)
                    {

                        List<CargoExportItem> itemList = new List<CargoExportItem>();
                        itemList.AddRange(this.cargoItemList);

                        /**
                         * filter in DB , not find in array anymore , this region use to add item to grid
                         */

                        /*
                        if (m_parm.ExcludeFnlLoading)
                        {
                            itemList = itemList.FindAll(FindExcludeFnlLoading);
                        }

                        if (m_parm.ExcludeFnlHI)
                        {
                            itemList = itemList.FindAll(FindExcludeFnlHI);
                        }
                        
                        if (!shippingNoteNo.Equals(Constants.STRING_NULL))
                        {
                            itemList = itemList.FindAll(FindShipgNoteNo);
                        }

                        if (!grNo.Equals(Constants.STRING_NULL))
                        {
                            itemList = itemList.FindAll(FindGr);
                        }
                        */

                        // add rows to data grid

                        grdGRList.Clear();
                        reloadGrDetail();

                        foreach (CargoExportItem item in itemList)
                        {
                            addnewRow(item);
                        }

                        grdGRList.Refresh();

                        // load data for other fields
                        if (itemList != null)
                        {
                            m_rowsCnt = itemList.Count;
                        }

                        /**
                         * lv.dat
                         * not use this way any more
                         */
                        /*
                        txtDocMt.Text = this.m_docMt.ToString();
                        txtDocM3.Text = this.m_docM3.ToString();
                        txtDocQty.Text = this.m_docQty.ToString();
                        txtActMt.Text = this.m_actMt.ToString();
                        txtActM3.Text = this.m_actM3.ToString();
                        txtActQty.Text = this.m_actQty.ToString();*/
                    }


                    #endregion
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

        private bool abortThread()
        {
            if (this.oThread != null)
            {
                if (this.isAlive == 2)
                {
                    oThread.Abort();
                    oThread.Join();
                    this.isAlive = 0;
                    return true;
                }
            }
            return false;
        }
        //QUANBTL 09-08-2012 fix G/R retrieve performance END 

        private void GetShippingNoteList()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                //if (!string.IsNullOrEmpty(txtArvlDateFrom.Text) &&
                //    !string.IsNullOrEmpty(txtArvlDateTo.Text))
                //{
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
                //}
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

        private string GetVslCallId()
        {
            string vslCallId = string.Empty;
            if (m_parm != null)
            {
                vslCallId = m_parm.Jpvc;
            }
            return vslCallId;
        }

        private void ReturnGRInfo()
        {
            int currRowIndex = grdGRList.CurrentRowIndex;
            string strGr = grdGRList.DataTable.Rows[currRowIndex][HEADER_GR].ToString();
            string strJpvc = grdGRList.DataTable.Rows[currRowIndex][HEADER_JPVC].ToString();
            string strSn = grdGRList.DataTable.Rows[currRowIndex][HEADER_SN].ToString();
            string strLorry = grdGRList.DataTable.Rows[currRowIndex][HEADER_LORRY].ToString();
            string strQty = grdGRList.DataTable.Rows[currRowIndex][HEADER_QTY].ToString();
            string strMt = grdGRList.DataTable.Rows[currRowIndex][HEADER_MT].ToString();
            string strM3 = grdGRList.DataTable.Rows[currRowIndex][HEADER_M3].ToString();
            string strTsptr = grdGRList.DataTable.Rows[currRowIndex][HEADER_HID_TSPTR].ToString();
            string strTsptrCompNm = grdGRList.DataTable.Rows[currRowIndex][HEADER_HID_TSPTCOMPNM].ToString();
            string strCgTpCd = grdGRList.DataTable.Rows[currRowIndex][HEADER_HID_CGTPCD].ToString();
            string strDelvTpCd = grdGRList.DataTable.Rows[currRowIndex][HEADER_HID_DELVTPCD].ToString();
            string strDelvTpNm = grdGRList.DataTable.Rows[currRowIndex][HEADER_DELVTPNM].ToString();
            string strDgYn = grdGRList.DataTable.Rows[currRowIndex][HEADER_HID_DGYN].ToString();
            string strDgStatCd = grdGRList.DataTable.Rows[currRowIndex][HEADER_HID_DGSTATCD].ToString();
            string strSpYn = grdGRList.DataTable.Rows[currRowIndex][HEADER_SPRYN].ToString();

            string strFnlOpeYn = grdGRList.DataTable.Rows[currRowIndex][HEADER_FNLOPEYN].ToString();
            string strHiFnlYn = grdGRList.DataTable.Rows[currRowIndex][HEADER_HIFNLYN].ToString();
            string strRhdlMode = grdGRList.DataTable.Rows[currRowIndex][HEADER_RHDLMODE].ToString();
            string strDmgYn = grdGRList.DataTable.Rows[currRowIndex][HEADER_DMGYN].ToString();
            string strShuYn = grdGRList.DataTable.Rows[currRowIndex][HEADER_SHUYN].ToString();
            string strPkgNo = grdGRList.DataTable.Rows[currRowIndex][HEADER_PKGNO].ToString();

            m_result = new CargoExportResult();
            m_result.GrNo = strGr;
            m_result.VslCallId = strJpvc;
            m_result.ShipgNoteNo = strSn;
            m_result.Lorry = strLorry;
            m_result.Qty = strQty;
            m_result.Mt = strMt;
            m_result.M3 = strM3;
            m_result.Tsptr = strTsptr;
            m_result.TsptrCompNm = strTsptrCompNm;
            m_result.CgTpCd = strCgTpCd;
            m_result.DelvTpCd = strDelvTpCd;
            m_result.DelvTpNm = strDelvTpNm;
            m_result.DgYn = strDgYn;
            m_result.DgStatCd = strDgStatCd;
            m_result.SpYn = strSpYn;
            m_result.PkgNo = strPkgNo;

            m_result.FnlOpeYn = strFnlOpeYn;
            m_result.HiFnlYn = strHiFnlYn;
            m_result.RhdlMode = strRhdlMode;
            m_result.DmgYn = strDmgYn;
            m_result.ShuYn = strShuYn;
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

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            if (this.validations(this.Controls))
            {
                //InitializeCboSNNo();
                if (!String.IsNullOrEmpty(txtGRNo.Text))
                    iFlag = 1;

                InitializeCboPaging();
                //F_Retrieve();

                //QUANBTL 09-08-2012 fix G/R retrieve performance START
                /*
                if (m_rowsCnt <= 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0058"));
                }
                 */
                //QUANBTL 09-08-2012 fix G/R retrieve performance END
            }
        }

        private void EstDateTextChanged(object sender, EventArgs e)
        {
            /*
            DateTimePicker mybutton = (DateTimePicker)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "txtArvlDateFrom":
                case "txtArvlDateTo":      
                    break;
            }*/
        }

        private void grdGRList_CurrentCellChanged(object sender, EventArgs e)
        {

            int currRowIndex = grdGRList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                txtActMt.Text = grdGRList.DataTable.Rows[currRowIndex][HEADER_MT].ToString();
                txtActM3.Text = grdGRList.DataTable.Rows[currRowIndex][HEADER_M3].ToString();
                txtActQty.Text = grdGRList.DataTable.Rows[currRowIndex][HEADER_QTY].ToString();
                txtDocMt.Text = grdGRList.DataTable.Rows[currRowIndex][HEADER_DOCMT].ToString();
                txtDocM3.Text = grdGRList.DataTable.Rows[currRowIndex][HEADER_DOCM3].ToString();
                txtDocQty.Text = grdGRList.DataTable.Rows[currRowIndex][HEADER_DOCQTY].ToString();
            }
        }

        //QUANBTL 09-08-2012 fix G/R retrieve performance START
        /*
         * Excute thread to reload data
         */
        private void excute()
        {

            Action<object> proc = delegate(object state)
            {
                //InitializeCboSNNo();
                //F_Retrieve();
                this.isAlive = 2;
            };

            if (this.InvokeRequired)
            {
                this.Invoke(proc, true);
            }
            else
            {
                proc.Invoke(null);
            }
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            // Make use of Timer to abort the loading DB thread
            if (abortThread())
            {
                this.changeControlEnabled(true);
            }

        }

        /**
         * lv.dat
         * add method for paging
         */ 

        private int getNumbPage(int iNumbRow)
        {
            if (iNumbRow == 0)
                return iNumbRow;

            int iNumbPage = iNumbRow / this.iNumbPerPage;
            if ((iNumbPage == 0 && iNumbRow > 0 ) || iNumbRow % this.iNumbPerPage != 0)
                iNumbPage++;

            return iNumbPage;
        }

        private void InitializeParameter()
        {
            cgExpParm = new CargoExportParm();

            cgExpParm.vslCallId = GetVslCallId();
            cgExpParm.shipgNoteNo = CommonUtility.GetComboboxSelectedValue(cboSNNo);

            if (txtGRNo.Text != null && !String.Empty.Equals(txtGRNo.Text))
            {
                cgExpParm.cgNo = txtGRNo.Text;
                iFlag = 1;
            }
            
            if (m_parm != null && !string.IsNullOrEmpty(m_parm.DelvTpCd))
            {
                //cgExpParm. = 
            }

            //Added by Chris 2016-03-08 Only load Reserved for finding GR on HHT
            if (m_parm != null && !string.IsNullOrEmpty(m_parm.Screenid))
            {
                if (m_parm.Screenid == "AC101")
                    cgExpParm.hhtFnlMode = "AC";
                if (m_parm.Screenid == "WC103")
                    cgExpParm.hhtFnlMode = "WC";
            }
            //End-------------------

            // Non- JPVC
            if (Constants.NONCALLID.Equals(m_parm.Jpvc))
            {
                cgExpParm.arrvDtFm = txtArvlDateFrom.Text;
                cgExpParm.arrvDtTo = txtArvlDateTo.Text;
            }


            /**
             * lv.dat
             * status filter and paging
             */
            //if (iFlag == 2)
            //    cgExpParm.stat = this.sStat;

            cgExpParm.stat = this.sStat;
            cgExpParm.numbPerPage = this.iNumbPerPage.ToString();
            cgExpParm.pageType = "GR";

            cargoItemList = new List<CargoExportItem>();
            cargoItemList.Clear();
        }

        private void InitializeCboPaging()
        {
            InitializeParameter();

            ICommonProxy proxy = new CommonProxy();
            ResponseInfo numbInfo = proxy.getCargoListNumbPage(cgExpParm);
            this.iTotalPage = getNumbPage((int)numbInfo.list[0]);

            cboPaging.Items.Clear();
            cboPaging.Text = "";

            for (int i = 1; i <= this.iTotalPage; i++)
                this.cboPaging.Items.Add(i);

            if (this.iTotalPage > 0)
            {
                cboPaging.SelectedIndex = 0;
            }
            else
                grdGRList.Clear();
        }

        private void getCargoItem(ICommonProxy proxy)
        {
            cargoItemList.Clear();

            //List<string> tempList = new List<string>();
            /**
             * lv.dat
             * TODO: fix later
             */
            //CommonUtility.InitializeCombobox(cboSNNo);

            ResponseInfo info = proxy.getCargoExportList(cgExpParm);

            for (int i = 0; i < info.list.Length; i++)
            {
                if (info.list[i] is CargoExportItem)
                {
                    CargoExportItem item = (CargoExportItem)info.list[i];
                    /*
                    if (!tempList.Contains(item.shipgNoteNo))
                    {
                        string descr = item.shipgNoteNo;
                        if (!string.IsNullOrEmpty(item.catgNm))
                        {
                            descr += "(" + item.catgNm + ")";
                        }
                        cboSNNo.Items.Add(new ComboboxValueDescriptionPair(item.shipgNoteNo, descr));
                        tempList.Add(item.shipgNoteNo);
                    }
                    */
                    cargoItemList.Add(item);
                    //if (!String.IsNullOrEmpty(txtGRNo.Text) && iFlag == 1)
                    //{
                    //    this.cboStatus.SelectedIndexChanged -= this.cboStatus_SelectedIndexChanged;
                    //    cboStatus.SelectedIndex = 0;
                    //    this.cboStatus.SelectedIndexChanged += this.cboStatus_SelectedIndexChanged;
                    //}
                }
            }
        }

        //private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.sStat = ((ComboBox)sender).Text;
        //    cgExpParm.stat = this.sStat;

        //    iFlag = 2;
        //    InitializeCboSNNo();
        //    InitializeCboPaging();
        //    F_Retrieve();
        //}

        private void cboPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.iCurrentPage = int.Parse(((ComboBox)sender).Text);
            cgExpParm.currentPage = this.iCurrentPage.ToString();

            if (cgExpParm != null)
            {
                ICommonProxy proxy = new CommonProxy();
                getCargoItem(proxy);

                F_Retrieve();
            }
        }

        private void executePaging(object sender, EventArgs e)
        {
            Button btnHandle = (Button)sender;
            switch (btnHandle.Name)
            {
                case "btnPrev":
                    if (string.IsNullOrEmpty(cboPaging.Text) || cboPaging.SelectedIndex <= 0)
                        return;
                    cboPaging.SelectedIndex--;
                    break;
                case "btnNext":
                    if (string.IsNullOrEmpty(cboPaging.Text) || cboPaging.SelectedIndex >= this.iTotalPage - 1)
                        return;
                    cboPaging.SelectedIndex++;
                    break;
                default:
                    break;
            }
        }

        private void HCM103_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                MessageBox.Show("Screen ID : " + this.Name + "\n\rScreen caption: " + this.Text, "Screen Information !");
            }
        }

        //QUANBTL 09-08-2012 fix G/R retrieve performance END
    }
}