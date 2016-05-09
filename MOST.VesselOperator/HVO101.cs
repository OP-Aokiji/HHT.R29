using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using Framework.Common.PopupManager;
using Framework.Common.Constants;
using Framework.Controls;
using MvcPatterns;
using MOST.Common;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.Common.Utility;
using MOST.VesselOperator.Parm;
using MOST.VesselOperator.Result;
using MOST.Client.Proxy.VesselOperatorProxy;
using Framework.Common.Helper;
using Framework.Common.UserInformation;
using Framework.Common.ExceptionHandler;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.VesselOperator
{
    public partial class HVO101 : TForm,IObserver
    {
        private const string CTRL_TBUTTON = "TButton";
        private SearchJPVCResult m_jpvcResult;

        //added by William (2015 July 09) issue 32956
        private const string SEARCH_TYPE_HATCH_EQU_INFO = "info";

        public HVO101()
        {
            InitializeComponent();
            this.initialFormSize();
            EnableAllButtons(false);
            this.btnVessel.Enabled = true;
            this.btnExit.Enabled = true;

            InitializeData();
            //////////////////////////////////////////////////////////////////////////
            // Making button text be multiline
            //WndProcHooker.MakeButtonMultiline(this.btnDelayLiquid);
            WndProcHooker.MakeButtonMultiline(this.btnJettyOPR);
            WndProcHooker.MakeButtonMultiline(this.btnSTSLiquid);
            WndProcHooker.MakeButtonMultiline(this.btnTranshipment);
            //////////////////////////////////////////////////////////////////////////
        }

        private bool CheckExistedData(string stParentStr, string stSubStr)
        {
            string[] splitArr = stParentStr.Split(',');
            foreach (string st in splitArr)
            {
                if (st.Equals(stSubStr)) return true;
            }
            return false;
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // Get vessel history info
                VesselHistoryInfo vslHistoryInfo = VesselHistoryInfo.GetInstance();
                if (vslHistoryInfo != null && !string.IsNullOrEmpty(vslHistoryInfo.VslCallId))
                {
                    txtJPVC.Text = vslHistoryInfo.VslCallId;
                    txtPurpCall.Text = vslHistoryInfo.PurpCallNm;
                    txtVslTpNm.Text = vslHistoryInfo.VslTpNm;

                    if (CommonUtility.IsValidJPVC(txtJPVC.Text, ref m_jpvcResult))
                    {
                        // Get SA ID
                        m_jpvcResult.ArrvSaId = vslHistoryInfo.ArrvSaId;
                        m_jpvcResult.DeprSaId = vslHistoryInfo.DeprSaId;

                        // Get hatch, EQU information
                        GetHatchAndEqu();

                        // Get liquid operation type then set liquid buttons.
                        SetLiquidButtons();
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

        /// <summary>
        /// Display inputted Hatch, Equipment
        /// </summary>
        private void GetHatchAndEqu()
        {
            try
            {
                string strHatches = "";
                string strEquipments = "";
                if (!string.IsNullOrEmpty(txtJPVC.Text))
                {
                    IVesselOperatorProxy proxy = new VesselOperatorProxy();
                    VORDryBreakBulkParm parm = new VORDryBreakBulkParm();
                    parm.searchType = SEARCH_TYPE_HATCH_EQU_INFO;
                    parm.rsDivCd = "EQ";
                    parm.vslCallId = txtJPVC.Text;

                    //added by William (2015 July 09) issue 32956
                    parm.workYmd = UserInfo.getInstance().Workdate;
                    parm.shift = UserInfo.getInstance().Shift;

                    ResponseInfo info = proxy.getVORDryBreakBulk(parm);
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is VORDryBreakBulkItem)
                        {
                            VORDryBreakBulkItem item = (VORDryBreakBulkItem)info.list[i];

                            // Hatches: "H1,H2,H3"
                            string hatchNo = item.hatchNo;
                            if (!string.IsNullOrEmpty(strHatches))
                            {
                                //added by William (2015 July 09) issue 32956
                                if (!CheckExistedData(strHatches,hatchNo))
                                {
                                    strHatches = strHatches + "," + hatchNo;
                                }
                            }
                            else
                            {
                                strHatches = strHatches + hatchNo;
                            }

                            // EQU: "BG1,BG2,LL1"
                            string eqFacNm = item.eqFacNm;
                            if (!string.IsNullOrEmpty(eqFacNm) && !string.IsNullOrEmpty(eqFacNm.Trim()))
                            {
                                if (!string.IsNullOrEmpty(strEquipments))
                                {
                                    //added by William (2015 July 09) issue 32956
                                    if (!CheckExistedData(strEquipments, eqFacNm))
                                    {
                                        strEquipments = strEquipments + "," + eqFacNm;
                                    }
                                }
                                else
                                {
                                    strEquipments = strEquipments + eqFacNm;
                                }
                            }
                        }
                    }
                }
                txtHatch.Text = strHatches;
                txtEqu.Text = strEquipments;
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

        /// <summary>
        /// Get operation type of liquid bulk then set liquid buttons.
        /// </summary>
        private void SetLiquidButtons()
        {
            // Ref: CT109
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                VORLiquidBulkParm parm = new VORLiquidBulkParm();
                parm.jpvcNo = txtJPVC.Text;
                ResponseInfo info = proxy.getVORLiquidBulkCgOprType(parm);

                if (info != null && info.list != null && info.list.Length > 0 && info.list[0] is VORLiquidBulkItem)
                {
                    VORLiquidBulkItem item = (VORLiquidBulkItem)info.list[0];
                    if ("Y".Equals(item.tlsOprYn))
                    {
                        btnTranshipment.Enabled = true;
                    }
                    if ("Y".Equals(item.stsOprYn))
                    {
                        btnSTSLiquid.Enabled = true;
                    }
                    if ("Y".Equals(item.genOprYn))
                    {
                        btnJettyOPR.Enabled = true;
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
        }
        
        private void EnableAllButtons(bool bEnable)
        {
            foreach (Control control in this.Controls)
            {
                if (!CTRL_TBUTTON.Equals(control.GetType().Name))
                {
                    continue;
                }
                control.Enabled = bEnable;
            }
            txtHatch.Enabled = bEnable;
            txtEqu.Enabled = bEnable;

            // Always set liquid buttons disabled when initialization.
            btnJettyOPR.Enabled = false;
            btnTranshipment.Enabled = false;
            btnSTSLiquid.Enabled = false;
            //Always set RoadBunkering enable
            btnRoadBunkering.Enabled = true;
        }

        private void txtJPVC_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJPVC.Text))
            {
                EnableAllButtons(true);
            }
            else
            {
                EnableAllButtons(false);
            }
        }

        private void ActionListener(object sender, EventArgs e)
        {
            MOST.Common.CommonParm.SearchJPVCParm jpvcParm;
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnVessel":
                    jpvcParm = new MOST.Common.CommonParm.SearchJPVCParm();
                    jpvcParm.Jpvc = txtJPVC.Text;
                    SearchJPVCResult jpvcResultTmp = (SearchJPVCResult)PopupManager.instance.ShowPopup(new HVO102(), jpvcParm);
                    if (jpvcResultTmp != null)
                    {
                        //m_jpvcResult = jpvcResultTmp;
                        //Refresh data
                        CommonUtility.IsValidJPVC(jpvcResultTmp.Jpvc, ref m_jpvcResult);
        
                        //added by William 2015.11.09
                        if (m_jpvcResult != null)
                        {
                        txtJPVC.Text = m_jpvcResult.Jpvc;
                        txtPurpCall.Text = m_jpvcResult.PurpCall;
                        txtVslTpNm.Text = m_jpvcResult.VslTpNm;

                        // Save history vslCallId
                        VesselHistoryInfo.SetInstance(m_jpvcResult.Jpvc, m_jpvcResult.VesselName,
                                                        m_jpvcResult.VslTp, m_jpvcResult.VslTpNm,
                                                        m_jpvcResult.PurpCallCd, m_jpvcResult.PurpCall);

                        VesselHistoryInfo.SetInstanceSAID(m_jpvcResult.DeprSaId, m_jpvcResult.ArrvSaId);
                        }
                        // Get hatch, EQU information
                        GetHatchAndEqu();

                        // Get liquid operation type then set liquid buttons.
                        SetLiquidButtons();
                    }
                    break;

                case "btnVslShifting":
                    HVO112Parm hvo112Parm = new HVO112Parm();
                    hvo112Parm.JpvcInfo = m_jpvcResult;
                    PopupManager.instance.ShowPopup(new HVO112(), hvo112Parm);
                    //Refresh Data
                    CommonUtility.IsValidJPVC(txtJPVC.Text, ref m_jpvcResult);
                    break;

                case "btnJettyOPR":
                    HVO113Parm jettyParm = new HVO113Parm();
                    jettyParm.JpvcInfo = m_jpvcResult;
                    PopupManager.instance.ShowPopup(new HVO113(HVO113.CONST_OPETP_GENERAL), jettyParm);
                    break;

                case "btnTranshipment":
                    HVO113Parm transhipmentParm = new HVO113Parm();
                    transhipmentParm.JpvcInfo = m_jpvcResult;
                    PopupManager.instance.ShowPopup(new HVO113(HVO113.CONST_OPETP_TRANSHIPMENT), transhipmentParm);
                    break;

                case "btnSTSLiquid":
                    HVO113Parm stsLQParm = new HVO113Parm();
                    stsLQParm.JpvcInfo = m_jpvcResult;
                    PopupManager.instance.ShowPopup(new HVO113(HVO113.CONST_OPETP_STS), stsLQParm);
                    break;

                case "btnDelay":
                    HVO107Parm hvo107Parm = new HVO107Parm();
                    hvo107Parm.JpvcInfo = m_jpvcResult;
                    PopupManager.instance.ShowPopup(new HVO107(), hvo107Parm);
                    break;

                case "btnSTV":
                    HVO110Parm hvo110Parm = new HVO110Parm();
                    hvo110Parm.JpvcInfo = m_jpvcResult;
                    PopupManager.instance.ShowPopup(new HVO110(), hvo110Parm);
                    break;

                case "btnDBanking":
                    HVO114Parm hvo108Parm = new HVO114Parm();
                    hvo108Parm.JpvcInfo = m_jpvcResult;
                    PopupManager.instance.ShowPopup(new HVO114(), hvo108Parm);
                    break;

                case "btnCargoShifting":
                    HVO118Parm cgSftParm = new HVO118Parm();
                    cgSftParm.JpvcInfo = m_jpvcResult;
                    PopupManager.instance.ShowPopup(new HVO118(), cgSftParm);
                    break;

                case "btnSTS":
                    HVO109Parm stsParm = new HVO109Parm();
                    stsParm.JpvcInfo = m_jpvcResult;
                    PopupManager.instance.ShowPopup(new HVO109(), stsParm);
                    break;

                case "btnRoadBunkering":
                    HVO125Parm rbkParm = new HVO125Parm();
                    rbkParm.JpvcInfo = m_jpvcResult;
                    PopupManager.instance.ShowPopup(new HVO125(), rbkParm);
                    break;

                case "btnUnclosedOPR":
                    MOST.Common.CommonParm.UnclosedOperationParm unclosedOprParm = new MOST.Common.CommonParm.UnclosedOperationParm();
                    unclosedOprParm.VslCallId = txtJPVC.Text;
                    PopupManager.instance.ShowPopup(new MOST.Common.HCM115(), unclosedOprParm);
                    break;

                case "btnContainer":
                    HVO123Parm containerParm = new HVO123Parm();
                    containerParm.JpvcInfo = m_jpvcResult;
                    PopupManager.instance.ShowPopup(new HVO123(), containerParm);
                    break;

                case "btnF1":
                    HVO103Parm hvo103Parm = new HVO103Parm();
                    hvo103Parm.JpvcInfo = m_jpvcResult;
                    HVO103Result hvo103Result = (HVO103Result)PopupManager.instance.ShowPopup(new HVO103(), hvo103Parm);
                    if (hvo103Result != null)
                    {
                        txtHatch.Text = hvo103Result.Hatches;
                    }

                    break;

                case "btnF2":
                    HVO104Parm hvo104Parm = new HVO104Parm();
                    hvo104Parm.JpvcInfo = m_jpvcResult;

                    HVO104Result hvo104Result = (HVO104Result)PopupManager.instance.ShowPopup(new HVO104(), hvo104Parm);
                    if (hvo104Result != null)
                    {
                        txtEqu.Text = hvo104Result.Equipments;
                    }

                    break;

                case "btnExit":
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    break;
            }
        }

        public void receiveNotify(NoticeMessage message)
        {
            this.Close();
        }
    }
}