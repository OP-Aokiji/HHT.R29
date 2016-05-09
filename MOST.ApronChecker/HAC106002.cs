using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MOST.WHChecker.Parm;
using MOST.WHChecker.Result;
using MOST.WHChecker;
using MOST.ApronChecker;
using Framework.Common.PopupManager;
using MOST.ApronChecker.Parm;

namespace MOST.Common
{
    public partial class HAC106002 : UserControl
    {
        #region Local variables
        public HAC106Parm m_parm;
        public HWC101001Result m_resLocDmg;
        #endregion
        
        public HAC106002()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.m_resLocDmg = (HWC101001Result)null;
            this.txtDmgLoc.Text = this.txtDmgM3.Text = this.txtDmgMT.Text = this.txtDmgQty.Text = string.Empty;
        }

        private void btnDmgLoc_Click(object sender, EventArgs e)
        {
            HWC101001Parm parmDmgLoc = new HWC101001Parm();
            parmDmgLoc.VslCallId = m_parm.BlInfo.VslCallId;
            if (m_resLocDmg != null && !string.IsNullOrEmpty(m_resLocDmg.LocId))
            {
                parmDmgLoc.LocId = m_resLocDmg.LocId;
                parmDmgLoc.WhId = m_resLocDmg.LocId
                    .Substring(0, m_resLocDmg.LocId.IndexOf("("));
            }

            parmDmgLoc.TotMt = this.txtDmgMT.Text;
            parmDmgLoc.TotM3 = this.txtDmgM3.Text;
            parmDmgLoc.TotQty = this.txtDmgQty.Text;
            parmDmgLoc.CgNo = m_parm.BlInfo.Bl;
            parmDmgLoc.WhTpCd = "D";    // General: G, Shut-out: S, Damge: D

            HWC101001Result resultDmgLoc = (HWC101001Result)PopupManager.instance.ShowPopup(new HWC101001(), parmDmgLoc);
            if (resultDmgLoc != null)
            {
                m_resLocDmg = resultDmgLoc;
                this.txtDmgLoc.Text = m_resLocDmg.LocId;
            }
        }
    }
}
