using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MOST.Common.Utility;
using Framework.Controls.Container;
using MOST.Common;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.Client.Proxy.CommonProxy;
using Framework.Common.ExceptionHandler;
using Framework.Common.PopupManager;
using Framework.Service.Provider.WebService.Provider;
using Framework.Common.Helper;
using Framework.Controls;
using Framework.Common.ResourceManager;
using Framework.Common.Constants;
using Framework.Common.UserInformation;
using MOST.WHChecker;
using MOST.WHChecker.Parm;
using MOST.WHChecker.Result;
using MOST.Client.Proxy.WHCheckerProxy;


namespace MOST.WHChecker
{
    public partial class HWC103 : TForm, IObserver
    {
        private SearchJPVCResult m_jpvcResult;
        private CargoExportResult m_grResult;
        private BLListResult m_blResult;
        private bool m_isValidJPVC;
        private bool m_isValidGR;
        private bool m_isValidBL;

        //Added by Chris 2015-11-20 Mantis issue 0051083
        private const string TYPE_CODE_VALIDATION = "validationFinalUpdate4Wh";

        public HWC103()
        {
            InitializeComponent();
            this.initialFormSize();
            InitializeData();

            //////////////////////////////////////////////////////////////////////////
            // Making button text be multiline
            WndProcHooker.MakeButtonMultiline(this.btnCargoMovement);
            WndProcHooker.MakeButtonMultiline(this.btnWHReconcile);
            //////////////////////////////////////////////////////////////////////////
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                m_isValidJPVC = false;
                m_isValidGR = false;
                m_isValidBL = false;
                rbtnGR.Checked = true;

                // Get history info
                VesselHistoryInfo vslHistoryInfo = VesselHistoryInfo.GetInstance();
                if (vslHistoryInfo != null && !string.IsNullOrEmpty(vslHistoryInfo.VslCallId))
                {
                    txtJPVC.Text = vslHistoryInfo.VslCallId;
                    txtJPVCName.Text = vslHistoryInfo.VslNm;

                    if (CommonUtility.IsValidJPVC(txtJPVC.Text, ref m_jpvcResult))
                    {
                        m_isValidJPVC = true;
                    }
                }
            } catch (System.Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void OnCheckRadioButton()
        {
            if (rbtnJPVC.Checked)
            {
                txtJPVC.Enabled = true;
                txtJPVCName.Enabled = true;
                btnF1.Enabled = true;
                btnHO.Enabled = true;
            }
            else if (rbtnNonJPVC.Checked)
            {
                txtJPVC.Enabled = false;
                txtJPVCName.Enabled = false;
                btnF1.Enabled = false;
                btnHO.Enabled = false;
            }

            if (rbtnGR.Checked)
            {
                txtGR.Enabled = true;
                btnF2.Enabled = true;
                btnHI.Enabled = true;
                txtBL.Enabled = false;
                btnF3.Enabled = false;

                //Added by Chris 2015-10-01
                txtBL.Text = "";
            }
            else if (rbtnBL.Checked)
            {
                txtGR.Enabled = false;
                btnF2.Enabled = false;
                btnHI.Enabled = false;
                txtBL.Enabled = true;
                btnF3.Enabled = true;

                //Added by Chris 2015-10-01
                txtGR.Text = "";
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
                case "rbtnGR":
                case "rbtnBL":
                    OnCheckRadioButton();
                    break;
            }
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;

            switch (buttonName)
            {
                case "btnHI":
                    if (validations(this.Controls) && ValidateCargoNo())
                    {
                        HWC101Parm parm = new HWC101Parm();
                        parm.GrInfo = null;
                        if (rbtnGR.Checked)
                        {
                            //Added by Chris 2016-04-12
                            if (m_grResult != null)
                            {
                                if (m_grResult.DelvTpCd.Equals("D"))
                                {
                                    DialogResult gpHODr = CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HWC101_0006"));
                                    //DialogResult gpHODr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0072");                       
                                    if (gpHODr == DialogResult.OK)
                                    {
                                        txtGR.Text = "";
                                        return;
                                    }
                                }
                            }
                            parm.GrInfo = m_grResult;
                            if ("Y".Equals(m_grResult.HiFnlYn))
                            {
                                CommonUtility.AlertMessage("This cargo already handled in. Please select another one.");
                            }
                            else
                            {
                                //Added by Chris 2015-11-20 Mantis issue 0051083
                                CommonCodeParm CCparm = new CommonCodeParm();
                                CommonProxy Cproxy = new CommonProxy();
                                CCparm.tyCd = TYPE_CODE_VALIDATION;
                                CCparm.col1 = GetVslCallId();
                                CCparm.col2 = m_grResult.ShipgNoteNo;

                                ResponseInfo info = Cproxy.getValidationCode(CCparm);



                                if (info != null && info.list != null && info.list.Length > 0 && info.list[0] is CommonCodeItem)
                                {
                                    CommonCodeItem item = (CommonCodeItem)info.list[0];
                                    if ("N".Equals(item.isValidated))
                                    {
                                        CommonUtility.AlertMessage("This cargo is no longer Handling Out, because of Handling Out Final");
                                        return;
                                    }

                                }
                                PopupManager.instance.ShowPopup(new HWC101(HWC101.TAB_HI), parm);
                                m_isValidGR = false;
                                //Added by Chris 2016-03-17
                                txtGR.Text = "";
                            }
                        }
                    }
                    break;

                case "btnHO":
                    if (validations(this.Controls) && ValidateCargoNo())
                    {
                        HWC101Parm parm = new HWC101Parm();
                        parm.GrInfo = null;
                        parm.BlInfo = null;
                        if (rbtnGR.Checked)
                        {
                            parm.GrInfo = m_grResult;
                            PopupManager.instance.ShowPopup(new HWC101(HWC101.TAB_HO), parm);
                        }
                        else if (rbtnBL.Checked)
                        {
                            parm.BlInfo = m_blResult;
                            if ("Y".Equals(m_blResult.FnlDelvYn))
                            {
                                CommonUtility.AlertMessage("This cargo already handled out. Please select another one.");
                            }
                            else
                            {
                                PopupManager.instance.ShowPopup(new HWC101(HWC101.TAB_HO), parm);
                                m_isValidBL = false;
                            }
                        }
                    }
                    break;

                case "btnCheckList":
                    int prgType = 0;
                    VSRCheckListParm hac102Parm = new VSRCheckListParm();
                    if (rbtnJPVC.Checked)
                    {
                        if (m_isValidJPVC == false && ValidateJPVC() == false)
                        {
                            return;
                        }
                        prgType = Constants.JPVC;
                        hac102Parm.JpvcInfo = m_jpvcResult;
                    }
                    else if (rbtnNonJPVC.Checked)
                    {
                        prgType = Constants.NONJPVC;
                    }
                    PopupManager.instance.ShowPopup(new HCM119(prgType), hac102Parm);
                    break;

                case "btnCargoJob":
                    HWC104Parm cgJobParm = new HWC104Parm();
                    if (!string.IsNullOrEmpty(txtJPVC.Text.Trim()) &&
                        CommonUtility.IsValidJPVC(txtJPVC.Text, ref m_jpvcResult))
                    {
                        m_isValidJPVC = true;
                        cgJobParm.VslCallId = txtJPVC.Text;
                    }
                    if (rbtnGR.Checked)
                    {
                        cgJobParm.CgNo = txtGR.Text;
                    }
                    else if (rbtnBL.Checked)
                    {
                        cgJobParm.CgNo = txtBL.Text;
                    }
                    PopupManager.instance.ShowPopup(new HWC104(), cgJobParm);
                    break;

                case "btnWHReconcile":
                    HWC105Parm whReconcParm = new HWC105Parm();
                    whReconcParm.VslCallId = GetVslCallId();
                    if (rbtnGR.Checked)
                    {
                        whReconcParm.GrNo = txtGR.Text;
                        if (m_grResult != null)
                        {
                            whReconcParm.SnNo = m_grResult.ShipgNoteNo;
                        }
                    }
                    else if (rbtnBL.Checked)
                    {
                        whReconcParm.BlNo = txtBL.Text;
                    }
                    PopupManager.instance.ShowPopup(new HWC105(), whReconcParm);
                    break;

                case "btnCargoMovement":
                    if (validations(this.Controls) && ValidateCargoNo())
                    {
                        HWC109Parm mvParm = new HWC109Parm();
                        if (rbtnGR.Checked)
                        {
                            if (m_grResult != null)
                            {
                                mvParm.VslCallId = m_grResult.VslCallId;
                                mvParm.ShipgNoteNo = m_grResult.ShipgNoteNo;
                            }
                            mvParm.CgNo = txtGR.Text;
                            mvParm.CaTyCd = "EX";
                        }
                        else if (rbtnBL.Checked)
                        {
                            if (m_blResult != null)
                            {
                                mvParm.VslCallId = m_blResult.VslCallId;
                                mvParm.ShipgNoteNo = string.Empty;
                            }
                            mvParm.CgNo = txtBL.Text;
                            mvParm.CaTyCd = "IM";
                        }
                        PopupManager.instance.ShowPopup(new HWC109(), mvParm);
                    }
                    break;

                case "btnRehandleOPR":
                    HWC107Parm rhdlParm = new HWC107Parm();
                    rhdlParm.VslCallId = GetVslCallId();
                    PopupManager.instance.ShowPopup(new HWC107(), rhdlParm);
                    break;

                case "btnUnclosedOPR":
                    MOST.Common.CommonParm.UnclosedOperationParm unclosedOprParm = new MOST.Common.CommonParm.UnclosedOperationParm();
                    unclosedOprParm.VslCallId = GetVslCallId();
                    PopupManager.instance.ShowPopup(new MOST.Common.HCM115(), unclosedOprParm);
                    break;

                case "btnGatepass":
                    MOST.Common.CommonParm.CargoGatePassParm gpParm = new MOST.Common.CommonParm.CargoGatePassParm();
                    gpParm.VslCallId = GetVslCallId();
                    if (rbtnGR.Checked)
                    {
                        gpParm.CgNo = txtGR.Text.Trim();
                    }
                    else if (rbtnBL.Checked)
                    {
                        gpParm.CgNo = txtBL.Text.Trim();
                    }
                    PopupManager.instance.ShowPopup(new MOST.Common.HCM116(), gpParm);
                    break;

                case "btnExit":
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    break;

                case "btnListHIHO":
                    HWC102Parm listGIParm = new HWC102Parm();
                    listGIParm.VslCallId = GetVslCallId();
                    PopupManager.instance.ShowPopup(new HWC102(HWC102.TAB_HI), listGIParm);
                    break;

                case "btnF1":
                    MOST.Common.CommonParm.SearchJPVCParm jpvcParm = new MOST.Common.CommonParm.SearchJPVCParm();
                    jpvcParm.Jpvc = txtJPVC.Text.Trim();
                    jpvcParm.IsWHChecker = "VSL";
                    SearchJPVCResult jpvcResultTmp = (SearchJPVCResult)PopupManager.instance.ShowPopup(new HCM101(), jpvcParm);
                    if (jpvcResultTmp != null)
                    {   
                        m_jpvcResult = jpvcResultTmp;
                        txtJPVC.Text = m_jpvcResult.Jpvc;
                        txtJPVCName.Text = m_jpvcResult.VesselName;
                        m_isValidJPVC = true;

                        // Save history vslCallId
                        VesselHistoryInfo.SetInstance(m_jpvcResult.Jpvc, m_jpvcResult.VesselName,
                                                        m_jpvcResult.VslTp, m_jpvcResult.VslTpNm,
                                                        m_jpvcResult.PurpCallCd, m_jpvcResult.PurpCall);

                        
                    }
                    break;

                case "btnF2":
                    GRListParm grParm = new GRListParm();
                    grParm.DelvTpCd = txtGR.Text.Trim();
                    grParm.Jpvc = GetVslCallId();
                    grParm.GrNo = txtGR.Text.Trim();
                    if ((m_grResult != null) && (!string.IsNullOrEmpty(m_grResult.ShipgNoteNo)))
                    {
                        grParm.ShipgNoteNo = m_grResult.ShipgNoteNo;
                    }
                    grParm.Screenid = "WC103";

                    CargoExportResult grResultTmp = (CargoExportResult)PopupManager.instance.ShowPopup(new HCM103(), grParm);
                    if (grResultTmp != null)
                    {
                        m_isValidGR = true;
                        m_grResult = grResultTmp;
                        txtGR.Text = m_grResult.GrNo;
                    }

                    break;

                case "btnF3":
                    BLListParm blParm = new BLListParm();
                    //blParm.ExcludeFnlHandlingOut = true;
                    blParm.Jpvc = GetVslCallId();
                    blParm.BlNo = txtBL.Text.Trim();
                    BLListResult blResultTmp = (BLListResult)PopupManager.instance.ShowPopup(new HCM104(), blParm);
                    if (blResultTmp != null)
                    {
                        m_isValidBL = true;
                        m_blResult = blResultTmp;
                        txtBL.Text = m_blResult.Bl;
                    }
                    break;
            }
        }

        private string GetVslCallId()
        {
            string vslCallId = string.Empty;
            if (rbtnJPVC.Checked)
            {
                vslCallId = txtJPVC.Text.Trim();
            }
            else if (rbtnNonJPVC.Checked)
            {
                vslCallId = Constants.NONCALLID;
            }
            return vslCallId;
        }

        private bool ValidateJPVC()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (rbtnNonJPVC.Checked)
                {
                    return true;
                }

                txtJPVCName.Text = "";
                if (!string.IsNullOrEmpty(txtJPVC.Text.Trim()))
                {
                    if (!CommonUtility.IsValidJPVC(txtJPVC.Text.Trim(), ref m_jpvcResult))
                    {
                        m_isValidJPVC = false;
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0027"));
                        txtJPVC.SelectAll();
                        txtJPVC.Focus();
                        return false;
                    }
                    else
                    {   
                        txtJPVC.Text = m_jpvcResult.Jpvc;
                        txtJPVCName.Text = m_jpvcResult.VesselName;
                        m_isValidJPVC = true;

                        // Save history vslCallId
                        VesselHistoryInfo.SetInstance(m_jpvcResult.Jpvc, m_jpvcResult.VesselName,
                                                        m_jpvcResult.VslTp, m_jpvcResult.VslTpNm,
                                                        m_jpvcResult.PurpCallCd, m_jpvcResult.PurpCall);
                        
                        return true;
                    }
                }
                else
                {
                    m_isValidJPVC = false;
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0027"));
                    txtJPVC.SelectAll();
                    txtJPVC.Focus();
                    return false;
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
            return false;
        }

        private bool ValidateCargoNo()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (rbtnJPVC.Checked && !string.IsNullOrEmpty(txtJPVC.Text.Trim()))
                {
                    if (m_isValidJPVC == false && ValidateJPVC() == false)
                    {
                        return false;
                    }
                }

                if (rbtnGR.Checked)
                {
                    if (m_isValidGR == false)
                    {
                        if (string.IsNullOrEmpty(txtGR.Text))
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0050"));
                            txtGR.SelectAll();
                            txtGR.Focus();
                            return false;
                        }

                        if (CommonUtility.IsValidCargoExportGR(GetVslCallId(), txtGR.Text, ref m_grResult) == false)
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0029"));
                            txtGR.SelectAll();
                            txtGR.Focus();
                            return false;
                        }
                        else
                        {
                            m_isValidGR = true;
                        }
                    }
                }
                else if (rbtnBL.Checked)
                {
                    if (m_isValidBL == false)
                    {
                        if (string.IsNullOrEmpty(txtBL.Text))
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0051"));
                            txtBL.SelectAll();
                            txtBL.Focus();
                            return false;
                        }

                        if (CommonUtility.IsValidBL(GetVslCallId(), txtBL.Text, ref m_blResult) == false)
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0030"));
                            txtBL.SelectAll();
                            txtBL.Focus();
                            return false;
                        }
                        else
                        {
                            m_isValidBL = true;
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

        private void txtJPVC_TextChanged(object sender, EventArgs e)
        {
            m_isValidJPVC = false;
            txtJPVCName.Text = "";
        }

        public void receiveNotify(NoticeMessage message)
        {
            this.Close();
        }

        private void txtGR_TextChanged(object sender, EventArgs e)
        {
            m_isValidGR = false;
        }

        private void txtBL_TextChanged(object sender, EventArgs e)
        {
            m_isValidBL = false;
        }

        private void txtJPVC_KeyPress(object sender, KeyPressEventArgs e)
        {
            m_isValidJPVC = false;
            txtJPVCName.Text = string.Empty;

            // if key = Enter then get vessel name
            if (e.KeyChar.Equals((char)Keys.Enter))
            {
                if (CommonUtility.IsValidJPVC(txtJPVC.Text, ref m_jpvcResult))
                {
                    m_isValidJPVC = true;

                    // Set vessel name
                    txtJPVCName.Text = m_jpvcResult.VesselName;

                    // Save history vslCallId
                    VesselHistoryInfo.SetInstance(m_jpvcResult.Jpvc, m_jpvcResult.VesselName,
                                                    m_jpvcResult.VslTp, m_jpvcResult.VslTpNm,
                                                    m_jpvcResult.PurpCallCd, m_jpvcResult.PurpCall);
                }
            }
        }
    }
}