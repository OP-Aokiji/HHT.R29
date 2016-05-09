namespace Framework.Common.ExceptionHandler
{
    partial class frmExceptionMsg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExceptionMsg));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.lbMessage = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtDetailMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbMessage
            // 
            this.lbMessage.Location = new System.Drawing.Point(72, 12);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(157, 67);
            this.lbMessage.Text = "Error Message";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(141, 253);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 33);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Program Exit";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(48, 253);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(87, 33);
            this.btnContinue.TabIndex = 4;
            this.btnContinue.Text = "Continue";
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(63, 67);
            // 
            // txtDetailMessage
            // 
            this.txtDetailMessage.Location = new System.Drawing.Point(11, 89);
            this.txtDetailMessage.Multiline = true;
            this.txtDetailMessage.Name = "txtDetailMessage";
            this.txtDetailMessage.Size = new System.Drawing.Size(219, 158);
            this.txtDetailMessage.TabIndex = 8;
            this.txtDetailMessage.Text = "textBox1";
            // 
            // frmExceptionMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.txtDetailMessage);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbMessage);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnContinue);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExceptionMsg";
            this.Text = "Error Message";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtDetailMessage;
    }
}