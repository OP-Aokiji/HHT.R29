namespace MOST.WHChecker
{
    partial class HWC109
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
            this.txtBalMT = new Framework.Controls.UserControls.TTextBox();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.txtCargoNo = new Framework.Controls.UserControls.TTextBox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.txtTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.btnDelete = new Framework.Controls.UserControls.TButton();
            this.btnUpdate = new Framework.Controls.UserControls.TButton();
            this.btnAdd = new Framework.Controls.UserControls.TButton();
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.txtBalM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtBalQty = new Framework.Controls.UserControls.TTextBox();
            this.cboFmLoc = new Framework.Controls.UserControls.TCombobox();
            this.tLabel6 = new Framework.Controls.UserControls.TLabel();
            this.cboToRow = new Framework.Controls.UserControls.TCombobox();
            this.tLabel5 = new Framework.Controls.UserControls.TLabel();
            this.cboToBay = new Framework.Controls.UserControls.TCombobox();
            this.tLabel4 = new Framework.Controls.UserControls.TLabel();
            this.txtToQty = new Framework.Controls.UserControls.TTextBox();
            this.txtToM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtToMT = new Framework.Controls.UserControls.TTextBox();
            this.tLabel10 = new Framework.Controls.UserControls.TLabel();
            this.cboToWH = new Framework.Controls.UserControls.TCombobox();
            this.tLabel11 = new Framework.Controls.UserControls.TLabel();
            this.tLabel9 = new Framework.Controls.UserControls.TLabel();
            this.txtFmWhTp = new Framework.Controls.UserControls.TTextBox();
            this.txtPlannedLoc = new Framework.Controls.UserControls.TTextBox();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
            // 
            // txtBalMT
            // 
            this.txtBalMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBalMT.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtBalMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtBalMT.Location = new System.Drawing.Point(29, 44);
            this.txtBalMT.Multiline = true;
            this.txtBalMT.Name = "txtBalMT";
            this.txtBalMT.ReadOnly = true;
            this.txtBalMT.Size = new System.Drawing.Size(49, 17);
            this.txtBalMT.TabIndex = 5;
            this.txtBalMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnOk.Location = new System.Drawing.Point(95, 244);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(167, 244);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtCargoNo
            // 
            this.txtCargoNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtCargoNo.isBusinessItemName = "Cg No";
            this.txtCargoNo.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtCargoNo.isMandatory = true;
            this.txtCargoNo.isSmallestInputLength = 5;
            this.txtCargoNo.Location = new System.Drawing.Point(28, 2);
            this.txtCargoNo.Multiline = true;
            this.txtCargoNo.Name = "txtCargoNo";
            this.txtCargoNo.ReadOnly = true;
            this.txtCargoNo.Size = new System.Drawing.Size(93, 17);
            this.txtCargoNo.TabIndex = 1;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(0, 3);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(30, 16);
            this.tLabel1.Text = "CgNo";
            // 
            // txtTime
            // 
            this.txtTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtTime.isBusinessItemName = "MV Date/Time";
            this.txtTime.isMandatory = true;
            this.txtTime.Location = new System.Drawing.Point(122, 21);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(115, 20);
            this.txtTime.TabIndex = 2;
            this.txtTime.Value = new System.DateTime(2008, 10, 23, 18, 35, 0, 203);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(145, 126);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(65, 20);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(74, 126);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(65, 20);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 126);
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
            this.grdData.Location = new System.Drawing.Point(2, 148);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(234, 94);
            this.grdData.TabIndex = 15;
            this.grdData.CurrentCellChanged += new System.EventHandler(this.grdData_CurrentCellChanged);
            // 
            // txtBalM3
            // 
            this.txtBalM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBalM3.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtBalM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtBalM3.Location = new System.Drawing.Point(79, 44);
            this.txtBalM3.Multiline = true;
            this.txtBalM3.Name = "txtBalM3";
            this.txtBalM3.ReadOnly = true;
            this.txtBalM3.Size = new System.Drawing.Size(49, 17);
            this.txtBalM3.TabIndex = 7;
            this.txtBalM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBalQty
            // 
            this.txtBalQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBalQty.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtBalQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtBalQty.Location = new System.Drawing.Point(129, 44);
            this.txtBalQty.Multiline = true;
            this.txtBalQty.Name = "txtBalQty";
            this.txtBalQty.ReadOnly = true;
            this.txtBalQty.Size = new System.Drawing.Size(49, 17);
            this.txtBalQty.TabIndex = 9;
            this.txtBalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cboFmLoc
            // 
            this.cboFmLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboFmLoc.isBusinessItemName = "From Cell";
            this.cboFmLoc.isMandatory = true;
            this.cboFmLoc.Location = new System.Drawing.Point(28, 21);
            this.cboFmLoc.Name = "cboFmLoc";
            this.cboFmLoc.Size = new System.Drawing.Size(90, 19);
            this.cboFmLoc.TabIndex = 1;
            this.cboFmLoc.SelectedIndexChanged += new System.EventHandler(this.OnSelectedIndexChanged);
            // 
            // tLabel6
            // 
            this.tLabel6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel6.Location = new System.Drawing.Point(0, 24);
            this.tLabel6.Name = "tLabel6";
            this.tLabel6.Size = new System.Drawing.Size(30, 16);
            this.tLabel6.Text = "From";
            // 
            // cboToRow
            // 
            this.cboToRow.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboToRow.isBusinessItemName = "To Cell (Row)";
            this.cboToRow.isMandatory = true;
            this.cboToRow.Location = new System.Drawing.Point(137, 81);
            this.cboToRow.Name = "cboToRow";
            this.cboToRow.Size = new System.Drawing.Size(51, 19);
            this.cboToRow.TabIndex = 3;
            // 
            // tLabel5
            // 
            this.tLabel5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel5.Location = new System.Drawing.Point(0, 86);
            this.tLabel5.Name = "tLabel5";
            this.tLabel5.Size = new System.Drawing.Size(44, 16);
            this.tLabel5.Text = "To(WH)";
            // 
            // cboToBay
            // 
            this.cboToBay.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboToBay.isBusinessItemName = "To Cell (Bay)";
            this.cboToBay.isMandatory = true;
            this.cboToBay.Location = new System.Drawing.Point(190, 81);
            this.cboToBay.Name = "cboToBay";
            this.cboToBay.Size = new System.Drawing.Size(47, 19);
            this.cboToBay.TabIndex = 4;
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(4, 46);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(22, 16);
            this.tLabel4.Text = "Bal";
            // 
            // txtToQty
            // 
            this.txtToQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtToQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtToQty.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtToQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtToQty.Location = new System.Drawing.Point(164, 104);
            this.txtToQty.Multiline = true;
            this.txtToQty.Name = "txtToQty";
            this.txtToQty.Size = new System.Drawing.Size(55, 17);
            this.txtToQty.TabIndex = 7;
            this.txtToQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtToM3
            // 
            this.txtToM3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtToM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtToM3.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtToM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtToM3.Location = new System.Drawing.Point(103, 104);
            this.txtToM3.Multiline = true;
            this.txtToM3.Name = "txtToM3";
            this.txtToM3.Size = new System.Drawing.Size(55, 17);
            this.txtToM3.TabIndex = 6;
            this.txtToM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtToMT
            // 
            this.txtToMT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtToMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtToMT.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtToMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtToMT.Location = new System.Drawing.Point(41, 104);
            this.txtToMT.Multiline = true;
            this.txtToMT.Name = "txtToMT";
            this.txtToMT.Size = new System.Drawing.Size(55, 17);
            this.txtToMT.TabIndex = 5;
            this.txtToMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel10
            // 
            this.tLabel10.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel10.Location = new System.Drawing.Point(136, 7);
            this.tLabel10.Name = "tLabel10";
            this.tLabel10.Size = new System.Drawing.Size(76, 14);
            this.tLabel10.Text = "MV Date/Time";
            // 
            // cboToWH
            // 
            this.cboToWH.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboToWH.isBusinessItemName = "To Cell (WH)";
            this.cboToWH.isMandatory = true;
            this.cboToWH.Location = new System.Drawing.Point(41, 81);
            this.cboToWH.Name = "cboToWH";
            this.cboToWH.Size = new System.Drawing.Size(73, 19);
            this.cboToWH.TabIndex = 191;
            this.cboToWH.SelectedIndexChanged += new System.EventHandler(this.OnSelectedIndexChanged);
            // 
            // tLabel11
            // 
            this.tLabel11.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel11.Location = new System.Drawing.Point(116, 84);
            this.tLabel11.Name = "tLabel11";
            this.tLabel11.Size = new System.Drawing.Size(23, 16);
            this.tLabel11.Text = "Cell";
            // 
            // tLabel9
            // 
            this.tLabel9.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel9.Location = new System.Drawing.Point(0, 107);
            this.tLabel9.Name = "tLabel9";
            this.tLabel9.Size = new System.Drawing.Size(42, 16);
            this.tLabel9.Text = "MV Amt";
            // 
            // txtFmWhTp
            // 
            this.txtFmWhTp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtFmWhTp.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtFmWhTp.Location = new System.Drawing.Point(181, 44);
            this.txtFmWhTp.Multiline = true;
            this.txtFmWhTp.Name = "txtFmWhTp";
            this.txtFmWhTp.ReadOnly = true;
            this.txtFmWhTp.Size = new System.Drawing.Size(56, 17);
            this.txtFmWhTp.TabIndex = 199;
            // 
            // txtPlannedLoc
            // 
            this.txtPlannedLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtPlannedLoc.isBusinessItemName = "Plan Location";
            this.txtPlannedLoc.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtPlannedLoc.Location = new System.Drawing.Point(41, 62);
            this.txtPlannedLoc.Multiline = true;
            this.txtPlannedLoc.Name = "txtPlannedLoc";
            this.txtPlannedLoc.ReadOnly = true;
            this.txtPlannedLoc.Size = new System.Drawing.Size(196, 17);
            this.txtPlannedLoc.TabIndex = 208;
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(0, 63);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(44, 16);
            this.tLabel2.Text = "Plan Loc";
            // 
            // HWC109
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.txtPlannedLoc);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.txtFmWhTp);
            this.Controls.Add(this.cboToWH);
            this.Controls.Add(this.txtToQty);
            this.Controls.Add(this.txtToM3);
            this.Controls.Add(this.txtToMT);
            this.Controls.Add(this.cboToBay);
            this.Controls.Add(this.cboToRow);
            this.Controls.Add(this.tLabel5);
            this.Controls.Add(this.cboFmLoc);
            this.Controls.Add(this.txtBalQty);
            this.Controls.Add(this.txtBalM3);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.txtCargoNo);
            this.Controls.Add(this.txtBalMT);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tLabel4);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.tLabel6);
            this.Controls.Add(this.tLabel10);
            this.Controls.Add(this.tLabel11);
            this.Controls.Add(this.tLabel9);
            this.Name = "HWC109";
            this.Text = "W/C - Cargo Movement";
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TTextBox txtBalMT;
        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TTextBox txtCargoNo;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        private Framework.Controls.UserControls.TDateTimePicker txtTime;
        internal Framework.Controls.UserControls.TButton btnDelete;
        internal Framework.Controls.UserControls.TButton btnUpdate;
        internal Framework.Controls.UserControls.TButton btnAdd;
        private Framework.Controls.UserControls.TGrid grdData;
        internal Framework.Controls.UserControls.TTextBox txtBalM3;
        internal Framework.Controls.UserControls.TTextBox txtBalQty;
        internal Framework.Controls.UserControls.TCombobox cboFmLoc;
        internal Framework.Controls.UserControls.TLabel tLabel6;
        internal Framework.Controls.UserControls.TCombobox cboToRow;
        internal Framework.Controls.UserControls.TLabel tLabel5;
        internal Framework.Controls.UserControls.TCombobox cboToBay;
        internal Framework.Controls.UserControls.TLabel tLabel4;
        internal Framework.Controls.UserControls.TTextBox txtToQty;
        internal Framework.Controls.UserControls.TTextBox txtToM3;
        internal Framework.Controls.UserControls.TTextBox txtToMT;
        internal Framework.Controls.UserControls.TLabel tLabel10;
        internal Framework.Controls.UserControls.TCombobox cboToWH;
        internal Framework.Controls.UserControls.TLabel tLabel11;
        internal Framework.Controls.UserControls.TLabel tLabel9;
        internal Framework.Controls.UserControls.TTextBox txtFmWhTp;
        internal Framework.Controls.UserControls.TTextBox txtPlannedLoc;
        internal Framework.Controls.UserControls.TLabel tLabel2;
    }
}