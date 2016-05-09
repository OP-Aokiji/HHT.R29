using MOST.Common.UserAttribute;

namespace MOST.PortSafety
{
    partial class HPS102
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
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.lblGRNo = new Framework.Controls.UserControls.TLabel();
            this.btnRetrieve = new Framework.Controls.UserControls.TButton();
            this.btnF2 = new Framework.Controls.UserControls.TButton();
            this.txtFwdAgent = new Framework.Controls.UserControls.TTextBox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.txtGR = new Framework.Controls.UserControls.TTextBox();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.txtStart = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtEnd = new Framework.Controls.UserControls.TDateTimePicker();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
            this.tLabel4 = new Framework.Controls.UserControls.TLabel();
            this.btnExit = new Framework.Controls.UserControls.TButton();
            this.rbtnNonJPVC = new Framework.Controls.UserControls.TRadioButton();
            this.rbtnJPVC = new Framework.Controls.UserControls.TRadioButton();
            this.cboSNNo = new Framework.Controls.UserControls.TCombobox();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(2, 117);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(234, 124);
            this.grdData.TabIndex = 12;
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
            this.txtJPVC.LostFocus += new System.EventHandler(this.txtJPVC_LostFocus);
            this.txtJPVC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJPVC_KeyPress);
            this.txtJPVC.TextChanged += new System.EventHandler(this.txtJPVC_TextChanged);
            // 
            // btnF1
            // 
            this.btnF1.Location = new System.Drawing.Point(202, 2);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(30, 18);
            this.btnF1.TabIndex = 2;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // lblGRNo
            // 
            this.lblGRNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblGRNo.Location = new System.Drawing.Point(0, 26);
            this.lblGRNo.Name = "lblGRNo";
            this.lblGRNo.Size = new System.Drawing.Size(18, 16);
            this.lblGRNo.Text = "SN";
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Location = new System.Drawing.Point(167, 91);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(65, 24);
            this.btnRetrieve.TabIndex = 10;
            this.btnRetrieve.Text = "Retrieve";
            this.btnRetrieve.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnF2
            // 
            this.btnF2.Location = new System.Drawing.Point(202, 47);
            this.btnF2.Name = "btnF2";
            this.btnF2.Size = new System.Drawing.Size(30, 18);
            this.btnF2.TabIndex = 7;
            this.btnF2.Text = "F2";
            this.btnF2.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtFwdAgent
            // 
            this.txtFwdAgent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtFwdAgent.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtFwdAgent.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtFwdAgent.Location = new System.Drawing.Point(151, 47);
            this.txtFwdAgent.Name = "txtFwdAgent";
            this.txtFwdAgent.Size = new System.Drawing.Size(48, 19);
            this.txtFwdAgent.TabIndex = 6;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(107, 48);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(46, 17);
            this.tLabel1.Text = "F.Agent";
            // 
            // txtGR
            // 
            this.txtGR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtGR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtGR.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtGR.Location = new System.Drawing.Point(18, 47);
            this.txtGR.Name = "txtGR";
            this.txtGR.Size = new System.Drawing.Size(85, 19);
            this.txtGR.TabIndex = 5;
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(0, 48);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(18, 16);
            this.tLabel2.Text = "GR";
            // 
            // txtStart
            // 
            this.txtStart.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtStart.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtStart.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtStart.isBusinessItemName = "";
            this.txtStart.isMandatory = false;
            this.txtStart.Location = new System.Drawing.Point(43, 68);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(81, 20);
            this.txtStart.TabIndex = 8;
            this.txtStart.Value = new System.DateTime(2008, 10, 23, 15, 17, 0, 484);
            // 
            // txtEnd
            // 
            this.txtEnd.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtEnd.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtEnd.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtEnd.isBusinessItemName = "";
            this.txtEnd.isMandatory = false;
            this.txtEnd.Location = new System.Drawing.Point(151, 68);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(81, 20);
            this.txtEnd.TabIndex = 9;
            this.txtEnd.Value = new System.DateTime(2008, 10, 23, 15, 17, 0, 812);
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(131, 70);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(9, 16);
            this.tLabel3.Text = "~";
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(0, 72);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(43, 16);
            this.tLabel4.Text = "GI Time";
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnExit.Location = new System.Drawing.Point(175, 243);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(60, 24);
            this.btnExit.TabIndex = 25;
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
            this.rbtnNonJPVC.TabIndex = 68;
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
            this.rbtnJPVC.TabIndex = 69;
            this.rbtnJPVC.Text = "JPVC";
            this.rbtnJPVC.CheckedChanged += new System.EventHandler(this.RadiobuttonListener);
            // 
            // cboSNNo
            // 
            this.cboSNNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboSNNo.isBusinessItemName = "S/N No";
            this.cboSNNo.isMandatory = false;
            this.cboSNNo.Location = new System.Drawing.Point(18, 23);
            this.cboSNNo.Name = "cboSNNo";
            this.cboSNNo.Size = new System.Drawing.Size(181, 19);
            this.cboSNNo.TabIndex = 70;
            // 
            // HPS102
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.cboSNNo);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tLabel4);
            this.Controls.Add(this.tLabel3);
            this.Controls.Add(this.txtEnd);
            this.Controls.Add(this.txtStart);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.txtGR);
            this.Controls.Add(this.btnF2);
            this.Controls.Add(this.txtFwdAgent);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.lblGRNo);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.btnRetrieve);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.rbtnNonJPVC);
            this.Controls.Add(this.rbtnJPVC);
            this.Name = "HPS102";
            this.Text = "P/S - List of Gate-In";
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TGrid grdData;
        private Framework.Controls.UserControls.TTextBox txtJPVC;
        private Framework.Controls.UserControls.TLabel lblGRNo;
        public Framework.Controls.UserControls.TButton btnRetrieve;
        public Framework.Controls.UserControls.TButton btnF1;
        public Framework.Controls.UserControls.TButton btnF2;
        private Framework.Controls.UserControls.TTextBox txtFwdAgent;
        private Framework.Controls.UserControls.TLabel tLabel1;
        private Framework.Controls.UserControls.TTextBox txtGR;
        private Framework.Controls.UserControls.TLabel tLabel2;
        private Framework.Controls.UserControls.TDateTimePicker txtStart;
        private Framework.Controls.UserControls.TDateTimePicker txtEnd;
        private Framework.Controls.UserControls.TLabel tLabel3;
        private Framework.Controls.UserControls.TLabel tLabel4;
        private Framework.Controls.UserControls.TButton btnExit;
        internal Framework.Controls.UserControls.TRadioButton rbtnNonJPVC;
        internal Framework.Controls.UserControls.TRadioButton rbtnJPVC;
        private Framework.Controls.UserControls.TCombobox cboSNNo;        
    }
}