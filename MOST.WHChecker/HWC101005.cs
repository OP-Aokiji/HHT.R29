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
    public partial class HWC101005 : UserControl
    {
        public String jPVC;
        public CargoExportResult grResultTmp;

        public HWC101005()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GRListParm grParm = new GRListParm();
            grParm.Jpvc = this.jPVC;
            grParm.GrNo = this.txtGR.Text.Trim();

            grParm.Screenid = "WC103";
            this.grResultTmp = null;
            this.grResultTmp = (CargoExportResult)PopupManager.instance.ShowPopup(new HCM103(), grParm);

            if (this.grResultTmp != null)
            {
                this.txtGR.Text = this.grResultTmp.GrNo;
            }
        }
    }
}
