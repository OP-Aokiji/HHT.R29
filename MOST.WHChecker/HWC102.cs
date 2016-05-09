using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MOST.Client.Proxy.WHCheckerProxy;
using MOST.Common;
using MOST.Common.Utility;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.WHChecker.Parm;
using Framework.Common.PopupManager;
using Framework.Controls.Container;
using Framework.Common.UserInformation;
using Framework.Common.Constants;
using Framework.Common.ExceptionHandler;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.WHChecker
{
    public partial class HWC102 : TForm, IPopupWindow
    {
        #region Local Variable
        private readonly String HEADER_CAT = "Cat";
        private readonly String HEADER_SNBL = "SN/BL";
        private readonly String HEADER_GRDO = "GR/DO";
        private readonly String HEADER_HITIME = "HI Time";
        private readonly String HEADER_HOTIME = "HO Time";
        private readonly String HEADER_SHF_AGENT = "S.Agent";
        private readonly String HEADER_FWD_AGNT = "F.Agent";
        private readonly String HEADER_CNG_SHP = "Cng/Shp";
        private readonly String HEADER_CARGO = "Cargo";
        private readonly String HEADER_PKG = "Pkg";
        private readonly String HEADER_WH = "WH";
        private readonly String HEADER_MT = "MT";
        private readonly String HEADER_M3 = "M3";
        private readonly String HEADER_QTY = "QTY";

        public const int TAB_HI = 1;
        public const int TAB_HO = 2;
        #endregion

        public HWC102(int tab)
        {
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid(tab);
            if (tab == HWC102.TAB_HI)
            {
                rbtnHI.Checked = true;
            }
            else if (tab == HWC102.TAB_HO)
            {
                rbtnHO.Checked = true;
            }
        }
        
        public IPopupResult ShowPopup(IPopupParm parm)
        {
            HWC102Parm hwc102Parm = (HWC102Parm)parm;
            if (hwc102Parm != null)
            {
                if (Constants.NONCALLID.Equals(hwc102Parm.VslCallId))
                {
                    rbtnNonJPVC.Checked = true;
                }
                else
                {
                    rbtnJPVC.Checked = true;
                    txtJPVC.Text = hwc102Parm.VslCallId;
                }
            }
            InitializeData();
            this.ShowDialog();
            return null;
        }

        private void InitializeDataGrid(int tab)
        {
            if (tab == HWC102.TAB_HI)
            {
                String[,] header = { { HEADER_CAT, "30" }, { HEADER_SNBL, "75" }, { HEADER_GRDO, "75" }, { HEADER_HITIME, "100" }, { HEADER_HOTIME, "0" }, { HEADER_SHF_AGENT, "40" }, { HEADER_FWD_AGNT, "40" }, { HEADER_CNG_SHP, "40" }, { HEADER_CARGO, "100" }, { HEADER_PKG, "40" }, { HEADER_WH, "50" }, { HEADER_MT, "50" }, { HEADER_M3, "50" }, { HEADER_QTY, "50" } };
                this.grdData.setHeader(header);
            }
            else if (tab == HWC102.TAB_HO)
            {
                String[,] header = { { HEADER_CAT, "30" }, { HEADER_SNBL, "75" }, { HEADER_GRDO, "75" }, { HEADER_HITIME, "0" }, { HEADER_HOTIME, "100" }, { HEADER_SHF_AGENT, "40" }, { HEADER_FWD_AGNT, "40" }, { HEADER_CNG_SHP, "40" }, { HEADER_CARGO, "100" }, { HEADER_PKG, "40" }, { HEADER_WH, "50" }, { HEADER_MT, "50" }, { HEADER_M3, "50" }, { HEADER_QTY, "50" } };
                this.grdData.setHeader(header);
            }
            else
            {
                String[,] header = { { HEADER_CAT, "30" }, { HEADER_SNBL, "75" }, { HEADER_GRDO, "75" }, { HEADER_HITIME, "100" }, { HEADER_HOTIME, "100" }, { HEADER_SHF_AGENT, "40" }, { HEADER_FWD_AGNT, "40" }, { HEADER_CNG_SHP, "40" }, { HEADER_CARGO, "100" }, { HEADER_PKG, "40" }, { HEADER_WH, "50" }, { HEADER_MT, "50" }, { HEADER_M3, "50" }, { HEADER_QTY, "50" } };
                this.grdData.setHeader(header);
            }
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                //txtFromTime.CustomFormat = Framework.Controls.UserControls.TDateTimePicker.FORMAT_DDMMYYYY;
                //txtToTime.CustomFormat = Framework.Controls.UserControls.TDateTimePicker.FORMAT_DDMMYYYY;
                //CommonUtility.SetDTPValueBlank(txtFromTime);
                //CommonUtility.SetDTPValueBlank(txtToTime);
                InitializeControls();

                #region Category
                cboCategory.Items.Add(new ComboboxValueDescriptionPair("", "All"));
                cboCategory.Items.Add(new ComboboxValueDescriptionPair("E", "Export"));
                cboCategory.Items.Add(new ComboboxValueDescriptionPair("I", "Import"));
                cboCategory.Items.Add(new ComboboxValueDescriptionPair("T", "TransShipment"));
                cboCategory.Items.Add(new ComboboxValueDescriptionPair("S", "Storage"));
                #endregion

                #region WHId
                GetWHComboList();
                #endregion

                /*
                 * lv.dat add retrieve data 20130625
                 */
                #region Retrieve
                F_Retrieve();
                #endregion
            }
            catch (Framework.Common.Exception.BusinessException ex)
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
                case "btnF1":
                    MOST.Common.CommonParm.SearchJPVCParm jpvcParm = new MOST.Common.CommonParm.SearchJPVCParm();
                    SearchJPVCResult jpvcResultTmp = (SearchJPVCResult)PopupManager.instance.ShowPopup(new HCM101(), jpvcParm);
                    if (jpvcResultTmp != null)
                    {
                        txtJPVC.Text = jpvcResultTmp.Jpvc;
                    }
                    break;

                case "btnRetrieve":
                    if (this.validations(this.Controls) && Validate())
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

        private void GetWHComboList()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                IWHCheckerProxy proxy = new WHCheckerProxy();
                CargoMasterParm parm = new CargoMasterParm();
                parm.locDivCd = "WHO";
                ResponseInfo info = proxy.getWHComboList(parm);
                CommonUtility.InitializeCombobox(cboWH, "Select");
                if (info != null)
                {
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is CargoMasterItem)
                        {
                            CargoMasterItem item = (CargoMasterItem)info.list[i];
                            cboWH.Items.Add(new ComboboxValueDescriptionPair(item.locId, item.locId));
                        }
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

        private void GetCargoHIList()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                IWHCheckerProxy proxy = new WHCheckerProxy();
                CargoHandlingInParm parm = new CargoHandlingInParm();
                parm.vslCallId = GetVslCallId();
                parm.locId = CommonUtility.GetComboboxSelectedValue(cboWH);
                parm.catgCd = CommonUtility.GetComboboxSelectedValue(cboCategory);
                parm.startDt = txtFromTime.Text;
                parm.endDt = txtToTime.Text;
                parm.workDT = UserInfo.getInstance().Workdate;
                parm.searchType = "HI";
                
                ResponseInfo info = proxy.getCargoHIList(parm);

                grdData.Clear();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CargoHandlingInItem)
                    {
                        CargoHandlingInItem item = (CargoHandlingInItem)info.list[i];
                        DataRow newRow = grdData.NewRow();
                        newRow[HEADER_CAT] = item.catgCd;
                        newRow[HEADER_SNBL] = item.blSn;
                        newRow[HEADER_GRDO] = item.grDo;
                        newRow[HEADER_HITIME] = item.hdlInDt;
                        newRow[HEADER_HOTIME] = string.Empty;
                        newRow[HEADER_SHF_AGENT] = item.shpgAgent;
                        newRow[HEADER_FWD_AGNT] = item.fwrAgnt;
                        newRow[HEADER_CNG_SHP] = item.shpCng;
                        newRow[HEADER_CARGO] = item.cargo;
                        newRow[HEADER_PKG] = item.pkgTpCd;
                        newRow[HEADER_WH] = item.currLocId;
                        newRow[HEADER_MT] = item.wgt;
                        newRow[HEADER_M3] = item.msrmt;
                        newRow[HEADER_QTY] = item.pkgQty;
                        grdData.Add(newRow);
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

        private void GetCargoHOList()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                IWHCheckerProxy proxy = new WHCheckerProxy();
                CargoHandlingOutParm parm = new CargoHandlingOutParm();
                parm.vslCallId = GetVslCallId();
                parm.locId = CommonUtility.GetComboboxSelectedValue(cboWH);
                parm.catgCd = CommonUtility.GetComboboxSelectedValue(cboCategory);
                parm.startDt = txtFromTime.Text;
                parm.endDt = txtToTime.Text;
                parm.workDT = UserInfo.getInstance().Workdate;
                parm.shftId = UserInfo.getInstance().Shift;
                parm.searchType = "HI";
                ResponseInfo info = proxy.getCargoHOList(parm);

                grdData.Clear();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CargoHandlingOutItem)
                    {
                        CargoHandlingOutItem item = (CargoHandlingOutItem)info.list[i];
                        DataRow newRow = grdData.NewRow();
                        newRow[HEADER_CAT] = item.catgCd;
                        newRow[HEADER_SNBL] = item.blSn;
                        newRow[HEADER_GRDO] = item.grDo;
                        newRow[HEADER_HITIME] = string.Empty;
                        newRow[HEADER_HOTIME] = item.hdlOutDt;
                        newRow[HEADER_SHF_AGENT] = item.shpgAgent;
                        newRow[HEADER_FWD_AGNT] = item.fwrAgnt;
                        newRow[HEADER_CNG_SHP] = item.shpCng;
                        newRow[HEADER_CARGO] = item.cargo;
                        newRow[HEADER_PKG] = item.pkgTpCd;
                        newRow[HEADER_WH] = item.currLocId;
                        newRow[HEADER_MT] = item.wgt;
                        newRow[HEADER_M3] = item.msrmt;
                        newRow[HEADER_QTY] = item.pkgQty;
                        grdData.Add(newRow);
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

        private void F_Retrieve()
        {
            if (rbtnHI.Checked)
            {
                GetCargoHIList();
            }
            else if (rbtnHO.Checked)
            {
                GetCargoHOList();
            }
        }

        private bool Validate()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (rbtnJPVC.Checked && string.IsNullOrEmpty(txtJPVC.Text) &&
                    string.IsNullOrEmpty(txtFromTime.Text) && string.IsNullOrEmpty(txtToTime.Text))
                {
                    CommonUtility.AlertMessage("Please input JPVC or Handle time.");
                    return false;
                }

                if (!CommonUtility.CheckDateStartEnd(txtFromTime, txtToTime))
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

        private void RadiobuttonListener(object sender, EventArgs e)
        {
            RadioButton mybutton = (RadioButton)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "rbtnHI":
                case "rbtnHO":
                    OnRbtnHIHOChecked();
                    break;

                case "rbtnJPVC":
                case "rbtnNonJPVC":
                    OnRbtnJPVCChecked();
                    break;
            }
        }

        private void OnRbtnJPVCChecked()
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

        private void OnRbtnHIHOChecked()
        {
            if (grdData != null && grdData.TableStyles != null && grdData.TableStyles.Count > 0)
            {
                DataGridTableStyle tableStyle = grdData.TableStyles[0];
                if (rbtnHI.Checked)
                {
                    tableStyle.GridColumnStyles[HEADER_HITIME].Width = 100;
                    tableStyle.GridColumnStyles[HEADER_HOTIME].Width = 0;
                }
                else if (rbtnHO.Checked)
                {
                    tableStyle.GridColumnStyles[HEADER_HITIME].Width = 0;
                    tableStyle.GridColumnStyles[HEADER_HOTIME].Width = 100;
                }
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

        private void InitializeControls()
        {
            //CommonUtility.SetDTPValueDMY(this.txtFromTime, UserInfo.getInstance().Workdate);
            //CommonUtility.SetDTPValueDMY(this.txtToTime, UserInfo.getInstance().Workdate);
            //txtFromTime.CustomFormat = Framework.Controls.UserControls.TDateTimePicker.FORMAT_DDMMYYYY;
            //txtToTime.CustomFormat = Framework.Controls.UserControls.TDateTimePicker.FORMAT_DDMMYYYY;
            CommonUtility.SetDTPWithinShift(txtFromTime, txtToTime);
        }
    }
}