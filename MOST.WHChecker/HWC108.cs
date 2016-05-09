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

namespace MOST.WHChecker
{
    public partial class HWC108 : TForm, IPopupWindow
    {
        #region variables
        private HWC108Parm m_parm;
        private CargoHandlingOutItem m_item;
        private HWC101002Result m_resHOLoc;

        private bool m_autoLocFlag;
        private bool m_manualActLocFlag;
        #endregion


        public HWC108()
        {
            InitializeComponent();
            this.initialFormSize();
            CommonUtility.SetDTPWithinShift(txtStartTime, txtEndTime);

            List<string> controlNames = new List<string>();
            controlNames.Add(txtLorry.Name);
            controlNames.Add(txtPkgNo.Name);
            controlNames.Add(txtStartTime.Name);
            controlNames.Add(txtEndTime.Name);
            controlNames.Add(txtLoadMT.Name);
            controlNames.Add(txtLoadM3.Name);
            controlNames.Add(txtLoadQty.Name);
            controlNames.Add(txtLoadLoc.Name);
            controlNames.Add(chkFinal.Name);
            this.AddOnChangeHandlerToInputControls(this.Controls, controlNames);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            this.m_parm = (HWC108Parm)parm;
            InitializeData();

            this.ShowDialog();
            return null;
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                #region Clearance
                CommonUtility.InitializeCombobox(cboClearance);
                cboClearance.Items.Add(new ComboboxValueDescriptionPair(Constants.CLEARANCE_HOLD, Constants.CLEARANCE_HOLD));
                cboClearance.Items.Add(new ComboboxValueDescriptionPair(Constants.CLEARANCE_RELEASE, Constants.CLEARANCE_RELEASE));
                cboClearance.Items.Add(new ComboboxValueDescriptionPair(Constants.CLEARANCE_INSPECTION, Constants.CLEARANCE_INSPECTION));
                #endregion

                #region Get cargo handling out
                if (m_parm != null)
                {
                    txtCgNo.Text = m_parm.CgNo;
                    GetCargoHandlingOutList();
                }
                #endregion

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

        private bool Validate()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // Need to validate cargo Status and block with message if cargo is Hold
                if (!Constants.CLEARANCE_RELEASE.Equals(CommonUtility.GetComboboxSelectedValue(cboClearance)))
                {
                    // Clearance status is not RELEASE. Do you want to proceed ?
                    if (CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HWC101_0005")) == DialogResult.No)
                    {
                        return false;
                    }
                }

                // Check if start time and end time are within work date & shift.
                if (!CommonUtility.ValidateStartEndDtWithinShift(txtStartTime, txtEndTime))
                {
                    return false;
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
                if (string.IsNullOrEmpty(txtLoadLoc.Text))
                {
                    bool isBBK = false;
                    bool isDBK = false;
                    double balMt = m_item.balMt;
                    int balQty = m_item.balQty;
                    double actMt = CommonUtility.ParseDouble(txtLoadMT.Text);
                    int actQty = CommonUtility.ParseInt(txtLoadQty.Text);
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

        private void GetCargoHandlingOutList()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region SN Amt, GR Amt, Lorry No, In Time
                // Request Webservice
                IWHCheckerProxy acProxy = new WHCheckerProxy();
                CargoHandlingOutParm rhdLdParm = new CargoHandlingOutParm();
                rhdLdParm.searchType = "RH";
                rhdLdParm.vslCallId = m_parm.VslCallId;
                rhdLdParm.cgNo = m_parm.CgNo;
                rhdLdParm.cgTpCd = m_parm.CgTpCd;
                rhdLdParm.shftDt = CommonUtility.FormatDateYYYYMMDD(UserInfo.getInstance().Workdate);
                rhdLdParm.shftId = UserInfo.getInstance().Shift;
                rhdLdParm.rhdlMode = m_parm.RhdlMode;
                rhdLdParm.rhdlNo = m_parm.RhdlNo;
                rhdLdParm.blSn = m_parm.BlSn;
                rhdLdParm.grNo = m_parm.OrgGrNo;
                rhdLdParm.catgCd = m_parm.OpeClassCd;
                rhdLdParm.jobCoCd = m_parm.CgCoCd;
                rhdLdParm.spCaCoCd = m_parm.SpCaCoCd;
                rhdLdParm.delvTpCd = m_parm.DelvTpCd;
                if (string.IsNullOrEmpty(m_parm.OrgGrNo))
                {
                    rhdLdParm.blNo = m_parm.OrgGrNo;
                }
                else
                {
                    rhdLdParm.shipgNoteNo = m_parm.OrgBlSn;
                }
                if ("I".Equals(m_parm.OpeClassCd))
                {
                    rhdLdParm.cgInOutCd = "O";
                }
                else if ("E".Equals(m_parm.OpeClassCd))
                {
                    rhdLdParm.cgInOutCd = "I";
                }

                ResponseInfo cgHOInfo = acProxy.getCargoRhdlHandlingOutList(rhdLdParm);

                // Display Data
                if (cgHOInfo != null && cgHOInfo.list.Length > 0 && cgHOInfo.list[0] is CargoHandlingOutItem)
                {
                    CargoHandlingOutItem item = (CargoHandlingOutItem)cgHOInfo.list[0];
                    m_item = item;
                    txtLorry.Text = item.lorryId;
                    txtDocM3.Text = item.docM3.ToString();
                    txtDocQty.Text = item.docQty.ToString();
                    txtDocMT.Text = item.docMt.ToString();
                    txtActMT.Text = item.actMt.ToString();
                    txtActM3.Text = item.actM3.ToString();
                    txtActQty.Text = item.actQty.ToString();
                    txtTsptr.Text = item.tsptr;

                    string strBalMT = item.balMt.ToString();
                    string strBalM3 = item.balM3.ToString();
                    string strBalQty = item.balQty.ToString();
                    txtBalMT.Text = strBalMT;
                    txtBalM3.Text = strBalM3;
                    txtBalQty.Text = strBalQty;
                    txtLoadMT.Text = strBalMT;
                    txtLoadM3.Text = strBalM3;
                    txtLoadQty.Text = strBalQty;

                    CommonUtility.SetComboboxSelectedItem(cboClearance, item.custMode);

                    if ("S".Equals(item.spCaCoCd) && "D".Equals(item.delvTpCd))
                    {
                        CheckSpareCargoDirect();
                        txtLoadLoc.Text = item.locId.ToString();
                    }
                }
                #endregion
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

        private void CheckSpareCargoDirect()
        {
            txtLoadLoc.Enabled = false;
            btnUnsetLoc.Enabled = false;
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
                // Check if handling-out amount == balance amount
                if (m_item != null)
                {
                    double balMt = m_item.balMt;
                    int balQty = m_item.balQty;
                    double totMt = CommonUtility.ParseDouble(txtLoadMT.Text);
                    int totQty = CommonUtility.ParseInt(txtLoadQty.Text);

                    DialogResult dr;
                    if (isBBK)
                    {
                        if (totMt >= balMt || totQty >= balQty)
                        {
                            dr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0069"));
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
                            dr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0069"));
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

        private bool ProcessCargoHandlingOutItem()
        {
            // ref: CT215002
            bool result = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ArrayList whConfigList = new ArrayList();
                IWHCheckerProxy proxy = new WHCheckerProxy();
                CargoHandlingOutItem item;
                if (m_item != null)
                {
                    item = m_item;
                }
                else
                {
                    return false;
                }

                item.dmgYn = CommonUtility.ToString(item.dmgYn);
                item.catgCd = CommonUtility.ToString(item.catgCd);
                item.delvTpCd = CommonUtility.ToString(item.delvTpCd);
                item.stat = CommonUtility.ToString(item.stat);
                item.fnlDelvYn = CommonUtility.ToString(item.fnlDelvYn);
                item.autoLocFlag = m_autoLocFlag ? "true" : "false";
                item.jobCoCd = m_parm != null ? CommonUtility.ToString(m_parm.CgCoCd) : string.Empty;

                item.cgNo = txtCgNo.Text;
                //item.docQty = m_cargoHOItem.docQty;
                //item.docMt = m_cargoHOItem.docMt;
                //item.docM3 = m_cargoHOItem.docM3;
                //item.actQty = CommonUtility.ParseInt(txtDocQty.Text);
                //item.actMt = CommonUtility.ParseDouble(txtActMT.Text);
                //item.actM3 = CommonUtility.ParseDouble(txtDocM3.Text);
                //item.balQty = CommonUtility.ParseInt(txtBalQty.Text);
                //item.balMt = CommonUtility.ParseDouble(txtBalMT.Text);
                //item.balM3 = CommonUtility.ParseDouble(txtBalM3.Text);
                item.loadQty = CommonUtility.ParseInt(txtLoadQty.Text);
                item.loadMt = CommonUtility.ParseDouble(txtLoadMT.Text);
                item.loadM3 = CommonUtility.ParseDouble(txtLoadM3.Text);
                item.blNo = txtCgNo.Text;
                //item.grNo
                //item.doNo
                //item.gateInDt
                //item.stat
                item.hdlOutStDt = txtStartTime.Text;
                item.hdlOutEndDt = txtEndTime.Text;
                //item.delvTpCd = "I";
                //item.hatchNo = m_cargoHOItem.hatchNo;
                //item.shftDt = UserInfo.getInstance().Workdate;
                //item.shftId = UserInfo.getInstance().Shift;
                //item.tsptr = m_cargoHOItem.tsptr;
                item.lorryId = txtLorry.Text;
                if (m_resHOLoc != null)
                {
                    item.locId = m_resHOLoc.LocId;
                }
                //item.catgCd = m_cargoHOItem.catgCd;
                item.whFnlDelvYn = GetFinalOprYN();
                //item.actlDelvTpCd = m_cargoHOItem.actlDelvTpCd;
                //item.disEndDt = m_cargoHOItem.disEndDt;
                item.userId = UserInfo.getInstance().UserId;
                item.CRUD = Constants.WS_INSERT;
                //item.cgTpCd = m_cargoHOItem.cgTpCd;
                //item.custMode
                //item.shipgNoteNo
                //item.jobCoCd
                //item.spCaCoCd
                if (m_parm != null)
                {
                    item.rhdlMode = m_parm.RhdlMode;
                    item.rhdlNo = m_parm.RhdlNo;
                }
                item.pkgNo = txtPkgNo.Text;
                item.rmk = txtRemark.Text;

                // Unset location - saving WhConfigurationItem
                if (m_resHOLoc != null && m_resHOLoc.ArrWHLocation != null && m_resHOLoc.ArrWHLocation.Count > 0)
                {
                    whConfigList.AddRange(m_resHOLoc.ArrWHLocation);
                }
                if (whConfigList.Count > 0)
                {
                    item.collection = whConfigList.ToArray();
                }
                if ("S".Equals(item.spCaCoCd) && "D".Equals(item.delvTpCd))
                {
                    WhConfigurationItem whItem = new WhConfigurationItem();
                    whItem.locId = item.sprLocId;
                    whItem.whTpCd = "G";
                    whItem.spCaCoCd = item.spCaCoCd;
                    whItem.wgt = item.loadMt;
                    whItem.msrmt = item.loadM3;
                    whItem.pkgQty = item.loadQty.ToString();
                    whItem.vslCallId = item.vslCallId;
                    whItem.cgNo = item.cgNo;
                    ArrayList whSpList = new ArrayList();
                    whSpList.Add(whItem);
                    item.collection = whSpList.ToArray();
                }

                Object[] obj = new Object[] { item };
                DataItemCollection dataCollection = new DataItemCollection();
                dataCollection.collection = obj;

                proxy.processCargoHandlingOutItem(dataCollection);
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
                case "btnOk":
                    if (this.IsDirty)
                    {
                        if (this.validations(this.Controls) && this.Validate() && ProcessCargoHandlingOutItem())
                        {
                            // Save successfully. Do you want to open the GatePass screen?
                            DialogResult gpDr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0072"));
                            if (gpDr == DialogResult.Yes)
                            {
                                MOST.Common.CommonParm.CargoGatePassParm gpParm = new MOST.Common.CommonParm.CargoGatePassParm();
                                if (m_parm != null)
                                {
                                    gpParm.VslCallId = m_parm.VslCallId;
                                    gpParm.CgNo = txtCgNo.Text;
                                }
                                PopupManager.instance.ShowPopup(new MOST.Common.HCM116(), gpParm);
                            }
                            this.Close();
                        }
                    }
                    else
                    {
                        this.Close();
                    }
                    break;

                case "btnCancel":
                    if (this.IsDirty)
                    {
                        DialogResult dr = CommonUtility.ConfirmMsgSaveChances();
                        if (dr == DialogResult.Yes)
                        {
                            if (this.validations(this.Controls))
                            {
                                if (ProcessCargoHandlingOutItem())
                                {
                                    // Save successfully. Do you want to open the GatePass screen?
                                    DialogResult gpDr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0072"));
                                    if (gpDr == DialogResult.Yes)
                                    {
                                        MOST.Common.CommonParm.CargoGatePassParm gpParm = new MOST.Common.CommonParm.CargoGatePassParm();
                                        if (m_parm != null)
                                        {
                                            gpParm.VslCallId = m_parm.VslCallId;
                                            gpParm.CgNo = txtCgNo.Text;
                                        }
                                        PopupManager.instance.ShowPopup(new MOST.Common.HCM116(), gpParm);
                                    }
                                    else
                                    {
                                        CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HCM0048"));
                                    }
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

                case "btnUnsetLoc":
                    HWC101002Parm parmUnsetActLoc = new HWC101002Parm();
                    parmUnsetActLoc.VslCallId = m_parm.VslCallId;
                    parmUnsetActLoc.CgNo = txtCgNo.Text;
                    parmUnsetActLoc.TotMt = txtLoadMT.Text;
                    parmUnsetActLoc.TotM3 = txtLoadM3.Text;
                    parmUnsetActLoc.TotQty = txtLoadQty.Text;
                    if ("D".Equals(m_parm.CgCoCd))
                    {
                        parmUnsetActLoc.IsDamageCg = true;
                    }
                    else if ("S".Equals(m_parm.CgCoCd))
                    {
                        parmUnsetActLoc.IsShutoutCg = true;
                    }
                    else if ("G".Equals(m_parm.CgCoCd))
                    {
                        parmUnsetActLoc.IsGeneralCg = true;
                    }
                    HWC101002Result resultUnsetActLoc = (HWC101002Result)PopupManager.instance.ShowPopup(new HWC101002(), parmUnsetActLoc);
                    if (resultUnsetActLoc != null)
                    {
                        m_resHOLoc = resultUnsetActLoc;
                        txtLoadLoc.Text = m_resHOLoc.LocId;
                    }
                    break;

                case "btnF1":
                    if (!string.IsNullOrEmpty(m_parm.OrgGrNo))
                    {
                        PartnerCodeListParm lorryHOParm = new PartnerCodeListParm();
                        if (!string.IsNullOrEmpty(txtTsptr.Text))
                        {
                            lorryHOParm.PtnrCd = txtTsptr.Text;
                        }
                        PartnerCodeListResult lorryHORes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_LORRY), lorryHOParm);
                        if (lorryHORes != null)
                        {
                            txtLorry.Text = lorryHORes.LorryNo;
                        }
                    }
                    else
                    {
                        MOST.Common.CommonParm.LorryListParm lorryParm = new MOST.Common.CommonParm.LorryListParm();
                        lorryParm.Jpvc = m_parm.VslCallId;
                        lorryParm.BlNo = m_parm.CgNo;
                        LorryListResult lorryResult = (LorryListResult)PopupManager.instance.ShowPopup(new HCM107(HCM107.TYPE_HAC_DISCHARGING), lorryParm);
                        if (lorryResult != null)
                        {
                            txtLorry.Text = lorryResult.LorryNo;
                        }
                    }
                    break;
            }
        }
        #endregion
    }
}