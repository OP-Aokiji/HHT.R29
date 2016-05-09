namespace MOST.WHChecker
{
    partial class HWC101006
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtBL = new Framework.Controls.UserControls.TTextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 20);
            this.label3.Text = "BL";
            // 
            // txtBL
            // 
            this.txtBL.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBL.Location = new System.Drawing.Point(32, 3);
            this.txtBL.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtBL.Name = "txtBL";
            this.txtBL.Size = new System.Drawing.Size(130, 19);
            this.txtBL.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(167, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(37, 20);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "F1";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // HWC101006
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtBL);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Name = "HWC101006";
            this.Size = new System.Drawing.Size(210, 25);
            this.ResumeLayout(false);   
        }

        #endregion

        private System.Windows.Forms.Label label3;
        public Framework.Controls.UserControls.TTextBox txtBL;
        private System.Windows.Forms.Button btnSearch;
    }
}
