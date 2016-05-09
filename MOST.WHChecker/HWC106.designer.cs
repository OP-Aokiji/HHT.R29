namespace MOST.WHChecker
{
    partial class HWC106
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
            this.txtSelMT = new Framework.Controls.UserControls.TTextBox();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.tLabel4 = new Framework.Controls.UserControls.TLabel();
            this.btnReconcile = new Framework.Controls.UserControls.TButton();
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.txtSelM3 = new Framework.Controls.UserControls.TTextBox();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.txtSelQty = new Framework.Controls.UserControls.TTextBox();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
            this.cboChangeCgCondition = new Framework.Controls.UserControls.TCombobox();
            this.tLabel6 = new Framework.Controls.UserControls.TLabel();
            this.tLabel5 = new Framework.Controls.UserControls.TLabel();
            this.txtSelCgCond = new Framework.Controls.UserControls.TTextBox();
            this.tLabel7 = new Framework.Controls.UserControls.TLabel();
            this.txtChangeQty = new Framework.Controls.UserControls.TTextBox();
            this.txtChangeM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtChangeMT = new Framework.Controls.UserControls.TTextBox();
            this.tLabel8 = new Framework.Controls.UserControls.TLabel();
            this.txtSNBL = new Framework.Controls.UserControls.TTextBox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.txtDatetime = new Framework.Controls.UserControls.TDateTimePicker();
            this.tLabel9 = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
            // 
            // txtSelMT
            // 
            this.txtSelMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSelMT.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtSelMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtSelMT.Location = new System.Drawing.Point(43, 30);
            this.txtSelMT.Multiline = true;
            this.txtSelMT.Name = "txtSelMT";
            this.txtSelMT.ReadOnly = true;
            this.txtSelMT.Size = new System.Drawing.Size(44, 17);
            this.txtSelMT.TabIndex = 5;
            this.txtSelMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(95, 244);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 244);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(43, 18);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(44, 12);
            this.tLabel4.Text = "MT";
            this.tLabel4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnReconcile
            // 
            this.btnReconcile.Location = new System.Drawing.Point(133, 116);
            this.btnReconcile.Name = "btnReconcile";
            this.btnReconcile.Size = new System.Drawing.Size(102, 20);
            this.btnReconcile.TabIndex = 12;
            this.btnReconcile.Text = "Reconcile";
            this.btnReconcile.Click += new System.EventHandler(this.ActionListener);
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(3, 139);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(232, 103);
            this.grdData.TabIndex = 15;
            this.grdData.CurrentCellChanged += new System.EventHandler(this.grdData_CurrentCellChanged);
            // 
            // txtSelM3
            // 
            this.txtSelM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSelM3.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtSelM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtSelM3.Location = new System.Drawing.Point(88, 30);
            this.txtSelM3.Multiline = true;
            this.txtSelM3.Name = "txtSelM3";
            this.txtSelM3.ReadOnly = true;
            this.txtSelM3.Size = new System.Drawing.Size(44, 17);
            this.txtSelM3.TabIndex = 7;
            this.txtSelM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(88, 18);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(44, 12);
            this.tLabel2.Text = "M3";
            this.tLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtSelQty
            // 
            this.txtSelQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSelQty.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtSelQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtSelQty.Location = new System.Drawing.Point(133, 30);
            this.txtSelQty.Multiline = true;
            this.txtSelQty.Name = "txtSelQty";
            this.txtSelQty.ReadOnly = true;
            this.txtSelQty.Size = new System.Drawing.Size(44, 17);
            this.txtSelQty.TabIndex = 9;
            this.txtSelQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(133, 18);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(44, 12);
            this.tLabel3.Text = "QTY";
            this.tLabel3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cboChangeCgCondition
            // 
            this.cboChangeCgCondition.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboChangeCgCondition.isBusinessItemName = "";
            this.cboChangeCgCondition.isMandatory = false;
            this.cboChangeCgCondition.Location = new System.Drawing.Point(96, 69);
            this.cboChangeCgCondition.Name = "cboChangeCgCondition";
            this.cboChangeCgCondition.Size = new System.Drawing.Size(139, 19);
            this.cboChangeCgCondition.TabIndex = 162;
            // 
            // tLabel6
            // 
            this.tLabel6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel6.Location = new System.Drawing.Point(0, 72);
            this.tLabel6.Name = "tLabel6";
            this.tLabel6.Size = new System.Drawing.Size(97, 16);
            this.tLabel6.Text = "To Cargo Condition";
            // 
            // tLabel5
            // 
            this.tLabel5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel5.Location = new System.Drawing.Point(0, 31);
            this.tLabel5.Name = "tLabel5";
            this.tLabel5.Size = new System.Drawing.Size(45, 16);
            this.tLabel5.Text = "Selected";
            // 
            // txtSelCgCond
            // 
            this.txtSelCgCond.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSelCgCond.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtSelCgCond.Location = new System.Drawing.Point(181, 30);
            this.txtSelCgCond.Multiline = true;
            this.txtSelCgCond.Name = "txtSelCgCond";
            this.txtSelCgCond.ReadOnly = true;
            this.txtSelCgCond.Size = new System.Drawing.Size(56, 17);
            this.txtSelCgCond.TabIndex = 175;
            // 
            // tLabel7
            // 
            this.tLabel7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel7.Location = new System.Drawing.Point(181, 18);
            this.tLabel7.Name = "tLabel7";
            this.tLabel7.Size = new System.Drawing.Size(53, 12);
            this.tLabel7.Text = "Condition";
            this.tLabel7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtChangeQty
            // 
            this.txtChangeQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtChangeQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtChangeQty.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtChangeQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtChangeQty.Location = new System.Drawing.Point(133, 50);
            this.txtChangeQty.Multiline = true;
            this.txtChangeQty.Name = "txtChangeQty";
            this.txtChangeQty.Size = new System.Drawing.Size(44, 17);
            this.txtChangeQty.TabIndex = 181;
            this.txtChangeQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtChangeM3
            // 
            this.txtChangeM3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtChangeM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtChangeM3.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtChangeM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtChangeM3.Location = new System.Drawing.Point(88, 50);
            this.txtChangeM3.Multiline = true;
            this.txtChangeM3.Name = "txtChangeM3";
            this.txtChangeM3.Size = new System.Drawing.Size(44, 17);
            this.txtChangeM3.TabIndex = 180;
            this.txtChangeM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtChangeMT
            // 
            this.txtChangeMT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtChangeMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtChangeMT.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtChangeMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtChangeMT.Location = new System.Drawing.Point(43, 50);
            this.txtChangeMT.Multiline = true;
            this.txtChangeMT.Name = "txtChangeMT";
            this.txtChangeMT.Size = new System.Drawing.Size(44, 17);
            this.txtChangeMT.TabIndex = 179;
            this.txtChangeMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel8
            // 
            this.tLabel8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel8.Location = new System.Drawing.Point(0, 51);
            this.tLabel8.Name = "tLabel8";
            this.tLabel8.Size = new System.Drawing.Size(45, 16);
            this.tLabel8.Text = "Change";
            // 
            // txtSNBL
            // 
            this.txtSNBL.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSNBL.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtSNBL.Location = new System.Drawing.Point(43, 1);
            this.txtSNBL.Multiline = true;
            this.txtSNBL.Name = "txtSNBL";
            this.txtSNBL.ReadOnly = true;
            this.txtSNBL.Size = new System.Drawing.Size(134, 17);
            this.txtSNBL.TabIndex = 192;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(4, 2);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(35, 16);
            this.tLabel1.Text = "SN/BL";
            // 
            // txtDatetime
            // 
            this.txtDatetime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtDatetime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtDatetime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtDatetime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDatetime.isBusinessItemName = "R/C Date";
            this.txtDatetime.isMandatory = true;
            this.txtDatetime.Location = new System.Drawing.Point(95, 91);
            this.txtDatetime.Name = "txtDatetime";
            this.txtDatetime.Size = new System.Drawing.Size(129, 22);
            this.txtDatetime.TabIndex = 202;
            this.txtDatetime.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 815);
            // 
            // tLabel9
            // 
            this.tLabel9.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel9.Location = new System.Drawing.Point(43, 96);
            this.tLabel9.Name = "tLabel9";
            this.tLabel9.Size = new System.Drawing.Size(49, 17);
            this.tLabel9.Text = "R/C Date";
            // 
            // HWC106
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.txtDatetime);
            this.Controls.Add(this.tLabel9);
            this.Controls.Add(this.txtSNBL);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.txtChangeQty);
            this.Controls.Add(this.txtChangeM3);
            this.Controls.Add(this.txtChangeMT);
            this.Controls.Add(this.tLabel8);
            this.Controls.Add(this.tLabel7);
            this.Controls.Add(this.txtSelCgCond);
            this.Controls.Add(this.cboChangeCgCondition);
            this.Controls.Add(this.txtSelQty);
            this.Controls.Add(this.txtSelM3);
            this.Controls.Add(this.btnReconcile);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.txtSelMT);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tLabel3);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.tLabel4);
            this.Controls.Add(this.tLabel5);
            this.Controls.Add(this.tLabel6);
            this.Name = "HWC106";
            this.Text = "W/C - WH Reconciliation Detail";
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TTextBox txtSelMT;
        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TLabel tLabel4;
        internal Framework.Controls.UserControls.TButton btnReconcile;
        private Framework.Controls.UserControls.TGrid grdData;
        internal Framework.Controls.UserControls.TTextBox txtSelM3;
        internal Framework.Controls.UserControls.TLabel tLabel2;
        internal Framework.Controls.UserControls.TTextBox txtSelQty;
        internal Framework.Controls.UserControls.TLabel tLabel3;
        internal Framework.Controls.UserControls.TCombobox cboChangeCgCondition;
        internal Framework.Controls.UserControls.TLabel tLabel6;
        internal Framework.Controls.UserControls.TLabel tLabel5;
        internal Framework.Controls.UserControls.TTextBox txtSelCgCond;
        internal Framework.Controls.UserControls.TLabel tLabel7;
        internal Framework.Controls.UserControls.TTextBox txtChangeQty;
        internal Framework.Controls.UserControls.TTextBox txtChangeM3;
        internal Framework.Controls.UserControls.TTextBox txtChangeMT;
        internal Framework.Controls.UserControls.TLabel tLabel8;
        internal Framework.Controls.UserControls.TTextBox txtSNBL;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        private Framework.Controls.UserControls.TDateTimePicker txtDatetime;
        internal Framework.Controls.UserControls.TLabel tLabel9;
    }
}