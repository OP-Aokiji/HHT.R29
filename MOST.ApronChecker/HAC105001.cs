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
    public partial class HAC105001 : TForm, IPopupWindow
    {
        #region Local Variable
        private bool m_isDirty;
        private HAC105001Result m_result;
        private HAC105001Parm m_parm;
        private HWC101001Result m_resLocDmg;
        private HWC101001Result m_resLocShut;
        #endregion

        public HAC105001()
        {
            InitializeComponent();
            this.initialFormSize();

            List<string> controlNames = new List<string>();
            controlNames.Add(txtShutMT.Name);
            controlNames.Add(txtShutM3.Name);
            controlNames.Add(txtShutQty.Name);
            controlNames.Add(cboRehandleShut.Name);
            controlNames.Add(txtShutLoc.Name);
            controlNames.Add(txtDmgMT.Name);
            controlNames.Add(txtDmgM3.Name);
            controlNames.Add(txtDmgQty.Name);
            controlNames.Add(cboRehandleDmg.Name);
            controlNames.Add(txtDmgLoc.Name);
            controlNames.Add(chkGP.Name);
            this.AddOnChangeHandlerToInputControls(this.Controls, controlNames);
        }

        public IPopupResult ShowPopup(IPopupParm parm)
        {
            m_parm = (HAC105001Parm)parm;
            InitializeData();

            this.ShowDialog();
            return m_result;
        }

        private void InitializeData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                #region Title
                string strTitle = "A/C - LD Cancel";
                if (m_parm != null)
                {
                    strTitle = strTitle + " - " + m_parm.CgNo;
                }
                this.Text = strTitle;
                #endregion


                CommonUtility.InitializeCombobox(cboRehandleShut);
                cboRehandleShut.Items.Add(new ComboboxValueDescriptionPair("R", "Return to Shipper"));
                cboRehandleShut.Items.Add(new ComboboxValueDescriptionPair("C", "Change Vessel"));

                CommonUtility.InitializeCombobox(cboRehandleDmg);
                cboRehandleDmg.Items.Add(new ComboboxValueDescriptionPair("R", "Return to Shipper"));
                cboRehandleDmg.Items.Add(new ComboboxValueDescriptionPair("C", "Change Vessel"));
                

                #region Doc, Bal, Mode
                if (m_parm != null)
                {
                    // Doc, Balance
                    txtDocMT.Text = m_parm.DocMt;
                    txtDocM3.Text = m_parm.DocM3;
                    txtDocQty.Text = m_parm.DocQty;
                    txtBalMT.Text = m_parm.BalMt.ToString();
                    txtBalM3.Text = m_parm.BalM3.ToString();
                    txtBalQty.Text = m_parm.BalQty.ToString();

                    // Delivery Mode
                    if ("D".Equals(m_parm.OpDelvTpCd))
                    {
                        txtDelvMode.Text = "DIRECT";
                    }
                    else if ("I".Equals(m_parm.OpDelvTpCd))
                    {
                        txtDelvMode.Text = "INDIRECT";
                    }

                    // Others
                    if (m_parm.LdCnclResult != null)
                    {
                        m_result = m_parm.LdCnclResult;

                        // Display data
                        m_resLocDmg = m_parm.LdCnclResult.DmgSetLocRes;
                        m_resLocShut = m_parm.LdCnclResult.ShutSetLocRes;
                        txtDmgMT.Text = m_result.DmgMt;
                        txtDmgM3.Text = m_result.DmgM3;
                        txtDmgQty.Text = m_result.DmgQty;
                        txtDmgLoc.Text = m_result.DmgLocId;
                        txtShutMT.Text = m_result.ShuMt;
                        txtShutM3.Text = m_result.ShuM3;
                        txtShutQty.Text = m_result.ShuQty;
                        txtShutLoc.Text = m_result.ShuLocId;
                        txtRemark.Text = m_result.Remark;
                        CommonUtility.SetComboboxSelectedItem(cboRehandleShut, m_result.ShutRhdlMode);
                        CommonUtility.SetComboboxSelectedItem(cboRehandleDmg, m_result.DmgRhdlMode);
                        chkGP.Checked = "Y".Equals(m_result.GatePassYn);

                        m_isDirty = true;
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

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName)
            {
                case "btnConfirm":
                    if ((this.IsDirty || this.m_isDirty))
                    {
                        if (this.validations(this.Controls) && ValidateAmount())
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
                            if (this.validations(this.Controls) && ValidateAmount())
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

                case "btnShutLoc":
                    HWC101001Parm parmShutLoc = new HWC101001Parm();
                    parmShutLoc.VslCallId = m_parm.VslCallId;
                    parmShutLoc.CgNo = m_parm.CgNo;
                    parmShutLoc.ShipgNoteNo = m_parm.ShipgNoteNo;
                    parmShutLoc.TotMt = txtShutMT.Text;
                    parmShutLoc.TotM3 = txtShutM3.Text;
                    parmShutLoc.TotQty = txtShutQty.Text;
                    parmShutLoc.WhTpCd = "S";    // General: G, Shut-out: S, Damge: D

                    HWC101001Result resultShutLoc = (HWC101001Result)PopupManager.instance.ShowPopup(new HWC101001(true), parmShutLoc);
                    if (resultShutLoc != null)
                    {
                        m_resLocShut = resultShutLoc;
                        txtShutLoc.Text = m_resLocShut.LocId;
                    }
                    break;

                case "btnDmgLoc":
                    HWC101001Parm parmDmgLoc = new HWC101001Parm();
                    parmDmgLoc.VslCallId = m_parm.VslCallId;
                    parmDmgLoc.CgNo = m_parm.CgNo;
                    parmDmgLoc.ShipgNoteNo = m_parm.ShipgNoteNo;
                    parmDmgLoc.TotMt = txtDmgMT.Text;
                    parmDmgLoc.TotM3 = txtDmgM3.Text;
                    parmDmgLoc.TotQty = txtDmgQty.Text;
                    parmDmgLoc.WhTpCd = "D";    // General: G, Shut-out: S, Damage: D

                    HWC101001Result resultDmgLoc = (HWC101001Result)PopupManager.instance.ShowPopup(new HWC101001(true), parmDmgLoc);
                    if (resultDmgLoc != null)
                    {
                        m_resLocDmg = resultDmgLoc;
                        txtDmgLoc.Text = m_resLocDmg.LocId;
                    }
                    break;
            }
        }

        private void CheckboxGPListener(object sender, EventArgs e)
        {
            ComboBox mycombobox = (ComboBox)sender;
            String cboName = mycombobox.Name;
            switch (cboName)
            {
                case "cboRehandleShut":
                    // Shut-out
                    if ("R".Equals(CommonUtility.GetComboboxSelectedValue(cboRehandleShut)))
                    {
                        txtShutLoc.Enabled = false;
                        btnShutLoc.Enabled = false;
                    }
                    else
                    {
                        txtShutLoc.Enabled = true;
                        btnShutLoc.Enabled = true;
                    }
                    break;

                case "cboRehandleDmg":
                    // Damage
                    if ("R".Equals(CommonUtility.GetComboboxSelectedValue(cboRehandleDmg)))
                    {
                        txtDmgLoc.Enabled = false;
                        btnDmgLoc.Enabled = false;
                    }
                    else
                    {
                        txtDmgLoc.Enabled = true;
                        btnDmgLoc.Enabled = true;
                    }
                    break;
            }

            if ((cboRehandleDmg.SelectedIndex > 0 && "R".Equals(CommonUtility.GetComboboxSelectedValue(cboRehandleDmg))) ||
                (cboRehandleShut.SelectedIndex > 0 && "R".Equals(CommonUtility.GetComboboxSelectedValue(cboRehandleShut))))
            {
                chkGP.Checked = true;
            }
            else
            {
                chkGP.Checked = false;
            }
        }

        private void ReturnInfo()
        {
            if (m_result == null)
            {
                m_result = new HAC105001Result();
            }

            m_result.DmgSetLocRes = m_resLocDmg;
            m_result.ShutSetLocRes = m_resLocShut;
            m_result.DmgMt = txtDmgMT.Text;
            m_result.DmgM3 = txtDmgM3.Text;
            m_result.DmgQty = txtDmgQty.Text;
            m_result.DmgLocId = txtDmgLoc.Text;
            m_result.ShuMt = txtShutMT.Text;
            m_result.ShuM3 = txtShutM3.Text;
            m_result.ShuQty = txtShutQty.Text;
            m_result.ShuLocId = txtShutLoc.Text;
            m_result.ShutRhdlMode = CommonUtility.GetComboboxSelectedValue(cboRehandleShut);
            m_result.DmgRhdlMode = CommonUtility.GetComboboxSelectedValue(cboRehandleDmg);
            m_result.GatePassYn = chkGP.Checked ? "Y" : "N";
            m_result.Remark = txtRemark.Text;
        }

        /// <summary>
        /// Validate inputted amount.
        /// Direct case: No validation needed.
        /// Indirect case: check if inputted amount is less or equal to balance amount.
        /// </summary>
        /// <returns></returns>
        private bool ValidateAmount()
        {   
            // Delivery Mode
            bool isIndirect = false;
            if ("D".Equals(m_parm.OpDelvTpCd))
            {
                return true;
            }
            else if ("I".Equals(m_parm.OpDelvTpCd))
            {
                isIndirect = true;
            }

            if (isIndirect)
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

                // LD Cancel balance
                double balMt = m_parm.BalMt;
                int balQty = m_parm.BalQty;

                // Shut-out
                double shuActMt = CommonUtility.ParseDouble(txtShutMT.Text);
                int shuActQty = CommonUtility.ParseInt(txtShutQty.Text);

                // Damage
                double dmgActMt = CommonUtility.ParseDouble(txtDmgMT.Text);
                int dmgActQty = CommonUtility.ParseInt(txtDmgQty.Text);

                // Total LD Cancel amount
                double actMt = shuActMt + dmgActMt;
                double actQty = shuActQty + dmgActQty;


                if (isBBK)
                {
                    if (balMt <= 0 && balQty <= 0)
                    {
                        CommonUtility.AlertMessage("Cannot input Loading Cancel because balance amount is less than 0.");
                        return false;
                    }
                    if (actMt > balMt || actQty > balQty)
                    {
                        CommonUtility.AlertMessage("Loading Cancel amount cannot exceed balance amount.");
                        return false;
                    }
                }
                else if (isDBK)
                {
                    if (balMt <= 0)
                    {
                        CommonUtility.AlertMessage("Cannot input Loading Cancel because balance amount is less than 0.");
                        return false;
                    }
                    if (actMt > balMt)
                    {
                        CommonUtility.AlertMessage("Loading Cancel amount cannot exceed balance amount.");
                        return false;
                    }
                }
            }

            return true;
        }

        private void ClearShutLoc(object sender, EventArgs e)
        {
            m_resLocShut = null;
            txtShutLoc.Text = "";
        }

        private void ClearDmgLoc(object sender, EventArgs e)
        {
            m_resLocDmg = null;
            txtDmgLoc.Text = "";
        }

        private void HAC105001_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q && e.Modifiers == Keys.Alt)
            {
                MessageBox.Show("Screen ID : " + this.Name + "\n\rScreen caption: " + this.Text, "Screen Information !");
            }
        }
    }
}