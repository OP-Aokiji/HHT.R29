using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MOST.Client.Proxy.WHCheckerProxy;
using MOST.WHChecker.Parm;
using MOST.WHChecker.Result;
using MOST.Common;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.Common.Utility;
using Framework.Service.Provider.WebService.Provider;
using Framework.Common.Constants;
using Framework.Common.PopupManager;
using Framework.Common.UserInformation;
using Framework.Common.ExceptionHandler;

namespace MOST.WHChecker
{
    public partial class HWC101004 : UserControl
    {
        #region Local variables
        public CargoExportResult m_grResultHI;
        public HWC101Parm m_parm;
        public HWC101001Result m_resHILocDmg;
        public bool chkHIGP;
        #endregion

        public HWC101004()
        {
            InitializeComponent();
            InitializeCombobox();
        }

        private void InitializeCombobox()
        {
            CommonUtility.InitializeCombobox(cboHIHndlDmg);
            cboHIHndlDmg.Items.Add(new ComboboxValueDescriptionPair("R", "Return to Shipper"));
            cboHIHndlDmg.Items.Add(new ComboboxValueDescriptionPair("C", "Change Vessel"));
        }

        private void btnHISetLocDmg_Click(object sender, EventArgs e)
        {
            if (IsNotEmpty())
            {
                HWC101001Parm parmDmgLoc = new HWC101001Parm();
                parmDmgLoc.VslCallId = m_grResultHI.VslCallId;
                parmDmgLoc.ShipgNoteNo = m_grResultHI.ShipgNoteNo;
                if (m_resHILocDmg != null && !string.IsNullOrEmpty(m_resHILocDmg.LocId))
                {
                    parmDmgLoc.LocId = m_resHILocDmg.LocId;
                    parmDmgLoc.WhId = m_resHILocDmg.LocId.Substring(0, m_resHILocDmg.LocId.IndexOf("("));
                }
                parmDmgLoc.TotMt = txtHIDmgMT.Text;
                parmDmgLoc.TotM3 = txtHIDmgM3.Text;
                parmDmgLoc.TotQty = txtHIDmgQty.Text;
                parmDmgLoc.CgNo = m_grResultHI.GrNo;
                parmDmgLoc.WhTpCd = "D";    // General: G, Shut-out: S, Damage: D
                //parmDmgLoc.JobNo = m_itemHI.jobNo;

                HWC101001Result resultDmgLoc = (HWC101001Result)PopupManager.instance.ShowPopup(new HWC101001(), parmDmgLoc);
                if (resultDmgLoc != null)
                {
                    m_resHILocDmg = resultDmgLoc;
                    txtHIDmgMT.Text = m_resHILocDmg.TotMt;
                    txtHIDmgM3.Text = m_resHILocDmg.TotM3;
                    txtHIDmgQty.Text = m_resHILocDmg.TotQty;
                    txtHIDmgLoc.Text = m_resHILocDmg.LocId;
                }
            }
            else
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0073"));
                this.txtHIDmgMT.Focus();
            }
        }

        private void cboHIHndlDmg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ("R".Equals(CommonUtility.GetComboboxSelectedValue(cboHIHndlDmg)))
            {
                txtHIDmgLoc.Enabled = false;
                btnHISetLocDmg.Enabled = false;
            }
            else
            {
                txtHIDmgLoc.Enabled = true;
                btnHISetLocDmg.Enabled = true;
            }

            if (cboHIHndlDmg.SelectedIndex > 0 && "R".Equals(CommonUtility.GetComboboxSelectedValue(cboHIHndlDmg)))
                this.chkHIGP = true;
            else
                this.chkHIGP = false;
        }

        /*
         * lv.dat add 20130702
         * check add real number of MT M3 QTY
         */
        private bool IsNotEmpty()
        {
            bool bIsNotEmpty = true;

            int iCount = 0;
            if (string.IsNullOrEmpty(txtHIDmgMT.Text) || "0".Equals(txtHIDmgMT.Text))
                iCount++;
            if (string.IsNullOrEmpty(txtHIDmgM3.Text) || "0".Equals(txtHIDmgM3.Text))
                iCount++;
            if (string.IsNullOrEmpty(txtHIDmgQty.Text) || "0".Equals(txtHIDmgQty.Text))
                iCount++;

            if (iCount == 3)
                bIsNotEmpty = false;

            return bIsNotEmpty;
        }

        private void TextActionListener(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtHIDmgLoc.Text))
                this.txtHIDmgLoc.Text = string.Empty;
        }
    }
}
