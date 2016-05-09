using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using MOST.Client.Proxy.CommonProxy;
using Framework.Service.Provider.WebService.Provider;
using System.Collections;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;
using Framework.Controls.UserControls;
using Framework.Common.ExceptionHandler;
using MOST.Common.Utility;

namespace MOST.Common
{
    public partial class HCM110 : TDialog, IPopupWindow
    {
        #region Local Variable
        private const string CONST_CD = "CD";   // Search by CD
        private const string CONST_NM = "NM";   // Search by Name

        public const int TYPE_REQUESTER = 2;    // Requester POPUP
        public const int TYPE_COMMODITY = 3;    // Commodity POPUP
        public const int TYPE_DRIVER    = 4;    // Driver POPUP
        public const int TYPE_LORRY     = 6;    // Lorry POPUP
        public const int TYPE_CONTRACTOR = 7;   // Contractor POPUP
        public const int TYPE_FORWARDER = 8;    // Forwarder POPUP
        public const int TYPE_PKGTYPE   = 9;    // Package Type POPUP
        public const int TYPE_TRANSPORTER   = 10;    // Transporter Type POPUP
        public const int TYPE_BAGTYPE = 11;         // Bag Type POPUP
        public const int TYPE_STEVEDORE = 12;       // Stevedore POPUP
        private readonly String HEADER_CODE = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM110_0001");
        private readonly String HEADER_NAME = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM110_0002");
        private readonly String HEADER_LORRY_NO = "Lorry No";
        private readonly String HEADER_LORRY_ID = "Lorry ID";
        private readonly String HEADER_LORRY_COM = "Company";
        private readonly String HEADER_DRIVER_NO = "Driver IC";
        private readonly String HEADER_DRIVER_ID = "Driver Nm";
        private readonly String HEADER_DRIVER_COM = "Company";

        //private readonly String HEADER_STEVE_DORE = "Stevedore";
        private const string HEADER_LICSNO      = "LICSNO";         // License number
        private const string HEADER_LICSEXPRYMD = "LICSEXPRYMD";    // License expired date
        private int m_type;
        private int m_rowsCnt;
        private string m_ptnrCd; // Transporter Company Code
        private MOST.Common.CommonParm.PartnerCodeListParm vsParm;
        private PartnerCodeListResult m_result;

        //added by William (2015/07/21 - HHT) Mantis issue: 49799
        private string cr_Id = "";
        private readonly String HEADER_PTNR_CD = "Ptnr CD";

        //Added by Chris 2015-10-12
        public const string M_HWC101 = "HWC101";


        #endregion
       
        public HCM110(int type)
        {
            this.m_type = type;
            m_ptnrCd = "";
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            InitializeOthers();
        }

        //added by William (2015/07/21 - HHT) Mantis issue 49799
        public HCM110(int type, string cr_Id)
   
        {
            this.cr_Id = cr_Id;
            this.m_type = type;
            m_ptnrCd = "";
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            InitializeOthers();
        }

        private void InitializeDataGrid()
        {
            btnOk.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0013");
            btnCancel.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0014");
            btnSearch.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0015");

            if (string.IsNullOrEmpty(cr_Id) && m_type == HCM110.TYPE_LORRY)
            {
                String[,] header = { { HEADER_LORRY_NO, "70" }, { HEADER_LORRY_ID, "70" } };
                grdList.setHeader(header);
            }
            else if (cr_Id.Equals(CommonUtility.PORT_SAFETY) && m_type == HCM110.TYPE_LORRY)
            {
                String[,] header = { { HEADER_LORRY_NO, "70" }, { HEADER_LORRY_ID, "70" }, { HEADER_PTNR_CD, "70" } };
                grdList.setHeader(header);
            }
            else if (m_type == HCM110.TYPE_DRIVER)
            {
                String[,] header = { { HEADER_DRIVER_NO, "70" }, { HEADER_DRIVER_ID, "70" }, { HEADER_DRIVER_COM, "120" }, { HEADER_LICSNO, "0" }, { HEADER_LICSEXPRYMD, "0" } };
                grdList.setHeader(header);
            }
            else
            {
                String[,] header = { { HEADER_CODE, "70" }, { HEADER_NAME, "150" }, { HEADER_LICSNO, "0" }, { HEADER_LICSEXPRYMD, "0" } };
                grdList.setHeader(header);
            }
        }

        private void InitializeOthers()
        {
            // Set title
            switch (m_type)
            {
                case HCM110.TYPE_REQUESTER:
                    this.Text = "Requester";
                    break;

                case HCM110.TYPE_COMMODITY:
                    this.Text = "Commodity";
                    break;

                case HCM110.TYPE_DRIVER:
                    this.Text = "Driver";
                    break;

                case HCM110.TYPE_LORRY:
                    this.Text = "Lorry";
                    break;

                case HCM110.TYPE_CONTRACTOR:
                    this.Text = "Contractor";
                    break;

                case HCM110.TYPE_FORWARDER:
                    this.Text = "Forwarder";
                    break;

                case HCM110.TYPE_PKGTYPE:
                    this.Text = "Package Type";
                    break;

                case HCM110.TYPE_TRANSPORTER:
                    this.Text = "Transporter";
                    break;

                case HCM110.TYPE_BAGTYPE:
                    this.Text = "Bag Type";
                    break;
                case HCM110.TYPE_STEVEDORE:
                    this.Text = "Stevedore";
                    break;
            }

            // Options: Code/Name. Default option is Code.
            cboOption.Items.Clear();
            if (m_type == HCM110.TYPE_DRIVER)
            {
                cboOption.Items.Add(new ComboboxValueDescriptionPair(CONST_CD, "IC"));
                cboOption.Items.Add(new ComboboxValueDescriptionPair(CONST_NM, "Name"));
            }
            else
            {
                cboOption.Items.Add(new ComboboxValueDescriptionPair(CONST_CD, "Code"));
                cboOption.Items.Add(new ComboboxValueDescriptionPair(CONST_NM, "Name"));
            }
            cboOption.SelectedIndex = 0;
        }

        private void F_Search()
        {
            try
            {
                #region Request Webservice

                Cursor.Current = Cursors.WaitCursor;

                string tyCd = CommonUtility.GetComboboxSelectedValue(cboOption);

                ICommonProxy proxy = new CommonProxy();
                ResponseInfo info = null;

                if (m_type == HCM110.TYPE_REQUESTER || 
                    m_type == HCM110.TYPE_CONTRACTOR ||
                    m_type == HCM110.TYPE_FORWARDER ||
                    m_type == HCM110.TYPE_TRANSPORTER ||
                    m_type == HCM110.TYPE_STEVEDORE)
                {
                    PartnerCodeParm partnerParm = new PartnerCodeParm();
                    partnerParm.tyCd = tyCd;
                    if (m_type == HCM110.TYPE_REQUESTER)
                    {
                        partnerParm.ptyDivCd = "SHA";
                    }
                    else if (m_type == HCM110.TYPE_CONTRACTOR)
                    {
                        partnerParm.ptyDivCd = "CTT";
                    }
                    else if (m_type == HCM110.TYPE_FORWARDER)
                    {
                        partnerParm.ptyDivCd = "FWD";
                    }
                    else if (m_type == HCM110.TYPE_TRANSPORTER)
                    {
                        partnerParm.ptyDivCd = "TRK";
                    } 
                    else if (m_type == HCM110.TYPE_STEVEDORE)
                    {
                        partnerParm.ptyDivCd = "STV";
                    }
                    if (!string.IsNullOrEmpty(txtSearch.Text))
                    {
                        if (CONST_CD.Equals(tyCd))
                        {
                            partnerParm.ptyCd = txtSearch.Text;
                        }
                        else if (CONST_NM.Equals(tyCd))
                        {
                            partnerParm.engPtyNm = txtSearch.Text;
                        }
                    }

                    info = proxy.getPartnerCodeList(partnerParm);
                }
                else if (m_type == HCM110.TYPE_COMMODITY)
                {
                    CommonCodeParm commonParm = new CommonCodeParm();
                    commonParm.searchType = "CMDT";
                    commonParm.tyCd = tyCd;
                    if (!string.IsNullOrEmpty(txtSearch.Text))
                    {
                        if (CONST_CD.Equals(tyCd))
                        {
                            commonParm.cd = txtSearch.Text;
                        }
                        else if (CONST_NM.Equals(tyCd))
                        {
                            commonParm.cdNm = txtSearch.Text;
                        }
                    }

                    info = proxy.getCommonCodeList(commonParm);
                }
                else if (m_type == HCM110.TYPE_PKGTYPE || m_type == HCM110.TYPE_BAGTYPE)
                {
                    CommonCodeParm commonParm = new CommonCodeParm();
                    commonParm.searchType = "COMM";
                    commonParm.divCd = "PKGTP";
                    commonParm.lcd = "MT";
                    commonParm.tyCd = tyCd;
                    if (!string.IsNullOrEmpty(txtSearch.Text))
                    {
                        if (CONST_CD.Equals(tyCd))
                        {
                            commonParm.cd = txtSearch.Text;
                        }
                        else if (CONST_NM.Equals(tyCd))
                        {
                            commonParm.cdNm = txtSearch.Text;
                        }
                    }
                    if (m_type == HCM110.TYPE_BAGTYPE)
                    {
                        commonParm.col1 = "LB";
                    }

                    info = proxy.getCommonCodeList(commonParm);
                }
                //Modified by William (2015/07/21 - HHT) Mantis issue: 49799 [added scr_Id]
                else if ((string.IsNullOrEmpty(cr_Id) && m_type == HCM110.TYPE_LORRY) || m_type == HCM110.TYPE_DRIVER)
                {
                    AssignmentLorrysParm lorryParm = new AssignmentLorrysParm();
                    lorryParm.searchType = this.m_type != 6 ? "popUpList" : "popUpListForDc";
                    lorryParm.divCd = m_type == HCM110.TYPE_LORRY ? "LR" : "DV";
                    lorryParm.ptnrCd = CommonUtility.PreprocessPtnrCd(m_ptnrCd);
                    //Added by Chris 2015-09-24 for 49779
                    lorryParm.lorryNo = vsParm.LorryNo;
                    //---------------------------------
                    lorryParm.tyCd = tyCd;

                    if (this.vsParm != null)
                    {
                        if (!string.IsNullOrEmpty(vsParm.ScreenId))
                            lorryParm.screenId = vsParm.ScreenId;

                        if (!string.IsNullOrEmpty(vsParm.VslCallId))
                            lorryParm.vslCallId = vsParm.VslCallId;

                        if (!string.IsNullOrEmpty(vsParm.ShippingNoteNo))
                            lorryParm.shipgNoteNo = vsParm.ShippingNoteNo;

                        //Added By Chris 2015-10-12 for WareHouseChecker heap Dump
                        if (!string.IsNullOrEmpty(vsParm.ScreenId))
                        {
                            if (vsParm.ScreenId.Equals(M_HWC101))
                            {
                                if (!string.IsNullOrEmpty(vsParm.GrNo))
                                {
                                    lorryParm.grNo = vsParm.GrNo;
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(vsParm.BlNo))
                        {
                            lorryParm.blNo = vsParm.BlNo;
                        }


                    }

                    if (!string.IsNullOrEmpty(txtSearch.Text))
                    {
                        if (CONST_CD.Equals(tyCd))
                        {
                            lorryParm.cd = txtSearch.Text;
                        }
                        else if (CONST_NM.Equals(tyCd))
                        {
                            lorryParm.cdNm = txtSearch.Text;
                        }
                    }

                    info = proxy.getAssignmentLorrysItems(lorryParm);
                }
                //added by William (2015/07/21 - HHT) Mantis issue 49799
                else if (cr_Id.Equals(CommonUtility.PORT_SAFETY) && m_type == HCM110.TYPE_LORRY)
                {
                    LorryListParm lorryParm = new LorryListParm();
                    lorryParm.ptnrCd = CommonUtility.PreprocessPtnrCd(m_ptnrCd);
                    lorryParm.LORRYNO = txtSearch.Text;

                    info = proxy.GetLorryList(lorryParm);
                }
                #endregion

                #region Display Data
                grdList.Clear();
                if (info != null)
                {
                    DataRow newRow;
                    if (m_type == HCM110.TYPE_REQUESTER || 
                        m_type == HCM110.TYPE_CONTRACTOR ||
                        m_type == HCM110.TYPE_FORWARDER ||
                        m_type == HCM110.TYPE_TRANSPORTER ||
                        m_type == HCM110.TYPE_STEVEDORE)
                    {
                        for (int i = 0; i < info.list.Length; i++)
                        {
                            if (info.list[i] is PartnerCodeItem)
                            {
                                PartnerCodeItem item = (PartnerCodeItem)info.list[i];
                                newRow = grdList.NewRow();
                                newRow[HEADER_CODE] = item.ptyCd;
                                newRow[HEADER_NAME] = item.engPtyNm;
                                grdList.Add(newRow);
                            }
                        }
                    }
                    else if (m_type == HCM110.TYPE_COMMODITY || m_type == HCM110.TYPE_PKGTYPE || m_type == HCM110.TYPE_BAGTYPE)
                    {
                        for (int i = 0; i < info.list.Length; i++)
                        {
                            CodeMasterListItem1 item = info.list[i] as CodeMasterListItem1;
                            if (item == null && info.list[i] is CodeMasterListItem)
                                item = CommonUtility.ToCodeMasterListItem1(info.list[i] as CodeMasterListItem);
                            if (item != null)
                            {
                                newRow = grdList.NewRow();
                                newRow[HEADER_CODE] = item.scd;
                                newRow[HEADER_NAME] = item.scdNm;
                                grdList.Add(newRow);
                            }
                        }
                    }
                    //added by William (2015/07/21 - HHT) Mantis issue 49799
                    else if (string.IsNullOrEmpty(cr_Id) && m_type == HCM110.TYPE_LORRY)
                    {
                        for (int i = 0; i < info.list.Length; i++)
                        {
                            if (info.list[i] is AssignmentLorrysItem)
                            {
                                AssignmentLorrysItem item = (AssignmentLorrysItem)info.list[i];
                                newRow = grdList.NewRow();
                                newRow[HEADER_LORRY_NO] = item.lorryNo;
                                newRow[HEADER_LORRY_ID] = item.lorryId;
                                if (m_type != HCM110.TYPE_LORRY)
                                {
                                    newRow[HEADER_LORRY_COM] = item.ptnrCd;
                                    newRow[HEADER_LICSNO] = item.licsNo;
                                    newRow[HEADER_LICSEXPRYMD] = item.licsExprYmd;
                                }
                                grdList.Add(newRow);
                            }
                        }
                    }
                    //added by William (2015/07/21 - HHT) Mantis issue 49799
                    else if (cr_Id.Equals(CommonUtility.PORT_SAFETY) && m_type == HCM110.TYPE_LORRY)
                    {
                        for (int i = 0; i < info.list.Length; i++)
                        {
                            if (info.list[i] is LorryListItem)
                            {
                                LorryListItem item = (LorryListItem)info.list[i];
                                newRow = grdList.NewRow();
                                newRow[HEADER_LORRY_NO] = item.LORRYNO;
                                newRow[HEADER_LORRY_ID] = item.LORRYID;
                                newRow[HEADER_PTNR_CD] = item.ptnrCd;
                                grdList.Add(newRow);
                            }
                        }
                    }
                    else if (m_type == HCM110.TYPE_DRIVER)
                    {
                        for (int i = 0; i < info.list.Length; i++)
                        {
                            if (info.list[i] is AssignmentLorrysItem)
                            {
                                AssignmentLorrysItem item = (AssignmentLorrysItem)info.list[i];
                                newRow = grdList.NewRow();
                                newRow[HEADER_DRIVER_NO] = item.cd;
                                newRow[HEADER_DRIVER_ID] = item.cdNm;
                                newRow[HEADER_DRIVER_COM] = item.ptnrCd;
                                newRow[HEADER_LICSNO] = item.licsNo;
                                newRow[HEADER_LICSEXPRYMD] = item.licsExprYmd;
                                grdList.Add(newRow);
                            }
                        }
                    }
                    grdList.Refresh();

                    if (info.list != null)
                    {
                        m_rowsCnt = info.list.Length;
                    }
                }
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

        private void ReturnInfo()
        {
            int currRowIndex = grdList.CurrentRowIndex;
            m_result = new PartnerCodeListResult();

            if (m_type == HCM110.TYPE_LORRY)
            {
                m_result.LorryNo = grdList.DataTable.Rows[currRowIndex][HEADER_LORRY_NO].ToString();
                m_result.LorryId = grdList.DataTable.Rows[currRowIndex][HEADER_LORRY_ID].ToString();
                //Added By Chris 2015-10-19
                if (grdList.DataTable.Columns.Count>2)
                {
                    m_result.PtnrCD = grdList.DataTable.Rows[currRowIndex][HEADER_PTNR_CD].ToString();    
                }              
                
                if (m_type == HCM110.TYPE_LORRY)
                {
                    return;
                }
                m_result.LorryCom = grdList.DataTable.Rows[currRowIndex][HEADER_LORRY_COM].ToString();
                m_result.LicsNo = grdList.DataTable.Rows[currRowIndex][HEADER_LICSNO].ToString();
                m_result.LicsExprYmd = grdList.DataTable.Rows[currRowIndex][HEADER_LICSEXPRYMD].ToString();
            }
            else if (m_type == HCM110.TYPE_DRIVER)
            {
                m_result.DriverNo = grdList.DataTable.Rows[currRowIndex][HEADER_DRIVER_NO].ToString();
                m_result.DriverId = grdList.DataTable.Rows[currRowIndex][HEADER_DRIVER_ID].ToString();
                m_result.DriverCom = grdList.DataTable.Rows[currRowIndex][HEADER_DRIVER_COM].ToString();
                m_result.LicsNo = grdList.DataTable.Rows[currRowIndex][HEADER_LICSNO].ToString();
                m_result.LicsExprYmd = grdList.DataTable.Rows[currRowIndex][HEADER_LICSEXPRYMD].ToString();
            }
            else
            {
                m_result.Code = grdList.DataTable.Rows[currRowIndex][HEADER_CODE].ToString();
                m_result.Name = grdList.DataTable.Rows[currRowIndex][HEADER_NAME].ToString();
                m_result.LicsNo = null;
                m_result.LicsExprYmd = null;
            }
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            vsParm = (MOST.Common.CommonParm.PartnerCodeListParm)parm;
            m_ptnrCd = vsParm.PtnrCd;
            txtSearch.Text = vsParm.SearchItem;
            if (!string.IsNullOrEmpty(vsParm.Option))
            {
                CommonUtility.SetComboboxSelectedItem(cboOption, vsParm.Option);
            }

            //added by William (2015/07/21 - HHT) Mantis issue: 49799
            if (cr_Id.Equals(CommonUtility.PORT_SAFETY) && m_type == HCM110.TYPE_LORRY && vsParm.SearchItem != null && vsParm.SearchItem.Length >= 3)
            { F_Search(); 
            }
            else if (m_type == HCM110.TYPE_LORRY && vsParm.SearchItem != null)
            { 
                F_Search(); 
            }

            

            this.ShowDialog();

            return m_result;
        }

        #region Event Handler
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.validations(this.Controls))
            {
                //added by William (2015/07/21 - HHT) Mantis issue: 49799
                if (cr_Id.Equals(CommonUtility.PORT_SAFETY) && m_type == HCM110.TYPE_LORRY)
                {
                    if (txtSearch.Text.Trim().Length < 3)
                    {
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HPS101_0010"));
                        return;
                    }
                }

                F_Search();

                if (m_rowsCnt <= 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0058"));
                    txtSearch.Focus();
                    txtSearch.SelectAll();
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int currRowIndex = grdList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnInfo();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void grdList_DoubleClick(object sender, EventArgs e)
        {
            int currRowIndex = grdList.CurrentRowIndex;
            if (-1 < currRowIndex && currRowIndex < m_rowsCnt)
            {
                ReturnInfo();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        #endregion
    }
}