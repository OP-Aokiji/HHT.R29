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
using Framework.Common.UserInformation;
using Framework.Common.ExceptionHandler;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.VesselOperator
{
    public partial class HVO101001 : TForm, IPopupWindow
    {
        private const string CTRL_TBUTTON = "TButton";
        private SearchJPVCResult m_jpvcResult;
        private string m_dblBankingVslCallId = null;  // Mother vessel

        public HVO101001()
        {
            InitializeComponent();
            this.initialFormSize();
            EnableAllButtons(false);
            this.btnVessel.Enabled = true;
            this.btnExit.Enabled = true;
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            HVO101001Parm _parm = (HVO101001Parm)parm;
            if (_parm != null)
            {
                m_dblBankingVslCallId = _parm.DblBankingVslCallId;

                if (_parm.StsJpvcInfo != null)
                {
                    m_jpvcResult = _parm.StsJpvcInfo;
                    txtJPVC.Text = m_jpvcResult.Jpvc;
                    txtPurpCall.Text = m_jpvcResult.PurpCall;
                    txtVslTpNm.Text = m_jpvcResult.VslTpNm;

                    // Get hatch, EQU information
                    GetVORDryBreakBulk();

                    // Get liquid operation type then set liquid buttons.
                    GetLiquidOpeType();
                }
            }
            this.ShowDialog();
            return null;
        }

        /// <summary>
        /// Display inputted Hatch, Equipment
        /// </summary>
        private void GetVORDryBreakBulk()
        {
            try
            {
                string strHatches = "";
                string strEquipments = "";
                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                VORDryBreakBulkParm parm = new VORDryBreakBulkParm();
                parm.searchType = "info";
                parm.rsDivCd = "EQ";
                parm.vslCallId = txtJPVC.Text;
                parm.workYmd = UserInfo.getInstance().Workdate;
                parm.shift = UserInfo.getInstance().Shift;
                ResponseInfo info = proxy.getVORDryBreakBulk(parm);
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is VORDryBreakBulkItem)
                    {
                        VORDryBreakBulkItem item = (VORDryBreakBulkItem) info.list[i];

                        // Hatches: "H1,H2,H3"
                        string hatchNo = item.hatchNo;
                        if (!string.IsNullOrEmpty(strHatches))
                        {
                            strHatches = strHatches + "," + hatchNo;
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
                                strEquipments = strEquipments + "," + eqFacNm;
                            }
                            else
                            {
                                strEquipments = strEquipments + eqFacNm;
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
        }

        /// <summary>
        /// Get operation type of liquid bulk.
        /// </summary>
        private void GetLiquidOpeType()
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
                    if ("Y".Equals(item.stsOprYn))
                    {
                        btnSTSLiquid.Enabled = true;
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
            btnSTSLiquid.Enabled = false;
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
            Button _mybutton = (Button)sender;
            String _buttonName = _mybutton.Name;
            switch (_buttonName)
            {
                case "btnVessel":
                    jpvcParm = new MOST.Common.CommonParm.SearchJPVCParm();
                    jpvcParm.Jpvc = txtJPVC.Text;
                    SearchJPVCResult jpvcResultTmp = (SearchJPVCResult)PopupManager.instance.ShowPopup(new HVO102(), jpvcParm);
                    if (jpvcResultTmp != null)
                    {
                        m_jpvcResult = jpvcResultTmp;
                        txtJPVC.Text = m_jpvcResult.Jpvc;
                        txtPurpCall.Text = m_jpvcResult.PurpCall;
                        txtVslTpNm.Text = m_jpvcResult.VslTpNm;

                        // Get hatch, EQU information
                        GetVORDryBreakBulk();

                        // Get liquid operation type then set liquid buttons.
                        GetLiquidOpeType();
                    }
                    break;

                case "btnVslShifting":
                    HVO112Parm hvo112Parm = new HVO112Parm();
                    hvo112Parm.JpvcInfo = m_jpvcResult;
                    PopupManager.instance.ShowPopup(new HVO112(), hvo112Parm);
                    break;

                case "btnSTSLiquid":
                    HVO113Parm stsLQParm = new HVO113Parm();
                    stsLQParm.JpvcInfo = m_jpvcResult;
                    PopupManager.instance.ShowPopup(new HVO113(HVO113.CONST_OPETP_STS), stsLQParm);
                    break;

                case "btnDelay":
                    HVO107Parm hvo107Parm = new HVO107Parm();
                    hvo107Parm.JpvcInfo = m_jpvcResult;
                    HVO107Result hvo107Result = (HVO107Result)PopupManager.instance.ShowPopup(new HVO107(), hvo107Parm);
                    if (hvo107Result != null)
                    {
                    }
                    break;

                //case "btnDelayLiquid":
                //    HVO120Parm hvo120Parm = new HVO120Parm();
                //    hvo120Parm.JpvcInfo = m_jpvcResult;
                //    PopupManager.instance.ShowPopup(new HVO120(), hvo120Parm);
                //    break;

                case "btnSTV":
                    HVO110Parm hvo110Parm = new HVO110Parm();
                    hvo110Parm.JpvcInfo = m_jpvcResult;

                    HVO110Result hvo110Result = (HVO110Result)PopupManager.instance.ShowPopup(new HVO110(), hvo110Parm);
                    if (hvo110Result != null)
                    {
                    }
                    break;

                case "btnCargoShifting":
                    HVO118Parm cgSftParm = new HVO118Parm();
                    cgSftParm.JpvcInfo = m_jpvcResult;
                    PopupManager.instance.ShowPopup(new HVO118(), cgSftParm);
                    break;

                case "btnSTS":
                    HVO109Parm stsParm = new HVO109Parm();
                    stsParm.JpvcInfo = m_jpvcResult;
                    stsParm.DblBankingVslCallId = m_dblBankingVslCallId;
                    PopupManager.instance.ShowPopup(new HVO109(), stsParm);
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
    }
}