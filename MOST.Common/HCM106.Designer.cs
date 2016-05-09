namespace MOST.Common
{
    partial class HCM106
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
            this.grdContractorList = new Framework.Controls.UserControls.TGrid();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.lblRole = new Framework.Controls.UserControls.TLabel();
            this.cboRole = new Framework.Controls.UserControls.TCombobox();
            this.SuspendLayout();
            // 
            // grdContractorList
            // 
            this.grdContractorList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdContractorList.DataTable = null;
            this.grdContractorList.Location = new System.Drawing.Point(4, 38);
            this.grdContractorList.Name = "grdContractorList";
            this.grdContractorList.RowHeadersVisible = false;
            this.grdContractorList.Size = new System.Drawing.Size(228, 195);
            this.grdContractorList.TabIndex = 4;
            this.grdContractorList.DoubleClick += new System.EventHandler(this.grdContractorList_DoubleClick);
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
            // lblType
            // 
            this.lblRole.Location = new System.Drawing.Point(1, 11);
            this.lblRole.Name = "lblType";
            this.lblRole.Size = new System.Drawing.Size(32, 18);
            this.lblRole.Text = "Role";
            // 
            // cboType
            // 
            this.cboRole.isBusinessItemName = "";
            this.cboRole.isMandatory = false;
            this.cboRole.Location = new System.Drawing.Point(30, 7);
            this.cboRole.Name = "cboType";
            this.cboRole.Size = new System.Drawing.Size(202, 23);
            this.cboRole.TabIndex = 2;
            this.cboRole.SelectedIndexChanged += new System.EventHandler(this.cboRole_SelectedIndexChanged);
            // 
            // HCM106
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.cboRole);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.grdContractorList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.Menu = null;
            this.MinimizeBox = false;
            this.Name = "HCM106";
            this.Text = "Contractor List";
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TGrid grdContractorList;
        private Framework.Controls.UserControls.TButton btnCancel;
        private Framework.Controls.UserControls.TButton btnOk;
        private Framework.Controls.UserControls.TLabel lblRole;
        private Framework.Controls.UserControls.TCombobox cboRole;

    }
}