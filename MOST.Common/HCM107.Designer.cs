namespace MOST.Common
{
    partial class HCM107
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.cboBLNo = new Framework.Controls.UserControls.TCombobox();
            this.lblSNBL = new Framework.Controls.UserControls.TLabel();
            this.grdLorryList = new Framework.Controls.UserControls.TGrid();
            this.btnSearch = new Framework.Controls.UserControls.TButton();
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.lblJPVC = new Framework.Controls.UserControls.TLabel();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.txtLorryNo = new Framework.Controls.UserControls.TTextBox();
            this.lblLorryNo = new Framework.Controls.UserControls.TLabel();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.pnlPS = new Framework.Controls.Container.TPanel();
            this.pnlAC = new Framework.Controls.Container.TPanel();
            this.txtSNBL = new Framework.Controls.UserControls.TTextBox();
            this.lblSNBL2 = new Framework.Controls.UserControls.TLabel();
            this.pnlPS.SuspendLayout();
            this.pnlAC.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboBLNo
            // 
            this.cboBLNo.isBusinessItemName = "";
            this.cboBLNo.isMandatory = false;
            this.cboBLNo.Location = new System.Drawing.Point(43, 3);
            this.cboBLNo.Name = "cboBLNo";
            this.cboBLNo.Size = new System.Drawing.Size(134, 23);
            this.cboBLNo.TabIndex = 4;
            // 
            // lblSNBL
            // 
            this.lblSNBL.Location = new System.Drawing.Point(4, 8);
            this.lblSNBL.Name = "lblSNBL";
            this.lblSNBL.Size = new System.Drawing.Size(22, 18);
            this.lblSNBL.Text = "BL";
            // 
            // grdLorryList
            // 
            this.grdLorryList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdLorryList.DataTable = null;
            this.grdLorryList.Location = new System.Drawing.Point(4, 81);
            this.grdLorryList.Name = "grdLorryList";
            this.grdLorryList.RowHeadersVisible = false;
            this.grdLorryList.Size = new System.Drawing.Size(225, 159);
            this.grdLorryList.TabIndex = 7;
            this.grdLorryList.DoubleClick += new System.EventHandler(this.grdLorryList_DoubleClick);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(168, 55);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(65, 24);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtJPVC
            // 
            this.txtJPVC.isBusinessItemName = "JPVC";
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.isMandatory = true;
            this.txtJPVC.Location = new System.Drawing.Point(43, 3);
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(124, 23);
            this.txtJPVC.TabIndex = 2;
            this.txtJPVC.LostFocus += new System.EventHandler(this.txtJPVC_LostFocus);
            // 
            // lblJPVC
            // 
            this.lblJPVC.Location = new System.Drawing.Point(1, 8);
            this.lblJPVC.Name = "lblJPVC";
            this.lblJPVC.Size = new System.Drawing.Size(35, 18);
            this.lblJPVC.Text = "JPVC";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(164, 242);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(93, 242);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtLorryNo
            // 
            this.txtLorryNo.isBusinessItemName = "JPVC";
            this.txtLorryNo.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtLorryNo.Location = new System.Drawing.Point(56, 56);
            this.txtLorryNo.Name = "txtLorryNo";
            this.txtLorryNo.Size = new System.Drawing.Size(110, 23);
            this.txtLorryNo.TabIndex = 5;
            // 
            // lblLorryNo
            // 
            this.lblLorryNo.Location = new System.Drawing.Point(1, 61);
            this.lblLorryNo.Name = "lblLorryNo";
            this.lblLorryNo.Size = new System.Drawing.Size(58, 18);
            this.lblLorryNo.Text = "Lorry No";
            // 
            // btnF1
            // 
            this.btnF1.Location = new System.Drawing.Point(169, 2);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(35, 24);
            this.btnF1.TabIndex = 1;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.btnF1_Click);
            // 
            // pnlPS
            // 
            this.pnlPS.Controls.Add(this.lblSNBL);
            this.pnlPS.Controls.Add(this.cboBLNo);
            this.pnlPS.Location = new System.Drawing.Point(0, 27);
            this.pnlPS.Name = "pnlPS";
            this.pnlPS.Size = new System.Drawing.Size(235, 27);
            // 
            // pnlAC
            // 
            this.pnlAC.Controls.Add(this.txtSNBL);
            this.pnlAC.Controls.Add(this.lblSNBL2);
            this.pnlAC.Location = new System.Drawing.Point(250, 27);
            this.pnlAC.Name = "pnlAC";
            this.pnlAC.Size = new System.Drawing.Size(235, 27);
            // 
            // txtSNBL
            // 
            this.txtSNBL.Enabled = false;
            this.txtSNBL.isBusinessItemName = "JPVC";
            this.txtSNBL.Location = new System.Drawing.Point(43, 3);
            this.txtSNBL.Name = "txtSNBL";
            this.txtSNBL.Size = new System.Drawing.Size(124, 23);
            this.txtSNBL.TabIndex = 13;
            // 
            // lblSNBL2
            // 
            this.lblSNBL2.Location = new System.Drawing.Point(4, 8);
            this.lblSNBL2.Name = "lblSNBL2";
            this.lblSNBL2.Size = new System.Drawing.Size(22, 18);
            this.lblSNBL2.Text = "BL";
            // 
            // HCM107
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(498, 296);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.pnlAC);
            this.Controls.Add(this.lblJPVC);
            this.Controls.Add(this.pnlPS);
            this.Controls.Add(this.grdLorryList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtLorryNo);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblLorryNo);
            this.MaximizeBox = false;
            this.Menu = null;
            this.MinimizeBox = false;
            this.Name = "HCM107";
            this.Text = "Lorry List";
            this.pnlPS.ResumeLayout(false);
            this.pnlAC.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TCombobox cboBLNo;
        private Framework.Controls.UserControls.TLabel lblSNBL;
        private Framework.Controls.UserControls.TGrid grdLorryList;
        private Framework.Controls.UserControls.TButton btnSearch;
        private Framework.Controls.UserControls.TTextBox txtJPVC;
        private Framework.Controls.UserControls.TLabel lblJPVC;
        private Framework.Controls.UserControls.TButton btnCancel;
        private Framework.Controls.UserControls.TButton btnOk;
        private Framework.Controls.UserControls.TTextBox txtLorryNo;
        private Framework.Controls.UserControls.TLabel lblLorryNo;
        private Framework.Controls.UserControls.TButton btnF1;
        private Framework.Controls.Container.TPanel pnlPS;
        private Framework.Controls.Container.TPanel pnlAC;
        private Framework.Controls.UserControls.TLabel lblSNBL2;
        private Framework.Controls.UserControls.TTextBox txtSNBL;

    }
}