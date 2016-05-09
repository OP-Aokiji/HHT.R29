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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Controls.Container;
using System.Collections.Specialized;
using System.Collections;
using Framework.Controls.UserControls;

namespace Framework.Controls.ValidationHandler
{
    public partial class frmValidationMsg : TDialog
    {
        private HybridDictionary errorControls;
        private IForm form;
        public frmValidationMsg()
        {
            InitializeComponent();
            this.initialFormSize();
        }

        public frmValidationMsg(IForm form, HybridDictionary errorControls)
        {
            InitializeComponent();

            this.form = form;
            this.errorControls = errorControls;

            displayError();
        }

        public frmValidationMsg(HybridDictionary errorControls)
        {
            InitializeComponent();
            this.errorControls = errorControls;

            displayError();
        }

        public void displayError(){
            if (errorControls != null)
            {
                ValidationError error;
                foreach (DictionaryEntry de in errorControls)
                {
                    error = (ValidationError)de.Value;

                    if (error.RaiseControl is TTextBox)
                    {
                        TTextBox textBox = (TTextBox)error.RaiseControl;
                        if (textBox.isBusinessItemName.Length == 0)
                        {
                            lstErrorControls.Items.Add(textBox.Name);
                        }
                        else
                        {
                            lstErrorControls.Items.Add(error.ErrorMessage);
                        }
                    }
                    else
                    {
                        lstErrorControls.Items.Add(error.ErrorMessage);
                    }
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstErrorControls_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectControl = lstErrorControls.Text;

            ValidationError error;
            foreach (DictionaryEntry de in errorControls)
            {
                error = (ValidationError)de.Value;
                if (error.ErrorMessage.Equals(selectControl))
                {
                    //form.focusControl(error.RaiseControl);
                    break;
                }
            }
        }
    }
}