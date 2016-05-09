using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using Framework.Common.Constants;
using Framework.Common.PopupManager;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using Framework.Controls.UserControls;
using MOST.Common.Utility;
using MOST.Common.CommonResult;
using MOST.ApronChecker.Parm;
using MOST.ApronChecker.Result;
using MOST.Client.Proxy.CommonProxy;
using MOST.Client.Proxy.ApronCheckerProxy;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.ApronChecker
{
    public partial class HAC104 : TForm, IPopupWindow
    {
        public HAC104()
        {
            InitializeComponent();
            this.initialFormSize();
            CommonUtility.SetDTPValueBlank(txtStartTime);
            CommonUtility.SetDTPValueBlank(txtEndTime);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            //this.m_parm = (HAC102Parm)parm;
            InitializeData();
            this.ShowDialog();
            return null;
        }

        private void actionListener(object sender, EventArgs e)
        {
            Button _mybutton = (Button)sender;
            String _buttonName = _mybutton.Name;
            switch (_buttonName)
            {
                case "btnOk":
                    break;

                case "btnCancel":
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    break;

                case "btnF1":
                    break;
            }
        }

        private void InitializeData()
        {
            cboSFType.Items.Add("S1(Same Level)");
            cboSFType.Items.Add("S2(Diff. Level)");
            cboSFType.Items.Add("S3(Hatch to Hatch)");
            cboSFType.Items.Add("S4(Hatch to Wharf)");
        }

        private void HAC104_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q && e.Modifiers == Keys.Alt)
            {
                MessageBox.Show("Screen ID : " + this.Name + "\n\rScreen caption: " + this.Text, "Screen Information !");
            }
        }
    }
}