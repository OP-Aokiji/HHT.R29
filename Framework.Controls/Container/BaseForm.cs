//#define DESKTOP
// tnkytn: for testing on PC

#region Explanation
//* --------------------------------------------------------------
//* CHANGE REVISION
//* --------------------------------------------------------------
//* DATE           AUTHOR      	   REVISION    	     Content
//* 2008-07-02   Mr Luis Lee	     1.0          First release.
//* --------------------------------------------------------------
//* CLASS DESCRIPTION
//* --------------------------------------------------------------
#endregion 

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MOST.Common.UserAttribute;
using System.Reflection;
using Framework.Controls.UserControls;
using System.Collections.Specialized;
using Framework.Controls.ValidationHandler;
using Framework.Common.Helper;
using Framework.Common.UserInformation;
using Framework.Service.Provider.WebService.Provider;
//tnkytn: for test (solution 1)
//using System.Runtime.InteropServices;

namespace Framework.Controls.Container
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
           
            this._isDirty = false;
            this._isClosedByUser = false;

        }

        #region Local Valiable
        private bool _isDirty;          // for determining if form's value(s) is changed or not
        private bool _isClosedByUser;   // for determining if form is closed in code or by clicking X button
        private HybridDictionary errorControls = new HybridDictionary();
        #endregion

        #region Initialize

        public void initialFormSize()
        {
            //tnkytn: for testing on PC
#if DESKTOP
            this.Height = 335;
            this.Width = 250;
#else
                  this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                  this.Width = Screen.PrimaryScreen.WorkingArea.Width;
#endif
        }

        public bool IsDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }
        }
        public bool IsClosedByUser
        {
            get { return _isClosedByUser; }
            set { _isClosedByUser = value; }
        }
        #endregion

        #region Clear Controls Value
        public void clearControlValue(System.Windows.Forms.Control.ControlCollection controls)
        {
            this._isDirty = false;

            foreach (Control ctl in controls)
            {
                if (ctl is IContainerControls)
                {
                    this.clearControlValue(ctl.Controls);
                }
                else if (ctl is IConstraint)
                {
                    IConstraint clearControl = (IConstraint)ctl;
                    clearControl.clearControlValue();
                }
                else
                {
                    if (ctl.Controls.Count > 0)
                    {
                        this.clearControlValue(ctl.Controls);
                    }
                }
            }
        }
        #endregion

        #region Validation Controls
        public bool validations(System.Windows.Forms.Control.ControlCollection controls)
        {
            errorControls = new HybridDictionary();

            this.checkControls(controls);

            if (errorControls.Count > 0)
            {
                displayError();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void checkControls(System.Windows.Forms.Control.ControlCollection controls)
        {
            foreach (Control ctl in controls)
            {
                if (ctl is IContainerControls)
                {
                    this.checkControls(ctl.Controls);
                }

                if (ctl is IConstraint)
                {
                    IConstraint chkControl = (IConstraint)ctl;
                    ValidationError error = chkControl.validation();

                    if (error != null)
                    {
                        errorControls.Add(error.RaiseControl.Name, error);
                    }
                }
            }
        }

        private void displayError()
        {
            frmValidationMsg msg = new frmValidationMsg(errorControls);
            msg.ShowDialog();
        }
        #endregion

        #region Check Authority
        public void authorityCheck()
        {
            Type type = this.GetType();

            AuthAccessAttribute auth;
            foreach (FieldInfo field in type.GetFields())
            {
                foreach (Attribute attr in field.GetCustomAttributes(true))
                {
                    auth = attr as AuthAccessAttribute;

                    if (auth == null)
                    {
                        continue;
                    }

                    foreach (Control ctl in this.Controls)
                    {
                        if (ctl is Framework.Controls.UserControls.TButton && field.Name == ctl.Name)
                        {
                            TButton button = (TButton)ctl;

                            AuthAccessItem item = applyAuthority(auth.ProgramID);
                            if (item != null)
                            {
                                button.Enabled = true;
                            }
                            else
                            {
                                button.Enabled = false;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        private AuthAccessItem applyAuthority(string programId)
        {
            return UserInfo.getInstance().getAccessItem(programId);
        }

        private void mnuAssemblyInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TEST");
        }

        #region Check if form is dirty
        /// <summary>
        /// Recursive function which traverses the Control tree.
        /// Whenever it locates a control that can be classified as an input control, 
        /// it attaches an event handler to know when the form is dirty.
        /// </summary>
        /// <param name="ctrlCollection"></param>
        protected void AddOnChangeHandlerToInputControls(ControlCollection ctrlCollection, List<String> controls)
        {
            foreach (Control subctrl in ctrlCollection)
            {
                if (subctrl is TextBox)
                {
                    if (controls.Contains(subctrl.Name))
                    {
                        ((TextBox)subctrl).TextChanged += new EventHandler(InputControls_OnChange);
                    }
                }
                else if (subctrl is CheckBox)
                {
                    if (controls.Contains(subctrl.Name))
                    {
                        ((CheckBox)subctrl).CheckStateChanged += new EventHandler(InputControls_OnChange);
                    }
                }
                else if (subctrl is RadioButton)
                {
                    if (controls.Contains(subctrl.Name))
                    {
                        ((RadioButton)subctrl).CheckedChanged += new EventHandler(InputControls_OnChange);
                    }
                }
                else if (subctrl is ListBox)
                {
                    if (controls.Contains(subctrl.Name))
                    {
                        ((ListBox)subctrl).SelectedIndexChanged += new EventHandler(InputControls_OnChange);
                    }
                }
                else if (subctrl is ComboBox)
                {
                    if (controls.Contains(subctrl.Name))
                    {
                        ((ComboBox)subctrl).SelectedIndexChanged += new EventHandler(InputControls_OnChange);
                    }
                }
                else if (subctrl is DateTimePicker)
                {
                    if (controls.Contains(subctrl.Name))
                    {
                        //((DateTimePicker)subctrl).TextChanged += new EventHandler(InputControls_OnChange);
                        ((DateTimePicker)subctrl).GotFocus += new EventHandler(InputControls_OnChange);
                    }
                }
                else
                {
                    if (subctrl.Controls.Count > 0)
                    {
                        this.AddOnChangeHandlerToInputControls(subctrl.Controls, controls);
                    }
                }
            }
        }

        /// <summary>
        /// Do something to indicate the form is dirty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputControls_OnChange(object sender, EventArgs e)
        {
            this.IsDirty = true;
        }
        #endregion

        /// <summary>
        /// Using new identifier to hide the Form.ShowDialog() function.
        /// Set form not be dirty when it is open.
        /// </summary>
        /// <returns></returns>
        public new DialogResult ShowDialog()
        {
            this._isDirty = false;
            this._isClosedByUser = false;
            return base.ShowDialog();
        }
    }
}
