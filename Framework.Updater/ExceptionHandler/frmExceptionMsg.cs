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
using Framework.Common.Exception;

namespace Framework.Common.ExceptionHandler
{
    public partial class frmExceptionMsg : Form
    {
        private object currentForm = null;
        private System.Exception exception = null;

        public frmExceptionMsg()
        {
            InitializeComponent();
        }

        public frmExceptionMsg(System.Exception e)
        {
            InitializeComponent();

            this.exception = e;

            displayError();
        }

        public frmExceptionMsg(object currentForm, System.Exception e)
        {
            InitializeComponent();

            this.currentForm = currentForm;
            this.exception = e;

            displayError();
        }

        /// <summary>
        /// 
        /// </summary>
        private void displayError()
        {
            string errorMessage = "";
            Cursor.Current = Cursors.Default;

            if (exception is NetworkException)
            {
                errorMessage = ((NetworkException)exception).ErrorMessage;
            }
            else
            {
                errorMessage = exception.Message;
            }

            lbMessage.Text = String.Format(errorMessage);
            this.Focus();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string[] formatMessage(string message)
        {
            return message.Split('\n');
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}