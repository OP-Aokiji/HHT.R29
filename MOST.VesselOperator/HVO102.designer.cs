namespace MOST.VesselOperator
{
    partial class HVO102
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
            this.tabControl = new Framework.Controls.Container.TTabControl();
            this.TabPage1 = new Framework.Controls.Container.TTabPage();
            this.txtBerthWharfE = new Framework.Controls.UserControls.TTextBox();
            this.txtBerthWharfS = new Framework.Controls.UserControls.TTextBox();
            this.cboBerthLoc = new Framework.Controls.UserControls.TCombobox();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.txtWharfE = new Framework.Controls.UserControls.TTextBox();
            this.txtWharfS = new Framework.Controls.UserControls.TTextBox();
            this.Label10 = new Framework.Controls.UserControls.TLabel();
            this.Label9 = new Framework.Controls.UserControls.TLabel();
            this.txtLOA = new Framework.Controls.UserControls.TTextBox();
            this.Label8 = new Framework.Controls.UserControls.TLabel();
            this.Label12 = new Framework.Controls.UserControls.TLabel();
            this.txtName = new Framework.Controls.UserControls.TTextBox();
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.Label1 = new Framework.Controls.UserControls.TLabel();
            this.Label2 = new Framework.Controls.UserControls.TLabel();
            this.TabPage2 = new Framework.Controls.Container.TTabPage();
            this.txtCurATB = new Framework.Controls.UserControls.TTextBox();
            this.tLabel5 = new Framework.Controls.UserControls.TLabel();
            this.txtTugATU = new Framework.Controls.UserControls.TTextBox();
            this.txtMooringATU = new Framework.Controls.UserControls.TTextBox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.tLabel4 = new Framework.Controls.UserControls.TLabel();
            this.chkPilotATU = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtTugATB = new Framework.Controls.UserControls.TTextBox();
            this.txtMooringATB = new Framework.Controls.UserControls.TTextBox();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.chkPilotATB = new System.Windows.Forms.CheckBox();
            this.txtATU = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtATB = new Framework.Controls.UserControls.TDateTimePicker();
            this.Label14 = new Framework.Controls.UserControls.TLabel();
            this.txtETU = new Framework.Controls.UserControls.TTextBox();
            this.Label13 = new Framework.Controls.UserControls.TLabel();
            this.Label7 = new Framework.Controls.UserControls.TLabel();
            this.txtATC = new Framework.Controls.UserControls.TTextBox();
            this.Label11 = new Framework.Controls.UserControls.TLabel();
            this.txtETC = new Framework.Controls.UserControls.TTextBox();
            this.txtATW = new Framework.Controls.UserControls.TTextBox();
            this.txtETW = new Framework.Controls.UserControls.TTextBox();
            this.txtETB = new Framework.Controls.UserControls.TTextBox();
            this.Label6 = new Framework.Controls.UserControls.TLabel();
            this.Label5 = new Framework.Controls.UserControls.TLabel();
            this.Label4 = new Framework.Controls.UserControls.TLabel();
            this.Label3 = new Framework.Controls.UserControls.TLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnTopclean = new Framework.Controls.UserControls.TButton();
            this.tabControl.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.TabPage1);
            this.tabControl.Controls.Add(this.TabPage2);
            this.tabControl.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tabControl.Location = new System.Drawing.Point(1, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(236, 235);
            this.tabControl.TabIndex = 8;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.txtBerthWharfE);
            this.TabPage1.Controls.Add(this.txtBerthWharfS);
            this.TabPage1.Controls.Add(this.cboBerthLoc);
            this.TabPage1.Controls.Add(this.btnF1);
            this.TabPage1.Controls.Add(this.txtWharfE);
            this.TabPage1.Controls.Add(this.txtWharfS);
            this.TabPage1.Controls.Add(this.Label10);
            this.TabPage1.Controls.Add(this.Label9);
            this.TabPage1.Controls.Add(this.txtLOA);
            this.TabPage1.Controls.Add(this.Label8);
            this.TabPage1.Controls.Add(this.Label12);
            this.TabPage1.Controls.Add(this.txtName);
            this.TabPage1.Controls.Add(this.txtJPVC);
            this.TabPage1.Controls.Add(this.Label1);
            this.TabPage1.Controls.Add(this.Label2);
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Size = new System.Drawing.Size(228, 209);
            this.TabPage1.Text = "Vessel";
            // 
            // txtBerthWharfE
            // 
            this.txtBerthWharfE.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBerthWharfE.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtBerthWharfE.Location = new System.Drawing.Point(166, 107);
            this.txtBerthWharfE.Name = "txtBerthWharfE";
            this.txtBerthWharfE.ReadOnly = true;
            this.txtBerthWharfE.Size = new System.Drawing.Size(59, 19);
            this.txtBerthWharfE.TabIndex = 15;
            this.txtBerthWharfE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBerthWharfS
            // 
            this.txtBerthWharfS.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBerthWharfS.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtBerthWharfS.Location = new System.Drawing.Point(103, 107);
            this.txtBerthWharfS.Name = "txtBerthWharfS";
            this.txtBerthWharfS.ReadOnly = true;
            this.txtBerthWharfS.Size = new System.Drawing.Size(59, 19);
            this.txtBerthWharfS.TabIndex = 14;
            this.txtBerthWharfS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cboBerthLoc
            // 
            this.cboBerthLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboBerthLoc.isBusinessItemName = "Berth Location";
            this.cboBerthLoc.isMandatory = true;
            this.cboBerthLoc.Location = new System.Drawing.Point(35, 107);
            this.cboBerthLoc.Name = "cboBerthLoc";
            this.cboBerthLoc.Size = new System.Drawing.Size(62, 19);
            this.cboBerthLoc.TabIndex = 5;
            this.cboBerthLoc.SelectedIndexChanged += new System.EventHandler(this.cboBerthLoc_SelectedIndexChanged);
            // 
            // btnF1
            // 
            this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF1.Location = new System.Drawing.Point(168, 14);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(30, 19);
            this.btnF1.TabIndex = 2;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtWharfE
            // 
            this.txtWharfE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtWharfE.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtWharfE.isBusinessItemName = "Wharf End";
            this.txtWharfE.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtWharfE.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtWharfE.Location = new System.Drawing.Point(62, 166);
            this.txtWharfE.Name = "txtWharfE";
            this.txtWharfE.Size = new System.Drawing.Size(81, 19);
            this.txtWharfE.TabIndex = 7;
            this.txtWharfE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtWharfS
            // 
            this.txtWharfS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtWharfS.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtWharfS.isBusinessItemName = "Wharf Start";
            this.txtWharfS.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtWharfS.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtWharfS.Location = new System.Drawing.Point(62, 137);
            this.txtWharfS.Name = "txtWharfS";
            this.txtWharfS.Size = new System.Drawing.Size(81, 19);
            this.txtWharfS.TabIndex = 6;
            this.txtWharfS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtWharfS.LostFocus += new System.EventHandler(this.txtWharfS_LostFocus);
            // 
            // Label10
            // 
            this.Label10.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label10.Location = new System.Drawing.Point(0, 169);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(58, 16);
            this.Label10.Text = "Wharf End";
            // 
            // Label9
            // 
            this.Label9.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label9.Location = new System.Drawing.Point(0, 140);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(61, 16);
            this.Label9.Text = "Wharf Start";
            // 
            // txtLOA
            // 
            this.txtLOA.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtLOA.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtLOA.Location = new System.Drawing.Point(62, 74);
            this.txtLOA.Name = "txtLOA";
            this.txtLOA.ReadOnly = true;
            this.txtLOA.Size = new System.Drawing.Size(81, 19);
            this.txtLOA.TabIndex = 4;
            this.txtLOA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label8
            // 
            this.Label8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label8.Location = new System.Drawing.Point(22, 77);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(28, 16);
            this.Label8.Text = "LOA";
            // 
            // Label12
            // 
            this.Label12.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label12.Location = new System.Drawing.Point(3, 110);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(35, 16);
            this.Label12.Text = "Berth";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtName.Location = new System.Drawing.Point(62, 44);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(150, 19);
            this.txtName.TabIndex = 3;
            // 
            // txtJPVC
            // 
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isBusinessItemName = "JPVC";
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.isMandatory = true;
            this.txtJPVC.Location = new System.Drawing.Point(62, 14);
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(103, 19);
            this.txtJPVC.TabIndex = 1;
            this.txtJPVC.TextChanged += new System.EventHandler(this.txtJPVC_TextChanged);
            this.txtJPVC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJPVC_KeyPress);
            this.txtJPVC.LostFocus += new System.EventHandler(this.txtJPVC_LostFocus);
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label1.Location = new System.Drawing.Point(22, 18);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(33, 13);
            this.Label1.Text = "JPVC";
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label2.Location = new System.Drawing.Point(22, 47);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(35, 13);
            this.Label2.Text = "Name";
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.txtCurATB);
            this.TabPage2.Controls.Add(this.tLabel5);
            this.TabPage2.Controls.Add(this.txtTugATU);
            this.TabPage2.Controls.Add(this.txtMooringATU);
            this.TabPage2.Controls.Add(this.tLabel1);
            this.TabPage2.Controls.Add(this.tLabel4);
            this.TabPage2.Controls.Add(this.chkPilotATU);
            this.TabPage2.Controls.Add(this.pictureBox2);
            this.TabPage2.Controls.Add(this.txtTugATB);
            this.TabPage2.Controls.Add(this.txtMooringATB);
            this.TabPage2.Controls.Add(this.tLabel3);
            this.TabPage2.Controls.Add(this.tLabel2);
            this.TabPage2.Controls.Add(this.chkPilotATB);
            this.TabPage2.Controls.Add(this.txtATU);
            this.TabPage2.Controls.Add(this.txtATB);
            this.TabPage2.Controls.Add(this.Label14);
            this.TabPage2.Controls.Add(this.txtETU);
            this.TabPage2.Controls.Add(this.Label13);
            this.TabPage2.Controls.Add(this.Label7);
            this.TabPage2.Controls.Add(this.txtATC);
            this.TabPage2.Controls.Add(this.Label11);
            this.TabPage2.Controls.Add(this.txtETC);
            this.TabPage2.Controls.Add(this.txtATW);
            this.TabPage2.Controls.Add(this.txtETW);
            this.TabPage2.Controls.Add(this.txtETB);
            this.TabPage2.Controls.Add(this.Label6);
            this.TabPage2.Controls.Add(this.Label5);
            this.TabPage2.Controls.Add(this.Label4);
            this.TabPage2.Controls.Add(this.Label3);
            this.TabPage2.Controls.Add(this.pictureBox1);
            this.TabPage2.Location = new System.Drawing.Point(4, 22);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Size = new System.Drawing.Size(228, 209);
            this.TabPage2.Text = "Schedule";
            // 
            // txtCurATB
            // 
            this.txtCurATB.Enabled = false;
            this.txtCurATB.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtCurATB.isBusinessItemName = "Cur ATB";
            this.txtCurATB.Location = new System.Drawing.Point(25, 51);
            this.txtCurATB.Name = "txtCurATB";
            this.txtCurATB.Size = new System.Drawing.Size(128, 19);
            this.txtCurATB.TabIndex = 206;
            // 
            // tLabel5
            // 
            this.tLabel5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel5.Location = new System.Drawing.Point(0, 46);
            this.tLabel5.Name = "tLabel5";
            this.tLabel5.Size = new System.Drawing.Size(25, 26);
            this.tLabel5.Text = "Cur ATB";
            // 
            // txtTugATU
            // 
            this.txtTugATU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtTugATU.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtTugATU.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtTugATU.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtTugATU.Location = new System.Drawing.Point(199, 186);
            this.txtTugATU.Name = "txtTugATU";
            this.txtTugATU.Size = new System.Drawing.Size(25, 19);
            this.txtTugATU.TabIndex = 190;
            this.txtTugATU.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMooringATU
            // 
            this.txtMooringATU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtMooringATU.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtMooringATU.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtMooringATU.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtMooringATU.Location = new System.Drawing.Point(199, 166);
            this.txtMooringATU.Name = "txtMooringATU";
            this.txtMooringATU.Size = new System.Drawing.Size(25, 19);
            this.txtMooringATU.TabIndex = 189;
            this.txtMooringATU.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(176, 190);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(23, 13);
            this.tLabel1.Text = "Tug";
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(159, 168);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(43, 13);
            this.tLabel4.Text = "Mooring";
            // 
            // chkPilotATU
            // 
            this.chkPilotATU.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.chkPilotATU.Location = new System.Drawing.Point(172, 144);
            this.chkPilotATU.Name = "chkPilotATU";
            this.chkPilotATU.Size = new System.Drawing.Size(49, 18);
            this.chkPilotATU.TabIndex = 188;
            this.chkPilotATU.Text = "Pilot";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(156, 143);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(71, 64);
            // 
            // txtTugATB
            // 
            this.txtTugATB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtTugATB.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtTugATB.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtTugATB.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtTugATB.Location = new System.Drawing.Point(199, 59);
            this.txtTugATB.Name = "txtTugATB";
            this.txtTugATB.Size = new System.Drawing.Size(25, 19);
            this.txtTugATB.TabIndex = 181;
            this.txtTugATB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMooringATB
            // 
            this.txtMooringATB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtMooringATB.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtMooringATB.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtMooringATB.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtMooringATB.Location = new System.Drawing.Point(199, 39);
            this.txtMooringATB.Name = "txtMooringATB";
            this.txtMooringATB.Size = new System.Drawing.Size(25, 19);
            this.txtMooringATB.TabIndex = 180;
            this.txtMooringATB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(176, 63);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(23, 13);
            this.tLabel3.Text = "Tug";
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(159, 41);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(43, 13);
            this.tLabel2.Text = "Mooring";
            // 
            // chkPilotATB
            // 
            this.chkPilotATB.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.chkPilotATB.Location = new System.Drawing.Point(172, 17);
            this.chkPilotATB.Name = "chkPilotATB";
            this.chkPilotATB.Size = new System.Drawing.Size(49, 18);
            this.chkPilotATB.TabIndex = 36;
            this.chkPilotATB.Text = "Pilot";
            // 
            // txtATU
            // 
            this.txtATU.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtATU.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtATU.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtATU.isBusinessItemName = "";
            this.txtATU.isMandatory = false;
            this.txtATU.Location = new System.Drawing.Point(25, 181);
            this.txtATU.Name = "txtATU";
            this.txtATU.Size = new System.Drawing.Size(128, 24);
            this.txtATU.TabIndex = 12;
            this.txtATU.Value = new System.DateTime(2008, 10, 6, 14, 53, 0, 191);
            this.txtATU.LostFocus += new System.EventHandler(this.txtATU_LostFocus);
            // 
            // txtATB
            // 
            this.txtATB.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtATB.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtATB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtATB.isBusinessItemName = "";
            this.txtATB.isMandatory = false;
            this.txtATB.Location = new System.Drawing.Point(25, 24);
            this.txtATB.Name = "txtATB";
            this.txtATB.Size = new System.Drawing.Size(128, 24);
            this.txtATB.TabIndex = 10;
            this.txtATB.Value = new System.DateTime(2008, 10, 6, 14, 53, 0, 191);
            this.txtATB.ValueChanged += new System.EventHandler(this.txtATB_ValueChanged);
            // 
            // Label14
            // 
            this.Label14.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label14.Location = new System.Drawing.Point(0, 184);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(24, 16);
            this.Label14.Text = "ATU";
            // 
            // txtETU
            // 
            this.txtETU.Enabled = false;
            this.txtETU.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtETU.Location = new System.Drawing.Point(25, 160);
            this.txtETU.Name = "txtETU";
            this.txtETU.Size = new System.Drawing.Size(128, 19);
            this.txtETU.TabIndex = 28;
            // 
            // Label13
            // 
            this.Label13.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label13.Location = new System.Drawing.Point(0, 163);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(24, 16);
            this.Label13.Text = "ETU";
            // 
            // Label7
            // 
            this.Label7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label7.Location = new System.Drawing.Point(0, 142);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(24, 16);
            this.Label7.Text = "ATC";
            // 
            // txtATC
            // 
            this.txtATC.BackColor = System.Drawing.Color.White;
            this.txtATC.Enabled = false;
            this.txtATC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtATC.Location = new System.Drawing.Point(25, 139);
            this.txtATC.Name = "txtATC";
            this.txtATC.Size = new System.Drawing.Size(128, 19);
            this.txtATC.TabIndex = 25;
            // 
            // Label11
            // 
            this.Label11.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label11.Location = new System.Drawing.Point(0, 120);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(23, 16);
            this.Label11.Text = "ETC";
            // 
            // txtETC
            // 
            this.txtETC.Enabled = false;
            this.txtETC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtETC.Location = new System.Drawing.Point(25, 117);
            this.txtETC.Name = "txtETC";
            this.txtETC.Size = new System.Drawing.Size(128, 19);
            this.txtETC.TabIndex = 23;
            // 
            // txtATW
            // 
            this.txtATW.BackColor = System.Drawing.Color.White;
            this.txtATW.Enabled = false;
            this.txtATW.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtATW.Location = new System.Drawing.Point(25, 95);
            this.txtATW.Name = "txtATW";
            this.txtATW.Size = new System.Drawing.Size(128, 19);
            this.txtATW.TabIndex = 22;
            this.txtATW.LostFocus += new System.EventHandler(this.txtATW_LostFocus);
            // 
            // txtETW
            // 
            this.txtETW.Enabled = false;
            this.txtETW.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtETW.Location = new System.Drawing.Point(25, 74);
            this.txtETW.Name = "txtETW";
            this.txtETW.Size = new System.Drawing.Size(128, 19);
            this.txtETW.TabIndex = 21;
            // 
            // txtETB
            // 
            this.txtETB.Enabled = false;
            this.txtETB.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtETB.Location = new System.Drawing.Point(25, 3);
            this.txtETB.Name = "txtETB";
            this.txtETB.Size = new System.Drawing.Size(128, 19);
            this.txtETB.TabIndex = 19;
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label6.Location = new System.Drawing.Point(0, 29);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(28, 16);
            this.Label6.Text = "ATB";
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label5.Location = new System.Drawing.Point(0, 77);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(28, 16);
            this.Label5.Text = "ETW";
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label4.Location = new System.Drawing.Point(0, 98);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(28, 16);
            this.Label4.Text = "ATW";
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label3.Location = new System.Drawing.Point(0, 6);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(28, 16);
            this.Label3.Text = "ETB";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(156, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 64);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(96, 242);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 14;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 242);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnTopclean
            // 
            this.btnTopclean.Location = new System.Drawing.Point(2, 242);
            this.btnTopclean.Name = "btnTopclean";
            this.btnTopclean.Size = new System.Drawing.Size(71, 24);
            this.btnTopclean.TabIndex = 16;
            this.btnTopclean.Text = "Top/Clean";
            this.btnTopclean.Click += new System.EventHandler(this.ActionListener);
            // 
            // HVO102
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.btnTopclean);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControl);
            this.Name = "HVO102";
            this.Text = "V/S - Vessel Information";
            this.tabControl.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.Container.TTabControl tabControl;
        internal Framework.Controls.Container.TTabPage TabPage1;
        internal Framework.Controls.Container.TTabPage TabPage2;
        internal Framework.Controls.UserControls.TButton btnF1;
        internal Framework.Controls.UserControls.TTextBox txtWharfE;
        internal Framework.Controls.UserControls.TTextBox txtWharfS;
        internal Framework.Controls.UserControls.TLabel Label10;
        internal Framework.Controls.UserControls.TLabel Label9;
        internal Framework.Controls.UserControls.TTextBox txtLOA;
        internal Framework.Controls.UserControls.TLabel Label8;
        internal Framework.Controls.UserControls.TLabel Label12;
        internal Framework.Controls.UserControls.TTextBox txtName;
        internal Framework.Controls.UserControls.TTextBox txtJPVC;
        internal Framework.Controls.UserControls.TLabel Label1;
        internal Framework.Controls.UserControls.TLabel Label2;
        internal Framework.Controls.UserControls.TLabel Label14;
        internal Framework.Controls.UserControls.TTextBox txtETU;
        internal Framework.Controls.UserControls.TLabel Label13;
        internal Framework.Controls.UserControls.TLabel Label7;
        internal Framework.Controls.UserControls.TTextBox txtATC;
        internal Framework.Controls.UserControls.TLabel Label11;
        internal Framework.Controls.UserControls.TTextBox txtETC;
        internal Framework.Controls.UserControls.TTextBox txtATW;
        internal Framework.Controls.UserControls.TTextBox txtETW;
        internal Framework.Controls.UserControls.TTextBox txtETB;
        internal Framework.Controls.UserControls.TLabel Label6;
        internal Framework.Controls.UserControls.TLabel Label5;
        internal Framework.Controls.UserControls.TLabel Label4;
        internal Framework.Controls.UserControls.TLabel Label3;
        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TButton btnCancel;
        private Framework.Controls.UserControls.TDateTimePicker txtATU;
        private Framework.Controls.UserControls.TDateTimePicker txtATB;
        internal Framework.Controls.UserControls.TCombobox cboBerthLoc;
        internal System.Windows.Forms.CheckBox chkPilotATB;
        internal Framework.Controls.UserControls.TTextBox txtTugATB;
        internal Framework.Controls.UserControls.TTextBox txtMooringATB;
        internal Framework.Controls.UserControls.TLabel tLabel3;
        internal Framework.Controls.UserControls.TLabel tLabel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        internal Framework.Controls.UserControls.TTextBox txtTugATU;
        internal Framework.Controls.UserControls.TTextBox txtMooringATU;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        internal Framework.Controls.UserControls.TLabel tLabel4;
        internal System.Windows.Forms.CheckBox chkPilotATU;
        private System.Windows.Forms.PictureBox pictureBox2;
        internal Framework.Controls.UserControls.TButton btnTopclean;
        internal Framework.Controls.UserControls.TTextBox txtCurATB;
        internal Framework.Controls.UserControls.TLabel tLabel5;
        internal Framework.Controls.UserControls.TTextBox txtBerthWharfE;
        internal Framework.Controls.UserControls.TTextBox txtBerthWharfS;
    }
}