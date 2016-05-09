using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using Framework.Common.Constants;
using MOST.Client.Proxy.PortSafetyProxy;
using Framework.Service.Provider.WebService.Provider;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using Framework.Common.PopupManager;
using MOST.Client.Proxy.CommonProxy;
using Framework.Common.ExceptionHandler;
using MOST.Common;
using MOST.Common.Utility;
using MOST.PortSafety.Parm;
using Framework.Controls;
using Framework.Common.UserInformation;

namespace MOST.PortSafety
{
    public partial class HPS101 : TForm, IObserver
    {
        #region Variables
        private const int TAB_GI = 1;
        private const int TAB_GO = 2;

        private const string DGSTAT_NOTCONFIRMED = "Not Confirmed";
        private const string DGSTAT_CONFIRMED = "Confirm";
        private const string DGSTAT_REJECT = "Reject";
        private const string DGSTAT_CANCEL = "Cancel";

        //Added By Chris 2015-10-01
        private const string DGSTAT_SAVE = "Save";
        private const string DGSTAT_SUBMIT = "Submit";
        private const string DGSTAT_SUBMIT_CD = "N";
        private const string DGSTAT_SAVE_CD = "S";
        private const string DGSTAT_CONFIRMED_CD = "Y";
        private const string DGSTAT_REJECT_CD = "C";

        //added by William (2015/08/04 - HHT) Mantis issue 49799
        private const string SELECT = "Select";
        private const string YES = "Y";
        private const string NO = "N";
        private const string GATE_OUT = "gateOut";
        private const string CHECK = "Chk";
        private const string IN = "I";
        private const string OUT = "O";
        private CargoArrvDelvResult m_cargoGOResult = null;
        private bool m_isValidGOLorry = false;
        private GRListResult m_grGIResult;
        private DOListResult m_doGIResult;
        private GRListResult m_grGOResult;
        private GatePassListResult m_gpGOResult;
        private bool m_isValidGIGR;
        private bool m_isValidGIDO;
        private bool m_isValidGIDriver;
        private bool m_isValidGOGR;
        private bool m_isValidGOGP;
        private bool m_isValidGODriver;

        //Added by Chris 2015-12-1
        private bool isFirstTime = true;
        //End



        #endregion

        //private void DrawTabItem(object sender, EventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    TabPage tp = tabMain.TabPages[e.Index];
        //    StringFormat sf = new StringFormat();
        //    sf.Alignment = StringAlignment.Center;
        //    RectangleF headerRect = new RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height - 2);
        //    SolidBrush sb = new SolidBrush(Color.Transparent);
        //    if (tabMain.SelectedIndex == e.Index)
        //        sb.Color = Color.LightBlue;
        //    g.FillRectangle(sb, e.Bounds);
        //    g.DrawString(tp.Text, tabMain.Font, new SolidBrush(Color.Black), headerRect, sf);
        //}

        public HPS101()
        {
            InitializeComponent();
            //this.tabMain.DrawMode = TabDrawMode.OwnerDrawFixed;
            //this.tabMain.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DrawTabItem);
            this.initialFormSize();
            this.authorityCheck();
            InitializeData();

            //Added by Chris 2015-11-27
            tabMain.TabPages[0].Text = "GATE IN";
            tabMain.TabPages[1].Text = "Gate Out";
        }


        private void InitializeData()
        {
            // Initialize variables
            m_isValidGIGR = false;
            m_isValidGIDO = false;
            m_isValidGIDriver = false;
            m_isValidGOGR = false;
            m_isValidGOGP = false;
            m_isValidGODriver = false;

            /*
            this.txtGITime.Value = CommonUtility.ParseStringToDate
                (UserInfo.getInstance().Workdate, "dd/MM/yyyy");
            this.txtGOTime.Value = CommonUtility.ParseStringToDate
                (UserInfo.getInstance().Workdate, "dd/MM/yyyy");
            */

            this.txtGITime.Value = DateTime.Now;
            this.txtGOTime.Value = DateTime.Now;
            // Load comboboxes data
            this.LoadCombobox();
            //this.rbtnGOGP.Checked = true;
            //this.rbtnGOGPCheck();
        }

        private void LoadCombobox()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // DG
                CommonUtility.InitializeCombobox(cboGIDGChk);
                cboGIDGChk.Items.Add(new ComboboxValueDescriptionPair(YES, YES));
                cboGIDGChk.Items.Add(new ComboboxValueDescriptionPair(NO, NO));

                // DG Status
                CommonUtility.InitializeCombobox(cboGIDGStatus);
                //cboGIDGStatus.Items.Add(new ComboboxValueDescriptionPair(DGSTAT_NOTCONFIRMED, DGSTAT_NOTCONFIRMED));
                //cboGIDGStatus.Items.Add(new ComboboxValueDescriptionPair(DGSTAT_CONFIRMED, DGSTAT_CONFIRMED));
                //cboGIDGStatus.Items.Add(new ComboboxValueDescriptionPair(DGSTAT_REJECT, DGSTAT_REJECT));
                //cboGIDGStatus.Items.Add(new ComboboxValueDescriptionPair(DGSTAT_CANCEL, DGSTAT_CANCEL));

                //Added by Chris 2015-10-01
                cboGIDGStatus.Items.Add(new ComboboxValueDescriptionPair(DGSTAT_CONFIRMED_CD, DGSTAT_CONFIRMED));
                cboGIDGStatus.Items.Add(new ComboboxValueDescriptionPair(DGSTAT_REJECT_CD, DGSTAT_REJECT));
                cboGIDGStatus.Items.Add(new ComboboxValueDescriptionPair(DGSTAT_SAVE_CD, DGSTAT_SAVE));
                cboGIDGStatus.Items.Add(new ComboboxValueDescriptionPair(DGSTAT_SUBMIT_CD, DGSTAT_SUBMIT));

                // Gate
                IPortSafetyProxy proxy = new PortSafetyProxy();
                CargoArrvDelvParm parm = new CargoArrvDelvParm();
                ResponseInfo info = proxy.getCargoArrvDelvComboList(parm);
                CommonUtility.InitializeCombobox(cboGIGate, SELECT);
                CommonUtility.InitializeCombobox(cboGOGate, SELECT);
                CommonUtility.InitializeCombobox((ComboBox)this.cboShftGO, SELECT);
                if (info != null)
                {
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is CodeMasterListItem)
                        {
                            CodeMasterListItem item = (CodeMasterListItem)info.list[i];
                            cboGIGate.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                            cboGOGate.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                        }
                        else if (info.list[i] is CodeMasterListItem1)
                        {
                            CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                            cboGIGate.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                            cboGOGate.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                        }
                    }
                }

                ICommonProxy commonProxy = (ICommonProxy)new CommonProxy();
                ShiftParm parm2 = new ShiftParm();
                ResponseInfo shift = commonProxy.getShift(parm2);
                if (shift == null)
                    return;
                for (int index = 0; index < shift.list.Length; ++index)
                {
                    if (shift.list[index] is ShiftDataItem)
                    {
                        ShiftDataItem shiftDataItem = (ShiftDataItem)shift.list[index];
                        this.cboShftGO.Items.Add((object)new ComboboxValueDescriptionPair
                            ((object)shiftDataItem.shiftId, (object)shiftDataItem.shiftName));
                    }
                }
                CommonUtility.SetComboboxSelectedItem((ComboBox)this.cboShftGO, UserInfo.getInstance().Shift);
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

        private void RadiobtnCheckedListener(object sender, EventArgs e)
        {
            String ctrlName = string.Empty;
            if (sender is Button)
            {
                Button mybutton = (Button)sender;
                ctrlName = mybutton.Name;
            }
            else if (sender is TextBox)
            {
                TextBox mytextbox = (TextBox)sender;
                ctrlName = mytextbox.Name;
            }

            switch (ctrlName)
            {
                case "btnGIF1":
                case "txtGIGRNo":
                    rbtnGIGR.Checked = true;
                    //rbnGIGRChecked();
                    break;
                case "btnGIF2":
                case "txtGIDONo":
                    rbtnGIDO.Checked = true;
                    //rbnGIDOChecked();
                    break;

                case "btnGOF2":
                    rbtnGOGR.Checked = true;
                    break;
                case "txtGOGR":
                    //rbtnGOGR.Checked = true;
                    break;
                case "btnGOF1":
                    rbtnGOGP.Checked = true;
                    break;
                case "txtGOGP":
                    //rbtnGOGP.Checked = true;
                    break;
            }
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnGIConfirm":

                    //added by William (2015/08/05 - HHT) Mantis issue 49799
                    if (string.IsNullOrEmpty(txtGILorry.Text.Trim()))
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0055"));
                        btnGIF3.Focus();
                        return;
                    }

                    if (!IsValidGILorry(txtGILorry.Text.Trim()))
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0032"));
                        btnGIF3.Focus();
                        return;
                    }
                    if (rbtnGIGR.Checked)
                    {
                        if (!string.IsNullOrEmpty(txtGIGRNo.Text.Trim()))
                        {
                            if (!m_isValidGIGR)
                            {
                                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0029"));
                                btnGIF1.Focus();
                                return;
                            }
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM00001111"));
                            btnGIF1.Focus();
                            return;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txtGIDONo.Text.Trim()))
                        {
                            if (!m_isValidGIDO)
                            {
                                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0031"));
                                btnGIF2.Focus();
                                return;
                            }
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM00001112"));
                            btnGIF2.Focus();
                            return;
                        }
                    }
                    //-----------------------------------------------------------------------------------------------


                    if (this.validations(this.tabGI.Controls) && Validate(HPS101.TAB_GI))
                    {
                        if (IsExpiredLicense(HPS101.TAB_GI))
                        {
                            if (DialogResult.Yes == CommonUtility.ConfirmMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0047")))
                            {
                                if (ProcessCargoArrvDelvItem(HPS101.TAB_GI))
                                {
                                    ClearCtrlValues(HPS101.TAB_GI);
                                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
                                }
                            }
                        }
                        else
                        {
                            if (ProcessCargoArrvDelvItem(HPS101.TAB_GI))
                            {
                                ClearCtrlValues(HPS101.TAB_GI);
                                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
                            }
                        }
                    }
                    break;

                case "btnGOConfirm":
                    //added by William (2015/08/05 - HHT) Mantis issue 49799
                    if (string.IsNullOrEmpty(txtGOLorry.Text.Trim()))
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0055"));
                        btnGOF3.Focus();
                        return;
                    }

                    if (!m_isValidGOLorry)
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0032"));
                        btnGOF3.Focus();
                        return;
                    }
                    if (rbtnGOGR.Checked)
                    {
                        if (!string.IsNullOrEmpty(txtGOGR.Text))
                        {
                            if (!m_isValidGOGR)
                            {
                                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0029"));
                                btnGOF2.Focus();
                                return;
                            }
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM00001111"));
                            btnGOF2.Focus();
                            return;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txtGOGP.Text))
                        {
                            if (!m_isValidGOGP)
                            {
                                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0033"));
                                btnGOF1.Focus();
                                return;
                            }
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM00001113"));
                            btnGOF1.Focus();
                            return;
                        }
                    }
                    //-----------------------------------------------------------------------------------------------


                    if (this.validations(this.tabGO.Controls) && Validate(HPS101.TAB_GO))
                    {
                        if (IsExpiredLicense(HPS101.TAB_GO))
                        {
                            if (DialogResult.Yes == CommonUtility.ConfirmMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0047")))
                            {
                                if (ProcessCargoArrvDelvItem(HPS101.TAB_GO))
                                {
                                    ClearCtrlValues(HPS101.TAB_GO);
                                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
                                }
                            }
                        }
                        else
                        {
                            if (ProcessCargoArrvDelvItem(HPS101.TAB_GO))
                            {
                                ClearCtrlValues(HPS101.TAB_GO);
                                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
                            }
                        }
                    }
                    break;

                case "btnGIExit":
                case "btnGOExit":
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    break;

                case "btnGIF1":
                    //added by William (2015/07/22 - HHT) Mantis issue 49799
                    if (string.IsNullOrEmpty(txtGILorry.Text.Trim()))
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0055"));
                        return;
                    }

                    GRListParm grGIParm = new GRListParm();
                    grGIParm.GateInOut = IN;


                    //commented by William (2015/07/21 - HHT) Mantis issue 49799

                    //grGIParm.GrNo = txtGIGRNo.Text;
                    //if (m_grGIResult != null)
                    //{
                    //    grGIParm.Jpvc = m_grGIResult.VslCallId;
                    //    grGIParm.ShipgNoteNo = m_grGIResult.ShipgNoteNo;
                    //}

                    //added by William (2015/07/21 - HHT) Mantis issue 49799
                    grGIParm.LorryNo = txtGILorry.Text.Trim();
                    grGIParm.DriverId = txtGIDriver.Text.Trim();
                    grGIParm.GrNo = txtGIGRNo.Text.Trim();

                    GRListResult grGIResult = (GRListResult)PopupManager.instance.ShowPopup(new HCM117(), grGIParm);

                    if (grGIResult != null)
                    {
                        m_grGIResult = grGIResult;
                        txtGIGRNo.Text = m_grGIResult.GrNo;
                        txtGICompany.Text = m_grGIResult.Tsptr;
                        //txtGILorry.Text = m_grGIResult.Lorry;
                        txtGIDelvMode.Text = m_grGIResult.DelvTpNm;
                        txtGICmdt.Text = m_grGIResult.CmdtCd;
                        txtGIMt.Text = m_grGIResult.Mt;
                        txtGIQty.Text = m_grGIResult.Qty;
                        CommonUtility.SetComboboxSelectedItem(cboGIDGChk, m_grGIResult.DgYn);
                        CommonUtility.SetComboboxSelectedItem(cboGIDGStatus, m_grGIResult.DgStatCd);

                        //Added by Chris 2015-11-18
                        txtGIDriver.Text = m_grGIResult.DriverId;
                        txtGILicense.Text = m_grGIResult.Lic;
                        txtGIExpDate.Text = m_grGIResult.Expire;

                        m_isValidGIGR = true;
                    }
                    break;

                case "btnGIF2":
                    //added by William (2015/07/22 - HHT) Mantis issue 49799
                    if (string.IsNullOrEmpty(txtGILorry.Text.Trim()))
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0055"));
                        return;
                    }
                    DOListParm doParm = new DOListParm();

                    //commented by William (2015/07/21 - HHT) Mantis issue 49799
                    //if (m_doGIResult != null)
                    //{
                    //    doParm.Jpvc = m_doGIResult.VslCallId;
                    //}

                    //added by William (2015/07/21 - HHT) Mantis issue 49799
                    doParm.LorryNo = txtGILorry.Text.Trim();
                    doParm.DriverId = txtGIDriver.Text.Trim();
                    doParm.DoNo = txtGIDONo.Text.Trim();

                    DOListResult doResult = (DOListResult)PopupManager.instance.ShowPopup(new HCM108(), doParm);
                    if (doResult != null)
                    {
                        m_doGIResult = doResult;
                        txtGICompany.Text = m_doGIResult.Tsptr;
                        txtGIDelvMode.Text = m_doGIResult.DelvTpNm;
                        CommonUtility.SetComboboxSelectedItem(cboGIDGChk, m_doGIResult.DgYn);
                        CommonUtility.SetComboboxSelectedItem(cboGIDGStatus, m_doGIResult.DgStatCd);
                        txtGIDONo.Text = m_doGIResult.DoNo;
                        m_isValidGIDO = true;
                        txtGICmdt.Text = m_doGIResult.CmdtCd;
                        txtGIMt.Text = m_doGIResult.Mt;
                        txtGIQty.Text = m_doGIResult.Qty;

                        //Added by Chris 2015-11-18
                        txtGIDriver.Text = m_doGIResult.DriverId;
                        txtGILicense.Text = m_doGIResult.Lic;
                        txtGIExpDate.Text = m_doGIResult.Expire;
                    }
                    break;

                case "btnGIF3":

                    #region commented by William (2015/07/21) Mantis issue 49799
                    //if (ValidateEmptyCargoNo(HPS101.TAB_GI))
                    //{
                    //if (rbtnGIGR.Checked || rbtnGIDO.Checked)
                    //{
                    //PartnerCodeListParm lorryGIParm = new PartnerCodeListParm();
                    //lorryGIParm.Option = "CD";
                    //if (m_grGIResult != null)
                    //{
                    //    lorryGIParm.PtnrCd = m_grGIResult.Tsptr;
                    //}

                    //commented by William (2015/07/21) Mantis issue: 49799
                    //if (rbtnGIGR.Checked)
                    //{
                    //    if (m_grGIResult != null)
                    //    {
                    //        lorryGIParm.VslCallId = m_grGIResult.VslCallId;
                    //        lorryGIParm.ShippingNoteNo = m_grGIResult.ShipgNoteNo;
                    //    }
                    //}
                    //else if (rbtnGIDO.Checked)
                    //{
                    //    if (m_doGIResult != null)
                    //    {
                    //        lorryGIParm.VslCallId = m_doGIResult.VslCallId;
                    //        lorryGIParm.DoNo = m_doGIResult.DoNo;
                    //    }
                    //}

                    //PartnerCodeListResult lorryGIRes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_LORRY), lorryGIParm);
                    //if (lorryGIRes != null)
                    //{   
                    //    txtGILorry.Text = lorryGIRes.LorryNo;
                    //}
                    //}

                    //}
                    #endregion commented by William (2015/07/21) Mantis issue: 49799

                    //added by William (2015/07/21 - HHT) Mantis issue 49799

                    //Commented by Chris 2015-09-17 for 49799
                    //if (txtGILorry.Text.Trim().Length < 3)
                    //{
                    //    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HPS101_0010"));
                    //    return;
                    //}

                    PartnerCodeListParm lorryGIParm = new PartnerCodeListParm();
                    lorryGIParm.Option = "CD";
                    //Commented By Chris 2015-10-20
                    //if (m_grGIResult != null)
                    //{
                    //    lorryGIParm.PtnrCd = m_grGIResult.Tsptr;
                    //}

                    lorryGIParm.SearchItem = txtGILorry.Text.Trim();

                    PartnerCodeListResult lorryGIRes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_LORRY, CommonUtility.PORT_SAFETY), lorryGIParm);
                    if (lorryGIRes != null && !string.IsNullOrEmpty(lorryGIRes.LorryNo))
                    {
                        txtGILorry.Text = lorryGIRes.LorryNo;
                        m_grGIResult = new GRListResult();
                        m_grGIResult.Tsptr = lorryGIRes.PtnrCD;
                        m_doGIResult = new DOListResult();
                        m_doGIResult.Tsptr = lorryGIRes.PtnrCD;
                    }


                    break;

                case "btnGIF4":
                    PartnerCodeListParm driverParm = new PartnerCodeListParm();
                    if (rbtnGIGR.Checked)
                    {
                        if (m_grGIResult != null)
                        {
                            driverParm.PtnrCd = m_grGIResult.Tsptr;
                        }
                    }
                    else if (rbtnGIDO.Checked)
                    {
                        if (m_doGIResult != null)
                        {
                            driverParm.PtnrCd = m_doGIResult.Tsptr;
                        }
                    }
                    if (!string.IsNullOrEmpty(txtGILorry.Text.Trim()))
                    {
                        driverParm.LorryNo = txtGILorry.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtGIDriver.Text.Trim()))
                    {
                        driverParm.SearchItem = txtGIDriver.Text.Trim();
                    }
                    PartnerCodeListResult driverResult = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_DRIVER), driverParm);
                    if (driverResult != null)
                    {
                        txtGIDriver.Text = driverResult.DriverNo;
                        txtGILicense.Text = driverResult.LicsNo;
                        txtGIExpDate.Text = driverResult.LicsExprYmd;

                        m_isValidGIDriver = true;
                    }
                    break;

                case "btnGOF2":

                    //added by William (2015/08/04 - HHT) Mantis issue 49799
                    if (string.IsNullOrEmpty(txtGOLorry.Text.Trim()))
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0055"));
                        return;
                    }

                    GRListParm grGOParm = new GRListParm();
                    grGOParm.GateInOut = OUT;
                    grGOParm.GrNo = txtGOGR.Text.Trim();
                    grGOParm.LorryNo = txtGOLorry.Text.Trim();

                    if (m_grGOResult != null)
                    {
                        grGOParm.Jpvc = m_grGOResult.VslCallId;
                        grGOParm.ShipgNoteNo = m_grGOResult.ShipgNoteNo;
                    }

                    //added by William (2015/08/04 - HHT) Mantis issue 49799 ==> added mode GI/GO to as new construtor
                    GRListResult grGOResult = (GRListResult)PopupManager.instance.ShowPopup(new HCM117("GO"), grGOParm);
                    if (grGOResult != null)
                    {
                        m_grGOResult = grGOResult;
                        txtGOGR.Text = m_grGOResult.GrNo;
                        txtGOCompany.Text = m_grGOResult.Tsptr;

                        //commented by William (2015/08/04 - HHT) Mantis issue 49799
                        //txtGOLorry.Text = m_grGOResult.Lorry;

                        txtGOCmdt.Text = m_grGOResult.CmdtCd;
                        txtGOMt.Text = m_grGOResult.Mt;
                        txtGOQty.Text = m_grGOResult.Qty;

                        //Added by Chris 2015-11-19
                        if (!string.IsNullOrEmpty(m_grGOResult.DriverId))
                        {
                            txtGODriver.Text = m_grGOResult.DriverId;
                        }
                        if (!string.IsNullOrEmpty(m_grGOResult.Lic))
                        {
                            txtGOLicense.Text = m_grGOResult.Lic;
                        }
                        if (!string.IsNullOrEmpty(m_grGOResult.Expire))
                        {
                            txtGOExpDate.Text = m_grGOResult.Expire;
                        }

                        m_isValidGOGR = true;
                    }
                    break;

                case "btnGOF1":

                    //added by William (2015/08/04 - HHT) Mantis issue 49799


                    if (string.IsNullOrEmpty(txtGOLorry.Text.Trim()))
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0055"));
                        return;
                    }

                    //commented by William (2015/07/21 - HHT) Mantis issue 49799
                    //gpParm.GatePass = txtGOGP.Text;
                    //gpParm.WordDate = this.txtGOTime.Text;
                    //gpParm.ShiftId = CommonUtility.GetComboboxSelectedValue(this.cboShftGO);
                    //gpParm.DriverId = txtGODriver.Text.Trim();

                    //added by William (2015/07/21 - HHT) Mantis issue 49799
                    GatePassListParm gpParm = new GatePassListParm();
                    gpParm.LorryNo = txtGOLorry.Text.Trim();
                    gpParm.GatePass = txtGOGP.Text;

                    GatePassListResult gpResult = (GatePassListResult)PopupManager.instance.ShowPopup(new HCM111(HCM111.TYPE_GATEPASS), gpParm);
                    if (gpResult != null)
                    {
                        txtGOGP.Text = gpResult.GatePass;

                        //commented by William (2015/07/21 - HHT) Mantis issue 49799
                        //m_gpGOResult = gpResult;
                        //txtGOLorry.Text = gpResult.LorryNo;
                        //m_isValidGOGP = true;
                        txtGOCmdt.Text = gpResult.CmdtCd;
                        txtGOMt.Text = gpResult.Wgt;
                        txtGOQty.Text = gpResult.PkgQty;
                        txtGOCompany.Text = gpResult.Tsptr;
                    }
                    break;

                case "btnGOF3":
                    #region commented by William (2015/07/27 - HHT) Mantis issue 49799
                    //if (rbtnGOGR.Checked || rbtnGOGP.Checked)
                    //{
                    //    if (ValidateEmptyCargoNo(HPS101.TAB_GO))
                    //    {
                    //        PartnerCodeListParm lorryGOParm = new PartnerCodeListParm();
                    //        lorryGOParm.Option = "CD";
                    //        if (m_grGOResult != null)
                    //        {
                    //            lorryGOParm.PtnrCd = m_grGOResult.Tsptr;
                    //        }


                    //        if (rbtnGOGR.Checked)
                    //        {
                    //            if (m_grGOResult != null)
                    //            {
                    //                lorryGOParm.VslCallId = m_grGOResult.VslCallId;
                    //                lorryGOParm.ShippingNoteNo = m_grGOResult.ShipgNoteNo;
                    //            }
                    //        }
                    //        else if (rbtnGOGP.Checked)
                    //        {
                    //            if (m_grGOResult != null)
                    //            {
                    //                lorryGOParm.VslCallId = m_gpGOResult.VslCallId;
                    //                //lorryGOParm.DoNo = m_gpGOResult.;
                    //            }
                    //        }


                    //        if (!string.IsNullOrEmpty(this.txtGOLorry.Text))
                    //        {
                    //            lorryGOParm.SearchItem = this.txtGOLorry.Text;
                    //        }

                    //        PartnerCodeListResult lorryGORes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_LORRY), lorryGOParm);
                    //        if (lorryGORes != null)
                    //        {
                    //            txtGOLorry.Text = lorryGORes.LorryNo;
                    //        }
                    //    }
                    //}
                    #endregion commented by William (2015/07/27 - HHT) Mantis issue 49799

                    //added by William (2015/07/27 - HHT) Mantis issue 49799


                    //Commented by Chris 2015-09-17 for 49799
                    //if (txtGOLorry.Text.Trim().Length < 3)
                    //{
                    //    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HPS101_0010"));
                    //    return;
                    //}
                    PartnerCodeListParm lorryGOParm = new PartnerCodeListParm();
                    lorryGOParm.Option = "CD";

                    //Commented by Chris 2015-10-22
                    //if (m_grGOResult != null)
                    //{
                    //    lorryGOParm.PtnrCd = m_grGOResult.Tsptr;

                    //}

                    lorryGOParm.SearchItem = txtGOLorry.Text.Trim();

                    PartnerCodeListResult lorryGORes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_LORRY, CommonUtility.PORT_SAFETY), lorryGOParm);
                    if (lorryGORes != null)
                    {
                        txtGOLorry.Text = lorryGORes.LorryNo;
                        //Added by Chris 2015-10-22
                        m_grGOResult = new GRListResult();
                        m_grGOResult.Tsptr = lorryGORes.PtnrCD;
                        m_gpGOResult = new GatePassListResult();
                        m_grGOResult.Tsptr = lorryGORes.PtnrCD;
                    }
                    //Added By Chris 2015-10-01
                    getGOInfo();
                    break;

                case "btnGOF4":
                    PartnerCodeListParm driverGOParm = new PartnerCodeListParm();
                    if (rbtnGOGR.Checked)
                    {
                        if (m_grGOResult != null)
                        {
                            driverGOParm.PtnrCd = m_grGOResult.Tsptr;
                        }
                    }
                    else if (rbtnGOGP.Checked)
                    {
                        if (m_gpGOResult != null)
                        {
                            driverGOParm.PtnrCd = m_gpGOResult.Tsptr;
                        }
                    }
                    PartnerCodeListResult driverGOResult = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_DRIVER), driverGOParm);
                    if (driverGOResult != null)
                    {
                        txtGODriver.Text = driverGOResult.DriverNo;
                        txtGOLicense.Text = driverGOResult.LicsNo;
                        txtGOExpDate.Text = driverGOResult.LicsExprYmd;

                        m_isValidGODriver = true;
                    }
                    break;

                case "btnGIList":
                    HPS102Parm listGIParm = new HPS102Parm();
                    if (rbtnGIGR.Checked && m_grGIResult != null)
                    {
                        listGIParm.VslCallId = m_grGIResult.VslCallId;
                        listGIParm.GrNo = m_grGIResult.GrNo;
                    }
                    else if (rbtnGIDO.Checked && m_doGIResult != null)
                    {
                        listGIParm.VslCallId = m_doGIResult.VslCallId;
                        listGIParm.DelvOdrNo = m_doGIResult.DoNo;
                        listGIParm.BlNo = m_doGIResult.Bl;
                    }
                    PopupManager.instance.ShowPopup(new HPS102(), listGIParm);
                    break;

                case "btnGOList":
                    HPS103Parm listGOParm = new HPS103Parm();
                    if (rbtnGOGR.Checked && m_grGOResult != null)
                    {
                        listGOParm.VslCallId = m_grGOResult.VslCallId;
                    }

                    PopupManager.instance.ShowPopup(new HPS103(), listGOParm);
                    break;

                case "btnGOLorryList":
                case "btnGILorryList":
                    PopupManager.instance.ShowPopup(new HPS104(), null);
                    break;
            }
        }

        private void ClearCtrlValues(int tab)
        {
            if (tab == HPS101.TAB_GI)
            {
                // Gate-In
                txtGIGRNo.Text = string.Empty;
                txtGIDONo.Text = string.Empty;
                txtGILorry.Text = string.Empty;
                txtGIDriver.Text = string.Empty;
                txtGILicense.Text = string.Empty;
                txtGIExpDate.Text = string.Empty;
                txtGICompany.Text = string.Empty;
                txtGIDelvMode.Text = string.Empty;
                //cboGIGate.SelectedIndex = -1;         //commented by Phuong Do, 23/05/2011, Mantis ID 0029124
                cboGIDGChk.SelectedIndex = -1;
                cboGIDGStatus.SelectedIndex = -1;
                txtGITime.Value = DateTime.Now;
                rbtnGIGR.Checked = true;
                m_grGIResult = null;
                m_doGIResult = null;
                m_isValidGIGR = false;
                m_isValidGIDO = false;
                m_isValidGIDriver = false;

                //Added By Chris 2015-10-15
                txtGICmdt.Text = string.Empty;
                txtGIMt.Text = string.Empty;
                txtGIQty.Text = string.Empty;
            }
            else if (tab == HPS101.TAB_GO)
            {
                // Gate-Out
                //cboGOGate.SelectedIndex = -1;         //commented by Phuong Do, 23/05/2011, Mantis ID 0029124
                txtGOGR.Text = string.Empty;
                txtGOLorry.Text = string.Empty;
                txtGOGP.Text = string.Empty;
                txtGODriver.Text = string.Empty;
                txtGOLicense.Text = string.Empty;
                txtGOExpDate.Text = string.Empty;
                txtGOCompany.Text = string.Empty;
                txtGOTime.Value = DateTime.Now;
                m_gpGOResult = null;
                m_grGOResult = null;
                m_isValidGOGR = false;
                m_isValidGOGP = false;
                m_isValidGODriver = false;

                //Added by Chris 2015-10-16
                txtGOCmdt.Text = string.Empty;
                txtGOQty.Text = string.Empty;
                txtGOMt.Text = string.Empty;
                txtGOTime.Value = DateTime.Now;
            }
        }

        private bool IsExpiredLicense(int tab)
        {
            DateTime expDt;
            DateTime curDt;
            string strCurDt = CommonUtility.GetCurrentServerTime();

            // Gate-In
            if (tab == HPS101.TAB_GI)
            {
                if (!string.IsNullOrEmpty(txtGIExpDate.Text))
                {
                    expDt = CommonUtility.ParseStringToDate(txtGIExpDate.Text, "dd/MM/yyyy");
                    curDt = CommonUtility.ParseStringToDate(strCurDt, "dd/MM/yyyy HH:mm");
                    if (expDt.CompareTo(curDt) < 0)
                    {
                        return true;
                    }
                }
            }
            else if (tab == HPS101.TAB_GO)
            {
                if (!string.IsNullOrEmpty(txtGOExpDate.Text))
                {
                    expDt = CommonUtility.ParseStringToDate(txtGOExpDate.Text, "dd/MM/yyyy");
                    curDt = CommonUtility.ParseStringToDate(strCurDt, "dd/MM/yyyy HH:mm");
                    if (expDt.CompareTo(curDt) < 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool ValidateCargoNo(int tab)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (tab == HPS101.TAB_GI)
                {
                    if (rbtnGIGR.Checked)
                    {
                        if (string.IsNullOrEmpty(txtGIGRNo.Text))
                        {
                            m_isValidGIGR = false;
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0050"));
                            txtGIGRNo.SelectAll();
                            txtGIGRNo.Focus();
                            return false;
                        }
                        if (m_isValidGIGR == false && CommonUtility.IsValidGR(txtGIGRNo.Text, ref m_grGIResult) == false)
                        {
                            m_isValidGIGR = false;
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0029"));
                            txtGIGRNo.SelectAll();
                            txtGIGRNo.Focus();
                            return false;
                        }
                        else
                        {
                            m_isValidGIGR = true;
                            return true;
                        }
                    }
                    else if (rbtnGIDO.Checked)
                    {
                        if (string.IsNullOrEmpty(txtGIDONo.Text))
                        {
                            m_isValidGIDO = false;
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0053"));
                            txtGIDONo.SelectAll();
                            txtGIDONo.Focus();
                            return false;
                        }
                        if (m_isValidGIDO == false && CommonUtility.IsValidDO(txtGIDONo.Text, ref m_doGIResult) == false)
                        {
                            m_isValidGIDO = false;
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0031"));
                            txtGIDONo.SelectAll();
                            txtGIDONo.Focus();
                            return false;
                        }
                        else
                        {
                            m_isValidGIDO = true;
                            return true;
                        }
                    }
                }
                else if (tab == HPS101.TAB_GO)
                {
                    if (rbtnGOGR.Checked)
                    {
                        if (string.IsNullOrEmpty(txtGOGR.Text))
                        {
                            m_isValidGOGR = false;
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0050"));
                            txtGOGR.SelectAll();
                            txtGOGR.Focus();
                            return false;
                        }
                        if (m_isValidGOGR == false && CommonUtility.IsValidGR(txtGOGR.Text, ref m_grGOResult) == false)
                        {
                            m_isValidGOGR = false;
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0029"));
                            txtGOGR.SelectAll();
                            txtGOGR.Focus();
                            return false;
                        }
                        else
                        {
                            m_isValidGOGR = true;
                            return true;
                        }
                    }
                    else if (rbtnGOGP.Checked)
                    {
                        if (string.IsNullOrEmpty(txtGOGP.Text))
                        {
                            m_isValidGOGP = false;
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0054"));
                            txtGOGP.SelectAll();
                            txtGOGP.Focus();
                            return false;
                        }
                        if (m_isValidGOGP == false && CommonUtility.IsValidGP(txtGOGP.Text, ref m_gpGOResult) == false)
                        {
                            m_isValidGOGP = false;
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0033"));
                            txtGOGP.SelectAll();
                            txtGOGP.Focus();
                            return false;
                        }
                        else
                        {
                            m_isValidGOGP = true;
                            return true;
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

        private bool ValidateDGStatus(int tab)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (tab == HPS101.TAB_GI)
                {
                    string dgYn = CommonUtility.GetComboboxSelectedValue(cboGIDGChk);
                    string dgStatus = CommonUtility.GetComboboxSelectedValue(cboGIDGStatus);
                    if (YES.Equals(dgYn) && !DGSTAT_CONFIRMED_CD.Equals(dgStatus))
                    {
                        // This DG cargo has not permitted yet. Do you still want to confirm Gate-In?
                        DialogResult dr = CommonUtility.ConfirmMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HPS101_0002"));
                        if (dr == DialogResult.Yes)
                        {
                            return true;
                        }
                        else
                        {
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

        private bool ValidateEmptyCargoNo(int tab)
        {
            if (tab == HPS101.TAB_GI)
            {
                if (rbtnGIGR.Checked)
                {
                    if (string.IsNullOrEmpty(txtGIGRNo.Text.Trim()))
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0050"));
                        txtGIGRNo.SelectAll();
                        txtGIGRNo.Focus();
                        return false;
                    }
                }
                else if (rbtnGIDO.Checked)
                {
                    // As BL/DO cannot be inputted (just select from grid), no need to validate BL/DO.
                    if (string.IsNullOrEmpty(txtGIDONo.Text.Trim()))
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0052"));
                        btnGIF2.Focus();
                        return false;
                    }
                }
            }
            else if (tab == HPS101.TAB_GO)
            {
                if (rbtnGOGR.Checked)
                {
                    if (string.IsNullOrEmpty(txtGOGR.Text.Trim()))
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0050"));
                        txtGOGR.SelectAll();
                        txtGOGR.Focus();
                        return false;
                    }
                }
                else if (rbtnGOGP.Checked)
                {
                    if (string.IsNullOrEmpty(txtGOGP.Text.Trim()))
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0054"));
                        txtGOGP.SelectAll();
                        txtGOGP.Focus();
                        return false;
                    }
                }

            }

            return true;
        }

        private bool GetArrvDelvIsCheck(int tab)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string cgNo = string.Empty;
                string lorryNo = string.Empty;
                string cgInOutCd = string.Empty;
                string gatePassNo = string.Empty;
                string vslCallId = string.Empty;

                ResponseInfo info = null;

                IPortSafetyProxy proxy = new PortSafetyProxy();
                CargoArrvDelvParm parm = new CargoArrvDelvParm();

                if (tab == HPS101.TAB_GI)
                {
                    if (rbtnGIGR.Checked)
                    {
                        cgNo = txtGIGRNo.Text;
                        lorryNo = txtGILorry.Text;
                        cgInOutCd = "I";
                        gatePassNo = string.Empty;

                        //Added by Chris 2015/16/11
                        if (!string.IsNullOrEmpty(m_grGIResult.VslCallId))
                        {
                            vslCallId = m_grGIResult.VslCallId;
                        }

                    }
                    parm.cgNo = cgNo;
                    parm.lorryNo = lorryNo;
                    parm.cgInOutCd = cgInOutCd;
                    parm.searchType = CHECK;
                    parm.vslCallId = vslCallId;

                    info = proxy.getArrvDelvIsCheck(parm);
                    //else if (rbtnGIDO.Checked)
                    //{
                    //    cgNo = m_doGIResult.Bl;
                    //    lorryNo = txtGILorry.Text;
                    //    cgInOutCd = "O";
                    //    gatePassNo = string.Empty;

                    //    //Added by Chris 2015/16/11
                    //    if (!string.IsNullOrEmpty(m_doGIResult.VslCallId))
                    //    {
                    //        vslCallId = m_doGIResult.VslCallId;
                    //    }
                    //}
                }
                else if (tab == HPS101.TAB_GO)
                {
                    //if (rbtnGOGR.Checked)
                    //{
                    //    cgNo = txtGOGR.Text;
                    //    lorryNo = txtGOLorry.Text;
                    //    cgInOutCd = "O";
                    //    gatePassNo = string.Empty;
                    //}
                    //else 
                    if (rbtnGOGP.Checked)
                    {
                        if (m_gpGOResult != null)
                        {
                            if (!string.IsNullOrEmpty(m_gpGOResult.CgNo))
                            {
                                cgNo = m_gpGOResult.CgNo;
                            }
                        }

                        lorryNo = txtGOLorry.Text;
                        cgInOutCd = "O";
                        gatePassNo = txtGOGP.Text;
                    }
                    parm.cgNo = cgNo;
                    parm.gatePassNo = gatePassNo;
                    parm.cgInOutCd = cgInOutCd;
                    parm.searchType = CHECK;
                    parm.vslCallId = vslCallId;
                    info = proxy.checkGateOut(parm);
                }


                //parm.cgNo = cgNo;
                //parm.lorryNo = lorryNo;
                //parm.cgInOutCd = cgInOutCd;
                //parm.gatePassNo = gatePassNo;
                //parm.searchType = CHECK;
                //parm.vslCallId = vslCallId;
                //ResponseInfo info = proxy.getArrvDelvIsCheck(parm);

                if (info != null && info.list != null && info.list.Length > 0 && info.list[0] is CargoArrvDelvItem)
                {
                    CargoArrvDelvItem item = (CargoArrvDelvItem)info.list[0];
                    if ("1".Equals(item.validCheck))
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

        private bool Validate(int tab)
        {
            if (!ValidateCargoNo(tab))
            {
                return false;
            }

            // This validation must be the last validation of this function.
            bool isChecked = GetArrvDelvIsCheck(tab);
            if (!isChecked)
            {
                DialogResult dr = CommonUtility.ConfirmMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HPS101_0001"));
                if (dr == DialogResult.Yes)
                {
                    // Validate DG Status
                    if (!ValidateDGStatus(tab))
                    {
                        return false;
                    }
                    else
                        return true;
                }
                else
                {
                    return false;
                }
            }

            // Validate DG Status
            if (!ValidateDGStatus(tab))
            {
                return false;
            }


            return true;
        }

        private bool ProcessCargoArrvDelvItem(int tab)
        {
            bool result = false;
            try
            {
                System.Collections.ArrayList arrObj = new System.Collections.ArrayList();
                CargoArrvDelvItem item;

                if (tab == HPS101.TAB_GI)
                {
                    // Gate-In
                    if (rbtnGIGR.Checked && m_grGIResult != null && (!string.IsNullOrEmpty(txtGIGRNo.Text)))
                    {
                        item = new CargoArrvDelvItem();
                        item.lorryNo = txtGILorry.Text;
                        item.gateInDt = txtGITime.Text;
                        item.tsptCompNm = txtGICompany.Text;
                        item.CRUD = Constants.WS_INSERT;
                        item.userId = UserInfo.getInstance().UserId;
                        item.delvTpCd = m_grGIResult.DelvTpCd;
                        item.shipgNoteNo = m_grGIResult.ShipgNoteNo;
                        item.delvTpNm = txtGIDelvMode.Text;
                        item.dgYn = CommonUtility.GetComboboxSelectedValue(cboGIDGChk);
                        item.dgStatCd = CommonUtility.GetComboboxSelectedValue(cboGIDGStatus);
                        item.gateCd = CommonUtility.GetComboboxSelectedValue(cboGIGate);
                        ///aaaaaaaaaaaaaa
                        item.msrmt = Double.Parse(txtGIMt.Text);
                        item.pkgQty = Int16.Parse(txtGIQty.Text);
                        item.cmdtCd = txtGICmdt.Text;
                        item.driverId = txtGIDriver.Text;
                        item.gateInDt = txtGITime.Value.ToString("dd/MM/yyyy HH:mm");

                        item.grNo = txtGIGRNo.Text;
                        item.tsptr = m_grGIResult.Tsptr;
                        item.vslCallId = m_grGIResult.VslCallId;
                        item.cgNo = txtGIGRNo.Text;
                        item.cgInOutCd = IN;
                        //Added By Chris 2015-10-16
                        item.wgt = Double.Parse(txtGIMt.Text);

                        arrObj.Add(item);
                    }
                    else if (rbtnGIDO.Checked && m_doGIResult != null && (!string.IsNullOrEmpty(txtGIDONo.Text)))
                    {
                        item = new CargoArrvDelvItem();
                        item.lorryNo = txtGILorry.Text;
                        item.gateInDt = txtGITime.Text;
                        item.tsptCompNm = txtGICompany.Text;
                        item.CRUD = Constants.WS_INSERT;
                        item.userId = UserInfo.getInstance().UserId;
                        item.delvTpNm = txtGIDelvMode.Text;
                        item.dgYn = CommonUtility.GetComboboxSelectedValue(cboGIDGChk);
                        item.dgStatCd = CommonUtility.GetComboboxSelectedValue(cboGIDGStatus);
                        item.gateCd = CommonUtility.GetComboboxSelectedValue(cboGIGate);

                        if (!string.IsNullOrEmpty(txtGIMt.Text))
                        {
                            item.msrmt = Double.Parse(txtGIMt.Text);
                        }
                        if (!string.IsNullOrEmpty(txtGIQty.Text))
                        {
                            item.pkgQty = Int16.Parse(txtGIQty.Text);
                        }
                        if (!string.IsNullOrEmpty(txtGICmdt.Text))
                        {
                            item.cmdtCd = txtGICmdt.Text;
                        }
                        if (!string.IsNullOrEmpty(txtGIDriver.Text))
                        {
                            item.driverId = txtGIDriver.Text;
                        }
                        if (!string.IsNullOrEmpty(txtGIMt.Text))
                        {
                            item.wgt = Double.Parse(txtGIMt.Text);
                        }
                        item.gateInDt = txtGITime.Value.ToString("dd/MM/yyyy HH:mm");

                        item.doNo = m_doGIResult.DoNo;
                        item.tsptr = m_doGIResult.Tsptr;
                        item.vslCallId = m_doGIResult.VslCallId;
                        item.cgNo = m_doGIResult.Bl;
                        item.cgInOutCd = OUT;
                        if (!string.IsNullOrEmpty(txtGIDriver.Text))
                        {
                            item.driverId = txtGIDriver.Text;
                        }

                        arrObj.Add(item);
                    }
                }
                else if (tab == HPS101.TAB_GO)
                {
                    // Gate-Out
                    if (rbtnGOGP.Checked && (!string.IsNullOrEmpty(txtGOGP.Text)) && m_gpGOResult != null)
                    {
                        item = new CargoArrvDelvItem();
                        item.lorryNo = txtGOLorry.Text;
                        item.gateOutDt = txtGOTime.Text.Trim();
                        item.tsptr = txtGOCompany.Text;
                        item.gatePassNo = txtGOGP.Text;
                        item.vslCallId = m_gpGOResult.VslCallId;
                        item.cgNo = m_gpGOResult.CgNo;
                        item.pkgQty = CommonUtility.ParseInt(m_gpGOResult.PkgQty);
                        item.msrmt = CommonUtility.ParseDouble(m_gpGOResult.Mrsmt);
                        item.wgt = CommonUtility.ParseDouble(m_gpGOResult.Wgt);
                        item.gateCd = CommonUtility.GetComboboxSelectedValue(cboGOGate);

                        //item.cgInOutCd = OUT;
                        item.cgInOutCd = m_gpGOResult.CgInOutCd;

                        item.seq = m_gpGOResult.Seq;
                        item.searchType = GATE_OUT;
                        item.CRUD = Constants.WS_INSERT;
                        item.userId = UserInfo.getInstance().UserId;
                        item.catgCd = m_gpGOResult.Catgcd;
                        item.licsExprYmd = txtGOExpDate.Text.Trim();
                        arrObj.Add(item);
                    }
                    else if (rbtnGOGR.Checked && (!string.IsNullOrEmpty(txtGOGR.Text)) && m_grGOResult != null)
                    {
                        item = new CargoArrvDelvItem();
                        item.lorryNo = txtGOLorry.Text;
                        item.gateOutDt = txtGOTime.Text.Trim();
                        item.tsptr = m_grGOResult.Tsptr;
                        item.vslCallId = m_grGOResult.VslCallId;
                        item.cgNo = txtGOGR.Text;
                        item.pkgQty = CommonUtility.ParseInt(m_grGOResult.Qty);
                        item.msrmt = CommonUtility.ParseDouble(m_grGOResult.M3);
                        item.wgt = CommonUtility.ParseDouble(m_grGOResult.Mt);
                        item.gateCd = CommonUtility.GetComboboxSelectedValue(cboGOGate);

                        //item.cgInOutCd = OUT;
                        item.cgInOutCd = m_grGOResult.CgInOutCd;

                        item.searchType = GATE_OUT;
                        item.CRUD = Constants.WS_INSERT;
                        item.userId = UserInfo.getInstance().UserId;
                        item.grNo = txtGOGR.Text;
                        item.licsExprYmd = txtGOExpDate.Text.Trim();
                        if (!string.IsNullOrEmpty(m_grGOResult.Seq))
                        {
                            item.seq = m_grGOResult.Seq;
                        }
                        arrObj.Add(item);
                    }
                }

                if (arrObj.Count > 0)
                {
                    Object[] obj = arrObj.ToArray();
                    IPortSafetyProxy proxy = new PortSafetyProxy();
                    DataItemCollection dataCollection = new DataItemCollection();
                    dataCollection.collection = obj;

                    proxy.processCargoArrvDelvItem(dataCollection);
                    result = true;
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
                case "rbtnGIGR":
                    rbnGIGRChecked();
                    break;
                case "rbtnGIDO":
                    rbnGIDOChecked();
                    break;

                case "rbtnGOGR":
                    this.rbtnGOGRCheck();
                    break;

                case "rbtnGOGP":
                    this.rbtnGOGPCheck();
                    break;
            }
        }

        private void rbnGIDOChecked()
        {
            //added by William (2015/07/22 - HHT) Mantis issue 49799
            //txtGILorry.Text = string.Empty;
            //txtGIDriver.Text = string.Empty;
            //txtGILicense.Text = string.Empty;
            //txtGIExpDate.Text = string.Empty;
            txtGIGRNo.Text = string.Empty;

            txtGICompany.Text = string.Empty;
            txtGIDelvMode.Text = string.Empty;
            cboGIDGChk.SelectedIndex = -1;
            cboGIDGStatus.SelectedIndex = -1;
            txtGICmdt.Text = string.Empty;
            txtGIMt.Text = string.Empty;
            txtGIQty.Text = string.Empty;
        }

        private void rbnGIGRChecked()
        {
            txtGIDONo.Text = string.Empty;

            txtGICompany.Text = string.Empty;
            txtGIDelvMode.Text = string.Empty;
            cboGIDGChk.SelectedIndex = -1;
            cboGIDGStatus.SelectedIndex = -1;
            txtGICmdt.Text = string.Empty;
            txtGIMt.Text = string.Empty;
            txtGIQty.Text = string.Empty;
        }

        private void rbtnGOGPCheck()
        {
            //Added by Chris 2015-12-1
            if (isFirstTime)
            {
                getGOInfo();
                isFirstTime = false;
            }
            else
            {
                this.txtGOGR.Enabled = false;
                this.txtGOGP.Enabled = true;

                //added by Chris (2015/10/13 - HHT) Mantis issue 49799
                //this.txtGOLorry.Text = string.Empty;
                //this.txtGODriver.Text = string.Empty;
                //this.txtGOLicense.Text = string.Empty;
                //this.txtGOExpDate.Text = string.Empty;

                this.txtGOCompany.Text = string.Empty;
                this.txtGOCmdt.Text = string.Empty;
                this.txtGOMt.Text = string.Empty;
                this.txtGOQty.Text = string.Empty;
                this.m_grGOResult = (GRListResult)null;
                this.txtGOGR.Text = string.Empty;
                
            }
                
            
                //this.txtGOGR.Enabled = false;
                //this.txtGOGP.Enabled = true;

                ////added by Chris (2015/10/13 - HHT) Mantis issue 49799
                ////this.txtGOLorry.Text = string.Empty;
                ////this.txtGODriver.Text = string.Empty;
                ////this.txtGOLicense.Text = string.Empty;
                ////this.txtGOExpDate.Text = string.Empty;

                //this.txtGOCompany.Text = string.Empty;
                //this.txtGOCmdt.Text = string.Empty;
                //this.txtGOMt.Text = string.Empty;
                //this.txtGOQty.Text = string.Empty;
                //this.m_grGOResult = (GRListResult)null;
                //this.txtGOGR.Text = string.Empty;
   
        }

        private void rbtnGOGRCheck()
        {
            //Added by Chris 2015-12-1
            if (isFirstTime)
            {
                getGOInfo();
                isFirstTime = false;
            }
            else
            {
                this.txtGOGR.Enabled = true;
                this.txtGOGP.Enabled = false;

                //added by William (2015/07/22 - HHT) Mantis issue 49799
                //this.txtGOLorry.Text = string.Empty;
                //this.txtGODriver.Text = string.Empty;
                //this.txtGOLicense.Text = string.Empty;
                //this.txtGOExpDate.Text = string.Empty;

                this.txtGOGP.Text = string.Empty;
                this.txtGOCompany.Text = string.Empty;
                this.txtGOCmdt.Text = string.Empty;
                this.txtGOMt.Text = string.Empty;
                this.txtGOQty.Text = string.Empty;

                this.m_grGOResult = (GRListResult)null;
                
            }
            
            //this.txtGOGR.Enabled = true;
            //this.txtGOGP.Enabled = false;

            ////added by William (2015/07/22 - HHT) Mantis issue 49799
            ////this.txtGOLorry.Text = string.Empty;
            ////this.txtGODriver.Text = string.Empty;
            ////this.txtGOLicense.Text = string.Empty;
            ////this.txtGOExpDate.Text = string.Empty;

            //this.txtGOGP.Text = string.Empty;
            //this.txtGOCompany.Text = string.Empty;
            //this.txtGOCmdt.Text = string.Empty;
            //this.txtGOMt.Text = string.Empty;
            //this.txtGOQty.Text = string.Empty;

            //this.m_grGOResult = (GRListResult)null;   
        }

        public void receiveNotify(NoticeMessage message)
        {
            this.Close();
        }

        private void TxtLostFocusListener(object sender, EventArgs e)
        {
            string ptnrCd;

            TextBox mytextbox = (TextBox)sender;
            String txtName = mytextbox.Name;
            switch (txtName)
            {
                case "txtGIGRNo":

                    //Commented by Chris 2015-09-29 for Gate in - out
                    //if (!m_isValidGIGR && !string.IsNullOrEmpty(txtGIGRNo.Text))
                    //{
                    //    if (!CommonUtility.IsValidGR2(txtGIGRNo.Text, txtGILorry.Text, ref m_grGIResult))
                    //    {
                    //        //CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0029"));
                    //        m_isValidGIGR = false;
                    //        btnGIF1.Focus();
                    //    }
                    //    else
                    //    {
                    //        txtGIGRNo.Text = m_grGIResult.GrNo;
                    //        txtGICompany.Text = m_grGIResult.Tsptr;

                    //        //Added by Chris 2015-09-24 for 49779
                    //        txtGICmdt.Text = m_grGIResult.CmdtCd;
                    //        txtGIMt.Text = m_grGIResult.Mt;
                    //        txtGIQty.Text = m_grGIResult.Qty;
                    //        //commented by William (2015/08/04 - HHT) Mantis issue 49799
                    //        //txtGILorry.Text = m_grGIResult.Lorry;
                    //        txtGIDelvMode.Text = m_grGIResult.DelvTpNm;
                    //        CommonUtility.SetComboboxSelectedItem(cboGIDGChk, m_grGIResult.DgYn);
                    //        CommonUtility.SetComboboxSelectedItem(cboGIDGStatus, m_grGIResult.DgStatCd);

                    //        m_isValidGIGR = true;
                    //    }
                    //}

                    //Added by Chris 2015-09-29 for Gate in - out
                    CheckValidGIGR();
                    break;

                case "txtGIDONo":
                    getGiDoNoInfo();
                    break;

                case "txtGIDriver":
                    if (!m_isValidGIDriver && !string.IsNullOrEmpty(txtGIDriver.Text))
                    {
                        PartnerCodeListResult driverGIResult = null;
                        ptnrCd = string.Empty;
                        if (rbtnGIGR.Checked)
                        {
                            if (m_grGIResult != null)
                            {
                                ptnrCd = m_grGIResult.Tsptr;
                            }
                        }
                        else if (rbtnGIDO.Checked)
                        {
                            if (m_doGIResult != null)
                            {
                                ptnrCd = m_doGIResult.Tsptr;
                            }
                        }

                        if (!CommonUtility.IsValidDriver(txtGIDriver.Text, ptnrCd, ref driverGIResult))
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0034"));
                            m_isValidGIDriver = false;
                            btnGIF4.Focus();
                        }
                        else
                        {
                            txtGILicense.Text = driverGIResult.LicsNo;
                            txtGIExpDate.Text = driverGIResult.LicsExprYmd;

                            m_isValidGIDriver = true;
                        }
                    }
                    break;

                case "txtGOGR":
                    if (!m_isValidGOGR && !string.IsNullOrEmpty(txtGOGR.Text))
                    {
                        if (!CommonUtility.IsValidGR(txtGOGR.Text, ref m_grGOResult))
                        {
                            //CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0029"));
                            m_isValidGOGR = false;
                            btnGOF2.Focus();
                        }
                        else
                        {
                            txtGOGR.Text = m_grGOResult.GrNo;
                            txtGOCompany.Text = m_grGOResult.Tsptr;

                            //commented by William (2015/08/04 - HHT) Mantis issue 49799
                            //txtGOLorry.Text = m_grGOResult.Lorry;

                            m_isValidGOGR = true;
                        }
                    }
                    break;

                case "txtGOGP":
                    if (!m_isValidGOGP && !string.IsNullOrEmpty(txtGOGP.Text))
                    {
                        if (!CommonUtility.IsValidGP(txtGOGP.Text, ref m_gpGOResult))
                        {
                            //CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0033"));
                            m_isValidGOGP = false;
                            btnGOF1.Focus();
                        }
                        else
                        {
                            txtGOGP.Text = m_gpGOResult.GatePass;
                            //txtGOCompany.Text = m_gpGOResult.Tsptr;

                            //commented by William (2015/08/04 - HHT) Mantis issue 49799
                            //txtGOLorry.Text = m_gpGOResult.LorryNo;

                            m_isValidGOGP = true;
                        }
                    }
                    break;

                case "txtGODriver":
                    if (!m_isValidGODriver && !string.IsNullOrEmpty(txtGODriver.Text))
                    {
                        PartnerCodeListResult driverGOResult = null;
                        ptnrCd = string.Empty;
                        if (rbtnGOGR.Checked)
                        {
                            if (m_grGOResult != null)
                            {
                                ptnrCd = m_grGOResult.Tsptr;
                            }
                        }
                        if (!CommonUtility.IsValidDriver(txtGODriver.Text, ptnrCd, ref driverGOResult))
                        {
                            //CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0034"));
                            m_isValidGODriver = false;
                            btnGOF4.Focus();
                        }
                        else
                        {
                            txtGOLicense.Text = driverGOResult.LicsNo;
                            txtGOExpDate.Text = driverGOResult.LicsExprYmd;

                            m_isValidGODriver = true;
                        }
                    }
                    break;

                //added by William (2015/08/04 - HHT) Mantis issue 49799
                case "txtGOLorry":
                    getGOInfo();
                    break;
                //Add by Chris (2015/11/27 - HHT)
                case "txtGILorry":
                    if (!string.IsNullOrEmpty(txtGILorry.Text) && txtGILorry.Text.Length >= 5)
                    {
                        if (!IsValidGILorry(txtGILorry.Text))
                        {
                            //Invalid lorry number.
                            txtGILorry.Text = "";
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0032"));
                            //txtGILorry.Focus();
                            
                        }
                    }
                    break;
            }
        }

        private void getGiDoNoInfo()
        {
            if (!m_isValidGIDO && !string.IsNullOrEmpty(txtGIDONo.Text))
            {
                if (!CommonUtility.IsValidDO2(txtGIDONo.Text, txtGILorry.Text, ref m_doGIResult))
                {
                    //Invalid D/O number.
                    //CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0031"));
                    m_isValidGIDO = false;
                    btnGIF2.Focus();
                }
                else
                {

                    if (m_doGIResult != null)
                    {
                        txtGIDONo.Text = m_doGIResult.DoNo;
                        txtGICompany.Text = m_doGIResult.Tsptr;
                        txtGIDelvMode.Text = m_doGIResult.DelvTpNm;
                        CommonUtility.SetComboboxSelectedItem(cboGIDGChk, m_doGIResult.DgYn);
                        CommonUtility.SetComboboxSelectedItem(cboGIDGStatus, m_doGIResult.DgStatCd);

                        //Added by Chris 2015-09-24 for 49779
                        txtGICmdt.Text = m_doGIResult.CmdtCd;
                        txtGIMt.Text = m_doGIResult.Mt;
                        txtGIQty.Text = m_doGIResult.Qty;

                        m_isValidGIDO = true;
                    }
                }
            }
        }

        private bool IsValidGOLorryNo(string lorryNo)
        {
            try
            {
                Framework.Service.Provider.WebService.Provider.LorryListParm lorryParm = new Framework.Service.Provider.WebService.Provider.LorryListParm();
                lorryParm.LORRYNO = lorryNo;
                ICommonProxy proxy = new CommonProxy();
                ResponseInfo info = null;
                info = proxy.CheckValidLorry(lorryParm);
                if ((info != null) && (info.list != null) && (info.list.Length > 0))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void getGOInfo()
        {
            if (!string.IsNullOrEmpty(txtGOLorry.Text) && txtGOLorry.Text.Length >= 5)
            {
                if (IsValidGOLorryNo(txtGOLorry.Text))
                {
                    if (!IsValidGOLorry(txtGOLorry.Text, ref m_cargoGOResult))
                    {
                        m_isValidGOLorry = false;
                        //txtGOLorry.Focus();
                    }
                    else
                    {
                        txtGODriver.Text = m_cargoGOResult.DriverId;
                        CommonUtility.SetDTPValueDMYHM(tDateTimePicker1, m_cargoGOResult.GateInDt);
                        txtGOCompany.Text = m_cargoGOResult.Tsptr;

                        //if (rbtnGOGP.Checked == true)
                        //{
                        //    txtGOGP.Text = m_cargoGOResult.GatePassNo;
                        //    m_isValidGOGP = true;
                        //}
                        //if (rbtnGOGR.Checked == true)
                        //{
                        //    txtGOGR.Text = m_cargoGOResult.GrNo;
                        //    m_isValidGOGR = true;
                        //}                    

                        //Added by Chris 2015-10-14

                        if (!string.IsNullOrEmpty(m_cargoGOResult.CgInOutCd))
                        {
                            if (m_cargoGOResult.CgInOutCd.Equals("O"))
                            {
                                rbtnGOGP.Checked = true;
                                txtGOGP.Text = m_cargoGOResult.GatePassNo;
                                txtGOGR.Enabled = false;
                                txtGOGR.Text = "";
                                txtGOGP.Enabled = true;
                                m_isValidGOGP = true;
                                m_gpGOResult = new GatePassListResult();
                                m_gpGOResult.CgInOutCd = m_cargoGOResult.CgInOutCd;
                                if (!string.IsNullOrEmpty(m_cargoGOResult.CgNo))
                                {
                                    m_gpGOResult.CgNo = m_cargoGOResult.CgNo;
                                }
                                if (!string.IsNullOrEmpty(m_cargoGOResult.Seq))
                                {
                                    m_gpGOResult.Seq = m_cargoGOResult.Seq;
                                }
                                if (!string.IsNullOrEmpty(m_cargoGOResult.VslCallId))
                                {
                                    m_gpGOResult.VslCallId = m_cargoGOResult.VslCallId;
                                }
                                if (!string.IsNullOrEmpty(m_cargoGOResult.Tsptr))
                                {
                                    m_gpGOResult.Tsptr = m_cargoGOResult.Tsptr;
                                }

                            }
                            else if (m_cargoGOResult.CgInOutCd.Equals("I"))
                            {
                                m_grGOResult = new GRListResult();
                                rbtnGOGR.Checked = true;
                                txtGOGR.Enabled = true;
                                txtGOGP.Enabled = false;
                                txtGOGP.Text = "";
                                txtGOGR.Text = m_cargoGOResult.GrNo;
                                m_isValidGOGR = true;
                                m_grGOResult.CgInOutCd = m_cargoGOResult.CgInOutCd;
                                if (!string.IsNullOrEmpty(m_cargoGOResult.GrNo))
                                {
                                    m_grGOResult.GrNo = m_cargoGOResult.GrNo;
                                }
                                if (!string.IsNullOrEmpty(m_cargoGOResult.Seq))
                                {
                                    m_grGOResult.Seq = m_cargoGOResult.Seq;
                                }
                                //if (!string.IsNullOrEmpty(m_cargoGOResult.CgInOutCd))
                                //{
                                //    m_gpGOResult.CgInOutCd = m_cargoGOResult.CgInOutCd;
                                //}
                                if (!string.IsNullOrEmpty(m_cargoGOResult.VslCallId))
                                {
                                    m_grGOResult.VslCallId = m_cargoGOResult.VslCallId;
                                }
                                if (!string.IsNullOrEmpty(m_cargoGOResult.Tsptr))
                                {
                                    m_grGOResult.Tsptr = m_cargoGOResult.Tsptr;
                                }

                            }

                        }

                        txtGOCmdt.Text = m_cargoGOResult.CmdCd;
                        txtGOMt.Text = m_cargoGOResult.Wgt;
                        txtGOQty.Text = m_cargoGOResult.PkgQty;
                        if (!string.IsNullOrEmpty(m_cargoGOResult.LicsExprYmd))
                        {
                            txtGOExpDate.Text = DateTime.Parse(m_cargoGOResult.LicsExprYmd.Substring(0, 10)).ToString("dd/MM/yyyy");
                        }
                        txtGOLicense.Text = m_cargoGOResult.LicsNo;
                        m_isValidGOLorry = true;

                    }
                }
                else
                {
                    txtGOLorry.Text = "";
                    isFirstTime = true;
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0032"));
                    m_isValidGOLorry = false;
                    
                }
                
            }
        }

        private void TxtTextChangedListener(object sender, EventArgs e)
        {
            TextBox mytextbox = (TextBox)sender;
            String txtName = mytextbox.Name;
            switch (txtName)
            {
                case "txtGIGRNo":
                    m_isValidGIGR = false;
                    //rbnGIGRChecked();
                    break;

                case "txtGIDONo":
                    m_isValidGIDO = false;
                    //rbnGIDOChecked();
                    break;

                case "txtGIDriver":
                    m_isValidGIDriver = false;
                    break;

                case "txtGOGR":
                    m_isValidGOGR = false;
                    break;

                case "txtGOGP":
                    m_isValidGOGP = false;
                    break;

                case "txtGODriver":
                    m_isValidGODriver = false;
                    break;

                //added by William (2015/08/04 - HHT) Mantis issue 49799
                case "txtGOLorry":
                    m_isValidGOLorry = false;

                    //Added by Chris 2015-12-1
                    isFirstTime = true;
                    break;
                case "txtGILorry":
                    break;
            }
        }

        //added by William (2015/08/04 - HHT) Mantis issue 49799
        private bool IsValidGOLorry(string stLorry, ref CargoArrvDelvResult result)
        {
            if (!string.IsNullOrEmpty(stLorry))
            {
                IPortSafetyProxy proxy = new PortSafetyProxy();
                ResponseInfo info = null;
                CargoArrvDelvParm parm = new CargoArrvDelvParm();
                parm.lorryNo = stLorry;

                info = proxy.GetCargoArrvDelvByLorryNo(parm);
                if ((info != null) && (info.list != null) && (info.list.Length == 1) && (info.list[0] is CargoArrvDelvItem))
                {
                    CargoArrvDelvItem tmp = new CargoArrvDelvItem();
                    tmp = (CargoArrvDelvItem)info.list[0];

                    result = new CargoArrvDelvResult();

                    result.CgNo = (tmp.cgNo == null) ? "" : tmp.cgNo;
                    result.CgInOutCd = (tmp.cgInOutCd == null) ? "" : tmp.cgInOutCd;
                    result.Seq = (tmp.seq == null) ? "" : tmp.seq;
                    result.Wgt = tmp.wgt.ToString();
                    result.PkgQty = tmp.pkgQty.ToString();
                    result.CmdCd = (tmp.cmdtCd == null) ? "" : tmp.cmdtCd;
                    result.GateInDt = (tmp.gateInDt == null) ? "" : tmp.gateInDt;
                    result.GrNo = (tmp.grNo == null) ? "" : tmp.cgNo;
                    result.Tsptr = (tmp.tsptr == null) ? "" : tmp.tsptr;
                    result.GatePassNo = (tmp.gatePassNo == null) ? "" : tmp.gatePassNo;
                    result.DriverId = (tmp.tsptTpCd == null) ? "" : tmp.driverId;
                    result.DriverNm = (tmp.tsptCompNm == null) ? "" : tmp.driverNm;
                    result.LicsNo = (tmp.licsNo == null) ? "" : tmp.licsNo;
                    result.LicsExprYmd = (tmp.licsExprYmd == null) ? "" : tmp.licsExprYmd;
                    result.VslCallId = (tmp.vslCallId == null) ? "" : tmp.vslCallId;

                    return true;
                }
            }

            return false;
        }
        private bool IsValidGILorry(string stLorry)
        {
            try
            {
                Framework.Service.Provider.WebService.Provider.LorryListParm lorryParm = new Framework.Service.Provider.WebService.Provider.LorryListParm();
                lorryParm.LORRYNO = txtGILorry.Text;
                ICommonProxy proxy = new CommonProxy();
                ResponseInfo info = null;
                info = proxy.CheckValidLorry(lorryParm);
                if ((info != null) && (info.list != null) && (info.list.Length > 0))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void txtGIGRNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                CheckValidGIGR();
            }
        }

        private void CheckValidGIGR()
        {
            if (!m_isValidGIGR && !string.IsNullOrEmpty(txtGIGRNo.Text))
            {
                if (!CommonUtility.IsValidGR2(txtGIGRNo.Text, txtGILorry.Text, ref m_grGIResult))
                {
                    //CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0029"));
                    m_isValidGIGR = false;
                    btnGIF1.Focus();
                }
                else
                {
                    txtGIGRNo.Text = m_grGIResult.GrNo;
                    txtGICompany.Text = m_grGIResult.Tsptr;

                    //Added by Chris 2015-09-24 for 49779
                    txtGICmdt.Text = m_grGIResult.CmdtCd;
                    txtGIMt.Text = m_grGIResult.Mt;
                    txtGIQty.Text = m_grGIResult.Qty;
                    //commented by William (2015/08/04 - HHT) Mantis issue 49799
                    //txtGILorry.Text = m_grGIResult.Lorry;
                    txtGIDelvMode.Text = m_grGIResult.DelvTpNm;
                    CommonUtility.SetComboboxSelectedItem(cboGIDGChk, m_grGIResult.DgYn);
                    CommonUtility.SetComboboxSelectedItem(cboGIDGStatus, m_grGIResult.DgStatCd);

                    m_isValidGIGR = true;
                }
            }
        }

        private void txtGOLorry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                getGOInfo();
            }
        }

        private void txtGIDONo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                getGiDoNoInfo();
            }
        }

        //Added by Chris 2015-11-27
        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabMain.SelectedIndex == 0)
            {
                tabMain.TabPages[0].Text = "GATE IN";
                tabMain.TabPages[1].Text = "Gate Out";
            }
            else
            {
                tabMain.TabPages[0].Text = "Gate In";
                tabMain.TabPages[1].Text = "GATE OUT";
            }
        }
    }
}