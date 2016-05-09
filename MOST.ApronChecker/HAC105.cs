using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using MOST.Common;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.Common.Utility;
using MOST.ApronChecker.Parm;
using MOST.ApronChecker.Result;
using MOST.WHChecker;
using MOST.WHChecker.Parm;
using MOST.WHChecker.Result;
using MOST.Client.Proxy.WHCheckerProxy;
using MOST.Client.Proxy.CommonProxy;
using MOST.Client.Proxy.ApronCheckerProxy;
using Framework.Common.ResourceManager;
using Framework.Common.Constants;
using Framework.Common.PopupManager;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using Framework.Service.Provider.WebService.Provider;

using Framework.Common.Helper;
using System.Threading;

namespace MOST.ApronChecker
{
    public partial class HAC105 : TForm, IPopupWindow
    {
        #region Local Variable
        private HAC105Parm m_parm;
        private SearchJPVCResult m_jpvcResult;
        private CargoLoadingItem m_item;
        private HWC101002Result m_unsetActResult;
        private HAC105001Result m_ldCnclRes;
        private HAC105002Result m_whDmgSprRes;
        private ResponseInfo m_cgLoadingInfo;
        private bool m_autoLocFlag;
        private bool m_autoNorLocFlag;
        private bool m_autoDmgLocFlag;
        private bool m_manualNorLocFlag;

        private Button selectButton = new Button();
        private HAC105003 selectPanel = new HAC105003();
        private Dictionary<string, CargoExportResult> grList = new Dictionary<string, CargoExportResult>();
        private Dictionary<string, CargoExportItem> snList = new Dictionary<string, CargoExportItem>();

        private String[] m_bbkHatchList;
        private String[] m_dbkHatchList;

        private const string FINAL_CG_HI = "validationFinalCargo4HandlingIn";

        //QUANBTL Loading performance START

        private Thread cgLoadThread;
        private Thread cdMstrLoadThread;
        private int isAliveCgLoadThread;
        private int isAliveCdMstrLoadThread;
        private bool itemCgTpCdLoaded;
        //QUANBTL Loading performance END

        /*
         * lv.dat final option 
         */
        private String sFnOpYN;

        #endregion

        public HAC105()
        {
            InitializeComponent();

            selectButton.Text = "G/R »";
            selectButton.Location = new Point(8, 224);
            selectButton.Size = new Size(53, 24);
            selectButton.Click += new EventHandler(selectButton_Click);
            this.Controls.Add(selectButton);

            selectPanel.Visible = false;
            selectPanel.Location = new Point(selectButton.Left, selectButton.Top - selectPanel.Height);
            this.Controls.Add(selectPanel);

            this.initialFormSize();
            CommonUtility.SetDTPWithinShift(txtStartTime, txtEndTime);

            List<string> controlNames = new List<string>();
            controlNames.Add(txtLorryNo.Name);
            controlNames.Add(txtLorryMT.Name);
            controlNames.Add(txtLorryM3.Name);
            controlNames.Add(txtLorryQty.Name);
            controlNames.Add(cboHatch.Name);
            controlNames.Add(txtActLoc.Name);
            controlNames.Add(txtPkgMT.Name);
            controlNames.Add(txtPkgM3.Name);
            controlNames.Add(txtPkgQty.Name);
            controlNames.Add(txtBagTp.Name);
            controlNames.Add(txtPkgNo.Name);
            controlNames.Add(chkLDmg.Name);
            controlNames.Add(txtLDmgMT.Name);
            controlNames.Add(txtLDmgM3.Name);
            controlNames.Add(txtLDmgQty.Name);
            controlNames.Add(cboOPRMode.Name);
            controlNames.Add(chkFinal.Name);
            controlNames.Add(txtStartTime.Name);
            controlNames.Add(txtEndTime.Name);
            this.AddOnChangeHandlerToInputControls(this.Controls, controlNames);


            //////////////////////////////////////////////////////////////////////////
            // Making button text be multi-lines
            WndProcHooker.MakeButtonMultiline(this.btnLDCancel);
            WndProcHooker.MakeButtonMultiline(this.btnWHDmgSpare);
            //////////////////////////////////////////////////////////////////////////

            //QUANBTL D/C performance START
            this.Closing += new CancelEventHandler(this.HAC105_Closing);
            //QUANBTL D/C performance END


        }

        private void ShippingNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string shippingNoteNo = CommonUtility.GetComboboxSelectedValue(selectPanel.ShippingNotes);
            //if (!String.IsNullOrEmpty(shippingNoteNo) &&
            //    snList.ContainsKey(shippingNoteNo))
            //{
            //    selectPanel.Shipper.Text = String.Format("{0}{1}",
            //                                             String.IsNullOrEmpty(snList[shippingNoteNo].shpr)
            //                                                ? String.Empty
            //                                                : (snList[shippingNoteNo].shpr + " - "),
            //                                             snList[shippingNoteNo].shprNm);
            //    LoadGoodReceipts();
            //}
            //else
            //{
            //    selectPanel.Shipper.Text = String.Empty;
            //    CommonUtility.InitializeCombobox(selectPanel.GoodReceipts);
            //}

        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            selectPanel.Visible = !selectPanel.Visible;

            if (selectPanel.Visible)
            {
                selectPanel.BringToFront();
                selectButton.Text = "OK";
                btnConfirm.Enabled = btnCancel.Enabled = false;
                this.selectPanel.jPVC = this.m_item.vslCallId;
            }
            else
            {
                if (m_parm.GrInfo != null)
                {
                    if (m_parm.GrInfo.GrNo != selectPanel.txtGR.Text)
                    {
                        if (String.IsNullOrEmpty(selectPanel.txtGR.Text))
                        {
                            selectButton.Text = "G/R »";
                            btnConfirm.Enabled = btnCancel.Enabled = true;
                            return;
                        }
                        if (!ValidateGr(selectPanel.txtGR.Text))
                        {
                            selectPanel.Visible = !selectPanel.Visible;
                            return;
                        }
                    }
                }

                selectButton.Text = "G/R »";
                btnConfirm.Enabled = btnCancel.Enabled = true;

                if (m_parm.GrInfo != null)
                {
                    if (m_parm.GrInfo.GrNo == selectPanel.txtGR.Text)
                        return;  // This GR is currently selected; no need to reload. 

                    if (this.IsDirty)  // If form is dirty, ask user to save changes before switch to other GRs. 
                    {
                        DialogResult dr = MessageBox.Show(String.Format("Do you want to save changes you made to {0}? ", m_parm.GrInfo.GrNo),
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

                m_parm.GrInfo.GrNo = selectPanel.txtGR.Text;

                // Set form title
                string strTitle = "A/C - LD Confirm";
                strTitle = strTitle + " - " + "Loading...";
                this.Text = strTitle;

                this.txtLorryM3.Text = this.txtLorryMT.Text = this.txtLorryQty.Text = this.txtLorryNo.Text = string.Empty;
                MakeEnable(true);
                new Thread(new ThreadStart(this.LoadCargoLoadingList)).Start();

            }
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            this.m_parm = (HAC105Parm)parm;
            InitializeData();

            this.ShowDialog();

            return null;
        }

        //QUANBTL Loading performance START
        /*Closing event
        * Abort threads upon closing to ensure thread safety
        */
        private void HAC105_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            abortThread();
            this.tmrRefresh.Enabled = false;
        }

        /*Start threads
         * Start threads upon called
         */
        private void threadStart()
        {
            this.itemCgTpCdLoaded = false;

            this.cdMstrLoadThread.Start();
            this.isAliveCdMstrLoadThread = 1;
            
            this.cgLoadThread.Start();
            this.isAliveCgLoadThread = 1;
        }
        //QUANBTL Loading performance END
        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                this.cdMstrLoadThread = new Thread(new ThreadStart(LoadCommonCodes));
                this.cgLoadThread = new Thread(new ThreadStart(LoadCargoLoadingList));
                this.itemCgTpCdLoaded = false;
                threadStart();

                //GetCargoLoadingList();
                SetPrimaryPkgType();
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

        private void LoadShippingNotes()
        {
            //// Clear old items if existing
            ////CommonUtility.InitializeCombobox(selectPanel.ShippingNotes);

            //ICommonProxy proxy = new CommonProxy();
            //CargoExportParm parm = new CargoExportParm();
            //parm.vslCallId = m_parm.VslCallId;
            //ResponseInfo info = proxy.getCargoExportList(parm);

            //snList.Clear();
            //for (int i = 0; i < info.list.Length; i++)
            //{
            //    CargoExportItem item = info.list[i] as CargoExportItem;
            //    if (item == null) continue;
            //    if (!snList.ContainsKey(item.shipgNoteNo))
            //    {
            //        string descr = item.shipgNoteNo;
            //        if (!String.IsNullOrEmpty(item.catgNm))
            //            descr += " (" + item.catgNm + ")";
            //        selectPanel.ShippingNotes.Items.Add(new ComboboxValueDescriptionPair(item.shipgNoteNo, descr));
            //        snList[item.shipgNoteNo] = item;
            //    }
            //}

            //CommonUtility.SetComboboxSelectedItem(selectPanel.ShippingNotes, m_item != null ? m_item.shipgNoteNo : String.Empty);
        }

        private void LoadGoodReceipts()
        {
            //ICommonProxy proxy = new CommonProxy();
            //string strShipgNoteNo = CommonUtility.GetComboboxSelectedValue(selectPanel.ShippingNotes);

            //CargoExportParm cgExpParm = new CargoExportParm();
            //cgExpParm.vslCallId = m_parm.VslCallId;
            //if (!String.IsNullOrEmpty(strShipgNoteNo))
            //    cgExpParm.shipgNoteNo = strShipgNoteNo;
            //cgExpParm.hhtFnlMode = Constants.FINAL_MODE_LDFN;
            //ResponseInfo info = proxy.getCargoExportList(cgExpParm);

            //grList.Clear();
            //CommonUtility.InitializeCombobox(selectPanel.GoodReceipts);
            //for (int i = 0; i < info.list.Length; i++)
            //{
            //    CargoExportItem item = info.list[i] as CargoExportItem;
            //    if (item == null) continue;

            //    CargoExportResult result = new CargoExportResult();
            //    result.GrNo = item.grNo;
            //    result.VslCallId = item.vslCallId;
            //    result.ShipgNoteNo = item.shipgNoteNo;
            //    result.CgTpCd = item.cgTpCd;
            //    result.DelvTpCd = item.delvTpCd;

            //    selectPanel.GoodReceipts.Items.Add(new ComboboxValueDescriptionPair(result.GrNo, result.GrNo));
            //    grList[result.GrNo] = result;
            //}

            //CommonUtility.SetComboboxSelectedItem(selectPanel.GoodReceipts, m_item != null ? m_item.grNo : String.Empty);
        }

        private void SetPrimaryPkgType()
        {
            // Only Break Bulk case should input Package Type.
            if (m_item != null)
            {
                this.txtPkgTp.isMandatory = "BBK".Equals(m_item.cgTpCd);
            }
        }

        private bool Validate()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // Must handle in Final before loading
                //Added by Chris 2016/02/19
                if ("I".Equals(m_item.opDelvTpCd))
                {
                    CommonCodeParm vParm = new CommonCodeParm();
                    vParm.tyCd = FINAL_CG_HI;
                    vParm.col1 = m_item.vslCallId;
                    vParm.col2 = m_item.shipgNoteNo;
                    ICommonProxy proxy = new CommonProxy();
                    ResponseInfo info = proxy.getValidationCode(vParm);
                    if (info != null && info.list.Length > 0 && info.list[0] is CommonCodeItem)
                    {
                        CommonCodeItem item = (CommonCodeItem)info.list[0];
                        if ("Y".Equals(item.isValidated))
                        {
                            CommonUtility.AlertMessage("Please handle in final before loading.");
                            return false;
                        }
                    }
                }

                //if (m_item != null && "I".Equals(m_item.delvTpCd) && "Y".Equals(m_parm.GrInfo.IsValidated))
                //{
                //    CommonUtility.AlertMessage("Please handle in final before loading.");
                //    return false;
                //}

                // Validate hatch
                if (!ValidateHatch())
                {
                    return false;
                }

                // Validate lorry
                if (!ValidateLorryNo())
                {
                    return false;
                }

                // Check Package Amount & Loose Bag Type
                if (!CheckInputPkgAmnt())
                {
                    return false;
                }

                // Validate loose bag type
                if (!CommonUtility.IsValidLooseBagTp(txtBagTp.Text.Trim()))
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0071"));
                    txtBagTp.SelectAll();
                    txtBagTp.Focus();
                    return false;
                }

                // Validate PkgTp
                if (!CommonUtility.IsValidPkgTp(txtPkgTp.Text.Trim()))
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0070"));
                    txtPkgTp.SelectAll();
                    txtPkgTp.Focus();
                    return false;
                }

                if (m_item != null)
                {
                    // Validate Actual, WH Damage, Spare amount 
                    if (!ValidateAmount())
                    {
                        return false;
                    }

                    // If Loading Cancel -> Return to shipper
                    if (!ValidateOnReturnToShipper())
                    {
                        return false;
                    }

                    // Check if start time and end time are within work date & shift.
                    if (!CommonUtility.ValidateStartEndDtWithinShift(txtStartTime, txtEndTime))
                    {
                        return false;
                    }

                    // Auto deallocation
                    if (!"D".Equals(m_item.delvTpCd))
                    {
                        m_autoNorLocFlag = GetNormalAutoDeallocationFlag();
                        if (m_manualNorLocFlag)
                        {
                            return false;
                        }

                        m_autoDmgLocFlag = m_whDmgSprRes != null && m_whDmgSprRes.AutoDmgLocFlag ? true : false;
                        if (m_autoNorLocFlag || m_autoDmgLocFlag)
                        {
                            m_autoLocFlag = true;
                            if (DialogResult.No == CommonUtility.ConfirmMessage("Do you want to unset location automatically ?"))
                            {
                                CommonUtility.AlertMessage("Please unset location manually before proceeding.");
                                return false;
                            }
                        }
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

        /// <summary>
        /// Hatch validation.
        /// Direct:      Need to input Hatch in case Actual amount is inputted.
        /// Indirect:    If Actual > (Dmg + Shut-out) ==> Need to input hatch.
        /// </summary>
        /// <returns></returns>
        private bool ValidateHatch()
        {
            if (cboHatch.SelectedIndex > 0)
            {
                return true;
            }

            // Cargo Type
            bool isBBK = false;
            if ("BBK".Equals(m_item.cgTpCd))
            {
                isBBK = true;
            }

            // Actual amount
            double actMt = CommonUtility.ParseDouble(txtLorryMT.Text);
            int actQty = CommonUtility.ParseInt(txtLorryQty.Text);
            bool isLoaded = false;
            if ("D".Equals(m_item.opDelvTpCd))
            {
                if (isBBK)
                {
                    if (actMt > 0 || actQty > 0)
                    {
                        isLoaded = true;
                    }
                }
                else
                {
                    if (actMt > 0)
                    {
                        isLoaded = true;
                    }
                }
            }
            else if ("I".Equals(m_item.opDelvTpCd))
            {
                // Loading Cancel: Shut-out/Damage
                double dmgMt = 0;
                int dmgQty = 0;
                double shuMt = 0;
                int shuQty = 0;
                if (m_ldCnclRes != null)
                {
                    shuQty = CommonUtility.ParseInt(m_ldCnclRes.ShuQty);
                    shuMt = CommonUtility.ParseDouble(m_ldCnclRes.ShuMt);
                    dmgQty = CommonUtility.ParseInt(m_ldCnclRes.DmgQty);
                    dmgMt = CommonUtility.ParseDouble(m_ldCnclRes.DmgMt);
                }
                double balMt = actMt - (dmgMt + shuMt);
                int balQty = actQty - (dmgQty + shuQty);

                if (isBBK)
                {
                    if (balMt > 0 || balQty > 0)
                    {
                        isLoaded = true;
                    }
                }
                else
                {
                    if (balMt > 0)
                    {
                        isLoaded = true;
                    }
                }
            }
            if (isLoaded)
            {
                CommonUtility.AlertMessage("Please select Hatch.");
                return false;
            }

            return true;
        }

        private bool ValidateLorryNo()
        {
            if (Constants.OPRMODE_LORRY.Equals(CommonUtility.GetComboboxSelectedValue(cboOPRMode)) &&
                string.IsNullOrEmpty(txtLorryNo.Text))
            {
                CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC105_0014"));
                return false;
            }

            // Validate lorry number.
            if (!CommonUtility.IsValidRegisterationLorry(txtLorryNo.Text, txtTsptr.Text))
            {
                CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HCM0032"));
                txtLorryNo.SelectAll();
                txtLorryNo.Focus();
                return false;
            }

            return true;
        }

        private bool CheckInputPkgAmnt()
        {
            double looseBagMt = CommonUtility.ParseDouble(txtPkgMT.Text);
            double looseBagM3 = CommonUtility.ParseDouble(txtPkgM3.Text);
            int looseBagQty = CommonUtility.ParseInt(txtPkgQty.Text);
            if ((looseBagMt != 0 || looseBagM3 != 0 || looseBagQty != 0) &&
                string.IsNullOrEmpty(txtBagTp.Text.Trim()))
            {
                CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC105_0015"));
                txtBagTp.SelectAll();
                txtBagTp.Focus();
                return false;
            }
            if (looseBagMt == 0 && looseBagM3 == 0 && looseBagQty == 0 &&
                !string.IsNullOrEmpty(txtBagTp.Text.Trim()))
            {
                CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC105_0016"));
                txtPkgMT.SelectAll();
                txtPkgMT.Focus();
                return false;
            }

            return true;
        }

        private bool ValidateAmount()
        {
            // Cargo Type
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

            // Actual loading
            double balMt = m_item.balMt;
            double balQty = m_item.balQty;
            double actMt = CommonUtility.ParseDouble(txtLorryMT.Text);
            int actQty = CommonUtility.ParseInt(txtLorryQty.Text);
            bool isActEmptyAmt = false;

            // Damage loading
            double dmgBalMt = m_item.whDmgBalMt;
            double dmgBalQty = m_item.whDmgBalQty;
            double dmgActMt = 0;
            int dmgActQty = 0;
            if (m_whDmgSprRes != null)
            {
                dmgActMt = m_whDmgSprRes.WhDmgMt;
                dmgActQty = m_whDmgSprRes.WhDmgQty;
            }
            bool isDmgEmptyAmt = false;

            // Spare loading
            double sprBalMt = m_item.sprBalMt;
            double sprBalQty = m_item.sprBalQty;
            double sprActMt = 0;
            int sprActQty = 0;
            if (m_whDmgSprRes != null)
            {
                sprActMt = m_whDmgSprRes.SprMt;
                sprActQty = m_whDmgSprRes.SprQty;
            }
            bool isSprEmptyAmt = false;

            //Loading Cancel
            double ldCnclMt = 0;
            double ldCnclQty = 0;
            double ldCnclBalMt = m_item.balMt;
            double ldCnclBalQty = m_item.balQty;
            if (m_ldCnclRes != null)
            {
                ldCnclMt = CommonUtility.ParseDouble(m_ldCnclRes.DmgMt) + CommonUtility.ParseDouble(m_ldCnclRes.ShuMt);
                ldCnclQty = CommonUtility.ParseDouble(m_ldCnclRes.DmgQty) +
                                   CommonUtility.ParseDouble(m_ldCnclRes.ShuQty);
            }

            bool isLdCnclEmptyAmt = false;


            if (isBBK)
            {
                // Actual
                if (actMt > 0 || actQty > 0)
                {
                    if (balMt <= 0 && balQty <= 0)
                    {
                        if ("I".Equals(m_item.opDelvTpCd))
                        {
                            CommonUtility.AlertMessage("This cargo cannot load because balance amount is less than 0.");
                            IsDirty = false;
                            return false;
                        }
                    }
                    if (actMt > balMt || actQty > balQty)
                    {
                        if ("I".Equals(m_item.opDelvTpCd))
                        {
                            CommonUtility.AlertMessage("Actual amount cannot exceed balance amount.");
                            return false;
                        }
                    }
                }
                else
                {
                    isActEmptyAmt = true;
                }

                // WH Damage
                if (dmgActMt > 0 || dmgActQty > 0)
                {
                    if (dmgBalMt <= 0 && dmgBalQty <= 0)
                    {
                        CommonUtility.AlertMessage("You cannot load WH Damage as its balance is equal or less than 0.");
                        return false;
                    }
                    if (dmgActMt > dmgBalMt || dmgActQty > dmgBalQty)
                    {
                        CommonUtility.AlertMessage("WH Damage amount cannot exceed WH Damage balance.");
                        return false;
                    }
                }
                else
                {
                    isDmgEmptyAmt = true;
                }

                // Spare
                if (sprActMt > 0 || sprActQty > 0)
                {
                    if (sprBalMt <= 0 && sprBalQty <= 0)
                    {
                        CommonUtility.AlertMessage("You cannot load Spare as its balance is equal or less than 0.");
                        return false;
                    }
                    if (sprActMt > sprBalMt || sprActQty > sprBalQty)
                    {
                        CommonUtility.AlertMessage("Spare amount cannot exceed Spare balance.");
                        return false;
                    }
                }
                else
                {
                    isSprEmptyAmt = true;
                }

                //Loading Cancel
                if (ldCnclMt > 0 || ldCnclQty > 0)
                {
                    if (ldCnclBalMt <= 0 && ldCnclBalQty <= 0)
                    {
                        CommonUtility.AlertMessage("You cannot loading cancel as its balance is equal or less than 0.");
                        return false;
                    }
                    if (ldCnclMt > ldCnclBalMt || ldCnclQty > ldCnclBalQty)
                    {
                        CommonUtility.AlertMessage("Loading amount cannot exceed loading balance.");
                        return false;
                    }
                }
                else
                {
                    isLdCnclEmptyAmt = true;
                }
            }
            else if (isDBK)
            {
                // Actual
                if (actMt > 0)
                {
                    if (balMt <= 0)
                    {
                        if ("I".Equals(m_item.opDelvTpCd))
                        {
                            CommonUtility.AlertMessage("This cargo cannot load because balance amount is less than 0.");
                            IsDirty = false;
                            return false;
                        }
                    }
                    if (actMt > balMt)
                    {
                        if ("I".Equals(m_item.opDelvTpCd))
                        {
                            CommonUtility.AlertMessage("Actual amount cannot exceed balance amount.");
                            return false;
                        }
                    }
                }
                else
                {
                    isActEmptyAmt = true;
                }

                // WH Damage
                if (dmgActMt > 0)
                {
                    if (dmgBalMt <= 0)
                    {
                        CommonUtility.AlertMessage("You cannot load WH Damage as its balance is equal or less than 0.");
                        return false;
                    }
                    if (dmgActMt > dmgBalMt)
                    {
                        CommonUtility.AlertMessage("WH Damage amount cannot exceed WH Damage balance.");
                        return false;
                    }
                }
                else
                {
                    isDmgEmptyAmt = true;
                }

                // Spare
                if (sprActMt > 0)
                {
                    if (sprBalMt <= 0)
                    {
                        CommonUtility.AlertMessage("You cannot load Spare as its balance is equal or less than 0.");
                        return false;
                    }
                    if (sprActMt > sprBalMt)
                    {
                        CommonUtility.AlertMessage("Spare amount cannot exceed Spare balance.");
                        return false;
                    }
                }
                else
                {
                    isSprEmptyAmt = true;
                }

                // Loading Cancel
                if (ldCnclMt > 0)
                {
                    if (ldCnclBalMt <= 0)
                    {
                        CommonUtility.AlertMessage("You cannot loading cancel as its balance is equal or less than 0.");
                        return false;
                    }
                    if (ldCnclMt > ldCnclBalMt)
                    {
                        CommonUtility.AlertMessage("Load amount cannot exceed load balance.");
                        return false;
                    }
                }
                else
                {
                    isLdCnclEmptyAmt = true;
                }
            }

            if (isActEmptyAmt && isDmgEmptyAmt && isSprEmptyAmt && isLdCnclEmptyAmt)
            {
                CommonUtility.AlertMessage("Please input at least Actual, WH Damage, Spare or Loading Cancel cargo.");
                return false;
            }

            return true;
        }

        private bool ValidateGr(string gr)
        {
            /*ICommonProxy proxy = new CommonProxy();
            Framework.Service.Provider.WebService.Provider.CargoExportParm cgExpParm
                = new Framework.Service.Provider.WebService.Provider.CargoExportParm();

            if (String.IsNullOrEmpty(gr))
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager
                    .ResourceManager.getInstance().getString("HCM0050"));
                return false;
            }

            if (!string.IsNullOrEmpty(m_item.vslCallId))
            {
                cgExpParm.vslCallId = m_item.vslCallId;
            }
            cgExpParm.cgNo = gr;

            ResponseInfo info = proxy.getCargoExportListHHT(cgExpParm);

            if (info != null)
            {
                if (info.list.Length == 1)
                {
                    return true;
                }
            }
            */
            CargoExportResult m_grResult = new CargoExportResult();
            if (CommonUtility.IsValidCargoExportGR(m_item.vslCallId, gr.Trim(), ref m_grResult) == false)
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0029"));
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Get auto deallocation flag of Normal cargo.
        /// Return true in case of: 
        /// Location is empty && 
        /// (1 cell partial load || 1 cell full load || 2 cell full load)
        /// </summary>
        /// <returns></returns>
        private bool GetNormalAutoDeallocationFlag()
        {
            m_manualNorLocFlag = false;
            bool bAutoFlag = false;
            try
            {
                if (string.IsNullOrEmpty(txtActLoc.Text))
                {
                    bool isBBK = false;
                    bool isDBK = false;
                    double balMt = m_item.balMt;
                    int balQty = m_item.balQty;
                    double actMt = CommonUtility.ParseDouble(txtLorryMT.Text);
                    int actQty = CommonUtility.ParseInt(txtLorryQty.Text);
                    if ("BBK".Equals(m_item.cgTpCd))
                    {
                        isBBK = true;
                    }
                    else if ("DBK".Equals(m_item.cgTpCd) || "DBE".Equals(m_item.cgTpCd) || "DBN".Equals(m_item.cgTpCd))
                    {
                        isDBK = true;
                    }
                    bool bHasValue = (isBBK && (actMt > 0 || actQty > 0)) || (isDBK && actMt > 0) ? true : false;

                    if (bHasValue)
                    {
                        if (m_item.locCount == 1)
                        {
                            bAutoFlag = true;
                        }
                        else if (m_item.locCount > 1)
                        {
                            if (isBBK)
                            {
                                bAutoFlag = ((0 < actMt && actMt == balMt && 0 < actQty && actQty == balQty) ||
                                    (0 < actMt && actMt == balMt && actQty == 0) ||
                                    (0 < actQty && actQty == balQty && actMt == 0))
                                    ? true : false;
                            }
                            else if (isDBK)
                            {
                                bAutoFlag = ((0 < actMt && actMt == balMt)) ? true : false;
                            }
                        }

                        if (!bAutoFlag)
                        {
                            m_manualNorLocFlag = true;
                            CommonUtility.AlertMessage("Please unset location of Normal manually.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            return bAutoFlag;
        }

        private bool ValidateOnReturnToShipper()
        {
            // If Loading Cancel is "Return to shipper", user has to input end time and lorry number.
            if (m_ldCnclRes != null && "Y".Equals(m_ldCnclRes.GatePassYn))
            {
                // End Time
                if (string.IsNullOrEmpty(txtEndTime.Text))
                {
                    CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC105_0003"));
                    return false;
                }

                // Lorry number
                if (string.IsNullOrEmpty(txtLorryNo.Text))
                {
                    CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC105_0004"));
                    return false;
                }
            }
            return true;
        }

        //QUANBTL Loading performance START
        private void LoadCommonCodes()
        {
            // Reset item list of OPR and CgTp combo boxes
            try
            {
                if (cboOPRMode.InvokeRequired)
                    cboOPRMode.Invoke(new Action<ComboBox>(CommonUtility.InitializeCombobox), cboOPRMode);
                else
                    CommonUtility.InitializeCombobox(cboOPRMode);
            }
            catch (ObjectDisposedException) { return; }

            try
            {
                if (cboCargoType.InvokeRequired)
                    cboCargoType.Invoke(new Action<ComboBox>(CommonUtility.InitializeCombobox), cboCargoType);
                else
                    CommonUtility.InitializeCombobox(cboCargoType);
            }
            catch (ObjectDisposedException) { return; }

            // Load item list from server
            IApronCheckerProxy proxy = new ApronCheckerProxy();
            CargoLoadingParm parm = new CargoLoadingParm();
            parm.hhtFlag = "CDMST";
            ResponseInfo responseInfo = proxy.getCargoLoadingList(parm);

            // Build item list of OPR and CgTp combo boxes
            if (responseInfo != null && responseInfo.list.Length > 0)
            {
                for (int i = 0; i < responseInfo.list.Length; i++)
                {
                    CodeMasterListItem1 item = responseInfo.list[i] as CodeMasterListItem1;
                    if (item == null && responseInfo.list[i] is CodeMasterListItem)
                        item = CommonUtility.ToCodeMasterListItem1(responseInfo.list[i] as CodeMasterListItem);
                    if (item == null)
                        continue;

                    switch (item.mcd)
                    {
                        case "TSPTTP":
                            Action<ComboboxValueDescriptionPair> action = delegate(ComboboxValueDescriptionPair comboBoxItem)
                                                                          {
                                                                              cboOPRMode.Items.Add(comboBoxItem);
                                                                          };
                            ComboboxValueDescriptionPair newComboBoxItem = new ComboboxValueDescriptionPair(item.scd, item.scdNm);
                            try
                            {
                                if (cboOPRMode.InvokeRequired)
                                    cboOPRMode.Invoke(action, newComboBoxItem);
                                else
                                    action.Invoke(newComboBoxItem);
                            }
                            catch (ObjectDisposedException) { return; }
                            break;
                        case "CGTPNLQ":
                            action = delegate(ComboboxValueDescriptionPair comboBoxItem)
                                                                          {
                                                                              cboCargoType.Items.Add(comboBoxItem);
                                                                          };
                            newComboBoxItem = new ComboboxValueDescriptionPair(item.scd, item.scd);
                            try
                            {
                                if (cboCargoType.InvokeRequired)
                                    cboCargoType.Invoke(action, newComboBoxItem);
                                else
                                    action.Invoke(newComboBoxItem);
                            }
                            catch (ObjectDisposedException) { return; }
                            break;
                    }
                }
            }

            //Get Hatch List
            GetHatchList();

            this.isAliveCdMstrLoadThread = 2;
        }

        private void LoadCargoLoadingList()
        {
            try
            {
                this.enableControl(false);

                // Request Webservice
                IApronCheckerProxy acProxy = new ApronCheckerProxy();
                CargoLoadingParm cgLoadingParm = new CargoLoadingParm();
                cgLoadingParm.hhtFlag = "CGLDLST";
                cgLoadingParm.shftDt = CommonUtility.FormatDateYYYYMMDD(UserInfo.getInstance().Workdate);
                cgLoadingParm.shftId = UserInfo.getInstance().Shift;
                if (m_parm.GrInfo != null)
                {
                    cgLoadingParm.vslCallId = m_parm.GrInfo.VslCallId;
                    cgLoadingParm.shipgNoteNo = m_parm.GrInfo.ShipgNoteNo;
                    cgLoadingParm.grNo = m_parm.GrInfo.GrNo;
                    cgLoadingParm.cgNo = m_parm.GrInfo.GrNo;
                    cgLoadingParm.delvTpCd = m_parm.GrInfo.DelvTpCd;
                    cgLoadingParm.cgTpCd = m_parm.GrInfo.CgTpCd;
                }

                ResponseInfo cgLoadingInfo = acProxy.getCargoLoadingList(cgLoadingParm);

                // Display Data
                if (cgLoadingInfo != null && cgLoadingInfo.list.Length > 0)
                {
                    if (cboHatch.InvokeRequired)
                        cboHatch.Invoke(new Action<ComboBox>(CommonUtility.InitializeCombobox), cboHatch);
                    else
                        CommonUtility.InitializeCombobox(cboHatch);
                    for (int i = 0; i < cgLoadingInfo.list.Length; i++)
                    {
                        OperationSetItem item = cgLoadingInfo.list[i] as OperationSetItem;
                        if (item == null)
                            continue;
                        Action<ComboboxValueDescriptionPair> action = delegate(ComboboxValueDescriptionPair newItem)
                                                                      {
                                                                          cboHatch.Items.Add(newItem);
                                                                      };
                        ComboboxValueDescriptionPair newComboBoxItem = new ComboboxValueDescriptionPair(item.hatchNo, item.hatchNo);
                        if (cboHatch.InvokeRequired)
                            cboHatch.Invoke(action, newComboBoxItem);
                        else
                            action.Invoke(newComboBoxItem);
                    }

                    // The first item in ResponseInfo is CargoLoadingItem.
                    // Doc, Balance infomation
                    m_item = cgLoadingInfo.list[0] as CargoLoadingItem;

                    #region Check loading final
                    bool bClose = false;
                    if (m_item != null)
                    {
                        // Check if this cargo was already loaded.
                        if ("Y".Equals(m_item.fnlLoadYn))
                        {
                            CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC105_0001"));
                            bClose = true;
                        }

                        // Check if its shipping note was already confirmed.
                        if ("N".Equals(m_item.snLdYn))
                        {
                            CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC105_0002"));
                            bClose = true;
                        }
                    }
                    if (bClose)
                    {
                        //this.Close();
                        Action<object> proc = delegate(object state)
                        {
                            this.btnActUnset.Enabled = false;
                            this.btnBagTp.Enabled = false;
                            this.btnConfirm.Enabled = false;
                            this.btnF1.Enabled = false;
                            this.btnLDCancel.Enabled = false;
                            this.btnPkgTp.Enabled = false;
                            this.btnWHDmgSpare.Enabled = false;
                        };
                        if (this.InvokeRequired)
                        {
                            this.Invoke(proc, true);
                        }
                        else
                        {
                            proc.Invoke(null);
                        }




                        return;
                    }
                    #endregion

                    Action<CargoLoadingItem> refreshAction = delegate(CargoLoadingItem item)
                                                             {
                                                                 // Set form title
                                                                 string strTitle = "A/C - LD Confirm";
                                                                 if (m_parm != null && m_parm.GrInfo != null)
                                                                     strTitle = strTitle + " - " + m_parm.GrInfo.GrNo;
                                                                 this.Text = strTitle;
                                                                 // Reset Loading Damage checkbox
                                                                 chkLDmg.Checked = false;


                                                                 //checkOGAstatus(m_parm.GrInfo.VslCallId);

                                                                 EnableLoadedDmg(chkLDmg.Checked);
                                                                 // Lorry
                                                                 txtLorryNo.Text = item != null ? item.lorryId : String.Empty;
                                                                 txtTsptr.Text = item != null ? item.tsptr : String.Empty;
                                                                 // PkgTp
                                                                 txtPkgTp.Text = item != null ? item.repkgTypeCd : String.Empty;
                                                                 // Balance, Actual, Document, W/H Damage balance
                                                                 txtBalM3.Text = item != null ? CommonUtility.ToString(item.balM3) : String.Empty;
                                                                 txtBalMT.Text = item != null ? CommonUtility.ToString(item.balMt) : String.Empty;
                                                                 txtBalQty.Text = item != null ? CommonUtility.ToString(item.balQty) : String.Empty;
                                                                 txtDocMT.Text = item != null ? CommonUtility.ToString(item.docMt) : String.Empty;
                                                                 txtDocM3.Text = item != null ? CommonUtility.ToString(item.docM3) : String.Empty;
                                                                 txtDocQty.Text = item != null ? CommonUtility.ToString(item.docQty) : String.Empty;
                                                                 // Clear Act MT, M3 and Qty
                                                                 //txtLorryMT.Text = txtLorryM3.Text = txtLorryQty.Text = String.Empty;
                                                                 // Set mandatory items (fix issue 0022089)
                                                                 txtLorryMT.isMandatory = true;
                                                                 txtLorryM3.isMandatory = item.cgTpCd == "BBK" && item.opDelvTpCd == "I";
                                                                 txtLorryQty.isMandatory = item.cgTpCd == "BBK";
                                                                 // Delivery Mode
                                                                 SetCtrlWithinDelvMode(item != null ? item.opDelvTpCd : String.Empty);
                                                                 // Operation Mode
                                                                 CommonUtility.SetComboboxSelectedItem(cboOPRMode, item != null ? item.tsptTpCd : String.Empty);
                                                                 CommonUtility.SetComboboxSelectedItem(cboCargoType, item != null ? item.cgTpCd : String.Empty);

                                                                 SetHatchList(this.m_item);
                                                             };
                    if (this.InvokeRequired)
                        this.Invoke(refreshAction, m_item);
                    else
                        refreshAction.Invoke(m_item);
                }

                this.IsDirty = true;
            }
            catch (Exception ex)
            {
                // Handle exceptions here. 
                // The following code just rethrow the exception for debugging. 
                throw ex;
            }
            finally
            {
                this.isAliveCgLoadThread = 2;
            }
        }

        private void enableControl(bool isEnable)
        {
            Action<object> proc = delegate(object state)
                {
                    this.btnConfirm.Enabled = isEnable;
                    this.btnWHDmgSpare.Enabled = isEnable;
                    this.btnLDCancel.Enabled = isEnable;
                    this.selectButton.Enabled = isEnable;
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

        private void GetCargoLoadingList()
        {
            // Request Webservice
            IApronCheckerProxy acProxy = new ApronCheckerProxy();
            CargoLoadingParm cgLoadingParm = new CargoLoadingParm();
            cgLoadingParm.shftDt = CommonUtility.FormatDateYYYYMMDD(UserInfo.getInstance().Workdate);
            cgLoadingParm.shftId = UserInfo.getInstance().Shift;
            if (m_parm.GrInfo != null)
            {
                cgLoadingParm.vslCallId = m_parm.GrInfo.VslCallId;
                cgLoadingParm.shipgNoteNo = m_parm.GrInfo.ShipgNoteNo;
                cgLoadingParm.grNo = m_parm.GrInfo.GrNo;
                cgLoadingParm.cgNo = m_parm.GrInfo.GrNo;
                cgLoadingParm.delvTpCd = m_parm.GrInfo.DelvTpCd;

                string strCdTpCd = m_parm.GrInfo.CgTpCd;
                cgLoadingParm.cgTpCd = strCdTpCd;
            }

            ResponseInfo cgLoadingInfo = acProxy.getCargoLoadingList(cgLoadingParm);

            // Display Data
            if (cgLoadingInfo != null && cgLoadingInfo.list.Length > 0)
            {
                CommonUtility.InitializeCombobox(cboHatch);
                CommonUtility.InitializeCombobox(cboOPRMode);
                CommonUtility.InitializeCombobox(cboCargoType);
                foreach (OperationSetItem item in cgLoadingInfo.list)
                    cboHatch.Items.Add(new ComboboxValueDescriptionPair(item.hatchNo, item.hatchNo));

                //Get Hatch List
                GetHatchList();

                // The first item in ResponseInfo is CargoLoadingItem.
                // Doc, Balance infomation
                if (cgLoadingInfo.list[0] is CargoLoadingItem)
                {
                    CargoLoadingItem item = (CargoLoadingItem)cgLoadingInfo.list[0];
                    m_item = item;

                    // Lorry
                    txtLorryNo.Text = item.lorryId;
                    txtTsptr.Text = item.tsptr;
                    // PkgTp
                    txtPkgTp.Text = item.repkgTypeCd;

                    // Balance, Actual, Document, W/H Damage balance
                    string strBalM3 = CommonUtility.ToString(item.balM3);
                    string strBalMt = CommonUtility.ToString(item.balMt);
                    string strBalQty = CommonUtility.ToString(item.balQty);
                    txtBalMT.Text = strBalMt;
                    txtBalM3.Text = strBalM3;
                    txtBalQty.Text = strBalQty;
                    txtDocMT.Text = CommonUtility.ToString(item.mt);
                    txtDocM3.Text = CommonUtility.ToString(item.m3);
                    txtDocQty.Text = CommonUtility.ToString(item.qty);

                    // Delivery Mode
                    SetCtrlWithinDelvMode(item.opDelvTpCd);
                    // Operation Mode

                    CommonUtility.SetComboboxSelectedItem(cboOPRMode, item.tsptTpCd);
                    CommonUtility.SetComboboxSelectedItem(cboCargoType, item.cgTpCd);

                    SetHatchList(item);
                }
            }

            this.IsDirty = true;
        }
        //QUANBTL Loading performance END

        private void SetCtrlWithinDelvMode(string delvMode)
        {
            // Delivery Mode
            if ("D".Equals(delvMode))
            {
                txtDelvMode.Text = "Direct";
                txtActLoc.Enabled = btnActUnset.Enabled = false;
            }
            else if ("I".Equals(delvMode))
            {
                txtDelvMode.Text = "Indirect";
                txtActLoc.Enabled = btnActUnset.Enabled = true;
            }
            else
            {
                txtDelvMode.Text = String.Empty;
                txtActLoc.Enabled = btnActUnset.Enabled = false;
            }
        }

        private bool ProcessLoadingConfirm()
        {
            // ref: CT121007
            bool result = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                m_cgLoadingInfo = null;
                ArrayList collectionList = new ArrayList();     // List of WHConfigurationItem of Normal cargo
                ArrayList shutItemsList = new ArrayList();      // List of WHConfigurationItem of Shut-out cargo (Loading Cancel)
                ArrayList dmgItemsList = new ArrayList();       // List of WHConfigurationItem of Damage cargo (Loading Cancel)
                ArrayList whDmgItemsList = new ArrayList();     // List of WHConfigurationItem of W/H Damage cargo
                ArrayList sprItemsList = new ArrayList();       // List of WHConfigurationItem of Spare cargo

                IApronCheckerProxy proxy = new ApronCheckerProxy();
                CargoLoadingItem item;
                if (m_item != null)
                {
                    item = m_item;
                }
                else
                {
                    return false;
                }

                /*
                 * get final option store and reuse later
                 */
                sFnOpYN = GetFinalOprYN();

                item.fnlOpeYn = sFnOpYN;
                item.loadCnclMode = "N";
                item.rhdlYn = "N";
                item.shutRhdlMode = CommonUtility.ToString(item.shutRhdlMode);
                item.dmgRhdlMode = CommonUtility.ToString(item.dmgRhdlMode);
                item.dmgYn = CommonUtility.ToString(item.dmgYn);
                item.gatePassYn = CommonUtility.ToString(item.gatePassYn);
                item.rhdlMode = CommonUtility.ToString(item.rhdlMode);
                item.sprYn = CommonUtility.ToString(item.sprYn);
                item.cgTpCd = CommonUtility.ToString(item.cgTpCd);
                item.whFnlDelvYn = CommonUtility.ToString(item.whFnlDelvYn);
                item.autoLocFlag = m_autoLocFlag ? "true" : "false";
                item.autoNorLocFlag = m_autoNorLocFlag ? "true" : "false";
                item.autoDmgLocFlag = m_autoDmgLocFlag ? "true" : "false";
                item.autoSprLocFlag = "false";

                //item.qty = CommonUtility.ParseInt(txtDocQty.Text);
                //item.mt = CommonUtility.ParseDouble(txtDocMT.Text);
                //item.m3 = CommonUtility.ParseDouble(txtDocM3.Text);
                //item.balQty = CommonUtility.ParseInt(txtBalQty.Text);
                //item.balMt = CommonUtility.ParseDouble(txtBalMT.Text);
                //item.balM3 = CommonUtility.ParseDouble(txtBalM3.Text);
                item.loadQty = CommonUtility.ParseInt(txtLorryQty.Text);
                item.loadMt = CommonUtility.ParseDouble(txtLorryMT.Text);
                item.loadM3 = CommonUtility.ParseDouble(txtLorryM3.Text);
                item.loadDmgMt = CommonUtility.ParseDouble(txtLDmgMT.Text);
                item.loadDmgM3 = CommonUtility.ParseDouble(txtLDmgM3.Text);
                item.loadDmgQty = CommonUtility.ParseInt(txtLDmgQty.Text);
                item.chkLoadDmgYn = chkLDmg.Checked;
                item.startDt = txtStartTime.Text;
                item.endDt = txtEndTime.Text;
                item.lorryId = txtLorryNo.Text;
                item.tsptTpCd = CommonUtility.GetComboboxSelectedValue(cboOPRMode);

                // Loading Cancel: Shut-out/Damage)
                if (m_ldCnclRes != null)
                {
                    item.shuQty = CommonUtility.ParseInt(m_ldCnclRes.ShuQty);
                    item.shuMt = CommonUtility.ParseDouble(m_ldCnclRes.ShuMt);
                    item.shuM3 = CommonUtility.ParseDouble(m_ldCnclRes.ShuM3);
                    //item.shuLocId
                    item.dmgQty = CommonUtility.ParseInt(m_ldCnclRes.DmgQty);
                    item.dmgMt = CommonUtility.ParseDouble(m_ldCnclRes.DmgMt);
                    item.dmgM3 = CommonUtility.ParseDouble(m_ldCnclRes.DmgM3);
                    //item.dmgLocId
                    item.shutRhdlMode = m_ldCnclRes.ShutRhdlMode;
                    item.dmgRhdlMode = m_ldCnclRes.DmgRhdlMode;
                    item.gatePassYn = m_ldCnclRes.GatePassYn;
                    item.rmk = m_ldCnclRes.Remark;
                    //item.delvTpCd
                    //item.opDelvTpCdd
                    //item.tsptr
                    //item.tsptTpCd
                    item.lorryId = txtLorryNo.Text;
                    //item.seq
                    //item.locId
                    //item.fnlOpeYn = chkFinal.Checked ? "Y" : "N";
                    //item.catgCd
                    //item.cmdtCd
                    //item.pkgTpCd
                    //item.wgtUnit
                    //item.msrmtUnit
                    //item.portOfLoad
                    //item.portOfDis


                    if ("BBK".Equals(item.cgTpCd))
                    {
                        // Shut-out
                        if (CommonUtility.ParseDouble(m_ldCnclRes.ShuMt) > 0 ||
                            CommonUtility.ParseDouble(m_ldCnclRes.ShuM3) > 0)
                        {
                            item.loadCnclMode = "Y";
                            if ("R".Equals(m_ldCnclRes.ShutRhdlMode) ||
                                "C".Equals(m_ldCnclRes.ShutRhdlMode))
                            {
                                item.rhdlYn = "Y";
                            }
                            else
                            {
                                item.rhdlYn = "N";
                            }
                        }
                        else
                        {
                            item.loadCnclMode = "N";
                        }
                        // Damage
                        if (CommonUtility.ParseDouble(m_ldCnclRes.DmgMt) > 0 ||
                            CommonUtility.ParseDouble(m_ldCnclRes.DmgM3) > 0)
                        {
                            item.loadCnclMode = "Y";
                            if ("R".Equals(m_ldCnclRes.DmgRhdlMode) ||
                                "C".Equals(m_ldCnclRes.DmgRhdlMode))
                            {
                                item.rhdlYn = "Y";
                            }
                            else
                            {
                                if (!"Y".Equals(item.rhdlYn))
                                {
                                    item.rhdlYn = "N";
                                }
                            }
                        }
                        else if (!"Y".Equals(item.loadCnclMode))
                        {
                            item.loadCnclMode = "N";
                        }
                    }
                    else if ("DBK".Equals(item.cgTpCd) ||
                        "DBE".Equals(item.cgTpCd) ||
                        "DBN".Equals(item.cgTpCd))
                    {
                        // Shut-out
                        if (CommonUtility.ParseDouble(m_ldCnclRes.ShuMt) > 0)
                        {
                            item.loadCnclMode = "Y";
                            if ("R".Equals(m_ldCnclRes.ShutRhdlMode) ||
                                "C".Equals(m_ldCnclRes.ShutRhdlMode))
                            {
                                item.rhdlYn = "Y";
                            }
                            else
                            {
                                item.rhdlYn = "N";
                            }
                        }
                        else
                        {
                            item.loadCnclMode = "N";
                        }
                        // Damage
                        if (CommonUtility.ParseDouble(m_ldCnclRes.DmgMt) > 0)
                        {
                            item.loadCnclMode = "Y";
                            if ("R".Equals(m_ldCnclRes.DmgRhdlMode) ||
                                "C".Equals(m_ldCnclRes.DmgRhdlMode))
                            {
                                item.rhdlYn = "Y";
                            }
                            else
                            {
                                if (!"Y".Equals(item.rhdlYn))
                                {
                                    item.rhdlYn = "N";
                                }
                            }
                        }
                        else if (!"Y".Equals(item.loadCnclMode))
                        {
                            item.loadCnclMode = "N";
                        }
                    }

                    item.shuLocId = m_ldCnclRes.ShuLocId;
                    item.dmgLocId = m_ldCnclRes.DmgLocId;
                    if (m_ldCnclRes.ShutSetLocRes != null &&
                        m_ldCnclRes.ShutSetLocRes.ArrWHLocation != null &&
                        m_ldCnclRes.ShutSetLocRes.ArrWHLocation.Count > 0)
                    {
                        shutItemsList.AddRange(m_ldCnclRes.ShutSetLocRes.ArrWHLocation);
                    }
                    if (m_ldCnclRes.DmgSetLocRes != null &&
                        m_ldCnclRes.DmgSetLocRes.ArrWHLocation != null &&
                        m_ldCnclRes.DmgSetLocRes.ArrWHLocation.Count > 0)
                    {
                        dmgItemsList.AddRange(m_ldCnclRes.DmgSetLocRes.ArrWHLocation);
                    }
                }


                item.repkgQty = CommonUtility.ParseInt(txtPkgQty.Text);
                item.repkgWgt = CommonUtility.ParseDouble(txtPkgMT.Text);
                item.repkgMsrmt = CommonUtility.ParseDouble(txtPkgM3.Text);
                item.repkgTpCd = txtBagTp.Text;
                item.repkgTypeCd = txtPkgTp.Text;
                item.pkgNo = txtPkgNo.Text;
                item.userId = UserInfo.getInstance().UserId;
                item.hatchNo = CommonUtility.GetComboboxSelectedValue(cboHatch);
                item.shftId = UserInfo.getInstance().Shift;
                item.shftDt = CommonUtility.FormatDateYYYYMMDD(UserInfo.getInstance().Workdate);
                //                if (m_parm != null && m_parm.GrInfo != null)
                //                {
                //                    item.cgTpCd = m_parm.GrInfo.CgTpCd;
                //                }

                if (m_whDmgSprRes != null)
                {
                    item.whDmgQty = m_whDmgSprRes.WhDmgQty;
                    item.whDmgMt = m_whDmgSprRes.WhDmgMt;
                    item.whDmgM3 = m_whDmgSprRes.WhDmgM3;
                    item.whDmgLocId = m_whDmgSprRes.WhDmgLocId;
                    item.sprQty = m_whDmgSprRes.SprQty;
                    item.sprMt = m_whDmgSprRes.SprMt;
                    item.sprM3 = m_whDmgSprRes.SprM3;
                    item.sprLocId = m_whDmgSprRes.SprLocId;
                }

                item.CRUD = Constants.WS_INSERT;

                // Set/Unset location - WhConfigurationItem
                if (m_unsetActResult != null && m_unsetActResult.ArrWHLocation != null)
                {
                    item.locId = m_unsetActResult.LocId;
                    if (m_unsetActResult.ArrWHLocation.Count > 0)
                    {
                        collectionList.AddRange(m_unsetActResult.ArrWHLocation);
                    }
                }
                if (m_whDmgSprRes != null)
                {
                    if (m_whDmgSprRes.SpareUnsetLocRes != null &&
                        m_whDmgSprRes.SpareUnsetLocRes.ArrWHLocation != null &&
                        m_whDmgSprRes.SpareUnsetLocRes.ArrWHLocation.Count > 0)
                    {
                        sprItemsList.AddRange(m_whDmgSprRes.SpareUnsetLocRes.ArrWHLocation);
                    }
                    if (m_whDmgSprRes.WhDmgUnsetLocRes != null &&
                        m_whDmgSprRes.WhDmgUnsetLocRes.ArrWHLocation != null &&
                        m_whDmgSprRes.WhDmgUnsetLocRes.ArrWHLocation.Count > 0)
                    {
                        whDmgItemsList.AddRange(m_whDmgSprRes.WhDmgUnsetLocRes.ArrWHLocation);
                    }
                }
                if (collectionList.Count > 0)
                {
                    item.collection = collectionList.ToArray();
                }
                if (shutItemsList.Count > 0)
                {
                    item.shutItems = shutItemsList.ToArray();
                }
                if (dmgItemsList.Count > 0)
                {
                    item.dmgItems = dmgItemsList.ToArray();
                }
                if (whDmgItemsList.Count > 0)
                {
                    item.whDmgItems = whDmgItemsList.ToArray();
                }
                if (sprItemsList.Count > 0)
                {
                    item.sprItems = sprItemsList.ToArray();
                }
                Object[] obj = new Object[] { item };
                DataItemCollection dataCollection = new DataItemCollection();
                dataCollection.collection = obj;

                if (checkDoubleAmountRisk(item))
                {
                    DialogResult gpDr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HAC105_0017"));
                    if (gpDr == DialogResult.No)
                    {
                        return false;
                    }
                }
                m_cgLoadingInfo = proxy.processCargoLoadingItem(dataCollection);
                result = true;
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

        private bool checkDoubleAmountRisk(CargoLoadingItem item)
        {
            if ((item.loadQty == item.shuQty && item.loadMt == item.shuMt && item.loadM3 == item.shuM3)
                || (item.loadQty == item.dmgQty && item.loadMt == item.dmgMt && item.loadM3 == item.dmgM3))
            {
                return true;
            }
            return false;
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnF1":
                    PartnerCodeListParm driverParm = new PartnerCodeListParm();

                    driverParm.PtnrCd = txtTsptr.Text;
                    driverParm.SearchItem = this.txtLorryNo.Text;

                    //added by William (2015 July 10): Lorry list is missing
                    driverParm.LorryNo = this.txtLorryNo.Text;
                    if (this.m_parm != null)
                    {
                        if (!string.IsNullOrEmpty(m_parm.VslCallId))
                            driverParm.VslCallId = m_parm.VslCallId;

                        if (!string.IsNullOrEmpty(m_parm.GrInfo.ShipgNoteNo))
                            driverParm.ShippingNoteNo = m_parm.GrInfo.ShipgNoteNo;
                    }

                    PartnerCodeListResult driverResult = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_LORRY), driverParm);
                    if (driverResult != null)
                    {
                        txtLorryNo.Text = driverResult.LorryNo;
                    }
                    break;

                case "btnBagTp":
                    PartnerCodeListParm bagTypeParm = new PartnerCodeListParm();
                    bagTypeParm.Option = "CD";
                    bagTypeParm.SearchItem = txtBagTp.Text;
                    PartnerCodeListResult bagTypeRes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_BAGTYPE), bagTypeParm);
                    if (bagTypeRes != null)
                    {
                        txtBagTp.Text = bagTypeRes.Code;
                    }
                    break;

                case "btnPkgTp":
                    PartnerCodeListParm pkgTypeParm = new PartnerCodeListParm();
                    pkgTypeParm.Option = "CD";
                    pkgTypeParm.SearchItem = txtPkgTp.Text;
                    PartnerCodeListResult pkgTypeRes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_PKGTYPE), pkgTypeParm);
                    if (pkgTypeRes != null)
                    {
                        txtPkgTp.Text = pkgTypeRes.Code;
                    }
                    break;

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
                            if (this.validations(this.Controls) && Validate())
                            {
                                ProcessLoadingConfirm();
                                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                               
                                this.Close();
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

                case "btnActUnset":
                    HWC101002Parm unsetActParm = new HWC101002Parm();
                    unsetActParm.VslCallId = m_parm.VslCallId;
                    unsetActParm.CgNo = m_parm.GrInfo.GrNo;
                    unsetActParm.TotMt = txtLorryMT.Text;
                    unsetActParm.TotM3 = txtLorryM3.Text;
                    unsetActParm.TotQty = txtLorryQty.Text;
                    unsetActParm.IsGeneralCg = true;
                    HWC101002Result resultActLoc = (HWC101002Result)PopupManager.instance.ShowPopup(new HWC101002(), unsetActParm);
                    if (resultActLoc != null)
                    {
                        m_unsetActResult = resultActLoc;
                        txtActLoc.Text = m_unsetActResult.LocId;
                    }
                    break;

                case "btnLDCancel":
                    HAC105001Parm ldCnclParm = new HAC105001Parm();
                    ldCnclParm.VslCallId = m_item.vslCallId;
                    ldCnclParm.ShipgNoteNo = m_item.shipgNoteNo;
                    ldCnclParm.CgNo = m_item.grNo;
                    ldCnclParm.DocMt = txtDocMT.Text;
                    ldCnclParm.DocM3 = txtDocM3.Text;
                    ldCnclParm.DocQty = txtDocQty.Text;
                    ldCnclParm.OpDelvTpCd = m_item.opDelvTpCd;
                    ldCnclParm.CgTpCd = m_item.cgTpCd;

                    // Bal, Delivery Mode
                    double balMt = 0;
                    double balM3 = 0;
                    int balQty = 0;
                    if ("D".Equals(m_item.opDelvTpCd))
                    {
                        balMt = m_item.balMt - CommonUtility.ParseDouble(txtLorryMT.Text);
                        balM3 = m_item.balM3 - CommonUtility.ParseDouble(txtLorryM3.Text);
                        balQty = m_item.balQty - CommonUtility.ParseInt(txtLorryQty.Text);
                    }
                    else if ("I".Equals(m_item.opDelvTpCd))
                    {
                        balMt = m_item.balMt - CommonUtility.ParseDouble(txtLorryMT.Text);
                        balM3 = m_item.balM3 - CommonUtility.ParseDouble(txtLorryM3.Text);
                        balQty = m_item.balQty - CommonUtility.ParseInt(txtLorryQty.Text);
                    }
                    ldCnclParm.BalMt = balMt;
                    ldCnclParm.BalM3 = balM3;
                    ldCnclParm.BalQty = balQty;

                    ldCnclParm.LdCnclResult = m_ldCnclRes;

                    HAC105001Result ldCnclRes = (HAC105001Result)PopupManager.instance.ShowPopup(new HAC105001(), ldCnclParm);
                    if (ldCnclRes != null)
                    {
                        m_ldCnclRes = ldCnclRes;
                        this.IsDirty = true;
                    }
                    break;

                case "btnWHDmgSpare":
                    HAC105002Parm whDmgSprParm = new HAC105002Parm();
                    whDmgSprParm.VslCallId = m_item.vslCallId;
                    whDmgSprParm.ShipgNoteNo = m_item.shipgNoteNo;
                    whDmgSprParm.CgNo = m_item.grNo;
                    whDmgSprParm.DocMt = txtDocMT.Text;
                    whDmgSprParm.DocM3 = txtDocM3.Text;
                    whDmgSprParm.DocQty = txtDocQty.Text;
                    whDmgSprParm.OpDelvTpCd = m_item.opDelvTpCd;
                    whDmgSprParm.CgTpCd = m_item.cgTpCd;
                    whDmgSprParm.BalMt = txtBalMT.Text;
                    whDmgSprParm.BalM3 = txtBalM3.Text;
                    whDmgSprParm.BalQty = txtBalQty.Text;

                    whDmgSprParm.WhDmgBalMt = m_item.whDmgBalMt;
                    whDmgSprParm.WhDmgBalM3 = m_item.whDmgBalM3;
                    whDmgSprParm.WhDmgBalQty = m_item.whDmgBalQty;
                    whDmgSprParm.LocDmgCount = m_item.locDmgCount;
                    whDmgSprParm.SprBalMt = m_item.sprBalMt;
                    whDmgSprParm.SprBalM3 = m_item.sprBalM3;
                    whDmgSprParm.SprBalQty = m_item.sprBalQty;

                    whDmgSprParm.WhDmgSprRes = m_whDmgSprRes;

                    HAC105002Result whDmgSprRes = (HAC105002Result)PopupManager.instance.ShowPopup(new HAC105002(), whDmgSprParm);
                    if (whDmgSprRes != null)
                    {
                        m_whDmgSprRes = whDmgSprRes;
                        this.IsDirty = true;
                    }
                    break;
            }
        }

        private bool Confirm()
        {
            // Keep form open after click Confirm button
            if (this.chkFinal.Checked)
            {
                return Confirm(true);
            }
            return Confirm(false);
        }

        private bool Confirm(bool closeOnClick)
        {
            if (this.IsDirty)
            {
                if (this.validations(this.Controls) && Validate() && ProcessLoadingConfirm())
                {
                    // In case of Loading Cancel -> Return to shipper, user can open GatePass screen to issue a GP.
                    if (m_ldCnclRes != null && "Y".Equals(m_ldCnclRes.GatePassYn))
                    {
                        // Save successfully. Do you want to open the GatePass screen?
                        DialogResult gpDr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0072"));
                        if (gpDr == DialogResult.Yes)
                        {
                            MOST.Common.CommonParm.CargoGatePassParm gpParm = new MOST.Common.CommonParm.CargoGatePassParm();
                            if (m_parm != null && m_parm.GrInfo != null)
                            {
                                gpParm.VslCallId = m_parm.GrInfo.VslCallId;
                                gpParm.CgNo = m_parm.GrInfo.GrNo;
                            }
                            if (m_cgLoadingInfo != null && m_cgLoadingInfo.list != null)
                            {
                                List<string> listGP = new List<string>();

                                for (int i = 0; i < m_cgLoadingInfo.list.Length; i++)
                                {
                                    if (m_cgLoadingInfo.list[i] is CargoArrvDelvItem)
                                    {
                                        CargoArrvDelvItem item = (CargoArrvDelvItem)m_cgLoadingInfo.list[i];
                                        listGP.Add(item.gatePassNo);
                                    }
                                }
                                gpParm.ArrInitGPNos = listGP;
                            }
                            PopupManager.instance.ShowPopup(new MOST.Common.HCM116(), gpParm);
                        }

                        /*
                         * use global string val to store the final option
                         */
                        Cursor.Current = Cursors.WaitCursor;
                        if (closeOnClick || "Y".Equals(this.sFnOpYN))
                        {
                            //as user request do not close , disable all input control prepare for another GR user will chose
                            //this.Close();
                            MakeEnable(false);
                        }
                        else
                        {
                            this.CargoReload();
                        }
                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HCM0048"));

                        Cursor.Current = Cursors.WaitCursor;
                        if (closeOnClick || "Y".Equals(this.sFnOpYN))
                        {
                            //as user request do not close , disable all input control prepare for another GR user will chose
                            //this.Close();
                            MakeEnable(false);
                        }
                        else
                        {
                            this.CargoReload();
                        }
                        Cursor.Current = Cursors.Default;
                    }
                }
                else
                    return false;

                this.IsDirty = false;
            }
            return true;
        }

        private void CargoReload()
        {
            try
            {
                // Request Webservice
                IApronCheckerProxy acProxy = new ApronCheckerProxy();
                CargoLoadingParm cgLoadingParm = new CargoLoadingParm();
                cgLoadingParm.hhtFlag = "CGLDLST";
                cgLoadingParm.shftDt = CommonUtility.FormatDateYYYYMMDD(UserInfo.getInstance().Workdate);
                cgLoadingParm.shftId = UserInfo.getInstance().Shift;
                if (m_parm.GrInfo != null)
                {
                    cgLoadingParm.vslCallId = m_parm.GrInfo.VslCallId;
                    cgLoadingParm.shipgNoteNo = m_parm.GrInfo.ShipgNoteNo;
                    cgLoadingParm.grNo = m_parm.GrInfo.GrNo;
                    cgLoadingParm.cgNo = m_parm.GrInfo.GrNo;
                    cgLoadingParm.delvTpCd = m_parm.GrInfo.DelvTpCd;
                    cgLoadingParm.cgTpCd = m_parm.GrInfo.CgTpCd;
                }

                ResponseInfo cgLoadingInfo = acProxy.getCargoLoadingList(cgLoadingParm);

                // Display Data
                if (cgLoadingInfo != null && cgLoadingInfo.list.Length > 0)
                {
                    // The first item in ResponseInfo is CargoLoadingItem.
                    // Doc, Balance infomation
                    m_item = cgLoadingInfo.list[0] as CargoLoadingItem;
                    Action<CargoLoadingItem> refreshAction = delegate(CargoLoadingItem item)
                        {
                            // Balance
                            txtBalM3.Text = item != null ? CommonUtility.ToString(item.balM3) : String.Empty;
                            txtBalMT.Text = item != null ? CommonUtility.ToString(item.balMt) : String.Empty;
                            txtBalQty.Text = item != null ? CommonUtility.ToString(item.balQty) : String.Empty;
                        };
                    if (this.InvokeRequired)
                        this.Invoke(refreshAction, m_item);
                    else
                        refreshAction.Invoke(m_item);
                }

                this.IsDirty = true;
            }
            catch (Exception ex)
            {
                // Handle exceptions here. 
                // The following code just rethrow the exception for debugging. 
                throw ex;
            }
        }

        private void EnableLoadedDmg(bool bChecked)
        {
            txtLDmgM3.Enabled = bChecked;
            txtLDmgMT.Enabled = bChecked;
            txtLDmgQty.Enabled = bChecked;
        }

        private void chkLDmg_CheckStateChanged(object sender, EventArgs e)
        {
            EnableLoadedDmg(chkLDmg.Checked);
        }

        private string GetFinalOprYN()
        {
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
                // Check if loading amount == balance amount
                // Loading amount = Load Normal + WH Damage + Shut-out(LD Cancel) + Damage(LD Cancel)
                double whDmgMt = 0;
                int whDmgQty = 0;
                if (m_whDmgSprRes != null)
                {
                    whDmgMt = m_whDmgSprRes.WhDmgMt;
                    whDmgQty = m_whDmgSprRes.WhDmgQty;
                }
                double balMt = 0;
                int balQty = 0;
                if (m_item != null)
                {
                    balMt = m_item.balMt;
                    balQty = m_item.balQty;
                }
                double totMt = CommonUtility.ParseDouble(txtLorryMT.Text) + whDmgMt;
                int totQty = CommonUtility.ParseInt(txtLorryQty.Text) + whDmgQty;

                if (m_ldCnclRes != null)
                {
                    totMt += CommonUtility.ParseDouble(m_ldCnclRes.DmgMt) + CommonUtility.ParseDouble(m_ldCnclRes.ShuMt);
                    totQty += CommonUtility.ParseInt(m_ldCnclRes.DmgQty) + CommonUtility.ParseInt(m_ldCnclRes.ShuQty);
                }

                DialogResult dr;

                //Added by Chris 2016-03-08 for Final Loading
                if (m_item != null)
                {
                    Boolean weight = false;
                    if (Double.Parse(m_item.snM3.ToString()) == 0 && Double.Parse(m_item.snMt.ToString()) == 0)
                    {
                        if(((Double.Parse(m_item.accuSumQty.ToString()) + Double.Parse(txtLorryQty.Text)) >= Double.Parse(m_item.snQty.ToString())))
                            weight = true;
                    }
                    else if (Double.Parse(m_item.snM3.ToString()) == 0 && Double.Parse(m_item.snQty.ToString()) == 0)
                    {
                        if(((Double.Parse(m_item.accuSumWgt.ToString()) + Double.Parse(txtLorryMT.Text)) >= Double.Parse(m_item.snMt.ToString())))
                            weight = true;
                    }
                    else if (Double.Parse(m_item.snQty.ToString()) == 0 && Double.Parse(m_item.snMt.ToString()) == 0)
                    {
                        if(((Double.Parse(m_item.accuSumMsrmt.ToString()) + Double.Parse(txtLorryM3.Text)) >= Double.Parse(m_item.snM3.ToString())))
                            weight = true;
                    }
                    else if (Double.Parse(m_item.snM3.ToString()) == 0)
                    {
                        if (((Double.Parse(m_item.accuSumWgt.ToString()) + Double.Parse(txtLorryMT.Text)) >= Double.Parse(m_item.snMt.ToString())) || ((Double.Parse(m_item.accuSumQty.ToString()) + Double.Parse(txtLorryQty.Text)) >= Double.Parse(m_item.snQty.ToString())))
                            weight = true;
                    }
                    else if (Double.Parse(m_item.snMt.ToString()) == 0)
                    {
                        if (((Double.Parse(m_item.accuSumMsrmt.ToString()) + Double.Parse(txtLorryM3.Text)) >= Double.Parse(m_item.snM3.ToString())) || ((Double.Parse(m_item.accuSumQty.ToString()) + Double.Parse(txtLorryQty.Text)) >= Double.Parse(m_item.snQty.ToString())))
                            weight = true;
                    }
                    else if (double.Parse(m_item.snQty.ToString()) == 0)
                    {
                        if (((Double.Parse(m_item.accuSumMsrmt.ToString()) + Double.Parse(txtLorryM3.Text)) >= Double.Parse(m_item.snM3.ToString())) || ((Double.Parse(m_item.accuSumWgt.ToString()) + Double.Parse(txtLorryMT.Text)) >= Double.Parse(m_item.snMt.ToString())))
                            weight = true;
                    }
                    else if (((Double.Parse(m_item.accuSumMsrmt.ToString()) + Double.Parse(txtLorryM3.Text)) >= Double.Parse(m_item.snM3.ToString())) || ((Double.Parse(m_item.accuSumWgt.ToString()) + Double.Parse(txtLorryMT.Text)) >= Double.Parse(m_item.snMt.ToString())) || ((Double.Parse(m_item.accuSumQty.ToString()) + Double.Parse(txtLorryQty.Text)) >= Double.Parse(m_item.snQty.ToString())))
                    {
                        weight = true;
                    }
                    if (weight)
                    {
                        dr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0064"));
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
                //End-----------------------
                if (isBBK)
                {
                    //Commented by Chris 2016-03-09 for finalLoading
                    //if (totMt >= balMt || totQty >= balQty)
                    //{

                    //    dr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0064"));
                    //    if (dr == DialogResult.Yes)
                    //    {
                    //        return "Y";
                    //    }
                    //    else if (dr == DialogResult.No)
                    //    {
                    //        return "N";
                    //    }

                    //}
                }
                else if (isDBK)
                {
                    //Commented by Chris 2016-03-09 for finalLoading
                    //if (totMt >= balMt)
                    //{
                    //    dr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0064"));
                    //    if (dr == DialogResult.Yes)
                    //    {
                    //        return "Y";
                    //    }
                    //    else if (dr == DialogResult.No)
                    //    {
                    //        return "N";
                    //    }
                    //}
                }
            }

            return "N";
        }

        //QUANBTL fix 0022089 17-10-2012 START
        private void cboCargoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            String cgTpCd = CommonUtility.GetComboboxSelectedValue(cboCargoType);
            if ("CTR".Equals(cgTpCd))   //Container Type is invalid
            {
                CommonUtility.AlertMessage("This is Container cargo. Only Bulk cargo can handle in this system.");
                CommonUtility.SetComboboxSelectedItem(cboCargoType, m_item.cgTpCd);
            }
            if (m_item != null && !CommonUtility.GetComboboxSelectedValue(cboCargoType).Equals(Constants.STRING_NULL))
            {
                m_item.cgTpCd = CommonUtility.GetComboboxSelectedValue(cboCargoType);
            }
            SetPrimaryPkgType();
            SetHatchList(m_item);
            SetPrimary();
        }
        //QUANBTL fix 0022089 17-10-2012 END
        //0966.258.515
        private void GetHatchList()
        {
            IApronCheckerProxy proxy = new ApronCheckerProxy();
            OperationSetParm hatchParm = new OperationSetParm();
            hatchParm.vslCallId = m_parm.GrInfo.VslCallId;
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

        private void SetHatchList(CargoLoadingItem item)
        {
            if (item != null)
            {
                CommonUtility.InitializeCombobox(cboHatch);
                if ("BBK".Equals(item.cgTpCd))
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
                else if ("DBK".Equals(item.cgTpCd) || "DBE".Equals(item.cgTpCd) || "DBN".Equals(item.cgTpCd))
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

        private void ClearLocAmount(object sender, EventArgs e)
        {
            m_unsetActResult = null;
            txtActLoc.Text = "";
        }

        //QUANBTL Loading performance START
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

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            abortThread();
            if (this.itemCgTpCdLoaded || this.isAliveCgLoadThread != 0 || this.isAliveCdMstrLoadThread != 0)
            {
                return;
            }
            else
            {
                CommonUtility.SetComboboxSelectedItem((ComboBox)this.cboCargoType, this.m_item.cgTpCd);
                if (!"Y".Equals(m_item.fnlOpeYn))
                {
                    this.enableControl(true);
                }
            }
        }
        //QUANBTL Loading performance END

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
                        && (Constants.STRING_DELIVERY_MODE_DIRECT.Equals(this.txtDelvMode.Text)))
                    {
                        this.txtLorryMT.isMandatory = true;
                        this.txtLorryM3.isMandatory = false;
                        this.txtLorryQty.isMandatory = true;
                    }

                    //BBK In-direct
                    if (Constants.STRING_BBK.Equals(CommonUtility.GetComboboxSelectedValue(cboCargoType))
                        && (Constants.STRING_DELIVERY_MODE_INDIRECT.Equals(this.txtDelvMode.Text)))
                    {
                        this.txtLorryMT.isMandatory = true;
                        this.txtLorryM3.isMandatory = false;
                        this.txtLorryQty.isMandatory = true;
                    }

                    //DBK In-direct/direct
                    if (
                        Constants.STRING_DBE.Equals(CommonUtility.GetComboboxSelectedValue(cboCargoType))
                        || Constants.STRING_DBN.Equals(CommonUtility.GetComboboxSelectedValue(cboCargoType)))
                    {
                        this.txtLorryMT.isMandatory = true;
                        this.txtLorryM3.isMandatory = false;
                        this.txtLorryQty.isMandatory = false;
                    }
                }
            }
        }

        //QUANBTL fix 0022089 17-10-2012 END

        /*
         * lv.dat add to fix issue HHT final will disable all control
         */
        private void MakeEnable(bool bEnable)
        {
            if (!bEnable)
                IsDirty = bEnable;

            txtLorryMT.Enabled = bEnable;
            txtLorryM3.Enabled = bEnable;
            txtLorryQty.Enabled = bEnable;
            cboHatch.Enabled = bEnable;
            txtActLoc.Enabled = bEnable;
            btnActUnset.Enabled = bEnable;
            cboCargoType.Enabled = bEnable;
            txtPkgMT.Enabled = bEnable;
            txtPkgM3.Enabled = bEnable;
            txtPkgQty.Enabled = bEnable;
            txtBagTp.Enabled = bEnable;
            btnBagTp.Enabled = bEnable;
            txtPkgNo.Enabled = bEnable;
            txtPkgTp.Enabled = bEnable;
            btnPkgTp.Enabled = bEnable;
            chkLDmg.Enabled = bEnable;
            txtLDmgMT.Enabled = bEnable;
            txtLDmgM3.Enabled = bEnable;
            txtLDmgQty.Enabled = bEnable;
            cboOPRMode.Enabled = bEnable;
            txtLorryNo.Enabled = bEnable;
            btnF1.Enabled = bEnable;
            txtTsptr.Enabled = bEnable;
            btnWHDmgSpare.Enabled = bEnable;
            btnLDCancel.Enabled = bEnable;
            chkFinal.Enabled = bEnable;
            txtStartTime.Enabled = bEnable;
            txtEndTime.Enabled = bEnable;
            btnConfirm.Enabled = bEnable;
        }

        #region OGA
        /*
        private void HAC105_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q && e.Modifiers == Keys.Alt)
            {
                MessageBox.Show("Screen ID : " + this.Name + "\n\rScreen caption: " + this.Text, "Screen Information !");
            }
        }

        private void chkFinal_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void txtDocMT_TextChanged(object sender, EventArgs e)
        {

        }

        //For OGA CR
        private int _mark = 0;
        string ogaStatus = "";
        string ogaStatusDate = "";
        string ogaQuarantine = "";
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