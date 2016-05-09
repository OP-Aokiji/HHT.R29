using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MOST.Common.CommonResult;
using MOST.Common.CommonParm;
using Framework.Common.PopupManager;
using MOST.Common;
using MOST.Common.Utility;
using Framework.Common.ExceptionHandler;
using MOST.Client.Proxy.ApronCheckerProxy;
using Framework.Service.Provider.WebService.Provider;
using Framework.Common.UserInformation;

namespace MOST.Common
{
    public partial class HCM120001 : UserControl
    {
        public List<string> refNoList;

        public HCM120001()
        {
            InitializeComponent();
            //CommonUtility.InitializeCombobox(this.cbo_RefNo);
        }

        private void RadiobtnCheckedListener(object sender, EventArgs e)
        {
            RadioButton mybutton = (RadioButton)sender;
            String ctrlName = mybutton.Name;

            switch (ctrlName)
            {
                case "rbtn_RefCbo":
                    this.txt_RefNo.Enabled = false;
                    this.cbo_RefNo.Enabled = false;
                    break;
                case "rbtn_RefTxt":
                    this.txt_RefNo.Enabled = false;
                    this.cbo_RefNo.Enabled = false;
                    break;
            }
        }

        public void addToCboRefNo()
        {
            List<string> tempList = new List<string>();
            foreach (string refNo in refNoList)
            {
                if (!String.IsNullOrEmpty(refNo) && !tempList.Contains(refNo))
                {
                    this.cbo_RefNo.Items.Add(new ComboboxValueDescriptionPair(refNo, refNo));
                    tempList.Add(refNo);
                }
            }
        }

        public void addToTxtRefNo()
        {
            this.txt_RefNo.Text = CommonUtility.ParseStringToDate(UserInfo.getInstance().Workdate, "dd/MM/yyyy").ToString("ddMMyyyy") + "@" + UserInfo.getInstance().ShiftNm;
        }
    }
}