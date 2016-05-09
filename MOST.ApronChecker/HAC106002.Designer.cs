namespace MOST.Common
{
    partial class HAC106002
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
            this.btnDmgLoc = new Framework.Controls.UserControls.TButton();
            this.lblDmgLoc = new Framework.Controls.UserControls.TLabel();
            this.txtDmgLoc = new Framework.Controls.UserControls.TTextBox();
            this.txtDmgQty = new Framework.Controls.UserControls.TTextBox();
            this.txtDmgMT = new Framework.Controls.UserControls.TTextBox();
            this.txtDmgM3 = new Framework.Controls.UserControls.TTextBox();
            this.lblDmg = new Framework.Controls.UserControls.TLabel();
            this.lblTitle = new Framework.Controls.UserControls.TLabel();
            this.btnClear = new Framework.Controls.UserControls.TButton();
            this.SuspendLayout();
            // 
            // btnDmgLoc
            // 
            this.txtDmgLoc.BackColor = System.Drawing.Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, 128);
            this.txtDmgLoc.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Regular);
            this.txtDmgLoc.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtDmgLoc.Location = new System.Drawing.Point(22, 39);
            this.txtDmgLoc.Multiline = true;
            this.txtDmgLoc.Name = "txtDmgLoc";
            this.txtDmgLoc.ReadOnly = true;
            this.txtDmgLoc.Size = new System.Drawing.Size(82, 17);
            this.txtDmgLoc.TabIndex = 29;
            // 
            // txtDmgQty
            // 
            this.txtDmgQty.BackColor = System.Drawing.Color.FromArgb(128, (int)byte.MaxValue, 128);
            this.txtDmgQty.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Regular);
            this.txtDmgQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtDmgQty.Location = new System.Drawing.Point(126, 21);
            this.txtDmgQty.Multiline = true;
            this.txtDmgQty.Name = "txtDmgQty";
            this.txtDmgQty.Size = new System.Drawing.Size(51, 17);
            this.txtDmgQty.TabIndex = 28;
            this.txtDmgQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDmgMT
            // 
            this.txtDmgMT.BackColor = System.Drawing.Color.FromArgb(128, (int)byte.MaxValue, 128);
            this.txtDmgMT.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Regular);
            this.txtDmgMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDmgMT.Location = new System.Drawing.Point(22, 21);
            this.txtDmgMT.Multiline = true;
            this.txtDmgMT.Name = "txtDmgMT";
            this.txtDmgMT.Size = new System.Drawing.Size(51, 17);
            this.txtDmgMT.TabIndex = 26;
            this.txtDmgMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDmgM3
            // 
            this.txtDmgM3.BackColor = System.Drawing.Color.FromArgb(128, (int)byte.MaxValue, 128);
            this.txtDmgM3.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Regular);
            this.txtDmgM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtDmgM3.Location = new System.Drawing.Point(74, 21);
            this.txtDmgM3.Multiline = true;
            this.txtDmgM3.Name = "txtDmgM3";
            this.txtDmgM3.Size = new System.Drawing.Size(51, 17);
            this.txtDmgM3.TabIndex = 27;
            this.txtDmgM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.Highlight;
            this.lblTitle.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblTitle.Location = new System.Drawing.Point(-2, -2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(187, 20);
            this.lblTitle.Text = "DISCHARGE DAMAGE INFO";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnDmgLoc
            // 
            this.btnDmgLoc.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Regular);
            this.btnDmgLoc.Location = new System.Drawing.Point(106, 39);
            this.btnDmgLoc.Name = "btnDmgLoc";
            this.btnDmgLoc.Size = new System.Drawing.Size(36, 17);
            this.btnDmgLoc.TabIndex = 30;
            this.btnDmgLoc.Text = "Loc";
            this.btnDmgLoc.Click += new System.EventHandler(this.btnDmgLoc_Click);
            // 
            // btnClear
            //
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Regular);
            this.btnClear.Location = new System.Drawing.Point(143, 39);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(36, 17);
            this.btnClear.TabIndex = 31;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblDmgLoc
            //
            this.lblDmgLoc.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Regular);
            this.lblDmgLoc.Location = new System.Drawing.Point(-1, 38);
            this.lblDmgLoc.Name = "lblDmgLoc";
            this.lblDmgLoc.Size = new System.Drawing.Size(22, 15);
            this.lblDmgLoc.Text = "Loc";
            this.lblDmg.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Regular);
            this.lblDmg.Location = new System.Drawing.Point(-1, 23);
            this.lblDmg.Name = "lblDmg";
            this.lblDmg.Size = new System.Drawing.Size(27, 15);
            this.lblDmg.Text = "Dmg";

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnDmgLoc);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblDmgLoc);
            this.Controls.Add(this.txtDmgLoc);
            this.Controls.Add(this.txtDmgQty);
            this.Controls.Add(this.txtDmgMT);
            this.Controls.Add(this.txtDmgM3);
            this.Controls.Add(this.lblDmg);
            this.Name = "HAC106002";
            this.Size = new System.Drawing.Size(184, 62);
            this.ResumeLayout(false);
        }

        #endregion

        internal Framework.Controls.UserControls.TButton btnDmgLoc;
        internal Framework.Controls.UserControls.TButton btnClear;
        internal Framework.Controls.UserControls.TLabel lblDmgLoc;
        internal Framework.Controls.UserControls.TTextBox txtDmgLoc;
        internal Framework.Controls.UserControls.TTextBox txtDmgQty;
        internal Framework.Controls.UserControls.TTextBox txtDmgMT;
        internal Framework.Controls.UserControls.TTextBox txtDmgM3;
        public Framework.Controls.UserControls.TLabel lblDmg;
        public Framework.Controls.UserControls.TLabel lblTitle;
    }
}
