namespace MOST.WHChecker
{
    partial class HWC101002
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
            this.Label6 = new Framework.Controls.UserControls.TLabel();
            this.cboLocation = new Framework.Controls.UserControls.TCombobox();
            this.btnConfirm = new Framework.Controls.UserControls.TButton();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnDelete = new Framework.Controls.UserControls.TButton();
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.btnUpdate = new Framework.Controls.UserControls.TButton();
            this.btnAdd = new Framework.Controls.UserControls.TButton();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.txtCgNo = new Framework.Controls.UserControls.TTextBox();
            this.txtQty = new Framework.Controls.UserControls.TTextBox();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
            this.txtM3 = new Framework.Controls.UserControls.TTextBox();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.txtMT = new Framework.Controls.UserControls.TTextBox();
            this.tLabel4 = new Framework.Controls.UserControls.TLabel();
            this.txtCellMt = new Framework.Controls.UserControls.TTextBox();
            this.txtCellM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtCellQty = new Framework.Controls.UserControls.TTextBox();
            this.txtTotQty = new Framework.Controls.UserControls.TTextBox();
            this.txtTotM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtTotMt = new Framework.Controls.UserControls.TTextBox();
            this.cboSpareCg = new Framework.Controls.UserControls.TCombobox();
            this.SuspendLayout();
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label6.Location = new System.Drawing.Point(0, 29);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(21, 15);
            this.Label6.Text = "Loc";
            // 
            // cboLocation
            // 
            this.cboLocation.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboLocation.isBusinessItemName = "W/H No";
            this.cboLocation.isMandatory = true;
            this.cboLocation.Location = new System.Drawing.Point(19, 25);
            this.cboLocation.Name = "cboLocation";
            this.cboLocation.Size = new System.Drawing.Size(88, 19);
            this.cboLocation.TabIndex = 42;
            this.cboLocation.SelectedIndexChanged += new System.EventHandler(this.cboLocation_SelectedIndexChanged);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnConfirm.Location = new System.Drawing.Point(95, 242);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(65, 24);
            this.btnConfirm.TabIndex = 30;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(167, 242);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(135, 73);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 20);
            this.btnDelete.TabIndex = 50;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.ActionListener);
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(2, 96);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(234, 143);
            this.grdData.TabIndex = 62;
            this.grdData.CurrentCellChanged += new System.EventHandler(this.grdData_CurrentCellChanged);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(69, 73);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(60, 20);
            this.btnUpdate.TabIndex = 67;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 73);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 20);
            this.btnAdd.TabIndex = 72;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.ActionListener);
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(0, 5);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(21, 13);
            this.tLabel1.Text = "Cg";
            // 
            // txtCgNo
            // 
            this.txtCgNo.Enabled = false;
            this.txtCgNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtCgNo.Location = new System.Drawing.Point(19, 2);
            this.txtCgNo.Multiline = true;
            this.txtCgNo.Name = "txtCgNo";
            this.txtCgNo.ReadOnly = true;
            this.txtCgNo.Size = new System.Drawing.Size(89, 17);
            this.txtCgNo.TabIndex = 78;
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtQty.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtQty.Location = new System.Drawing.Point(177, 49);
            this.txtQty.Multiline = true;
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(55, 17);
            this.txtQty.TabIndex = 84;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(157, 51);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(23, 16);
            this.tLabel3.Text = "Qty";
            // 
            // txtM3
            // 
            this.txtM3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtM3.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtM3.Location = new System.Drawing.Point(102, 49);
            this.txtM3.Multiline = true;
            this.txtM3.Name = "txtM3";
            this.txtM3.Size = new System.Drawing.Size(55, 17);
            this.txtM3.TabIndex = 83;
            this.txtM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(86, 51);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(19, 16);
            this.tLabel2.Text = "M3";
            // 
            // txtMT
            // 
            this.txtMT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtMT.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtMT.Location = new System.Drawing.Point(29, 49);
            this.txtMT.Multiline = true;
            this.txtMT.Name = "txtMT";
            this.txtMT.Size = new System.Drawing.Size(55, 17);
            this.txtMT.TabIndex = 82;
            this.txtMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(9, 51);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(19, 16);
            this.tLabel4.Text = "MT";
            // 
            // txtCellMt
            // 
            this.txtCellMt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtCellMt.Location = new System.Drawing.Point(109, 27);
            this.txtCellMt.Multiline = true;
            this.txtCellMt.Name = "txtCellMt";
            this.txtCellMt.ReadOnly = true;
            this.txtCellMt.Size = new System.Drawing.Size(42, 17);
            this.txtCellMt.TabIndex = 90;
            // 
            // txtCellM3
            // 
            this.txtCellM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtCellM3.Location = new System.Drawing.Point(152, 27);
            this.txtCellM3.Multiline = true;
            this.txtCellM3.Name = "txtCellM3";
            this.txtCellM3.ReadOnly = true;
            this.txtCellM3.Size = new System.Drawing.Size(42, 17);
            this.txtCellM3.TabIndex = 91;
            // 
            // txtCellQty
            // 
            this.txtCellQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtCellQty.Location = new System.Drawing.Point(195, 27);
            this.txtCellQty.Multiline = true;
            this.txtCellQty.Name = "txtCellQty";
            this.txtCellQty.ReadOnly = true;
            this.txtCellQty.Size = new System.Drawing.Size(42, 17);
            this.txtCellQty.TabIndex = 92;
            // 
            // txtTotQty
            // 
            this.txtTotQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtTotQty.Location = new System.Drawing.Point(195, 2);
            this.txtTotQty.Multiline = true;
            this.txtTotQty.Name = "txtTotQty";
            this.txtTotQty.ReadOnly = true;
            this.txtTotQty.Size = new System.Drawing.Size(42, 17);
            this.txtTotQty.TabIndex = 103;
            // 
            // txtTotM3
            // 
            this.txtTotM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtTotM3.Location = new System.Drawing.Point(152, 2);
            this.txtTotM3.Multiline = true;
            this.txtTotM3.Name = "txtTotM3";
            this.txtTotM3.ReadOnly = true;
            this.txtTotM3.Size = new System.Drawing.Size(42, 17);
            this.txtTotM3.TabIndex = 102;
            // 
            // txtTotMt
            // 
            this.txtTotMt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtTotMt.Location = new System.Drawing.Point(109, 2);
            this.txtTotMt.Multiline = true;
            this.txtTotMt.Name = "txtTotMt";
            this.txtTotMt.ReadOnly = true;
            this.txtTotMt.Size = new System.Drawing.Size(42, 17);
            this.txtTotMt.TabIndex = 101;
            // 
            // cboSpareCg
            // 
            this.cboSpareCg.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboSpareCg.isBusinessItemName = "Spare Cg";
            this.cboSpareCg.isMandatory = true;
            this.cboSpareCg.Location = new System.Drawing.Point(19, 2);
            this.cboSpareCg.Name = "cboSpareCg";
            this.cboSpareCg.Size = new System.Drawing.Size(88, 19);
            this.cboSpareCg.TabIndex = 104;
            this.cboSpareCg.SelectedIndexChanged += new System.EventHandler(this.cboSpareCg_SelectedIndexChanged);
            // 
            // HWC101002
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.cboSpareCg);
            this.Controls.Add(this.txtTotQty);
            this.Controls.Add(this.txtTotM3);
            this.Controls.Add(this.txtTotMt);
            this.Controls.Add(this.txtCellQty);
            this.Controls.Add(this.txtCellM3);
            this.Controls.Add(this.txtCellMt);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.txtM3);
            this.Controls.Add(this.txtMT);
            this.Controls.Add(this.txtCgNo);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.cboLocation);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.tLabel3);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.tLabel4);
            this.Name = "HWC101002";
            this.Text = "W/C - UnSet Location";
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TLabel Label6;
        internal Framework.Controls.UserControls.TCombobox cboLocation;
        internal Framework.Controls.UserControls.TButton btnConfirm;
        internal Framework.Controls.UserControls.TButton btnCancel;
        private Framework.Controls.UserControls.TButton btnDelete;
        private Framework.Controls.UserControls.TGrid grdData;
        private Framework.Controls.UserControls.TButton btnUpdate;
        internal Framework.Controls.UserControls.TButton btnAdd;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        internal Framework.Controls.UserControls.TTextBox txtCgNo;
        internal Framework.Controls.UserControls.TTextBox txtQty;
        internal Framework.Controls.UserControls.TLabel tLabel3;
        internal Framework.Controls.UserControls.TTextBox txtM3;
        internal Framework.Controls.UserControls.TLabel tLabel2;
        internal Framework.Controls.UserControls.TTextBox txtMT;
        internal Framework.Controls.UserControls.TLabel tLabel4;
        internal Framework.Controls.UserControls.TTextBox txtCellMt;
        internal Framework.Controls.UserControls.TTextBox txtCellM3;
        internal Framework.Controls.UserControls.TTextBox txtCellQty;
        internal Framework.Controls.UserControls.TTextBox txtTotQty;
        internal Framework.Controls.UserControls.TTextBox txtTotM3;
        internal Framework.Controls.UserControls.TTextBox txtTotMt;
        internal Framework.Controls.UserControls.TCombobox cboSpareCg;
    }
}

