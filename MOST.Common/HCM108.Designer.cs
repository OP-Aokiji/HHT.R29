namespace MOST.Common
{
    partial class HCM108
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
            this.grdDOList = new Framework.Controls.UserControls.TGrid();
            this.btnSearch = new Framework.Controls.UserControls.TButton();
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.lblJPVC = new Framework.Controls.UserControls.TLabel();
            this.txtDONo = new Framework.Controls.UserControls.TTextBox();
            this.lblDONo = new Framework.Controls.UserControls.TLabel();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.lblEstArrDate = new Framework.Controls.UserControls.TLabel();
            this.txtArvlDateTo = new Framework.Controls.UserControls.TDateTimePicker();
            this.lblEstArrDateFmTo = new Framework.Controls.UserControls.TLabel();
            this.txtArvlDateFrom = new Framework.Controls.UserControls.TDateTimePicker();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(95, 240);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grdDOList
            // 
            this.grdDOList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdDOList.IsDirty = false;
            this.grdDOList.Location = new System.Drawing.Point(6, 73);
            this.grdDOList.Name = "grdDOList";
            this.grdDOList.RowHeadersVisible = false;
            this.grdDOList.Size = new System.Drawing.Size(225, 164);
            this.grdDOList.TabIndex = 5;
            this.grdDOList.DoubleClick += new System.EventHandler(this.grdDOList_DoubleClick);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnSearch.Location = new System.Drawing.Point(166, 50);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(65, 20);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtJPVC
            // 
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isBusinessItemName = "JPVC";
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.Location = new System.Drawing.Point(48, 29);
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(112, 19);
            this.txtJPVC.TabIndex = 1;
            // 
            // lblJPVC
            // 
            this.lblJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblJPVC.Location = new System.Drawing.Point(3, 32);
            this.lblJPVC.Name = "lblJPVC";
            this.lblJPVC.Size = new System.Drawing.Size(39, 17);
            this.lblJPVC.Text = "JPVC";
            this.lblJPVC.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDONo
            // 
            this.txtDONo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDONo.isBusinessItemName = "G/R No";
            this.txtDONo.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtDONo.Location = new System.Drawing.Point(48, 51);
            this.txtDONo.Name = "txtDONo";
            this.txtDONo.Size = new System.Drawing.Size(112, 19);
            this.txtDONo.TabIndex = 3;
            // 
            // lblDONo
            // 
            this.lblDONo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblDONo.Location = new System.Drawing.Point(1, 53);
            this.lblDONo.Name = "lblDONo";
            this.lblDONo.Size = new System.Drawing.Size(49, 17);
            this.lblDONo.Text = "DO No";
            // 
            // btnF1
            // 
            this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF1.Location = new System.Drawing.Point(166, 28);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(44, 19);
            this.btnF1.TabIndex = 2;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.btnF1_Click);
            // 
            // lblEstArrDate
            // 
            this.lblEstArrDate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblEstArrDate.Location = new System.Drawing.Point(2, 0);
            this.lblEstArrDate.Name = "lblEstArrDate";
            this.lblEstArrDate.Size = new System.Drawing.Size(44, 25);
            this.lblEstArrDate.Text = "Est. Arr. Date";
            // 
            // txtArvlDateTo
            // 
            this.txtArvlDateTo.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtArvlDateTo.CustomFormat = "dd/MM/yyyy";
            this.txtArvlDateTo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtArvlDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtArvlDateTo.isBusinessItemName = "Estimate Arrival Date";
            this.txtArvlDateTo.isMandatory = false;
            this.txtArvlDateTo.Location = new System.Drawing.Point(149, 5);
            this.txtArvlDateTo.Name = "txtArvlDateTo";
            this.txtArvlDateTo.Size = new System.Drawing.Size(84, 20);
            this.txtArvlDateTo.TabIndex = 12;
            this.txtArvlDateTo.Value = new System.DateTime(2008, 10, 23, 0, 0, 0, 484);
            this.txtArvlDateTo.LostFocus += new System.EventHandler(this.txtArvlDateTo_LostFocus);
            // 
            // lblEstArrDateFmTo
            // 
            this.lblEstArrDateFmTo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblEstArrDateFmTo.Location = new System.Drawing.Point(137, 7);
            this.lblEstArrDateFmTo.Name = "lblEstArrDateFmTo";
            this.lblEstArrDateFmTo.Size = new System.Drawing.Size(9, 16);
            this.lblEstArrDateFmTo.Text = "~";
            // 
            // txtArvlDateFrom
            // 
            this.txtArvlDateFrom.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtArvlDateFrom.CustomFormat = "dd/MM/yyyy";
            this.txtArvlDateFrom.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtArvlDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtArvlDateFrom.isBusinessItemName = "Estimate Arrival Date";
            this.txtArvlDateFrom.isMandatory = false;
            this.txtArvlDateFrom.Location = new System.Drawing.Point(50, 5);
            this.txtArvlDateFrom.Name = "txtArvlDateFrom";
            this.txtArvlDateFrom.Size = new System.Drawing.Size(84, 20);
            this.txtArvlDateFrom.TabIndex = 10;
            this.txtArvlDateFrom.Value = new System.DateTime(2008, 10, 23, 0, 0, 0, 484);
            this.txtArvlDateFrom.LostFocus += new System.EventHandler(this.txtArvlDateFrom_LostFocus);
            // 
            // HCM108
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.lblEstArrDate);
            this.Controls.Add(this.txtArvlDateTo);
            this.Controls.Add(this.lblEstArrDateFmTo);
            this.Controls.Add(this.txtArvlDateFrom);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.txtDONo);
            this.Controls.Add(this.lblDONo);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.lblJPVC);
            this.Controls.Add(this.grdDOList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "HCM108";
            this.Text = "D/O List";
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TButton btnOk;
        private Framework.Controls.UserControls.TButton btnCancel;
        private Framework.Controls.UserControls.TGrid grdDOList;
        private Framework.Controls.UserControls.TButton btnSearch;
        private Framework.Controls.UserControls.TTextBox txtJPVC;
        private Framework.Controls.UserControls.TLabel lblJPVC;
        private Framework.Controls.UserControls.TTextBox txtDONo;
        private Framework.Controls.UserControls.TLabel lblDONo;
        private Framework.Controls.UserControls.TButton btnF1;
        private Framework.Controls.UserControls.TLabel lblEstArrDate;
        private Framework.Controls.UserControls.TDateTimePicker txtArvlDateTo;
        private Framework.Controls.UserControls.TLabel lblEstArrDateFmTo;
        private Framework.Controls.UserControls.TDateTimePicker txtArvlDateFrom;
    }
}