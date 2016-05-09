namespace MOST.WHChecker
{
    partial class HWC101003
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
            this.tLabel9 = new Framework.Controls.UserControls.TLabel();
            this.txtHIShutLoc = new Framework.Controls.UserControls.TTextBox();
            this.btnHISetLocSOut = new Framework.Controls.UserControls.TButton();
            this.txtHISOutMT = new Framework.Controls.UserControls.TTextBox();
            this.txtHISOutM3 = new Framework.Controls.UserControls.TTextBox();
            this.txtHISOutQty = new Framework.Controls.UserControls.TTextBox();
            this.cboHIHndlSOut = new Framework.Controls.UserControls.TCombobox();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.title = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
            // 
            // tLabel9
            // 
            this.tLabel9.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular);
            this.tLabel9.Location = new System.Drawing.Point(3, 36);
            this.tLabel9.Name = "tLabel9";
            this.tLabel9.Size = new System.Drawing.Size(21, 15);
            this.tLabel9.Text = "Loc";
            // 
            // txtHIShutLoc
            // 
            this.txtHIShutLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtHIShutLoc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtHIShutLoc.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtHIShutLoc.Location = new System.Drawing.Point(28, 36);
            this.txtHIShutLoc.Multiline = true;
            this.txtHIShutLoc.Name = "txtHIShutLoc";
            this.txtHIShutLoc.ReadOnly = true;
            this.txtHIShutLoc.Size = new System.Drawing.Size(62, 17);
            this.txtHIShutLoc.TabIndex = 89;
            // 
            // btnHISetLocSOut
            // 
            this.btnHISetLocSOut.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnHISetLocSOut.Location = new System.Drawing.Point(96, 36);
            this.btnHISetLocSOut.Name = "btnHISetLocSOut";
            this.btnHISetLocSOut.Size = new System.Drawing.Size(30, 17);
            this.btnHISetLocSOut.TabIndex = 21;
            this.btnHISetLocSOut.Text = "Loc";
            this.btnHISetLocSOut.Click += new System.EventHandler(this.btnHISetLocSOut_Click);
            // 
            // txtHISOutMT
            // 
            this.txtHISOutMT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtHISOutMT.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtHISOutMT.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtHISOutMT.Location = new System.Drawing.Point(30, 17);
            this.txtHISOutMT.Multiline = true;
            this.txtHISOutMT.Name = "txtHISOutMT";
            this.txtHISOutMT.Size = new System.Drawing.Size(42, 17);
            this.txtHISOutMT.TabIndex = 18;
            this.txtHISOutMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHISOutMT.TextChanged += new System.EventHandler(this.TextActionListener);
            // 
            // txtHISOutM3
            // 
            this.txtHISOutM3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtHISOutM3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtHISOutM3.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtHISOutM3.Location = new System.Drawing.Point(78, 17);
            this.txtHISOutM3.Multiline = true;
            this.txtHISOutM3.Name = "txtHISOutM3";
            this.txtHISOutM3.Size = new System.Drawing.Size(42, 17);
            this.txtHISOutM3.TabIndex = 19;
            this.txtHISOutM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHISOutM3.TextChanged += new System.EventHandler(this.TextActionListener);
            // 
            // txtHISOutQty
            // 
            this.txtHISOutQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtHISOutQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtHISOutQty.isInputType = Framework.Controls.UserControls.TTextBox.InputList.NumberOnly;
            this.txtHISOutQty.Location = new System.Drawing.Point(126, 17);
            this.txtHISOutQty.Multiline = true;
            this.txtHISOutQty.Name = "txtHISOutQty";
            this.txtHISOutQty.Size = new System.Drawing.Size(42, 17);
            this.txtHISOutQty.TabIndex = 20;
            this.txtHISOutQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;    
            this.txtHISOutQty.TextChanged += new System.EventHandler(this.TextActionListener);
            // 
            // cboHIHndlSOut
            // 
            this.cboHIHndlSOut.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboHIHndlSOut.isBusinessItemName = "";
            this.cboHIHndlSOut.isMandatory = false;
            this.cboHIHndlSOut.Location = new System.Drawing.Point(130, 37);
            this.cboHIHndlSOut.Name = "cboHIHndlSOut";
            this.cboHIHndlSOut.Size = new System.Drawing.Size(72, 19);
            this.cboHIHndlSOut.TabIndex = 16;
            this.cboHIHndlSOut.SelectedIndexChanged += new System.EventHandler(this.cboHIHndlSOut_SelectedIndexChanged);
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(3, 19);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(26, 15);
            this.tLabel2.Text = "Shut";
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.SystemColors.WindowText;
            this.title.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular);
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(211, 14);
            this.title.Text = "SHUT OUT";
            this.title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // HWC101003
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tLabel9);
            this.Controls.Add(this.txtHIShutLoc);
            this.Controls.Add(this.txtHISOutQty);
            this.Controls.Add(this.txtHISOutM3);
            this.Controls.Add(this.txtHISOutMT);
            this.Controls.Add(this.btnHISetLocSOut);
            this.Controls.Add(this.cboHIHndlSOut);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.title);
            this.Name = "HWC101003";
            this.Size = new System.Drawing.Size(209, 63);
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TLabel tLabel9;
        internal Framework.Controls.UserControls.TTextBox txtHIShutLoc;
        internal Framework.Controls.UserControls.TButton btnHISetLocSOut;
        internal Framework.Controls.UserControls.TTextBox txtHISOutQty;
        internal Framework.Controls.UserControls.TTextBox txtHISOutMT;
        internal Framework.Controls.UserControls.TTextBox txtHISOutM3;
        internal Framework.Controls.UserControls.TCombobox cboHIHndlSOut;
        internal Framework.Controls.UserControls.TLabel tLabel2;
        internal Framework.Controls.UserControls.TLabel title;
    }
}
