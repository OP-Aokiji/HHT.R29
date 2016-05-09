namespace MOST.Common
{
    partial class HCM110
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
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.grdList = new Framework.Controls.UserControls.TGrid();
            this.btnSearch = new Framework.Controls.UserControls.TButton();
            this.txtSearch = new Framework.Controls.UserControls.TTextBox();
            this.cboOption = new Framework.Controls.UserControls.TCombobox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(95, 240);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
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
            // grdList
            // 
            this.grdList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdList.Location = new System.Drawing.Point(7, 35);
            this.grdList.Name = "grdList";
            this.grdList.RowHeadersVisible = false;
            this.grdList.Size = new System.Drawing.Size(225, 195);
            this.grdList.TabIndex = 4;
            this.grdList.DoubleClick += new System.EventHandler(this.grdList_DoubleClick);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(166, 7);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(65, 24);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.isBusinessItemName = "Search Condition";
            this.txtSearch.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtSearch.Location = new System.Drawing.Point(68, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(95, 23);
            this.txtSearch.TabIndex = 2;
            // 
            // cboOption
            // 
            this.cboOption.isBusinessItemName = "Option";
            this.cboOption.isMandatory = false;
            this.cboOption.Location = new System.Drawing.Point(2, 8);
            this.cboOption.Name = "cboOption";
            this.cboOption.Size = new System.Drawing.Size(62, 23);
            this.cboOption.TabIndex = 1;
            // 
            // HCM110
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.cboOption);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.Menu = null;
            this.MinimizeBox = false;
            this.Name = "HCM110";
            this.Text = "";
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TButton btnOk;
        private Framework.Controls.UserControls.TButton btnCancel;
        private Framework.Controls.UserControls.TGrid grdList;
        private Framework.Controls.UserControls.TButton btnSearch;
        private Framework.Controls.UserControls.TTextBox txtSearch;
        private Framework.Controls.UserControls.TCombobox cboOption;
    }
}