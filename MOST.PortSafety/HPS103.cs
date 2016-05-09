using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
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
    public partial class HPS103 : TForm, IPopupWindow
    {
        #region Local Variable
        private readonly String HEADER_GP = "G/P No";
        private readonly String HEADER_JPVC = "JPVC";
        private readonly String HEADER_BLSN = "BL/SN";
        private readonly String HEADER_DO = "D/O No";
        private readonly String HEADER_GR = "G/R No";
        private readonly String HEADER_GI_DATE = "GI Time";
        private readonly String HEADER_GO_DATE = "GO Time";
        private readonly String HEADER_FWD_AGENT = "F.Agent";
        private readonly String HEADER_GATE = "Gate";
        private readonly String HEADER_LORRY_NO = "Lorry";
        private readonly String HEADER_ACT_MT = "MT";
        private readonly String HEADER_ACT_M3 = "M3";
        private readonly String HEADER_ACT_QTY = "QTY";
        private readonly String HEADER_CAT = "Category";
        private readonly String HEADER_PKG_TYPE = "Pkg.Type";
        private readonly String HEADER_CG_TYPE = "Cg.Type";
        private readonly String HEADER_GATEOUT_BY = "GO By";
        #endregion

        public HPS103()
        {
            InitializeComponent();
            this.initialFormSize();
            
            /*
                Authority Check
             */
            this.authorityCheck();

            InitializeDataGrid();

            txtStart.CustomFormat = TDateTimePicker.FORMAT_DDMMYYYY;
            txtEnd.CustomFormat = TDateTimePicker.FORMAT_DDMMYYYY;

            txtStart.Value = CommonUtility.ParseStringToDate
                (UserInfo.getInstance().Workdate, "dd/MM/yyyy");

            txtEnd.Value = CommonUtility.ParseStringToDate
                (UserInfo.getInstance().Workdate, "dd/MM/yyyy");
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            HPS103Parm hps103Parm = (HPS103Parm)parm;
            if (hps103Parm != null)
            {
                if (Constants.NONCALLID.Equals(hps103Parm.VslCallId))
                {
                    rbtnNonJPVC.Checked = true;
                }
                else
                {
                    rbtnJPVC.Checked = true;
                    txtJPVC.Text = hps103Parm.VslCallId;
                }
            }
            this.ShowDialog();
            return null;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_JPVC, "80" }, { HEADER_GP, "80" }, { HEADER_BLSN, "80" }, { HEADER_DO, "80" }, { HEADER_GR, "80" }, { HEADER_GI_DATE, "90" }, { HEADER_GO_DATE, "90" }, { HEADER_FWD_AGENT, "50" }, { HEADER_GATE, "50" }, { HEADER_LORRY_NO, "50" }, { HEADER_ACT_MT, "50" }, { HEADER_ACT_M3, "50" }, { HEADER_ACT_QTY, "50" }, { HEADER_CAT, "50" }, { HEADER_PKG_TYPE, "50" }, { HEADER_CG_TYPE, "50" }, { HEADER_GATEOUT_BY, "80" } };
            this.grdData.setHeader(header);
        }

        private void F_Retrieve()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Request Webservice
                IPortSafetyProxy proxy = new PortSafetyProxy();
                ImportMfCtrlListParm parm = new ImportMfCtrlListParm();
                parm.jpvcNo = GetVslCallId();
                parm.doNo = txtDo.Text;
                parm.gatePassNo = txtGP.Text;
                parm.fwrAgnt = txtFwdAgent.Text;
                parm.searchType = "selectListOfGateOut2";
                parm.dischgStDt = txtStart.Text;
                parm.dischgEndDt = txtEnd.Text;
                ResponseInfo info = proxy.getListOfGateOut(parm);
                #endregion

                #region Display Data
                grdData.Clear();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is ImportMfCtrlListItem)
                    {
                        ImportMfCtrlListItem item = (ImportMfCtrlListItem)info.list[i];
                        DataRow newRow = grdData.NewRow();
                        newRow[HEADER_GP] = item.gatePassNo;
                        newRow[HEADER_JPVC] = item.vslCallId;
                        newRow[HEADER_BLSN] = item.blNo;
                        newRow[HEADER_DO] = item.delvOdrNo;
                        newRow[HEADER_GR] = item.grNo;
                        newRow[HEADER_GI_DATE] = item.firstGateInDt;
                        newRow[HEADER_GO_DATE] = item.lastGateOutDt;
                        newRow[HEADER_FWD_AGENT] = item.fwrAgnt;
                        newRow[HEADER_GATE] = item.gateNm;
                        newRow[HEADER_LORRY_NO] = item.lorryNo;
                        newRow[HEADER_ACT_MT] = item.actlWgt;
                        newRow[HEADER_ACT_M3] = item.vol;
                        newRow[HEADER_ACT_QTY] = item.pkgQty;
                        newRow[HEADER_CAT] = item.catgCd;
                        newRow[HEADER_PKG_TYPE] = item.pkgTpCd;
                        newRow[HEADER_CG_TYPE] = item.cgTpCd;
                        newRow[HEADER_GATEOUT_BY] = item.gateOutBy;
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
    }
}