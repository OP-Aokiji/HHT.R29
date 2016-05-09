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

namespace Framework.Encrypter
{
    public partial class frmEncrypter : Form
    {
        public frmEncrypter()
        {
            InitializeComponent();
        }

        private void txtInputText_TextChanged(object sender, EventArgs e)
        {
            if (txtInputText.Text.Length > 0)
            {
                btnEncrypt.Enabled = true;
            }
            else
            {
                btnEncrypt.Enabled = false;
            }
        }

        private void txtEncryptText_TextChanged(object sender, EventArgs e)
        {
            if (txtEncryptText.Text.Length > 0)
            {
                btnDecrypt.Enabled = true;
            }
            else
            {
                btnDecrypt.Enabled = false;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEncryptText.Text = "";
            txtInputText.Text = "";
            txtInputText.Focus();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (txtInputText.Text.Length > 0)
            {
                txtEncryptText.Text = Security.Encrypt(txtInputText.Text);
            }
            else
            {
                MessageBox.Show("Please Input Text");
                txtInputText.Focus();
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if(txtEncryptText.Text.Length > 0){
                txtInputText.Text = Security.Decrypt(txtEncryptText.Text);
            }
        }
    }
}