namespace MOST.WHChecker
{
    partial class HWC105
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
            this.btnReconcile = new Framework.Controls.UserControls.TButton();
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.btnRetrieve = new Framework.Controls.UserControls.TButton();
            this.cboWH = new Framework.Controls.UserControls.TCombobox();
            this.lblWH = new Framework.Controls.UserControls.TLabel();
            this.cboCategory = new Framework.Controls.UserControls.TCombobox();
            this.lblCategory = new Framework.Controls.UserControls.TLabel();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.txtGR = new Framework.Controls.UserControls.TTextBox();
            this.btnF2 = new Framework.Controls.UserControls.TButton();
            this.lblGR = new Framework.Controls.UserControls.TLabel();
            this.txtToTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtFromTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.lblDate = new Framework.Controls.UserControls.TLabel();
            this.lblDate2 = new Framework.Controls.UserControls.TLabel();
            this.lblSN = new Framework.Controls.UserControls.TLabel();
            this.lblBL = new Framework.Controls.UserControls.TLabel();
            this.cboBL = new Framework.Controls.UserControls.TCombobox();
            this.cboSN = new Framework.Controls.UserControls.TCombobox();
            this.lblJPVC = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(95, 243);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 20;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 243);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnReconcile
            // 
            this.btnReconcile.Location = new System.Drawing.Point(3, 99);
            this.btnReconcile.Name = "btnReconcile";
            this.btnReconcile.Size = new System.Drawing.Size(115, 20);
            this.btnReconcile.TabIndex = 17;
            this.btnReconcile.Text = "Reconcile";
            this.btnReconcile.Click += new System.EventHandler(this.ActionListener);
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(3, 121);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(232, 120);
            this.grdData.TabIndex = 15;
            this.grdData.DoubleClick += new System.EventHandler(this.grdData_DoubleClick);
            this.grdData.CurrentCellChanged += new System.EventHandler(this.grdData_CurrentCellChanged);
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Location = new System.Drawing.Point(169, 70);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(63, 30);
            this.btnRetrieve.TabIndex = 14;
            this.btnRetrieve.Text = "Retrieve";
            this.btnRetrieve.Click += new System.EventHandler(this.ActionListener);
            // 
            // cboWH
            // 
            this.cboWH.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboWH.isBusinessItemName = "WH";
            this.cboWH.isMandatory = false;
            this.cboWH.Location = new System.Drawing.Point(21, 1);
            this.cboWH.Name = "cboWH";
            this.cboWH.Size = new System.Drawing.Size(109, 19);
            this.cboWH.TabIndex = 1;
            // 
            // lblWH
            // 
            this.lblWH.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblWH.Location = new System.Drawing.Point(1, 5);
            this.lblWH.Name = "lblWH";
            this.lblWH.Size = new System.Drawing.Size(27, 16);
            this.lblWH.Text = "WH";
            // 
            // cboCategory
            // 
            this.cboCategory.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboCategory.isBusinessItemName = "Category";
            this.cboCategory.isMandatory = false;
            this.cboCategory.Location = new System.Drawing.Point(159, 1);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(77, 19);
            this.cboCategory.TabIndex = 2;
            this.cboCategory.SelectedIndexChanged += new System.EventHandler(this.cboCategory_SelectedIndexChanged);
            // 
            // lblCategory
            // 
            this.lblCategory.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblCategory.Location = new System.Drawing.Point(131, 4);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(29, 16);
            this.lblCategory.Text = "Catg.";
            // 
            // btnF1
            // 
            this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF1.Location = new System.Drawing.Point(124, 24);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(36, 19);
            this.btnF1.TabIndex = 4;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtJPVC
            // 
            this.txtJPVC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isBusinessItemName = "JPVC";
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.Location = new System.Drawing.Point(30, 24);
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(92, 19);
            this.txtJPVC.TabIndex = 3;
            this.txtJPVC.LostFocus += new System.EventHandler(this.txtJPVC_LostFocus);
            this.txtJPVC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJPVC_KeyPress);
            this.txtJPVC.TextChanged += new System.EventHandler(this.txtJPVC_TextChanged);
            // 
            // txtGR
            // 
            this.txtGR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtGR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtGR.isBusinessItemName = "GR";
            this.txtGR.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtGR.Location = new System.Drawing.Point(16, 70);
            this.txtGR.Multiline = true;
            this.txtGR.Name = "txtGR";
            this.txtGR.Size = new System.Drawing.Size(68, 17);
            this.txtGR.TabIndex = 9;
            this.txtGR.Text = "R09050006";
            // 
            // btnF2
            // 
            this.btnF2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF2.Location = new System.Drawing.Point(87, 70);
            this.btnF2.Name = "btnF2";
            this.btnF2.Size = new System.Drawing.Size(33, 17);
            this.btnF2.TabIndex = 10;
            this.btnF2.Text = "F2";
            this.btnF2.Click += new System.EventHandler(this.ActionListener);
            // 
            // lblGR
            // 
            this.lblGR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblGR.Location = new System.Drawing.Point(0, 72);
            this.lblGR.Name = "lblGR";
            this.lblGR.Size = new System.Drawing.Size(20, 15);
            this.lblGR.Text = "GR";
            // 
            // txtToTime
            // 
            this.txtToTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtToTime.CustomFormat = "dd/MM/yyyy";
            this.txtToTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtToTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtToTime.isBusinessItemName = "";
            this.txtToTime.isMandatory = false;
            this.txtToTime.Location = new System.Drawing.Point(129, 24);
            this.txtToTime.Name = "txtToTime";
            this.txtToTime.Size = new System.Drawing.Size(94, 20);
            this.txtToTime.TabIndex = 6;
            this.txtToTime.Value = new System.DateTime(2008, 10, 17, 9, 19, 55, 812);
            this.txtToTime.Visible = false;
            // 
            // txtFromTime
            // 
            this.txtFromTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtFromTime.CustomFormat = "dd/MM/yyyy";
            this.txtFromTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtFromTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtFromTime.isBusinessItemName = "";
            this.txtFromTime.isMandatory = false;
            this.txtFromTime.Location = new System.Drawing.Point(27, 24);
            this.txtFromTime.Name = "txtFromTime";
            this.txtFromTime.Size = new System.Drawing.Size(93, 20);
            this.txtFromTime.TabIndex = 5;
            this.txtFromTime.Value = new System.DateTime(2008, 10, 17, 9, 19, 56, 109);
            this.txtFromTime.Visible = false;
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblDate.Location = new System.Drawing.Point(0, 28);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(28, 18);
            this.lblDate.Text = "Date";
            this.lblDate.Visible = false;
            // 
            // lblDate2
            // 
            this.lblDate2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblDate2.Location = new System.Drawing.Point(119, 28);
            this.lblDate2.Name = "lblDate2";
            this.lblDate2.Size = new System.Drawing.Size(9, 16);
            this.lblDate2.Text = "~";
            this.lblDate2.Visible = false;
            // 
            // lblSN
            // 
            this.lblSN.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblSN.Location = new System.Drawing.Point(120, 50);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(16, 15);
            this.lblSN.Text = "BL";
            // 
            // lblBL
            // 
            this.lblBL.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblBL.Location = new System.Drawing.Point(0, 49);
            this.lblBL.Name = "lblBL";
            this.lblBL.Size = new System.Drawing.Size(18, 16);
            this.lblBL.Text = "SN";
            // 
            // cboBL
            // 
            this.cboBL.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboBL.isBusinessItemName = "WH";
            this.cboBL.isMandatory = false;
            this.cboBL.Location = new System.Drawing.Point(133, 46);
            this.cboBL.Name = "cboBL";
            this.cboBL.Size = new System.Drawing.Size(104, 19);
            this.cboBL.TabIndex = 8;
            // 
            // cboSN
            // 
            this.cboSN.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboSN.isBusinessItemName = "WH";
            this.cboSN.isMandatory = false;
            this.cboSN.Location = new System.Drawing.Point(16, 46);
            this.cboSN.Name = "cboSN";
            this.cboSN.Size = new System.Drawing.Size(102, 19);
            this.cboSN.TabIndex = 7;
            // 
            // lblJPVC
            // 
            this.lblJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblJPVC.Location = new System.Drawing.Point(2, 25);
            this.lblJPVC.Name = "lblJPVC";
            this.lblJPVC.Size = new System.Drawing.Size(28, 16);
            this.lblJPVC.Text = "JPVC";
            // 
            // HWC105
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.lblJPVC);
            this.Controls.Add(this.cboSN);
            this.Controls.Add(this.cboBL);
            this.Controls.Add(this.txtGR);
            this.Controls.Add(this.btnF2);
            this.Controls.Add(this.lblGR);
            this.Controls.Add(this.lblBL);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.lblSN);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cboWH);
            this.Controls.Add(this.btnRetrieve);
            this.Controls.Add(this.btnReconcile);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblWH);
            this.Controls.Add(this.txtToTime);
            this.Controls.Add(this.lblDate2);
            this.Controls.Add(this.txtFromTime);
            this.Controls.Add(this.lblDate);
            this.Name = "HWC105";
            this.Text = "W/C - WH Reconciliation List";
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TButton btnReconcile;
        private Framework.Controls.UserControls.TGrid grdData;
        internal Framework.Controls.UserControls.TButton btnRetrieve;
        internal Framework.Controls.UserControls.TCombobox cboWH;
        internal Framework.Controls.UserControls.TLabel lblWH;
        internal Framework.Controls.UserControls.TCombobox cboCategory;
        internal Framework.Controls.UserControls.TLabel lblCategory;
        internal Framework.Controls.UserControls.TButton btnF1;
        internal Framework.Controls.UserControls.TTextBox txtJPVC;
        internal Framework.Controls.UserControls.TTextBox txtGR;
        internal Framework.Controls.UserControls.TButton btnF2;
        internal Framework.Controls.UserControls.TLabel lblGR;
        private Framework.Controls.UserControls.TDateTimePicker txtToTime;
        private Framework.Controls.UserControls.TDateTimePicker txtFromTime;
        private Framework.Controls.UserControls.TLabel lblDate;
        internal Framework.Controls.UserControls.TLabel lblDate2;
        internal Framework.Controls.UserControls.TLabel lblSN;
        internal Framework.Controls.UserControls.TLabel lblBL;
        internal Framework.Controls.UserControls.TCombobox cboBL;
        internal Framework.Controls.UserControls.TCombobox cboSN;
        internal Framework.Controls.UserControls.TLabel lblJPVC;
    }
}