namespace MOST.ApronChecker
{
    partial class HAC105001
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
            this.cboRehandleDmg = new Framework.Controls.UserControls.TCombobox();
            this.cboRehandleShut = new Framework.Controls.UserControls.TCombobox();
            this.txtDmgQty = new Framework.Controls.UserControls.TTextBox();
            this.txtDmgMT = new Framework.Controls.UserControls.TTextBox();
            this.txtDmgM3 = new Framework.Controls.UserControls.TTextBox();
            this.Label5 = new Framework.Controls.UserControls.TLabel();
            this.txtShutQty = new Framework.Controls.UserControls.TTextBox();
            this.Label6 = new Framework.Controls.UserControls.TLabel();
            this.txtShutMT = new Framework.Controls.UserControls.TTextBox();
            this.txtShutM3 = new Framework.Controls.UserControls.TTextBox();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnConfirm = new Framework.Controls.UserControls.TButton();
            this.chkGP = new System.Windows.Forms.CheckBox();
            this.txtShutLoc = new Framework.Controls.UserControls.TTextBox();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
            this.btnShutLoc = new Framework.Controls.UserControls.TButton();
            this.txtDmgLoc = new Framework.Controls.UserControls.TTextBox();
            this.btnDmgLoc = new Framework.Controls.UserControls.TButton();
            this.tLabel4 = new Framework.Controls.UserControls.TLabel();
            this.txtBalQty = new Framework.Controls.UserControls.TTextBox();
            this.txtBalM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtBalMT = new Framework.Controls.UserControls.TTextBox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.txtDelvMode = new Framework.Controls.UserControls.TTextBox();
            this.tLabel5 = new Framework.Controls.UserControls.TLabel();
            this.txtDocQty = new Framework.Controls.UserControls.TTextBox();
            this.txtDocM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtDocMT = new Framework.Controls.UserControls.TTextBox();
            this.Label1 = new Framework.Controls.UserControls.TLabel();
            this.txtRemark = new Framework.Controls.UserControls.TTextBox();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
            // 
            // cboRehandleDmg
            // 
            this.cboRehandleDmg.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboRehandleDmg.isBusinessItemName = "";
            this.cboRehandleDmg.isMandatory = false;
            this.cboRehandleDmg.Location = new System.Drawing.Point(49, 162);
            this.cboRehandleDmg.Name = "cboRehandleDmg";
            this.cboRehandleDmg.Size = new System.Drawing.Size(122, 19);
            this.cboRehandleDmg.TabIndex = 28;
            this.cboRehandleDmg.SelectedIndexChanged += new System.EventHandler(this.CheckboxGPListener);
            // 
            // cboRehandleShut
            // 
            this.cboRehandleShut.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboRehandleShut.isBusinessItemName = "";
            this.cboRehandleShut.isMandatory = false;
            this.cboRehandleShut.Location = new System.Drawing.Point(49, 86);
            this.cboRehandleShut.Name = "cboRehandleShut";
            this.cboRehandleShut.Size = new System.Drawing.Size(122, 19);
            this.cboRehandleShut.TabIndex = 18;
            this.cboRehandleShut.SelectedIndexChanged += new System.EventHandler(this.CheckboxGPListener);
            // 
            // txtDmgQty
            // 
            this.txtDmgQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtDmgQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDmgQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtDmgQty.Location = new System.Drawing.Point(169, 141);
            this.txtDmgQty.Name = "txtDmgQty";
            this.txtDmgQty.Size = new System.Drawing.Size(58, 19);
            this.txtDmgQty.TabIndex = 27;
            this.txtDmgQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDmgQty.TextChanged += new System.EventHandler(this.ClearDmgLoc);
            // 
            // txtDmgMT
            // 
            this.txtDmgMT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtDmgMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDmgMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDmgMT.Location = new System.Drawing.Point(49, 141);
            this.txtDmgMT.Name = "txtDmgMT";
            this.txtDmgMT.Size = new System.Drawing.Size(58, 19);
            this.txtDmgMT.TabIndex = 25;
            this.txtDmgMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDmgMT.TextChanged += new System.EventHandler(this.ClearDmgLoc);
            // 
            // txtDmgM3
            // 
            this.txtDmgM3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtDmgM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDmgM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDmgM3.Location = new System.Drawing.Point(109, 141);
            this.txtDmgM3.Name = "txtDmgM3";
            this.txtDmgM3.Size = new System.Drawing.Size(58, 19);
            this.txtDmgM3.TabIndex = 26;
            this.txtDmgM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDmgM3.TextChanged += new System.EventHandler(this.ClearDmgLoc);
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label5.Location = new System.Drawing.Point(1, 67);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(46, 15);
            this.Label5.Text = "Shut-out";
            // 
            // txtShutQty
            // 
            this.txtShutQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtShutQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtShutQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtShutQty.Location = new System.Drawing.Point(169, 65);
            this.txtShutQty.Name = "txtShutQty";
            this.txtShutQty.Size = new System.Drawing.Size(58, 19);
            this.txtShutQty.TabIndex = 17;
            this.txtShutQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtShutQty.TextChanged += new System.EventHandler(this.ClearShutLoc);
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label6.Location = new System.Drawing.Point(1, 143);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(44, 15);
            this.Label6.Text = "Damage";
            // 
            // txtShutMT
            // 
            this.txtShutMT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtShutMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtShutMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtShutMT.Location = new System.Drawing.Point(49, 65);
            this.txtShutMT.Name = "txtShutMT";
            this.txtShutMT.Size = new System.Drawing.Size(58, 19);
            this.txtShutMT.TabIndex = 15;
            this.txtShutMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtShutMT.TextChanged += new System.EventHandler(this.ClearShutLoc);
            // 
            // txtShutM3
            // 
            this.txtShutM3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtShutM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtShutM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtShutM3.Location = new System.Drawing.Point(109, 65);
            this.txtShutM3.Name = "txtShutM3";
            this.txtShutM3.Size = new System.Drawing.Size(58, 19);
            this.txtShutM3.TabIndex = 16;
            this.txtShutM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtShutM3.TextChanged += new System.EventHandler(this.ClearShutLoc);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(168, 241);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 40;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnConfirm.Location = new System.Drawing.Point(99, 241);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(65, 24);
            this.btnConfirm.TabIndex = 39;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.ActionListener);
            // 
            // chkGP
            // 
            this.chkGP.Enabled = false;
            this.chkGP.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.chkGP.Location = new System.Drawing.Point(28, 242);
            this.chkGP.Name = "chkGP";
            this.chkGP.Size = new System.Drawing.Size(41, 17);
            this.chkGP.TabIndex = 35;
            this.chkGP.Text = "GP";
            // 
            // txtShutLoc
            // 
            this.txtShutLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtShutLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtShutLoc.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtShutLoc.Location = new System.Drawing.Point(49, 110);
            this.txtShutLoc.Name = "txtShutLoc";
            this.txtShutLoc.ReadOnly = true;
            this.txtShutLoc.Size = new System.Drawing.Size(65, 19);
            this.txtShutLoc.TabIndex = 20;
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(24, 112);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(24, 15);
            this.tLabel3.Text = "Loc";
            // 
            // btnShutLoc
            // 
            this.btnShutLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnShutLoc.Location = new System.Drawing.Point(117, 110);
            this.btnShutLoc.Name = "btnShutLoc";
            this.btnShutLoc.Size = new System.Drawing.Size(54, 19);
            this.btnShutLoc.TabIndex = 21;
            this.btnShutLoc.Text = "SetLoc";
            this.btnShutLoc.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtDmgLoc
            // 
            this.txtDmgLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtDmgLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDmgLoc.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtDmgLoc.Location = new System.Drawing.Point(49, 187);
            this.txtDmgLoc.Name = "txtDmgLoc";
            this.txtDmgLoc.ReadOnly = true;
            this.txtDmgLoc.Size = new System.Drawing.Size(65, 19);
            this.txtDmgLoc.TabIndex = 29;
            // 
            // btnDmgLoc
            // 
            this.btnDmgLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnDmgLoc.Location = new System.Drawing.Point(117, 187);
            this.btnDmgLoc.Name = "btnDmgLoc";
            this.btnDmgLoc.Size = new System.Drawing.Size(54, 19);
            this.btnDmgLoc.TabIndex = 30;
            this.btnDmgLoc.Text = "SetLoc";
            this.btnDmgLoc.Click += new System.EventHandler(this.ActionListener);
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(25, 189);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(24, 15);
            this.tLabel4.Text = "Loc";
            // 
            // txtBalQty
            // 
            this.txtBalQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBalQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtBalQty.Location = new System.Drawing.Point(169, 23);
            this.txtBalQty.Multiline = true;
            this.txtBalQty.Name = "txtBalQty";
            this.txtBalQty.ReadOnly = true;
            this.txtBalQty.Size = new System.Drawing.Size(58, 17);
            this.txtBalQty.TabIndex = 58;
            this.txtBalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBalM3
            // 
            this.txtBalM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBalM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtBalM3.Location = new System.Drawing.Point(109, 23);
            this.txtBalM3.Multiline = true;
            this.txtBalM3.Name = "txtBalM3";
            this.txtBalM3.ReadOnly = true;
            this.txtBalM3.Size = new System.Drawing.Size(58, 17);
            this.txtBalM3.TabIndex = 56;
            this.txtBalM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBalMT
            // 
            this.txtBalMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBalMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtBalMT.Location = new System.Drawing.Point(49, 23);
            this.txtBalMT.Multiline = true;
            this.txtBalMT.Name = "txtBalMT";
            this.txtBalMT.ReadOnly = true;
            this.txtBalMT.Size = new System.Drawing.Size(58, 17);
            this.txtBalMT.TabIndex = 57;
            this.txtBalMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(22, 25);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(27, 15);
            this.tLabel1.Text = "Bal";
            // 
            // txtDelvMode
            // 
            this.txtDelvMode.Enabled = false;
            this.txtDelvMode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDelvMode.Location = new System.Drawing.Point(49, 42);
            this.txtDelvMode.Multiline = true;
            this.txtDelvMode.Name = "txtDelvMode";
            this.txtDelvMode.Size = new System.Drawing.Size(58, 17);
            this.txtDelvMode.TabIndex = 104;
            // 
            // tLabel5
            // 
            this.tLabel5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel5.Location = new System.Drawing.Point(16, 45);
            this.tLabel5.Name = "tLabel5";
            this.tLabel5.Size = new System.Drawing.Size(30, 14);
            this.tLabel5.Text = "Mode";
            // 
            // txtDocQty
            // 
            this.txtDocQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDocQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtDocQty.Location = new System.Drawing.Point(169, 4);
            this.txtDocQty.Multiline = true;
            this.txtDocQty.Name = "txtDocQty";
            this.txtDocQty.ReadOnly = true;
            this.txtDocQty.Size = new System.Drawing.Size(58, 17);
            this.txtDocQty.TabIndex = 27;
            this.txtDocQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDocM3
            // 
            this.txtDocM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDocM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDocM3.Location = new System.Drawing.Point(109, 4);
            this.txtDocM3.Multiline = true;
            this.txtDocM3.Name = "txtDocM3";
            this.txtDocM3.ReadOnly = true;
            this.txtDocM3.Size = new System.Drawing.Size(58, 17);
            this.txtDocM3.TabIndex = 25;
            this.txtDocM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDocMT
            // 
            this.txtDocMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDocMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDocMT.Location = new System.Drawing.Point(49, 4);
            this.txtDocMT.Multiline = true;
            this.txtDocMT.Name = "txtDocMT";
            this.txtDocMT.ReadOnly = true;
            this.txtDocMT.Size = new System.Drawing.Size(58, 17);
            this.txtDocMT.TabIndex = 26;
            this.txtDocMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label1.Location = new System.Drawing.Point(22, 6);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(30, 14);
            this.Label1.Text = "Doc";
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtRemark.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtRemark.isBusinessItemName = "Remark";
            this.txtRemark.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtRemark.Location = new System.Drawing.Point(48, 215);
            this.txtRemark.MaxLength = 50;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(185, 19);
            this.txtRemark.TabIndex = 113;
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(22, 217);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(26, 15);
            this.tLabel2.Text = "Rmk";
            // 
            // HAC105001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.txtDelvMode);
            this.Controls.Add(this.tLabel4);
            this.Controls.Add(this.btnDmgLoc);
            this.Controls.Add(this.txtDmgLoc);
            this.Controls.Add(this.btnShutLoc);
            this.Controls.Add(this.tLabel3);
            this.Controls.Add(this.txtShutLoc);
            this.Controls.Add(this.txtBalQty);
            this.Controls.Add(this.txtBalMT);
            this.Controls.Add(this.txtBalM3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.chkGP);
            this.Controls.Add(this.cboRehandleDmg);
            this.Controls.Add(this.cboRehandleShut);
            this.Controls.Add(this.txtDmgQty);
            this.Controls.Add(this.txtDmgMT);
            this.Controls.Add(this.txtDmgM3);
            this.Controls.Add(this.txtShutQty);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.txtShutMT);
            this.Controls.Add(this.txtShutM3);
            this.Controls.Add(this.txtDocQty);
            this.Controls.Add(this.txtDocMT);
            this.Controls.Add(this.txtDocM3);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.tLabel5);
            this.KeyPreview = true;
            this.Name = "HAC105001";
            this.Text = "A/C - Loading Cancel Information";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HAC105001_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TCombobox cboRehandleDmg;
        internal Framework.Controls.UserControls.TCombobox cboRehandleShut;
        internal Framework.Controls.UserControls.TTextBox txtDmgQty;
        internal Framework.Controls.UserControls.TTextBox txtDmgMT;
        internal Framework.Controls.UserControls.TTextBox txtDmgM3;
        internal Framework.Controls.UserControls.TLabel Label5;
        internal Framework.Controls.UserControls.TTextBox txtShutQty;
        internal Framework.Controls.UserControls.TLabel Label6;
        internal Framework.Controls.UserControls.TTextBox txtShutMT;
        internal Framework.Controls.UserControls.TTextBox txtShutM3;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TButton btnConfirm;
        internal System.Windows.Forms.CheckBox chkGP;
        internal Framework.Controls.UserControls.TTextBox txtShutLoc;
        internal Framework.Controls.UserControls.TLabel tLabel3;
        internal Framework.Controls.UserControls.TButton btnShutLoc;
        internal Framework.Controls.UserControls.TTextBox txtDmgLoc;
        internal Framework.Controls.UserControls.TButton btnDmgLoc;
        internal Framework.Controls.UserControls.TLabel tLabel4;
        internal Framework.Controls.UserControls.TTextBox txtBalQty;
        internal Framework.Controls.UserControls.TTextBox txtBalM3;
        internal Framework.Controls.UserControls.TTextBox txtBalMT;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        internal Framework.Controls.UserControls.TTextBox txtDelvMode;
        internal Framework.Controls.UserControls.TLabel tLabel5;
        internal Framework.Controls.UserControls.TTextBox txtDocQty;
        internal Framework.Controls.UserControls.TTextBox txtDocM3;
        internal Framework.Controls.UserControls.TTextBox txtDocMT;
        internal Framework.Controls.UserControls.TLabel Label1;
        internal Framework.Controls.UserControls.TTextBox txtRemark;
        internal Framework.Controls.UserControls.TLabel tLabel2;

    }
}