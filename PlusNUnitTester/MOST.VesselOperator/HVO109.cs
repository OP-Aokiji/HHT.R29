using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using Framework.Common.Constants;
using MOST.Common;
using MOST.Common.Utility;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.VesselOperator.Parm;
using MOST.VesselOperator.Result;
using MOST.Client.Proxy.CommonProxy;
using MOST.Client.Proxy.VesselOperatorProxy;
using Framework.Common.PopupManager;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.VesselOperator
{
    public partial class HVO109 : TForm, IPopupWindow
    {
        // SL: Ship to ship - Loading
        private string _SL_Qty;
        private string _SL_Mt;
        private string _SL_M3;
        // SD: Ship to ship - Discharging
        private string _SD_Qty;
        private string _SD_Mt;
        private string _SD_M3;
        // SL: Ship to ship - Loading Balance
        private long _SL_Qty_Bal;
        private double _SL_Mt_Bal;
        private double _SL_M3_Bal;
        // SD: Ship to ship - Discharging Balance
        private long _SD_Qty_Bal;
        private double _SD_Mt_Bal;
        private double _SD_M3_Bal;
        // SL: Commodity - Loading
        private string _cmdtCdSL = string.Empty;
        //private string _cmdtCdNmSL = string.Empty;
        // SL: Commodity - Discharging
		private string _cmdtCdSD = string.Empty;
        //private string _cmdtCdNmSD = string.Empty;

        private ResponseInfo m_docInfo;    // Get document response info from Confirmation Slip
        private ResponseInfo m_balInfo;    // Get balance response info from Confirmation Slip
        private ResponseInfo m_cmdtInfo;   // Get commodity response info from Confirmation Slip

        private string m_dblBankingVslCallId = string.Empty;
        private HVO109Parm m_parm;

        public HVO109()
        {
            InitializeComponent();
            this.initialFormSize();

            List<string> controlNames = new List<string>();
            controlNames.Add(txtNextJPVC.Name);
            controlNames.Add(cboOprMode.Name);
            controlNames.Add(cboHatch.Name);
            controlNames.Add(cboWP.Name);
            controlNames.Add(txtCommodity.Name);
            controlNames.Add(txtPkgType.Name);
            controlNames.Add(txtBalMT.Name);
            controlNames.Add(txtBalM3.Name);
            controlNames.Add(txtBalQty.Name);
            controlNames.Add(txtStartTime.Name);
            controlNames.Add(txtEndTime.Name);
            this.AddOnChangeHandlerToInputControls(this.Controls, controlNames);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HVO109Parm)parm;
            if (m_parm != null)
            {
                m_dblBankingVslCallId = m_parm.DblBankingVslCallId;
                if (m_parm.JpvcInfo != null)
                {
                    txtNextJPVC.Text = m_parm.JpvcInfo.Jpvc;
                }
            }
            InitializeData();
            this.ShowDialog();
            return null;
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnF1":
                    PartnerCodeListParm cmdtParm = new PartnerCodeListParm();
                    cmdtParm.Option = "CD";
                    cmdtParm.SearchItem = txtCommodity.Text;
                    PartnerCodeListResult cmdtRes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_COMMODITY), cmdtParm);
                    if (cmdtRes != null)
                    {
                        txtCommodity.Text = cmdtRes.Code;
                    }
                    break;

                case "btnF2":
                    PartnerCodeListParm pkgTypeParm = new PartnerCodeListParm();
                    pkgTypeParm.Option = "CD";
                    pkgTypeParm.SearchItem = txtPkgType.Text;
                    PartnerCodeListResult pkgTypeRes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_PKGTYPE), pkgTypeParm);
                    if (pkgTypeRes != null)
                    {
                        txtPkgType.Text = pkgTypeRes.Code;
                    }
                    break;

                case "btnOk":
                    if (this.IsDirty)
                    {
                        if (this.validations(this.Controls) && Validate())
                        {
                            if (ProcessgetSftDblBankingItem())
                            {
                                ClearForm();
                                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
                            }
                        }
                    }
                    else
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
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
                                ProcessgetSftDblBankingItem();
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

                case "btnList":
                    HVO117Parm listParm = new HVO117Parm();
                    listParm.VslCallId = m_dblBankingVslCallId;
                    PopupManager.instance.ShowPopup(new HVO117(), listParm);
                    break;
            }
        }

        private void GetConfirmationSlipInfo(string nextJPVC)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (!string.IsNullOrEmpty(nextJPVC))
                {
                    IVesselOperatorProxy proxy = new VesselOperatorProxy();

                    #region Get Document Amount By OPR Mode (Within Confirmation Slip Info)
                    SftDblBankingParm parm = new SftDblBankingParm();
                    parm.searchType = "HHT_IF_STS_DOC";
                    parm.vslCallId = nextJPVC;
                    m_docInfo = proxy.getSftDblBankingList(parm);
                    #endregion

                    #region Get Balance Amount By OPR Mode (Within Confirmation Slip Info)
                    parm = new SftDblBankingParm();
                    parm.searchType = "HHT_IF_STS";
                    parm.vslCallId = nextJPVC;
                    m_balInfo = proxy.getSftDblBankingList(parm);
                    #endregion

                    #region Get Commodity By OPR Mode (Within Confirmation Slip Info)
                    parm = new SftDblBankingParm();
                    parm.vslCallId = nextJPVC;
                    parm.searchType = "HHT_IF_STS_CMDT_FROM_CS";
                    m_cmdtInfo = proxy.getSftDblBankingList(parm);
                    #endregion
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

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Initialize title
                string strTitle = "V/S - STS";
                if (m_parm != null && m_parm.JpvcInfo != null)
                {
                    strTitle = strTitle + " - " + m_parm.JpvcInfo.Jpvc;
                }
                this.Text = strTitle;
                #endregion

                #region Initialize data
                CommonUtility.SetDTPValueBlank(txtStartTime);
                CommonUtility.SetDTPValueBlank(txtEndTime);
                #endregion

                #region HatchNo, Harchdrt
                CommonUtility.GetDeclaredHatches(txtNextJPVC.Text, cboHatch);
                CommonUtility.SetHatchDirectionAPFP(cboWP);
                #endregion

                #region OPR Mode
                // Request Webservice
                ICommonProxy commonProxy = new CommonProxy();
                CommonCodeParm commonParm = new CommonCodeParm();
                commonParm.searchType = "COMM";
                commonParm.lcd = "MT";
                commonParm.divCd = "STSOPTP";
                ResponseInfo commonInfo = commonProxy.getCommonCodeList(commonParm);

                // Display Data
                CommonUtility.InitializeCombobox(cboOprMode);
                for (int i = 0; i < commonInfo.list.Length; i++)
                {
                    if (commonInfo.list[i] is CodeMasterListItem)
                    {
                        CodeMasterListItem item = (CodeMasterListItem)commonInfo.list[i];
                        cboOprMode.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                    else if (commonInfo.list[i] is CodeMasterListItem1)
                    {
                        CodeMasterListItem1 item = (CodeMasterListItem1)commonInfo.list[i];
                        cboOprMode.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                }
                #endregion

                #region Cargo type
                IVesselOperatorProxy proxy = new VesselOperatorProxy();

                SftDblBankingParm parm = new SftDblBankingParm();
                parm.vslCallId = txtNextJPVC.Text;
                parm.searchType = "HHT_IF_STS_CGTP";
                ResponseInfo info = proxy.getSftDblBankingList(parm);

                CommonUtility.InitializeCombobox(cboCargoType);
                if (info != null && info.list != null)
                {
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        SftDblBankingItem item = (SftDblBankingItem)info.list[i];
                        cboCargoType.Items.Add(new ComboboxValueDescriptionPair(item.cgTpCd, item.cgTpNm));
                    }
                }
                #endregion

                #region Get info of 2nd JPVC from confirmation slip.
                GetConfirmationSlipInfo(txtNextJPVC.Text);
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

        private bool ProcessgetSftDblBankingItem()
        {
            bool result = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                IVesselOperatorProxy proxy = new VesselOperatorProxy();

                // ref: CT209 & CT105006
                SftDblBankingItem item = new SftDblBankingItem();
                item.searchType = "stsOperation";
                item.vslCallId = m_dblBankingVslCallId;
                item.nextCalCallId = txtNextJPVC.Text;
                item.cgTpCd = CommonUtility.GetComboboxSelectedValue(cboCargoType);
                item.stsOpTp = CommonUtility.GetComboboxSelectedValue(cboOprMode);
                item.mt = txtActMT.Text;
                item.m3 = txtActM3.Text;
                item.qty = txtActQty.Text;
                item.hatchNo = CommonUtility.GetComboboxSelectedValue(cboHatch);
                item.cmdtCd = txtCommodity.Text;
                item.pkgTpCd = txtPkgType.Text;
                item.stDt = txtStartTime.Text;
                item.endDt = txtEndTime.Text;
                item.hatchDrtCd = CommonUtility.GetComboboxSelectedValue(cboWP);
                item.userId = UserInfo.getInstance().UserId;
                item.CRUD = Constants.WS_INSERT;

                Object[] obj = new Object[] { item };
                DataItemCollection dataCollection = new DataItemCollection();
                dataCollection.collection = obj;

                proxy.processgetSftDblBankingItem(dataCollection);
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

        private void ClearForm()
        {
            //txtNextJPVC.Text = string.Empty;
            cboOprMode.SelectedIndex = -1;
            cboHatch.SelectedIndex = -1;
            cboWP.SelectedIndex = -1;
            txtCommodity.Text = string.Empty;
            txtPkgType.Text = string.Empty;
            txtDocM3.Text = string.Empty;
            txtDocMT.Text = string.Empty;
            txtDocQty.Text = string.Empty;
            txtBalM3.Text = string.Empty;
            txtBalMT.Text = string.Empty;
            txtBalQty.Text = string.Empty;
            txtActM3.Text = string.Empty;
            txtActMT.Text = string.Empty;
            txtActQty.Text = string.Empty;
            CommonUtility.SetDTPValueBlank(txtStartTime);
            CommonUtility.SetDTPValueBlank(txtEndTime);

            this.IsDirty = false;
        }

        private bool Validate()
        {
            // Validate PkgTp
            if (!CommonUtility.IsValidPkgTp(txtPkgType.Text.Trim()))
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0070"));
                txtPkgType.SelectAll();
                txtPkgType.Focus();
                return false;
            }

            // Check if JPVC2 is different from JPVC1
            if (!IsDifferentVessel())
            {
                return false;
            }

            // Check if StartTime <= EndTime
            if (!CommonUtility.CheckDateStartEnd(txtStartTime, txtEndTime))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether next vessel is different from mother vessel.
        /// </summary>
        /// <returns></returns>
        private bool IsDifferentVessel()
        {
            bool result = true;
            if (txtNextJPVC.Text.Equals(m_dblBankingVslCallId))
            {
                CommonUtility.AlertMessage("Second vessel must be different from mother vessel.");
                txtNextJPVC.SelectAll();
                txtNextJPVC.Focus();
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Update document, balance amount and commodity code from Confirmation Slip within operation mode and cargo type.
        /// </summary>
        /// <returns></returns>
        private void UpdateAmntCmdtByOprModeAndCgTp()
        {
            string strCargoType = CommonUtility.GetComboboxSelectedValue(cboCargoType);

            #region Document amount
            _SL_M3 = "0";
            _SL_Mt = "0";
            _SL_Qty = "0";
            _SD_M3 = "0";
            _SD_Mt = "0";
            _SD_Qty = "0";
            if (m_docInfo != null && m_docInfo.list != null)
            {   
                for (int i = 0; i < m_docInfo.list.Length; i++)
                {
                    SftDblBankingItem item = (SftDblBankingItem)m_docInfo.list[i];
                    if (strCargoType.Equals(item.cgTpCd))
                    {
                        if ("SL".Equals(item.cgOptTpCd))
                        {
                            _SL_M3 = item.m3;
                            _SL_Mt = item.mt;
                            _SL_Qty = item.qty;
                        }
                        if ("SD".Equals(item.cgOptTpCd))
                        {
                            _SD_M3 = item.m3;
                            _SD_Mt = item.mt;
                            _SD_Qty = item.qty;
                        }
                    }
                }
            }
            #endregion

            #region Balance amount
            _SL_M3_Bal = 0;
            _SL_Mt_Bal = 0;
            _SL_Qty_Bal = 0;
            _SD_M3_Bal = 0;
            _SD_Mt_Bal = 0;
            _SD_Qty_Bal = 0;
            if (m_balInfo != null && m_balInfo.list != null)
            {
                for (int i = 0; i < m_balInfo.list.Length; i++)
                {
                    SftDblBankingItem item = (SftDblBankingItem)m_balInfo.list[i];
                    if (txtNextJPVC.Text.Equals(item.nextCalCallId))
                    {
                        if ("SL".Equals(item.stsOpTp) && "LD".Equals(CommonUtility.GetComboboxSelectedValue(cboOprMode)))
                        {
                            _SL_M3_Bal += CommonUtility.ParseDouble(item.m3);
                            _SL_Mt_Bal += CommonUtility.ParseDouble(item.mt);
                            _SL_Qty_Bal += CommonUtility.ParseLong(item.qty);
                        }
                        if ("SD".Equals(item.stsOpTp) && "DS".Equals(CommonUtility.GetComboboxSelectedValue(cboOprMode)))
                        {
                            _SD_M3_Bal += CommonUtility.ParseDouble(item.m3);
                            _SD_Mt_Bal += CommonUtility.ParseDouble(item.mt);
                            _SD_Qty_Bal += CommonUtility.ParseLong(item.qty);
                        }
                    }
                }

                _SL_M3_Bal = CommonUtility.ParseDouble(_SL_M3) - _SL_M3_Bal - CommonUtility.ParseDouble(txtActM3.Text);
                _SL_Mt_Bal = CommonUtility.ParseDouble(_SL_Mt) - _SL_Mt_Bal - CommonUtility.ParseDouble(txtActMT.Text);
                _SL_Qty_Bal = CommonUtility.ParseLong(_SL_Qty) - _SL_Qty_Bal - CommonUtility.ParseLong(txtActQty.Text);

                _SD_M3_Bal = CommonUtility.ParseDouble(_SD_M3) - _SD_M3_Bal - CommonUtility.ParseDouble(txtActM3.Text);
                _SD_Mt_Bal = CommonUtility.ParseDouble(_SD_Mt) - _SD_Mt_Bal - CommonUtility.ParseDouble(txtActMT.Text);
                _SD_Qty_Bal = CommonUtility.ParseLong(_SD_Qty) - _SD_Qty_Bal - CommonUtility.ParseLong(txtActQty.Text);
            }
            #endregion

            #region Commodity
            _cmdtCdSL = string.Empty;
            _cmdtCdSD = string.Empty;
            if (m_cmdtInfo != null && m_cmdtInfo.list != null)
            {
                for (int i = 0; i < m_cmdtInfo.list.Length; i++)
                {
                    SftDblBankingItem item = (SftDblBankingItem)m_cmdtInfo.list[i];
                    if (strCargoType.Equals(item.cgTpCd))
                    {
                        if ("SL".Equals(item.cgOptTpCd))
                        {
                            _cmdtCdSL = item.cmdtCd;
                        }
                        if ("SD".Equals(item.cgOptTpCd))
                        {
                            _cmdtCdSD = item.cmdtCd;
                        }
                    }
                }
            }
            #endregion
        }

        private void CboSelectedIndexChanged(object sender, EventArgs e)
        {
            txtDocMT.Text = "0";
            txtDocM3.Text = "0";
            txtDocQty.Text = "0";
            txtBalMT.Text = "0";
            txtBalM3.Text = "0";
            txtBalQty.Text = "0";

            UpdateAmntCmdtByOprModeAndCgTp();

            if (cboOprMode.SelectedIndex > 0 && cboCargoType.SelectedIndex > 0)
            {
                string strOprMode = CommonUtility.GetComboboxSelectedValue(cboOprMode);
                if ("LD".Equals(strOprMode))
                {
                    txtDocMT.Text = string.IsNullOrEmpty(_SL_Mt) ? "0" : _SL_Mt;
                    txtDocM3.Text = string.IsNullOrEmpty(_SL_M3) ? "0" : _SL_M3;
                    txtDocQty.Text = string.IsNullOrEmpty(_SL_Qty) ? "0" : _SL_Qty;

                    txtBalM3.Text = CommonUtility.ToString(_SL_M3_Bal);
                    txtBalMT.Text = CommonUtility.ToString(_SL_Mt_Bal);
                    txtBalQty.Text = CommonUtility.ToString(_SL_Qty_Bal);

                    txtCommodity.Text = _cmdtCdSL;
                }
                if ("DS".Equals(strOprMode))
                {
                    txtDocMT.Text = string.IsNullOrEmpty(_SD_Mt) ? "0" : _SD_Mt;//_SD_Mt;
                    txtDocM3.Text = string.IsNullOrEmpty(_SD_M3) ? "0" : _SD_M3;//_SD_M3;
                    txtDocQty.Text = string.IsNullOrEmpty(_SD_Qty) ? "0" : _SD_Qty;//_SD_Qty;

                    txtBalM3.Text = CommonUtility.ToString(_SD_M3_Bal);
                    txtBalMT.Text = CommonUtility.ToString(_SD_Mt_Bal);
                    txtBalQty.Text = CommonUtility.ToString(_SD_Qty_Bal);

                    txtCommodity.Text = _cmdtCdSD;
                }
            }
        }
    }
}