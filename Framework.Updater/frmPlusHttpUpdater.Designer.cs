namespace Framework.Updater
{
    partial class frmPlusHttpUpdater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlusHttpUpdater));
            this.pbTrans = new System.Windows.Forms.ProgressBar();
            this.pbTotalTrans = new System.Windows.Forms.ProgressBar();
            this.tLabel2 = new System.Windows.Forms.Label();
            this.tLabel3 = new System.Windows.Forms.Label();
            this.lblTransFile = new System.Windows.Forms.Label();
            this.lbTotalTransFiles = new System.Windows.Forms.Label();
            this.lstUpdate = new System.Windows.Forms.ListBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // pbTrans
            // 
            this.pbTrans.Location = new System.Drawing.Point(13, 175);
            this.pbTrans.Name = "pbTrans";
            this.pbTrans.Size = new System.Drawing.Size(216, 19);
            // 
            // pbTotalTrans
            // 
            this.pbTotalTrans.Location = new System.Drawing.Point(13, 213);
            this.pbTotalTrans.Name = "pbTotalTrans";
            this.pbTotalTrans.Size = new System.Drawing.Size(216, 19);
            this.pbTotalTrans.Value = 30;
            // 
            // tLabel2
            // 
            this.tLabel2.Location = new System.Drawing.Point(13, 197);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(144, 14);
            this.tLabel2.Text = "Total transfer status";
            // 
            // tLabel3
            // 
            this.tLabel3.Location = new System.Drawing.Point(13, 158);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(87, 17);
            this.tLabel3.Text = "Downloading";
            // 
            // lblTransFile
            // 
            this.lblTransFile.Location = new System.Drawing.Point(95, 158);
            this.lblTransFile.Name = "lblTransFile";
            this.lblTransFile.Size = new System.Drawing.Size(132, 14);
            this.lblTransFile.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbTotalTransFiles
            // 
            this.lbTotalTransFiles.Location = new System.Drawing.Point(142, 197);
            this.lbTotalTransFiles.Name = "lbTotalTransFiles";
            this.lbTotalTransFiles.Size = new System.Drawing.Size(85, 14);
            this.lbTotalTransFiles.Text = "0/0";
            this.lbTotalTransFiles.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lstUpdate
            // 
            this.lstUpdate.Location = new System.Drawing.Point(13, 55);
            this.lstUpdate.Name = "lstUpdate";
            this.lstUpdate.Size = new System.Drawing.Size(214, 98);
            this.lstUpdate.TabIndex = 7;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(73, 238);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 28);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(153, 238);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(235, 155);
            // 
            // frmPlusHttpUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.lblTransFile);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lstUpdate);
            this.Controls.Add(this.lbTotalTransFiles);
            this.Controls.Add(this.tLabel3);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.pbTrans);
            this.Controls.Add(this.pbTotalTrans);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPlusHttpUpdater";
            this.Text = "PLUS HHT Updater - HTTP";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbTrans;
        private System.Windows.Forms.ProgressBar pbTotalTrans;
        private System.Windows.Forms.Label tLabel2;
        private System.Windows.Forms.Label tLabel3;
        private System.Windows.Forms.Label lblTransFile;
        private System.Windows.Forms.Label lbTotalTransFiles;
        private System.Windows.Forms.ListBox lstUpdate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}