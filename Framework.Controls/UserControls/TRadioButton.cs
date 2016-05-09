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
    public partial class TRadioButton : RadioButton, ITransactionControls
    {
        private ValidationError error;
        public TRadioButton()
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

        private Color fColor = Color.LightBlue;
        public Color focusColor
        {
            get { return fColor; }
        }

        public ValidationError validation()
        {
            if (mandatory && this.Visible && this.Enabled && this.Text == null && this.Text.Trim().Equals(string.Empty))
            {
                error = new ValidationError();
                error.ErrorID = "ERR-00001";
                error.ErrorMessage = string.Format("{0} is mandatory item", businessItemName);
                error.RaiseControl = this;

                return error;
            }

            return null;
        }

        #region Clear ControlValue
        public void clearControlValue()
        {
            this.Checked = false;
        }
        #endregion
    }
}
