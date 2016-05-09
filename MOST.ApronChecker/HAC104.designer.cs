namespace MOST.ApronChecker
{
    partial class HAC104
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
            this.label1 = new Framework.Controls.UserControls.TLabel();
            this.cboSFType = new Framework.Controls.UserControls.TCombobox();
            this.label2 = new Framework.Controls.UserControls.TLabel();
            this.label3 = new Framework.Controls.UserControls.TLabel();
            this.cboNHatch = new Framework.Controls.UserControls.TCombobox();
            this.txtShftQty = new Framework.Controls.UserControls.TTextBox();
            this.txtShftM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtShftMT = new Framework.Controls.UserControls.TTextBox();
            this.label8 = new Framework.Controls.UserControls.TLabel();
            this.label9 = new Framework.Controls.UserControls.TLabel();
            this.label10 = new Framework.Controls.UserControls.TLabel();
            this.label11 = new Framework.Controls.UserControls.TLabel();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.cboHatch = new Framework.Controls.UserControls.TCombobox();
            this.cboAPFP = new Framework.Controls.UserControls.TCombobox();
            this.cboManpower = new Framework.Controls.UserControls.TCombobox();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.txtPackage = new Framework.Controls.UserControls.TTextBox();
            this.txtEndTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtStartTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.lblEndTime = new Framework.Controls.UserControls.TLabel();
            this.lblStartTime = new Framework.Controls.UserControls.TLabel();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.txtRemark = new Framework.Controls.UserControls.TTextBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnOk.Location = new System.Drawing.Point(95, 240);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.actionListener);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 20);
            this.label1.Text = "Hatch";
            // 
            // cboSFType
            // 
            this.cboSFType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboSFType.isBusinessItemName = "";
            this.cboSFType.isMandatory = false;
            this.cboSFType.Location = new System.Drawing.Point(62, 34);
            this.cboSFType.Name = "cboSFType";
            this.cboSFType.Size = new System.Drawing.Size(161, 19);
            this.cboSFType.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(3, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.Text = "SF/Type";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(3, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.Text = "Next Hatch";
            // 
            // cboNHatch
            // 
            this.cboNHatch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboNHatch.isBusinessItemName = "";
            this.cboNHatch.isMandatory = false;
            this.cboNHatch.Location = new System.Drawing.Point(62, 59);
            this.cboNHatch.Name = "cboNHatch";
            this.cboNHatch.Size = new System.Drawing.Size(94, 19);
            this.cboNHatch.TabIndex = 10;
            // 
            // txtShftQty
            // 
            this.txtShftQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtShftQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtShftQty.Location = new System.Drawing.Point(172, 187);
            this.txtShftQty.Name = "txtShftQty";
            this.txtShftQty.Size = new System.Drawing.Size(51, 19);
            this.txtShftQty.TabIndex = 22;
            // 
            // txtShftM3
            // 
            this.txtShftM3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtShftM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtShftM3.Location = new System.Drawing.Point(117, 187);
            this.txtShftM3.Name = "txtShftM3";
            this.txtShftM3.Size = new System.Drawing.Size(51, 19);
            this.txtShftM3.TabIndex = 21;
            // 
            // txtShftMT
            // 
            this.txtShftMT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtShftMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtShftMT.Location = new System.Drawing.Point(62, 187);
            this.txtShftMT.Name = "txtShftMT";
            this.txtShftMT.Size = new System.Drawing.Size(51, 19);
            this.txtShftMT.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label8.Location = new System.Drawing.Point(5, 186);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 20);
            this.label8.Text = "Shift";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label9.Location = new System.Drawing.Point(62, 168);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.Text = "MT";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label10.Location = new System.Drawing.Point(117, 168);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.Text = "M3";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label11.Location = new System.Drawing.Point(172, 168);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.Text = "Qty";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(167, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.actionListener);
            // 
            // cboHatch
            // 
            this.cboHatch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboHatch.isBusinessItemName = "";
            this.cboHatch.isMandatory = false;
            this.cboHatch.Location = new System.Drawing.Point(38, 9);
            this.cboHatch.Name = "cboHatch";
            this.cboHatch.Size = new System.Drawing.Size(53, 19);
            this.cboHatch.TabIndex = 117;
            // 
            // cboAPFP
            // 
            this.cboAPFP.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboAPFP.isBusinessItemName = "";
            this.cboAPFP.isMandatory = false;
            this.cboAPFP.Location = new System.Drawing.Point(94, 9);
            this.cboAPFP.Name = "cboAPFP";
            this.cboAPFP.Size = new System.Drawing.Size(52, 19);
            this.cboAPFP.TabIndex = 118;
            // 
            // cboManpower
            // 
            this.cboManpower.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboManpower.isBusinessItemName = "";
            this.cboManpower.isMandatory = false;
            this.cboManpower.Location = new System.Drawing.Point(149, 9);
            this.cboManpower.Name = "cboManpower";
            this.cboManpower.Size = new System.Drawing.Size(86, 19);
            this.cboManpower.TabIndex = 119;
            // 
            // btnF1
            // 
            this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF1.Location = new System.Drawing.Point(167, 84);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(39, 19);
            this.btnF1.TabIndex = 122;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.actionListener);
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(3, 88);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(45, 15);
            this.tLabel1.Text = "Package";
            // 
            // txtPackage
            // 
            this.txtPackage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPackage.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtPackage.Location = new System.Drawing.Point(62, 84);
            this.txtPackage.Name = "txtPackage";
            this.txtPackage.Size = new System.Drawing.Size(94, 19);
            this.txtPackage.TabIndex = 121;
            // 
            // txtEndTime
            // 
            this.txtEndTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtEndTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtEndTime.isBusinessItemName = "";
            this.txtEndTime.isMandatory = false;
            this.txtEndTime.Location = new System.Drawing.Point(62, 135);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(128, 24);
            this.txtEndTime.TabIndex = 145;
            this.txtEndTime.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 518);
            // 
            // txtStartTime
            // 
            this.txtStartTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtStartTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtStartTime.isBusinessItemName = "";
            this.txtStartTime.isMandatory = false;
            this.txtStartTime.Location = new System.Drawing.Point(62, 109);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(128, 24);
            this.txtStartTime.TabIndex = 144;
            this.txtStartTime.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 96);
            // 
            // lblEndTime
            // 
            this.lblEndTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblEndTime.Location = new System.Drawing.Point(1, 140);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(52, 19);
            this.lblEndTime.Text = "End Time";
            // 
            // lblStartTime
            // 
            this.lblStartTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblStartTime.Location = new System.Drawing.Point(1, 114);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(55, 19);
            this.lblStartTime.Text = "Start Time";
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(3, 216);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(45, 15);
            this.tLabel2.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtRemark.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtRemark.Location = new System.Drawing.Point(62, 212);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(161, 19);
            this.txtRemark.TabIndex = 149;
            // 
            // HAC104
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.txtEndTime);
            this.Controls.Add(this.txtStartTime);
            this.Controls.Add(this.lblEndTime);
            this.Controls.Add(this.lblStartTime);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.txtPackage);
            this.Controls.Add(this.cboManpower);
            this.Controls.Add(this.cboAPFP);
            this.Controls.Add(this.cboHatch);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtShftQty);
            this.Controls.Add(this.txtShftM3);
            this.Controls.Add(this.txtShftMT);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboNHatch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboSFType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "HAC104";
            this.Text = "A/C - Cargo Shifting";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HAC104_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TButton btnOk;
        private Framework.Controls.UserControls.TLabel label1;
        private Framework.Controls.UserControls.TCombobox cboSFType;
        private Framework.Controls.UserControls.TLabel label2;
        private Framework.Controls.UserControls.TLabel label3;
        private Framework.Controls.UserControls.TCombobox cboNHatch;
        private Framework.Controls.UserControls.TTextBox txtShftQty;
        private Framework.Controls.UserControls.TTextBox txtShftM3;
        private Framework.Controls.UserControls.TTextBox txtShftMT;
        private Framework.Controls.UserControls.TLabel label8;
        private Framework.Controls.UserControls.TLabel label9;
        private Framework.Controls.UserControls.TLabel label10;
        private Framework.Controls.UserControls.TLabel label11;
        private Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TCombobox cboHatch;
        internal Framework.Controls.UserControls.TCombobox cboAPFP;
        internal Framework.Controls.UserControls.TCombobox cboManpower;
        private Framework.Controls.UserControls.TButton btnF1;
        private Framework.Controls.UserControls.TLabel tLabel1;
        private Framework.Controls.UserControls.TTextBox txtPackage;
        private Framework.Controls.UserControls.TDateTimePicker txtEndTime;
        private Framework.Controls.UserControls.TDateTimePicker txtStartTime;
        internal Framework.Controls.UserControls.TLabel lblEndTime;
        internal Framework.Controls.UserControls.TLabel lblStartTime;
        private Framework.Controls.UserControls.TLabel tLabel2;
        private Framework.Controls.UserControls.TTextBox txtRemark;
    }
}