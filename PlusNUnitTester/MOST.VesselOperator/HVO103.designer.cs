namespace MOST.VesselOperator
{
    partial class HVO103
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
            this.Label6 = new Framework.Controls.UserControls.TLabel();
            this.cboAPFP = new Framework.Controls.UserControls.TCombobox();
            this.Label3 = new Framework.Controls.UserControls.TLabel();
            this.Label2 = new Framework.Controls.UserControls.TLabel();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.cboHatch = new Framework.Controls.UserControls.TCombobox();
            this.txtEndTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtStartTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.btnAdd = new Framework.Controls.UserControls.TButton();
            this.btnUpdate = new Framework.Controls.UserControls.TButton();
            this.btnDelete = new Framework.Controls.UserControls.TButton();
            this.rbtnDry = new Framework.Controls.UserControls.TRadioButton();
            this.rbtnBreak = new Framework.Controls.UserControls.TRadioButton();
            this.cboTopClean = new Framework.Controls.UserControls.TCombobox();
            this.SuspendLayout();
            // 
            // txtJPVC
            // 
            this.txtJPVC.Enabled = false;
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isBusinessItemName = "JPVC";
            this.txtJPVC.isMandatory = true;
            this.txtJPVC.Location = new System.Drawing.Point(39, 3);
            this.txtJPVC.Multiline = true;
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(126, 17);
            this.txtJPVC.TabIndex = 1;
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label6.Location = new System.Drawing.Point(3, 6);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(33, 16);
            this.Label6.Text = "JPVC";
            // 
            // cboAPFP
            // 
            this.cboAPFP.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboAPFP.isBusinessItemName = "AP/FP";
            this.cboAPFP.isMandatory = false;
            this.cboAPFP.Location = new System.Drawing.Point(84, 45);
            this.cboAPFP.Name = "cboAPFP";
            this.cboAPFP.Size = new System.Drawing.Size(52, 19);
            this.cboAPFP.TabIndex = 5;
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label3.Location = new System.Drawing.Point(136, 48);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(53, 16);
            this.Label3.Text = "Top/Clean";
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label2.Location = new System.Drawing.Point(0, 48);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(36, 16);
            this.Label2.Text = "Hatch";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(95, 242);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 24;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 242);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // cboHatch
            // 
            this.cboHatch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboHatch.isBusinessItemName = "Hatch";
            this.cboHatch.isMandatory = true;
            this.cboHatch.Location = new System.Drawing.Point(30, 45);
            this.cboHatch.Name = "cboHatch";
            this.cboHatch.Size = new System.Drawing.Size(52, 19);
            this.cboHatch.TabIndex = 3;
            // 
            // txtEndTime
            // 
            this.txtEndTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtEndTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtEndTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtEndTime.isBusinessItemName = "";
            this.txtEndTime.isMandatory = false;
            this.txtEndTime.Location = new System.Drawing.Point(124, 68);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(114, 20);
            this.txtEndTime.TabIndex = 9;
            this.txtEndTime.Value = new System.DateTime(2008, 10, 24, 16, 17, 8, 828);
            // 
            // txtStartTime
            // 
            this.txtStartTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtStartTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtStartTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtStartTime.isBusinessItemName = "StartTime";
            this.txtStartTime.isMandatory = true;
            this.txtStartTime.Location = new System.Drawing.Point(0, 68);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(114, 20);
            this.txtStartTime.TabIndex = 7;
            this.txtStartTime.Value = new System.DateTime(2008, 10, 24, 16, 17, 9, 437);
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(114, 70);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(9, 16);
            this.tLabel1.Text = "~";
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(3, 121);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(232, 117);
            this.grdData.TabIndex = 20;
            this.grdData.CurrentCellChanged += new System.EventHandler(this.grdData_CurrentCellChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 97);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(65, 20);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(74, 97);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(65, 20);
            this.btnUpdate.TabIndex = 14;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(145, 97);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(65, 20);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.ActionListener);
            // 
            // rbtnDry
            // 
            this.rbtnDry.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnDry.isBusinessItemName = "";
            this.rbtnDry.isMandatory = false;
            this.rbtnDry.Location = new System.Drawing.Point(85, 26);
            this.rbtnDry.Name = "rbtnDry";
            this.rbtnDry.Size = new System.Drawing.Size(76, 13);
            this.rbtnDry.TabIndex = 31;
            this.rbtnDry.Text = "Dry Bulk";
            this.rbtnDry.CheckedChanged += new System.EventHandler(this.RadiobuttonAction);
            // 
            // rbtnBreak
            // 
            this.rbtnBreak.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnBreak.isBusinessItemName = "";
            this.rbtnBreak.isMandatory = false;
            this.rbtnBreak.Location = new System.Drawing.Point(3, 26);
            this.rbtnBreak.Name = "rbtnBreak";
            this.rbtnBreak.Size = new System.Drawing.Size(76, 13);
            this.rbtnBreak.TabIndex = 30;
            this.rbtnBreak.Text = "Break Bulk";
            this.rbtnBreak.CheckedChanged += new System.EventHandler(this.RadiobuttonAction);
            // 
            // cboTopClean
            // 
            this.cboTopClean.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboTopClean.isBusinessItemName = "AP/FP";
            this.cboTopClean.isMandatory = false;
            this.cboTopClean.Location = new System.Drawing.Point(186, 45);
            this.cboTopClean.Name = "cboTopClean";
            this.cboTopClean.Size = new System.Drawing.Size(51, 19);
            this.cboTopClean.TabIndex = 36;
            // 
            // HVO103
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.cboTopClean);
            this.Controls.Add(this.rbtnDry);
            this.Controls.Add(this.rbtnBreak);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.txtEndTime);
            this.Controls.Add(this.txtStartTime);
            this.Controls.Add(this.cboHatch);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cboAPFP);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.tLabel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.Name = "HVO103";
            this.Text = "V/S - Hatch Operation";
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TTextBox txtJPVC;
        internal Framework.Controls.UserControls.TLabel Label6;
        internal Framework.Controls.UserControls.TCombobox cboAPFP;
        internal Framework.Controls.UserControls.TLabel Label3;
        internal Framework.Controls.UserControls.TLabel Label2;
        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TCombobox cboHatch;
        private Framework.Controls.UserControls.TDateTimePicker txtEndTime;
        private Framework.Controls.UserControls.TDateTimePicker txtStartTime;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        private Framework.Controls.UserControls.TGrid grdData;
        internal Framework.Controls.UserControls.TButton btnAdd;
        internal Framework.Controls.UserControls.TButton btnUpdate;
        internal Framework.Controls.UserControls.TButton btnDelete;
        internal Framework.Controls.UserControls.TRadioButton rbtnDry;
        internal Framework.Controls.UserControls.TRadioButton rbtnBreak;
        internal Framework.Controls.UserControls.TCombobox cboTopClean;

    }
}