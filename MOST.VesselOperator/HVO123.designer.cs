namespace MOST.VesselOperator
{
    partial class HVO123
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
            this.txtStartTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtEndTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.tLabel4 = new Framework.Controls.UserControls.TLabel();
            this.tLabel6 = new Framework.Controls.UserControls.TLabel();
            this.btnList = new Framework.Controls.UserControls.TButton();
            this.txtDS40 = new Framework.Controls.UserControls.TTextBox();
            this.txtDS20 = new Framework.Controls.UserControls.TTextBox();
            this.tLabel8 = new Framework.Controls.UserControls.TLabel();
            this.tLabel9 = new Framework.Controls.UserControls.TLabel();
            this.txtLD40 = new Framework.Controls.UserControls.TTextBox();
            this.txtLD20 = new Framework.Controls.UserControls.TTextBox();
            this.cboHatch = new Framework.Controls.UserControls.TCombobox();
            this.Label19 = new Framework.Controls.UserControls.TLabel();
            this.Label7 = new Framework.Controls.UserControls.TLabel();
            this.Label15 = new Framework.Controls.UserControls.TLabel();
            this.Label16 = new Framework.Controls.UserControls.TLabel();
            this.Label17 = new Framework.Controls.UserControls.TLabel();
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.Label1 = new Framework.Controls.UserControls.TLabel();
            this.cboEqu = new Framework.Controls.UserControls.TCombobox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(96, 240);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 18;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(166, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtStartTime
            // 
            this.txtStartTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtStartTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtStartTime.isBusinessItemName = "Start Time";
            this.txtStartTime.isMandatory = true;
            this.txtStartTime.Location = new System.Drawing.Point(61, 159);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(131, 24);
            this.txtStartTime.TabIndex = 11;
            this.txtStartTime.Value = new System.DateTime(2008, 10, 20, 15, 4, 25, 812);
            // 
            // txtEndTime
            // 
            this.txtEndTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtEndTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtEndTime.isBusinessItemName = "End Time";
            this.txtEndTime.isMandatory = true;
            this.txtEndTime.Location = new System.Drawing.Point(61, 189);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(131, 24);
            this.txtEndTime.TabIndex = 12;
            this.txtEndTime.Value = new System.DateTime(2008, 10, 20, 15, 4, 25, 421);
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(4, 195);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(50, 18);
            this.tLabel4.Text = "EndTime";
            // 
            // tLabel6
            // 
            this.tLabel6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel6.Location = new System.Drawing.Point(4, 165);
            this.tLabel6.Name = "tLabel6";
            this.tLabel6.Size = new System.Drawing.Size(51, 18);
            this.tLabel6.Text = "StartTime";
            // 
            // btnList
            // 
            this.btnList.Location = new System.Drawing.Point(4, 240);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(58, 24);
            this.btnList.TabIndex = 15;
            this.btnList.Text = "List";
            this.btnList.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtDS40
            // 
            this.txtDS40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtDS40.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDS40.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDS40.Location = new System.Drawing.Point(179, 123);
            this.txtDS40.Multiline = true;
            this.txtDS40.Name = "txtDS40";
            this.txtDS40.Size = new System.Drawing.Size(52, 17);
            this.txtDS40.TabIndex = 8;
            this.txtDS40.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDS20
            // 
            this.txtDS20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtDS20.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDS20.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDS20.Location = new System.Drawing.Point(105, 123);
            this.txtDS20.Multiline = true;
            this.txtDS20.Name = "txtDS20";
            this.txtDS20.Size = new System.Drawing.Size(52, 17);
            this.txtDS20.TabIndex = 7;
            this.txtDS20.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel8
            // 
            this.tLabel8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel8.Location = new System.Drawing.Point(162, 125);
            this.tLabel8.Name = "tLabel8";
            this.tLabel8.Size = new System.Drawing.Size(18, 15);
            this.tLabel8.Text = "40\'";
            // 
            // tLabel9
            // 
            this.tLabel9.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel9.Location = new System.Drawing.Point(87, 125);
            this.tLabel9.Name = "tLabel9";
            this.tLabel9.Size = new System.Drawing.Size(18, 15);
            this.tLabel9.Text = "20\'";
            // 
            // txtLD40
            // 
            this.txtLD40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtLD40.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtLD40.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtLD40.Location = new System.Drawing.Point(179, 98);
            this.txtLD40.Multiline = true;
            this.txtLD40.Name = "txtLD40";
            this.txtLD40.Size = new System.Drawing.Size(52, 17);
            this.txtLD40.TabIndex = 6;
            this.txtLD40.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLD20
            // 
            this.txtLD20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtLD20.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtLD20.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtLD20.Location = new System.Drawing.Point(105, 98);
            this.txtLD20.Multiline = true;
            this.txtLD20.Name = "txtLD20";
            this.txtLD20.Size = new System.Drawing.Size(52, 17);
            this.txtLD20.TabIndex = 5;
            this.txtLD20.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cboHatch
            // 
            this.cboHatch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboHatch.isBusinessItemName = "Hatch";
            this.cboHatch.isMandatory = true;
            this.cboHatch.Location = new System.Drawing.Point(61, 35);
            this.cboHatch.Name = "cboHatch";
            this.cboHatch.Size = new System.Drawing.Size(131, 19);
            this.cboHatch.TabIndex = 1;
            // 
            // Label19
            // 
            this.Label19.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label19.Location = new System.Drawing.Point(162, 100);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(21, 15);
            this.Label19.Text = "40\'";
            // 
            // Label7
            // 
            this.Label7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label7.Location = new System.Drawing.Point(87, 100);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(18, 15);
            this.Label7.Text = "20\'";
            // 
            // Label15
            // 
            this.Label15.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label15.Location = new System.Drawing.Point(3, 125);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(82, 15);
            this.Label15.Text = "Discharging Qty";
            // 
            // Label16
            // 
            this.Label16.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label16.Location = new System.Drawing.Point(3, 100);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(64, 15);
            this.Label16.Text = "Loading Qty";
            // 
            // Label17
            // 
            this.Label17.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label17.Location = new System.Drawing.Point(14, 37);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(32, 13);
            this.Label17.Text = "Hatch";
            // 
            // txtJPVC
            // 
            this.txtJPVC.Enabled = false;
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isBusinessItemName = "JPVC";
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.isMandatory = true;
            this.txtJPVC.Location = new System.Drawing.Point(61, 10);
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(131, 19);
            this.txtJPVC.TabIndex = 58;
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label1.Location = new System.Drawing.Point(12, 14);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(33, 13);
            this.Label1.Text = "JPVC";
            // 
            // cboEqu
            // 
            this.cboEqu.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboEqu.isBusinessItemName = "Equipment";
            this.cboEqu.isMandatory = false;
            this.cboEqu.Location = new System.Drawing.Point(61, 60);
            this.cboEqu.Name = "cboEqu";
            this.cboEqu.Size = new System.Drawing.Size(131, 19);
            this.cboEqu.TabIndex = 3;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(3, 62);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(56, 13);
            this.tLabel1.Text = "Equipment";
            // 
            // HVO123
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.cboEqu);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtDS40);
            this.Controls.Add(this.txtDS20);
            this.Controls.Add(this.tLabel8);
            this.Controls.Add(this.tLabel9);
            this.Controls.Add(this.txtLD40);
            this.Controls.Add(this.txtLD20);
            this.Controls.Add(this.cboHatch);
            this.Controls.Add(this.Label19);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.Label17);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.txtStartTime);
            this.Controls.Add(this.txtEndTime);
            this.Controls.Add(this.tLabel4);
            this.Controls.Add(this.tLabel6);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Name = "HVO123";
            this.Text = "V/S - Container";
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TButton btnCancel;
        private Framework.Controls.UserControls.TDateTimePicker txtStartTime;
        private Framework.Controls.UserControls.TDateTimePicker txtEndTime;
        internal Framework.Controls.UserControls.TLabel tLabel4;
        internal Framework.Controls.UserControls.TLabel tLabel6;
        internal Framework.Controls.UserControls.TButton btnList;
        internal Framework.Controls.UserControls.TTextBox txtDS40;
        internal Framework.Controls.UserControls.TTextBox txtDS20;
        internal Framework.Controls.UserControls.TLabel tLabel8;
        internal Framework.Controls.UserControls.TLabel tLabel9;
        internal Framework.Controls.UserControls.TTextBox txtLD40;
        internal Framework.Controls.UserControls.TTextBox txtLD20;
        internal Framework.Controls.UserControls.TCombobox cboHatch;
        internal Framework.Controls.UserControls.TLabel Label19;
        internal Framework.Controls.UserControls.TLabel Label7;
        internal Framework.Controls.UserControls.TLabel Label15;
        internal Framework.Controls.UserControls.TLabel Label16;
        internal Framework.Controls.UserControls.TLabel Label17;
        internal Framework.Controls.UserControls.TTextBox txtJPVC;
        internal Framework.Controls.UserControls.TLabel Label1;
        internal Framework.Controls.UserControls.TCombobox cboEqu;
        internal Framework.Controls.UserControls.TLabel tLabel1;
    }
}