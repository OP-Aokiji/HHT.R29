namespace MOST.VesselOperator
{
    partial class HVO111
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
            this.cboHatch = new Framework.Controls.UserControls.TCombobox();
            this.Label2 = new Framework.Controls.UserControls.TLabel();
            this.txtTotal = new Framework.Controls.UserControls.TTextBox();
            this.txtRate = new Framework.Controls.UserControls.TTextBox();
            this.Label8 = new Framework.Controls.UserControls.TLabel();
            this.cboRole = new Framework.Controls.UserControls.TCombobox();
            this.Label6 = new Framework.Controls.UserControls.TLabel();
            this.Label5 = new Framework.Controls.UserControls.TLabel();
            this.cboParticulars = new Framework.Controls.UserControls.TCombobox();
            this.Label4 = new Framework.Controls.UserControls.TLabel();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.txtPntyDescr = new Framework.Controls.UserControls.TTextBox();
            this.txtUnitPrc = new Framework.Controls.UserControls.TTextBox();
            this.btnDelete = new Framework.Controls.UserControls.TButton();
            this.btnUpdate = new Framework.Controls.UserControls.TButton();
            this.btnAdd = new Framework.Controls.UserControls.TButton();
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.Label7 = new Framework.Controls.UserControls.TLabel();
            this.txtDate = new Framework.Controls.UserControls.TDateTimePicker();
            this.Label1 = new Framework.Controls.UserControls.TLabel();
            this.cboContractor = new Framework.Controls.UserControls.TCombobox();
            this.txtEndTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtStartTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.tLabel5 = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
            // 
            // cboHatch
            // 
            this.cboHatch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboHatch.isBusinessItemName = "Hatch";
            this.cboHatch.isMandatory = true;
            this.cboHatch.Location = new System.Drawing.Point(31, 2);
            this.cboHatch.Name = "cboHatch";
            this.cboHatch.Size = new System.Drawing.Size(52, 19);
            this.cboHatch.TabIndex = 2;
            this.cboHatch.SelectedIndexChanged += new System.EventHandler(this.cboHatch_SelectedIndexChanged);
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label2.Location = new System.Drawing.Point(0, 6);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(32, 16);
            this.Label2.Text = "Hatch";
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtTotal.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtTotal.Location = new System.Drawing.Point(159, 115);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(78, 19);
            this.txtTotal.TabIndex = 91;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRate
            // 
            this.txtRate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtRate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtRate.isBusinessItemName = "Rate";
            this.txtRate.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtRate.isMandatory = true;
            this.txtRate.Location = new System.Drawing.Point(184, 94);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(53, 19);
            this.txtRate.TabIndex = 12;
            this.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRate.LostFocus += new System.EventHandler(this.txtRate_LostFocus);
            // 
            // Label8
            // 
            this.Label8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label8.Location = new System.Drawing.Point(132, 119);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(28, 15);
            this.Label8.Text = "Total";
            // 
            // cboRole
            // 
            this.cboRole.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboRole.isBusinessItemName = "Role";
            this.cboRole.isMandatory = false;
            this.cboRole.Location = new System.Drawing.Point(47, 48);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(188, 19);
            this.cboRole.TabIndex = 6;
            this.cboRole.SelectedIndexChanged += new System.EventHandler(this.cboRole_SelectedIndexChanged);
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label6.Location = new System.Drawing.Point(0, 100);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(39, 13);
            this.Label6.Text = "Penalty";
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label5.Location = new System.Drawing.Point(7, 52);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(25, 13);
            this.Label5.Text = "Role";
            // 
            // cboParticulars
            // 
            this.cboParticulars.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboParticulars.isBusinessItemName = "Particulars";
            this.cboParticulars.isMandatory = true;
            this.cboParticulars.Location = new System.Drawing.Point(47, 25);
            this.cboParticulars.Name = "cboParticulars";
            this.cboParticulars.Size = new System.Drawing.Size(188, 19);
            this.cboParticulars.TabIndex = 5;
            this.cboParticulars.SelectedIndexChanged += new System.EventHandler(this.cboParticulars_SelectedIndexChanged);
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label4.Location = new System.Drawing.Point(0, 30);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(49, 13);
            this.Label4.Text = "Particular";
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnOk.Location = new System.Drawing.Point(95, 247);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 21);
            this.btnOk.TabIndex = 38;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(167, 247);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 21);
            this.btnCancel.TabIndex = 39;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtPntyDescr
            // 
            this.txtPntyDescr.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtPntyDescr.Location = new System.Drawing.Point(37, 94);
            this.txtPntyDescr.Name = "txtPntyDescr";
            this.txtPntyDescr.Size = new System.Drawing.Size(85, 19);
            this.txtPntyDescr.TabIndex = 21;
            // 
            // txtUnitPrc
            // 
            this.txtUnitPrc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtUnitPrc.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtUnitPrc.Location = new System.Drawing.Point(123, 94);
            this.txtUnitPrc.Name = "txtUnitPrc";
            this.txtUnitPrc.Size = new System.Drawing.Size(54, 19);
            this.txtUnitPrc.TabIndex = 22;
            this.txtUnitPrc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(145, 139);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(65, 20);
            this.btnDelete.TabIndex = 33;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(74, 139);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(65, 20);
            this.btnUpdate.TabIndex = 32;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 139);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(65, 20);
            this.btnAdd.TabIndex = 31;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.ActionListener);
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(2, 161);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(234, 85);
            this.grdData.TabIndex = 34;
            this.grdData.CurrentCellChanged += new System.EventHandler(this.grdData_CurrentCellChanged);
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(176, 96);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(10, 16);
            this.tLabel1.Text = "x";
            // 
            // Label7
            // 
            this.Label7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label7.Location = new System.Drawing.Point(1, 118);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(29, 16);
            this.Label7.Text = "Date";
            // 
            // txtDate
            // 
            this.txtDate.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtDate.CustomFormat = "dd/MM/yyyy";
            this.txtDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDate.isBusinessItemName = "Date";
            this.txtDate.isMandatory = true;
            this.txtDate.Location = new System.Drawing.Point(37, 114);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(93, 22);
            this.txtDate.TabIndex = 15;
            this.txtDate.Value = new System.DateTime(2008, 10, 17, 0, 0, 0, 0);
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label1.Location = new System.Drawing.Point(85, 5);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(56, 16);
            this.Label1.Text = "Contractor";
            // 
            // cboContractor
            // 
            this.cboContractor.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboContractor.isBusinessItemName = "Contractor";
            this.cboContractor.isMandatory = true;
            this.cboContractor.Location = new System.Drawing.Point(139, 2);
            this.cboContractor.Name = "cboContractor";
            this.cboContractor.Size = new System.Drawing.Size(96, 19);
            this.cboContractor.TabIndex = 3;
            // 
            // txtEndTime
            // 
            this.txtEndTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtEndTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtEndTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtEndTime.isBusinessItemName = "End Time";
            this.txtEndTime.isMandatory = false;
            this.txtEndTime.Location = new System.Drawing.Point(124, 72);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(114, 20);
            this.txtEndTime.TabIndex = 8;
            this.txtEndTime.Value = new System.DateTime(2008, 10, 23, 18, 35, 0, 890);
            // 
            // txtStartTime
            // 
            this.txtStartTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtStartTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtStartTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtStartTime.isBusinessItemName = "Start Time";
            this.txtStartTime.isMandatory = false;
            this.txtStartTime.Location = new System.Drawing.Point(0, 72);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(114, 20);
            this.txtStartTime.TabIndex = 7;
            this.txtStartTime.Value = new System.DateTime(2008, 10, 23, 18, 35, 0, 203);
            // 
            // tLabel5
            // 
            this.tLabel5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel5.Location = new System.Drawing.Point(114, 74);
            this.tLabel5.Name = "tLabel5";
            this.tLabel5.Size = new System.Drawing.Size(9, 16);
            this.tLabel5.Text = "~";
            // 
            // HVO111
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.txtEndTime);
            this.Controls.Add(this.txtStartTime);
            this.Controls.Add(this.tLabel5);
            this.Controls.Add(this.cboContractor);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.txtUnitPrc);
            this.Controls.Add(this.txtPntyDescr);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtRate);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.cboRole);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.cboParticulars);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.cboHatch);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.tLabel1);
            this.Name = "HVO111";
            this.Text = "V/S - Stevedore Penalty";
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TCombobox cboHatch;
        internal Framework.Controls.UserControls.TLabel Label2;
        internal Framework.Controls.UserControls.TTextBox txtTotal;
        internal Framework.Controls.UserControls.TTextBox txtRate;
        internal Framework.Controls.UserControls.TLabel Label8;
        internal Framework.Controls.UserControls.TCombobox cboRole;
        internal Framework.Controls.UserControls.TLabel Label6;
        internal Framework.Controls.UserControls.TLabel Label5;
        internal Framework.Controls.UserControls.TCombobox cboParticulars;
        internal Framework.Controls.UserControls.TLabel Label4;
        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TTextBox txtPntyDescr;
        internal Framework.Controls.UserControls.TTextBox txtUnitPrc;
        internal Framework.Controls.UserControls.TButton btnDelete;
        internal Framework.Controls.UserControls.TButton btnUpdate;
        internal Framework.Controls.UserControls.TButton btnAdd;
        private Framework.Controls.UserControls.TGrid grdData;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        internal Framework.Controls.UserControls.TLabel Label7;
        private Framework.Controls.UserControls.TDateTimePicker txtDate;
        internal Framework.Controls.UserControls.TLabel Label1;
        internal Framework.Controls.UserControls.TCombobox cboContractor;
        private Framework.Controls.UserControls.TDateTimePicker txtEndTime;
        private Framework.Controls.UserControls.TDateTimePicker txtStartTime;
        internal Framework.Controls.UserControls.TLabel tLabel5;
    }
}