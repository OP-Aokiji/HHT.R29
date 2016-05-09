using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using Framework.Common.Constants;
using MOST.Common;
using MOST.Common.Utility;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.VesselOperator.Parm;
using MOST.VesselOperator.Result;
using MOST.Client.Proxy.CommonProxy;
using MOST.Client.Proxy.VesselOperatorProxy;
using Framework.Common.PopupManager;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.VesselOperator
{
    public partial class HVO123 : TForm, IPopupWindow
    {
        private HVO123Parm m_parm;

        public HVO123()
        {
            InitializeComponent();
            this.initialFormSize();

            List<string> controlNames = new List<string>();
            controlNames.Add(cboHatch.Name);
            controlNames.Add(cboEqu.Name);
            controlNames.Add(txtLD20.Name);
            controlNames.Add(txtLD40.Name);
            controlNames.Add(txtDS20.Name);
            controlNames.Add(txtDS40.Name);
            controlNames.Add(txtStartTime.Name);
            controlNames.Add(txtEndTime.Name);
            this.AddOnChangeHandlerToInputControls(this.Controls, controlNames);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HVO123Parm)parm;
            if (m_parm != null && m_parm.JpvcInfo != null)
            {
                txtJPVC.Text = m_parm.JpvcInfo.Jpvc;
            }
            InitializeData();
            this.ShowDialog();
            return null;
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnOk":
                    if (this.IsDirty)
                    {
                        if (this.validations(this.Controls) && this.Validate())
                        {
                            if (ProcessContainerProcessItems())
                            {
                                ClearForm();
                                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0048"));
                            }
                        }
                    }
                    else
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                    }
                    break;

                case "btnCancel":
                    if (this.IsDirty)
                    {
                        DialogResult dr = CommonUtility.ConfirmMsgSaveChances();
                        if (dr == DialogResult.Yes)
                        {
                            if (this.validations(this.Controls) && this.Validate())
                            {
                                ProcessContainerProcessItems();
                                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                                this.Close();
                            }
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

                case "btnList":
                    HVO124Parm listParm = new HVO124Parm();
                    if (m_parm != null && m_parm.JpvcInfo != null)
                    {
                        listParm.JpvcInfo = m_parm.JpvcInfo;
                    }
                    PopupManager.instance.ShowPopup(new HVO124(), listParm);
                    break;
            }
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Initialize data
                CommonUtility.SetDTPValueBlank(txtStartTime);
                CommonUtility.SetDTPValueBlank(txtEndTime);
                #endregion

                #region HatchNo
                CommonUtility.SetHatchInfo(cboHatch);
                #endregion

                #region Equipment
                IVesselOperatorProxy proxy = new VesselOperatorProxy();
                ContainerProcessParm parm = new ContainerProcessParm();
                parm.searchType = "comboList";
                ResponseInfo info = proxy.getContainerProcessList(parm);

                CommonUtility.InitializeCombobox(cboEqu);
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is ContainerProcessItem)
                    {
                        ContainerProcessItem item = (ContainerProcessItem)info.list[i];
                        cboEqu.Items.Add(new ComboboxValueDescriptionPair(item.eqNo, item.eqNm));
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
                Cursor.Current = Cursors.Default;
            }
        }

        private bool ProcessContainerProcessItems()
        {
            bool result = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                IVesselOperatorProxy proxy = new VesselOperatorProxy();

                // ref: CT212
                ContainerProcessItem item = new ContainerProcessItem();
                item.vslCallId = txtJPVC.Text;
                item.workYmd = UserInfo.getInstance().Workdate;
                item.hatchNo = CommonUtility.GetComboboxSelectedValue(cboHatch);
                item.eqNo = CommonUtility.GetComboboxSelectedValue(cboEqu);
                item.shftId = UserInfo.getInstance().Shift;
                item.stDt = txtStartTime.Text;
                item.endDt = txtEndTime.Text;
                item.l20 = txtLD20.Text;
                item.l40 = txtLD40.Text;
                item.d20 = txtDS20.Text;
                item.d40 = txtDS40.Text;
                item.CRUD = Constants.WS_INSERT;

                Object[] obj = new Object[] { item };
                DataItemCollection dataCollection = new DataItemCollection();
                dataCollection.collection = obj;

                proxy.processContainerProcessItems(dataCollection);
                result = true;
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

        private void ClearForm()
        {
            txtLD20.Text = "";
            txtLD40.Text = "";
            txtDS20.Text = "";
            txtDS40.Text = "";
            cboHatch.SelectedIndex = 0;
            cboEqu.SelectedIndex = 0;
            CommonUtility.SetDTPValueBlank(txtStartTime);
            CommonUtility.SetDTPValueBlank(txtEndTime);

            this.IsDirty = false;
        }

        private bool Validate()
        {
            try
            {
                // Validate start, end time
                if (!CommonUtility.CheckDateStartEnd(txtStartTime, txtEndTime))
                {
                    txtStartTime.Focus();
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
    }
}