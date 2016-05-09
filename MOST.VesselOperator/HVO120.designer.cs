namespace MOST.VesselOperator
{
    partial class HVO120
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
            this.cboCgTp = new Framework.Controls.UserControls.TCombobox();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.txtEndTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtStartTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.tLabel5 = new Framework.Controls.UserControls.TLabel();
            this.btnDelete = new Framework.Controls.UserControls.TButton();
            this.btnUpdate = new Framework.Controls.UserControls.TButton();
            this.btnAdd = new Framework.Controls.UserControls.TButton();
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.chkAcceptDelay = new System.Windows.Forms.CheckBox();
            this.txtRemark = new Framework.Controls.UserControls.TTextBox();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
            // 
            // btnF1
            // 
            this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF1.Location = new System.Drawing.Point(79, 22);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(35, 20);
            this.btnF1.TabIndex = 6;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtDelayCode
            // 
            this.txtDelayCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtDelayCode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDelayCode.isBusinessItemName = "Delay Code";
            this.txtDelayCode.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtDelayCode.isMandatory = true;
            this.txtDelayCode.Location = new System.Drawing.Point(30, 23);
            this.txtDelayCode.Name = "txtDelayCode";
            this.txtDelayCode.Size = new System.Drawing.Size(47, 19);
            this.txtDelayCode.TabIndex = 5;
            this.txtDelayCode.LostFocus += new System.EventHandler(this.txtDelayCode_LostFocus);
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label1.Location = new System.Drawing.Point(0, 25);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(31, 16);
            this.Label1.Text = "Delay";
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnOk.Location = new System.Drawing.Point(95, 243);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 28;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(167, 243);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtJPVC
            // 
            this.txtJPVC.Enabled = false;
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isBusinessItemName = "JPVC";
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.isMandatory = true;
            this.txtJPVC.Location = new System.Drawing.Point(30, 2);
            this.txtJPVC.Multiline = true;
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.ReadOnly = true;
            this.txtJPVC.Size = new System.Drawing.Size(106, 17);
            this.txtJPVC.TabIndex = 58;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(0, 4);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(28, 16);
            this.tLabel1.Text = "JPVC";
            // 
            // cboCgTp
            // 
            this.cboCgTp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboCgTp.isBusinessItemName = "Cargo Type";
            this.cboCgTp.isMandatory = true;
            this.cboCgTp.Location = new System.Drawing.Point(168, 2);
            this.cboCgTp.Name = "cboCgTp";
            this.cboCgTp.Size = new System.Drawing.Size(66, 19);
            this.cboCgTp.TabIndex = 1;
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(139, 4);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(29, 16);
            this.tLabel2.Text = "CgTp";
            // 
            // txtEndTime
            // 
            this.txtEndTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtEndTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtEndTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtEndTime.isBusinessItemName = "End Time";
            this.txtEndTime.isMandatory = true;
            this.txtEndTime.Location = new System.Drawing.Point(124, 67);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(114, 20);
            this.txtEndTime.TabIndex = 14;
            this.txtEndTime.Value = new System.DateTime(2008, 10, 23, 18, 35, 0, 890);
            // 
            // txtStartTime
            // 
            this.txtStartTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtStartTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtStartTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtStartTime.isBusinessItemName = "Start Time";
            this.txtStartTime.isMandatory = true;
            this.txtStartTime.Location = new System.Drawing.Point(0, 67);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(114, 20);
            this.txtStartTime.TabIndex = 12;
            this.txtStartTime.Value = new System.DateTime(2008, 10, 23, 18, 35, 0, 203);
            // 
            // tLabel5
            // 
            this.tLabel5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel5.Location = new System.Drawing.Point(114, 69);
            this.tLabel5.Name = "tLabel5";
            this.tLabel5.Size = new System.Drawing.Size(9, 16);
            this.tLabel5.Text = "~";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(145, 92);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(65, 20);
            this.btnDelete.TabIndex = 20;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(74, 92);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(65, 20);
            this.btnUpdate.TabIndex = 19;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 92);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(65, 20);
            this.btnAdd.TabIndex = 18;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.ActionListener);
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(3, 115);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(232, 125);
            this.grdData.TabIndex = 24;
            this.grdData.CurrentCellChanged += new System.EventHandler(this.grdData_CurrentCellChanged);
            // 
            // chkAcceptDelay
            // 
            this.chkAcceptDelay.Enabled = false;
            this.chkAcceptDelay.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.chkAcceptDelay.Location = new System.Drawing.Point(132, 27);
            this.chkAcceptDelay.Name = "chkAcceptDelay";
            this.chkAcceptDelay.Size = new System.Drawing.Size(100, 15);
            this.chkAcceptDelay.TabIndex = 7;
            this.chkAcceptDelay.Text = "Accepted delay";
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtRemark.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtRemark.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtRemark.Location = new System.Drawing.Point(39, 45);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(197, 19);
            this.txtRemark.TabIndex = 10;
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(0, 47);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(41, 16);
            this.tLabel3.Text = "Remark";
            // 
            // HVO120
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.tLabel3);
            this.Controls.Add(this.chkAcceptDelay);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.txtEndTime);
            this.Controls.Add(this.txtStartTime);
            this.Controls.Add(this.cboCgTp);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.txtDelayCode);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.tLabel5);
            this.Name = "HVO120";
            this.Text = "V/S - Delay Liquid";
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
        internal Framework.Controls.UserControls.TCombobox cboCgTp;
        internal Framework.Controls.UserControls.TLabel tLabel2;
        private Framework.Controls.UserControls.TDateTimePicker txtEndTime;
        private Framework.Controls.UserControls.TDateTimePicker txtStartTime;
        internal Framework.Controls.UserControls.TLabel tLabel5;
        internal Framework.Controls.UserControls.TButton btnDelete;
        internal Framework.Controls.UserControls.TButton btnUpdate;
        internal Framework.Controls.UserControls.TButton btnAdd;
        private Framework.Controls.UserControls.TGrid grdData;
        internal System.Windows.Forms.CheckBox chkAcceptDelay;
        internal Framework.Controls.UserControls.TTextBox txtRemark;
        internal Framework.Controls.UserControls.TLabel tLabel3;
    }
}