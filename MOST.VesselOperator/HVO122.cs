using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using Framework.Common.Constants;
using Framework.Common.PopupManager;
using MOST.Common.Utility;
using MOST.VesselOperator.Parm;
using MOST.VesselOperator.Result;
using MOST.Client.Proxy.CommonProxy;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.Client.Proxy.VesselOperatorProxy;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.VesselOperator
{
    public partial class HVO122 : TForm, IPopupWindow
    {
        private SearchJPVCResult m_jpvcResult;
        private bool m_isValidJPVC;

        public HVO122()
        {
            InitializeComponent();
            this.initialFormSize();

            m_isValidJPVC = false;

            List<string> controlNames = new List<string>();
            controlNames.Add(txtLOA.Name);
            controlNames.Add(txtATB.Name);
            controlNames.Add(txtATU.Name);
            controlNames.Add(txtATW.Name);
            controlNames.Add(txtATC.Name);
            controlNames.Add(txtJPVC.Name);
            this.AddOnChangeHandlerToInputControls(this.Controls, controlNames);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            HVO122Parm hvo122Parm = (HVO122Parm)parm;
            if (hvo122Parm != null && hvo122Parm.JpvcInfo != null)
            {
                m_isValidJPVC = true;
                m_jpvcResult = hvo122Parm.JpvcInfo;
                InitializeData(hvo122Parm.BankingType);
            }
            this.ShowDialog();
            return m_jpvcResult;
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnF1":
                    MOST.Common.CommonParm.SearchJPVCParm jpvc2Parm = new MOST.Common.CommonParm.SearchJPVCParm();
                    jpvc2Parm.Jpvc = txtJPVC.Text;
                    MOST.Common.CommonResult.SearchJPVCResult jpvc2Result = (SearchJPVCResult)PopupManager.instance.ShowPopup(new MOST.Common.HCM101(), jpvc2Parm);
                    if (jpvc2Result != null)
                    {
                        m_isValidJPVC = true;
                        m_jpvcResult = jpvc2Result;
                        txtJPVC.Text = m_jpvcResult.Jpvc;
                        txtLOA.Text = m_jpvcResult.Loa;
                        CommonUtility.SetDTPValueDMYHM(txtATB, m_jpvcResult.Atb);
                        CommonUtility.SetDTPValueDMYHM(txtATU, m_jpvcResult.Atu);
                        CommonUtility.SetDTPValueDMYHM(txtATW, m_jpvcResult.Atw);
                        CommonUtility.SetDTPValueDMYHM(txtATC, m_jpvcResult.Atc);
                    }
                    break;

                case "btnOk":
                    if (this.validations(this.Controls) && ValidateJPVC() && ValidateDatetime())
                    {
                        ReturnJPVCInfo();
                        this.Close();
                    }
                    break;

                case "btnCancel":
                    if (this.IsDirty)
                    {
                        DialogResult dr = CommonUtility.ConfirmMsgSaveChances();
                        if (dr == DialogResult.Yes)
                        {
                            if (this.validations(this.Controls) && ValidateJPVC() && ValidateDatetime())
                            {
                                ReturnJPVCInfo();
                                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                                this.Close();
                            }
                        }
                        else if (dr == DialogResult.No)
                        {
                            m_jpvcResult = null;
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            this.Close();
                        }
                    }
                    else
                    {
                        m_jpvcResult = null;
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                        this.Close();
                    }
                    break;
            }
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
            ClearData();

            // if key = Enter then get vessel schedule detail
            if (e.KeyChar.Equals((char)Keys.Enter))
            {
                GetVesselScheduleDetail(txtJPVC.Text);
            }
        }

        private void GetVesselScheduleDetail(string argJPVC)
        {
            try
            {
                if (!string.IsNullOrEmpty(argJPVC))
                {
                    Cursor.Current = Cursors.WaitCursor;

                    ClearData();

                    #region Request Webservice
                    IVesselOperatorProxy proxy = new VesselOperatorProxy();
                    VesselScheduleParm parm = new VesselScheduleParm();
                    parm.jpvcNo = argJPVC;
                    ResponseInfo info = proxy.getVesselScheduleListDetail(parm);
                    #endregion

                    #region Display Data
                    if (info != null && info.list.Length > 0)
                    {
                        // ResponseInfo info: VesselScheduleItem, BerthInfoItem, BerthInfoItem, ....
                        // Vessel Schedule
                        if (info.list[0] is VesselScheduleItem)
                        {
                            // For handling event in case it occurs: KeyPress -> LostFocus
                            m_isValidJPVC = true;

                            VesselScheduleItem item = (VesselScheduleItem)info.list[0];
                            txtJPVC.Text = item.jpvcNo;
                            txtLOA.Text = item.loa;
                            CommonUtility.SetDTPValueDMYHM(txtATB, item.atb);
                            CommonUtility.SetDTPValueDMYHM(txtATW, item.atw);
                            CommonUtility.SetDTPValueDMYHM(txtATC, item.atc);
                            CommonUtility.SetDTPValueDMYHM(txtATU, item.atu);
                        }
                    }
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

        private void InitializeData(string strBType)
        {
            ClearData();
            if (m_jpvcResult != null)
            {
                txtJPVC.Text = m_jpvcResult.Jpvc;

                switch (strBType)
                {
                    // 'Barge/Tug'
                    case "BT":
                        txtLOA.Enabled = true;
                        txtATB.Enabled = true;
                        txtATW.Enabled = true;
                        txtATC.Enabled = true;
                        txtATU.Enabled = true;
                        txtLOA.isMandatory = true;
                        txtATB.isMandatory = true;
                        txtATW.isMandatory = true;
                        txtATC.isMandatory = true;
                        txtATU.isMandatory = true;
                        break;

                    // 'Tug Replacement'
                    case "TR": 
                        txtLOA.Enabled = true;
                        txtATB.Enabled = true;
                        txtLOA.isMandatory = true;
                        txtATB.isMandatory = true;
                        break;
                }

                txtLOA.Text = m_jpvcResult.Loa;
                CommonUtility.SetDTPValueDMYHM(txtATB, m_jpvcResult.Atb);
                CommonUtility.SetDTPValueDMYHM(txtATU, m_jpvcResult.Atu);
                CommonUtility.SetDTPValueDMYHM(txtATW, m_jpvcResult.Atw);
                CommonUtility.SetDTPValueDMYHM(txtATC, m_jpvcResult.Atc);
            }
        }

        private void ClearData()
        {
            txtLOA.Text = "0";
            CommonUtility.SetDTPValueBlank(txtATB);
            CommonUtility.SetDTPValueBlank(txtATU);
            CommonUtility.SetDTPValueBlank(txtATW);
            CommonUtility.SetDTPValueBlank(txtATC);
        }

        private void ReturnJPVCInfo()
        {
            m_jpvcResult.Jpvc = txtJPVC.Text;
            m_jpvcResult.Loa = txtLOA.Text;
            m_jpvcResult.Atb = txtATB.Text;
            m_jpvcResult.Atw = txtATW.Text;
            m_jpvcResult.Atc = txtATC.Text;
            m_jpvcResult.Atu = txtATU.Text;
        }

        private bool ValidateDatetime()
        {
            // ATB <= ATW <= ATC <= ATU
            if (!CommonUtility.ValidateDateOrder(txtATB, txtATW, txtATC, txtATU))
            {
                return false;
            }
            return true;
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
    }
}