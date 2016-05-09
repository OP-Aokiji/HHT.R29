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
using Framework.Common.ResourceManager;
using Framework.Service.Provider.WebService.Provider;

using Framework.Controls;
using Framework.Common.Constants;
using MOST.ApronChecker.Parm;
using MOST.ApronChecker.Result;

namespace MOST.ApronChecker
{
    public partial class HAC101 : TForm, IObserver
    {
        //added by William 2015.11.20 Mantis issue 0051083
        private const string FINAL_CARGO = "validationFinalCargo";

        private SearchJPVCResult m_jpvcResult;
        private CargoExportResult m_grResult;
        public CargoExportResult GrResult
        {
            get { return m_grResult; }
            set { m_grResult = value; }
        }
        private BLListResult m_blResult;
        public BLListResult BlResult
        {
            get { return m_blResult; }
            set { m_blResult = value; }
        }
        private bool m_isValidJPVC;
        private bool m_isValidGR;
        private bool m_isValidBL;

        public HAC101()
        {
            InitializeComponent();
            this.initialFormSize();
            InitializeData();
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
            }
            catch (System.Exception ex)
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
                btnConfirm.Enabled = true;
                btnLoadingList.Enabled = true;
                btnDischargingList.Enabled = true;

                rbtnGR.Enabled = true;
                rbtnBL.Enabled = true;
                txtGR.Enabled = true;
                txtBL.Enabled = true;

                btnF2.Enabled = true;
                btnF3.Enabled = true;


            }
            else if (rbtnNonJPVC.Checked)
            {
                txtJPVC.Enabled = false;
                txtJPVCName.Enabled = false;
                btnF1.Enabled = false;
                btnConfirm.Enabled = false;
                btnLoadingList.Enabled = false;
                btnDischargingList.Enabled = false;
            }

            if (rbtnGR.Checked)
            {
                txtGR.Enabled = true;
                btnF2.Enabled = true;
                txtBL.Enabled = false;
                btnF3.Enabled = false;
            }
            else if (rbtnBL.Checked)
            {
                txtGR.Enabled = false;
                btnF2.Enabled = false;
                txtBL.Enabled = true;
                btnF3.Enabled = true;
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

                        VesselHistoryInfo.SetInstance(m_jpvcResult.Jpvc, m_jpvcResult.VesselName,
                                                        m_jpvcResult.VslTp, m_jpvcResult.VslTpNm,
                                                        m_jpvcResult.PurpCallCd, m_jpvcResult.PurpCall);

                        return true;
                    }
                }
                else
                {
                    m_isValidJPVC = false;
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0049"));
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
                        if (string.IsNullOrEmpty(txtGR.Text.Trim()))
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0050"));
                            txtGR.SelectAll();
                            txtGR.Focus();
                            return false;
                        }

                        if (CommonUtility.IsValidCargoExportGR(GetVslCallId(), txtGR.Text.Trim(), ref m_grResult) == false)
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
                        if (string.IsNullOrEmpty(txtBL.Text.Trim()))
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0051"));
                            txtBL.SelectAll();
                            txtBL.Focus();
                            return false;
                        }

                        if (CommonUtility.IsValidBL(GetVslCallId(), txtBL.Text.Trim(), ref m_blResult) == false)
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0030"));
                            txtBL.SelectAll();
                            txtBL.Focus();
                            return false;
                        }
                        else
                        {
                            if (m_blResult.FnlOpeYn.Equals("Y"))
                            {
                                CommonUtility.AlertMessage("This BL is final operation", "Warning");
                                return false;
                            }

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
                case "btnConfirm":
                    if (validations(this.Controls) && ValidateCargoNo())
                    {
                        if (rbtnGR.Checked)
                        {
                            // Check if this cargo is special cargo.
                            if ("Y".Equals(m_grResult.SpYn))
                            {
                                CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC101_0001"));
                                break;
                            }

                            //added by William 2015.11.20 Mantis issue 0051083
                            //Check Final Loading
                            if (m_grResult != null)
                            {
                                CommonCodeParm vParm = new CommonCodeParm();
                                vParm.tyCd = FINAL_CARGO;
                                vParm.col1 = GetVslCallId();
                                vParm.col2 = m_grResult.ShipgNoteNo;
                                ICommonProxy proxy = new CommonProxy();
                                ResponseInfo info = proxy.getValidationCode(vParm);
                                if (info != null && info.list.Length > 0 && info.list[0] is CommonCodeItem)
                                {
                                    CommonCodeItem item = (CommonCodeItem)info.list[0];
                                    m_grResult.IsValidated = item.isValidated;
                                    if ("N".Equals(item.isValidated))
                                    {
                                        if ("ST".Equals(item.ref5))
                                        {
                                            goto Continue_tag;
                                        }
                                        else
                                        {
                                            CommonUtility.AlertMessage("This cargo is no longer loading, because of loading final.");
                                            return;
                                        }
                                    }
                                }
                                //end
                            Continue_tag:
                                HAC105Parm parm = new HAC105Parm();
                                parm.VslCallId = GetVslCallId();
                                m_grResult.GrNo = this.txtGR.Text;
                                parm.GrInfo = m_grResult;
                                PopupManager.instance.ShowPopup(new MOST.ApronChecker.HAC105(), parm);
                                
                                //Added by Chris 2015-03-07
                                //this.txtGR.Text = m_grResult.GrNo;
                                txtGR.Text = "";
                            }
                        }
                        else if (rbtnBL.Checked)
                        {
                            HAC106Parm parm = new HAC106Parm();
                            parm.VslCallId = GetVslCallId();
                            m_blResult.Bl = this.txtBL.Text;
                            parm.BlInfo = m_blResult;
                            PopupManager.instance.ShowPopup(new MOST.ApronChecker.HAC106(), parm);

                            this.m_isValidBL = false;
                            //Added by Chris 2015-03-07
                            //this.txtBL.Text = m_blResult.Bl;
                            txtBL.Text = "";
                        }
                       
                    }
                    break;

                case "btnCargoJob":
                    if (validations(this.Controls))
                    {
                        MOST.ApronChecker.Parm.HAC110Parm parm = new MOST.ApronChecker.Parm.HAC110Parm();
                        if (!string.IsNullOrEmpty(txtJPVC.Text.Trim()) && 
                            CommonUtility.IsValidJPVC(txtJPVC.Text, ref m_jpvcResult))
                        {
                            m_isValidJPVC = true;
                            parm.VslCallId = txtJPVC.Text;
                        }
                        if (rbtnGR.Checked)
                        {   
                            parm.CgNo = txtGR.Text.Trim();
                        }
                        else if (rbtnBL.Checked)
                        {
                            parm.CgNo = txtBL.Text.Trim();
                        }
                        PopupManager.instance.ShowPopup(new MOST.ApronChecker.HAC110(), parm);
                    }
                    break;

                case "btnGatePass":
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

                case "btnUnclosedOPR":
                    MOST.Common.CommonParm.UnclosedOperationParm unclosedOprParm = new MOST.Common.CommonParm.UnclosedOperationParm();
                    unclosedOprParm.VslCallId = GetVslCallId();
                    PopupManager.instance.ShowPopup(new MOST.Common.HCM115(), unclosedOprParm);
                    break;

                case "btnExit":
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
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

                    HCM119 frm = new HCM119(prgType);
                    frm.HideStevedoreTab();
                    PopupManager.instance.ShowPopup(frm, hac102Parm);
                    
                    break;

                case "btnRehandleOPR":
                    HAC111Parm hac111Parm = new HAC111Parm();
                    hac111Parm.VslCallId = GetVslCallId();
                    PopupManager.instance.ShowPopup(new HAC111(), hac111Parm);
                    break;

                case "btnLoadingList":
                    if (m_isValidJPVC == false && ValidateJPVC() == false)
                    {
                        return;
                    }
                    HAC107Parm hac107Parm = new HAC107Parm();
                    hac107Parm.JpvcInfo = m_jpvcResult;
                    if (this.m_grResult != null)
                    {
                        hac107Parm.GrInfo = m_grResult;
                        hac107Parm.GrInfo.GrNo = this.txtGR.Text;
                    }
                    else if (!string.Empty.Equals(this.txtGR.Text))
                    {
                        hac107Parm.GrInfo = new CargoExportResult();
                        hac107Parm.GrInfo.GrNo = this.txtGR.Text;
                    }
                    
                    PopupManager.instance.ShowPopup(new HAC107(), hac107Parm);
                    break;

                case "btnDischargingList":
                    if (m_isValidJPVC == false && ValidateJPVC() == false)
                    {
                        return;
                    }
                    HAC108Parm hac108Parm = new HAC108Parm();
                    hac108Parm.JpvcInfo = m_jpvcResult;
                    if (!string.Empty.Equals(this.txtBL.Text))
                    {
                        hac108Parm.BlInfo = new BLListResult();
                        hac108Parm.BlInfo.Bl = this.txtBL.Text;
                    }
                    PopupManager.instance.ShowPopup(new HAC108(), hac108Parm);
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

                        VesselHistoryInfo.SetInstance(m_jpvcResult.Jpvc, m_jpvcResult.VesselName,
                                                        m_jpvcResult.VslTp, m_jpvcResult.VslTpNm,
                                                        m_jpvcResult.PurpCallCd, m_jpvcResult.PurpCall);
                    }
                    break;

                case "btnF2":
                    GRListParm grParm = new GRListParm();
                    grParm.Jpvc = GetVslCallId();
                    grParm.GrNo = txtGR.Text.Trim();
                    if ((m_grResult != null) && (!string.IsNullOrEmpty(m_grResult.ShipgNoteNo)))
                    {
                        grParm.ShipgNoteNo = m_grResult.ShipgNoteNo;
                    }
                    grParm.Screenid = "AC101";

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
        public void receiveNotify(NoticeMessage message)
        {
            this.Close();
        }
        private void txtJPVC_TextChanged(object sender, EventArgs e)
        {
            m_isValidJPVC = false;
            txtJPVCName.Text = "";

            btnConfirm.Enabled = true;

            rbtnGR.Enabled = true;
            rbtnBL.Enabled = true;
            txtGR.Enabled = true;
            txtBL.Enabled = true;

            btnF2.Enabled = true;
            btnF3.Enabled = true;

        }
       
        private void txtGR_TextChanged(object sender, EventArgs e)
        {
            m_isValidGR = false;
        }
        private void setStatusControl_CR_OGA(Boolean b)
        {
            rbtnGR.Enabled = b;
            rbtnBL.Enabled = b;
            txtGR.Enabled = b;
            txtBL.Enabled = b;
            btnF2.Enabled = b;
            btnF3.Enabled = b;
            btnConfirm.Enabled = b;
        }
        private void txtBL_TextChanged(object sender, EventArgs e)
        {
            m_isValidBL = false;
        }
        private void txtJPVC_KeyPress(object sender, KeyPressEventArgs e)
        {
            m_isValidJPVC = false;
            txtJPVCName.Text = string.Empty;

            if (e.KeyChar.Equals((char)Keys.Enter))
            {
                if (CommonUtility.IsValidJPVC(txtJPVC.Text, ref m_jpvcResult))
                {
                    m_isValidJPVC = true;

                    
                    txtJPVCName.Text = m_jpvcResult.VesselName;

                    VesselHistoryInfo.SetInstance(m_jpvcResult.Jpvc, m_jpvcResult.VesselName,
                                                    m_jpvcResult.VslTp, m_jpvcResult.VslTpNm,
                                                    m_jpvcResult.PurpCallCd, m_jpvcResult.PurpCall);

                }
            }
        }
        private void HAC101_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q && e.Modifiers == Keys.Alt)
            {
                MessageBox.Show("Screen ID : " + this.Name + "\n\rScreen caption: " + this.Text, "Screen Information !");
            }
        }
    }
}