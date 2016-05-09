namespace MOST.WHChecker
{
    partial class HWC103
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
            this.btnListHIHO = new Framework.Controls.UserControls.TButton();
            this.txtBL = new Framework.Controls.UserControls.TTextBox();
            this.btnF3 = new Framework.Controls.UserControls.TButton();
            this.rbtnBL = new Framework.Controls.UserControls.TRadioButton();
            this.rbtnGR = new Framework.Controls.UserControls.TRadioButton();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.btnF2 = new Framework.Controls.UserControls.TButton();
            this.txtGR = new Framework.Controls.UserControls.TTextBox();
            this.txtJPVCName = new Framework.Controls.UserControls.TTextBox();
            this.btnHI = new Framework.Controls.UserControls.TButton();
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.btnExit = new Framework.Controls.UserControls.TButton();
            this.btnCargoJob = new Framework.Controls.UserControls.TButton();
            this.btnHO = new Framework.Controls.UserControls.TButton();
            this.btnCargoMovement = new Framework.Controls.UserControls.TButton();
            this.btnWHReconcile = new Framework.Controls.UserControls.TButton();
            this.btnUnclosedOPR = new Framework.Controls.UserControls.TButton();
            this.btnRehandleOPR = new Framework.Controls.UserControls.TButton();
            this.pnlJPVC = new Framework.Controls.Container.TPanel();
            this.rbtnNonJPVC = new Framework.Controls.UserControls.TRadioButton();
            this.rbtnJPVC = new Framework.Controls.UserControls.TRadioButton();
            this.tPanel2 = new Framework.Controls.Container.TPanel();
            this.btnGatepass = new Framework.Controls.UserControls.TButton();
            this.btnCheckList = new Framework.Controls.UserControls.TButton();
            this.pnlJPVC.SuspendLayout();
            this.tPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnListHIHO
            // 
            this.btnListHIHO.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnListHIHO.Location = new System.Drawing.Point(3, 191);
            this.btnListHIHO.Name = "btnListHIHO";
            this.btnListHIHO.Size = new System.Drawing.Size(104, 24);
            this.btnListHIHO.TabIndex = 22;
            this.btnListHIHO.Text = "List of HI/HO";
            this.btnListHIHO.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtBL
            // 
            this.txtBL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtBL.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBL.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtBL.Location = new System.Drawing.Point(61, 109);
            this.txtBL.Name = "txtBL";
            this.txtBL.Size = new System.Drawing.Size(108, 19);
            this.txtBL.TabIndex = 12;
            this.txtBL.TextChanged += new System.EventHandler(this.txtBL_TextChanged);
            // 
            // btnF3
            // 
            this.btnF3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF3.Location = new System.Drawing.Point(175, 108);
            this.btnF3.Name = "btnF3";
            this.btnF3.Size = new System.Drawing.Size(56, 20);
            this.btnF3.TabIndex = 13;
            this.btnF3.Text = "F3";
            this.btnF3.Click += new System.EventHandler(this.ActionListener);
            // 
            // rbtnBL
            // 
            this.rbtnBL.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnBL.isBusinessItemName = "";
            this.rbtnBL.isMandatory = false;
            this.rbtnBL.Location = new System.Drawing.Point(2, 27);
            this.rbtnBL.Name = "rbtnBL";
            this.rbtnBL.Size = new System.Drawing.Size(56, 17);
            this.rbtnBL.TabIndex = 11;
            this.rbtnBL.Text = "B/L No";
            this.rbtnBL.CheckedChanged += new System.EventHandler(this.RadiobuttonListener);
            // 
            // rbtnGR
            // 
            this.rbtnGR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnGR.isBusinessItemName = "";
            this.rbtnGR.isMandatory = false;
            this.rbtnGR.Location = new System.Drawing.Point(2, 4);
            this.rbtnGR.Name = "rbtnGR";
            this.rbtnGR.Size = new System.Drawing.Size(56, 17);
            this.rbtnGR.TabIndex = 8;
            this.rbtnGR.Text = "G/R No";
            this.rbtnGR.CheckedChanged += new System.EventHandler(this.RadiobuttonListener);
            // 
            // btnF1
            // 
            this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF1.Location = new System.Drawing.Point(175, 25);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(56, 20);
            this.btnF1.TabIndex = 2;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnF2
            // 
            this.btnF2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF2.Location = new System.Drawing.Point(175, 82);
            this.btnF2.Name = "btnF2";
            this.btnF2.Size = new System.Drawing.Size(56, 20);
            this.btnF2.TabIndex = 10;
            this.btnF2.Text = "F2";
            this.btnF2.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtGR
            // 
            this.txtGR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtGR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtGR.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtGR.Location = new System.Drawing.Point(61, 83);
            this.txtGR.Name = "txtGR";
            this.txtGR.Size = new System.Drawing.Size(108, 19);
            this.txtGR.TabIndex = 9;
            this.txtGR.TextChanged += new System.EventHandler(this.txtGR_TextChanged);
            // 
            // txtJPVCName
            // 
            this.txtJPVCName.Enabled = false;
            this.txtJPVCName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVCName.Location = new System.Drawing.Point(49, 49);
            this.txtJPVCName.Name = "txtJPVCName";
            this.txtJPVCName.Size = new System.Drawing.Size(184, 19);
            this.txtJPVCName.TabIndex = 58;
            // 
            // btnHI
            // 
            this.btnHI.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnHI.Location = new System.Drawing.Point(3, 143);
            this.btnHI.Name = "btnHI";
            this.btnHI.Size = new System.Drawing.Size(52, 24);
            this.btnHI.TabIndex = 20;
            this.btnHI.Text = "H/I";
            this.btnHI.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtJPVC
            // 
            this.txtJPVC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.Location = new System.Drawing.Point(49, 26);
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(120, 19);
            this.txtJPVC.TabIndex = 1;
            this.txtJPVC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJPVC_KeyPress);
            this.txtJPVC.TextChanged += new System.EventHandler(this.txtJPVC_TextChanged);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnExit.Location = new System.Drawing.Point(164, 239);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(72, 24);
            this.btnExit.TabIndex = 24;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCargoJob
            // 
            this.btnCargoJob.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCargoJob.Location = new System.Drawing.Point(3, 215);
            this.btnCargoJob.Name = "btnCargoJob";
            this.btnCargoJob.Size = new System.Drawing.Size(161, 24);
            this.btnCargoJob.TabIndex = 21;
            this.btnCargoJob.Text = "Cargo Job Monitoring";
            this.btnCargoJob.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnHO
            // 
            this.btnHO.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnHO.Location = new System.Drawing.Point(55, 143);
            this.btnHO.Name = "btnHO";
            this.btnHO.Size = new System.Drawing.Size(52, 24);
            this.btnHO.TabIndex = 60;
            this.btnHO.Text = "H/O";
            this.btnHO.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCargoMovement
            // 
            this.btnCargoMovement.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCargoMovement.Location = new System.Drawing.Point(107, 143);
            this.btnCargoMovement.Name = "btnCargoMovement";
            this.btnCargoMovement.Size = new System.Drawing.Size(57, 48);
            this.btnCargoMovement.TabIndex = 62;
            this.btnCargoMovement.Text = "Cargo\r\nMovement";
            this.btnCargoMovement.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnWHReconcile
            // 
            this.btnWHReconcile.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnWHReconcile.Location = new System.Drawing.Point(164, 143);
            this.btnWHReconcile.Name = "btnWHReconcile";
            this.btnWHReconcile.Size = new System.Drawing.Size(72, 48);
            this.btnWHReconcile.TabIndex = 63;
            this.btnWHReconcile.Text = "WH\r\nReconciliation";
            this.btnWHReconcile.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnUnclosedOPR
            // 
            this.btnUnclosedOPR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnUnclosedOPR.Location = new System.Drawing.Point(3, 239);
            this.btnUnclosedOPR.Name = "btnUnclosedOPR";
            this.btnUnclosedOPR.Size = new System.Drawing.Size(161, 24);
            this.btnUnclosedOPR.TabIndex = 65;
            this.btnUnclosedOPR.Text = "Unclosed Operations";
            this.btnUnclosedOPR.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnRehandleOPR
            // 
            this.btnRehandleOPR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnRehandleOPR.Location = new System.Drawing.Point(107, 191);
            this.btnRehandleOPR.Name = "btnRehandleOPR";
            this.btnRehandleOPR.Size = new System.Drawing.Size(129, 24);
            this.btnRehandleOPR.TabIndex = 67;
            this.btnRehandleOPR.Text = "Rehandle OPR";
            this.btnRehandleOPR.Click += new System.EventHandler(this.ActionListener);
            // 
            // pnlJPVC
            // 
            this.pnlJPVC.Controls.Add(this.rbtnNonJPVC);
            this.pnlJPVC.Controls.Add(this.rbtnJPVC);
            this.pnlJPVC.Location = new System.Drawing.Point(1, 2);
            this.pnlJPVC.Name = "pnlJPVC";
            this.pnlJPVC.Size = new System.Drawing.Size(72, 44);
            // 
            // rbtnNonJPVC
            // 
            this.rbtnNonJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnNonJPVC.isBusinessItemName = "";
            this.rbtnNonJPVC.isMandatory = false;
            this.rbtnNonJPVC.Location = new System.Drawing.Point(2, 3);
            this.rbtnNonJPVC.Name = "rbtnNonJPVC";
            this.rbtnNonJPVC.Size = new System.Drawing.Size(68, 17);
            this.rbtnNonJPVC.TabIndex = 64;
            this.rbtnNonJPVC.Text = "Non-JPVC";
            this.rbtnNonJPVC.CheckedChanged += new System.EventHandler(this.RadiobuttonListener);
            // 
            // rbtnJPVC
            // 
            this.rbtnJPVC.Checked = true;
            this.rbtnJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnJPVC.isBusinessItemName = "";
            this.rbtnJPVC.isMandatory = false;
            this.rbtnJPVC.Location = new System.Drawing.Point(2, 26);
            this.rbtnJPVC.Name = "rbtnJPVC";
            this.rbtnJPVC.Size = new System.Drawing.Size(45, 17);
            this.rbtnJPVC.TabIndex = 65;
            this.rbtnJPVC.Text = "JPVC";
            this.rbtnJPVC.CheckedChanged += new System.EventHandler(this.RadiobuttonListener);
            // 
            // tPanel2
            // 
            this.tPanel2.Controls.Add(this.rbtnGR);
            this.tPanel2.Controls.Add(this.rbtnBL);
            this.tPanel2.Location = new System.Drawing.Point(3, 83);
            this.tPanel2.Name = "tPanel2";
            this.tPanel2.Size = new System.Drawing.Size(62, 47);
            // 
            // btnGatepass
            // 
            this.btnGatepass.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnGatepass.Location = new System.Drawing.Point(164, 215);
            this.btnGatepass.Name = "btnGatepass";
            this.btnGatepass.Size = new System.Drawing.Size(72, 24);
            this.btnGatepass.TabIndex = 70;
            this.btnGatepass.Text = "GatePass";
            this.btnGatepass.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCheckList
            // 
            this.btnCheckList.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCheckList.Location = new System.Drawing.Point(3, 167);
            this.btnCheckList.Name = "btnCheckList";
            this.btnCheckList.Size = new System.Drawing.Size(104, 24);
            this.btnCheckList.TabIndex = 73;
            this.btnCheckList.Text = "VSR Check List";
            this.btnCheckList.Click += new System.EventHandler(this.ActionListener);
            // 
            // HWC103
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.btnCheckList);
            this.Controls.Add(this.btnGatepass);
            this.Controls.Add(this.btnRehandleOPR);
            this.Controls.Add(this.btnUnclosedOPR);
            this.Controls.Add(this.btnWHReconcile);
            this.Controls.Add(this.btnCargoMovement);
            this.Controls.Add(this.btnHO);
            this.Controls.Add(this.btnCargoJob);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnListHIHO);
            this.Controls.Add(this.txtBL);
            this.Controls.Add(this.btnF3);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.btnF2);
            this.Controls.Add(this.txtGR);
            this.Controls.Add(this.txtJPVCName);
            this.Controls.Add(this.btnHI);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.pnlJPVC);
            this.Controls.Add(this.tPanel2);
            this.Name = "HWC103";
            this.Text = "Warehouse Checker - Main";
            this.pnlJPVC.ResumeLayout(false);
            this.tPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TButton btnListHIHO;
        internal Framework.Controls.UserControls.TTextBox txtBL;
        internal Framework.Controls.UserControls.TButton btnF3;
        internal Framework.Controls.UserControls.TRadioButton rbtnBL;
        internal Framework.Controls.UserControls.TRadioButton rbtnGR;
        internal Framework.Controls.UserControls.TButton btnF1;
        internal Framework.Controls.UserControls.TButton btnF2;
        internal Framework.Controls.UserControls.TTextBox txtGR;
        internal Framework.Controls.UserControls.TTextBox txtJPVCName;
        internal Framework.Controls.UserControls.TButton btnHI;
        internal Framework.Controls.UserControls.TTextBox txtJPVC;
        internal Framework.Controls.UserControls.TButton btnExit;
        internal Framework.Controls.UserControls.TButton btnCargoJob;
        internal Framework.Controls.UserControls.TButton btnHO;
        internal Framework.Controls.UserControls.TButton btnCargoMovement;
        internal Framework.Controls.UserControls.TButton btnWHReconcile;
        internal Framework.Controls.UserControls.TButton btnUnclosedOPR;
        internal Framework.Controls.UserControls.TButton btnRehandleOPR;
        private Framework.Controls.Container.TPanel pnlJPVC;
        internal Framework.Controls.UserControls.TRadioButton rbtnNonJPVC;
        internal Framework.Controls.UserControls.TRadioButton rbtnJPVC;
        private Framework.Controls.Container.TPanel tPanel2;
        internal Framework.Controls.UserControls.TButton btnGatepass;
        internal Framework.Controls.UserControls.TButton btnCheckList;
    }
}