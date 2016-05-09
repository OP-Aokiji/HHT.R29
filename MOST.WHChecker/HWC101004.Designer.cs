namespace MOST.WHChecker
{
    partial class HWC101004
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtHIDmgM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtHIDmgMT = new Framework.Controls.UserControls.TTextBox();
            this.txtHIDmgQty = new Framework.Controls.UserControls.TTextBox();
            this.cboHIHndlDmg = new Framework.Controls.UserControls.TCombobox();
            this.txtHIDmgLoc = new Framework.Controls.UserControls.TTextBox();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
            this.tLabel8 = new Framework.Controls.UserControls.TLabel();
            this.title = new Framework.Controls.UserControls.TLabel();
            this.btnHISetLocDmg = new Framework.Controls.UserControls.TButton();
            this.SuspendLayout();
            // 
            // txtHIDmgM3
            // 
            this.txtHIDmgM3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtHIDmgM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtHIDmgM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtHIDmgM3.Location = new System.Drawing.Point(88, 17);
            this.txtHIDmgM3.Multiline = true;
            this.txtHIDmgM3.Name = "txtHIDmgM3";
            this.txtHIDmgM3.Size = new System.Drawing.Size(44, 17);
            this.txtHIDmgM3.TabIndex = 23;
            this.txtHIDmgM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHIDmgM3.TextChanged += new System.EventHandler(this.TextActionListener);
            // 
            // txtHIDmgMT
            // 
            this.txtHIDmgMT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtHIDmgMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtHIDmgMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtHIDmgMT.Location = new System.Drawing.Point(37, 17);
            this.txtHIDmgMT.Multiline = true;
            this.txtHIDmgMT.Name = "txtHIDmgMT";
            this.txtHIDmgMT.Size = new System.Drawing.Size(44, 17);
            this.txtHIDmgMT.TabIndex = 22;
            this.txtHIDmgMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHIDmgMT.TextChanged += new System.EventHandler(this.TextActionListener);
            // 
            // txtHIDmgQty
            // 
            this.txtHIDmgQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtHIDmgQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtHIDmgQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtHIDmgQty.Location = new System.Drawing.Point(138, 17);
            this.txtHIDmgQty.Multiline = true;
            this.txtHIDmgQty.Name = "txtHIDmgQty";
            this.txtHIDmgQty.Size = new System.Drawing.Size(44, 17);
            this.txtHIDmgQty.TabIndex = 24;
            this.txtHIDmgQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHIDmgQty.TextChanged += new System.EventHandler(this.TextActionListener);
            // 
            // cboHIHndlDmg
            // 
            this.cboHIHndlDmg.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboHIHndlDmg.isBusinessItemName = "";
            this.cboHIHndlDmg.isMandatory = false;
            this.cboHIHndlDmg.Location = new System.Drawing.Point(138, 36);
            this.cboHIHndlDmg.Name = "cboHIHndlDmg";
            this.cboHIHndlDmg.Size = new System.Drawing.Size(73, 19);
            this.cboHIHndlDmg.TabIndex = 17;
            this.cboHIHndlDmg.SelectedIndexChanged += new System.EventHandler(this.cboHIHndlDmg_SelectedIndexChanged);
            // 
            // txtHIDmgLoc
            // 
            this.txtHIDmgLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtHIDmgLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtHIDmgLoc.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtHIDmgLoc.Location = new System.Drawing.Point(38, 37);
            this.txtHIDmgLoc.Multiline = true;
            this.txtHIDmgLoc.Name = "txtHIDmgLoc";
            this.txtHIDmgLoc.ReadOnly = true;
            this.txtHIDmgLoc.Size = new System.Drawing.Size(62, 17);
            this.txtHIDmgLoc.TabIndex = 125;
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(3, 17);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(28, 15);
            this.tLabel3.Text = "Dmg";
            // 
            // tLabel8
            // 
            this.tLabel8.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular);
            this.tLabel8.Location = new System.Drawing.Point(3, 36);
            this.tLabel8.Name = "tLabel8";
            this.tLabel8.Size = new System.Drawing.Size(23, 15);
            this.tLabel8.Text = "Loc";
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.SystemColors.WindowText;
            this.title.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular);
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(219, 15);
            this.title.Text = "DAMAGED";
            this.title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnHISetLocDmg
            // 
            this.btnHISetLocDmg.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnHISetLocDmg.Location = new System.Drawing.Point(104, 37);
            this.btnHISetLocDmg.Name = "btnHISetLocDmg";
            this.btnHISetLocDmg.Size = new System.Drawing.Size(30, 17);
            this.btnHISetLocDmg.TabIndex = 128;
            this.btnHISetLocDmg.Text = "Loc";
            this.btnHISetLocDmg.Click += new System.EventHandler(this.btnHISetLocDmg_Click);
            // 
            // HWC101004
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnHISetLocDmg);
            this.Controls.Add(this.title);
            this.Controls.Add(this.txtHIDmgM3);
            this.Controls.Add(this.txtHIDmgMT);
            this.Controls.Add(this.txtHIDmgQty);
            this.Controls.Add(this.cboHIHndlDmg);
            this.Controls.Add(this.txtHIDmgLoc);
            this.Controls.Add(this.tLabel3);
            this.Controls.Add(this.tLabel8);
            this.Name = "HWC101004";
            this.Size = new System.Drawing.Size(217, 58);
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TTextBox txtHIDmgM3;
        internal Framework.Controls.UserControls.TTextBox txtHIDmgMT;
        internal Framework.Controls.UserControls.TTextBox txtHIDmgQty;
        internal Framework.Controls.UserControls.TCombobox cboHIHndlDmg;
        internal Framework.Controls.UserControls.TTextBox txtHIDmgLoc;
        internal Framework.Controls.UserControls.TLabel tLabel8;
        internal Framework.Controls.UserControls.TLabel tLabel3;
        internal Framework.Controls.UserControls.TLabel title;
        internal Framework.Controls.UserControls.TButton btnHISetLocDmg;
    }
}
