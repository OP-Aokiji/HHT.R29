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
using Framework.Controls.UserControls;
using Framework.Common.ExceptionHandler;

namespace MOST.PortSafety
{
    public partial class HPS104 : TForm, IPopupWindow
    {
        #region Local Variable
        private readonly String HEADER_TSPTR = "Transporter";
        private readonly String HEADER_LORRY = "Lorry No";
        private readonly String HEADER_DRIVER = "Driver";
        private readonly String HEADER_SN = "SN";
        private readonly String HEADER_BL = "BL";
        private readonly String HEADER_EXP_DATE = "Expire Date";
        private readonly String HEADER_LICENSE = "Licence";
        #endregion

        public HPS104()
        {
            InitializeComponent();
            this.initialFormSize();
            
            /*
                Authority Check
             */
            this.authorityCheck();

            InitializeDataGrid();

            SetDefaultArvlDate();
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            this.ShowDialog();
            return null;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_TSPTR, "50" }, { HEADER_LORRY, "70" }, { HEADER_DRIVER, "100" }, { HEADER_SN, "70" }, { HEADER_BL, "70" }, { HEADER_EXP_DATE, "70" }, { HEADER_LICENSE, "90" } };
            this.grdData.setHeader(header);
        }

        private void SetDefaultArvlDate()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                txtArvlDateFrom.CustomFormat = TDateTimePicker.FORMAT_DDMMYYYY;
                txtArvlDateTo.CustomFormat = TDateTimePicker.FORMAT_DDMMYYYY;

                string strCurDt = CommonUtility.GetCurrentServerTime();
                DateTime curDt = CommonUtility.ParseStringToDate(strCurDt, CommonUtility.DDMMYYYYHHMM);
                txtArvlDateFrom.Value = curDt.AddDays(-1);
                txtArvlDateTo.Value = curDt.AddDays(8);
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

        private void F_Retrieve()
        {
            try
            {
                // ref: DM117
                Cursor.Current = Cursors.WaitCursor;

                #region Request Webservice
                IPortSafetyProxy proxy = new PortSafetyProxy();
                Framework.Service.Provider.WebService.Provider.LorryListParm parm = new Framework.Service.Provider.WebService.Provider.LorryListParm();
                parm.searchType = "lorryList";
                parm.noGate = "noGate";
                parm.ptnrCd = txtTsptr.Text;
                parm.LORRYNO = txtLorry.Text;
                parm.aplyYmd = txtArvlDateFrom.Text;
                parm.exprYmd = txtArvlDateTo.Text;
                ResponseInfo info = proxy.getLorryListItems(parm);
                #endregion

                #region Display Data
                grdData.Clear();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is LorryListItem)
                    {
                        LorryListItem item = (LorryListItem)info.list[i];
                        DataRow newRow = grdData.NewRow();
                        newRow[HEADER_TSPTR] = item.TSPTCD;
                        newRow[HEADER_LORRY] = item.LORRYNO;
                        newRow[HEADER_DRIVER] = item.DRIVER;
                        newRow[HEADER_SN] = item.SNNO;
                        newRow[HEADER_BL] = item.BLNO;
                        newRow[HEADER_EXP_DATE] = item.EXPRDT;
                        newRow[HEADER_LICENSE] = item.LICSNO;
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

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnOk":
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                    break;

                case "btnExit":
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    break;

                case "btnF2":
                    PartnerCodeListParm tsptrParm = new PartnerCodeListParm();
                    tsptrParm.SearchItem = txtTsptr.Text;
                    PartnerCodeListResult trptrResult = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_TRANSPORTER), tsptrParm);
                    if (trptrResult != null)
                    {
                        txtTsptr.Text = trptrResult.Code;
                    }
                    break;

                case "btnRetrieve":
                    if (this.validations(this.Controls) && CommonUtility.CheckDateStartEnd(txtArvlDateFrom, txtArvlDateTo))
                    {
                        F_Retrieve();
                    }
                    break;
            }
        }
    }
}