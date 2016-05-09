using MOST.Common.UserAttribute;

namespace MOST.CEClient
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.btnExit = new Framework.Controls.UserControls.TButton();
            this.btnLogIn = new Framework.Controls.UserControls.TButton();
            this.pnLoginCheck = new Framework.Controls.Container.TPanel();
            this.txtUserId = new Framework.Controls.UserControls.TTextBox();
            this.txtPasswords = new Framework.Controls.UserControls.TTextBox();
            this.label2 = new Framework.Controls.UserControls.TLabel();
            this.label1 = new Framework.Controls.UserControls.TLabel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pnLogin = new Framework.Controls.Container.TPanel();
            this.cmbShift = new Framework.Controls.UserControls.TCombobox();
            this.lbWorkDate = new Framework.Controls.UserControls.TLabel();
            this.txtWorkDate = new Framework.Controls.UserControls.TDateTimePicker();
            this.tPanel1 = new Framework.Controls.Container.TPanel();
            this.blShift = new Framework.Controls.UserControls.TLabel();
            this.cmbProgram = new Framework.Controls.UserControls.TCombobox();
            this.btnGoto = new Framework.Controls.UserControls.TButton();
            this.lbProgram = new Framework.Controls.UserControls.TLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusPanel = new System.Windows.Forms.StatusBar();
            this.pnLoginCheck.SuspendLayout();
            this.pnLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnExit.Location = new System.Drawing.Point(161, 220);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(72, 25);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnLogIn
            // 
            this.btnLogIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.btnLogIn.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnLogIn.Location = new System.Drawing.Point(85, 220);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(72, 25);
            this.btnLogIn.TabIndex = 3;
            this.btnLogIn.Text = "Log In";
            this.btnLogIn.Click += new System.EventHandler(this.ActionListener);
            // 
            // pnLoginCheck
            // 
            this.pnLoginCheck.Controls.Add(this.txtUserId);
            this.pnLoginCheck.Controls.Add(this.txtPasswords);
            this.pnLoginCheck.Controls.Add(this.label2);
            this.pnLoginCheck.Controls.Add(this.label1);
            this.pnLoginCheck.Controls.Add(this.pictureBox2);
            this.pnLoginCheck.Location = new System.Drawing.Point(2, 2);
            this.pnLoginCheck.Name = "pnLoginCheck";
            this.pnLoginCheck.Size = new System.Drawing.Size(235, 214);
            // 
            // txtUserId
            // 
            this.txtUserId.isBusinessItemName = "User ID";
            this.txtUserId.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtUserId.isInputType = Framework.Controls.UserControls.TTextBox.InputList.AlphabetOnly;
            this.txtUserId.isMandatory = true;
            this.txtUserId.Location = new System.Drawing.Point(74, 134);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(141, 23);
            this.txtUserId.TabIndex = 1;
            this.txtUserId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeypressListener);
            // 
            // txtPasswords
            // 
            this.txtPasswords.isBusinessItemName = "Password";
            this.txtPasswords.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtPasswords.isInputType = Framework.Controls.UserControls.TTextBox.InputList.AlphaNumeric;
            this.txtPasswords.isMandatory = true;
            this.txtPasswords.Location = new System.Drawing.Point(74, 173);
            this.txtPasswords.Name = "txtPasswords";
            this.txtPasswords.PasswordChar = '*';
            this.txtPasswords.Size = new System.Drawing.Size(141, 23);
            this.txtPasswords.TabIndex = 2;
            this.txtPasswords.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeypressListener);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(18, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.Text = "Password";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(18, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.Text = "User ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(1, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(232, 100);
            // 
            // pnLogin
            // 
            this.pnLogin.Controls.Add(this.cmbShift);
            this.pnLogin.Controls.Add(this.lbWorkDate);
            this.pnLogin.Controls.Add(this.txtWorkDate);
            this.pnLogin.Controls.Add(this.tPanel1);
            this.pnLogin.Controls.Add(this.blShift);
            this.pnLogin.Controls.Add(this.cmbProgram);
            this.pnLogin.Controls.Add(this.btnGoto);
            this.pnLogin.Controls.Add(this.lbProgram);
            this.pnLogin.Controls.Add(this.pictureBox1);
            this.pnLogin.Location = new System.Drawing.Point(243, 2);
            this.pnLogin.Name = "pnLogin";
            this.pnLogin.Size = new System.Drawing.Size(235, 214);
            this.pnLogin.Visible = false;
            // 
            // cmbShift
            // 
            this.cmbShift.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cmbShift.isBusinessItemName = "Shift";
            this.cmbShift.isMandatory = true;
            this.cmbShift.Location = new System.Drawing.Point(87, 162);
            this.cmbShift.Name = "cmbShift";
            this.cmbShift.Size = new System.Drawing.Size(134, 19);
            this.cmbShift.TabIndex = 7;
            // 
            // lbWorkDate
            // 
            this.lbWorkDate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lbWorkDate.Location = new System.Drawing.Point(21, 139);
            this.lbWorkDate.Name = "lbWorkDate";
            this.lbWorkDate.Size = new System.Drawing.Size(60, 19);
            this.lbWorkDate.Text = "Work Date";
            this.lbWorkDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtWorkDate
            // 
            this.txtWorkDate.CustomFormat = "dd/MM/yyyy";
            this.txtWorkDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtWorkDate.isBusinessItemName = "Work Date";
            this.txtWorkDate.isMandatory = true;
            this.txtWorkDate.Location = new System.Drawing.Point(87, 133);
            this.txtWorkDate.Name = "txtWorkDate";
            this.txtWorkDate.Size = new System.Drawing.Size(134, 24);
            this.txtWorkDate.TabIndex = 6;
            this.txtWorkDate.Value = new System.DateTime(2008, 10, 24, 0, 0, 0, 93);
            // 
            // tPanel1
            // 
            this.tPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tPanel1.Location = new System.Drawing.Point(26, 128);
            this.tPanel1.Name = "tPanel1";
            this.tPanel1.Size = new System.Drawing.Size(195, 3);
            // 
            // blShift
            // 
            this.blShift.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.blShift.Location = new System.Drawing.Point(21, 163);
            this.blShift.Name = "blShift";
            this.blShift.Size = new System.Drawing.Size(59, 19);
            this.blShift.Text = "Shift";
            this.blShift.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbProgram
            // 
            this.cmbProgram.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.cmbProgram.isBusinessItemName = "Program";
            this.cmbProgram.isMandatory = true;
            this.cmbProgram.Location = new System.Drawing.Point(87, 103);
            this.cmbProgram.Name = "cmbProgram";
            this.cmbProgram.Size = new System.Drawing.Size(134, 21);
            this.cmbProgram.TabIndex = 5;
            this.cmbProgram.SelectedIndexChanged += new System.EventHandler(this.cmbProgram_SelectedIndexChanged);
            // 
            // btnGoto
            // 
            this.btnGoto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(176)))), ((int)(((byte)(181)))));
            this.btnGoto.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnGoto.Location = new System.Drawing.Point(149, 187);
            this.btnGoto.Name = "btnGoto";
            this.btnGoto.Size = new System.Drawing.Size(72, 25);
            this.btnGoto.TabIndex = 8;
            this.btnGoto.Text = "Goto";
            this.btnGoto.Click += new System.EventHandler(this.ActionListener);
            // 
            // lbProgram
            // 
            this.lbProgram.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lbProgram.Location = new System.Drawing.Point(21, 106);
            this.lbProgram.Name = "lbProgram";
            this.lbProgram.Size = new System.Drawing.Size(60, 20);
            this.lbProgram.Text = "Program";
            this.lbProgram.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(232, 100);
            // 
            // statusPanel
            // 
            this.statusPanel.Location = new System.Drawing.Point(0, 245);
            this.statusPanel.Name = "statusPanel";
            this.statusPanel.Size = new System.Drawing.Size(484, 24);
            this.statusPanel.Text = "Status : it is not connected";
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(484, 269);
            this.Controls.Add(this.statusPanel);
            this.Controls.Add(this.pnLogin);
            this.Controls.Add(this.pnLoginCheck);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLogIn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLogin";
            this.Text = "Login";
            this.Closed += new System.EventHandler(this.frmLogin_Closed);
            this.pnLoginCheck.ResumeLayout(false);
            this.pnLogin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion


        private Framework.Controls.UserControls.TButton btnExit;

        //[AuthAccess("000001", "Login")]
        public Framework.Controls.UserControls.TButton btnLogIn;
        //[AuthAccess("000002", "Goto")]
        public Framework.Controls.UserControls.TButton btnGoto;

        private Framework.Controls.Container.TPanel pnLoginCheck;
        private Framework.Controls.UserControls.TTextBox txtUserId;
        private Framework.Controls.UserControls.TTextBox txtPasswords;
        private Framework.Controls.UserControls.TLabel label2;
        private Framework.Controls.UserControls.TLabel label1;
        private Framework.Controls.Container.TPanel pnLogin;
        private Framework.Controls.UserControls.TLabel lbProgram;
        private Framework.Controls.UserControls.TCombobox cmbProgram;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Framework.Controls.UserControls.TLabel blShift;
        private Framework.Controls.Container.TPanel tPanel1;
        private Framework.Controls.UserControls.TCombobox cmbShift;
        private Framework.Controls.UserControls.TLabel lbWorkDate;
        private Framework.Controls.UserControls.TDateTimePicker txtWorkDate;
        private System.Windows.Forms.StatusBar statusPanel;
    }
}

