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

namespace MOST.WHChecker
{
    public partial class HWC101006 : UserControl
    {
        public String jPVC;

        public HWC101006()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BLListParm blParm = new BLListParm();
            blParm.Jpvc = this.jPVC;
            blParm.BlNo = this.txtBL.Text.Trim();

            BLListResult blResultTmp = (BLListResult)PopupManager.instance.ShowPopup(new HCM104(), blParm);
            if (blResultTmp != null)
            {
                this.txtBL.Text = blResultTmp.Bl;
            }
        }        
    }
}
