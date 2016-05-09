namespace MOST.ApronChecker
{
    partial class HAC101
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
            this.btnCheckList = new Framework.Controls.UserControls.TButton();
            this.txtBL = new Framework.Controls.UserControls.TTextBox();
            this.btnF3 = new Framework.Controls.UserControls.TButton();
            this.btnExit = new Framework.Controls.UserControls.TButton();
            this.rbtnBL = new Framework.Controls.UserControls.TRadioButton();
            this.rbtnGR = new Framework.Controls.UserControls.TRadioButton();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.btnF2 = new Framework.Controls.UserControls.TButton();
            this.txtGR = new Framework.Controls.UserControls.TTextBox();
            this.txtJPVCName = new Framework.Controls.UserControls.TTextBox();
            this.btnConfirm = new Framework.Controls.UserControls.TButton();
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.btnLoadingList = new Framework.Controls.UserControls.TButton();
            this.btnDischargingList = new Framework.Controls.UserControls.TButton();
            this.btnCargoJob = new Framework.Controls.UserControls.TButton();
            this.btnGatePass = new Framework.Controls.UserControls.TButton();
            this.btnUnclosedOPR = new Framework.Controls.UserControls.TButton();
            this.rbtnNonJPVC = new Framework.Controls.UserControls.TRadioButton();
            this.rbtnJPVC = new Framework.Controls.UserControls.TRadioButton();
            this.pnlJPVC = new Framework.Controls.Container.TPanel();
            this.tPanel2 = new Framework.Controls.Container.TPanel();
            this.btnRehandleOPR = new Framework.Controls.UserControls.TButton();
            this.pnlJPVC.SuspendLayout();
            this.tPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCheckList
            // 
            this.btnCheckList.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCheckList.Location = new System.Drawing.Point(2, 187);
            this.btnCheckList.Name = "btnCheckList";
            this.btnCheckList.Size = new System.Drawing.Size(81, 25);
            this.btnCheckList.TabIndex = 22;
            this.btnCheckList.Text = "VSR Check List";
            this.btnCheckList.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtBL
            // 
            this.txtBL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtBL.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBL.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtBL.Location = new System.Drawing.Point(61, 117);
            this.txtBL.Name = "txtBL";
            this.txtBL.Size = new System.Drawing.Size(108, 19);
            this.txtBL.TabIndex = 12;
            this.txtBL.TextChanged += new System.EventHandler(this.txtBL_TextChanged);
            // 
            // btnF3
            // 
            this.btnF3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF3.Location = new System.Drawing.Point(175, 116);
            this.btnF3.Name = "btnF3";
            this.btnF3.Size = new System.Drawing.Size(56, 20);
            this.btnF3.TabIndex = 13;
            this.btnF3.Text = "F3";
            this.btnF3.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnExit.Location = new System.Drawing.Point(161, 237);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.TabIndex = 26;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.ActionListener);
            // 
            // rbtnBL
            // 
            this.rbtnBL.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnBL.isBusinessItemName = "";
            this.rbtnBL.isMandatory = false;
            this.rbtnBL.Location = new System.Drawing.Point(3, 28);
            this.rbtnBL.Name = "rbtnBL";
            this.rbtnBL.Size = new System.Drawing.Size(54, 17);
            this.rbtnBL.TabIndex = 11;
            this.rbtnBL.Text = "B/L No";
            this.rbtnBL.CheckedChanged += new System.EventHandler(this.RadiobuttonListener);
            // 
            // rbtnGR
            // 
            this.rbtnGR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnGR.isBusinessItemName = "";
            this.rbtnGR.isMandatory = false;
            this.rbtnGR.Location = new System.Drawing.Point(3, 2);
            this.rbtnGR.Name = "rbtnGR";
            this.rbtnGR.Size = new System.Drawing.Size(56, 17);
            this.rbtnGR.TabIndex = 8;
            this.rbtnGR.Text = "G/R No";
            this.rbtnGR.CheckedChanged += new System.EventHandler(this.RadiobuttonListener);
            // 
            // btnF1
            // 
            this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF1.Location = new System.Drawing.Point(175, 27);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(56, 20);
            this.btnF1.TabIndex = 2;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnF2
            // 
            this.btnF2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF2.Location = new System.Drawing.Point(175, 90);
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
            this.txtGR.Location = new System.Drawing.Point(61, 91);
            this.txtGR.Name = "txtGR";
            this.txtGR.Size = new System.Drawing.Size(108, 19);
            this.txtGR.TabIndex = 9;
            this.txtGR.TextChanged += new System.EventHandler(this.txtGR_TextChanged);
            // 
            // txtJPVCName
            // 
            this.txtJPVCName.Enabled = false;
            this.txtJPVCName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVCName.Location = new System.Drawing.Point(49, 51);
            this.txtJPVCName.Name = "txtJPVCName";
            this.txtJPVCName.Size = new System.Drawing.Size(184, 19);
            this.txtJPVCName.TabIndex = 58;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnConfirm.Location = new System.Drawing.Point(161, 162);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 50);
            this.btnConfirm.TabIndex = 20;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtJPVC
            // 
            this.txtJPVC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.Location = new System.Drawing.Point(49, 28);
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(120, 19);
            this.txtJPVC.TabIndex = 1;
            this.txtJPVC.TextChanged += new System.EventHandler(this.txtJPVC_TextChanged);
            this.txtJPVC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJPVC_KeyPress);
            // 
            // btnLoadingList
            // 
            this.btnLoadingList.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnLoadingList.Location = new System.Drawing.Point(2, 212);
            this.btnLoadingList.Name = "btnLoadingList";
            this.btnLoadingList.Size = new System.Drawing.Size(81, 25);
            this.btnLoadingList.TabIndex = 24;
            this.btnLoadingList.Text = "LD List";
            this.btnLoadingList.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnDischargingList
            // 
            this.btnDischargingList.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnDischargingList.Location = new System.Drawing.Point(83, 212);
            this.btnDischargingList.Name = "btnDischargingList";
            this.btnDischargingList.Size = new System.Drawing.Size(78, 25);
            this.btnDischargingList.TabIndex = 25;
            this.btnDischargingList.Text = "DS List";
            this.btnDischargingList.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCargoJob
            // 
            this.btnCargoJob.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCargoJob.Location = new System.Drawing.Point(2, 162);
            this.btnCargoJob.Name = "btnCargoJob";
            this.btnCargoJob.Size = new System.Drawing.Size(159, 25);
            this.btnCargoJob.TabIndex = 21;
            this.btnCargoJob.Text = "Cargo Job Monitoring";
            this.btnCargoJob.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnGatePass
            // 
            this.btnGatePass.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnGatePass.Location = new System.Drawing.Point(83, 187);
            this.btnGatePass.Name = "btnGatePass";
            this.btnGatePass.Size = new System.Drawing.Size(78, 25);
            this.btnGatePass.TabIndex = 60;
            this.btnGatePass.Text = "Gate Pass";
            this.btnGatePass.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnUnclosedOPR
            // 
            this.btnUnclosedOPR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnUnclosedOPR.Location = new System.Drawing.Point(2, 237);
            this.btnUnclosedOPR.Name = "btnUnclosedOPR";
            this.btnUnclosedOPR.Size = new System.Drawing.Size(159, 25);
            this.btnUnclosedOPR.TabIndex = 62;
            this.btnUnclosedOPR.Text = "Unclosed Operations";
            this.btnUnclosedOPR.Click += new System.EventHandler(this.ActionListener);
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
            // pnlJPVC
            // 
            this.pnlJPVC.Controls.Add(this.rbtnNonJPVC);
            this.pnlJPVC.Controls.Add(this.rbtnJPVC);
            this.pnlJPVC.Location = new System.Drawing.Point(2, 4);
            this.pnlJPVC.Name = "pnlJPVC";
            this.pnlJPVC.Size = new System.Drawing.Size(72, 44);
            // 
            // tPanel2
            // 
            this.tPanel2.Controls.Add(this.rbtnGR);
            this.tPanel2.Controls.Add(this.rbtnBL);
            this.tPanel2.Location = new System.Drawing.Point(3, 92);
            this.tPanel2.Name = "tPanel2";
            this.tPanel2.Size = new System.Drawing.Size(63, 46);
            // 
            // btnRehandleOPR
            // 
            this.btnRehandleOPR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnRehandleOPR.Location = new System.Drawing.Point(161, 212);
            this.btnRehandleOPR.Name = "btnRehandleOPR";
            this.btnRehandleOPR.Size = new System.Drawing.Size(75, 25);
            this.btnRehandleOPR.TabIndex = 65;
            this.btnRehandleOPR.Text = "Rehandle OPR";
            this.btnRehandleOPR.Click += new System.EventHandler(this.ActionListener);
            // 
            // HAC101
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.btnRehandleOPR);
            this.Controls.Add(this.btnUnclosedOPR);
            this.Controls.Add(this.btnGatePass);
            this.Controls.Add(this.btnCargoJob);
            this.Controls.Add(this.btnDischargingList);
            this.Controls.Add(this.btnLoadingList);
            this.Controls.Add(this.btnCheckList);
            this.Controls.Add(this.txtBL);
            this.Controls.Add(this.btnF3);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.btnF2);
            this.Controls.Add(this.txtGR);
            this.Controls.Add(this.txtJPVCName);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.pnlJPVC);
            this.Controls.Add(this.tPanel2);
            this.KeyPreview = true;
            this.Name = "HAC101";
            this.Text = "Apron Checker - Main";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HAC101_KeyDown);
            this.pnlJPVC.ResumeLayout(false);
            this.tPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TButton btnCheckList;
        internal Framework.Controls.UserControls.TTextBox txtBL;
        internal Framework.Controls.UserControls.TButton btnF3;
        internal Framework.Controls.UserControls.TButton btnExit;
        internal Framework.Controls.UserControls.TRadioButton rbtnBL;
        internal Framework.Controls.UserControls.TRadioButton rbtnGR;
        internal Framework.Controls.UserControls.TButton btnF1;
        internal Framework.Controls.UserControls.TButton btnF2;
        internal Framework.Controls.UserControls.TTextBox txtGR;
        internal Framework.Controls.UserControls.TTextBox txtJPVCName;
        internal Framework.Controls.UserControls.TButton btnConfirm;
        internal Framework.Controls.UserControls.TTextBox txtJPVC;
        internal Framework.Controls.UserControls.TButton btnLoadingList;
        internal Framework.Controls.UserControls.TButton btnDischargingList;
        internal Framework.Controls.UserControls.TButton btnCargoJob;
        internal Framework.Controls.UserControls.TButton btnGatePass;
        internal Framework.Controls.UserControls.TButton btnUnclosedOPR;
        internal Framework.Controls.UserControls.TRadioButton rbtnNonJPVC;
        internal Framework.Controls.UserControls.TRadioButton rbtnJPVC;
        private Framework.Controls.Container.TPanel pnlJPVC;
        private Framework.Controls.Container.TPanel tPanel2;
        internal Framework.Controls.UserControls.TButton btnRehandleOPR;
    }
}