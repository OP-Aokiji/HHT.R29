namespace MOST.WHChecker
{
    partial class HWC101001
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
            this.Label7 = new Framework.Controls.UserControls.TLabel();
            this.Label6 = new Framework.Controls.UserControls.TLabel();
            this.cboWHNO = new Framework.Controls.UserControls.TCombobox();
            this.cboRow = new Framework.Controls.UserControls.TCombobox();
            this.cboBay = new Framework.Controls.UserControls.TCombobox();
            this.txtActQty = new Framework.Controls.UserControls.TTextBox();
            this.txtActM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtActMT = new Framework.Controls.UserControls.TTextBox();
            this.Label2 = new Framework.Controls.UserControls.TLabel();
            this.btnConfirm = new Framework.Controls.UserControls.TButton();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnDelete = new Framework.Controls.UserControls.TButton();
            this.txtBalQty = new Framework.Controls.UserControls.TTextBox();
            this.txtBalM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtBalMT = new Framework.Controls.UserControls.TTextBox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.btnUpdate = new Framework.Controls.UserControls.TButton();
            this.btnAdd = new Framework.Controls.UserControls.TButton();
            this.txtPlannedLoc = new Framework.Controls.UserControls.TTextBox();
            this.lblPlannedLoc = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
            // 
            // Label7
            // 
            this.Label7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label7.Location = new System.Drawing.Point(4, 26);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(41, 17);
            this.Label7.Text = "Cell Loc";
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label6.Location = new System.Drawing.Point(4, 6);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(42, 13);
            this.Label6.Text = "W/H No";
            // 
            // cboWHNO
            // 
            this.cboWHNO.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboWHNO.isBusinessItemName = "W/H No";
            this.cboWHNO.isMandatory = true;
            this.cboWHNO.Location = new System.Drawing.Point(46, 2);
            this.cboWHNO.Name = "cboWHNO";
            this.cboWHNO.Size = new System.Drawing.Size(184, 19);
            this.cboWHNO.TabIndex = 42;
            this.cboWHNO.SelectedIndexChanged += new System.EventHandler(this.cboWHNO_SelectedIndexChanged);
            // 
            // cboRow
            // 
            this.cboRow.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboRow.isBusinessItemName = "Bay";
            this.cboRow.isMandatory = true;
            this.cboRow.Location = new System.Drawing.Point(46, 24);
            this.cboRow.Name = "cboRow";
            this.cboRow.Size = new System.Drawing.Size(90, 19);
            this.cboRow.TabIndex = 39;
            // 
            // cboBay
            // 
            this.cboBay.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboBay.isBusinessItemName = "Row";
            this.cboBay.isMandatory = true;
            this.cboBay.Location = new System.Drawing.Point(140, 24);
            this.cboBay.Name = "cboBay";
            this.cboBay.Size = new System.Drawing.Size(90, 19);
            this.cboBay.TabIndex = 38;
            // 
            // txtActQty
            // 
            this.txtActQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtActQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtActQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtActQty.Location = new System.Drawing.Point(170, 84);
            this.txtActQty.Multiline = true;
            this.txtActQty.Name = "txtActQty";
            this.txtActQty.Size = new System.Drawing.Size(60, 17);
            this.txtActQty.TabIndex = 37;
            this.txtActQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtActM3
            // 
            this.txtActM3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtActM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtActM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtActM3.Location = new System.Drawing.Point(108, 83);
            this.txtActM3.Multiline = true;
            this.txtActM3.Name = "txtActM3";
            this.txtActM3.Size = new System.Drawing.Size(60, 17);
            this.txtActM3.TabIndex = 36;
            this.txtActM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtActMT
            // 
            this.txtActMT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtActMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtActMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtActMT.Location = new System.Drawing.Point(46, 83);
            this.txtActMT.Multiline = true;
            this.txtActMT.Name = "txtActMT";
            this.txtActMT.Size = new System.Drawing.Size(60, 17);
            this.txtActMT.TabIndex = 35;
            this.txtActMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label2.Location = new System.Drawing.Point(4, 66);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(42, 15);
            this.Label2.Text = "Balance";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnConfirm.Location = new System.Drawing.Point(96, 243);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(65, 24);
            this.btnConfirm.TabIndex = 30;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(168, 243);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(137, 104);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 20);
            this.btnDelete.TabIndex = 50;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtBalQty
            // 
            this.txtBalQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBalQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtBalQty.Location = new System.Drawing.Point(170, 65);
            this.txtBalQty.Multiline = true;
            this.txtBalQty.Name = "txtBalQty";
            this.txtBalQty.ReadOnly = true;
            this.txtBalQty.Size = new System.Drawing.Size(60, 17);
            this.txtBalQty.TabIndex = 60;
            this.txtBalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBalM3
            // 
            this.txtBalM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBalM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtBalM3.Location = new System.Drawing.Point(108, 65);
            this.txtBalM3.Multiline = true;
            this.txtBalM3.Name = "txtBalM3";
            this.txtBalM3.ReadOnly = true;
            this.txtBalM3.Size = new System.Drawing.Size(60, 17);
            this.txtBalM3.TabIndex = 59;
            this.txtBalM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBalMT
            // 
            this.txtBalMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBalMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtBalMT.Location = new System.Drawing.Point(46, 65);
            this.txtBalMT.Multiline = true;
            this.txtBalMT.Name = "txtBalMT";
            this.txtBalMT.ReadOnly = true;
            this.txtBalMT.Size = new System.Drawing.Size(60, 17);
            this.txtBalMT.TabIndex = 58;
            this.txtBalMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(4, 85);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(42, 15);
            this.tLabel1.Text = "Actual";
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(5, 127);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(229, 113);
            this.grdData.TabIndex = 62;
            this.grdData.CurrentCellChanged += new System.EventHandler(this.grdData_CurrentCellChanged);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(71, 104);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(60, 20);
            this.btnUpdate.TabIndex = 67;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(5, 104);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 20);
            this.btnAdd.TabIndex = 72;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtPlannedLoc
            // 
            this.txtPlannedLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtPlannedLoc.Location = new System.Drawing.Point(61, 47);
            this.txtPlannedLoc.Multiline = true;
            this.txtPlannedLoc.Name = "txtPlannedLoc";
            this.txtPlannedLoc.ReadOnly = true;
            this.txtPlannedLoc.Size = new System.Drawing.Size(169, 17);
            this.txtPlannedLoc.TabIndex = 78;
            // 
            // lblPlannedLoc
            // 
            this.lblPlannedLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblPlannedLoc.Location = new System.Drawing.Point(0, 49);
            this.lblPlannedLoc.Name = "lblPlannedLoc";
            this.lblPlannedLoc.Size = new System.Drawing.Size(65, 15);
            this.lblPlannedLoc.Text = "Planned Loc";
            // 
            // HWC101001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.txtPlannedLoc);
            this.Controls.Add(this.lblPlannedLoc);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.txtBalQty);
            this.Controls.Add(this.txtBalM3);
            this.Controls.Add(this.txtBalMT);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.cboWHNO);
            this.Controls.Add(this.cboRow);
            this.Controls.Add(this.cboBay);
            this.Controls.Add(this.txtActQty);
            this.Controls.Add(this.txtActM3);
            this.Controls.Add(this.txtActMT);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnCancel);
            this.Name = "HWC101001";
            this.Text = "W/C - Set Location";
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TLabel Label7;
        internal Framework.Controls.UserControls.TLabel Label6;
        internal Framework.Controls.UserControls.TCombobox cboWHNO;
        internal Framework.Controls.UserControls.TCombobox cboRow;
        internal Framework.Controls.UserControls.TCombobox cboBay;
        internal Framework.Controls.UserControls.TTextBox txtActQty;
        internal Framework.Controls.UserControls.TTextBox txtActM3;
        internal Framework.Controls.UserControls.TTextBox txtActMT;
        internal Framework.Controls.UserControls.TLabel Label2;
        internal Framework.Controls.UserControls.TButton btnConfirm;
        internal Framework.Controls.UserControls.TButton btnCancel;
        private Framework.Controls.UserControls.TButton btnDelete;
        internal Framework.Controls.UserControls.TTextBox txtBalQty;
        internal Framework.Controls.UserControls.TTextBox txtBalM3;
        internal Framework.Controls.UserControls.TTextBox txtBalMT;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        private Framework.Controls.UserControls.TGrid grdData;
        private Framework.Controls.UserControls.TButton btnUpdate;
        internal Framework.Controls.UserControls.TButton btnAdd;
        internal Framework.Controls.UserControls.TTextBox txtPlannedLoc;
        internal Framework.Controls.UserControls.TLabel lblPlannedLoc;
    }
}

