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
    public partial class HWC101003 : UserControl
    {
        #region Local variables
        public CargoExportResult m_grResultHI;
        public HWC101Parm m_parm;
        public HWC101001Result m_resHILocSOut;
        public bool chkHIGP;
        #endregion

        public HWC101003()
        {
            InitializeComponent();
            InitializeCombobox();
        }

        private void InitializeCombobox()
        {
            CommonUtility.InitializeCombobox(cboHIHndlSOut);
            cboHIHndlSOut.Items.Add(new ComboboxValueDescriptionPair("R", "Return to Shipper"));
            cboHIHndlSOut.Items.Add(new ComboboxValueDescriptionPair("C", "Change Vessel"));
        }

        private void btnHISetLocSOut_Click(object sender, EventArgs e)
        {
            if (IsNotEmpty())
            {
                HWC101001Parm parmShutLoc = new HWC101001Parm();
                parmShutLoc.VslCallId = m_grResultHI.VslCallId;
                parmShutLoc.ShipgNoteNo = m_grResultHI.ShipgNoteNo;
                if (m_resHILocSOut != null && !string.IsNullOrEmpty(m_resHILocSOut.LocId))
                {
                    parmShutLoc.LocId = m_resHILocSOut.LocId;
                    parmShutLoc.WhId = m_resHILocSOut.LocId.Substring(0, m_resHILocSOut.LocId.IndexOf("("));
                }
                parmShutLoc.TotMt = txtHISOutMT.Text;
                parmShutLoc.TotM3 = txtHISOutM3.Text;
                parmShutLoc.TotQty = txtHISOutQty.Text;
                parmShutLoc.CgNo = m_grResultHI.GrNo;
                parmShutLoc.WhTpCd = "S";    // General: G, Shut-out: S, Damage: D
                //parmShutLoc.JobNo = m_itemHI.jobNo;

                HWC101001Result resultShutLoc = (HWC101001Result)PopupManager.instance.ShowPopup(new HWC101001(), parmShutLoc);
                if (resultShutLoc != null)
                {
                    m_resHILocSOut = resultShutLoc;
                    txtHISOutMT.Text = m_resHILocSOut.TotMt;
                    txtHISOutM3.Text = m_resHILocSOut.TotM3;
                    txtHISOutQty.Text = m_resHILocSOut.TotQty;
                    txtHIShutLoc.Text = m_resHILocSOut.LocId;
                }
            }
            else
            {
                CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0073"));
                this.txtHISOutMT.Focus();
            }
        }

        private void cboHIHndlSOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ("R".Equals(CommonUtility.GetComboboxSelectedValue(cboHIHndlSOut)))
            {
                txtHIShutLoc.Enabled = false;
                btnHISetLocSOut.Enabled = false;
            }
            else
            {
                txtHIShutLoc.Enabled = true;
                btnHISetLocSOut.Enabled = true;
            }

            if(cboHIHndlSOut.SelectedIndex > 0 && "R".Equals(CommonUtility.GetComboboxSelectedValue(cboHIHndlSOut)))
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
            if (string.IsNullOrEmpty(txtHISOutMT.Text) || "0".Equals(txtHISOutMT.Text))
                iCount++;
            if (string.IsNullOrEmpty(txtHISOutM3.Text) || "0".Equals(txtHISOutM3.Text))
                iCount++;
            if (string.IsNullOrEmpty(txtHISOutQty.Text) || "0".Equals(txtHISOutQty.Text))
                iCount++;

            if (iCount == 3)
                bIsNotEmpty = false;

            return bIsNotEmpty;
        }

        private void TextActionListener(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtHIShutLoc.Text))
                this.txtHIShutLoc.Text = string.Empty;
        }
    }
}
