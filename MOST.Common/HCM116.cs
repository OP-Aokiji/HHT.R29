using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Framework.Controls.Container;
using Framework.Common.Constants;
using Framework.Common.PopupManager;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using MOST.Client.Proxy.CommonProxy;
using Framework.Service.Provider.WebService.Provider;
using MOST.Common.CommonResult;
using MOST.Common.Utility;
using ZEBRAPRINTLib;

namespace MOST.Common
{
    public partial class HCM116 : TDialog, IPopupWindow
    {
        #region Local Variable
        private const string CRNL = "\r\n";
        private const int TEXT_LENGTH = 24;
        private int m_rowsCnt;
        public MOST.Common.CommonParm.CargoGatePassParm m_parm;
        private readonly String HEADER_NO = "No";
        private readonly String HEADER_JPVC = "JPVC";
        private readonly String HEADER_CGNO = "CgNo";
        private readonly String HEADER_GP = "GP No";
        private readonly String HEADER_LORRY = "Lorry";
        private readonly String HEADER_TRANSPORTER = "Transporter";
        private readonly String HEADER_DELV_MT = "MT";
        private readonly String HEADER_DELV_M3 = "M3";
        private readonly String HEADER_DELV_QTY = "Qty";
        private readonly String HEADER_RMK = "_RMK";
        private readonly String HEADER_CGINOUTCD = "_CGINOUTCD";
        private readonly String HEADER_SEQ = "_SEQ";
        #endregion

        delegate void Action();

        public HCM116()
        {
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            SettingPrinter();
            btnPrint.Enabled = false;
            CommonUtility.SetDTPWithinShift(tGPStart, tGPEnd);
            this.Closing += new CancelEventHandler(this.HCM116_Closing);
        }

        private void HCM116_Closing(object sender, CancelEventArgs e)
        {
            PrinterSettingsHistoryInfo.GetInstance().ComPort 
                = CommonUtility.GetComboboxSelectedValue((ComboBox)this.cboComPort);
            PrinterSettingsHistoryInfo.GetInstance().BaudRate 
                = int.Parse(((object)CommonUtility.GetComboboxSelectedValue
                ((ComboBox)this.cboBaudRate)).ToString());
        }

        private void InitializeDataGrid()
        {
            btnPrint.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0057");
            btnCancel.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0014");
            btnSearch.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0015");
            String[,] header = { { HEADER_NO, "30" }, { HEADER_JPVC, "90" }, { HEADER_CGNO, "85" }, { HEADER_GP, "65" }, { HEADER_LORRY, "60" }, { HEADER_TRANSPORTER, "60" }, { HEADER_DELV_MT, "60" }, { HEADER_DELV_M3, "60" }, { HEADER_DELV_QTY, "60" }, { HEADER_RMK, "0" }, { HEADER_CGINOUTCD, "0" }, { HEADER_SEQ, "0" } };
            grdData.setHeader(header);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (MOST.Common.CommonParm.CargoGatePassParm)parm;
            if (Constants.NONCALLID.Equals(m_parm.VslCallId))
            {
                rbtnNonJPVC.Checked = true;
            }
            else
            {
                txtJPVC.Text = m_parm.VslCallId;
            }
            txtGRBL.Text = m_parm.CgNo;
            if (m_parm.ArrInitGPNos != null && m_parm.ArrInitGPNos.Count == 1)
            {
                txtGP.Text = m_parm.ArrInitGPNos[0];
            }
            if (!string.IsNullOrEmpty(GetVslCallId()) ||
                !string.IsNullOrEmpty(txtGRBL.Text) ||
                !string.IsNullOrEmpty(txtGP.Text))
            {
                F_Search(true);
            }
            this.ShowDialog();
            return null;
        }

        public void F_Search(bool firstInit)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ICommonProxy proxy = new CommonProxy();
                CargoGatePassParm parm = new CargoGatePassParm();
                parm.vslCallId = GetVslCallId();
                if (!string.IsNullOrEmpty(txtGRBL.Text))
                {
                    parm.cgNo = txtGRBL.Text;
                }
                if (!string.IsNullOrEmpty(txtGP.Text))
                {
                    parm.gatePassNo = txtGP.Text;
                }
                if (!string.IsNullOrEmpty(tGPStart.Text))
                {
                    parm.startDt = tGPStart.Text;
                }
                if (!string.IsNullOrEmpty(tGPEnd.Text))
                {
                    parm.endDt = tGPEnd.Text;
                }

                ResponseInfo info = proxy.getCargoGatePassList(parm);

                grdData.Clear();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CargoGatePassItem)
                    {
                        int iNo = i + 1;
                        //DataRow newRow;
                        if (firstInit)
                        {
                            CargoGatePassItem item = (CargoGatePassItem)info.list[i];
                            if (m_parm.ArrInitGPNos != null && m_parm.ArrInitGPNos.Count > 0 &&
                                m_parm.ArrInitGPNos.Contains(item.gatePassNo))
                            {
                                DataRow newRow = grdData.NewRow();
                                newRow[HEADER_NO] = iNo.ToString();
                                newRow[HEADER_JPVC] = item.vslCallId;
                                newRow[HEADER_CGNO] = item.cgNo;
                                newRow[HEADER_GP] = item.gatePassNo;
                                newRow[HEADER_LORRY] = item.lorryNo;
                                newRow[HEADER_TRANSPORTER] = item.tsptr;
                                newRow[HEADER_DELV_MT] = item.wgt;
                                newRow[HEADER_DELV_M3] = item.msrmt;
                                newRow[HEADER_DELV_QTY] = item.pkgQty;
                                newRow[HEADER_RMK] = item.rmk;
                                newRow[HEADER_CGINOUTCD] = item.cgInOutCd;
                                newRow[HEADER_SEQ] = item.seq;
                                this.grdData.Add(newRow);
                                this.grdData.Select(0);
                            }
                        }
                        else
                        {
                            CargoGatePassItem item = (CargoGatePassItem)info.list[i];
                            DataRow newRow = grdData.NewRow();
                            newRow[HEADER_NO] = iNo.ToString();
                            newRow[HEADER_JPVC] = item.vslCallId;
                            newRow[HEADER_CGNO] = item.cgNo;
                            newRow[HEADER_GP] = item.gatePassNo;
                            newRow[HEADER_LORRY] = item.lorryNo;
                            newRow[HEADER_TRANSPORTER] = item.tsptr;
                            newRow[HEADER_DELV_MT] = item.wgt;
                            newRow[HEADER_DELV_M3] = item.msrmt;
                            newRow[HEADER_DELV_QTY] = item.pkgQty;
                            newRow[HEADER_RMK] = item.rmk;
                            newRow[HEADER_CGINOUTCD] = item.cgInOutCd;
                            newRow[HEADER_SEQ] = item.seq;
                            this.grdData.Add(newRow);
                            this.grdData.Select(0);
                        }
                    }
                }

                if (info.list != null)
                {
                    m_rowsCnt = info.list.Length;
                }
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

        public string GetVslCallId()
        {
            string vslCallId = string.Empty;
            if (rbtnJPVC.Checked)
            {
                vslCallId = txtJPVC.Text;
            }
            else if (rbtnNonJPVC.Checked)
            {
                vslCallId = Constants.NONCALLID;
            }
            return vslCallId;
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;

            switch (buttonName)
            {
                case "btnSearch":
                    F_Search(false);
                    if (m_rowsCnt <= 0)
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0058"));
                        txtJPVC.Focus();
                        txtJPVC.SelectAll();
                    }
                    break;

                case "btnPrint":
                    if (validations(this.Controls))
                    {
                        PrintSerialPort();
                        UpdateGPRemark();
                        //new Thread(new ThreadStart(
                        //    delegate()
                        //    {
                        //        PrintSerialPort();
                        //        UpdateGPRemark();
                        //    }
                        //)).Start();
                    }
                    break;

                case "btnCancel":
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    break;

                case "btnF1":
                    MOST.Common.CommonParm.SearchJPVCParm jpvcParm = new MOST.Common.CommonParm.SearchJPVCParm();
                    jpvcParm.Jpvc = txtJPVC.Text.Trim();
                    jpvcParm.IsWHChecker = "VSL";
                    SearchJPVCResult jpvcResultTmp = (SearchJPVCResult)PopupManager.instance.ShowPopup(new HCM101(), jpvcParm);
                    if (jpvcResultTmp != null)
                    {
                        txtJPVC.Text = jpvcResultTmp.Jpvc;
                    }
                    break;

                case "btnF2":
                    MOST.Common.CommonParm.GRBLListParm grParm = new MOST.Common.CommonParm.GRBLListParm();
                    grParm.CgNo = txtGRBL.Text;
                    GRBLListResult grResultTmp = (GRBLListResult)PopupManager.instance.ShowPopup(new HCM114(), grParm);
                    if (grResultTmp != null)
                    {
                        txtGRBL.Text = grResultTmp.CgNo;
                    }
                    break;
            }
        }

        private void SettingPrinter()
        {
            // Get printer settings history info
            PrinterSettingsHistoryInfo prevPrinterSettings = PrinterSettingsHistoryInfo.GetInstance();

            // COM port
            CommonUtility.InitializeCombobox(cboComPort);
            for (int i = 1; i < 10; i++)
            {
                cboComPort.Items.Add(new ComboboxValueDescriptionPair("COM" + i + ":", i.ToString()));
            }
            if (prevPrinterSettings != null && !string.IsNullOrEmpty(prevPrinterSettings.ComPort))
            {
                // Set previous COM port as default port
                PrinterSettingsHistoryInfo.GetInstance().ComPort = "COM9:";
                CommonUtility.SetComboboxSelectedItem(cboComPort, prevPrinterSettings.ComPort);
            } 
            else
            {
                // Default port is COM3.
                cboComPort.SelectedIndex = 9;
            }
            

            // Baud rate. 
            CommonUtility.InitializeCombobox(cboBaudRate);
            cboBaudRate.Items.Add("9600");
            cboBaudRate.Items.Add("19200");
            cboBaudRate.Items.Add("38400");
            cboBaudRate.Items.Add("57600");
            cboBaudRate.Items.Add("115200");
            if (prevPrinterSettings != null && prevPrinterSettings.BaudRate > 0)
            {
                // Set previous COM port as default port
                CommonUtility.SetComboboxSelectedItem(cboBaudRate, prevPrinterSettings.BaudRate.ToString());
            }
            else
            {   // Default rate is 19200.
                cboBaudRate.SelectedIndex = 2;
            }
        }

        public void PrintSerialPort()
        {
            string comPort = string.Empty;
            int baudRate = 0;
            ZebraPrintCtlClass ctrl = null;
            try
            {
                //Cursor.Current = Cursors.WaitCursor;

                // Get gate pass detail
                string strData = GetGatePassDetail();

                comPort = CommonUtility.GetComboboxSelectedValue(cboComPort);
                baudRate = CommonUtility.ParseInt(CommonUtility.GetComboboxSelectedValue(cboBaudRate));
                ctrl = new ZebraPrintCtlClass();
                ctrl.SerialConnection(comPort, baudRate);
                ctrl.Print(strData);
            }
            catch (Exception ex)
            {
                //ctrl.ShowLastError();
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                if (ctrl != null)
                {
                    ctrl.Close();
                }

                // Save printer settings histoy
                PrinterSettingsHistoryInfo.SetInstance(comPort, baudRate);

                //Cursor.Current = Cursors.Default;
            }
        }

        private string GetGatePassDetail()
        {
            string result = string.Empty;
            try
            {
                if (grdData.CurrentRowIndex > -1 && grdData.CurrentRowIndex < grdData.DataTable.Rows.Count)
                {
                    //Cursor.Current = Cursors.WaitCursor;

                    ICommonProxy proxy = new CommonProxy();
                    GatePassImportParm parm = new GatePassImportParm();
                    string strJPVC = grdData.DataTable.Rows[grdData.CurrentRowIndex][HEADER_JPVC].ToString();
                    string strGP = grdData.DataTable.Rows[grdData.CurrentRowIndex][HEADER_GP].ToString();
                    if (!string.IsNullOrEmpty(strJPVC) && !Constants.NONCALLID.Equals(strJPVC))
                    {
                        parm.jpvcNo = strJPVC;
                    }
                    if (!string.IsNullOrEmpty(strGP))
                    {
                        parm.gatePassNo = strGP;
                    }
                    ResponseInfo info = proxy.getGatePassImport(parm);
                    if (info != null && info.list != null && info.list.Length > 0 && info.list[0] is GatePassImportItem)
                    {
                        GatePassImportItem item = (GatePassImportItem)info.list[0];
                        result = GetPrintCommand(item);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                //Cursor.Current = Cursors.Default;
            }
            return result;
        }

        /// <summary>
        /// Update gatepass remark.
        /// </summary>
        private void UpdateGPRemark()
        {
            try
            {
                //Cursor.Current = Cursors.WaitCursor;

                int index = grdData.CurrentRowIndex;
                if (index > -1 && index < grdData.DataTable.Rows.Count)
                {
                    // Get keys of the current item
                    string strGPNo = grdData.DataTable.Rows[index][HEADER_GP].ToString();
                    string strCgNo = grdData.DataTable.Rows[index][HEADER_CGNO].ToString();
                    string strSeq = grdData.DataTable.Rows[index][HEADER_SEQ].ToString();
                    string strCgInOutCd = grdData.DataTable.Rows[index][HEADER_CGINOUTCD].ToString();

                    ICommonProxy proxy = new CommonProxy();
                    CargoGatePassItem item = new CargoGatePassItem();
                    item.rmk = txtRemark.Text;
                    item.gatePassNo = strGPNo;
                    item.cgNo = strCgNo;
                    item.seq = CommonUtility.ParseInt(strSeq);
                    item.cgInOutCd = strCgInOutCd;
                    item.CRUD = Constants.WS_UPDATE;
                    item.updUserId = UserInfo.getInstance().UserId;

                    Object[] obj = { item };
                    DataItemCollection dataCollection = new DataItemCollection();
                    dataCollection.collection = obj;

                    proxy.updateCargoGatePassRemark(dataCollection);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                //Cursor.Current = Cursors.Default;
            }
        }

        private string GetPrintCommand(GatePassImportItem item)
        {
            string result = string.Empty;
            try
            {
                if (item != null)
                {
                    StringBuilder strData = new StringBuilder();

                    // Using Label Vista to design gatepass
                    //------------------------- header ----------------------------
                    //strData.Append(CRNL + "! 0 200 200 930 1");
                    strData.Append(CRNL + "! 0 103 203 1000 4");
                    strData.Append(CRNL + "JOURNAL");       //  LABEL/JOURNAL
                    strData.Append(CRNL + "CONTRAST 0");
                    strData.Append(CRNL + "TONE 0");
                    strData.Append(CRNL + "SPEED 3");
                    strData.Append(CRNL + "PAGE-WIDTH 420");

                    //------------------------- content ----------------------------
                    // logo
                    strData.Append(CRNL + "PCX 86 1 !<JPB.pcx");
                    // title
                    strData.Append(CRNL + "T 7 1 32 31 BULK & BREAK BULK TERMINAL");
                    // GP No
                    strData.Append(CRNL + "T Tim05pt.cpf 0 200 83 G/P No: ");
                    strData.Append(CRNL + "T Tim09Bpt.cpf 0 255 78 {0}");

                    // title
                    strData.Append(CRNL + "T 5 0 88 109 GATE PASS SLIP");
                    // BL NO/GR NO 
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 144 BL NO/GR NO");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 144 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 115 143 {1}");
                    // DO/SN
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 164 DO/SN");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 164 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 115 163 {2}");
                    // VSL
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 184 VSL");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 184 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 115 183 {3}");
                    // JPVC
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 204 JPVC");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 204 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 115 203 {4}");
                    // POD/POL
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 224 POD/POL");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 224 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 115 223 {5}");
                    // HATCH NO - WHARF
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 244 HATCH NO");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 244 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 115 243 {6}");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 179 244 WHARF:");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 241 243 {7}");
                    // WH LOC
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 264 WH LOC");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 264 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 115 263 {8}");
                    // PKG TYPE - S/A - F/A
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 284 PKG TYPE");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 284 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 115 283 {9}");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 179 284 S/A:");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 208 283 {10}");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 269 284 F/A:");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 298 283 {11}");
                    // SHIPPER
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 304 SHIPPER");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 304 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 115 303 {12}");
                    // CONSIGNEE
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 324 CONSIGNEE");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 324 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 115 323 {13}");
                    // FINAL DEST
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 344 FINAL DEST.");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 344 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 115 343 {14}");
                    // CUSTOM/FZ APPR
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 364 CUSTOM/FZ APPR:");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 364 :");
                    strData.Append(CRNL + "T Tim07Bpt.cpf 0 144 361 {15}");
                    // RELEASE NO
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 384 RELEASE NO");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 124 384 :");
                    strData.Append(CRNL + "T Tim07Bpt.cpf 0 130 381 {16}");

                    // DG APPROVAL
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 404 DG APPROVAL");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 124 404 :");
                    strData.Append(CRNL + "T Tim07Bpt.cpf 0 130 401 {17}");
                    // LORRY/WAGON - TRANSPORTER
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 424 LORRY/WAGON");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 124 424 :");
                    strData.Append(CRNL + "T Tim09Bpt.cpf 0 130 419 {18}");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 250 424 T/PORTER:");  // Change position from (232, 424) to (250, 424) according to note #0070824 (issue #0030825)
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 328 424 {19}");      // Change position from (310, 424) to (328, 424) according to note #0070824 (issue #0030825)
                    // NO.TRIPS - OPR MODE
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 444 NO.TRIPS");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 124 444 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 130 443 {20}");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 237 444 OPR MODE:");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 320 443 {21}");
                    // PACKING NO
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 464 PACKING NO");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 124 464 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 130 463 {22}");
                    // COMMODITY
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 484 COMMODITY");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 124 484 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 130 483 {23}");
                    // CARGO STATUS
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 504 CARGO STATUS");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 124 504 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 130 503 {24}");
                    // DOC AMT
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 524 DOC AMT");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 524 :");
                    strData.Append(CRNL + "T Tim07Bpt.cpf 0 115 521 {25}");
                    strData.Append(CRNL + "T Tim07Bpt.cpf 0 213 521 {26}");
                    strData.Append(CRNL + "T Tim07Bpt.cpf 0 325 521 {27}");
                    // ACT AMT
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 544 ACT AMT");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 544 :");
                    strData.Append(CRNL + "T Tim07Bpt.cpf 0 115 541 {28}");
                    strData.Append(CRNL + "T Tim07Bpt.cpf 0 213 541 {29}");
                    strData.Append(CRNL + "T Tim07Bpt.cpf 0 325 541 {30}");
                    // DELIVER
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 564 DELIVER");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 564 :");
                    strData.Append(CRNL + "T Tim07Bpt.cpf 0 115 561 {31}");
                    strData.Append(CRNL + "T Tim07Bpt.cpf 0 213 561 {32}");
                    strData.Append(CRNL + "T Tim07Bpt.cpf 0 325 561 {33}");
                    // TOT DELV remove 8/5/2014 by Hien , Tung request
                    /*strData.Append(CRNL + "T Tim05pt.cpf 0 11 584 TOT DELV");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 584 :");
                    strData.Append(CRNL + "T Tim07Bpt.cpf 0 115 581 {34}");
                    strData.Append(CRNL + "T Tim07Bpt.cpf 0 213 581 {35}");
                    strData.Append(CRNL + "T Tim07Bpt.cpf 0 325 581 {36}");*/
                    // BALANCE remove 8/5/2014 by Hien , Tung request
                    /*strData.Append(CRNL + "T Tim05pt.cpf 0 11 604 BALANCE");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 604 :");
                    strData.Append(CRNL + "T Tim07Bpt.cpf 0 115 601 {37}");
                    strData.Append(CRNL + "T Tim07Bpt.cpf 0 213 601 {38}");
                    strData.Append(CRNL + "T Tim07Bpt.cpf 0 325 601 {39}");*/
                    // back up this part because change position after comment the TOT DELV and BALANCE
                    /*// CARGO DELIVERY
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 624 CARGO DELIVERY:");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 148 623 {40}");
                    // REMARK
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 644 REMARK");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 644 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 148 643 {41}");
                    // mention
                    strData.Append(CRNL + "T Tim04Ipt.cpf 0 21 685 Hereby affirm that cargos delivered to me/us are in apparent good order");
                    strData.Append(CRNL + "T Tim04Ipt.cpf 0 21 705 and condition. I/We also acknowledge that the Johor Port Berhad is not ");
                    strData.Append(CRNL + "T Tim04Ipt.cpf 0 21 725 responsible for the contents or condition thereof.");
                    strData.Append(CRNL + "T Tim04Ipt.cpf 0 21 745 Dengan ini mengesahkan bahawa kargo yang diserah kepada saya/kami");
                    strData.Append(CRNL + "T Tim04Ipt.cpf 0 21 765 adalah di dalam kedudukan dan keadaan baik pada zahirnya.");
                    strData.Append(CRNL + "T Tim04Ipt.cpf 0 21 785 Saya/kami juga maklum bahawa Johor Port Berhad tidak");
                    strData.Append(CRNL + "T Tim04Ipt.cpf 0 21 805 bertanggungjawab terhadap isi kandungan atau keadaannya.");
                    // CONFIRMED BY
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 845 CONFIRMED BY");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 128 845 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 135 844 {42}");
                    // DATE/TIME
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 865 DATE/TIME");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 128 865 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 135 864 {43}");
                    // PRINTED BY
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 885 PRINTED BY");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 128 885 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 135 884 {44}");
                    // DATE/TIME
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 905 DATE/TIME");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 128 905 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 135 904 {45}");
                    // DISCLAIMER
                    strData.Append(CRNL + "T Tim05pt.cpf 0 153 973 DISCLAIMER");
                    strData.Append(CRNL + "T Tim04Ipt.cpf 0 21 993 This transaction is subject to JPB's Terms and Conditions of Business (TCB).");
                    strData.Append(CRNL + "T Tim04Ipt.cpf 0 21 1013 This slip is computer generated and no signature is required.");
                    //------------------------- footer ----------------------------
                    strData.Append(CRNL + "PRINT" + CRNL);*/
                    //here the new position
                    // CARGO DELIVERY
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 584 CARGO DELIVERY:");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 148 583 {40}");
                    // REMARK
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 604 REMARK");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 108 604 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 148 603 {41}");
                    // mention
                    strData.Append(CRNL + "T Tim04Ipt.cpf 0 21 645 Hereby affirm that cargos delivered to me/us are in apparent good order");
                    strData.Append(CRNL + "T Tim04Ipt.cpf 0 21 665 and condition. I/We also acknowledge that the Johor Port Berhad is not ");
                    strData.Append(CRNL + "T Tim04Ipt.cpf 0 21 685 responsible for the contents or condition thereof.");
                    strData.Append(CRNL + "T Tim04Ipt.cpf 0 21 705 Dengan ini mengesahkan bahawa kargo yang diserah kepada saya/kami");
                    strData.Append(CRNL + "T Tim04Ipt.cpf 0 21 725 adalah di dalam kedudukan dan keadaan baik pada zahirnya.");
                    strData.Append(CRNL + "T Tim04Ipt.cpf 0 21 745 Saya/kami juga maklum bahawa Johor Port Berhad tidak");
                    strData.Append(CRNL + "T Tim04Ipt.cpf 0 21 765 bertanggungjawab terhadap isi kandungan atau keadaannya.");
                    // CONFIRMED BY
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 805 CONFIRMED BY");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 128 805 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 135 804 {42}");
                    // DATE/TIME
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 825 DATE/TIME");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 128 825 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 135 824 {43}");
                    // PRINTED BY
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 845 PRINTED BY");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 128 845 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 135 844 {44}");
                    // DATE/TIME
                    strData.Append(CRNL + "T Tim05pt.cpf 0 11 865 DATE/TIME");
                    strData.Append(CRNL + "T Tim05pt.cpf 0 128 865 :");
                    strData.Append(CRNL + "T Tim05Bpt.cpf 0 135 864 {45}");
                    // DISCLAIMER
                    strData.Append(CRNL + "T Tim05pt.cpf 0 153 893 DISCLAIMER");
                    strData.Append(CRNL + "T Tim04Ipt.cpf 0 21 913 This transaction is subject to JPB's Terms and Conditions of Business (TCB).");
                    strData.Append(CRNL + "T Tim04Ipt.cpf 0 21 933 This slip is computer generated and no signature is required.");
                    //------------------------- footer ----------------------------
                    strData.Append(CRNL + "PRINT" + CRNL);


                    //=============================== Binding data ===========================================
                    string serverTime = CommonUtility.GetCurrentServerTime();

                    // Document amount
                    string strDocMt = CommonUtility.ToStringNumber(item.grossTot);
                    string strDocM3 = CommonUtility.ToStringNumber(item.cumulTot);
                    string strDocQty = CommonUtility.ToStringNumber(item.outQty);

                    // Actual amount
                    string strActMt = CommonUtility.ToStringNumber(item.actMt);
                    string strActM3 = CommonUtility.ToStringNumber(item.actM3);
                    string strActQty = CommonUtility.ToStringNumber(item.actQty);

                    // GP (delivery) amount
                    string strDelvMt = CommonUtility.ToStringNumber(item.wgt);
                    string strDelvM3 = CommonUtility.ToStringNumber(item.msrmt);
                    string strDelvQty = CommonUtility.ToStringNumber(item.pkgQty);

                    // Accmulative total delivered amount
                    string strAccDelvMt = CommonUtility.ToStringNumber(item.totDelvMt);
                    string strAccDelvM3 = CommonUtility.ToStringNumber(item.totDelvM3);
                    string strAccDelvQty = CommonUtility.ToStringNumber(item.totDelvQty);

                    // Balance amount
                    string strBalMt = CommonUtility.ToStringNumber(item.balMt); 
                    string strBalM3 = CommonUtility.ToStringNumber(item.balM3);
                    string strBalQty = CommonUtility.ToStringNumber(item.balQty);
                    /*
                    double balMt = 0;
                    double balM3 = 0;
                    int balQty = 0;
                    double docMt = CommonUtility.ParseDouble(strDocMt);
                    double docM3 = CommonUtility.ParseDouble(strDocM3);
                    int docQty = CommonUtility.ParseInt(strDocQty);
                    double actMt = CommonUtility.ParseDouble(strActMt);
                    double actM3 = CommonUtility.ParseDouble(strActM3);
                    int actQty = CommonUtility.ParseInt(strActQty);
                    double accDelvMt = CommonUtility.ParseDouble(strAccDelvMt);
                    double accDelvM3 = CommonUtility.ParseDouble(strAccDelvM3);
                    int accDelvQty = CommonUtility.ParseInt(strAccDelvQty);
                    string catgCd = item.catgCd;
                    string delvTpCd = item.delvTpCd;
                    // Export/Transhipment case
                    if ("E".Equals(catgCd) || "T".Equals(catgCd))
                    {
                        // Balance = Actual amount - accumulative total delivered amount
                        balMt = actMt - accDelvMt;
                        balM3 = actM3 - accDelvM3;
                        balQty = actQty - accDelvQty;
                    }
                    // Import case
                    else if ("I".Equals(catgCd))
                    {
                        // Indirect(I)
                        if ("I".Equals(delvTpCd))
                        {
                            // Balance = Actual amount - accumulative total delivered amount
                            balMt = actMt - accDelvMt;
                            balM3 = actM3 - accDelvM3;
                            balQty = actQty - accDelvQty;
                        }
                        // Direct(D)/Both(B)
                        else
                        {
                            // Balance = Max(Actual amount, document amount) - accumulative total delivered amount
                            double maxMt = docMt > actMt ? docMt : actMt;
                            double maxM3 = docM3 > actM3 ? docM3 : actM3;
                            int maxQty = docQty > actQty ? docQty : actQty;
                            balMt = maxMt - accDelvMt;
                            balM3 = maxM3 - accDelvM3;
                            balQty = maxQty - accDelvQty;
                        }
                    }*/


                    string bl_gr = string.Empty;
                    string do_sn = string.Empty;
                    string pod_pol = CommonUtility.ToString(item.portOfDis) + "/" + CommonUtility.ToString(item.portOfLoad);
                    if (!string.IsNullOrEmpty(item.grNo) || !string.IsNullOrEmpty(item.sn))
                    {
                        bl_gr = CommonUtility.ToString(item.grNo);
                        do_sn = CommonUtility.ToString(item.sn);
                    }
                    else if (!string.IsNullOrEmpty(item.blNo) || !string.IsNullOrEmpty(item.doNo))
                    {
                        bl_gr = CommonUtility.ToString(item.blNo);
                        do_sn = CommonUtility.ToString(item.doNo);
                    }
                    string[] parm = { 
                        CommonUtility.ToString(item.gatePassNo), 
                        bl_gr,
                        do_sn,
                        CommonUtility.CutText(item.vslName, TEXT_LENGTH),
                        CommonUtility.ToString(item.jpvcNo),
                        pod_pol, 
                        CommonUtility.ToString(item.hatchNo),
                        CommonUtility.ToString(item.wharf),
                        CommonUtility.ToString(item.whLoc),
                        CommonUtility.ToString(item.pkgTpCd),
                        CommonUtility.ToString(item.shipgAgnt),
                        CommonUtility.ToString(item.fwrAgnt),
                        CommonUtility.CutText(item.shprNm, TEXT_LENGTH),
                        CommonUtility.CutText(item.cnsneNm, TEXT_LENGTH),
                        CommonUtility.CutText(item.finalDest, TEXT_LENGTH),
                        CommonUtility.ToString(item.custAppr),
                        CommonUtility.ToString(item.releaseNo),  // fix issue 0031208
                        CommonUtility.ToString(item.dgApproval),
                        CommonUtility.ToString(item.lorryNo),
                        CommonUtility.ToString(item.tsptr),
                        CommonUtility.ToString(item.noTrips),
                        CommonUtility.ToString(item.tsptTpCd),
                        CommonUtility.ToString(item.packingNo),
                        CommonUtility.CutText(item.commodity, TEXT_LENGTH),
                        CommonUtility.ToString(item.statCd),
                        strDocMt,
                        strDocM3,
                        strDocQty,
                        strActMt,
                        strActM3,
                        strActQty,
                        strDelvMt,
                        strDelvM3,
                        strDelvQty,
						strAccDelvMt,
                        strAccDelvM3,
                        strAccDelvQty,
                        //balMt.ToString(),
                        //balM3.ToString(),
                        //balQty.ToString(),
                        strBalMt,
                        strBalM3,
                        strBalQty,
                        CommonUtility.ToString(item.cgDelivery),
                        CommonUtility.CutText(txtRemark.Text, TEXT_LENGTH),
                        item.updUserId,
                        item.updDt,
                        UserInfo.getInstance().UserId,
                        serverTime
                    };
                    result = string.Format(strData.ToString(), parm);
                    //========================================================================================

                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            return result;
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
                    break;
            }
        }

        private void OnCheckRadioButton()
        {
            if (rbtnJPVC.Checked)
            {
                txtJPVC.Enabled = true;
                btnF1.Enabled = true;
            }
            else if (rbtnNonJPVC.Checked)
            {
                txtJPVC.Enabled = false;
                btnF1.Enabled = false;
            }
        }

        private void grdData_CurrentCellChanged(object sender, EventArgs e)
        {
            int index = grdData.CurrentRowIndex;
            if (index > -1 && index < grdData.DataTable.Rows.Count)
            {
                btnPrint.Enabled = true;

                string strRmk = grdData.DataTable.Rows[index][HEADER_RMK].ToString();
                txtRemark.Text = strRmk;
            }
        }
    }
}