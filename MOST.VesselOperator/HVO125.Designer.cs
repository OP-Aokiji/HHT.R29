namespace MOST.VesselOperator
{
    partial class HVO125
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
            this.btnUpdate = new Framework.Controls.UserControls.TButton();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.Label1 = new Framework.Controls.UserControls.TLabel();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.lblReqTit = new Framework.Controls.UserControls.TLabel();
            this.txtReqVol = new Framework.Controls.UserControls.TTextBox();
            this.lblReqUom = new Framework.Controls.UserControls.TLabel();
            this.tLabel4 = new Framework.Controls.UserControls.TLabel();
            this.txtReqDate = new Framework.Controls.UserControls.TTextBox();
            this.tLabel5 = new Framework.Controls.UserControls.TLabel();
            this.txtReqLoc = new Framework.Controls.UserControls.TTextBox();
            this.lblRecUom = new Framework.Controls.UserControls.TLabel();
            this.txtRecVol = new Framework.Controls.UserControls.TTextBox();
            this.lblRecTit = new Framework.Controls.UserControls.TLabel();
            this.tLabel8 = new Framework.Controls.UserControls.TLabel();
            this.txtRecDate = new Framework.Controls.UserControls.TDateTimePicker();
            this.tLabel9 = new Framework.Controls.UserControls.TLabel();
            this.txtRecCom = new Framework.Controls.UserControls.TTextBox();
            this.btnDelete = new Framework.Controls.UserControls.TButton();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabSearch = new System.Windows.Forms.TabPage();
            this.btnRetrieve = new Framework.Controls.UserControls.TButton();
            this.tdTo = new Framework.Controls.UserControls.TDateTimePicker();
            this.tdFrom = new Framework.Controls.UserControls.TDateTimePicker();
            this.btnF2 = new Framework.Controls.UserControls.TButton();
            this.txtJPVCName = new Framework.Controls.UserControls.TTextBox();
            this.rbtnNonJPVC = new Framework.Controls.UserControls.TRadioButton();
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.rbtnJPVC = new Framework.Controls.UserControls.TRadioButton();
            this.tabDetails = new System.Windows.Forms.TabPage();
            this.tabMain.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.tabDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = true;
            this.grdData.Location = new System.Drawing.Point(3, 125);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(232, 116);
            this.grdData.TabIndex = 25;
            this.grdData.CurrentCellChanged += new System.EventHandler(this.grdData_CurrentCellChanged);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(47, 79);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(65, 18);
            this.btnUpdate.TabIndex = 26;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnOk.Location = new System.Drawing.Point(51, 243);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 30;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(123, 243);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 31;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.Label1.Location = new System.Drawing.Point(0, 2);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(56, 15);
            this.Label1.Text = "Request";
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.tLabel1.Location = new System.Drawing.Point(0, 41);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(56, 15);
            this.tLabel1.Text = "Record";
            // 
            // lblReqTit
            // 
            this.lblReqTit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblReqTit.Location = new System.Drawing.Point(59, 2);
            this.lblReqTit.Name = "lblReqTit";
            this.lblReqTit.Size = new System.Drawing.Size(63, 15);
            this.lblReqTit.Text = "Volume";
            // 
            // txtReqVol
            // 
            this.txtReqVol.Enabled = false;
            this.txtReqVol.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtReqVol.isBusinessItemName = "JPVC";
            this.txtReqVol.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtReqVol.isMandatory = true;
            this.txtReqVol.Location = new System.Drawing.Point(109, 2);
            this.txtReqVol.Multiline = true;
            this.txtReqVol.Name = "txtReqVol";
            this.txtReqVol.ReadOnly = true;
            this.txtReqVol.Size = new System.Drawing.Size(77, 15);
            this.txtReqVol.TabIndex = 59;
            this.txtReqVol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblReqUom
            // 
            this.lblReqUom.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblReqUom.Location = new System.Drawing.Point(187, 2);
            this.lblReqUom.Name = "lblReqUom";
            this.lblReqUom.Size = new System.Drawing.Size(40, 15);
            this.lblReqUom.Text = "LT";
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(0, 21);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(32, 16);
            this.tLabel4.Text = "Dt";
            // 
            // txtReqDate
            // 
            this.txtReqDate.Enabled = false;
            this.txtReqDate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtReqDate.isBusinessItemName = "JPVC";
            this.txtReqDate.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtReqDate.isMandatory = true;
            this.txtReqDate.Location = new System.Drawing.Point(16, 20);
            this.txtReqDate.Multiline = true;
            this.txtReqDate.Name = "txtReqDate";
            this.txtReqDate.ReadOnly = true;
            this.txtReqDate.Size = new System.Drawing.Size(90, 17);
            this.txtReqDate.TabIndex = 64;
            // 
            // tLabel5
            // 
            this.tLabel5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel5.Location = new System.Drawing.Point(113, 21);
            this.tLabel5.Name = "tLabel5";
            this.tLabel5.Size = new System.Drawing.Size(26, 16);
            this.tLabel5.Text = "Loc";
            // 
            // txtReqLoc
            // 
            this.txtReqLoc.Enabled = false;
            this.txtReqLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtReqLoc.isBusinessItemName = "JPVC";
            this.txtReqLoc.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtReqLoc.isMandatory = true;
            this.txtReqLoc.Location = new System.Drawing.Point(139, 20);
            this.txtReqLoc.Multiline = true;
            this.txtReqLoc.Name = "txtReqLoc";
            this.txtReqLoc.ReadOnly = true;
            this.txtReqLoc.Size = new System.Drawing.Size(90, 17);
            this.txtReqLoc.TabIndex = 67;
            // 
            // lblRecUom
            // 
            this.lblRecUom.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblRecUom.Location = new System.Drawing.Point(187, 40);
            this.lblRecUom.Name = "lblRecUom";
            this.lblRecUom.Size = new System.Drawing.Size(56, 15);
            this.lblRecUom.Text = "LT";
            // 
            // txtRecVol
            // 
            this.txtRecVol.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtRecVol.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtRecVol.isBusinessItemName = "JPVC";
            this.txtRecVol.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtRecVol.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtRecVol.Location = new System.Drawing.Point(109, 39);
            this.txtRecVol.Multiline = true;
            this.txtRecVol.Name = "txtRecVol";
            this.txtRecVol.Size = new System.Drawing.Size(77, 17);
            this.txtRecVol.TabIndex = 70;
            this.txtRecVol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblRecTit
            // 
            this.lblRecTit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblRecTit.Location = new System.Drawing.Point(59, 40);
            this.lblRecTit.Name = "lblRecTit";
            this.lblRecTit.Size = new System.Drawing.Size(63, 15);
            this.lblRecTit.Text = "Volume";
            // 
            // tLabel8
            // 
            this.tLabel8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel8.Location = new System.Drawing.Point(0, 60);
            this.tLabel8.Name = "tLabel8";
            this.tLabel8.Size = new System.Drawing.Size(32, 16);
            this.tLabel8.Text = "Dt";
            // 
            // txtRecDate
            // 
            this.txtRecDate.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtRecDate.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtRecDate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtRecDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtRecDate.isBusinessItemName = "Start Time";
            this.txtRecDate.isMandatory = true;
            this.txtRecDate.Location = new System.Drawing.Point(16, 58);
            this.txtRecDate.Name = "txtRecDate";
            this.txtRecDate.Size = new System.Drawing.Size(120, 20);
            this.txtRecDate.TabIndex = 75;
            this.txtRecDate.Value = new System.DateTime(2008, 10, 23, 18, 35, 0, 203);
            // 
            // tLabel9
            // 
            this.tLabel9.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel9.Location = new System.Drawing.Point(136, 61);
            this.tLabel9.Name = "tLabel9";
            this.tLabel9.Size = new System.Drawing.Size(43, 15);
            this.tLabel9.Text = "Cmdty";
            // 
            // txtRecCom
            // 
            this.txtRecCom.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtRecCom.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtRecCom.isBusinessItemName = "JPVC";
            this.txtRecCom.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtRecCom.Location = new System.Drawing.Point(171, 58);
            this.txtRecCom.Multiline = true;
            this.txtRecCom.Name = "txtRecCom";
            this.txtRecCom.Size = new System.Drawing.Size(37, 18);
            this.txtRecCom.TabIndex = 78;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(119, 79);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(65, 18);
            this.btnDelete.TabIndex = 89;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnF1
            // 
            this.btnF1.Location = new System.Drawing.Point(209, 58);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(22, 18);
            this.btnF1.TabIndex = 90;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabSearch);
            this.tabMain.Controls.Add(this.tabDetails);
            this.tabMain.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(238, 124);
            this.tabMain.TabIndex = 101;
            // 
            // tabSearch
            // 
            this.tabSearch.Controls.Add(this.btnRetrieve);
            this.tabSearch.Controls.Add(this.tdTo);
            this.tabSearch.Controls.Add(this.tdFrom);
            this.tabSearch.Controls.Add(this.btnF2);
            this.tabSearch.Controls.Add(this.txtJPVCName);
            this.tabSearch.Controls.Add(this.rbtnNonJPVC);
            this.tabSearch.Controls.Add(this.txtJPVC);
            this.tabSearch.Controls.Add(this.rbtnJPVC);
            this.tabSearch.Location = new System.Drawing.Point(4, 22);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Size = new System.Drawing.Size(230, 98);
            this.tabSearch.Text = "Search";
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnRetrieve.Location = new System.Drawing.Point(88, 80);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(54, 18);
            this.btnRetrieve.TabIndex = 81;
            this.btnRetrieve.Text = "Retrieve";
            this.btnRetrieve.Click += new System.EventHandler(this.ActionListener);
            // 
            // tdTo
            // 
            this.tdTo.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.tdTo.CustomFormat = "dd/MM/yyyy";
            this.tdTo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tdTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tdTo.isBusinessItemName = "Start Time";
            this.tdTo.isMandatory = true;
            this.tdTo.Location = new System.Drawing.Point(119, 59);
            this.tdTo.Name = "tdTo";
            this.tdTo.Size = new System.Drawing.Size(108, 20);
            this.tdTo.TabIndex = 80;
            this.tdTo.Value = new System.DateTime(2008, 10, 23, 0, 0, 0, 203);
            // 
            // tdFrom
            // 
            this.tdFrom.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.tdFrom.CustomFormat = "dd/MM/yyyy";
            this.tdFrom.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tdFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tdFrom.isBusinessItemName = "Start Time";
            this.tdFrom.isMandatory = true;
            this.tdFrom.Location = new System.Drawing.Point(1, 59);
            this.tdFrom.Name = "tdFrom";
            this.tdFrom.Size = new System.Drawing.Size(109, 20);
            this.tdFrom.TabIndex = 77;
            this.tdFrom.Value = new System.DateTime(2008, 10, 23, 0, 0, 0, 203);
            // 
            // btnF2
            // 
            this.btnF2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF2.Location = new System.Drawing.Point(173, 17);
            this.btnF2.Name = "btnF2";
            this.btnF2.Size = new System.Drawing.Size(54, 18);
            this.btnF2.TabIndex = 67;
            this.btnF2.Text = "F2";
            this.btnF2.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtJPVCName
            // 
            this.txtJPVCName.Enabled = false;
            this.txtJPVCName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVCName.Location = new System.Drawing.Point(46, 38);
            this.txtJPVCName.Name = "txtJPVCName";
            this.txtJPVCName.Size = new System.Drawing.Size(181, 19);
            this.txtJPVCName.TabIndex = 68;
            // 
            // rbtnNonJPVC
            // 
            this.rbtnNonJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnNonJPVC.isBusinessItemName = "";
            this.rbtnNonJPVC.isMandatory = false;
            this.rbtnNonJPVC.Location = new System.Drawing.Point(0, 0);
            this.rbtnNonJPVC.Name = "rbtnNonJPVC";
            this.rbtnNonJPVC.Size = new System.Drawing.Size(68, 17);
            this.rbtnNonJPVC.TabIndex = 69;
            this.rbtnNonJPVC.Text = "Non-JPVC";
            this.rbtnNonJPVC.CheckedChanged += new System.EventHandler(this.RadiobuttonListener);
            // 
            // txtJPVC
            // 
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.Location = new System.Drawing.Point(47, 17);
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(120, 19);
            this.txtJPVC.TabIndex = 66;
            this.txtJPVC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJPVC_KeyPress);
            this.txtJPVC.TextChanged += new System.EventHandler(this.txtJPVC_TextChanged);
            // 
            // rbtnJPVC
            // 
            this.rbtnJPVC.Checked = true;
            this.rbtnJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnJPVC.isBusinessItemName = "";
            this.rbtnJPVC.isMandatory = false;
            this.rbtnJPVC.Location = new System.Drawing.Point(0, 18);
            this.rbtnJPVC.Name = "rbtnJPVC";
            this.rbtnJPVC.Size = new System.Drawing.Size(45, 17);
            this.rbtnJPVC.TabIndex = 70;
            this.rbtnJPVC.Text = "JPVC";
            this.rbtnJPVC.CheckedChanged += new System.EventHandler(this.RadiobuttonListener);
            // 
            // tabDetails
            // 
            this.tabDetails.Controls.Add(this.txtRecVol);
            this.tabDetails.Controls.Add(this.txtReqVol);
            this.tabDetails.Controls.Add(this.btnDelete);
            this.tabDetails.Controls.Add(this.tLabel5);
            this.tabDetails.Controls.Add(this.btnF1);
            this.tabDetails.Controls.Add(this.Label1);
            this.tabDetails.Controls.Add(this.btnUpdate);
            this.tabDetails.Controls.Add(this.tLabel1);
            this.tabDetails.Controls.Add(this.txtRecCom);
            this.tabDetails.Controls.Add(this.lblReqTit);
            this.tabDetails.Controls.Add(this.tLabel9);
            this.tabDetails.Controls.Add(this.txtRecDate);
            this.tabDetails.Controls.Add(this.lblReqUom);
            this.tabDetails.Controls.Add(this.tLabel8);
            this.tabDetails.Controls.Add(this.lblRecUom);
            this.tabDetails.Controls.Add(this.txtReqDate);
            this.tabDetails.Controls.Add(this.txtReqLoc);
            this.tabDetails.Controls.Add(this.lblRecTit);
            this.tabDetails.Controls.Add(this.tLabel4);
            this.tabDetails.Location = new System.Drawing.Point(4, 22);
            this.tabDetails.Name = "tabDetails";
            this.tabDetails.Size = new System.Drawing.Size(230, 98);
            this.tabDetails.Text = "Details";
            // 
            // HVO125
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grdData);
            this.Name = "HVO125";
            this.Text = "Road Bunkering";
            this.tabMain.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.tabDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TGrid grdData;
        internal Framework.Controls.UserControls.TButton btnUpdate;
        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TLabel Label1;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        internal Framework.Controls.UserControls.TLabel lblReqTit;
        internal Framework.Controls.UserControls.TTextBox txtReqVol;
        internal Framework.Controls.UserControls.TLabel lblReqUom;
        internal Framework.Controls.UserControls.TLabel tLabel4;
        internal Framework.Controls.UserControls.TTextBox txtReqDate;
        internal Framework.Controls.UserControls.TLabel tLabel5;
        internal Framework.Controls.UserControls.TTextBox txtReqLoc;
        internal Framework.Controls.UserControls.TLabel lblRecUom;
        internal Framework.Controls.UserControls.TTextBox txtRecVol;
        internal Framework.Controls.UserControls.TLabel lblRecTit;
        internal Framework.Controls.UserControls.TLabel tLabel8;
        private Framework.Controls.UserControls.TDateTimePicker txtRecDate;
        internal Framework.Controls.UserControls.TLabel tLabel9;
        internal Framework.Controls.UserControls.TTextBox txtRecCom;
        internal Framework.Controls.UserControls.TButton btnDelete;
        internal Framework.Controls.UserControls.TButton btnF1;
        internal System.Windows.Forms.TabControl tabMain;
        internal System.Windows.Forms.TabPage tabSearch;
        internal System.Windows.Forms.TabPage tabDetails;
        internal Framework.Controls.UserControls.TButton btnF2;
        internal Framework.Controls.UserControls.TTextBox txtJPVCName;
        internal Framework.Controls.UserControls.TRadioButton rbtnNonJPVC;
        internal Framework.Controls.UserControls.TTextBox txtJPVC;
        internal Framework.Controls.UserControls.TRadioButton rbtnJPVC;
        internal Framework.Controls.UserControls.TButton btnRetrieve;
        private Framework.Controls.UserControls.TDateTimePicker tdTo;
        private Framework.Controls.UserControls.TDateTimePicker tdFrom;
    }
}