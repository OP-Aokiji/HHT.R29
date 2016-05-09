namespace MOST.WHChecker
{
    partial class HWC102
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
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
            this.cboWH = new Framework.Controls.UserControls.TCombobox();
            this.cboCategory = new Framework.Controls.UserControls.TCombobox();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.tLabel5 = new Framework.Controls.UserControls.TLabel();
            this.txtToTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtFromTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.btnRetrieve = new Framework.Controls.UserControls.TButton();
            this.rbtnHI = new Framework.Controls.UserControls.TRadioButton();
            this.rbtnHO = new Framework.Controls.UserControls.TRadioButton();
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.btnExit = new Framework.Controls.UserControls.TButton();
            this.rbtnNonJPVC = new Framework.Controls.UserControls.TRadioButton();
            this.rbtnJPVC = new Framework.Controls.UserControls.TRadioButton();
            this.pnlJPVC = new Framework.Controls.Container.TPanel();
            this.tPanel2 = new Framework.Controls.Container.TPanel();
            this.pnlJPVC.SuspendLayout();
            this.tPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnF1
            // 
            this.btnF1.Location = new System.Drawing.Point(202, 2);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(35, 18);
            this.btnF1.TabIndex = 2;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtJPVC
            // 
            this.txtJPVC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isBusinessItemName = "JPVC";
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.Location = new System.Drawing.Point(113, 2);
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(87, 19);
            this.txtJPVC.TabIndex = 1;
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(136, 25);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(22, 16);
            this.tLabel3.Text = "WH";
            // 
            // cboWH
            // 
            this.cboWH.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboWH.isBusinessItemName = "";
            this.cboWH.isMandatory = false;
            this.cboWH.Location = new System.Drawing.Point(156, 23);
            this.cboWH.Name = "cboWH";
            this.cboWH.Size = new System.Drawing.Size(78, 19);
            this.cboWH.TabIndex = 4;
            // 
            // cboCategory
            // 
            this.cboCategory.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboCategory.isBusinessItemName = "";
            this.cboCategory.isMandatory = false;
            this.cboCategory.Location = new System.Drawing.Point(47, 23);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(87, 19);
            this.cboCategory.TabIndex = 3;
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(0, 25);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(55, 16);
            this.tLabel2.Text = "Category";
            // 
            // tLabel5
            // 
            this.tLabel5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel5.Location = new System.Drawing.Point(129, 49);
            this.tLabel5.Name = "tLabel5";
            this.tLabel5.Size = new System.Drawing.Size(9, 16);
            this.tLabel5.Text = "~";
            // 
            // txtToTime
            // 
            this.txtToTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtToTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtToTime.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.txtToTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtToTime.isBusinessItemName = "";
            this.txtToTime.isMandatory = false;
            this.txtToTime.Location = new System.Drawing.Point(140, 46);
            this.txtToTime.Name = "txtToTime";
            this.txtToTime.Size = new System.Drawing.Size(98, 19);
            this.txtToTime.TabIndex = 6;
            this.txtToTime.Value = new System.DateTime(2008, 10, 17, 9, 19, 0, 812);
            // 
            // txtFromTime
            // 
            this.txtFromTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtFromTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtFromTime.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.txtFromTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtFromTime.isBusinessItemName = "";
            this.txtFromTime.isMandatory = false;
            this.txtFromTime.Location = new System.Drawing.Point(32, 46);
            this.txtFromTime.Name = "txtFromTime";
            this.txtFromTime.Size = new System.Drawing.Size(98, 19);
            this.txtFromTime.TabIndex = 5;
            this.txtFromTime.Value = new System.DateTime(2008, 10, 17, 9, 19, 0, 109);
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Location = new System.Drawing.Point(167, 70);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(65, 24);
            this.btnRetrieve.TabIndex = 10;
            this.btnRetrieve.Text = "Retrieve";
            this.btnRetrieve.Click += new System.EventHandler(this.ActionListener);
            // 
            // rbtnHI
            // 
            this.rbtnHI.Checked = true;
            this.rbtnHI.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.rbtnHI.isBusinessItemName = "";
            this.rbtnHI.isMandatory = false;
            this.rbtnHI.Location = new System.Drawing.Point(0, 3);
            this.rbtnHI.Name = "rbtnHI";
            this.rbtnHI.Size = new System.Drawing.Size(80, 17);
            this.rbtnHI.TabIndex = 7;
            this.rbtnHI.Text = "Handle In";
            this.rbtnHI.CheckedChanged += new System.EventHandler(this.RadiobuttonListener);
            // 
            // rbtnHO
            // 
            this.rbtnHO.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.rbtnHO.isBusinessItemName = "";
            this.rbtnHO.isMandatory = false;
            this.rbtnHO.Location = new System.Drawing.Point(78, 3);
            this.rbtnHO.Name = "rbtnHO";
            this.rbtnHO.Size = new System.Drawing.Size(84, 17);
            this.rbtnHO.TabIndex = 163;
            this.rbtnHO.Text = "Handle Out";
            this.rbtnHO.CheckedChanged += new System.EventHandler(this.RadiobuttonListener);
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(3, 97);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(232, 143);
            this.grdData.TabIndex = 11;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(0, 49);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(52, 18);
            this.tLabel1.Text = "HdlDt";
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnExit.Location = new System.Drawing.Point(170, 243);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(65, 24);
            this.btnExit.TabIndex = 169;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.ActionListener);
            // 
            // rbtnNonJPVC
            // 
            this.rbtnNonJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnNonJPVC.isBusinessItemName = "";
            this.rbtnNonJPVC.isMandatory = false;
            this.rbtnNonJPVC.Location = new System.Drawing.Point(0, 3);
            this.rbtnNonJPVC.Name = "rbtnNonJPVC";
            this.rbtnNonJPVC.Size = new System.Drawing.Size(68, 17);
            this.rbtnNonJPVC.TabIndex = 175;
            this.rbtnNonJPVC.Text = "Non-JPVC";
            this.rbtnNonJPVC.CheckedChanged += new System.EventHandler(this.RadiobuttonListener);
            // 
            // rbtnJPVC
            // 
            this.rbtnJPVC.Checked = true;
            this.rbtnJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnJPVC.isBusinessItemName = "";
            this.rbtnJPVC.isMandatory = false;
            this.rbtnJPVC.Location = new System.Drawing.Point(68, 3);
            this.rbtnJPVC.Name = "rbtnJPVC";
            this.rbtnJPVC.Size = new System.Drawing.Size(45, 17);
            this.rbtnJPVC.TabIndex = 176;
            this.rbtnJPVC.Text = "JPVC";
            this.rbtnJPVC.CheckedChanged += new System.EventHandler(this.RadiobuttonListener);
            // 
            // pnlJPVC
            // 
            this.pnlJPVC.Controls.Add(this.rbtnJPVC);
            this.pnlJPVC.Controls.Add(this.rbtnNonJPVC);
            this.pnlJPVC.Location = new System.Drawing.Point(0, 1);
            this.pnlJPVC.Name = "pnlJPVC";
            this.pnlJPVC.Size = new System.Drawing.Size(119, 21);
            // 
            // tPanel2
            // 
            this.tPanel2.Controls.Add(this.rbtnHO);
            this.tPanel2.Controls.Add(this.rbtnHI);
            this.tPanel2.Location = new System.Drawing.Point(0, 70);
            this.tPanel2.Name = "tPanel2";
            this.tPanel2.Size = new System.Drawing.Size(166, 23);
            // 
            // HWC102
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.btnRetrieve);
            this.Controls.Add(this.tPanel2);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.pnlJPVC);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.txtToTime);
            this.Controls.Add(this.txtFromTime);
            this.Controls.Add(this.cboWH);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.tLabel3);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.tLabel5);
            this.Name = "HWC102";
            this.Text = "W/C - Handling In/Out List";
            this.pnlJPVC.ResumeLayout(false);
            this.tPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public Framework.Controls.UserControls.TButton btnF1;
        private Framework.Controls.UserControls.TTextBox txtJPVC;
        internal Framework.Controls.UserControls.TLabel tLabel3;
        internal Framework.Controls.UserControls.TCombobox cboWH;
        internal Framework.Controls.UserControls.TCombobox cboCategory;
        internal Framework.Controls.UserControls.TLabel tLabel2;
        internal Framework.Controls.UserControls.TLabel tLabel5;
        private Framework.Controls.UserControls.TDateTimePicker txtToTime;
        private Framework.Controls.UserControls.TDateTimePicker txtFromTime;
        public Framework.Controls.UserControls.TButton btnRetrieve;
        internal Framework.Controls.UserControls.TRadioButton rbtnHI;
        internal Framework.Controls.UserControls.TRadioButton rbtnHO;
        private Framework.Controls.UserControls.TGrid grdData;
        private Framework.Controls.UserControls.TLabel tLabel1;
        internal Framework.Controls.UserControls.TButton btnExit;
        internal Framework.Controls.UserControls.TRadioButton rbtnNonJPVC;
        internal Framework.Controls.UserControls.TRadioButton rbtnJPVC;
        private Framework.Controls.Container.TPanel pnlJPVC;
        private Framework.Controls.Container.TPanel tPanel2;
    }
}