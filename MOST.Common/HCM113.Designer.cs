using MOST.Common.UserAttribute;

namespace MOST.Common
{
    partial class HCM113
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSNNo = new Framework.Controls.UserControls.TLabel();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.cboOption = new Framework.Controls.UserControls.TCombobox();
            this.listViewData = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lblSNNo
            // 
            this.lblSNNo.Location = new System.Drawing.Point(7, 7);
            this.lblSNNo.Name = "lblSNNo";
            this.lblSNNo.Size = new System.Drawing.Size(63, 21);
            this.lblSNNo.Text = "Category";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.actionListener);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(95, 240);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.actionListener);
            // 
            // cboOption
            // 
            this.cboOption.isBusinessItemName = "S/N No";
            this.cboOption.isMandatory = true;
            this.cboOption.Location = new System.Drawing.Point(66, 5);
            this.cboOption.Name = "cboOption";
            this.cboOption.Size = new System.Drawing.Size(126, 23);
            this.cboOption.TabIndex = 1;
            this.cboOption.SelectedIndexChanged += new System.EventHandler(this.cboOption_SelectedIndexChanged);
            // 
            // listViewData
            // 
            this.listViewData.CheckBoxes = true;
            this.listViewData.Location = new System.Drawing.Point(66, 34);
            this.listViewData.Name = "listViewData";
            this.listViewData.Size = new System.Drawing.Size(126, 191);
            this.listViewData.TabIndex = 2;
            // 
            // HCM113
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.listViewData);
            this.Controls.Add(this.cboOption);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblSNNo);
            this.MaximizeBox = false;
            this.Menu = null;
            this.MinimizeBox = false;
            this.Name = "HCM113";
            this.Text = "Working Area";
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TLabel lblSNNo;
        private Framework.Controls.UserControls.TCombobox cboOption;
        private Framework.Controls.UserControls.TButton btnCancel;
        private Framework.Controls.UserControls.TButton btnOk;
        private System.Windows.Forms.ListView listViewData;
        
        // tnkytn: for test
        //[AuthAccess("000001", "G/R Search")]
        //[AuthAccess("000002", "JPVC Search")]        
    }
}