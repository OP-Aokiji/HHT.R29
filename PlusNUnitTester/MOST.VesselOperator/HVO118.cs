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
    public partial class HVO118 : TForm, IPopupWindow
    {
        private HVO118Parm m_parm;

        public HVO118()
        {
            InitializeComponent();
            this.initialFormSize();

            List<string> controlNames = new List<string>();
            controlNames.Add(cboHatch.Name);
            controlNames.Add(cboWP.Name);
            controlNames.Add(cboStcrDiv.Name);
            controlNames.Add(cboSftTp.Name);
            controlNames.Add(cboNextHatch.Name);
            controlNames.Add(txtPkgType.Name);
            controlNames.Add(txtStartTime.Name);
            controlNames.Add(txtEndTime.Name);
            controlNames.Add(txtMT.Name);
            controlNames.Add(txtM3.Name);
            controlNames.Add(txtQty.Name);
            controlNames.Add(txtRemark.Name);
            this.AddOnChangeHandlerToInputControls(this.Controls, controlNames);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HVO118Parm)parm;
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
                        if (this.validations(this.Controls) && this.Validate())
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
                            if (this.validations(this.Controls) && this.Validate())
                            {
                                ProcessgetSftDblBankingItem();
                                this.DialogResult = System.Windows.Forms.DialogResult.OK;
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
                    HVO119Parm listParm = new HVO119Parm();
                    if (m_parm != null && m_parm.JpvcInfo != null)
                    {
                        listParm.JpvcInfo = m_parm.JpvcInfo;
                    }
                    PopupManager.instance.ShowPopup(new HVO119(), listParm);
                    break;
            }
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Initialize data
                CommonUtility.SetDTPValueBlank(txtStartTime);
                CommonUtility.SetDTPValueBlank(txtEndTime);
                #endregion

                #region HatchNo, Harchdrt
                CommonUtility.SetHatchInfo(cboHatch);
                CommonUtility.SetHatchInfo(cboNextHatch);
                CommonUtility.SetHatchDirectionAPFP(cboWP);
                #endregion

                #region Stevedore / Ship's Crew
                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                SftDblBankingParm parm = new SftDblBankingParm();
                parm.searchType = "HHT_CB_SC";
                ResponseInfo info = proxy.getSftDblBankingList(parm);

                CommonUtility.InitializeCombobox(cboStcrDiv);
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CodeMasterListItem)
                    {
                        CodeMasterListItem item = (CodeMasterListItem)info.list[i];
                        cboStcrDiv.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                    else if (info.list[i] is CodeMasterListItem1)
                    {
                        CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                        cboStcrDiv.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                }
                #endregion

                #region Shifting Type
                // Request Webservice
                ICommonProxy commonProxy = new CommonProxy();
                CommonCodeParm commonParm = new CommonCodeParm();
                commonParm.searchType = "COMM";
                commonParm.lcd = "MT";
                commonParm.divCd = "VSLSHFTTP";
                ResponseInfo commonInfo = commonProxy.getCommonCodeList(commonParm);

                // Display Data
                CommonUtility.InitializeCombobox(cboSftTp);
                for (int i = 0; i < commonInfo.list.Length; i++)
                {
                    if (commonInfo.list[i] is CodeMasterListItem)
                    {
                        CodeMasterListItem item = (CodeMasterListItem)commonInfo.list[i];
                        cboSftTp.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                    else if (commonInfo.list[i] is CodeMasterListItem1)
                    {
                        CodeMasterListItem1 item = (CodeMasterListItem1)commonInfo.list[i];
                        cboSftTp.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
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

        private bool ProcessgetSftDblBankingItem()
        {
            bool result = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                IVesselOperatorProxy proxy = new VesselOperatorProxy();

                // ref: CT209 & CT105008
                SftDblBankingItem item = new SftDblBankingItem();
                item.searchType = "cargoShifting";
                item.vslCallId = m_parm.JpvcInfo.Jpvc;
                item.hatchNo = CommonUtility.GetComboboxSelectedValue(cboHatch);
                item.stcrDiv = CommonUtility.GetComboboxSelectedValue(cboStcrDiv);
                item.hatchDrtCd = CommonUtility.GetComboboxSelectedValue(cboWP);
                item.nextHatchNo = CommonUtility.GetComboboxSelectedValue(cboNextHatch);
                item.sftTp = CommonUtility.GetComboboxSelectedValue(cboSftTp);
                item.pkgTpCd = txtPkgType.Text;
                item.stDt = txtStartTime.Text;
                item.endDt = txtEndTime.Text;
                item.mt = txtMT.Text;
                item.m3 = txtM3.Text;
                item.qty = txtQty.Text;
                item.rmk = txtRemark.Text;
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
            txtPkgType.Text = "";
            txtMT.Text = "";
            txtM3.Text = "";
            txtQty.Text = "";
            txtRemark.Text = "";
            cboHatch.SelectedIndex = 0;
            cboNextHatch.SelectedIndex = 0;
            cboSftTp.SelectedIndex = 0;
            cboStcrDiv.SelectedIndex = 0;
            cboWP.SelectedIndex = 0;
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

            // Check if StartTime <= EndTime
            if (!CommonUtility.CheckDateStartEnd(txtStartTime, txtEndTime))
            {
                return false;
            }

            return true;
        }

        private void cboSftTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
                S1	S1(Same Level) 
                S2	S2(Diff. Level) 
                S3	S3(Hatch to Hatch) 
                S4	S4(Hatch to Wharf)
            */
            cboNextHatch.Enabled = false;
            cboNextHatch.isMandatory = false;
            txtPkgType.isMandatory = false;
            txtPkgType.Enabled = true;
            btnF1.Enabled = true;

            string strSftTp = CommonUtility.GetComboboxSelectedValue(cboSftTp);
            if ("S3".Equals(strSftTp))
            {
                cboNextHatch.Enabled = true;
                cboNextHatch.isMandatory = true;
            }
            else if ("S4".Equals(strSftTp))
            {   
                txtPkgType.isMandatory = true;
            }
        }

    }
}