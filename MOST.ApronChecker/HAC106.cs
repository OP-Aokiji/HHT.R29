using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Framework.Common.Constants;
using Framework.Common.ExceptionHandler;
using Framework.Common.Helper;
using Framework.Common.PopupManager;
using Framework.Common.ResourceManager;
using Framework.Common.UserInformation;
using Framework.Controls.Container;
using Framework.Service.Provider.WebService.Provider;
using MOST.ApronChecker.Parm;
using MOST.ApronChecker.Result;
using MOST.Client.Proxy.ApronCheckerProxy;
using MOST.Client.Proxy.CommonProxy;
using MOST.Common;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.Common.Utility;
using MOST.WHChecker;
using MOST.WHChecker.Parm;
using MOST.WHChecker.Result;

namespace MOST.ApronChecker
{
    public partial class HAC106 : TForm, IPopupWindow
    {
        #region Local Variable

        ResponseInfo m_cgDischargingInfo;
        private HAC106Result m_result;
        private HAC106Parm m_parm;
        private HWC101001Result m_resLocWh;
        private HWC101001Result m_resLocDmg;
        private CargoDischargingItem m_item;
        private String[] m_bbkHatchList;
        private String[] m_dbkHatchList;

        private HAC106001 selectPanel = new HAC106001();
        private Dictionary<string, BLListResult> blList = new Dictionary<string, BLListResult>();

        //QUANBTL D/C performance START

        private Thread cgLoadThread;
        private Thread cdMstrLoadThread;
        private int isAliveCgLoadThread;
        private int isAliveCdMstrLoadThread;
        private CargoDischargingParm cgDchrgeParm;
        private CargoDischargingParm cgCdMstrParm;
        Dictionary<string, string> delvTpHash;
        private string itemDelvTpCd;
        private bool itemDelvTpCdLoaded;
        private HAC106002 pnlDischargingDmg = new HAC106002();
        private bool itemCgTpCdLoaded;
        private string oldLocID = string.Empty;
        private string oldMt = string.Empty;
        private string oldM3 = string.Empty;
        private string oldQty = string.Empty;
        private HWC101001Result old_M_resLocDmg;

        //QUANBTL D/C performance END

        #endregion

        public HAC106()
        {
            InitializeComponent();

            selectPanel.Visible = false;
            selectPanel.Location = new Point(btnSelect.Left, btnSelect.Top - selectPanel.Height);
            //selectPanel.cboBL.SelectedIndexChanged += new EventHandler(cboBL_SelectedIndexChanged);
            this.Controls.Add(selectPanel);

            //QUANBTL 09-10-2012 0036484 START
            this.pnlDischargingDmg.Visible = false;
            this.pnlDischargingDmg.Location = new Point(this.btnGP.Left, this.btnGP.Top
                - this.pnlDischargingDmg.Height);
            this.Controls.Add((Control)this.pnlDischargingDmg);
            //QUANBTL 09-10-2012 0036484 END


            this.initialFormSize();
            CommonUtility.SetDTPWithinShift(txtStartTime, txtEndTime);

            ////////////////////////////////////////////////////////////////////////////
            //// Making button & checkbox text be multiline
            //WndProcHooker.MakeButtonMultiline(this.btnPrint);
            WndProcHooker.MakeCheckboxMultiline(this.chkOverLanded);
            ////////////////////////////////////////////////////////////////////////////

            List<string> controlNames = new List<string>();
            controlNames.Add(cboHatch.Name);
            controlNames.Add(txtPkgNo.Name);
            controlNames.Add(txtPkgTp.Name);
            controlNames.Add(cboOprMode.Name);
            controlNames.Add(txtStartTime.Name);
            controlNames.Add(txtEndTime.Name);
            controlNames.Add(rbtnDirect.Name);
            controlNames.Add(rbtnIndirect.Name);
            controlNames.Add(txtLorryNo.Name);
            controlNames.Add(txtLorryMT.Name);
            controlNames.Add(txtLorryM3.Name);
            controlNames.Add(txtLorryQty.Name);
            controlNames.Add(txtWHMT.Name);
            controlNames.Add(txtWHM3.Name);
            controlNames.Add(txtWHQty.Name);
            controlNames.Add(txtWHLoc.Name);
            /*
            controlNames.Add(txtDmgMT.Name);
            controlNames.Add(txtDmgM3.Name);
            controlNames.Add(txtDmgQty.Name);
            controlNames.Add(txtDmgLoc.Name);
            */
            controlNames.Add(chkFinal.Name);
            controlNames.Add(chkOverLanded.Name);
            this.AddOnChangeHandlerToInputControls(this.Controls, controlNames);

            //QUANBTL D/C performance START
            this.Closing += new CancelEventHandler(this.HAC106_Closing);
            //QUANBTL D/C performance END
        }

        private void cboBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string blNo = CommonUtility.GetComboboxSelectedValue(selectPanel.cboBL);
            //if (!String.IsNullOrEmpty(blNo) && blList.ContainsKey(blNo))
            //    selectPanel.txtConsignee.Text = String.Format("{0}{1}",
            //                                                  String.IsNullOrEmpty(blList[blNo].CnsneeCd)
            //                                                      ? String.Empty
            //                                                      : (blList[blNo].CnsneeCd + " - "),
            //                                                  blList[blNo].CnsneeNm);
            //else
            //    selectPanel.txtConsignee.Text = String.Empty;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            selectPanel.Visible = !selectPanel.Visible;

            if (selectPanel.Visible)
            {                
                selectPanel.BringToFront();
                btnSelect.Text = "OK";
                btnConfirm.Enabled = btnCancel.Enabled = false;
                selectPanel.jPVC = m_item.vslCallId;
            }
            else
            {
                if (m_parm.BlInfo != null)
                {
                    if (m_parm.BlInfo.Bl != selectPanel.txtBL.Text)
                    {
                        if (String.IsNullOrEmpty(selectPanel.txtBL.Text))
                        {
                            btnSelect.Text = "B/L »";
                            btnConfirm.Enabled = btnCancel.Enabled = true;
                            return;
                        }
                        if (!ValidateBL())
                        {
                            selectPanel.Visible = !selectPanel.Visible;
                            return;
                        }
                    }
                }

                btnSelect.Text = "B/L »";
                btnConfirm.Enabled = btnCancel.Enabled = true;

                if (m_parm.BlInfo != null)
                {
                    if (m_parm.BlInfo.Bl == selectPanel.txtBL.Text)
                        return;  // This BL is currently selected; no need to reload. 

                    if (this.IsDirty)  // If form is dirty, ask user to save changes before switch to other GRs. 
                    {
                        DialogResult dr = MessageBox.Show(String.Format("Do you want to save changes you made to {0}? ", m_parm.BlInfo.Bl),
                                                          "Apron Checker",
                                                          MessageBoxButtons.YesNoCancel,
                                                          MessageBoxIcon.Question,
                                                          MessageBoxDefaultButton.Button1);
                        switch (dr)
                        {
                            case DialogResult.Cancel:
                                return;  // User clicked Cancel. Return to the current GR, don't load the new GR info. 
                            case DialogResult.Yes:
                                if (!Confirm())  // If cannot save, then process as if user clicked Cancel button. 
                                    return;
                                break;
                        }
                    }
                }

                m_parm.BlInfo.Bl = selectPanel.txtBL.Text;

                // Set form title
                string strTitle = "A/C - D/C Confirm - Loading....";
                this.Text = strTitle;

                GetCargoDischargingList(false);
            }
        }

        private void LoadBLs()
        {
            /*
            // Clear old items if existing
            CommonUtility.InitializeCombobox(selectPanel.cboBL);

            ICommonProxy proxy = new CommonProxy();
            CargoImportParm parm = new CargoImportParm();
            parm.vslCallId = m_parm.VslCallId;
            parm.hhtFnlMode = Constants.FINAL_MODE_DSFN;
            ResponseInfo info = proxy.getCargoImportList(parm);

            blList.Clear();
            for (int i = 0; i < info.list.Length; i++)
            {
                CargoImportItem item = info.list[i] as CargoImportItem;
                if (item == null) continue;
                if (!blList.ContainsKey(item.blNo))
                {
                    string descr = item.blNo;
                    if (!String.IsNullOrEmpty(item.catgNm))
                        descr += " (" + item.catgNm + ")";
                    selectPanel.cboBL.Items.Add(new ComboboxValueDescriptionPair(item.blNo, descr));

                    BLListResult bl = new BLListResult();
                    bl.VslCallId = item.vslCallId;
                    bl.Bl = item.blNo;
                    bl.DoNo = item.doNo;
                    bl.FwrAgnt = item.fwrAgnt;
                    bl.Mt = item.mt.ToString();
                    bl.M3 = item.m3.ToString();
                    bl.Qty = item.qty.ToString();
                    bl.CgTpCd = item.cgTpCd;
                    bl.FnlOpeYn = item.fnlOpeYn;
                    bl.FnlDelvYn = item.fnlDelvYn;
                    bl.CnsneeCd = item.cnsneeCd;
                    bl.CnsneeNm = item.cnsneeNm;
                    blList[item.blNo] = bl;
                }
            }

            CommonUtility.SetComboboxSelectedItem(selectPanel.cboBL, m_item != null ? m_item.blNo : String.Empty);
           */
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            this.m_parm = (HAC106Parm)parm;
            InitializeDoc();

            bool bClose = false;
            if (m_item != null)
            {
                // Check if this cargo was already discharged as a final operation.
                if ("Y".Equals(m_item.fnlDis))
                {
                    CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC106_0001"));
                    bClose = true;
                }
            }
            if (bClose)
            {
                this.Close();
            }
            else
            {
                this.ShowDialog();
            }

            return m_result;
        }

        private void InitializeDoc()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.itemDelvTpCdLoaded = false;
                this.itemCgTpCdLoaded = false;
                this.GetCargoDischargingList(true);
                this.SetPrimaryPkgType();
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

        private void SetPrimaryPkgType()
        {
            // Only Break Bulk case should input Package Type.
            if (m_item != null)
            {
                //this.txtPkgTp.isMandatory = Constants.STRING_BBK.Equals(m_item.cgTpCd);
            }

        }

        private void ClearCtrlValues()
        {
            this.chkFinal.Checked = false;
            this.chkOverLanded.Checked = false;
            this.txtPkgNo.Text = string.Empty;
            this.txtWHLoc.Text = string.Empty;
            this.txtRemark.Text = string.Empty;
            this.txtLorryNo.Text = string.Empty;
            this.m_result = (HAC106Result)null;
            this.m_resLocDmg = (HWC101001Result)null;
            this.m_resLocWh = (HWC101001Result)null;
            this.old_M_resLocDmg = (HWC101001Result)null;
            this.pnlDischargingDmg.m_resLocDmg = (HWC101001Result)null;
            this.pnlDischargingDmg.m_parm = (HAC106Parm)null;
            this.pnlDischargingDmg.txtDmgLoc.Text
                = this.pnlDischargingDmg.txtDmgM3.Text
                = this.pnlDischargingDmg.txtDmgMT.Text
                = this.pnlDischargingDmg.txtDmgQty.Text
                = this.oldLocID
                = this.oldM3
                = this.oldMt
                = this.oldQty
                = this.lblDcDmgStt.Text
                = string.Empty;
            this.IsDirty = true;
        }

        //QUANBTL D/C performance START

        /*Closing event
         * Abort threads upon closing to ensure thread safety
         */
        private void HAC106_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            abortThread();
        }

        /*Abort threads
         * Abort threads to ensure thread safety
         */
        private void abortThread()
        {
            if (this.cdMstrLoadThread != null)
            {
                if (this.isAliveCdMstrLoadThread == 2)
                {
                    cdMstrLoadThread.Abort();
                    cdMstrLoadThread.Join();
                    this.isAliveCdMstrLoadThread = 0;
                }
            }

            if (this.cgLoadThread != null)
            {
                if (this.isAliveCgLoadThread == 2)
                {
                    cgLoadThread.Abort();
                    cgLoadThread.Join();
                    this.isAliveCgLoadThread = 0;
                }
            }
        }

        /*Start threads
         * Start threads upon called
         */
        private void threadStart(bool isFirstInit)
        {
            if (isFirstInit)
            {
                this.cdMstrLoadThread.Start();
                this.isAliveCdMstrLoadThread = 1;
            }
            this.cgLoadThread.Start();
           
            this.isAliveCgLoadThread = 1;
            this.itemDelvTpCdLoaded = false;
            this.itemCgTpCdLoaded = false;
        }

        /*Disable controls to ensure thread safety
         */
        private void enableControls(bool isEnable)
        {
            this.btnConfirm.Enabled = isEnable;
            this.btnSelect.Enabled = isEnable;
        }

        /*Load Code Master data
         * Excutable job for threading 
         */
        private void loadCdMstr()
        {
            ResponseInfo info;
            IApronCheckerProxy proxy = new ApronCheckerProxy();

            info = proxy.getCargoDischargingList(this.cgCdMstrParm);

            #region " CodeMasterListItem1/OperationSetItem"
            if (info != null && info.list.Length > 0)
            {

                Action<object> proc = delegate(object state)
                                              {
                                                  // Hatch, Operation Mode, Delivery Type info
                                                  CommonUtility.InitializeCombobox(cboHatch);
                                                  CommonUtility.InitializeCombobox(cboOprMode);
                                                  //this.cboHatch.Items.Clear();
                                                  //this.cboOprMode.Items.Clear();
                                                  for (int i = 0; i < info.list.Length; i++)
                                                  {
                                                      // Hatch
                                                      OperationSetItem osi = info.list[i] as OperationSetItem;
                                                      if (osi != null)
                                                          cboHatch.Items.Add(new ComboboxValueDescriptionPair(osi.hatchNo, osi.hatchNo));
                                                      //cboHatch.Items.Add(osi.hatchNo.ToString());
                                                      /*
                                                       * Operation Mode
                                                      TSPTTP	CV	Conveyor	
                                                      TSPTTP	LR	Lorry	
                                                      TSPTTP	WG	Wagon	
                                                      */
                                                      CodeMasterListItem1 cmli = info.list[i] as CodeMasterListItem1;
                                                      if (cmli == null && info.list[i] is CodeMasterListItem)
                                                          cmli = CommonUtility.ToCodeMasterListItem1(info.list[i] as CodeMasterListItem);
                                                      if (cmli != null)
                                                      {
                                                          switch (cmli.mcd)
                                                          {
                                                              case "TSPTTP":
                                                                  cboOprMode.Items.Add(new ComboboxValueDescriptionPair(cmli.scd, cmli.scdNm));
                                                                  //cboOprMode.Items.Add(cmli.scdNm.ToString());
                                                                  break;
                                                              case "DELVTP":
                                                                  this.delvTpHash[cmli.scd] = cmli.scdNm;
                                                                  break;
                                                              case "CGTPNLQ":
                                                                  cboCargoType.Items.Add(new ComboboxValueDescriptionPair(cmli.scd, cmli.scd));
                                                                  break;
                                                          }
                                                      }
                                                  }

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

            GetHatchList();

            #endregion

            this.isAliveCdMstrLoadThread = 2;
        }

        /*Load Cargo D/C list
         *Excutable job for threading 
         */
        private void loadCgItem()
        {
            ResponseInfo info;
            IApronCheckerProxy proxy = new ApronCheckerProxy();

            info = proxy.getCargoDischargingList(this.cgDchrgeParm);

            // Doc, Balance info)))
            #region " CargoDischargingItem"
            this.m_item = info.list[0] as CargoDischargingItem;

            Action<CargoDischargingItem> func = delegate(CargoDischargingItem item)
            {

                // D/O document amount
                txtDOQtyD.Text = item.dQty.ToString();
                txtDOMtD.Text = item.dMt.ToString();
                txtDOM3D.Text = item.dM3.ToString();
                txtDOQtyI.Text = item.iQty.ToString();
                txtDOMtI.Text = item.iMt.ToString();
                txtDOM3I.Text = item.iM3.ToString();

                // Package type
                txtPkgTp.Text = item.repkgTypeCd;

                // Clearance
                DisplayClearance(item.custMode);

                // Operation Mode
                CommonUtility.SetComboboxSelectedItem(cboOprMode, item.tsptTpCd);

                // Delivery Mode
                /*
                if (this.delvTpHash.ContainsKey(item.delvTpCd))
                    txtDelvMode.Text = this.delvTpHash[item.delvTpCd];
                 */
                this.itemDelvTpCd = item.delvTpCd;
                if ("D".Equals(item.delvTpCd))
                    rbtnDirect.Checked = true;
                else if ("I".Equals(item.delvTpCd))
                    rbtnIndirect.Checked = true;

                // Document, Balance
                txtDocM3.Text = item.m3.ToString();
                txtDocMT.Text = item.mt.ToString();
                txtDocQty.Text = item.qty.ToString();
                txtBalanceQty.Text = item.balQty.ToString();
                txtBalanceMT.Text = item.balMt.ToString();
                txtBalanceM3.Text = item.balM3.ToString();

                // Actual
                txtLorryMT.Text = txtLorryM3.Text = txtLorryQty.Text = String.Empty;
                // Set mandatory items (fix issue 0022089)
                //txtLorryMT.isMandatory = true;
                //txtLorryM3.isMandatory = item.cgTpCd == "BBK" && item.opDelvTpCd == "I";
                //txtLorryQty.isMandatory = item.cgTpCd == "BBK";

                // Set form title
                string strTitle = "A/C - D/C Confirm";
                if (item != null && item.grNo != null)
                    strTitle = strTitle + " - " + item.grNo;
                this.Text = strTitle;
            };
            if (this.InvokeRequired)
                this.Invoke(func, m_item);
            else
                func.Invoke(m_item);

            #endregion

            this.isAliveCgLoadThread = 2;
        }

        /*Load data for HAC106
         * Main method for loading data
         */
        private void GetCargoDischargingList(bool isFirstInit)
        {

            Action<bool> setFormEnabledAction = delegate(bool isEnabled)
                                                {
                                                    this.Enabled = isEnabled;
                                                };
            if (this.InvokeRequired)
                this.Invoke(setFormEnabledAction, false);
            else
                setFormEnabledAction.Invoke(false);

            try
            {
                IApronCheckerProxy proxy = new ApronCheckerProxy();
                //CargoDischargingParm parm = new CargoDischargingParm();

                #region Parm for getting Doc, Balance, Hatch, OPR Mode info

                this.cgCdMstrParm = new CargoDischargingParm();

                this.cgCdMstrParm.vslCallId = m_parm.BlInfo.VslCallId;
                this.cgCdMstrParm.shftDt = CommonUtility.FormatDateYYYYMMDD(UserInfo.getInstance().Workdate);
                this.cgCdMstrParm.shftId = UserInfo.getInstance().Shift;
                if (m_parm.BlInfo != null)
                {
                    this.cgCdMstrParm.blNo = m_parm.BlInfo.Bl;
                    this.cgCdMstrParm.cgTpCd = m_parm.BlInfo.CgTpCd;
                    //checkOGAstatus(m_parm.BlInfo.VslCallId);
                }

                this.cgDchrgeParm = new CargoDischargingParm();

                this.cgDchrgeParm.vslCallId = m_parm.BlInfo.VslCallId;
                this.cgDchrgeParm.shftDt = CommonUtility.FormatDateYYYYMMDD(UserInfo.getInstance().Workdate);
                this.cgDchrgeParm.shftId = UserInfo.getInstance().Shift;
                if (m_parm.BlInfo != null)
                {
                    this.cgDchrgeParm.blNo = m_parm.BlInfo.Bl;
                    this.cgDchrgeParm.cgTpCd = m_parm.BlInfo.CgTpCd;
                }

                #endregion

                // load CodeMasterListItem1/OperationSetItem
                if (isFirstInit)
                {
                    delvTpHash = new Dictionary<string, string>();

                    this.cgCdMstrParm.hhtFlags = isFirstInit
                        ? Constants.LOAD_CODE_MATER_FLAG : String.Empty;
                    this.cdMstrLoadThread = new Thread(new ThreadStart(loadCdMstr));
                }

                // load CodeDischargingItem
                this.cgDchrgeParm.hhtFlags = isFirstInit
                    ? Constants.LOAD_CARGO_FLAG : String.Empty;
                this.cgLoadThread = new Thread(new ThreadStart(loadCgItem));

                // disable controls to ensure thread safety
                this.enableControls(false);

                /*
                // cgType
                cboCargoType.Items.Add(
                    new ComboboxValueDescriptionPair(m_parm.BlInfo.CgTpCd, m_parm.BlInfo.CgTpCd));
                CommonUtility.SetComboboxSelectedItem(cboCargoType, m_parm.BlInfo.CgTpCd);
                */

                this.threadStart(isFirstInit);
                this.IsDirty = false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                if (this.InvokeRequired)
                    this.Invoke(setFormEnabledAction, true);
                else
                    setFormEnabledAction.Invoke(true);

            }
        }

        private void CargoReload()
        {
            ResponseInfo info;
            IApronCheckerProxy proxy = new ApronCheckerProxy();

            info = proxy.getCargoDischargingList(this.cgDchrgeParm);
            #region "Reload Bal"

            this.m_item = info.list[0] as CargoDischargingItem;

            Action<CargoDischargingItem> func = delegate(CargoDischargingItem item)
            {
                txtBalanceQty.Text = item.balQty.ToString();
                txtBalanceMT.Text = item.balMt.ToString();
                txtBalanceM3.Text = item.balM3.ToString();
               
            };
            if (this.InvokeRequired)
                this.Invoke(func, m_item);
            else
                func.Invoke(m_item);

            #endregion
        }

        //QUANBTL D/C performance END

        private void DisplayClearance(string custMode)
        {
            switch (custMode)
            {
                case Constants.CLEARANCE_HOLD:
                    txtClearance.Text = Constants.CLEARANCE_HOLD;
                    break;
                case Constants.CLEARANCE_RELEASE:
                    txtClearance.Text = Constants.CLEARANCE_RELEASE;
                    break;
                case Constants.CLEARANCE_INSPECTION:
                    txtClearance.Text = Constants.CLEARANCE_INSPECTION;
                    break;
                default:
                    txtClearance.Text = Constants.CLEARANCE_HOLD;
                    break;
            }
        }

        private void GetHatchList()
        {
            try
            {
                IApronCheckerProxy proxy = new ApronCheckerProxy();
                OperationSetParm hatchParm = new OperationSetParm();
                hatchParm.vslCallId = m_parm.BlInfo.VslCallId;
                hatchParm.shftDt = CommonUtility.FormatDateYYYYMMDD(UserInfo.getInstance().Workdate);
                hatchParm.shftId = UserInfo.getInstance().Shift;

                ResponseInfo bbkInfo = proxy.getOperationSetHtBBKList(hatchParm);
                if (bbkInfo != null && bbkInfo.list.Length > 0)
                {
                    m_bbkHatchList = new string[bbkInfo.list.Length];
                    for (int i = 0; i < bbkInfo.list.Length; i++)
                    {
                        m_bbkHatchList[i] = ((OperationSetItem)bbkInfo.list[i]).hatchNo;
                    }
                }
                ResponseInfo dbkInfo = proxy.getOperationSetHtDBKList(hatchParm);
                if (dbkInfo != null && dbkInfo.list.Length > 0)
                {
                    m_dbkHatchList = new string[dbkInfo.list.Length];
                    for (int i = 0; i < dbkInfo.list.Length; i++)
                    {
                        m_dbkHatchList[i] = ((OperationSetItem)dbkInfo.list[i]).hatchNo;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private void SetHatchList()
        {
            if (m_item != null)
            {
                CommonUtility.InitializeCombobox(cboHatch);
                if ("BBK".Equals(m_item.cgTpCd))
                {
                    if (m_bbkHatchList != null)
                    {
                        for (int i = 0; i < m_bbkHatchList.Length; i++)
                        {
                            cboHatch.Items.Add(new ComboboxValueDescriptionPair(m_bbkHatchList[i],
                                                                                m_bbkHatchList[i]));
                        }
                    }
                }
                else if ("DBK".Equals(m_item.cgTpCd) || "DBE".Equals(m_item.cgTpCd) || "DBN".Equals(m_item.cgTpCd))
                {
                    if (m_dbkHatchList != null)
                    {
                        for (int i = 0; i < m_dbkHatchList.Length; i++)
                        {
                            cboHatch.Items.Add(new ComboboxValueDescriptionPair(m_dbkHatchList[i],
                                                                                m_dbkHatchList[i]));
                        }
                    }
                }
            }
        }

        private bool ValidateBL()
        {
            if (string.IsNullOrEmpty(selectPanel.txtBL.Text.Trim()))
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0051"));
                return false;
            }

            BLListResult result = new BLListResult();
            if (CommonUtility.IsValidBL(m_item.vslCallId, selectPanel.txtBL.Text.Trim(), ref result) == false)
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0030"));
                return false;
            }

            return true;
        }

        private bool ProcessDischargingConfirm(bool isShortLanded)
        {
            // ref: CT121006
            bool result = false;
            m_cgDischargingInfo = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                IApronCheckerProxy proxy = new ApronCheckerProxy();
                CargoDischargingItem item;
                if (m_item != null)
                {
                    item = m_item;
                }
                else
                {
                    return false;
                }

                item.opDelvTpCd = CommonUtility.ToString(item.opDelvTpCd);
                item.fnlOpeYn = CommonUtility.ToString(item.fnlOpeYn);
                item.whFnlDelvYn = CommonUtility.ToString(item.whFnlDelvYn);
                item.fnlDelvYn = CommonUtility.ToString(item.fnlDelvYn);
                item.cgTpCd = CommonUtility.ToString(item.cgTpCd);

                item.hhtChk = CommonUtility.ToString(item.hhtChk);

                item.cgNo = m_parm.BlInfo.Bl;
                item.vslCallId = m_parm.BlInfo.VslCallId;
                item.hatchNo = CommonUtility.GetComboboxSelectedValue(cboHatch);
                //item.qty = CommonUtility.ParseInt(txtDocQty.Text);
                //item.mt = CommonUtility.ParseDouble(txtDocMT.Text);
                //item.m3 = CommonUtility.ParseDouble(txtDocM3.Text);
                //item.balQty = CommonUtility.ParseInt(txtBalanceQty.Text);
                //item.balMt = CommonUtility.ParseDouble(txtBalanceMT.Text);
                //item.balM3 = CommonUtility.ParseDouble(txtBalanceM3.Text);
                item.loadQty = CommonUtility.ParseInt(txtLorryQty.Text);
                item.loadMt = CommonUtility.ParseDouble(txtLorryMT.Text);
                item.loadM3 = CommonUtility.ParseDouble(txtLorryM3.Text);
                item.blNo = m_parm.BlInfo.Bl;
                item.doNo = m_parm.BlInfo.DoNo;
                //item.stat = m_parm.Stat;
                item.startDt = txtStartTime.Text;
                item.endDt = txtEndTime.Text;
                item.hdlInStDt = txtStartTime.Text;
                item.disStDt = txtStartTime.Text;
                item.whQty = CommonUtility.ParseInt(txtWHQty.Text);
                item.whWgt = CommonUtility.ParseDouble(txtWHMT.Text);
                item.whM3 = CommonUtility.ParseDouble(txtWHM3.Text);
                item.dmgQty = CommonUtility.ParseInt(this.pnlDischargingDmg.txtDmgQty.Text);
                item.dmgWgt = CommonUtility.ParseDouble(this.pnlDischargingDmg.txtDmgMT.Text);
                item.dmgM3 = CommonUtility.ParseDouble(this.pnlDischargingDmg.txtDmgM3.Text);
                item.overChk = chkOverLanded.Checked;
                //item.delvTpCd = m_parm.DelvTpCd;
                //item.tsptr = m_parm.Tsptr;
                item.tsptTpCd = CommonUtility.GetComboboxSelectedValue(cboOprMode);
                //item.catgCd = m_parm.CatgCd;
                item.fnlOpeYn = GetFinalOprYN();
                item.repkgTypeCd = txtPkgTp.Text;
                item.pkgNo = txtPkgNo.Text;
                item.rmk = txtRemark.Text;
                item.CRUD = Constants.WS_INSERT;
                item.userId = UserInfo.getInstance().UserId;
                item.shftId = UserInfo.getInstance().Shift;
                item.shftDt = CommonUtility.FormatDateYYYYMMDD(UserInfo.getInstance().Workdate);
                if (rbtnDirect.Checked)
                {
                    item.opDelvTpCd = "D";
                    item.lorryId = txtLorryNo.Text;
                }
                else if (rbtnIndirect.Checked)
                {
                    item.opDelvTpCd = "I";
                    item.lorryId = string.Empty;
                }
                if (string.IsNullOrEmpty(item.cgTpCd) && m_parm.BlInfo != null)
                {
                    item.cgTpCd = m_parm.BlInfo.CgTpCd;
                }
                if (chkOverLanded.Checked && rbtnIndirect.Checked)
                {
                    item.spCaCoCd = "O";
                }
                if (isShortLanded)
                {
                    item.shortYn = "Y";
                    item.spCaCoCd = "T";
                }
                else
                {
                    item.shortYn = "N";
                }

                // Set location (For saving WhConfigurationItem)
                item.hhtChk = "Y";
                ArrayList whConfigList = new ArrayList();
                if (m_resLocWh != null)
                {
                    item.locId = m_resLocWh.LocId;
                }
                if (m_resLocDmg != null)
                {
                    item.dmgLocId = m_resLocDmg.LocId;
                }
                if (m_resLocWh != null && m_resLocWh.ArrWHLocation != null && m_resLocWh.ArrWHLocation.Count > 0)
                {
                    whConfigList.AddRange(m_resLocWh.ArrWHLocation);
                }
                if (m_resLocDmg != null && m_resLocDmg.ArrWHLocation != null && m_resLocDmg.ArrWHLocation.Count > 0)
                {
                    whConfigList.AddRange(m_resLocDmg.ArrWHLocation);
                }
                if (whConfigList.Count > 0)
                {
                    item.collection = whConfigList.ToArray();
                }

                Object[] obj = new Object[] { item };
                DataItemCollection dataCollection = new DataItemCollection();
                dataCollection.collection = obj;

                m_cgDischargingInfo = proxy.processCargoDischargingItem(dataCollection);

                result = true;
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
            return result;
        }

        private void RadiobuttonListener(object sender, EventArgs e)
        {
            RadioButton mybutton = (RadioButton)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "rbtnDirect":
                case "rbtnIndirect":
                    CheckRadioButton();
                    break;
            }
        }

        //QUANBTL fix 0022089 17-10-2012 START
        private void CheckRadioButton()
        {
            if (rbtnDirect.Checked)
            {
                txtLorryNo.Enabled = true;
                btnF2.Enabled = true;
                txtLorryQty.Enabled = true;
                txtLorryMT.Enabled = true;
                txtLorryM3.Enabled = true;

                txtWHQty.Enabled = false;
                txtWHMT.Enabled = false;
                txtWHM3.Enabled = false;
                /*
                txtDmgQty.Enabled = false;
                txtDmgMT.Enabled = false;
                txtDmgM3.Enabled = false;
                */
                txtWHLoc.Enabled = false;
                //txtDmgLoc.Enabled = false;
                btnWHLoc.Enabled = false;
                //btnDmgLoc.Enabled = false;
                this.btnDcDmg.Enabled = false;
                chkOverLanded.Enabled = false;
            }
            else if (rbtnIndirect.Checked)
            {
                txtLorryNo.Enabled = false;
                btnF2.Enabled = false;
                txtLorryQty.Enabled = false;
                txtLorryMT.Enabled = false;
                txtLorryM3.Enabled = false;

                txtWHQty.Enabled = true;
                txtWHMT.Enabled = true;
                txtWHM3.Enabled = true;
                /*
                txtDmgQty.Enabled = true;
                txtDmgMT.Enabled = true;
                txtDmgM3.Enabled = true;
                */
                txtWHLoc.Enabled = true;
                //txtDmgLoc.Enabled = true;
                btnWHLoc.Enabled = true;
                //btnDmgLoc.Enabled = true;
                this.btnDcDmg.Enabled = true;
                chkOverLanded.Enabled = true;
            }

            SetPrimary();
        }
        //QUANBTL fix 0022089 17-10-2012 END

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnF1":
                    PartnerCodeListParm pkgTypeParm = new PartnerCodeListParm();
                    pkgTypeParm.Option = "CD";
                    pkgTypeParm.SearchItem = txtPkgTp.Text;
                    PartnerCodeListResult pkgTypeRes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_PKGTYPE), pkgTypeParm);
                    if (pkgTypeRes != null)
                    {
                        txtPkgTp.Text = pkgTypeRes.Code;
                    }
                    break;

                case "btnF2":
                    if ("BBK".Equals(this.m_item.cgTpCd))
                    {
                        MOST.Common.CommonParm.LorryListParm lorryListParm = new MOST.Common.CommonParm.LorryListParm();
                        if (this.m_parm != null)
                        {
                            lorryListParm.Jpvc = this.m_parm.BlInfo.VslCallId;

                            //modified by William 2015 July 10: Lorry List missing (Bl == > DoNo)
                            //lorryListParm.BlNo = this.m_parm.BlInfo.Bl;

                            lorryListParm.DoNo = this.m_parm.BlInfo.DoNo;
                            lorryListParm.BlNo = this.m_parm.BlInfo.Bl;
                        }

                        lorryListParm.LorryNo = this.txtLorryNo.Text;
                       

                        LorryListResult lorryListResult = (LorryListResult)PopupManager.instance.ShowPopup((IPopupWindow)new HCM107(2), (IPopupParm)lorryListParm);
                        if (lorryListResult == null)
                            break;
                        this.txtLorryNo.Text = lorryListResult.LorryNo;
                        break;
                    }
                    else
                    {
                        PartnerCodeListParm partnerCodeListParm = new PartnerCodeListParm();
                        if (!string.IsNullOrEmpty(this.m_item.tsptr))
                        {
                            partnerCodeListParm.PtnrCd = this.m_item.tsptr;
                        }

                        //modified by William 2015 July 10: Lorry List missing
                        if (this.m_parm != null)
                        {
                            partnerCodeListParm.VslCallId = this.m_parm.VslCallId;
                            partnerCodeListParm.DoNo = this.m_parm.BlInfo.DoNo;
                            partnerCodeListParm.BlNo = this.m_parm.BlInfo.Bl;
                        }

                        partnerCodeListParm.SearchItem = this.txtLorryNo.Text;

                        PartnerCodeListResult partnerCodeListResult2 = (PartnerCodeListResult)PopupManager.instance.ShowPopup((IPopupWindow)new HCM110(6), (IPopupParm)partnerCodeListParm);
                        if (partnerCodeListResult2 == null)
                            break;
                        this.txtLorryNo.Text = partnerCodeListResult2.LorryNo;
                        break;
                    }

                case "btnConfirm":

                    #region OGA
                    /*
                    if (_mark > 0)
                    {
                        #region Case 1: Health Status = Approve: Allow do operation
                        if (_mark == 1)
                        {
                            Confirm();
                            return;
                        }
                        #endregion Case 1: Health Status = Approve: Allow do operation

                        #region Case 2: Health Status = Reject: Stop Process
                        else if (_mark == 2)
                        {
                            string stQuarantine = (ogaQuarantine.Equals("IN PROGRESS")) ? "Vessel is in progress" : (ogaQuarantine.Equals("QUARANTINE AT ANCHORAGE")) ? "Vessel is under Quarantine at Anchorage" : "Vessel is under Quarantine at Wharf";

                            MessageBox.Show("Application for Health Clearance Status is Reject.\r\n" + stQuarantine + "\r\nYou can't continue progress.", "OGA Status");
                            return;
                        }
                        #endregion Case 2: Health Status = Reject: Stop Process

                        #region Case 3: Health Status = N/A: Stop Process
                        if (_mark == 3)
                        {
                            MessageBox.Show("Application for Health Clearance Status is N/A.\r\nYou can't continue progress.", "OGA Status");
                            return;
                        }
                        #endregion  Case 3: Health Status = N/A: Stop Process

                        #region Case 4: Quanrantine at Anchorage: Stop Process
                        else if (_mark == 4)
                        {
                            MessageBox.Show("Application for Health Clearance Status is " + ogaStatus + ".\r\nVessel is under Quarantine at Anchorage.\r\nYou can't continue progress.", "OGA Status");
                            return;
                        }
                        #endregion Case 4: Quanrantine at Anchorage: Stop Process

                        #region Case 5: Quanrantine: In Progress
                        if (_mark == 5)
                        {
                            DialogResult result = MessageBox.Show("Application for Health Clearance Status is " + ogaStatus + ".\r\nVessel is in progress.\r\nDo you want to continue progress?", "OGA Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (result == DialogResult.Yes)
                            {
                                Confirm();
                            }
                            return;
                        }
                        else if (_mark == 7)
                        {
                            MessageBox.Show("Application for Health Clearance Status is " + ogaStatus + ".\r\nVessel is in progress.\r\nYou can't continue progress.", "OGA Status");
                            return;
                        }
                        #endregion Case 5: Quanrantine: In Progress

                        #region Case 6: Quanrantine at Wharf
                        else if (_mark == 6)
                        {
                            DialogResult result = MessageBox.Show("Application for Health Clearance Status is " + ogaStatus + ".\r\nVessel is under Quarantine at Wharf.\r\nDo you want to continue progress?", "OGA Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (result == DialogResult.Yes)
                            {
                                Confirm();
                            }
                            return;
                        }
                        #endregion Case 6: Quanrantine at Wharf
                    }
                    */
                    #endregion OGA

                    Confirm();
                    break;
                case "btnCancel":
                    if (this.IsDirty)
                    {
                        DialogResult dr = CommonUtility.ConfirmMsgSaveChances();
                        if (dr == DialogResult.Yes)
                        {
                            if (this.IsDirty && this.validations(this.Controls) && Validate())
                            {
                                if (IsShortLanded())
                                {
                                    DialogResult drShortlanded = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HAC106_0009"));
                                    if (drShortlanded == DialogResult.Yes)
                                    {
                                        ProcessDischargingConfirm(true);
                                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                                        this.Close();
                                    }
                                }
                                else
                                {
                                    ProcessDischargingConfirm(false);
                                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                                    this.Close();
                                }
                            }
                        }
                        else if (dr == DialogResult.No)
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            this.Close();
                        }
                    }
                    else
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                        this.Close();
                    }
                    break;

                case "btnWHLoc":
                    HWC101001Parm parmShutLoc = new HWC101001Parm();
                    parmShutLoc.VslCallId = m_parm.BlInfo.VslCallId;
                    if (m_resLocWh != null && !string.IsNullOrEmpty(m_resLocWh.LocId))
                    {
                        parmShutLoc.LocId = m_resLocWh.LocId;
                        parmShutLoc.WhId = m_resLocWh.LocId.Substring(0, m_resLocWh.LocId.IndexOf("("));
                    }
                    parmShutLoc.TotMt = txtWHMT.Text;
                    parmShutLoc.TotM3 = txtWHM3.Text;
                    parmShutLoc.TotQty = txtWHQty.Text;
                    parmShutLoc.CgNo = m_parm.BlInfo.Bl;
                    parmShutLoc.WhTpCd = "G";    // General: G, Shut-out: S, Damge: D

                    HWC101001Result resultShutLoc = (HWC101001Result)PopupManager.instance.ShowPopup(new HWC101001(), parmShutLoc);
                    if (resultShutLoc != null)
                    {
                        m_resLocWh = resultShutLoc;
                        txtWHLoc.Text = m_resLocWh.LocId;
                    }
                    break;

                case "btnGatePass":
                case "btnGP":
                    MOST.Common.CommonParm.CargoGatePassParm cargoGatePassParm = new MOST.Common.CommonParm.CargoGatePassParm();
                    if (!string.Empty.Equals(this.m_item.vslCallId))
                        cargoGatePassParm.VslCallId = this.m_item.vslCallId;
                    if (!string.Empty.Equals(this.m_item.blNo))
                        cargoGatePassParm.CgNo = this.m_item.blNo;
                    PopupManager.instance.ShowPopup((IPopupWindow)new HCM116(), (IPopupParm)cargoGatePassParm);
                    break;
            }
        }

        //QUANBTL fix 0036484 12-10-2012 START
        private bool Confirm()
        {
            if (!this.IsDirty || !this.validations(this.Controls) || !this.Validate())
                return false;

            #region shortlanded
            if (this.IsShortLanded())
            {
                if (CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HAC106_0009")) == DialogResult.Yes && this.ProcessDischargingConfirm(true))
                {
                    if (this.rbtnIndirect.Checked)
                    {
                        if (this.chkFinal.Checked || "Y".Equals(this.m_item.fnlOpeYn))
                        {
                            int num = (int)CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HCM0048"));
                            this.Close();
                        }
                        else
                        {
                            this.ClearCtrlValues();
                            int num = (int)CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HCM0048"));
                            this.CargoReload();
                        }
                    }
                    else if (this.rbtnDirect.Checked)
                    {
                        if (CommonUtility.ConfirmMessage(ResourceManager.getInstance().
                             getString("HCM0072") + ((CargoArrvDelvItem)this.m_cgDischargingInfo.list[0]).gatePassNo
                        + "?") == DialogResult.Yes)
                        {
                            MOST.Common.CommonParm.CargoGatePassParm cargoGatePassParm = new MOST.Common.CommonParm.CargoGatePassParm();
                            if (this.m_parm != null && this.m_parm.BlInfo != null)
                            {
                                cargoGatePassParm.VslCallId = this.m_parm.BlInfo.VslCallId;
                                cargoGatePassParm.CgNo = this.m_parm.BlInfo.Bl;
                            }
                            if (this.m_cgDischargingInfo != null && this.m_cgDischargingInfo.list != null)
                            {
                                List<string> list = new List<string>();
                                for (int index = 0; index < this.m_cgDischargingInfo.list.Length; ++index)
                                {
                                    if (this.m_cgDischargingInfo.list[index] is CargoArrvDelvItem)
                                    {
                                        CargoArrvDelvItem cargoArrvDelvItem = (CargoArrvDelvItem)this.m_cgDischargingInfo.list[index];
                                        list.Add(cargoArrvDelvItem.gatePassNo);
                                    }
                                }
                                cargoGatePassParm.ArrInitGPNos = list;
                            }
                            HCM116 hcM116 = new HCM116();
                            hcM116.m_parm = cargoGatePassParm;
                            if ("NonCallId".Equals(hcM116.m_parm.VslCallId))
                                hcM116.rbtnNonJPVC.Checked = true;
                            else
                                hcM116.txtJPVC.Text = hcM116.m_parm.VslCallId;
                            hcM116.txtGRBL.Text = hcM116.m_parm.CgNo;
                            if (hcM116.m_parm.ArrInitGPNos != null && hcM116.m_parm.ArrInitGPNos.Count == 1)
                                hcM116.txtGP.Text = hcM116.m_parm.ArrInitGPNos[0];
                            if (!string.IsNullOrEmpty(hcM116.GetVslCallId()) || !string.IsNullOrEmpty(hcM116.txtGRBL.Text) || !string.IsNullOrEmpty(hcM116.txtGP.Text))
                                hcM116.F_Search(true);
                            hcM116.PrintSerialPort();
                            hcM116.DialogResult = DialogResult.Cancel;
                            hcM116.Close();
                        }
                        if (this.chkFinal.Checked || "Y".Equals(this.m_item.fnlOpeYn))
                        {
                            this.Close();
                        }
                        else
                        {
                            this.ClearCtrlValues();
                            this.CargoReload();
                        }
                    }
                }
            }
            #endregion
            #region normal cargo
            else if (this.ProcessDischargingConfirm(false))
            {
                if (this.rbtnIndirect.Checked)
                {
                    if (this.chkFinal.Checked || "Y".Equals(this.m_item.fnlOpeYn))
                    {
                        int num = (int)CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HCM0048"));
                        this.Close();
                    }
                    else
                    {
                        this.ClearCtrlValues();
                        int num = (int)CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HCM0048"));
                        this.CargoReload();
                    }
                }
                else if (this.rbtnDirect.Checked)
                {
                    if (CommonUtility.ConfirmMessage(ResourceManager.getInstance().
                             getString("HCM0072") + ((CargoArrvDelvItem)this.m_cgDischargingInfo.list[0]).gatePassNo
                        + "?") == DialogResult.Yes)
                    {
                        MOST.Common.CommonParm.CargoGatePassParm cargoGatePassParm = new MOST.Common.CommonParm.CargoGatePassParm();
                        if (this.m_parm != null && this.m_parm.BlInfo != null)
                        {
                            cargoGatePassParm.VslCallId = this.m_parm.BlInfo.VslCallId;
                            cargoGatePassParm.CgNo = this.m_parm.BlInfo.Bl;
                        }
                        if (this.m_cgDischargingInfo != null && this.m_cgDischargingInfo.list != null)
                        {
                            List<string> list = new List<string>();
                            for (int index = 0; index < this.m_cgDischargingInfo.list.Length; ++index)
                            {
                                if (this.m_cgDischargingInfo.list[index] is CargoArrvDelvItem)
                                {
                                    CargoArrvDelvItem cargoArrvDelvItem = (CargoArrvDelvItem)this.m_cgDischargingInfo.list[index];
                                    list.Add(cargoArrvDelvItem.gatePassNo);
                                }
                            }
                            cargoGatePassParm.ArrInitGPNos = list;
                        }
                        HCM116 hcM116 = new HCM116();
                        hcM116.m_parm = cargoGatePassParm;
                        if ("NonCallId".Equals(hcM116.m_parm.VslCallId))
                            hcM116.rbtnNonJPVC.Checked = true;
                        else
                            hcM116.txtJPVC.Text = hcM116.m_parm.VslCallId;
                        hcM116.txtGRBL.Text = hcM116.m_parm.CgNo;
                        if (hcM116.m_parm.ArrInitGPNos != null && hcM116.m_parm.ArrInitGPNos.Count == 1)
                            hcM116.txtGP.Text = hcM116.m_parm.ArrInitGPNos[0];
                        if (!string.IsNullOrEmpty(hcM116.GetVslCallId()) || !string.IsNullOrEmpty(hcM116.txtGRBL.Text) || !string.IsNullOrEmpty(hcM116.txtGP.Text))
                            hcM116.F_Search(true);
                        hcM116.PrintSerialPort();
                        hcM116.DialogResult = DialogResult.Cancel;
                        hcM116.Close();
                    }
                    if (this.chkFinal.Checked || "Y".Equals(this.m_item.fnlOpeYn))
                    {
                        this.Close();
                    }
                    else
                    {
                        this.ClearCtrlValues();
                        this.CargoReload();
                    }
                }
            }
            #endregion
            return true;
        }
        //QUANBTL fix 0036484 12-10-2012 END

        private void OverlandedChkListener(object sender, EventArgs e)
        {
            if (chkOverLanded.Checked)
            {
                /*
                txtDmgMT.Enabled = false;
                txtDmgM3.Enabled = false;
                txtDmgQty.Enabled = false;
                txtDmgLoc.Enabled = false;
                btnDmgLoc.Enabled = false;

                txtDmgMT.Text = "0";
                txtDmgM3.Text = "0";
                txtDmgQty.Text = "0";
                txtDmgLoc.Text = string.Empty;
                */
                m_resLocDmg = null;
            }
            else
            {
                /*
                txtDmgMT.Enabled = true;
                txtDmgM3.Enabled = true;
                txtDmgQty.Enabled = true;
                txtDmgLoc.Enabled = true;
                btnDmgLoc.Enabled = true;
                */
            }
        }

        private bool Validate()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (m_item != null)
                {
                    // Need to validate cargo Status and block with message if cargo is Hold
                    if (!Constants.CLEARANCE_RELEASE.Equals(m_item.custMode))
                    {
                        // There is no permission of this cargo from customs. Do you want to go proceed to discharge ?
                        if (CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HAC106_0010")) == DialogResult.No)
                        {
                            return false;
                        }
                    }

                    // Validate delivery type
                    if (rbtnDirect.Checked)
                    {
                        if (!string.IsNullOrEmpty(m_item.doNo))
                        {
                            if ("I".Equals(m_item.delvTpCd))
                            {
                                CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC106_0004"));
                                rbtnIndirect.Checked = true;
                                return false;
                            }
                            if (Constants.OPRMODE_LORRY.Equals(CommonUtility.GetComboboxSelectedValue(cboOprMode)) &&
                                string.IsNullOrEmpty(txtLorryNo.Text))
                            {
                                CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC106_0005"));
                                txtLorryNo.SelectAll();
                                txtLorryNo.Focus();
                                return false;
                            }
                        }
                        else
                        {
                            CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC106_0006"));
                            return false;
                        }
                    }
                    else if (rbtnIndirect.Checked)
                    {
                        if ("D".Equals(m_item.delvTpCd) && !chkOverLanded.Checked)
                        {
                            CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC106_0007"));
                            chkOverLanded.Checked = true;
                            return false;
                        }
                    }

                    // Validate PkgTp
                    if (!CommonUtility.IsValidPkgTp(txtPkgTp.Text.Trim()))
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0070"));
                        txtPkgTp.SelectAll();
                        txtPkgTp.Focus();
                        return false;
                    }

                    // Validate Lorry
                    if (!ValidateLorryNo())
                    {
                        return false;
                    }

                    // Check if balance amount is zero or not
                    if (!ValidateZeroBalanceAmt())
                    {
                        return false;
                    }

                    // Validate discharging amount in case of Over-landed/Short-landed
                    if (!ValidateInputZeroAmt())
                    {
                        return false;
                    }

                    // Validate discharging amount in case of Over-landed/Short-landed
                    if (!ValidateDischargeAmt())
                    {
                        return false;
                    }

                    // Validate locations in case of Indirect
                    if (this.rbtnIndirect.Checked)
                    {
                        if (!ValidateLocations())
                        {
                            return false;
                        }
                    }

                    // Check if start time and end time are within work date & shift.
                    if (!CommonUtility.ValidateStartEndDtWithinShift(txtStartTime, txtEndTime))
                    {
                        return false;
                    }
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
            return true;
        }

        private bool ValidateLorryNo()
        {
            if (rbtnDirect.Checked)
            {
                if (Constants.OPRMODE_LORRY.Equals(CommonUtility.GetComboboxSelectedValue(cboOprMode)) &&
                    !string.IsNullOrEmpty(txtLorryNo.Text) &&
                    m_item != null
                    //&& !string.IsNullOrEmpty(m_item.tsptr)
                    )
                {
                    // Validate lorry number.
                    if ("BBK".Equals(m_item.cgTpCd))
                    {
                        if (!CommonUtility.IsValidAssignmentLorry(m_item.vslCallId, m_item.blNo, txtLorryNo.Text, m_item.tsptr))
                        {
                            CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HCM0032"));
                            txtLorryNo.SelectAll();
                            txtLorryNo.Focus();
                            return false;
                        }
                    }
                    else
                    {
                        if (!CommonUtility.IsValidRegisterationLorry(txtLorryNo.Text, m_item.tsptr))
                        {
                            CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HCM0032"));
                            txtLorryNo.SelectAll();
                            txtLorryNo.Focus();
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool ValidateInputZeroAmt()
        {
            if (rbtnDirect.Checked)
            {
                double mt = CommonUtility.ParseDouble(txtLorryMT.Text);
                if (mt <= 0)
                {
                    CommonUtility.AlertMessage("Please input MT > 0");
                    txtLorryMT.Focus();
                    txtLorryMT.SelectAll();
                    return false;
                }
            }
            else if (rbtnIndirect.Checked)
            {
                double whMt = CommonUtility.ParseDouble(txtWHMT.Text);
                double dmgMt = CommonUtility.ParseDouble(this.pnlDischargingDmg.txtDmgMT.Text);
                if (whMt <= 0 && dmgMt <= 0)
                {
                    CommonUtility.AlertMessage("Please input at least WH or Dmg amount.");
                    return false;
                }
            }

            return true;
        }

        private bool ValidateZeroBalanceAmt()
        {
            bool isBBK = false;
            bool isDBK = false;
            double balMt = m_item.balMt;
            int balQty = m_item.balQty;

            if (rbtnIndirect.Checked && chkOverLanded.Checked)
            {
                return true;
            }

            if ("BBK".Equals(m_item.cgTpCd))
            {
                isBBK = true;
            }
            else if ("DBK".Equals(m_item.cgTpCd) || "DBE".Equals(m_item.cgTpCd) || "DBN".Equals(m_item.cgTpCd))
            {
                isDBK = true;
            }

            // no need to check actMT over than balance
            // 24/7/2014 lv.dat , issue 47237
            /*
            // Balance is zero. 
            // If this is overlanded cargo, please select Indirect radio button and tick on Overlanded check box.
            if (isBBK && (balMt == 0 && balQty == 0))
            {
                CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC106_0002"));
                rbtnIndirect.Checked = true;
                chkOverLanded.Checked = true;
                return false;
            }
            if (isDBK && balMt == 0)
            {
                CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC106_0002"));
                rbtnIndirect.Checked = true;
                chkOverLanded.Checked = true;
                return false;
            }*/

            return true;
        }

        private bool ValidateDischargeAmt()
        {
            bool isOverlanded = chkOverLanded.Checked;
            bool isBBK = false;
            bool isDBK = false;
            double balMt = m_item.balMt;
            int balQty = m_item.balQty;
            double actMt = 0;
            int actQty = 0;

            if ("BBK".Equals(m_item.cgTpCd))
            {
                isBBK = true;
            }
            else if ("DBK".Equals(m_item.cgTpCd) || "DBE".Equals(m_item.cgTpCd) || "DBN".Equals(m_item.cgTpCd))
            {
                isDBK = true;
            }

            if (rbtnDirect.Checked)
            {
                actMt = CommonUtility.ParseDouble(txtLorryMT.Text);
                actQty = CommonUtility.ParseInt(txtLorryQty.Text);
            }
            else if (rbtnIndirect.Checked)
            {
                actMt = CommonUtility.ParseDouble(txtWHMT.Text) + CommonUtility.ParseDouble(this.pnlDischargingDmg.txtDmgMT.Text);
                actQty = CommonUtility.ParseInt(txtWHQty.Text) + CommonUtility.ParseInt(this.pnlDischargingDmg.txtDmgQty.Text);
            }

            //BTL.QUAN #0039318 START
            if (isBBK)
            {
                if (actQty < 1)
                {
                    //CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC106_0011"));
                    //return false;
                }
            }
            //BTL.QUAN #0039318 END

            double subMt = balMt - actMt;
            int subQty = balQty - actQty;

            // no need to check actMT over than balance
            // 22/7/2014 lv.dat , issue 47237
            /*if (!isOverlanded)
            {
                if (isBBK && (subMt < 0 || subQty < 0))
                {
                    CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC106_0008"));
                    return false;
                }
                if (isDBK && subMt < 0)
                {
                    CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC106_0008"));
                    return false;
                }
            }*/

            return true;
        }

        /// <summary>
        /// Validate location in case of Indirect.
        /// Locations should be allocated before proceeding.
        /// </summary>
        /// <returns></returns>
        private bool ValidateLocations()
        {
            try
            {
                double actMt;
                int actQty;
                bool bHasValue;
                bool bSetLocWH = false;
                bool bSetLocDmg = false;
                bool isBBK = false;
                bool isDBK = false;
                if ("BBK".Equals(m_item.cgTpCd))
                {
                    isBBK = true;
                }
                else if ("DBK".Equals(m_item.cgTpCd) || "DBE".Equals(m_item.cgTpCd) || "DBN".Equals(m_item.cgTpCd))
                {
                    isDBK = true;
                }

                // Validate WH Location
                if (string.IsNullOrEmpty(txtWHLoc.Text))
                {
                    actMt = CommonUtility.ParseDouble(txtWHMT.Text);
                    actQty = CommonUtility.ParseInt(txtWHQty.Text);
                    bHasValue = (isBBK && (actMt > 0 || actQty > 0)) || (isDBK && actMt > 0) ? true : false;

                    if (bHasValue)
                    {
                        bSetLocWH = true;
                    }
                }

                // Validate Damage Location
                if (string.IsNullOrEmpty(this.pnlDischargingDmg.txtDmgLoc.Text))
                {
                    actMt = CommonUtility.ParseDouble(this.pnlDischargingDmg.txtDmgMT.Text);
                    actQty = CommonUtility.ParseInt(this.pnlDischargingDmg.txtDmgQty.Text);
                    bHasValue = (isBBK && (actMt > 0 || actQty > 0)) || (isDBK && actMt > 0) ? true : false;

                    if (bHasValue)
                    {
                        bSetLocDmg = true;
                    }
                }

                if (bSetLocWH && bSetLocDmg)
                {
                    CommonUtility.AlertMessage("Please set location of WH and Damage before proceeding.");
                    btnWHLoc.Focus();
                    return false;
                }
                else if (bSetLocWH && !bSetLocDmg)
                {
                    CommonUtility.AlertMessage("Please set location of WH before proceeding.");
                    btnWHLoc.Focus();
                    return false;
                }
                else if (!bSetLocWH && bSetLocDmg)
                {
                    CommonUtility.AlertMessage("Please set location of Damage before proceeding.");
                    //btnDmgLoc.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            return true;
        }

        private bool IsShortLanded()
        {
            bool isFinalDischarge = chkFinal.Checked;
            bool isBBK = false;
            bool isDBK = false;
            double balMt = m_item.balMt;
            int balQty = m_item.balQty;
            double actMt = 0;
            int actQty = 0;

            if ("BBK".Equals(m_item.cgTpCd))
            {
                isBBK = true;
            }
            else if ("DBK".Equals(m_item.cgTpCd) || "DBE".Equals(m_item.cgTpCd) || "DBN".Equals(m_item.cgTpCd))
            {
                isDBK = true;
            }

            if (rbtnDirect.Checked)
            {
                actMt = CommonUtility.ParseDouble(txtLorryMT.Text);
                actQty = CommonUtility.ParseInt(txtLorryQty.Text);
            }
            else if (rbtnIndirect.Checked)
            {
                actMt = CommonUtility.ParseDouble(txtWHMT.Text) + CommonUtility.ParseDouble(this.pnlDischargingDmg.txtDmgMT.Text);
                actQty = CommonUtility.ParseInt(txtWHQty.Text) + CommonUtility.ParseInt(this.pnlDischargingDmg.txtDmgQty.Text);
            }
            double subMt = balMt - actMt;
            int subQty = balQty - actQty;

            if (isFinalDischarge)
            {
                if (isBBK && (subMt > 0 || subQty > 0))
                {
                    return true;
                }
                if (isDBK && subMt > 0)
                {
                    return true;
                }
            }

            return false;
        }

        private string GetFinalOprYN()
        {
            double totMt = 0;
            int totQty = 0;
            bool isBBK = m_item != null && "BBK".Equals(m_item.cgTpCd) ? true : false;
            bool isDBK = m_item != null &&
                        ("DBK".Equals(m_item.cgTpCd) ||
                        "DBE".Equals(m_item.cgTpCd) ||
                        "DBN".Equals(m_item.cgTpCd)) ? true : false;

            if (chkFinal.Checked)
            {
                return "Y";
            }
            else
            {
                // Check if discharging amount == balance amount
                // Direct case:     Discharging amount = Lorry Load
                // Indirect case:   Discharging amount = WH + Damage
                double balMt = 0;
                int balQty = 0;
                if (m_item != null)
                {
                    balMt = m_item.balMt;
                    balQty = m_item.balQty;
                }
                if (rbtnDirect.Checked)
                {
                    totMt = CommonUtility.ParseDouble(txtLorryMT.Text);
                    totQty = CommonUtility.ParseInt(txtLorryQty.Text);
                }
                else if (rbtnIndirect.Checked)
                {
                    totMt = CommonUtility.ParseDouble(txtWHMT.Text) + CommonUtility.ParseDouble(this.pnlDischargingDmg.txtDmgMT.Text);
                    totQty = CommonUtility.ParseInt(txtWHQty.Text) + CommonUtility.ParseInt(this.pnlDischargingDmg.txtDmgQty.Text);
                }

                DialogResult dr;
                if (isBBK)
                {
                    if (totMt >= balMt || totQty >= balQty)
                    {
                        dr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0065"));
                        if (dr == DialogResult.Yes)
                        {
                            return "Y";
                        }
                        else if (dr == DialogResult.No)
                        {
                            return "N";
                        }
                    }
                }
                else if (isDBK)
                {
                    if (totMt >= balMt)
                    {
                        dr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0065"));
                        if (dr == DialogResult.Yes)
                        {
                            return "Y";
                        }
                        else if (dr == DialogResult.No)
                        {
                            return "N";
                        }
                    }
                }
            }

            return "N";
        }

        private void cboCargoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //QUANBTL D/C performance START

            String cgTpCd = CommonUtility.GetComboboxSelectedValue(cboCargoType);
            if ("CTR".Equals(cgTpCd))   //Container Type is invalid
            {
                CommonUtility.AlertMessage("This is Container cargo. Only Bulk cargo can handle in this system.");
                CommonUtility.SetComboboxSelectedItem(cboCargoType, m_item.cgTpCd);
            }
            if (m_item != null)
            {
                m_item.cgTpCd = CommonUtility.GetComboboxSelectedValue(cboCargoType);
            }
            SetPrimaryPkgType();
            SetHatchList();
            SetPrimary();
            //QUANBTL D/C performance END
        }

        private void ClearActLoc(object sender, EventArgs e)
        {
            m_resLocWh = null;
            txtWHLoc.Text = "";
        }

        //QUANBTL D/C performance START
        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            this.abortThread();
            if (!this.itemDelvTpCdLoaded && this.isAliveCgLoadThread == 0 && this.isAliveCdMstrLoadThread == 0)
            {
                if (this.delvTpHash.ContainsKey(this.itemDelvTpCd))
                {
                    this.txtDelvMode.Text = this.delvTpHash[this.itemDelvTpCd];
                    if ("Both Direct/Indirect".Equals(this.txtDelvMode.Text))
                        this.rbtnDirect.Checked = true;
                    this.itemDelvTpCdLoaded = true;
                    this.SetPrimary();
                }
                this.enableControls(true);
            }
            if (this.itemCgTpCdLoaded || this.isAliveCgLoadThread != 0 || this.isAliveCdMstrLoadThread != 0)
                return;
            CommonUtility.SetComboboxSelectedItem((ComboBox)this.cboCargoType, this.m_item.cgTpCd);
        }
        //QUANBTL D/C performance END

        //QUANBTL fix 0036484 12-10-2012 START
        private void btnDcDmg_Click(object sender, EventArgs e)
        {
            if (!this.pnlDischargingDmg.Visible)
            {
                this.pnlDischargingDmg.BringToFront();
                this.btnDcDmg.Text = "OK";
                this.btnConfirm.Enabled = this.btnCancel.Enabled = this.btnSelect.Enabled = this.btnGP.Enabled = false;
                this.pnlDischargingDmg.m_parm = this.m_parm;
                this.pnlDischargingDmg.m_resLocDmg = this.m_resLocDmg;
                this.pnlDischargingDmg.Visible = true;
            }
            else
            {
                if (!this.checkPrimaryForDcDmg())
                    return;
                this.btnDcDmg.Text = "DcDmg";
                this.btnConfirm.Enabled = this.btnCancel.Enabled = this.btnSelect.Enabled = this.btnGP.Enabled = true;
                switch (!this.oldLocID.Equals(this.pnlDischargingDmg.txtDmgLoc.Text) || !this.oldMt.Equals(this.pnlDischargingDmg.txtDmgMT.Text) || (!this.oldM3.Equals(this.pnlDischargingDmg.txtDmgM3.Text) || !this.oldQty.Equals(this.pnlDischargingDmg.txtDmgQty.Text)) ? MessageBox.Show("Do you want to save the changes?", "Discharging damage", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) : DialogResult.No)
                {
                    case DialogResult.No:
                        this.m_resLocDmg = this.old_M_resLocDmg;
                        this.pnlDischargingDmg.m_resLocDmg = this.old_M_resLocDmg;
                        this.pnlDischargingDmg.txtDmgLoc.Text = this.oldLocID;
                        this.pnlDischargingDmg.txtDmgM3.Text = this.oldM3;
                        this.pnlDischargingDmg.txtDmgMT.Text = this.oldMt;
                        this.pnlDischargingDmg.txtDmgQty.Text = this.oldQty;
                        break;
                    case DialogResult.Yes:
                        this.m_resLocDmg = this.pnlDischargingDmg.m_resLocDmg;
                        this.oldLocID = this.pnlDischargingDmg.txtDmgLoc.Text;
                        this.oldMt = this.pnlDischargingDmg.txtDmgMT.Text;
                        this.oldM3 = this.pnlDischargingDmg.txtDmgM3.Text;
                        this.oldQty = this.pnlDischargingDmg.txtDmgQty.Text;
                        this.old_M_resLocDmg = this.m_resLocDmg;
                        break;
                }
                if (!string.Empty.Equals(this.pnlDischargingDmg.txtDmgLoc.Text))
                {
                    this.lblDcDmgStt.Text = "DcDmg data exists!";
                }
                else
                {
                    this.lblDcDmgStt.Text = string.Empty;
                }
                this.pnlDischargingDmg.Visible = false;
            }
        }
        //QUANBTL fix 0036484 12-10-2012 END

        //QUANBTL fix 0022089 17-10-2012 START
        private void SetPrimary()
        {
            if (this.isAliveCgLoadThread == 0
                    && this.isAliveCdMstrLoadThread == 0)
            {
                if (!Constants.STRING_NULL.Equals(CommonUtility.GetComboboxSelectedValue(cboCargoType)))
                {
                    //BBK direct
                    if (Constants.STRING_BBK.Equals(CommonUtility.GetComboboxSelectedValue(cboCargoType))
                        && (this.rbtnDirect.Checked == true))
                    {
                        this.txtLorryMT.isMandatory = true;
                        this.txtLorryM3.isMandatory = false;
                        this.txtLorryQty.isMandatory = true;
                    }

                    //BBK In-direct
                    if (Constants.STRING_BBK.Equals(CommonUtility.GetComboboxSelectedValue(cboCargoType))
                        && (this.rbtnIndirect.Checked == true))
                    {
                        this.txtWHMT.isMandatory = true;
                        this.txtWHM3.isMandatory = false;
                        this.txtWHQty.isMandatory = true;
                    }

                    //DBK In-direct/direct
                    if (
                        Constants.STRING_DBE.Equals(CommonUtility.GetComboboxSelectedValue(cboCargoType))
                        || Constants.STRING_DBN.Equals(CommonUtility.GetComboboxSelectedValue(cboCargoType)))
                    {
                        this.txtLorryMT.isMandatory = true;
                        this.txtLorryM3.isMandatory = false;
                        this.txtLorryQty.isMandatory = false;
                        this.txtWHMT.isMandatory = true;
                        this.txtWHM3.isMandatory = false;
                        this.txtWHQty.isMandatory = false;
                    }
                }
            }
        }

        private bool checkPrimaryForDcDmg()
        {
            if (!string.Empty.Equals(this.pnlDischargingDmg.txtDmgLoc.Text))
            {
                if ("BBK".Equals(CommonUtility.GetComboboxSelectedValue((ComboBox)this.cboCargoType)) && this.rbtnIndirect.Checked && (string.Empty.Equals(this.pnlDischargingDmg.txtDmgMT.Text) || string.Empty.Equals(this.pnlDischargingDmg.txtDmgM3.Text) || string.Empty.Equals(this.pnlDischargingDmg.txtDmgQty.Text)))
                {
                    int num = (int)CommonUtility.AlertMessage("BBK In-direct. MT, M3, QTY are mandatory.");
                    return false;
                }
                else if (("DBE".Equals(CommonUtility.GetComboboxSelectedValue((ComboBox)this.cboCargoType)) || "DBN".Equals(CommonUtility.GetComboboxSelectedValue((ComboBox)this.cboCargoType))) && string.Empty.Equals(this.pnlDischargingDmg.txtDmgMT.Text))
                {
                    int num = (int)CommonUtility.AlertMessage("DBK In-direct. MT is mandatory.");
                    return false;
                }
            }
            else if ("BBK".Equals(CommonUtility.GetComboboxSelectedValue((ComboBox)this.cboCargoType)) && this.rbtnIndirect.Checked && (!string.Empty.Equals(this.pnlDischargingDmg.txtDmgMT.Text) || !string.Empty.Equals(this.pnlDischargingDmg.txtDmgM3.Text) || !string.Empty.Equals(this.pnlDischargingDmg.txtDmgQty.Text)) && string.Empty.Equals(this.pnlDischargingDmg.txtDmgLoc.Text))
            {
                int num = (int)CommonUtility.AlertMessage("Please select W/H Location.");
                return false;
            }
            else if ("DBE".Equals(CommonUtility.GetComboboxSelectedValue((ComboBox)this.cboCargoType)) || "DBN".Equals(CommonUtility.GetComboboxSelectedValue((ComboBox)this.cboCargoType)))
            {
                if (!string.Empty.Equals(this.pnlDischargingDmg.txtDmgMT.Text) && string.Empty.Equals(this.pnlDischargingDmg.txtDmgLoc.Text))
                {
                    int num = (int)CommonUtility.AlertMessage("Please select W/H Location.");
                    return false;
                }
                else if ((!string.Empty.Equals(this.pnlDischargingDmg.txtDmgM3.Text) || !string.Empty.Equals(this.pnlDischargingDmg.txtDmgQty.Text)) && string.Empty.Equals(this.pnlDischargingDmg.txtDmgMT.Text))
                {
                    int num = (int)CommonUtility.AlertMessage("DBK In-direct. MT is mandatory.");
                    return false;
                }
            }
            return true;
        }

        #region OGA
        //For OGA CR
        /*
        private void HAC106_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q && e.Modifiers == Keys.Alt)
            {
                MessageBox.Show("Screen ID : " + this.Name + "\n\rScreen caption: " + this.Text, "Screen Information !");
            }
        }
       
        private int _mark = 0;
        string ogaStatus = "";
        string ogaStatusDate = "";
        string ogaQuarantine = "";
        private SearchJPVCResult m_jpvcResult;

        private void setControlStatus(Boolean b)
        {
            tLabelHlth.Visible = b;
            tLabelHealth.Visible = b;
            tLabelQua.Visible = b;
            tLabelQuanrantine.Visible = b;
        }

        private void showOGAstatus(string health, string location)
        {
            tLabelHealth.Text = health;
            tLabelQuanrantine.Text = location;
        }

        private void checkOGAstatus(string VslCallId)
        {
            if (CommonUtility.IsValidJPVC(VslCallId, ref m_jpvcResult))
            {
                string _Health = "";
                string _Location = "";

                ogaStatus = m_jpvcResult.OgaStatus.Trim();
                ogaStatusDate = m_jpvcResult.OgaStatusDate.Trim();
                ogaQuarantine = m_jpvcResult.OgaQuarantine.Trim();

                if (ogaStatus.Equals("APPROVE"))
                    _Health = "AP";
                else if (ogaStatus.Equals("HOLD"))
                    _Health = "HL";
                else if (ogaStatus.Equals("IN PROGRESS"))
                    _Health = "IP";
                else if (ogaStatus.Equals("REJECT"))
                    _Health = "RJ";
                else if (ogaStatus.Equals("N/A"))
                    _Health = "NA";

                if (ogaQuarantine.Equals("QUARANTINE AT ANCHORAGE"))
                    _Location = "AN";
                else if (ogaQuarantine.Equals("QUARANTINE AT WHARF"))
                    _Location = "WH";
                else _Location = "IP";

                setControlStatus(true);
                showOGAstatus(_Health, _Location);


                #region Case 1: Health Status = Approve: Allow do operation
                if (_Health.Equals("AP"))
                {
                    _mark = 1;
                    return;
                }
                #endregion Case 1: Health Status = Approve: Allow do operation

                #region Case 2: Health Status = Reject,N/A: Stop Process
                if (_Health.Equals("RJ"))
                {
                    _mark = 2;
                    return;
                }
                #endregion Case 2: Health Status = Reject,N/A: Stop Process

                #region Case 3: Health Status = N/A: Stop Process
                if (_Health.Equals("NA"))
                {
                    _mark = 3;
                    return;
                }
                #endregion  Case 3: Health Status = N/A: Stop Process

                #region Case 4: Quanrantine at Anchorage: Stop Process
                if (_Location.Equals("AN"))
                {
                    _mark = 4;
                    return;
                }
                #endregion Case 4: Quanrantine at Anchorage: Stop Process

                #region Case 5: Quanrantine: In Progress
                else if (_Location.Equals("IP"))
                {
                    if (_Health.Equals("IP") || _Health.Equals("HL"))
                        _mark = 5;
                    else
                        _mark = 7;
                    return;
                }
                #endregion  Case 5: Quanrantine: In Progress

                #region Case 6: Quanrantine at Wharf
                else if (_Location.Equals(("WH")))
                {
                    _mark = 6;
                    return;
                }
                #endregion Case 6: Quanrantine at Wharf
            }
        }
          */
        #endregion OGA
    }
}