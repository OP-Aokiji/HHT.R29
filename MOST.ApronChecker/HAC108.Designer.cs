using MOST.Common.UserAttribute;

namespace MOST.ApronChecker
{
    partial class HAC108
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
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.lblJPVC = new Framework.Controls.UserControls.TLabel();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.lblActDelv = new Framework.Controls.UserControls.TLabel();
            this.btnF2 = new Framework.Controls.UserControls.TButton();
            this.txtFwdAgent = new Framework.Controls.UserControls.TTextBox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.cboActDelv = new Framework.Controls.UserControls.TCombobox();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnRetrieve = new Framework.Controls.UserControls.TButton();
            this.cboHatch = new Framework.Controls.UserControls.TCombobox();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
            this.cboShift = new Framework.Controls.UserControls.TCombobox();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.cboBL = new Framework.Controls.UserControls.TCombobox();
            this.tLabel4 = new Framework.Controls.UserControls.TLabel();
            this.cboAmntMode = new Framework.Controls.UserControls.TCombobox();
            this.txtDoc = new Framework.Controls.UserControls.TTextBox();
            this.tLabel5 = new Framework.Controls.UserControls.TLabel();
            this.txtAct = new Framework.Controls.UserControls.TTextBox();
            this.tLabel6 = new Framework.Controls.UserControls.TLabel();
            this.txtBal = new Framework.Controls.UserControls.TTextBox();
            this.tLabel7 = new Framework.Controls.UserControls.TLabel();
            this.tLabel8 = new Framework.Controls.UserControls.TLabel();
            this.cboPaging = new Framework.Controls.UserControls.TCombobox();
            this.btnPrev = new Framework.Controls.UserControls.TButton();
            this.btnNext = new Framework.Controls.UserControls.TButton();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(2, 108);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(234, 118);
            this.grdData.TabIndex = 24;
            // 
            // txtJPVC
            // 
            this.txtJPVC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isBusinessItemName = "JPVC";
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.isMandatory = true;
            this.txtJPVC.Location = new System.Drawing.Point(55, 2);
            this.txtJPVC.Multiline = true;
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(114, 17);
            this.txtJPVC.TabIndex = 1;
            this.txtJPVC.TextChanged += new System.EventHandler(this.txtJPVC_TextChanged);
            this.txtJPVC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJPVC_KeyPress);
            this.txtJPVC.LostFocus += new System.EventHandler(this.txtJPVC_LostFocus);
            // 
            // lblJPVC
            // 
            this.lblJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblJPVC.Location = new System.Drawing.Point(7, 5);
            this.lblJPVC.Name = "lblJPVC";
            this.lblJPVC.Size = new System.Drawing.Size(39, 14);
            this.lblJPVC.Text = "JPVC";
            // 
            // btnF1
            // 
            this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF1.Location = new System.Drawing.Point(174, 2);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(35, 18);
            this.btnF1.TabIndex = 2;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // lblActDelv
            // 
            this.lblActDelv.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblActDelv.Location = new System.Drawing.Point(8, 66);
            this.lblActDelv.Name = "lblActDelv";
            this.lblActDelv.Size = new System.Drawing.Size(45, 15);
            this.lblActDelv.Text = "Act Delv";
            // 
            // btnF2
            // 
            this.btnF2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF2.Location = new System.Drawing.Point(174, 22);
            this.btnF2.Name = "btnF2";
            this.btnF2.Size = new System.Drawing.Size(35, 18);
            this.btnF2.TabIndex = 5;
            this.btnF2.Text = "F2";
            this.btnF2.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtFwdAgent
            // 
            this.txtFwdAgent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtFwdAgent.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtFwdAgent.isBusinessItemName = "F.Agent";
            this.txtFwdAgent.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtFwdAgent.Location = new System.Drawing.Point(55, 21);
            this.txtFwdAgent.Multiline = true;
            this.txtFwdAgent.Name = "txtFwdAgent";
            this.txtFwdAgent.Size = new System.Drawing.Size(114, 17);
            this.txtFwdAgent.TabIndex = 4;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(7, 23);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(46, 15);
            this.tLabel1.Text = "F.Agent";
            // 
            // cboActDelv
            // 
            this.cboActDelv.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboActDelv.isBusinessItemName = "Act Delv";
            this.cboActDelv.isMandatory = false;
            this.cboActDelv.Location = new System.Drawing.Point(56, 62);
            this.cboActDelv.Name = "cboActDelv";
            this.cboActDelv.Size = new System.Drawing.Size(81, 19);
            this.cboActDelv.TabIndex = 9;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(167, 248);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 20);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "Exit";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Location = new System.Drawing.Point(3, 85);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(60, 19);
            this.btnRetrieve.TabIndex = 22;
            this.btnRetrieve.Text = "Retrieve";
            this.btnRetrieve.Click += new System.EventHandler(this.ActionListener);
            // 
            // cboHatch
            // 
            this.cboHatch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboHatch.isBusinessItemName = "Hatch";
            this.cboHatch.isMandatory = false;
            this.cboHatch.Location = new System.Drawing.Point(179, 41);
            this.cboHatch.Name = "cboHatch";
            this.cboHatch.Size = new System.Drawing.Size(53, 19);
            this.cboHatch.TabIndex = 18;
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(143, 44);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(32, 16);
            this.tLabel3.Text = "Hatch";
            this.tLabel3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cboShift
            // 
            this.cboShift.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboShift.isBusinessItemName = "Shift";
            this.cboShift.isMandatory = false;
            this.cboShift.Location = new System.Drawing.Point(179, 62);
            this.cboShift.Name = "cboShift";
            this.cboShift.Size = new System.Drawing.Size(53, 19);
            this.cboShift.TabIndex = 14;
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(143, 66);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(26, 16);
            this.tLabel2.Text = "Shift";
            this.tLabel2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cboBL
            // 
            this.cboBL.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboBL.isBusinessItemName = "B/L";
            this.cboBL.isMandatory = false;
            this.cboBL.Location = new System.Drawing.Point(55, 40);
            this.cboBL.Name = "cboBL";
            this.cboBL.Size = new System.Drawing.Size(82, 19);
            this.cboBL.TabIndex = 32;
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(7, 44);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(22, 15);
            this.tLabel4.Text = "B/L";
            // 
            // cboAmntMode
            // 
            this.cboAmntMode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboAmntMode.isBusinessItemName = "Amount mode";
            this.cboAmntMode.isMandatory = false;
            this.cboAmntMode.Location = new System.Drawing.Point(62, 247);
            this.cboAmntMode.Name = "cboAmntMode";
            this.cboAmntMode.Size = new System.Drawing.Size(46, 19);
            this.cboAmntMode.TabIndex = 27;
            this.cboAmntMode.SelectedIndexChanged += new System.EventHandler(this.cboAmntMode_SelectedIndexChanged);
            // 
            // txtDoc
            // 
            this.txtDoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDoc.isBusinessItemName = "Total Doc Amount";
            this.txtDoc.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtDoc.Location = new System.Drawing.Point(23, 228);
            this.txtDoc.Multiline = true;
            this.txtDoc.Name = "txtDoc";
            this.txtDoc.ReadOnly = true;
            this.txtDoc.Size = new System.Drawing.Size(58, 17);
            this.txtDoc.TabIndex = 28;
            // 
            // tLabel5
            // 
            this.tLabel5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel5.Location = new System.Drawing.Point(0, 230);
            this.tLabel5.Name = "tLabel5";
            this.tLabel5.Size = new System.Drawing.Size(23, 15);
            this.tLabel5.Text = "Doc";
            // 
            // txtAct
            // 
            this.txtAct.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtAct.isBusinessItemName = "Total Act Amount";
            this.txtAct.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtAct.Location = new System.Drawing.Point(102, 228);
            this.txtAct.Multiline = true;
            this.txtAct.Name = "txtAct";
            this.txtAct.ReadOnly = true;
            this.txtAct.Size = new System.Drawing.Size(58, 17);
            this.txtAct.TabIndex = 29;
            // 
            // tLabel6
            // 
            this.tLabel6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel6.Location = new System.Drawing.Point(82, 230);
            this.tLabel6.Name = "tLabel6";
            this.tLabel6.Size = new System.Drawing.Size(19, 15);
            this.tLabel6.Text = "Act";
            // 
            // txtBal
            // 
            this.txtBal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBal.isBusinessItemName = "Total Bal Amount";
            this.txtBal.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtBal.Location = new System.Drawing.Point(179, 228);
            this.txtBal.Multiline = true;
            this.txtBal.Name = "txtBal";
            this.txtBal.ReadOnly = true;
            this.txtBal.Size = new System.Drawing.Size(58, 17);
            this.txtBal.TabIndex = 29;
            // 
            // tLabel7
            // 
            this.tLabel7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel7.Location = new System.Drawing.Point(161, 230);
            this.tLabel7.Name = "tLabel7";
            this.tLabel7.Size = new System.Drawing.Size(19, 15);
            this.tLabel7.Text = "Bal";
            // 
            // tLabel8
            // 
            this.tLabel8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel8.Location = new System.Drawing.Point(0, 249);
            this.tLabel8.Name = "tLabel8";
            this.tLabel8.Size = new System.Drawing.Size(62, 17);
            this.tLabel8.Text = "MT/M3/QTY";
            // 
            // cboPaging
            // 
            this.cboPaging.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboPaging.isBusinessItemName = "Paging Discharging";
            this.cboPaging.isMandatory = false;
            this.cboPaging.Location = new System.Drawing.Point(163, 85);
            this.cboPaging.Name = "cboPaging";
            this.cboPaging.Size = new System.Drawing.Size(36, 19);
            this.cboPaging.TabIndex = 5;
            this.cboPaging.SelectedIndexChanged += new System.EventHandler(this.cboPaging_SelectedIndexChanged);
            // 
            // btnPrev
            // 
            this.btnPrev.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnPrev.Location = new System.Drawing.Point(125, 85);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(35, 19);
            this.btnPrev.TabIndex = 4;
            this.btnPrev.Text = "Prev";
            this.btnPrev.Click += new System.EventHandler(this.executePaging);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnNext.Location = new System.Drawing.Point(201, 85);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(35, 19);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.executePaging);
            // 
            // HAC108
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.cboPaging);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.tLabel8);
            this.Controls.Add(this.txtBal);
            this.Controls.Add(this.txtAct);
            this.Controls.Add(this.txtDoc);
            this.Controls.Add(this.cboAmntMode);
            this.Controls.Add(this.cboBL);
            this.Controls.Add(this.tLabel4);
            this.Controls.Add(this.cboHatch);
            this.Controls.Add(this.tLabel3);
            this.Controls.Add(this.cboShift);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.btnRetrieve);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cboActDelv);
            this.Controls.Add(this.btnF2);
            this.Controls.Add(this.txtFwdAgent);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.lblActDelv);
            this.Controls.Add(this.lblJPVC);
            this.Controls.Add(this.tLabel7);
            this.Controls.Add(this.tLabel6);
            this.Controls.Add(this.tLabel5);
            this.KeyPreview = true;
            this.Name = "HAC108";
            this.Text = "A/C - List of Discharging";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HAC108_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TGrid grdData;
        private Framework.Controls.UserControls.TTextBox txtJPVC;
        private Framework.Controls.UserControls.TLabel lblJPVC;
        private Framework.Controls.UserControls.TLabel lblActDelv;
        public Framework.Controls.UserControls.TButton btnF1;
        public Framework.Controls.UserControls.TButton btnF2;
        private Framework.Controls.UserControls.TTextBox txtFwdAgent;
        private Framework.Controls.UserControls.TLabel tLabel1;
        private Framework.Controls.UserControls.TCombobox cboActDelv;
        internal Framework.Controls.UserControls.TButton btnCancel;
        public Framework.Controls.UserControls.TButton btnRetrieve;
        private Framework.Controls.UserControls.TCombobox cboHatch;
        private Framework.Controls.UserControls.TLabel tLabel3;
        private Framework.Controls.UserControls.TCombobox cboShift;
        private Framework.Controls.UserControls.TLabel tLabel2;
        private Framework.Controls.UserControls.TCombobox cboBL;
        private Framework.Controls.UserControls.TLabel tLabel4;
        private Framework.Controls.UserControls.TCombobox cboAmntMode;
        private Framework.Controls.UserControls.TTextBox txtDoc;
        private Framework.Controls.UserControls.TLabel tLabel5;
        private Framework.Controls.UserControls.TTextBox txtAct;
        private Framework.Controls.UserControls.TLabel tLabel6;
        private Framework.Controls.UserControls.TTextBox txtBal;
        private Framework.Controls.UserControls.TLabel tLabel7;
        private Framework.Controls.UserControls.TLabel tLabel8;
        private Framework.Controls.UserControls.TCombobox cboPaging;
        private Framework.Controls.UserControls.TButton btnPrev;
        private Framework.Controls.UserControls.TButton btnNext;        
    }
}