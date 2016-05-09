namespace MOST.VesselOperator
{
    partial class HVO101
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
            this.btnF2 = new Framework.Controls.UserControls.TButton();
            this.txtEqu = new Framework.Controls.UserControls.TTextBox();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.txtHatch = new Framework.Controls.UserControls.TTextBox();
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.lblJPVC = new Framework.Controls.UserControls.TLabel();
            this.btnExit = new Framework.Controls.UserControls.TButton();
            this.btnJettyOPR = new Framework.Controls.UserControls.TButton();
            this.btnVslShifting = new Framework.Controls.UserControls.TButton();
            this.btnVessel = new Framework.Controls.UserControls.TButton();
            this.btnDBanking = new Framework.Controls.UserControls.TButton();
            this.btnCargoShifting = new Framework.Controls.UserControls.TButton();
            this.btnRoadBunkering = new Framework.Controls.UserControls.TButton();
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.btnSTV = new Framework.Controls.UserControls.TButton();
            this.btnDelay = new Framework.Controls.UserControls.TButton();
            this.txtPurpCall = new Framework.Controls.UserControls.TTextBox();
            this.lblPurpCall = new Framework.Controls.UserControls.TLabel();
            this.txtVslTpNm = new Framework.Controls.UserControls.TTextBox();
            this.lblVslTpNm = new Framework.Controls.UserControls.TLabel();
            this.btnContainer = new Framework.Controls.UserControls.TButton();
            this.btnUnclosedOPR = new Framework.Controls.UserControls.TButton();
            this.btnTranshipment = new Framework.Controls.UserControls.TButton();
            this.btnSTSLiquid = new Framework.Controls.UserControls.TButton();
            this.SuspendLayout();
            // 
            // btnF2
            // 
            this.btnF2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF2.Location = new System.Drawing.Point(189, 118);
            this.btnF2.Name = "btnF2";
            this.btnF2.Size = new System.Drawing.Size(39, 19);
            this.btnF2.TabIndex = 5;
            this.btnF2.Text = "F2";
            this.btnF2.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtEqu
            // 
            this.txtEqu.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtEqu.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtEqu.Location = new System.Drawing.Point(75, 118);
            this.txtEqu.Name = "txtEqu";
            this.txtEqu.ReadOnly = true;
            this.txtEqu.Size = new System.Drawing.Size(110, 19);
            this.txtEqu.TabIndex = 4;
            // 
            // btnF1
            // 
            this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF1.Location = new System.Drawing.Point(189, 94);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(39, 19);
            this.btnF1.TabIndex = 3;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtHatch
            // 
            this.txtHatch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtHatch.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtHatch.Location = new System.Drawing.Point(75, 94);
            this.txtHatch.Name = "txtHatch";
            this.txtHatch.ReadOnly = true;
            this.txtHatch.Size = new System.Drawing.Size(110, 19);
            this.txtHatch.TabIndex = 2;
            // 
            // txtJPVC
            // 
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.Location = new System.Drawing.Point(75, 10);
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.ReadOnly = true;
            this.txtJPVC.Size = new System.Drawing.Size(96, 19);
            this.txtJPVC.TabIndex = 56;
            this.txtJPVC.TextChanged += new System.EventHandler(this.txtJPVC_TextChanged);
            // 
            // lblJPVC
            // 
            this.lblJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblJPVC.Location = new System.Drawing.Point(27, 13);
            this.lblJPVC.Name = "lblJPVC";
            this.lblJPVC.Size = new System.Drawing.Size(33, 16);
            this.lblJPVC.Text = "JPVC";
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnExit.Location = new System.Drawing.Point(183, 206);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 59);
            this.btnExit.TabIndex = 17;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnJettyOPR
            // 
            this.btnJettyOPR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnJettyOPR.Location = new System.Drawing.Point(1, 206);
            this.btnJettyOPR.Name = "btnJettyOPR";
            this.btnJettyOPR.Size = new System.Drawing.Size(53, 37);
            this.btnJettyOPR.TabIndex = 13;
            this.btnJettyOPR.Text = "Jetty OPR";
            this.btnJettyOPR.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnVslShifting
            // 
            this.btnVslShifting.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnVslShifting.Location = new System.Drawing.Point(110, 162);
            this.btnVslShifting.Name = "btnVslShifting";
            this.btnVslShifting.Size = new System.Drawing.Size(73, 22);
            this.btnVslShifting.TabIndex = 12;
            this.btnVslShifting.Text = "Vsl Shifting";
            this.btnVslShifting.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnVessel
            // 
            this.btnVessel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnVessel.Location = new System.Drawing.Point(173, 8);
            this.btnVessel.Name = "btnVessel";
            this.btnVessel.Size = new System.Drawing.Size(63, 22);
            this.btnVessel.TabIndex = 1;
            this.btnVessel.Text = "Vessel ";
            this.btnVessel.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnDBanking
            // 
            this.btnDBanking.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnDBanking.Location = new System.Drawing.Point(54, 184);
            this.btnDBanking.Name = "btnDBanking";
            this.btnDBanking.Size = new System.Drawing.Size(56, 22);
            this.btnDBanking.TabIndex = 16;
            this.btnDBanking.Text = "D/Banking";
            this.btnDBanking.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCargoShifting
            // 
            this.btnCargoShifting.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCargoShifting.Location = new System.Drawing.Point(54, 162);
            this.btnCargoShifting.Name = "btnCargoShifting";
            this.btnCargoShifting.Size = new System.Drawing.Size(56, 22);
            this.btnCargoShifting.TabIndex = 14;
            this.btnCargoShifting.Text = "Cargo SFT";
            this.btnCargoShifting.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnRoadBunkering
            // 
            this.btnRoadBunkering.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnRoadBunkering.Location = new System.Drawing.Point(110, 184);
            this.btnRoadBunkering.Name = "btnRoadBunkering";
            this.btnRoadBunkering.Size = new System.Drawing.Size(127, 22);
            this.btnRoadBunkering.TabIndex = 15;
            this.btnRoadBunkering.Text = "Road Bunkering";
            this.btnRoadBunkering.Click += new System.EventHandler(this.ActionListener);
            // 
            // contextMenu
            // 
            this.contextMenu.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Vessel Operator List";
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(26, 97);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(34, 16);
            this.tLabel1.Text = "Hatch";
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(27, 121);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(34, 16);
            this.tLabel2.Text = "EQU";
            // 
            // btnSTV
            // 
            this.btnSTV.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnSTV.Location = new System.Drawing.Point(1, 162);
            this.btnSTV.Name = "btnSTV";
            this.btnSTV.Size = new System.Drawing.Size(53, 22);
            this.btnSTV.TabIndex = 11;
            this.btnSTV.Text = "STV";
            this.btnSTV.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnDelay
            // 
            this.btnDelay.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnDelay.Location = new System.Drawing.Point(1, 184);
            this.btnDelay.Name = "btnDelay";
            this.btnDelay.Size = new System.Drawing.Size(53, 22);
            this.btnDelay.TabIndex = 10;
            this.btnDelay.Text = "Delay";
            this.btnDelay.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtPurpCall
            // 
            this.txtPurpCall.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtPurpCall.Location = new System.Drawing.Point(75, 54);
            this.txtPurpCall.Name = "txtPurpCall";
            this.txtPurpCall.ReadOnly = true;
            this.txtPurpCall.Size = new System.Drawing.Size(162, 19);
            this.txtPurpCall.TabIndex = 93;
            // 
            // lblPurpCall
            // 
            this.lblPurpCall.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblPurpCall.Location = new System.Drawing.Point(0, 57);
            this.lblPurpCall.Name = "lblPurpCall";
            this.lblPurpCall.Size = new System.Drawing.Size(81, 16);
            this.lblPurpCall.Text = "Purpose of Call";
            // 
            // txtVslTpNm
            // 
            this.txtVslTpNm.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtVslTpNm.Location = new System.Drawing.Point(75, 32);
            this.txtVslTpNm.Name = "txtVslTpNm";
            this.txtVslTpNm.ReadOnly = true;
            this.txtVslTpNm.Size = new System.Drawing.Size(162, 19);
            this.txtVslTpNm.TabIndex = 112;
            // 
            // lblVslTpNm
            // 
            this.lblVslTpNm.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblVslTpNm.Location = new System.Drawing.Point(1, 35);
            this.lblVslTpNm.Name = "lblVslTpNm";
            this.lblVslTpNm.Size = new System.Drawing.Size(69, 16);
            this.lblVslTpNm.Text = "Vessel Type";
            // 
            // btnContainer
            // 
            this.btnContainer.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnContainer.Location = new System.Drawing.Point(183, 162);
            this.btnContainer.Name = "btnContainer";
            this.btnContainer.Size = new System.Drawing.Size(54, 22);
            this.btnContainer.TabIndex = 118;
            this.btnContainer.Text = "Container";
            this.btnContainer.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnUnclosedOPR
            // 
            this.btnUnclosedOPR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnUnclosedOPR.Location = new System.Drawing.Point(1, 243);
            this.btnUnclosedOPR.Name = "btnUnclosedOPR";
            this.btnUnclosedOPR.Size = new System.Drawing.Size(182, 22);
            this.btnUnclosedOPR.TabIndex = 124;
            this.btnUnclosedOPR.Text = "Unclosed Operations";
            this.btnUnclosedOPR.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnTranshipment
            // 
            this.btnTranshipment.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnTranshipment.Location = new System.Drawing.Point(110, 206);
            this.btnTranshipment.Name = "btnTranshipment";
            this.btnTranshipment.Size = new System.Drawing.Size(73, 37);
            this.btnTranshipment.TabIndex = 130;
            this.btnTranshipment.Text = "Transhipment";
            this.btnTranshipment.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnSTSLiquid
            // 
            this.btnSTSLiquid.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnSTSLiquid.Location = new System.Drawing.Point(54, 206);
            this.btnSTSLiquid.Name = "btnSTSLiquid";
            this.btnSTSLiquid.Size = new System.Drawing.Size(56, 37);
            this.btnSTSLiquid.TabIndex = 132;
            this.btnSTSLiquid.Text = "STS\r\nfor Liquid";
            this.btnSTSLiquid.Click += new System.EventHandler(this.ActionListener);
            // 
            // HVO101
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.ContextMenu = this.contextMenu;
            this.Controls.Add(this.btnSTSLiquid);
            this.Controls.Add(this.btnTranshipment);
            this.Controls.Add(this.btnUnclosedOPR);
            this.Controls.Add(this.btnContainer);
            this.Controls.Add(this.txtVslTpNm);
            this.Controls.Add(this.txtPurpCall);
            this.Controls.Add(this.btnSTV);
            this.Controls.Add(this.btnDelay);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.txtHatch);
            this.Controls.Add(this.btnRoadBunkering);
            this.Controls.Add(this.btnF2);
            this.Controls.Add(this.txtEqu);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.lblJPVC);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnJettyOPR);
            this.Controls.Add(this.btnVslShifting);
            this.Controls.Add(this.btnVessel);
            this.Controls.Add(this.btnDBanking);
            this.Controls.Add(this.btnCargoShifting);
            this.Controls.Add(this.lblPurpCall);
            this.Controls.Add(this.lblVslTpNm);
            this.Name = "HVO101";
            this.Text = "V/S - Main";
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TButton btnF2;
        internal Framework.Controls.UserControls.TTextBox txtEqu;
        internal Framework.Controls.UserControls.TButton btnF1;
        internal Framework.Controls.UserControls.TTextBox txtHatch;
        internal Framework.Controls.UserControls.TTextBox txtJPVC;
        internal Framework.Controls.UserControls.TLabel lblJPVC;
        internal Framework.Controls.UserControls.TButton btnExit;
        internal Framework.Controls.UserControls.TButton btnJettyOPR;
        internal Framework.Controls.UserControls.TButton btnVslShifting;
        internal Framework.Controls.UserControls.TButton btnVessel;
        internal Framework.Controls.UserControls.TButton btnDBanking;
        internal Framework.Controls.UserControls.TButton btnCargoShifting;
        internal Framework.Controls.UserControls.TButton btnRoadBunkering;
        private System.Windows.Forms.ContextMenu contextMenu;
        private System.Windows.Forms.MenuItem menuItem1;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        internal Framework.Controls.UserControls.TLabel tLabel2;
        internal Framework.Controls.UserControls.TButton btnSTV;
        internal Framework.Controls.UserControls.TButton btnDelay;
        internal Framework.Controls.UserControls.TTextBox txtPurpCall;
        internal Framework.Controls.UserControls.TLabel lblPurpCall;
        internal Framework.Controls.UserControls.TTextBox txtVslTpNm;
        internal Framework.Controls.UserControls.TLabel lblVslTpNm;
        internal Framework.Controls.UserControls.TButton btnContainer;
        internal Framework.Controls.UserControls.TButton btnUnclosedOPR;
        internal Framework.Controls.UserControls.TButton btnTranshipment;
        internal Framework.Controls.UserControls.TButton btnSTSLiquid;
    }
}