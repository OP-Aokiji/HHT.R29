#region Explanation
//* --------------------------------------------------------------
//* CHANGE REVISION
//* --------------------------------------------------------------
//* DATE           AUTHOR      	   REVISION    	     Content
//* 2008-05-20   Mr Luis Lee	     1.0          First release.
//* --------------------------------------------------------------
//* CLASS DESCRIPTION
//* --------------------------------------------------------------
#endregion 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Framework.Controls;
using Framework.Controls.ValidationHandler;

namespace Framework.Controls.UserControls
{
    public partial class TCombobox : ComboBox, ITransactionControls, IConstraint
    {
        private ValidationError error;

        public TCombobox()
        {
            InitializeComponent();
        }

        private string businessItemName = string.Empty;
        public string isBusinessItemName
        {
            get { return businessItemName; }
            set { businessItemName = value; }
        }

        private bool mandatory = false;
        [DefaultValue(false)]
        public bool isMandatory
        {
            get { return mandatory; }
            set { mandatory = value; }
        }

        private Color fColor = Framework.Common.Constants.Constants.DefaultColor;
        private Color orignalColor;
        public Color focusColor
        {
            get { return fColor; }
        }

        #region Control Validation Check
        public ValidationError validation()
        {
            if (mandatory && this.Visible && this.Enabled && (this.Text == null || this.Text.Trim().Equals(string.Empty) || "Select".Equals(this.Text)))
            {
                error = new ValidationError();
                error.ErrorID = "ERR-00001";
                error.ErrorMessage = string.Format("{0} is a mandatory item", businessItemName);
                error.RaiseControl = this;

                return error;
            }

            return null;
        }
        private void makeError()
        {
            this.BackColor = Framework.Common.Constants.Constants.DefaultColor;
        }
        #endregion

        #region Clear ControlValue
        public void clearControlValue()
        {
            this.SelectedIndex = -1;
        }
        #endregion

        private void TCombobox_GotFocus(object sender, EventArgs e)
        {
            orignalColor = this.BackColor;
            this.BackColor = fColor;
        }

        private void TCombobox_LostFocus(object sender, EventArgs e)
        {
            if(orignalColor != null){
                this.BackColor = orignalColor;
            }
        }
    }
}
