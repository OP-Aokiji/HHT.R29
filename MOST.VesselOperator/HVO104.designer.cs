namespace MOST.VesselOperator
{
    partial class HVO104
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
            this.cboHatch = new Framework.Controls.UserControls.TCombobox();
            this.Label3 = new Framework.Controls.UserControls.TLabel();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.cboEQU = new Framework.Controls.UserControls.TCombobox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.btnDelete = new Framework.Controls.UserControls.TButton();
            this.btnUpdate = new Framework.Controls.UserControls.TButton();
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.rbtnBreak = new Framework.Controls.UserControls.TRadioButton();
            this.rbtnDry = new Framework.Controls.UserControls.TRadioButton();
            this.cboFac = new Framework.Controls.UserControls.TCombobox();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.txtRemark = new Framework.Controls.UserControls.TTextBox();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
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
            this.txtJPVC.Size = new System.Drawing.Size(121, 17);
            this.txtJPVC.TabIndex = 1;
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label6.Location = new System.Drawing.Point(3, 6);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(33, 14);
            this.Label6.Text = "JPVC";
            // 
            // cboHatch
            // 
            this.cboHatch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboHatch.isBusinessItemName = "Hatch";
            this.cboHatch.isMandatory = false;
            this.cboHatch.Location = new System.Drawing.Point(39, 45);
            this.cboHatch.Name = "cboHatch";
            this.cboHatch.Size = new System.Drawing.Size(51, 19);
            this.cboHatch.TabIndex = 4;
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label3.Location = new System.Drawing.Point(0, 48);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(33, 16);
            this.Label3.Text = "Hatch";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(95, 243);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 20;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 243);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // cboEQU
            // 
            this.cboEQU.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboEQU.isBusinessItemName = "Equipment";
            this.cboEQU.isMandatory = false;
            this.cboEQU.Location = new System.Drawing.Point(124, 45);
            this.cboEQU.Name = "cboEQU";
            this.cboEQU.Size = new System.Drawing.Size(111, 19);
            this.cboEQU.TabIndex = 5;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(95, 48);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(29, 16);
            this.tLabel1.Text = "EQU";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(74, 115);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(65, 20);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(3, 115);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(65, 20);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.ActionListener);
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(3, 138);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(232, 102);
            this.grdData.TabIndex = 17;
            this.grdData.CurrentCellChanged += new System.EventHandler(this.grdData_CurrentCellChanged);
            // 
            // rbtnBreak
            // 
            this.rbtnBreak.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnBreak.isBusinessItemName = "";
            this.rbtnBreak.isMandatory = false;
            this.rbtnBreak.Location = new System.Drawing.Point(3, 26);
            this.rbtnBreak.Name = "rbtnBreak";
            this.rbtnBreak.Size = new System.Drawing.Size(76, 13);
            this.rbtnBreak.TabIndex = 2;
            this.rbtnBreak.Text = "Break Bulk";
            this.rbtnBreak.CheckedChanged += new System.EventHandler(this.RadioListener);
            // 
            // rbtnDry
            // 
            this.rbtnDry.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtnDry.isBusinessItemName = "";
            this.rbtnDry.isMandatory = false;
            this.rbtnDry.Location = new System.Drawing.Point(77, 26);
            this.rbtnDry.Name = "rbtnDry";
            this.rbtnDry.Size = new System.Drawing.Size(76, 13);
            this.rbtnDry.TabIndex = 3;
            this.rbtnDry.Text = "Dry Bulk";
            this.rbtnDry.CheckedChanged += new System.EventHandler(this.RadioListener);
            // 
            // cboFac
            // 
            this.cboFac.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboFac.isBusinessItemName = "Equipment";
            this.cboFac.isMandatory = false;
            this.cboFac.Location = new System.Drawing.Point(39, 69);
            this.cboFac.Name = "cboFac";
            this.cboFac.Size = new System.Drawing.Size(111, 19);
            this.cboFac.TabIndex = 9;
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(0, 72);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(37, 16);
            this.tLabel2.Text = "Facility";
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtRemark.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtRemark.isBusinessItemName = "Remark";
            this.txtRemark.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtRemark.Location = new System.Drawing.Point(39, 93);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(196, 17);
            this.txtRemark.TabIndex = 12;
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(0, 96);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(41, 14);
            this.tLabel3.Text = "Remark";
            // 
            // HVO104
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.rbtnDry);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.cboFac);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.rbtnBreak);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.cboEQU);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cboHatch);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.tLabel3);
            this.Name = "HVO104";
            this.Text = "V/S - Equipment Operation";
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TTextBox txtJPVC;
        internal Framework.Controls.UserControls.TLabel Label6;
        internal Framework.Controls.UserControls.TCombobox cboHatch;
        internal Framework.Controls.UserControls.TLabel Label3;
        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TCombobox cboEQU;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        internal Framework.Controls.UserControls.TButton btnDelete;
        internal Framework.Controls.UserControls.TButton btnUpdate;
        private Framework.Controls.UserControls.TGrid grdData;
        internal Framework.Controls.UserControls.TRadioButton rbtnBreak;
        internal Framework.Controls.UserControls.TRadioButton rbtnDry;
        internal Framework.Controls.UserControls.TCombobox cboFac;
        internal Framework.Controls.UserControls.TLabel tLabel2;
        internal Framework.Controls.UserControls.TTextBox txtRemark;
        internal Framework.Controls.UserControls.TLabel tLabel3;
    }
}