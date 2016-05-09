using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using MOST.Client.Proxy.CommonProxy;
using MOST.Client.Proxy.PortSafetyProxy;
using Framework.Service.Provider.WebService.Provider;
using System.Collections;
using Framework.Common.PopupManager;
using MOST.Common;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.Common.Utility;
using MOST.PortSafety.Parm;
using Framework.Common.Constants;
using Framework.Controls.UserControls;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;

namespace MOST.PortSafety
{
    public partial class HPS102 : TForm, IPopupWindow
    {
        #region Local Variable
        private readonly String HEADER_SN = "S/N No";
        private readonly String HEADER_GR = "G/R No";
        private readonly String HEADER_OPR_MODE = "O.Mode";
        private readonly String HEADER_DELV_MODE = "D.Mode";
        private readonly String HEADER_DELV_STATUS = "D.Status";
        private readonly String HEADER_GI_DATE = "GI Time";
        private readonly String HEADER_GO_DATE = "GO Time";
        private readonly String HEADER_FWD_AGENT = "F.Agent";
        private readonly String HEADER_GATE = "Gate";
        private readonly String HEADER_ACT_MT = "MT";
        private readonly String HEADER_ACT_M3 = "M3";
        private readonly String HEADER_ACT_QTY = "QTY";
        private readonly String HEADER_LORRY_NO = "Lorry";
        private readonly String HEADER_DG_APPR_STAT = "DG Appr. Status";
        private readonly String HEADER_DG_CARGO = "DG Cargo";
        private readonly String HEADER_GATEIN_BY = "GI By";

        //Added By Chris 2015-09-25
        private readonly String HEADER_DO = "D/O No";

        //Added By Chris 2015-10-06
        HPS102Parm hps102Parm;

        private bool m_initializedSN;
        #endregion

        public HPS102()
        {
            InitializeComponent();
            this.initialFormSize();
            
            /*
                Authority Check
             */
            this.authorityCheck();

            InitializeDataGrid();

            m_initializedSN = false;
            txtStart.CustomFormat = TDateTimePicker.FORMAT_DDMMYYYY;
            txtEnd.CustomFormat = TDateTimePicker.FORMAT_DDMMYYYY;

            //Added by Chris 2015-09-24 for 49779
            txtStart.Value = CommonUtility.ParseStringToDate
                (UserInfo.getInstance().Workdate, "dd/MM/yyyy");

            txtEnd.Value = CommonUtility.ParseStringToDate
                (UserInfo.getInstance().Workdate, "dd/MM/yyyy");

            //Commented by Chris 2015-09-24
            //CommonUtility.SetDTPValueBlank(txtStart);
            //CommonUtility.SetDTPValueBlank(txtEnd);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            hps102Parm = (HPS102Parm)parm;
            if (hps102Parm != null)
            {
                if (Constants.NONCALLID.Equals(hps102Parm.VslCallId))
                {
                    rbtnNonJPVC.Checked = true;
                }
                else
                {
                    rbtnJPVC.Checked = true;
                    txtJPVC.Text = hps102Parm.VslCallId;
                }
                if (hps102Parm.GrNo != null)
                {
                    txtGR.Text = hps102Parm.GrNo;
                }
               
            }
            this.ShowDialog();
            return null;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_SN, "90" }, { HEADER_GR, "90" }, { HEADER_DO, "80" }, { HEADER_OPR_MODE, "70" }, { HEADER_DELV_MODE, "70" }, { HEADER_DELV_STATUS, "70" }, { HEADER_GI_DATE, "120" }, { HEADER_GO_DATE, "120" }, { HEADER_FWD_AGENT, "50" }, { HEADER_GATE, "50" }, { HEADER_ACT_MT, "60" }, { HEADER_ACT_M3, "60" }, { HEADER_ACT_QTY, "60" }, { HEADER_LORRY_NO, "50" }, { HEADER_DG_APPR_STAT, "70" }, { HEADER_DG_CARGO, "70" }, { HEADER_GATEIN_BY, "80" } };
            this.grdData.setHeader(header);
        }

        private void InitializeCboSNNo(String argJpvc)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ResponseInfo info;
                ICommonProxy proxy = new CommonProxy();
                CommonUtility.InitializeCombobox(cboSNNo);

                // In case user input JPVC directly (don't choose from pop up):
                // return if JPVC is not correct
                SearchJPVCResult jpvcResult = null;
                if (rbtnJPVC.Checked && CommonUtility.IsValidJPVC(argJpvc, ref jpvcResult) == false)
                {
                    return;
                }

                if (!string.IsNullOrEmpty(argJpvc))
                {
                    Framework.Service.Provider.WebService.Provider.ShippingNoteParm parm = new Framework.Service.Provider.WebService.Provider.ShippingNoteParm();
                    parm.vslCallId = argJpvc;
                    parm.searchType = "combo";
                    info = proxy.getShippingNoteComboList(parm);
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is ShippingNoteItem)
                        {
                            ShippingNoteItem item = (ShippingNoteItem)info.list[i];
                            cboSNNo.Items.Add(item.shipgNoteNo);
                        }
                    }
                }
                cboSNNo.Refresh();
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
                m_initializedSN = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void F_Retrieve()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Request Webservice
                IPortSafetyProxy proxy = new PortSafetyProxy();
                ExportMfCtrlListParm parm = new ExportMfCtrlListParm();
                parm.jpvcNo = GetVslCallId();
                parm.snNo = CommonUtility.GetComboboxSelectedValue(cboSNNo);
                parm.grNo = txtGR.Text;
                parm.fwrAgent = txtFwdAgent.Text;
                parm.searchType = "selectListOfGateIn2";
                parm.gateInStDt = txtStart.Text;
                parm.gateInEndDt = txtEnd.Text;
                if (hps102Parm != null)
                {
                    if (hps102Parm.DelvOdrNo != null)
                    {
                        parm.delvOdrNo = hps102Parm.DelvOdrNo;
                    }
                    if (hps102Parm.BlNo != null)
	                {
                        parm.blNo = hps102Parm.BlNo;
	                }
                }
                ResponseInfo info = proxy.getListOfGateIn(parm);
                #endregion

                #region Display Data
                grdData.Clear();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is ExportMfCtrlListItem)
                    {
                        ExportMfCtrlListItem item = (ExportMfCtrlListItem)info.list[i];
                        DataRow newRow = grdData.NewRow();
                        newRow[HEADER_SN] = item.shipgNoteNo;
                        newRow[HEADER_GR] = item.grNo;

                        //Added by Chris 2015-09-25
                        newRow[HEADER_DO] = item.delvOdrNo;

                        newRow[HEADER_OPR_MODE] = item.tsptTpCd;
                        newRow[HEADER_DELV_MODE] = item.delvTpCd;
                        newRow[HEADER_DELV_STATUS] = item.statCd;
                        newRow[HEADER_GI_DATE] = item.firstGateInDt;
                        newRow[HEADER_GO_DATE] = item.lastGateOutDt;
                        newRow[HEADER_FWD_AGENT] = item.fwrAgnt;
                        newRow[HEADER_GATE] = item.gateNm;
                        newRow[HEADER_ACT_MT] = item.wgt;
                        newRow[HEADER_ACT_M3] = item.msrmt;
                        newRow[HEADER_ACT_QTY] = item.pkgQty;
                        newRow[HEADER_LORRY_NO] = item.lorryNo;
                        newRow[HEADER_DG_APPR_STAT] = item.dgApprStat;
                        newRow[HEADER_DG_CARGO] = item.isDgCargo;
                        newRow[HEADER_GATEIN_BY] = item.gateInBy;
                        grdData.Add(newRow);
                    }
                }
                grdData.Refresh();
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

        private string GetVslCallId()
        {
            string vslCallId = string.Empty;
            if (rbtnJPVC.Checked)
            {
                vslCallId = txtJPVC.Text;
            }
            else if (rbtnNonJPVC.Checked)
            {
                vslCallId = Constants.NONCALLID;
            }
            return vslCallId;
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnF1":
                    MOST.Common.CommonParm.SearchJPVCParm jpvcParm = new MOST.Common.CommonParm.SearchJPVCParm();
                    jpvcParm.Jpvc = txtJPVC.Text;
                    SearchJPVCResult jpvcResultTmp = (SearchJPVCResult)PopupManager.instance.ShowPopup(new HCM101(), jpvcParm);
                    if (jpvcResultTmp != null)
                    {
                        txtJPVC.Text = jpvcResultTmp.Jpvc;
                        InitializeCboSNNo(jpvcResultTmp.Jpvc);
                    }
                    break;

                case "btnF2":
                    PartnerCodeListParm fwdParm = new PartnerCodeListParm();
                    fwdParm.SearchItem = txtFwdAgent.Text;
                    PartnerCodeListResult fwdResult = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_FORWARDER), fwdParm);
                    if (fwdResult != null)
                    {
                        txtFwdAgent.Text = fwdResult.Code;
                    }
                    break;

                case "btnRetrieve":
                    if (this.validations(this.Controls) && CommonUtility.CheckDateStartEnd(txtStart, txtEnd))
                    {
                        F_Retrieve();
                    }
                    break;

                case "btnExit":
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    break;

            }
        }

        private void RadiobuttonListener(object sender, EventArgs e)
        {
            RadioButton mybutton = (RadioButton)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "rbtnJPVC":
                case "rbtnNonJPVC":
                    OnCheckRadioButton();
                    InitializeCboSNNo(GetVslCallId());
                    break;
            }
        }

        private void OnCheckRadioButton()
        {
            if (rbtnJPVC.Checked)
            {
                txtJPVC.Enabled = true;
                btnF1.Enabled = true;
            }
            else if (rbtnNonJPVC.Checked)
            {
                txtJPVC.Enabled = false;
                btnF1.Enabled = false;
            }
        }

        private void txtJPVC_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJPVC.Text) && !m_initializedSN)
            {
                InitializeCboSNNo(txtJPVC.Text);
            }
        }

        private void txtJPVC_KeyPress(object sender, KeyPressEventArgs e)
        {
            m_initializedSN = false;

            // if key = Enter then get vessel schedule detail
            if (e.KeyChar.Equals((char)Keys.Enter))
            {
                InitializeCboSNNo(txtJPVC.Text);
            }
        }

        private void txtJPVC_TextChanged(object sender, EventArgs e)
        {
            m_initializedSN = false;
        }
    }
}