namespace MOST.WHChecker
{
    partial class HWC108
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
            this.cboClearance = new Framework.Controls.UserControls.TCombobox();
            this.txtTsptr = new Framework.Controls.UserControls.TTextBox();
            this.txtLoadLoc = new Framework.Controls.UserControls.TTextBox();
            this.tLabel13 = new Framework.Controls.UserControls.TLabel();
            this.txtPkgNo = new Framework.Controls.UserControls.TTextBox();
            this.txtEndTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtStartTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtActQty = new Framework.Controls.UserControls.TTextBox();
            this.txtActM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtDocMT = new Framework.Controls.UserControls.TTextBox();
            this.tLabel5 = new Framework.Controls.UserControls.TLabel();
            this.txtBalQty = new Framework.Controls.UserControls.TTextBox();
            this.txtBalM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtBalMT = new Framework.Controls.UserControls.TTextBox();
            this.tLabel4 = new Framework.Controls.UserControls.TLabel();
            this.btnUnsetLoc = new Framework.Controls.UserControls.TButton();
            this.txtLoadQty = new Framework.Controls.UserControls.TTextBox();
            this.txtLoadM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtLoadMT = new Framework.Controls.UserControls.TTextBox();
            this.txtDocQty = new Framework.Controls.UserControls.TTextBox();
            this.txtDocM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtActMT = new Framework.Controls.UserControls.TTextBox();
            this.lblLorry = new Framework.Controls.UserControls.TLabel();
            this.Label14 = new Framework.Controls.UserControls.TLabel();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.txtLorry = new Framework.Controls.UserControls.TTextBox();
            this.txtCgNo = new Framework.Controls.UserControls.TTextBox();
            this.Label10 = new Framework.Controls.UserControls.TLabel();
            this.chkFinal = new System.Windows.Forms.CheckBox();
            this.tLabel11 = new Framework.Controls.UserControls.TLabel();
            this.tLabel12 = new Framework.Controls.UserControls.TLabel();
            this.tLabel14 = new Framework.Controls.UserControls.TLabel();
            this.tLabel16 = new Framework.Controls.UserControls.TLabel();
            this.tLabel17 = new Framework.Controls.UserControls.TLabel();
            this.txtRemark = new Framework.Controls.UserControls.TTextBox();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(95, 242);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 30;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 242);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 31;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // cboClearance
            // 
            this.cboClearance.Enabled = false;
            this.cboClearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboClearance.isBusinessItemName = "";
            this.cboClearance.isMandatory = false;
            this.cboClearance.Location = new System.Drawing.Point(153, 3);
            this.cboClearance.Name = "cboClearance";
            this.cboClearance.Size = new System.Drawing.Size(82, 19);
            this.cboClearance.TabIndex = 2;
            // 
            // txtTsptr
            // 
            this.txtTsptr.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtTsptr.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtTsptr.Location = new System.Drawing.Point(153, 28);
            this.txtTsptr.Multiline = true;
            this.txtTsptr.Name = "txtTsptr";
            this.txtTsptr.ReadOnly = true;
            this.txtTsptr.Size = new System.Drawing.Size(81, 17);
            this.txtTsptr.TabIndex = 205;
            // 
            // txtLoadLoc
            // 
            this.txtLoadLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtLoadLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtLoadLoc.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtLoadLoc.Location = new System.Drawing.Point(57, 192);
            this.txtLoadLoc.Multiline = true;
            this.txtLoadLoc.Name = "txtLoadLoc";
            this.txtLoadLoc.ReadOnly = true;
            this.txtLoadLoc.Size = new System.Drawing.Size(89, 17);
            this.txtLoadLoc.TabIndex = 22;
            // 
            // tLabel13
            // 
            this.tLabel13.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel13.Location = new System.Drawing.Point(10, 194);
            this.tLabel13.Name = "tLabel13";
            this.tLabel13.Size = new System.Drawing.Size(48, 15);
            this.tLabel13.Text = "Location";
            // 
            // txtPkgNo
            // 
            this.txtPkgNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtPkgNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtPkgNo.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtPkgNo.Location = new System.Drawing.Point(40, 47);
            this.txtPkgNo.Multiline = true;
            this.txtPkgNo.Name = "txtPkgNo";
            this.txtPkgNo.Size = new System.Drawing.Size(194, 17);
            this.txtPkgNo.TabIndex = 7;
            // 
            // txtEndTime
            // 
            this.txtEndTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtEndTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtEndTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtEndTime.isBusinessItemName = "";
            this.txtEndTime.isMandatory = false;
            this.txtEndTime.Location = new System.Drawing.Point(57, 90);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(114, 20);
            this.txtEndTime.TabIndex = 11;
            this.txtEndTime.Value = new System.DateTime(2008, 10, 24, 16, 17, 0, 828);
            // 
            // txtStartTime
            // 
            this.txtStartTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtStartTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtStartTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtStartTime.isBusinessItemName = "Start Date";
            this.txtStartTime.isMandatory = true;
            this.txtStartTime.Location = new System.Drawing.Point(57, 68);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(114, 20);
            this.txtStartTime.TabIndex = 9;
            this.txtStartTime.Value = new System.DateTime(2008, 10, 24, 16, 17, 0, 437);
            // 
            // txtActQty
            // 
            this.txtActQty.BackColor = System.Drawing.Color.White;
            this.txtActQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtActQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtActQty.Location = new System.Drawing.Point(171, 134);
            this.txtActQty.Multiline = true;
            this.txtActQty.Name = "txtActQty";
            this.txtActQty.ReadOnly = true;
            this.txtActQty.Size = new System.Drawing.Size(54, 17);
            this.txtActQty.TabIndex = 200;
            this.txtActQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtActM3
            // 
            this.txtActM3.BackColor = System.Drawing.Color.White;
            this.txtActM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtActM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtActM3.Location = new System.Drawing.Point(114, 134);
            this.txtActM3.Multiline = true;
            this.txtActM3.Name = "txtActM3";
            this.txtActM3.ReadOnly = true;
            this.txtActM3.Size = new System.Drawing.Size(54, 17);
            this.txtActM3.TabIndex = 199;
            this.txtActM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDocMT
            // 
            this.txtDocMT.BackColor = System.Drawing.Color.White;
            this.txtDocMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDocMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDocMT.Location = new System.Drawing.Point(57, 115);
            this.txtDocMT.Multiline = true;
            this.txtDocMT.Name = "txtDocMT";
            this.txtDocMT.ReadOnly = true;
            this.txtDocMT.Size = new System.Drawing.Size(54, 17);
            this.txtDocMT.TabIndex = 198;
            this.txtDocMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel5
            // 
            this.tLabel5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel5.Location = new System.Drawing.Point(8, 136);
            this.tLabel5.Name = "tLabel5";
            this.tLabel5.Size = new System.Drawing.Size(42, 15);
            this.tLabel5.Text = "Actual";
            // 
            // txtBalQty
            // 
            this.txtBalQty.BackColor = System.Drawing.Color.White;
            this.txtBalQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBalQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtBalQty.Location = new System.Drawing.Point(171, 153);
            this.txtBalQty.Multiline = true;
            this.txtBalQty.Name = "txtBalQty";
            this.txtBalQty.ReadOnly = true;
            this.txtBalQty.Size = new System.Drawing.Size(54, 17);
            this.txtBalQty.TabIndex = 192;
            this.txtBalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBalM3
            // 
            this.txtBalM3.BackColor = System.Drawing.Color.White;
            this.txtBalM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBalM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtBalM3.Location = new System.Drawing.Point(114, 153);
            this.txtBalM3.Multiline = true;
            this.txtBalM3.Name = "txtBalM3";
            this.txtBalM3.ReadOnly = true;
            this.txtBalM3.Size = new System.Drawing.Size(54, 17);
            this.txtBalM3.TabIndex = 191;
            this.txtBalM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBalMT
            // 
            this.txtBalMT.BackColor = System.Drawing.Color.White;
            this.txtBalMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBalMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtBalMT.Location = new System.Drawing.Point(57, 153);
            this.txtBalMT.Multiline = true;
            this.txtBalMT.Name = "txtBalMT";
            this.txtBalMT.ReadOnly = true;
            this.txtBalMT.Size = new System.Drawing.Size(54, 17);
            this.txtBalMT.TabIndex = 190;
            this.txtBalMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(10, 155);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(44, 15);
            this.tLabel4.Text = "W/H Bal";
            // 
            // btnUnsetLoc
            // 
            this.btnUnsetLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnUnsetLoc.Location = new System.Drawing.Point(153, 191);
            this.btnUnsetLoc.Name = "btnUnsetLoc";
            this.btnUnsetLoc.Size = new System.Drawing.Size(72, 19);
            this.btnUnsetLoc.TabIndex = 23;
            this.btnUnsetLoc.Text = "UnSetLoc";
            this.btnUnsetLoc.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtLoadQty
            // 
            this.txtLoadQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtLoadQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtLoadQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtLoadQty.Location = new System.Drawing.Point(171, 172);
            this.txtLoadQty.Multiline = true;
            this.txtLoadQty.Name = "txtLoadQty";
            this.txtLoadQty.Size = new System.Drawing.Size(54, 17);
            this.txtLoadQty.TabIndex = 19;
            this.txtLoadQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLoadM3
            // 
            this.txtLoadM3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtLoadM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtLoadM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtLoadM3.Location = new System.Drawing.Point(114, 172);
            this.txtLoadM3.Multiline = true;
            this.txtLoadM3.Name = "txtLoadM3";
            this.txtLoadM3.Size = new System.Drawing.Size(54, 17);
            this.txtLoadM3.TabIndex = 17;
            this.txtLoadM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLoadMT
            // 
            this.txtLoadMT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtLoadMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtLoadMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtLoadMT.Location = new System.Drawing.Point(57, 172);
            this.txtLoadMT.Multiline = true;
            this.txtLoadMT.Name = "txtLoadMT";
            this.txtLoadMT.Size = new System.Drawing.Size(54, 17);
            this.txtLoadMT.TabIndex = 15;
            this.txtLoadMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDocQty
            // 
            this.txtDocQty.BackColor = System.Drawing.Color.White;
            this.txtDocQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDocQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDocQty.Location = new System.Drawing.Point(171, 115);
            this.txtDocQty.Multiline = true;
            this.txtDocQty.Name = "txtDocQty";
            this.txtDocQty.ReadOnly = true;
            this.txtDocQty.Size = new System.Drawing.Size(54, 17);
            this.txtDocQty.TabIndex = 186;
            this.txtDocQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDocM3
            // 
            this.txtDocM3.BackColor = System.Drawing.Color.White;
            this.txtDocM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDocM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDocM3.Location = new System.Drawing.Point(114, 115);
            this.txtDocM3.Multiline = true;
            this.txtDocM3.Name = "txtDocM3";
            this.txtDocM3.ReadOnly = true;
            this.txtDocM3.Size = new System.Drawing.Size(54, 17);
            this.txtDocM3.TabIndex = 185;
            this.txtDocM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtActMT
            // 
            this.txtActMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtActMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtActMT.Location = new System.Drawing.Point(57, 134);
            this.txtActMT.Multiline = true;
            this.txtActMT.Name = "txtActMT";
            this.txtActMT.ReadOnly = true;
            this.txtActMT.Size = new System.Drawing.Size(54, 17);
            this.txtActMT.TabIndex = 184;
            this.txtActMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblLorry
            // 
            this.lblLorry.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblLorry.Location = new System.Drawing.Point(0, 174);
            this.lblLorry.Name = "lblLorry";
            this.lblLorry.Size = new System.Drawing.Size(53, 15);
            this.lblLorry.Text = "Lorry load";
            // 
            // Label14
            // 
            this.Label14.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label14.Location = new System.Drawing.Point(10, 117);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(42, 15);
            this.Label14.Text = "Doc";
            // 
            // btnF1
            // 
            this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF1.Location = new System.Drawing.Point(113, 28);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(37, 17);
            this.btnF1.TabIndex = 5;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtLorry
            // 
            this.txtLorry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtLorry.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtLorry.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtLorry.Location = new System.Drawing.Point(40, 28);
            this.txtLorry.Multiline = true;
            this.txtLorry.Name = "txtLorry";
            this.txtLorry.Size = new System.Drawing.Size(69, 17);
            this.txtLorry.TabIndex = 4;
            // 
            // txtCgNo
            // 
            this.txtCgNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtCgNo.isBusinessItemName = "Cargo No";
            this.txtCgNo.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtCgNo.isMandatory = true;
            this.txtCgNo.Location = new System.Drawing.Point(31, 5);
            this.txtCgNo.Multiline = true;
            this.txtCgNo.Name = "txtCgNo";
            this.txtCgNo.ReadOnly = true;
            this.txtCgNo.Size = new System.Drawing.Size(95, 17);
            this.txtCgNo.TabIndex = 1;
            // 
            // Label10
            // 
            this.Label10.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label10.Location = new System.Drawing.Point(8, 29);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(29, 15);
            this.Label10.Text = "Lorry";
            // 
            // chkFinal
            // 
            this.chkFinal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.chkFinal.Location = new System.Drawing.Point(19, 246);
            this.chkFinal.Name = "chkFinal";
            this.chkFinal.Size = new System.Drawing.Size(50, 17);
            this.chkFinal.TabIndex = 28;
            this.chkFinal.Text = "Final";
            // 
            // tLabel11
            // 
            this.tLabel11.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel11.Location = new System.Drawing.Point(1, 47);
            this.tLabel11.Name = "tLabel11";
            this.tLabel11.Size = new System.Drawing.Size(38, 17);
            this.tLabel11.Text = "Pkg No";
            // 
            // tLabel12
            // 
            this.tLabel12.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel12.Location = new System.Drawing.Point(0, 7);
            this.tLabel12.Name = "tLabel12";
            this.tLabel12.Size = new System.Drawing.Size(33, 15);
            this.tLabel12.Text = "CgNo";
            // 
            // tLabel14
            // 
            this.tLabel14.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel14.Location = new System.Drawing.Point(4, 71);
            this.tLabel14.Name = "tLabel14";
            this.tLabel14.Size = new System.Drawing.Size(56, 17);
            this.tLabel14.Text = "Start Time";
            // 
            // tLabel16
            // 
            this.tLabel16.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel16.Location = new System.Drawing.Point(4, 91);
            this.tLabel16.Name = "tLabel16";
            this.tLabel16.Size = new System.Drawing.Size(54, 17);
            this.tLabel16.Text = "End Time";
            // 
            // tLabel17
            // 
            this.tLabel17.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel17.Location = new System.Drawing.Point(129, 5);
            this.tLabel17.Name = "tLabel17";
            this.tLabel17.Size = new System.Drawing.Size(26, 17);
            this.tLabel17.Text = "CLR";
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtRemark.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtRemark.isBusinessItemName = "Remark";
            this.txtRemark.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtRemark.Location = new System.Drawing.Point(57, 215);
            this.txtRemark.MaxLength = 50;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(168, 19);
            this.txtRemark.TabIndex = 26;
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(24, 217);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(26, 15);
            this.tLabel2.Text = "Rmk";
            // 
            // HWC108
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.cboClearance);
            this.Controls.Add(this.txtTsptr);
            this.Controls.Add(this.txtLoadLoc);
            this.Controls.Add(this.tLabel13);
            this.Controls.Add(this.txtPkgNo);
            this.Controls.Add(this.txtEndTime);
            this.Controls.Add(this.txtStartTime);
            this.Controls.Add(this.txtActQty);
            this.Controls.Add(this.txtActM3);
            this.Controls.Add(this.txtDocMT);
            this.Controls.Add(this.tLabel5);
            this.Controls.Add(this.txtBalQty);
            this.Controls.Add(this.txtBalM3);
            this.Controls.Add(this.txtBalMT);
            this.Controls.Add(this.tLabel4);
            this.Controls.Add(this.btnUnsetLoc);
            this.Controls.Add(this.txtLoadQty);
            this.Controls.Add(this.txtLoadM3);
            this.Controls.Add(this.txtLoadMT);
            this.Controls.Add(this.txtDocQty);
            this.Controls.Add(this.txtDocM3);
            this.Controls.Add(this.txtActMT);
            this.Controls.Add(this.lblLorry);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.txtLorry);
            this.Controls.Add(this.txtCgNo);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.chkFinal);
            this.Controls.Add(this.tLabel11);
            this.Controls.Add(this.tLabel12);
            this.Controls.Add(this.tLabel14);
            this.Controls.Add(this.tLabel16);
            this.Controls.Add(this.tLabel17);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Name = "HWC108";
            this.Text = "W/C - Rehandle H/O";
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TCombobox cboClearance;
        internal Framework.Controls.UserControls.TTextBox txtTsptr;
        internal Framework.Controls.UserControls.TTextBox txtLoadLoc;
        internal Framework.Controls.UserControls.TLabel tLabel13;
        internal Framework.Controls.UserControls.TTextBox txtPkgNo;
        private Framework.Controls.UserControls.TDateTimePicker txtEndTime;
        private Framework.Controls.UserControls.TDateTimePicker txtStartTime;
        internal Framework.Controls.UserControls.TTextBox txtActQty;
        internal Framework.Controls.UserControls.TTextBox txtActM3;
        internal Framework.Controls.UserControls.TTextBox txtDocMT;
        internal Framework.Controls.UserControls.TLabel tLabel5;
        internal Framework.Controls.UserControls.TTextBox txtBalQty;
        internal Framework.Controls.UserControls.TTextBox txtBalM3;
        internal Framework.Controls.UserControls.TTextBox txtBalMT;
        internal Framework.Controls.UserControls.TLabel tLabel4;
        internal Framework.Controls.UserControls.TButton btnUnsetLoc;
        internal Framework.Controls.UserControls.TTextBox txtLoadQty;
        internal Framework.Controls.UserControls.TTextBox txtLoadM3;
        internal Framework.Controls.UserControls.TTextBox txtLoadMT;
        internal Framework.Controls.UserControls.TTextBox txtDocQty;
        internal Framework.Controls.UserControls.TTextBox txtDocM3;
        internal Framework.Controls.UserControls.TTextBox txtActMT;
        internal Framework.Controls.UserControls.TLabel lblLorry;
        internal Framework.Controls.UserControls.TLabel Label14;
        internal Framework.Controls.UserControls.TButton btnF1;
        internal Framework.Controls.UserControls.TTextBox txtLorry;
        internal Framework.Controls.UserControls.TTextBox txtCgNo;
        internal Framework.Controls.UserControls.TLabel Label10;
        internal System.Windows.Forms.CheckBox chkFinal;
        internal Framework.Controls.UserControls.TLabel tLabel11;
        internal Framework.Controls.UserControls.TLabel tLabel12;
        internal Framework.Controls.UserControls.TLabel tLabel14;
        internal Framework.Controls.UserControls.TLabel tLabel16;
        internal Framework.Controls.UserControls.TLabel tLabel17;
        internal Framework.Controls.UserControls.TTextBox txtRemark;
        internal Framework.Controls.UserControls.TLabel tLabel2;
    }
}