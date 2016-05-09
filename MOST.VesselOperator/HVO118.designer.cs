namespace MOST.VesselOperator
{
    partial class HVO118
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
            this.Label9 = new Framework.Controls.UserControls.TLabel();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.txtPkgType = new Framework.Controls.UserControls.TTextBox();
            this.Label11 = new Framework.Controls.UserControls.TLabel();
            this.Label8 = new Framework.Controls.UserControls.TLabel();
            this.txtQty = new Framework.Controls.UserControls.TTextBox();
            this.txtMT = new Framework.Controls.UserControls.TTextBox();
            this.Label3 = new Framework.Controls.UserControls.TLabel();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.txtM3 = new Framework.Controls.UserControls.TTextBox();
            this.Label7 = new Framework.Controls.UserControls.TLabel();
            this.txtRemark = new Framework.Controls.UserControls.TTextBox();
            this.Label10 = new Framework.Controls.UserControls.TLabel();
            this.cboWP = new Framework.Controls.UserControls.TCombobox();
            this.cboStcrDiv = new Framework.Controls.UserControls.TCombobox();
            this.cboHatch = new Framework.Controls.UserControls.TCombobox();
            this.Label5 = new Framework.Controls.UserControls.TLabel();
            this.txtStartTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtEndTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.tLabel4 = new Framework.Controls.UserControls.TLabel();
            this.tLabel6 = new Framework.Controls.UserControls.TLabel();
            this.btnList = new Framework.Controls.UserControls.TButton();
            this.cboSftTp = new Framework.Controls.UserControls.TCombobox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.cboNextHatch = new Framework.Controls.UserControls.TCombobox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(90, 242);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 18;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // Label9
            // 
            this.Label9.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label9.Location = new System.Drawing.Point(160, 174);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(48, 12);
            this.Label9.Text = "QTY";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnF1
            // 
            this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF1.Location = new System.Drawing.Point(145, 86);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(35, 20);
            this.btnF1.TabIndex = 10;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtPkgType
            // 
            this.txtPkgType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtPkgType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtPkgType.isBusinessItemName = "Package Type";
            this.txtPkgType.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtPkgType.Location = new System.Drawing.Point(68, 87);
            this.txtPkgType.Name = "txtPkgType";
            this.txtPkgType.Size = new System.Drawing.Size(71, 19);
            this.txtPkgType.TabIndex = 9;
            // 
            // Label11
            // 
            this.Label11.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label11.Location = new System.Drawing.Point(8, 91);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(55, 15);
            this.Label11.Text = "Pkg Type";
            // 
            // Label8
            // 
            this.Label8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label8.Location = new System.Drawing.Point(107, 174);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(48, 12);
            this.Label8.Text = "M3";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtQty.Location = new System.Drawing.Point(160, 186);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(48, 19);
            this.txtQty.TabIndex = 15;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMT
            // 
            this.txtMT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtMT.Location = new System.Drawing.Point(54, 186);
            this.txtMT.Name = "txtMT";
            this.txtMT.Size = new System.Drawing.Size(48, 19);
            this.txtMT.TabIndex = 13;
            this.txtMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label3.Location = new System.Drawing.Point(23, 190);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(30, 15);
            this.Label3.Text = "Shift";
            // 
            // btnCancel
            // 
            //this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(162, 242);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtM3
            // 
            this.txtM3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtM3.Location = new System.Drawing.Point(107, 186);
            this.txtM3.Name = "txtM3";
            this.txtM3.Size = new System.Drawing.Size(48, 19);
            this.txtM3.TabIndex = 14;
            this.txtM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label7
            // 
            this.Label7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label7.Location = new System.Drawing.Point(54, 174);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(48, 12);
            this.Label7.Text = "MT";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtRemark.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtRemark.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtRemark.Location = new System.Drawing.Point(54, 211);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(154, 19);
            this.txtRemark.TabIndex = 16;
            // 
            // Label10
            // 
            this.Label10.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label10.Location = new System.Drawing.Point(13, 215);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(48, 15);
            this.Label10.Text = "Remark";
            // 
            // cboWP
            // 
            this.cboWP.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboWP.isBusinessItemName = "AP/FP";
            this.cboWP.isMandatory = false;
            this.cboWP.Location = new System.Drawing.Point(84, 3);
            this.cboWP.Name = "cboWP";
            this.cboWP.Size = new System.Drawing.Size(48, 19);
            this.cboWP.TabIndex = 5;
            // 
            // cboStcrDiv
            // 
            this.cboStcrDiv.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboStcrDiv.isBusinessItemName = "Stevedore / Ship\'s Crew";
            this.cboStcrDiv.isMandatory = true;
            this.cboStcrDiv.Location = new System.Drawing.Point(135, 3);
            this.cboStcrDiv.Name = "cboStcrDiv";
            this.cboStcrDiv.Size = new System.Drawing.Size(100, 19);
            this.cboStcrDiv.TabIndex = 6;
            // 
            // cboHatch
            // 
            this.cboHatch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboHatch.isBusinessItemName = "Hatch";
            this.cboHatch.isMandatory = true;
            this.cboHatch.Location = new System.Drawing.Point(32, 3);
            this.cboHatch.Name = "cboHatch";
            this.cboHatch.Size = new System.Drawing.Size(49, 19);
            this.cboHatch.TabIndex = 4;
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label5.Location = new System.Drawing.Point(1, 6);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(32, 16);
            this.Label5.Text = "Hatch";
            // 
            // txtStartTime
            // 
            this.txtStartTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtStartTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtStartTime.isBusinessItemName = "Start Time";
            this.txtStartTime.isMandatory = true;
            this.txtStartTime.Location = new System.Drawing.Point(68, 112);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(131, 24);
            this.txtStartTime.TabIndex = 11;
            this.txtStartTime.Value = new System.DateTime(2008, 10, 20, 15, 4, 25, 812);
            // 
            // txtEndTime
            // 
            this.txtEndTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtEndTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtEndTime.isBusinessItemName = "End Time";
            this.txtEndTime.isMandatory = true;
            this.txtEndTime.Location = new System.Drawing.Point(68, 138);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(131, 24);
            this.txtEndTime.TabIndex = 12;
            this.txtEndTime.Value = new System.DateTime(2008, 10, 20, 15, 4, 25, 421);
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(11, 144);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(50, 18);
            this.tLabel4.Text = "EndTime";
            // 
            // tLabel6
            // 
            this.tLabel6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel6.Location = new System.Drawing.Point(11, 118);
            this.tLabel6.Name = "tLabel6";
            this.tLabel6.Size = new System.Drawing.Size(51, 18);
            this.tLabel6.Text = "StartTime";
            // 
            // btnList
            // 
            this.btnList.Location = new System.Drawing.Point(8, 242);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(58, 24);
            this.btnList.TabIndex = 20;
            this.btnList.Text = "List";
            this.btnList.Click += new System.EventHandler(this.ActionListener);
            // 
            // cboSftTp
            // 
            this.cboSftTp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboSftTp.isBusinessItemName = "Shifting Type";
            this.cboSftTp.isMandatory = true;
            this.cboSftTp.Location = new System.Drawing.Point(78, 28);
            this.cboSftTp.Name = "cboSftTp";
            this.cboSftTp.Size = new System.Drawing.Size(157, 19);
            this.cboSftTp.TabIndex = 7;
            this.cboSftTp.SelectedIndexChanged += new System.EventHandler(this.cboSftTp_SelectedIndexChanged);
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(3, 31);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(69, 16);
            this.tLabel1.Text = "Shifting Type";
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(3, 56);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(69, 16);
            this.tLabel2.Text = "Next Hatch";
            // 
            // cboNextHatch
            // 
            this.cboNextHatch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboNextHatch.isBusinessItemName = "Next Hatch";
            this.cboNextHatch.isMandatory = false;
            this.cboNextHatch.Location = new System.Drawing.Point(78, 53);
            this.cboNextHatch.Name = "cboNextHatch";
            this.cboNextHatch.Size = new System.Drawing.Size(157, 19);
            this.cboNextHatch.TabIndex = 8;
            // 
            // HVO118
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.cboNextHatch);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.cboSftTp);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.txtStartTime);
            this.Controls.Add(this.txtEndTime);
            this.Controls.Add(this.tLabel4);
            this.Controls.Add(this.tLabel6);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.txtPkgType);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.txtMT);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtM3);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.cboWP);
            this.Controls.Add(this.cboStcrDiv);
            this.Controls.Add(this.cboHatch);
            this.Controls.Add(this.Label5);
            this.Name = "HVO118";
            this.Text = "V/S - Cargo Shifting";
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TLabel Label9;
        internal Framework.Controls.UserControls.TButton btnF1;
        internal Framework.Controls.UserControls.TTextBox txtPkgType;
        internal Framework.Controls.UserControls.TLabel Label11;
        internal Framework.Controls.UserControls.TLabel Label8;
        internal Framework.Controls.UserControls.TTextBox txtQty;
        internal Framework.Controls.UserControls.TTextBox txtMT;
        internal Framework.Controls.UserControls.TLabel Label3;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TTextBox txtM3;
        internal Framework.Controls.UserControls.TLabel Label7;
        internal Framework.Controls.UserControls.TTextBox txtRemark;
        internal Framework.Controls.UserControls.TLabel Label10;
        internal Framework.Controls.UserControls.TCombobox cboWP;
        internal Framework.Controls.UserControls.TCombobox cboStcrDiv;
        internal Framework.Controls.UserControls.TCombobox cboHatch;
        internal Framework.Controls.UserControls.TLabel Label5;
        private Framework.Controls.UserControls.TDateTimePicker txtStartTime;
        private Framework.Controls.UserControls.TDateTimePicker txtEndTime;
        internal Framework.Controls.UserControls.TLabel tLabel4;
        internal Framework.Controls.UserControls.TLabel tLabel6;
        internal Framework.Controls.UserControls.TButton btnList;
        internal Framework.Controls.UserControls.TCombobox cboSftTp;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        internal Framework.Controls.UserControls.TLabel tLabel2;
        internal Framework.Controls.UserControls.TCombobox cboNextHatch;
    }
}