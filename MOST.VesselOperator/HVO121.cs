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
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using Framework.Service.Provider.WebService.Provider;
using Framework.Controls.UserControls;
using MOST.Client.Proxy.VesselOperatorProxy;

namespace MOST.VesselOperator
{
    public partial class HVO121 : TForm, IPopupWindow
    {
        private const string HEADER_ITEM_STATUS = "ITEM_STATUS";
        private const string HEADER_WS_NM = "Status";
        private const string HEADER_CG_OPR = "Cg OPR";
        private const string HEADER_CG_TP = "Cg Type";
        private const string HEADER_CMDT = "Commodity";
        private const string HEADER_HATCH = "Hatch";
        private const string HEADER_TOPCLEAN = "Top/Clean";

        public HVO121()
        {   
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            HVO121Parm hvo121Parm = (HVO121Parm)parm;
            txtJPVC.Text = hvo121Parm.VslCallId;
            F_Search();
            this.ShowDialog();
            return null;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_ITEM_STATUS, "0" }, { HEADER_WS_NM, "40" }, { HEADER_CG_OPR, "40" }, { HEADER_CG_TP, "40" }, { HEADER_CMDT, "40" }, { HEADER_HATCH, "40" }, { HEADER_TOPCLEAN, "50" } };
            this.grdData.setHeader(header);
        }

        private void F_Search()
        {
            try
            {
                // Ref: PN101TAB & PN101002
                if (this.validations(this.Controls))
                {
                    Cursor.Current = Cursors.WaitCursor;

                    IVesselOperatorProxy proxy = new VesselOperatorProxy();
                    ConfirmationSlipParm parm = new ConfirmationSlipParm();
                    parm.vslCallId = txtJPVC.Text;

                    ResponseInfo info = proxy.getConfirmationSlipBreakBulkList(parm);

                    grdData.Clear();
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is ConfirmationSlipItem)
                        {
                            ConfirmationSlipItem item = (ConfirmationSlipItem)info.list[i];
                            DataRow newRow = grdData.NewRow();
                            newRow[HEADER_CG_OPR] = item.cgOptTpCd;
                            newRow[HEADER_CG_TP] = item.cgTpNm;
                            newRow[HEADER_CMDT] = item.cmdtCd;
                            newRow[HEADER_HATCH] = item.workHatchNo;
                            newRow[HEADER_TOPCLEAN] = item.topCln;
                            grdData.Add(newRow);
                        }
                    }
                    grdData.Refresh();
                }
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}