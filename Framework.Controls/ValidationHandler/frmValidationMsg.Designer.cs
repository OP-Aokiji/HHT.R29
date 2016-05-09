namespace Framework.Controls.ValidationHandler
{
    partial class frmValidationMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmValidationMsg));
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.lstErrorControls = new Framework.Controls.UserControls.TListBox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(141, 230);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(92, 30);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Confirm";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lstErrorControls
            // 
            this.lstErrorControls.isBusinessItemName = "";
            this.lstErrorControls.isMandatory = false;
            this.lstErrorControls.Location = new System.Drawing.Point(6, 70);
            this.lstErrorControls.Name = "lstErrorControls";
            this.lstErrorControls.Size = new System.Drawing.Size(227, 146);
            this.lstErrorControls.TabIndex = 1;
            this.lstErrorControls.SelectedIndexChanged += new System.EventHandler(this.lstErrorControls_SelectedIndexChanged);
            // 
            // tLabel1
            // 
            this.tLabel1.Location = new System.Drawing.Point(75, 33);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(158, 38);
            this.tLabel1.Text = "The following list missed value in mandatory fields";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(63, 67);
            // 
            // frmValidationMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.lstErrorControls);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tLabel1);
            this.MaximizeBox = false;
            this.Menu = null;
            this.MinimizeBox = false;
            this.Name = "frmValidationMsg";
            this.Text = "Warning";
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TButton btnOk;
        private Framework.Controls.UserControls.TListBox lstErrorControls;
        private Framework.Controls.UserControls.TLabel tLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}