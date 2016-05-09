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
using MOST.ApronChecker.Parm;
using MOST.ApronChecker.Result;
using MOST.Client.Proxy.CommonProxy;
using MOST.Client.Proxy.ApronCheckerProxy;
using MOST.Common;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.WHChecker;
using MOST.WHChecker.Parm;
using MOST.WHChecker.Result;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.ApronChecker
{
    public partial class HAC110 : TForm, IPopupWindow
    {
        private const string HEADER_ITEM_STATUS = "_ITEM_STATUS";
        private const string HEADER_WS_NM = "Status";
        private const string HEADER_CAT = "Catg";
        private const string HEADER_JPVC = "JPVC";
        private const string HEADER_CGNO = "Cargo No";
        private const string HEADER_SPCG = "Special Cg";
        private const string HEADER_CGCOND = "Cg Cond";
        private const string HEADER_MT = "MT";
        private const string HEADER_M3 = "M3";
        private const string HEADER_QTY = "Qty";
        private const string HEADER_PKGTYPE = "PkgTp";
        private const string HEADER_OPRMODE = "OPR Mode";
        private const string HEADER_HATCH = "Hatch";
        private const string HEADER_FINAL = "Final";
        private const string HEADER_DELV = "Delivery";
        private const string HEADER_DIRECTION_NM = "Direction";
        private const string HEADER_RCCOND = "RC Cond";
        private const string HEADER_ST_DATE = "StartDate";
        private const string HEADER_ED_DATE = "EndDate";
        private const string HEADER_LOC = "Location";
        private const string HEADER_JOB_TYPE_NM = "Job Type";
        private const string HEADER_DMG = "Dmg";
        private const string HEADER_SHUT = "Shut";
        private const string HEADER_REHANDLE = "Rehandle";
        private const string HEADER_NO = "Seq.";

        private HAC110Parm m_parm;
        private ArrayList m_arrGridData;
        private string m_vslCallId = string.Empty;

        /*
         * lv.dat add paging 20130611
         */
        private int iTotalPage = 0;
        private int iCurrentPage = 0;
        private int iNumbPerPage = Constants.iNumbPerPage;

        CargoJobParm parm;

        public HAC110()
        {
            m_arrGridData = new ArrayList();
            InitializeComponent();
            this.initialFormSize();
            InitializeDataGrid();
            InitializeData();
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HAC110Parm)parm;
            if (m_parm != null)
            {
                txtCargo.Text = m_parm.CgNo;
                m_vslCallId = m_parm.VslCallId;
                //GetCargoJobList();

                initializeCboPaging();
            }
            this.ShowDialog();
            return null;
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_ITEM_STATUS, "0" }, { HEADER_NO, "30" }, { HEADER_WS_NM, "40" }, { HEADER_CAT, "40" }, { HEADER_JPVC, "100" }, { HEADER_CGNO, "70" }, { HEADER_SPCG, "70" }, { HEADER_CGCOND, "70" }
                                , { HEADER_MT, "40" }, { HEADER_M3, "40" }, { HEADER_QTY, "40" }
                                , { HEADER_PKGTYPE, "40" }, { HEADER_OPRMODE, "40" }, { HEADER_HATCH, "30" }, { HEADER_FINAL, "40" }
                                , { HEADER_DELV, "60" } , { HEADER_DIRECTION_NM, "90" }, { HEADER_RCCOND, "60" }
                                , { HEADER_ST_DATE, "85" } , { HEADER_ED_DATE, "90" } , { HEADER_LOC, "60" } , { HEADER_JOB_TYPE_NM, "60" }
                                , { HEADER_DMG, "40" } , { HEADER_SHUT, "40" } , { HEADER_REHANDLE, "70" } };
            this.grdData.setHeader(header);
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                btnUpdate.Enabled = false;
                CommonUtility.SetDTPValueBlank(txtStartTime);
                CommonUtility.SetDTPValueBlank(txtEndTime);

                #region Hatch
                CommonUtility.SetHatchInfo(cboHatch);
                #endregion

                #region OPR Mode
                IApronCheckerProxy proxy = new ApronCheckerProxy();
                CargoJobParm cdparm = new CargoJobParm();
                cdparm.searchType = "HHT_TSPTTP";
                ResponseInfo info = proxy.getCargoJobList(cdparm);

                CommonUtility.InitializeCombobox(cboOPRMode);
                if (info != null)
                {
                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is CodeMasterListItem)
                        {
                            CodeMasterListItem item = (CodeMasterListItem)info.list[i];
                            cboOPRMode.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                        }
                        else if (info.list[i] is CodeMasterListItem1)
                        {
                            CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                            cboOPRMode.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                        }
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

        private void GetCargoJobList()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //if (!string.IsNullOrEmpty(txtCargo.Text))
                //{
                IApronCheckerProxy proxy = new ApronCheckerProxy();
                /*CargoJobParm parm = new CargoJobParm();
                parm.searchType = "HHT_cargoJob";
                parm.vslCallId = m_vslCallId;
                parm.cgNo = txtCargo.Text;*/

                ResponseInfo info = proxy.getCargoJobList(parm);

                m_arrGridData.Clear();
                grdData.Clear();
                if (info != null)
                {
                    //int tempNo = 0;
                    //String tempJobGroup = string.Empty;

                    for (int i = 0; i < info.list.Length; i++)
                    {
                        if (info.list[i] is CargoJobItem)
                        {
                            CargoJobItem item = (CargoJobItem)info.list[i];
                            if (!string.IsNullOrEmpty(item.cgNo))
                            {
                                DataRow newRow = grdData.NewRow();

                                //Number of trips
                                //if (!tempJobGroup.Equals(item.jobGroup))
                                //{
                                //    tempNo++;
                                //}
                                int iHeadNo = (i + 1) + ((this.iCurrentPage - 1) * this.iNumbPerPage);
                                //tempJobGroup = item.jobGroup;
                                //newRow[HEADER_NO] = tempNo.ToString();
                                newRow[HEADER_NO] = iHeadNo.ToString();

                                newRow[HEADER_ITEM_STATUS] = Constants.ITEM_OLD;
                                newRow[HEADER_WS_NM] = Constants.WS_NM_INITIAL;
                                newRow[HEADER_CAT] = item.opeClassNm;
                                newRow[HEADER_JPVC] = item.vslCallId;
                                newRow[HEADER_CGNO] = item.cgNo;
                                newRow[HEADER_SPCG] = item.spCaCoNm;
                                newRow[HEADER_CGCOND] = item.jobCoNm;
                                newRow[HEADER_MT] = item.wgt;
                                newRow[HEADER_M3] = item.msrmt;
                                newRow[HEADER_QTY] = item.pkgQty;

                                newRow[HEADER_PKGTYPE] = item.repkgTypeCd;
                                newRow[HEADER_OPRMODE] = item.tsptTpCd;
                                newRow[HEADER_HATCH] = item.hatchNo;
                                newRow[HEADER_FINAL] = item.fnlOpeYn;

                                newRow[HEADER_DELV] = item.delvTpNm;
                                newRow[HEADER_DIRECTION_NM] = item.jobPurpNm;
                                newRow[HEADER_RCCOND] = item.rcCoNm;
                                newRow[HEADER_ST_DATE] = item.workStDt;
                                newRow[HEADER_ED_DATE] = item.workEndDt;
                                newRow[HEADER_LOC] = item.locId;
                                newRow[HEADER_JOB_TYPE_NM] = item.jobTpNm;
                                newRow[HEADER_DMG] = item.dmgYn;
                                newRow[HEADER_SHUT] = item.shuYn;
                                newRow[HEADER_REHANDLE] = item.rhdlModeNm;

                                grdData.Add(newRow);
                                m_arrGridData.Add(item);
                            }
                        }
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                grdData.IsDirty = false;
                Cursor.Current = Cursors.Default;
            }
        }

        private bool ProcessCargoJobItem()
        {
            bool result = false;
            try
            {
                // Ref: CT121011
                Cursor.Current = Cursors.WaitCursor;

                IApronCheckerProxy proxy = new ApronCheckerProxy();
                System.Collections.ArrayList arrObj = new System.Collections.ArrayList();

                if (grdData != null)
                {
                    for (int i = 0; i < grdData.DataTable.Rows.Count; i++)
                    {
                        // Process only items that are modified.
                        string crud = CommonUtility.GetCRUDFromName(grdData.DataTable.Rows[i][HEADER_WS_NM].ToString());
                        if (!Constants.WS_INITIAL.Equals(crud))
                        {
                            CargoJobItem item = (CargoJobItem)m_arrGridData[i];
                            item.repkgTypeCd = grdData.DataTable.Rows[i][HEADER_PKGTYPE].ToString();
                            item.hatchNo = grdData.DataTable.Rows[i][HEADER_HATCH].ToString();
                            item.tsptTpCd = grdData.DataTable.Rows[i][HEADER_OPRMODE].ToString();
                            item.fnlOpeYn = grdData.DataTable.Rows[i][HEADER_FINAL].ToString();
                            item.workStDt = grdData.DataTable.Rows[i][HEADER_ST_DATE].ToString();
                            item.workEndDt = grdData.DataTable.Rows[i][HEADER_ED_DATE].ToString();
                            item.CRUD = crud;

                            item.opeClassCd = CommonUtility.ToString(item.opeClassCd);
                            item.jobGroup = CommonUtility.ToString(item.jobGroup);
                            item.jobPurpCd = CommonUtility.ToString(item.jobPurpCd);
                            item.jobTpCd = CommonUtility.ToString(item.jobTpCd);
                            item.delvTpCd = CommonUtility.ToString(item.delvTpCd);
                            item.fnlOpeYn = CommonUtility.ToString(item.fnlOpeYn);
                            item.rhdlMode = CommonUtility.ToString(item.rhdlMode);

                            arrObj.Add(item);
                        }
                    }
                }

                if (arrObj.Count > 0)
                {
                    Object[] obj = arrObj.ToArray();
                    DataItemCollection dataCollection = new DataItemCollection();
                    dataCollection.collection = obj;

                    proxy.processCargoJobItem(dataCollection);
                    result = true;
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
            return result;
        }

        private void UpdateItem()
        {
            if (grdData.CurrentRowIndex > -1 && grdData.CurrentRowIndex < grdData.DataTable.Rows.Count)
            {
                // Set Readonly False to update column data.
                DataTable dataTable = grdData.DataTable;
                int colCnt = dataTable.Columns.Count;
                for (int i = 0; i < colCnt; i++)
                {
                    dataTable.Columns[i].ReadOnly = false;
                }

                // Update columns data
                DataRow row = grdData.DataTable.Rows[grdData.CurrentRowIndex];
                if (Constants.ITEM_OLD.Equals(grdData.DataTable.Rows[grdData.CurrentRowIndex][HEADER_ITEM_STATUS].ToString()))
                {
                    row[HEADER_WS_NM] = Constants.WS_NM_UPDATE;
                }
                row[HEADER_PKGTYPE] = txtPkgTp.Text;
                row[HEADER_ST_DATE] = txtStartTime.Text;
                row[HEADER_ED_DATE] = txtEndTime.Text;
                row[HEADER_OPRMODE] = CommonUtility.GetComboboxSelectedValue(cboOPRMode);
                row[HEADER_HATCH] = CommonUtility.GetComboboxSelectedValue(cboHatch);
                row[HEADER_FINAL] = chkFinal.Checked ? "Y" : "N";
                grdData.DataTable.AcceptChanges();
                grdData.Refresh();

                // Reset original Readonly
                for (int i = 0; i < colCnt; i++)
                {
                    dataTable.Columns[i].ReadOnly = true;
                }
            }
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnOk":
                    if (grdData.IsDirty)
                    {
                        if (ProcessCargoJobItem())
                        {
                            ClearForm();
                            GetCargoJobList();
                            CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
                        }
                    }
                    else
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                    }
                    break;

                case "btnCancel":
                    if (grdData.IsDirty)
                    {
                        DialogResult dr = CommonUtility.ConfirmMsgSaveChances();
                        if (dr == DialogResult.Yes)
                        {
                            ProcessCargoJobItem();
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            this.Close();
                        }
                        else if (dr == DialogResult.No)
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

                case "btnUpdate":
                    if (this.validations(this.pnlUpdate.Controls) && ValidateOnUpdate())
                    {
                        UpdateItem();
                    }
                    break;

                case "btnF1":
                    GRBLListParm grParm = new GRBLListParm();
                    grParm.CgNo = txtCargo.Text;
                    grParm.VslCallId = this.m_vslCallId;
                    GRBLListResult grResultTmp = (GRBLListResult)PopupManager.instance.ShowPopup(new HCM114(), grParm);
                    if (grResultTmp != null)
                    {
                        txtCargo.Text = grResultTmp.CgNo;
                        m_vslCallId = grResultTmp.VslCallId;
                    }
                    break;

                case "btnF2":
                    PartnerCodeListParm pkgTypeParm = new PartnerCodeListParm();
                    pkgTypeParm.Option = "CD";
                    pkgTypeParm.SearchItem = txtPkgTp.Text;
                    PartnerCodeListResult pkgTypeRes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_PKGTYPE), pkgTypeParm);
                    if (pkgTypeRes != null)
                    {
                        txtPkgTp.Text = pkgTypeRes.Code;
                    }
                    break;

                case "btnRetrieve":
                    if (this.validations(this.pnlRetrieve.Controls))
                    {
                        //GetCargoJobList();
                        initializeCboPaging();
                    }
                    break;
            }
        }

        private void grdData_CurrentCellChanged(object sender, EventArgs e)
        {
            int index = grdData.CurrentRowIndex;
            if (index > -1 && index < grdData.DataTable.Rows.Count)
            {
                string strPkgTp = grdData.DataTable.Rows[index][HEADER_PKGTYPE].ToString();
                string strHatch = grdData.DataTable.Rows[index][HEADER_HATCH].ToString();
                string strOprMode = grdData.DataTable.Rows[index][HEADER_OPRMODE].ToString();
                string strStartTime = grdData.DataTable.Rows[index][HEADER_ST_DATE].ToString();
                string strEndTime = grdData.DataTable.Rows[index][HEADER_ED_DATE].ToString();

                txtPkgTp.Text = strPkgTp;
                CommonUtility.SetComboboxSelectedItem(cboHatch, strHatch);
                CommonUtility.SetComboboxSelectedItem(cboOPRMode, strOprMode);
                CommonUtility.SetDTPValueDMYHM(txtStartTime, strStartTime);
                CommonUtility.SetDTPValueDMYHM(txtEndTime, strEndTime);
                SetFinalCheckbox();

                // Disable UPDATE, DELETE buttons if this row was marked as deleted.
                string strWorkingStatus = grdData.DataTable.Rows[index][HEADER_WS_NM].ToString();
                btnUpdate.Enabled = !Constants.WS_NM_DELETE.Equals(strWorkingStatus);
            }
        }

        private void SetFinalCheckbox()
        {
            int index = grdData.CurrentRowIndex;
            if (m_arrGridData != null && -1 < index && index < m_arrGridData.Count)
            {
                CargoJobItem item = (CargoJobItem)m_arrGridData[index];

                string strFinal = item.fnlOpeYn;
                string strJobPurpCd = item.jobPurpCd;
                string strEndtime = item.workEndDt;

                if ("Y".Equals(strFinal) || "true".Equals(strFinal))
                {
                    chkFinal.Checked = true;
                }
                else
                {
                    chkFinal.Checked = false;
                }

                if (string.IsNullOrEmpty(strEndtime))
                {
                    chkFinal.Enabled = false;
                }
                else
                {

                    if ("GV".Equals(strJobPurpCd) || "VG".Equals(strJobPurpCd))
                    {
                        chkFinal.Enabled = true;
                    }
                    else
                    {
                        chkFinal.Enabled = false;
                        if ("VW".Equals(strJobPurpCd) ||
                            "GW".Equals(strJobPurpCd) ||
                            "WG".Equals(strJobPurpCd) ||
                            "WV".Equals(strJobPurpCd) ||
                            "AV".Equals(strJobPurpCd) ||
                            "WA".Equals(strJobPurpCd))
                        {
                            chkFinal.Enabled = true;
                        }
                    }
                }
            }
        }

        private bool ValidateOnUpdate()
        {
            if (!ValidateReconciliationExists())
            {
                return false;
            }

            // Validate PkgTp
            if (!CommonUtility.IsValidPkgTp(txtPkgTp.Text.Trim()))
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0070"));
                txtPkgTp.SelectAll();
                txtPkgTp.Focus();
                return false;
            }

            int currIndex = grdData.CurrentRowIndex;
            if (-1 < currIndex && currIndex < m_arrGridData.Count)
            {
                CargoJobItem item = (CargoJobItem)m_arrGridData[currIndex];

                if (("VA".Equals(item.jobPurpCd) && "Y".Equals(item.jobRootYn)) ||
                    ("VA".Equals(item.jobPurpCd) && "Y".Equals(item.jobRootYn)) ||
                    ("GA".Equals(item.jobPurpCd) && "Y".Equals(item.jobRootYn)))
                {
                    CommonUtility.AlertMessage("This cargo can not update because of location. If you want to modify, you have to re-set location.");
                    return false;
                }

                return true;
            }

            if (!CommonUtility.CheckDateStartEnd(txtStartTime, txtEndTime))
            {
                return false;
            }

            return false;
        }

        private bool ValidateReconciliationExists()
        {
            int currIndex = grdData.CurrentRowIndex;
            if (-1 < currIndex && currIndex < m_arrGridData.Count)
            {
                CargoJobItem item = (CargoJobItem)m_arrGridData[currIndex];
                if ("Y".Equals(item.cudYn))
                {
                    if (item.rcCount <= 0)
                    {
                        CommonUtility.AlertMessage("Cannot delete job which contains reconciliation operations.");
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        private void ClearForm()
        {
            this.IsDirty = false;
        }

        private void txtCargo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //m_vslCallId = string.Empty;
        }

        /**
         * lv.dat add 20130611
         */
        private int getNumbPage(int iNumbRow)
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
            parm = new CargoJobParm();

            parm.searchType = "HHT_cargoJob";
            parm.vslCallId = m_vslCallId;
            parm.cgNo = txtCargo.Text;
            parm.shftId = UserInfo.getInstance().Shift.ToString();
            parm.shftDt = UserInfo.getInstance().Workdate.ToString();

            parm.currentPage = this.iCurrentPage.ToString();
            parm.numbPerPage = this.iNumbPerPage.ToString();
            parm.pageType = "CJ";
        }

        private void initializeCboPaging()
        {
            cboPaging.Items.Clear();
            cboPaging.Text = "";

            initializeParameter();

            IApronCheckerProxy proxy = new ApronCheckerProxy();
            ResponseInfo numbInfo = proxy.getCargoJobNumbPage(parm);
            this.iTotalPage = getNumbPage((int)numbInfo.list[0]);

            for (int i = 1; i <= this.iTotalPage; i++)
                this.cboPaging.Items.Add(i);

            if (this.iTotalPage > 0)
            {
                this.iCurrentPage = 1;
                cboPaging.SelectedIndex = 0;
            }
        }

        private void cboPaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.iCurrentPage = int.Parse(((ComboBox)sender).Text);
            parm.currentPage = this.iCurrentPage.ToString();

            GetCargoJobList();
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

        private void HAC110_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q && e.Modifiers == Keys.Alt)
            {
                MessageBox.Show("Screen ID : " + this.Name + "\n\rScreen caption: " + this.Text, "Screen Information !");
            }
        }
    }
}