namespace MOST.VesselOperator
{
    partial class HVO107
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
            this.txtDelayCode = new Framework.Controls.UserControls.TTextBox();
            this.Label1 = new Framework.Controls.UserControls.TLabel();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.cboHatch = new Framework.Controls.UserControls.TCombobox();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.tLabel4 = new Framework.Controls.UserControls.TLabel();
            this.cboEqu = new Framework.Controls.UserControls.TCombobox();
            this.txtEndTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtStartTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.btnDelete = new Framework.Controls.UserControls.TButton();
            this.btnUpdate = new Framework.Controls.UserControls.TButton();
            this.btnAdd = new Framework.Controls.UserControls.TButton();
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.chkAcceptDelay = new System.Windows.Forms.CheckBox();
            this.cboAPFP = new Framework.Controls.UserControls.TCombobox();
            this.txtRemark = new Framework.Controls.UserControls.TTextBox();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
            this.txtContractor = new Framework.Controls.UserControls.TTextBox();
            this.tlblContractor = new Framework.Controls.UserControls.TLabel();
            this.btnF2 = new Framework.Controls.UserControls.TButton();
            this.tLabel5 = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
            // 
            // btnF1
            // 
            this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF1.Location = new System.Drawing.Point(100, 40);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(35, 20);
            this.btnF1.TabIndex = 6;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtDelayCode
            // 
            this.txtDelayCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDelayCode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDelayCode.isBusinessItemName = "Delay Code";
            this.txtDelayCode.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtDelayCode.isMandatory = true;
            this.txtDelayCode.Location = new System.Drawing.Point(58, 41);
            this.txtDelayCode.Name = "txtDelayCode";
            this.txtDelayCode.Size = new System.Drawing.Size(40, 19);
            this.txtDelayCode.TabIndex = 5;
            this.txtDelayCode.LostFocus += new System.EventHandler(this.txtDelayCode_LostFocus);
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label1.Location = new System.Drawing.Point(0, 43);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(57, 16);
            this.Label1.Text = "Delay code";
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnOk.Location = new System.Drawing.Point(95, 240);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 18;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(167, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtJPVC
            // 
            this.txtJPVC.Enabled = false;
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.Location = new System.Drawing.Point(31, 1);
            this.txtJPVC.Multiline = true;
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.ReadOnly = true;
            this.txtJPVC.Size = new System.Drawing.Size(201, 17);
            this.txtJPVC.TabIndex = 58;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(0, 3);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(33, 16);
            this.tLabel1.Text = "JPVC";
            // 
            // cboHatch
            // 
            this.cboHatch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboHatch.isBusinessItemName = "Hatch";
            this.cboHatch.isMandatory = false;
            this.cboHatch.Location = new System.Drawing.Point(31, 19);
            this.cboHatch.Name = "cboHatch";
            this.cboHatch.Size = new System.Drawing.Size(50, 19);
            this.cboHatch.TabIndex = 1;
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(0, 21);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(32, 16);
            this.tLabel2.Text = "Hatch";
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(135, 21);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(25, 16);
            this.tLabel4.Text = "EQU";
            // 
            // cboEqu
            // 
            this.cboEqu.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboEqu.isBusinessItemName = "";
            this.cboEqu.isMandatory = false;
            this.cboEqu.Location = new System.Drawing.Point(157, 19);
            this.cboEqu.Name = "cboEqu";
            this.cboEqu.Size = new System.Drawing.Size(75, 19);
            this.cboEqu.TabIndex = 4;
            // 
            // txtEndTime
            // 
            this.txtEndTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtEndTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtEndTime.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular);
            this.txtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtEndTime.isBusinessItemName = "End Time";
            this.txtEndTime.isMandatory = true;
            this.txtEndTime.Location = new System.Drawing.Point(122, 84);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(110, 20);
            this.txtEndTime.TabIndex = 10;
            this.txtEndTime.Value = new System.DateTime(2008, 10, 23, 18, 35, 0, 890);
            // 
            // txtStartTime
            // 
            this.txtStartTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtStartTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtStartTime.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular);
            this.txtStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtStartTime.isBusinessItemName = "Start Time";
            this.txtStartTime.isMandatory = true;
            this.txtStartTime.Location = new System.Drawing.Point(1, 84);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(110, 20);
            this.txtStartTime.TabIndex = 9;
            this.txtStartTime.Value = new System.DateTime(2008, 10, 23, 18, 35, 0, 203);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(150, 129);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(73, 20);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(79, 129);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(65, 20);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(8, 129);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(65, 20);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.ActionListener);
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(4, 151);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(228, 85);
            this.grdData.TabIndex = 16;
            this.grdData.CurrentCellChanged += new System.EventHandler(this.grdData_CurrentCellChanged);
            // 
            // chkAcceptDelay
            // 
            this.chkAcceptDelay.Enabled = false;
            this.chkAcceptDelay.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular);
            this.chkAcceptDelay.Location = new System.Drawing.Point(137, 41);
            this.chkAcceptDelay.Name = "chkAcceptDelay";
            this.chkAcceptDelay.Size = new System.Drawing.Size(95, 18);
            this.chkAcceptDelay.TabIndex = 7;
            this.chkAcceptDelay.Text = "Accepted delay";
            // 
            // cboAPFP
            // 
            this.cboAPFP.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboAPFP.isBusinessItemName = "";
            this.cboAPFP.isMandatory = false;
            this.cboAPFP.Location = new System.Drawing.Point(85, 19);
            this.cboAPFP.Name = "cboAPFP";
            this.cboAPFP.Size = new System.Drawing.Size(50, 19);
            this.cboAPFP.TabIndex = 2;
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtRemark.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtRemark.isBusinessItemName = "Delay Code";
            this.txtRemark.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtRemark.Location = new System.Drawing.Point(48, 106);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(184, 19);
            this.txtRemark.TabIndex = 64;
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(1, 109);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(41, 16);
            this.tLabel3.Text = "Remark";
            // 
            // txtContractor
            // 
            this.txtContractor.Enabled = false;
            this.txtContractor.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtContractor.isBusinessItemName = "Contractor";
            this.txtContractor.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtContractor.Location = new System.Drawing.Point(58, 62);
            this.txtContractor.Multiline = true;
            this.txtContractor.Name = "txtContractor";
            this.txtContractor.ReadOnly = true;
            this.txtContractor.Size = new System.Drawing.Size(133, 21);
            this.txtContractor.TabIndex = 72;
            // 
            // tlblContractor
            // 
            this.tlblContractor.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tlblContractor.Location = new System.Drawing.Point(1, 64);
            this.tlblContractor.Name = "tlblContractor";
            this.tlblContractor.Size = new System.Drawing.Size(77, 14);
            this.tlblContractor.Text = "Contractor";
            // 
            // btnF2
            // 
            this.btnF2.Enabled = false;
            this.btnF2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF2.Location = new System.Drawing.Point(197, 62);
            this.btnF2.Name = "btnF2";
            this.btnF2.Size = new System.Drawing.Size(35, 20);
            this.btnF2.TabIndex = 74;
            this.btnF2.Text = "F2";
            this.btnF2.Click += new System.EventHandler(this.ActionListener);
            // 
            // tLabel5
            // 
            this.tLabel5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel5.Location = new System.Drawing.Point(110, 86);
            this.tLabel5.Name = "tLabel5";
            this.tLabel5.Size = new System.Drawing.Size(17, 14);
            this.tLabel5.Text = "~";
            // 
            // HVO107
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.btnF2);
            this.Controls.Add(this.txtContractor);
            this.Controls.Add(this.tlblContractor);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.cboAPFP);
            this.Controls.Add(this.chkAcceptDelay);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.txtEndTime);
            this.Controls.Add(this.txtStartTime);
            this.Controls.Add(this.cboEqu);
            this.Controls.Add(this.cboHatch);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.txtDelayCode);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.tLabel4);
            this.Controls.Add(this.tLabel3);
            this.Controls.Add(this.tLabel5);
            this.Name = "HVO107";
            this.Text = "V/S - Vessel Delay";
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TButton btnF1;
        internal Framework.Controls.UserControls.TTextBox txtDelayCode;
        internal Framework.Controls.UserControls.TLabel Label1;
        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TTextBox txtJPVC;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        internal Framework.Controls.UserControls.TCombobox cboHatch;
        internal Framework.Controls.UserControls.TLabel tLabel2;
        internal Framework.Controls.UserControls.TLabel tLabel4;
        internal Framework.Controls.UserControls.TCombobox cboEqu;
        private Framework.Controls.UserControls.TDateTimePicker txtEndTime;
        private Framework.Controls.UserControls.TDateTimePicker txtStartTime;
        internal Framework.Controls.UserControls.TButton btnDelete;
        internal Framework.Controls.UserControls.TButton btnUpdate;
        internal Framework.Controls.UserControls.TButton btnAdd;
        private Framework.Controls.UserControls.TGrid grdData;
        internal System.Windows.Forms.CheckBox chkAcceptDelay;
        internal Framework.Controls.UserControls.TCombobox cboAPFP;
        internal Framework.Controls.UserControls.TTextBox txtRemark;
        internal Framework.Controls.UserControls.TLabel tLabel3;
        internal Framework.Controls.UserControls.TTextBox txtContractor;
        internal Framework.Controls.UserControls.TLabel tlblContractor;
        internal Framework.Controls.UserControls.TButton btnF2;
        internal Framework.Controls.UserControls.TLabel tLabel5;
    }
}