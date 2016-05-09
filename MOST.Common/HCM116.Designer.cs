namespace MOST.Common
{
    partial class HCM116
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnPrint = new Framework.Controls.UserControls.TButton();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.btnF2 = new Framework.Controls.UserControls.TButton();
            this.txtGRBL = new Framework.Controls.UserControls.TTextBox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.txtGP = new Framework.Controls.UserControls.TTextBox();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.btnSearch = new Framework.Controls.UserControls.TButton();
            this.lblPrinter = new Framework.Controls.UserControls.TLabel();
            this.cboBaudRate = new Framework.Controls.UserControls.TCombobox();
            this.cboComPort = new Framework.Controls.UserControls.TCombobox();
            this.lblSNNo = new Framework.Controls.UserControls.TLabel();
            this.lblBaudRate = new Framework.Controls.UserControls.TLabel();
            this.rbtnNonJPVC = new Framework.Controls.UserControls.TRadioButton();
            this.rbtnJPVC = new Framework.Controls.UserControls.TRadioButton();
            this.txtRemark = new Framework.Controls.UserControls.TTextBox();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
            this.tGPStart = new Framework.Controls.UserControls.TDateTimePicker();
            this.tGPEnd = new Framework.Controls.UserControls.TDateTimePicker();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(2, 76);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(234, 117);
            this.grdData.TabIndex = 7;
            this.grdData.CurrentCellChanged += new System.EventHandler(this.grdData_CurrentCellChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(169, 248);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 20);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(98, 248);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(65, 20);
            this.btnPrint.TabIndex = 10;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnF1
            // 
            this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF1.Location = new System.Drawing.Point(198, 1);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(38, 17);
            this.btnF1.TabIndex = 2;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtJPVC
            // 
            this.txtJPVC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.Location = new System.Drawing.Point(114, 1);
            this.txtJPVC.Multiline = true;
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(82, 17);
            this.txtJPVC.TabIndex = 1;
            // 
            // btnF2
            // 
            this.btnF2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF2.Location = new System.Drawing.Point(140, 19);
            this.btnF2.Name = "btnF2";
            this.btnF2.Size = new System.Drawing.Size(25, 17);
            this.btnF2.TabIndex = 4;
            this.btnF2.Text = "F2";
            this.btnF2.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtGRBL
            // 
            this.txtGRBL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtGRBL.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtGRBL.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtGRBL.Location = new System.Drawing.Point(41, 19);
            this.txtGRBL.Multiline = true;
            this.txtGRBL.Name = "txtGRBL";
            this.txtGRBL.Size = new System.Drawing.Size(95, 17);
            this.txtGRBL.TabIndex = 3;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(2, 21);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(41, 15);
            this.tLabel1.Text = "GR/BL";
            // 
            // txtGP
            // 
            this.txtGP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtGP.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtGP.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtGP.Location = new System.Drawing.Point(41, 37);
            this.txtGP.Multiline = true;
            this.txtGP.Name = "txtGP";
            this.txtGP.Size = new System.Drawing.Size(124, 17);
            this.txtGP.TabIndex = 5;
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(2, 39);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(41, 15);
            this.tLabel2.Text = "GP";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(171, 30);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(65, 24);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Retrieve";
            this.btnSearch.Click += new System.EventHandler(this.ActionListener);
            // 
            // lblPrinter
            // 
            this.lblPrinter.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblPrinter.Location = new System.Drawing.Point(5, 221);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new System.Drawing.Size(58, 31);
            this.lblPrinter.Text = "Printer Settings";
            // 
            // cboBaudRate
            // 
            this.cboBaudRate.isBusinessItemName = "Baud rate";
            this.cboBaudRate.isMandatory = true;
            this.cboBaudRate.Location = new System.Drawing.Point(143, 224);
            this.cboBaudRate.Name = "cboBaudRate";
            this.cboBaudRate.Size = new System.Drawing.Size(92, 23);
            this.cboBaudRate.TabIndex = 9;
            // 
            // cboComPort
            // 
            this.cboComPort.isBusinessItemName = "COM Port";
            this.cboComPort.isMandatory = true;
            this.cboComPort.Location = new System.Drawing.Point(67, 224);
            this.cboComPort.Name = "cboComPort";
            this.cboComPort.Size = new System.Drawing.Size(70, 23);
            this.cboComPort.TabIndex = 8;
            // 
            // lblSNNo
            // 
            this.lblSNNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblSNNo.Location = new System.Drawing.Point(67, 211);
            this.lblSNNo.Name = "lblSNNo";
            this.lblSNNo.Size = new System.Drawing.Size(63, 19);
            this.lblSNNo.Text = "COM Port";
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblBaudRate.Location = new System.Drawing.Point(143, 211);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(64, 19);
            this.lblBaudRate.Text = "Baud rate";
            // 
            // rbtnNonJPVC
            // 
            this.rbtnNonJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnNonJPVC.isBusinessItemName = "";
            this.rbtnNonJPVC.isMandatory = false;
            this.rbtnNonJPVC.Location = new System.Drawing.Point(0, 1);
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
            this.rbtnJPVC.Location = new System.Drawing.Point(69, 1);
            this.rbtnJPVC.Name = "rbtnJPVC";
            this.rbtnJPVC.Size = new System.Drawing.Size(45, 17);
            this.rbtnJPVC.TabIndex = 65;
            this.rbtnJPVC.Text = "JPVC";
            this.rbtnJPVC.CheckedChanged += new System.EventHandler(this.RadiobuttonListener);
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtRemark.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtRemark.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtRemark.Location = new System.Drawing.Point(40, 194);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(196, 17);
            this.txtRemark.TabIndex = 72;
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(-1, 194);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(41, 15);
            this.tLabel3.Text = "Remark";
            // 
            // tGPStart
            // 
            this.tGPStart.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.tGPStart.CustomFormat = "dd/MM/yyyy HH:mm";
            this.tGPStart.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tGPStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tGPStart.isBusinessItemName = "Start Date";
            this.tGPStart.isMandatory = true;
            this.tGPStart.Location = new System.Drawing.Point(3, 55);
            this.tGPStart.Name = "tGPStart";
            this.tGPStart.Size = new System.Drawing.Size(111, 20);
            this.tGPStart.TabIndex = 110;
            this.tGPStart.Value = new System.DateTime(2008, 10, 24, 16, 17, 0, 437);
            // 
            // tGPEnd
            // 
            this.tGPEnd.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.tGPEnd.CustomFormat = "dd/MM/yyyy HH:mm";
            this.tGPEnd.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tGPEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tGPEnd.isBusinessItemName = "Start Date";
            this.tGPEnd.isMandatory = true;
            this.tGPEnd.Location = new System.Drawing.Point(125, 55);
            this.tGPEnd.Name = "tGPEnd";
            this.tGPEnd.Size = new System.Drawing.Size(111, 20);
            this.tGPEnd.TabIndex = 111;
            this.tGPEnd.Value = new System.DateTime(2008, 10, 24, 16, 17, 0, 437);
            // 
            // HCM116
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.tGPEnd);
            this.Controls.Add(this.tGPStart);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.tLabel3);
            this.Controls.Add(this.rbtnNonJPVC);
            this.Controls.Add(this.rbtnJPVC);
            this.Controls.Add(this.cboBaudRate);
            this.Controls.Add(this.cboComPort);
            this.Controls.Add(this.lblSNNo);
            this.Controls.Add(this.lblBaudRate);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtGP);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.btnF2);
            this.Controls.Add(this.txtGRBL);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblPrinter);
            this.Name = "HCM116";
            this.Text = "Gate Pass Printing";
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TGrid grdData;
        private Framework.Controls.UserControls.TButton btnCancel;
        private Framework.Controls.UserControls.TButton btnPrint;
        internal Framework.Controls.UserControls.TButton btnF1;
        public Framework.Controls.UserControls.TTextBox txtJPVC;
        internal Framework.Controls.UserControls.TButton btnF2;
        public Framework.Controls.UserControls.TTextBox txtGRBL;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        public Framework.Controls.UserControls.TTextBox txtGP;
        internal Framework.Controls.UserControls.TLabel tLabel2;
        private Framework.Controls.UserControls.TButton btnSearch;
        private Framework.Controls.UserControls.TLabel lblPrinter;
        private Framework.Controls.UserControls.TCombobox cboBaudRate;
        private Framework.Controls.UserControls.TCombobox cboComPort;
        private Framework.Controls.UserControls.TLabel lblSNNo;
        private Framework.Controls.UserControls.TLabel lblBaudRate;
        public Framework.Controls.UserControls.TRadioButton rbtnNonJPVC;
        internal Framework.Controls.UserControls.TRadioButton rbtnJPVC;
        internal Framework.Controls.UserControls.TTextBox txtRemark;
        internal Framework.Controls.UserControls.TLabel tLabel3;
        private Framework.Controls.UserControls.TDateTimePicker tGPStart;
        private Framework.Controls.UserControls.TDateTimePicker tGPEnd;

    }
}