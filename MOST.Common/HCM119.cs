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

namespace MOST.Common
{
    internal class CodeHold : IEquatable<CodeHold>
    {
        public CodeHold(string capaCd, string oprator, string workArea)
        {
            this.capaCd = capaCd;
            this.oprator = oprator;
            this.workArea = workArea;
        }

        public string capaCd = "";
        public string oprator = "";
        public string workArea = "";

        public bool Equals(CodeHold other)
        {
            return this.capaCd == other.capaCd &&
                   this.oprator == other.oprator &&
                   this.workArea == other.workArea;
        }
    }

    public partial class HCM119 : TForm, IPopupWindow
    {
        #region Local Variable
        // Tab page
        public const int TAB_ALL = -1;
        public const int TAB_MANPOWER = 0;
        public const int TAB_PORTCRANE = 1;
        public const int TAB_STEVEDORE = 2;
        public const int TAB_FORKLIFT = 3;
        public const int TAB_TRAILER = 4;
        public const int TAB_EQU = 5;

        // Operations {JPB, Contractor}
        private const string OPR_JPB = "JPB";
        private const string OPR_CRTR = "CTR";

        // Header
        private const string HEADER_ITEM_STATUS = "_ITEM_STATUS";
        private const string HEADER_WS_NM = "Status";
        private const string HEADER_ROLE = "Role";
        private const string HEADER_STAFF_ID = "Staff Id";
        private const string HEADER_STAFF_NM = "Staff Name";
        private const string HEADER_WAREA = "W.Area";
        private const string HEADER_APFP = "AP/FP";
        private const string HEADER_ST_TIME = "Start Time";
        private const string HEADER_ED_TIME = "End Time";
        private const string HEADER_SEQ = "_SEQ";
        private const string HEADER_JPVC = "_JPVC";
        private const string HEADER_EQ_NO = "EQ No";
        private const string HEADER_OPR = "Operator";
        private const string HEADER_WO_NO = "W/O No";
        private const string HEADER_EQ_TYPE = "EQ No";
        private const string HEADER_CAPA = "Capacity";
        private const string HEADER_NOS = "Nos.";
        private const string HEADER_CGTPNM = "Cg Type";
        private const string HEADER_REQUESTER = "Requestor";
        private const string HEADER_DELVMODE = "D.Mode";
        private const string HEADER_EQARRTIME = "Eq Arr";
        private const string HEADER_STEVEDORE = "Stevedore";
        private const string HEADER_SUPERVISOR = "Supervisor";
        private const string HEADER_NONTONNAGE = "Non Tonnage";
        private const string HEADER_PURPOSE = "_PURPOSE";
        private const string HEADER_PURPOSE_NM = "Purpose";

        private int m_prg_type;
        private string m_vslCallId = string.Empty;
        private VSRCheckListParm m_parm;
        private List<CheckListVSRItem> m_grdDataMP;   // Man power
        private List<CheckListVSRItem> m_grdDataPC;   // Port Crane
        private List<CheckListVSRItem> m_grdDataST;   // Stevedore
        private List<CheckListVSRItem> m_grdDataFL;   // Forklift
        private List<CheckListVSRItem> m_grdDataTR;   // Trailer
        private List<CheckListVSRItem> m_grdDataEQ;   // Mechanical Equipment

        // Fix issue 0032959
        private bool hideStevedoreTabOnDetail = false;
        #endregion

        public HCM119(int prgType)
        {
            m_prg_type = prgType;
            InitializeComponent();

            this.initialFormSize();
            //InitializeDataGrid();

            m_grdDataMP = new List<CheckListVSRItem>();
            m_grdDataPC = new List<CheckListVSRItem>();
            m_grdDataST = new List<CheckListVSRItem>();
            m_grdDataFL = new List<CheckListVSRItem>();
            m_grdDataTR = new List<CheckListVSRItem>();
            m_grdDataEQ = new List<CheckListVSRItem>();

            btnMan_Update.Enabled = false;
            btnMan_Delete.Enabled = false;
            btnPC_Update.Enabled = false;
            btnPC_Delete.Enabled = false;
            btnFL_Update.Enabled = false;
            btnFL_Delete.Enabled = false;
            btnTR_Update.Enabled = false;
            btnTR_Delete.Enabled = false;
            btnEQ_Update.Enabled = false;
            btnEQ_Delete.Enabled = false;
            btnSave.Enabled = false;
        }

        // Fix issue 0032959   
        public void HideStevedoreTab()
        {
            // Only hide STV tab when user select JPVC case
            hideStevedoreTabOnDetail = Constants.JPVC == m_prg_type;
            if (hideStevedoreTabOnDetail)
                tabChecklist.TabPages.RemoveAt(tabChecklist.TabPages.IndexOf(tabSTV));
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            this.m_parm = (VSRCheckListParm)parm;
            m_vslCallId = (m_prg_type == Constants.NONJPVC) ? Constants.NONCALLID : m_parm.JpvcInfo.Jpvc;
            DisplayTabs();
            F_Retrieve();
            this.ShowDialog();
            return null;
        }

        private void DisplayTabs()
        {
            /*
             *  0:tabMan    - Man power
             *  1:tabPC     - Port Crane
             *  2:tabST     - Stevedore
             *  3:tabFL     - Forklift
             *  4:tabTR     - Trailer
             *  5:tabMEQ    - Mechanical Equipment
             */
            if (m_prg_type == Constants.NONJPVC)
            {
                // Remove tab Port Crane
                tabChecklist.TabPages.RemoveAt(1);
            }
            else if (m_prg_type == Constants.JPVC)
            {
                // Remove tab Stevedore
                //tabChecklist.TabPages.RemoveAt(2);            //issue 0026198 : doesnot remove ST tab by Phuong 20110304
            }

            tabChecklist.SelectedIndex = 0;
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnMan_Add":
                    AddItem(HCM119.TAB_MANPOWER);
                    break;

                case "btnMan_Update":
                    UpdateItem(HCM119.TAB_MANPOWER);
                    break;

                case "btnMan_Delete":
                    if (DialogResult.Yes == CommonUtility.ConfirmMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0046")))
                    {
                        DeleteItem(HCM119.TAB_MANPOWER);
                    }
                    break;

                case "btnPC_Add":
                    AddItem(HCM119.TAB_PORTCRANE);
                    break;

                case "btnPC_Update":
                    UpdateItem(HCM119.TAB_PORTCRANE);
                    break;

                case "btnPC_Delete":
                    if (DialogResult.Yes == CommonUtility.ConfirmMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0046")))
                    {
                        DeleteItem(HCM119.TAB_PORTCRANE);
                    }
                    break;

                case "btnST_Add":
                    AddItem(HCM119.TAB_STEVEDORE);
                    break;

                case "btnST_Update":
                    UpdateItem(HCM119.TAB_STEVEDORE);
                    break;

                case "btnST_Delete":
                    if (DialogResult.Yes == CommonUtility.ConfirmMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0046")))
                    {
                        DeleteItem(HCM119.TAB_STEVEDORE);
                    }
                    break;

                case "btnFL_Add":
                    AddItem(HCM119.TAB_FORKLIFT);
                    break;

                case "btnFL_Update":
                    UpdateItem(HCM119.TAB_FORKLIFT);
                    break;

                case "btnFL_Delete":
                    if (DialogResult.Yes == CommonUtility.ConfirmMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0046")))
                    {
                        DeleteItem(HCM119.TAB_FORKLIFT);
                    }
                    break;

                case "btnTR_Add":
                    AddItem(HCM119.TAB_TRAILER);
                    break;

                case "btnTR_Update":
                    UpdateItem(HCM119.TAB_TRAILER);
                    break;

                case "btnTR_Delete":
                    if (DialogResult.Yes == CommonUtility.ConfirmMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0046")))
                    {
                        DeleteItem(HCM119.TAB_TRAILER);
                    }
                    break;

                case "btnEQ_Add":
                    AddItem(HCM119.TAB_EQU);
                    break;

                case "btnEQ_Update":
                    UpdateItem(HCM119.TAB_EQU);
                    break;

                case "btnEQ_Delete":
                    if (DialogResult.Yes == CommonUtility.ConfirmMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0046")))
                    {
                        DeleteItem(HCM119.TAB_EQU);
                    }
                    break;

                case "btnSave":
                    if (ProcessVSRListItem())
                    {
                        F_Retrieve();
                        CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
                    }
                    break;

                case "btnExit":
                    if (grdMan_Data.IsDirty ||
                        grdPC_Data.IsDirty ||
                        grdST_Data.IsDirty ||
                        grdFL_Data.IsDirty ||
                        grdTR_Data.IsDirty ||
                        grdEQ_Data.IsDirty)
                    {
                        DialogResult drMan = CommonUtility.ConfirmMsgSaveChances();
                        if (drMan == DialogResult.Yes)
                        {
                            ProcessVSRListItem();
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            this.Close();
                        }
                        else if (drMan == DialogResult.No)
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            this.Close();
                        }
                    }
                    else
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                        this.Close();
                    }
                    break;
            }
        }

        private void RemoveNewItems()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string itemStatus;
                List<int> deleteIndex = new List<int>();

                // Man Power
                if (grdMan_Data != null && manPowerListBindingSource.Count > 0)
                {
                    IList itemList = manPowerListBindingSource.List;
                    int j = 0;
                    foreach (VsrList itemInLst in itemList)
                    {
                        itemStatus = itemInLst.ItemStatus;
                        if (Constants.ITEM_NEW.Equals(itemStatus))
                        {
                            deleteIndex.Add(j);
                        }
                        j++;
                    }

                    deleteIndex.Reverse();
                    foreach (int index in deleteIndex)
                    {
                        manPowerListBindingSource.RemoveAt(index);
                    }

                    grdMan_Data.Refresh();
                }

                // Port Crane
                if (grdPC_Data != null && portCraneListBindingSource.Count > 0)
                {
                    IList itemList = portCraneListBindingSource.List;
                    int j = 0;
                    foreach (VsrList itemInLst in itemList)
                    {
                        itemStatus = itemInLst.ItemStatus;
                        if (Constants.ITEM_NEW.Equals(itemStatus))
                        {
                            deleteIndex.Add(j);
                        }
                        j++;
                    }

                    deleteIndex.Reverse();
                    foreach (int index in deleteIndex)
                    {
                        portCraneListBindingSource.RemoveAt(index);
                    }

                    grdPC_Data.Refresh();
                }

                // Stevedore
                if (grdST_Data != null && stevedoreListBindingSource.Count > 0)
                {
                    IList itemList = stevedoreListBindingSource.List;
                    int j = 0;
                    foreach (VsrList itemInLst in itemList)
                    {
                        itemStatus = itemInLst.ItemStatus;
                        if (Constants.ITEM_NEW.Equals(itemStatus))
                        {
                            deleteIndex.Add(j);
                        }
                        j++;
                    }

                    deleteIndex.Reverse();
                    foreach (int index in deleteIndex)
                    {
                        stevedoreListBindingSource.RemoveAt(index);
                    }
                }

                // Forklift
                if (grdFL_Data != null && forkliftListBindingSource.Count > 0)
                {
                    IList itemList = forkliftListBindingSource.List;
                    int j = 0;
                    foreach (VsrList itemInLst in itemList)
                    {
                        itemStatus = itemInLst.ItemStatus;
                        if (Constants.ITEM_NEW.Equals(itemStatus))
                        {
                            deleteIndex.Add(j);
                        }
                        j++;
                    }

                    deleteIndex.Reverse();
                    foreach (int index in deleteIndex)
                    {
                        forkliftListBindingSource.RemoveAt(index);
                    }
                }

                // Trailer
                if (grdTR_Data != null && trailerListBindingSource.Count > 0)
                {
                    IList itemList = trailerListBindingSource.List;
                    int j = 0;
                    foreach (VsrList itemInLst in itemList)
                    {
                        itemStatus = itemInLst.ItemStatus;
                        if (Constants.ITEM_NEW.Equals(itemStatus))
                        {
                            deleteIndex.Add(j);
                        }
                        j++;
                    }

                    deleteIndex.Reverse();
                    foreach (int index in deleteIndex)
                    {
                        trailerListBindingSource.RemoveAt(index);
                    }
                }

                // EQU
                if (grdEQ_Data != null && mechanicalEqListBindingSource.Count > 0)
                {
                    IList itemList = mechanicalEqListBindingSource.List;
                    int j = 0;
                    foreach (VsrList itemInLst in itemList)
                    {
                        itemStatus = itemInLst.ItemStatus;
                        if (Constants.ITEM_NEW.Equals(itemStatus))
                        {
                            deleteIndex.Add(j);
                        }
                        j++;
                    }

                    deleteIndex.Reverse();
                    foreach (int index in deleteIndex)
                    {
                        mechanicalEqListBindingSource.RemoveAt(index);
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

        /// <summary>
        /// Add items to grids.
        /// Input 1st time: arrGrdData = {old, new1} ==> add new1 to grid.
        /// Input 2nd time: arrGrdData = {old, new1, new2} ==> Remove new1 from grid. Then Add new1, new2 to grid.
        /// </summary>
        /// <param name="retResult"></param>
        private void AddItemsToGrids(VSRDetailResult retResult)
        {
            CheckListVSRItem item;

            if (retResult != null)
            {
                //////////////////////////////////////////////////////////////////////////
                // Remove all new items in data grids
                //////////////////////////////////////////////////////////////////////////
                RemoveNewItems();

                //////////////////////////////////////////////////////////////////////////
                // Returned list contains old items and new items.
                // Add all new items to grids.
                //////////////////////////////////////////////////////////////////////////
                m_grdDataMP = retResult.ArrGrdDataMP;
                m_grdDataPC = retResult.ArrGrdDataPC;
                m_grdDataST = retResult.ArrGrdDataST;
                m_grdDataFL = retResult.ArrGrdDataFL;
                m_grdDataTR = retResult.ArrGrdDataTR;
                m_grdDataEQ = retResult.ArrGrdDataEQ;

                // Manpower
                int i;
                VsrList grdItem;
                if (m_grdDataMP != null && manPowerListBindingSource.Count < m_grdDataMP.Count)
                {
                    for (i = 0; i < m_grdDataMP.Count; i++)
                    {
                        // Returned list contains old items and new items.
                        // Only add new items to the grid data.
                        item = m_grdDataMP[i];
                        if (Constants.WS_INSERT.Equals(item.CRUD))
                        {
                            //Man power
                            grdItem = new VsrList();
                            grdItem.ItemStatus = Constants.ITEM_NEW;
                            grdItem.Status = Constants.WS_NM_INSERT;
                            grdItem.Role = item.rsNm;
                            grdItem.StaffId = item.empId;
                            grdItem.StaffName = item.empNm;
                            grdItem.WArea = item.workLoc;
                            grdItem.StartTime = item.workStDt;
                            grdItem.EndTime = item.workEndDt;
                            grdItem.SEQ = item.seq.ToString();
                            grdItem.JPVC = item.vslCallID;

                            manPowerListBindingSource.Add(grdItem);
                        }
                    }
                }

                // PortCrane
                if (m_grdDataPC != null && portCraneListBindingSource.Count < m_grdDataPC.Count)
                {
                    for (i = 0; i < m_grdDataPC.Count; i++)
                    {
                        item = m_grdDataPC[i];
                        if (Constants.WS_INSERT.Equals(item.CRUD))
                        {
                            //Port Crane
                            grdItem = new VsrList();
                            grdItem.ItemStatus = Constants.ITEM_NEW;
                            grdItem.Status = Constants.WS_NM_INSERT;
                            grdItem.EqNo = item.rsNm;
                            grdItem.Capacity = item.capaDescr;

                            if (HCM119.OPR_JPB.Equals(item.mbsCd))
                            {
                                grdItem.OperatorVsr = item.empId;
                            }
                            else if (HCM119.OPR_CRTR.Equals(item.mbsCd))
                            {
                                grdItem.OperatorVsr = item.cnrtCd;
                            }

                            grdItem.Requestor = item.payer;
                            grdItem.CgType = item.cgTpNm;
                            grdItem.Purpose = item.purpose;
                            grdItem.PurposeNm = item.purposeNm;
                            grdItem.StartTime = item.workStDt;
                            grdItem.EndTime = item.workEndDt;
                            grdItem.SEQ = item.seq.ToString();
                            grdItem.JPVC = item.vslCallID;

                            this.portCraneListBindingSource.Add(grdItem);
                            this.grdPC_Data.DataSource = portCraneListBindingSource;
                        }
                    }
                }

                // Stevedore
                if (m_grdDataST != null && stevedoreListBindingSource.Count < m_grdDataST.Count)
                {

                    for (i = 0; i < m_grdDataST.Count; i++)
                    {
                        item = m_grdDataST[i];
                        if (Constants.WS_INSERT.Equals(item.CRUD))
                        {
                            //Stevedore
                            grdItem = new VsrList();
                            grdItem.ItemStatus = Constants.ITEM_NEW;
                            grdItem.Status = Constants.WS_NM_INSERT;
                            grdItem.Stevedore = item.stvdComp;
                            grdItem.Supervisor = item.nofStvdSprr;
                            grdItem.NonTonnage = item.stvdNonTon;
                            grdItem.Requestor = item.payer;
                            grdItem.WArea = item.workLoc;
                            grdItem.Purpose = item.purpose;
                            grdItem.PurposeNm = item.purposeNm;
                            grdItem.StartTime = item.workStDt;
                            grdItem.EndTime = item.workEndDt;
                            grdItem.SEQ = item.seq.ToString();
                            grdItem.JPVC = item.purposeNm;

                            this.stevedoreListBindingSource.Add(grdItem);
                            this.grdST_Data.DataSource = stevedoreListBindingSource;
                        }
                    }
                }

                // Forklift
                if (m_grdDataFL != null && forkliftListBindingSource.Count < m_grdDataFL.Count)
                {
                    for (i = 0; i < m_grdDataFL.Count; i++)
                    {
                        item = m_grdDataFL[i];
                        if (Constants.WS_INSERT.Equals(item.CRUD))
                        {
                            //Forklift
                            grdItem = new VsrList();
                            grdItem.ItemStatus = Constants.ITEM_NEW;
                            grdItem.Status = Constants.WS_NM_INSERT;
                            grdItem.EqNo = item.engNm;
                            grdItem.Capacity = item.capaDescr;

                            if (HCM119.OPR_JPB.Equals(item.mbsCd))
                            {
                                grdItem.OperatorVsr = item.empId;
                            }
                            else if (HCM119.OPR_CRTR.Equals(item.mbsCd))
                            {
                                grdItem.OperatorVsr = item.cnrtCd;
                            }

                            grdItem.WArea = item.workLoc;
                            grdItem.StartTime = item.workStDt;
                            grdItem.EndTime = item.workEndDt;
                            grdItem.SEQ = item.seq.ToString();
                            grdItem.JPVC = item.vslCallID;
                            grdItem.CgType = item.cgTpNm;
                            grdItem.Requestor = item.payer;
                            grdItem.Purpose = item.purpose;
                            grdItem.PurposeNm = item.purposeNm;
                            grdItem.DevlMode = item.delvTpNm;
                            grdItem.eqArr = item.setupTime;
                            //add by lv.dat 
                            grdItem.RefNo = item.refNo;

                            this.forkliftListBindingSource.Add(grdItem);
                            this.grdFL_Data.DataSource = forkliftListBindingSource;
                        }
                    }
                }

                // Trailer
                if (m_grdDataTR != null && trailerListBindingSource.Count < m_grdDataTR.Count)
                {
                    for (i = 0; i < m_grdDataTR.Count; i++)
                    {
                        item = m_grdDataTR[i];
                        if (Constants.WS_INSERT.Equals(item.CRUD))
                        {//Trailer
                            grdItem = new VsrList();
                            grdItem.ItemStatus = Constants.ITEM_NEW;
                            grdItem.Status = Constants.WS_NM_INSERT;
                            grdItem.EqNo = item.rsNm;
                            grdItem.Capacity = item.capaDescr;
                            grdItem.Nos = item.rsQty;
                            grdItem.WArea = item.workLoc;
                            grdItem.ApFp = item.hatchDir;
                            grdItem.OperatorVsr = item.cnrtCd;
                            grdItem.StartTime = item.workStDt;
                            grdItem.EndTime = item.workEndDt;
                            grdItem.SEQ = item.seq.ToString();
                            grdItem.JPVC = item.vslCallID;
                            grdItem.CgType = item.cgTpNm;
                            grdItem.Requestor = item.payer;
                            grdItem.Purpose = item.purpose;
                            grdItem.PurposeNm = item.purposeNm;
                            grdItem.DevlMode = item.delvTpNm;
                            grdItem.eqArr = item.setupTime;

                            this.trailerListBindingSource.Add(grdItem);
                            this.grdTR_Data.DataSource = trailerListBindingSource;
                        }
                    }
                }

                // EQU
                if (m_grdDataEQ != null && mechanicalEqListBindingSource.Count < m_grdDataEQ.Count)
                {
                    for (i = 0; i < m_grdDataEQ.Count; i++)
                    {
                        item = m_grdDataEQ[i];
                        if (Constants.WS_INSERT.Equals(item.CRUD))
                        {
                            //Mech eq
                            grdItem = new VsrList();
                            grdItem.ItemStatus = Constants.ITEM_NEW;
                            grdItem.Status = Constants.WS_NM_INSERT;
                            grdItem.WoNo = item.workOdrNo;
                            grdItem.EqNo = item.rsNm;
                            grdItem.Capacity = item.capaDescr;
                            grdItem.Nos = item.rsQty;
                            grdItem.WArea = item.workLoc;

                            if ("Y".Equals(item.shpCrew))
                            {
                                grdItem.OperatorVsr = "Ship's Crew";
                            }
                            else
                            {
                                grdItem.OperatorVsr = item.cnrtCd;
                            }

                            grdItem.Purpose = item.purpose;
                            grdItem.PurposeNm = item.purposeNm;
                            grdItem.StartTime = item.workStDt;
                            grdItem.EndTime = item.workEndDt;
                            grdItem.SEQ = item.seq.ToString();
                            grdItem.JPVC = item.vslCallID;
                            grdItem.CgType = item.cgTpNm;
                            grdItem.Requestor = item.payer;
                            grdItem.eqArr = item.setupTime;

                            this.mechanicalEqListBindingSource.Add(grdItem);
                            this.grdEQ_Data.DataSource = mechanicalEqListBindingSource;
                        }
                    }
                }
            }
        }

        private void AddItem(int tab)
        {
            VSRDetailResult retResult = null;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                HCM120 frm = new HCM120(m_prg_type, Constants.MODE_ADD);
                if (hideStevedoreTabOnDetail)
                    frm.HideStevedoreTab();
                frm.ActiveTab = tab;
                switch (tab)
                {
                    case HCM119.TAB_MANPOWER:
                        VSRDetailParm manParm = new VSRDetailParm();
                        manParm.VslCallId = m_vslCallId;
                        manParm.ArrGrdDataMP = m_grdDataMP;
                        manParm.ArrGrdDataPC = m_grdDataPC;
                        manParm.ArrGrdDataST = m_grdDataST;
                        manParm.ArrGrdDataFL = m_grdDataFL;
                        manParm.ArrGrdDataTR = m_grdDataTR;
                        manParm.ArrGrdDataEQ = m_grdDataEQ;
                        //frm.ActiveTab = HCM119.TAB_MANPOWER;
                        VSRDetailResult manResult = (VSRDetailResult)PopupManager.instance.ShowPopup(frm, manParm);
                        if (manResult != null)
                        {
                            retResult = manResult;
                        }
                        break;

                    case HCM119.TAB_PORTCRANE:
                        VSRDetailParm pcParm = new VSRDetailParm();
                        pcParm.VslCallId = m_vslCallId;
                        pcParm.ArrGrdDataMP = m_grdDataMP;
                        pcParm.ArrGrdDataPC = m_grdDataPC;
                        pcParm.ArrGrdDataST = m_grdDataST;
                        pcParm.ArrGrdDataFL = m_grdDataFL;
                        pcParm.ArrGrdDataTR = m_grdDataTR;
                        pcParm.ArrGrdDataEQ = m_grdDataEQ;
                        //frm.ActiveTab = HCM119.TAB_PORTCRANE;
                        VSRDetailResult pcResult = (VSRDetailResult)PopupManager.instance.ShowPopup(frm, pcParm);
                        if (pcResult != null)
                        {
                            retResult = pcResult;
                        }
                        break;

                    case HCM119.TAB_STEVEDORE:
                        VSRDetailParm stvParm = new VSRDetailParm();
                        stvParm.VslCallId = m_vslCallId;
                        stvParm.ArrGrdDataMP = m_grdDataMP;
                        stvParm.ArrGrdDataPC = m_grdDataPC;
                        stvParm.ArrGrdDataST = m_grdDataST;
                        stvParm.ArrGrdDataFL = m_grdDataFL;
                        stvParm.ArrGrdDataTR = m_grdDataTR;
                        stvParm.ArrGrdDataEQ = m_grdDataEQ;
                        //frm.ActiveTab = HCM119.TAB_STEVEDORE;
                        VSRDetailResult stvResult = (VSRDetailResult)PopupManager.instance.ShowPopup(frm, stvParm);
                        if (stvResult != null)
                        {
                            retResult = stvResult;
                        }
                        break;

                    case HCM119.TAB_FORKLIFT:
                        VSRDetailParm flParm = new VSRDetailParm();
                        flParm.VslCallId = m_vslCallId;
                        flParm.ArrGrdDataMP = m_grdDataMP;
                        flParm.ArrGrdDataPC = m_grdDataPC;
                        flParm.ArrGrdDataST = m_grdDataST;
                        flParm.ArrGrdDataFL = m_grdDataFL;
                        flParm.ArrGrdDataTR = m_grdDataTR;
                        flParm.ArrGrdDataEQ = m_grdDataEQ;
                        //frm.ActiveTab = HCM119.TAB_FORKLIFT;
                        VSRDetailResult flResult = (VSRDetailResult)PopupManager.instance.ShowPopup(frm, flParm);
                        if (flResult != null)
                        {
                            retResult = flResult;
                        }
                        break;

                    case HCM119.TAB_TRAILER:
                        VSRDetailParm trParm = new VSRDetailParm();
                        trParm.VslCallId = m_vslCallId;
                        trParm.ArrGrdDataMP = m_grdDataMP;
                        trParm.ArrGrdDataPC = m_grdDataPC;
                        trParm.ArrGrdDataST = m_grdDataST;
                        trParm.ArrGrdDataFL = m_grdDataFL;
                        trParm.ArrGrdDataTR = m_grdDataTR;
                        trParm.ArrGrdDataEQ = m_grdDataEQ;
                        //frm.ActiveTab = HCM119.TAB_TRAILER;
                        VSRDetailResult trResult = (VSRDetailResult)PopupManager.instance.ShowPopup(frm, trParm);
                        if (trResult != null)
                        {
                            retResult = trResult;
                        }
                        break;

                    case HCM119.TAB_EQU:
                        VSRDetailParm eqParm = new VSRDetailParm();
                        eqParm.VslCallId = m_vslCallId;
                        eqParm.ArrGrdDataMP = m_grdDataMP;
                        eqParm.ArrGrdDataPC = m_grdDataPC;
                        eqParm.ArrGrdDataST = m_grdDataST;
                        eqParm.ArrGrdDataFL = m_grdDataFL;
                        eqParm.ArrGrdDataTR = m_grdDataTR;
                        eqParm.ArrGrdDataEQ = m_grdDataEQ;
                        //frm.ActiveTab = HCM119.TAB_EQU;
                        VSRDetailResult eqResult = (VSRDetailResult)PopupManager.instance.ShowPopup(frm, eqParm);
                        if (eqResult != null)
                        {
                            retResult = eqResult;
                        }
                        break;
                }

                if (retResult != null)
                {
                    AddItemsToGrids(retResult);
                    if (ProcessVSRListItem())
                    {
                        F_Retrieve();
                        if(frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
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

        private void UpdateItem(int tab)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                HCM120 frm = new HCM120(m_prg_type, Constants.MODE_UPDATE);
                if (hideStevedoreTabOnDetail)
                    frm.HideStevedoreTab();
                frm.ActiveTab = tab;
                int currRowIndex;
                switch (tab)
                {
                    case HCM119.TAB_MANPOWER:
                        currRowIndex = grdMan_Data.CurrentRowIndex;
                        if (-1 < currRowIndex && currRowIndex < manPowerListBindingSource.Count)
                        {
                            VSRDetailParm manParm = new VSRDetailParm();
                            manParm.VslCallId = m_vslCallId;
                            manParm.CurrIndex = currRowIndex;
                            manParm.ArrUpdGrdData = m_grdDataMP;
                            //frm.ActiveTab = HCM119.TAB_MANPOWER;
                            VSRDetailResult manResult = (VSRDetailResult)PopupManager.instance.ShowPopup(frm, manParm);

                            if (manResult != null && manResult.UpdVsrItem != null)
                            {
                                CheckListVSRItem item = manResult.UpdVsrItem;
                                m_grdDataMP[currRowIndex] = item;

                                IList itemList = manPowerListBindingSource.List;
                                int j = 0;
                                foreach (VsrList itemInLst in itemList)
                                {
                                    if (j == currRowIndex)
                                    {
                                        //manPowerListBindingSource
                                        if (Constants.ITEM_OLD.Equals(itemInLst.ItemStatus.ToString()))
                                        {
                                            itemInLst.Status = Constants.WS_NM_UPDATE;
                                        }

                                        itemInLst.Role = item.rsNm;
                                        itemInLst.StaffId = item.empId;
                                        itemInLst.StaffName = item.empNm;
                                        itemInLst.WArea = item.workLoc;
                                        itemInLst.StartTime = item.workStDt;
                                        itemInLst.EndTime = item.workEndDt;

                                        grdMan_Data.DataSource = manPowerListBindingSource;
                                        break;
                                    }
                                    j++;
                                }
                            }
                        }
                        break;

                    case HCM119.TAB_PORTCRANE:
                        currRowIndex = grdPC_Data.CurrentRowIndex;
                        VSRDetailParm pcParm = new VSRDetailParm();
                        pcParm.VslCallId = m_vslCallId;
                        pcParm.CurrIndex = currRowIndex;
                        pcParm.ArrUpdGrdData = m_grdDataPC;
                        //frm.ActiveTab = HCM119.TAB_PORTCRANE;
                        VSRDetailResult pcResult = (VSRDetailResult)PopupManager.instance.ShowPopup(frm, pcParm);

                        if (pcResult != null && pcResult.UpdVsrItem != null)
                        {
                            CheckListVSRItem item = pcResult.UpdVsrItem;
                            m_grdDataPC[currRowIndex] = item;

                            IList itemList = portCraneListBindingSource.List;
                            int j = 0;
                            String strOperator = String.Empty;

                            foreach (VsrList itemInLst in itemList)
                            {
                                if (j == currRowIndex)
                                {

                                    //manPowerListBindingSource
                                    if (Constants.ITEM_OLD.Equals(itemInLst.ItemStatus.ToString()))
                                    {
                                        itemInLst.Status = Constants.WS_NM_UPDATE;
                                    }

                                    strOperator = ("JPB".Equals(item.mbsCd)) ? item.empId : item.cnrtCd;

                                    //Port Crane
                                    itemInLst.EqNo = item.rsNm;
                                    itemInLst.Capacity = item.capaDescr;
                                    itemInLst.OperatorVsr = strOperator;
                                    itemInLst.Requestor = item.payer;
                                    itemInLst.CgType = item.cgTpNm;
                                    itemInLst.Purpose = item.purpose;
                                    itemInLst.PurposeNm = item.purposeNm;
                                    itemInLst.StartTime = item.workStDt;
                                    itemInLst.EndTime = item.workEndDt;

                                    this.grdPC_Data.DataSource = portCraneListBindingSource;
                                    break;
                                }
                                j++;
                            }
                        }
                        break;

                    case HCM119.TAB_STEVEDORE:
                        currRowIndex = grdST_Data.CurrentRowIndex;
                        VSRDetailParm stvParm = new VSRDetailParm();
                        stvParm.VslCallId = m_vslCallId;
                        stvParm.CurrIndex = currRowIndex;
                        stvParm.ArrUpdGrdData = m_grdDataST;
                        //frm.ActiveTab = HCM119.TAB_STEVEDORE;
                        VSRDetailResult stvResult = (VSRDetailResult)PopupManager.instance.ShowPopup(frm, stvParm);

                        if (stvResult != null && stvResult.UpdVsrItem != null)
                        {
                            CheckListVSRItem item = stvResult.UpdVsrItem;
                            m_grdDataST[currRowIndex] = item;

                            IList itemList = stevedoreListBindingSource.List;
                            int j = 0;

                            foreach (VsrList itemInLst in itemList)
                            {
                                if (j == currRowIndex)
                                {

                                    //manPowerListBindingSource
                                    if (Constants.ITEM_OLD.Equals(itemInLst.ItemStatus.ToString()))
                                    {
                                        itemInLst.Status = Constants.WS_NM_UPDATE;
                                    }

                                    //Stevedore
                                    itemInLst.Stevedore = item.stvdComp;
                                    itemInLst.Supervisor = item.nofStvdSprr;
                                    itemInLst.NonTonnage = item.stvdNonTon;
                                    itemInLst.Requestor = item.payer;
                                    itemInLst.WArea = item.workLoc;
                                    itemInLst.Purpose = item.purpose;
                                    itemInLst.PurposeNm = item.purposeNm;
                                    itemInLst.StartTime = item.workStDt;
                                    itemInLst.EndTime = item.workEndDt;

                                    this.grdST_Data.DataSource = stevedoreListBindingSource;
                                    break;
                                }
                                j++;
                            }
                        }
                        break;

                    case HCM119.TAB_FORKLIFT:
                        currRowIndex = grdFL_Data.CurrentRowIndex;
                        VSRDetailParm flParm = new VSRDetailParm();
                        flParm.VslCallId = m_vslCallId;
                        flParm.CurrIndex = currRowIndex;
                        flParm.ArrUpdGrdData = m_grdDataFL;
                        //frm.ActiveTab = HCM119.TAB_FORKLIFT;
                        VSRDetailResult flResult = (VSRDetailResult)PopupManager.instance.ShowPopup(frm, flParm);
                        if (flResult != null && flResult.UpdVsrItem != null)
                        {
                            CheckListVSRItem item = flResult.UpdVsrItem;
                            m_grdDataFL[currRowIndex] = item;

                            IList itemList = forkliftListBindingSource.List;
                            int j = 0;
                            String strOperator = String.Empty;
                            foreach (VsrList itemInLst in itemList)
                            {
                                if (j == currRowIndex)
                                {

                                    //manPowerListBindingSource
                                    if (Constants.ITEM_OLD.Equals(itemInLst.ItemStatus.ToString()))
                                    {
                                        itemInLst.Status = Constants.WS_NM_UPDATE;
                                    }
                                    if (Constants.ITEM_MEGA.Equals(itemInLst.ItemStatus.ToString()))
                                    {
                                        itemInLst.Status = Constants.ITEM_CONFIRMED;
                                        itemInLst.ItemStatus = Constants.ITEM_OLD;
                                    }

                                    strOperator = ("JPB".Equals(item.mbsCd)) ? item.empId : item.cnrtCd;

                                    //Forklift
                                    itemInLst.EqNo = item.engNm;
                                    itemInLst.Capacity = item.capaDescr;
                                    itemInLst.OperatorVsr = strOperator;
                                    itemInLst.WArea = item.workLoc;
                                    itemInLst.StartTime = item.workStDt;
                                    itemInLst.EndTime = item.workEndDt;
                                    itemInLst.CgType = item.cgTpNm;
                                    itemInLst.Requestor = item.payer;
                                    itemInLst.Purpose = item.purpose;
                                    itemInLst.PurposeNm = item.purposeNm;
                                    itemInLst.DevlMode = item.delvTpNm;
                                    itemInLst.eqArr = item.setupTime;
                                    itemInLst.RefNo = item.refNo;

                                    this.grdFL_Data.DataSource = forkliftListBindingSource;
                                    break;
                                }
                                j++;
                            }
                        }
                        break;

                    case HCM119.TAB_TRAILER:
                        currRowIndex = grdTR_Data.CurrentRowIndex;
                        VSRDetailParm trParm = new VSRDetailParm();
                        trParm.VslCallId = m_vslCallId;
                        trParm.CurrIndex = currRowIndex;
                        trParm.ArrUpdGrdData = m_grdDataTR;
                        //frm.ActiveTab = HCM119.TAB_TRAILER;
                        VSRDetailResult trResult = (VSRDetailResult)PopupManager.instance.ShowPopup(frm, trParm);
                        if (trResult != null && trResult.UpdVsrItem != null)
                        {
                            CheckListVSRItem item = trResult.UpdVsrItem;
                            m_grdDataTR[currRowIndex] = item;

                            IList itemList = trailerListBindingSource.List;
                            int j = 0;
                            foreach (VsrList itemInLst in itemList)
                            {
                                if (j == currRowIndex)
                                {

                                    //manPowerListBindingSource
                                    if (Constants.ITEM_OLD.Equals(itemInLst.ItemStatus.ToString()))
                                    {
                                        itemInLst.Status = Constants.WS_NM_UPDATE;
                                    }
                                    if (Constants.ITEM_MEGA.Equals(itemInLst.ItemStatus.ToString()))
                                    {
                                        itemInLst.Status = Constants.ITEM_CONFIRMED;
                                        itemInLst.ItemStatus = Constants.ITEM_OLD;
                                    }

                                    //Trailer
                                    itemInLst.EqNo = item.rsNm;
                                    itemInLst.Capacity = item.capaDescr;
                                    itemInLst.Nos = item.rsQty;
                                    itemInLst.WArea = item.workLoc;
                                    itemInLst.ApFp = item.hatchDir;
                                    itemInLst.OperatorVsr = item.cnrtCd;
                                    itemInLst.StartTime = item.workStDt;
                                    itemInLst.EndTime = item.workEndDt;
                                    itemInLst.CgType = item.cgTpNm;
                                    itemInLst.Requestor = item.payer;
                                    itemInLst.Purpose = item.purpose;
                                    itemInLst.PurposeNm = item.purposeNm;
                                    itemInLst.DevlMode = item.delvTpNm;
                                    itemInLst.eqArr = item.setupTime;
                                    itemInLst.RefNo = item.refNo;

                                    this.grdTR_Data.DataSource = trailerListBindingSource;
                                    break;
                                }
                                j++;
                            }
                        }
                        break;

                    case HCM119.TAB_EQU:
                        currRowIndex = grdEQ_Data.CurrentRowIndex;
                        VSRDetailParm eqParm = new VSRDetailParm();
                        eqParm.VslCallId = m_vslCallId;
                        eqParm.CurrIndex = currRowIndex;
                        eqParm.ArrUpdGrdData = m_grdDataEQ;
                        //frm.ActiveTab = HCM119.TAB_EQU;
                        VSRDetailResult eqResult = (VSRDetailResult)PopupManager.instance.ShowPopup(frm, eqParm);
                        if (eqResult != null && eqResult.UpdVsrItem != null)
                        {
                            CheckListVSRItem item = eqResult.UpdVsrItem;
                            m_grdDataEQ[currRowIndex] = item;

                            IList itemList = mechanicalEqListBindingSource.List;
                            int j = 0;
                            foreach (VsrList itemInLst in itemList)
                            {
                                if (j == currRowIndex)
                                {

                                    //manPowerListBindingSource
                                    if (Constants.ITEM_OLD.Equals(itemInLst.ItemStatus.ToString()))
                                    {
                                        itemInLst.Status = Constants.WS_NM_UPDATE;
                                    }
                                    if (Constants.ITEM_MEGA.Equals(itemInLst.ItemStatus.ToString()))
                                    {
                                        itemInLst.Status = Constants.ITEM_CONFIRMED;
                                        itemInLst.ItemStatus = Constants.ITEM_OLD;
                                    }

                                    //Mech EQ
                                    itemInLst.WoNo = item.workOdrNo;
                                    itemInLst.EqNo = item.rsNm;
                                    itemInLst.Capacity = item.capaDescr;
                                    itemInLst.Nos = item.rsQty;
                                    itemInLst.WArea = item.workLoc;
                                    itemInLst.OperatorVsr = item.cnrtCd;
                                    itemInLst.Purpose = item.purpose;
                                    itemInLst.PurposeNm = item.purposeNm;
                                    itemInLst.StartTime = item.workStDt;
                                    itemInLst.EndTime = item.workEndDt;
                                    itemInLst.CgType = item.cgTpNm;
                                    itemInLst.Requestor = item.payer;
                                    itemInLst.eqArr = item.setupTime;
                                    itemInLst.RefNo = item.refNo;

                                    this.grdEQ_Data.DataSource = mechanicalEqListBindingSource;
                                    break;
                                }
                                j++;
                            }
                        }
                        break;
                }

                if (ProcessVSRListItem())
                {
                    F_Retrieve();
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
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
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                IApronCheckerProxy proxy = new ApronCheckerProxy();
                CheckListVSRParm parm = new CheckListVSRParm();
                //parm.searchType = "craneList";
                parm.searchType = "EXISTENCEHHT";
                parm.vslCallID = m_vslCallId;
                parm.workYmd = UserInfo.getInstance().Workdate;
                parm.shftId = UserInfo.getInstance().Shift;

                ResponseInfo info = proxy.getVSRList(parm);

                VsrList grdItem;

                m_grdDataMP.Clear();
                m_grdDataPC.Clear();
                m_grdDataST.Clear();
                m_grdDataFL.Clear();
                m_grdDataTR.Clear();
                m_grdDataEQ.Clear();
                manPowerListBindingSource.Clear();
                portCraneListBindingSource.Clear();
                stevedoreListBindingSource.Clear();
                forkliftListBindingSource.Clear();
                trailerListBindingSource.Clear();
                mechanicalEqListBindingSource.Clear();

                //Dictionary<string, string> FLEQNo = new Dictionary<string, string>();
                //Dictionary<string, string> TREQNo = new Dictionary<string, string>();
                //Dictionary<string, string> MEQNo = new Dictionary<string, string>();
                List<CodeHold> FLHold = new List<CodeHold>();
                List<CodeHold> TRHold = new List<CodeHold>();
                List<CodeHold> MEHold = new List<CodeHold>();



                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is CheckListVSRItem)
                    {
                        CheckListVSRItem item = (CheckListVSRItem)info.list[i];

                        //Man Power
                        if ("SD".Equals(item.divCd))
                        {
                            //Man power
                            grdItem = new VsrList();
                            grdItem.ItemStatus = Constants.ITEM_OLD;
                            grdItem.Status = Constants.WS_NM_INITIAL;
                            grdItem.Role = item.rsNm;
                            grdItem.StaffId = item.empId;
                            grdItem.StaffName = item.empNm;
                            grdItem.WArea = item.workLoc;
                            grdItem.StartTime = item.workStDt;
                            grdItem.EndTime = item.workEndDt;
                            grdItem.SEQ = item.seq.ToString();
                            grdItem.JPVC = item.vslCallID;

                            manPowerListBindingSource.Add(grdItem);
                            this.m_grdDataMP.Add(item);

                        }

                        // Port Crane
                        string strOperator;
                        if ("PC".Equals(item.divCd))
                        {
                            strOperator = ("JPB".Equals(item.mbsCd)) ? item.empId : item.cnrtCd;

                            //Port Crane
                            grdItem = new VsrList();
                            grdItem.ItemStatus = Constants.ITEM_OLD;
                            grdItem.Status = Constants.WS_NM_INITIAL;
                            grdItem.EqNo = item.engNm;
                            grdItem.Capacity = item.capaDescr;
                            grdItem.OperatorVsr = strOperator;
                            grdItem.Requestor = item.payer;
                            grdItem.CgType = item.cgTpNm;
                            grdItem.Purpose = item.purpose;
                            grdItem.PurposeNm = item.purposeNm;
                            grdItem.StartTime = item.workStDt;
                            grdItem.EndTime = item.workEndDt;
                            grdItem.SEQ = item.seq.ToString();
                            grdItem.JPVC = item.vslCallID;

                            this.portCraneListBindingSource.Add(grdItem);
                            this.m_grdDataPC.Add(item);

                        }

                        // Stevedore
                        if ("ST".Equals(item.divCd))
                        {
                            //Stevedore
                            grdItem = new VsrList();
                            grdItem.ItemStatus = Constants.ITEM_OLD;
                            grdItem.Status = Constants.WS_NM_INITIAL;
                            grdItem.Stevedore = item.stvdComp;
                            grdItem.Supervisor = item.nofStvdSprr;
                            grdItem.NonTonnage = item.stvdNonTon;
                            grdItem.Requestor = item.payer;
                            grdItem.WArea = item.workLoc;
                            grdItem.Purpose = item.purpose;
                            grdItem.PurposeNm = item.purposeNm;
                            grdItem.StartTime = item.workStDt;
                            grdItem.EndTime = item.workEndDt;
                            grdItem.SEQ = item.seq.ToString();
                            grdItem.JPVC = item.purposeNm;

                            this.stevedoreListBindingSource.Add(grdItem);
                            this.m_grdDataST.Add(item);
                        }

                        // Forklift
                        if ("FL".Equals(item.divCd))
                        {
                            strOperator = ("JPB".Equals(item.mbsCd)) ? item.empId : item.cnrtCd;

                            //Forklift
                            grdItem = new VsrList();
                            grdItem.ItemStatus = Constants.ITEM_OLD;
                            //grdItem.Status = Constants.WS_NM_INITIAL;
                            grdItem.Status = Constants.ITEM_CONFIRMED;
                            grdItem.EqNo = item.engNm;
                            grdItem.Capacity = item.capaDescr;
                            grdItem.OperatorVsr = strOperator;
                            grdItem.WArea = item.workLoc;
                            grdItem.StartTime = item.workStDt;
                            grdItem.EndTime = item.workEndDt;
                            grdItem.SEQ = item.seq.ToString();
                            grdItem.JPVC = item.vslCallID;
                            grdItem.CgType = item.cgTpNm;
                            grdItem.Requestor = item.payer;
                            grdItem.Purpose = item.purpose;
                            grdItem.PurposeNm = item.purposeNm;
                            grdItem.DevlMode = item.delvTpNm;
                            grdItem.eqArr = item.setupTime;
                            grdItem.RefNo = item.refNo;

                            FLHold.Add(new CodeHold(item.capaCd, ("JPB".Equals(item.mbsCd) ? "JPB" : strOperator), item.workLoc));
                            item.IsFL = Constants.ITEM_CONFIRMED;

                            this.forkliftListBindingSource.Add(grdItem);
                            this.m_grdDataFL.Add(item);
                        }

                        // Trailer
                        if ("TR".Equals(item.divCd))
                        {
                            //Trailer
                            grdItem = new VsrList();
                            grdItem.ItemStatus = Constants.ITEM_OLD;
                            grdItem.Status = Constants.ITEM_CONFIRMED;
                            grdItem.EqNo = item.rsNm;
                            grdItem.Capacity = item.capaDescr;
                            grdItem.Nos = item.rsQty;
                            grdItem.WArea = item.workLoc;
                            grdItem.ApFp = item.hatchDir;
                            grdItem.OperatorVsr = item.cnrtCd;
                            grdItem.StartTime = item.workStDt;
                            grdItem.EndTime = item.workEndDt;
                            grdItem.SEQ = item.seq.ToString();
                            grdItem.JPVC = item.vslCallID;
                            grdItem.CgType = item.cgTpNm;
                            grdItem.Requestor = item.payer;
                            grdItem.Purpose = item.purpose;
                            grdItem.PurposeNm = item.purposeNm;
                            grdItem.DevlMode = item.delvTpNm;
                            grdItem.eqArr = item.setupTime;
                            grdItem.RefNo = item.refNo;

                            TRHold.Add(new CodeHold(item.capaCd, item.cnrtCd, item.workLoc));
                            item.IsTR = Constants.ITEM_CONFIRMED;

                            this.trailerListBindingSource.Add(grdItem);
                            this.m_grdDataTR.Add(item);
                        }


                        // Mechanical EQU
                        if ("ME".Equals(item.divCd))
                        {
                            //Mech eq
                            grdItem = new VsrList();
                            grdItem.ItemStatus = Constants.ITEM_OLD;
                            grdItem.Status = Constants.ITEM_CONFIRMED;
                            grdItem.WoNo = item.workOdrNo;
                            grdItem.EqNo = item.rsNm;
                            grdItem.Capacity = item.capaDescr;
                            grdItem.Nos = item.rsQty;
                            grdItem.WArea = item.workLoc;
                            grdItem.OperatorVsr = item.cnrtCd;
                            grdItem.Purpose = item.purpose;
                            grdItem.PurposeNm = item.purposeNm;
                            grdItem.StartTime = item.workStDt;
                            grdItem.EndTime = item.workEndDt;
                            grdItem.SEQ = item.seq.ToString();
                            grdItem.JPVC = item.vslCallID;
                            grdItem.CgType = item.cgTpNm;
                            grdItem.Requestor = item.payer;
                            grdItem.eqArr = item.setupTime;
                            grdItem.RefNo = item.refNo;

                            MEHold.Add(new CodeHold(item.capaCd, item.cnrtCd, item.workLoc));
                            item.IsME = Constants.ITEM_CONFIRMED;

                            this.mechanicalEqListBindingSource.Add(grdItem);
                            this.m_grdDataEQ.Add(item);

                        }
                    }
                }

                ICommonProxy commonProxy = new CommonProxy();
                VOperationDeployParm planningParm = new VOperationDeployParm();

                planningParm.vslCallId = m_vslCallId;
                planningParm.workYmd = UserInfo.getInstance().Workdate;
                planningParm.shftId = UserInfo.getInstance().Shift;

                ResponseInfo megaInfo = commonProxy.getForkliftDeployListForVSRHHT(planningParm);

                for (int i = 0; i < megaInfo.list.Length; i++)
                {
                    if (megaInfo.list[i] is CheckListVSRItem)
                    {
                        CheckListVSRItem item = (CheckListVSRItem)megaInfo.list[i];

                        item.IsFL = Constants.ITEM_NOTCONFIRMED;
                        item.workYmd = UserInfo.getInstance().Workdate;

                        if (string.Empty.Equals(item.workLocTp) || " ".Equals(item.workLocTp))
                        {
                            item.workLocTp = CommonUtility.GetWorkAreaType(item.workLoc);
                        }
                        if (item.workLoc.Length > 6 && HCM113.AREA_WHARF.Equals(item.workLocTp) && "Wharf(".Equals(item.workLoc.Substring(0, 6)))
                        {
                            item.workLoc = item.workLoc.Substring(6, item.workLoc.Length - 7);
                        }

                        string strStartTime = UserInfo.getInstance().Workdate + " ";
                        string strEndTime = UserInfo.getInstance().Workdate + " ";
                        if ("1".Equals(UserInfo.getInstance().ShiftIndex))
                        {
                            strStartTime += CommonUtility.SHIFT_1_FRM;
                            strEndTime += CommonUtility.SHIFT_1_TO;
                        }
                        else if ("2".Equals(UserInfo.getInstance().ShiftIndex))
                        {
                            strStartTime += CommonUtility.SHIFT_2_FRM;
                            strEndTime += CommonUtility.SHIFT_2_TO;
                        }
                        else if ("3".Equals(UserInfo.getInstance().ShiftIndex))
                        {
                            strStartTime += CommonUtility.SHIFT_3_FRM;

                            DateTime workdate = CommonUtility.ParseYMD(UserInfo.getInstance().Workdate);
                            workdate = workdate.AddDays(1);
                            strEndTime = workdate.ToString(CommonUtility.DDMMYYYY);
                            strEndTime += " " + CommonUtility.SHIFT_3_TO;
                        }
                        item.workStDt = strStartTime;
                        item.workEndDt = strEndTime;
                        item.setupTime = strStartTime;

                        //Forklift
                        if ("FL".Equals(item.divCd))
                        {
                            string strOperator = ("JPB".Equals(item.mbsCd)) ? item.empId : item.cnrtCd;

                            grdItem = new VsrList();
                            grdItem.ItemStatus = Constants.ITEM_MEGA;
                            grdItem.Status = Constants.ITEM_NOTCONFIRMED;
                            grdItem.EqNo = item.rsNm;
                            grdItem.Capacity = item.capaDescr;
                            grdItem.OperatorVsr = strOperator;
                            grdItem.WArea = item.workLoc;
                            grdItem.StartTime = item.workStDt;
                            grdItem.EndTime = item.workEndDt;
                            grdItem.SEQ = item.seq.ToString();
                            grdItem.JPVC = item.vslCallID;
                            grdItem.CgType = item.cgTpNm;
                            grdItem.Requestor = item.payer;
                            grdItem.Purpose = item.purpose;
                            grdItem.PurposeNm = item.purposeNm;
                            grdItem.DevlMode = item.delvTpNm;
                            grdItem.eqArr = item.setupTime;
                            grdItem.RefNo = item.refNo;

                            item.IsFL = Constants.ITEM_NOTCONFIRMED;

                            int numb = int.Parse(item.rsQty);
                            /*if (numb == 1)
                            {
                                this.forkliftListBindingSource.Add(grdItem);
                                this.m_grdDataFL.Add(CloneVSRItem(item));
                            }
                            else
                            {
                                CodeHold codeHold = new CodeHold(item.capaCd, ("JPB".Equals(item.mbsCd) ? "JPB" : strOperator), item.workLoc);
                                if (FLHold.Contains(codeHold))
                                {
                                    int count = 0;
                                    foreach (CodeHold val in FLHold)
                                    {
                                        if (val.Equals(codeHold))
                                            count++;
                                    }
                                    numb = numb - count;
                                }
                                */
                            for (int j = 0; j < numb; j++)
                            {
                                this.forkliftListBindingSource.Add(grdItem);
                                this.m_grdDataFL.Add(CloneVSRItem(item));
                            }
                            //}
                        }

                        // Trailer
                        if ("TR".Equals(item.divCd))
                        {
                            //Trailer
                            grdItem = new VsrList();
                            grdItem.ItemStatus = Constants.ITEM_MEGA;
                            grdItem.Status = Constants.ITEM_NOTCONFIRMED;
                            grdItem.EqNo = item.divCd;
                            grdItem.Capacity = item.capaDescr;
                            grdItem.Nos = "1";
                            grdItem.WArea = item.workLoc;
                            grdItem.ApFp = item.hatchDir;
                            grdItem.OperatorVsr = item.cnrtCd;
                            grdItem.StartTime = item.workStDt;
                            grdItem.EndTime = item.workEndDt;
                            grdItem.SEQ = item.seq.ToString();
                            grdItem.JPVC = item.vslCallID;
                            grdItem.CgType = item.cgTpNm;
                            grdItem.Requestor = item.payer;
                            grdItem.Purpose = item.purpose;
                            grdItem.PurposeNm = item.purposeNm;
                            grdItem.DevlMode = item.delvTpNm;
                            grdItem.eqArr = item.setupTime;
                            grdItem.RefNo = item.refNo;

                            item.rsNm = item.eqNo;
                            item.IsTR = Constants.ITEM_NOTCONFIRMED;

                            int numb = int.Parse(item.rsQty);
                            /*if (numb == 1)
                            {
                                this.trailerListBindingSource.Add(grdItem);
                                this.m_grdDataTR.Add(CloneVSRItem(item));
                            }
                            else
                            {
                                CodeHold codeHold = new CodeHold(item.capaCd, item.cnrtCd, item.workLoc);
                                if (TRHold.Contains(codeHold))
                                {
                                    int count = 0;
                                    foreach (CodeHold val in TRHold)
                                    {
                                        if (val.Equals(codeHold))
                                            count++;
                                    }
                                    numb = numb - count;
                                }*/

                            for (int j = 0; j < numb; j++)
                            {
                                this.trailerListBindingSource.Add(grdItem);
                                this.m_grdDataTR.Add(CloneVSRItem(item));
                            }
                            //}
                        }

                        // Mechanical EQU

                        if ("ME".Equals(item.divCd))
                        {
                            //Mech eq
                            grdItem = new VsrList();
                            grdItem.ItemStatus = Constants.ITEM_MEGA;
                            grdItem.Status = Constants.ITEM_NOTCONFIRMED;
                            grdItem.WoNo = item.workOdrNo;
                            grdItem.EqNo = item.divCd;
                            grdItem.Capacity = item.capaDescr;
                            grdItem.Nos = item.rsQty;
                            grdItem.WArea = item.workLoc;
                            grdItem.OperatorVsr = item.cnrtCd;
                            grdItem.Purpose = item.purpose;
                            grdItem.PurposeNm = item.purposeNm;
                            grdItem.StartTime = item.workStDt;
                            grdItem.EndTime = item.workEndDt;
                            grdItem.SEQ = item.seq.ToString();
                            grdItem.JPVC = item.vslCallID;
                            grdItem.CgType = item.cgTpNm;
                            grdItem.Requestor = item.payer;
                            grdItem.eqArr = item.setupTime;
                            grdItem.RefNo = item.refNo;

                            item.rsNm = item.eqNo;
                            item.IsME = Constants.ITEM_NOTCONFIRMED;

                            int numb = int.Parse(item.rsQty);
                            //if (numb == 1)
                            //{
                            //    this.mechanicalEqListBindingSource.Add(grdItem);
                            //    this.m_grdDataEQ.Add(CloneVSRItem(item));
                            //}
                            //else
                            //{
                            //    CodeHold codeHold = new CodeHold(item.capaCd, item.cnrtCd, item.workLoc);
                            //    if (MEHold.Contains(codeHold))
                            //    {
                            //        int count = 0;
                            //        foreach (CodeHold val in MEHold)
                            //        {
                            //            if (val.Equals(codeHold))
                            //                count++;
                            //        }
                            //        numb = numb - count;
                            //    }

                            //for (int j = 0; j < numb; j++)
                            //{
                            this.mechanicalEqListBindingSource.Add(grdItem);
                            this.m_grdDataEQ.Add(CloneVSRItem(item));
                            //}
                            //}
                        }
                    }
                }


                this.grdMan_Data.DataSource = manPowerListBindingSource;
                this.grdEQ_Data.DataSource = mechanicalEqListBindingSource;
                this.grdTR_Data.DataSource = trailerListBindingSource;
                this.grdFL_Data.DataSource = forkliftListBindingSource;
                this.grdPC_Data.DataSource = portCraneListBindingSource;
                this.grdST_Data.DataSource = stevedoreListBindingSource;
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                grdMan_Data.IsDirty = false;
                grdPC_Data.IsDirty = false;
                grdST_Data.IsDirty = false;
                grdFL_Data.IsDirty = false;
                grdTR_Data.IsDirty = false;
                grdEQ_Data.IsDirty = false;
                Cursor.Current = Cursors.Default;
            }
        }

        private CheckListVSRItem CloneVSRItem(CheckListVSRItem item)
        {
            CheckListVSRItem cloneItem = new CheckListVSRItem();
            cloneItem.atb = item.atb;
            cloneItem.capa = item.capa;
            cloneItem.capaCd = item.capaCd;
            cloneItem.capaDescr = item.capaDescr;
            cloneItem.cgTpCd = item.cgTpCd;
            cloneItem.cgTpNm = item.cgTpNm;
            cloneItem.chagYN = item.chagYN;
            cloneItem.cnrtCd = item.cnrtCd;
            cloneItem.CRUD = item.CRUD;
            cloneItem.delvTpCd = item.delvTpCd;
            cloneItem.delvTpNm = item.delvTpNm;
            cloneItem.direct = item.direct;
            cloneItem.divCd = item.divCd;
            cloneItem.driver = item.driver;
            cloneItem.empId = item.empId;
            cloneItem.empNm = item.empNm;
            cloneItem.engNm = item.engNm;
            cloneItem.eqNo = item.eqNo;
            cloneItem.hatchDir = item.hatchDir;
            cloneItem.hatchNo = item.hatchNo;
            cloneItem.indirect = item.indirect;
            cloneItem.IsFL = item.IsFL;
            cloneItem.IsME = item.IsME;
            cloneItem.IsPC = item.IsPC;
            cloneItem.IsSD = item.IsSD;
            cloneItem.IsST = item.IsST;
            cloneItem.IsTR = item.IsTR;
            cloneItem.mbsCd = item.mbsCd;
            cloneItem.megaNo = item.megaNo;
            cloneItem.mt = item.mt;
            cloneItem.no = item.no;
            cloneItem.nofStvdSprr = item.nofStvdSprr;
            cloneItem.NOS_SF1 = item.NOS_SF1;
            cloneItem.NOS_SF2 = item.NOS_SF2;
            cloneItem.NOS_SF3 = item.NOS_SF3;
            cloneItem.nosCapa = item.nosCapa;
            cloneItem.nosLb = item.nosLb;
            cloneItem.opeCompCd = item.opeCompCd;
            cloneItem.opeCompNm = item.opeCompNm;
            cloneItem.@operator = item.@operator;
            cloneItem.ownDivCdNm = item.ownDivCdNm;
            cloneItem.payer = item.payer;
            cloneItem.purpose = item.purpose;
            cloneItem.purposeNm = item.purposeNm;
            cloneItem.refNo = item.refNo;
            cloneItem.rmk = item.rmk;
            cloneItem.roleCd = item.roleCd;
            cloneItem.rsNm = item.rsNm;

            //add by William Nguyen 2015.03.12
            if (item.divCd.Equals("ME"))
                cloneItem.rsQty = item.rsQty;
            else
                cloneItem.rsQty = "1"; // this must be 1 not item.rsQty

            cloneItem.seq = item.seq;
            cloneItem.setupTime = item.setupTime;
            cloneItem.shftCg = item.shftCg;
            cloneItem.shftId = item.shftId;
            cloneItem.shftLb = item.shftLb;
            cloneItem.shftNm = item.shftNm;
            cloneItem.shpCrew = item.shpCrew;
            cloneItem.stvdComp = item.stvdComp;
            cloneItem.stvdCompNm = item.stvdCompNm;
            cloneItem.stvdNonTon = item.stvdNonTon;
            cloneItem.sumitBy = item.sumitBy;
            cloneItem.sumitDt = item.sumitDt;
            cloneItem.updateDt = item.updateDt;
            cloneItem.updateId = item.updateId;
            cloneItem.userId = item.userId;
            cloneItem.vslCallID = item.vslCallID;
            cloneItem.vslName = item.vslName;
            cloneItem.workEndDt = item.workEndDt;
            cloneItem.workLoc = item.workLoc;
            cloneItem.workLocTp = item.workLocTp;
            cloneItem.workLocTpNm = item.workLocTpNm;
            cloneItem.workOdrNo = item.workOdrNo;
            cloneItem.workStDt = item.workStDt;
            cloneItem.workYmd = item.workYmd;

            return cloneItem;
        }

        private void DeleteItem(int tab)
        {
            List<CheckListVSRItem> arrTemp = null;
            Framework.Controls.UserControls.TGrid grdData = null;
            BindingSource dataSource = new BindingSource();
            if (tab == HCM119.TAB_MANPOWER)
            {
                grdData = grdMan_Data;
                dataSource = manPowerListBindingSource;
                arrTemp = m_grdDataMP;
            }
            else if (tab == HCM119.TAB_PORTCRANE)
            {
                grdData = grdPC_Data;
                dataSource = portCraneListBindingSource;
                arrTemp = m_grdDataPC;
            }
            else if (tab == HCM119.TAB_STEVEDORE)
            {
                grdData = grdST_Data;
                dataSource = stevedoreListBindingSource;
                arrTemp = m_grdDataST;
            }
            else if (tab == HCM119.TAB_FORKLIFT)
            {
                grdData = grdFL_Data;
                dataSource = forkliftListBindingSource;
                arrTemp = m_grdDataFL;
            }
            else if (tab == HCM119.TAB_TRAILER)
            {
                grdData = grdTR_Data;
                dataSource = trailerListBindingSource;
                arrTemp = m_grdDataTR;
            }
            else if (tab == HCM119.TAB_EQU)
            {
                grdData = grdEQ_Data;
                dataSource = mechanicalEqListBindingSource;
                arrTemp = m_grdDataEQ;
            }
            if (grdData != null && dataSource != null)
            {
                int currRowIndex = grdData.CurrentRowIndex;

                if (currRowIndex > -1 && currRowIndex < dataSource.Count)
                {

                    // In case item status is NEW: remove this row from grid and ArrayList
                    // In case item status is OLD: change WORKING STATUS of this row in grid and ArrayList
                    IList itemList = dataSource.List;
                    string itemStatus = String.Empty;
                    int j = 0;
                    foreach (VsrList itemInLst in itemList)
                    {
                        if (j == currRowIndex)
                        {
                            itemStatus = itemInLst.ItemStatus;
                            break;
                        }
                        j++;
                    }


                    if (Constants.ITEM_NEW.Equals(itemStatus))
                    {
                        arrTemp.RemoveAt(currRowIndex);
                        dataSource.RemoveAt(currRowIndex);
                    }
                    else if (Constants.ITEM_OLD.Equals(itemStatus))
                    {
                        // Change the status of that item to DELETE
                        if (arrTemp != null && arrTemp.Count > currRowIndex)
                        {
                            CheckListVSRItem item = arrTemp[currRowIndex];
                            item.CRUD = Constants.WS_DELETE;
                        }

                        // Change WORKING STATUS of this row to DELETE.
                        j = 0;
                        foreach (VsrList itemInLst in itemList)
                        {
                            if (j == currRowIndex)
                            {
                                itemInLst.Status = Constants.WS_NM_DELETE; ;
                                break;
                            }
                            j++;
                        }
                    }
                }
            }

            if (ProcessVSRListItem())
            {
                F_Retrieve();
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
            }
        }

        private bool ProcessVSRListItem()
        {
            bool result = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                CheckListVSRItem item;
                ArrayList arrObj = new ArrayList();

                // Man Power (ref: CT106001)
                if (m_grdDataMP != null)
                {
                    for (int i = 0; i < m_grdDataMP.Count; i++)
                    {
                        item = m_grdDataMP[i];

                        // Process only items that are modified.
                        if (Constants.WS_INSERT.Equals(item.CRUD) ||
                            Constants.WS_UPDATE.Equals(item.CRUD) ||
                            Constants.WS_DELETE.Equals(item.CRUD))
                        {
                            arrObj.Add(item);
                        }
                    }
                }
                // Port Crane (ref: CT106007)
                if (m_grdDataPC != null)
                {
                    for (int i = 0; i < m_grdDataPC.Count; i++)
                    {
                        item = m_grdDataPC[i];

                        // Process only items that are modified.
                        if (Constants.WS_INSERT.Equals(item.CRUD) ||
                            Constants.WS_UPDATE.Equals(item.CRUD) ||
                            Constants.WS_DELETE.Equals(item.CRUD))
                        {
                            arrObj.Add(item);
                        }
                    }
                }
                // Stevedore (ref: CT211004)
                if (m_grdDataST != null)
                {
                    for (int i = 0; i < m_grdDataST.Count; i++)
                    {
                        item = m_grdDataST[i];

                        // Process only items that are modified.
                        if (Constants.WS_INSERT.Equals(item.CRUD) ||
                            Constants.WS_UPDATE.Equals(item.CRUD) ||
                            Constants.WS_DELETE.Equals(item.CRUD))
                        {
                            arrObj.Add(item);
                        }
                    }
                }
                // Forklift (ref: CT106002)
                if (m_grdDataFL != null)
                {
                    for (int i = 0; i < m_grdDataFL.Count; i++)
                    {
                        item = m_grdDataFL[i];

                        // Process only items that are modified.
                        if (Constants.WS_INSERT.Equals(item.CRUD) ||
                            Constants.WS_UPDATE.Equals(item.CRUD) ||
                            Constants.WS_DELETE.Equals(item.CRUD))
                        {
                            arrObj.Add(item);
                        }
                    }
                }
                // Trailer (ref: CT106009)
                if (m_grdDataTR != null)
                {
                    for (int i = 0; i < m_grdDataTR.Count; i++)
                    {
                        item = m_grdDataTR[i];

                        // Process only items that are modified.
                        if (Constants.WS_INSERT.Equals(item.CRUD) ||
                            Constants.WS_UPDATE.Equals(item.CRUD) ||
                            Constants.WS_DELETE.Equals(item.CRUD))
                        {
                            arrObj.Add(item);
                        }
                    }
                }
                // Mechanical Equipment (ref: CT106003)
                if (m_grdDataEQ != null)
                {
                    for (int i = 0; i < m_grdDataEQ.Count; i++)
                    {
                        item = m_grdDataEQ[i];

                        // Process only items that are modified.
                        if (Constants.WS_INSERT.Equals(item.CRUD) ||
                            Constants.WS_UPDATE.Equals(item.CRUD) ||
                            Constants.WS_DELETE.Equals(item.CRUD))
                        {
                            arrObj.Add(item);
                        }
                    }
                }

                if (arrObj.Count > 0)
                {
                    Object[] obj = arrObj.ToArray();
                    IApronCheckerProxy proxy = new ApronCheckerProxy();
                    DataItemCollection dataCollection = new DataItemCollection();
                    dataCollection.collection = obj;

                    proxy.processVSRListItem(dataCollection);
                    result = true;
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
            return result;
        }

        private void GridCellChangedListener(object sender, EventArgs e)
        {
            int index;
            string strWorkingStatus;
            DataGrid mygrid = (DataGrid)sender;
            String gridName = mygrid.Name;
            switch (gridName)
            {
                case "grdMan_Data":
                    index = grdMan_Data.CurrentRowIndex;
                    if (index > -1 && index < grdMan_Data.DataTable.Rows.Count)
                    {
                        // Disable UPDATE, DELETE buttons if this row was marked as deleted.
                        strWorkingStatus = grdMan_Data.DataTable.Rows[index][HEADER_WS_NM].ToString();
                        if (Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                        {
                            btnMan_Update.Enabled = false;
                            btnMan_Delete.Enabled = false;
                        }
                        else
                        {
                            btnMan_Update.Enabled = true;
                            btnMan_Delete.Enabled = true;
                        }
                    }
                    break;

                case "grdPC_Data":
                    index = grdPC_Data.CurrentRowIndex;
                    if (index > -1 && index < grdPC_Data.DataTable.Rows.Count)
                    {
                        // Disable UPDATE, DELETE buttons if this row was marked as deleted.
                        strWorkingStatus = grdPC_Data.DataTable.Rows[index][HEADER_WS_NM].ToString();
                        if (Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                        {
                            btnPC_Update.Enabled = false;
                            btnPC_Delete.Enabled = false;
                        }
                        else
                        {
                            btnPC_Update.Enabled = true;
                            btnPC_Delete.Enabled = true;
                        }
                    }
                    break;

                case "grdST_Data":
                    index = grdST_Data.CurrentRowIndex;
                    if (index > -1 && index < grdST_Data.DataTable.Rows.Count)
                    {
                        // Disable UPDATE, DELETE buttons if this row was marked as deleted.
                        strWorkingStatus = grdST_Data.DataTable.Rows[index][HEADER_WS_NM].ToString();
                        if (Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                        {
                            btnST_Update.Enabled = false;
                            btnST_Delete.Enabled = false;
                        }
                        else
                        {
                            btnST_Update.Enabled = true;
                            btnST_Delete.Enabled = true;
                        }
                    }
                    break;

                case "grdFL_Data":
                    index = grdFL_Data.CurrentRowIndex;
                    if (index > -1 && index < grdFL_Data.DataTable.Rows.Count)
                    {
                        // Disable UPDATE, DELETE buttons if this row was marked as deleted.
                        strWorkingStatus = grdFL_Data.DataTable.Rows[index][HEADER_WS_NM].ToString();
                        if (Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                        {
                            btnFL_Update.Enabled = false;
                            btnFL_Delete.Enabled = false;
                        }
                        else
                        {
                            btnFL_Update.Enabled = true;
                            btnFL_Delete.Enabled = true;
                        }
                    }
                    break;

                case "grdTR_Data":
                    index = grdTR_Data.CurrentRowIndex;
                    if (index > -1 && index < grdTR_Data.DataTable.Rows.Count)
                    {
                        // Disable UPDATE, DELETE buttons if this row was marked as deleted.
                        strWorkingStatus = grdTR_Data.DataTable.Rows[index][HEADER_WS_NM].ToString();
                        if (Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                        {
                            btnTR_Update.Enabled = false;
                            btnTR_Delete.Enabled = false;
                        }
                        else
                        {
                            btnTR_Update.Enabled = true;
                            btnTR_Delete.Enabled = true;
                        }
                    }
                    break;

                case "grdEQ_Data":
                    index = grdEQ_Data.CurrentRowIndex;
                    if (index > -1 && index < grdEQ_Data.DataTable.Rows.Count)
                    {
                        // Disable UPDATE, DELETE buttons if this row was marked as deleted.
                        strWorkingStatus = grdEQ_Data.DataTable.Rows[index][HEADER_WS_NM].ToString();
                        if (Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                        {
                            btnEQ_Update.Enabled = false;
                            btnEQ_Delete.Enabled = false;
                        }
                        else
                        {
                            btnEQ_Update.Enabled = true;
                            btnEQ_Delete.Enabled = true;
                        }
                    }
                    break;
            }
        }

        private void GridDoubleClickListener(object sender, EventArgs e)
        {
            int index;
            string strWorkingStatus;
            DataGrid mygrid = (DataGrid)sender;
            String gridName = mygrid.Name;
            switch (gridName)
            {
                case "grdMan_Data":
                    index = grdMan_Data.CurrentRowIndex;
                    if (index > -1 && index < grdMan_Data.DataTable.Rows.Count)
                    {
                        // Proceed update processing if this row was NOT marked as deleted.
                        strWorkingStatus = grdMan_Data.DataTable.Rows[index][HEADER_WS_NM].ToString();
                        if (!Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                        {
                            UpdateItem(TAB_MANPOWER);
                        }
                    }
                    break;

                case "grdPC_Data":
                    index = grdPC_Data.CurrentRowIndex;
                    if (index > -1 && index < grdPC_Data.DataTable.Rows.Count)
                    {
                        // Proceed update processing if this row was NOT marked as deleted.
                        strWorkingStatus = grdPC_Data.DataTable.Rows[index][HEADER_WS_NM].ToString();
                        if (!Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                        {
                            UpdateItem(TAB_PORTCRANE);
                        }
                    }
                    break;

                case "grdST_Data":
                    index = grdST_Data.CurrentRowIndex;
                    if (index > -1 && index < grdST_Data.DataTable.Rows.Count)
                    {
                        // Proceed update processing if this row was NOT marked as deleted.
                        strWorkingStatus = grdST_Data.DataTable.Rows[index][HEADER_WS_NM].ToString();
                        if (!Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                        {
                            UpdateItem(TAB_STEVEDORE);
                        }
                    }
                    break;

                case "grdFL_Data":
                    index = grdFL_Data.CurrentRowIndex;
                    if (index > -1 && index < grdFL_Data.DataTable.Rows.Count)
                    {
                        // Proceed update processing if this row was NOT marked as deleted.
                        strWorkingStatus = grdFL_Data.DataTable.Rows[index][HEADER_WS_NM].ToString();
                        if (!Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                        {
                            UpdateItem(TAB_FORKLIFT);
                        }
                    }
                    break;

                case "grdTR_Data":
                    index = grdTR_Data.CurrentRowIndex;
                    if (index > -1 && index < grdTR_Data.DataTable.Rows.Count)
                    {
                        // Proceed update processing if this row was NOT marked as deleted.
                        strWorkingStatus = grdTR_Data.DataTable.Rows[index][HEADER_WS_NM].ToString();
                        if (!Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                        {
                            UpdateItem(TAB_TRAILER);
                        }
                    }
                    break;

                case "grdEQ_Data":
                    index = grdEQ_Data.CurrentRowIndex;
                    if (index > -1 && index < grdEQ_Data.DataTable.Rows.Count)
                    {
                        // Proceed update processing if this row was NOT marked as deleted.
                        strWorkingStatus = grdEQ_Data.DataTable.Rows[index][HEADER_WS_NM].ToString();
                        if (!Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                        {
                            UpdateItem(TAB_EQU);
                        }
                    }
                    break;
            }
        }

        private void Mouse_up(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int index;
            string strWorkingStatus = String.Empty;
            DataGrid mygrid = (DataGrid)sender;
            String gridName = mygrid.Name;
            switch (gridName)
            {
                case "grdMan_Data":
                    index = grdMan_Data.CurrentRowIndex;
                    if (index > -1 && index < manPowerListBindingSource.Count)
                    {
                        IList list = manPowerListBindingSource.List;
                        int i = 0;
                        foreach (VsrList item in list)
                        {
                            if (i == index)
                            {
                                strWorkingStatus = item.Status;
                                break;
                            }
                            i++;
                        }

                        if (Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                        {
                            btnMan_Update.Enabled = false;
                            btnMan_Delete.Enabled = false;
                        }
                        else
                        {
                            btnMan_Update.Enabled = true;
                            btnMan_Delete.Enabled = true;
                        }
                    }
                    break;

                case "grdPC_Data":
                    index = grdPC_Data.CurrentRowIndex;
                    if (index > -1 && index < portCraneListBindingSource.Count)
                    {
                        // Disable UPDATE, DELETE buttons if this row was marked as deleted.
                        IList list = portCraneListBindingSource.List;
                        int i = 0;
                        foreach (VsrList item in list)
                        {
                            if (i == index)
                            {
                                strWorkingStatus = item.Status;
                                break;
                            }
                            i++;
                        }

                        //strWorkingStatus = grdPC_Data.DataTable.Rows[index][HEADER_WS_NM].ToString();

                        if (Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                        {
                            btnPC_Update.Enabled = false;
                            btnPC_Delete.Enabled = false;
                        }
                        else
                        {
                            btnPC_Update.Enabled = true;
                            btnPC_Delete.Enabled = true;
                        }
                    }
                    break;

                case "grdST_Data":
                    index = grdST_Data.CurrentRowIndex;
                    if (index > -1 && index < stevedoreListBindingSource.Count)
                    {
                        // Disable UPDATE, DELETE buttons if this row was marked as deleted.
                        IList list = stevedoreListBindingSource.List;
                        int i = 0;
                        foreach (VsrList item in list)
                        {
                            if (i == index)
                            {
                                strWorkingStatus = item.Status;
                                break;
                            }
                            i++;
                        }
                        //strWorkingStatus = grdST_Data.DataTable.Rows[index][HEADER_WS_NM].ToString();
                        if (Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                        {
                            btnST_Update.Enabled = false;
                            btnST_Delete.Enabled = false;
                        }
                        else
                        {
                            btnST_Update.Enabled = true;
                            btnST_Delete.Enabled = true;
                        }
                    }
                    break;

                case "grdFL_Data":
                    index = grdFL_Data.CurrentRowIndex;
                    if (index > -1 && index < forkliftListBindingSource.Count)
                    {
                        // Disable UPDATE, DELETE buttons if this row was marked as deleted.
                        IList list = forkliftListBindingSource.List;
                        int i = 0;
                        foreach (VsrList item in list)
                        {
                            if (i == index)
                            {
                                strWorkingStatus = item.Status;
                                break;
                            }
                            i++;
                        }

                        //strWorkingStatus = grdFL_Data.DataTable.Rows[index][HEADER_WS_NM].ToString();
                        if (Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                        {
                            btnFL_Update.Enabled = false;
                            btnFL_Delete.Enabled = false;
                        }
                        else
                        {
                            btnFL_Update.Enabled = true;
                            btnFL_Delete.Enabled = true;
                        }
                    }
                    break;

                case "grdTR_Data":
                    index = grdTR_Data.CurrentRowIndex;
                    if (index > -1 && index < trailerListBindingSource.Count)
                    {
                        // Disable UPDATE, DELETE buttons if this row was marked as deleted.
                        IList list = trailerListBindingSource.List;
                        int i = 0;
                        foreach (VsrList item in list)
                        {
                            if (i == index)
                            {
                                strWorkingStatus = item.Status;
                                break;
                            }
                            i++;
                        }

                        //strWorkingStatus = grdTR_Data.DataTable.Rows[index][HEADER_WS_NM].ToString();
                        if (Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                        {
                            btnTR_Update.Enabled = false;
                            btnTR_Delete.Enabled = false;
                        }
                        else
                        {
                            btnTR_Update.Enabled = true;
                            btnTR_Delete.Enabled = true;
                        }
                    }
                    break;

                case "grdEQ_Data":
                    index = grdEQ_Data.CurrentRowIndex;
                    if (index > -1 && index < mechanicalEqListBindingSource.Count)
                    {
                        // Disable UPDATE, DELETE buttons if this row was marked as deleted.
                        IList list = mechanicalEqListBindingSource.List;
                        int i = 0;
                        foreach (VsrList item in list)
                        {
                            if (i == index)
                            {
                                strWorkingStatus = item.Status;
                                break;
                            }
                            i++;
                        }

                        //strWorkingStatus = grdEQ_Data.DataTable.Rows[index][HEADER_WS_NM].ToString();
                        if (Constants.WS_NM_DELETE.Equals(strWorkingStatus))
                        {
                            btnEQ_Update.Enabled = false;
                            btnEQ_Delete.Enabled = false;
                        }
                        else
                        {
                            btnEQ_Update.Enabled = true;
                            btnEQ_Delete.Enabled = true;
                        }
                    }
                    break;
            }
        }

    }
}