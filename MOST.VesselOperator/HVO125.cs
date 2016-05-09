using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Framework.Common.Constants;
using Framework.Common.ExceptionHandler;
using Framework.Common.PopupManager;
using Framework.Common.UserInformation;
using Framework.Controls.Container;
using Framework.Service.Provider.WebService.Provider;
using MOST.Client.Proxy.VesselOperatorProxy;
using MOST.Common;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.Common.Utility;
using MOST.VesselOperator.Parm;

namespace MOST.VesselOperator
{
    public partial class HVO125 : TForm, IPopupWindow
    {
        private const string HEADER_STATUS = "Status";
        private const string HEADER_DOC_VOL = "Doc Vol";
        private const string HEADER_DOC_TIME = "Doc Time";
        private const string HEADER_DOC_LOC = "Doc Loc";
        private const string HEADER_ACT_VOL = "Act Vol";
        private const string HEADER_ACT_TIME = "Act Time";
        private const string HEADER_ACT_COMMODITY = "Act Commodity";

        private HVO125Parm m_parm;
        private ArrayList m_arrGridData;
        private int m_decNum = 1;

        public HVO125()
        {
            InitializeComponent();
            InitializeDataGrid();
        }

        private void InitializeDataGrid()
        {
            String[,] header = { { HEADER_STATUS, "40" }, { HEADER_DOC_VOL, "40" }, { HEADER_DOC_TIME, "80" }, { HEADER_DOC_LOC, "40" }, { HEADER_ACT_VOL, "40" }, { HEADER_ACT_TIME, "80" }, { HEADER_ACT_COMMODITY, "90" }};
            this.grdData.setHeader(header);
        }

        private void InitializeData()
        {
            m_arrGridData = new ArrayList();

            //Initialize Record Date
            CommonUtility.SetDTPValueDMYHM(txtRecDate, CommonUtility.GetCurrentServerTime());
            CommonUtility.SetDTPValueBlank(tdFrom);
            CommonUtility.SetDTPValueBlank(tdTo);

            //Initialize JPVC Nool
            if (m_parm != null && m_parm.JpvcInfo != null)
            {
                rbtnJPVC.Checked = true;
                txtJPVC.Text = m_parm.JpvcInfo.Jpvc;
                txtJPVCName.Text = m_parm.JpvcInfo.VesselName;
                //Initialize Data Grid
                GetServiceOrderList();
            } else
            {
                rbtnNonJPVC.Checked = true;
            }
        }

         
        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HVO125Parm)parm;
            InitializeData();
            this.ShowDialog();
            return null;
        }

        private void GetServiceOrderList()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Grid Data
                IVesselOperatorProxy soProxy = new VesselOperatorProxy();

                ServiceOrderParm soParm = new ServiceOrderParm();
                if (rbtnJPVC.Checked == true)
                {
                    soParm.vslCallId = txtJPVC.Text;
                } else
                {
                    soParm.vslCallId = Constants.NONCALLID;
                }
                soParm.svcDtFm = tdFrom.Text;
                soParm.svcDtTo = tdTo.Text;
                soParm.searchType = "bunkeringList";

                ResponseInfo soInfo = soProxy.getServiceOrderList(soParm);

                grdData.Clear();
                m_arrGridData.Clear();
                if (soInfo != null)
                {
                    for (int i = 0; i < soInfo.list.Length; i++)
                    {
                        if (soInfo.list[i] is ServiceOrderItem)
                        {
                            ServiceOrderItem item = (ServiceOrderItem) soInfo.list[i];
                            DataRow newRow = grdData.NewRow();
                            newRow[HEADER_STATUS] = item.statNm;
                            newRow[HEADER_DOC_VOL] = item.unit;
                            newRow[HEADER_DOC_TIME] = item.svcDtFm;
                            newRow[HEADER_DOC_LOC] = item.locId;
                            newRow[HEADER_ACT_VOL] = item.comUnit;
                            newRow[HEADER_ACT_TIME] = item.comSvcDtFm;
                            newRow[HEADER_ACT_COMMODITY] = item.comCmdtyCd;
 
                            grdData.Add(newRow);
                            m_arrGridData.Add(item);
                            if (!String.IsNullOrEmpty(item.unitDec))
                            {
                                m_decNum = CommonUtility.ParseInt(item.unitDec);
                            }
                        }
                    }
                }
                #endregion
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

        private void SetGridColReadOnly(bool isReadOnly)
        {
            DataTable dataTable = grdData.DataTable;
            int colCnt = dataTable.Columns.Count;
            for (int i = 0; i < colCnt; i++)
            {
                dataTable.Columns[i].ReadOnly = isReadOnly;
            }
        }

        private bool CheckDecimal()
        {
            string[] arrTmp = txtRecVol.Text.Split('.');
            if (arrTmp != null && arrTmp.Length > 1)
            {
                string decimalNumber = arrTmp[1];
                if (!string.IsNullOrEmpty(decimalNumber) && decimalNumber.Length > m_decNum) 
                {
                    return false;
                }
            }
            return true;
        }

        private void UpdateGridRow()
        {
            int index = grdData.CurrentRowIndex;
            if (!CheckDecimal())
            {
                CommonUtility.AlertMessage("Record Amount must have less than " + m_decNum + " decimal number(s)");
                return;
            }
            if (index > -1 && index < grdData.DataTable.Rows.Count)
            {
                SetGridColReadOnly(false);
                grdData.DataTable.Rows[index][HEADER_ACT_VOL] = txtRecVol.Text.ToString();
                grdData.DataTable.Rows[index][HEADER_ACT_TIME] = txtRecDate.Text.ToString();
                grdData.DataTable.Rows[index][HEADER_ACT_COMMODITY] = txtRecCom.Text.ToString();
                grdData.IsDirty = true;
                grdData.DataTable.AcceptChanges();
                grdData.Refresh();

              
                // Update array list
                ServiceOrderItem item = (ServiceOrderItem)m_arrGridData[index];
                item.comUnit = grdData.DataTable.Rows[index][HEADER_ACT_VOL].ToString();
                item.comSvcDtFm = grdData.DataTable.Rows[index][HEADER_ACT_TIME].ToString();
                item.comCmdtyCd = grdData.DataTable.Rows[index][HEADER_ACT_COMMODITY].ToString();
                item.shftId = UserInfo.getInstance().Shift;
                item.statCd = "CP";
                item.CRUD = Constants.WS_UPDATE;
                SetGridColReadOnly(true);
            }
        }

        private bool ProcessServiceOrderCUD()
        {
            bool result = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                System.Collections.ArrayList arrObj = new System.Collections.ArrayList();

                if (m_arrGridData != null)
                {
                    for (int i = 0; i < m_arrGridData.Count; i++)
                    {
                        ServiceOrderItem item = (ServiceOrderItem)m_arrGridData[i];
                        if (Constants.WS_UPDATE.Equals(item.CRUD))
                        {
                            item.comShftId = "";    //to let server automatically fill shiftId
                            item.comChk = "Y";      //to check completion
                            arrObj.Add(item);
                        }
                    }
                }

                if (arrObj.Count > 0)
                {
                    Object[] obj = arrObj.ToArray();
                    DataItemCollection dataCollection = new DataItemCollection();
                    dataCollection.collection = obj;

                    proxy.processServiceOrderCUD(dataCollection);
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
        
        private void btnOK_Click()
        {
            if (grdData.IsDirty)
            {
                if (ProcessServiceOrderCUD())
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
                    GetServiceOrderList();
                }
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click()
        {
            if (grdData.IsDirty)
            {
                DialogResult dr = CommonUtility.ConfirmMsgSaveChances();
                if (dr == DialogResult.Yes)
                {
                    btnOK_Click();
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
        }

        private void FindCommodityCode()
        {
            PartnerCodeListParm cmdtParm = new PartnerCodeListParm();
            cmdtParm.Option = "CD";
            cmdtParm.SearchItem = txtRecCom.Text;
            PartnerCodeListResult cmdtRes = (PartnerCodeListResult)PopupManager.instance.ShowPopup(new HCM110(HCM110.TYPE_COMMODITY), cmdtParm);
            if (cmdtRes != null)
            {
                txtRecCom.Text = cmdtRes.Code;
            }
        }

        private void FindJPVC()
        {
            MOST.Common.CommonParm.SearchJPVCParm jpvcParm = new MOST.Common.CommonParm.SearchJPVCParm();
            jpvcParm.Jpvc = txtJPVC.Text.Trim();

            SearchJPVCResult jpvcResult = (SearchJPVCResult)PopupManager.instance.ShowPopup(new HCM101(), jpvcParm);
            if (jpvcResult != null)
            {
                txtJPVC.Text = jpvcResult.Jpvc;
                txtJPVCName.Text = jpvcResult.VesselName;
            } else
            {
                txtJPVC.Text = string.Empty;
                txtJPVCName.Text = string.Empty;
            }
        }

        private void Clear()
        {
            txtRecVol.Text = string.Empty;
            CommonUtility.SetDTPValueBlank(txtRecDate);
            txtRecCom.Text = string.Empty;
        }

        private void CheckRadioJpvc()
        {
            if (rbtnJPVC.Checked)
            {
                txtJPVC.Enabled = true;
                btnF2.Enabled = true;
            } else
            {
                txtJPVC.Enabled = false;
                btnF2.Enabled = false;
            }
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnF1":
                    FindCommodityCode();
                    break;
                case "btnF2":
                    FindJPVC();
                    break;
                case "btnRetrieve":
                    GetServiceOrderList();
                    break;
                case "btnUpdate":
                    
                    UpdateGridRow();
                    break;
                case "btnOk":
                    btnOK_Click();
                    break;
                case "btnCancel":
                    btnCancel_Click();
                    break;
                case "btnDelete":
                    Clear();
                    break;
            }
        }

        private void grdData_CurrentCellChanged(object sender, EventArgs e)
        {
            int index = grdData.CurrentRowIndex;
            if (index > -1 && index < grdData.DataTable.Rows.Count)
            {
                txtReqVol.Text = grdData.DataTable.Rows[index][HEADER_DOC_VOL].ToString();
                txtReqDate.Text = grdData.DataTable.Rows[index][HEADER_DOC_TIME].ToString();
                txtReqLoc.Text = grdData.DataTable.Rows[index][HEADER_DOC_LOC].ToString();
                txtRecVol.Text = grdData.DataTable.Rows[index][HEADER_ACT_VOL].ToString();
                CommonUtility.SetDTPValueDMYHM(txtRecDate, grdData.DataTable.Rows[index][HEADER_ACT_TIME].ToString());
                txtRecCom.Text = grdData.DataTable.Rows[index][HEADER_ACT_COMMODITY].ToString();
                if (m_arrGridData != null && index < m_arrGridData.Count)
                {
                    ServiceOrderItem item = (ServiceOrderItem) m_arrGridData[index];
                    lblReqTit.Text = item.unitTit;
                    lblReqUom.Text = item.unitUom;
                    lblRecTit.Text = item.unitTit;
                    lblRecUom.Text = item.unitUom;
                }
            }
        }

        private void RadiobuttonListener(object sender, EventArgs e)
        {
            CheckRadioJpvc();
        }

        private void txtJPVC_KeyPress(object sender, KeyPressEventArgs e)
        {
            SearchJPVCResult m_jpvcResult = new SearchJPVCResult();
            txtJPVCName.Text = string.Empty;
            // if key = Enter then get vessel name
            if (e.KeyChar.Equals((char)Keys.Enter))
            {
                if (CommonUtility.IsValidJPVC(txtJPVC.Text, ref m_jpvcResult))
                {
                    txtJPVCName.Text = m_jpvcResult.VesselName;
                }
            }
        }

        private void txtJPVC_TextChanged(object sender, EventArgs e)
        {
            txtJPVCName.Text = string.Empty;
        }
       
    }
}