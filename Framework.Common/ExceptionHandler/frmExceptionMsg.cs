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
            Internationalization();
        }

        #region Internationalization( i18n )
        private void Internationalization()
        {
            this.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0012");
            btnContinue.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0006");
            btnClose.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0007");
        }
        #endregion

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

            if (exception is BusinessException)
            {
                errorMessage = ((BusinessException)exception).ErrorMessage;
                if (errorMessage == null)
                {
                    errorMessage = ((BusinessException)exception).ErrorCode;
                    if (errorMessage != null)
                    {
                        errorMessage = Framework.Common.ResourceManager.MessageResourceManager.getInstance().getString(errorMessage);
                    }
                }
                formRedesign();
            }
            else
            {
                if (exception.InnerException != null && exception.InnerException.Message != null)
                {
                    errorMessage = exception.Message;
                    txtDetailMessage.Text = exception.InnerException.Message;
                }
                else
                {
                    errorMessage = exception.Message;
                    formRedesign();
                }
            }
            if (errorMessage != null)
            {
                lbMessage.Text = errorMessage;
                //lbMessage.Text = String.Format(errorMessage);
            }
            this.Focus();
        }

        private void formRedesign()
        {
            this.Height = 157;
            txtDetailMessage.Visible = false;
            btnClose.Top = 90;
            btnContinue.Top = 90;
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