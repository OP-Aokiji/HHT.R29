namespace PlusHHTUpdater
{
    partial class frmPlusLogo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlusLogo));
            this.lbCheck = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.SuspendLayout();
            // 
            // lbCheck
            // 
            this.lbCheck.Location = new System.Drawing.Point(23, 151);
            this.lbCheck.Name = "lbCheck";
            this.lbCheck.Size = new System.Drawing.Size(200, 47);
            this.lbCheck.Text = "PLUS HHT Updater is checking version";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(235, 116);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 245);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(238, 24);
            this.statusBar.Text = "Status : Version Checking";
            // 
            // frmPlusLogo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.lbCheck);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPlusLogo";
            this.Text = "PLUS HHT Updater Ver 1.0";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lbCheck;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.StatusBar statusBar;

    }
}