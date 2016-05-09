namespace MOST.VesselOperator
{
    partial class HVO109
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
            this.btnF2 = new Framework.Controls.UserControls.TButton();
            this.txtPkgType = new Framework.Controls.UserControls.TTextBox();
            this.Label11 = new Framework.Controls.UserControls.TLabel();
            this.txtActQty = new Framework.Controls.UserControls.TTextBox();
            this.txtActMT = new Framework.Controls.UserControls.TTextBox();
            this.txtActM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtDocQty = new Framework.Controls.UserControls.TTextBox();
            this.txtDocMT = new Framework.Controls.UserControls.TTextBox();
            this.Label3 = new Framework.Controls.UserControls.TLabel();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.txtDocM3 = new Framework.Controls.UserControls.TTextBox();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.txtCommodity = new Framework.Controls.UserControls.TTextBox();
            this.Label10 = new Framework.Controls.UserControls.TLabel();
            this.Label6 = new Framework.Controls.UserControls.TLabel();
            this.cboWP = new Framework.Controls.UserControls.TCombobox();
            this.cboOprMode = new Framework.Controls.UserControls.TCombobox();
            this.cboHatch = new Framework.Controls.UserControls.TCombobox();
            this.Label5 = new Framework.Controls.UserControls.TLabel();
            this.Label1 = new Framework.Controls.UserControls.TLabel();
            this.Label4 = new Framework.Controls.UserControls.TLabel();
            this.txtNextJPVC = new Framework.Controls.UserControls.TTextBox();
            this.btnList = new Framework.Controls.UserControls.TButton();
            this.tLabel4 = new Framework.Controls.UserControls.TLabel();
            this.tLabel6 = new Framework.Controls.UserControls.TLabel();
            this.txtBalQty = new Framework.Controls.UserControls.TTextBox();
            this.txtBalMT = new Framework.Controls.UserControls.TTextBox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.txtBalM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtEndTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtStartTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.cboCargoType = new Framework.Controls.UserControls.TCombobox();
            this.lblCargoType = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(95, 242);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 24;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnF2
            // 
            this.btnF2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF2.Location = new System.Drawing.Point(138, 115);
            this.btnF2.Name = "btnF2";
            this.btnF2.Size = new System.Drawing.Size(40, 19);
            this.btnF2.TabIndex = 10;
            this.btnF2.Text = "F2";
            this.btnF2.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtPkgType
            // 
            this.txtPkgType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtPkgType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtPkgType.isBusinessItemName = "Pkg Type";
            this.txtPkgType.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtPkgType.Location = new System.Drawing.Point(62, 115);
            this.txtPkgType.Name = "txtPkgType";
            this.txtPkgType.Size = new System.Drawing.Size(71, 19);
            this.txtPkgType.TabIndex = 9;
            // 
            // Label11
            // 
            this.Label11.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label11.Location = new System.Drawing.Point(5, 119);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(50, 15);
            this.Label11.Text = "Pkg Type";
            // 
            // txtActQty
            // 
            this.txtActQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtActQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtActQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtActQty.isBusinessItemName = "Actual Qty";
            this.txtActQty.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtActQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtActQty.Location = new System.Drawing.Point(162, 180);
            this.txtActQty.Name = "txtActQty";
            this.txtActQty.Size = new System.Drawing.Size(54, 19);
            this.txtActQty.TabIndex = 18;
            this.txtActQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtActMT
            // 
            this.txtActMT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtActMT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtActMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtActMT.isBusinessItemName = "Actual MT";
            this.txtActMT.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtActMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtActMT.Location = new System.Drawing.Point(43, 180);
            this.txtActMT.Name = "txtActMT";
            this.txtActMT.Size = new System.Drawing.Size(54, 19);
            this.txtActMT.TabIndex = 16;
            this.txtActMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtActM3
            // 
            this.txtActM3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtActM3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtActM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtActM3.isBusinessItemName = "Actual M3";
            this.txtActM3.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtActM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtActM3.Location = new System.Drawing.Point(102, 180);
            this.txtActM3.Name = "txtActM3";
            this.txtActM3.Size = new System.Drawing.Size(54, 19);
            this.txtActM3.TabIndex = 17;
            this.txtActM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDocQty
            // 
            this.txtDocQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDocQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtDocQty.Location = new System.Drawing.Point(162, 138);
            this.txtDocQty.Name = "txtDocQty";
            this.txtDocQty.ReadOnly = true;
            this.txtDocQty.Size = new System.Drawing.Size(54, 19);
            this.txtDocQty.TabIndex = 15;
            this.txtDocQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDocMT
            // 
            this.txtDocMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDocMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDocMT.Location = new System.Drawing.Point(43, 138);
            this.txtDocMT.Name = "txtDocMT";
            this.txtDocMT.ReadOnly = true;
            this.txtDocMT.Size = new System.Drawing.Size(54, 19);
            this.txtDocMT.TabIndex = 13;
            this.txtDocMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label3.Location = new System.Drawing.Point(12, 141);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(30, 16);
            this.Label3.Text = "Doc.";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(166, 242);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtDocM3
            // 
            this.txtDocM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDocM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDocM3.Location = new System.Drawing.Point(102, 138);
            this.txtDocM3.Name = "txtDocM3";
            this.txtDocM3.ReadOnly = true;
            this.txtDocM3.Size = new System.Drawing.Size(54, 19);
            this.txtDocM3.TabIndex = 14;
            this.txtDocM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnF1
            // 
            this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF1.Location = new System.Drawing.Point(138, 94);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(40, 19);
            this.btnF1.TabIndex = 8;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtCommodity
            // 
            this.txtCommodity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtCommodity.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtCommodity.isBusinessItemName = "Commodity";
            this.txtCommodity.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtCommodity.Location = new System.Drawing.Point(62, 94);
            this.txtCommodity.Name = "txtCommodity";
            this.txtCommodity.Size = new System.Drawing.Size(71, 19);
            this.txtCommodity.TabIndex = 7;
            // 
            // Label10
            // 
            this.Label10.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label10.Location = new System.Drawing.Point(1, 98);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(59, 15);
            this.Label10.Text = "Commodity";
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label6.Location = new System.Drawing.Point(0, 27);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(54, 15);
            this.Label6.Text = "OPR Mode";
            // 
            // cboWP
            // 
            this.cboWP.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboWP.isBusinessItemName = "";
            this.cboWP.isMandatory = false;
            this.cboWP.Location = new System.Drawing.Point(124, 46);
            this.cboWP.Name = "cboWP";
            this.cboWP.Size = new System.Drawing.Size(54, 19);
            this.cboWP.TabIndex = 5;
            // 
            // cboOprMode
            // 
            this.cboOprMode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboOprMode.isBusinessItemName = "OPR Mode";
            this.cboOprMode.isMandatory = true;
            this.cboOprMode.Location = new System.Drawing.Point(62, 23);
            this.cboOprMode.Name = "cboOprMode";
            this.cboOprMode.Size = new System.Drawing.Size(116, 19);
            this.cboOprMode.TabIndex = 3;
            this.cboOprMode.SelectedIndexChanged += new System.EventHandler(this.CboSelectedIndexChanged);
            // 
            // cboHatch
            // 
            this.cboHatch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboHatch.isBusinessItemName = "";
            this.cboHatch.isMandatory = false;
            this.cboHatch.Location = new System.Drawing.Point(62, 46);
            this.cboHatch.Name = "cboHatch";
            this.cboHatch.Size = new System.Drawing.Size(57, 19);
            this.cboHatch.TabIndex = 4;
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label5.Location = new System.Drawing.Point(8, 50);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(33, 15);
            this.Label5.Text = "Hatch";
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label1.Location = new System.Drawing.Point(11, 5);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(49, 15);
            this.Label1.Text = "2nd JPVC";
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label4.Location = new System.Drawing.Point(5, 183);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(37, 16);
            this.Label4.Text = "Actual";
            // 
            // txtNextJPVC
            // 
            this.txtNextJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtNextJPVC.isBusinessItemName = "Next JPVC";
            this.txtNextJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtNextJPVC.isMandatory = true;
            this.txtNextJPVC.Location = new System.Drawing.Point(62, 3);
            this.txtNextJPVC.Multiline = true;
            this.txtNextJPVC.Name = "txtNextJPVC";
            this.txtNextJPVC.ReadOnly = true;
            this.txtNextJPVC.Size = new System.Drawing.Size(154, 17);
            this.txtNextJPVC.TabIndex = 227;
            // 
            // btnList
            // 
            this.btnList.Location = new System.Drawing.Point(3, 242);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(58, 24);
            this.btnList.TabIndex = 26;
            this.btnList.Text = "List";
            this.btnList.Click += new System.EventHandler(this.ActionListener);
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(121, 202);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(50, 13);
            this.tLabel4.Text = "EndTime";
            // 
            // tLabel6
            // 
            this.tLabel6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel6.Location = new System.Drawing.Point(1, 202);
            this.tLabel6.Name = "tLabel6";
            this.tLabel6.Size = new System.Drawing.Size(51, 13);
            this.tLabel6.Text = "StartTime";
            // 
            // txtBalQty
            // 
            this.txtBalQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtBalQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBalQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtBalQty.Location = new System.Drawing.Point(162, 159);
            this.txtBalQty.Name = "txtBalQty";
            this.txtBalQty.ReadOnly = true;
            this.txtBalQty.Size = new System.Drawing.Size(54, 19);
            this.txtBalQty.TabIndex = 208;
            this.txtBalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBalMT
            // 
            this.txtBalMT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtBalMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBalMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtBalMT.Location = new System.Drawing.Point(43, 159);
            this.txtBalMT.Name = "txtBalMT";
            this.txtBalMT.ReadOnly = true;
            this.txtBalMT.Size = new System.Drawing.Size(54, 19);
            this.txtBalMT.TabIndex = 206;
            this.txtBalMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(11, 162);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(30, 16);
            this.tLabel1.Text = "Bal.";
            // 
            // txtBalM3
            // 
            this.txtBalM3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtBalM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBalM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtBalM3.Location = new System.Drawing.Point(102, 159);
            this.txtBalM3.Name = "txtBalM3";
            this.txtBalM3.ReadOnly = true;
            this.txtBalM3.Size = new System.Drawing.Size(54, 19);
            this.txtBalM3.TabIndex = 207;
            this.txtBalM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtEndTime
            // 
            this.txtEndTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtEndTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtEndTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtEndTime.isBusinessItemName = "End Time";
            this.txtEndTime.isMandatory = true;
            this.txtEndTime.Location = new System.Drawing.Point(121, 216);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(116, 20);
            this.txtEndTime.TabIndex = 21;
            this.txtEndTime.Value = new System.DateTime(2008, 10, 23, 18, 35, 0, 890);
            // 
            // txtStartTime
            // 
            this.txtStartTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtStartTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtStartTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtStartTime.isBusinessItemName = "Start Time";
            this.txtStartTime.isMandatory = true;
            this.txtStartTime.Location = new System.Drawing.Point(1, 216);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(116, 20);
            this.txtStartTime.TabIndex = 20;
            this.txtStartTime.Value = new System.DateTime(2008, 10, 23, 18, 35, 0, 203);
            // 
            // cboCargoType
            // 
            this.cboCargoType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboCargoType.isBusinessItemName = "Cargo Type";
            this.cboCargoType.isMandatory = true;
            this.cboCargoType.Location = new System.Drawing.Point(62, 69);
            this.cboCargoType.Name = "cboCargoType";
            this.cboCargoType.Size = new System.Drawing.Size(116, 19);
            this.cboCargoType.TabIndex = 242;
            this.cboCargoType.SelectedIndexChanged += new System.EventHandler(this.CboSelectedIndexChanged);
            // 
            // lblCargoType
            // 
            this.lblCargoType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblCargoType.Location = new System.Drawing.Point(0, 73);
            this.lblCargoType.Name = "lblCargoType";
            this.lblCargoType.Size = new System.Drawing.Size(61, 15);
            this.lblCargoType.Text = "Cargo Type";
            // 
            // HVO109
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.cboCargoType);
            this.Controls.Add(this.lblCargoType);
            this.Controls.Add(this.txtEndTime);
            this.Controls.Add(this.txtStartTime);
            this.Controls.Add(this.txtBalQty);
            this.Controls.Add(this.txtBalMT);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.txtBalM3);
            this.Controls.Add(this.tLabel4);
            this.Controls.Add(this.tLabel6);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.txtNextJPVC);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnF2);
            this.Controls.Add(this.txtPkgType);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.txtActQty);
            this.Controls.Add(this.txtActMT);
            this.Controls.Add(this.txtActM3);
            this.Controls.Add(this.txtDocQty);
            this.Controls.Add(this.txtDocMT);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtDocM3);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.txtCommodity);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.cboWP);
            this.Controls.Add(this.cboOprMode);
            this.Controls.Add(this.cboHatch);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label4);
            this.Name = "HVO109";
            this.Text = "V/S - STS";
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TButton btnF2;
        internal Framework.Controls.UserControls.TTextBox txtPkgType;
        internal Framework.Controls.UserControls.TLabel Label11;
        internal Framework.Controls.UserControls.TTextBox txtActQty;
        internal Framework.Controls.UserControls.TTextBox txtActMT;
        internal Framework.Controls.UserControls.TTextBox txtActM3;
        internal Framework.Controls.UserControls.TTextBox txtDocQty;
        internal Framework.Controls.UserControls.TTextBox txtDocMT;
        internal Framework.Controls.UserControls.TLabel Label3;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TTextBox txtDocM3;
        internal Framework.Controls.UserControls.TButton btnF1;
        internal Framework.Controls.UserControls.TTextBox txtCommodity;
        internal Framework.Controls.UserControls.TLabel Label10;
        internal Framework.Controls.UserControls.TLabel Label6;
        internal Framework.Controls.UserControls.TCombobox cboWP;
        internal Framework.Controls.UserControls.TCombobox cboOprMode;
        internal Framework.Controls.UserControls.TCombobox cboHatch;
        internal Framework.Controls.UserControls.TLabel Label5;
        internal Framework.Controls.UserControls.TLabel Label1;
        internal Framework.Controls.UserControls.TLabel Label4;
        internal Framework.Controls.UserControls.TTextBox txtNextJPVC;
        internal Framework.Controls.UserControls.TButton btnList;
        internal Framework.Controls.UserControls.TLabel tLabel4;
        internal Framework.Controls.UserControls.TLabel tLabel6;
        internal Framework.Controls.UserControls.TTextBox txtBalQty;
        internal Framework.Controls.UserControls.TTextBox txtBalMT;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        internal Framework.Controls.UserControls.TTextBox txtBalM3;
        private Framework.Controls.UserControls.TDateTimePicker txtEndTime;
        private Framework.Controls.UserControls.TDateTimePicker txtStartTime;
        internal Framework.Controls.UserControls.TCombobox cboCargoType;
        internal Framework.Controls.UserControls.TLabel lblCargoType;
    }
}