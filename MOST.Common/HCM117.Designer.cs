using MOST.Common.UserAttribute;

namespace MOST.Common
{
    partial class HCM117
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
            this.grdGRList = new Framework.Controls.UserControls.TGrid();
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.lblSNNo = new Framework.Controls.UserControls.TLabel();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.cboSNNo = new Framework.Controls.UserControls.TCombobox();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.lblGRNo = new Framework.Controls.UserControls.TLabel();
            this.btnRetrieve = new Framework.Controls.UserControls.TButton();
            this.txtGRNo = new Framework.Controls.UserControls.TTextBox();
            this.rbtnNonJPVC = new Framework.Controls.UserControls.TRadioButton();
            this.rbtnJPVC = new Framework.Controls.UserControls.TRadioButton();
            this.txtArvlDateTo = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtArvlDateFrom = new Framework.Controls.UserControls.TDateTimePicker();
            this.lblEstArrDate = new Framework.Controls.UserControls.TLabel();
            this.lblEstArrDateFmTo = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
            // 
            // grdGRList
            // 
            this.grdGRList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdGRList.IsDirty = false;
            this.grdGRList.Location = new System.Drawing.Point(2, 97);
            this.grdGRList.Name = "grdGRList";
            this.grdGRList.RowHeadersVisible = false;
            this.grdGRList.Size = new System.Drawing.Size(234, 145);
            this.grdGRList.TabIndex = 16;
            this.grdGRList.DoubleClick += new System.EventHandler(this.grdGRList_DoubleClick);
            // 
            // txtJPVC
            // 
            this.txtJPVC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isBusinessItemName = "JPVC";
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.Location = new System.Drawing.Point(112, 27);
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(93, 19);
            this.txtJPVC.TabIndex = 4;
            this.txtJPVC.TextChanged += new System.EventHandler(this.txtJPVC_TextChanged);
            this.txtJPVC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJPVC_KeyPress);
            this.txtJPVC.LostFocus += new System.EventHandler(this.txtJPVC_LostFocus);
            // 
            // lblSNNo
            // 
            this.lblSNNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblSNNo.Location = new System.Drawing.Point(2, 52);
            this.lblSNNo.Name = "lblSNNo";
            this.lblSNNo.Size = new System.Drawing.Size(35, 16);
            this.lblSNNo.Text = "SN No";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 244);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(95, 244);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 18;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cboSNNo
            // 
            this.cboSNNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboSNNo.isBusinessItemName = "S/N No";
            this.cboSNNo.isMandatory = false;
            this.cboSNNo.Location = new System.Drawing.Point(37, 49);
            this.cboSNNo.Name = "cboSNNo";
            this.cboSNNo.Size = new System.Drawing.Size(131, 19);
            this.cboSNNo.TabIndex = 10;
            // 
            // btnF1
            // 
            this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF1.Location = new System.Drawing.Point(207, 26);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(31, 20);
            this.btnF1.TabIndex = 1;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.btnF1_Click);
            // 
            // lblGRNo
            // 
            this.lblGRNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblGRNo.Location = new System.Drawing.Point(2, 74);
            this.lblGRNo.Name = "lblGRNo";
            this.lblGRNo.Size = new System.Drawing.Size(37, 15);
            this.lblGRNo.Text = "GR No";
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnRetrieve.Location = new System.Drawing.Point(178, 52);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(60, 39);
            this.btnRetrieve.TabIndex = 14;
            this.btnRetrieve.Text = "Retrieve";
            this.btnRetrieve.Click += new System.EventHandler(this.btnRetrieve_Click);
            // 
            // txtGRNo
            // 
            this.txtGRNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtGRNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtGRNo.isBusinessItemName = "G/R No";
            this.txtGRNo.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtGRNo.Location = new System.Drawing.Point(37, 72);
            this.txtGRNo.Name = "txtGRNo";
            this.txtGRNo.Size = new System.Drawing.Size(131, 19);
            this.txtGRNo.TabIndex = 12;
            // 
            // rbtnNonJPVC
            // 
            this.rbtnNonJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnNonJPVC.isBusinessItemName = "";
            this.rbtnNonJPVC.isMandatory = false;
            this.rbtnNonJPVC.Location = new System.Drawing.Point(-1, 29);
            this.rbtnNonJPVC.Name = "rbtnNonJPVC";
            this.rbtnNonJPVC.Size = new System.Drawing.Size(68, 17);
            this.rbtnNonJPVC.TabIndex = 3;
            this.rbtnNonJPVC.Text = "Non-JPVC";
            this.rbtnNonJPVC.CheckedChanged += new System.EventHandler(this.RadiobuttonListener);
            // 
            // rbtnJPVC
            // 
            this.rbtnJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnJPVC.isBusinessItemName = "";
            this.rbtnJPVC.isMandatory = false;
            this.rbtnJPVC.Location = new System.Drawing.Point(65, 29);
            this.rbtnJPVC.Name = "rbtnJPVC";
            this.rbtnJPVC.Size = new System.Drawing.Size(45, 17);
            this.rbtnJPVC.TabIndex = 2;
            this.rbtnJPVC.TabStop = false;
            this.rbtnJPVC.Text = "JPVC";
            this.rbtnJPVC.CheckedChanged += new System.EventHandler(this.RadiobuttonListener);
            // 
            // txtArvlDateTo
            // 
            this.txtArvlDateTo.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtArvlDateTo.CustomFormat = "dd/MM/yyyy";
            this.txtArvlDateTo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtArvlDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtArvlDateTo.isBusinessItemName = "Estimate Arrival Date";
            this.txtArvlDateTo.isMandatory = false;
            this.txtArvlDateTo.Location = new System.Drawing.Point(154, 4);
            this.txtArvlDateTo.Name = "txtArvlDateTo";
            this.txtArvlDateTo.Size = new System.Drawing.Size(84, 20);
            this.txtArvlDateTo.TabIndex = 8;
            this.txtArvlDateTo.Value = new System.DateTime(2008, 10, 23, 0, 0, 0, 484);
            this.txtArvlDateTo.LostFocus += new System.EventHandler(this.EstDateLostFocus);
            // 
            // txtArvlDateFrom
            // 
            this.txtArvlDateFrom.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtArvlDateFrom.CustomFormat = "dd/MM/yyyy";
            this.txtArvlDateFrom.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtArvlDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtArvlDateFrom.isBusinessItemName = "Estimate Arrival Date";
            this.txtArvlDateFrom.isMandatory = false;
            this.txtArvlDateFrom.Location = new System.Drawing.Point(53, 4);
            this.txtArvlDateFrom.Name = "txtArvlDateFrom";
            this.txtArvlDateFrom.Size = new System.Drawing.Size(84, 20);
            this.txtArvlDateFrom.TabIndex = 7;
            this.txtArvlDateFrom.Value = new System.DateTime(2008, 10, 23, 0, 0, 0, 484);
            this.txtArvlDateFrom.LostFocus += new System.EventHandler(this.EstDateLostFocus);
            // 
            // lblEstArrDate
            // 
            this.lblEstArrDate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblEstArrDate.Location = new System.Drawing.Point(3, 1);
            this.lblEstArrDate.Name = "lblEstArrDate";
            this.lblEstArrDate.Size = new System.Drawing.Size(44, 25);
            this.lblEstArrDate.Text = "Est. Arr. Date";
            // 
            // lblEstArrDateFmTo
            // 
            this.lblEstArrDateFmTo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblEstArrDateFmTo.Location = new System.Drawing.Point(140, 6);
            this.lblEstArrDateFmTo.Name = "lblEstArrDateFmTo";
            this.lblEstArrDateFmTo.Size = new System.Drawing.Size(9, 16);
            this.lblEstArrDateFmTo.Text = "~";
            // 
            // HCM117
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(243, 277);
            this.Controls.Add(this.lblEstArrDate);
            this.Controls.Add(this.txtArvlDateTo);
            this.Controls.Add(this.lblEstArrDateFmTo);
            this.Controls.Add(this.txtArvlDateFrom);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cboSNNo);
            this.Controls.Add(this.btnRetrieve);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblSNNo);
            this.Controls.Add(this.grdGRList);
            this.Controls.Add(this.txtGRNo);
            this.Controls.Add(this.rbtnNonJPVC);
            this.Controls.Add(this.lblGRNo);
            this.Controls.Add(this.rbtnJPVC);
            this.Name = "HCM117";
            this.Text = "G/R List";
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TGrid grdGRList;
        private Framework.Controls.UserControls.TTextBox txtJPVC;
        private Framework.Controls.UserControls.TLabel lblSNNo;
        private Framework.Controls.UserControls.TTextBox txtGRNo;
        private Framework.Controls.UserControls.TCombobox cboSNNo;
        private Framework.Controls.UserControls.TLabel lblGRNo;
        private Framework.Controls.UserControls.TButton btnCancel;
        private Framework.Controls.UserControls.TButton btnOk;
        
        // tnkytn: for test
        //[AuthAccess("000001", "G/R Search")]
        public Framework.Controls.UserControls.TButton btnRetrieve;
        //[AuthAccess("000002", "JPVC Search")]
        public Framework.Controls.UserControls.TButton btnF1;
        internal Framework.Controls.UserControls.TRadioButton rbtnNonJPVC;
        internal Framework.Controls.UserControls.TRadioButton rbtnJPVC;
        private Framework.Controls.UserControls.TDateTimePicker txtArvlDateTo;
        private Framework.Controls.UserControls.TDateTimePicker txtArvlDateFrom;
        private Framework.Controls.UserControls.TLabel lblEstArrDate;
        private Framework.Controls.UserControls.TLabel lblEstArrDateFmTo;        
    }
}