using MOST.Common.UserAttribute;

namespace MOST.ApronChecker
{
    partial class HAC111
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
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.btnExit = new Framework.Controls.UserControls.TButton();
            this.cboCatgTp = new Framework.Controls.UserControls.TCombobox();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.cboCgCoCd = new Framework.Controls.UserControls.TCombobox();
            this.btnLoading = new Framework.Controls.UserControls.TButton();
            this.btnRetrieve = new Framework.Controls.UserControls.TButton();
            this.rbtnNonJPVC = new Framework.Controls.UserControls.TRadioButton();
            this.rbtnJPVC = new Framework.Controls.UserControls.TRadioButton();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(2, 94);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(234, 147);
            this.grdData.TabIndex = 6;
            this.grdData.CurrentCellChanged += new System.EventHandler(this.grdData_CurrentCellChanged);
            // 
            // txtJPVC
            // 
            this.txtJPVC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isBusinessItemName = "JPVC";
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.Location = new System.Drawing.Point(116, 3);
            this.txtJPVC.Multiline = true;
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(81, 17);
            this.txtJPVC.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(167, 243);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(65, 24);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.ActionListener);
            // 
            // cboCatgTp
            // 
            this.cboCatgTp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboCatgTp.isBusinessItemName = "Category";
            this.cboCatgTp.isMandatory = false;
            this.cboCatgTp.Location = new System.Drawing.Point(67, 23);
            this.cboCatgTp.Name = "cboCatgTp";
            this.cboCatgTp.Size = new System.Drawing.Size(102, 19);
            this.cboCatgTp.TabIndex = 3;
            // 
            // btnF1
            // 
            this.btnF1.Location = new System.Drawing.Point(199, 2);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(36, 19);
            this.btnF1.TabIndex = 2;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(18, 26);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(48, 16);
            this.tLabel1.Text = "Category";
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(1, 49);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(66, 16);
            this.tLabel2.Text = "Cg Condition";
            // 
            // cboCgCoCd
            // 
            this.cboCgCoCd.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboCgCoCd.isBusinessItemName = "S/N No";
            this.cboCgCoCd.isMandatory = false;
            this.cboCgCoCd.Location = new System.Drawing.Point(67, 46);
            this.cboCgCoCd.Name = "cboCgCoCd";
            this.cboCgCoCd.Size = new System.Drawing.Size(102, 19);
            this.cboCgCoCd.TabIndex = 4;
            // 
            // btnLoading
            // 
            this.btnLoading.Location = new System.Drawing.Point(3, 71);
            this.btnLoading.Name = "btnLoading";
            this.btnLoading.Size = new System.Drawing.Size(88, 21);
            this.btnLoading.TabIndex = 7;
            this.btnLoading.Text = "Loading";
            this.btnLoading.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Location = new System.Drawing.Point(173, 27);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(62, 38);
            this.btnRetrieve.TabIndex = 5;
            this.btnRetrieve.Text = "Retrieve";
            this.btnRetrieve.Click += new System.EventHandler(this.ActionListener);
            // 
            // rbtnNonJPVC
            // 
            this.rbtnNonJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnNonJPVC.isBusinessItemName = "";
            this.rbtnNonJPVC.isMandatory = false;
            this.rbtnNonJPVC.Location = new System.Drawing.Point(1, 3);
            this.rbtnNonJPVC.Name = "rbtnNonJPVC";
            this.rbtnNonJPVC.Size = new System.Drawing.Size(68, 17);
            this.rbtnNonJPVC.TabIndex = 66;
            this.rbtnNonJPVC.Text = "Non-JPVC";
            this.rbtnNonJPVC.CheckedChanged += new System.EventHandler(this.RadiobuttonListener);
            // 
            // rbtnJPVC
            // 
            this.rbtnJPVC.Checked = true;
            this.rbtnJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnJPVC.isBusinessItemName = "";
            this.rbtnJPVC.isMandatory = false;
            this.rbtnJPVC.Location = new System.Drawing.Point(71, 3);
            this.rbtnJPVC.Name = "rbtnJPVC";
            this.rbtnJPVC.Size = new System.Drawing.Size(45, 17);
            this.rbtnJPVC.TabIndex = 67;
            this.rbtnJPVC.Text = "JPVC";
            this.rbtnJPVC.CheckedChanged += new System.EventHandler(this.RadiobuttonListener);
            // 
            // HAC111
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.rbtnNonJPVC);
            this.Controls.Add(this.btnRetrieve);
            this.Controls.Add(this.btnLoading);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.cboCgCoCd);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.cboCatgTp);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.rbtnJPVC);
            this.KeyPreview = true;
            this.Name = "HAC111";
            this.Text = "Rehandle OPR List";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HAC111_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TGrid grdData;
        private Framework.Controls.UserControls.TTextBox txtJPVC;
        private Framework.Controls.UserControls.TCombobox cboCatgTp;
        private Framework.Controls.UserControls.TButton btnExit;

        // tnkytn: for test
        //[AuthAccess("000001", "G/R Search")]
        //[AuthAccess("000002", "JPVC Search")]
        public Framework.Controls.UserControls.TButton btnF1;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        internal Framework.Controls.UserControls.TLabel tLabel2;
        private Framework.Controls.UserControls.TCombobox cboCgCoCd;
        private Framework.Controls.UserControls.TButton btnLoading;
        public Framework.Controls.UserControls.TButton btnRetrieve;
        internal Framework.Controls.UserControls.TRadioButton rbtnNonJPVC;
        internal Framework.Controls.UserControls.TRadioButton rbtnJPVC;
    }
}