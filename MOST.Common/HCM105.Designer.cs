namespace MOST.Common
{
    partial class HCM105
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.grdDriverList = new Framework.Controls.UserControls.TGrid();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.btnSearch = new Framework.Controls.UserControls.TButton();
            this.txtStaffName = new Framework.Controls.UserControls.TTextBox();
            this.lblStaffName = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
            // 
            // grdDriverList
            // 
            this.grdDriverList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdDriverList.IsDirty = false;
            this.grdDriverList.Location = new System.Drawing.Point(7, 35);
            this.grdDriverList.Name = "grdDriverList";
            this.grdDriverList.RowHeadersVisible = false;
            this.grdDriverList.Size = new System.Drawing.Size(225, 195);
            this.grdDriverList.TabIndex = 3;
            this.grdDriverList.DoubleClick += new System.EventHandler(this.grdDriverList_DoubleClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(95, 240);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(168, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(65, 24);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtStaffName
            // 
            this.txtStaffName.isBusinessItemName = "Staff Name";
            this.txtStaffName.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtStaffName.Location = new System.Drawing.Point(69, 6);
            this.txtStaffName.Name = "txtStaffName";
            this.txtStaffName.Size = new System.Drawing.Size(95, 23);
            this.txtStaffName.TabIndex = 1;
            // 
            // lblStaffName
            // 
            this.lblStaffName.Location = new System.Drawing.Point(0, 11);
            this.lblStaffName.Name = "lblStaffName";
            this.lblStaffName.Size = new System.Drawing.Size(73, 17);
            this.lblStaffName.Text = "Staff Name";
            // 
            // HCM105
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtStaffName);
            this.Controls.Add(this.lblStaffName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grdDriverList);
            this.Name = "HCM105";
            this.Text = "JPB Driver List";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HCM105_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TGrid grdDriverList;
        private Framework.Controls.UserControls.TButton btnCancel;
        private Framework.Controls.UserControls.TButton btnOk;
        private Framework.Controls.UserControls.TButton btnSearch;
        private Framework.Controls.UserControls.TTextBox txtStaffName;
        private Framework.Controls.UserControls.TLabel lblStaffName;

    }
}