using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using MOST.Client.Proxy.ApronCheckerProxy;
using Framework.Service.Provider.WebService.Provider;
using System.Collections;
using Framework.Common.PopupManager;
using MOST.Common;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.Common.Utility;
using Framework.Controls.UserControls;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using Framework.Common.Constants;

namespace MOST.ApronChecker
{
    public partial class HAC108 : TDialog, IPopupWindow
    {
        #region Local Variable
        private const string CONST_AMNTMODE_MT = "MT";
        private const string CONST_AMNTMODE_M3 = "M3";
        private const string CONST_AMNTMODE_QTY = "QTY";

        private double m_docMt;
        private double m_actMt;
        private double m_docM3;
        private double m_actM3;
        private double m_docQty;
        private double m_actQty;

        private bool m_initializedBL;
        private readonly string HEADER_NO = "No";
        private readonly String HEADER_BL = "B/L No";
        private readonly String HEADER_DO = "D/O No";
        private readonly String HEADER_ACT_DELVMODE = "Act Delv";
        private readonly String HEADER_OPR_MODE = "Opr. Mode";
        private readonly String HEADER_GROSS_MT = "Gross MT";
        private readonly String HEADER_GROSS_M3 = "Gross M3";
        private readonly String HEADER_GROSS_QTY = "QTY";
        private readonly String HEADER_ACT_MT = "Act MT";
        private readonly String HEADER_ACT_M3 = "Act M3";
        private readonly String HEADER_ACT_QTY = "Act QTY";
        private readonly String HEADER_LORRY_NO = "Lorry";
        private readonly String HEADER_FWD_AGENT = "F.Agent";
        private readonly String HEADER_CG_TYPE = "Cargo type";
        private readonly String HEADER_SHIFT = "Shift";
        private readonly String HEADER_HATCH = "Hatch";
        private readonly String HEADER_DISCHARGE_DATE = "Discharged Date";

        //QUANBTL 09-08-2012 fix D/C list retrieve performance START

        private List<ImportMfCtrlListItem> cargoItemList;

        //QUANBTL 09-08-2012 fix D/C list retrieve performance END

        /**
         * lv.dat add paging 20130613
         */
        private int iTotalPage = 0;
        private int iCurrentPage = 0;
        private int iNumbPerPage = Constants.iNumbPerPage;
        private bool bIsFirst = true;

        ImportMfCtrlListParm parm;

        #endregion

        public HAC108()
        {
            m_initializedBL = false;
            InitializeComponent();
            this.initialFormSize();
            
            /*
                Authority Check
             */
            this.authorityCheck();

            InitializeDataGrid();
            InitializeData();
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            MOST.ApronChecker.Parm.HAC108Parm hac108Parm = (MOST.ApronChecker.Parm.HAC108Parm)parm;
            if (hac108Parm.JpvcInfo != null)
            {
                txtJPVC.Text = hac108Parm.JpvcInfo.Jpvc;
                InitializeCboBL();
            }
            if (hac108Parm.BlInfo != null)
            {
                CommonUtility.SetComboboxSelectedItem((ComboBox)this.cboBL, hac108Parm.BlInfo.Bl);
            }
            if (string.IsNullOrEmpty(CommonUtility.GetComboboxSelectedValue((ComboBox)this.cboBL)))
            {
                CommonUtility.SetComboboxSelectedItem((ComboBox)this.cboBL, "");
            }
            //F_Retrieve();
            InitializeCboPaging();
            this.ShowDialog();
            return null;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_NO, "25" }, { HEADER_BL, "90" }, { HEADER_DO, "90" }
                , { HEADER_ACT_DELVMODE, "70" }, { HEADER_OPR_MODE, "65" }, { HEADER_GROSS_MT, "60" }
                , { HEADER_GROSS_M3, "60" }, { HEADER_GROSS_QTY, "60" }, { HEADER_ACT_MT, "60" }
                , { HEADER_ACT_M3, "60" }, { HEADER_ACT_QTY, "60" }, { HEADER_LORRY_NO, "50" }
                , { HEADER_FWD_AGENT, "50" }, { HEADER_CG_TYPE, "70" }, { HEADER_SHIFT, "50" }
                , { HEADER_HATCH, "50" }, { HEADER_DISCHARGE_DATE, "95" } };
            this.grdData.setHeader(header);
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Amount Mode (MT/M3/QTY)
                cboAmntMode.Items.Add(new ComboboxValueDescriptionPair(CONST_AMNTMODE_MT, CONST_AMNTMODE_MT));
                cboAmntMode.Items.Add(new ComboboxValueDescriptionPair(CONST_AMNTMODE_M3, CONST_AMNTMODE_M3));
                cboAmntMode.Items.Add(new ComboboxValueDescriptionPair(CONST_AMNTMODE_QTY, CONST_AMNTMODE_QTY));
                cboAmntMode.SelectedIndex = 0;
                #endregion

                #region Operation Mode, Shift, Hatch
                IApronCheckerProxy proxy = new ApronCheckerProxy();
                ImportMfCtrlListParm cdparm = new ImportMfCtrlListParm();
                cdparm.searchType = "modeOfOpr";
                ResponseInfo info = proxy.getModeOfOpr(cdparm);
                CommonUtility.InitializeCombobox(cboActDelv, "All");
                CommonUtility.InitializeCombobox(cboShift, "All");
                CommonUtility.InitializeCombobox(cboHatch, "All");
                for (int i = 0; i < info.list.Length; i++)
                {
                    // Operation mode
                    if (info.list[i] is ImportMfCtrlListItem)
                    {
                        ImportMfCtrlListItem item = (ImportMfCtrlListItem)info.list[i];
                        cboActDelv.Items.Add(new ComboboxValueDescriptionPair(item.tsptTpCd, item.tsptTpCdNm));
                    }
                    // Shift
                    else if (info.list[i] is ShiftGroupDefItem)
                    {
                        ShiftGroupDefItem item = (ShiftGroupDefItem)info.list[i];
                        cboShift.Items.Add(new ComboboxValueDescriptionPair(item.shftId, item.shftNm));
                    }
                    // Hatch
                    else if (info.list[i] is CodeMasterListItem)
                    {
                        CodeMasterListItem item = (CodeMasterListItem)info.list[i];
                        cboHatch.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                    else if (info.list[i] is CodeMasterListItem1)
                    {
                        CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                        cboHatch.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    } 
                    CommonUtility.SetComboboxSelectedItem((ComboBox)this.cboShift, UserInfo.getInstance().ShiftNm);
                }
                #endregion
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

        //QUANBTL 09-08-2012 fix D/C list retrieve performance START

        private void InitializeCboBL()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                IApronCheckerProxy proxy = new ApronCheckerProxy();
                CommonUtility.InitializeCombobox(cboBL, Constants.STRING_SELECT);

                ImportMfCtrlListParm parmBL = new ImportMfCtrlListParm();

                parmBL.jpvcNo = txtJPVC.Text;

                DateTime dtTemp = CommonUtility.ParseStringToDate(UserInfo.getInstance().Workdate.ToString(),"dd/MM/yyyy");
                parmBL.shiftDate = dtTemp.ToString("MM/dd/yyyy");

                ResponseInfo info = proxy.getListOfDischargingNumbPage(parmBL);

                List<string> tempList = new List<string>();
                cargoItemList = new List<ImportMfCtrlListItem>();
                cargoItemList.Clear();
                
                if (info != null && info.list != null)
                {
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is ImportMfCtrlListItem)
                        {
                            ImportMfCtrlListItem item = (ImportMfCtrlListItem)info.list[i];

                            if (!tempList.Contains(item.blNo))
                            {
                                cboBL.Items.Add(new ComboboxValueDescriptionPair(item.blNo, item.blNo));
                                tempList.Add(item.blNo);
                            }
                            cargoItemList.Add(item);
                        }
                    }
                }

                /*IApronCheckerProxy proxy = new ApronCheckerProxy();
                CommonUtility.InitializeCombobox(cboBL, Constants.STRING_SELECT);
                ImportMfCtrlListParm parm = new ImportMfCtrlListParm();

                parm.jpvcNo = txtJPVC.Text;
                parm.shiftDate = UserInfo.getInstance().Workdate.ToString();

                ResponseInfo info = proxy.getListOfDischarging(parm);
                
                List<string> tempList = new List<string>();
                cargoItemList = new List<ImportMfCtrlListItem>();
                cargoItemList.Clear();
                
                if (info != null && info.list != null)
                {
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is ImportMfCtrlListItem)
                        {
                            ImportMfCtrlListItem item = (ImportMfCtrlListItem)info.list[i];

                            if (!tempList.Contains(item.blNo))
                            {
                                cboBL.Items.Add(new ComboboxValueDescriptionPair(item.blNo, item.blNo));
                                tempList.Add(item.blNo);
                            }
                            cargoItemList.Add(item);
                        }
                    }
                }*/
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
                m_initializedBL = true;
                Cursor.Current = Cursors.Default;
            }
        }

        /*
         * Reload G/R details for each time F_Retrieve is called
         */
        private void reloadGrDetail()
        {
            this.m_docMt = 0;
            this.m_docM3 = 0;
            this.m_docQty = 0;
            this.m_actMt = 0;
            this.m_actM3 = 0;
            this.m_actQty = 0;
        }

        private void addnewRow(ImportMfCtrlListItem item, string cachBL, int no)
        {
            DataRow newRow = grdData.NewRow();
            newRow[HEADER_NO] = no;
            newRow[HEADER_BL] = item.blNo;
            newRow[HEADER_DO] = item.delvOdrNo;
            newRow[HEADER_ACT_DELVMODE] = item.actDelvTpCd;
            newRow[HEADER_OPR_MODE] = item.oprsMode;
            newRow[HEADER_GROSS_MT] = item.wgt;
            newRow[HEADER_GROSS_M3] = item.vol;
            newRow[HEADER_GROSS_QTY] = item.pkgQty;
            newRow[HEADER_ACT_MT] = item.outWgt;
            newRow[HEADER_ACT_M3] = item.outMsrmt;
            newRow[HEADER_ACT_QTY] = item.outQty;
            newRow[HEADER_LORRY_NO] = item.lorryNo;
            newRow[HEADER_FWD_AGENT] = item.fwrAgnt;
            newRow[HEADER_CG_TYPE] = item.cgTpCd;
            newRow[HEADER_SHIFT] = item.shftNm;
            newRow[HEADER_HATCH] = item.hatchNo;
            newRow[HEADER_DISCHARGE_DATE] = item.disEndDt;
            grdData.Add(newRow);

            //CalcAmount(item, cachBL);
        }

        // For searching in object list
        #region Explicit predicate delegate

        private bool FindBl(ImportMfCtrlListItem item)
        {
            if (item.blNo.Equals(CommonUtility.GetComboboxSelectedValue(cboBL)))
            {
                return true;
            }
            {
                return false;
            }
        }

        private bool FindHatchNo(ImportMfCtrlListItem item)
        {
            if (CommonUtility.GetComboboxSelectedValue(cboHatch).Equals(item.hatchNo))
            {
                return true; 
            }
            {
                return false;
            }
        }

        private bool FindShift(ImportMfCtrlListItem item)
        {
            if (CommonUtility.GetComboboxSelectedValue(cboShift).Equals(item.shftNm))
            {
                return true;
            }
            {
                return false;
            }
        }

        private bool FindModeOfOpr(ImportMfCtrlListItem item)
        {
            if (CommonUtility.GetComboboxSelectedValue(cboActDelv).Equals(item.actDelvTpCd))
            {
                return true;
            }
            {
                return false;
            }
        }

        private bool FindFwrAgnt(ImportMfCtrlListItem item)
        {
            if (item.fwrAgnt.IndexOf(txtFwdAgent.Text) > -1)
            {
                return true;
            }
            {
                return false;
            }
        }

        #endregion 

        private void F_Retrieve()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Query from cargoItemList
                /*IApronCheckerProxy proxy = new ApronCheckerProxy();

                // using linQ to query from List<CargoExportItem> cargoItemList
                string blNo = CommonUtility.GetComboboxSelectedValue(cboBL);
                string hatchNo = CommonUtility.GetComboboxSelectedValue(cboHatch);
                string shift = CommonUtility.GetComboboxSelectedValue(cboShift);
                string modeOfOpr = CommonUtility.GetComboboxSelectedValue(cboActDelv);
                string fwrAgent = "";
                List<ImportMfCtrlListItem> itemList = new List<ImportMfCtrlListItem>();

                if (txtFwdAgent != null)
                {
                    fwrAgent = txtFwdAgent.Text;
                }
                if (this.cargoItemList.Count > 0)
                {
                    itemList.AddRange(this.cargoItemList);

                    if (!blNo.Equals(Constants.STRING_NULL))
                    {
                        itemList = itemList.FindAll(FindBl);
                    }

                    if (!hatchNo.Equals(Constants.STRING_NULL))
                    {
                        itemList = itemList.FindAll(FindHatchNo);
                    }

                    if (!shift.Equals(Constants.STRING_NULL))
                    {
                        itemList = itemList.FindAll(FindShift);
                    }

                    if (!modeOfOpr.Equals(Constants.STRING_NULL))
                    {
                        itemList = itemList.FindAll(FindModeOfOpr);
                    }

                    if (!fwrAgent.Equals(Constants.STRING_NULL))
                    {
                        itemList = itemList.FindAll(FindFwrAgnt);
                    }
                }                
                */
                #endregion

                #region Query from Database

                IApronCheckerProxy proxy = new ApronCheckerProxy();
                ResponseInfo info = proxy.getListOfDischarging(parm);

                #endregion

                #region Display Data

                //string cachBL = "";
                grdData.Clear();
                //reloadGrDetail();

                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is ImportMfCtrlListItem)
                    {
                        ImportMfCtrlListItem item = (ImportMfCtrlListItem)info.list[i];
                        this.addnewRow(item, "", (i + 1) + ((this.iCurrentPage - 1) * this.iNumbPerPage));
                    }
                }

                /*if (itemList.Count > 0)
                {
                    int num = 1;
                    foreach (ImportMfCtrlListItem item in itemList)
                    {
                        addnewRow(item, cachBL, num++);
                        cachBL = item.blNo;
                    }
                    grdData.Refresh();                    
                }
                RefreshAmount();*/
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

        //QUANBTL 09-08-2012 fix D/C list retrieve performance END
        /*
        private void CalcAmount(ImportMfCtrlListItem item, string cachBL)
        {
            try
            {
                if (string.IsNullOrEmpty(cachBL) || !cachBL.Equals(item.blNo))
                {
                    m_docMt += CommonUtility.ParseDouble(item.wgt);
                    m_docM3 += CommonUtility.ParseDouble(item.vol);
                    m_docQty += CommonUtility.ParseDouble(item.pkgQty);
                }

                m_actMt += CommonUtility.ParseDouble(item.outWgt);
                m_actM3 += CommonUtility.ParseDouble(item.outMsrmt);
                m_actQty += CommonUtility.ParseDouble(item.outQty);
            }
            catch (Framework.Common.Exception.BusinessException ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }
        */
        private void RefreshAmount()
        {
            try
            {
                string amntMode = CommonUtility.GetComboboxSelectedValue(cboAmntMode);
                switch (amntMode)
                {
                    case CONST_AMNTMODE_MT:
                        txtDoc.Text = m_docMt.ToString();
                        txtAct.Text = m_actMt.ToString();
                        double balMt = m_docMt - m_actMt;
                        balMt = Math.Round(balMt, 3);
                        txtBal.Text = balMt.ToString();
                        break;
                    case CONST_AMNTMODE_M3:
                        txtDoc.Text = m_docM3.ToString();
                        txtAct.Text = m_actM3.ToString();
                        double balM3 = m_docM3 - m_actM3;
                        balM3 = Math.Round(balM3, 3);
                        txtBal.Text = balM3.ToString();
                        break;
                    case CONST_AMNTMODE_QTY:
                        txtDoc.Text = m_docQty.ToString();
                        txtAct.Text = m_actQty.ToString();
                        double balQty = m_docQty - m_actQty;
                        balQty = Math.Round(balQty, 3);
                        txtBal.Text = balQty.ToString();
                        break;
                }
            }
            catch (Framework.Common.Exception.BusinessException ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnCancel":
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    break;

                case "btnF1":
                    MOST.Common.CommonParm.SearchJPVCParm jpvcParm = new MOST.Common.CommonParm.SearchJPVCParm();
                    jpvcParm.Jpvc = txtJPVC.Text.Trim();
                    jpvcParm.IsWHChecker = "VSL";
                    SearchJPVCResult jpvcResultTmp = (SearchJPVCResult)PopupManager.instance.ShowPopup(new HCM101(), jpvcParm);
                    if (jpvcResultTmp != null)
                    {
                        txtJPVC.Text = jpvcResultTmp.Jpvc;
                        InitializeCboBL();
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
                    if (this.validations(this.Controls))
                    {
                        //F_Retrieve();
                        InitializeCboPaging();
                    }
                    break;
            }
        }

        private void txtJPVC_KeyPress(object sender, KeyPressEventArgs e)
        {
            m_initializedBL = false;

            // if key = Enter then initialize combobox B/L
            if (e.KeyChar.Equals((char)Keys.Enter))
            {
                InitializeCboBL();
                InitializeCboPaging();
            }
        }

        private void txtJPVC_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJPVC.Text) && !m_initializedBL)
            {
                InitializeCboBL();
            }
        }

        private void txtJPVC_TextChanged(object sender, EventArgs e)
        {
            m_initializedBL = false;
        }

        private void cboAmntMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshAmount();
        }

        /**
         * lv.dat add 20130613
         */

        public int getNumbPage(int iNumbRow)
        {
            if (iNumbRow == 0)
                return iNumbRow;

            int iNumbPage = iNumbRow / this.iNumbPerPage;
            if ((iNumbPage == 0 && iNumbRow > 0) || iNumbRow % this.iNumbPerPage != 0)
                iNumbPage++;

            return iNumbPage;
        }

        private void initializeParameter()
        {
            parm = new ImportMfCtrlListParm();
            parm.jpvcNo = txtJPVC.Text;

            DateTime dtTemp = CommonUtility.ParseStringToDate(UserInfo.getInstance().Workdate.ToString(), "dd/MM/yyyy");
            parm.shiftDate = dtTemp.ToString("MM/dd/yyyy");

            parm.blNo = CommonUtility.GetComboboxSelectedValue(cboBL);
            parm.hatchNo = CommonUtility.GetComboboxSelectedValue(cboHatch);
            parm.shiftId = CommonUtility.GetComboboxSelectedValue(cboShift);
            parm.modeOfOpr = CommonUtility.GetComboboxSelectedValue(cboActDelv);
            parm.fwrAgnt = txtFwdAgent.Text;

            parm.currentPage = this.iCurrentPage.ToString();
            parm.numbPerPage = this.iNumbPerPage.ToString();
            parm.pageType = "LOD";
        }

        private void InitializeCboPaging()
        {
            cboPaging.Items.Clear();
            cboPaging.Text = "";

            initializeParameter();
            InitializeAmount();

            IApronCheckerProxy proxy = new ApronCheckerProxy();
            ResponseInfo info = proxy.getListOfDischargingNumbPage(parm);

            if (bIsFirst)
            {
                this.iTotalPage = getNumbPage(cargoItemList.Count);
                bIsFirst = false;
            }
            else
                this.iTotalPage = getNumbPage(info.list.Length);

            for (int i = 1; i <= this.iTotalPage; i++)
                this.cboPaging.Items.Add(i);

            if (this.iTotalPage > 0)
            {
                cboPaging.SelectedIndex = 0;
            }
            else
                grdData.Clear();
        }

        private void cboPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.iCurrentPage = int.Parse(((ComboBox)sender).Text);
            parm.currentPage = this.iCurrentPage.ToString();

            F_Retrieve();
        }

        private void executePaging(object sender, EventArgs e)
        {
            Button btnHandle = (Button)sender;
            switch (btnHandle.Name)
            {
                case "btnPrev":
                    if (string.IsNullOrEmpty(cboPaging.Text) || cboPaging.SelectedIndex <= 0)
                        return;
                    cboPaging.SelectedIndex--;
                    break;
                case "btnNext":
                    if (string.IsNullOrEmpty(cboPaging.Text) || cboPaging.SelectedIndex >= this.iTotalPage - 1)
                        return;
                    cboPaging.SelectedIndex++;
                    break;
                default:
                    break;
            }
        }

        private void InitializeAmount()
        {
            try
            {
                IApronCheckerProxy proxy = new ApronCheckerProxy();
                ResponseInfo info = proxy.getListOfDischargingSummary(parm);

                ImportMfCtrlListItem item = (ImportMfCtrlListItem)info.list[0];

                reloadGrDetail();
                this.m_docMt += CommonUtility.ParseDouble(item.wgt);
                this.m_docM3 += CommonUtility.ParseDouble(item.vol);
                this.m_docQty += CommonUtility.ParseDouble(item.pkgQty);
                this.m_actMt += CommonUtility.ParseDouble(item.outWgt);
                this.m_actM3 += CommonUtility.ParseDouble(item.outMsrmt);
                this.m_actQty += CommonUtility.ParseDouble(item.outQty);

                RefreshAmount();
            }
            catch (Framework.Common.Exception.BusinessException ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private void HAC108_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q && e.Modifiers == Keys.Alt)
            {
                MessageBox.Show("Screen ID : " + this.Name + "\n\rScreen caption: " + this.Text, "Screen Information !");
            }
        }
    }
}