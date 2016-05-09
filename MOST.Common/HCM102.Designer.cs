namespace MOST.Common
{
    partial class HCM102
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
            this.btnSearch = new Framework.Controls.UserControls.TButton();
            this.txtDelayCode = new Framework.Controls.UserControls.TTextBox();
            this.lblDelayCode = new Framework.Controls.UserControls.TLabel();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.lstSearch = new Framework.Controls.UserControls.TListBox();
            this.grdDelayCodeList = new Framework.Controls.UserControls.TGrid();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(167, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(65, 24);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtDelayCode
            // 
            this.txtDelayCode.isBusinessItemName = "Delay Code";
            this.txtDelayCode.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtDelayCode.Location = new System.Drawing.Point(75, 6);
            this.txtDelayCode.Name = "txtDelayCode";
            this.txtDelayCode.Size = new System.Drawing.Size(89, 23);
            this.txtDelayCode.TabIndex = 2;
            // 
            // lblDelayCode
            // 
            this.lblDelayCode.Location = new System.Drawing.Point(1, 11);
            this.lblDelayCode.Name = "lblDelayCode";
            this.lblDelayCode.Size = new System.Drawing.Size(73, 18);
            this.lblDelayCode.Text = "Delay Code";
            this.lblDelayCode.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lstSearch
            // 
            this.lstSearch.isBusinessItemName = "";
            this.lstSearch.isMandatory = false;
            this.lstSearch.Location = new System.Drawing.Point(3, 34);
            this.lstSearch.Name = "lstSearch";
            this.lstSearch.Size = new System.Drawing.Size(38, 194);
            this.lstSearch.TabIndex = 1;
            this.lstSearch.SelectedIndexChanged += new System.EventHandler(this.lstSearch_SelectedIndexChanged);
            // 
            // grdDelayCodeList
            // 
            this.grdDelayCodeList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdDelayCodeList.IsDirty = false;
            this.grdDelayCodeList.Location = new System.Drawing.Point(44, 34);
            this.grdDelayCodeList.Name = "grdDelayCodeList";
            this.grdDelayCodeList.RowHeadersVisible = false;
            this.grdDelayCodeList.Size = new System.Drawing.Size(188, 194);
            this.grdDelayCodeList.TabIndex = 4;
            this.grdDelayCodeList.DoubleClick += new System.EventHandler(this.grdDelayCodeList_DoubleClick);
            // 
            // HCM102
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.grdDelayCodeList);
            this.Controls.Add(this.lstSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtDelayCode);
            this.Controls.Add(this.lblDelayCode);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "HCM102";
            this.Text = "Find Delay Code";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HCM102_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TButton btnSearch;
        private Framework.Controls.UserControls.TTextBox txtDelayCode;
        private Framework.Controls.UserControls.TLabel lblDelayCode;
        private Framework.Controls.UserControls.TButton btnCancel;
        private Framework.Controls.UserControls.TButton btnOk;
        private Framework.Controls.UserControls.TGrid grdDelayCodeList;
        private Framework.Controls.UserControls.TListBox lstSearch;

    }
}