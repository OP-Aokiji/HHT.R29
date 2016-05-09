using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using MOST.Common;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using MOST.Common.Utility;
using MOST.ApronChecker.Parm;
using MOST.ApronChecker.Result;
using MOST.WHChecker;
using MOST.WHChecker.Parm;
using MOST.WHChecker.Result;
using MOST.Client.Proxy.WHCheckerProxy;
using MOST.Client.Proxy.CommonProxy;
using MOST.Client.Proxy.ApronCheckerProxy;
using Framework.Common.Constants;
using Framework.Common.PopupManager;
using Framework.Common.ExceptionHandler;
using Framework.Common.UserInformation;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.ApronChecker
{
    public partial class HAC105002 : TForm, IPopupWindow
    {
        #region Local Variable
        private HAC105002Result m_result;
        private HAC105002Parm m_parm;
        private HWC101002Result m_unsetWhDmgResult;
        private HWC101002Result m_unsetSpareResult;
        private bool m_autoDmgLocFlag;
        private bool m_manualDmgLocFlag;
        #endregion

        public HAC105002()
        {
            InitializeComponent();
            this.initialFormSize();

            List<string> controlNames = new List<string>();
            controlNames.Add(txtWHDmgMT.Name);
            controlNames.Add(txtWHDmgM3.Name);
            controlNames.Add(txtWHDmgQty.Name);
            controlNames.Add(txtWHDmgLoc.Name);
            controlNames.Add(txtSpareMT.Name);
            controlNames.Add(txtSpareM3.Name);
            controlNames.Add(txtSpareQty.Name);
            controlNames.Add(txtSpareLoc.Name);
            this.AddOnChangeHandlerToInputControls(this.Controls, controlNames);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HAC105002Parm)parm;
            InitializeData();

            this.ShowDialog();
            return m_result;
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Doc, Bal, Mode, WH Dmg, Spare
                if (m_parm != null)
                {
                    txtDocMT.Text = m_parm.DocMt;
                    txtDocM3.Text = m_parm.DocM3;
                    txtDocQty.Text = m_parm.DocQty;
                    txtBalMT.Text = m_parm.BalMt;
                    txtBalM3.Text = m_parm.BalM3;
                    txtBalQty.Text = m_parm.BalQty;
                    // Delivery Mode
                    if ("D".Equals(m_parm.OpDelvTpCd))
                    {
                        txtDelvMode.Text = "Direct";
                    }
                    else if ("I".Equals(m_parm.OpDelvTpCd))
                    {
                        txtDelvMode.Text = "Indirect";
                    }

                    txtWHDmgBalMt.Text = m_parm.WhDmgBalMt.ToString();
                    txtWHDmgBalM3.Text = m_parm.WhDmgBalM3.ToString();
                    txtWHDmgBalQty.Text = m_parm.WhDmgBalQty.ToString();
                    txtWHDmgMT.Text = "0";
                    txtWHDmgM3.Text = "0";
                    txtWHDmgQty.Text = "0";

                    txtSpareBalMt.Text = m_parm.SprBalMt.ToString();
                    txtSpareBalM3.Text = m_parm.SprBalM3.ToString();
                    txtSpareBalQty.Text = m_parm.SprBalQty.ToString();
                    txtSpareMT.Text = "0";
                    txtSpareM3.Text = "0";
                    txtSpareQty.Text = "0";

                    // Others
                    if (m_parm.WhDmgSprRes != null)
                    {
                        m_result = m_parm.WhDmgSprRes;
                        m_unsetWhDmgResult = m_parm.WhDmgSprRes.WhDmgUnsetLocRes;
                        m_unsetSpareResult = m_parm.WhDmgSprRes.SpareUnsetLocRes;

                        // Display data
                        txtWHDmgMT.Text = m_result.WhDmgMt.ToString();
                        txtWHDmgM3.Text = m_result.WhDmgM3.ToString();
                        txtWHDmgQty.Text = m_result.WhDmgQty.ToString();
                        txtSpareMT.Text = m_result.SprMt.ToString();
                        txtSpareM3.Text = m_result.SprM3.ToString();
                        txtSpareQty.Text = m_result.SprQty.ToString();
                        txtWHDmgLoc.Text = m_result.WhDmgLocId;
                        txtSpareLoc.Text = m_result.SprLocId;
                    }
                    if ("S".Equals(m_parm.SpCaCoCd) && "D".Equals(m_parm.OpDelvTpCd))
                    {
                        CheckSpareCargoDirect();
                    }
                }
                #endregion
            }
            catch (Framework.Common.Exception.BusinessException ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void CheckSpareCargoDirect()
        {
            txtWHDmgBalMt.Enabled = false;
            txtWHDmgBalM3.Enabled = false;
            txtWHDmgBalQty.Enabled = false;
            txtWHDmgMT.Enabled = false;
            txtWHDmgM3.Enabled = false;
            txtWHDmgQty.Enabled = false;
            txtWHDmgLoc.Enabled = false;
            btnWHDmgUnset.Enabled = false;
        }

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnConfirm":
                    if (this.IsDirty)
                    {
                        if (this.validations(this.Controls) && Validate())
                        {
                            ReturnInfo();
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            this.Close();
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
                            if (this.validations(this.Controls) && Validate())
                            {
                                ReturnInfo();
                                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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

                case "btnWHDmgUnset":
                    HWC101002Parm unsetWhDmgParm = new HWC101002Parm();
                    unsetWhDmgParm.VslCallId = m_parm.VslCallId;
                    unsetWhDmgParm.CgNo = m_parm.CgNo;
                    unsetWhDmgParm.TotMt = txtWHDmgMT.Text;
                    unsetWhDmgParm.TotM3 = txtWHDmgM3.Text;
                    unsetWhDmgParm.TotQty = txtWHDmgQty.Text;
                    unsetWhDmgParm.IsDamageCg = true;
                    HWC101002Result unsetWhDmgRes = (HWC101002Result)PopupManager.instance.ShowPopup(new HWC101002(), unsetWhDmgParm);
                    if (unsetWhDmgRes != null)
                    {
                        m_unsetWhDmgResult = unsetWhDmgRes;
                        txtWHDmgLoc.Text = m_unsetWhDmgResult.LocId;
                    }
                    break;

                case "btnSpareUnset":
                    HWC101002Parm unsetSpareParm = new HWC101002Parm();
                    unsetSpareParm.VslCallId = m_parm.VslCallId;
                    unsetSpareParm.ShipgNoteNo = m_parm.ShipgNoteNo;
                    unsetSpareParm.CgNo = m_parm.CgNo;
                    unsetSpareParm.TotMt = txtSpareMT.Text;
                    unsetSpareParm.TotM3 = txtSpareM3.Text;
                    unsetSpareParm.TotQty = txtSpareQty.Text;
                    HWC101002Result unsetSpareRes = (HWC101002Result)PopupManager.instance.ShowPopup(new HWC101002(true), unsetSpareParm);
                    if (unsetSpareRes != null)
                    {
                        m_unsetSpareResult = unsetSpareRes;
                        txtSpareLoc.Text = m_unsetSpareResult.LocId;
                    }
                    break;
            }
        }

        private void ReturnInfo()
        {
            m_result = new HAC105002Result();
            m_result.WhDmgUnsetLocRes = m_unsetWhDmgResult;
            m_result.SpareUnsetLocRes = m_unsetSpareResult;
            m_result.WhDmgMt = CommonUtility.ParseDouble(txtWHDmgMT.Text);
            m_result.WhDmgM3 = CommonUtility.ParseDouble(txtWHDmgM3.Text);
            m_result.WhDmgQty = CommonUtility.ParseInt(txtWHDmgQty.Text);
            m_result.WhDmgLocId = txtWHDmgLoc.Text;
            m_result.SprMt = CommonUtility.ParseDouble(txtSpareMT.Text);
            m_result.SprM3 = CommonUtility.ParseDouble(txtSpareM3.Text);
            m_result.SprQty = CommonUtility.ParseInt(txtSpareQty.Text);
            m_result.SprLocId = txtSpareLoc.Text;
            m_result.AutoDmgLocFlag = m_autoDmgLocFlag;
        }

        private bool ValidateAmount()
        {
            // Cargo Type
            bool isBBK = false;
            bool isDBK = false;
            if ("BBK".Equals(m_parm.CgTpCd))
            {
                isBBK = true;
            }
            else if ("DBK".Equals(m_parm.CgTpCd) || "DBE".Equals(m_parm.CgTpCd) || "DBN".Equals(m_parm.CgTpCd))
            {
                isDBK = true;
            }

            // Damage loading
            double dmgBalMt = m_parm.WhDmgBalMt;
            double dmgBalQty = m_parm.WhDmgBalQty;
            double dmgActMt = CommonUtility.ParseDouble(txtWHDmgMT.Text);
            int dmgActQty = CommonUtility.ParseInt(txtWHDmgQty.Text);

            // Spare loading
            double sprBalMt = m_parm.SprBalMt;
            double sprBalQty = m_parm.SprBalQty;
            double sprActMt = CommonUtility.ParseDouble(txtSpareMT.Text);
            int sprActQty = CommonUtility.ParseInt(txtSpareQty.Text);

            if (isBBK)
            {
                // WH Damage
                if (dmgActMt > 0 || dmgActQty > 0)
                {
                    if (dmgBalMt <= 0 && dmgBalQty <= 0)
                    {
                        CommonUtility.AlertMessage("Cannot load WH Damage as its balance is equal or less than 0.");
                        return false;
                    }
                    if (dmgActMt > dmgBalMt || dmgActQty > dmgBalQty)
                    {
                        CommonUtility.AlertMessage("WH Damage amount cannot exceed WH Damage balance.");
                        return false;
                    }
                }

                // Spare
                if (sprActMt > 0 || sprActQty > 0)
                {
                    if (sprBalMt <= 0 && sprBalQty <= 0)
                    {
                        CommonUtility.AlertMessage("Cannot load Spare as its balance is equal or less than 0.");
                        return false;
                    }
                    if (sprActMt > sprBalMt || sprActQty > sprBalQty)
                    {
                        CommonUtility.AlertMessage("Spare amount cannot exceed Spare balance.");
                        return false;
                    }
                }
            }
            else if (isDBK)
            {
                // WH Damage
                if (dmgActMt > 0)
                {
                    if (dmgBalMt <= 0)
                    {
                        CommonUtility.AlertMessage("Cannot load WH Damage as its balance is equal or less than 0.");
                        return false;
                    }
                    if (dmgActMt > dmgBalMt)
                    {
                        CommonUtility.AlertMessage("WH Damage amount cannot exceed WH Damage balance.");
                        return false;
                    }
                }

                // Spare
                if (sprActMt > 0)
                {
                    if (sprBalMt <= 0)
                    {
                        CommonUtility.AlertMessage("Cannot load Spare as its balance is equal or less than 0.");
                        return false;
                    }
                    if (sprActMt > sprBalMt)
                    {
                        CommonUtility.AlertMessage("Spare amount cannot exceed Spare balance.");
                        return false;
                    }
                }
            }

            return true;
        }

        private bool Validate()
        {
            if (!ValidateAmount())
            {
                return false;
            }

            m_autoDmgLocFlag = GetWHDmgAutoDeallocationFlag();
            if (m_manualDmgLocFlag)
            {
                return false;
            }

            if (!ValidateSpareLocation())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get auto deallocation flag of WH Damage cargo.
        /// Return true in case of: 
        /// Location is empty && 
        /// (1 cell partial load || 1 cell full load || 2 cell full load)
        /// </summary>
        /// <returns></returns>
        private bool GetWHDmgAutoDeallocationFlag()
        {
            bool bAutoFlag = false;
            try
            {
                if (string.IsNullOrEmpty(txtWHDmgLoc.Text))
                {
                    bool isBBK = false;
                    bool isDBK = false;
                    double balMt = CommonUtility.ParseDouble(txtWHDmgBalMt.Text);
                    int balQty = CommonUtility.ParseInt(txtWHDmgBalQty.Text);
                    double actMt = CommonUtility.ParseDouble(txtWHDmgMT.Text);
                    int actQty = CommonUtility.ParseInt(txtWHDmgQty.Text);
                    if ("BBK".Equals(m_parm.CgTpCd))
                    {
                        isBBK = true;
                    }
                    else if ("DBK".Equals(m_parm.CgTpCd) || "DBE".Equals(m_parm.CgTpCd) || "DBN".Equals(m_parm.CgTpCd))
                    {
                        isDBK = true;
                    }
                    bool bHasValue = (isBBK && (actMt > 0 || actQty > 0)) || (isDBK && actMt > 0) ? true : false;

                    if (bHasValue)
                    {
                        if (m_parm.LocDmgCount == 1)
                        {
                            bAutoFlag = true;
                        }
                        else if (m_parm.LocDmgCount > 1)
                        {
                            if (isBBK)
                            {
                                bAutoFlag = ((0 < actMt && actMt == balMt && 0 < actQty && actQty == balQty) ||
                                    (0 < actMt && actMt == balMt && actQty == 0) ||
                                    (0 < actQty && actQty == balQty && actMt == 0))
                                    ? true : false;
                            }
                            else if (isDBK)
                            {
                                bAutoFlag = ((0 < actMt && actMt == balMt)) ? true : false;
                            }
                        }

                        if (!bAutoFlag)
                        {
                            m_manualDmgLocFlag = true;
                            CommonUtility.AlertMessage("Please unset location of WH Damage manually.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            return bAutoFlag;
        }

        /// <summary>
        /// Get auto deallocation flag of WH Damage cargo.
        /// Return true in case of: 
        /// Location is empty && 
        /// (1 cell partial load || 1 cell full load || 2 cell full load)
        /// </summary>
        /// <returns></returns>
        private bool ValidateSpareLocation()
        {
            try
            {
                if (string.IsNullOrEmpty(txtSpareLoc.Text))
                {
                    bool isBBK = false;
                    bool isDBK = false;
                    double actMt = CommonUtility.ParseDouble(txtSpareMT.Text);
                    int actQty = CommonUtility.ParseInt(txtSpareQty.Text);
                    if ("BBK".Equals(m_parm.CgTpCd))
                    {
                        isBBK = true;
                    }
                    else if ("DBK".Equals(m_parm.CgTpCd) || "DBE".Equals(m_parm.CgTpCd) || "DBN".Equals(m_parm.CgTpCd))
                    {
                        isDBK = true;
                    }
                    bool bHasValue = (isBBK && (actMt > 0 || actQty > 0)) || (isDBK && actMt > 0) ? true : false;

                    if (bHasValue)
                    {
                        CommonUtility.AlertMessage("Please unset location of Spare cargo.");
                        btnSpareUnset.Focus();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.ErrorHandler(this, ex);
            }
            return true;
        }

        private void txtWHDmgLoc_TextChanged(object sender, EventArgs e)
        {

        }

        private void ClearDmgLoc(object sender, EventArgs e)
        {
            m_unsetWhDmgResult = null;
            txtWHDmgLoc.Text = "";
        }

        private void ClearSpareLoc(object sender, EventArgs e)
        {
            m_unsetSpareResult = null;
            txtSpareLoc.Text = "";
        }

        private void HAC105002_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q && e.Modifiers == Keys.Alt)
            {
                MessageBox.Show("Screen ID : " + this.Name + "\n\rScreen caption: " + this.Text, "Screen Information !");
            }
        }
    }
}