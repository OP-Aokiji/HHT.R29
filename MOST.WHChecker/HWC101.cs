using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MOST.Common;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.Common.Utility;
using MOST.Client.Proxy.CommonProxy;
using MOST.Client.Proxy.WHCheckerProxy;
using MOST.WHChecker.Parm;
using MOST.WHChecker.Result;
using Framework.Service.Provider.WebService.Provider;
using Framework.Controls;
using Framework.Common.ResourceManager;
using Framework.Controls.Container;
using Framework.Common.Constants;
using Framework.Common.PopupManager;
using Framework.Common.UserInformation;
using Framework.Common.ExceptionHandler;
using System.Threading;

namespace MOST.WHChecker
{
    public partial class HWC101 : TForm, IPopupWindow
    {
        #region variables
        public const int TAB_HI = 1;
        public const int TAB_HO = 2;
        public const int TYPE_JPVC = 1;
        public const int TYPE_NONJPVC = 2;
        private int m_tab;
        ResponseInfo m_responseInfo;
        private CargoExportResult m_grResultHI;
        private CargoExportResult m_grResultHO;
        private BLListResult m_blResultHO;
        private HWC101Parm m_parm;
        private HWC101002Result m_resHOLoc;
        private HWC101001Result m_resHILocAct;
        private HWC101001Result m_resHILocDmg;
        private HWC101001Result m_resHILocSOut;
        private CargoHandlingInItem m_itemHI;
        private CargoHandlingOutItem m_itemHO;

        private HWC101003 pnlHISOut = new HWC101003();
        private HWC101004 pnlHIDmg = new HWC101004();
        private HWC101005 selectGRPanel = new HWC101005();
        private HWC101006 selectBLPanel = new HWC101006();

        private bool m_autoLocFlag;
        private bool m_manualActLocFlag;
        private bool isLoadedHI = false;
        private bool isLoadedHO = false;


        #endregion

        public HWC101(int tab)
        {
            m_tab = tab;
            InitializeComponent();
            this.initialFormSize();
            CommonUtility.SetDTPWithinShift(txtHIDtStart, txtHIDtEnd);
            CommonUtility.SetDTPWithinShift(txtHODtStart, txtHODtEnd);
            SetEnableControls();
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            this.m_parm = (HWC101Parm)parm;

            if (m_tab == HWC101.TAB_HI)
            {
                this.initializeTabs("tabHI");                
                this.Show();
                InitializeData();
                InitializePanel("tabHI");
            }
            else if (m_tab == HWC101.TAB_HO)
            {

                this.initializeTabs("tabHO");

                //new Thread(new ThreadStart(delegate()
                //        {
                //            InitializeData();
                //        })).Start();
                
                InitializeData();
                InitializePanel("tabHO");
                this.ShowDialog();
            }

            return null;
        }

        private void ChkSpareCargo()
        {
            txtHIPkgNo.Enabled = false;
            txtHIPkgTp.Enabled = false;
            btnHIPkgTp.Enabled = false;
            txtHIActLoc.Enabled = false;
            btnHISetLocAct.Enabled = false;
            //txtHISOutMT.Enabled = false;
            //txtHISOutM3.Enabled = false;
            //txtHISOutQty.Enabled = false;
            //cboHIHndlSOut.Enabled = false;
            //txtHIShutLoc.Enabled = false;
            //btnHISetLocSOut.Enabled = false;
            //txtHIDmgMT.Enabled = false;
            //txtHIDmgM3.Enabled = false;
            //txtHIDmgQty.Enabled = false;
            //cboHIHndlDmg.Enabled = false;
            //txtHIDmgLoc.Enabled = false;
            //btnHISetLocDmg.Enabled = false;
            chkHIGP.Enabled = false;
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (m_tab == HWC101.TAB_HI)
                {
                    tabMain.SelectedIndex = 0;

                    #region Get cargo handling in
                    if (m_parm != null && m_parm.GrInfo != null)
                    {
                        m_grResultHI = m_parm.GrInfo;
                        txtHIGR.Text = m_grResultHI.GrNo;
                        new Thread(new ThreadStart(delegate()
                        {
                            GetCargoHandlingInList();
                        })).Start();

                    }
                    #endregion

                    //CommonUtility.InitializeCombobox(cboHIHndlSOut);
                    //cboHIHndlSOut.Items.Add(new ComboboxValueDescriptionPair("R", "Return to Shipper"));
                    //cboHIHndlSOut.Items.Add(new ComboboxValueDescriptionPair("C", "Change Vessel"));

                    //CommonUtility.InitializeCombobox(cboHIHndlDmg);
                    //cboHIHndlDmg.Items.Add(new ComboboxValueDescriptionPair("R", "Return to Shipper"));
                    //cboHIHndlDmg.Items.Add(new ComboboxValueDescriptionPair("C", "Change Vessel"));
                }
                else if (m_tab == HWC101.TAB_HO)
                {
                    tabMain.SelectedIndex = 1;

                    #region Clearance
                    CommonUtility.InitializeCombobox(cboHOClearance);
                    cboHOClearance.Items.Add(new ComboboxValueDescriptionPair(Constants.CLEARANCE_HOLD, Constants.CLEARANCE_HOLD));
                    cboHOClearance.Items.Add(new ComboboxValueDescriptionPair(Constants.CLEARANCE_RELEASE, Constants.CLEARANCE_RELEASE));
                    cboHOClearance.Items.Add(new ComboboxValueDescriptionPair(Constants.CLEARANCE_INSPECTION, Constants.CLEARANCE_INSPECTION));
                    #endregion

                    #region Get cargo handling out
                    if (m_parm != null)
                    {
                        if (m_parm.GrInfo != null)
                        {
                            m_grResultHO = m_parm.GrInfo;
                            txtHOGRBL.Text = m_grResultHO.GrNo;
                        }
                        else if (m_parm.BlInfo != null)
                        {
                            m_blResultHO = m_parm.BlInfo;
                            txtHOGRBL.Text = m_blResultHO.Bl;
                        }
                        GetCargoHandlingOutList();
                    }
                    #endregion
                }

                // Only Break Bulk case should input Package Type.
                SetPrimaryPkgType();

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

        private void SetPrimaryPkgType()
        {
            // Only Break Bulk case should input Package Type.
            if (m_tab == HWC101.TAB_HI)
            {
                if (m_itemHI != null)
                {
                    this.txtHIPkgTp.isMandatory = "BBK".Equals(m_itemHI.cgTpCd);
                }
            }
            else if (m_tab == HWC101.TAB_HO)
            {
                if (m_itemHO != null)
                {
                    this.txtHOPkgTp.isMandatory = "BBK".Equals(m_itemHO.cgTpCd);
                }
            }

        }

        private bool Validate()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (m_tab == HWC101.TAB_HI)
                {
                    // Validate PkgTp
                    if (!CommonUtility.IsValidPkgTp(txtHIPkgTp.Text.Trim()))
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0070"));
                        txtHIPkgTp.SelectAll();
                        txtHIPkgTp.Focus();
                        return false;
                    }

                    // Validate end time
                    if (chkHIGP.Checked)
                    {
                        if (string.IsNullOrEmpty(txtHIDtEnd.Text))
                        {
                            CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HWC101_0007"));
                            return false;
                        }
                    }

                    // Validate lorry number
                    if (!ValidateLorryNo(HWC101.TAB_HI))
                    {
                        return false;
                    }

                    // Check if start time and end time are within work date & shift.
                    if (!CommonUtility.ValidateStartEndDtWithinShift(txtHIDtStart, txtHIDtEnd))
                    {
                        return false;
                    }

                    // Have to set location before confirming
                    if (string.IsNullOrEmpty(txtHIActLoc.Text))
                    {
                        double actMt = CommonUtility.ParseDouble(txtHIActMT.Text);
                        double actM3 = CommonUtility.ParseDouble(txtHIActM3.Text);
                        double actQty = CommonUtility.ParseInt(txtHIActQty.Text);
                        if (actMt != 0 || actM3 != 0 || actQty != 0)
                        {
                            CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HWC101_0001"));
                            btnHISetLocAct.Focus();
                            return false;
                        }
                    }

                    //if (txtHIShutLoc.Enabled &&
                    //    string.IsNullOrEmpty(txtHIShutLoc.Text))
                    //{
                    //    double shuMt = CommonUtility.ParseDouble(txtHISOutMT.Text);
                    //    double shuM3 = CommonUtility.ParseDouble(txtHISOutM3.Text);
                    //    double shuQty = CommonUtility.ParseInt(txtHISOutQty.Text);
                    //    if (shuMt != 0 || shuM3 != 0 || shuQty != 0)
                    //    {
                    //        CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HWC101_0003"));
                    //        btnHISetLocSOut.Focus();
                    //        return false;
                    //    }
                    //}

                    //if (txtHIDmgLoc.Enabled &&
                    //    string.IsNullOrEmpty(txtHIDmgLoc.Text))
                    //{
                    //    double dmgMt = CommonUtility.ParseDouble(txtHIDmgMT.Text);
                    //    double dmgM3 = CommonUtility.ParseDouble(txtHIDmgM3.Text);
                    //    double dmgQty = CommonUtility.ParseInt(txtHIDmgQty.Text);
                    //    if (dmgMt != 0 || dmgM3 != 0 || dmgQty != 0)
                    //    {
                    //        CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HWC101_0002"));
                    //        btnHISetLocDmg.Focus();
                    //        return false;
                    //    }
                    //}
                }
                else if (m_tab == HWC101.TAB_HO)
                {
                    // Validate lorry number
                    if (!ValidateLorryNo(HWC101.TAB_HO))
                    {
                        return false;
                    }

                    // Validate PkgTp
                    if (!CommonUtility.IsValidPkgTp(txtHOPkgTp.Text.Trim()))
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0070"));
                        txtHOPkgTp.SelectAll();
                        txtHOPkgTp.Focus();
                        return false;
                    }

                    // Validate amount
                    double mt = CommonUtility.ParseDouble(txtHOLoadMT.Text);
                    double m3 = CommonUtility.ParseDouble(txtHOLoadM3.Text);
                    int qty = CommonUtility.ParseInt(txtHOLoadQty.Text);
                    if (mt <= 0 && m3 <= 0 && qty <= 0)
                    {
                        CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HWC101_0011"));
                        return false;
                    }

                    // Check if start time and end time are within work date & shift.
                    if (!CommonUtility.ValidateStartEndDtWithinShift(txtHODtStart, txtHODtEnd))
                    {
                        return false;
                    }

                    // Need to validate cargo Status and block with message if cargo is Hold
                    if (!Constants.CLEARANCE_RELEASE.Equals(CommonUtility.GetComboboxSelectedValue(cboHOClearance)))
                    {
                        // Clearance status is not RELEASE. Do you want to proceed ?
                        if (CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HWC101_0005")) == DialogResult.No)
                        {
                            return false;
                        }
                    }

                    // Auto deallocation
                    m_autoLocFlag = GetActAutoDeallocationFlag();
                    if (m_manualActLocFlag)
                    {
                        return false;
                    }
                    if (m_autoLocFlag)
                    {
                        if (DialogResult.No == CommonUtility.ConfirmMessage("Do you want to unset location automatically ?"))
                        {
                            CommonUtility.AlertMessage("Please unset location manually before proceeding.");
                            return false;
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

        private bool ValidateLorryNo(int tab)
        {
            if (tab == HWC101.TAB_HI)
            {
                if (string.IsNullOrEmpty(txtHILorry.Text))
                {
                    CommonUtility.AlertMessage("Please input lorry number.");
                    txtHILorry.Focus();
                    return false;
                }

                // Validate lorry number.
                if (!CommonUtility.IsValidRegisterationLorry(txtHILorry.Text, txtHITsptr.Text))
                {
                    CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HCM0032"));
                    txtHILorry.SelectAll();
                    txtHILorry.Focus();
                    return false;
                }
            }
            else if (tab == HWC101.TAB_HO)
            {
                if (string.IsNullOrEmpty(txtHOLorry.Text))
                {
                    CommonUtility.AlertMessage("Please input lorry number.");
                    txtHOLorry.Focus();
                    return false;
                }

                // Validate lorry number.
                if (m_grResultHO != null || !"BBK".Equals(m_itemHO.cgTpCd))
                {
                    // GR case -> Check registeration lorry
                    if (!CommonUtility.IsValidRegisterationLorry(txtHOLorry.Text, txtHOTsptr.Text))
                    {
                        CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HCM0032"));
                        txtHOLorry.SelectAll();
                        txtHOLorry.Focus();
                        return false;
                    }
                }
                else if (m_blResultHO != null)
                {
                    // BL case -> Check assignment lorry
                    if (!CommonUtility.IsValidAssignmentLorry(m_blResultHO.VslCallId, m_blResultHO.Bl, txtHOLorry.Text, txtHOTsptr.Text))
                    {
                        CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HCM0032"));
                        txtHOLorry.SelectAll();
                        txtHOLorry.Focus();
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Get auto deallocation flag of Actual cargo.
        /// Return true in case of: 
        /// Location is empty && 
        /// (1 cell partial load || 1 cell full load || 2 cell full load)
        /// </summary>
        /// <returns></returns>
        private bool GetActAutoDeallocationFlag()
        {
            m_manualActLocFlag = false;
            bool bAutoFlag = false;
            try
            {
                if (string.IsNullOrEmpty(txtHOLoadLoc.Text))
                {
                    bool isBBK = false;
                    bool isDBK = false;
                    double balMt = m_itemHO.balMt;
                    int balQty = m_itemHO.balQty;
                    double actMt = CommonUtility.ParseDouble(txtHOLoadMT.Text);
                    int actQty = CommonUtility.ParseInt(txtHOLoadQty.Text);
                    if ("BBK".Equals(m_itemHO.cgTpCd))
                    {
                        isBBK = true;
                    }
                    else if ("DBK".Equals(m_itemHO.cgTpCd) || "DBE".Equals(m_itemHO.cgTpCd) || "DBN".Equals(m_itemHO.cgTpCd))
                    {
                        isDBK = true;
                    }
                    bool bHasValue = (isBBK && (actMt > 0 || actQty > 0)) || (isDBK && actMt > 0) ? true : false;

                    if (bHasValue)
                    {
                        if (m_itemHO.locCount == 1)
                        {
                            bAutoFlag = true;
                        }
                        else if (m_itemHO.locCount > 1)
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
                            m_manualActLocFlag = true;
                            CommonUtility.AlertMessage("Please unset location manually.");
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

        private string GetFinalOprYN()
        {
            double totMt;
            int totQty;
            double balMt = 0;
            int balQty = 0;
            bool isBBK;
            bool isDBK;
            DialogResult dr;

            if (m_tab == HWC101.TAB_HI)
            {
                isBBK = (m_itemHI != null && "BBK".Equals(m_itemHI.cgTpCd)) ? true : false;
                isDBK = (m_itemHI != null &&
                            ("DBK".Equals(m_itemHI.cgTpCd) ||
                            "DBE".Equals(m_itemHI.cgTpCd) ||
                            "DBN".Equals(m_itemHI.cgTpCd))) ? true : false;

                if (chkHIFinal.Checked)
                {
                    return "Y";
                }
                else
                {
                    // Check if handling-in amount == balance amount
                    // Handling-in amount = Lorry Load + Shut-out + Damage
                    if (m_itemHI != null)
                    {
                        balMt = m_itemHI.balMt;
                        balQty = m_itemHI.balQty;
                        //Added by Chris 2016-03-09 for Final Handling In
                        Boolean weight = false;
                        if (Double.Parse(m_itemHI.snM3.ToString()) == 0 && Double.Parse(m_itemHI.snMt.ToString()) == 0)
                        {
                            if (((Double.Parse(m_itemHI.accuSumQty.ToString()) + Double.Parse(txtHIAccQty.Text)) >= Double.Parse(m_itemHI.snQty.ToString())))
                                weight = true;
                        }
                        else if (Double.Parse(m_itemHI.snM3.ToString()) == 0 && Double.Parse(m_itemHI.snQty.ToString()) == 0)
                        {
                            if (((Double.Parse(m_itemHI.accuSumWgt.ToString()) + Double.Parse(txtHIActMT.Text)) >= Double.Parse(m_itemHI.snMt.ToString())))
                                weight = true;
                        }
                        else if (Double.Parse(m_itemHI.snQty.ToString()) == 0 && Double.Parse(m_itemHI.snMt.ToString()) == 0)
                        {
                            if (((Double.Parse(m_itemHI.accuSumMsrmt.ToString()) + Double.Parse(txtHIActM3.Text)) >= Double.Parse(m_itemHI.snM3.ToString())))
                                weight = true;
                        }
                        else if (Double.Parse(m_itemHI.snM3.ToString()) == 0)
                        {
                            if (((Double.Parse(m_itemHI.accuSumWgt.ToString()) + Double.Parse(txtHIActMT.Text)) >= Double.Parse(m_itemHI.snMt.ToString())) || ((Double.Parse(m_itemHI.accuSumQty.ToString()) + Double.Parse(txtHIActQty.Text)) >= Double.Parse(m_itemHI.snQty.ToString())))
                                weight = true;
                        }
                        else if (Double.Parse(m_itemHI.snMt.ToString()) == 0)
                        {
                            if (((Double.Parse(m_itemHI.accuSumMsrmt.ToString()) + Double.Parse(txtHIActM3.Text)) >= Double.Parse(m_itemHI.snM3.ToString())) || ((Double.Parse(m_itemHI.accuSumQty.ToString()) + Double.Parse(txtHIAccQty.Text)) >= Double.Parse(m_itemHI.snQty.ToString())))
                                weight = true;
                        }
                        else if (double.Parse(m_itemHI.snQty.ToString()) == 0)
                        {
                            if (((Double.Parse(m_itemHI.accuSumMsrmt.ToString()) + Double.Parse(txtHIAccM3.Text)) >= Double.Parse(m_itemHI.snM3.ToString())) || ((Double.Parse(m_itemHI.accuSumWgt.ToString()) + Double.Parse(txtHIActMT.Text)) >= Double.Parse(m_itemHI.snMt.ToString())))
                                weight = true;
                        }
                        else if (((Double.Parse(m_itemHI.accuSumMsrmt.ToString()) + Double.Parse(txtHIActM3.Text)) >= Double.Parse(m_itemHI.snM3.ToString())) || ((Double.Parse(m_itemHI.accuSumWgt.ToString()) + Double.Parse(txtHIActMT.Text)) >= Double.Parse(m_itemHI.snMt.ToString())) || ((Double.Parse(m_itemHI.accuSumQty.ToString()) + Double.Parse(txtHIActQty.Text)) >= Double.Parse(m_itemHI.snQty.ToString())))
                        {
                            weight = true;
                        }
                        if (weight)
                        {
                            //CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HCM0076"));
                            //return "Y";
                            dr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0066"));
                            if (dr == DialogResult.Yes)
                            {
                                return "Y";
                            }
                            else if (dr == DialogResult.No)
                            {
                                return "N";
                            }
                        }
                        //End-----------------------------------
                    }
                    //totMt = CommonUtility.ParseDouble(txtHIActMT.Text) +
                    //        //CommonUtility.ParseDouble(txtHISOutMT.Text) +
                    //        CommonUtility.ParseDouble(txtHIDmgMT.Text);
                    //totQty = CommonUtility.ParseInt(txtHIActQty.Text) +
                    //        //CommonUtility.ParseInt(txtHISOutQty.Text) +
                    //        CommonUtility.ParseInt(txtHIDmgQty.Text);

                    if (isBBK)
                    {
                        //if (totMt >= balMt || totQty >= balQty)
                        //{
                        //    dr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0066"));
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
                        //if (totMt >= balMt)
                        //{
                        //    dr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0066"));
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
            }
            else if (m_tab == HWC101.TAB_HO)
            {
                isBBK = (m_itemHO != null && "BBK".Equals(m_itemHO.cgTpCd)) ? true : false;
                isDBK = (m_itemHO != null &&
                            ("DBK".Equals(m_itemHO.cgTpCd) ||
                            "DBE".Equals(m_itemHO.cgTpCd) ||
                            "DBN".Equals(m_itemHO.cgTpCd))) ? true : false;

                if (chkHOFinal.Checked)
                {
                    return "Y";
                }
                else
                {
                    // Check if handling-out amount == balance amount
                    if (m_itemHO != null)
                    {
                        balMt = m_itemHO.balMt;
                        balQty = m_itemHO.balQty;
                    }
                    totMt = CommonUtility.ParseDouble(txtHOLoadMT.Text);
                    totQty = CommonUtility.ParseInt(txtHOLoadQty.Text);

                    if (isBBK)
                    {
                        if (totMt >= balMt || totQty >= balQty)
                        {
                            dr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0067"));
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
                            dr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0067"));
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
            }

            return "N";
        }

        private void SetEnableHI(bool bEnable)
        {
            txtHILorry.Enabled = bEnable;
            txtHIDtStart.Enabled = bEnable;
            txtHIDtEnd.Enabled = bEnable;
            txtHIActMT.Enabled = bEnable;
            txtHIActM3.Enabled = bEnable;
            txtHIActQty.Enabled = bEnable;
            //txtHISOutMT.Enabled = bEnable;
            //txtHISOutM3.Enabled = bEnable;
            //txtHISOutQty.Enabled = bEnable;
            //txtHIDmgMT.Enabled = bEnable;
            //txtHIDmgM3.Enabled = bEnable;
            //txtHIDmgQty.Enabled = bEnable;
            txtHIActLoc.Enabled = bEnable;
            //txtHIShutLoc.Enabled = bEnable;
            //txtHIDmgLoc.Enabled = bEnable;
            txtHIPkgNo.Enabled = bEnable;
            txtHIPkgTp.Enabled = bEnable;
            //cboHIHndlDmg.Enabled = bEnable;
            //cboHIHndlSOut.Enabled = bEnable;
            btnHIConfirm.Enabled = bEnable;
            btnF1.Enabled = bEnable;
            btnHIPkgTp.Enabled = bEnable;
            btnHISetLocAct.Enabled = bEnable;
            //btnHISetLocDmg.Enabled = bEnable;
            //btnHISetLocSOut.Enabled = bEnable;
            btnHIExit.Enabled = bEnable;
            chkHIFinal.Enabled = bEnable;
            btnHISOut.Enabled = bEnable;
            btnHIDmg.Enabled = bEnable;
            btnGR.Enabled = bEnable;
        }

        private void SetEnableHO(bool bEnable)
        {
            txtHOLorry.Enabled = bEnable;
            txtHODtStart.Enabled = bEnable;
            txtHODtEnd.Enabled = bEnable;
            txtHOLoadMT.Enabled = bEnable;
            txtHOLoadM3.Enabled = bEnable;
            txtHOLoadQty.Enabled = bEnable;
            txtHOPkgNo.Enabled = bEnable;
            txtHOPkgTp.Enabled = bEnable;
            txtHOLoadLoc.Enabled = bEnable;
            chkHOFinal.Enabled = bEnable;
            btnHOConfirm.Enabled = bEnable;
            btnF3.Enabled = bEnable;
            btnF4.Enabled = bEnable;
            btnHOUnsetLoc.Enabled = bEnable;
            btnHOExit.Enabled = bEnable;
            chkHOFinal.Enabled = bEnable;
            btnBL.Enabled = bEnable;
            btnHOGP.Enabled = bEnable;
        }

        private void SetEnableControls()
        {
            if (m_tab == HWC101.TAB_HI)
            {
                SetEnableHO(false);
            }
            else if (m_tab == HWC101.TAB_HO)
            {
                SetEnableHI(false);
            }
        }

        private void GetCargoHandlingInList()
        {
            try
            {
                //Cursor.Current = Cursors.WaitCursor;
                Action<object> proc = delegate(object state)
                {
                    #region SN Amt, GR Amt, Lorry No, In Time
                    // Request Webservice
                    IWHCheckerProxy acProxy = new WHCheckerProxy();
                    CargoHandlingInParm cgHIParm = new CargoHandlingInParm();
                    cgHIParm.vslCallId = m_grResultHI.VslCallId;
                    if (!String.IsNullOrEmpty(txtHIGR.Text))
                    {
                        cgHIParm.grNo = txtHIGR.Text;
                    }

                    ResponseInfo cgHIInfo = acProxy.getCargoHandlingInList(cgHIParm);
                    //Cargo Type
                    CommonUtility.InitializeCombobox(cboHICargoType);
                    for (int i = 0; i < cgHIInfo.list.Length; i++)
                    {
                        // Operation Mode
                        if (cgHIInfo.list[i] is CodeMasterListItem)
                            cgHIInfo.list[i] = CommonUtility.ToCodeMasterListItem1(cgHIInfo.list[i] as CodeMasterListItem);
                        if (cgHIInfo.list[i] is CodeMasterListItem1)
                        {
                            CodeMasterListItem1 item = (CodeMasterListItem1)cgHIInfo.list[i];
                            if ("CGTPNLQ".Equals(item.mcd))
                            {
                                cboHICargoType.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scd));
                            }
                        }
                    }

                    // Display Data
                    if (cgHIInfo != null && cgHIInfo.list.Length > 0 && cgHIInfo.list[0] is CargoHandlingInItem)
                    {
                        CargoHandlingInItem item = (CargoHandlingInItem)cgHIInfo.list[0];
                        m_itemHI = item;

                        m_itemHI.cgTpCd = item.cgTpCd;
                        txtHIPkgTp.Text = item.repkgTypeCd;
                        txtHILorry.Text = item.lorryId;
                        txtHISNMT.Text = item.snMt.ToString();
                        txtHISNM3.Text = item.snM3.ToString();
                        txtHISNQty.Text = item.snQty.ToString();
                        txtHIAccMT.Text = item.accuSumWgt.ToString();
                        txtHIAccM3.Text = item.accuSumMsrmt.ToString();
                        txtHIAccQty.Text = item.accuSumQty.ToString();
                        txtHITsptr.Text = item.tsptr;

                        string strGRBalMt = item.balMt.ToString();
                        string strGRBalM3 = item.balM3.ToString();
                        string strGRBalQty = item.balQty.ToString();
                        txtHIGRMT.Text = strGRBalMt;
                        txtHIGRM3.Text = strGRBalM3;
                        txtHIGRQty.Text = strGRBalQty;
                        txtHIActMT.Text = strGRBalMt;
                        txtHIActM3.Text = strGRBalM3;
                        txtHIActQty.Text = strGRBalQty;
                        txtHIActLoc.Text = item.locId;
                        if ("S".Equals(item.spCaCoCd))
                        {
                            chkHISpare.Checked = true;
                        }

                        // Set mandatory items (fix issue 0022089)

                        txtHIActMT.isMandatory = true;
                        txtHIActM3.isMandatory = item.cgTpCd == "BBK" && item.delvTpCd == "I";
                        txtHIActQty.isMandatory = item.cgTpCd == "BBK";

                        SetPrimary();

                        this.selectGRPanel.jPVC = this.m_itemHI.vslCallId;
                    }
                    CommonUtility.SetComboboxSelectedItem(cboHICargoType, m_itemHI.cgTpCd);

                    // Validate delivery type
                    if (m_itemHI != null && "D".Equals(m_itemHI.delvTpCd) && !"S".Equals(m_itemHI.spCaCoCd))
                    {
                        CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HWC101_0006"));
                        return;
                    }
                    if ("S".Equals(m_itemHI.spCaCoCd))
                    {
                        ChkSpareCargo();
                    }
                    #endregion
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
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void GetCargoHandlingOutList()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Action<object> proc = delegate(object state)
                {
                    #region SN Amt, GR Amt, Lorry No, In Time
                    // Request Webservice
                    IWHCheckerProxy acProxy = new WHCheckerProxy();
                    CargoHandlingOutParm cgHOParm = new CargoHandlingOutParm();
                    cgHOParm.cgNo = txtHOGRBL.Text;
                    if (m_blResultHO != null)
                    {
                        cgHOParm.vslCallId = m_blResultHO.VslCallId;
                        cgHOParm.blNo = txtHOGRBL.Text;
                        cgHOParm.catgCd = "I";
                        cgHOParm.cgInOutCd = "O";
                    }
                    else if (m_grResultHO != null)
                    {
                        cgHOParm.vslCallId = m_grResultHO.VslCallId;
                        cgHOParm.grNo = txtHOGRBL.Text;
                        cgHOParm.catgCd = "E";
                        cgHOParm.cgInOutCd = "I";
                    }

                    ResponseInfo cgHOInfo = acProxy.getCargoHandlingOutList(cgHOParm);

                    //Cargo Type
                    CommonUtility.InitializeCombobox(cboHOCargoType);
                    for (int i = 0; i < cgHOInfo.list.Length; i++)
                    {
                        // Operation Mode
                        if (cgHOInfo.list[i] is CodeMasterListItem)
                            cgHOInfo.list[i] = CommonUtility.ToCodeMasterListItem1(cgHOInfo.list[i] as CodeMasterListItem);
                        if (cgHOInfo.list[i] is CodeMasterListItem1)
                        {
                            CodeMasterListItem1 item = (CodeMasterListItem1)cgHOInfo.list[i];
                            if ("CGTPNLQ".Equals(item.mcd))
                            {
                                cboHOCargoType.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scd));
                            }
                        }
                    }

                    // Display Data
                    if (cgHOInfo != null && cgHOInfo.list.Length > 0 && cgHOInfo.list[0] is CargoHandlingOutItem)
                    {
                        CargoHandlingOutItem item = (CargoHandlingOutItem)cgHOInfo.list[0];
                        m_itemHO = item;
                        txtHOPkgTp.Text = item.repkgTypeCd;
                        txtHILorry.Text = item.lorryId;
                        txtHODocMT.Text = item.docMt.ToString();
                        txtHODocM3.Text = item.docM3.ToString();
                        txtHODocQty.Text = item.docQty.ToString();
                        txtHOActMT.Text = item.actMt.ToString();
                        txtHOActM3.Text = item.actM3.ToString();
                        txtHOActQty.Text = item.actQty.ToString();
                        txtHOTsptr.Text = item.tsptr;

                        string txtBalMT = item.balMt.ToString();
                        string txtBalM3 = item.balM3.ToString();
                        string txtBalQty = item.balQty.ToString();
                        txtHOBalMT.Text = txtBalMT;
                        txtHOBalM3.Text = txtBalM3;
                        txtHOBalQty.Text = txtBalQty;
                        txtHOLoadMT.Text = txtBalMT;
                        txtHOLoadM3.Text = txtBalM3;
                        txtHOLoadQty.Text = txtBalQty;
                        CommonUtility.SetComboboxSelectedItem(cboHOClearance, item.custMode);
                        CommonUtility.SetComboboxSelectedItem(cboHOCargoType, m_itemHO.cgTpCd);

                        // Set mandatory items (fix issue 0022089)
                        /*
                        txtHOActMT.isMandatory = true;
                        txtHOActM3.isMandatory = item.cgTpCd == "BBK" && item.delvTpCd == "I";
                        txtHOActQty.isMandatory = item.cgTpCd == "BBK";
                        */
                        SetPrimary();

                        if (this.m_parm.GrInfo != null)
                            this.selectGRPanel.jPVC = this.m_itemHO.vslCallId;
                        else if (this.m_parm.BlInfo != null)
                            this.selectBLPanel.jPVC = this.m_itemHO.vslCallId;
                    }

                    // Validate D/O number
                    if (m_blResultHO != null && string.IsNullOrEmpty(m_blResultHO.DoNo))
                    {
                        CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HWC101_0009"));
                        return;
                    }

                    // Validate cargo type and category
                    if (m_itemHO != null && "E".Equals(m_itemHO.catgCd) && "G".Equals(m_itemHO.cgTpCd))
                    {
                        CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HWC101_0010"));
                        return;
                    }
                    #endregion
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
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool ProcessCargoHandlingInItem()
        {
            // ref: CT121008
            bool result = false;
            m_responseInfo = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ArrayList whConfigList = new ArrayList();
                IWHCheckerProxy proxy = new WHCheckerProxy();
                CargoHandlingInItem item;
                if (m_itemHI != null)
                {
                    item = m_itemHI;
                }
                else
                {
                    return false;
                }

                item.loadCnclMode = CommonUtility.ToString(item.loadCnclMode);
                item.cgTpCd = CommonUtility.ToString(item.cgTpCd);
                item.shutRhdlMode = CommonUtility.ToString(item.shutRhdlMode);
                item.dmgRhdlMode = CommonUtility.ToString(item.dmgRhdlMode);
                item.rhdlYn = CommonUtility.ToString(item.rhdlYn);
                item.fnlDelvYn = CommonUtility.ToString(item.fnlDelvYn);

                item.cgNo = m_grResultHI.GrNo;
                item.grNo = m_grResultHI.GrNo;
                item.vslCallId = m_grResultHI.VslCallId;
                item.shipgNoteNo = m_grResultHI.ShipgNoteNo;
                item.snQty = CommonUtility.ParseInt(txtHISNQty.Text);
                item.snMt = CommonUtility.ParseDouble(txtHISNMT.Text);
                item.snM3 = CommonUtility.ParseDouble(txtHISNM3.Text);
                item.balQty = CommonUtility.ParseInt(txtHIGRQty.Text);
                item.balMt = CommonUtility.ParseDouble(txtHIGRMT.Text);
                item.balM3 = CommonUtility.ParseDouble(txtHIGRM3.Text);
                item.pkgQty = CommonUtility.ParseInt(txtHIActQty.Text);
                item.wgt = CommonUtility.ParseDouble(txtHIActMT.Text);
                item.msrmt = CommonUtility.ParseDouble(txtHIActM3.Text);
                item.hdlInStDt = txtHIDtStart.Text;
                item.hdlInEndDt = txtHIDtEnd.Text;
                item.stat = m_itemHI.stat;
                item.shftId = UserInfo.getInstance().Shift;
                item.shftNm = UserInfo.getInstance().ShiftNm;
                item.shftDt = UserInfo.getInstance().Workdate;
                item.tsptr = m_itemHI.tsptr;
                item.tsptTpCd = m_itemHI.tsptTpCd;
                item.lorryId = txtHILorry.Text;
                //item.seq
                //item.lorryFlag
                if (m_resHILocAct != null)
                {
                    item.locId = m_resHILocAct.LocId;
                }
                item.catgCd = m_itemHI.catgCd;
                item.cmdtCd = m_itemHI.cmdtCd;
                item.pkgTpCd = m_itemHI.pkgTpCd;
                item.wgtUnit = m_itemHI.wgtUnit;
                item.msrmtUnit = m_itemHI.msrmtUnit;
                item.portOfLoad = m_itemHI.portOfLoad;
                item.portOfDis = m_itemHI.portOfDis;
                item.fdest = m_itemHI.fdest;
                item.cgTpCd = m_itemHI.cgTpCd;
                item.fwrAgnt = m_itemHI.fwrAgnt;
                item.shpgAgent = m_itemHI.shpgAgent;
                item.cntryOfOrg = m_itemHI.cntryOfOrg;
                item.cgInOutCd = "I";
                //item.shuQty = CommonUtility.ParseInt(txtHISOutQty.Text);
                //item.shuMt = CommonUtility.ParseDouble(txtHISOutMT.Text);
                //item.shuM3 = CommonUtility.ParseDouble(txtHISOutM3.Text);
                item.shuQty = CommonUtility.ParseInt(this.pnlHISOut.txtHISOutQty.Text);
                item.shuMt = CommonUtility.ParseDouble(this.pnlHISOut.txtHISOutMT.Text);
                item.shuM3 = CommonUtility.ParseDouble(this.pnlHISOut.txtHISOutM3.Text);
                if (m_resHILocSOut != null)
                {
                    item.shuLocId = m_resHILocSOut.LocId;
                }
                //item.dmgQty = CommonUtility.ParseInt(txtHIDmgQty.Text);
                //item.dmgMt = CommonUtility.ParseDouble(txtHIDmgMT.Text);
                //item.dmgM3 = CommonUtility.ParseDouble(txtHIDmgM3.Text);
                item.dmgQty = CommonUtility.ParseInt(this.pnlHIDmg.txtHIDmgQty.Text);
                item.dmgMt = CommonUtility.ParseDouble(this.pnlHIDmg.txtHIDmgMT.Text);
                item.dmgM3 = CommonUtility.ParseDouble(this.pnlHIDmg.txtHIDmgM3.Text);
                if (m_resHILocDmg != null)
                {
                    item.dmgLocId = m_resHILocDmg.LocId;
                }
                //item.loadCnclMode
                //item.rhdlYn
                item.shutRhdlMode = CommonUtility.GetComboboxSelectedValue(this.pnlHISOut.cboHIHndlSOut);
                //item.dmgRhdlMode = CommonUtility.GetComboboxSelectedValue(cboHIHndlDmg);
                item.gatePassYn = chkHIGP.Checked;
                item.CRUD = Constants.WS_INSERT;
                item.userId = UserInfo.getInstance().UserId;
                //item.collection
                item.pkgNo = txtHIPkgNo.Text;
                item.repkgTypeCd = txtHIPkgTp.Text;
                item.rmk = txtHIRemark.Text;
                item.fnlOpeYn = GetFinalOprYN();
                item.accuSumQty = m_itemHI.accuSumQty;
                item.accuSumWgt = m_itemHI.accuSumWgt;
                item.accuSumMsrmt = m_itemHI.accuSumMsrmt;
                item.shpr = m_itemHI.shpr;
                item.cnsne = m_itemHI.cnsne;

                if ("BBK".Equals(item.cgTpCd))
                {
                    // Shut-out
                    if ((!string.IsNullOrEmpty(this.pnlHISOut.txtHISOutMT.Text) && CommonUtility.ParseDouble(this.pnlHISOut.txtHISOutMT.Text) > 0) ||
                        (!string.IsNullOrEmpty(this.pnlHISOut.txtHISOutM3.Text) && CommonUtility.ParseDouble(this.pnlHISOut.txtHISOutM3.Text) > 0))
                    {
                        item.loadCnclMode = "Y";
                        if ("R".Equals(CommonUtility.GetComboboxSelectedValue(this.pnlHISOut.cboHIHndlSOut)) ||
                            "C".Equals(CommonUtility.GetComboboxSelectedValue(this.pnlHISOut.cboHIHndlSOut)))
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
                    //if ((!string.IsNullOrEmpty(txtHIDmgMT.Text) && CommonUtility.ParseDouble(txtHIDmgMT.Text) > 0) ||
                    //    (!string.IsNullOrEmpty(txtHIDmgM3.Text) && CommonUtility.ParseDouble(txtHIDmgM3.Text) > 0))
                    //{
                    //    item.loadCnclMode = "Y";
                    //    if ("R".Equals(CommonUtility.GetComboboxSelectedValue(cboHIHndlDmg)))
                    //    {
                    //        item.rhdlYn = "Y";
                    //    }
                    //    else
                    //    {
                    //        if (!"Y".Equals(item.rhdlYn))
                    //        {
                    //            item.rhdlYn = "N";
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (!"Y".Equals(item.loadCnclMode))
                    //    {
                    //        item.loadCnclMode = "N";
                    //    }
                    //}
                }
                else if ("DBK".Equals(item.cgTpCd) ||
                    "DBE".Equals(item.cgTpCd) ||
                    "DBN".Equals(item.cgTpCd))
                {
                    // Shut-out
                    if ((!string.IsNullOrEmpty(this.pnlHISOut.txtHISOutMT.Text) && CommonUtility.ParseDouble(this.pnlHISOut.txtHISOutMT.Text) > 0))
                    {
                        item.loadCnclMode = "Y";
                        if ("R".Equals(CommonUtility.GetComboboxSelectedValue(this.pnlHISOut.cboHIHndlSOut)) ||
                            "C".Equals(CommonUtility.GetComboboxSelectedValue(this.pnlHISOut.cboHIHndlSOut)))
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
                    //if ((!string.IsNullOrEmpty(txtHIDmgMT.Text) && CommonUtility.ParseDouble(txtHIDmgMT.Text) > 0))
                    //{
                    //    item.loadCnclMode = "Y";
                    //    if ("R".Equals(CommonUtility.GetComboboxSelectedValue(cboHIHndlDmg)))
                    //    {
                    //        item.rhdlYn = "Y";
                    //    }
                    //    else
                    //    {
                    //        if (!"Y".Equals(item.rhdlYn))
                    //        {
                    //            item.rhdlYn = "N";
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (!"Y".Equals(item.loadCnclMode))
                    //    {
                    //        item.loadCnclMode = "N";
                    //    }
                    //}
                }

                item.lorryFlag = false;

                // Set/Unset location - saving WhConfigurationItem
                if (m_resHILocAct != null && m_resHILocAct.ArrWHLocation != null && m_resHILocAct.ArrWHLocation.Count > 0)
                {
                    whConfigList.AddRange(m_resHILocAct.ArrWHLocation);
                }
                if (m_resHILocSOut != null && m_resHILocSOut.ArrWHLocation != null && m_resHILocSOut.ArrWHLocation.Count > 0)
                {
                    whConfigList.AddRange(m_resHILocSOut.ArrWHLocation);
                }
                if (m_resHILocDmg != null && m_resHILocDmg.ArrWHLocation != null && m_resHILocDmg.ArrWHLocation.Count > 0)
                {
                    whConfigList.AddRange(m_resHILocDmg.ArrWHLocation);
                }
                if (whConfigList.Count > 0)
                {
                    item.collection = whConfigList.ToArray();
                }
                if ("S".Equals(item.spCaCoCd) && "D".Equals(item.delvTpCd))
                {
                    WhConfigurationItem whItem = new WhConfigurationItem();
                    whItem.locId = item.spLocId;
                    whItem.whTpCd = "G";
                    whItem.spCaCoCd = item.spCaCoCd;
                    whItem.wgt = item.wgt;
                    whItem.msrmt = item.msrmt;
                    whItem.pkgQty = item.pkgQty.ToString();
                    whItem.vslCallId = item.vslCallId;
                    whItem.cgNo = item.cgNo;
                    ArrayList whSpList = new ArrayList();
                    whSpList.Add(whItem);
                    item.collection = whSpList.ToArray();
                }
                Object[] obj = new Object[] { item };
                DataItemCollection dataCollection = new DataItemCollection();
                dataCollection.collection = obj;

                m_responseInfo = proxy.processCargoHandlingInItem(dataCollection);
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

        private bool ProcessCargoHandlingOutItem()
        {
            // ref: CT121009
            bool result = false;
            m_responseInfo = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ArrayList whConfigList = new ArrayList();
                IWHCheckerProxy proxy = new WHCheckerProxy();
                CargoHandlingOutItem item;
                if (m_itemHO != null)
                {
                    item = m_itemHO;
                }
                else
                {
                    return false;
                }

                item.catgCd = CommonUtility.ToString(item.catgCd);
                item.cgTpCd = CommonUtility.ToString(item.cgTpCd);
                item.dmgYn = CommonUtility.ToString(item.dmgYn);
                item.shuYn = CommonUtility.ToString(item.shuYn);
                item.delvTpCd = CommonUtility.ToString(item.delvTpCd);
                item.stat = CommonUtility.ToString(item.stat);
                item.fnlDelvYn = CommonUtility.ToString(item.fnlDelvYn);
                item.jobCoCd = "G";
                item.autoLocFlag = m_autoLocFlag ? "true" : "false";

                if (m_grResultHO != null)
                {
                    item.vslCallId = m_grResultHO.VslCallId;
                    item.grNo = txtHOGRBL.Text;
                }
                else if (m_blResultHO != null)
                {
                    item.vslCallId = m_blResultHO.VslCallId;
                    item.blNo = txtHOGRBL.Text;
                    item.doNo = m_blResultHO.DoNo;
                }
                item.cgNo = txtHOGRBL.Text;
                //item.actQty = CommonUtility.ParseInt(txtHODocQty.Text);
                //item.actMt = CommonUtility.ParseDouble(txtHODocMT.Text);
                //item.actM3 = CommonUtility.ParseDouble(txtHODocM3.Text);
                //item.qty = CommonUtility.ParseInt(txtHOActQty.Text);
                //item.mt = CommonUtility.ParseDouble(txtHOActMT.Text);
                //item.m3 = CommonUtility.ParseDouble(txtHOActM3.Text);
                //item.balQty = CommonUtility.ParseInt(txtHOBalQty.Text);
                //item.balMt = CommonUtility.ParseDouble(txtHOBalMT.Text);
                //item.balM3 = CommonUtility.ParseDouble(txtHOBalM3.Text);
                item.loadQty = CommonUtility.ParseInt(txtHOLoadQty.Text);
                item.loadMt = CommonUtility.ParseDouble(txtHOLoadMT.Text);
                item.loadM3 = CommonUtility.ParseDouble(txtHOLoadM3.Text);


                //item.gateInDt = m_itemHO.gateInDt;
                //item.stat = m_itemHO.stat;
                item.hdlOutStDt = txtHODtStart.Text;
                item.hdlOutEndDt = txtHODtEnd.Text;
                item.delvTpCd = "I";
                //item.hatchNo = m_itemHO.hatchNo;
                item.shftDt = UserInfo.getInstance().Workdate;
                item.shftId = UserInfo.getInstance().Shift;
                //item.tsptr = m_itemHO.tsptr;
                item.lorryId = txtHOLorry.Text;
                if (m_resHOLoc != null)
                {
                    item.locId = m_resHOLoc.LocId;
                }
                //item.catgCd = m_itemHO.catgCd;
                item.whFnlDelvYn = GetFinalOprYN();
                //item.actlDelvTpCd = m_itemHO.actlDelvTpCd;
                //item.disEndDt = m_itemHO.disEndDt;
                //item.cgTpCd = m_itemHO.cgTpCd;
                item.CRUD = Constants.WS_INSERT;
                item.userId = UserInfo.getInstance().UserId;

                item.pkgNo = txtHIPkgNo.Text;
                item.repkgTypeCd = txtHIPkgTp.Text;
                item.rmk = txtHORemark.Text;

                // Set/Unset location - saving WhConfigurationItem
                if (m_resHOLoc != null && m_resHOLoc.ArrWHLocation != null && m_resHOLoc.ArrWHLocation.Count > 0)
                {
                    whConfigList.AddRange(m_resHOLoc.ArrWHLocation);
                }
                if (whConfigList.Count > 0)
                {
                    item.collection = whConfigList.ToArray();
                }

                Object[] obj = new Object[] { item };
                DataItemCollection dataCollection = new DataItemCollection();
                dataCollection.collection = obj;

                m_responseInfo = proxy.processCargoHandlingOutItem(dataCollection);
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

        #region Event listener
        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnHIConfirm":
                    ConfirmHI();
                    break;
                case "btnHOConfirm":
                    ConfirmHO();
                    break;
                case "btnHISetLocAct":
                    HWC101001Parm parmActLoc = new HWC101001Parm();
                    parmActLoc.VslCallId = m_grResultHI.VslCallId;
                    parmActLoc.ShipgNoteNo = m_grResultHI.ShipgNoteNo;
                    if (m_resHILocAct != null && !string.IsNullOrEmpty(m_resHILocAct.LocId))
                    {
                        parmActLoc.LocId = m_resHILocAct.LocId;
                        parmActLoc.WhId = m_resHILocAct.LocId.Substring(0, m_resHILocAct.LocId.IndexOf("("));
                    }
                    parmActLoc.TotMt = txtHIActMT.Text;
                    parmActLoc.TotM3 = txtHIActM3.Text;
                    parmActLoc.TotQty = txtHIActQty.Text;
                    parmActLoc.CgNo = txtHIGR.Text;
                    parmActLoc.WhTpCd = "G";    // General: G, Shut-out: S, Damage: D
                    parmActLoc.JobNo = m_itemHI.jobNo;

                    HWC101001Result resultActLoc = (HWC101001Result)PopupManager.instance.ShowPopup(new HWC101001(), parmActLoc);
                    if (resultActLoc != null)
                    {
                        m_resHILocAct = resultActLoc;
                        txtHIActLoc.Text = m_resHILocAct.LocId;
                    }
                    break;

                case "btnHISetLocSOut":
                    HWC101001Parm parmShutLoc = new HWC101001Parm();
                    parmShutLoc.VslCallId = m_grResultHI.VslCallId;
                    parmShutLoc.ShipgNoteNo = m_grResultHI.ShipgNoteNo;
                    if (m_resHILocSOut != null && !string.IsNullOrEmpty(m_resHILocSOut.LocId))
                    {
                        parmShutLoc.LocId = m_resHILocSOut.LocId;
                        parmShutLoc.WhId = m_resHILocSOut.LocId.Substring(0, m_resHILocSOut.LocId.IndexOf("("));
                    }
                    //parmShutLoc.TotMt = txtHISOutMT.Text;
                    //parmShutLoc.TotM3 = txtHISOutM3.Text;
                    //parmShutLoc.TotQty = txtHISOutQty.Text;
                    parmShutLoc.CgNo = txtHIGR.Text;
                    parmShutLoc.WhTpCd = "S";    // General: G, Shut-out: S, Damage: D
                    parmShutLoc.JobNo = m_itemHI.jobNo;

                    HWC101001Result resultShutLoc = (HWC101001Result)PopupManager.instance.ShowPopup(new HWC101001(), parmShutLoc);
                    if (resultShutLoc != null)
                    {
                        m_resHILocSOut = resultShutLoc;
                        //txtHIShutLoc.Text = m_resHILocSOut.LocId;
                    }
                    break;

                case "btnHISetLocDmg":
                    //HWC101001Parm parmDmgLoc = new HWC101001Parm();
                    //parmDmgLoc.VslCallId = m_grResultHI.VslCallId;
                    //parmDmgLoc.ShipgNoteNo = m_grResultHI.ShipgNoteNo;
                    //if (m_resHILocDmg != null && !string.IsNullOrEmpty(m_resHILocDmg.LocId))
                    //{
                    //    parmDmgLoc.LocId = m_resHILocDmg.LocId;
                    //    parmDmgLoc.WhId = m_resHILocDmg.LocId.Substring(0, m_resHILocDmg.LocId.IndexOf("("));
                    //}
                    //parmDmgLoc.TotMt = txtHIDmgMT.Text;
                    //parmDmgLoc.TotM3 = txtHIDmgM3.Text;
                    //parmDmgLoc.TotQty = txtHIDmgQty.Text;
                    //parmDmgLoc.CgNo = txtHIGR.Text;
                    //parmDmgLoc.WhTpCd = "D";    // General: G, Shut-out: S, Damage: D
                    //parmDmgLoc.JobNo = m_itemHI.jobNo;

                    //HWC101001Result resultDmgLoc = (HWC101001Result)PopupManager.instance.ShowPopup(new HWC101001(), parmDmgLoc);
                    //if (resultDmgLoc != null)
                    //{
                    //    m_resHILocDmg = resultDmgLoc;
                    //    txtHIDmgLoc.Text = m_resHILocDmg.LocId;
                    //}
                    break;

                case "btnHOUnsetLoc":
                    HWC101002Parm parmUnsetActLoc = new HWC101002Parm();
                    if (m_blResultHO != null)
                    {
                        parmUnsetActLoc.VslCallId = m_blResultHO.VslCallId;
                    }
                    else if (m_grResultHO != null)
                    {
                        parmUnsetActLoc.VslCallId = m_grResultHO.VslCallId;
                    }
                    parmUnsetActLoc.CgNo = txtHOGRBL.Text;
                    parmUnsetActLoc.TotMt = txtHOLoadMT.Text;
                    parmUnsetActLoc.TotM3 = txtHOLoadM3.Text;
                    parmUnsetActLoc.TotQty = txtHOLoadQty.Text;
                    parmUnsetActLoc.IsGeneralCg = true;
                    HWC101002Result resultUnsetActLoc = (HWC101002Result)PopupManager.instance.ShowPopup(new HWC101002(), parmUnsetActLoc);
                    if (resultUnsetActLoc != null)
                    {
                        m_resHOLoc = resultUnsetActLoc;
                        txtHOLoadLoc.Text = m_resHOLoc.LocId;
                    }
                    break;

                case "btnHIExit":
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    break;

                case "btnHOExit":
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    break;

                case "btnF1":
                    PartnerCodeListParm lorryHIParm = new PartnerCodeListParm();
                    if (!string.IsNullOrEmpty(txtHITsptr.Text))
                    {
                        lorryHIParm.PtnrCd = txtHITsptr.Text;
                        lorryHIParm.GrNo = txtHIGR.Text;
                        lorryHIParm.VslCallId = m_itemHI.vslCallId;
                        lorryHIParm.ScreenId = HCM110.M_HWC101;
                    }
                    lorryHIParm.SearchItem = this.txtHILorry.Text;

                    PartnerCodeListResult lorryHIRes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_LORRY), lorryHIParm);
                    if (lorryHIRes != null)
                    {
                        txtHILorry.Text = lorryHIRes.LorryNo;
                    }
                    break;

                case "btnF3":
                    /*
                     * get lorry list : 
                     * if we do import with BBK cargo type : get lorry list from lorry assignment
                     * if we do import with DBK cargo type or do export : get lorry list from transporter assignment
                     */
                    if (m_grResultHO != null || m_itemHO != null)
                    {
                        PartnerCodeListParm lorryHOParm = new PartnerCodeListParm();
                        if (!string.IsNullOrEmpty(txtHOTsptr.Text))
                        {
                            lorryHOParm.PtnrCd = txtHOTsptr.Text;
                        }
                        //Added By Chris 2015-10-12
                        if (!string.IsNullOrEmpty(txtHOGRBL.Text))
                        {
                            lorryHOParm.GrNo = txtHOGRBL.Text;
                            lorryHOParm.BlNo = "";
                        }
                        if (m_grResultHO != null)
                        {
                            if (m_grResultHO.VslCallId != null)
                            {
                                lorryHOParm.VslCallId = m_grResultHO.VslCallId;
                            }
                        }
                        else if (m_itemHO != null)
                        {
                            if (m_itemHO.vslCallId != null)
                            {
                                lorryHOParm.VslCallId = m_itemHO.vslCallId;
                            }
                            if (m_itemHO.blNo != null)
                            {
                                lorryHOParm.BlNo = m_itemHO.blNo;
                                lorryHOParm.GrNo = "";
                            }

                        }

                        lorryHOParm.ScreenId = HCM110.M_HWC101;

                        lorryHOParm.SearchItem = this.txtHOLorry.Text;
                        PartnerCodeListResult lorryHORes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_LORRY), lorryHOParm);
                        if (lorryHORes != null)
                        {
                            txtHOLorry.Text = lorryHORes.LorryNo;
                        }
                    }
                    else if (m_blResultHO != null)
                    {
                        MOST.Common.CommonParm.LorryListParm lorryParm = new MOST.Common.CommonParm.LorryListParm();
                        lorryParm.Jpvc = m_blResultHO.VslCallId;
                        lorryParm.BlNo = m_blResultHO.Bl;
                        lorryParm.LorryNo = txtHOLorry.Text;
                        LorryListResult lorryResult = (LorryListResult)PopupManager.instance.ShowPopup(new HCM107(HCM107.TYPE_HAC_DISCHARGING), lorryParm);
                        if (lorryResult != null)
                        {
                            txtHOLorry.Text = lorryResult.LorryNo;
                        }
                    }
                    break;

                case "btnHIPkgTp":
                    PartnerCodeListParm pkgTpParmHI = new PartnerCodeListParm();
                    pkgTpParmHI.Option = "CD";
                    pkgTpParmHI.SearchItem = txtHIPkgTp.Text;
                    PartnerCodeListResult pkgTpResHI = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_PKGTYPE), pkgTpParmHI);
                    if (pkgTpResHI != null)
                    {
                        txtHIPkgTp.Text = pkgTpResHI.Code;
                    }
                    break;

                case "btnF4":
                    PartnerCodeListParm pkgTpParmHO = new PartnerCodeListParm();
                    pkgTpParmHO.Option = "CD";
                    pkgTpParmHO.SearchItem = txtHOPkgTp.Text;
                    PartnerCodeListResult pkgTpResHO = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_PKGTYPE), pkgTpParmHO);
                    if (pkgTpResHO != null)
                    {
                        txtHOPkgTp.Text = pkgTpResHO.Code;
                    }
                    break;
                case "btnHISOut":
                    if (!this.pnlHISOut.Visible)
                    {
                        this.pnlHISOut.BringToFront();
                        this.btnHISOut.Text = "OK";
                        this.btnHIConfirm.Enabled = this.btnHIExit.Enabled = this.btnHIDmg.Enabled = this.btnGR.Enabled = false;
                        this.pnlHISOut.m_parm = this.m_parm;
                        this.pnlHISOut.m_resHILocSOut = this.m_resHILocSOut;
                        this.pnlHISOut.Visible = true;
                    }
                    else
                    {
                        this.btnHISOut.Text = "Shut out";
                        this.m_resHILocSOut = this.pnlHISOut.m_resHILocSOut;
                        this.btnHIConfirm.Enabled = this.btnHIExit.Enabled = this.btnHIDmg.Enabled = this.btnGR.Enabled = true;
                        //if (!string.Empty.Equals(this.pnlHISOut.txtHIShutLoc.Text))
                        //{
                        //    this.pnlHISOut.txtHIShutLoc.Text = "DcDmg data exists!";
                        //}
                        //else
                        //{
                        //    this.pnlHISOut.txtHIShutLoc.Text = string.Empty;
                        //}
                        this.pnlHISOut.Visible = false;
                        ChkGP(this.pnlHISOut.chkHIGP);
                    }
                    break;
                case "btnHIDmg":
                    if (!this.pnlHIDmg.Visible)
                    {
                        this.pnlHIDmg.BringToFront();
                        this.btnHIDmg.Text = "OK";
                        this.btnHIConfirm.Enabled = this.btnHIExit.Enabled = this.btnHISOut.Enabled = this.btnGR.Enabled = false;
                        this.pnlHIDmg.m_parm = this.m_parm;
                        this.pnlHIDmg.m_resHILocDmg = this.m_resHILocDmg;
                        this.pnlHIDmg.Visible = true;
                    }
                    else
                    {
                        this.btnHIDmg.Text = "Damged";
                        this.m_resHILocDmg = this.pnlHIDmg.m_resHILocDmg;
                        this.btnHIConfirm.Enabled = this.btnHIExit.Enabled = this.btnHISOut.Enabled = this.btnGR.Enabled = true;
                        //if (!string.Empty.Equals(this.pnlHISOut.txtHIShutLoc.Text))
                        //{
                        //    this.pnlHISOut.txtHIShutLoc.Text = "DcDmg data exists!";
                        //}
                        //else
                        //{
                        //    this.pnlHISOut.txtHIShutLoc.Text = string.Empty;
                        //}
                        this.pnlHIDmg.Visible = false;
                        ChkGP(this.pnlHIDmg.chkHIGP);
                    }
                    break;
                case "btnGR":
                    selectGRPanel.Visible = !selectGRPanel.Visible;

                    if (selectGRPanel.Visible)
                    {
                        this.selectGRPanel.BringToFront();
                        this.btnGR.Text = "OK";
                        this.btnHIConfirm.Enabled = this.btnHIExit.Enabled = this.btnHIDmg.Enabled = this.btnHISOut.Enabled = false;
                    }
                    else
                    {
                        if (m_parm.GrInfo != null)
                        {
                            if (m_parm.GrInfo.GrNo != selectGRPanel.txtGR.Text)
                            {
                                if (!ValidateGr(selectGRPanel.txtGR.Text))
                                {
                                    selectGRPanel.Visible = !selectGRPanel.Visible;
                                    return;
                                }
                            }
                        }

                        this.btnGR.Text = "G/R �";
                        this.btnHIConfirm.Enabled = this.btnHIExit.Enabled = this.btnHIDmg.Enabled = this.btnHISOut.Enabled = true;

                        if (m_parm.GrInfo != null)
                        {
                            if (m_parm.GrInfo.GrNo == this.selectGRPanel.txtGR.Text)
                                return;  // This GR is currently selected; no need to reload. 
                            else  // If form is dirty, ask user to save changes before switch to other GRs. 
                            {
                                DialogResult dr = MessageBox.Show(String.Format("Do you want to save changes you made to {0}? ", m_parm.GrInfo.GrNo),
                                                                  "Warehouse Checker",
                                                                  MessageBoxButtons.YesNoCancel,
                                                                  MessageBoxIcon.Question,
                                                                  MessageBoxDefaultButton.Button1);
                                switch (dr)
                                {
                                    case DialogResult.Cancel:
                                        return;  // User clicked Cancel. Return to the current GR, don't load the new GR info. 
                                    case DialogResult.Yes:
                                        //if (!Confirm())  // If cannot save, then process as if user clicked Cancel button. 
                                            //return;
                                        ConfirmHI();
                                        break;
                                }
                            }
                        }

                        m_parm.GrInfo.GrNo = this.selectGRPanel.txtGR.Text;
                        InitializeData();
                    }
                    break;
                case "btnHOGP":
                    MOST.Common.CommonParm.CargoGatePassParm gpParm = new MOST.Common.CommonParm.CargoGatePassParm();
                    if (m_parm != null)
                    {
                        if (m_parm.BlInfo != null)
                        {
                            gpParm.VslCallId = m_parm.BlInfo.VslCallId;
                        }
                        else if (m_parm.GrInfo != null)
                        {
                            gpParm.VslCallId = m_parm.GrInfo.VslCallId;
                        }
                    }
                    gpParm.CgNo = txtHOGRBL.Text;
                    if (m_responseInfo != null && m_responseInfo.list != null)
                    {
                        List<string> listGP = new List<string>();
                        for (int i = 0; i < m_responseInfo.list.Length; i++)
                        {
                            if (m_responseInfo.list[i] is CargoArrvDelvItem)
                            {
                                CargoArrvDelvItem item = (CargoArrvDelvItem)m_responseInfo.list[i];
                                listGP.Add(item.gatePassNo);
                            }
                        }
                        gpParm.ArrInitGPNos = listGP;
                    }
                    PopupManager.instance.ShowPopup(new MOST.Common.HCM116(), gpParm);
                    break;
                case "btnBL":
                    if (this.m_parm.GrInfo != null)
                    {
                        selectGRPanel.Visible = !selectGRPanel.Visible;

                        if (selectGRPanel.Visible)
                        {
                            this.selectGRPanel.BringToFront();
                            this.btnBL.Text = "OK";
                            this.btnHOConfirm.Enabled = this.btnHOExit.Enabled = this.btnHOGP.Enabled = false;
                        }
                        else
                        {
                            if (m_parm.GrInfo != null)
                            {
                                if (m_parm.GrInfo.GrNo != selectGRPanel.txtGR.Text)
                                {
                                    if (!ValidateGr(selectGRPanel.txtGR.Text))
                                    {
                                        selectGRPanel.Visible = !selectGRPanel.Visible;
                                        return;
                                    }
                                }
                            }

                            this.btnBL.Text = "G/R �";
                            this.btnHOConfirm.Enabled = this.btnHOExit.Enabled = this.btnHOGP.Enabled = true;

                            if (m_parm.GrInfo != null)
                            {
                                if (m_parm.GrInfo.GrNo == this.selectGRPanel.txtGR.Text)
                                    return;  // This GR is currently selected; no need to reload. 
                                else  // If form is dirty, ask user to save changes before switch to other GRs. 
                                {
                                    DialogResult dr = MessageBox.Show(String.Format("Do you want to save changes you made to {0}? ", m_parm.GrInfo.GrNo),
                                                                      "Warehouse Checker",
                                                                      MessageBoxButtons.YesNoCancel,
                                                                      MessageBoxIcon.Question,
                                                                      MessageBoxDefaultButton.Button1);
                                    switch (dr)
                                    {
                                        case DialogResult.Cancel:
                                            return;  // User clicked Cancel. Return to the current GR, don't load the new GR info. 
                                        case DialogResult.Yes:
                                            //if (!Confirm())  // If cannot save, then process as if user clicked Cancel button. 
                                            //return;
                                            ConfirmHO();
                                            break;
                                    }
                                }
                            }

                            m_parm.GrInfo.GrNo = this.selectGRPanel.txtGR.Text;
                            InitializeData();
                        }
                    }
                    else if (this.m_parm.BlInfo != null)
                    {
                        selectBLPanel.Visible = !selectBLPanel.Visible;

                        if (selectBLPanel.Visible)
                        {
                            selectBLPanel.BringToFront();
                            btnBL.Text = "OK";
                            btnHOConfirm.Enabled = btnHOExit.Enabled = btnHOGP.Enabled = false;
                        }
                        else
                        {
                            if (m_parm.BlInfo != null)
                            {
                                if (m_parm.BlInfo.Bl != selectBLPanel.txtBL.Text)
                                {
                                    if (!ValidateBL())
                                    {
                                        selectBLPanel.Visible = !selectBLPanel.Visible;
                                        return;
                                    }
                                }
                            }

                            btnBL.Text = "B/L �";
                            btnHOConfirm.Enabled = btnHOExit.Enabled = btnHOGP.Enabled = true;

                            if (m_parm.BlInfo != null)
                            {
                                if (m_parm.BlInfo.Bl == selectBLPanel.txtBL.Text)
                                    return;  // This BL is currently selected; no need to reload. 
                                else  // If form is dirty, ask user to save changes before switch to other GRs. 
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
                                            //if (!Confirm())  // If cannot save, then process as if user clicked Cancel button. 
                                            //return;
                                            ConfirmHO();
                                            break;
                                    }
                                }
                            }

                            m_parm.BlInfo.Bl = selectBLPanel.txtBL.Text;
                            InitializeData();
                        }
                    }
                    break;
            }
        }

        private void ConfirmHI()
        {
            if (this.validations(this.tabHI.Controls) && this.Validate())
            {
                if (ProcessCargoHandlingInItem())
                {
                    if (m_itemHI.gatePassYn)
                    {
                        // Save successfully. Do you want to open the GatePass screen?
                        DialogResult gpHIDr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0072") + " : " + ((m_responseInfo != null && m_responseInfo.list != null) ? ((CargoArrvDelvItem)m_responseInfo.list[0]).gatePassNo : ""));
                        if (gpHIDr == DialogResult.Yes)
                        {
                            MOST.Common.CommonParm.CargoGatePassParm gpParm = new MOST.Common.CommonParm.CargoGatePassParm();
                            if (m_parm != null && m_parm.GrInfo != null)
                            {
                                gpParm.VslCallId = m_parm.GrInfo.VslCallId;
                            }
                            gpParm.CgNo = txtHIGR.Text;
                            if (m_responseInfo != null && m_responseInfo.list != null)
                            {
                                List<string> listGP = new List<string>();
                                for (int i = 0; i < m_responseInfo.list.Length; i++)
                                {
                                    if (m_responseInfo.list[i] is CargoArrvDelvItem)
                                    {
                                        CargoArrvDelvItem item = (CargoArrvDelvItem)m_responseInfo.list[i];
                                        listGP.Add(item.gatePassNo);
                                    }
                                }
                                gpParm.ArrInitGPNos = listGP;
                            }
                            PopupManager.instance.ShowPopup(new MOST.Common.HCM116(), gpParm);
                        }
                        this.Close();
                    }
                    else
                    {
                        CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HCM0048"));
                        this.Close();
                    }
                }
            }
        }

        private void ConfirmHO()
        {
            if (this.validations(this.tabHO.Controls) && this.Validate())
            {
                if (ProcessCargoHandlingOutItem())
                {
                    // Save successfully. Do you want to open the GatePass screen?
                    DialogResult gpHODr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0072")
                        + ((CargoArrvDelvItem)this.m_responseInfo.list[0]).gatePassNo + "?");
                    if (gpHODr == DialogResult.Yes)
                    {
                        MOST.Common.CommonParm.CargoGatePassParm gpParm = new MOST.Common.CommonParm.CargoGatePassParm();
                        if (m_parm != null)
                        {
                            if (m_parm.BlInfo != null)
                            {
                                gpParm.VslCallId = m_parm.BlInfo.VslCallId;
                            }
                            else if (m_parm.GrInfo != null)
                            {
                                gpParm.VslCallId = m_parm.GrInfo.VslCallId;
                            }
                        }
                        gpParm.CgNo = txtHOGRBL.Text;
                        if (m_responseInfo != null && m_responseInfo.list != null)
                        {
                            List<string> listGP = new List<string>();
                            for (int i = 0; i < m_responseInfo.list.Length; i++)
                            {
                                if (m_responseInfo.list[i] is CargoArrvDelvItem)
                                {
                                    CargoArrvDelvItem item = (CargoArrvDelvItem)m_responseInfo.list[i];
                                    listGP.Add(item.gatePassNo);
                                }
                            }
                            gpParm.ArrInitGPNos = listGP;
                        }
                        PopupManager.instance.ShowPopup(new MOST.Common.HCM116(), gpParm);
                    }
                    this.Close();
                }
            }
        }

        private void ChkGP(bool bChkHIGP)
        {
            if (!this.chkHIGP.Checked && bChkHIGP)
                this.chkHIGP.Checked = bChkHIGP;
        }

        private bool ValidateGr(string gr)
        {
            ICommonProxy proxy = new CommonProxy();
            Framework.Service.Provider.WebService.Provider.CargoExportParm cgExpParm
                = new Framework.Service.Provider.WebService.Provider.CargoExportParm();

            if (String.IsNullOrEmpty(gr))
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager
                    .ResourceManager.getInstance().getString("HCM0050"));
                return false;
            }

            if (!string.IsNullOrEmpty(m_itemHI.vslCallId))
            {
                cgExpParm.vslCallId = m_itemHI.vslCallId;
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

            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0029"));
            return false;
        }

        private bool ValidateBL()
        {
            if (string.IsNullOrEmpty(selectBLPanel.txtBL.Text.Trim()))
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0051"));
                return false;
            }

            BLListResult result = new BLListResult();
            if (CommonUtility.IsValidBL(m_itemHO.vslCallId, selectBLPanel.txtBL.Text.Trim(), ref result) == false)
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0030"));
                return false;
            }

            return true;
        }

        /*
         * private void ChkGPListener(object sender, EventArgs e)
        {
            ComboBox mycombobox = (ComboBox)sender;
            String cboName = mycombobox.Name;
            switch (cboName)
            {
                case "cboHIHndlSOut":
                    // Shut-out
                    if ("R".Equals(CommonUtility.GetComboboxSelectedValue(cboHIHndlSOut)))
                    {
                        //txtHIShutLoc.Enabled = false;
                        btnHISetLocSOut.Enabled = false;
                    }
                    else
                    {
                        //txtHIShutLoc.Enabled = true;
                        btnHISetLocSOut.Enabled = true;
                    }
                    break;

                case "cboHIHndlDmg":
                    // Damage
                    if ("R".Equals(CommonUtility.GetComboboxSelectedValue(cboHIHndlDmg)))
                    {
                        txtHIDmgLoc.Enabled = false;
                        //btnHISetLocDmg.Enabled = false;
                    }
                    else
                    {
                        txtHIDmgLoc.Enabled = true;
                        //btnHISetLocDmg.Enabled = true;
                    }
                    break;
            }

            // GatePass
            if ((cboHIHndlSOut.SelectedIndex > 0 && "R".Equals(CommonUtility.GetComboboxSelectedValue(cboHIHndlSOut))) ||
                (cboHIHndlDmg.SelectedIndex > 0 && "R".Equals(CommonUtility.GetComboboxSelectedValue(cboHIHndlDmg))))
            {
                chkHIGP.Checked = true;
            }
            else
            {
                chkHIGP.Checked = false;
            }
        }
         */
        #endregion

        private void cboHICargoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            String cgTpCd = CommonUtility.GetComboboxSelectedValue(cboHICargoType);
            if ("CTR".Equals(cgTpCd))   //Container Type is invalid
            {
                CommonUtility.AlertMessage("This is Container cargo. Only Bulk cargo can handle in this system.");
                CommonUtility.SetComboboxSelectedItem(cboHICargoType, m_itemHI.cgTpCd);
            }
            if (m_itemHI != null)
            {
                m_itemHI.cgTpCd = CommonUtility.GetComboboxSelectedValue(cboHICargoType);
            }
            SetPrimaryPkgType();
            SetPrimary();
        }

        private void cboHOCargoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            String cgTpCd = CommonUtility.GetComboboxSelectedValue(cboHOCargoType);
            if ("CTR".Equals(cgTpCd))   //Container Type is invalid
            {
                CommonUtility.AlertMessage("This is Container cargo. Only Bulk cargo can handle in this system.");
                CommonUtility.SetComboboxSelectedItem(cboHOCargoType, m_itemHO.cgTpCd);
            }
            if (m_itemHO != null)
            {
                m_itemHO.cgTpCd = CommonUtility.GetComboboxSelectedValue(cboHOCargoType);
            }
            SetPrimaryPkgType();
            SetPrimary();
        }

        private void ClearHIActLoc(object sender, EventArgs e)
        {
            m_resHILocAct = null;
            txtHIActLoc.Text = "";
        }

        private void ClearHIDmgLoc(object sender, EventArgs e)
        {
            m_resHILocDmg = null;
            //txtHIDmgLoc.Text = "";
        }

        private void ClearHIShutLoc(object sender, EventArgs e)
        {
            m_resHILocSOut = null;
            //txtHIShutLoc.Text = "";
        }

        private void ClearHOActLoc(object sender, EventArgs e)
        {
            m_resHOLoc = null;
            txtHOLoadLoc.Text = "";
        }

        private void SetPrimary()
        {
            #region H/I
            if (!Constants.STRING_NULL.Equals(CommonUtility.GetComboboxSelectedValue(cboHICargoType)))
            {
                //BBK HI
                if (Constants.STRING_BBK.Equals(CommonUtility.GetComboboxSelectedValue(cboHICargoType)))
                {
                    txtHIActQty.isMandatory = true;
                    txtHIActM3.isMandatory = false;
                    txtHIActQty.isMandatory = true;
                }

                //DBK HI
                if (
                    Constants.STRING_DBE.Equals(CommonUtility.GetComboboxSelectedValue(cboHICargoType))
                    || Constants.STRING_DBN.Equals(CommonUtility.GetComboboxSelectedValue(cboHICargoType)))
                {
                    txtHIActMT.isMandatory = true;
                    txtHIActM3.isMandatory = false;
                    txtHIActQty.isMandatory = false;
                }
            }
            #endregion
            #region H/O
            else if (!Constants.STRING_NULL.Equals(CommonUtility.GetComboboxSelectedValue(cboHOCargoType)))
            {
                //BBK HO
                if (Constants.STRING_BBK.Equals(CommonUtility.GetComboboxSelectedValue(cboHOCargoType)))
                {
                    txtHOLoadMT.isMandatory = true;
                    txtHOLoadM3.isMandatory = false;
                    txtHOLoadQty.isMandatory = true;
                }

                //DBK HO
                if (
                    Constants.STRING_DBE.Equals(CommonUtility.GetComboboxSelectedValue(cboHOCargoType))
                    || Constants.STRING_DBN.Equals(CommonUtility.GetComboboxSelectedValue(cboHOCargoType)))
                {
                    txtHOLoadMT.isMandatory = true;
                    txtHOLoadM3.isMandatory = false;
                    txtHOLoadQty.isMandatory = false;
                }
            }
            
            #endregion
        }

        private void tabHIHOSelectedIndexChange(object sender, EventArgs e)
        {
            TabControl senderTabControl = (TabControl)sender;
            System.Windows.Forms.TabControl.TabPageCollection pages = senderTabControl.TabPages;
            int index = 0;
            String tabName = String.Empty;

            foreach (TabPage page in pages)
            {
                if (index == senderTabControl.SelectedIndex)
                {
                    tabName = page.Name;
                    break;
                }
                index++;
            }

            initializeTabs(tabName);
        }

        private void initializeTabs(String tabName)
        {
            if ("tabHI".Equals(tabName))
            {
                if (!isLoadedHI)
                {
                    #region Initilize HI tab

                    // 
                    // tLabel10
                    // 
                    this.tLabel10.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular);
                    this.tLabel10.Location = new System.Drawing.Point(1, 128);
                    this.tLabel10.Name = "tLabel10";
                    this.tLabel10.Size = new System.Drawing.Size(20, 15);
                    this.tLabel10.Text = "Loc";
                    // 
                    // cboHICargoType
                    // 
                    this.cboHICargoType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.cboHICargoType.isBusinessItemName = "";
                    this.cboHICargoType.isMandatory = false;
                    this.cboHICargoType.Location = new System.Drawing.Point(177, 57);
                    this.cboHICargoType.Name = "cboHICargoType";
                    this.cboHICargoType.Size = new System.Drawing.Size(55, 19);
                    this.cboHICargoType.TabIndex = 238;
                    this.cboHICargoType.SelectedIndexChanged += new System.EventHandler(this.cboHICargoType_SelectedIndexChanged);
                    // 
                    // tLabel19
                    // 
                    this.tLabel19.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.tLabel19.Location = new System.Drawing.Point(143, 58);
                    this.tLabel19.Name = "tLabel19";
                    this.tLabel19.Size = new System.Drawing.Size(30, 15);
                    this.tLabel19.Text = "CgTp";
                    // 
                    // tLabel15
                    // 
                    this.tLabel15.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.tLabel15.Location = new System.Drawing.Point(170, 114);
                    this.tLabel15.Name = "tLabel15";
                    this.tLabel15.Size = new System.Drawing.Size(44, 13);
                    this.tLabel15.Text = "Remark";
                    // 
                    // txtHIRemark
                    // 
                    this.txtHIRemark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                    this.txtHIRemark.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHIRemark.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                    this.txtHIRemark.Location = new System.Drawing.Point(158, 127);
                    this.txtHIRemark.Multiline = true;
                    this.txtHIRemark.Name = "txtHIRemark";
                    this.txtHIRemark.ReadOnly = true;
                    this.txtHIRemark.Size = new System.Drawing.Size(70, 17);
                    this.txtHIRemark.TabIndex = 223;
                    // 
                    // txtHIAccQty
                    // 
                    this.txtHIAccQty.BackColor = System.Drawing.Color.White;
                    this.txtHIAccQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHIAccQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
                    this.txtHIAccQty.Location = new System.Drawing.Point(148, 74);
                    this.txtHIAccQty.Multiline = true;
                    this.txtHIAccQty.Name = "txtHIAccQty";
                    this.txtHIAccQty.ReadOnly = true;
                    this.txtHIAccQty.Size = new System.Drawing.Size(38, 17);
                    this.txtHIAccQty.TabIndex = 209;
                    this.txtHIAccQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    // 
                    // txtHIAccM3
                    // 
                    this.txtHIAccM3.BackColor = System.Drawing.Color.White;
                    this.txtHIAccM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHIAccM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHIAccM3.Location = new System.Drawing.Point(109, 74);
                    this.txtHIAccM3.Multiline = true;
                    this.txtHIAccM3.Name = "txtHIAccM3";
                    this.txtHIAccM3.ReadOnly = true;
                    this.txtHIAccM3.Size = new System.Drawing.Size(38, 17);
                    this.txtHIAccM3.TabIndex = 208;
                    this.txtHIAccM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    // 
                    // txtHIAccMT
                    // 
                    this.txtHIAccMT.BackColor = System.Drawing.Color.White;
                    this.txtHIAccMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHIAccMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHIAccMT.Location = new System.Drawing.Point(70, 74);
                    this.txtHIAccMT.Multiline = true;
                    this.txtHIAccMT.Name = "txtHIAccMT";
                    this.txtHIAccMT.ReadOnly = true;
                    this.txtHIAccMT.Size = new System.Drawing.Size(38, 17);
                    this.txtHIAccMT.TabIndex = 207;
                    this.txtHIAccMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    // 
                    // txtHILorry
                    // 
                    this.txtHILorry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                    this.txtHILorry.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHILorry.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                    this.txtHILorry.Location = new System.Drawing.Point(110, 0);
                    this.txtHILorry.Multiline = true;
                    this.txtHILorry.Name = "txtHILorry";
                    this.txtHILorry.Size = new System.Drawing.Size(46, 17);
                    this.txtHILorry.TabIndex = 193;
                    // 
                    // chkHIFinal
                    // 
                    this.chkHIFinal.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
                    this.chkHIFinal.Location = new System.Drawing.Point(47, 183);
                    this.chkHIFinal.Name = "chkHIFinal";
                    this.chkHIFinal.Size = new System.Drawing.Size(46, 17);
                    this.chkHIFinal.TabIndex = 192;
                    this.chkHIFinal.Text = "Final";
                    // 
                    // chkHISpare
                    // 
                    this.chkHISpare.Enabled = false;
                    this.chkHISpare.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
                    this.chkHISpare.Location = new System.Drawing.Point(159, 94);
                    this.chkHISpare.Name = "chkHISpare";
                    this.chkHISpare.Size = new System.Drawing.Size(51, 17);
                    this.chkHISpare.TabIndex = 191;
                    this.chkHISpare.Text = "Spare";
                    // 
                    // txtHIPkgTp
                    // 
                    this.txtHIPkgTp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                    this.txtHIPkgTp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHIPkgTp.isBusinessItemName = "Package Type";
                    this.txtHIPkgTp.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                    this.txtHIPkgTp.Location = new System.Drawing.Point(141, 19);
                    this.txtHIPkgTp.Multiline = true;
                    this.txtHIPkgTp.Name = "txtHIPkgTp";
                    this.txtHIPkgTp.Size = new System.Drawing.Size(48, 17);
                    this.txtHIPkgTp.TabIndex = 189;
                    // 
                    // btnHIPkgTp
                    // 
                    this.btnHIPkgTp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.btnHIPkgTp.Location = new System.Drawing.Point(190, 19);
                    this.btnHIPkgTp.Name = "btnHIPkgTp";
                    this.btnHIPkgTp.Size = new System.Drawing.Size(39, 17);
                    this.btnHIPkgTp.TabIndex = 190;
                    this.btnHIPkgTp.Text = "PkgTp";
                    this.btnHIPkgTp.Click += new System.EventHandler(this.ActionListener);
                    // 
                    // txtHITsptr
                    // 
                    this.txtHITsptr.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHITsptr.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                    this.txtHITsptr.Location = new System.Drawing.Point(189, 0);
                    this.txtHITsptr.Multiline = true;
                    this.txtHITsptr.Name = "txtHITsptr";
                    this.txtHITsptr.ReadOnly = true;
                    this.txtHITsptr.Size = new System.Drawing.Size(40, 17);
                    this.txtHITsptr.TabIndex = 156;
                    // 
                    // txtHIGR
                    // 
                    this.txtHIGR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHIGR.isBusinessItemName = "G/R";
                    this.txtHIGR.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                    this.txtHIGR.isMandatory = true;
                    this.txtHIGR.Location = new System.Drawing.Point(17, 1);
                    this.txtHIGR.Multiline = true;
                    this.txtHIGR.Name = "txtHIGR";
                    this.txtHIGR.ReadOnly = true;
                    this.txtHIGR.Size = new System.Drawing.Size(68, 17);
                    this.txtHIGR.TabIndex = 127;                    
                    // 
                    // txtHIActLoc
                    // 
                    this.txtHIActLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                    this.txtHIActLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHIActLoc.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                    this.txtHIActLoc.Location = new System.Drawing.Point(22, 128);
                    this.txtHIActLoc.Multiline = true;
                    this.txtHIActLoc.Name = "txtHIActLoc";
                    this.txtHIActLoc.ReadOnly = true;
                    this.txtHIActLoc.Size = new System.Drawing.Size(62, 17);
                    this.txtHIActLoc.TabIndex = 122;                 
                    // 
                    // txtHIDtEnd
                    // 
                    this.txtHIDtEnd.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                    this.txtHIDtEnd.CustomFormat = "dd/MM/yyyy HH:mm";
                    this.txtHIDtEnd.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHIDtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                    this.txtHIDtEnd.isBusinessItemName = "";
                    this.txtHIDtEnd.isMandatory = false;
                    this.txtHIDtEnd.Location = new System.Drawing.Point(116, 37);
                    this.txtHIDtEnd.Name = "txtHIDtEnd";
                    this.txtHIDtEnd.Size = new System.Drawing.Size(114, 20);
                    this.txtHIDtEnd.TabIndex = 108;
                    //this.txtHIDtEnd.Value = new System.DateTime(2008, 10, 24, 16, 17, 0, 828);
                    // 
                    // txtHIPkgNo
                    // 
                    this.txtHIPkgNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                    this.txtHIPkgNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHIPkgNo.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                    this.txtHIPkgNo.Location = new System.Drawing.Point(38, 19);
                    this.txtHIPkgNo.Multiline = true;
                    this.txtHIPkgNo.Name = "txtHIPkgNo";
                    this.txtHIPkgNo.Size = new System.Drawing.Size(102, 17);
                    this.txtHIPkgNo.TabIndex = 120;
                    // 
                    // txtHIDtStart
                    // 
                    this.txtHIDtStart.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                    this.txtHIDtStart.CustomFormat = "dd/MM/yyyy HH:mm";
                    this.txtHIDtStart.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHIDtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                    this.txtHIDtStart.isBusinessItemName = "Start Date";
                    this.txtHIDtStart.isMandatory = true;
                    this.txtHIDtStart.Location = new System.Drawing.Point(0, 37);
                    this.txtHIDtStart.Name = "txtHIDtStart";
                    this.txtHIDtStart.Size = new System.Drawing.Size(114, 20);
                    this.txtHIDtStart.TabIndex = 107;
                    //this.txtHIDtStart.Value = new System.DateTime(2008, 10, 24, 16, 17, 0, 437);
                    // 
                    // chkHIGP
                    // 
                    this.chkHIGP.Enabled = false;
                    this.chkHIGP.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
                    this.chkHIGP.Location = new System.Drawing.Point(3, 183);
                    this.chkHIGP.Name = "chkHIGP";
                    this.chkHIGP.Size = new System.Drawing.Size(41, 17);
                    this.chkHIGP.TabIndex = 76;
                    this.chkHIGP.Text = "GP";
                    // 
                    // btnHIConfirm
                    // 
                    this.btnHIConfirm.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.btnHIConfirm.Location = new System.Drawing.Point(97, 178);
                    this.btnHIConfirm.Name = "btnHIConfirm";
                    this.btnHIConfirm.Size = new System.Drawing.Size(65, 24);
                    this.btnHIConfirm.TabIndex = 75;
                    this.btnHIConfirm.Text = "Confirm";
                    this.btnHIConfirm.Click += new System.EventHandler(this.ActionListener);                    
                    // 
                    // txtHIActQty
                    // 
                    this.txtHIActQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                    this.txtHIActQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHIActQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
                    this.txtHIActQty.Location = new System.Drawing.Point(112, 110);
                    this.txtHIActQty.Multiline = true;
                    this.txtHIActQty.Name = "txtHIActQty";
                    this.txtHIActQty.Size = new System.Drawing.Size(44, 17);
                    this.txtHIActQty.TabIndex = 14;
                    this.txtHIActQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    this.txtHIActQty.TextChanged += new System.EventHandler(this.ClearHIActLoc);
                    // 
                    // txtHIActM3
                    // 
                    this.txtHIActM3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                    this.txtHIActM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHIActM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHIActM3.Location = new System.Drawing.Point(67, 110);
                    this.txtHIActM3.Multiline = true;
                    this.txtHIActM3.Name = "txtHIActM3";
                    this.txtHIActM3.Size = new System.Drawing.Size(44, 17);
                    this.txtHIActM3.TabIndex = 13;
                    this.txtHIActM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    this.txtHIActM3.TextChanged += new System.EventHandler(this.ClearHIActLoc);
                    // 
                    // txtHIActMT
                    // 
                    this.txtHIActMT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                    this.txtHIActMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHIActMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHIActMT.Location = new System.Drawing.Point(22, 110);
                    this.txtHIActMT.Multiline = true;
                    this.txtHIActMT.Name = "txtHIActMT";
                    this.txtHIActMT.Size = new System.Drawing.Size(44, 17);
                    this.txtHIActMT.TabIndex = 12;
                    this.txtHIActMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    this.txtHIActMT.TextChanged += new System.EventHandler(this.ClearHIActLoc);
                    // 
                    // txtHIGRQty
                    // 
                    this.txtHIGRQty.BackColor = System.Drawing.Color.White;
                    this.txtHIGRQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHIGRQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
                    this.txtHIGRQty.Location = new System.Drawing.Point(112, 92);
                    this.txtHIGRQty.Multiline = true;
                    this.txtHIGRQty.Name = "txtHIGRQty";
                    this.txtHIGRQty.ReadOnly = true;
                    this.txtHIGRQty.Size = new System.Drawing.Size(44, 17);
                    this.txtHIGRQty.TabIndex = 23;
                    this.txtHIGRQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    // 
                    // txtHIGRM3
                    // 
                    this.txtHIGRM3.BackColor = System.Drawing.Color.White;
                    this.txtHIGRM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHIGRM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHIGRM3.Location = new System.Drawing.Point(67, 92);
                    this.txtHIGRM3.Multiline = true;
                    this.txtHIGRM3.Name = "txtHIGRM3";
                    this.txtHIGRM3.ReadOnly = true;
                    this.txtHIGRM3.Size = new System.Drawing.Size(44, 17);
                    this.txtHIGRM3.TabIndex = 22;
                    this.txtHIGRM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    // 
                    // txtHIGRMT
                    // 
                    this.txtHIGRMT.BackColor = System.Drawing.Color.White;
                    this.txtHIGRMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHIGRMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHIGRMT.Location = new System.Drawing.Point(22, 92);
                    this.txtHIGRMT.Multiline = true;
                    this.txtHIGRMT.Name = "txtHIGRMT";
                    this.txtHIGRMT.ReadOnly = true;
                    this.txtHIGRMT.Size = new System.Drawing.Size(44, 17);
                    this.txtHIGRMT.TabIndex = 21;
                    this.txtHIGRMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    // 
                    // txtHISNQty
                    // 
                    this.txtHISNQty.BackColor = System.Drawing.Color.White;
                    this.txtHISNQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHISNQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
                    this.txtHISNQty.Location = new System.Drawing.Point(96, 57);
                    this.txtHISNQty.Multiline = true;
                    this.txtHISNQty.Name = "txtHISNQty";
                    this.txtHISNQty.ReadOnly = true;
                    this.txtHISNQty.Size = new System.Drawing.Size(38, 17);
                    this.txtHISNQty.TabIndex = 20;
                    this.txtHISNQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    // 
                    // txtHISNM3
                    // 
                    this.txtHISNM3.BackColor = System.Drawing.Color.White;
                    this.txtHISNM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHISNM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHISNM3.Location = new System.Drawing.Point(57, 57);
                    this.txtHISNM3.Multiline = true;
                    this.txtHISNM3.Name = "txtHISNM3";
                    this.txtHISNM3.ReadOnly = true;
                    this.txtHISNM3.Size = new System.Drawing.Size(38, 17);
                    this.txtHISNM3.TabIndex = 19;
                    this.txtHISNM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    // 
                    // txtHISNMT
                    // 
                    this.txtHISNMT.BackColor = System.Drawing.Color.White;
                    this.txtHISNMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHISNMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHISNMT.Location = new System.Drawing.Point(18, 57);
                    this.txtHISNMT.Multiline = true;
                    this.txtHISNMT.Name = "txtHISNMT";
                    this.txtHISNMT.ReadOnly = true;
                    this.txtHISNMT.Size = new System.Drawing.Size(38, 17);
                    this.txtHISNMT.TabIndex = 18;
                    this.txtHISNMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    // 
                    // btnHIExit
                    // 
                    this.btnHIExit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.btnHIExit.Location = new System.Drawing.Point(164, 178);
                    this.btnHIExit.Name = "btnHIExit";
                    this.btnHIExit.Size = new System.Drawing.Size(65, 24);
                    this.btnHIExit.TabIndex = 26;
                    this.btnHIExit.Text = "Exit";
                    this.btnHIExit.Click += new System.EventHandler(this.ActionListener);
                    // 
                    // btnHISetLocAct
                    // 
                    this.btnHISetLocAct.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.btnHISetLocAct.Location = new System.Drawing.Point(88, 128);
                    this.btnHISetLocAct.Name = "btnHISetLocAct";
                    this.btnHISetLocAct.Size = new System.Drawing.Size(54, 17);
                    this.btnHISetLocAct.TabIndex = 15;
                    this.btnHISetLocAct.Text = "SetLoc";
                    this.btnHISetLocAct.Click += new System.EventHandler(this.ActionListener);
                    // 
                    // Label8
                    // 
                    this.Label8.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular);
                    this.Label8.Location = new System.Drawing.Point(0, 112);
                    this.Label8.Name = "Label8";
                    this.Label8.Size = new System.Drawing.Size(19, 15);
                    this.Label8.Text = "Act";
                    // 
                    // Label7
                    // 
                    this.Label7.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular);
                    this.Label7.Location = new System.Drawing.Point(-1, 94);
                    this.Label7.Name = "Label7";
                    this.Label7.Size = new System.Drawing.Size(23, 15);
                    this.Label7.Text = "GR";
                    // 
                    // btnF1
                    // 
                    this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.btnF1.Location = new System.Drawing.Point(157, 0);
                    this.btnF1.Name = "btnF1";
                    this.btnF1.Size = new System.Drawing.Size(28, 17);
                    this.btnF1.TabIndex = 8;
                    this.btnF1.Text = "F1";
                    this.btnF1.Click += new System.EventHandler(this.ActionListener);
                    // 
                    // Label4
                    // 
                    this.Label4.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular);
                    this.Label4.Location = new System.Drawing.Point(85, 1);
                    this.Label4.Name = "Label4";
                    this.Label4.Size = new System.Drawing.Size(27, 16);
                    this.Label4.Text = "Lorry";
                    // 
                    // Label2
                    // 
                    this.Label2.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular);
                    this.Label2.Location = new System.Drawing.Point(0, 2);
                    this.Label2.Name = "Label2";
                    this.Label2.Size = new System.Drawing.Size(20, 15);
                    this.Label2.Text = "GR";
                    // 
                    // tLabel1
                    // 
                    this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.tLabel1.Location = new System.Drawing.Point(0, 21);
                    this.tLabel1.Name = "tLabel1";
                    this.tLabel1.Size = new System.Drawing.Size(38, 17);
                    this.tLabel1.Text = "Pkg No";
                    // 
                    // tLabel7
                    // 
                    this.tLabel7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.tLabel7.Location = new System.Drawing.Point(0, 60);
                    this.tLabel7.Name = "tLabel7";
                    this.tLabel7.Size = new System.Drawing.Size(29, 11);
                    this.tLabel7.Text = "SN";
                    // 
                    // tLabel18
                    // 
                    this.tLabel18.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.tLabel18.Location = new System.Drawing.Point(0, 76);
                    this.tLabel18.Name = "tLabel18";
                    this.tLabel18.Size = new System.Drawing.Size(67, 12);
                    this.tLabel18.Text = "Handled Amt";
                    // 
                    // btnHISOut
                    // 
                    this.btnHISOut.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.btnHISOut.Location = new System.Drawing.Point(5, 150);
                    this.btnHISOut.Name = "btnHISOut";
                    this.btnHISOut.Size = new System.Drawing.Size(65, 21);
                    this.btnHISOut.TabIndex = 26;
                    this.btnHISOut.Text = "Shut out";
                    this.btnHISOut.Click += new System.EventHandler(this.ActionListener);
                    // 
                    // btnHIDmg
                    // 
                    this.btnHIDmg.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.btnHIDmg.Location = new System.Drawing.Point(75, 150);
                    this.btnHIDmg.Name = "btnHIDmg";
                    this.btnHIDmg.Size = new System.Drawing.Size(65, 21);
                    this.btnHIDmg.TabIndex = 27;
                    this.btnHIDmg.Text = "Damage";
                    this.btnHIDmg.Click += new System.EventHandler(this.ActionListener);
                    //
                    // btnGR
                    //
                    this.btnGR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.btnGR.Location = new System.Drawing.Point(145, 150);
                    this.btnGR.Name = "btnGR";
                    this.btnGR.Size = new System.Drawing.Size(35, 21);
                    this.btnGR.TabIndex = 28;
                    this.btnGR.Text = "GR �";
                    this.btnGR.Click += new System.EventHandler(this.ActionListener);
                    #endregion

                    isLoadedHI = true;
                }
            }
            else if ("tabHO".Equals(tabName))
            {
                if (!isLoadedHO)
                {
                    #region Initilize HO tab
                    // 
                    // cboHOCargoType
                    // 
                    this.cboHOCargoType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.cboHOCargoType.isBusinessItemName = "";
                    this.cboHOCargoType.isMandatory = false;
                    this.cboHOCargoType.Location = new System.Drawing.Point(172, 56);
                    this.cboHOCargoType.Name = "cboHOCargoType";
                    this.cboHOCargoType.Size = new System.Drawing.Size(53, 19);
                    this.cboHOCargoType.TabIndex = 215;
                    this.cboHOCargoType.SelectedIndexChanged += new System.EventHandler(this.cboHOCargoType_SelectedIndexChanged);
                    // 
                    // tLabel16
                    // 
                    this.tLabel16.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.tLabel16.Location = new System.Drawing.Point(136, 57);
                    this.tLabel16.Name = "tLabel16";
                    this.tLabel16.Size = new System.Drawing.Size(30, 15);
                    this.tLabel16.Text = "CgTp";
                    // 
                    // txtHORemark
                    // 
                    this.txtHORemark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                    this.txtHORemark.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHORemark.isBusinessItemName = "Remark";
                    this.txtHORemark.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                    this.txtHORemark.Location = new System.Drawing.Point(46, 192);
                    this.txtHORemark.MaxLength = 50;
                    this.txtHORemark.Name = "txtHORemark";
                    this.txtHORemark.Size = new System.Drawing.Size(181, 19);
                    this.txtHORemark.TabIndex = 201;
                    // 
                    // tLabel14
                    // 
                    this.tLabel14.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.tLabel14.Location = new System.Drawing.Point(16, 194);
                    this.tLabel14.Name = "tLabel14";
                    this.tLabel14.Size = new System.Drawing.Size(26, 15);
                    this.tLabel14.Text = "Rmk";
                    // 
                    // txtHOPkgTp
                    // 
                    this.txtHOPkgTp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                    this.txtHOPkgTp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHOPkgTp.isBusinessItemName = "Package Type";
                    this.txtHOPkgTp.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                    this.txtHOPkgTp.Location = new System.Drawing.Point(36, 56);
                    this.txtHOPkgTp.Multiline = true;
                    this.txtHOPkgTp.Name = "txtHOPkgTp";
                    this.txtHOPkgTp.Size = new System.Drawing.Size(65, 17);
                    this.txtHOPkgTp.TabIndex = 186;
                    // 
                    // btnF4
                    // 
                    this.btnF4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.btnF4.Location = new System.Drawing.Point(104, 56);
                    this.btnF4.Name = "btnF4";
                    this.btnF4.Size = new System.Drawing.Size(30, 17);
                    this.btnF4.TabIndex = 187;
                    this.btnF4.Text = "F4";
                    this.btnF4.Click += new System.EventHandler(this.ActionListener);
                    // 
                    // cboHOClearance
                    // 
                    this.cboHOClearance.Enabled = false;
                    this.cboHOClearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.cboHOClearance.isBusinessItemName = "";
                    this.cboHOClearance.isMandatory = false;
                    this.cboHOClearance.Location = new System.Drawing.Point(171, 15);
                    this.cboHOClearance.Name = "cboHOClearance";
                    this.cboHOClearance.Size = new System.Drawing.Size(53, 19);
                    this.cboHOClearance.TabIndex = 172;
                    // 
                    // txtHOTsptr
                    // 
                    this.txtHOTsptr.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHOTsptr.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                    this.txtHOTsptr.Location = new System.Drawing.Point(120, 20);
                    this.txtHOTsptr.Multiline = true;
                    this.txtHOTsptr.Name = "txtHOTsptr";
                    this.txtHOTsptr.ReadOnly = true;
                    this.txtHOTsptr.Size = new System.Drawing.Size(45, 17);
                    this.txtHOTsptr.TabIndex = 157;
                    // 
                    // txtHOLoadLoc
                    // 
                    this.txtHOLoadLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                    this.txtHOLoadLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHOLoadLoc.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                    this.txtHOLoadLoc.Location = new System.Drawing.Point(46, 172);
                    this.txtHOLoadLoc.Multiline = true;
                    this.txtHOLoadLoc.Name = "txtHOLoadLoc";
                    this.txtHOLoadLoc.ReadOnly = true;
                    this.txtHOLoadLoc.Size = new System.Drawing.Size(70, 17);
                    this.txtHOLoadLoc.TabIndex = 127;
                    // 
                    // tLabel13
                    // 
                    this.tLabel13.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.tLabel13.Location = new System.Drawing.Point(1, 172);
                    this.tLabel13.Name = "tLabel13";
                    this.tLabel13.Size = new System.Drawing.Size(48, 15);
                    this.tLabel13.Text = "Location";
                    // 
                    // txtHOPkgNo
                    // 
                    this.txtHOPkgNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                    this.txtHOPkgNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHOPkgNo.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                    this.txtHOPkgNo.Location = new System.Drawing.Point(36, 38);
                    this.txtHOPkgNo.Multiline = true;
                    this.txtHOPkgNo.Name = "txtHOPkgNo";
                    this.txtHOPkgNo.Size = new System.Drawing.Size(189, 17);
                    this.txtHOPkgNo.TabIndex = 122;
                    // 
                    // txtHODtEnd
                    // 
                    this.txtHODtEnd.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                    this.txtHODtEnd.CustomFormat = "dd/MM/yyyy HH:mm";
                    this.txtHODtEnd.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHODtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                    this.txtHODtEnd.isBusinessItemName = "";
                    this.txtHODtEnd.isMandatory = false;
                    this.txtHODtEnd.Location = new System.Drawing.Point(116, 76);
                    this.txtHODtEnd.Name = "txtHODtEnd";
                    this.txtHODtEnd.Size = new System.Drawing.Size(114, 20);
                    this.txtHODtEnd.TabIndex = 110;
                    //this.txtHODtEnd.Value = new System.DateTime(2008, 10, 24, 16, 17, 0, 828);
                    // 
                    // txtHODtStart
                    // 
                    this.txtHODtStart.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                    this.txtHODtStart.CustomFormat = "dd/MM/yyyy HH:mm";
                    this.txtHODtStart.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHODtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                    this.txtHODtStart.isBusinessItemName = "Start Date";
                    this.txtHODtStart.isMandatory = true;
                    this.txtHODtStart.Location = new System.Drawing.Point(0, 76);
                    this.txtHODtStart.Name = "txtHODtStart";
                    this.txtHODtStart.Size = new System.Drawing.Size(114, 20);
                    this.txtHODtStart.TabIndex = 109;
                    //this.txtHODtStart.Value = new System.DateTime(2008, 10, 24, 16, 17, 0, 437);
                    // 
                    // btnHOConfirm
                    // 
                    this.btnHOConfirm.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.btnHOConfirm.Location = new System.Drawing.Point(97, 217);
                    this.btnHOConfirm.Name = "btnHOConfirm";
                    this.btnHOConfirm.Size = new System.Drawing.Size(60, 24);
                    this.btnHOConfirm.TabIndex = 76;
                    this.btnHOConfirm.Text = "Confirm";
                    this.btnHOConfirm.Click += new System.EventHandler(this.ActionListener);
                    // 
                    // txtHOActQty
                    // 
                    this.txtHOActQty.BackColor = System.Drawing.Color.White;
                    this.txtHOActQty.Enabled = false;
                    this.txtHOActQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHOActQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHOActQty.Location = new System.Drawing.Point(160, 117);
                    this.txtHOActQty.Multiline = true;
                    this.txtHOActQty.Name = "txtHOActQty";
                    this.txtHOActQty.Size = new System.Drawing.Size(54, 17);
                    this.txtHOActQty.TabIndex = 63;
                    this.txtHOActQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    // 
                    // txtHOActM3
                    // 
                    this.txtHOActM3.BackColor = System.Drawing.Color.White;
                    this.txtHOActM3.Enabled = false;
                    this.txtHOActM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHOActM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHOActM3.Location = new System.Drawing.Point(103, 117);
                    this.txtHOActM3.Multiline = true;
                    this.txtHOActM3.Name = "txtHOActM3";
                    this.txtHOActM3.Size = new System.Drawing.Size(54, 17);
                    this.txtHOActM3.TabIndex = 62;
                    this.txtHOActM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    // 
                    // txtHOActMT
                    // 
                    this.txtHOActMT.BackColor = System.Drawing.Color.White;
                    this.txtHOActMT.Enabled = false;
                    this.txtHOActMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHOActMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHOActMT.Location = new System.Drawing.Point(46, 117);
                    this.txtHOActMT.Multiline = true;
                    this.txtHOActMT.Name = "txtHOActMT";
                    this.txtHOActMT.Size = new System.Drawing.Size(54, 17);
                    this.txtHOActMT.TabIndex = 61;
                    this.txtHOActMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    // 
                    // tLabel5
                    // 
                    this.tLabel5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.tLabel5.Location = new System.Drawing.Point(6, 119);
                    this.tLabel5.Name = "tLabel5";
                    this.tLabel5.Size = new System.Drawing.Size(35, 15);
                    this.tLabel5.Text = "Actual";
                    // 
                    // txtHOBalQty
                    // 
                    this.txtHOBalQty.BackColor = System.Drawing.Color.White;
                    this.txtHOBalQty.Enabled = false;
                    this.txtHOBalQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHOBalQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHOBalQty.Location = new System.Drawing.Point(160, 135);
                    this.txtHOBalQty.Multiline = true;
                    this.txtHOBalQty.Name = "txtHOBalQty";
                    this.txtHOBalQty.Size = new System.Drawing.Size(54, 17);
                    this.txtHOBalQty.TabIndex = 45;
                    this.txtHOBalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    // 
                    // txtHOBalM3
                    // 
                    this.txtHOBalM3.BackColor = System.Drawing.Color.White;
                    this.txtHOBalM3.Enabled = false;
                    this.txtHOBalM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHOBalM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHOBalM3.Location = new System.Drawing.Point(103, 135);
                    this.txtHOBalM3.Multiline = true;
                    this.txtHOBalM3.Name = "txtHOBalM3";
                    this.txtHOBalM3.Size = new System.Drawing.Size(54, 17);
                    this.txtHOBalM3.TabIndex = 44;
                    this.txtHOBalM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    // 
                    // txtHOBalMT
                    // 
                    this.txtHOBalMT.BackColor = System.Drawing.Color.White;
                    this.txtHOBalMT.Enabled = false;
                    this.txtHOBalMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHOBalMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHOBalMT.Location = new System.Drawing.Point(46, 135);
                    this.txtHOBalMT.Multiline = true;
                    this.txtHOBalMT.Name = "txtHOBalMT";
                    this.txtHOBalMT.Size = new System.Drawing.Size(54, 17);
                    this.txtHOBalMT.TabIndex = 43;
                    this.txtHOBalMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    // 
                    // tLabel4
                    // 
                    this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.tLabel4.Location = new System.Drawing.Point(1, 137);
                    this.tLabel4.Name = "tLabel4";
                    this.tLabel4.Size = new System.Drawing.Size(44, 15);
                    this.tLabel4.Text = "W/H Bal";
                    // 
                    // btnHOExit
                    // 
                    this.btnHOExit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.btnHOExit.Location = new System.Drawing.Point(164, 217);
                    this.btnHOExit.Name = "btnHOExit";
                    this.btnHOExit.Size = new System.Drawing.Size(50, 24);
                    this.btnHOExit.TabIndex = 59;
                    this.btnHOExit.Text = "Exit";
                    this.btnHOExit.Click += new System.EventHandler(this.ActionListener);
                    // 
                    // btnHOUnsetLoc
                    // 
                    this.btnHOUnsetLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.btnHOUnsetLoc.Location = new System.Drawing.Point(125, 172);
                    this.btnHOUnsetLoc.Name = "btnHOUnsetLoc";
                    this.btnHOUnsetLoc.Size = new System.Drawing.Size(50, 17);
                    this.btnHOUnsetLoc.TabIndex = 58;
                    this.btnHOUnsetLoc.Text = "UnSet";
                    this.btnHOUnsetLoc.Click += new System.EventHandler(this.ActionListener);
                    // 
                    // txtHOLoadQty
                    // 
                    this.txtHOLoadQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                    this.txtHOLoadQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHOLoadQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
                    this.txtHOLoadQty.Location = new System.Drawing.Point(160, 154);
                    this.txtHOLoadQty.Multiline = true;
                    this.txtHOLoadQty.Name = "txtHOLoadQty";
                    this.txtHOLoadQty.Size = new System.Drawing.Size(54, 17);
                    this.txtHOLoadQty.TabIndex = 52;
                    this.txtHOLoadQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    this.txtHOLoadQty.TextChanged += new System.EventHandler(this.ClearHOActLoc);
                    // 
                    // txtHOLoadM3
                    // 
                    this.txtHOLoadM3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                    this.txtHOLoadM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHOLoadM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHOLoadM3.Location = new System.Drawing.Point(103, 154);
                    this.txtHOLoadM3.Multiline = true;
                    this.txtHOLoadM3.Name = "txtHOLoadM3";
                    this.txtHOLoadM3.Size = new System.Drawing.Size(54, 17);
                    this.txtHOLoadM3.TabIndex = 51;
                    this.txtHOLoadM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    this.txtHOLoadM3.TextChanged += new System.EventHandler(this.ClearHOActLoc);
                    // 
                    // txtHOLoadMT
                    // 
                    this.txtHOLoadMT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                    this.txtHOLoadMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHOLoadMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHOLoadMT.Location = new System.Drawing.Point(46, 154);
                    this.txtHOLoadMT.Multiline = true;
                    this.txtHOLoadMT.Name = "txtHOLoadMT";
                    this.txtHOLoadMT.Size = new System.Drawing.Size(54, 17);
                    this.txtHOLoadMT.TabIndex = 50;
                    this.txtHOLoadMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    this.txtHOLoadMT.TextChanged += new System.EventHandler(this.ClearHOActLoc);
                    // 
                    // txtHODocQty
                    // 
                    this.txtHODocQty.BackColor = System.Drawing.Color.White;
                    this.txtHODocQty.Enabled = false;
                    this.txtHODocQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHODocQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHODocQty.Location = new System.Drawing.Point(160, 99);
                    this.txtHODocQty.Multiline = true;
                    this.txtHODocQty.Name = "txtHODocQty";
                    this.txtHODocQty.Size = new System.Drawing.Size(54, 17);
                    this.txtHODocQty.TabIndex = 28;
                    this.txtHODocQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    // 
                    // txtHODocM3
                    // 
                    this.txtHODocM3.BackColor = System.Drawing.Color.White;
                    this.txtHODocM3.Enabled = false;
                    this.txtHODocM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHODocM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHODocM3.Location = new System.Drawing.Point(103, 99);
                    this.txtHODocM3.Multiline = true;
                    this.txtHODocM3.Name = "txtHODocM3";
                    this.txtHODocM3.Size = new System.Drawing.Size(54, 17);
                    this.txtHODocM3.TabIndex = 27;
                    this.txtHODocM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    // 
                    // txtHODocMT
                    // 
                    this.txtHODocMT.Enabled = false;
                    this.txtHODocMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHODocMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
                    this.txtHODocMT.Location = new System.Drawing.Point(46, 99);
                    this.txtHODocMT.Multiline = true;
                    this.txtHODocMT.Name = "txtHODocMT";
                    this.txtHODocMT.Size = new System.Drawing.Size(54, 17);
                    this.txtHODocMT.TabIndex = 26;
                    this.txtHODocMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    // 
                    // lblLorry
                    // 
                    this.lblLorry.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.lblLorry.Location = new System.Drawing.Point(15, 156);
                    this.lblLorry.Name = "lblLorry";
                    this.lblLorry.Size = new System.Drawing.Size(29, 15);
                    this.lblLorry.Text = "Load";
                    // 
                    // Label14
                    // 
                    this.Label14.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.Label14.Location = new System.Drawing.Point(15, 101);
                    this.Label14.Name = "Label14";
                    this.Label14.Size = new System.Drawing.Size(26, 15);
                    this.Label14.Text = "Doc";
                    // 
                    // btnF3
                    // 
                    this.btnF3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.btnF3.Location = new System.Drawing.Point(85, 20);
                    this.btnF3.Name = "btnF3";
                    this.btnF3.Size = new System.Drawing.Size(32, 17);
                    this.btnF3.TabIndex = 43;
                    this.btnF3.Text = "F3";
                    this.btnF3.Click += new System.EventHandler(this.ActionListener);
                    // 
                    // txtHOLorry
                    // 
                    this.txtHOLorry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                    this.txtHOLorry.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHOLorry.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                    this.txtHOLorry.Location = new System.Drawing.Point(36, 20);
                    this.txtHOLorry.Multiline = true;
                    this.txtHOLorry.Name = "txtHOLorry";
                    this.txtHOLorry.Size = new System.Drawing.Size(47, 17);
                    this.txtHOLorry.TabIndex = 42;
                    // 
                    // txtHOGRBL
                    // 
                    this.txtHOGRBL.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.txtHOGRBL.isBusinessItemName = "GR/BL";
                    this.txtHOGRBL.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                    this.txtHOGRBL.isMandatory = true;
                    this.txtHOGRBL.Location = new System.Drawing.Point(36, 1);
                    this.txtHOGRBL.Multiline = true;
                    this.txtHOGRBL.Name = "txtHOGRBL";
                    this.txtHOGRBL.ReadOnly = true;
                    this.txtHOGRBL.Size = new System.Drawing.Size(130, 17);
                    this.txtHOGRBL.TabIndex = 36;
                    // 
                    // Label10
                    // 
                    this.Label10.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.Label10.Location = new System.Drawing.Point(5, 20);
                    this.Label10.Name = "Label10";
                    this.Label10.Size = new System.Drawing.Size(30, 15);
                    this.Label10.Text = "Lorry";
                    // 
                    // chkHOFinal
                    // 
                    this.chkHOFinal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.chkHOFinal.Location = new System.Drawing.Point(180, 172);
                    this.chkHOFinal.Name = "chkHOFinal";
                    this.chkHOFinal.Size = new System.Drawing.Size(50, 17);
                    this.chkHOFinal.TabIndex = 55;
                    this.chkHOFinal.Text = "Final";
                    // 
                    // tLabel11
                    // 
                    this.tLabel11.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.tLabel11.Location = new System.Drawing.Point(0, 38);
                    this.tLabel11.Name = "tLabel11";
                    this.tLabel11.Size = new System.Drawing.Size(38, 17);
                    this.tLabel11.Text = "Pkg No";
                    // 
                    // tLabel12
                    // 
                    this.tLabel12.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.tLabel12.Location = new System.Drawing.Point(1, 2);
                    this.tLabel12.Name = "tLabel12";
                    this.tLabel12.Size = new System.Drawing.Size(34, 15);
                    this.tLabel12.Text = "GR/BL";
                    // 
                    // tLabel17
                    // 
                    this.tLabel17.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.tLabel17.Location = new System.Drawing.Point(172, 2);
                    this.tLabel17.Name = "tLabel17";
                    this.tLabel17.Size = new System.Drawing.Size(52, 15);
                    this.tLabel17.Text = "Clearance";
                    // 
                    // tLabel6
                    // 
                    this.tLabel6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.tLabel6.Location = new System.Drawing.Point(0, 56);
                    this.tLabel6.Name = "tLabel6";
                    this.tLabel6.Size = new System.Drawing.Size(38, 17);
                    this.tLabel6.Text = "Pkg Tp";
                    //
                    // btnHOGP
                    //
                    this.btnHOGP.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.btnHOGP.Location = new System.Drawing.Point(1, 217);
                    this.btnHOGP.Name = "btnHOGP";
                    this.btnHOGP.Size = new System.Drawing.Size(45, 24);
                    this.btnHOGP.TabIndex = 76;
                    this.btnHOGP.Text = "GPass";
                    this.btnHOGP.Click += new System.EventHandler(this.ActionListener);
                    //
                    // btnBL
                    //
                    this.btnBL.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                    this.btnBL.Location = new System.Drawing.Point(50, 217);
                    this.btnBL.Name = "btnBL";
                    this.btnBL.Size = new System.Drawing.Size(40, 24);
                    this.btnBL.TabIndex = 77;
                    this.btnBL.Text = "B/L �";
                    this.btnBL.Click += new System.EventHandler(this.ActionListener);
                    #endregion

                    isLoadedHO = true;
                }
            }
        }

        private void InitializePanel(string tabName)
        {
            if ("tabHI".Equals(tabName))
            {
                this.pnlHISOut.Visible = false;
                this.pnlHISOut.Location = new Point(this.btnHISOut.Left, this.btnHISOut.Top - this.pnlHISOut.Height);
                this.pnlHISOut.m_grResultHI = this.m_grResultHI;
                this.tabHI.Controls.Add(this.pnlHISOut);

                this.pnlHIDmg.Visible = false;
                this.pnlHIDmg.Location = new Point(this.btnHISOut.Left, this.btnHIDmg.Top - this.pnlHIDmg.Height);
                this.pnlHIDmg.m_grResultHI = this.m_grResultHI;
                this.tabHI.Controls.Add(this.pnlHIDmg);

                this.selectGRPanel.Visible = false;
                this.selectGRPanel.Location = new Point(btnHISOut.Left, this.btnGR.Top - this.selectGRPanel.Height);
                this.tabHI.Controls.Add(this.selectGRPanel);
            }
            if ("tabHO".Equals(tabName))
            {
                if (this.m_parm.GrInfo != null)
                {
                    this.btnBL.Text = "GR �";

                    this.selectGRPanel.Visible = false;
                    this.selectGRPanel.Location = new Point(btnHOGP.Left, this.btnBL.Top - this.selectGRPanel.Height);
                    this.tabHO.Controls.Add(this.selectGRPanel);
                }
                else if (this.m_parm.BlInfo != null)
                {
                    this.selectBLPanel.Visible = false;
                    this.selectBLPanel.Location = new Point(btnHOGP.Left, this.btnBL.Top - this.selectBLPanel.Height);
                    this.tabHO.Controls.Add(this.selectBLPanel);
                }
            }
        }
    }
}