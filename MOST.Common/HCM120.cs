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
using MOST.Common;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.Common.Utility;
using MOST.Client.Proxy.CommonProxy;
using Framework.Common.PopupManager;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using MOST.Client.Proxy.ApronCheckerProxy;
using Framework.Service.Provider.WebService.Provider;
using System.Threading;

namespace MOST.Common
{
    public partial class HCM120 : TForm, IPopupWindow
    {
        #region Local Variable
        private const string AREA_WHO = "WHO";
        private const string AREA_HATCH = "HTC";
        private const string AREA_WHARF = "WRF";

        private const string CONST_JPB = "JPB";
        private const string CONST_CTR = "CTR";
        private const string CONST_SHIPCR = "SHIPCR";
        private const string CONST_SHIPCR_NM = "SHIP CRANE";

        private int m_prg_type;
        private int m_mode;
        private int m_activeTab;
        private string m_vslCallId = string.Empty;
        private VSRDetailParm m_parm;
        private VSRDetailResult m_result;
        private ArrayList m_listCapaPC;
        private ArrayList m_listCapaDescrPC;
        private ArrayList m_listCapaFL;
        private ArrayList m_listCapaDescrFL;
        private EquipmentCodeResult m_eqCodeResult;  // Mechanical Equipment
        private EquipmentCodeResult m_trCodeResult;  // Trailer
        private WorkingAreaResult m_waFLResult;      // Working Area - Forklift
        private CheckListVSRItem m_updItem;
        private List<string> CONST_LIST_SHIPCR = new List<string>(new string[] { "SHIPCR1", "SHIPCR2", "SHIPCR3" });
        private Boolean isLoadMP = false;
        private Boolean isLoadPC = false;
        private Boolean isLoadTR = false;
        private Boolean isLoadST = false;
        private Boolean isLoadFL = false;
        private Boolean isLoadME = false;
        private String _Operator = string.Empty;
        private String _Mbscd = string.Empty;

        #endregion

        public HCM120()
        {
            InitializeComponent();
            this.initialFormSize();
            /*if (isInsert)bool isInsert
            {
                this.txtTR_Nos.Enabled = true;
                this.txtEQ_Nos.Enabled = true;
            }
            else
            {
                this.txtTR_Nos.Enabled = false;
                this.txtEQ_Nos.Enabled = false;
            }*/
        }

        public HCM120(int prgType, int mode)
            : this()
        {
            m_result = new VSRDetailResult();
            m_prg_type = prgType;
            m_mode = mode;
        }

        public HCM120(int prgType, int mode, int activeTab)
            : this(prgType, mode)
        {
            ActiveTab = activeTab;
        }

        // Fix issue 0032959   
        public void HideStevedoreTab()
        {
            tabChecklist.TabPages.RemoveAt(tabChecklist.TabPages.IndexOf(tabSTV));
        }

        public int ActiveTab
        {
            get { return m_activeTab; }
            set
            {
                m_activeTab = value;
                ActivateTab();
            }
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            this.m_parm = (VSRDetailParm)parm;
            m_vslCallId = (m_prg_type == Constants.NONJPVC) ? Constants.NONCALLID : m_parm.VslCallId;

            if (Constants.MODE_ADD.Equals(m_mode))
            {
                this.txtTR_Nos.Enabled = true;
                this.txtEQ_Nos.Enabled = true;
            }
            if (Constants.MODE_UPDATE.Equals(m_mode) &&
                m_parm != null && m_parm.ArrUpdGrdData.Count > 0 && m_parm.CurrIndex < m_parm.ArrUpdGrdData.Count)
            {
                m_updItem = m_parm.ArrUpdGrdData[m_parm.CurrIndex];
                if (Constants.ITEM_NOTCONFIRMED.Equals(m_updItem.IsFL) ||
                    Constants.ITEM_NOTCONFIRMED.Equals(m_updItem.IsTR) ||
                    Constants.ITEM_NOTCONFIRMED.Equals(m_updItem.IsME))
                    m_updItem.CRUD = Constants.WS_INSERT;
                this._Operator = m_updItem.@operator;
                this._Mbscd = m_updItem.mbsCd;

                    //  Description [William - 2014.01.06] : If item is not confirmed then textbox Nos is enable  = false
                    //  else if item is confirmed then check 2 case: if refN has contain letter @ [Case Insert] then  textbox Nos is enable = true, else [case update]
                    //  then  textbox Nos is enable = false
                    /*if (Constants.ITEM_NOTCONFIRMED.Equals(m_updItem.IsME))
                    {
                        this.txtEQ_Nos.Enabled = false;
                    }
                    else if (Constants.ITEM_CONFIRMED.Equals(m_updItem.IsME))
                    {
                        if (m_updItem.refNo.IndexOf("@") > 0)
                            this.txtEQ_Nos.Enabled = true;
                        else this.txtEQ_Nos.Enabled = false;
                    }
                    */

                    if (Constants.ITEM_NOTCONFIRMED.Equals(m_updItem.IsTR))
                    {
                        this.txtTR_Nos.Enabled = false;
                    }
                    else if (Constants.ITEM_CONFIRMED.Equals(m_updItem.IsTR))
                    {
                        if (m_updItem.refNo.IndexOf("@") > 0)
                            this.txtTR_Nos.Enabled = true;
                        else this.txtTR_Nos.Enabled = false;
                    }


            }
            InitializeData();
            DisplayUpdData();
            //InitializeAllRefNo();
            this.ShowDialog();
            return m_result;
        }

        private void ActivateTab()
        {
            /*
             * In case of ADD: 
             *      - In case of JPVC: display all tabs (exclude tab Stevedore) and select the active one.
             *      - In case of NONJPVC: display all tabs (exclude tab PortCrane) and select the active one.
             * In case of UPDATE: display active tab only.
             *  0:tabMan
             *  1:tabPC
             *  2:tabST
             *  3:tabFL
             *  4:tabTR
             *  5:tabMEQ
             */
            if (m_mode == Constants.MODE_ADD)
            {
                if (m_prg_type == Constants.JPVC)
                {
                    //tabChecklist.TabPages.RemoveAt(2);        //issue 0026198 : doesnot remove ST tab by Phuong
                }
                else if (m_prg_type == Constants.NONJPVC)
                {
                    tabChecklist.TabPages.RemoveAt(1);
                }
                if (m_prg_type == Constants.JPVC)
                {
                    switch (m_activeTab)
                    {
                        case HCM119.TAB_MANPOWER:
                            //tabChecklist.SelectedIndex = 0;
                            tabChecklist.SelectedIndex = tabChecklist.TabPages.IndexOf(tabMan);
                            this.initializeTabs(this.tabMan.Name);
                            break;
                        case HCM119.TAB_PORTCRANE:
                            //tabChecklist.SelectedIndex = 1;
                            tabChecklist.SelectedIndex = tabChecklist.TabPages.IndexOf(tabPC);
                            this.initializeTabs(this.tabPC.Name);
                            break;
                        case HCM119.TAB_STEVEDORE:
                            //tabChecklist.SelectedIndex = 2;
                            tabChecklist.SelectedIndex = tabChecklist.TabPages.IndexOf(tabSTV);
                            this.initializeTabs(this.tabSTV.Name);
                            break;
                        case HCM119.TAB_FORKLIFT:
                            //tabChecklist.SelectedIndex = 3;
                            tabChecklist.SelectedIndex = tabChecklist.TabPages.IndexOf(tabFL);
                            this.initializeTabs(this.tabFL.Name);
                            break;
                        case HCM119.TAB_TRAILER:
                            //tabChecklist.SelectedIndex = 4;
                            tabChecklist.SelectedIndex = tabChecklist.TabPages.IndexOf(tabTR);
                            this.initializeTabs(this.tabTR.Name);
                            break;
                        case HCM119.TAB_EQU:
                            //tabChecklist.SelectedIndex = 5;
                            tabChecklist.SelectedIndex = tabChecklist.TabPages.IndexOf(tabMEQ);
                            this.initializeTabs(this.tabMEQ.Name);
                            break;
                    }
                }
                else
                {
                    switch (m_activeTab)
                    {
                        case HCM119.TAB_MANPOWER:
                            this.initializeTabs(this.tabMan.Name);
                            break;
                        case HCM119.TAB_PORTCRANE:
                            //tabChecklist.SelectedIndex = 0;
                            tabChecklist.SelectedIndex = tabChecklist.TabPages.IndexOf(tabMan);
                            this.initializeTabs(this.tabPC.Name);
                            break;
                        case HCM119.TAB_STEVEDORE:
                            //tabChecklist.SelectedIndex = 1;
                            tabChecklist.SelectedIndex = tabChecklist.TabPages.IndexOf(tabSTV);
                            this.initializeTabs(this.tabSTV.Name);
                            break;
                        case HCM119.TAB_FORKLIFT:
                            //tabChecklist.SelectedIndex = 2;
                            tabChecklist.SelectedIndex = tabChecklist.TabPages.IndexOf(tabFL);
                            this.initializeTabs(this.tabFL.Name);
                            break;
                        case HCM119.TAB_TRAILER:
                            //tabChecklist.SelectedIndex = 3;
                            tabChecklist.SelectedIndex = tabChecklist.TabPages.IndexOf(tabTR);
                            this.initializeTabs(this.tabTR.Name);
                            break;
                        case HCM119.TAB_EQU:
                            //tabChecklist.SelectedIndex = 4;
                            tabChecklist.SelectedIndex = tabChecklist.TabPages.IndexOf(tabMEQ);
                            this.initializeTabs(this.tabMEQ.Name);
                            break;
                    }
                }
            }
            else if (m_mode == Constants.MODE_UPDATE)
            {
                int removeIndex = 0;
                while (tabChecklist.TabPages.Count > 1)
                {
                    if (removeIndex == 0 &&
                        ((m_activeTab == HCM119.TAB_MANPOWER && tabChecklist.TabPages[0] == tabMan) ||
                         (m_activeTab == HCM119.TAB_PORTCRANE && tabChecklist.TabPages[0] == tabPC) ||
                         (m_activeTab == HCM119.TAB_STEVEDORE && tabChecklist.TabPages[0] == tabSTV) ||
                         (m_activeTab == HCM119.TAB_FORKLIFT && tabChecklist.TabPages[0] == tabFL) ||
                         (m_activeTab == HCM119.TAB_TRAILER && tabChecklist.TabPages[0] == tabTR) ||
                         (m_activeTab == HCM119.TAB_EQU && tabChecklist.TabPages[0] == tabMEQ)))
                    {
                        removeIndex++;
                        continue;
                    }
                    if (removeIndex < tabChecklist.TabPages.Count)
                    {
                        tabChecklist.TabPages.RemoveAt(removeIndex);
                    }
                }

                switch (m_activeTab)
                {
                    case HCM119.TAB_MANPOWER:
                        this.initializeTabs(this.tabMan.Name);
                        break;
                    case HCM119.TAB_PORTCRANE:
                        this.initializeTabs(this.tabPC.Name);
                        break;
                    case HCM119.TAB_STEVEDORE:
                        this.initializeTabs(this.tabSTV.Name);
                        break;
                    case HCM119.TAB_FORKLIFT:
                        this.initializeTabs(this.tabFL.Name);
                        break;
                    case HCM119.TAB_TRAILER:
                        this.initializeTabs(this.tabTR.Name);
                        break;
                    case HCM119.TAB_EQU:
                        this.initializeTabs(this.tabMEQ.Name);
                        break;
                }
            }
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // Button Add/Update, Exit/Cancel
                if (m_mode == Constants.MODE_UPDATE)
                {
                    btnAddUpd.Text = "Update";
                    btnExitCancel.Text = "Cancel";
                }
                else if (m_mode == Constants.MODE_ADD)
                {
                    btnAddUpd.Text = "Add";
                    btnExitCancel.Text = "Exit";

                    m_result.ArrGrdDataMP = m_parm.ArrGrdDataMP;
                    m_result.ArrGrdDataPC = m_parm.ArrGrdDataPC;
                    m_result.ArrGrdDataST = m_parm.ArrGrdDataST;
                    m_result.ArrGrdDataFL = m_parm.ArrGrdDataFL;
                    m_result.ArrGrdDataTR = m_parm.ArrGrdDataTR;
                    m_result.ArrGrdDataEQ = m_parm.ArrGrdDataEQ;
                }

                // user want EQ Arr time now is start time of shift issue 46960 10/6/2014 lvdat
                // Date time - no need to set blank anymore
                //CommonUtility.SetDTPValueBlank(txtFL_EqArrDt);
                //CommonUtility.SetDTPValueBlank(txtTR_EqArrDt);
                //CommonUtility.SetDTPValueBlank(txtEQ_EqArrDt);
                if (m_mode == Constants.MODE_ADD)
                {
                    //if (m_prg_type == Constants.JPVC)                 //issue 0026198 : doesnot remove ST tab by Phuong
                    //{ 
                    CommonUtility.SetDTPWithinShift(txtPC_StartTime, txtPC_EndTime);
                    //}
                    //else if (m_prg_type == Constants.NONJPVC)
                    //{
                    CommonUtility.SetDTPWithinShift(txtST_StartTime, txtST_EndTime);
                    //}
                    CommonUtility.SetDTPWithinShift(txtMan_StartTime, txtMan_EndTime);
                    CommonUtility.SetDTPWithinShift(txtFL_StartTime, txtFL_EndTime);
                    CommonUtility.SetDTPWithinShift(txtTR_StartTime, txtTR_EndTime);
                    CommonUtility.SetDTPWithinShift(txtEQ_StartTime, txtEQ_EndTime);
                    // user want EQ Arr time now is start time of shift issue 46960 10/6/2014 lvdat
                    String startShift = txtFL_StartTime.Value.ToString("dd/MM/yyyy HH:mm");
                    CommonUtility.SetDTPValueDMYHM(txtFL_EqArrDt, startShift);
                    CommonUtility.SetDTPValueDMYHM(txtTR_EqArrDt, startShift);
                    CommonUtility.SetDTPValueDMYHM(txtEQ_EqArrDt, startShift);
                }
                else
                {
                    CommonUtility.SetDTPValueBlank(txtMan_StartTime);
                    CommonUtility.SetDTPValueBlank(txtMan_EndTime);
                    CommonUtility.SetDTPValueBlank(txtPC_StartTime);
                    CommonUtility.SetDTPValueBlank(txtPC_EndTime);
                    CommonUtility.SetDTPValueBlank(txtFL_StartTime);
                    CommonUtility.SetDTPValueBlank(txtFL_EndTime);
                    CommonUtility.SetDTPValueBlank(txtTR_StartTime);
                    CommonUtility.SetDTPValueBlank(txtTR_EndTime);
                    CommonUtility.SetDTPValueBlank(txtEQ_StartTime);
                    CommonUtility.SetDTPValueBlank(txtEQ_EndTime);
                }

                // Initialize data
                m_listCapaPC = new ArrayList();
                m_listCapaFL = new ArrayList();
                m_listCapaDescrPC = new ArrayList();
                m_listCapaDescrFL = new ArrayList();
                if (m_mode == Constants.MODE_ADD)
                {
                    InitializeAllTabs();

                }
                else if (m_mode == Constants.MODE_UPDATE)
                {
                    switch (m_activeTab)
                    {
                        case HCM119.TAB_MANPOWER:
                            InitializeTabMP();
                            break;
                        case HCM119.TAB_PORTCRANE:
                            InitializeTabPC();
                            break;
                        case HCM119.TAB_STEVEDORE:
                            InitializeTabST();
                            break;
                        case HCM119.TAB_FORKLIFT:
                            InitializeTabFL();
                            break;
                        case HCM119.TAB_TRAILER:
                            InitializeTabTR();
                            break;
                        case HCM119.TAB_EQU:
                            InitializeTabEQ();
                            break;
                    }
                }
            }
            /*
        catch (Framework.Common.Exception.BusinessException ex)
        {
            ExceptionHandler.ErrorHandler(this, ex);
        }
             */

            catch (Exception ex)
            {
                CommonUtility.AlertMessage(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void InitializeTabMP()
        {
            #region Tab Manpower
            new Thread(new ThreadStart(delegate()
                {
                    SetComboboxRoleCd();
                    DisplayUpdData();
                })).Start();
            #endregion
        }

        private void InitializeTabPC()
        {
            //rbtnPC_PortCrane.Checked = true;
            //rbtnPC_JPB.Checked = true;

            #region Tab Port Crane
            // Requester
            //SetComboboxRequester(cboPC_Requester);

            new Thread(new ThreadStart(delegate()
                {
                    // Cargo Type
                    SetComboboxCargoTp(cboPC_CgTp);
                    // Purpose
                    SetComboboxPurpose(cboPC_Purpose);
                    // PC No
                    //if (rbtnPC_PortCrane.Checked)
                    //SetComboboxPortCrane();
                    SetComboboxDeployedEmpId(cboPC_JPB, "PC");

                    DisplayUpdData();
                }))
                        .Start();
            #endregion
        }

        private void InitializeTabST()
        {
            #region Tab Stevedore
            #endregion
        }

        private void InitializeTabFL()
        {
            rbtnFL_JPB.Checked = true;

            #region Tab Forklift
            new Thread(new ThreadStart(delegate()
                {
                    this.SetHatchDirectionAPFP(cboFL_HatchDir);
                    this.SetRefnoInit(pnlFL_RefNo);
                    // FL No
                    SetComboboxForklift();
                    DisplayUpdData();
                })).Start();

            new Thread(new ThreadStart(delegate()
                {
                    // JPB
                    SetComboboxDeployedEmpId(cboFL_JPB, "FL");
                    //// Category
                    //SetComboboxWArea(cboFL_WArea);
                    // Delivery Mode
                    SetComboboxDelvMode(cboFL_DMode);
                    DisplayUpdData();
                })).Start();

            new Thread(new ThreadStart(delegate()
                {
                    // Cargo Type
                    SetComboboxCargoTp(cboFL_CgTp);
                    // Requester
                    //SetComboboxRequester(cboFL_Requester);
                    // Purpose
                    SetComboboxPurpose(cboFL_Purpose);
                    DisplayUpdData();
                })).Start();
            #endregion
        }

        private void InitializeTabTR()
        {
            #region Tab Trailer
            // AP/FP
            CommonUtility.SetHatchDirectionAPFP(cboTR_HatchDir);

            new Thread(new ThreadStart(delegate()
                {
                    this.SetRefnoInit(pnlTR_RefNo);
                    // Category
                    SetComboboxWArea(cboTR_WArea);
                    // Delivery Mode
                    SetComboboxDelvMode(cboTR_DMode);
                    DisplayUpdData();
                })).Start();

            new Thread(new ThreadStart(delegate()
                {
                    // Cargo Type
                    SetComboboxCargoTp(cboTR_CgTp);
                    // Purpose
                    SetComboboxPurpose(cboTR_Purpose);
                    DisplayUpdData();
                })).Start();
            #endregion
        }

        private void InitializeTabEQ()
        {
            rbtnEQ_Cnrt.Checked = true;

            #region Tab EQ

            new Thread(new ThreadStart(delegate()
                {
                    this.SetRefnoInit(pnlME_RefNo);
                    // Category
                    SetComboboxWArea(cboEQ_WArea);
                    DisplayUpdData();
                })).Start();

            new Thread(new ThreadStart(delegate()
                {
                    // Cargo Type
                    SetComboboxCargoTp(cboEQ_CgTp);
                    // Purpose
                    SetComboboxPurpose(cboEQ_Purpose);
                    DisplayUpdData();
                })).Start();
            #endregion
        }

        private void InitializeAllTabs()
        {
            if (m_prg_type == Constants.JPVC)
            {
                // MP, PC, FL, TR, EQ
                rbtnPC_PortCrane.Checked = true;
                rbtnPC_JPB.Checked = true;
                rbtnFL_JPB.Checked = true;
                rbtnFL_JPB.Checked = true;
                rbtnEQ_Cnrt.Checked = true;

                new Thread(new ThreadStart(delegate()
                {
                    // Role(MP)
                    SetComboboxRoleCd();

                    // PC No (PC)
                    SetComboboxPortCrane();

                    // Cargo Type (PC, FL, TR, EQ)
                    SetComboboxCargoTp(cboPC_CgTp, cboFL_CgTp, cboTR_CgTp, cboEQ_CgTp);

                    // Purpose (PC, FL, TR, EQ)
                    SetComboboxPurpose(cboPC_Purpose, cboFL_Purpose, cboTR_Purpose, cboEQ_Purpose);

                    // refno
                    this.SetRefnoInit(pnlFL_RefNo, pnlTR_RefNo, pnlME_RefNo);
                }))
                        .Start();


                // Requester (PC, FL)
                //SetComboboxRequester(cboPC_Requester, cboFL_Requester);

                new Thread(new ThreadStart(delegate()
                {
                    // FL No (FL)
                    SetComboboxForklift();
                }))
                        .Start();


                new Thread(new ThreadStart(delegate()
                {
                    // JPB (FL)
                    SetComboboxDeployedEmpId(cboFL_JPB, "FL");

                    // Category (TR, EQ)
                    SetComboboxWArea(cboTR_WArea, cboEQ_WArea);

                    // Delivery Mode (FL, TR)
                    SetComboboxDelvMode(cboFL_DMode, cboTR_DMode);

                    // AP/FP
                    this.SetHatchDirectionAPFP(cboTR_HatchDir);
                    this.SetHatchDirectionAPFP(cboFL_HatchDir);
                }))
                        .Start();
            }
            else if (m_prg_type == Constants.NONJPVC)
            {
                // MP, ST, FL, TR, EQ
                rbtnFL_JPB.Checked = true;
                //rbtnFL_JPB.Checked = true;
                rbtnEQ_Cnrt.Checked = true;

                new Thread(new ThreadStart(delegate()
                {
                    // Role(MP)
                    SetComboboxRoleCd();


                    // Requester (FL)
                    //SetComboboxRequester(cboFL_Requester);

                    // Cargo Type (FL, TR, EQ)
                    SetComboboxCargoTp(cboFL_CgTp, cboTR_CgTp, cboEQ_CgTp);

                    // Purpose (FL, TR, EQ)
                    SetComboboxPurpose(cboFL_Purpose, cboTR_Purpose, cboEQ_Purpose);

                    // Refno (FL, TR, EQ)
                    this.SetRefnoInit(pnlFL_RefNo, pnlTR_RefNo, pnlME_RefNo);
                }))
                        .Start();

                new Thread(new ThreadStart(delegate()
                {
                    // FL No (FL)
                    SetComboboxForklift();
                }))
                       .Start();

                new Thread(new ThreadStart(delegate()
                {
                    // JPB (FL)
                    SetComboboxDeployedEmpId(cboFL_JPB, "FL");

                    // Category (TR, EQ)
                    SetComboboxWArea(cboTR_WArea, cboEQ_WArea);

                    // Delivery Mode (FL, TR)
                    SetComboboxDelvMode(cboFL_DMode, cboTR_DMode);

                }))
                      .Start();

                // AP/FP (TR)
                CommonUtility.SetHatchDirectionAPFP(cboTR_HatchDir);
                this.SetHatchDirectionAPFP(cboFL_HatchDir);
            }
        }

        private void SetComboboxRoleCd()
        {
            // ManPower - Role
            try
            {
                IApronCheckerProxy proxy = new ApronCheckerProxy();
                CheckListVSRParm parm = new CheckListVSRParm();
                parm.searchType = "roleCd";
                ResponseInfo info = proxy.getVSRList(parm);

                Action<object> proc = delegate(object state)
                {
                    CommonUtility.InitializeCombobox(cboMan_Role);
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is CodeMasterListItem)
                        {
                            CodeMasterListItem item = (CodeMasterListItem)info.list[i];
                            cboMan_Role.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                        }
                        else if (info.list[i] is CodeMasterListItem1)
                        {
                            CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                            cboMan_Role.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                        }
                    }
                };

                if (this.InvokeRequired)
                {
                    this.Invoke(proc, true);
                }
                else
                {
                    proc.Invoke(null);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        public void SetHatchDirectionAPFP(ComboBox cboAPFP)
        {
            //CommonUtility.initializeCombobox(cboAPFP);
            //cboAPFP.Items.Add(new ComboboxValueDescriptionPair("FP", "FP"));
            //cboAPFP.Items.Add(new ComboboxValueDescriptionPair("AP", "AP"));

            // Request Webservice
            ICommonProxy proxy = new CommonProxy();
            CommonCodeParm parm = new CommonCodeParm();
            parm.searchType = "COMM";
            parm.lcd = "MT";
            parm.divCd = "HCHDRT";
            ResponseInfo resInfo = proxy.getCommonCodeList(parm);

            // Display Data
            Action<object> proc = delegate(object state)
                {
                    CommonUtility.InitializeCombobox(cboAPFP, "");
                    for (int i = 0; i < resInfo.list.Length; i++)
                    {
                        if (resInfo.list[i] is CodeMasterListItem)
                        {
                            CodeMasterListItem item = (CodeMasterListItem)resInfo.list[i];
                            cboAPFP.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scd));
                        }
                        else if (resInfo.list[i] is CodeMasterListItem1)
                        {
                            CodeMasterListItem1 item = (CodeMasterListItem1)resInfo.list[i];
                            cboAPFP.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scd));
                        }
                    }
                };
            if (this.InvokeRequired)
            {
                this.Invoke(proc, true);
            }
            else
            {
                proc.Invoke(null);
            }
        }

        private void SetComboboxForklift()
        {
            // Forklift - Forklift no
            try
            {
                IApronCheckerProxy proxy = new ApronCheckerProxy();
                CheckListVSRParm parm = new CheckListVSRParm();
                parm.searchType = "HHT_FLNo";
                ResponseInfo info = proxy.getVSRList(parm);
                Action<object> proc = delegate(object state)
                {
                    CommonUtility.InitializeCombobox(cboFL_FLNo);
                    m_listCapaFL.Clear();
                    m_listCapaDescrFL.Clear();
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is CheckListVSRItem)
                        {
                            CheckListVSRItem item = (CheckListVSRItem)info.list[i];
                            cboFL_FLNo.Items.Add(new ComboboxValueDescriptionPair(item.eqNo, item.engNm));
                            m_listCapaFL.Add(item.capaCd);
                            m_listCapaDescrFL.Add(item.capaDescr);
                        }
                    }
                };
                if (this.InvokeRequired)
                {
                    this.Invoke(proc, true);
                }
                else
                {
                    proc.Invoke(null);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private void SetComboboxPortCrane()
        {
            try
            {
                // PC No
                IApronCheckerProxy proxy = new ApronCheckerProxy();
                CheckListVSRParm parm = new CheckListVSRParm();
                parm.searchType = "HHT_DeployedPCNo";
                parm.vslCallID = m_vslCallId;
                parm.shftId = UserInfo.getInstance().Shift;
                parm.workYmd = UserInfo.getInstance().Workdate;
                ResponseInfo info = proxy.getVSRList(parm);

                Action<object> proc = delegate(object state)
                {
                    CommonUtility.InitializeCombobox(cboPC_EQNo);
                    m_listCapaPC.Clear();
                    m_listCapaDescrPC.Clear();
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is CheckListVSRItem)
                        {
                            CheckListVSRItem item = (CheckListVSRItem)info.list[i];
                            cboPC_EQNo.Items.Add(new ComboboxValueDescriptionPair(item.eqNo, item.engNm));
                            m_listCapaPC.Add(item.capaCd);
                            m_listCapaDescrPC.Add(item.capaDescr);
                        }
                    }
                };

                if (this.InvokeRequired)
                {
                    this.Invoke(proc, true);
                }
                else
                {
                    proc.Invoke(null);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private void SetComboboxDeployedEmpId(ComboBox cboEmpId, string eqTp)
        {
            try
            {
                IApronCheckerProxy proxy = new ApronCheckerProxy();
                CheckListVSRParm parm = new CheckListVSRParm();
                parm.searchType = "EmpId";
                parm.eqTp = eqTp;
                parm.vslCallID = m_vslCallId;
                parm.shftId = UserInfo.getInstance().Shift;
                parm.workYmd = UserInfo.getInstance().Workdate;
                ResponseInfo info = proxy.getVSRList(parm);

                Action<object> proc = delegate(object state)
                {
                    CommonUtility.InitializeCombobox(cboEmpId);
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is CheckListVSRItem)
                        {
                            CheckListVSRItem item = (CheckListVSRItem)info.list[i];
                            cboEmpId.Items.Add(new ComboboxValueDescriptionPair(item.empId, item.empNm));
                        }
                    }
                };

                if (this.InvokeRequired)
                {
                    this.Invoke(proc, true);
                }
                else
                {
                    proc.Invoke(null);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private void SetComboboxWArea(params ComboBox[] cboCtrls)
        {
            // Working Area
            foreach (ComboBox ctrl in cboCtrls)
            {
                Action<object> proc = delegate(object state)
                {
                    CommonUtility.InitializeCombobox(ctrl);
                    ctrl.Items.Add(new ComboboxValueDescriptionPair(AREA_HATCH, "Hatch"));
                    ctrl.Items.Add(new ComboboxValueDescriptionPair(AREA_WHARF, "Bulk Wharf"));
                    ctrl.Items.Add(new ComboboxValueDescriptionPair(AREA_WHO, "Warehouse"));
                };

                if (this.InvokeRequired)
                {
                    this.Invoke(proc, true);
                }
                else
                {
                    proc.Invoke(null);
                }
            }
        }

        private void SetComboboxDelvMode(params ComboBox[] cboCtrls)
        {
            // Delivery Mode
            foreach (ComboBox ctrl in cboCtrls)
            {
                Action<object> proc = delegate(object state)
                {
                    CommonUtility.InitializeCombobox(ctrl);
                    ctrl.Items.Add(new ComboboxValueDescriptionPair("D", "DIRECT"));
                    ctrl.Items.Add(new ComboboxValueDescriptionPair("I", "INDIRECT"));
                };

                if (this.InvokeRequired)
                {
                    this.Invoke(proc, true);
                }
                else
                {
                    proc.Invoke(null);
                }
            }
        }

        private void SetComboboxCargoTp(params ComboBox[] cboCtrls)
        {
            try
            {
                // Cargo Type
                ICommonProxy commonProxy = new CommonProxy();
                CommonCodeParm partyCode = new CommonCodeParm();
                partyCode.searchType = "COMM";
                partyCode.lcd = "MT";
                partyCode.divCd = "CGTPNLQ";
                ResponseInfo commonInfo = commonProxy.getCommonCodeList(partyCode);
                Action<object> proc = delegate(object state)
                {
                    foreach (ComboBox ctrl in cboCtrls)
                    {
                        CommonUtility.InitializeCombobox(ctrl);
                    }

                    for (int i = 0; i < commonInfo.list.Length; i++)
                    {
                        if (commonInfo.list[i] is CodeMasterListItem)
                            commonInfo.list[i] = CommonUtility.ToCodeMasterListItem1(commonInfo.list[i] as CodeMasterListItem);
                        if (commonInfo.list[i] is CodeMasterListItem1)
                        {
                            CodeMasterListItem1 item = (CodeMasterListItem1)commonInfo.list[i];
                            foreach (ComboBox ctrl in cboCtrls)
                            {
                                ctrl.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                            }
                        }
                    }

                    // now cgType combobox default is BBK issue 46960 11/6/2014 lvdat
                    foreach (ComboBox ctrl in cboCtrls)
                    {
                        CommonUtility.SetComboboxSelectedItem(ctrl, "BBK");
                    }
                };
                if (this.InvokeRequired)
                {
                    this.Invoke(proc, true);
                }
                else
                {
                    proc.Invoke(null);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private void SetComboboxPurpose(params ComboBox[] cboCtrls)
        {
            try
            {
                IApronCheckerProxy proxy = new ApronCheckerProxy();
                CheckListVSRParm parm = new CheckListVSRParm();
                parm.searchType = "HHT_MegaPurpose";
                parm.shftId = UserInfo.getInstance().Shift;
                parm.workYmd = UserInfo.getInstance().Workdate;
                ResponseInfo info = proxy.getVSRList(parm);
                Action<object> proc = delegate(object state)
                {
                    foreach (ComboBox ctrl in cboCtrls)
                    {
                        CommonUtility.InitializeCombobox(ctrl);
                    }

                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is CodeMasterListItem)
                            info.list[i] = CommonUtility.ToCodeMasterListItem1(info.list[i] as CodeMasterListItem);
                        if (info.list[i] is CodeMasterListItem1)
                        {
                            CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                            foreach (ComboBox ctrl in cboCtrls)
                            {
                                ctrl.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                            }
                        }
                    }
                };

                if (this.InvokeRequired)
                {
                    this.Invoke(proc, true);
                }
                else
                {
                    proc.Invoke(null);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private void SetComboboxRequester(params ComboBox[] cboCtrls)
        {
            try
            {
                IApronCheckerProxy proxy = new ApronCheckerProxy();
                CheckListVSRParm parm = new CheckListVSRParm();
                parm.searchType = "HHTPayerList";
                parm.shftId = UserInfo.getInstance().Shift;
                parm.workYmd = UserInfo.getInstance().Workdate;
                ResponseInfo info = proxy.getVSRList(parm);
                Action<object> proc = delegate(object state)
                {
                    foreach (ComboBox ctrl in cboCtrls)
                    {
                        CommonUtility.InitializeCombobox(ctrl);
                    }

                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is CheckListVSRItem)
                        {
                            CheckListVSRItem item = (CheckListVSRItem)info.list[i];
                            foreach (ComboBox ctrl in cboCtrls)
                            {
                                ctrl.Items.Add(new ComboboxValueDescriptionPair(item.payer, item.payer));
                            }
                        }
                    }
                };

                if (this.InvokeRequired)
                {
                    this.Invoke(proc, true);
                }
                else
                {
                    proc.Invoke(null);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private void DisplayUpdData()
        {
            Action<object> proc = delegate(object state)
                {
                    if (m_mode == Constants.MODE_UPDATE && m_updItem != null)
                    {
                        switch (m_activeTab)
                        {
                            case HCM119.TAB_MANPOWER:

                                CommonUtility.SetComboboxSelectedItem(cboMan_Role, m_updItem.rsNm);
                                txtMan_JPB.Text = m_updItem.empId;
                                txtMan_JPBName.Text = m_updItem.empNm;
                                txtMan_WArea.Text = m_updItem.workLoc;
                                CommonUtility.SetDTPValueDMYHM(txtMan_StartTime, m_updItem.workStDt);
                                CommonUtility.SetDTPValueDMYHM(txtMan_EndTime, m_updItem.workEndDt);
                                pnlMan_Remark.txt_Remark.Text = m_updItem.rmk;

                                break;

                            case HCM119.TAB_PORTCRANE:
                                txtPC_Cnrt.Enabled = false;
                                btnPC_F1.Enabled = false;

                                //if (CONST_SHIPCR.Equals(m_updItem.rsNm))
                                if (CONST_LIST_SHIPCR.Contains(m_updItem.rsNm))
                                {
                                    rbtnPC_ShipCrane.Checked = true;
                                }
                                else
                                {
                                    rbtnPC_PortCrane.Checked = true;
                                }
                                CommonUtility.SetComboboxSelectedItem(cboPC_EQNo, m_updItem.rsNm);
                                txtPC_Capacity.Text = m_updItem.capaDescr;
                                if (CONST_JPB.Equals(m_updItem.mbsCd))
                                {
                                    rbtnPC_JPB.Checked = true;
                                    CommonUtility.SetComboboxSelectedItem(cboPC_JPB, m_updItem.empId);
                                }
                                else if (CONST_CTR.Equals(m_updItem.mbsCd))
                                {
                                    rbtnPC_Cnrt.Checked = true;
                                    txtPC_Cnrt.Text = m_updItem.cnrtCd;
                                }

                                txtPC_Requester.Text = m_updItem.payer;
                                CommonUtility.SetComboboxSelectedItem(cboPC_Purpose, m_updItem.purpose);
                                CommonUtility.SetDTPValueDMYHM(txtPC_StartTime, m_updItem.workStDt);
                                CommonUtility.SetDTPValueDMYHM(txtPC_EndTime, m_updItem.workEndDt);
                                pnlPC_Remark.txt_Remark.Text = m_updItem.rmk;
                                break;

                            case HCM119.TAB_STEVEDORE:
                                txtST_Stvd.Text = m_updItem.stvdComp;
                                txtST_Sprr.Text = m_updItem.nofStvdSprr;
                                txtST_NonTon.Text = m_updItem.stvdNonTon;
                                txtST_WArea.Text = m_updItem.workLoc;
                                txtST_Requester.Text = m_updItem.payer;
                                CommonUtility.SetDTPValueDMYHM(txtST_StartTime, m_updItem.workStDt);
                                CommonUtility.SetDTPValueDMYHM(txtST_EndTime, m_updItem.workEndDt);
                                CommonUtility.SetComboboxSelectedItem(cboST_Purpose, m_updItem.purpose);
                                break;

                            case HCM119.TAB_FORKLIFT:
                                cboFL_FLNo.Enabled = true;
                      

                                CommonUtility.SetComboboxSelectedItem(cboFL_FLNo, m_updItem.rsNm);
                                txtFL_Capacity.Text = m_updItem.capaDescr;
                                if (CONST_JPB.Equals(m_updItem.mbsCd))
                                {
                                    rbtnFL_JPB.Checked = true;
                                    txtFL_Cnrt.Enabled = false;
                                    btnFL_F2.Enabled = false;
                                    CommonUtility.SetComboboxSelectedItem(cboFL_JPB, m_updItem.empId);
                                }
                                else if (CONST_CTR.Equals(m_updItem.mbsCd))
                                {
                                    rbtnFL_Cnrt.Checked = true;
                                    txtFL_Cnrt.Enabled = true;
                                    btnFL_F2.Enabled = true;
                                    txtFL_Cnrt.Text = m_updItem.cnrtCd;
                                }
                                else if (String.Empty.Equals(m_updItem.mbsCd) || " ".Equals(m_updItem.mbsCd))
                                {
                                    rbtnFL_Driver.Checked = true;
                                }
                                txtFL_Requester.Text = m_updItem.payer;
                                if (!string.IsNullOrEmpty(m_updItem.cgTpCd))
                                    CommonUtility.SetComboboxSelectedItem(cboFL_CgTp, m_updItem.cgTpCd);
                                CommonUtility.SetComboboxSelectedItem(cboFL_Purpose, m_updItem.purpose);
                                if (!string.IsNullOrEmpty(m_updItem.delvTpCd))
                                    CommonUtility.SetComboboxSelectedItem(cboFL_DMode, m_updItem.delvTpCd);
                                CommonUtility.SetDTPValueDMYHM(txtFL_EqArrDt, m_updItem.setupTime);
                                CommonUtility.SetDTPValueDMYHM(txtFL_StartTime, m_updItem.workStDt);
                                CommonUtility.SetDTPValueDMYHM(txtFL_EndTime, m_updItem.workEndDt);
                                txtFL_WArea.Text = m_updItem.workLoc;
                                CommonUtility.SetComboboxSelectedItem(cboFL_HatchDir, m_updItem.hatchDir);
                                if (!string.IsNullOrEmpty(m_updItem.refNo))
                                {
                                    //pnlFL_RefNo.txt_RefNo.Text = m_updItem.refNo;
                                    //pnlFL_RefNo.rbtn_RefTxt.Checked = true;
                                    if (pnlFL_RefNo.txt_RefNo.Text.Equals(m_updItem.refNo))
                                        pnlFL_RefNo.rbtn_RefTxt.Checked = true;
                                    else
                                    {
                                        CommonUtility.SetComboboxSelectedItem(pnlFL_RefNo.cbo_RefNo, m_updItem.refNo);
                                        pnlFL_RefNo.rbtn_RefCbo.Checked = true;
                                    }
                                }
                                else
                                    pnlFL_RefNo.rbtn_RefTxt.Checked = true;

                                pnlFL_Remark.txt_Remark.Text = m_updItem.rmk;
                                break;
                            case HCM119.TAB_TRAILER:
                                txtTR_EQTp.Text = m_updItem.rsNm;
                                if ("Trailer 40'".Equals(m_updItem.capaDescr))
                                {
                                    cboTR_HatchDir.isMandatory = true;
                                }
                                else
                                {
                                    cboTR_HatchDir.isMandatory = false;
                                }
                                txtTR_Nos.Text = m_updItem.rsQty;
                                //CommonUtility.SetComboboxSelectedItem(cboTR_WAreaDtl, m_updItem.workLoc);
                                txtTR_Cntr.Text = m_updItem.cnrtCd;
                                if (!string.IsNullOrEmpty(m_updItem.cgTpCd))
                                    CommonUtility.SetComboboxSelectedItem(cboTR_CgTp, m_updItem.cgTpCd);
                                txtTR_Requester.Text = m_updItem.payer;
                                CommonUtility.SetComboboxSelectedItem(cboTR_Purpose, m_updItem.purpose);
                                if (!string.IsNullOrEmpty(m_updItem.delvTpCd))
                                    CommonUtility.SetComboboxSelectedItem(cboTR_DMode, m_updItem.delvTpCd);
                                CommonUtility.SetDTPValueDMYHM(txtTR_EqArrDt, m_updItem.setupTime);
                                CommonUtility.SetDTPValueDMYHM(txtTR_StartTime, m_updItem.workStDt);
                                CommonUtility.SetDTPValueDMYHM(txtTR_EndTime, m_updItem.workEndDt);
                                if (Constants.ITEM_NOTCONFIRMED.Equals(m_updItem.IsTR))
                                    m_updItem.workLocTp = CommonUtility.GetWorkAreaType(m_updItem.workLoc);
                                CommonUtility.SetComboboxSelectedItem(cboTR_WArea, m_updItem.workLocTp);
                                CommonUtility.SetComboboxSelectedItem(cboTR_WAreaDtl, m_updItem.workLoc);
                                CommonUtility.SetComboboxSelectedItem(cboTR_HatchDir, m_updItem.hatchDir);
                                if (!string.IsNullOrEmpty(m_updItem.refNo))
                                {
                                    //pnlTR_RefNo.txt_RefNo.Text = m_updItem.refNo;
                                    //pnlTR_RefNo.rbtn_RefTxt.Checked = true;
                                    if (pnlTR_RefNo.txt_RefNo.Text.Equals(m_updItem.refNo))
                                        pnlTR_RefNo.rbtn_RefTxt.Checked = true;
                                    else
                                    {
                                        CommonUtility.SetComboboxSelectedItem(pnlTR_RefNo.cbo_RefNo, m_updItem.refNo);
                                        pnlTR_RefNo.rbtn_RefCbo.Checked = true;
                                    }
                                }
                                else
                                    pnlTR_RefNo.rbtn_RefTxt.Checked = true;

                                pnlTR_Remark.txt_Remark.Text = m_updItem.rmk;
                                break;

                            case HCM119.TAB_EQU:
                                txtEQ_WO.Text = m_updItem.workOdrNo;
                                txtEQ_EQType.Text = m_updItem.rsNm;
                                txtEQ_Nos.Text = m_updItem.rsQty;
                                CommonUtility.SetComboboxSelectedItem(cboEQ_WAreaDtl, m_updItem.workLoc);
                                if ("Y".Equals(m_updItem.shpCrew))
                                {
                                    rbtnEQ_ShipCrew.Checked = true;
                                }
                                else
                                {
                                    rbtnEQ_Cnrt.Checked = true;
                                    txtEQ_Cnrt.Text = m_updItem.cnrtCd;
                                }
                                if (!string.IsNullOrEmpty(m_updItem.cgTpCd))
                                    CommonUtility.SetComboboxSelectedItem(cboEQ_CgTp, m_updItem.cgTpCd);
                                CommonUtility.SetComboboxSelectedItem(cboEQ_Purpose, m_updItem.purpose);
                                txtEQ_Requester.Text = m_updItem.payer;
                                CommonUtility.SetDTPValueDMYHM(txtEQ_EqArrDt, m_updItem.setupTime);
                                CommonUtility.SetDTPValueDMYHM(txtEQ_StartTime, m_updItem.workStDt);
                                CommonUtility.SetDTPValueDMYHM(txtEQ_EndTime, m_updItem.workEndDt);
                                if (Constants.ITEM_NOTCONFIRMED.Equals(m_updItem.IsME))
                                    m_updItem.workLocTp = CommonUtility.GetWorkAreaType(m_updItem.workLoc);
                                CommonUtility.SetComboboxSelectedItem(cboEQ_WArea, m_updItem.workLocTp);
                                CommonUtility.SetComboboxSelectedItem(cboEQ_WAreaDtl, m_updItem.workLoc);
                                if (!string.IsNullOrEmpty(m_updItem.refNo))
                                {
                                    //pnlME_RefNo.txt_RefNo.Text = m_updItem.refNo;
                                    //pnlME_RefNo.rbtn_RefTxt.Checked = true;
                                    if (pnlME_RefNo.txt_RefNo.Text.Equals(m_updItem.refNo))
                                        pnlME_RefNo.rbtn_RefTxt.Checked = true;
                                    else
                                    {
                                        CommonUtility.SetComboboxSelectedItem(pnlME_RefNo.cbo_RefNo, m_updItem.refNo);
                                        pnlME_RefNo.rbtn_RefCbo.Checked = true;
                                    }
                                }
                                else
                                    pnlME_RefNo.rbtn_RefTxt.Checked = true;

                                pnlEQ_Remark.txt_Remark.Text = m_updItem.rmk;
                                break;
                        }
                    }
                };

            if (this.InvokeRequired)
            {
                this.Invoke(proc, true);
            }
            else
            {
                proc.Invoke(null);
            }
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnAddUpd":
                    if (m_mode == Constants.MODE_ADD)
                    {
                        if (AddItem())
                        {
                            //CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HAC103_0001"));
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
                        }
                    }
                    else if (m_mode == Constants.MODE_UPDATE)
                    {
                        if (UpdateItem())
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            this.Close();
                        }
                    }
                    break;

                case "btnExitCancel":
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    break;

                case "btnMan_F1":
                    ContractorListParm jpbManParm = new ContractorListParm();
                    jpbManParm.ShftDt = UserInfo.getInstance().Workdate;
                    jpbManParm.ShftId = UserInfo.getInstance().Shift;
                    jpbManParm.RoleCd = CommonUtility.GetComboboxSelectedValue(cboMan_Role);
                    ContractorListResult jpbManResult = (ContractorListResult)PopupManager.instance.ShowPopup(new MOST.Common.HCM106(), jpbManParm);
                    if (jpbManResult != null)
                    {
                        txtMan_JPB.Text = jpbManResult.EmpId;
                        txtMan_JPBName.Text = jpbManResult.EmpNm;
                        CommonUtility.SetComboboxSelectedItem(cboMan_Role, jpbManResult.RoleCd);
                    }
                    break;

                case "btnMan_F2":
                    WorkingAreaParm waManParm = new WorkingAreaParm();
                    waManParm.WorkingArea = txtMan_WArea.Text;
                    WorkingAreaResult waManRes = (WorkingAreaResult)PopupManager.instance.ShowPopup(new HCM113(HCM113.TYPE_MANPOWER), waManParm);
                    if (waManRes != null)
                    {
                        txtMan_WArea.Text = waManRes.WorkingArea;
                    }
                    break;

                case "btnPC_F1":
                    PartnerCodeListParm cnrtPCParm = new PartnerCodeListParm();
                    cnrtPCParm.Option = "CD";
                    cnrtPCParm.SearchItem = txtPC_Cnrt.Text;
                    PartnerCodeListResult cnrtPCRes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_CONTRACTOR), cnrtPCParm);
                    if (cnrtPCRes != null)
                    {
                        txtPC_Cnrt.Text = cnrtPCRes.Code;
                    }
                    break;
                case "btnPC_F2":
                    RequesterListParm reqrPCParm = new RequesterListParm();
                    reqrPCParm.Option = "CD";
                    reqrPCParm.SearchItem = txtPC_Requester.Text;
                    RequesterListResult reqrPCRes = (RequesterListResult)PopupManager.instance.ShowPopup(new HCM118(), reqrPCParm);
                    if (reqrPCRes != null)
                    {
                        txtPC_Requester.Text = reqrPCRes.Code;
                    }
                    break;
                case "btnST_F1":
                    //                    MegaStvTrmParm stvSTParm = new MegaStvTrmParm();
                    //                    stvSTParm.VslCallId = m_vslCallId;
                    //                    stvSTParm.ShftId = UserInfo.getInstance().Shift;
                    //                    stvSTParm.WorkYmd = UserInfo.getInstance().Workdate;
                    //                    MegaStvTrmResult stvSTRes = (MegaStvTrmResult)PopupManager.instance.ShowPopup(new HCM109(HCM109.TYPE_STEVEDORE), stvSTParm);
                    //                    if (stvSTRes != null)
                    //                    {
                    //                        txtST_Stvd.Text = stvSTRes.Code;
                    //                    }
                    PartnerCodeListParm stevedoreParm = new PartnerCodeListParm();
                    stevedoreParm.SearchItem = txtST_Stvd.Text;
                    PartnerCodeListResult stevedoreResult = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_STEVEDORE), stevedoreParm);
                    if (stevedoreResult != null)
                    {
                        txtST_Stvd.Text = stevedoreResult.Code;
                    }
                    break;

                case "btnST_F2":
                    WorkingAreaParm waSTParm = new WorkingAreaParm();
                    waSTParm.WorkingArea = txtST_WArea.Text;
                    WorkingAreaResult waSTRes = (WorkingAreaResult)PopupManager.instance.ShowPopup(new HCM113(HCM113.TYPE_STEVEDORE), waSTParm);
                    if (waSTRes != null)
                    {
                        txtST_WArea.Text = waSTRes.WorkingArea;
                    }
                    break;
                case "btnST_F3":
                    RequesterListParm reqrSTParm = new RequesterListParm();
                    reqrSTParm.Option = "CD";
                    reqrSTParm.SearchItem = this.txtST_Requester.Text;
                    RequesterListResult reqrSTRst = (RequesterListResult)PopupManager.instance.ShowPopup(new HCM118(), reqrSTParm);
                    if (reqrSTRst != null)
                    {
                        txtST_Requester.Text = reqrSTRst.Code;
                    }
                    break;
                case "btnFL_F1":
                    WorkingAreaParm waFLParm = new WorkingAreaParm();
                    waFLParm.WorkingArea = txtFL_WArea.Text;
                    WorkingAreaResult waFLResult = (WorkingAreaResult)PopupManager.instance.ShowPopup(new HCM113(HCM113.TYPE_FORKLIFT), waFLParm);
                    if (waFLResult != null)
                    {
                        m_waFLResult = waFLResult;
                        txtFL_WArea.Text = m_waFLResult.WorkingArea;

                        if (string.Empty.Equals(m_waFLResult.WorkingAreaType) ||
                            AREA_HATCH.Equals(m_waFLResult.WorkingAreaType))
                        {
                            cboFL_DMode.isMandatory = false;
                        }
                        else
                        {
                            cboFL_DMode.isMandatory = true;
                        }
                    }
                    break;

                case "btnFL_F2":
                    PartnerCodeListParm cnrtFLParm = new PartnerCodeListParm();
                    cnrtFLParm.Option = "CD";
                    cnrtFLParm.SearchItem = txtFL_Cnrt.Text;
                    PartnerCodeListResult cnrtFLRes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_CONTRACTOR), cnrtFLParm);
                    if (cnrtFLRes != null)
                    {
                        txtFL_Cnrt.Text = cnrtFLRes.Code;
                    }
                    break;
                case "btnFL_F3":
                    RequesterListParm reqrFLParm = new RequesterListParm();
                    reqrFLParm.Option = "CD";
                    reqrFLParm.SearchItem = this.txtFL_Requester.Text;
                    RequesterListResult reqrFLRes = (RequesterListResult)PopupManager.instance.ShowPopup(new HCM118(), reqrFLParm);
                    if (reqrFLRes != null)
                    {
                        txtFL_Requester.Text = reqrFLRes.Code;
                    }
                    break;
                case "btnTR_F1":
                    MOST.Common.CommonParm.EquipmentCodeParm eqTRParm = new MOST.Common.CommonParm.EquipmentCodeParm();
                    // Trailer(TR)
                    eqTRParm.EqIncludedList = "TR";
                    EquipmentCodeResult eqTRResult = (EquipmentCodeResult)PopupManager.instance.ShowPopup(new MOST.Common.HCM112(), eqTRParm);
                    if (eqTRResult != null)
                    {
                        m_trCodeResult = eqTRResult;
                        txtTR_EQTp.Text = eqTRResult.EquCd;
                        if ("Trailer 40'".Equals(eqTRResult.CapaDescr))
                        {
                            cboTR_HatchDir.isMandatory = true;
                        }
                        else
                        {
                            cboTR_HatchDir.isMandatory = false;
                        }
                    }
                    break;

                case "btnTR_F2":
                    PartnerCodeListParm cnrtTRParm = new PartnerCodeListParm();
                    cnrtTRParm.Option = "CD";
                    cnrtTRParm.SearchItem = txtTR_Cntr.Text;
                    PartnerCodeListResult cnrtTRRes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_CONTRACTOR), cnrtTRParm);
                    if (cnrtTRRes != null)
                    {
                        txtTR_Cntr.Text = cnrtTRRes.Code;
                    }
                    break;

                case "btnTR_F3":
                    RequesterListParm reqrTRParm = new RequesterListParm();
                    reqrTRParm.Option = "CD";
                    reqrTRParm.SearchItem = this.txtTR_Requester.Text;
                    RequesterListResult reqrTRRes = (RequesterListResult)PopupManager.instance.ShowPopup(new HCM118(), reqrTRParm);
                    if (reqrTRRes != null)
                    {
                        txtTR_Requester.Text = reqrTRRes.Code;
                    }
                    break;

                case "btnEQ_F1":
                    MOST.Common.CommonParm.EquipmentCodeParm equParm = new MOST.Common.CommonParm.EquipmentCodeParm();
                    // Excavator(EV), Gear(GR), Shore Crane(SC), Shovel(SH), Skylift(SL)
                    equParm.EqIncludedList = "EV,GR,SC,SH,SL";
                    EquipmentCodeResult equResult = (EquipmentCodeResult)PopupManager.instance.ShowPopup(new MOST.Common.HCM112(), equParm);
                    if (equResult != null)
                    {
                        m_eqCodeResult = equResult;
                        txtEQ_EQType.Text = equResult.EquCd;
                    }
                    break;

                case "btnEQ_F2":
                    PartnerCodeListParm cnrtEQParm = new PartnerCodeListParm();
                    cnrtEQParm.Option = "CD";
                    cnrtEQParm.SearchItem = txtEQ_Cnrt.Text;
                    PartnerCodeListResult cnrtEQRes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_CONTRACTOR), cnrtEQParm);
                    if (cnrtEQRes != null)
                    {
                        txtEQ_Cnrt.Text = cnrtEQRes.Code;
                    }
                    break;

                case "btnEQ_F3":
                    RequesterListParm reqrEQParm = new RequesterListParm();
                    reqrEQParm.Option = "CD";
                    reqrEQParm.SearchItem = this.txtEQ_Requester.Text;
                    RequesterListResult reqrEQRes = (RequesterListResult)PopupManager.instance.ShowPopup(new HCM118(), reqrEQParm);
                    if (reqrEQRes != null)
                    {
                        txtEQ_Requester.Text = reqrEQRes.Code;
                    }
                    break;
                case "btnFL_RefNo":
                    this.pnlFL_RefNo.Visible = !pnlFL_RefNo.Visible;

                    if (pnlFL_RefNo.Visible)
                    {
                        this.pnlFL_RefNo.BringToFront();
                        this.btnFL_RefNo.Text = "OK";
                        this.btnAddUpd.Enabled = this.btnExitCancel.Enabled = false;
                    }
                    else
                    {
                        this.pnlFL_RefNo.Visible = false;
                        btnFL_RefNo.Text = "Rf.No";
                        this.btnAddUpd.Enabled = this.btnExitCancel.Enabled = true;
                    }
                    break;
                case "btnTR_RefNo":
                    this.pnlTR_RefNo.Visible = !pnlTR_RefNo.Visible;

                    if (pnlTR_RefNo.Visible)
                    {
                        this.pnlTR_RefNo.BringToFront();
                        this.btnTR_RefNo.Text = "OK";
                        this.btnAddUpd.Enabled = this.btnExitCancel.Enabled = false;
                    }
                    else
                    {
                        this.pnlTR_RefNo.Visible = false;
                        btnTR_RefNo.Text = "Rf.No";
                        this.btnAddUpd.Enabled = this.btnExitCancel.Enabled = true;
                    }
                    break;
                case "btnEQ_RefNo":
                    this.pnlME_RefNo.Visible = !pnlME_RefNo.Visible;

                    if (pnlME_RefNo.Visible)
                    {
                        this.pnlME_RefNo.BringToFront();
                        this.btnEQ_RefNo.Text = "OK";
                        this.btnAddUpd.Enabled = this.btnExitCancel.Enabled = false;
                    }
                    else
                    {
                        this.pnlME_RefNo.Visible = false;
                        btnEQ_RefNo.Text = "Rf.No";
                        this.btnAddUpd.Enabled = this.btnExitCancel.Enabled = true;
                    }
                    break;
                case "btnFL_Remark":
                case "btnPC_Remark":
                case "btnEQ_Remark":
                case "btnTR_Remark":
                    UserControl mypanel = null;
                    switch (buttonName)
                    {
                        case "btnFL_Remark":
                            mypanel = pnlFL_Remark;
                            break;
                        case "btnPC_Remark":
                            mypanel = pnlPC_Remark;
                            break;
                        case "btnEQ_Remark":
                            mypanel = pnlEQ_Remark;
                            break;
                        case "btnTR_Remark":
                            mypanel = pnlTR_Remark;
                            break;
                    }
                    mypanel.Visible = !mypanel.Visible;

                    if (mypanel.Visible)
                    {
                        mypanel.BringToFront();
                        mybutton.Text = "OK";
                        this.btnAddUpd.Enabled = this.btnExitCancel.Enabled = false;
                    }
                    else
                    {
                        mypanel.Visible = false;
                        mybutton.Text = "Remark";
                        this.btnAddUpd.Enabled = this.btnExitCancel.Enabled = true;
                    }
                    break;
            }
        }

        private void ReturnItems(int tab)
        {
            CheckListVSRItem item = null;
            

            if (m_mode == Constants.MODE_ADD)
            {
                item = new CheckListVSRItem();
                item.CRUD = Constants.WS_INSERT;
            }
            else if (m_mode == Constants.MODE_UPDATE)
            {
                item = m_updItem;
                item.cnrtcd_temp = this._Operator;
                item.mbscd_temp = this._Mbscd;

                // If Working Status is not INSERT (exist item), change it to UPDATE
                // Else (new item), remain status.
                if (!Constants.WS_INSERT.Equals(item.CRUD))
                {
                    item.CRUD = Constants.WS_UPDATE;
                }
            }

            if (item != null)
            {
                switch (tab)
                {
                    case HCM119.TAB_MANPOWER:
                        // Ref: CT106001
                        item.vslCallID = m_vslCallId;
                        item.workYmd = UserInfo.getInstance().Workdate;
                        item.shftId = UserInfo.getInstance().Shift;
                        item.userId = UserInfo.getInstance().UserId;
                        item.mbsCd = CONST_JPB;
                        item.empId = txtMan_JPB.Text;
                        item.empNm = txtMan_JPBName.Text;
                        item.rsNm = CommonUtility.GetComboboxSelectedValue(cboMan_Role);
                        item.divCd = "SD";
                        item.workLoc = txtMan_WArea.Text;
                        item.workStDt = txtMan_StartTime.Text;
                        item.workEndDt = txtMan_EndTime.Text;
                        item.rmk = pnlMan_Remark.txt_Remark.Text;

                        if (m_mode == Constants.MODE_ADD)
                        {
                            m_result.ArrGrdDataMP.Add(item);
                        }
                        break;

                    case HCM119.TAB_PORTCRANE:
                        // Ref: CT106007
                        string strPCCapaCd = string.Empty;
                        string strPCRsNm = string.Empty;
                        string strPCEngNm = string.Empty;

                        item.vslCallID = m_vslCallId;
                        item.workYmd = UserInfo.getInstance().Workdate;
                        item.shftId = UserInfo.getInstance().Shift;
                        item.userId = UserInfo.getInstance().UserId;
                        item.capaDescr = txtPC_Capacity.Text;
                        item.payer = txtPC_Requester.Text;
                        item.workStDt = txtPC_StartTime.Text;
                        item.workEndDt = txtPC_EndTime.Text;
                        if (rbtnPC_JPB.Checked)
                        {
                            item.mbsCd = CONST_JPB;
                            item.empId = CommonUtility.GetComboboxSelectedValue(cboPC_JPB);
                        }
                        else if (rbtnPC_Cnrt.Checked)
                        {
                            item.mbsCd = CONST_CTR;
                            item.cnrtCd = txtPC_Cnrt.Text;
                        }
                        //item.cnrtCd = txtPC_Cnrt.Text;
                        item.divCd = "PC";
                        item.cgTpCd = CommonUtility.GetComboboxSelectedValue(cboPC_CgTp);
                        item.cgTpNm = CommonUtility.GetComboboxSelectedDescription(cboPC_CgTp);
                        item.purpose = CommonUtility.GetComboboxSelectedValue(cboPC_Purpose);
                        item.purposeNm = CommonUtility.GetComboboxSelectedDescription(cboPC_Purpose);

                        if (rbtnPC_ShipCrane.Checked)
                        {
                            //strPCRsNm = CONST_SHIPCR;
                            //strPCEngNm = CONST_SHIPCR_NM;
                            strPCRsNm = CommonUtility.GetComboboxSelectedValue(cboPC_EQNo);
                            strPCEngNm = CommonUtility.GetComboboxSelectedDescription(cboPC_EQNo);
                        }
                        else if (rbtnPC_PortCrane.Checked)
                        {
                            strPCRsNm = CommonUtility.GetComboboxSelectedValue(cboPC_EQNo);
                            strPCEngNm = CommonUtility.GetComboboxSelectedDescription(cboPC_EQNo);
                            if (!string.IsNullOrEmpty(strPCEngNm) && strPCEngNm.IndexOf("/") > -1)
                            {
                                strPCEngNm = strPCEngNm.Substring(0, strPCEngNm.IndexOf("/"));
                            }

                            if (cboPC_EQNo != null && cboPC_EQNo.SelectedIndex > 0 &&
                                m_listCapaPC != null && cboPC_EQNo.SelectedIndex <= m_listCapaPC.Count)
                            {
                                strPCCapaCd = m_listCapaPC[cboPC_EQNo.SelectedIndex - 1].ToString();
                            }
                        }
                        item.rsNm = strPCRsNm;
                        item.engNm = strPCEngNm;
                        item.capaCd = strPCCapaCd;
                        item.rmk = pnlPC_Remark.txt_Remark.Text;

                        if (m_mode == Constants.MODE_ADD)
                        {
                            m_result.ArrGrdDataPC.Add(item);
                        }
                        break;

                    case HCM119.TAB_STEVEDORE:
                        // Ref: CT211004
                        item.vslCallID = m_vslCallId;
                        item.workYmd = UserInfo.getInstance().Workdate;
                        item.shftId = UserInfo.getInstance().Shift;
                        item.userId = UserInfo.getInstance().UserId;
                        item.workStDt = txtST_StartTime.Text;
                        item.workEndDt = txtST_EndTime.Text;
                        item.stvdComp = txtST_Stvd.Text;
                        item.divCd = "ST";
                        item.payer = txtST_Requester.Text;
                        item.workLoc = txtST_WArea.Text;
                        item.stvdNonTon = txtST_NonTon.Text;
                        item.nofStvdSprr = txtST_Sprr.Text;
                        item.purpose = CommonUtility.GetComboboxSelectedValue(cboST_Purpose);
                        item.purposeNm = CommonUtility.GetComboboxSelectedDescription(cboST_Purpose);

                        if (m_mode == Constants.MODE_ADD)
                        {
                            m_result.ArrGrdDataST.Add(item);
                        }
                        break;

                    case HCM119.TAB_FORKLIFT:
                        // Ref: CT106002
                        string strFLCapaCd = string.Empty;
                        if (cboFL_FLNo != null && cboFL_FLNo.SelectedIndex > 0 &&
                            m_listCapaFL != null && cboFL_FLNo.SelectedIndex <= m_listCapaFL.Count)
                        {
                            strFLCapaCd = m_listCapaFL[cboFL_FLNo.SelectedIndex - 1].ToString();
                        }
                        item.vslCallID = m_vslCallId;
                        item.workYmd = UserInfo.getInstance().Workdate;
                        item.shftId = UserInfo.getInstance().Shift;
                        item.userId = UserInfo.getInstance().UserId;
                        if (m_waFLResult != null)
                        {
                            item.workLoc = m_waFLResult.WorkingArea;
                            item.workLocTp = m_waFLResult.WorkingAreaType;
                        }
                        else
                            item.workLocTp = CommonUtility.GetWorkAreaType(item.workLoc);
                        if (item.workLoc.Length > 6 && AREA_WHARF.Equals(item.workLocTp) && "Wharf(".Equals(item.workLoc.Substring(0, 6)))
                        {
                            item.workLoc = item.workLoc.Substring(6, item.workLoc.Length - 7);
                        }
                        item.hatchDir = (AREA_HATCH.Equals(item.workLocTp) || AREA_WHARF.Equals(item.workLocTp)) ? CommonUtility.GetComboboxSelectedValue(cboFL_HatchDir) : string.Empty;
                        item.capaCd = strFLCapaCd;
                        item.capaDescr = txtFL_Capacity.Text;
                        item.workStDt = txtFL_StartTime.Text;
                        item.workEndDt = txtFL_EndTime.Text;
                        item.rsNm = CommonUtility.GetComboboxSelectedValue(cboFL_FLNo);
                        item.engNm = CommonUtility.GetComboboxSelectedDescription(cboFL_FLNo);
                        if (rbtnFL_JPB.Checked)
                        {
                            item.mbsCd = CONST_JPB;
                            item.empId = CommonUtility.GetComboboxSelectedValue(cboFL_JPB);
                        }
                        else if (rbtnFL_Cnrt.Checked)
                        {
                            item.mbsCd = CONST_CTR;
                            item.cnrtCd = txtFL_Cnrt.Text;
                        }
                        else if (rbtnFL_Driver.Checked)
                        {
                            item.mbsCd = "";
                            item.cnrtCd = "";
                        }
                        item.divCd = "FL";
                        item.cgTpCd = CommonUtility.GetComboboxSelectedValue(cboFL_CgTp);
                        item.cgTpNm = CommonUtility.GetComboboxSelectedDescription(cboFL_CgTp);
                        item.payer = txtFL_Requester.Text;
                        item.delvTpCd = CommonUtility.GetComboboxSelectedValue(cboFL_DMode);
                        item.delvTpNm = CommonUtility.GetComboboxSelectedDescription(cboFL_DMode);
                        item.purpose = CommonUtility.GetComboboxSelectedValue(cboFL_Purpose);
                        item.purposeNm = CommonUtility.GetComboboxSelectedDescription(cboFL_Purpose);
                        item.setupTime = txtFL_EqArrDt.Text;

                        item.refNo = pnlFL_RefNo.rbtn_RefCbo.Checked ? CommonUtility.GetComboboxSelectedValue(pnlFL_RefNo.cbo_RefNo) : pnlFL_RefNo.txt_RefNo.Text;

                        item.refYn = pnlFL_RefNo.rbtn_RefCbo.Checked ? "Y" : "N";
                        item.rmk = pnlFL_Remark.txt_Remark.Text;

                        if (m_mode == Constants.MODE_ADD)
                        {
                            m_result.ArrGrdDataFL.Add(item);
                        }
                        break;

                    case HCM119.TAB_TRAILER:
                        // Ref: CT106009
                        item.vslCallID = m_vslCallId;
                        item.workYmd = UserInfo.getInstance().Workdate;
                        item.shftId = UserInfo.getInstance().Shift;
                        item.userId = UserInfo.getInstance().UserId;
                        item.workLoc = CommonUtility.GetComboboxSelectedValue(cboTR_WAreaDtl);
                        string strWArea = CommonUtility.GetComboboxSelectedValue(cboTR_WArea);
                        item.workLocTp = strWArea;
                        item.workLocTpNm = CommonUtility.GetComboboxSelectedDescription(cboTR_WArea);
                        item.hatchDir = (AREA_HATCH.Equals(strWArea) || AREA_WHARF.Equals(item.workLocTp)) ? CommonUtility.GetComboboxSelectedValue(cboTR_HatchDir) : string.Empty;
                        item.rsQty = txtTR_Nos.Text;
                        item.workStDt = txtTR_StartTime.Text;
                        item.workEndDt = txtTR_EndTime.Text;
                        item.mbsCd = CONST_CTR;
                        item.cnrtCd = txtTR_Cntr.Text;
                        if (m_trCodeResult != null)
                        {
                            item.rsNm = m_trCodeResult.EquCd;
                            item.capaCd = m_trCodeResult.CapaCd;
                            item.capaDescr = m_trCodeResult.CapaDescr;
                        }
                        item.divCd = "TR";
                        item.cgTpCd = CommonUtility.GetComboboxSelectedValue(cboTR_CgTp);
                        item.cgTpNm = CommonUtility.GetComboboxSelectedDescription(cboTR_CgTp);
                        item.payer = txtTR_Requester.Text;
                        item.delvTpCd = CommonUtility.GetComboboxSelectedValue(cboTR_DMode);
                        item.delvTpNm = CommonUtility.GetComboboxSelectedDescription(cboTR_DMode);
                        item.purpose = CommonUtility.GetComboboxSelectedValue(cboTR_Purpose);
                        item.purposeNm = CommonUtility.GetComboboxSelectedDescription(cboTR_Purpose);
                        item.setupTime = txtTR_EqArrDt.Text;
                        item.refNo = pnlTR_RefNo.rbtn_RefCbo.Checked ? CommonUtility.GetComboboxSelectedValue(pnlTR_RefNo.cbo_RefNo) : pnlTR_RefNo.txt_RefNo.Text;
                        item.refYn = pnlTR_RefNo.rbtn_RefCbo.Checked ? "Y" : "N";
                        item.rmk = pnlTR_Remark.txt_Remark.Text;

                        if (m_mode == Constants.MODE_ADD)
                        {
                            m_result.ArrGrdDataTR.Add(item);
                        }
                        break;

                    case HCM119.TAB_EQU:
                        // Ref: CT106003
                        item.vslCallID = m_vslCallId;
                        item.workYmd = UserInfo.getInstance().Workdate;
                        item.shftId = UserInfo.getInstance().Shift;
                        item.workLoc = CommonUtility.GetComboboxSelectedValue(cboEQ_WAreaDtl);
                        item.workLocTp = CommonUtility.GetComboboxSelectedValue(cboEQ_WArea);
                        item.workOdrNo = txtEQ_WO.Text;
                        item.rsQty = txtEQ_Nos.Text;
                        item.workStDt = txtEQ_StartTime.Text;
                        item.workEndDt = txtEQ_EndTime.Text;
                        item.cnrtCd = txtEQ_Cnrt.Text;
                        item.mbsCd = CONST_CTR;
                        item.shpCrew = rbtnEQ_ShipCrew.Checked ? "Y" : "N";
                        if (m_eqCodeResult != null)
                        {
                            item.rsNm = m_eqCodeResult.EquCd;
                            item.capaCd = m_eqCodeResult.CapaCd;
                            item.capaDescr = m_eqCodeResult.CapaDescr;
                        }
                        item.userId = UserInfo.getInstance().UserId;
                        item.cgTpCd = CommonUtility.GetComboboxSelectedValue(cboEQ_CgTp);
                        item.cgTpNm = CommonUtility.GetComboboxSelectedDescription(cboEQ_CgTp);
                        item.purpose = CommonUtility.GetComboboxSelectedValue(cboEQ_Purpose);
                        item.purposeNm = CommonUtility.GetComboboxSelectedDescription(cboEQ_Purpose);
                        item.payer = txtEQ_Requester.Text;
                        item.setupTime = txtEQ_EqArrDt.Text;
                        item.divCd = "ME";
                        item.refNo = pnlME_RefNo.rbtn_RefCbo.Checked ? CommonUtility.GetComboboxSelectedValue(pnlME_RefNo.cbo_RefNo) : pnlME_RefNo.txt_RefNo.Text;
                        item.refYn = pnlME_RefNo.rbtn_RefCbo.Checked ? "Y" : "N";
                        item.rmk = pnlEQ_Remark.txt_Remark.Text;

                        if (m_mode == Constants.MODE_ADD)
                        {
                            m_result.ArrGrdDataEQ.Add(item);
                        }
                        break;
                }
            }

            if (m_mode == Constants.MODE_UPDATE)
            {
                m_result.UpdVsrItem = item;
            }
        }

        private bool IsExistAlready(int tab)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                int currIndex = m_parm.CurrIndex;
                List<CheckListVSRItem> arrGrdData = null;
                switch (tab)
                {
                    case HCM119.TAB_MANPOWER:
                        // Make sure Role & (StartTime or EndTime) to be unique.
                        if (m_mode == Constants.MODE_ADD)
                        {
                            arrGrdData = m_parm.ArrGrdDataMP;
                        }
                        else if (m_mode == Constants.MODE_UPDATE)
                        {
                            arrGrdData = m_parm.ArrUpdGrdData;
                        }
                        string newEmpId = txtMan_JPB.Text;
                        string newWArea = txtMan_WArea.Text;
                        string newSTTime = txtMan_StartTime.Text;
                        string newEDTime = txtMan_EndTime.Text;
                        if (arrGrdData != null && arrGrdData.Count > 0)
                        {
                            int index = arrGrdData.Count - 1;
                            CheckListVSRItem item = arrGrdData[index];
                            string oldEmpId = item.empId;
                            string oldWArea = item.workLoc;
                            string oldStTime = item.workStDt;
                            string oldEdTime = item.workEndDt;
                            /*while (!newEmpId.Equals(oldEmpId) || !newWArea.Equals(oldWArea) ||
                                (!newSTTime.Equals(oldStTime) && !newEDTime.Equals(oldEdTime)))*/
                            while (!newEmpId.Equals(oldEmpId))
                            {
                                index = index - 1;
                                if (index < 0)
                                {
                                    return false;
                                }

                                item = arrGrdData[index];
                                if (m_mode == Constants.MODE_ADD)
                                {
                                    oldEmpId = item.empId;
                                    oldWArea = item.workLoc;
                                    oldStTime = item.workStDt;
                                    oldEdTime = item.workEndDt;
                                }
                                else if (m_mode == Constants.MODE_UPDATE)
                                {
                                    if (index != currIndex)
                                    {
                                        oldEmpId = item.empId;
                                        oldWArea = item.workLoc;
                                        oldStTime = item.workStDt;
                                        oldEdTime = item.workEndDt;
                                    }
                                }
                            }
                            if (m_mode == Constants.MODE_ADD)
                            {
                                if (index >= 0)
                                {
                                    return true;
                                }
                            }
                            else if (m_mode == Constants.MODE_UPDATE)
                            {
                                if (index >= 0 && index != currIndex)
                                {
                                    return true;
                                }
                            }
                        }
                        break;

                    case HCM119.TAB_PORTCRANE:
                        // In case of Contractor, no validation
                        if (rbtnPC_Cnrt.Checked)
                        {
                            return false;
                        }

                        // Make sure PortCrane No & Staff ID && (StartTime or EndTime) to be unique.
                        bool isJPB = false;
                        bool isCntr = false;
                        string newPCEqNo = string.Empty;
                        string newPCStaffId = string.Empty;
                        string newPCStTime = txtPC_StartTime.Text;
                        string newPCEdTime = txtPC_EndTime.Text;
                        if (rbtnPC_JPB.Checked)
                        {
                            isJPB = true;
                        }
                        else if (rbtnPC_Cnrt.Checked)
                        {
                            isCntr = true;
                        }

                        if (m_mode == Constants.MODE_ADD)
                        {
                            arrGrdData = m_parm.ArrGrdDataPC;
                        }
                        else if (m_mode == Constants.MODE_UPDATE)
                        {
                            arrGrdData = m_parm.ArrUpdGrdData;
                        }
                        if (rbtnPC_PortCrane.Checked)
                        {
                            newPCEqNo = CommonUtility.GetComboboxSelectedValue(cboPC_EQNo);
                        }
                        else if (rbtnPC_ShipCrane.Checked)
                        {
                            newPCEqNo = HCM120.CONST_SHIPCR;
                        }
                        if (isJPB)
                        {
                            newPCStaffId = CommonUtility.GetComboboxSelectedValue(cboPC_JPB);
                        }
                        else if (isCntr)
                        {
                            newPCStaffId = txtPC_Cnrt.Text;
                        }


                        if (arrGrdData != null && arrGrdData.Count > 0)
                        {
                            int index = arrGrdData.Count - 1;
                            CheckListVSRItem item = arrGrdData[index];
                            string oldPCEqNo = item.rsNm;
                            string oldPCStDate = item.workStDt;
                            string oldPCEdDate = item.workEndDt;
                            string oldPCStaffId = string.Empty;
                            if (isJPB)
                            {
                                oldPCStaffId = item.empId;
                            }
                            else if (isCntr)
                            {
                                oldPCStaffId = item.cnrtCd;
                            }
                            while (
                                // Different Eq, different Staffs
                                ((!newPCEqNo.Equals(oldPCEqNo) && !newPCStaffId.Equals(oldPCStaffId)))
                                // Same Eq, different Staffs        //, same start/end time
                                || (newPCEqNo.Equals(oldPCEqNo) && !newPCStaffId.Equals(oldPCStaffId))      //&& (newPCStTime.Equals(oldPCStDate) || newPCEdTime.Equals(oldPCEdDate)) )
                                // Different Eq, same Staff, different start/end time
                                || (!newPCEqNo.Equals(oldPCEqNo) && newPCStaffId.Equals(oldPCStaffId) && (!newPCStTime.Equals(oldPCStDate) || !newPCEdTime.Equals(oldPCEdDate)))
                                )
                            {
                                index = index - 1;
                                if (index < 0)
                                {
                                    return false;
                                }

                                item = arrGrdData[index];
                                if (m_mode == Constants.MODE_ADD)
                                {
                                    oldPCEqNo = item.rsNm;
                                    oldPCStDate = item.workStDt;
                                    oldPCEdDate = item.workEndDt;
                                    if (isJPB)
                                    {
                                        oldPCStaffId = item.empId;
                                    }
                                    else if (isCntr)
                                    {
                                        oldPCStaffId = item.cnrtCd;
                                    }
                                }
                                else if (m_mode == Constants.MODE_UPDATE)
                                {
                                    if (index != currIndex)
                                    {
                                        oldPCEqNo = item.rsNm;
                                        oldPCStDate = item.workStDt;
                                        oldPCEdDate = item.workEndDt;
                                        if (isJPB)
                                        {
                                            oldPCStaffId = item.empId;
                                        }
                                        else if (isCntr)
                                        {
                                            oldPCStaffId = item.cnrtCd;
                                        }
                                    }
                                }
                            }
                            if (m_mode == Constants.MODE_ADD)
                            {
                                if (index >= 0)
                                {
                                    return true;
                                }
                            }
                            else if (m_mode == Constants.MODE_UPDATE)
                            {
                                if (index >= 0 && index != currIndex)
                                {
                                    return true;
                                }
                            }
                        }
                        break;

                    case HCM119.TAB_STEVEDORE:
                        // Make sure Stevedore & (StartTime or EndTime) to be unique.
                        if (m_mode == Constants.MODE_ADD)
                        {
                            arrGrdData = m_parm.ArrGrdDataST;
                        }
                        else if (m_mode == Constants.MODE_UPDATE)
                        {
                            arrGrdData = m_parm.ArrUpdGrdData;
                        }
                        string newSTStvd = txtST_Stvd.Text;
                        string newSTStTime = txtST_StartTime.Text;
                        string newSTEdTime = txtST_EndTime.Text;
                        if (arrGrdData != null && arrGrdData.Count > 0)
                        {
                            int index = arrGrdData.Count - 1;
                            CheckListVSRItem item = arrGrdData[index];
                            string oldSTStvd = item.stvdComp;
                            string oldSTStDate = item.workStDt;
                            string oldSTEdDate = item.workEndDt;
                            while (!newSTStvd.Equals(oldSTStvd) ||
                                (!newSTStTime.Equals(oldSTStDate) && !newSTEdTime.Equals(oldSTEdDate)))
                            {
                                index = index - 1;
                                if (index < 0)
                                {
                                    return false;
                                }

                                item = arrGrdData[index];
                                if (m_mode == Constants.MODE_ADD)
                                {
                                    oldSTStvd = item.stvdComp;
                                    oldSTStDate = item.workStDt;
                                    oldSTEdDate = item.workEndDt;
                                }
                                else if (m_mode == Constants.MODE_UPDATE)
                                {
                                    if (index != currIndex)
                                    {
                                        oldSTStvd = item.stvdComp;
                                        oldSTStDate = item.workStDt;
                                        oldSTEdDate = item.workEndDt;
                                    }
                                }
                            }
                            if (m_mode == Constants.MODE_ADD)
                            {
                                if (index >= 0)
                                {
                                    return true;
                                }
                            }
                            else if (m_mode == Constants.MODE_UPDATE)
                            {
                                if (index >= 0 && index != currIndex)
                                {
                                    return true;
                                }
                            }
                        }
                        break;

                    case HCM119.TAB_FORKLIFT:
                        // In case of Contractor, no validation
                        if (rbtnFL_Cnrt.Checked || rbtnFL_Driver.Checked)
                        {
                            return false;
                        }

                        // Make sure Forklift No & staff & (StartTime or EndTime) to be unique.
                        if (m_mode == Constants.MODE_ADD)
                        {
                            arrGrdData = m_parm.ArrGrdDataFL;
                        }
                        else if (m_mode == Constants.MODE_UPDATE)
                        {
                            arrGrdData = m_parm.ArrUpdGrdData;
                        }
                        string newFLNo = CommonUtility.GetComboboxSelectedValue(cboFL_FLNo);
                        string newFLStTime = txtFL_StartTime.Text;
                        string newFLEdTime = txtFL_EndTime.Text;
                        string newFLStaff = rbtnFL_JPB.Checked ? CommonUtility.GetComboboxSelectedValue(cboFL_JPB) : txtFL_Cnrt.Text;
                        if (arrGrdData != null && arrGrdData.Count > 0)
                        {
                            int index = arrGrdData.Count - 1;
                            CheckListVSRItem item = arrGrdData[index];
                            string oldFlNo = item.rsNm;
                            string oldFLStTime = item.workStDt;
                            string oldFLEdTime = item.workEndDt;
                            string oldFlStaff = rbtnFL_JPB.Checked ? item.empId : item.cnrtCd;
                            while (
                                // Different Eq, different Staffs
                                ((!newFLNo.Equals(oldFlNo) && !newFLStaff.Equals(oldFlStaff)))
                                // Same Eq, different Staffs        //, same start/end time
                                || (newFLNo.Equals(oldFlNo) && !newFLStaff.Equals(oldFlStaff))      //&& (newFLStTime.Equals(oldFLStTime) || newFLEdTime.Equals(oldFLEdTime)) )
                                // Different Eq, same Staff, different start/end time
                                || (!newFLNo.Equals(oldFlNo) && newFLStaff.Equals(oldFlStaff) && (!newFLStTime.Equals(oldFLStTime) || !newFLEdTime.Equals(oldFLEdTime)))
                                )
                            {
                                index = index - 1;
                                if (index < 0)
                                {
                                    return false;
                                }

                                item = arrGrdData[index];
                                if (m_mode == Constants.MODE_ADD)
                                {
                                    oldFlNo = item.rsNm;
                                    oldFLStTime = item.workStDt;
                                    oldFLEdTime = item.workEndDt;
                                    oldFlStaff = rbtnFL_JPB.Checked ? item.empId : item.cnrtCd;
                                }
                                else if (m_mode == Constants.MODE_UPDATE)
                                {
                                    if (index != currIndex)
                                    {
                                        oldFlNo = item.rsNm;
                                        oldFLStTime = item.workStDt;
                                        oldFLEdTime = item.workEndDt;
                                        oldFlStaff = rbtnFL_JPB.Checked ? item.empId : item.cnrtCd;
                                    }
                                }
                            }
                            if (m_mode == Constants.MODE_ADD)
                            {
                                if (index >= 0)
                                {
                                    return true;
                                }
                            }
                            else if (m_mode == Constants.MODE_UPDATE)
                            {
                                if (index >= 0 && index != currIndex)
                                {
                                    return true;
                                }
                            }
                        }
                        break;

                    case HCM119.TAB_TRAILER:
                        // Enable to input records of trailer that working in 3 different hatch ==> No validation
                        //// Make sure Trailer EQ Type & Capacity & (StartTime or EndTime) to be unique.
                        //if (m_mode == Constants.MODE_ADD)
                        //{
                        //    arrGrdData = m_parm.ArrGrdDataTR;
                        //}
                        //else if (m_mode == Constants.MODE_UPDATE)
                        //{
                        //    arrGrdData = m_parm.ArrUpdGrdData;
                        //}
                        //string newTRNo = string.Empty;
                        //string newTRCapa = string.Empty;
                        //string newTRStTime = txtTR_StartTime.Text;
                        //string newTREdTime = txtTR_EndTime.Text;
                        //if (m_trCodeResult != null)
                        //{
                        //    newTRNo = m_trCodeResult.EquCd;
                        //    newTRCapa = m_trCodeResult.CapaCd;
                        //}

                        //if (arrGrdData != null && arrGrdData.Count > 0)
                        //{
                        //    int index = arrGrdData.Count - 1;
                        //    CheckListVSRItem item = arrGrdData[index];
                        //    string oldTRNo = item.rsNm;
                        //    string oldCapaCd = item.capaCd;
                        //    string oldStTime = item.workStDt;
                        //    string oldEdTime = item.workEndDt;
                        //    while (!newTRNo.Equals(oldTRNo) || !newTRCapa.Equals(oldCapaCd) ||
                        //        (!newTRStTime.Equals(oldStTime) && !newTREdTime.Equals(oldEdTime)))
                        //    {
                        //        index = index - 1;
                        //        if (index < 0)
                        //        {
                        //            return false;
                        //        }

                        //        item = arrGrdData[index];
                        //        if (m_mode == Constants.MODE_ADD)
                        //        {
                        //            oldTRNo = item.rsNm;
                        //            oldCapaCd = item.capaCd;
                        //            oldStTime = item.workStDt;
                        //            oldEdTime = item.workEndDt;
                        //        }
                        //        else if (m_mode == Constants.MODE_UPDATE)
                        //        {
                        //            if (index != currIndex)
                        //            {
                        //                oldTRNo = item.rsNm;
                        //                oldCapaCd = item.capaCd;
                        //                oldStTime = item.workStDt;
                        //                oldEdTime = item.workEndDt;
                        //            }
                        //        }
                        //    }
                        //    if (m_mode == Constants.MODE_ADD)
                        //    {
                        //        if (index >= 0)
                        //        {
                        //            return true;
                        //        }
                        //    }
                        //    else if (m_mode == Constants.MODE_UPDATE)
                        //    {
                        //        if (index >= 0 && index !=  currIndex)
                        //        {
                        //            return true;
                        //        }
                        //    }
                        //}
                        break;

                    case HCM119.TAB_EQU:
                        // Make sure Trailer No & Capacity & StartTime to be unique.
                        //if (m_mode == Constants.MODE_ADD)
                        //{
                        //    arrGrdData = m_parm.ArrGrdDataEQ;
                        //}
                        //else if (m_mode == Constants.MODE_UPDATE)
                        //{
                        //    arrGrdData = m_parm.ArrUpdGrdData;
                        //}
                        //string newEQNo = string.Empty;
                        //string newEQCapa = string.Empty;
                        //string newEQStTime = txtEQ_StartTime.Text;
                        //string newEQEdTime = txtEQ_EndTime.Text;
                        //if (m_eqCodeResult != null)
                        //{
                        //    newEQNo = m_eqCodeResult.EquCd;
                        //    newEQCapa = m_eqCodeResult.CapaCd;
                        //}

                        //if (arrGrdData != null && arrGrdData.Count > 0)
                        //{
                        //    int index = arrGrdData.Count - 1;
                        //    CheckListVSRItem item = arrGrdData[index];
                        //    string oldEqNo = item.rsNm;
                        //    string oldCapaCd = item.capaCd;
                        //    string oldStTime = item.workStDt;
                        //    string oldEdTime = item.workEndDt;
                        //    while (!newEQNo.Equals(oldEqNo) || !newEQCapa.Equals(oldCapaCd) ||
                        //        (!newEQStTime.Equals(oldStTime) && !newEQEdTime.Equals(oldEdTime)))
                        //    {
                        //        index = index - 1;
                        //        if (index < 0)
                        //        {
                        //            return false;
                        //        }

                        //        item = arrGrdData[index];
                        //        if (m_mode == Constants.MODE_ADD)
                        //        {
                        //            oldEqNo = item.rsNm;
                        //            oldCapaCd = item.capaCd;
                        //            oldStTime = item.workStDt;
                        //            oldEdTime = item.workEndDt;
                        //        }
                        //        else if (m_mode == Constants.MODE_UPDATE)
                        //        {
                        //            if (index != currIndex)
                        //            {
                        //                oldEqNo = item.rsNm;
                        //                oldCapaCd = item.capaCd;
                        //                oldStTime = item.workStDt;
                        //                oldEdTime = item.workEndDt;
                        //            }
                        //        }
                        //    }
                        //    if (m_mode == Constants.MODE_ADD)
                        //    {
                        //        if (index >= 0)
                        //        {
                        //            return true;
                        //        }
                        //    }
                        //    else if (m_mode == Constants.MODE_UPDATE)
                        //    {
                        //        if (index >= 0 && index != currIndex)
                        //        {
                        //            return true;
                        //        }
                        //    }
                        //}
                        break;
                }
            }
            catch (Framework.Common.Exception.BusinessException ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return false;
        }

        private bool ValidateDateRange(int tab)
        {
            switch (tab)
            {
                case HCM119.TAB_MANPOWER:
                    if (!CommonUtility.ValidateStartEndDtWithinShift(txtMan_StartTime, txtMan_EndTime))
                    {
                        return false;
                    }
                    break;

                case HCM119.TAB_PORTCRANE:
                    if (!CommonUtility.ValidateStartEndDtWithinShift(txtPC_StartTime, txtPC_EndTime))
                    {
                        return false;
                    }
                    break;

                case HCM119.TAB_STEVEDORE:
                    if (!CommonUtility.ValidateStartEndDtWithinShift(txtST_StartTime, txtST_EndTime))
                    {
                        return false;
                    }
                    break;

                case HCM119.TAB_FORKLIFT:
                    if (!CommonUtility.ValidateStartEndDtWithinShift(txtFL_StartTime, txtFL_EndTime))
                    {
                        return false;
                    }
                    break;

                case HCM119.TAB_TRAILER:
                    if (!CommonUtility.ValidateStartEndDtWithinShift(txtTR_StartTime, txtTR_EndTime))
                    {
                        return false;
                    }
                    break;

                case HCM119.TAB_EQU:
                    if (!CommonUtility.ValidateStartEndDtWithinShift(txtEQ_StartTime, txtEQ_EndTime))
                    {
                        return false;
                    }
                    break;
            }

            return true;
        }

        private bool ValidateNumber(int tab)
        {
            string strNos;
            switch (tab)
            {
                case HCM119.TAB_TRAILER:
                    strNos = txtTR_Nos.Text;
                    if (CommonUtility.ParseInt(strNos) <= 0)
                    {
                        CommonUtility.AlertMessage("Please input integer which is greater than 0.");
                        txtTR_Nos.Focus();
                        txtTR_Nos.SelectAll();
                        return false;
                    }
                    break;

                case HCM119.TAB_EQU:
                    strNos = txtEQ_Nos.Text;
                    if (CommonUtility.ParseInt(strNos) <= 0)
                    {
                        CommonUtility.AlertMessage("Please input integer which is greater than 0.");
                        txtEQ_Nos.Focus();
                        txtEQ_Nos.SelectAll();
                        return false;
                    }
                    break;
            }

            return true;
        }

        private bool ValidateCgType(int tab)
        {
            switch (tab)
            {
                case HCM119.TAB_PORTCRANE:
                    if (rbtnPC_PortCrane.Checked && cboPC_CgTp.SelectedIndex < 1)
                    {
                        CommonUtility.AlertMessage("Please select cargo type.");
                        cboPC_CgTp.Focus();
                        return false;
                    }
                    break;
            }

            return true;
        }

        private bool ValidateContractor(int tab)
        {
            string validateCode = "";
            switch (tab)
            {
                case HCM119.TAB_PORTCRANE:
                    if (!rbtnPC_Cnrt.Checked)
                        return true;
                    validateCode = txtPC_Cnrt.Text;
                    break;
                case HCM119.TAB_FORKLIFT:
                    if (!rbtnFL_Cnrt.Checked)
                        return true;
                    validateCode = txtFL_Cnrt.Text;
                    break;
                case HCM119.TAB_TRAILER:
                    validateCode = txtTR_Cntr.Text;
                    break;
                case HCM119.TAB_EQU:
                    if (!rbtnEQ_Cnrt.Checked)
                        return true;
                    validateCode = txtEQ_Cnrt.Text;
                    break;
                default:
                    return true;
            }

            CommonProxy proxy = new CommonProxy();
            CommonCodeParm parm = new CommonCodeParm();
            parm.tyCd = "checkCTT";
            parm.col1 = validateCode;
            ResponseInfo info = proxy.getValidationCode(parm);

            if (info.list.Length > 0)
            {
                CommonCodeItem item = (CommonCodeItem) info.list[0];
                if ("N".Equals(item.isValidated))
                {
                    CommonUtility.AlertMessage("Please input right contractor");
                    return false;
                }

            }
            
            return true;
        }

        private bool ValidateRequester(int tab)
        {
            string validateCode = "";
            switch (tab)
            {
                case HCM119.TAB_PORTCRANE:
                    validateCode = txtPC_Requester.Text;
                    break;
                case HCM119.TAB_FORKLIFT:
                    validateCode = txtFL_Requester.Text;
                    break;
                case HCM119.TAB_TRAILER:
                    validateCode = txtTR_Requester.Text;
                    break;
                case HCM119.TAB_EQU:
                    validateCode = txtEQ_Requester.Text;
                    break;
                default:
                    return true;
            }

            CommonProxy proxy = new CommonProxy();
            CommonCodeParm parm = new CommonCodeParm();
            parm.tyCd = "checkFWD";
            parm.col1 = validateCode;
            ResponseInfo info = proxy.getValidationCode(parm);

            if (info.list.Length > 0)
            {
                CommonCodeItem item = (CommonCodeItem)info.list[0];
                if ("N".Equals(item.isValidated))
                {
                    CommonUtility.AlertMessage("Please input right requester");
                    return false;
                }
            }

            return true;
        }

        private bool ValidateWorkArea(int tab)
        {
            string areaCode = "";
            string[] codeList;
            bool result = true;
            CommonProxy proxy = new CommonProxy();
            ResponseInfo infoH = null, infoW = null, infoWH = null;
            switch (tab)
            {
                case HCM119.TAB_FORKLIFT:
                    areaCode = txtFL_WArea.Text;
                    break;
                case HCM119.TAB_MANPOWER:
                    areaCode = txtMan_WArea.Text;
                    break;
                default:
                    return true;
            }

            if (areaCode.IndexOf(",") != -1)
            {
                codeList = areaCode.Split(',');
            }
            else
            {
                codeList = new string[] { areaCode };
            }

            for (int j = 0; j < codeList.Length; j++)
            {
                string validateCode = codeList[j];
                string WAreaType = CommonUtility.GetWorkAreaType(validateCode);
                bool inside = false;
                switch (WAreaType)
                {
                    case HCM113.AREA_HATCH:
                        if (infoH == null)
                        {
                            CommonCodeParm comCodeParm = new CommonCodeParm();
                            comCodeParm.searchType = "COMM";
                            comCodeParm.lcd = "MT";
                            comCodeParm.divCd = "HTC";
                            infoH = proxy.getCommonCodeList(comCodeParm);
                        }
                        for (int i = 0; i < infoH.list.Length; i++)
                        {
                            if (infoH.list[i] is CodeMasterListItem)
                            {
                                CodeMasterListItem item = (CodeMasterListItem)infoH.list[i];
                                if (validateCode.Equals(item.scd))
                                    inside = true;
                            }
                            else if (infoH.list[i] is CodeMasterListItem1)
                            {
                                CodeMasterListItem1 item = (CodeMasterListItem1)infoH.list[i];
                                if (validateCode.Equals(item.scd))
                                    inside = true;
                            }
                        }
                        break;
                    case HCM113.AREA_WHARF:
                        if (infoW == null)
                        {
                            LocationCodeParm locParm = new LocationCodeParm();
                            locParm.searchType = "BerthLoc";
                            locParm.berthTp = "WRF";
                            locParm.locCd = HCM113.LOC_CD;
                            infoW = proxy.getLocationCodeList(locParm);
                        }
                        for (int i = 0; i < infoW.list.Length; i++)
                        {
                            if (infoW.list[i] is LocationCodeItem)
                            {
                                LocationCodeItem item = (LocationCodeItem)infoW.list[i];
                                if (validateCode.Equals(item.cd))
                                    inside = true;
                            }
                            else if (infoW.list[i] is CodeMasterListItem)
                            {
                                CodeMasterListItem item = (CodeMasterListItem)infoW.list[i];
                                if (validateCode.Equals(item.scd))
                                    inside = true;
                            }
                            else if (infoW.list[i] is CodeMasterListItem1)
                            {
                                CodeMasterListItem1 item = (CodeMasterListItem1)infoW.list[i];
                                if (validateCode.Equals(item.scd))
                                    inside = true;
                            }
                        }
                        break;
                    case HCM113.AREA_WHO:
                        if (infoWH == null)
                        {
                            LocationCodeParm locCodeParm = new LocationCodeParm();
                            locCodeParm.searchType = "LocDef";
                            locCodeParm.locDivCd = "WHO";
                            infoWH = proxy.getLocationCodeList(locCodeParm);
                        }
                        for (int i = 0; i < infoWH.list.Length; i++)
                        {
                            if (infoWH.list[i] is LocationCodeItem)
                            {
                                LocationCodeItem item = (LocationCodeItem)infoWH.list[i];
                                if (validateCode.Equals(item.cd))
                                    inside = true;
                            }
                        }
                        break;
                }

                if (!inside)
                {
                    result = false;
                    CommonUtility.AlertMessage("Please input right work area");
                    continue;
                }
            }
            return result;
        }

        private bool Validate(int tab)
        {
            if (!ValidateCgType(tab))
            {
                return false;
            }

            if (!ValidateNumber(tab))
            {
                return false;
            }

            if (!ValidateDateRange(tab))
            {
                return false;
            }

            if (!ValidateContractor(tab))
            {
                return false;
            }

            if (!ValidateRequester(tab))
            {
                return false;
            }

            if (!ValidateWorkArea(tab))
            {
                return false;
            }

            return true;
        }

        private bool AddItem()
        {
            int activeTab = -1;
            if (tabChecklist.SelectedIndex == tabChecklist.TabPages.IndexOf(tabMan))
                activeTab = HCM119.TAB_MANPOWER;
            else if (tabChecklist.SelectedIndex == tabChecklist.TabPages.IndexOf(tabPC))
                activeTab = HCM119.TAB_PORTCRANE;
            else if (tabChecklist.SelectedIndex == tabChecklist.TabPages.IndexOf(tabSTV))
                activeTab = HCM119.TAB_STEVEDORE;
            else if (tabChecklist.SelectedIndex == tabChecklist.TabPages.IndexOf(tabFL))
                activeTab = HCM119.TAB_FORKLIFT;
            else if (tabChecklist.SelectedIndex == tabChecklist.TabPages.IndexOf(tabTR))
                activeTab = HCM119.TAB_TRAILER;
            else if (tabChecklist.SelectedIndex == tabChecklist.TabPages.IndexOf(tabMEQ))
                activeTab = HCM119.TAB_EQU;
            switch (activeTab)
            {
                case HCM119.TAB_MANPOWER:
                    if (this.validations(this.tabMan.Controls) && this.Validate(HCM119.TAB_MANPOWER))
                    {
                        // Check constraints: JPB & (StartTime or EndTime)
                        if (!IsExistAlready(HCM119.TAB_MANPOWER))
                        {
                            ReturnItems(HCM119.TAB_MANPOWER);
                            //ClearValueAtTab(HCM119.TAB_MANPOWER);
                            return true;
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
                            return false;
                        }
                    }
                    break;

                case HCM119.TAB_PORTCRANE:
                    if (this.validations(this.tabPC.Controls) && this.Validate(HCM119.TAB_PORTCRANE))
                    {
                        // Check constraints: JPB & (StartTime or EndTime)
                        if (!IsExistAlready(HCM119.TAB_PORTCRANE))
                        {
                            ReturnItems(HCM119.TAB_PORTCRANE);
                            //ClearValueAtTab(HCM119.TAB_PORTCRANE);
                            return true;
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
                            return false;
                        }
                    }
                    break;

                case HCM119.TAB_STEVEDORE:
                    if (this.validations(this.tabSTV.Controls) && this.Validate(HCM119.TAB_STEVEDORE))
                    {
                        if (!IsExistAlready(HCM119.TAB_STEVEDORE))
                        {
                            ReturnItems(HCM119.TAB_STEVEDORE);
                            //ClearValueAtTab(HCM119.TAB_STEVEDORE);
                            return true;
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
                            return false;
                        }
                    }
                    break;

                case HCM119.TAB_FORKLIFT:
                    if (this.validations(this.tabFL.Controls) && this.Validate(HCM119.TAB_FORKLIFT))
                    {
                        if (!IsExistAlready(HCM119.TAB_FORKLIFT))
                        {
                            ReturnItems(HCM119.TAB_FORKLIFT);
                            //ClearValueAtTab(HCM119.TAB_FORKLIFT);
                            return true;
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
                            return false;
                        }
                    }
                    break;

                case HCM119.TAB_TRAILER:
                    if (this.validations(this.tabTR.Controls) && this.Validate(HCM119.TAB_TRAILER))
                    {
                        if (!IsExistAlready(HCM119.TAB_TRAILER))
                        {
                            ReturnItems(HCM119.TAB_TRAILER);
                            //ClearValueAtTab(HCM119.TAB_TRAILER);
                            return true;
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
                            return false;
                        }
                    }
                    break;

                case HCM119.TAB_EQU:
                    if (this.validations(this.tabMEQ.Controls) && this.Validate(HCM119.TAB_EQU))
                    {
                        if (!IsExistAlready(HCM119.TAB_EQU))
                        {
                            ReturnItems(HCM119.TAB_EQU);
                            //ClearValueAtTab(HCM119.TAB_EQU);
                            return true;
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
                            return false;
                        }
                    }
                    break;
            }

            return false;
        }

        private bool UpdateItem()
        {
            switch (m_activeTab)
            {
                case HCM119.TAB_MANPOWER:
                    if (this.validations(this.tabMan.Controls) && this.Validate(HCM119.TAB_MANPOWER))
                    {
                        if (!IsExistAlready(HCM119.TAB_MANPOWER))
                        {
                            ReturnItems(m_activeTab);
                            return true;
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
                            return false;
                        }
                    }
                    break;

                case HCM119.TAB_PORTCRANE:
                    if (this.validations(this.tabPC.Controls) && this.Validate(HCM119.TAB_PORTCRANE))
                    {
                        if (!IsExistAlready(HCM119.TAB_PORTCRANE))
                        {
                            ReturnItems(m_activeTab);
                            return true;
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
                            return false;
                        }
                    }
                    break;

                case HCM119.TAB_STEVEDORE:
                    if (this.validations(this.tabSTV.Controls) && this.Validate(HCM119.TAB_STEVEDORE))
                    {
                        if (!IsExistAlready(HCM119.TAB_STEVEDORE))
                        {
                            ReturnItems(m_activeTab);
                            return true;
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
                            return false;
                        }
                    }
                    break;

                case HCM119.TAB_FORKLIFT:
                    if (this.validations(this.tabFL.Controls) && this.Validate(HCM119.TAB_FORKLIFT))
                    {
                        if (!IsExistAlready(HCM119.TAB_FORKLIFT))
                        {
                            ReturnItems(m_activeTab);
                            return true;
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
                            return false;
                        }
                    }
                    break;

                case HCM119.TAB_TRAILER:
                    if (this.validations(this.tabTR.Controls) && this.Validate(HCM119.TAB_TRAILER))
                    {
                        if (!IsExistAlready(HCM119.TAB_TRAILER))
                        {
                            ReturnItems(m_activeTab);
                            return true;
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
                            return false;
                        }
                    }
                    break;

                case HCM119.TAB_EQU:
                    if (this.validations(this.tabMEQ.Controls) && this.Validate(HCM119.TAB_EQU))
                    {
                        if (!IsExistAlready(HCM119.TAB_EQU))
                        {
                            ReturnItems(m_activeTab);
                            return true;
                        }
                        else
                        {
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0045"));
                            return false;
                        }
                    }
                    break;
            }

            return false;
        }

        private void cboPC_EQNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnPC_PortCrane.Checked)
            {
                if (cboPC_EQNo != null && cboPC_EQNo.SelectedIndex > 0)
                {
                    if (m_listCapaDescrPC != null && cboPC_EQNo.SelectedIndex <= m_listCapaDescrPC.Count)
                    {
                        txtPC_Capacity.Text = m_listCapaDescrPC[cboPC_EQNo.SelectedIndex - 1].ToString();
                    }

                    SetComboboxDeployedEmpId(cboPC_JPB, "PC");
                }
                else
                {
                    txtPC_Capacity.Text = string.Empty;
                    cboPC_JPB.Items.Clear();
                    cboPC_JPB.Refresh();
                }
            }
            else if (rbtnPC_ShipCrane.Checked)
            {
                if (cboPC_EQNo != null && cboPC_EQNo.SelectedIndex > 0)
                {
                    SetComboboxDeployedEmpId(cboPC_JPB, "PC");
                }
                else
                {
                    txtPC_Capacity.Text = string.Empty;
                    cboPC_JPB.Items.Clear();
                    cboPC_JPB.Refresh();
                }
            }
        }

        private void cboFL_FLNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFL_FLNo != null && cboFL_FLNo.SelectedIndex > 0 &&
                m_listCapaDescrFL != null && cboFL_FLNo.SelectedIndex <= m_listCapaDescrFL.Count)
            {
                txtFL_Capacity.Text = m_listCapaDescrFL[cboFL_FLNo.SelectedIndex - 1].ToString();
            }
            else
            {
                txtFL_Capacity.Text = string.Empty;
            }
            if ("FLD 3T".Equals(txtFL_Capacity.Text))
            {
                cboFL_HatchDir.isMandatory = true;
            }
            else
            {
                cboFL_HatchDir.isMandatory = false;
            }
        }

        private void RadiobtnCheckedListener(object sender, EventArgs e)
        {
            RadioButton mybutton = (RadioButton)sender;
            String ctrlName = mybutton.Name;

            switch (ctrlName)
            {
                case "rbtnPC_ShipCrane":
                    /*
                     * lv.dat fix issue 41585 20130618
                    cboPC_EQNo.Enabled = false; 
                    cboPC_EQNo.isMandatory = false;
                     */
                    cboPC_EQNo.Items.Clear();
                    CommonUtility.InitializeCombobox(cboPC_EQNo);
                    txtPC_Capacity.clearControlValue();
                    //will no enable ship crane in this release
                    ArrayList SCList = getShipCrane();

                    for (int i = 0; i < SCList.Count; i++)
                    {
                        Dictionary<string, string> SC = (Dictionary<string, string>)SCList[i];
                        cboPC_EQNo.Items.Add(new ComboboxValueDescriptionPair(SC["eqFacNo"], SC["eqFacNm"]));
                    }
                    cboPC_EQNo.isMandatory = false;
                    //SetComboboxDeployedEmpId(cboPC_JPB, "PC");
                    break;
                case "rbtnPC_PortCrane":
                    //cboPC_EQNo.Enabled = true;
                    cboPC_EQNo.isMandatory = true;
                    txtPC_Capacity.clearControlValue();
                    SetComboboxPortCrane();
                    //SetComboboxDeployedEmpId(cboPC_JPB, "PC");
                    break;

                case "rbtnPC_JPB":
                    cboPC_JPB.Enabled = true;
                    txtPC_Cnrt.Enabled = false;
                    btnPC_F1.Enabled = false;
                    cboPC_JPB.isMandatory = true;
                    txtPC_Cnrt.isMandatory = false;
                    break;
                case "rbtnPC_Cnrt":
                    cboPC_JPB.Enabled = false;
                    txtPC_Cnrt.Enabled = true;
                    btnPC_F1.Enabled = true;
                    cboPC_JPB.isMandatory = false;
                    txtPC_Cnrt.isMandatory = true;
                    break;

                case "rbtnFL_JPB":
                    cboFL_JPB.Enabled = true;
                    txtFL_Cnrt.Enabled = false;
                    btnFL_F2.Enabled = false;
                    cboFL_JPB.isMandatory = true;
                    txtFL_Cnrt.isMandatory = false;
                    break;
                case "rbtnFL_Driver":
                    cboFL_JPB.Enabled = false;
                    txtFL_Cnrt.Enabled = false;
                    btnFL_F2.Enabled = false;
                    cboFL_JPB.isMandatory = false;
                    txtFL_Cnrt.isMandatory = false;
                    break;
                case "rbtnFL_Cnrt":
                    cboFL_JPB.Enabled = false;
                    txtFL_Cnrt.Enabled = true;
                    btnFL_F2.Enabled = true;
                    cboFL_JPB.isMandatory = false;
                    txtFL_Cnrt.isMandatory = true;
                    break;

                case "rbtnEQ_ShipCrew":
                    txtEQ_Cnrt.Enabled = false;
                    btnEQ_F2.Enabled = false;
                    txtEQ_Cnrt.isMandatory = false;
                    break;
                case "rbtnEQ_Cnrt":
                    txtEQ_Cnrt.Enabled = true;
                    btnEQ_F2.Enabled = true;
                    txtEQ_Cnrt.isMandatory = true;
                    break;
            }
        }

        private void WAreaSelectedIndexChangedListener(object sender, EventArgs e)
        {
            ComboBox mycombobox = (ComboBox)sender;
            String cboName = mycombobox.Name;

            switch (cboName)
            {
                case "cboTR_WArea":
                    // If working area is hatch, no need to input delivery mode(direct/indirect)
                    string strWArea = CommonUtility.GetComboboxSelectedValue(cboTR_WArea);
                    if ("".Equals(strWArea) || AREA_HATCH.Equals(strWArea))
                    {
                        cboTR_DMode.isMandatory = false;
                    }
                    else
                    {
                        cboTR_DMode.isMandatory = true;
                    }

                    // AP/FP
                    //if (AREA_HATCH.Equals(strWArea))
                    //{
                    //    cboTR_HatchDir.isMandatory = true;
                    //    cboTR_HatchDir.Enabled = true;
                    //}
                    //else
                    //{
                    //    cboTR_HatchDir.isMandatory = false;
                    //    cboTR_HatchDir.Enabled = false;
                    //}

                    FetchWArea(cboTR_WArea, cboTR_WAreaDtl);
                    break;

                case "cboEQ_WArea":
                    FetchWArea(cboEQ_WArea, cboEQ_WAreaDtl);
                    break;
            }
        }

        private void WAreaTextboxChangedListener(object sender, EventArgs e)
        {
            TextBox mytextbox = (TextBox)sender;
            String txtName = mytextbox.Name;

            switch (txtName)
            {
                case "txtFL_WArea":
                    string strWArea = mytextbox.Text;
                    string strWAreaType = CommonUtility.GetWorkAreaType(mytextbox.Text);
                    if (!string.Empty.Equals(strWAreaType))
                    {
                        m_waFLResult = new WorkingAreaResult();
                        m_waFLResult.WorkingArea = strWArea;
                        m_waFLResult.WorkingAreaType = strWAreaType;
                    }
                    break;
                case "txtST_WArea":
                    break;
                case "txtMAN_WArea":
                    break;
            }
        }

        private void PurposeSelectedIndexChangedListener(object sender, EventArgs e)
        {
            ComboBox mycombobox = (ComboBox)sender;
            ComboBox targetComboBox = cboFL_DMode;
            String cboName = mycombobox.Name;

            switch (cboName)
            {
                case "cboFL_Purpose":
                    targetComboBox = cboFL_DMode;
                    break;
                case "cboTR_Purpose":
                    targetComboBox = cboTR_DMode;
                    break;
            }

            if ("MP0001".Equals(CommonUtility.GetComboboxSelectedValue(mycombobox)))
                CommonUtility.SetComboboxSelectedItem(targetComboBox, "I");
            else if ("NP0001".Equals(CommonUtility.GetComboboxSelectedValue(mycombobox)))
                CommonUtility.SetComboboxSelectedItem(targetComboBox, "D");
        }

        private void tabVsrCheckListSelectedIndexChange(object sender, EventArgs e)
        {
            TabControl senderTabControl = (TabControl)sender;
            System.Windows.Forms.TabControl.TabPageCollection pages = senderTabControl.TabPages;
            int index = 0;
            String tabName = String.Empty;

            foreach (TabPage page in pages)
            {
                if (index == senderTabControl.SelectedIndex)
                {
                    tabName = page.Name;
                    break;
                }
                index++;
            }

            initializeTabs(tabName);
        }

        private void initializeTabs(String tabName)
        {
            if (this.tabMan.Name.Equals(tabName))
            {
                if (this.isLoadMP)
                {
                    return;
                }

                #region Man Power Tab Initialize
                // 
                // txtMan_JPBName
                // 
                this.txtMan_JPBName.Enabled = false;
                this.txtMan_JPBName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtMan_JPBName.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtMan_JPBName.Location = new System.Drawing.Point(62, 70);
                this.txtMan_JPBName.Name = "txtMan_JPBName";
                this.txtMan_JPBName.ReadOnly = true;
                this.txtMan_JPBName.Size = new System.Drawing.Size(147, 19);
                this.txtMan_JPBName.TabIndex = 158;
                // 
                // txtMan_EndTime
                // 
                this.txtMan_EndTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtMan_EndTime.CustomFormat = "dd/MM/yyyy HH:mm";
                this.txtMan_EndTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtMan_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.txtMan_EndTime.isBusinessItemName = "End Time";
                this.txtMan_EndTime.isMandatory = true;
                this.txtMan_EndTime.Location = new System.Drawing.Point(62, 150);
                this.txtMan_EndTime.Name = "txtMan_EndTime";
                this.txtMan_EndTime.Size = new System.Drawing.Size(129, 22);
                this.txtMan_EndTime.TabIndex = 149;
                //this.txtMan_EndTime.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 299);
                // 
                // txtMan_StartTime
                // 
                this.txtMan_StartTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtMan_StartTime.CustomFormat = "dd/MM/yyyy HH:mm";
                this.txtMan_StartTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtMan_StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.txtMan_StartTime.isBusinessItemName = "Start Time";
                this.txtMan_StartTime.isMandatory = true;
                this.txtMan_StartTime.Location = new System.Drawing.Point(62, 124);
                this.txtMan_StartTime.Name = "txtMan_StartTime";
                this.txtMan_StartTime.Size = new System.Drawing.Size(129, 22);
                this.txtMan_StartTime.TabIndex = 148;
                //this.txtMan_StartTime.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 815);
                // 
                // tLabel8
                // 
                this.tLabel8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel8.Location = new System.Drawing.Point(7, 155);
                this.tLabel8.Name = "tLabel8";
                this.tLabel8.Size = new System.Drawing.Size(52, 19);
                this.tLabel8.Text = "End Time";
                // 
                // tLabel10
                // 
                this.tLabel10.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel10.Location = new System.Drawing.Point(7, 129);
                this.tLabel10.Name = "tLabel10";
                this.tLabel10.Size = new System.Drawing.Size(55, 19);
                this.tLabel10.Text = "Start Time";
                // 
                // tLabel1
                // 
                this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel1.Location = new System.Drawing.Point(6, 99);
                this.tLabel1.Name = "tLabel1";
                this.tLabel1.Size = new System.Drawing.Size(41, 16);
                this.tLabel1.Text = "W.Area";
                // 
                // btnMan_F2
                // 
                this.btnMan_F2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnMan_F2.Location = new System.Drawing.Point(170, 96);
                this.btnMan_F2.Name = "btnMan_F2";
                this.btnMan_F2.Size = new System.Drawing.Size(39, 19);
                this.btnMan_F2.TabIndex = 91;
                this.btnMan_F2.Text = "F2";
                this.btnMan_F2.Click += new System.EventHandler(this.ActionListener);
                // 
                // txtMan_WArea
                // 
                this.txtMan_WArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.txtMan_WArea.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtMan_WArea.isBusinessItemName = "W.Area";
                this.txtMan_WArea.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtMan_WArea.isMandatory = true;
                this.txtMan_WArea.Location = new System.Drawing.Point(62, 96);
                this.txtMan_WArea.Name = "txtMan_WArea";
                //user want to input text in this textbox 10/6/2014 
                //this.txtMan_WArea.ReadOnly = true;
                this.txtMan_WArea.Size = new System.Drawing.Size(100, 19);
                this.txtMan_WArea.TabIndex = 90;
                this.txtMan_WArea.TextChanged += WAreaTextboxChangedListener;
                // 
                // tLabel9
                // 
                this.tLabel9.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel9.Location = new System.Drawing.Point(15, 49);
                this.tLabel9.Name = "tLabel9";
                this.tLabel9.Size = new System.Drawing.Size(32, 16);
                this.tLabel9.Text = "JPB";
                // 
                // btnMan_F1
                // 
                this.btnMan_F1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnMan_F1.Location = new System.Drawing.Point(170, 46);
                this.btnMan_F1.Name = "btnMan_F1";
                this.btnMan_F1.Size = new System.Drawing.Size(39, 19);
                this.btnMan_F1.TabIndex = 65;
                this.btnMan_F1.Text = "F1";
                this.btnMan_F1.Click += new System.EventHandler(this.ActionListener);
                // 
                // txtMan_JPB
                // 
                this.txtMan_JPB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.txtMan_JPB.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtMan_JPB.isBusinessItemName = "JPB";
                this.txtMan_JPB.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtMan_JPB.isMandatory = true;
                this.txtMan_JPB.Location = new System.Drawing.Point(62, 46);
                this.txtMan_JPB.Name = "txtMan_JPB";
                this.txtMan_JPB.ReadOnly = true;
                this.txtMan_JPB.Size = new System.Drawing.Size(100, 19);
                this.txtMan_JPB.TabIndex = 63;
                // 
                // cboMan_Role
                // 
                this.cboMan_Role.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboMan_Role.isBusinessItemName = "Role";
                this.cboMan_Role.isMandatory = true;
                this.cboMan_Role.Location = new System.Drawing.Point(62, 21);
                this.cboMan_Role.Name = "cboMan_Role";
                this.cboMan_Role.Size = new System.Drawing.Size(147, 19);
                this.cboMan_Role.TabIndex = 1;
                // 
                // lblMan_Role
                // 
                this.lblMan_Role.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.lblMan_Role.Location = new System.Drawing.Point(15, 24);
                this.lblMan_Role.Name = "lblMan_Role";
                this.lblMan_Role.Size = new System.Drawing.Size(32, 16);
                this.lblMan_Role.Text = "Role";
                //
                // pnlMan_Remark
                //
                this.pnlMan_Remark.Visible = true;
                this.pnlMan_Remark.Location = new Point(16, 174);
                this.pnlMan_Remark.BorderStyle = BorderStyle.None;

                #endregion

                this.isLoadMP = true;
            }
            else if (this.tabPC.Name.Equals(tabName))
            {
                if (this.isLoadPC)
                {
                    return;
                }

                #region Port Crane Tab Initialize
                // 
                // btnPC_F2
                // 
                this.btnPC_F2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnPC_F2.Location = new System.Drawing.Point(138, 135);
                this.btnPC_F2.Name = "btnPC_F2";
                this.btnPC_F2.Size = new System.Drawing.Size(35, 18);
                this.btnPC_F2.TabIndex = 37;
                this.btnPC_F2.Text = "F2";
                this.btnPC_F2.Click += new System.EventHandler(this.ActionListener);
                // 
                // txtPC_Requester
                // 
                this.txtPC_Requester.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.txtPC_Requester.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtPC_Requester.isBusinessItemName = "Requester";
                this.txtPC_Requester.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtPC_Requester.isMandatory = true;
                this.txtPC_Requester.Location = new System.Drawing.Point(54, 136);
                this.txtPC_Requester.Multiline = true;
                this.txtPC_Requester.Name = "txtPC_Requester";
                this.txtPC_Requester.ReadOnly = false;
                this.txtPC_Requester.Size = new System.Drawing.Size(82, 17);
                this.txtPC_Requester.TabIndex = 36;
                // 
                // cboPC_Purpose
                // 
                this.cboPC_Purpose.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboPC_Purpose.isBusinessItemName = "Purpose";
                this.cboPC_Purpose.isMandatory = true;
                this.cboPC_Purpose.Location = new System.Drawing.Point(53, 114);
                this.cboPC_Purpose.Name = "cboPC_Purpose";
                this.cboPC_Purpose.Size = new System.Drawing.Size(83, 19);
                this.cboPC_Purpose.TabIndex = 25;
                // 
                // tLabel37
                // 
                this.tLabel37.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel37.Location = new System.Drawing.Point(0, 115);
                this.tLabel37.Name = "tLabel37";
                this.tLabel37.Size = new System.Drawing.Size(53, 16);
                this.tLabel37.Text = "Purpose";
                // 
                // cboPC_CgTp
                // 
                this.cboPC_CgTp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboPC_CgTp.isBusinessItemName = "Cargo Type";
                this.cboPC_CgTp.isMandatory = false;
                this.cboPC_CgTp.Location = new System.Drawing.Point(167, 115);
                this.cboPC_CgTp.Name = "cboPC_CgTp";
                this.cboPC_CgTp.Size = new System.Drawing.Size(61, 19);
                this.cboPC_CgTp.TabIndex = 17;
                // 
                // tLabel36
                // 
                this.tLabel36.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel36.Location = new System.Drawing.Point(135, 116);
                this.tLabel36.Name = "tLabel36";
                this.tLabel36.Size = new System.Drawing.Size(29, 16);
                this.tLabel36.Text = "CgTp";
                // 
                // tLabel35
                // 
                this.tLabel35.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel35.Location = new System.Drawing.Point(0, 138);
                this.tLabel35.Name = "tLabel35";
                this.tLabel35.Size = new System.Drawing.Size(53, 16);
                this.tLabel35.Text = "Requestor";
                // 
                // pnlPC_JPB
                // 
                this.pnlPC_JPB.Controls.Add(this.rbtnPC_JPB);
                this.pnlPC_JPB.Controls.Add(this.rbtnPC_Cnrt);
                this.pnlPC_JPB.Location = new System.Drawing.Point(5, 68);
                this.pnlPC_JPB.Name = "pnlPC_JPB";
                this.pnlPC_JPB.Size = new System.Drawing.Size(76, 42);
                // 
                // rbtnPC_JPB
                // 
                this.rbtnPC_JPB.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.rbtnPC_JPB.isBusinessItemName = "";
                this.rbtnPC_JPB.isMandatory = false;
                this.rbtnPC_JPB.Location = new System.Drawing.Point(3, 2);
                this.rbtnPC_JPB.Name = "rbtnPC_JPB";
                this.rbtnPC_JPB.Size = new System.Drawing.Size(39, 17);
                this.rbtnPC_JPB.TabIndex = 5;
                this.rbtnPC_JPB.Text = "JPB";
                this.rbtnPC_JPB.CheckedChanged += new System.EventHandler(this.RadiobtnCheckedListener);
                // 
                // rbtnPC_Cnrt
                // 
                this.rbtnPC_Cnrt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.rbtnPC_Cnrt.isBusinessItemName = "";
                this.rbtnPC_Cnrt.isMandatory = false;
                this.rbtnPC_Cnrt.Location = new System.Drawing.Point(3, 23);
                this.rbtnPC_Cnrt.Name = "rbtnPC_Cnrt";
                this.rbtnPC_Cnrt.Size = new System.Drawing.Size(72, 17);
                this.rbtnPC_Cnrt.TabIndex = 6;
                this.rbtnPC_Cnrt.Text = "Contractor";
                this.rbtnPC_Cnrt.CheckedChanged += new System.EventHandler(this.RadiobtnCheckedListener);
                // 
                // pnlPC_Crane
                // 
                this.pnlPC_Crane.Controls.Add(this.rbtnPC_ShipCrane);
                this.pnlPC_Crane.Controls.Add(this.rbtnPC_PortCrane);
                this.pnlPC_Crane.Location = new System.Drawing.Point(0, 2);
                this.pnlPC_Crane.Name = "pnlPC_Crane";
                this.pnlPC_Crane.Size = new System.Drawing.Size(79, 42);
                // 
                // rbtnPC_ShipCrane
                // 
                this.rbtnPC_ShipCrane.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.rbtnPC_ShipCrane.isBusinessItemName = "";
                this.rbtnPC_ShipCrane.isMandatory = false;
                this.rbtnPC_ShipCrane.Location = new System.Drawing.Point(3, 3);
                this.rbtnPC_ShipCrane.Name = "rbtnPC_ShipCrane";
                this.rbtnPC_ShipCrane.Size = new System.Drawing.Size(74, 17);
                this.rbtnPC_ShipCrane.TabIndex = 1;
                this.rbtnPC_ShipCrane.Text = "Ship Crane";
                this.rbtnPC_ShipCrane.CheckedChanged += new System.EventHandler(this.RadiobtnCheckedListener);
                // 
                // rbtnPC_PortCrane
                // 
                this.rbtnPC_PortCrane.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.rbtnPC_PortCrane.isBusinessItemName = "";
                this.rbtnPC_PortCrane.isMandatory = false;
                this.rbtnPC_PortCrane.Location = new System.Drawing.Point(3, 24);
                this.rbtnPC_PortCrane.Name = "rbtnPC_PortCrane";
                this.rbtnPC_PortCrane.Size = new System.Drawing.Size(72, 17);
                this.rbtnPC_PortCrane.TabIndex = 2;
                this.rbtnPC_PortCrane.Text = "Port Crane";
                this.rbtnPC_PortCrane.CheckedChanged += new System.EventHandler(this.RadiobtnCheckedListener);
                // 
                // cboPC_JPB
                // 
                this.cboPC_JPB.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboPC_JPB.isBusinessItemName = "JPB";
                this.cboPC_JPB.isMandatory = false;
                this.cboPC_JPB.Location = new System.Drawing.Point(83, 68);
                this.cboPC_JPB.Name = "cboPC_JPB";
                this.cboPC_JPB.Size = new System.Drawing.Size(144, 19);
                this.cboPC_JPB.TabIndex = 7;
                // 
                // txtPC_EndTime
                // 
                this.txtPC_EndTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtPC_EndTime.CustomFormat = "dd/MM/yyyy HH:mm";
                this.txtPC_EndTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtPC_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.txtPC_EndTime.isBusinessItemName = "End Time";
                this.txtPC_EndTime.isMandatory = true;
                this.txtPC_EndTime.Location = new System.Drawing.Point(83, 187);
                this.txtPC_EndTime.Name = "txtPC_EndTime";
                this.txtPC_EndTime.Size = new System.Drawing.Size(129, 22);
                this.txtPC_EndTime.TabIndex = 13;
                //this.txtPC_EndTime.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 299);
                // 
                // txtPC_StartTime
                // 
                this.txtPC_StartTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtPC_StartTime.CustomFormat = "dd/MM/yyyy HH:mm";
                this.txtPC_StartTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtPC_StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.txtPC_StartTime.isBusinessItemName = "Start Time";
                this.txtPC_StartTime.isMandatory = true;
                this.txtPC_StartTime.Location = new System.Drawing.Point(83, 162);
                this.txtPC_StartTime.Name = "txtPC_StartTime";
                this.txtPC_StartTime.Size = new System.Drawing.Size(129, 22);
                this.txtPC_StartTime.TabIndex = 12;
                //this.txtPC_StartTime.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 815);
                // 
                // tLabel3
                // 
                this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel3.Location = new System.Drawing.Point(27, 192);
                this.tLabel3.Name = "tLabel3";
                this.tLabel3.Size = new System.Drawing.Size(52, 17);
                this.tLabel3.Text = "End Time";
                // 
                // tLabel5
                // 
                this.tLabel5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel5.Location = new System.Drawing.Point(27, 167);
                this.tLabel5.Name = "tLabel5";
                this.tLabel5.Size = new System.Drawing.Size(55, 17);
                this.tLabel5.Text = "Start Time";
                // 
                // txtPC_Capacity
                // 
                this.txtPC_Capacity.Enabled = false;
                this.txtPC_Capacity.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtPC_Capacity.Location = new System.Drawing.Point(81, 46);
                this.txtPC_Capacity.Name = "txtPC_Capacity";
                this.txtPC_Capacity.Size = new System.Drawing.Size(138, 19);
                this.txtPC_Capacity.TabIndex = 4;
                // 
                // btnPC_F1
                // 
                this.btnPC_F1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnPC_F1.Location = new System.Drawing.Point(188, 93);
                this.btnPC_F1.Name = "btnPC_F1";
                this.btnPC_F1.Size = new System.Drawing.Size(39, 19);
                this.btnPC_F1.TabIndex = 9;
                this.btnPC_F1.Text = "F1";
                this.btnPC_F1.Click += new System.EventHandler(this.ActionListener);
                // 
                // txtPC_Cnrt
                // 
                this.txtPC_Cnrt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.txtPC_Cnrt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtPC_Cnrt.isBusinessItemName = "Contractor";
                this.txtPC_Cnrt.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtPC_Cnrt.Location = new System.Drawing.Point(83, 91);
                this.txtPC_Cnrt.Name = "txtPC_Cnrt";
                this.txtPC_Cnrt.ReadOnly = false;
                this.txtPC_Cnrt.Size = new System.Drawing.Size(100, 19);
                this.txtPC_Cnrt.TabIndex = 8;
                // 
                // cboPC_EQNo
                // 
                this.cboPC_EQNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboPC_EQNo.isBusinessItemName = "P.Crane No";
                this.cboPC_EQNo.isMandatory = true;
                this.cboPC_EQNo.Location = new System.Drawing.Point(81, 19);
                this.cboPC_EQNo.Name = "cboPC_EQNo";
                this.cboPC_EQNo.Size = new System.Drawing.Size(138, 19);
                this.cboPC_EQNo.TabIndex = 3;
                this.cboPC_EQNo.SelectedIndexChanged += new System.EventHandler(this.cboPC_EQNo_SelectedIndexChanged);
                //
                // btnPC_Remark
                //
                this.btnPC_Remark.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnPC_Remark.Location = new System.Drawing.Point(175, 138);
                this.btnPC_Remark.Name = "btnPC_Remark";
                this.btnPC_Remark.Size = new System.Drawing.Size(50, 20);
                this.btnPC_Remark.TabIndex = 18;
                this.btnPC_Remark.Text = "Remark";
                this.btnPC_Remark.Click += new System.EventHandler(this.ActionListener);
                //
                // pnlPC_Remark
                //
                this.pnlPC_Remark.Visible = false;
                this.pnlPC_Remark.Location = new Point(this.btnPC_Remark.Left - this.pnlPC_Remark.Width, this.btnPC_Remark.Top - this.pnlPC_Remark.Height + this.btnPC_Remark.Height);

                #endregion

                this.isLoadPC = true;
            }
            else if (this.tabTR.Name.Equals(tabName))
            {
                if (this.isLoadTR)
                {
                    return;
                }

                #region Trailer Tab Initialize
                // 
                // txtTR_Requester
                // 
                this.txtTR_Requester.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.txtTR_Requester.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtTR_Requester.isBusinessItemName = "Requester";
                this.txtTR_Requester.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtTR_Requester.isMandatory = true;
                this.txtTR_Requester.Location = new System.Drawing.Point(59, 92);
                this.txtTR_Requester.Multiline = true;
                this.txtTR_Requester.Name = "txtTR_Requester";
                this.txtTR_Requester.ReadOnly = false;
                this.txtTR_Requester.Size = new System.Drawing.Size(82, 17);
                this.txtTR_Requester.TabIndex = 32;
                // 
                // btnTR_F3
                // 
                this.btnTR_F3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnTR_F3.Location = new System.Drawing.Point(146, 91);
                this.btnTR_F3.Name = "btnTR_F3";
                this.btnTR_F3.Size = new System.Drawing.Size(39, 18);
                this.btnTR_F3.TabIndex = 33;
                this.btnTR_F3.Text = "F3";
                this.btnTR_F3.Click += new System.EventHandler(this.ActionListener);
                // 
                // cboTR_HatchDir
                // 
                this.cboTR_HatchDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboTR_HatchDir.isBusinessItemName = "AP/FP";
                this.cboTR_HatchDir.isMandatory = false;
                this.cboTR_HatchDir.Enabled = true;
                this.cboTR_HatchDir.Location = new System.Drawing.Point(179, 46);
                this.cboTR_HatchDir.Name = "cboTR_HatchDir";
                this.cboTR_HatchDir.Size = new System.Drawing.Size(49, 19);
                this.cboTR_HatchDir.TabIndex = 31;
                // 
                // txtTR_Cntr
                // 
                this.txtTR_Cntr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.txtTR_Cntr.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtTR_Cntr.isBusinessItemName = "Contractor";
                this.txtTR_Cntr.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtTR_Cntr.isMandatory = true;
                this.txtTR_Cntr.Location = new System.Drawing.Point(59, 48);
                this.txtTR_Cntr.Multiline = true;
                this.txtTR_Cntr.Name = "txtTR_Cntr";
                this.txtTR_Cntr.ReadOnly = false;
                this.txtTR_Cntr.Size = new System.Drawing.Size(71, 17);
                this.txtTR_Cntr.TabIndex = 10;
                // 
                // cboTR_Purpose
                // 
                this.cboTR_Purpose.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboTR_Purpose.isBusinessItemName = "Purpose";
                this.cboTR_Purpose.isMandatory = true;
                this.cboTR_Purpose.Location = new System.Drawing.Point(43, 115);
                this.cboTR_Purpose.Name = "cboTR_Purpose";
                this.cboTR_Purpose.Size = new System.Drawing.Size(87, 19);
                this.cboTR_Purpose.TabIndex = 10;
                this.cboTR_Purpose.SelectedIndexChanged += PurposeSelectedIndexChangedListener;
                // 
                // lblTR_Purpose
                // 
                this.lblTR_Purpose.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.lblTR_Purpose.Location = new System.Drawing.Point(0, 116);
                this.lblTR_Purpose.Name = "lblTR_Purpose";
                this.lblTR_Purpose.Size = new System.Drawing.Size(44, 16);
                this.lblTR_Purpose.Text = "Purpose";
                // 
                // tLabel27
                // 
                this.tLabel27.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel27.Location = new System.Drawing.Point(0, 49);
                this.tLabel27.Name = "tLabel27";
                this.tLabel27.Size = new System.Drawing.Size(60, 16);
                this.tLabel27.Text = "Contractor";
                // 
                // cboTR_DMode
                // 
                this.cboTR_DMode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboTR_DMode.isBusinessItemName = "D.Mode";
                this.cboTR_DMode.isMandatory = false;
                this.cboTR_DMode.Location = new System.Drawing.Point(171, 115);
                this.cboTR_DMode.Name = "cboTR_DMode";
                this.cboTR_DMode.Size = new System.Drawing.Size(58, 19);
                this.cboTR_DMode.TabIndex = 18;
                // 
                // tLabel24
                // 
                this.tLabel24.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel24.Location = new System.Drawing.Point(0, 93);
                this.tLabel24.Name = "tLabel24";
                this.tLabel24.Size = new System.Drawing.Size(53, 16);
                this.tLabel24.Text = "Requestor";
                // 
                // cboTR_CgTp
                // 
                this.cboTR_CgTp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboTR_CgTp.isBusinessItemName = "Cargo Type";
                this.cboTR_CgTp.isMandatory = true;
                this.cboTR_CgTp.Location = new System.Drawing.Point(59, 68);
                this.cboTR_CgTp.Name = "cboTR_CgTp";
                this.cboTR_CgTp.Size = new System.Drawing.Size(120, 19);
                this.cboTR_CgTp.TabIndex = 14;
                // 
                // tLabel25
                // 
                this.tLabel25.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel25.Location = new System.Drawing.Point(0, 69);
                this.tLabel25.Name = "tLabel25";
                this.tLabel25.Size = new System.Drawing.Size(60, 16);
                this.tLabel25.Text = "Cargo Type";
                // 
                // tLabel26
                // 
                this.tLabel26.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel26.Location = new System.Drawing.Point(132, 118);
                this.tLabel26.Name = "tLabel26";
                this.tLabel26.Size = new System.Drawing.Size(41, 16);
                this.tLabel26.Text = "D.Mode";
                // 
                // tLabel18
                // 
                this.tLabel18.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel18.Location = new System.Drawing.Point(2, 143);
                this.tLabel18.Name = "tLabel18";
                this.tLabel18.Size = new System.Drawing.Size(63, 19);
                this.tLabel18.Text = "EQ Arr Time";
                // 
                // txtTR_EqArrDt
                // 
                this.txtTR_EqArrDt.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtTR_EqArrDt.CustomFormat = "dd/MM/yyyy HH:mm";
                this.txtTR_EqArrDt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtTR_EqArrDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.txtTR_EqArrDt.isBusinessItemName = "EQ Arr Time";
                this.txtTR_EqArrDt.isMandatory = false;
                this.txtTR_EqArrDt.Location = new System.Drawing.Point(66, 140);
                this.txtTR_EqArrDt.Name = "txtTR_EqArrDt";
                this.txtTR_EqArrDt.Size = new System.Drawing.Size(129, 22);
                this.txtTR_EqArrDt.TabIndex = 22;
                //this.txtTR_EqArrDt.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 96);
                this.txtTR_EqArrDt.Value = new System.DateTime(DateTime.Now.Ticks);
                // 
                // cboTR_WAreaDtl
                // 
                this.cboTR_WAreaDtl.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboTR_WAreaDtl.isBusinessItemName = "W.Area Detail";
                this.cboTR_WAreaDtl.isMandatory = true;
                this.cboTR_WAreaDtl.Location = new System.Drawing.Point(112, 23);
                this.cboTR_WAreaDtl.Name = "cboTR_WAreaDtl";
                this.cboTR_WAreaDtl.Size = new System.Drawing.Size(116, 19);
                this.cboTR_WAreaDtl.TabIndex = 8;
                this.cboTR_WAreaDtl.DropDownStyle = ComboBoxStyle.DropDownList;
                // 
                // cboTR_WArea
                // 
                this.cboTR_WArea.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboTR_WArea.isBusinessItemName = "W.Area";
                this.cboTR_WArea.isMandatory = true;
                this.cboTR_WArea.Location = new System.Drawing.Point(41, 23);
                this.cboTR_WArea.Name = "cboTR_WArea";
                this.cboTR_WArea.Size = new System.Drawing.Size(69, 19);
                this.cboTR_WArea.TabIndex = 6;
                this.cboTR_WArea.SelectedIndexChanged += new System.EventHandler(this.WAreaSelectedIndexChangedListener);
                // 
                // tLabel19
                // 
                this.tLabel19.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel19.Location = new System.Drawing.Point(0, 24);
                this.tLabel19.Name = "tLabel19";
                this.tLabel19.Size = new System.Drawing.Size(39, 16);
                this.tLabel19.Text = "W.Area";
                // 
                // txtTR_Nos
                // 
                this.txtTR_Nos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.txtTR_Nos.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtTR_Nos.isBusinessItemName = "Nos";
                this.txtTR_Nos.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtTR_Nos.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
                this.txtTR_Nos.isMandatory = true;
                this.txtTR_Nos.Location = new System.Drawing.Point(179, 4);
                this.txtTR_Nos.Multiline = true;
                this.txtTR_Nos.Name = "txtTR_Nos";
                this.txtTR_Nos.Size = new System.Drawing.Size(48, 17);
                this.txtTR_Nos.TabIndex = 4;
                this.txtTR_Nos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
              
                // btnTR_F1
                // 
                this.btnTR_F1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnTR_F1.Location = new System.Drawing.Point(112, 3);
                this.btnTR_F1.Name = "btnTR_F1";
                this.btnTR_F1.Size = new System.Drawing.Size(39, 18);
                this.btnTR_F1.TabIndex = 2;
                this.btnTR_F1.Text = "F1";
                this.btnTR_F1.Click += new System.EventHandler(this.ActionListener);
                // 
                // txtTR_EQTp
                // 
                this.txtTR_EQTp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtTR_EQTp.isBusinessItemName = "EQ Type";
                this.txtTR_EQTp.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtTR_EQTp.isMandatory = true;
                this.txtTR_EQTp.Location = new System.Drawing.Point(44, 4);
                this.txtTR_EQTp.Multiline = true;
                this.txtTR_EQTp.Name = "txtTR_EQTp";
                this.txtTR_EQTp.Size = new System.Drawing.Size(65, 17);
                this.txtTR_EQTp.TabIndex = 1;
                // 
                // txtTR_EndTime
                // 
                this.txtTR_EndTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtTR_EndTime.CustomFormat = "dd/MM/yyyy HH:mm";
                this.txtTR_EndTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtTR_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.txtTR_EndTime.isBusinessItemName = "End Time";
                this.txtTR_EndTime.isMandatory = true;
                this.txtTR_EndTime.Location = new System.Drawing.Point(66, 188);
                this.txtTR_EndTime.Name = "txtTR_EndTime";
                this.txtTR_EndTime.Size = new System.Drawing.Size(129, 22);
                this.txtTR_EndTime.TabIndex = 26;
                //this.txtTR_EndTime.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 518);
                // 
                // txtTR_StartTime
                // 
                this.txtTR_StartTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtTR_StartTime.CustomFormat = "dd/MM/yyyy HH:mm";
                this.txtTR_StartTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtTR_StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.txtTR_StartTime.isBusinessItemName = "Start Time";
                this.txtTR_StartTime.isMandatory = true;
                this.txtTR_StartTime.Location = new System.Drawing.Point(66, 164);
                this.txtTR_StartTime.Name = "txtTR_StartTime";
                this.txtTR_StartTime.Size = new System.Drawing.Size(129, 22);
                this.txtTR_StartTime.TabIndex = 24;
                //this.txtTR_StartTime.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 96);
                // 
                // tLabel20
                // 
                this.tLabel20.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel20.Location = new System.Drawing.Point(2, 191);
                this.tLabel20.Name = "tLabel20";
                this.tLabel20.Size = new System.Drawing.Size(52, 19);
                this.tLabel20.Text = "End Time";
                // 
                // tLabel21
                // 
                this.tLabel21.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel21.Location = new System.Drawing.Point(2, 167);
                this.tLabel21.Name = "tLabel21";
                this.tLabel21.Size = new System.Drawing.Size(55, 19);
                this.tLabel21.Text = "Start Time";
                // 
                // tLabel22
                // 
                this.tLabel22.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel22.Location = new System.Drawing.Point(0, 6);
                this.tLabel22.Name = "tLabel22";
                this.tLabel22.Size = new System.Drawing.Size(45, 15);
                this.tLabel22.Text = "EQ Type";
                // 
                // btnTR_F2
                // 
                this.btnTR_F2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnTR_F2.Location = new System.Drawing.Point(133, 47);
                this.btnTR_F2.Name = "btnTR_F2";
                this.btnTR_F2.Size = new System.Drawing.Size(40, 18);
                this.btnTR_F2.TabIndex = 12;
                this.btnTR_F2.Text = "F2";
                this.btnTR_F2.Click += new System.EventHandler(this.ActionListener);
                // 
                // tLabel23
                // 
                this.tLabel23.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel23.Location = new System.Drawing.Point(154, 5);
                this.tLabel23.Name = "tLabel23";
                this.tLabel23.Size = new System.Drawing.Size(26, 16);
                this.tLabel23.Text = "Nos.";
                //
                // btnTR_RefNo
                //
                this.btnTR_RefNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnTR_RefNo.Location = new System.Drawing.Point(197, 168);
                this.btnTR_RefNo.Name = "btnTR_RefNo";
                this.btnTR_RefNo.Size = new System.Drawing.Size(30, 36);
                this.btnTR_RefNo.TabIndex = 14;
                this.btnTR_RefNo.Text = "Rf.No";
                this.btnTR_RefNo.Click += new System.EventHandler(this.ActionListener);
                //
                // pnlTR_RefNo
                //
                this.pnlTR_RefNo.Visible = false;
                this.pnlTR_RefNo.rbtn_RefTxt.Checked = true;
                this.pnlTR_RefNo.Location = new Point(this.btnTR_RefNo.Left - this.pnlTR_RefNo.Width, this.btnTR_RefNo.Top - this.pnlTR_RefNo.Height + this.btnTR_RefNo.Height);
                //
                // btnTR_Remark
                //
                this.btnTR_Remark.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnTR_Remark.Location = new System.Drawing.Point(179, 68);
                this.btnTR_Remark.Name = "btnTR_Remark";
                this.btnTR_Remark.Size = new System.Drawing.Size(50, 20);
                this.btnTR_Remark.TabIndex = 18;
                this.btnTR_Remark.Text = "Remark";
                this.btnTR_Remark.Click += new System.EventHandler(this.ActionListener);
                //
                // pnlTR_Remark
                //
                this.pnlTR_Remark.Visible = false;
                this.pnlTR_Remark.Location = new Point(this.btnTR_Remark.Left - this.pnlTR_Remark.Width, this.btnTR_Remark.Top - this.pnlTR_Remark.Height + this.btnTR_Remark.Height);
                #endregion

                this.isLoadTR = true;
            }
            else if (this.tabSTV.Name.Equals(tabName))
            {
                if (this.isLoadST)
                {
                    return;
                }

                #region Stevedore Tab Initialize
                // 
                // txtST_Stvd
                // 
                this.txtST_Stvd.BackColor = System.Drawing.SystemColors.Control;
                this.txtST_Stvd.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtST_Stvd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                this.txtST_Stvd.isBusinessItemName = "Stevedore";
                this.txtST_Stvd.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtST_Stvd.isMandatory = true;
                this.txtST_Stvd.Location = new System.Drawing.Point(68, 32);
                this.txtST_Stvd.Name = "txtST_Stvd";
                this.txtST_Stvd.ReadOnly = true;
                this.txtST_Stvd.Size = new System.Drawing.Size(100, 19);
                this.txtST_Stvd.TabIndex = 1;
                // 
                // txtST_EndTime
                // 
                this.txtST_EndTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtST_EndTime.CustomFormat = "dd/MM/yyyy HH:mm";
                this.txtST_EndTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtST_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.txtST_EndTime.isBusinessItemName = "End Time";
                this.txtST_EndTime.isMandatory = true;
                this.txtST_EndTime.Location = new System.Drawing.Point(68, 176);
                this.txtST_EndTime.Name = "txtST_EndTime";
                this.txtST_EndTime.Size = new System.Drawing.Size(129, 22);
                this.txtST_EndTime.TabIndex = 14;
                //this.txtST_EndTime.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 299);
                // 
                // txtST_StartTime
                // 
                this.txtST_StartTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtST_StartTime.CustomFormat = "dd/MM/yyyy HH:mm";
                this.txtST_StartTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtST_StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.txtST_StartTime.isBusinessItemName = "Start Time";
                this.txtST_StartTime.isMandatory = true;
                this.txtST_StartTime.Location = new System.Drawing.Point(68, 150);
                this.txtST_StartTime.Name = "txtST_StartTime";
                this.txtST_StartTime.Size = new System.Drawing.Size(129, 22);
                this.txtST_StartTime.TabIndex = 12;
                //this.txtST_StartTime.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 815);
                // 
                // tLabel32
                // 
                this.tLabel32.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel32.Location = new System.Drawing.Point(13, 179);
                this.tLabel32.Name = "tLabel32";
                this.tLabel32.Size = new System.Drawing.Size(52, 19);
                this.tLabel32.Text = "End Time";
                // 
                // tLabel33
                // 
                this.tLabel33.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel33.Location = new System.Drawing.Point(13, 153);
                this.tLabel33.Name = "tLabel33";
                this.tLabel33.Size = new System.Drawing.Size(55, 19);
                this.tLabel33.Text = "Start Time";
                // 
                // txtST_NonTon
                // 
                this.txtST_NonTon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.txtST_NonTon.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtST_NonTon.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtST_NonTon.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
                this.txtST_NonTon.Location = new System.Drawing.Point(68, 76);
                this.txtST_NonTon.Name = "txtST_NonTon";
                this.txtST_NonTon.Size = new System.Drawing.Size(100, 19);
                this.txtST_NonTon.TabIndex = 6;
                this.txtST_NonTon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                // 
                // tLabel30
                // 
                this.tLabel30.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel30.Location = new System.Drawing.Point(12, 101);
                this.tLabel30.Name = "tLabel30";
                this.tLabel30.Size = new System.Drawing.Size(41, 16);
                this.tLabel30.Text = "W.Area";
                // 
                // btnST_F2
                // 
                this.btnST_F2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnST_F2.Location = new System.Drawing.Point(176, 98);
                this.btnST_F2.Name = "btnST_F2";
                this.btnST_F2.Size = new System.Drawing.Size(39, 19);
                this.btnST_F2.TabIndex = 10;
                this.btnST_F2.Text = "F2";
                this.btnST_F2.Click += new System.EventHandler(this.ActionListener);
                // 
                // txtST_WArea
                // 
                this.txtST_WArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.txtST_WArea.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtST_WArea.isBusinessItemName = "W.Area";
                this.txtST_WArea.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtST_WArea.isMandatory = true;
                this.txtST_WArea.Location = new System.Drawing.Point(68, 98);
                this.txtST_WArea.Name = "txtST_WArea";
                //this.txtST_WArea.ReadOnly = true;
                this.txtST_WArea.Size = new System.Drawing.Size(100, 19);
                this.txtST_WArea.TabIndex = 8;
                this.txtST_WArea.TextChanged += WAreaTextboxChangedListener;
                // 
                // tLabel29
                // 
                this.tLabel29.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel29.Location = new System.Drawing.Point(5, 57);
                this.tLabel29.Name = "tLabel29";
                this.tLabel29.Size = new System.Drawing.Size(55, 16);
                this.tLabel29.Text = "Supervisor";
                // 
                // txtST_Sprr
                // 
                this.txtST_Sprr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.txtST_Sprr.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtST_Sprr.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtST_Sprr.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
                this.txtST_Sprr.Location = new System.Drawing.Point(68, 54);
                this.txtST_Sprr.Name = "txtST_Sprr";
                this.txtST_Sprr.Size = new System.Drawing.Size(100, 19);
                this.txtST_Sprr.TabIndex = 4;
                this.txtST_Sprr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                // 
                // tLabel28
                // 
                this.tLabel28.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel28.Location = new System.Drawing.Point(5, 35);
                this.tLabel28.Name = "tLabel28";
                this.tLabel28.Size = new System.Drawing.Size(53, 16);
                this.tLabel28.Text = "Stevedore";
                // 
                // btnST_F1
                // 
                this.btnST_F1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnST_F1.Location = new System.Drawing.Point(176, 32);
                this.btnST_F1.Name = "btnST_F1";
                this.btnST_F1.Size = new System.Drawing.Size(39, 19);
                this.btnST_F1.TabIndex = 2;
                this.btnST_F1.Text = "F1";
                this.btnST_F1.Click += new System.EventHandler(this.ActionListener);
                // 
                // tLabel31
                // 
                this.tLabel31.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel31.Location = new System.Drawing.Point(0, 79);
                this.tLabel31.Name = "tLabel31";
                this.tLabel31.Size = new System.Drawing.Size(69, 16);
                this.tLabel31.Text = "Non Tonnage";
                //
                // cboST_Purpose
                //
                this.cboST_Purpose.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboST_Purpose.Location = new System.Drawing.Point(68, 10);
                this.cboST_Purpose.Name = "cboST_Purpose";
                this.cboST_Purpose.Size = new System.Drawing.Size(129, 22);
                this.cboST_Purpose.TabIndex = 1;
                this.cboST_Purpose.Items.Add(new MOST.Common.Utility.ComboboxValueDescriptionPair("", ""));
                this.cboST_Purpose.Items.Add(new MOST.Common.Utility.ComboboxValueDescriptionPair("NP0001", "Vessel Operation"));
                this.cboST_Purpose.Items.Add(new MOST.Common.Utility.ComboboxValueDescriptionPair("MP0001", "Warehouse Operation"));
                //
                // tLabel40
                //
                this.tLabel40.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel40.Location = new System.Drawing.Point(20, 13);
                this.tLabel40.Name = "tLabel40";
                this.tLabel40.Size = new System.Drawing.Size(53, 16);
                this.tLabel40.Text = "Purpose";
                // 
                // btnST_F3
                // 
                this.btnST_F3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnST_F3.Location = new System.Drawing.Point(176, 120);
                this.btnST_F3.Name = "btnST_F3";
                this.btnST_F3.Size = new System.Drawing.Size(39, 18);
                this.btnST_F3.TabIndex = 38;
                this.btnST_F3.TabStop = false;
                this.btnST_F3.Text = "F3";
                this.btnST_F3.Click += new System.EventHandler(this.ActionListener);
                // 
                // txtST_Requester
                // 
                this.txtST_Requester.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.txtST_Requester.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtST_Requester.isBusinessItemName = "Requester";
                this.txtST_Requester.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtST_Requester.isMandatory = true;
                this.txtST_Requester.Location = new System.Drawing.Point(68, 120);
                this.txtST_Requester.Multiline = true;
                this.txtST_Requester.Name = "txtST_Requester";
                this.txtST_Requester.ReadOnly = false;
                this.txtST_Requester.Size = new System.Drawing.Size(101, 20);
                this.txtST_Requester.TabIndex = 37;
                // 
                // tLabel39
                // 
                this.tLabel39.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel39.Location = new System.Drawing.Point(12, 123);
                this.tLabel39.Name = "tLabel39";
                this.tLabel39.Size = new System.Drawing.Size(53, 16);
                this.tLabel39.Text = "Requestor";
                #endregion

                this.isLoadST = true;
            }
            else if (this.tabFL.Name.Equals(tabName))
            {
                if (this.isLoadFL)
                {
                    return;
                }

                #region Forklift Tab Initialize
                // 
                // btnFL_F3
                // 
                this.btnFL_F3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnFL_F3.Location = new System.Drawing.Point(203, 91);
                this.btnFL_F3.Name = "btnFL_F3";
                this.btnFL_F3.Size = new System.Drawing.Size(25, 18);
                this.btnFL_F3.TabIndex = 35;
                this.btnFL_F3.Text = "F3";
                this.btnFL_F3.Click += new System.EventHandler(this.ActionListener);
                // 
                // txtFL_Requester
                // 
                this.txtFL_Requester.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.txtFL_Requester.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtFL_Requester.isBusinessItemName = "Requester";
                this.txtFL_Requester.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtFL_Requester.isMandatory = true;
                this.txtFL_Requester.Location = new System.Drawing.Point(151, 92);
                this.txtFL_Requester.Multiline = true;
                this.txtFL_Requester.Name = "txtFL_Requester";
                this.txtFL_Requester.ReadOnly = false;
                this.txtFL_Requester.Size = new System.Drawing.Size(50, 17);
                this.txtFL_Requester.TabIndex = 34;
                // 
                // btnFL_F1
                // 
                this.btnFL_F1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnFL_F1.Location = new System.Drawing.Point(149, 26);
                this.btnFL_F1.Name = "btnFL_F1";
                this.btnFL_F1.Size = new System.Drawing.Size(39, 19);
                this.btnFL_F1.TabIndex = 4;
                this.btnFL_F1.Text = "F1";
                this.btnFL_F1.Click += new System.EventHandler(this.ActionListener);
                // 
                // txtFL_WArea
                // 
                this.txtFL_WArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.txtFL_WArea.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtFL_WArea.isBusinessItemName = "W.Area";
                this.txtFL_WArea.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtFL_WArea.isMandatory = true;
                this.txtFL_WArea.Location = new System.Drawing.Point(39, 26);
                this.txtFL_WArea.Name = "txtFL_WArea";
                //user want to input text in this textbox and display APFP for forklift ( text change event ) 10/6/2014 
                //this.txtFL_WArea.ReadOnly = true;
                this.txtFL_WArea.Size = new System.Drawing.Size(107, 19);
                this.txtFL_WArea.TabIndex = 3;
                this.txtFL_WArea.TextChanged += new System.EventHandler(this.WAreaTextboxChangedListener);
                // 
                // cboFL_Purpose
                // 
                this.cboFL_Purpose.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboFL_Purpose.isBusinessItemName = "Purpose";
                this.cboFL_Purpose.isMandatory = true;
                this.cboFL_Purpose.Location = new System.Drawing.Point(34, 115);
                this.cboFL_Purpose.Name = "cboFL_Purpose";
                this.cboFL_Purpose.Size = new System.Drawing.Size(128, 19);
                this.cboFL_Purpose.TabIndex = 20;
                this.cboFL_Purpose.SelectedIndexChanged += PurposeSelectedIndexChangedListener;
                // 
                // tLabel34
                // 
                this.tLabel34.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel34.Location = new System.Drawing.Point(0, 116);
                this.tLabel34.Name = "tLabel34";
                this.tLabel34.Size = new System.Drawing.Size(32, 16);
                this.tLabel34.Text = "Purp.";
                // 
                // cboFL_DMode
                // 
                this.cboFL_DMode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboFL_DMode.isBusinessItemName = "D.Mode";
                this.cboFL_DMode.isMandatory = false;
                this.cboFL_DMode.Location = new System.Drawing.Point(169, 142);
                this.cboFL_DMode.Name = "cboFL_DMode";
                this.cboFL_DMode.Size = new System.Drawing.Size(59, 19);
                this.cboFL_DMode.TabIndex = 22;
                // 
                // cboFL_CgTp
                // 
                this.cboFL_CgTp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboFL_CgTp.isBusinessItemName = "Cg Tp";
                this.cboFL_CgTp.isMandatory = true;
                this.cboFL_CgTp.Location = new System.Drawing.Point(27, 91);
                this.cboFL_CgTp.Name = "cboFL_CgTp";
                this.cboFL_CgTp.Size = new System.Drawing.Size(70, 19);
                this.cboFL_CgTp.TabIndex = 16;
                // 
                // tLabel13
                // 
                this.tLabel13.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel13.Location = new System.Drawing.Point(0, 94);
                this.tLabel13.Name = "tLabel13";
                this.tLabel13.Size = new System.Drawing.Size(29, 16);
                this.tLabel13.Text = "CgTp";
                // 
                // txtFL_EqArrDt
                // 
                this.txtFL_EqArrDt.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtFL_EqArrDt.CustomFormat = "dd/MM/yyyy HH:mm";
                this.txtFL_EqArrDt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtFL_EqArrDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.txtFL_EqArrDt.isBusinessItemName = "EQ Arrival Time";
                this.txtFL_EqArrDt.isMandatory = true;
                this.txtFL_EqArrDt.Location = new System.Drawing.Point(34, 140);
                this.txtFL_EqArrDt.Name = "txtFL_EqArrDt";
                this.txtFL_EqArrDt.Size = new System.Drawing.Size(129, 22);
                this.txtFL_EqArrDt.TabIndex = 24;
                //this.txtFL_EqArrDt.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 909);
                this.txtFL_EqArrDt.Value = new System.DateTime(DateTime.Now.Ticks);
                // 
                // tLabel11
                // 
                this.tLabel11.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel11.Location = new System.Drawing.Point(0, 143);
                this.tLabel11.Name = "tLabel11";
                this.tLabel11.Size = new System.Drawing.Size(36, 19);
                this.tLabel11.Text = "EQ Arr";
                // 
                // tLabel4
                // 
                this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel4.Location = new System.Drawing.Point(0, 27);
                this.tLabel4.Name = "tLabel4";
                this.tLabel4.Size = new System.Drawing.Size(39, 16);
                this.tLabel4.Text = "W.Area";
                // 
                // cboFL_JPB
                // 
                this.cboFL_JPB.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboFL_JPB.isBusinessItemName = "JPB";
                this.cboFL_JPB.isMandatory = false;
                this.cboFL_JPB.Location = new System.Drawing.Point(50, 48);
                this.cboFL_JPB.Name = "cboFL_JPB";
                this.cboFL_JPB.Size = new System.Drawing.Size(80, 19);
                this.cboFL_JPB.TabIndex = 9;
                // 
                // txtFL_EndTime
                // 
                this.txtFL_EndTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtFL_EndTime.CustomFormat = "dd/MM/yyyy HH:mm";
                this.txtFL_EndTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtFL_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.txtFL_EndTime.isBusinessItemName = "End Time";
                this.txtFL_EndTime.isMandatory = true;
                this.txtFL_EndTime.Location = new System.Drawing.Point(60, 188);
                this.txtFL_EndTime.Name = "txtFL_EndTime";
                this.txtFL_EndTime.Size = new System.Drawing.Size(135, 22);
                this.txtFL_EndTime.TabIndex = 28;
                //this.txtFL_EndTime.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 643);
                // 
                // txtFL_StartTime
                // 
                this.txtFL_StartTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtFL_StartTime.CustomFormat = "dd/MM/yyyy HH:mm";
                this.txtFL_StartTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtFL_StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.txtFL_StartTime.isBusinessItemName = "Start Time";
                this.txtFL_StartTime.isMandatory = true;
                this.txtFL_StartTime.Location = new System.Drawing.Point(60, 164);
                this.txtFL_StartTime.Name = "txtFL_StartTime";
                this.txtFL_StartTime.Size = new System.Drawing.Size(135, 22);
                this.txtFL_StartTime.TabIndex = 26;
                //this.txtFL_StartTime.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 909);
                // 
                // tLabel6
                // 
                this.tLabel6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel6.Location = new System.Drawing.Point(1, 190);
                this.tLabel6.Name = "tLabel6";
                this.tLabel6.Size = new System.Drawing.Size(55, 19);
                this.tLabel6.Text = "End Time";
                // 
                // tLabel7
                // 
                this.tLabel7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel7.Location = new System.Drawing.Point(1, 166);
                this.tLabel7.Name = "tLabel7";
                this.tLabel7.Size = new System.Drawing.Size(55, 19);
                this.tLabel7.Text = "Start Time";
                // 
                // txtFL_Capacity
                // 
                this.txtFL_Capacity.Enabled = false;
                this.txtFL_Capacity.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtFL_Capacity.Location = new System.Drawing.Point(182, 3);
                this.txtFL_Capacity.Name = "txtFL_Capacity";
                this.txtFL_Capacity.Size = new System.Drawing.Size(47, 19);
                this.txtFL_Capacity.TabIndex = 2;
                // 
                // btnFL_F2
                // 
                this.btnFL_F2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnFL_F2.Location = new System.Drawing.Point(182, 72);
                this.btnFL_F2.Name = "btnFL_F2";
                this.btnFL_F2.Size = new System.Drawing.Size(39, 17);
                this.btnFL_F2.TabIndex = 14;
                this.btnFL_F2.Text = "F2";
                this.btnFL_F2.Click += new System.EventHandler(this.ActionListener);
                // 
                // txtFL_Cnrt
                // 
                this.txtFL_Cnrt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.txtFL_Cnrt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtFL_Cnrt.isBusinessItemName = "Contractor";
                this.txtFL_Cnrt.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtFL_Cnrt.Location = new System.Drawing.Point(77, 72);
                this.txtFL_Cnrt.Multiline = true;
                this.txtFL_Cnrt.Name = "txtFL_Cnrt";
                this.txtFL_Cnrt.ReadOnly = false;
                this.txtFL_Cnrt.Size = new System.Drawing.Size(100, 17);
                this.txtFL_Cnrt.TabIndex = 13;
                // 
                // rbtnFL_Cnrt
                // 
                this.rbtnFL_Cnrt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.rbtnFL_Cnrt.isBusinessItemName = "";
                this.rbtnFL_Cnrt.isMandatory = false;
                this.rbtnFL_Cnrt.Location = new System.Drawing.Point(3, 72);
                this.rbtnFL_Cnrt.Name = "rbtnFL_Cnrt";
                this.rbtnFL_Cnrt.Size = new System.Drawing.Size(74, 17);
                this.rbtnFL_Cnrt.TabIndex = 11;
                this.rbtnFL_Cnrt.Text = "Contractor";
                this.rbtnFL_Cnrt.CheckedChanged += new System.EventHandler(this.RadiobtnCheckedListener);
                // 
                // rbtnFL_JPB
                // 
                this.rbtnFL_JPB.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.rbtnFL_JPB.isBusinessItemName = "";
                this.rbtnFL_JPB.isMandatory = false;
                this.rbtnFL_JPB.Location = new System.Drawing.Point(3, 50);
                this.rbtnFL_JPB.Name = "rbtnFL_JPB";
                this.rbtnFL_JPB.Size = new System.Drawing.Size(44, 17);
                this.rbtnFL_JPB.TabIndex = 8;
                this.rbtnFL_JPB.Text = "JPB";
                this.rbtnFL_JPB.CheckedChanged += new System.EventHandler(this.RadiobtnCheckedListener);
                // 
                // rbtnFL_Driver
                // 
                this.rbtnFL_Driver.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.rbtnFL_Driver.isBusinessItemName = "";
                this.rbtnFL_Driver.isMandatory = false;
                this.rbtnFL_Driver.Location = new System.Drawing.Point(135, 50);
                this.rbtnFL_Driver.Name = "rbtnFL_Driver";
                this.rbtnFL_Driver.Size = new System.Drawing.Size(70, 17);
                this.rbtnFL_Driver.TabIndex = 8;
                this.rbtnFL_Driver.Text = "No Driver";
                this.rbtnFL_Driver.CheckedChanged += new System.EventHandler(this.RadiobtnCheckedListener);
                // 
                // cboFL_FLNo
                // 
                this.cboFL_FLNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboFL_FLNo.isBusinessItemName = "F/L No";
                this.cboFL_FLNo.isMandatory = true;
                this.cboFL_FLNo.Location = new System.Drawing.Point(39, 3);
                this.cboFL_FLNo.Name = "cboFL_FLNo";
                this.cboFL_FLNo.Size = new System.Drawing.Size(137, 19);
                this.cboFL_FLNo.TabIndex = 1;
                this.cboFL_FLNo.SelectedIndexChanged += new System.EventHandler(this.cboFL_FLNo_SelectedIndexChanged);
                // 
                // lblFL_FLNo
                // 
                this.lblFL_FLNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.lblFL_FLNo.Location = new System.Drawing.Point(0, 6);
                this.lblFL_FLNo.Name = "lblFL_FLNo";
                this.lblFL_FLNo.Size = new System.Drawing.Size(36, 16);
                this.lblFL_FLNo.Text = "F/L No";
                // 
                // tLabel14
                // 
                this.tLabel14.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel14.Location = new System.Drawing.Point(97, 92);
                this.tLabel14.Name = "tLabel14";
                this.tLabel14.Size = new System.Drawing.Size(53, 16);
                this.tLabel14.Text = "Requestor";
                // 
                // tLabel15
                // 
                this.tLabel15.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel15.Location = new System.Drawing.Point(169, 129);
                this.tLabel15.Name = "tLabel15";
                this.tLabel15.Size = new System.Drawing.Size(41, 12);
                this.tLabel15.Text = "D.Mode";
                // user want to add APFP for forklift issue 46960 10/6/2014 lvdat
                // 
                // cboFL_HatchDir
                // 
                this.cboFL_HatchDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboFL_HatchDir.isBusinessItemName = "AP/FP";
                this.cboFL_HatchDir.isMandatory = false;
                this.cboFL_HatchDir.Location = new System.Drawing.Point(189, 26);
                this.cboFL_HatchDir.Name = "cboFL_HatchDir";
                this.cboFL_HatchDir.Size = new System.Drawing.Size(39, 19);
                this.cboFL_HatchDir.TabIndex = 16;
                //this.cboFL_HatchDir.Visible = false;
                //
                // btnFL_RefNo
                //
                this.btnFL_RefNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnFL_RefNo.Location = new System.Drawing.Point(197, 168);
                this.btnFL_RefNo.Name = "btnFL_RefNo";
                this.btnFL_RefNo.Size = new System.Drawing.Size(30, 36);
                this.btnFL_RefNo.TabIndex = 14;
                this.btnFL_RefNo.Text = "Rf.No";
                this.btnFL_RefNo.Click += new System.EventHandler(this.ActionListener);
                //
                // pnlFL_RefNo
                //
                this.pnlFL_RefNo.Visible = false;
                this.pnlFL_RefNo.rbtn_RefTxt.Checked = true;
                this.pnlFL_RefNo.Location = new Point(this.btnFL_RefNo.Left - this.pnlFL_RefNo.Width, this.btnFL_RefNo.Top - this.pnlFL_RefNo.Height + this.btnFL_RefNo.Height);
                //
                // btnFL_Remark
                //
                this.btnFL_Remark.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnFL_Remark.Location = new System.Drawing.Point(169, 110);
                this.btnFL_Remark.Name = "btnFL_Remark";
                this.btnFL_Remark.Size = new System.Drawing.Size(50, 20);
                this.btnFL_Remark.TabIndex = 18;
                this.btnFL_Remark.Text = "Remark";
                this.btnFL_Remark.Click += new System.EventHandler(this.ActionListener);
                //
                // pnlFL_Remark
                //
                this.pnlFL_Remark.Visible = false;
                this.pnlFL_Remark.Location = new Point(this.btnFL_Remark.Left - this.pnlFL_Remark.Width, this.btnFL_Remark.Top - this.pnlFL_Remark.Height + this.btnFL_Remark.Height);

                #endregion

                this.isLoadFL = true;
            }
            else if (this.tabMEQ.Name.Equals(tabName))
            {
                if (this.isLoadME)
                {
                    return;
                }

                #region Mech EQ Tab Initialize
                // 
                // cboEQ_Purpose
                // 
                this.cboEQ_Purpose.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboEQ_Purpose.isBusinessItemName = "Purpose";
                this.cboEQ_Purpose.isMandatory = true;
                this.cboEQ_Purpose.Location = new System.Drawing.Point(161, 1);
                this.cboEQ_Purpose.Name = "cboEQ_Purpose";
                this.cboEQ_Purpose.Size = new System.Drawing.Size(70, 19);
                this.cboEQ_Purpose.TabIndex = 48;
                // 
                // tLabel38
                // 
                this.tLabel38.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel38.Location = new System.Drawing.Point(133, 2);
                this.tLabel38.Name = "tLabel38";
                this.tLabel38.Size = new System.Drawing.Size(30, 16);
                this.tLabel38.Text = "Purp.";
                // 
                // txtEQ_Requester
                // 
                this.txtEQ_Requester.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.txtEQ_Requester.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtEQ_Requester.isBusinessItemName = "Requester";
                this.txtEQ_Requester.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtEQ_Requester.isMandatory = true;
                this.txtEQ_Requester.Location = new System.Drawing.Point(149, 112);
                this.txtEQ_Requester.Multiline = true;
                this.txtEQ_Requester.Name = "txtEQ_Requester";
                this.txtEQ_Requester.ReadOnly = false;
                this.txtEQ_Requester.Size = new System.Drawing.Size(45, 17);
                this.txtEQ_Requester.TabIndex = 36;
                // 
                // btnEQ_F3
                // 
                this.btnEQ_F3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnEQ_F3.Location = new System.Drawing.Point(196, 111);
                this.btnEQ_F3.Name = "btnEQ_F3";
                this.btnEQ_F3.Size = new System.Drawing.Size(33, 18);
                this.btnEQ_F3.TabIndex = 37;
                this.btnEQ_F3.Text = "F3";
                this.btnEQ_F3.Click += new System.EventHandler(this.ActionListener);
                // 
                // txtEQ_WO
                // 
                this.txtEQ_WO.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtEQ_WO.isBusinessItemName = "W/O No";
                this.txtEQ_WO.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtEQ_WO.Location = new System.Drawing.Point(43, 3);
                this.txtEQ_WO.Multiline = true;
                this.txtEQ_WO.Name = "txtEQ_WO";
                this.txtEQ_WO.Size = new System.Drawing.Size(89, 17);
                this.txtEQ_WO.TabIndex = 1;
                // 
                // txtEQ_Cnrt
                // 
                this.txtEQ_Cnrt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.txtEQ_Cnrt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtEQ_Cnrt.isBusinessItemName = "Contractor";
                this.txtEQ_Cnrt.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtEQ_Cnrt.Location = new System.Drawing.Point(81, 68);
                this.txtEQ_Cnrt.Multiline = true;
                this.txtEQ_Cnrt.Name = "txtEQ_Cnrt";
                this.txtEQ_Cnrt.ReadOnly = false;
                this.txtEQ_Cnrt.Size = new System.Drawing.Size(82, 17);
                this.txtEQ_Cnrt.TabIndex = 15;
                // 
                // tLabel12
                // 
                this.tLabel12.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel12.Location = new System.Drawing.Point(1, 139);
                this.tLabel12.Name = "tLabel12";
                this.tLabel12.Size = new System.Drawing.Size(63, 19);
                this.tLabel12.Text = "EQ Arr Time";
                // 
                // tLabel17
                // 
                this.tLabel17.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel17.Location = new System.Drawing.Point(97, 114);
                this.tLabel17.Name = "tLabel17";
                this.tLabel17.Size = new System.Drawing.Size(53, 16);
                this.tLabel17.Text = "Requestor";
                // 
                // cboEQ_CgTp
                // 
                this.cboEQ_CgTp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboEQ_CgTp.isBusinessItemName = "Cg Tp";
                this.cboEQ_CgTp.isMandatory = true;
                this.cboEQ_CgTp.Location = new System.Drawing.Point(31, 111);
                this.cboEQ_CgTp.Name = "cboEQ_CgTp";
                this.cboEQ_CgTp.Size = new System.Drawing.Size(65, 19);
                this.cboEQ_CgTp.TabIndex = 22;
                // 
                // tLabel16
                // 
                this.tLabel16.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel16.Location = new System.Drawing.Point(0, 112);
                this.tLabel16.Name = "tLabel16";
                this.tLabel16.Size = new System.Drawing.Size(33, 16);
                this.tLabel16.Text = "Cg Tp";
                // 
                // txtEQ_EqArrDt
                // 
                this.txtEQ_EqArrDt.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtEQ_EqArrDt.CustomFormat = "dd/MM/yyyy HH:mm";
                this.txtEQ_EqArrDt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtEQ_EqArrDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.txtEQ_EqArrDt.isBusinessItemName = "EQ Arr Time";
                this.txtEQ_EqArrDt.isMandatory = true;
                this.txtEQ_EqArrDt.Location = new System.Drawing.Point(65, 136);
                this.txtEQ_EqArrDt.Name = "txtEQ_EqArrDt";
                this.txtEQ_EqArrDt.Size = new System.Drawing.Size(129, 22);
                this.txtEQ_EqArrDt.TabIndex = 26;
                //this.txtEQ_EqArrDt.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 96);
                this.txtEQ_EqArrDt.Value = new System.DateTime(DateTime.Now.Ticks);
                // 
                // rbtnEQ_ShipCrew
                // 
                this.rbtnEQ_ShipCrew.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.rbtnEQ_ShipCrew.isBusinessItemName = "";
                this.rbtnEQ_ShipCrew.isMandatory = false;
                this.rbtnEQ_ShipCrew.Location = new System.Drawing.Point(5, 89);
                this.rbtnEQ_ShipCrew.Name = "rbtnEQ_ShipCrew";
                this.rbtnEQ_ShipCrew.Size = new System.Drawing.Size(77, 17);
                this.rbtnEQ_ShipCrew.TabIndex = 19;
                this.rbtnEQ_ShipCrew.Text = "Ship\'s Crew";
                this.rbtnEQ_ShipCrew.CheckedChanged += new System.EventHandler(this.RadiobtnCheckedListener);
                // 
                // cboEQ_WAreaDtl
                // 
                this.cboEQ_WAreaDtl.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboEQ_WAreaDtl.isBusinessItemName = "W.Area Detail";
                this.cboEQ_WAreaDtl.isMandatory = true;
                this.cboEQ_WAreaDtl.Location = new System.Drawing.Point(107, 43);
                this.cboEQ_WAreaDtl.Name = "cboEQ_WAreaDtl";
                this.cboEQ_WAreaDtl.Size = new System.Drawing.Size(124, 19);
                this.cboEQ_WAreaDtl.TabIndex = 11;
                this.cboEQ_WAreaDtl.DropDownStyle = ComboBoxStyle.DropDownList;
                // 
                // cboEQ_WArea
                // 
                this.cboEQ_WArea.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.cboEQ_WArea.isBusinessItemName = "W.Area";
                this.cboEQ_WArea.isMandatory = true;
                this.cboEQ_WArea.Location = new System.Drawing.Point(39, 43);
                this.cboEQ_WArea.Name = "cboEQ_WArea";
                this.cboEQ_WArea.Size = new System.Drawing.Size(68, 19);
                this.cboEQ_WArea.TabIndex = 9;
                this.cboEQ_WArea.SelectedIndexChanged += new System.EventHandler(this.WAreaSelectedIndexChangedListener);
                // 
                // tLabel2
                // 
                this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.tLabel2.Location = new System.Drawing.Point(-1, 44);
                this.tLabel2.Name = "tLabel2";
                this.tLabel2.Size = new System.Drawing.Size(39, 16);
                this.tLabel2.Text = "W.Area";
                // 
                // txtEQ_Nos
                // 
                this.txtEQ_Nos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.txtEQ_Nos.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtEQ_Nos.isBusinessItemName = "Nos";
                this.txtEQ_Nos.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtEQ_Nos.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
                this.txtEQ_Nos.isMandatory = true;
                this.txtEQ_Nos.Location = new System.Drawing.Point(183, 24);
                this.txtEQ_Nos.Multiline = true;
                this.txtEQ_Nos.Name = "txtEQ_Nos";
                this.txtEQ_Nos.Size = new System.Drawing.Size(44, 17);
                this.txtEQ_Nos.TabIndex = 7;
                this.txtEQ_Nos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
              
                // 
                // btnEQ_F1
                // 
                this.btnEQ_F1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnEQ_F1.Location = new System.Drawing.Point(107, 23);
                this.btnEQ_F1.Name = "btnEQ_F1";
                this.btnEQ_F1.Size = new System.Drawing.Size(39, 18);
                this.btnEQ_F1.TabIndex = 5;
                this.btnEQ_F1.Text = "F1";
                this.btnEQ_F1.Click += new System.EventHandler(this.ActionListener);
                // 
                // txtEQ_EQType
                // 
                this.txtEQ_EQType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.txtEQ_EQType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.txtEQ_EQType.isBusinessItemName = "EQ Type";
                this.txtEQ_EQType.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
                this.txtEQ_EQType.isMandatory = true;
                this.txtEQ_EQType.Location = new System.Drawing.Point(43, 24);
                this.txtEQ_EQType.Multiline = true;
                this.txtEQ_EQType.Name = "txtEQ_EQType";
                this.txtEQ_EQType.Size = new System.Drawing.Size(61, 17);
                this.txtEQ_EQType.TabIndex = 3;
                // 
                // txtEQ_EndTime
                // 
                this.txtEQ_EndTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtEQ_EndTime.CustomFormat = "dd/MM/yyyy HH:mm";
                this.txtEQ_EndTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtEQ_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.txtEQ_EndTime.isBusinessItemName = "End Time";
                this.txtEQ_EndTime.isMandatory = true;
                this.txtEQ_EndTime.Location = new System.Drawing.Point(65, 187);
                this.txtEQ_EndTime.Name = "txtEQ_EndTime";
                this.txtEQ_EndTime.Size = new System.Drawing.Size(129, 22);
                this.txtEQ_EndTime.TabIndex = 30;
                //this.txtEQ_EndTime.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 518);
                // 
                // txtEQ_StartTime
                // 
                this.txtEQ_StartTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtEQ_StartTime.CustomFormat = "dd/MM/yyyy HH:mm";
                this.txtEQ_StartTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
                this.txtEQ_StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.txtEQ_StartTime.isBusinessItemName = "Start Time";
                this.txtEQ_StartTime.isMandatory = true;
                this.txtEQ_StartTime.Location = new System.Drawing.Point(65, 162);
                this.txtEQ_StartTime.Name = "txtEQ_StartTime";
                this.txtEQ_StartTime.Size = new System.Drawing.Size(129, 22);
                this.txtEQ_StartTime.TabIndex = 28;
                //this.txtEQ_StartTime.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 96);
                // 
                // lblEndTime
                // 
                this.lblEndTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.lblEndTime.Location = new System.Drawing.Point(1, 190);
                this.lblEndTime.Name = "lblEndTime";
                this.lblEndTime.Size = new System.Drawing.Size(52, 19);
                this.lblEndTime.Text = "End Time";
                // 
                // lblStartTime
                // 
                this.lblStartTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.lblStartTime.Location = new System.Drawing.Point(1, 165);
                this.lblStartTime.Name = "lblStartTime";
                this.lblStartTime.Size = new System.Drawing.Size(55, 19);
                this.lblStartTime.Text = "Start Time";
                // 
                // lblEQ_EQType
                // 
                this.lblEQ_EQType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.lblEQ_EQType.Location = new System.Drawing.Point(0, 25);
                this.lblEQ_EQType.Name = "lblEQ_EQType";
                this.lblEQ_EQType.Size = new System.Drawing.Size(45, 16);
                this.lblEQ_EQType.Text = "EQ Type";
                // 
                // lblEQ_WONo
                // 
                this.lblEQ_WONo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.lblEQ_WONo.Location = new System.Drawing.Point(0, 4);
                this.lblEQ_WONo.Name = "lblEQ_WONo";
                this.lblEQ_WONo.Size = new System.Drawing.Size(43, 16);
                this.lblEQ_WONo.Text = "W/O No";
                // 
                // btnEQ_F2
                // 
                this.btnEQ_F2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnEQ_F2.Location = new System.Drawing.Point(169, 67);
                this.btnEQ_F2.Name = "btnEQ_F2";
                this.btnEQ_F2.Size = new System.Drawing.Size(39, 18);
                this.btnEQ_F2.TabIndex = 17;
                this.btnEQ_F2.Text = "F2";
                this.btnEQ_F2.Click += new System.EventHandler(this.ActionListener);
                // 
                // lblEQ_Hatch
                // 
                this.lblEQ_Hatch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.lblEQ_Hatch.Location = new System.Drawing.Point(156, 25);
                this.lblEQ_Hatch.Name = "lblEQ_Hatch";
                this.lblEQ_Hatch.Size = new System.Drawing.Size(26, 15);
                this.lblEQ_Hatch.Text = "Nos.";
                // 
                // rbtnEQ_Cnrt
                // 
                this.rbtnEQ_Cnrt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.rbtnEQ_Cnrt.isBusinessItemName = "";
                this.rbtnEQ_Cnrt.isMandatory = false;
                this.rbtnEQ_Cnrt.Location = new System.Drawing.Point(5, 68);
                this.rbtnEQ_Cnrt.Name = "rbtnEQ_Cnrt";
                this.rbtnEQ_Cnrt.Size = new System.Drawing.Size(74, 17);
                this.rbtnEQ_Cnrt.TabIndex = 13;
                this.rbtnEQ_Cnrt.Text = "Contractor";
                this.rbtnEQ_Cnrt.CheckedChanged += new System.EventHandler(this.RadiobtnCheckedListener);
                //
                // btnEQ_RefNo
                //
                this.btnEQ_RefNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnEQ_RefNo.Location = new System.Drawing.Point(197, 168);
                this.btnEQ_RefNo.Name = "btnEQ_RefNo";
                this.btnEQ_RefNo.Size = new System.Drawing.Size(30, 36);
                this.btnEQ_RefNo.TabIndex = 14;
                this.btnEQ_RefNo.Text = "Rf.No";
                this.btnEQ_RefNo.Click += new System.EventHandler(this.ActionListener);
                //
                // pnlME_RefNo
                //
                this.pnlME_RefNo.Visible = false;
                this.pnlME_RefNo.rbtn_RefTxt.Checked = true;
                this.pnlME_RefNo.Location = new Point(this.btnEQ_RefNo.Left - this.pnlME_RefNo.Width, this.btnEQ_RefNo.Top - this.pnlME_RefNo.Height + this.btnEQ_RefNo.Height);
                //
                // btnEQ_Remark
                //
                this.btnEQ_Remark.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
                this.btnEQ_Remark.Location = new System.Drawing.Point(169, 89);
                this.btnEQ_Remark.Name = "btnEQ_Remark";
                this.btnEQ_Remark.Size = new System.Drawing.Size(50, 20);
                this.btnEQ_Remark.TabIndex = 18;
                this.btnEQ_Remark.Text = "Remark";
                this.btnEQ_Remark.Click += new System.EventHandler(this.ActionListener);
                //
                // pnlEQ_Remark
                //
                this.pnlEQ_Remark.Visible = false;
                this.pnlEQ_Remark.Location = new Point(this.btnEQ_Remark.Left - this.pnlEQ_Remark.Width, this.btnEQ_Remark.Top - this.pnlEQ_Remark.Height + this.btnEQ_Remark.Height);

                #endregion

                this.isLoadME = true;
            }
        }

        private void FetchWArea(ComboBox cboCategory, ComboBox cboWArea)
        {
            try
            {
                ICommonProxy proxy = new CommonProxy();
                LocationCodeParm parm = new LocationCodeParm();
                ResponseInfo info;

                string category = CommonUtility.GetComboboxSelectedValue(cboCategory);
                if (AREA_HATCH.Equals(category))
                {
                    CommonUtility.SetHatchInfo(cboWArea);
                }
                else if (AREA_WHARF.Equals(category))
                {
                    parm.searchType = "BerthLoc";
                    parm.berthTp = "WRF";
                    parm.locCd = "BBT";
                    info = proxy.getLocationCodeList(parm);
                    CommonUtility.InitializeCombobox(cboWArea);
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is LocationCodeItem)
                        {
                            LocationCodeItem item = (LocationCodeItem)info.list[i];
                            cboWArea.Items.Add(new ComboboxValueDescriptionPair(item.cd, item.cdNm));
                        }
                    }
                    /*
                     * lv.dat 20130619
                     * add hatch to wharf 
                     */
                    ComboBox cboHatch = new ComboBox();
                    CommonUtility.SetHatchInfo(cboHatch);
                    for (int i = 1; i < cboHatch.Items.Count; i++)
                    {
                        cboHatch.Items[i] = cboHatch.Items[i].ToString();
                        if (cboHatch.Items[i] != null)
                            cboWArea.Items.Add(cboHatch.Items[i]);
                    }
                }
                else if (AREA_WHO.Equals(category))
                {
                    parm.searchType = "LocDef";
                    parm.locDivCd = category;
                    info = proxy.getLocationCodeList(parm);

                    CommonUtility.InitializeCombobox(cboWArea);
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is LocationCodeItem)
                        {
                            LocationCodeItem item = (LocationCodeItem)info.list[i];
                            cboWArea.Items.Add(new ComboboxValueDescriptionPair(item.cd, item.cdNm));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        private void ClearValueAtTab(int tab)
        {
            switch (tab)
            {
                case HCM119.TAB_MANPOWER:
                    cboMan_Role.SelectedIndex = -1;
                    txtMan_JPB.Text = string.Empty;
                    txtMan_JPBName.Text = string.Empty;
                    break;
                case HCM119.TAB_PORTCRANE:
                    cboPC_EQNo.SelectedIndex = -1;
                    cboPC_JPB.SelectedIndex = -1;
                    txtPC_Requester.Text = string.Empty;
                    cboPC_CgTp.SelectedIndex = -1;
                    cboPC_Purpose.SelectedIndex = -1;
                    txtPC_Capacity.Text = string.Empty;
                    txtPC_Cnrt.Text = string.Empty;
                    break;
                case HCM119.TAB_STEVEDORE:
                    txtST_Stvd.Text = string.Empty;
                    txtST_Sprr.Text = string.Empty;
                    txtST_NonTon.Text = string.Empty;
                    txtST_WArea.Text = string.Empty;
                    txtST_Requester.Text = string.Empty;
                    cboST_Purpose.SelectedIndex = -1;
                    break;
                case HCM119.TAB_FORKLIFT:
                    m_waFLResult = null;
                    cboFL_FLNo.SelectedIndex = -1;
                    cboFL_JPB.SelectedIndex = -1;
                    cboFL_CgTp.SelectedIndex = -1;
                    txtFL_Requester.Text = string.Empty;
                    cboFL_DMode.SelectedIndex = -1;
                    cboFL_Purpose.SelectedIndex = -1;
                    txtFL_Capacity.Text = string.Empty;
                    txtFL_Cnrt.Text = string.Empty;
                    txtFL_WArea.Text = string.Empty;
                    CommonUtility.SetDTPValueBlank(txtFL_EqArrDt);
                    break;
                case HCM119.TAB_TRAILER:
                    m_trCodeResult = null;
                    cboTR_WArea.SelectedIndex = -1;
                    cboTR_WAreaDtl.SelectedIndex = -1;
                    cboTR_CgTp.SelectedIndex = -1;
                    txtTR_Requester.Text = string.Empty;
                    cboTR_DMode.SelectedIndex = -1;
                    cboTR_Purpose.SelectedIndex = -1;
                    txtTR_Cntr.Text = string.Empty;
                    txtTR_EQTp.Text = string.Empty;
                    txtTR_Nos.Text = string.Empty;
                    CommonUtility.SetDTPValueBlank(txtTR_EqArrDt);
                    break;
                case HCM119.TAB_EQU:
                    m_eqCodeResult = null;
                    cboEQ_WArea.SelectedIndex = -1;
                    cboEQ_WAreaDtl.SelectedIndex = -1;
                    cboEQ_CgTp.SelectedIndex = -1;
                    txtEQ_Requester.Text = string.Empty;
                    txtEQ_Cnrt.Text = string.Empty;
                    txtEQ_WO.Text = string.Empty;
                    txtEQ_EQType.Text = string.Empty;
                    txtEQ_Nos.Text = string.Empty;
                    CommonUtility.SetDTPValueBlank(txtEQ_EqArrDt);
                    break;
            }
        }

        //lv.dat fix issue 41585 20130618

        private ArrayList getShipCrane()
        {
            ArrayList SCList = new ArrayList();

            for (int i = 1; i <= 3; i++)
            {
                Dictionary<string, string> SC = new Dictionary<string, string>();
                SC["eqGrp"] = i.ToString();
                SC["eqTpCd"] = "SR" + i.ToString();
                SC["eqFacNo"] = "SHIPCR" + i.ToString();
                SC["eqFacNm"] = "SHIP CRANE " + i.ToString();

                SCList.Add(SC);
            }

            return SCList;
        }

        public void SetRefnoInit(params HCM120001[] pnlCtrls)
        {
            try
            {
                IApronCheckerProxy proxy = new ApronCheckerProxy();
                CheckListVSRParm parm = new CheckListVSRParm();
                parm.searchType = "RefNoCombo";
                parm.workYmd = UserInfo.getInstance().Workdate;
                parm.shftId = UserInfo.getInstance().Shift;
                parm.vslCallID = m_vslCallId;
                ResponseInfo info = proxy.getVSRList(parm);

                List<string> refNoList = new List<string>();
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CheckListVSRItem)
                    {
                        CheckListVSRItem item = (CheckListVSRItem)info.list[i];
                        refNoList.Add(item.refNo);
                    }
                }

                // Display Data
                Action<object> proc = delegate(object state)
                {
                    foreach (HCM120001 ctrl in pnlCtrls)
                    {
                        ctrl.refNoList = refNoList;
                        ctrl.addToCboRefNo();
                        ctrl.addToTxtRefNo();

                        if (m_mode == Constants.MODE_ADD)
                        {
                            ctrl.rbtn_RefTxt.Checked = true;
                            ctrl.rbtn_RefTxt.Enabled = false;
                            ctrl.rbtn_RefCbo.Enabled = false;
                        }
                        else if (m_mode == Constants.MODE_UPDATE)
                        {
                            ctrl.rbtn_RefCbo.Checked = true;
                            ctrl.rbtn_RefCbo.Enabled = false;
                            ctrl.rbtn_RefTxt.Enabled = false;
                        }
                    }
                };
                if (this.InvokeRequired)
                {
                    this.Invoke(proc, true);
                }
                else
                {
                    proc.Invoke(null);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
        }

        //check if user close by x button
        private void HCM120_Closing(object sender, CancelEventArgs e)
        {
            //if (string.Equals((sender as Button).Name, @"CloseButton"))
            if (this.DialogResult == DialogResult.Cancel)
            {
                if (Constants.MODE_UPDATE.Equals(m_mode) &&
                        ((Constants.ITEM_NOTCONFIRMED.Equals(m_updItem.IsFL) ||
                            Constants.ITEM_NOTCONFIRMED.Equals(m_updItem.IsTR) ||
                            Constants.ITEM_NOTCONFIRMED.Equals(m_updItem.IsME))))
                    m_updItem.CRUD = "";
            }
        }
    }
}