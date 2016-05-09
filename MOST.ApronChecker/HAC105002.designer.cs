namespace MOST.ApronChecker
{
    partial class HAC105002
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
            this.txtSpareQty = new Framework.Controls.UserControls.TTextBox();
            this.txtSpareMT = new Framework.Controls.UserControls.TTextBox();
            this.txtSpareM3 = new Framework.Controls.UserControls.TTextBox();
            this.Label5 = new Framework.Controls.UserControls.TLabel();
            this.txtWHDmgQty = new Framework.Controls.UserControls.TTextBox();
            this.Label6 = new Framework.Controls.UserControls.TLabel();
            this.txtWHDmgMT = new Framework.Controls.UserControls.TTextBox();
            this.txtWHDmgM3 = new Framework.Controls.UserControls.TTextBox();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnConfirm = new Framework.Controls.UserControls.TButton();
            this.txtWHDmgLoc = new Framework.Controls.UserControls.TTextBox();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
            this.btnWHDmgUnset = new Framework.Controls.UserControls.TButton();
            this.txtSpareLoc = new Framework.Controls.UserControls.TTextBox();
            this.btnSpareUnset = new Framework.Controls.UserControls.TButton();
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
            this.txtWHDmgBalQty = new Framework.Controls.UserControls.TTextBox();
            this.txtWHDmgBalMt = new Framework.Controls.UserControls.TTextBox();
            this.txtWHDmgBalM3 = new Framework.Controls.UserControls.TTextBox();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.txtSpareBalQty = new Framework.Controls.UserControls.TTextBox();
            this.txtSpareBalMt = new Framework.Controls.UserControls.TTextBox();
            this.txtSpareBalM3 = new Framework.Controls.UserControls.TTextBox();
            this.tLabel6 = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
            // 
            // txtSpareQty
            // 
            this.txtSpareQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtSpareQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSpareQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtSpareQty.Location = new System.Drawing.Point(166, 180);
            this.txtSpareQty.Multiline = true;
            this.txtSpareQty.Name = "txtSpareQty";
            this.txtSpareQty.Size = new System.Drawing.Size(52, 17);
            this.txtSpareQty.TabIndex = 20;
            this.txtSpareQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSpareQty.TextChanged += new System.EventHandler(this.ClearSpareLoc);
            // 
            // txtSpareMT
            // 
            this.txtSpareMT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtSpareMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSpareMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtSpareMT.Location = new System.Drawing.Point(58, 180);
            this.txtSpareMT.Multiline = true;
            this.txtSpareMT.Name = "txtSpareMT";
            this.txtSpareMT.Size = new System.Drawing.Size(52, 17);
            this.txtSpareMT.TabIndex = 18;
            this.txtSpareMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSpareMT.TextChanged += new System.EventHandler(this.ClearSpareLoc);
            // 
            // txtSpareM3
            // 
            this.txtSpareM3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtSpareM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSpareM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtSpareM3.Location = new System.Drawing.Point(112, 180);
            this.txtSpareM3.Multiline = true;
            this.txtSpareM3.Name = "txtSpareM3";
            this.txtSpareM3.Size = new System.Drawing.Size(52, 17);
            this.txtSpareM3.TabIndex = 19;
            this.txtSpareM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSpareM3.TextChanged += new System.EventHandler(this.ClearSpareLoc);
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label5.Location = new System.Drawing.Point(8, 105);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(48, 15);
            this.Label5.Text = "WH Dmg";
            // 
            // txtWHDmgQty
            // 
            this.txtWHDmgQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtWHDmgQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtWHDmgQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtWHDmgQty.Location = new System.Drawing.Point(166, 103);
            this.txtWHDmgQty.Multiline = true;
            this.txtWHDmgQty.Name = "txtWHDmgQty";
            this.txtWHDmgQty.Size = new System.Drawing.Size(52, 17);
            this.txtWHDmgQty.TabIndex = 17;
            this.txtWHDmgQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtWHDmgQty.TextChanged += new System.EventHandler(this.ClearDmgLoc);
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label6.Location = new System.Drawing.Point(20, 182);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(32, 15);
            this.Label6.Text = "Spare";
            // 
            // txtWHDmgMT
            // 
            this.txtWHDmgMT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtWHDmgMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtWHDmgMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtWHDmgMT.Location = new System.Drawing.Point(58, 103);
            this.txtWHDmgMT.Multiline = true;
            this.txtWHDmgMT.Name = "txtWHDmgMT";
            this.txtWHDmgMT.Size = new System.Drawing.Size(52, 17);
            this.txtWHDmgMT.TabIndex = 15;
            this.txtWHDmgMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtWHDmgMT.TextChanged += new System.EventHandler(this.ClearDmgLoc);
            // 
            // txtWHDmgM3
            // 
            this.txtWHDmgM3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtWHDmgM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtWHDmgM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtWHDmgM3.Location = new System.Drawing.Point(112, 103);
            this.txtWHDmgM3.Multiline = true;
            this.txtWHDmgM3.Name = "txtWHDmgM3";
            this.txtWHDmgM3.Size = new System.Drawing.Size(52, 17);
            this.txtWHDmgM3.TabIndex = 16;
            this.txtWHDmgM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtWHDmgM3.TextChanged += new System.EventHandler(this.ClearDmgLoc);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(168, 242);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnConfirm.Location = new System.Drawing.Point(99, 242);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(65, 24);
            this.btnConfirm.TabIndex = 24;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtWHDmgLoc
            // 
            this.txtWHDmgLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtWHDmgLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtWHDmgLoc.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtWHDmgLoc.Location = new System.Drawing.Point(58, 122);
            this.txtWHDmgLoc.Multiline = true;
            this.txtWHDmgLoc.Name = "txtWHDmgLoc";
            this.txtWHDmgLoc.ReadOnly = true;
            this.txtWHDmgLoc.Size = new System.Drawing.Size(62, 17);
            this.txtWHDmgLoc.TabIndex = 87;
            this.txtWHDmgLoc.TextChanged += new System.EventHandler(this.txtWHDmgLoc_TextChanged);
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(32, 124);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(24, 15);
            this.tLabel3.Text = "Loc";
            // 
            // btnWHDmgUnset
            // 
            this.btnWHDmgUnset.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnWHDmgUnset.Location = new System.Drawing.Point(126, 122);
            this.btnWHDmgUnset.Name = "btnWHDmgUnset";
            this.btnWHDmgUnset.Size = new System.Drawing.Size(63, 17);
            this.btnWHDmgUnset.TabIndex = 90;
            this.btnWHDmgUnset.Text = "UnSetLoc";
            this.btnWHDmgUnset.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtSpareLoc
            // 
            this.txtSpareLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtSpareLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSpareLoc.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtSpareLoc.Location = new System.Drawing.Point(58, 199);
            this.txtSpareLoc.Multiline = true;
            this.txtSpareLoc.Name = "txtSpareLoc";
            this.txtSpareLoc.ReadOnly = true;
            this.txtSpareLoc.Size = new System.Drawing.Size(62, 17);
            this.txtSpareLoc.TabIndex = 91;
            // 
            // btnSpareUnset
            // 
            this.btnSpareUnset.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnSpareUnset.Location = new System.Drawing.Point(126, 199);
            this.btnSpareUnset.Name = "btnSpareUnset";
            this.btnSpareUnset.Size = new System.Drawing.Size(63, 17);
            this.btnSpareUnset.TabIndex = 92;
            this.btnSpareUnset.Text = "UnSetLoc";
            this.btnSpareUnset.Click += new System.EventHandler(this.ActionListener);
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(33, 201);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(24, 15);
            this.tLabel4.Text = "Loc";
            // 
            // txtBalQty
            // 
            this.txtBalQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBalQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtBalQty.Location = new System.Drawing.Point(168, 29);
            this.txtBalQty.Multiline = true;
            this.txtBalQty.Name = "txtBalQty";
            this.txtBalQty.ReadOnly = true;
            this.txtBalQty.Size = new System.Drawing.Size(52, 17);
            this.txtBalQty.TabIndex = 58;
            this.txtBalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBalM3
            // 
            this.txtBalM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBalM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtBalM3.Location = new System.Drawing.Point(113, 29);
            this.txtBalM3.Multiline = true;
            this.txtBalM3.Name = "txtBalM3";
            this.txtBalM3.ReadOnly = true;
            this.txtBalM3.Size = new System.Drawing.Size(52, 17);
            this.txtBalM3.TabIndex = 56;
            this.txtBalM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBalMT
            // 
            this.txtBalMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBalMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtBalMT.Location = new System.Drawing.Point(58, 29);
            this.txtBalMT.Multiline = true;
            this.txtBalMT.Name = "txtBalMT";
            this.txtBalMT.ReadOnly = true;
            this.txtBalMT.Size = new System.Drawing.Size(52, 17);
            this.txtBalMT.TabIndex = 57;
            this.txtBalMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(30, 31);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(24, 15);
            this.tLabel1.Text = "Bal";
            // 
            // txtDelvMode
            // 
            this.txtDelvMode.Enabled = false;
            this.txtDelvMode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDelvMode.Location = new System.Drawing.Point(58, 48);
            this.txtDelvMode.Multiline = true;
            this.txtDelvMode.Name = "txtDelvMode";
            this.txtDelvMode.Size = new System.Drawing.Size(52, 17);
            this.txtDelvMode.TabIndex = 104;
            // 
            // tLabel5
            // 
            this.tLabel5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel5.Location = new System.Drawing.Point(26, 51);
            this.tLabel5.Name = "tLabel5";
            this.tLabel5.Size = new System.Drawing.Size(30, 14);
            this.tLabel5.Text = "Mode";
            // 
            // txtDocQty
            // 
            this.txtDocQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDocQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtDocQty.Location = new System.Drawing.Point(168, 10);
            this.txtDocQty.Multiline = true;
            this.txtDocQty.Name = "txtDocQty";
            this.txtDocQty.ReadOnly = true;
            this.txtDocQty.Size = new System.Drawing.Size(52, 17);
            this.txtDocQty.TabIndex = 27;
            this.txtDocQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDocM3
            // 
            this.txtDocM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDocM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDocM3.Location = new System.Drawing.Point(113, 10);
            this.txtDocM3.Multiline = true;
            this.txtDocM3.Name = "txtDocM3";
            this.txtDocM3.ReadOnly = true;
            this.txtDocM3.Size = new System.Drawing.Size(52, 17);
            this.txtDocM3.TabIndex = 25;
            this.txtDocM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDocMT
            // 
            this.txtDocMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDocMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDocMT.Location = new System.Drawing.Point(58, 10);
            this.txtDocMT.Multiline = true;
            this.txtDocMT.Name = "txtDocMT";
            this.txtDocMT.ReadOnly = true;
            this.txtDocMT.Size = new System.Drawing.Size(52, 17);
            this.txtDocMT.TabIndex = 26;
            this.txtDocMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label1.Location = new System.Drawing.Point(30, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(25, 14);
            this.Label1.Text = "Doc";
            // 
            // txtWHDmgBalQty
            // 
            this.txtWHDmgBalQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtWHDmgBalQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtWHDmgBalQty.Location = new System.Drawing.Point(166, 84);
            this.txtWHDmgBalQty.Multiline = true;
            this.txtWHDmgBalQty.Name = "txtWHDmgBalQty";
            this.txtWHDmgBalQty.ReadOnly = true;
            this.txtWHDmgBalQty.Size = new System.Drawing.Size(52, 17);
            this.txtWHDmgBalQty.TabIndex = 115;
            this.txtWHDmgBalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtWHDmgBalMt
            // 
            this.txtWHDmgBalMt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtWHDmgBalMt.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtWHDmgBalMt.Location = new System.Drawing.Point(58, 84);
            this.txtWHDmgBalMt.Multiline = true;
            this.txtWHDmgBalMt.Name = "txtWHDmgBalMt";
            this.txtWHDmgBalMt.ReadOnly = true;
            this.txtWHDmgBalMt.Size = new System.Drawing.Size(52, 17);
            this.txtWHDmgBalMt.TabIndex = 113;
            this.txtWHDmgBalMt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtWHDmgBalM3
            // 
            this.txtWHDmgBalM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtWHDmgBalM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtWHDmgBalM3.Location = new System.Drawing.Point(112, 84);
            this.txtWHDmgBalM3.Multiline = true;
            this.txtWHDmgBalM3.Name = "txtWHDmgBalM3";
            this.txtWHDmgBalM3.ReadOnly = true;
            this.txtWHDmgBalM3.Size = new System.Drawing.Size(52, 17);
            this.txtWHDmgBalM3.TabIndex = 114;
            this.txtWHDmgBalM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(0, 86);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(62, 15);
            this.tLabel2.Text = "WHDmg Bal";
            // 
            // txtSpareBalQty
            // 
            this.txtSpareBalQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSpareBalQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtSpareBalQty.Location = new System.Drawing.Point(166, 161);
            this.txtSpareBalQty.Multiline = true;
            this.txtSpareBalQty.Name = "txtSpareBalQty";
            this.txtSpareBalQty.ReadOnly = true;
            this.txtSpareBalQty.Size = new System.Drawing.Size(52, 17);
            this.txtSpareBalQty.TabIndex = 120;
            this.txtSpareBalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSpareBalMt
            // 
            this.txtSpareBalMt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSpareBalMt.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtSpareBalMt.Location = new System.Drawing.Point(58, 161);
            this.txtSpareBalMt.Multiline = true;
            this.txtSpareBalMt.Name = "txtSpareBalMt";
            this.txtSpareBalMt.ReadOnly = true;
            this.txtSpareBalMt.Size = new System.Drawing.Size(52, 17);
            this.txtSpareBalMt.TabIndex = 118;
            this.txtSpareBalMt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSpareBalM3
            // 
            this.txtSpareBalM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSpareBalM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtSpareBalM3.Location = new System.Drawing.Point(112, 161);
            this.txtSpareBalM3.Multiline = true;
            this.txtSpareBalM3.Name = "txtSpareBalM3";
            this.txtSpareBalM3.ReadOnly = true;
            this.txtSpareBalM3.Size = new System.Drawing.Size(52, 17);
            this.txtSpareBalM3.TabIndex = 119;
            this.txtSpareBalM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel6
            // 
            this.tLabel6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel6.Location = new System.Drawing.Point(6, 163);
            this.tLabel6.Name = "tLabel6";
            this.tLabel6.Size = new System.Drawing.Size(50, 15);
            this.tLabel6.Text = "Spare Bal";
            // 
            // HAC105002
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.txtSpareBalQty);
            this.Controls.Add(this.txtSpareBalMt);
            this.Controls.Add(this.txtSpareBalM3);
            this.Controls.Add(this.tLabel6);
            this.Controls.Add(this.txtWHDmgBalQty);
            this.Controls.Add(this.txtWHDmgBalMt);
            this.Controls.Add(this.txtWHDmgBalM3);
            this.Controls.Add(this.txtDelvMode);
            this.Controls.Add(this.tLabel4);
            this.Controls.Add(this.btnSpareUnset);
            this.Controls.Add(this.txtSpareLoc);
            this.Controls.Add(this.btnWHDmgUnset);
            this.Controls.Add(this.tLabel3);
            this.Controls.Add(this.txtWHDmgLoc);
            this.Controls.Add(this.txtBalQty);
            this.Controls.Add(this.txtBalMT);
            this.Controls.Add(this.txtBalM3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtSpareQty);
            this.Controls.Add(this.txtSpareMT);
            this.Controls.Add(this.txtSpareM3);
            this.Controls.Add(this.txtWHDmgQty);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.txtWHDmgMT);
            this.Controls.Add(this.txtWHDmgM3);
            this.Controls.Add(this.txtDocQty);
            this.Controls.Add(this.txtDocMT);
            this.Controls.Add(this.txtDocM3);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.tLabel5);
            this.Controls.Add(this.tLabel2);
            this.Name = "HAC105002";
            this.Text = "A/C - Loading WHDmg & Spare";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HAC105002_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TTextBox txtSpareQty;
        internal Framework.Controls.UserControls.TTextBox txtSpareMT;
        internal Framework.Controls.UserControls.TTextBox txtSpareM3;
        internal Framework.Controls.UserControls.TLabel Label5;
        internal Framework.Controls.UserControls.TTextBox txtWHDmgQty;
        internal Framework.Controls.UserControls.TLabel Label6;
        internal Framework.Controls.UserControls.TTextBox txtWHDmgMT;
        internal Framework.Controls.UserControls.TTextBox txtWHDmgM3;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TButton btnConfirm;
        internal Framework.Controls.UserControls.TTextBox txtWHDmgLoc;
        internal Framework.Controls.UserControls.TLabel tLabel3;
        internal Framework.Controls.UserControls.TButton btnWHDmgUnset;
        internal Framework.Controls.UserControls.TTextBox txtSpareLoc;
        internal Framework.Controls.UserControls.TButton btnSpareUnset;
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
        internal Framework.Controls.UserControls.TTextBox txtWHDmgBalQty;
        internal Framework.Controls.UserControls.TTextBox txtWHDmgBalMt;
        internal Framework.Controls.UserControls.TTextBox txtWHDmgBalM3;
        internal Framework.Controls.UserControls.TLabel tLabel2;
        internal Framework.Controls.UserControls.TTextBox txtSpareBalQty;
        internal Framework.Controls.UserControls.TTextBox txtSpareBalMt;
        internal Framework.Controls.UserControls.TTextBox txtSpareBalM3;
        internal Framework.Controls.UserControls.TLabel tLabel6;

    }
}