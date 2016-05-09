namespace MOST.Common
{
    partial class HCM112
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
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.lblRole = new Framework.Controls.UserControls.TLabel();
            this.cboRole = new Framework.Controls.UserControls.TCombobox();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.DataTable = null;
            this.grdData.Location = new System.Drawing.Point(4, 38);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(228, 195);
            this.grdData.TabIndex = 4;
            this.grdData.DoubleClick += new System.EventHandler(this.grdContractorList_DoubleClick);
            // 
            // lblRole
            // 
            this.lblRole.Location = new System.Drawing.Point(4, 12);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(40, 18);
            this.lblRole.Text = "Type";
            // 
            // cboRole
            // 
            this.cboRole.isBusinessItemName = "";
            this.cboRole.isMandatory = false;
            this.cboRole.Location = new System.Drawing.Point(41, 7);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(173, 23);
            this.cboRole.TabIndex = 2;
            this.cboRole.SelectedIndexChanged += new System.EventHandler(this.cboRole_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(95, 240);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // HCM112
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.cboRole);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.Menu = null;
            this.MinimizeBox = false;
            this.Name = "HCM112";
            this.Text = "Mechanical Equipment";
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TGrid grdData;
        private Framework.Controls.UserControls.TLabel lblRole;
        private Framework.Controls.UserControls.TCombobox cboRole;
        private Framework.Controls.UserControls.TButton btnCancel;
        private Framework.Controls.UserControls.TButton btnOk;

    }
}