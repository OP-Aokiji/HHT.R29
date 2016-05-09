namespace MOST.WHChecker
{
    partial class HWC104
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
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.txtCargo = new Framework.Controls.UserControls.TTextBox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.btnUpdate = new Framework.Controls.UserControls.TButton();
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.btnRetrieve = new Framework.Controls.UserControls.TButton();
            this.cboHatch = new Framework.Controls.UserControls.TCombobox();
            this.Label2 = new Framework.Controls.UserControls.TLabel();
            this.cboOPRMode = new Framework.Controls.UserControls.TCombobox();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.btnF2 = new Framework.Controls.UserControls.TButton();
            this.txtPkgTp = new Framework.Controls.UserControls.TTextBox();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
            this.chkFinal = new System.Windows.Forms.CheckBox();
            this.pnlRetrieve = new Framework.Controls.Container.TPanel();
            this.pnlUpdate = new Framework.Controls.Container.TPanel();
            this.txtEndTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtStartTime = new Framework.Controls.UserControls.TDateTimePicker();
            this.tLabel4 = new Framework.Controls.UserControls.TLabel();
            this.cboPaging = new Framework.Controls.UserControls.TCombobox();
            this.btnPrev = new Framework.Controls.UserControls.TButton();
            this.btnNext = new Framework.Controls.UserControls.TButton();
            this.pnlRetrieve.SuspendLayout();
            this.pnlUpdate.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnOk.Location = new System.Drawing.Point(95, 243);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 30;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(167, 243);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 31;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtCargo
            // 
            this.txtCargo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtCargo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtCargo.isBusinessItemName = "GR/BL";
            this.txtCargo.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtCargo.isSmallestInputLength = 3;
            this.txtCargo.Location = new System.Drawing.Point(34, 2);
            this.txtCargo.Name = "txtCargo";
            this.txtCargo.Size = new System.Drawing.Size(85, 19);
            this.txtCargo.TabIndex = 1;
            this.txtCargo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCargo_KeyPress);
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(0, 4);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(36, 16);
            this.tLabel1.Text = "GR/BL";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(3, 100);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(65, 20);
            this.btnUpdate.TabIndex = 25;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.ActionListener);
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(2, 122);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(234, 119);
            this.grdData.TabIndex = 27;
            this.grdData.CurrentCellChanged += new System.EventHandler(this.grdData_CurrentCellChanged);
            // 
            // btnF1
            // 
            this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF1.Location = new System.Drawing.Point(121, 2);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(36, 19);
            this.btnF1.TabIndex = 2;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Location = new System.Drawing.Point(167, 2);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(65, 20);
            this.btnRetrieve.TabIndex = 3;
            this.btnRetrieve.Text = "Retrieve";
            this.btnRetrieve.Click += new System.EventHandler(this.ActionListener);
            // 
            // cboHatch
            // 
            this.cboHatch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboHatch.isBusinessItemName = "Hatch";
            this.cboHatch.isMandatory = false;
            this.cboHatch.Location = new System.Drawing.Point(36, 3);
            this.cboHatch.Name = "cboHatch";
            this.cboHatch.Size = new System.Drawing.Size(52, 19);
            this.cboHatch.TabIndex = 7;
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label2.Location = new System.Drawing.Point(3, 6);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(32, 16);
            this.Label2.Text = "Hatch";
            // 
            // cboOPRMode
            // 
            this.cboOPRMode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboOPRMode.isBusinessItemName = "OPR Mode";
            this.cboOPRMode.isMandatory = false;
            this.cboOPRMode.Location = new System.Drawing.Point(146, 3);
            this.cboOPRMode.Name = "cboOPRMode";
            this.cboOPRMode.Size = new System.Drawing.Size(89, 19);
            this.cboOPRMode.TabIndex = 10;
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(91, 6);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(54, 16);
            this.tLabel2.Text = "OPR Mode";
            // 
            // btnF2
            // 
            this.btnF2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF2.Location = new System.Drawing.Point(109, 27);
            this.btnF2.Name = "btnF2";
            this.btnF2.Size = new System.Drawing.Size(36, 19);
            this.btnF2.TabIndex = 14;
            this.btnF2.Text = "F2";
            this.btnF2.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtPkgTp
            // 
            this.txtPkgTp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtPkgTp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtPkgTp.isBusinessItemName = "PkgTp";
            this.txtPkgTp.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtPkgTp.Location = new System.Drawing.Point(36, 27);
            this.txtPkgTp.Name = "txtPkgTp";
            this.txtPkgTp.Size = new System.Drawing.Size(71, 19);
            this.txtPkgTp.TabIndex = 12;
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(3, 29);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(36, 16);
            this.tLabel3.Text = "PkgTp";
            // 
            // chkFinal
            // 
            this.chkFinal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.chkFinal.Location = new System.Drawing.Point(170, 29);
            this.chkFinal.Name = "chkFinal";
            this.chkFinal.Size = new System.Drawing.Size(53, 17);
            this.chkFinal.TabIndex = 16;
            this.chkFinal.Text = "Final";
            // 
            // pnlRetrieve
            // 
            this.pnlRetrieve.Controls.Add(this.btnRetrieve);
            this.pnlRetrieve.Controls.Add(this.txtCargo);
            this.pnlRetrieve.Controls.Add(this.btnF1);
            this.pnlRetrieve.Controls.Add(this.tLabel1);
            this.pnlRetrieve.Location = new System.Drawing.Point(2, 1);
            this.pnlRetrieve.Name = "pnlRetrieve";
            this.pnlRetrieve.Size = new System.Drawing.Size(236, 23);
            // 
            // pnlUpdate
            // 
            this.pnlUpdate.Controls.Add(this.txtEndTime);
            this.pnlUpdate.Controls.Add(this.txtStartTime);
            this.pnlUpdate.Controls.Add(this.tLabel4);
            this.pnlUpdate.Controls.Add(this.txtPkgTp);
            this.pnlUpdate.Controls.Add(this.chkFinal);
            this.pnlUpdate.Controls.Add(this.cboHatch);
            this.pnlUpdate.Controls.Add(this.btnF2);
            this.pnlUpdate.Controls.Add(this.tLabel2);
            this.pnlUpdate.Controls.Add(this.cboOPRMode);
            this.pnlUpdate.Controls.Add(this.tLabel3);
            this.pnlUpdate.Controls.Add(this.Label2);
            this.pnlUpdate.Location = new System.Drawing.Point(0, 28);
            this.pnlUpdate.Name = "pnlUpdate";
            this.pnlUpdate.Size = new System.Drawing.Size(238, 70);
            // 
            // txtEndTime
            // 
            this.txtEndTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtEndTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtEndTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtEndTime.isBusinessItemName = "";
            this.txtEndTime.isMandatory = false;
            this.txtEndTime.Location = new System.Drawing.Point(123, 48);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(114, 20);
            this.txtEndTime.TabIndex = 21;
            this.txtEndTime.Value = new System.DateTime(2008, 10, 24, 16, 17, 0, 828);
            // 
            // txtStartTime
            // 
            this.txtStartTime.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtStartTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtStartTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtStartTime.isBusinessItemName = "StartTime";
            this.txtStartTime.isMandatory = true;
            this.txtStartTime.Location = new System.Drawing.Point(-1, 48);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(114, 20);
            this.txtStartTime.TabIndex = 20;
            this.txtStartTime.Value = new System.DateTime(2008, 10, 24, 16, 17, 0, 437);
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(113, 50);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(9, 16);
            this.tLabel4.Text = "~";
            // 
            // cboPaging
            // 
            this.cboPaging.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboPaging.isBusinessItemName = "Paging CargoJob";
            this.cboPaging.isMandatory = false;
            this.cboPaging.Location = new System.Drawing.Point(162, 100);
            this.cboPaging.Name = "cboPaging";
            this.cboPaging.Size = new System.Drawing.Size(36, 19);
            this.cboPaging.TabIndex = 5;
            // 
            // btnPrev
            // 
            this.btnPrev.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnPrev.Location = new System.Drawing.Point(124, 100);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(35, 19);
            this.btnPrev.TabIndex = 4;
            this.btnPrev.Text = "Prev";
            this.btnPrev.Click += new System.EventHandler(this.executePaging);
            this.cboPaging.SelectedIndexChanged += new System.EventHandler(this.cboPaging_SelectedIndexChanged);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnNext.Location = new System.Drawing.Point(200, 100);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(35, 19);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.executePaging);
            // 
            // HWC104
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.cboPaging);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.pnlUpdate);
            this.Controls.Add(this.pnlRetrieve);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Name = "HWC104";
            this.Text = "W/C - Cargo Job Monitoring";
            this.pnlRetrieve.ResumeLayout(false);
            this.pnlUpdate.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TTextBox txtCargo;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        internal Framework.Controls.UserControls.TButton btnUpdate;
        private Framework.Controls.UserControls.TGrid grdData;
        internal Framework.Controls.UserControls.TButton btnF1;
        internal Framework.Controls.UserControls.TButton btnRetrieve;
        internal Framework.Controls.UserControls.TCombobox cboHatch;
        internal Framework.Controls.UserControls.TLabel Label2;
        internal Framework.Controls.UserControls.TCombobox cboOPRMode;
        internal Framework.Controls.UserControls.TLabel tLabel2;
        internal Framework.Controls.UserControls.TButton btnF2;
        internal Framework.Controls.UserControls.TTextBox txtPkgTp;
        internal Framework.Controls.UserControls.TLabel tLabel3;
        internal System.Windows.Forms.CheckBox chkFinal;
        private Framework.Controls.Container.TPanel pnlRetrieve;
        private Framework.Controls.Container.TPanel pnlUpdate;
        private Framework.Controls.UserControls.TDateTimePicker txtEndTime;
        private Framework.Controls.UserControls.TDateTimePicker txtStartTime;
        internal Framework.Controls.UserControls.TLabel tLabel4;
        private Framework.Controls.UserControls.TCombobox cboPaging;
        private Framework.Controls.UserControls.TButton btnPrev;
        private Framework.Controls.UserControls.TButton btnNext;
    }
}