using MOST.Common.UserAttribute;

namespace MOST.Common
{
    partial class HCM103
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
            this.lblSNNo = new Framework.Controls.UserControls.TLabel();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.cboSNNo = new Framework.Controls.UserControls.TCombobox();
            this.cboPaging = new Framework.Controls.UserControls.TCombobox();
            this.btnPrev = new Framework.Controls.UserControls.TButton();
            this.btnNext = new Framework.Controls.UserControls.TButton();
            this.lblGRNo = new Framework.Controls.UserControls.TLabel();
            this.btnRetrieve = new Framework.Controls.UserControls.TButton();
            this.txtGRNo = new Framework.Controls.UserControls.TTextBox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.txtDocMt = new Framework.Controls.UserControls.TTextBox();
            this.txtDocM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtDocQty = new Framework.Controls.UserControls.TTextBox();
            this.txtActQty = new Framework.Controls.UserControls.TTextBox();
            this.txtActM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtActMt = new Framework.Controls.UserControls.TTextBox();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.txtArvlDateFrom = new Framework.Controls.UserControls.TDateTimePicker();
            this.lblEstArrDateFmTo = new Framework.Controls.UserControls.TLabel();
            this.txtArvlDateTo = new Framework.Controls.UserControls.TDateTimePicker();
            this.lblEstArrDate = new Framework.Controls.UserControls.TLabel();
            this.tmrRefresh = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // grdGRList
            // 
            this.grdGRList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdGRList.IsDirty = false;
            this.grdGRList.Location = new System.Drawing.Point(2, 116);
            this.grdGRList.Name = "grdGRList";
            this.grdGRList.RowHeadersVisible = false;
            this.grdGRList.Size = new System.Drawing.Size(234, 126);
            this.grdGRList.TabIndex = 16;
            this.grdGRList.DoubleClick += new System.EventHandler(this.grdGRList_DoubleClick);
            this.grdGRList.CurrentCellChanged += new System.EventHandler(this.grdGRList_CurrentCellChanged);
            // 
            // lblSNNo
            // 
            this.lblSNNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblSNNo.Location = new System.Drawing.Point(-1, 30);
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
            this.cboSNNo.Location = new System.Drawing.Point(35, 27);
            this.cboSNNo.Name = "cboSNNo";
            this.cboSNNo.Size = new System.Drawing.Size(99, 19);
            this.cboSNNo.TabIndex = 2;
            // 
            // cboPaging
            // 
            this.cboPaging.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboPaging.isBusinessItemName = "Paging G/R";
            this.cboPaging.isMandatory = false;
            this.cboPaging.Location = new System.Drawing.Point(40, 91);
            this.cboPaging.Name = "cboPaging";
            this.cboPaging.Size = new System.Drawing.Size(36, 19);
            this.cboPaging.TabIndex = 6;
            this.cboPaging.SelectedIndexChanged += new System.EventHandler(this.cboPaging_SelectedIndexChanged);
            // 
            // btnPrev
            // 
            this.btnPrev.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnPrev.Location = new System.Drawing.Point(2, 91);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(35, 19);
            this.btnPrev.TabIndex = 5;
            this.btnPrev.Text = "Prev";
            this.btnPrev.Click += new System.EventHandler(this.executePaging);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnNext.Location = new System.Drawing.Point(78, 91);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(35, 19);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.executePaging);
            // 
            // lblGRNo
            // 
            this.lblGRNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblGRNo.Location = new System.Drawing.Point(134, 30);
            this.lblGRNo.Name = "lblGRNo";
            this.lblGRNo.Size = new System.Drawing.Size(37, 15);
            this.lblGRNo.Text = "GR No";
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnRetrieve.Location = new System.Drawing.Point(182, 87);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(54, 27);
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
            this.txtGRNo.Location = new System.Drawing.Point(170, 27);
            this.txtGRNo.Name = "txtGRNo";
            this.txtGRNo.Size = new System.Drawing.Size(66, 19);
            this.txtGRNo.TabIndex = 4;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(2, 52);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(23, 15);
            this.tLabel1.Text = "Doc";
            // 
            // txtDocMt
            // 
            this.txtDocMt.Enabled = false;
            this.txtDocMt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDocMt.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtDocMt.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDocMt.Location = new System.Drawing.Point(25, 50);
            this.txtDocMt.Multiline = true;
            this.txtDocMt.Name = "txtDocMt";
            this.txtDocMt.Size = new System.Drawing.Size(70, 17);
            this.txtDocMt.TabIndex = 19;
            this.txtDocMt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDocM3
            // 
            this.txtDocM3.Enabled = false;
            this.txtDocM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDocM3.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtDocM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDocM3.Location = new System.Drawing.Point(96, 50);
            this.txtDocM3.Multiline = true;
            this.txtDocM3.Name = "txtDocM3";
            this.txtDocM3.Size = new System.Drawing.Size(70, 17);
            this.txtDocM3.TabIndex = 20;
            this.txtDocM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDocQty
            // 
            this.txtDocQty.Enabled = false;
            this.txtDocQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDocQty.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtDocQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtDocQty.Location = new System.Drawing.Point(167, 50);
            this.txtDocQty.Multiline = true;
            this.txtDocQty.Name = "txtDocQty";
            this.txtDocQty.Size = new System.Drawing.Size(70, 17);
            this.txtDocQty.TabIndex = 21;
            this.txtDocQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtActQty
            // 
            this.txtActQty.Enabled = false;
            this.txtActQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtActQty.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtActQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtActQty.Location = new System.Drawing.Point(167, 68);
            this.txtActQty.Multiline = true;
            this.txtActQty.Name = "txtActQty";
            this.txtActQty.Size = new System.Drawing.Size(70, 17);
            this.txtActQty.TabIndex = 25;
            this.txtActQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtActM3
            // 
            this.txtActM3.Enabled = false;
            this.txtActM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtActM3.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtActM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtActM3.Location = new System.Drawing.Point(96, 68);
            this.txtActM3.Multiline = true;
            this.txtActM3.Name = "txtActM3";
            this.txtActM3.Size = new System.Drawing.Size(70, 17);
            this.txtActM3.TabIndex = 24;
            this.txtActM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtActMt
            // 
            this.txtActMt.Enabled = false;
            this.txtActMt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtActMt.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtActMt.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtActMt.Location = new System.Drawing.Point(25, 68);
            this.txtActMt.Multiline = true;
            this.txtActMt.Name = "txtActMt";
            this.txtActMt.Size = new System.Drawing.Size(70, 17);
            this.txtActMt.TabIndex = 23;
            this.txtActMt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(2, 70);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(23, 15);
            this.tLabel2.Text = "Act";
            // 
            // txtArvlDateFrom
            // 
            this.txtArvlDateFrom.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtArvlDateFrom.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtArvlDateFrom.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtArvlDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtArvlDateFrom.isBusinessItemName = "Estimate Arrival Date";
            this.txtArvlDateFrom.isMandatory = false;
            this.txtArvlDateFrom.Location = new System.Drawing.Point(50, 4);
            this.txtArvlDateFrom.Name = "txtArvlDateFrom";
            this.txtArvlDateFrom.Size = new System.Drawing.Size(84, 20);
            this.txtArvlDateFrom.TabIndex = 7;
            this.txtArvlDateFrom.Value = new System.DateTime(2008, 10, 23, 15, 17, 0, 484);
            // 
            // lblEstArrDateFmTo
            // 
            this.lblEstArrDateFmTo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblEstArrDateFmTo.Location = new System.Drawing.Point(137, 6);
            this.lblEstArrDateFmTo.Name = "lblEstArrDateFmTo";
            this.lblEstArrDateFmTo.Size = new System.Drawing.Size(9, 16);
            this.lblEstArrDateFmTo.Text = "~";
            // 
            // txtArvlDateTo
            // 
            this.txtArvlDateTo.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtArvlDateTo.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtArvlDateTo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtArvlDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtArvlDateTo.isBusinessItemName = "Estimate Arrival Date";
            this.txtArvlDateTo.isMandatory = false;
            this.txtArvlDateTo.Location = new System.Drawing.Point(149, 4);
            this.txtArvlDateTo.Name = "txtArvlDateTo";
            this.txtArvlDateTo.Size = new System.Drawing.Size(84, 20);
            this.txtArvlDateTo.TabIndex = 8;
            this.txtArvlDateTo.Value = new System.DateTime(2008, 10, 23, 15, 17, 0, 484);
            // 
            // lblEstArrDate
            // 
            this.lblEstArrDate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblEstArrDate.Location = new System.Drawing.Point(0, 1);
            this.lblEstArrDate.Name = "lblEstArrDate";
            this.lblEstArrDate.Size = new System.Drawing.Size(44, 25);
            this.lblEstArrDate.Text = "Est. Arr. Date";
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Enabled = true;
            this.tmrRefresh.Interval = 1000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // HCM103
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.txtActQty);
            this.Controls.Add(this.lblEstArrDate);
            this.Controls.Add(this.txtActM3);
            this.Controls.Add(this.txtActMt);
            this.Controls.Add(this.txtArvlDateTo);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.lblEstArrDateFmTo);
            this.Controls.Add(this.txtDocQty);
            this.Controls.Add(this.txtArvlDateFrom);
            this.Controls.Add(this.txtDocM3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtDocMt);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.grdGRList);
            this.Controls.Add(this.cboSNNo);
            this.Controls.Add(this.cboPaging);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnRetrieve);
            this.Controls.Add(this.lblSNNo);
            this.Controls.Add(this.txtGRNo);
            this.Controls.Add(this.lblGRNo);
            this.Name = "HCM103";
            this.Text = "G/R List";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HCM103_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TGrid grdGRList;
        private Framework.Controls.UserControls.TLabel lblSNNo;
        private Framework.Controls.UserControls.TTextBox txtGRNo;
        private Framework.Controls.UserControls.TCombobox cboSNNo;
        private Framework.Controls.UserControls.TLabel lblGRNo;
        private Framework.Controls.UserControls.TButton btnCancel;
        private Framework.Controls.UserControls.TButton btnOk;
        //add control for paging
        private Framework.Controls.UserControls.TCombobox cboPaging;
        private Framework.Controls.UserControls.TButton btnPrev;
        private Framework.Controls.UserControls.TButton btnNext;
        //add control for filter STATUS
        //private Framework.Controls.UserControls.TCombobox cboStatus;

        // tnkytn: for test
        //[AuthAccess("000001", "G/R Search")]
        public Framework.Controls.UserControls.TButton btnRetrieve;
        //[AuthAccess("000002", "JPVC Search")]
        private Framework.Controls.UserControls.TTextBox txtDocQty;
        private Framework.Controls.UserControls.TTextBox txtDocM3;
        private Framework.Controls.UserControls.TTextBox txtDocMt;
        private Framework.Controls.UserControls.TLabel tLabel1;
        private Framework.Controls.UserControls.TTextBox txtActQty;
        private Framework.Controls.UserControls.TTextBox txtActM3;
        private Framework.Controls.UserControls.TTextBox txtActMt;
        private Framework.Controls.UserControls.TLabel tLabel2;
        private Framework.Controls.UserControls.TDateTimePicker txtArvlDateFrom;
        private Framework.Controls.UserControls.TLabel lblEstArrDateFmTo;
        private Framework.Controls.UserControls.TDateTimePicker txtArvlDateTo;
        private Framework.Controls.UserControls.TLabel lblEstArrDate;

        //QUANBTL 09-08-2012 fix G/R retrieve performance START
        //private Framework.Controls.UserControls.TLabel lblDownloadSpd;
        private System.Windows.Forms.Timer tmrRefresh;
        
        //QUANBTL 09-08-2012 fix G/R retrieve performance END
    }
}