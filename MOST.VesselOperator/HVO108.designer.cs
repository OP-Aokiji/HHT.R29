namespace MOST.VesselOperator
{
    partial class HVO108
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
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.Label1 = new Framework.Controls.UserControls.TLabel();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.cboBType = new Framework.Controls.UserControls.TCombobox();
            this.Label2 = new Framework.Controls.UserControls.TLabel();
            this.txtJPVC2 = new Framework.Controls.UserControls.TTextBox();
            this.Label3 = new Framework.Controls.UserControls.TLabel();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.txtLOA2 = new Framework.Controls.UserControls.TTextBox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.txtATB2 = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtATU2 = new Framework.Controls.UserControls.TDateTimePicker();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
            this.txtATW2 = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtATC2 = new Framework.Controls.UserControls.TDateTimePicker();
            this.tLabel4 = new Framework.Controls.UserControls.TLabel();
            this.tLabel6 = new Framework.Controls.UserControls.TLabel();
            this.txtJPVC3 = new Framework.Controls.UserControls.TTextBox();
            this.tLabel8 = new Framework.Controls.UserControls.TLabel();
            this.btnJPVC3 = new Framework.Controls.UserControls.TButton();
            this.btnJPVC2 = new Framework.Controls.UserControls.TButton();
            this.SuspendLayout();
            // 
            // txtJPVC
            // 
            this.txtJPVC.Enabled = false;
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isBusinessItemName = "JPVC";
            this.txtJPVC.Location = new System.Drawing.Point(64, 2);
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(153, 19);
            this.txtJPVC.TabIndex = 1;
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label1.Location = new System.Drawing.Point(19, 5);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(39, 16);
            this.Label1.Text = "JPVC1";
            // 
            // btnF1
            // 
            this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF1.Location = new System.Drawing.Point(131, 61);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(35, 20);
            this.btnF1.TabIndex = 9;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // cboBType
            // 
            this.cboBType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboBType.isBusinessItemName = "Banking Type";
            this.cboBType.isMandatory = true;
            this.cboBType.Location = new System.Drawing.Point(64, 23);
            this.cboBType.Name = "cboBType";
            this.cboBType.Size = new System.Drawing.Size(153, 19);
            this.cboBType.TabIndex = 3;
            this.cboBType.SelectedIndexChanged += new System.EventHandler(this.cboBType_SelectedIndexChanged);
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label2.Location = new System.Drawing.Point(0, 65);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(39, 16);
            this.Label2.Text = "JPVC2";
            // 
            // txtJPVC2
            // 
            this.txtJPVC2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtJPVC2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC2.isBusinessItemName = "2nd JPVC";
            this.txtJPVC2.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC2.isMandatory = true;
            this.txtJPVC2.Location = new System.Drawing.Point(33, 62);
            this.txtJPVC2.Name = "txtJPVC2";
            this.txtJPVC2.Size = new System.Drawing.Size(95, 19);
            this.txtJPVC2.TabIndex = 8;
            this.txtJPVC2.Text = "09GGAG-GGAG2";
            this.txtJPVC2.LostFocus += new System.EventHandler(this.txtJPVC2_LostFocus);
            this.txtJPVC2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJPVC2_KeyPress);
            this.txtJPVC2.EnabledChanged += new System.EventHandler(this.txtJPVC2_EnabledChanged);
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label3.Location = new System.Drawing.Point(11, 26);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(47, 16);
            this.Label3.Text = "B. Type";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(171, 243);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(102, 243);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 25;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtLOA2
            // 
            this.txtLOA2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtLOA2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtLOA2.isBusinessItemName = "2nd LOA";
            this.txtLOA2.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtLOA2.Location = new System.Drawing.Point(194, 62);
            this.txtLOA2.Name = "txtLOA2";
            this.txtLOA2.Size = new System.Drawing.Size(43, 19);
            this.txtLOA2.TabIndex = 11;
            this.txtLOA2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(171, 65);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(24, 16);
            this.tLabel1.Text = "LOA";
            // 
            // txtATB2
            // 
            this.txtATB2.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtATB2.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtATB2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtATB2.isBusinessItemName = "2nd ATB";
            this.txtATB2.isMandatory = false;
            this.txtATB2.Location = new System.Drawing.Point(33, 83);
            this.txtATB2.Name = "txtATB2";
            this.txtATB2.Size = new System.Drawing.Size(128, 24);
            this.txtATB2.TabIndex = 13;
            this.txtATB2.Value = new System.DateTime(2008, 10, 20, 15, 4, 0, 812);
            // 
            // txtATU2
            // 
            this.txtATU2.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtATU2.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtATU2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtATU2.isBusinessItemName = "2nd ATU";
            this.txtATU2.isMandatory = false;
            this.txtATU2.Location = new System.Drawing.Point(33, 161);
            this.txtATU2.Name = "txtATU2";
            this.txtATU2.Size = new System.Drawing.Size(128, 24);
            this.txtATU2.TabIndex = 14;
            this.txtATU2.Value = new System.DateTime(2008, 10, 20, 15, 4, 0, 421);
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(7, 115);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(28, 18);
            this.tLabel2.Text = "ATW";
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(7, 88);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(29, 18);
            this.tLabel3.Text = "ATB";
            // 
            // txtATW2
            // 
            this.txtATW2.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtATW2.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtATW2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtATW2.isBusinessItemName = "3rd ATB";
            this.txtATW2.isMandatory = false;
            this.txtATW2.Location = new System.Drawing.Point(33, 109);
            this.txtATW2.Name = "txtATW2";
            this.txtATW2.Size = new System.Drawing.Size(128, 24);
            this.txtATW2.TabIndex = 21;
            this.txtATW2.Value = new System.DateTime(2008, 10, 20, 15, 4, 0, 812);
            // 
            // txtATC2
            // 
            this.txtATC2.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtATC2.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtATC2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtATC2.isBusinessItemName = "3rd ATU";
            this.txtATC2.isMandatory = false;
            this.txtATC2.Location = new System.Drawing.Point(33, 135);
            this.txtATC2.Name = "txtATC2";
            this.txtATC2.Size = new System.Drawing.Size(128, 24);
            this.txtATC2.TabIndex = 22;
            this.txtATC2.Value = new System.DateTime(2008, 10, 20, 15, 4, 0, 421);
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(7, 167);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(24, 18);
            this.tLabel4.Text = "ATU";
            // 
            // tLabel6
            // 
            this.tLabel6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel6.Location = new System.Drawing.Point(7, 141);
            this.tLabel6.Name = "tLabel6";
            this.tLabel6.Size = new System.Drawing.Size(29, 18);
            this.tLabel6.Text = "ATC";
            // 
            // txtJPVC3
            // 
            this.txtJPVC3.Enabled = false;
            this.txtJPVC3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC3.isBusinessItemName = "3rd JPVC";
            this.txtJPVC3.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC3.Location = new System.Drawing.Point(33, 205);
            this.txtJPVC3.Name = "txtJPVC3";
            this.txtJPVC3.Size = new System.Drawing.Size(95, 19);
            this.txtJPVC3.TabIndex = 16;
            // 
            // tLabel8
            // 
            this.tLabel8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel8.Location = new System.Drawing.Point(0, 208);
            this.tLabel8.Name = "tLabel8";
            this.tLabel8.Size = new System.Drawing.Size(39, 16);
            this.tLabel8.Text = "JPVC3";
            // 
            // btnJPVC3
            // 
            this.btnJPVC3.Location = new System.Drawing.Point(131, 202);
            this.btnJPVC3.Name = "btnJPVC3";
            this.btnJPVC3.Size = new System.Drawing.Size(104, 22);
            this.btnJPVC3.TabIndex = 17;
            this.btnJPVC3.Text = "JPVC3 Info";
            this.btnJPVC3.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnJPVC2
            // 
            this.btnJPVC2.Location = new System.Drawing.Point(168, 109);
            this.btnJPVC2.Name = "btnJPVC2";
            this.btnJPVC2.Size = new System.Drawing.Size(68, 53);
            this.btnJPVC2.TabIndex = 48;
            this.btnJPVC2.Text = "Input\r\nJPVC2\r\nOperation";
            this.btnJPVC2.Click += new System.EventHandler(this.ActionListener);
            // 
            // HVO108
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.btnJPVC2);
            this.Controls.Add(this.btnJPVC3);
            this.Controls.Add(this.txtATW2);
            this.Controls.Add(this.txtATC2);
            this.Controls.Add(this.tLabel6);
            this.Controls.Add(this.txtJPVC3);
            this.Controls.Add(this.tLabel8);
            this.Controls.Add(this.txtATB2);
            this.Controls.Add(this.txtATU2);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.tLabel3);
            this.Controls.Add(this.txtLOA2);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.cboBType);
            this.Controls.Add(this.txtJPVC2);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.tLabel4);
            this.Name = "HVO108";
            this.Text = "V/S - Double Banking";
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TTextBox txtJPVC;
        internal Framework.Controls.UserControls.TLabel Label1;
        internal Framework.Controls.UserControls.TButton btnF1;
        internal Framework.Controls.UserControls.TCombobox cboBType;
        internal Framework.Controls.UserControls.TLabel Label2;
        internal Framework.Controls.UserControls.TTextBox txtJPVC2;
        internal Framework.Controls.UserControls.TLabel Label3;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TTextBox txtLOA2;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        private Framework.Controls.UserControls.TDateTimePicker txtATB2;
        private Framework.Controls.UserControls.TDateTimePicker txtATU2;
        internal Framework.Controls.UserControls.TLabel tLabel2;
        internal Framework.Controls.UserControls.TLabel tLabel3;
        private Framework.Controls.UserControls.TDateTimePicker txtATW2;
        private Framework.Controls.UserControls.TDateTimePicker txtATC2;
        internal Framework.Controls.UserControls.TLabel tLabel4;
        internal Framework.Controls.UserControls.TLabel tLabel6;
        internal Framework.Controls.UserControls.TTextBox txtJPVC3;
        internal Framework.Controls.UserControls.TLabel tLabel8;
        internal Framework.Controls.UserControls.TButton btnJPVC3;
        internal Framework.Controls.UserControls.TButton btnJPVC2;
    }
}