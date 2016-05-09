namespace MOST.VesselOperator
{
    partial class HVO113
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
            this.lblJPVC = new Framework.Controls.UserControls.TLabel();
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.btnDelete = new Framework.Controls.UserControls.TButton();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.btnUpdate = new Framework.Controls.UserControls.TButton();
            this.btnAdd = new Framework.Controls.UserControls.TButton();
            this.txtMLAD = new Framework.Controls.UserControls.TTextBox();
            this.txtMLAL = new Framework.Controls.UserControls.TTextBox();
            this.lblMLAL = new Framework.Controls.UserControls.TLabel();
            this.txtFHoseD = new Framework.Controls.UserControls.TTextBox();
            this.txtFHoseL = new Framework.Controls.UserControls.TTextBox();
            this.lblFHoseL = new Framework.Controls.UserControls.TLabel();
            this.tLabel6 = new Framework.Controls.UserControls.TLabel();
            this.tLabel5 = new Framework.Controls.UserControls.TLabel();
            this.btnDelay = new Framework.Controls.UserControls.TButton();
            this.SuspendLayout();
            // 
            // txtJPVC
            // 
            this.txtJPVC.Enabled = false;
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isBusinessItemName = "JPVC";
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.isMandatory = true;
            this.txtJPVC.Location = new System.Drawing.Point(41, 1);
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(98, 19);
            this.txtJPVC.TabIndex = 1;
            // 
            // lblJPVC
            // 
            this.lblJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblJPVC.Location = new System.Drawing.Point(0, 1);
            this.lblJPVC.Name = "lblJPVC";
            this.lblJPVC.Size = new System.Drawing.Size(34, 15);
            this.lblJPVC.Text = "JPVC";
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(3, 105);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(232, 135);
            this.grdData.TabIndex = 11;
            this.grdData.DoubleClick += new System.EventHandler(this.grdData_DoubleClick);
            this.grdData.CurrentCellChanged += new System.EventHandler(this.grdData_CurrentCellChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(145, 83);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(65, 20);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(170, 243);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnOk.Location = new System.Drawing.Point(99, 243);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 20;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(74, 83);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(65, 20);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 83);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(65, 20);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Input";
            this.btnAdd.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtMLAD
            // 
            this.txtMLAD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtMLAD.Enabled = false;
            this.txtMLAD.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtMLAD.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtMLAD.Location = new System.Drawing.Point(147, 56);
            this.txtMLAD.Name = "txtMLAD";
            this.txtMLAD.Size = new System.Drawing.Size(63, 19);
            this.txtMLAD.TabIndex = 5;
            this.txtMLAD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMLAL
            // 
            this.txtMLAL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtMLAL.Enabled = false;
            this.txtMLAL.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtMLAL.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtMLAL.Location = new System.Drawing.Point(57, 56);
            this.txtMLAL.Name = "txtMLAL";
            this.txtMLAL.Size = new System.Drawing.Size(63, 19);
            this.txtMLAL.TabIndex = 3;
            this.txtMLAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblMLAL
            // 
            this.lblMLAL.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblMLAL.Location = new System.Drawing.Point(1, 58);
            this.lblMLAL.Name = "lblMLAL";
            this.lblMLAL.Size = new System.Drawing.Size(42, 17);
            this.lblMLAL.Text = "MLA";
            // 
            // txtFHoseD
            // 
            this.txtFHoseD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtFHoseD.Enabled = false;
            this.txtFHoseD.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtFHoseD.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtFHoseD.Location = new System.Drawing.Point(147, 36);
            this.txtFHoseD.Name = "txtFHoseD";
            this.txtFHoseD.Size = new System.Drawing.Size(63, 19);
            this.txtFHoseD.TabIndex = 4;
            this.txtFHoseD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFHoseL
            // 
            this.txtFHoseL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtFHoseL.Enabled = false;
            this.txtFHoseL.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtFHoseL.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtFHoseL.Location = new System.Drawing.Point(57, 36);
            this.txtFHoseL.Name = "txtFHoseL";
            this.txtFHoseL.Size = new System.Drawing.Size(63, 19);
            this.txtFHoseL.TabIndex = 2;
            this.txtFHoseL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblFHoseL
            // 
            this.lblFHoseL.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblFHoseL.Location = new System.Drawing.Point(1, 38);
            this.lblFHoseL.Name = "lblFHoseL";
            this.lblFHoseL.Size = new System.Drawing.Size(42, 17);
            this.lblFHoseL.Text = "F/Hose";
            // 
            // tLabel6
            // 
            this.tLabel6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel6.Location = new System.Drawing.Point(147, 22);
            this.tLabel6.Name = "tLabel6";
            this.tLabel6.Size = new System.Drawing.Size(63, 17);
            this.tLabel6.Text = "Discharging";
            // 
            // tLabel5
            // 
            this.tLabel5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel5.Location = new System.Drawing.Point(57, 22);
            this.tLabel5.Name = "tLabel5";
            this.tLabel5.Size = new System.Drawing.Size(63, 17);
            this.tLabel5.Text = "Loading";
            // 
            // btnDelay
            // 
            this.btnDelay.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnDelay.Location = new System.Drawing.Point(3, 242);
            this.btnDelay.Name = "btnDelay";
            this.btnDelay.Size = new System.Drawing.Size(65, 24);
            this.btnDelay.TabIndex = 15;
            this.btnDelay.Text = "Delay";
            this.btnDelay.Click += new System.EventHandler(this.ActionListener);
            // 
            // HVO113
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.btnDelay);
            this.Controls.Add(this.txtMLAD);
            this.Controls.Add(this.txtMLAL);
            this.Controls.Add(this.lblMLAL);
            this.Controls.Add(this.txtFHoseD);
            this.Controls.Add(this.txtFHoseL);
            this.Controls.Add(this.lblFHoseL);
            this.Controls.Add(this.tLabel6);
            this.Controls.Add(this.tLabel5);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.lblJPVC);
            this.Name = "HVO113";
            this.Text = "V/S - Jetty Operation List";
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TTextBox txtJPVC;
        private Framework.Controls.UserControls.TLabel lblJPVC;
        private Framework.Controls.UserControls.TGrid grdData;
        internal Framework.Controls.UserControls.TButton btnDelete;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TButton btnUpdate;
        internal Framework.Controls.UserControls.TButton btnAdd;
        internal Framework.Controls.UserControls.TTextBox txtMLAD;
        internal Framework.Controls.UserControls.TTextBox txtMLAL;
        internal Framework.Controls.UserControls.TLabel lblMLAL;
        internal Framework.Controls.UserControls.TTextBox txtFHoseD;
        internal Framework.Controls.UserControls.TTextBox txtFHoseL;
        internal Framework.Controls.UserControls.TLabel lblFHoseL;
        internal Framework.Controls.UserControls.TLabel tLabel6;
        internal Framework.Controls.UserControls.TLabel tLabel5;
        internal Framework.Controls.UserControls.TButton btnDelay;
    }
}