namespace MOST.Common
{
    partial class HCM120002
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
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.txt_Remark = new Framework.Controls.UserControls.TTextBox();
            this.SuspendLayout();
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(2, 5);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(42, 17);
            this.tLabel1.Text = "Remark";
            // 
            // txt_Remark
            // 
            this.txt_Remark.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txt_Remark.isBusinessItemName = "Ref No Input";
            this.txt_Remark.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txt_Remark.Location = new System.Drawing.Point(46, 3);
            this.txt_Remark.Name = "txt_Remark";
            this.txt_Remark.Size = new System.Drawing.Size(107, 19);
            this.txt_Remark.TabIndex = 40;
            // 
            // HCM120002
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.txt_Remark);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Name = "HCM120002";
            this.Size = new System.Drawing.Size(158, 27);
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TLabel tLabel1;
        public Framework.Controls.UserControls.TTextBox txt_Remark;
    }
}
