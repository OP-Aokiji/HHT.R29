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

namespace MOST.ApronChecker
{
    public partial class HAC112 : TForm, IPopupWindow
    {
        #region Local Variable
        private HAC112Parm m_parm;
        private CargoLoadingItem m_item;
        private HWC101002Result m_resLocAct;

        private bool m_autoLocFlag;
        private bool m_manualActLocFlag;
        #endregion

        public HAC112()
        {
            InitializeComponent();
            this.initialFormSize();
            CommonUtility.SetDTPWithinShift(txtStartTime, txtEndTime);

            List<string> controlNames = new List<string>();
            controlNames.Add(cboOprMode.Name);
            controlNames.Add(cboHatch.Name);
            controlNames.Add(txtLorryNo.Name);
            controlNames.Add(txtActMT.Name);
            controlNames.Add(txtActM3.Name);
            controlNames.Add(txtActQty.Name);
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
            controlNames.Add(chkFinal.Name);
            controlNames.Add(txtStartTime.Name);
            controlNames.Add(txtEndTime.Name);
            this.AddOnChangeHandlerToInputControls(this.Controls, controlNames);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            this.m_parm = (HAC112Parm)parm;
            InitializeData();

            bool bClose = false;
            if (m_item != null)
            {
                // Check if this shipping note was already confirmed.
                if ("N".Equals(m_item.snLdYn))
                {
                    CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC105_0002"));
                    bClose = true;
                }
                else
                {
                    // Check if this cargo was already loaded.
                    if ("Y".Equals(m_item.fnlLoadYn))
                    {
                        CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC105_0001"));
                        bClose = true;
                    }
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
            return null;
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Init
                string strTitle = "A/C - Rehandle Loading";
                if (m_parm != null)
                {
                    strTitle = strTitle + " - " + m_parm.CgNo;
                }
                this.Text = strTitle;
                chkLDmg.Checked = false;
                EnableLoadedDmg(chkLDmg.Checked);
                #endregion

                #region Doc, Balance, Hatch, Delivery Mode, PkgType
                GetCargoLoadingList();
                SetPrimaryPkgType();
                #endregion
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
                this.txtPkgTp.isMandatory = "BBK".Equals(m_item.cgTpCd);
            }
        }

        private bool Validate()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (Constants.OPRMODE_LORRY.Equals(CommonUtility.GetComboboxSelectedValue(cboOprMode)) &&
                    string.IsNullOrEmpty(txtLorryNo.Text))
                {
                    CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC112_0001"));
                    return false;
                }

                // Check Package Amount & Loose Bag Type
                // No check any more , empty or not still allow to save
                //if (!CheckInputPkgAmnt())
                //{
                //    return false;
                //}

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

                // Validate Actual amount 
                if (!ValidateAmount())
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
                    m_autoLocFlag = GetActAutoDeallocationFlag();
                    if (m_manualActLocFlag)
                    {
                        return false;
                    }
                    if (m_autoLocFlag)
                    {
                        if (DialogResult.No == CommonUtility.ConfirmMessage("Do you want to unset location automatically ?"))
                        {
                            CommonUtility.AlertMessage("Please unset Actual location manually before proceeding.");
                            return false;
                        }
                    }
                }

                // Validate lorry input by key
                if (!ValidateLorryAssignment())
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0074"));
                    txtLorryNo.Focus();
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
                CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC112_0002"));
                txtBagTp.SelectAll();
                txtBagTp.Focus();
                return false;
            }
            if (looseBagMt == 0 && looseBagM3 == 0 && looseBagQty == 0 &&
                !string.IsNullOrEmpty(txtBagTp.Text.Trim()))
            {
                CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HAC112_0003"));
                txtPkgMT.SelectAll();
                txtPkgMT.Focus();
                return false;
            }

            return true;
        }

        private bool ValidateAmount()
        {
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
            double actMt = CommonUtility.ParseDouble(txtActMT.Text);
            double actQty = CommonUtility.ParseDouble(txtActM3.Text);
            bool isActEmptyAmt = false;

            if ("D".Equals(m_item.delvTpCd))
            {
                CommonUtility.AlertMessage("Cannot load cargo whose delivery type is direct. Please check again.");
                return false;
            }
            else if ("I".Equals(m_item.delvTpCd))
            {
                if (isBBK)
                {
                    // Actual
                    if (actMt > 0 || actQty > 0)
                    {
                        if (balMt <= 0 && balQty <= 0)
                        {
                            CommonUtility.AlertMessage("This cargo cannot load because balance amount is less than 0.");
                            return false;
                        }
                        if (actMt > balMt || actQty > balQty)
                        {
                            CommonUtility.AlertMessage("Actual amount cannot exceed balance amount.");
                            return false;
                        }
                    }
                    else
                    {
                        isActEmptyAmt = true;
                    }
                }
                else if (isDBK)
                {
                    // Actual
                    if (actMt > 0)
                    {
                        if (balMt <= 0)
                        {
                            CommonUtility.AlertMessage("This cargo cannot load because balance amount is less than 0.");
                            return false;
                        }
                        if (actMt > balMt)
                        {
                            CommonUtility.AlertMessage("Actual amount cannot exceed balance amount.");
                            return false;
                        }
                    }
                    else
                    {
                        isActEmptyAmt = true;
                    }
                }

                if (isActEmptyAmt)
                {
                    CommonUtility.AlertMessage("Please input loading amount.");
                    return false;
                }
            }

            return true;
        }

        private bool ValidateLorryAssignment()
        {
            // Request Webservice
            ICommonProxy proxy = new CommonProxy();
            AssignmentLorrysParm lorryParm = new AssignmentLorrysParm();
            lorryParm.cd = txtLorryNo.Text;
            lorryParm.ptnrCd = "'" + m_item.tsptr + "'";
            lorryParm.divCd = "LR";
            lorryParm.searchType = "popUpList";

            ResponseInfo info = proxy.getAssignmentLorrysItems(lorryParm);

            if (info != null && info.list != null && info.list.Length > 0)
            {
                if (info.list[0] is AssignmentLorrysItem)
                {
                    return true;
                }
            }

            return false;
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
                if (string.IsNullOrEmpty(txtActLoc.Text))
                {
                    bool isBBK = false;
                    bool isDBK = false;
                    double balMt = m_item.balMt;
                    int balQty = m_item.balQty;
                    double actMt = CommonUtility.ParseDouble(txtActMT.Text);
                    int actQty = CommonUtility.ParseInt(txtActQty.Text);
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
                            CommonUtility.AlertMessage("Please unset Actual location manually.");
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

        private void GetCargoLoadingList()
        {
            try
            {
                // Request Webservice
                IApronCheckerProxy acProxy = new ApronCheckerProxy();
                CargoLoadingParm rhdLdParm = new CargoLoadingParm();
                rhdLdParm.vslCallId = m_parm.VslCallId;
                rhdLdParm.grNo = m_parm.GrNo;
                rhdLdParm.cgNo = m_parm.CgNo;
                rhdLdParm.jobCoCd = m_parm.JobCoCd;
                rhdLdParm.shftDt = CommonUtility.FormatDateYYYYMMDD(UserInfo.getInstance().Workdate);
                rhdLdParm.shftId = UserInfo.getInstance().Shift;
                rhdLdParm.cgTpCd = m_parm.CgTpCd;
                rhdLdParm.shipgNoteNo = m_parm.ShipgNoteNo;
                rhdLdParm.delvTpCd = m_parm.DelvTpCd;
                rhdLdParm.catgCd = m_parm.CatgCd;
                rhdLdParm.spCaCoCd = m_parm.SpCaCoCd;
                rhdLdParm.jobCoCd = m_parm.JobCoCd;
                rhdLdParm.opeClassCd = m_parm.OpeClassCd;
                rhdLdParm.rhdlNo = m_parm.RhdlNo;
                rhdLdParm.orgVslCallId = m_parm.OrgVslCallId;
                rhdLdParm.orgCgNo = m_parm.OrgCgNo;

                // Fix issue 33939
                rhdLdParm.rhdlGroupNo = m_parm.RhdlGroupNo;
                rhdLdParm.orgrefno = m_parm.OrgRefNo;

                ResponseInfo rhdLdInfo = acProxy.getCargoRhdlLoadingList(rhdLdParm);

                // Display Data
                if (rhdLdInfo != null && rhdLdInfo.list != null && rhdLdInfo.list.Length > 0)
                {
                    // Remained items in ResponseInfo are OperationSetItem/CodeMasterListItem
                    CommonUtility.InitializeCombobox(cboHatch);
                    CommonUtility.InitializeCombobox(cboOprMode);
                    for (int i = 0; i < rhdLdInfo.list.Length; i++)
                    {
                        // Hatch No
                        if (rhdLdInfo.list[i] is OperationSetItem)
                        {
                            OperationSetItem item = (OperationSetItem) rhdLdInfo.list[i];
                            cboHatch.Items.Add(new ComboboxValueDescriptionPair(item.hatchNo, item.hatchNo));
                        }
                            // Mode of Operation
                        else if (rhdLdInfo.list[i] is CodeMasterListItem)
                        {
                            CodeMasterListItem item = (CodeMasterListItem) rhdLdInfo.list[i];
                            if ("TSPTTP".Equals(item.mcd))
                            {
                                cboOprMode.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                            }
                        }
                        else if (rhdLdInfo.list[i] is CodeMasterListItem1)
                        {
                            CodeMasterListItem1 item = (CodeMasterListItem1)rhdLdInfo.list[i];
                            if ("TSPTTP".Equals(item.mcd))
                            {
                                cboOprMode.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                            }
                        }
                    }

                    // Fix issue 33939
                    for (int i = 0; i < rhdLdInfo.list.Length; i++)
                    {
                        CargoLoadingItem item = rhdLdInfo.list[i] as CargoLoadingItem;

                        // If ResponseInfo is CargoLoadingItem.
                        // Doc, Balance infomation
                        if (item != null)
                        {
                            m_item = item;

                            // Lorry
                            txtLorryNo.Text = item.lorryId;
                            // PkgTp
                            txtPkgTp.Text = item.repkgTypeCd;

                            // Document, Balance
                            txtDocM3.Text = item.docM3.ToString();
                            txtDocMT.Text = item.docMt.ToString();
                            txtDocQty.Text = item.docQty.ToString();
                            txtBalM3.Text = item.balM3.ToString();
                            txtBalMT.Text = item.balMt.ToString();
                            txtBalQty.Text = item.balQty.ToString();

                            // Delivery Mode
                            if ("D".Equals(item.opDelvTpCd))
                            {
                                txtDelvMode.Text = "Direct";
                                txtActLoc.Enabled = false;
                                btnActUnset.Enabled = false;
                            }
                            else if ("I".Equals(item.opDelvTpCd))
                            {
                                txtDelvMode.Text = "Indirect";
                            }

                            // Mode of operation
                            CommonUtility.SetComboboxSelectedItem(cboOprMode, item.tsptTpCd);
                            break;
                        }
                    }


                }

                this.IsDirty = false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private bool ProcessCargoRhdlLoadingItem()
        {
            // ref: CT215001
            bool result = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ArrayList whConfigList = new ArrayList();
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

                item.loadCnclMode = "N";
                item.rhdlYn = "N";
                item.gatePassYn = "N";
                item.shutRhdlMode = CommonUtility.ToString(item.shutRhdlMode);
                item.dmgRhdlMode = CommonUtility.ToString(item.dmgRhdlMode);
                item.repkgTpCd = CommonUtility.ToString(item.repkgTpCd);
                item.jobCoCd = CommonUtility.ToString(item.jobCoCd);
                item.autoLocFlag = m_autoLocFlag ? "true" : "false";
                
                item.rhdlNo = m_parm.RhdlNo;
                item.searchType = "RH";
                item.qty = CommonUtility.ParseInt(txtDocQty.Text);
                item.mt = CommonUtility.ParseDouble(txtDocMT.Text);
                item.m3 = CommonUtility.ParseDouble(txtDocM3.Text);
                item.balQty = CommonUtility.ParseInt(txtBalQty.Text);
                item.balMt = CommonUtility.ParseDouble(txtBalMT.Text);
                item.balM3 = CommonUtility.ParseDouble(txtBalM3.Text);
                item.loadQty = CommonUtility.ParseInt(txtActQty.Text);
                item.loadMt = CommonUtility.ParseDouble(txtActMT.Text);
                item.loadM3 = CommonUtility.ParseDouble(txtActM3.Text);
                item.chkLoadDmgYn = chkLDmg.Checked;
                if (chkLDmg.Checked)
                {
                    item.loadDmgMt = CommonUtility.ParseDouble(txtLDmgMT.Text);
                    item.loadDmgM3 = CommonUtility.ParseDouble(txtLDmgM3.Text);
                    item.loadDmgQty = CommonUtility.ParseInt(txtLDmgQty.Text);
                }
                item.startDt = txtStartTime.Text;
                item.endDt = txtEndTime.Text;
                item.tsptTpCd = CommonUtility.GetComboboxSelectedValue(cboOprMode);
                item.lorryId = txtLorryNo.Text;
                item.fnlOpeYn = GetFinalOprYN();
                item.repkgQty = CommonUtility.ParseInt(txtPkgQty.Text);
                item.repkgWgt = CommonUtility.ParseDouble(txtPkgMT.Text);
                item.repkgMsrmt = CommonUtility.ParseDouble(txtPkgM3.Text);
                item.repkgTpCd = txtBagTp.Text;
                item.userId = UserInfo.getInstance().UserId;
                item.shftDt = CommonUtility.FormatDateYYYYMMDD(UserInfo.getInstance().Workdate);
                item.shftId = UserInfo.getInstance().Shift;
                item.hatchNo = CommonUtility.GetComboboxSelectedValue(cboHatch);

                item.CRUD = Constants.WS_INSERT;

                // set spCaCoCd to null if is empty to fit server side
                foreach (Object childObj in item.childItems)
                {
                    if (childObj is CargoLoadingItem)
                    {
                        CargoLoadingItem cgItem = (CargoLoadingItem)childObj;
                        if (String.IsNullOrEmpty(cgItem.spCaCoCd))
                            cgItem.spCaCoCd = null;
                    }
                }

                // Set/Unset location - saving WhConfigurationItem
                if (m_resLocAct != null)
                {
                    item.locId = m_resLocAct.LocId;
                    if (m_resLocAct.ArrWHLocation != null && m_resLocAct.ArrWHLocation.Count > 0)
                    {
                        foreach (Object whObj in m_resLocAct.ArrWHLocation)
                        {
                            if (whObj is WhConfigurationItem)
                            {
                                WhConfigurationItem whItem = (WhConfigurationItem)whObj;
                                whItem.rhdlMode = item.rhdlMode;
                                whItem.rhdlNo = item.rhdlNo;
                            }
                        }
                        whConfigList.AddRange(m_resLocAct.ArrWHLocation);
                    }
                }
                if (whConfigList.Count > 0)
                {
                    item.collection = whConfigList.ToArray();
                }

                Object[] obj = new Object[] { item };
                DataItemCollection dataCollection = new DataItemCollection();
                dataCollection.collection = obj;

                proxy.processCargoRhdlLoadingItem(dataCollection);
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

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnF1":
                    PartnerCodeListParm driverParm = new PartnerCodeListParm();
                    driverParm.SearchItem = txtLorryNo.Text;
                    if (m_item != null)
                    {
                        driverParm.PtnrCd = m_item.tsptr;
                        driverParm.ScreenId = "Assigned";
                        driverParm.VslCallId = m_item.vslCallId;
                        driverParm.ShippingNoteNo = m_item.shipgNoteNo;
                    }
                    PartnerCodeListResult driverResult = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_LORRY), driverParm);
                    if (driverResult != null)
                    {
                        txtLorryNo.Text = driverResult.LorryNo;
                    }
                    break;

                case "btnF2":
                    PartnerCodeListParm bagTypeParm = new PartnerCodeListParm();
                    bagTypeParm.Option = "CD";
                    bagTypeParm.SearchItem = txtBagTp.Text;
                    PartnerCodeListResult bagTypeRes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_BAGTYPE), bagTypeParm);
                    if (bagTypeRes != null)
                    {
                        txtBagTp.Text = bagTypeRes.Code;
                    }
                    break;

                case "btnConfirm":
                    if (this.IsDirty)
                    {
                        if (this.validations(this.Controls) && Validate() && ProcessCargoRhdlLoadingItem())
                        {
                            //ClearCtrlValues();
                            //GetCargoLoadingList();
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
                            this.Close();
                        }
                    }
                    break;

                case "btnCancel":
                    if (this.IsDirty)
                    {
                        DialogResult dr = CommonUtility.ConfirmMsgSaveChances();
                        if (dr == DialogResult.Yes)
                        {
                            if (this.validations(this.Controls) && Validate())
                            {
                                ProcessCargoRhdlLoadingItem();
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
                    HWC101002Parm parmActLoc = new HWC101002Parm();
                    parmActLoc.VslCallId = m_item.orgVslCallId;
                    parmActLoc.BlSn = string.IsNullOrEmpty(m_item.grNo) ? m_item.orgBlSn : m_item.grNo;
                    parmActLoc.CgNo = m_item.orgCgNo;
                    parmActLoc.CatgCd = m_item.catgCd;
                    parmActLoc.TotMt = txtActMT.Text;
                    parmActLoc.TotM3 = txtActM3.Text;
                    parmActLoc.TotQty = txtActQty.Text;
                    parmActLoc.SpCaCoCd = m_parm.SpCaCoCd;
                    if ("G".Equals(m_parm.CgCoCd))
                    {
                        parmActLoc.IsGeneralCg = true;
                    }
                    else if ("D".Equals(m_parm.CgCoCd))
                    {
                        parmActLoc.IsDamageCg = true;
                    }
                    else if ("S".Equals(m_parm.CgCoCd))
                    {
                        parmActLoc.IsShutoutCg = true;
                    }
                    
                    HWC101002Result resultActLoc = (HWC101002Result)PopupManager.instance.ShowPopup(new HWC101002(), parmActLoc);
                    if (resultActLoc != null)
                    {
                        m_resLocAct = resultActLoc;
                        txtActLoc.Text = m_resLocAct.LocId;
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
                // Loading amount = Load Normal
                double balMt = 0;
                int balQty = 0;
                if (m_item != null)
                {
                    balMt = m_item.balMt;
                    balQty = m_item.balQty;
                }
                double totMt = CommonUtility.ParseDouble(txtActMT.Text);
                int totQty = CommonUtility.ParseInt(txtActQty.Text);

                DialogResult dr;
                if (isBBK)
                {
                    if (totMt >= balMt || totQty >= balQty)
                    {
                        dr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0068"));
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
                        dr = CommonUtility.ConfirmMessage(ResourceManager.getInstance().getString("HCM0068"));
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

        private void HAC112_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q && e.Modifiers == Keys.Alt)
            {
                MessageBox.Show("Screen ID : " + this.Name + "\n\rScreen caption: " + this.Text, "Screen Information !");
            }
        }
    }
}