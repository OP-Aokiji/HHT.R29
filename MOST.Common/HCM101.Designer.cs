namespace MOST.Common
{
    partial class HCM101
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
            this.grdJPVCList = new Framework.Controls.UserControls.TGrid();
            this.btnSearch = new Framework.Controls.UserControls.TButton();
            this.txtVslCdNm = new Framework.Controls.UserControls.TTextBox();
            this.lbJPVC = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
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
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grdJPVCList
            // 
            this.grdJPVCList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdJPVCList.IsDirty = false;
            this.grdJPVCList.Location = new System.Drawing.Point(7, 35);
            this.grdJPVCList.Name = "grdJPVCList";
            this.grdJPVCList.RowHeadersVisible = false;
            this.grdJPVCList.Size = new System.Drawing.Size(225, 195);
            this.grdJPVCList.TabIndex = 3;
            this.grdJPVCList.DoubleClick += new System.EventHandler(this.grdJPVCList_DoubleClick);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(166, 7);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(65, 24);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtVslCdNm
            // 
            this.txtVslCdNm.isBusinessItemName = "Vessel Name";
            this.txtVslCdNm.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtVslCdNm.isMandatory = true;
            this.txtVslCdNm.isSmallestInputLength = 4;
            this.txtVslCdNm.Location = new System.Drawing.Point(50, 8);
            this.txtVslCdNm.Name = "txtVslCdNm";
            this.txtVslCdNm.Size = new System.Drawing.Size(114, 23);
            this.txtVslCdNm.TabIndex = 1;
            // 
            // lbJPVC
            // 
            this.lbJPVC.Location = new System.Drawing.Point(0, 10);
            this.lbJPVC.Name = "lbJPVC";
            this.lbJPVC.Size = new System.Drawing.Size(57, 17);
            this.lbJPVC.Text = "Vessel";
            // 
            // HCM101
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtVslCdNm);
            this.Controls.Add(this.lbJPVC);
            this.Controls.Add(this.grdJPVCList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "HCM101";
            this.Text = "JPVC";
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TButton btnOk;
        private Framework.Controls.UserControls.TButton btnCancel;
        private Framework.Controls.UserControls.TGrid grdJPVCList;
        private Framework.Controls.UserControls.TButton btnSearch;
        private Framework.Controls.UserControls.TTextBox txtVslCdNm;
        private Framework.Controls.UserControls.TLabel lbJPVC;
    }
}