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
using MOST.Common;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using Framework.Service.Provider.WebService.Provider;
using MOST.Client.Proxy.VesselOperatorProxy;
using Framework.Common.ResourceManager;

namespace MOST.VesselOperator
{
    public partial class HVO106001 : TForm, IPopupWindow
    {
        private string m_opeTp;
        private int m_workingStatus;
        private HVO106001Parm m_parm = null;
        private HVO106001Result m_result = null;

        public HVO106001(string opeTp, int workingStatus)
        {
            m_opeTp = opeTp;
            m_workingStatus = workingStatus;
            InitializeComponent();
            this.initialFormSize();

            // For checking if form is dirty or not
            List<string> controlNames = new List<string>();
            controlNames.Add(txtHoseOnDt.Name);
            controlNames.Add(txtHoseOffDt.Name);
            controlNames.Add(txtCommenceDt.Name);
            controlNames.Add(txtCompleteDt.Name);
            this.AddOnChangeHandlerToInputControls(this.Controls, controlNames);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HVO106001Parm)parm;
            DisplayData();
            this.ShowDialog();
            return m_result;
        }

        private void DisplayData()
        {
            // Operation type name
            string opeTpNm = string.Empty;
            switch (m_opeTp)
            {
                case HVO113.CONST_OPETP_GENERAL:
                    opeTpNm = "Load/Discharge";
            	    break;
                case HVO113.CONST_OPETP_TRANSHIPMENT:
                    opeTpNm = "Transhipment";
                    break;
                case HVO113.CONST_OPETP_STS:
                    opeTpNm = "Ship to ship";
                    break;
            }
            txtOpeTpNm.Text = opeTpNm;

            // Set hose time (time, prev time)
            txtPrevHoseOff.Text = string.Empty;
            txtPrevComplete.Text = string.Empty;
            txtPrevCommence.Text = string.Empty;
            txtPrevHoseOn.Text = string.Empty;
            /***** DEL - 22/06/2010 - Always enable Complete/Hose Off (user's requirement) ****
            txtHoseOffDt.Enabled = false;
            txtCompleteDt.Enabled = false;
            ***********************************************************************************/
            txtCommenceDt.Enabled = false;
            txtHoseOnDt.Enabled = false;
            CommonUtility.SetDTPValueBlank(txtHoseOnDt);
            CommonUtility.SetDTPValueBlank(txtCommenceDt);
            CommonUtility.SetDTPValueBlank(txtCompleteDt);
            CommonUtility.SetDTPValueBlank(txtHoseOffDt);

            // JPVC
            if (m_parm != null && m_parm.JpvcInfo != null)
            {
                txtJPVC.Text = m_parm.JpvcInfo.Jpvc;
            }
            else
            {
                txtJPVC.Text = string.Empty;
                return;
            }

            // Mode UPDATE
            if (m_workingStatus == Constants.MODE_UPDATE)
            {
                int seqOrder = GetHosetimeSeqOrder(m_parm.Seq,
                                                    m_parm.PrevHoseOnDt,
                                                    m_parm.PrevCommenceDt,
                                                    m_parm.PrevCompleteDt,
                                                    m_parm.PrevHoseOffDt);

                // Enalbe 1 hose time in case:
                //  Its upcoming hose times were not inputted (based on seqOrder)
                //  AND - exist hose time
                //      - OR exist original hose time
                //      - OR exist prev hose time
                if (!string.IsNullOrEmpty(m_parm.OrgHoseOnDt) || 
                    !string.IsNullOrEmpty(m_parm.HoseOnDt) || 
                    string.IsNullOrEmpty(m_parm.PrevHoseOnDt))
                {
                    txtHoseOnDt.Enabled = true;
                    CommonUtility.SetDTPValueDMYHM(txtHoseOnDt, m_parm.HoseOnDt);
                }
                if (seqOrder >= 1 &&
                    (!string.IsNullOrEmpty(m_parm.OrgCommenceDt) || 
                    !string.IsNullOrEmpty(m_parm.CommenceDt) ||
                    (string.IsNullOrEmpty(m_parm.PrevCommenceDt) && txtHoseOnDt.Enabled)))
                {
                    txtCommenceDt.Enabled = true;
                    CommonUtility.SetDTPValueDMYHM(txtCommenceDt, m_parm.CommenceDt);
                }
                /***** DEL - 22/06/2010 - Always enable Complete/Hose Off (user's requirement) ****
                if (seqOrder >= 2 &&
                    (!string.IsNullOrEmpty(m_parm.OrgCompleteDt) || 
                    !string.IsNullOrEmpty(m_parm.CompleteDt) ||
                    (string.IsNullOrEmpty(m_parm.PrevCompleteDt) && txtCommenceDt.Enabled)))
                {
                    txtCompleteDt.Enabled = true;
                    CommonUtility.SetDTPValueDMYHM(txtCompleteDt, m_parm.CompleteDt);
                }
                if (seqOrder >= 3 &&
                    (!string.IsNullOrEmpty(m_parm.OrgHoseOffDt) || 
                    !string.IsNullOrEmpty(m_parm.HoseOffDt) ||
                    (string.IsNullOrEmpty(m_parm.PrevHoseOffDt) && txtCompleteDt.Enabled)))
                {
                    txtHoseOffDt.Enabled = true;
                    CommonUtility.SetDTPValueDMYHM(txtHoseOffDt, m_parm.HoseOffDt);
                }
                ***********************************************************************************/
                /***** ADD - 22/06/2010 - Always enable Complete/Hose Off (user's requirement) ****/
                if (!string.IsNullOrEmpty(m_parm.OrgCompleteDt) || 
                    !string.IsNullOrEmpty(m_parm.CompleteDt))
                {
                    CommonUtility.SetDTPValueDMYHM(txtCompleteDt, m_parm.CompleteDt);
                }
                if (!string.IsNullOrEmpty(m_parm.OrgHoseOffDt) || 
                    !string.IsNullOrEmpty(m_parm.HoseOffDt))
                {
                    CommonUtility.SetDTPValueDMYHM(txtHoseOffDt, m_parm.HoseOffDt);
                }
                /************************************************************************************/

                // Only display previous hose time (compared to the current row)
                if (txtCommenceDt.Enabled)
                {
                    if (!txtHoseOnDt.Enabled)
                    {
                        txtPrevHoseOn.Text = m_parm.PrevHoseOnDt;
                    }
                }

                /***** DEL - 22/06/2010 - Always enable Complete/Hose Off (user's requirement) ****
                if (txtCompleteDt.Enabled)
                {
                    if (!txtCommenceDt.Enabled)
                    {
                        txtPrevCommence.Text = m_parm.PrevCommenceDt;
                    }
                    if (!txtHoseOnDt.Enabled)
                    {
                        txtPrevHoseOn.Text = m_parm.PrevHoseOnDt;
                    }
                }
                if (txtHoseOffDt.Enabled)
                {
                    if (!txtCompleteDt.Enabled)
                    {
                        txtPrevComplete.Text = m_parm.PrevCompleteDt;
                    }
                    if (!txtCommenceDt.Enabled)
                    {
                        txtPrevCommence.Text = m_parm.PrevCommenceDt;
                    }
                    if (!txtHoseOnDt.Enabled)
                    {
                        txtPrevHoseOn.Text = m_parm.PrevHoseOnDt;
                    }
                }
                ************************************************************************************/
                /***** ADD - 22/06/2010 - Always enable Complete/Hose Off (user's requirement) ******/
                if (txtCompleteDt.Enabled)
                {
                    if (!txtCommenceDt.Enabled && !txtHoseOnDt.Enabled)
                    {
                        txtPrevCommence.Text = m_parm.PrevCommenceDt;
                    }
                    if (!txtHoseOnDt.Enabled)
                    {
                        txtPrevHoseOn.Text = m_parm.PrevHoseOnDt;
                    }
                }
                /************************************************************************************/

                // In case no hose time is turned enable at previous steps, 
                // we enable only hose times that have not been inputted.
                if (!txtHoseOnDt.Enabled && 
                    !txtCommenceDt.Enabled &&
                    !txtCompleteDt.Enabled &&
                    !txtHoseOffDt.Enabled)
                {   
                    if (string.IsNullOrEmpty(m_parm.PrevHoseOnDt))
                    {
                        txtHoseOnDt.Enabled = true;
                    }
                    else
                    {
                        txtPrevHoseOn.Text = m_parm.PrevHoseOnDt;
                    }
                    if (seqOrder > 1)
                    {
                        if (string.IsNullOrEmpty(m_parm.PrevCommenceDt))
                        {
                            txtCommenceDt.Enabled = true;
                        }
                        else
                        {
                            txtPrevCommence.Text = m_parm.PrevCommenceDt;
                        }
                    }
                    /***** DEL - 22/06/2010 - Always enable Complete/Hose Off (user's requirement) ****
                    if (seqOrder > 2)
                    {
                        if (string.IsNullOrEmpty(m_parm.PrevCompleteDt))
                        {
                            txtCompleteDt.Enabled = true;
                        }
                        else
                        {
                            txtPrevComplete.Text = m_parm.PrevCompleteDt;
                        }
                        
                    }
                    if (seqOrder > 3)
                    {
                        if (string.IsNullOrEmpty(m_parm.PrevHoseOffDt))
                        {
                            txtHoseOffDt.Enabled = true;
                        }
                        else
                        {
                            txtPrevHoseOff.Text = m_parm.PrevHoseOffDt;
                        }
                    }
                     ************************************************************************************/
                    /***** ADD - 22/06/2010 - Always enable Complete/Hose Off (user's requirement) ******/
                    if (seqOrder > 2)
                    {
                        txtPrevComplete.Text = m_parm.PrevCompleteDt;
                    }
                    if (seqOrder > 3)
                    {
                        txtPrevHoseOff.Text = m_parm.PrevHoseOffDt;
                    }
                    /************************************************************************************/
                }
            }
            // Mode ADD
            else
            {
                // Enable hose times that have not been inputted.
                if (string.IsNullOrEmpty(m_parm.PrevHoseOnDt))
                {
                    txtHoseOnDt.Enabled = true;
                    CommonUtility.SetDTPValueDMYHM(txtHoseOnDt, m_parm.HoseOnDt);
                }
                else
                {
                    txtHoseOnDt.Enabled = false;
                    txtPrevHoseOn.Text = m_parm.PrevHoseOnDt;
                }
                if (string.IsNullOrEmpty(m_parm.PrevCommenceDt))
                {
                    txtCommenceDt.Enabled = true;
                    CommonUtility.SetDTPValueDMYHM(txtCommenceDt, m_parm.CommenceDt);
                }
                else
                {
                    txtCommenceDt.Enabled = false;
                    txtPrevCommence.Text = m_parm.PrevCommenceDt;
                }
                /***** DEL - 22/06/2010 - Always enable Complete/Hose Off (user's requirement) ****
                if (string.IsNullOrEmpty(m_parm.PrevCompleteDt))
                {
                    txtCompleteDt.Enabled = true;
                    CommonUtility.SetDTPValueDMYHM(txtCompleteDt, m_parm.CompleteDt);
                }
                else
                {
                    txtCompleteDt.Enabled = false;
                    txtPrevComplete.Text = m_parm.PrevCompleteDt;
                }
                if (string.IsNullOrEmpty(m_parm.PrevHoseOffDt))
                {
                    txtHoseOffDt.Enabled = true;
                    CommonUtility.SetDTPValueDMYHM(txtHoseOffDt, m_parm.HoseOffDt);
                }
                else
                {
                    txtHoseOffDt.Enabled = false;
                    txtPrevHoseOff.Text = m_parm.PrevHoseOffDt;
                }
                 ************************************************************************************/
                /***** ADD - 22/06/2010 - Always enable Complete/Hose Off (user's requirement) ******/
                if (string.IsNullOrEmpty(m_parm.PrevCompleteDt))
                {
                    CommonUtility.SetDTPValueDMYHM(txtCompleteDt, m_parm.CompleteDt);
                }
                else
                {
                    txtPrevComplete.Text = m_parm.PrevCompleteDt;
                }
                if (string.IsNullOrEmpty(m_parm.PrevHoseOffDt))
                {
                    CommonUtility.SetDTPValueDMYHM(txtHoseOffDt, m_parm.HoseOffDt);
                }
                else
                {
                    txtPrevHoseOff.Text = m_parm.PrevHoseOffDt;
                }
                /************************************************************************************/
            }
        }

        private int GetHosetimeSeqOrder(string seq, string hoseOn, string commence, string complete, string hoseOff)
        {
            int ret = 0;
            try
            {
                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                VORLiquidBulkParm parm = new VORLiquidBulkParm();
                parm.searchType = "HHT_HosetimeSeqOrder";
                parm.seq = seq;
                parm.hoseOnDt = hoseOn;
                parm.hoseOffDt = hoseOff;
                parm.commenceDt = commence;
                parm.completeDt = complete;
                ResponseInfo info = proxy.getVORLiquidBulk(parm);

                if (info != null && info.list != null && info.list.Length > 0 && info.list[0] is VORLiquidBulkItem)
                {
                    VORLiquidBulkItem item = (VORLiquidBulkItem) info.list[0];
                    ret = CommonUtility.ParseInt(item.seqOrder);
                }

                return ret;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            return 0;
        }

        private void ReturnInfo()
        {
            // ref: CT108 & CT108003
            m_result = new HVO106001Result();
            m_result.HoseOnDt = txtHoseOnDt.Text;
            m_result.HoseOffDt = txtHoseOffDt.Text;
            m_result.CommenceDt = txtCommenceDt.Text;
            m_result.CompleteDt = txtCompleteDt.Text;
            m_result.PrevHoseOnDt = m_parm.PrevHoseOnDt;
            m_result.PrevHoseOffDt = m_parm.PrevHoseOffDt;
            m_result.PrevCommenceDt = m_parm.PrevCommenceDt;
            m_result.PrevCompleteDt = m_parm.PrevCompleteDt;
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnOk":
                    if (this.validations(this.Controls) && Validate())
                    {
                        ReturnInfo();
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                    }
                    break;

                case "btnCancel":
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    break;
            }
        }

        private bool Validate()
        {
            if (!ValidateEndtime())
            {
                return false;
            }
            if (!ValidateDatetimeWithinShift())
            {
                return false;
            }
            if (!ValidateDatetimeOrder())
            {
                return false;
            }

            return true;
        }

        private bool ValidateEndtime()
        {
            if (txtHoseOnDt.Enabled && string.IsNullOrEmpty(txtHoseOnDt.Text))
            {
                // Alert message [Please input Hose On time]
                CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HVO106_0004"));
                return false;
            }

            if (!string.IsNullOrEmpty(txtCompleteDt.Text) &&
                txtCommenceDt.Enabled && 
                string.IsNullOrEmpty(txtCommenceDt.Text) &&
                string.IsNullOrEmpty(txtPrevCommence.Text))
            {
                // Alert message [Please input Commence time]
                CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HVO106_0005"));
                return false;
            }

            if (!string.IsNullOrEmpty(txtHoseOffDt.Text) &&
                txtCompleteDt.Enabled && 
                string.IsNullOrEmpty(txtCompleteDt.Text) &&
                string.IsNullOrEmpty(txtPrevComplete.Text))
            {
                // Alert message [Please input Complete time]
                CommonUtility.AlertMessage(ResourceManager.getInstance().getString("HVO106_0006"));
                return false;
            }

            return true;
        }

        private bool ValidateDatetimeWithinShift()
        {
            if (txtHoseOnDt.Enabled && !string.IsNullOrEmpty(txtHoseOnDt.Text) &&
                !CommonUtility.ValidateDatetimeWithinShift(txtHoseOnDt))
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO106001_0001"));
                return false;
            }
            if (txtCommenceDt.Enabled && !string.IsNullOrEmpty(txtCommenceDt.Text) &&
                !CommonUtility.ValidateDatetimeWithinShift(txtCommenceDt))
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO106001_0002"));
                return false;
            }
            if (txtCompleteDt.Enabled && !string.IsNullOrEmpty(txtCompleteDt.Text) &&
                !CommonUtility.ValidateDatetimeWithinShift(txtCompleteDt))
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO106001_0003"));
                return false;
            }
            if (txtHoseOffDt.Enabled && !string.IsNullOrEmpty(txtHoseOffDt.Text) &&
                !CommonUtility.ValidateDatetimeWithinShift(txtHoseOffDt))
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HVO106001_0004"));
                return false;
            }
            return true;
        }

        private bool ValidateDatetimeOrder()
        {
            // Hose On <= Commence <= Complete <= Hose Off
            bool hoseonEmpty = true;
            bool commenceEmpty = true;
            bool completeEmpty = true;
            bool hoseoffEmpty = true;
            DateTime dtHoseon = DateTime.MinValue;
            DateTime dtCommence = DateTime.MinValue;
            DateTime dtComplete = DateTime.MinValue;
            DateTime dtHoseoff = DateTime.MinValue;
            if (!string.IsNullOrEmpty(txtHoseOnDt.Text))
            {
                hoseonEmpty = false;
                dtHoseon = txtHoseOnDt.Value;
            }
            else if (!string.IsNullOrEmpty(txtPrevHoseOn.Text))
            {
                hoseonEmpty = false;
                dtHoseon = CommonUtility.ParseYMDHM(txtPrevHoseOn.Text);
            }

            if (!string.IsNullOrEmpty(txtCommenceDt.Text))
            {
                commenceEmpty = false;
                dtCommence = txtCommenceDt.Value;
            }
            else if (!string.IsNullOrEmpty(txtPrevCommence.Text))
            {
                commenceEmpty = false;
                dtCommence = CommonUtility.ParseYMDHM(txtPrevCommence.Text);
            }

            if (!string.IsNullOrEmpty(txtCompleteDt.Text))
            {
                completeEmpty = false;
                dtComplete = txtCompleteDt.Value;
            }
            else if (!string.IsNullOrEmpty(txtPrevComplete.Text))
            {
                completeEmpty = false;
                dtComplete = CommonUtility.ParseYMDHM(txtPrevComplete.Text);
            }

            if (!string.IsNullOrEmpty(txtHoseOffDt.Text))
            {
                hoseoffEmpty = false;
                dtHoseoff = txtHoseOffDt.Value;
            }
            else if (!string.IsNullOrEmpty(txtPrevHoseOff.Text))
            {
                hoseoffEmpty = false;
                dtHoseoff = CommonUtility.ParseYMDHM(txtPrevHoseOff.Text);
            }

            if (!hoseonEmpty)
            {
                if (!commenceEmpty && dtHoseon.CompareTo(dtCommence) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0044"));
                    txtHoseOnDt.Focus();
                    return false;
                }

                if (!completeEmpty && dtHoseon.CompareTo(dtComplete) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0044"));
                    txtCommenceDt.Focus();
                    return false;
                }

                if (!hoseoffEmpty && dtHoseon.CompareTo(dtHoseoff) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0044"));
                    txtCompleteDt.Focus();
                    return false;
                }
            }

            if (!commenceEmpty)
            {
                if (!completeEmpty && dtCommence.CompareTo(dtComplete) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0044"));
                    txtCommenceDt.Focus();
                    return false;
                }

                if (!hoseoffEmpty && dtCommence.CompareTo(dtHoseoff) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0044"));
                    txtCompleteDt.Focus();
                    return false;
                }
            }

            if (!completeEmpty)
            {
                if (!hoseoffEmpty && dtComplete.CompareTo(dtHoseoff) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0044"));
                    txtCompleteDt.Focus();
                    return false;
                }
            }

            return true;
        }
    }
}