namespace MOST.VesselOperator
{
    partial class HVO106
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
            this.cboTmnlOPR = new Framework.Controls.UserControls.TCombobox();
            this.lblLineType = new Framework.Controls.UserControls.TLabel();
            this.txtPumping = new Framework.Controls.UserControls.TTextBox();
            this.Label9 = new Framework.Controls.UserControls.TLabel();
            this.txtTonnage = new Framework.Controls.UserControls.TTextBox();
            this.Label8 = new Framework.Controls.UserControls.TLabel();
            this.cboLineType = new Framework.Controls.UserControls.TCombobox();
            this.cboOPRType = new Framework.Controls.UserControls.TCombobox();
            this.Label2 = new Framework.Controls.UserControls.TLabel();
            this.Label1 = new Framework.Controls.UserControls.TLabel();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.cboCargoType = new Framework.Controls.UserControls.TCombobox();
            this.pnlTmnlOPR = new Framework.Controls.Container.TPanel();
            this.pnlMain = new Framework.Controls.Container.TPanel();
            this.txtLineNos = new Framework.Controls.UserControls.TTextBox();
            this.btnInputDt = new Framework.Controls.UserControls.TButton();
            this.cboPkgTp = new Framework.Controls.UserControls.TCombobox();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.cboCnsne = new Framework.Controls.UserControls.TCombobox();
            this.cboShprCnsne = new Framework.Controls.UserControls.TCombobox();
            this.tLabel11 = new Framework.Controls.UserControls.TLabel();
            this.tLabel4 = new Framework.Controls.UserControls.TLabel();
            this.chkCompleted = new System.Windows.Forms.CheckBox();
            this.cboCmdt = new Framework.Controls.UserControls.TCombobox();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
            this.txtDSBal = new Framework.Controls.UserControls.TTextBox();
            this.txtDSDoc = new Framework.Controls.UserControls.TTextBox();
            this.txtDSHandled = new Framework.Controls.UserControls.TTextBox();
            this.txtLDBal = new Framework.Controls.UserControls.TTextBox();
            this.txtLDDoc = new Framework.Controls.UserControls.TTextBox();
            this.txtLDHandled = new Framework.Controls.UserControls.TTextBox();
            this.tLabel9 = new Framework.Controls.UserControls.TLabel();
            this.tLabel6 = new Framework.Controls.UserControls.TLabel();
            this.tLabel7 = new Framework.Controls.UserControls.TLabel();
            this.tLabel8 = new Framework.Controls.UserControls.TLabel();
            this.tLabel5 = new Framework.Controls.UserControls.TLabel();
            this.tLabel10 = new Framework.Controls.UserControls.TLabel();
            this.pnlOPRType = new Framework.Controls.Container.TPanel();
            this.pnlTmnlOPR.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlOPRType.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboTmnlOPR
            // 
            this.cboTmnlOPR.Enabled = false;
            this.cboTmnlOPR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboTmnlOPR.isBusinessItemName = "Terminal OPR";
            this.cboTmnlOPR.isMandatory = true;
            this.cboTmnlOPR.Location = new System.Drawing.Point(84, 1);
            this.cboTmnlOPR.Name = "cboTmnlOPR";
            this.cboTmnlOPR.Size = new System.Drawing.Size(148, 19);
            this.cboTmnlOPR.TabIndex = 2;
            this.cboTmnlOPR.SelectedIndexChanged += new System.EventHandler(this.ComboboxListener);
            // 
            // lblLineType
            // 
            this.lblLineType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblLineType.Location = new System.Drawing.Point(8, 69);
            this.lblLineType.Name = "lblLineType";
            this.lblLineType.Size = new System.Drawing.Size(24, 16);
            this.lblLineType.Text = "Line";
            // 
            // txtPumping
            // 
            this.txtPumping.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtPumping.Enabled = false;
            this.txtPumping.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtPumping.isBusinessItemName = "Pump.Rate";
            this.txtPumping.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtPumping.isMandatory = true;
            this.txtPumping.Location = new System.Drawing.Point(61, 88);
            this.txtPumping.Multiline = true;
            this.txtPumping.Name = "txtPumping";
            this.txtPumping.Size = new System.Drawing.Size(60, 17);
            this.txtPumping.TabIndex = 10;
            this.txtPumping.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label9
            // 
            this.Label9.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label9.Location = new System.Drawing.Point(3, 90);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(59, 15);
            this.Label9.Text = "Pump.Rate";
            // 
            // txtTonnage
            // 
            this.txtTonnage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtTonnage.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtTonnage.isBusinessItemName = "Tonnage";
            this.txtTonnage.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtTonnage.isMandatory = true;
            this.txtTonnage.Location = new System.Drawing.Point(168, 44);
            this.txtTonnage.Multiline = true;
            this.txtTonnage.Name = "txtTonnage";
            this.txtTonnage.Size = new System.Drawing.Size(60, 17);
            this.txtTonnage.TabIndex = 9;
            this.txtTonnage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTonnage.LostFocus += new System.EventHandler(this.txtTonnage_LostFocus);
            // 
            // Label8
            // 
            this.Label8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label8.Location = new System.Drawing.Point(117, 46);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(46, 15);
            this.Label8.Text = "Tonnage";
            // 
            // cboLineType
            // 
            this.cboLineType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboLineType.isBusinessItemName = "Line Type";
            this.cboLineType.isMandatory = true;
            this.cboLineType.Location = new System.Drawing.Point(41, 66);
            this.cboLineType.Name = "cboLineType";
            this.cboLineType.Size = new System.Drawing.Size(115, 19);
            this.cboLineType.TabIndex = 4;
            // 
            // cboOPRType
            // 
            this.cboOPRType.Enabled = false;
            this.cboOPRType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboOPRType.isBusinessItemName = "Operation Type";
            this.cboOPRType.isMandatory = true;
            this.cboOPRType.Location = new System.Drawing.Point(45, 22);
            this.cboOPRType.Name = "cboOPRType";
            this.cboOPRType.Size = new System.Drawing.Size(67, 19);
            this.cboOPRType.TabIndex = 1;
            this.cboOPRType.SelectedIndexChanged += new System.EventHandler(this.ComboboxListener);
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label2.Location = new System.Drawing.Point(4, 25);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(35, 16);
            this.Label2.Text = "LD/DS";
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label1.Location = new System.Drawing.Point(6, 5);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(70, 16);
            this.Label1.Text = "Terminal OPR";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(95, 244);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 244);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(118, 25);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(45, 16);
            this.tLabel1.Text = "CG Type";
            // 
            // cboCargoType
            // 
            this.cboCargoType.Enabled = false;
            this.cboCargoType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboCargoType.isBusinessItemName = "Cargo Type";
            this.cboCargoType.isMandatory = true;
            this.cboCargoType.Location = new System.Drawing.Point(169, 22);
            this.cboCargoType.Name = "cboCargoType";
            this.cboCargoType.Size = new System.Drawing.Size(63, 19);
            this.cboCargoType.TabIndex = 3;
            this.cboCargoType.SelectedIndexChanged += new System.EventHandler(this.ComboboxListener);
            // 
            // pnlTmnlOPR
            // 
            this.pnlTmnlOPR.Controls.Add(this.cboTmnlOPR);
            this.pnlTmnlOPR.Controls.Add(this.Label1);
            this.pnlTmnlOPR.Location = new System.Drawing.Point(0, 42);
            this.pnlTmnlOPR.Name = "pnlTmnlOPR";
            this.pnlTmnlOPR.Size = new System.Drawing.Size(238, 22);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.txtLineNos);
            this.pnlMain.Controls.Add(this.btnInputDt);
            this.pnlMain.Controls.Add(this.cboPkgTp);
            this.pnlMain.Controls.Add(this.tLabel2);
            this.pnlMain.Controls.Add(this.cboCnsne);
            this.pnlMain.Controls.Add(this.txtPumping);
            this.pnlMain.Controls.Add(this.Label9);
            this.pnlMain.Controls.Add(this.cboShprCnsne);
            this.pnlMain.Controls.Add(this.tLabel4);
            this.pnlMain.Controls.Add(this.tLabel11);
            this.pnlMain.Controls.Add(this.chkCompleted);
            this.pnlMain.Controls.Add(this.txtDSBal);
            this.pnlMain.Controls.Add(this.txtDSDoc);
            this.pnlMain.Controls.Add(this.txtDSHandled);
            this.pnlMain.Controls.Add(this.txtLDBal);
            this.pnlMain.Controls.Add(this.txtLDDoc);
            this.pnlMain.Controls.Add(this.txtLDHandled);
            this.pnlMain.Controls.Add(this.cboLineType);
            this.pnlMain.Controls.Add(this.txtTonnage);
            this.pnlMain.Controls.Add(this.lblLineType);
            this.pnlMain.Controls.Add(this.tLabel9);
            this.pnlMain.Controls.Add(this.tLabel6);
            this.pnlMain.Controls.Add(this.Label8);
            this.pnlMain.Controls.Add(this.tLabel7);
            this.pnlMain.Controls.Add(this.tLabel8);
            this.pnlMain.Controls.Add(this.tLabel5);
            this.pnlMain.Controls.Add(this.tLabel10);
            this.pnlMain.Location = new System.Drawing.Point(0, 64);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(238, 179);
            // 
            // txtLineNos
            // 
            this.txtLineNos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtLineNos.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtLineNos.isBusinessItemName = "Line Nos.";
            this.txtLineNos.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtLineNos.Location = new System.Drawing.Point(187, 67);
            this.txtLineNos.Multiline = true;
            this.txtLineNos.Name = "txtLineNos";
            this.txtLineNos.Size = new System.Drawing.Size(48, 17);
            this.txtLineNos.TabIndex = 5;
            this.txtLineNos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnInputDt
            // 
            this.btnInputDt.Location = new System.Drawing.Point(7, 154);
            this.btnInputDt.Name = "btnInputDt";
            this.btnInputDt.Size = new System.Drawing.Size(134, 24);
            this.btnInputDt.TabIndex = 11;
            this.btnInputDt.Text = "Input Hose Datetime";
            this.btnInputDt.Click += new System.EventHandler(this.ActionListener);
            // 
            // cboPkgTp
            // 
            this.cboPkgTp.Enabled = false;
            this.cboPkgTp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboPkgTp.isBusinessItemName = "Pkg Tp";
            this.cboPkgTp.isMandatory = true;
            this.cboPkgTp.Location = new System.Drawing.Point(41, 44);
            this.cboPkgTp.Name = "cboPkgTp";
            this.cboPkgTp.Size = new System.Drawing.Size(72, 19);
            this.cboPkgTp.TabIndex = 9;
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(8, 47);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(34, 16);
            this.tLabel2.Text = "PkgTp";
            // 
            // cboCnsne
            // 
            this.cboCnsne.Enabled = false;
            this.cboCnsne.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboCnsne.isBusinessItemName = "Consignee";
            this.cboCnsne.isMandatory = false;
            this.cboCnsne.Location = new System.Drawing.Point(84, 22);
            this.cboCnsne.Name = "cboCnsne";
            this.cboCnsne.Size = new System.Drawing.Size(148, 19);
            this.cboCnsne.TabIndex = 7;
            // 
            // cboShprCnsne
            // 
            this.cboShprCnsne.Enabled = false;
            this.cboShprCnsne.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboShprCnsne.isBusinessItemName = "Shipper/Consignee";
            this.cboShprCnsne.isMandatory = false;
            this.cboShprCnsne.Location = new System.Drawing.Point(84, 0);
            this.cboShprCnsne.Name = "cboShprCnsne";
            this.cboShprCnsne.Size = new System.Drawing.Size(148, 19);
            this.cboShprCnsne.TabIndex = 8;
            // 
            // tLabel11
            // 
            this.tLabel11.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel11.Location = new System.Drawing.Point(19, 23);
            this.tLabel11.Name = "tLabel11";
            this.tLabel11.Size = new System.Drawing.Size(42, 16);
            this.tLabel11.Text = "Cnsigne";
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(19, 1);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(42, 16);
            this.tLabel4.Text = "Shipper";
            // 
            // chkCompleted
            // 
            this.chkCompleted.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.chkCompleted.Location = new System.Drawing.Point(139, 88);
            this.chkCompleted.Name = "chkCompleted";
            this.chkCompleted.Size = new System.Drawing.Size(79, 17);
            this.chkCompleted.TabIndex = 13;
            this.chkCompleted.Text = "Completed";
            // 
            // cboCmdt
            // 
            this.cboCmdt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboCmdt.isBusinessItemName = "Commodity";
            this.cboCmdt.isMandatory = false;
            this.cboCmdt.Location = new System.Drawing.Point(85, 1);
            this.cboCmdt.Name = "cboCmdt";
            this.cboCmdt.Size = new System.Drawing.Size(147, 19);
            this.cboCmdt.TabIndex = 6;
            this.cboCmdt.SelectedIndexChanged += new System.EventHandler(this.ComboboxListener);
            this.cboCmdt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(12, 4);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(59, 16);
            this.tLabel3.Text = "Commodity";
            // 
            // txtDSBal
            // 
            this.txtDSBal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDSBal.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDSBal.Location = new System.Drawing.Point(177, 135);
            this.txtDSBal.Multiline = true;
            this.txtDSBal.Name = "txtDSBal";
            this.txtDSBal.ReadOnly = true;
            this.txtDSBal.Size = new System.Drawing.Size(60, 17);
            this.txtDSBal.TabIndex = 93;
            this.txtDSBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDSDoc
            // 
            this.txtDSDoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDSDoc.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDSDoc.Location = new System.Drawing.Point(53, 135);
            this.txtDSDoc.Multiline = true;
            this.txtDSDoc.Name = "txtDSDoc";
            this.txtDSDoc.ReadOnly = true;
            this.txtDSDoc.Size = new System.Drawing.Size(60, 17);
            this.txtDSDoc.TabIndex = 91;
            this.txtDSDoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDSHandled
            // 
            this.txtDSHandled.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDSHandled.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDSHandled.Location = new System.Drawing.Point(115, 135);
            this.txtDSHandled.Multiline = true;
            this.txtDSHandled.Name = "txtDSHandled";
            this.txtDSHandled.ReadOnly = true;
            this.txtDSHandled.Size = new System.Drawing.Size(60, 17);
            this.txtDSHandled.TabIndex = 92;
            this.txtDSHandled.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLDBal
            // 
            this.txtLDBal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtLDBal.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtLDBal.Location = new System.Drawing.Point(177, 117);
            this.txtLDBal.Multiline = true;
            this.txtLDBal.Name = "txtLDBal";
            this.txtLDBal.ReadOnly = true;
            this.txtLDBal.Size = new System.Drawing.Size(60, 17);
            this.txtLDBal.TabIndex = 87;
            this.txtLDBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLDDoc
            // 
            this.txtLDDoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtLDDoc.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtLDDoc.Location = new System.Drawing.Point(53, 117);
            this.txtLDDoc.Multiline = true;
            this.txtLDDoc.Name = "txtLDDoc";
            this.txtLDDoc.ReadOnly = true;
            this.txtLDDoc.Size = new System.Drawing.Size(60, 17);
            this.txtLDDoc.TabIndex = 81;
            this.txtLDDoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLDHandled
            // 
            this.txtLDHandled.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtLDHandled.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtLDHandled.Location = new System.Drawing.Point(115, 117);
            this.txtLDHandled.Multiline = true;
            this.txtLDHandled.Name = "txtLDHandled";
            this.txtLDHandled.ReadOnly = true;
            this.txtLDHandled.Size = new System.Drawing.Size(60, 17);
            this.txtLDHandled.TabIndex = 82;
            this.txtLDHandled.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel9
            // 
            this.tLabel9.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel9.Location = new System.Drawing.Point(2, 118);
            this.tLabel9.Name = "tLabel9";
            this.tLabel9.Size = new System.Drawing.Size(45, 15);
            this.tLabel9.Text = "Load";
            // 
            // tLabel6
            // 
            this.tLabel6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel6.Location = new System.Drawing.Point(115, 105);
            this.tLabel6.Name = "tLabel6";
            this.tLabel6.Size = new System.Drawing.Size(60, 13);
            this.tLabel6.Text = "Handled";
            this.tLabel6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tLabel7
            // 
            this.tLabel7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel7.Location = new System.Drawing.Point(53, 105);
            this.tLabel7.Name = "tLabel7";
            this.tLabel7.Size = new System.Drawing.Size(60, 13);
            this.tLabel7.Text = "Doc";
            this.tLabel7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tLabel8
            // 
            this.tLabel8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel8.Location = new System.Drawing.Point(177, 105);
            this.tLabel8.Name = "tLabel8";
            this.tLabel8.Size = new System.Drawing.Size(60, 13);
            this.tLabel8.Text = "Balance";
            this.tLabel8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tLabel5
            // 
            this.tLabel5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel5.Location = new System.Drawing.Point(2, 136);
            this.tLabel5.Name = "tLabel5";
            this.tLabel5.Size = new System.Drawing.Size(52, 15);
            this.tLabel5.Text = "Discharge";
            // 
            // tLabel10
            // 
            this.tLabel10.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel10.Location = new System.Drawing.Point(160, 69);
            this.tLabel10.Name = "tLabel10";
            this.tLabel10.Size = new System.Drawing.Size(26, 15);
            this.tLabel10.Text = "Nos.";
            // 
            // pnlOPRType
            // 
            this.pnlOPRType.Controls.Add(this.cboOPRType);
            this.pnlOPRType.Controls.Add(this.Label2);
            this.pnlOPRType.Controls.Add(this.tLabel1);
            this.pnlOPRType.Controls.Add(this.cboCargoType);
            this.pnlOPRType.Controls.Add(this.cboCmdt);
            this.pnlOPRType.Controls.Add(this.tLabel3);
            this.pnlOPRType.Location = new System.Drawing.Point(0, 0);
            this.pnlOPRType.Name = "pnlOPRType";
            this.pnlOPRType.Size = new System.Drawing.Size(238, 42);
            // 
            // HVO106
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.pnlOPRType);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTmnlOPR);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "HVO106";
            this.Text = "V/S - Jetty Operation";
            this.pnlTmnlOPR.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlOPRType.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TCombobox cboTmnlOPR;
        internal Framework.Controls.UserControls.TLabel lblLineType;
        internal Framework.Controls.UserControls.TTextBox txtPumping;
        internal Framework.Controls.UserControls.TLabel Label9;
        internal Framework.Controls.UserControls.TTextBox txtTonnage;
        internal Framework.Controls.UserControls.TLabel Label8;
        internal Framework.Controls.UserControls.TCombobox cboLineType;
        internal Framework.Controls.UserControls.TCombobox cboOPRType;
        internal Framework.Controls.UserControls.TLabel Label2;
        internal Framework.Controls.UserControls.TLabel Label1;
        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        internal Framework.Controls.UserControls.TCombobox cboCargoType;
        private Framework.Controls.Container.TPanel pnlTmnlOPR;
        private Framework.Controls.Container.TPanel pnlMain;
        internal Framework.Controls.UserControls.TTextBox txtLDDoc;
        internal Framework.Controls.UserControls.TTextBox txtLDHandled;
        internal Framework.Controls.UserControls.TLabel tLabel5;
        internal Framework.Controls.UserControls.TLabel tLabel6;
        internal Framework.Controls.UserControls.TLabel tLabel7;
        internal Framework.Controls.UserControls.TTextBox txtDSBal;
        internal Framework.Controls.UserControls.TTextBox txtDSDoc;
        internal Framework.Controls.UserControls.TTextBox txtDSHandled;
        internal Framework.Controls.UserControls.TLabel tLabel9;
        internal Framework.Controls.UserControls.TTextBox txtLDBal;
        internal Framework.Controls.UserControls.TLabel tLabel8;
        internal System.Windows.Forms.CheckBox chkCompleted;
        private Framework.Controls.Container.TPanel pnlOPRType;
        internal Framework.Controls.UserControls.TCombobox cboShprCnsne;
        internal Framework.Controls.UserControls.TLabel tLabel4;
        internal Framework.Controls.UserControls.TCombobox cboCmdt;
        internal Framework.Controls.UserControls.TLabel tLabel3;
        internal Framework.Controls.UserControls.TCombobox cboPkgTp;
        internal Framework.Controls.UserControls.TLabel tLabel2;
        internal Framework.Controls.UserControls.TButton btnInputDt;
        internal Framework.Controls.UserControls.TTextBox txtLineNos;
        internal Framework.Controls.UserControls.TLabel tLabel10;
        internal Framework.Controls.UserControls.TCombobox cboCnsne;
        internal Framework.Controls.UserControls.TLabel tLabel11;
    }
}