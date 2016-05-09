using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using MOST.Client.Proxy.CommonProxy;
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
    public partial class HAC107 : TDialog, IPopupWindow
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

        private bool m_initializedSN;
        private readonly String HEADER_SN = "S/N No";
        private readonly String HEADER_GR = "G/R No";
        private readonly String HEADER_DELV_MODE = "Delv. Mode";
        private readonly String HEADER_GROSS_MT = "Doc MT";
        private readonly String HEADER_GROSS_M3 = "Doc M3";
        private readonly String HEADER_GROSS_QTY = "Doc QTY";
        private readonly String HEADER_ACT_MT = "Act MT";
        private readonly String HEADER_ACT_M3 = "Act M3";
        private readonly String HEADER_ACT_QTY = "Act QTY";
        private readonly String HEADER_LORRY_NO = "Lorry";
        private readonly String HEADER_FWD_AGENT = "F.Agent";
        private readonly String HEADER_CG_TYPE = "Cargo type";
        private readonly String HEADER_SHIFT = "Shift";
        private readonly String HEADER_HATCH = "Hatch";
        private readonly String HEADER_LOADED_DATE = "Loaded Date";

        //QUANBTL 09-08-2012 fix Loading list retrieve performance START

        private List<ExportMfCtrlListItem> cargoItemList;
        private string defaultGr;

        //QUANBTL 09-08-2012 fix Loading list retrieve performance END

        /**
         * lv.dat add paging 20130612
         */
        private int iTotalPage = 0;
        private int iCurrentPage = 0;
        private int iNumbPerPage = Constants.iNumbPerPage;

        ExportMfCtrlListParm parm;

        #endregion

        public HAC107()
        {
            m_initializedSN = false;
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
            MOST.ApronChecker.Parm.HAC107Parm hac107Parm = (MOST.ApronChecker.Parm.HAC107Parm)parm;

            if (hac107Parm.GrInfo != null)
            {
                CommonUtility.SetComboboxSelectedItem((ComboBox)this.cboSNNo, hac107Parm.GrInfo.ShipgNoteNo);
                this.defaultGr = hac107Parm.GrInfo.GrNo;
            }
            else
            {
                this.defaultGr = string.Empty;
            }

            if (hac107Parm.JpvcInfo != null)
            {
                txtJPVC.Text = hac107Parm.JpvcInfo.Jpvc;
            }

            InitializeCboSNNo();
            //F_Retrieve(true);
            InitializeCboPaging();
            this.ShowDialog();
            return null;
        }

        private void InitializeDataGrid()
        {
            
            String[,] header = { { HEADER_SN, "90" }, { HEADER_GR, "68" }, { HEADER_DELV_MODE, "40" }, { HEADER_GROSS_MT, "60" }, { HEADER_GROSS_M3, "60" }, { HEADER_GROSS_QTY, "60" }, { HEADER_ACT_MT, "60" }, { HEADER_ACT_M3, "60" }, { HEADER_ACT_QTY, "60" }, { HEADER_LORRY_NO, "60" }, { HEADER_FWD_AGENT, "50" }, { HEADER_CG_TYPE, "70" }, { HEADER_SHIFT, "50" }, { HEADER_HATCH, "50" }, { HEADER_LOADED_DATE, "95" } };
            this.grdData.setHeader(header);
        }

        private void InitializeData()
        {
            #region Amount Mode (MT/M3/QTY)
            cboAmntMode.Items.Add(new ComboboxValueDescriptionPair(CONST_AMNTMODE_MT, CONST_AMNTMODE_MT));
            cboAmntMode.Items.Add(new ComboboxValueDescriptionPair(CONST_AMNTMODE_M3, CONST_AMNTMODE_M3));
            cboAmntMode.Items.Add(new ComboboxValueDescriptionPair(CONST_AMNTMODE_QTY, CONST_AMNTMODE_QTY));
            cboAmntMode.SelectedIndex = 0;
            #endregion

            #region Hatch, Shift
            try
            {
                IApronCheckerProxy proxy = new ApronCheckerProxy();
                ExportMfCtrlListParm cdparm = new ExportMfCtrlListParm();
                cdparm.searchType = "modeOfOpr";
                ResponseInfo info = proxy.getModeOfOpr(cdparm);
                CommonUtility.InitializeCombobox(cboHatch, "All");
                CommonUtility.InitializeCombobox(cboShift, "All");
                for (int i = 0; i < info.list.Length; i++)
                {
                    // Shift
                    if (info.list[i] is ShiftGroupDefItem)
                    {
                        ShiftGroupDefItem item = (ShiftGroupDefItem) info.list[i];
                        cboShift.Items.Add(new ComboboxValueDescriptionPair(item.shftId, item.shftNm));
                    }
                        // Hatch
                    else if (info.list[i] is CodeMasterListItem)
                    {
                        CodeMasterListItem item = (CodeMasterListItem) info.list[i];
                        cboHatch.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                    else if (info.list[i] is CodeMasterListItem1)
                    {
                        CodeMasterListItem1 item = (CodeMasterListItem1) info.list[i];
                        cboHatch.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                    }
                }

                CommonUtility.SetComboboxSelectedItem((ComboBox)this.cboShift, UserInfo.getInstance().Shift);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }

            #endregion
        }

        //QUANBTL 09-08-2012 fix Loading list retrieve performance START
        private void InitializeCboSNNo()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                IApronCheckerProxy proxy = new ApronCheckerProxy();

                CommonUtility.InitializeCombobox(cboSNNo, Constants.STRING_SELECT);

                ExportMfCtrlListParm SNparm = new ExportMfCtrlListParm();
                SNparm.jpvcNo = txtJPVC.Text;
                SNparm.searchType = "SNNo";
                SNparm.fwrAgent = txtFwdAgent.Text;
                SNparm.shift = CommonUtility.GetComboboxSelectedValue(cboShift);
                SNparm.shiftFromDt = UserInfo.getInstance().Workdate;
                SNparm.shiftToDt = UserInfo.getInstance().Workdate;
                SNparm.hatchNo = CommonUtility.GetComboboxSelectedValue(cboHatch);
                ResponseInfo info = proxy.getListOfLoading(SNparm);

                List<string> SNNoList = new List<string>();

                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is ExportMfCtrlListItem)
                    {
                        ExportMfCtrlListItem item = (ExportMfCtrlListItem)info.list[i];
                        if (!SNNoList.Contains(item.shipgNoteNo))
                        {
                            cboSNNo.Items.Add(new ComboboxValueDescriptionPair(item.shipgNoteNo, item.shipgNoteNo));
                            SNNoList.Add(item.shipgNoteNo);
                        }
                    }
                }

                /*ResponseInfo info = proxy.getListOfLoading(parm);
         
                List<string> tempList = new List<string>();
                cargoItemList = new List<ExportMfCtrlListItem>();
                cargoItemList.Clear();

                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is ExportMfCtrlListItem)
                    {
                        ExportMfCtrlListItem item = (ExportMfCtrlListItem)info.list[i];
                        if (!tempList.Contains(item.shipgNoteNo))
                        {
                            cboSNNo.Items.Add(item.shipgNoteNo);
                            tempList.Add(item.shipgNoteNo);
                        }

                        //QUANBTL 09-08-2012 fix Loading list retrieve performance START
                        cargoItemList.Add(item);
                        //QUANBTL 09-08-2012 fix Loading list retrieve performance END
                    }
                }*/
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

        /*
         * Add new row for datagrid
         * Data is 1 elem taken from cargoItemList
         */
        private void addnewRow(ExportMfCtrlListItem item, string cachGr)
        {
            DataRow newRow = grdData.NewRow();
            newRow[HEADER_SN] = item.shipgNoteNo;
            newRow[HEADER_GR] = item.grNo;
            newRow[HEADER_DELV_MODE] = item.delvTpCd;
            newRow[HEADER_GROSS_MT] = item.wgt;
            newRow[HEADER_GROSS_M3] = item.msrmt;
            newRow[HEADER_GROSS_QTY] = item.pkgQty;
            newRow[HEADER_ACT_MT] = item.totInWgt;
            newRow[HEADER_ACT_M3] = item.totInMsrmt;
            newRow[HEADER_ACT_QTY] = item.totInPkgQty;
            newRow[HEADER_LORRY_NO] = item.lorryNo;
            newRow[HEADER_FWD_AGENT] = item.fwrAgnt;
            newRow[HEADER_CG_TYPE] = item.cgTpCd;
            newRow[HEADER_SHIFT] = item.shftNm;
            newRow[HEADER_HATCH] = item.hatchNo;
            newRow[HEADER_LOADED_DATE] = item.loadEndDt;
            grdData.Add(newRow);

            //this.CalcAmount(item, cachGr);
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

        // For searching in object list
        #region Explicit predicate delegate

        private bool FindShipgNoteNo(ExportMfCtrlListItem item)
        {
            if (item.shipgNoteNo.Equals(CommonUtility.GetComboboxSelectedValue(cboSNNo)))
            {
                return true;
            }
            {
                return false;
            }
        }

        private bool FindGrNo(ExportMfCtrlListItem item)
        {
            if (item.grNo.IndexOf(this.defaultGr) > -1)
            {
                return true;
            }
            {
                return false;
            }
        }

        private bool FindHatchNo(ExportMfCtrlListItem item)
        {
            if (CommonUtility.GetComboboxSelectedValue(cboHatch).Equals(item.hatchNo))
            {
                return true;
            }
            {
                return false;
            }
        }

        private bool FindShift(ExportMfCtrlListItem item)
        {
            if (CommonUtility.GetComboboxSelectedValue(cboShift).Equals(item.shftId))
            {
                return true;
            }
            {
                return false;
            }
        }

        private bool FindFwrAgnt(ExportMfCtrlListItem item)
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

        private void F_Retrieve(bool isFirstInit)
        {
            try
            {
                // Ref: CT117
                Cursor.Current = Cursors.WaitCursor;

                #region Query from cargoItemList not use anymore

                // using linQ to query from List<CargoExportItem> cargoItemList
                /*string snNo = CommonUtility.GetComboboxSelectedValue(cboSNNo);
                string hatchNo = CommonUtility.GetComboboxSelectedValue(cboHatch);
                string shift = CommonUtility.GetComboboxSelectedValue(cboShift);
                string fwrAgent = "";
                string cachGr = "";
                List<ExportMfCtrlListItem> itemList = new List<ExportMfCtrlListItem>();

                if(txtFwdAgent != null){
                    fwrAgent = txtFwdAgent.Text;
                }
                if (this.cargoItemList.Count > 0)
                {
                    itemList.AddRange(this.cargoItemList);

                    if (!snNo.Equals(Constants.STRING_NULL))
                    {
                        itemList = itemList.FindAll(FindShipgNoteNo);
                    }

                    if (!hatchNo.Equals(Constants.STRING_NULL))
                    {
                        itemList = itemList.FindAll(FindHatchNo);
                    }

                    if (!shift.Equals(Constants.STRING_NULL))
                    {
                        itemList = itemList.FindAll(FindShift);
                    }

                    if (!fwrAgent.Equals(Constants.STRING_NULL))
                    {
                        itemList = itemList.FindAll(FindFwrAgnt);
                    }

                    if (!fwrAgent.Equals(Constants.STRING_NULL))
                    {
                        itemList = itemList.FindAll(FindFwrAgnt);
                    }

                    if (isFirstInit && !string.Empty.Equals(this.defaultGr))
                    {
                        itemList = itemList.FindAll(FindGrNo);
                        
                    }
                }*/

                #endregion

                #region Query Database

                IApronCheckerProxy proxy = new ApronCheckerProxy();
                ResponseInfo info = proxy.getListOfLoading(parm);

                //cargoItemList = new List<ExportMfCtrlListItem>();
                //cargoItemList.Clear();

                #endregion

                #region Display Data

                grdData.Clear();
                //reloadGrDetail();

                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is ExportMfCtrlListItem)
                    {
                        ExportMfCtrlListItem item = (ExportMfCtrlListItem)info.list[i];
                        this.addnewRow(item, "");
                    }
                }

                /*
                if (itemList.Count > 0)
                {    
                    foreach (ExportMfCtrlListItem item in itemList)
                    {
                        this.addnewRow(item, cachGr);
                        cachGr = item.grNo;
                        if (this.defaultGr.Equals(item.grNo))
                        {
                            CommonUtility.SetComboboxSelectedItem((ComboBox)this.cboSNNo,
                                item.shipgNoteNo);
                        }
                    }

                    grdData.Refresh();
                    RefreshAmount();
                }

                this.defaultGr = string.Empty;*/
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

        //QUANBTL 09-08-2012 fix Loading list retrieve performance END
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
                        InitializeCboSNNo();
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
                        //InitializeCboSNNo();
                        InitializeCboPaging();
                    }
                    break;
            }
        }

        private void txtJPVC_KeyPress(object sender, KeyPressEventArgs e)
        {
            m_initializedSN = false;

            // if key = Enter then initialize combobox shipping note
            if (e.KeyChar.Equals((char)Keys.Enter))
            {
                InitializeCboSNNo();
            }
        }

        private void txtJPVC_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJPVC.Text) && !m_initializedSN)
            {
                InitializeCboSNNo();
            }
        }

        private void txtJPVC_TextChanged(object sender, EventArgs e)
        {
            m_initializedSN = false;
        }

        private void cboAmntMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshAmount();
        }

        /*
        private void CalcAmount(ExportMfCtrlListItem item, string cachGr)
        {
            try
            {
                if (string.IsNullOrEmpty(cachGr) || !cachGr.Equals(item.grNo))
                {
                    m_docMt += CommonUtility.ParseDouble(item.wgt);
                    m_docM3 += CommonUtility.ParseDouble(item.msrmt);
                    m_docQty += CommonUtility.ParseDouble(item.pkgQty);
                }
                m_actMt += CommonUtility.ParseDouble(item.totInWgt);
                m_actM3 += CommonUtility.ParseDouble(item.totInMsrmt);
                m_actQty += CommonUtility.ParseDouble(item.totInPkgQty);
            }
            catch (Framework.Common.Exception.BusinessException ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }*/

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

        /**
         * lv.dat add 20130612
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
            parm = new ExportMfCtrlListParm();
            parm.jpvcNo = txtJPVC.Text;
            parm.searchType = "Loading";
            if (this.defaultGr != null && !String.Empty.Equals(this.defaultGr))
            {
                parm.grNo = this.defaultGr;
            }

            parm.shftDt = UserInfo.getInstance().Workdate.ToString();

            parm.snNo = CommonUtility.GetComboboxSelectedValue(cboSNNo);
            parm.hatchNo = CommonUtility.GetComboboxSelectedValue(cboHatch);
            parm.shift = CommonUtility.GetComboboxSelectedValue(cboShift);
            parm.fwrAgent = txtFwdAgent.Text;

            parm.currentPage = this.iCurrentPage.ToString();
            parm.numbPerPage = this.iNumbPerPage.ToString();
            parm.pageType = "LOL";
        }

        private void InitializeCboPaging()
        {
            cboPaging.Items.Clear();
            cboPaging.Text = "";

            initializeParameter();
            InitializeAmount();

            IApronCheckerProxy proxy = new ApronCheckerProxy();
            ResponseInfo numbInfo = proxy.getListOfLoadingNumbPage(parm);
            this.iTotalPage = getNumbPage((int)numbInfo.list[0]);

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

            F_Retrieve(true);
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
                ResponseInfo info = proxy.getListOfLoadingSummary(parm);

                ExportMfCtrlListItem item = (ExportMfCtrlListItem)info.list[0];

                reloadGrDetail();
                m_docMt += CommonUtility.ParseDouble(item.wgt);
                m_docM3 += CommonUtility.ParseDouble(item.msrmt);
                m_docQty += CommonUtility.ParseDouble(item.pkgQty);
                m_actMt += CommonUtility.ParseDouble(item.totInWgt);
                m_actM3 += CommonUtility.ParseDouble(item.totInMsrmt);
                m_actQty += CommonUtility.ParseDouble(item.totInPkgQty);

                RefreshAmount();
            }
            catch (Framework.Common.Exception.BusinessException ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private void HAC107_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q && e.Modifiers == Keys.Alt)
            {
                MessageBox.Show("Screen ID : " + this.Name + "\n\rScreen caption: " + this.Text, "Screen Information !");
            }
        }
    }
}