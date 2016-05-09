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
using MOST.Common.Utility;
using Framework.Controls.UserControls;
using Framework.Common.ExceptionHandler;

namespace MOST.Common
{
    public partial class HCM113 : TDialog, IPopupWindow
    {
        #region Local Variable
        public const string AREA_WHO = "WHO";
        public const string AREA_HATCH = "HTC";
        public const string AREA_WHARF = "WRF";

        public const string AREA_LOCCD_WHS = "WHS"; //Weigh Scale
		public const string AREA_LOCCD_HATCH = "HTC";
		public const string AREA_LOCCD_WHARF = "WRF";
		public const string AREA_LOCCD_EDIBLE = "EDJ";
		public const string AREA_LOCCD_NON_EDIBLE = "NDJ";
		public const string AREA_LOCCD_OTHERS = "OTH";
        public const string LOC_CD = "BBT";

        public const int TYPE_MANPOWER = 1;    // Requester POPUP
        public const int TYPE_STEVEDORE = 2;    // Requester POPUP
        public const int TYPE_FORKLIFT = 3;    // Commodity POPUP

        private int m_type;
        private WorkingAreaResult m_result;
        #endregion

        public HCM113(int type)
        {
            m_type = type;
            InitializeComponent();
            InitializeForm();
            this.initialFormSize();
            InitializeTitle();
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            InitializeOption();
            this.ShowDialog();
            return m_result;
        }

        private void InitializeForm()
        {
            btnOk.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0013");
            btnCancel.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0014");
            this.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM113_0001");
            lblSNNo.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM113_0002");
            cboOption.isBusinessItemName = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM113_0002");

            // Set the view to show details.
            listViewData.View = View.Details;
            // Display check boxes.
            listViewData.CheckBoxes = true;
            // Select the item and subitems when selection is made.
            listViewData.FullRowSelect = true;
            // Create columns for the items and subitems.
            listViewData.Columns.Add(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM113_0003"), -2, HorizontalAlignment.Left);
        }

        private void InitializeTitle()
        {
            // Set title
            switch (m_type)
            {
                case HCM113.TYPE_MANPOWER:
                case HCM113.TYPE_STEVEDORE:
                    this.Text = "Working Area - ManPower";
                    break;

                case HCM113.TYPE_FORKLIFT:
                    this.Text = "Working Area - Forklift";
                    break;
            }
        }

        private void InitializeOption()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                CommonUtility.InitializeCombobox(cboOption, "Select");
                switch (m_type)
                {
                    case HCM113.TYPE_MANPOWER:
                    case HCM113.TYPE_STEVEDORE:
                        ICommonProxy proxy = new CommonProxy();
                        CommonCodeParm commonParm = new CommonCodeParm();
                        commonParm.searchType = "COMM";
                        commonParm.lcd = "MT";
                        commonParm.divCd = "LOCDIV1";
                        ResponseInfo info = proxy.getCommonCodeList(commonParm);

                        for (int i = 0; i < info.list.Length; i++)
                        {
                            if (info.list[i] is CodeMasterListItem)
                            {
                                CodeMasterListItem item = (CodeMasterListItem)info.list[i];
                                cboOption.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                            }
                            else if (info.list[i] is CodeMasterListItem1)
                            {
                                CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                                cboOption.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                            }
                        }
                        break;

                    case HCM113.TYPE_FORKLIFT:
                        cboOption.Items.Add(new ComboboxValueDescriptionPair(HCM113.AREA_HATCH, "Hatch"));
                        cboOption.Items.Add(new ComboboxValueDescriptionPair(HCM113.AREA_WHARF, "Bulk Wharf"));
                        cboOption.Items.Add(new ComboboxValueDescriptionPair(HCM113.AREA_WHO, "Warehouse"));
                        break;
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
        }

        private void F_Search()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ICommonProxy proxy = new CommonProxy();
                ResponseInfo info;
                listViewData.Items.Clear();

                switch (m_type)
                {
                    case HCM113.TYPE_MANPOWER:
                    case HCM113.TYPE_STEVEDORE:
                        CheckListVSRParm parm = new CheckListVSRParm();
                        String divCd = CommonUtility.GetComboboxSelectedValue(cboOption);

                        if (AREA_LOCCD_HATCH.Equals(divCd))
                        {
                            info = SearchHatchLocList(proxy, divCd);
                        } else if (AREA_LOCCD_WHARF.Equals(divCd) || AREA_LOCCD_EDIBLE.Equals(divCd) || AREA_LOCCD_NON_EDIBLE.Equals(divCd))
                        {
                            info = SearchBerthLocList(proxy, divCd);
                        } else if (AREA_LOCCD_WHS.Equals(divCd))
                        {
                            info = SearchWhsLocList(proxy, divCd);
                        } else if (AREA_LOCCD_OTHERS.Equals(divCd))
                        {
                            info = SearchOthersList(proxy, divCd);
                        } else
                        {
                            info = SearchLocDefList(proxy, divCd);
                        }
                        
                        for (int i = 0; i < info.list.Length; i++)
                        {
                            if (info.list[i] is LocationCodeItem)
                            {
                                LocationCodeItem item = (LocationCodeItem)info.list[i];
                                listViewData.Items.Add(new ListViewItem(item.cd));
                            }
                            else if (info.list[i] is CodeMasterListItem)
                            {
                                CodeMasterListItem item = (CodeMasterListItem)info.list[i];
                                listViewData.Items.Add(new ListViewItem(item.scd));
                            }
                            else if (info.list[i] is CodeMasterListItem1)
                            {
                                CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                                listViewData.Items.Add(new ListViewItem(item.scd));
                            }
                        }
                        listViewData.Refresh();
                        break;

                    case HCM113.TYPE_FORKLIFT:
                        string category = CommonUtility.GetComboboxSelectedValue(cboOption);

                        if (AREA_HATCH.Equals(category))
                        {
                            CommonCodeParm comCodeParm = new CommonCodeParm();
                            comCodeParm.searchType = "COMM";
                            comCodeParm.lcd = "MT";
                            comCodeParm.divCd = "HTC";
                            info = proxy.getCommonCodeList(comCodeParm);
                            for (int i = 0; i < info.list.Length; i++)
                            {
                                if (info.list[i] is CodeMasterListItem)
                                {
                                    CodeMasterListItem item = (CodeMasterListItem)info.list[i];
                                    listViewData.Items.Add(new ListViewItem(item.scd));
                                }
                                else if (info.list[i] is CodeMasterListItem1)
                                {
                                    CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                                    listViewData.Items.Add(new ListViewItem(item.scd));
                                }
                            }
                        }
                        else if (AREA_WHARF.Equals(category))
                        {
                            info = SearchBerthLocList(proxy, "WRF");
                            for (int i = 0; i < info.list.Length; i++)
                            {
                                if (info.list[i] is LocationCodeItem)
                                {
                                    LocationCodeItem item = (LocationCodeItem)info.list[i];
                                    listViewData.Items.Add(new ListViewItem(item.cd));
                                }
                                else if (info.list[i] is CodeMasterListItem)
                                {
                                    CodeMasterListItem item = (CodeMasterListItem)info.list[i];
                                    listViewData.Items.Add(new ListViewItem(item.scd));
                                }
                                else if (info.list[i] is CodeMasterListItem1)
                                {
                                    CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                                    listViewData.Items.Add(new ListViewItem(item.scd));
                                }
                            }
                            /**
                             * lv.dat add 20130621
                             * add hatch to wharft view
                             */ 
                            CommonCodeParm comCodeParm = new CommonCodeParm();
                            comCodeParm.searchType = "COMM";
                            comCodeParm.lcd = "MT";
                            comCodeParm.divCd = "HTC";
                            info = proxy.getCommonCodeList(comCodeParm);
                            for (int i = 0; i < info.list.Length; i++)
                            {
                                if (info.list[i] is CodeMasterListItem)
                                {
                                    CodeMasterListItem item = (CodeMasterListItem)info.list[i];
                                    listViewData.Items.Add(new ListViewItem(item.scd));
                                }
                                else if (info.list[i] is CodeMasterListItem1)
                                {
                                    CodeMasterListItem1 item = (CodeMasterListItem1)info.list[i];
                                    listViewData.Items.Add(new ListViewItem(item.scd));
                                }
                            }

                            listViewData.Refresh();
                        }
                        else if (AREA_WHO.Equals(category))
                        {
                            LocationCodeParm locCodeParm = new LocationCodeParm();
                            locCodeParm.searchType = "LocDef";
                            locCodeParm.locDivCd = category;
                            info = proxy.getLocationCodeList(locCodeParm);
                            for (int i = 0; i < info.list.Length; i++)
                            {
                                if (info.list[i] is LocationCodeItem)
                                {
                                    LocationCodeItem item = (LocationCodeItem)info.list[i];
                                    listViewData.Items.Add(new ListViewItem(item.cd));
                                }
                            }
                        }
                        break;
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
        }

        private ResponseInfo SearchLocDefList(ICommonProxy proxy, string divCd)
        {
            LocationCodeParm parm = new LocationCodeParm();
            parm.locDivCd = divCd;
            parm.searchType = "LocDef";
            return proxy.getLocationCodeList(parm);
        }

        private ResponseInfo SearchOthersList(ICommonProxy proxy, string divCd)
        {
            LocationCodeParm parm = new LocationCodeParm();
            parm.locDivCd = divCd;
            parm.searchType = "Others";
            return proxy.getLocationCodeList(parm);
        }

        private ResponseInfo SearchWhsLocList(ICommonProxy proxy, string divCd)
        {
            CommonCodeParm parm = new CommonCodeParm();
            parm.lcd = "MT";
            parm.divCd = divCd;
            parm.searchType = "COMM";
            return proxy.getCommonCodeList(parm);
        }

        private ResponseInfo SearchBerthLocList(ICommonProxy proxy, string divCd)
        {
            LocationCodeParm locParm = new LocationCodeParm();
            locParm.searchType = "BerthLoc";
            locParm.berthTp = divCd;
            locParm.locCd = LOC_CD;
            return proxy.getLocationCodeList(locParm);
        }

        private ResponseInfo SearchHatchLocList(ICommonProxy proxy, string divCd)
        {
            CommonCodeParm parm = new CommonCodeParm();
            parm.lcd = "MT";
            parm.divCd = divCd;
            parm.searchType = "COMM";
            return proxy.getCommonCodeList(parm);
        }

        private void ReturnInfo()
        {
            string strWorkLoc = "";
            if (listViewData != null)
            {
                for (int i = 0; i < listViewData.Items.Count; i++)
                {
                    if (listViewData.Items[i].Checked)
                    {
                        if (!string.IsNullOrEmpty(strWorkLoc))
                        {
                            strWorkLoc = strWorkLoc + "," + listViewData.Items[i].Text;
                        }
                        else
                        {
                            strWorkLoc = strWorkLoc + listViewData.Items[i].Text;
                        }
                    }
                }
            }

            m_result = new WorkingAreaResult();
            m_result.WorkingArea = strWorkLoc;
            m_result.WorkingAreaType = CommonUtility.GetComboboxSelectedValue(cboOption);
        }

        private void cboOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOption != null && cboOption.SelectedIndex > 0)
            {
                F_Search();
            }
        }

        private void actionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnOk":
                    ReturnInfo();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                    break;

                case "btnCancel":
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    break;
            }
        }
    }
}