using MOST.Common.UserAttribute;

namespace MOST.PortSafety
{
    partial class HPS104
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
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.lblJPVC = new Framework.Controls.UserControls.TLabel();
            this.lblGRNo = new Framework.Controls.UserControls.TLabel();
            this.btnRetrieve = new Framework.Controls.UserControls.TButton();
            this.txtLorry = new Framework.Controls.UserControls.TTextBox();
            this.btnF2 = new Framework.Controls.UserControls.TButton();
            this.txtTsptr = new Framework.Controls.UserControls.TTextBox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.txtArvlDateFrom = new Framework.Controls.UserControls.TDateTimePicker();
            this.btnExit = new Framework.Controls.UserControls.TButton();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
            this.txtArvlDateTo = new Framework.Controls.UserControls.TDateTimePicker();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(3, 92);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(232, 148);
            this.grdData.TabIndex = 12;
            // 
            // lblJPVC
            // 
            this.lblJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblJPVC.Location = new System.Drawing.Point(1, 0);
            this.lblJPVC.Name = "lblJPVC";
            this.lblJPVC.Size = new System.Drawing.Size(69, 29);
            this.lblJPVC.Text = "Expected Arrival Date";
            // 
            // lblGRNo
            // 
            this.lblGRNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblGRNo.Location = new System.Drawing.Point(23, 29);
            this.lblGRNo.Name = "lblGRNo";
            this.lblGRNo.Size = new System.Drawing.Size(36, 16);
            this.lblGRNo.Text = "Lorry";
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Location = new System.Drawing.Point(170, 66);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(65, 24);
            this.btnRetrieve.TabIndex = 10;
            this.btnRetrieve.Text = "Retrieve";
            this.btnRetrieve.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtLorry
            // 
            this.txtLorry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtLorry.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.txtLorry.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtLorry.Location = new System.Drawing.Point(65, 27);
            this.txtLorry.Name = "txtLorry";
            this.txtLorry.Size = new System.Drawing.Size(81, 18);
            this.txtLorry.TabIndex = 4;
            // 
            // btnF2
            // 
            this.btnF2.Location = new System.Drawing.Point(148, 46);
            this.btnF2.Name = "btnF2";
            this.btnF2.Size = new System.Drawing.Size(35, 18);
            this.btnF2.TabIndex = 7;
            this.btnF2.Text = "F2";
            this.btnF2.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtTsptr
            // 
            this.txtTsptr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtTsptr.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.txtTsptr.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtTsptr.Location = new System.Drawing.Point(65, 46);
            this.txtTsptr.Name = "txtTsptr";
            this.txtTsptr.Size = new System.Drawing.Size(81, 18);
            this.txtTsptr.TabIndex = 6;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(1, 47);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(67, 17);
            this.tLabel1.Text = "Transporter";
            // 
            // txtArvlDateFrom
            // 
            this.txtArvlDateFrom.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtArvlDateFrom.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtArvlDateFrom.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtArvlDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtArvlDateFrom.isBusinessItemName = "Expected Arrival Date";
            this.txtArvlDateFrom.isMandatory = true;
            this.txtArvlDateFrom.Location = new System.Drawing.Point(65, 3);
            this.txtArvlDateFrom.Name = "txtArvlDateFrom";
            this.txtArvlDateFrom.Size = new System.Drawing.Size(81, 20);
            this.txtArvlDateFrom.TabIndex = 1;
            this.txtArvlDateFrom.Value = new System.DateTime(2008, 10, 23, 15, 17, 24, 484);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(170, 242);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(65, 24);
            this.btnExit.TabIndex = 14;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.ActionListener);
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(145, 5);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(9, 16);
            this.tLabel3.Text = "~";
            // 
            // txtArvlDateTo
            // 
            this.txtArvlDateTo.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtArvlDateTo.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtArvlDateTo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtArvlDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtArvlDateTo.isBusinessItemName = "Expected Arrival Date";
            this.txtArvlDateTo.isMandatory = true;
            this.txtArvlDateTo.Location = new System.Drawing.Point(155, 3);
            this.txtArvlDateTo.Name = "txtArvlDateTo";
            this.txtArvlDateTo.Size = new System.Drawing.Size(81, 20);
            this.txtArvlDateTo.TabIndex = 2;
            this.txtArvlDateTo.Value = new System.DateTime(2008, 10, 23, 15, 17, 24, 484);
            // 
            // HPS104
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.txtArvlDateTo);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtArvlDateFrom);
            this.Controls.Add(this.btnF2);
            this.Controls.Add(this.txtTsptr);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.txtLorry);
            this.Controls.Add(this.lblGRNo);
            this.Controls.Add(this.btnRetrieve);
            this.Controls.Add(this.lblJPVC);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.tLabel3);
            this.Name = "HPS104";
            this.Text = "P/S - Assigned Lorry List";
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TGrid grdData;
        private Framework.Controls.UserControls.TLabel lblJPVC;
        private Framework.Controls.UserControls.TTextBox txtLorry;
        private Framework.Controls.UserControls.TLabel lblGRNo;
        public Framework.Controls.UserControls.TButton btnRetrieve;
        public Framework.Controls.UserControls.TButton btnF2;
        private Framework.Controls.UserControls.TTextBox txtTsptr;
        private Framework.Controls.UserControls.TLabel tLabel1;
        private Framework.Controls.UserControls.TDateTimePicker txtArvlDateFrom;
        private Framework.Controls.UserControls.TButton btnExit;
        private Framework.Controls.UserControls.TLabel tLabel3;
        private Framework.Controls.UserControls.TDateTimePicker txtArvlDateTo;        
    }
}