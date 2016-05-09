namespace MOST.Common
{
    partial class HCM120001
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
            this.tLabel41 = new Framework.Controls.UserControls.TLabel();
            this.cbo_RefNo = new Framework.Controls.UserControls.TCombobox();
            this.txt_RefNo = new Framework.Controls.UserControls.TTextBox();
            this.rbtn_RefCbo = new Framework.Controls.UserControls.TRadioButton();
            this.rbtn_RefTxt = new Framework.Controls.UserControls.TRadioButton();
            this.SuspendLayout();
            // 
            // tLabel41
            // 
            this.tLabel41.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel41.Location = new System.Drawing.Point(12, 5);
            this.tLabel41.Name = "tLabel41";
            this.tLabel41.Size = new System.Drawing.Size(47, 15);
            this.tLabel41.Text = "Rf.No";
            // 
            // cbo_RefNo
            // 
            this.cbo_RefNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cbo_RefNo.isBusinessItemName = "Ref No Combobox";
            this.cbo_RefNo.isMandatory = false;
            this.cbo_RefNo.Location = new System.Drawing.Point(32, 23);
            this.cbo_RefNo.Name = "cbo_RefNo";
            this.cbo_RefNo.Size = new System.Drawing.Size(100, 19);
            this.cbo_RefNo.TabIndex = 32;
            // 
            // txt_RefNo
            // 
            this.txt_RefNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txt_RefNo.isBusinessItemName = "Ref No Input";
            this.txt_RefNo.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txt_RefNo.Location = new System.Drawing.Point(32, 48);
            this.txt_RefNo.Name = "txt_RefNo";
            this.txt_RefNo.Size = new System.Drawing.Size(100, 19);
            this.txt_RefNo.TabIndex = 34;
            // 
            // rbtn_RefCbo
            // 
            this.rbtn_RefCbo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtn_RefCbo.isBusinessItemName = "";
            this.rbtn_RefCbo.isMandatory = false;
            this.rbtn_RefCbo.Location = new System.Drawing.Point(11, 23);
            this.rbtn_RefCbo.Name = "rbtn_RefCbo";
            this.rbtn_RefCbo.Size = new System.Drawing.Size(15, 17);
            this.rbtn_RefCbo.TabIndex = 31;
            this.rbtn_RefCbo.CheckedChanged += new System.EventHandler(this.RadiobtnCheckedListener);
            // 
            // rbtn_RefTxt
            // 
            this.rbtn_RefTxt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.rbtn_RefTxt.isBusinessItemName = "";
            this.rbtn_RefTxt.isMandatory = false;
            this.rbtn_RefTxt.Location = new System.Drawing.Point(11, 48);
            this.rbtn_RefTxt.Name = "rbtn_RefTxt";
            this.rbtn_RefTxt.Size = new System.Drawing.Size(15, 17);
            this.rbtn_RefTxt.TabIndex = 33;
            this.rbtn_RefTxt.CheckedChanged += new System.EventHandler(this.RadiobtnCheckedListener);
            // 
            // HCM120001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tLabel41);
            this.Controls.Add(this.cbo_RefNo);
            this.Controls.Add(this.txt_RefNo);
            this.Controls.Add(this.rbtn_RefCbo);
            this.Controls.Add(this.rbtn_RefTxt);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Name = "HCM120001";
            this.Size = new System.Drawing.Size(154, 79);
            this.ResumeLayout(false);
        }

        #endregion

        internal Framework.Controls.UserControls.TLabel tLabel41;
        public Framework.Controls.UserControls.TCombobox cbo_RefNo;
        public Framework.Controls.UserControls.TTextBox txt_RefNo;
        internal Framework.Controls.UserControls.TRadioButton rbtn_RefCbo;
        internal Framework.Controls.UserControls.TRadioButton rbtn_RefTxt;
    }
}
