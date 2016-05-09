using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using Framework.Common.PopupManager;
using Framework.Common.Constants;
using MOST.Common.Utility;
using MOST.Common;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.VesselOperator.Parm;
using MOST.VesselOperator.Result;
using MOST.Client.Proxy.CommonProxy;
using MOST.Client.Proxy.VesselOperatorProxy;
using Framework.Controls.UserControls;
using Framework.Common.ExceptionHandler;
using Framework.Service.Provider.WebService.Provider;
using System.Runtime.InteropServices;
using System.Threading;

namespace MOST.VesselOperator
{
    public partial class HVO102 : TForm, IPopupWindow
    {
        private bool m_isValidJPVC;
        private SearchJPVCResult m_jpvcResult;
        private VesselScheduleItem m_item;
        private bool m_isAtbChanged;
        private ArrayList m_arrBerthItems;
        private const string YES = "Y";
        private const string NO = "N";

        public HVO102()
        {
            InitializeComponent();
            this.initialFormSize();

            this.InitializeData();
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            MOST.Common.CommonParm.SearchJPVCParm vsParm = (MOST.Common.CommonParm.SearchJPVCParm)parm;
            _flagTabChange = 0;
            try
            {
                if (!string.IsNullOrEmpty(vsParm.Jpvc))
                {
                    Cursor.Current = Cursors.WaitCursor;
                    new Thread(new ThreadStart(delegate()
                    {
                        GetVesselScheduleDetail(vsParm.Jpvc);
                    })).Start();
                    
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

            this.ShowDialog();

            return m_jpvcResult;
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Initialize variables
                m_isValidJPVC = false;
                m_jpvcResult = new SearchJPVCResult();
                m_arrBerthItems = new ArrayList();
                #endregion

                #region For checking if form is dirty or not
                // For checking if form is dirty or not
                List<string> controlNames = new List<string>();
                controlNames.Add(txtWharfS.Name);
                controlNames.Add(txtWharfE.Name);
                controlNames.Add(txtATU.Name);
                controlNames.Add(chkPilotATU.Name);
                controlNames.Add(txtTugATU.Name);
                controlNames.Add(txtMooringATU.Name);
                controlNames.Add(txtATB.Name);
                controlNames.Add(chkPilotATB.Name);
                controlNames.Add(txtTugATB.Name);
                controlNames.Add(txtMooringATB.Name);
                controlNames.Add(cboBerthLoc.Name);
                this.AddOnChangeHandlerToInputControls(this.Controls, controlNames);
                #endregion

                #region ATB, ATU
                CommonUtility.SetDTPValueBlank(txtATB);
                CommonUtility.SetDTPValueBlank(txtATU);
                #endregion

                #region Get Berth Info


                new Thread(new ThreadStart(delegate()
                {
                    setBerthLoc();
                }))
                        .Start();
                #endregion
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

        private void setBerthLoc()
        {
            Action<object> proc = delegate(object state)
                {
                    IVesselOperatorProxy proxy = new VesselOperatorProxy();
                    VesselScheduleParm parm = new VesselScheduleParm();
                    ResponseInfo info = proxy.getBerthInfoList(parm);
                    // Berth Info
                    CommonUtility.InitializeCombobox(cboBerthLoc, "Select");
                    if (info != null && info.list.Length > 0)
                    {
                        for (int i = 0; i < info.list.Length; i++)
                        {
                            if (info.list[i] is BerthInfoItem)
                            {
                                BerthInfoItem item = (BerthInfoItem)info.list[i];
                                cboBerthLoc.Items.Add(new ComboboxValueDescriptionPair(item.berthCd, item.berthCd));
                                m_arrBerthItems.Add(item);
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

        private void ClearCtrlValues()
        {
            txtName.Text = "";
            txtLOA.Text = "0";
            txtWharfS.Text = "0";
            txtWharfE.Text = "0";
            txtETB.Text = "";
            txtETW.Text = "";
            txtETC.Text = "";
            txtETU.Text = "";
            txtATW.Text = "";
            txtATC.Text = "";
            CommonUtility.SetDTPValueBlank(txtATB);
            CommonUtility.SetDTPValueBlank(txtATU);
            cboBerthLoc.SelectedIndex = -1;
        }

        private void GetVesselScheduleDetail(string argJPVC)
        {
            try
            {
                if (!string.IsNullOrEmpty(argJPVC))
                {
                    Action<object> proc = delegate(object state)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        
                        ClearCtrlValues();

                        #region Request Webservice
                        IVesselOperatorProxy proxy = new VesselOperatorProxy();
                        VesselScheduleParm parm = new VesselScheduleParm();
                        parm.jpvcNo = argJPVC;

                        ResponseInfo info = proxy.getVesselScheduleListDetail(parm);
                        #endregion

                        #region Display Data
                        if (info != null && info.list.Length > 0)
                        {
                            // Vessel Schedule
                            if (info.list[0] is VesselScheduleItem)
                            {
                                // For handling event in case it occurs: KeyPress -> LostFocus
                                m_isValidJPVC = true;

                                m_item = (VesselScheduleItem)info.list[0];
                                txtJPVC.Text = m_item.jpvcNo;
                                txtName.Text = m_item.vslNm;
                                CommonUtility.SetComboboxSelectedItem(cboBerthLoc, m_item.berthLoc);
                                txtLOA.Text = m_item.loa;
                                txtWharfS.Text = m_item.wharfMarkFrom;
                                txtWharfE.Text = m_item.wharfMarkTo;
                                txtETB.Text = m_item.etb;
                                txtCurATB.Text = m_item.curAtb;
                                CommonUtility.SetDTPValueDMYHM(txtATB, m_item.atb);

                                //txtATB.Value = DateTime.ParseExact(m_item.atb, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.CreateSpecificCulture("ms-MY"));

                                txtETW.Text = m_item.etw;
                                txtATW.Text = m_item.atw;
                                txtETC.Text = m_item.etc;
                                txtATC.Text = m_item.atc;
                                CommonUtility.SetDTPValueDMYHM(txtATU, m_item.atu);
                                txtTugATB.Text = m_item.atbTug;
                                txtTugATU.Text = m_item.atuTug;
                                txtMooringATB.Text = m_item.atbMooring;
                                txtMooringATU.Text = m_item.atuMooring;
                                chkPilotATB.Checked = YES.Equals(m_item.atbPilot) ? true : false;
                                chkPilotATU.Checked = YES.Equals(m_item.atuPilot) ? true : false;

                                m_jpvcResult.Jpvc = m_item.jpvcNo;
                                m_jpvcResult.VesselName = m_item.vslNm;
                                m_jpvcResult.WharfStart = m_item.wharfMarkFrom;
                                m_jpvcResult.WharfEnd = m_item.wharfMarkTo;
                                m_jpvcResult.BerthLocation = m_item.berthLoc;
                                m_jpvcResult.VslTp = m_item.vslTp;
                                m_jpvcResult.VslTpNm = m_item.vslTpNm;
                                m_jpvcResult.Loa = m_item.loa;
                                m_jpvcResult.Etb = m_item.etb;
                                m_jpvcResult.Etw = m_item.etw;
                                m_jpvcResult.Etc = m_item.etc;
                                m_jpvcResult.Etu = m_item.etu;
                                m_jpvcResult.Atb = m_item.atb;
                                m_jpvcResult.CurAtb = m_item.curAtb;
                                m_jpvcResult.Atw = m_item.atw;
                                m_jpvcResult.Atc = m_item.atc;
                                m_jpvcResult.Atu = m_item.atu;
                                m_jpvcResult.PurpCall = m_item.purpCall;
                                m_jpvcResult.PurpCallCd = m_item.purpCallCd;
                                m_jpvcResult.DeprSaId = m_item.deprSaId;
                                m_jpvcResult.ArrvSaId = m_item.arrvSaId;

                            }
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
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                m_isAtbChanged = false;
                Cursor.Current = Cursors.Default;
            }
        }

        private bool Validate()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (!ValidateJPVC())
                {
                    return false;
                }

                if (!ValidateDatetime())
                {
                    return false;
                }

                if (!ValidateWharfFromTo())
                {
                    return false;
                }

                if (!ValidateAtbEditable())
                {
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

        private bool ValidateWharfFromTo()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (m_item != null && !Constants.VSLTYPECD_LIQUID.Equals(m_item.vslTp))
                {
                    double wharfFm = CommonUtility.ParseDouble(txtWharfS.Text);
                    double wharfTo = CommonUtility.ParseDouble(txtWharfE.Text);

                    // Wharf Mark End must be greater than Wharf Mark Start
                    if (wharfTo < wharfFm)
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO102_0004"));
                        txtWharfE.Focus();
                        txtWharfE.SelectAll();
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

        private bool ValidateAtbEditable()
        {
            bool result = true;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (m_isAtbChanged)
                {
                    // Check if there is ATU
                    if (m_item != null && !string.IsNullOrEmpty(m_item.atu))
                    {
                        CommonUtility.AlertMessage("Cannot change ATB because it exists ATU.");
                        return false;
                    }

                    // Check if there is ATW/ATC
                    if (!string.IsNullOrEmpty(txtATW.Text) || !string.IsNullOrEmpty(txtATC.Text))
                    {
                        CommonUtility.AlertMessage("Cannot change ATB because it exists ATW/ATC.");
                        return false;
                    }

                    // Check if there is Shifting/Double Banking information
                    string[] cols = { txtJPVC.Text, txtATB.Text };
                    result = CommonUtility.ValidateFunc("vesselAtbEditable", cols);
                    if (!result)
                    {
                        //Msg: Cannot change ATB because it exists Shifting or Double Banking info.
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO110"));
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
            return result;
        }

        private bool ValidateJPVC()
        {
            bool result = true;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (!string.IsNullOrEmpty(txtJPVC.Text) && m_isValidJPVC == false)
                {
                    if (!CommonUtility.IsValidJPVC(txtJPVC.Text, ref m_jpvcResult))
                    {
                        m_isValidJPVC = false;
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0027"));
                        txtJPVC.SelectAll();
                        txtJPVC.Focus();
                        result = false;
                    }
                    else
                    {
                        m_isValidJPVC = true;
                        txtJPVC.Text = m_jpvcResult.Jpvc;
                        // Get vessel schedule detail (including Purpose of Call)
                        GetVesselScheduleDetail(m_jpvcResult.Jpvc);
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
            return result;
        }

        private bool ValidateDatetime()
        {
            // ATB <= ATW <= ATC <= ATU
            bool atbEmpty = true;
            bool atwEmpty = true;
            bool atcEmpty = true;
            bool atuEmpty = true;
            DateTime dtATB = DateTime.MinValue;
            DateTime dtATW = DateTime.MinValue;
            DateTime dtATC = DateTime.MinValue;
            DateTime dtATU = DateTime.MinValue;
            if (!string.IsNullOrEmpty(txtATB.Text))
            {
                atbEmpty = false;
                dtATB = txtATB.Value;
            }
            if (!string.IsNullOrEmpty(txtATW.Text))
            {
                atwEmpty = false;
                dtATW = CommonUtility.ParseYMDHM(txtATW.Text);
            }
            if (!string.IsNullOrEmpty(txtATC.Text))
            {
                atcEmpty = false;
                dtATC = CommonUtility.ParseYMDHM(txtATC.Text);
            }
            if (!string.IsNullOrEmpty(txtATU.Text))
            {
                atuEmpty = false;
                dtATU = txtATU.Value;
            }

            if (!atbEmpty)
            {
                if (!atwEmpty && dtATB.CompareTo(dtATW) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0042"));
                    txtATB.Focus();
                    return false;
                }

                if (!atcEmpty && dtATB.CompareTo(dtATC) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0042"));
                    txtATB.Focus();
                    return false;
                }

                if (!atuEmpty && dtATB.CompareTo(dtATU) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0042"));
                    txtATB.Focus();
                    return false;
                }
            }

            if (!atwEmpty)
            {
                if (!atcEmpty && dtATW.CompareTo(dtATC) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0042"));
                    txtATW.Focus();
                    return false;
                }

                if (!atuEmpty && dtATW.CompareTo(dtATU) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0042"));
                    txtATW.Focus();
                    return false;
                }
            }

            if (!atcEmpty)
            {
                if (!atuEmpty && dtATC.CompareTo(dtATU) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0042"));
                    txtATC.Focus();
                    return false;
                }
            }

            // ETB <= ETW <= ETC <= ETU
            //            bool etbEmpty = true;
            //            bool etwEmpty = true;
            //            bool etcEmpty = true;
            //            bool etuEmpty = true;
            //            DateTime dtETB = DateTime.MinValue;
            //            DateTime dtETW = DateTime.MinValue;
            //            DateTime dtETC = DateTime.MinValue;
            //            DateTime dtETU = DateTime.MinValue;
            //            if (!string.IsNullOrEmpty(txtETB.Text))
            //            {
            //                etbEmpty = false;
            //                dtETB = CommonUtility.ParseYMDHM(txtETB.Text);
            //            }
            //            if (!string.IsNullOrEmpty(txtETW.Text))
            //            {
            //                etwEmpty = false;
            //                dtETW = CommonUtility.ParseYMDHM(txtETW.Text);
            //            }
            //            if (!string.IsNullOrEmpty(txtETC.Text))
            //            {
            //                etcEmpty = false;
            //                dtETC = CommonUtility.ParseYMDHM(txtETC.Text);
            //            }
            //            if (!string.IsNullOrEmpty(txtETU.Text))
            //            {
            //                etuEmpty = false;
            //                dtETU = CommonUtility.ParseYMDHM(txtETU.Text);
            //            }
            //
            //            if (!etbEmpty)
            //            {
            //                if (!etwEmpty && dtETB.CompareTo(dtETW) > 0)
            //                {
            //                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0043"));
            //                    txtETB.Focus();
            //                    return false;
            //                }
            //
            //                if (!etcEmpty && dtETB.CompareTo(dtETC) > 0)
            //                {
            //                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0043"));
            //                    txtETB.Focus();
            //                    return false;
            //                }
            //
            //                if (!etuEmpty && dtETB.CompareTo(dtETU) > 0)
            //                {
            //                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0043"));
            //                    txtETB.Focus();
            //                    return false;
            //                }
            //            }
            //
            //            if (!etwEmpty)
            //            {
            //                if (!etcEmpty && dtETW.CompareTo(dtETC) > 0)
            //                {
            //                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0043"));
            //                    txtETW.Focus();
            //                    return false;
            //                }
            //
            //                if (!etuEmpty && dtETW.CompareTo(dtETU) > 0)
            //                {
            //                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0043"));
            //                    txtETW.Focus();
            //                    return false;
            //                }
            //            }
            //
            //            if (!etcEmpty)
            //            {
            //                if (!etuEmpty && dtETC.CompareTo(dtETU) > 0)
            //                {
            //                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0043"));
            //                    txtETC.Focus();
            //                    return false;
            //                }
            //            }

            return true;
        }

        private void UpdateVesselDetailItem()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                tabControl.SelectedIndex = 1;

                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                VesselScheduleItem item = m_item;
                item.atu = txtATU.Text;
                item.atb = txtATB.Text;
                item.atbTug = txtTugATB.Text;
                item.atuTug = txtTugATU.Text;
                item.atbMooring = txtMooringATB.Text;
                item.atuMooring = txtMooringATU.Text;
                item.atbPilot = chkPilotATB.Checked ? YES : NO;
                item.atuPilot = chkPilotATU.Checked ? YES : NO;
                item.wharfMarkFrom = string.IsNullOrEmpty(txtWharfS.Text) ? "0" : txtWharfS.Text;
                item.wharfMarkTo = string.IsNullOrEmpty(txtWharfE.Text) ? "0" : txtWharfE.Text;
                item.berthLoc = CommonUtility.GetComboboxSelectedValue(cboBerthLoc);
                item.CRUD = Constants.WS_UPDATE;

                Object[] obj = new Object[] { item };
                DataItemCollection dataCollection = new DataItemCollection();
                dataCollection.collection = obj;

                proxy.updateVesselDetailItem(dataCollection);



            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                tabControl.SelectedIndex = 0;
                Cursor.Current = Cursors.Default;
            }
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnOk":
                    if (this.IsDirty)
                    {
                        if (this.validations(this.Controls) && Validate())
                        {
                            if (!validationATB()) return;

                            if (!checkValidation4Atu()) return;

                            UpdateVesselDetailItem();
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
                            if (this.validations(this.Controls) && Validate())
                            {
                                if (!validationATB()) return;

                                if (!checkValidation4Atu()) return;

                                UpdateVesselDetailItem();
                                this.Close();
                            }
                        }
                        else if (dr == DialogResult.No)
                        {
                            m_jpvcResult = null;
                            this.Close();
                        }
                    }
                    else
                    {
                        m_jpvcResult = null;
                        this.Close();
                    }
                    break;

                case "btnF1":
                    MOST.Common.CommonParm.SearchJPVCParm jpvcParm = new MOST.Common.CommonParm.SearchJPVCParm();
                    jpvcParm.Jpvc = txtJPVC.Text;
                    jpvcParm.IsWHChecker = "VSL";
                    SearchJPVCResult jpvcResultTmp = (SearchJPVCResult)PopupManager.instance.ShowPopup(new HCM101(), jpvcParm);
                    if (jpvcResultTmp != null)
                    {
                        m_isValidJPVC = true;
                        txtJPVC.Text = jpvcResultTmp.Jpvc;
                        m_jpvcResult = jpvcResultTmp;
                        GetVesselScheduleDetail(m_jpvcResult.Jpvc);
                    }
                    break;

                case "btnTopclean":
                    HVO121Parm topclnParm = new HVO121Parm();
                    topclnParm.VslCallId = txtJPVC.Text;
                    PopupManager.instance.ShowPopup(new HVO121(), topclnParm);
                    break;
            }
        }
        private int _flagTabChange = 0; // Vessel, 1: Schedule

        private bool validationATB()
        {
            if (_flagTabChange == 0)
            {
                if (m_item.atb.Length <= 0 || string.IsNullOrEmpty(m_item.atb))
                {
                    CommonUtility.AlertMessage("Please update ATB for this vessel first !", "WARNING !!!");
                    //txtATU.Text = "";
                    return false;
                }
            }
            else
            {
                if (txtATB.Text.Length <= 0 || string.IsNullOrEmpty(txtATB.Text))
                {
                    CommonUtility.AlertMessage("Please update ATB for this vessel first !", "WARNING !!!");
                    //txtATU.Text = "";
                    return false;
                }
            }
            return true;
        }

        private bool checkValidation4Atu()
        {
                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                BerthPlanParm parm = new BerthPlanParm();
                parm.vslCallId = txtJPVC.Text;
                parm.etb = txtETB.Text;
                parm.berthLoc = CommonUtility.GetComboboxSelectedValue(cboBerthLoc);

                ResponseInfo info = proxy.checkValidation4Atu(parm);

                if (info != null && info.list.Length > 0)
                {
                    if (info.list[0] is BerthPlanItem)
                    {
                        CommonUtility.AlertMessage("Please update ATU for vessel \"" + ((BerthPlanItem)info.list[0]).jpvcNo + "\" first.");
                        return false;
                    }
                }
                return true;
        }

        private void txtJPVC_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJPVC.Text) && !m_isValidJPVC)
            {
                GetVesselScheduleDetail(txtJPVC.Text);
            }
        }

        private void txtJPVC_KeyPress(object sender, KeyPressEventArgs e)
        {
            m_isValidJPVC = false;
            ClearCtrlValues();

            // if key = Enter then get vessel schedule detail
            if (e.KeyChar.Equals((char)Keys.Enter))
            {
                GetVesselScheduleDetail(txtJPVC.Text);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = false;
            if (this.IsClosedByUser)
            {
                if (this.IsDirty)
                {
                    DialogResult dr = CommonUtility.ConfirmMsgSaveChances();
                    if (dr == DialogResult.Yes)
                    {
                        if (this.validations(this.Controls) && ValidateJPVC() && ValidateDatetime())
                        {
                            UpdateVesselDetailItem();
                        }
                    }
                    else if (dr == DialogResult.No)
                    {
                        m_jpvcResult = null;
                    }
                    else if (dr == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        this.IsClosedByUser = false;
                    }
                }
                else
                {
                    m_jpvcResult = null;
                }
            }
            base.OnClosing(e);
        }

        private void txtATB_ValueChanged(object sender, EventArgs e)
        {
            m_isAtbChanged = true;
            if (string.IsNullOrEmpty(txtCurATB.Text))
            {
                txtCurATB.Text = txtATB.Text;
            }
        }

        private void txtWharfS_LostFocus(object sender, EventArgs e)
        {
            double motherVslLoa = CommonUtility.ParseDouble(txtLOA.Text);
            double wharfFm = CommonUtility.ParseDouble(txtWharfS.Text);
            double wharfTo = wharfFm + motherVslLoa;

            txtWharfE.Text = wharfTo.ToString();
        }

        private void cboBerthLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pstSta = "0";
            string pstEnd = "0";
            int currIndex = cboBerthLoc.SelectedIndex;
            if (m_arrBerthItems != null && 0 < currIndex && currIndex <= m_arrBerthItems.Count)
            {
                BerthInfoItem item = (BerthInfoItem)m_arrBerthItems[currIndex - 1];
                pstSta = item.pstSta;
                pstEnd = item.pstEnd;
            }
            txtBerthWharfS.Text = pstSta;
            txtBerthWharfE.Text = pstEnd;
        }

        private void txtJPVC_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtATU_LostFocus(object sender, EventArgs e)
        {
            validationATB();
        }

        private void txtATW_LostFocus(object sender, EventArgs e)
        {
            validationATB();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            _flagTabChange = 1;
        }
    }
}