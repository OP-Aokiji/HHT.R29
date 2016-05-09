namespace MOST.VesselOperator
{
    partial class HVO121
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
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.Label6 = new Framework.Controls.UserControls.TLabel();
            this.btnExit = new Framework.Controls.UserControls.TButton();
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
            // 
            // txtJPVC
            // 
            this.txtJPVC.Enabled = false;
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isMandatory = true;
            this.txtJPVC.Location = new System.Drawing.Point(53, 5);
            this.txtJPVC.Multiline = true;
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(122, 17);
            this.txtJPVC.TabIndex = 1;
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label6.Location = new System.Drawing.Point(3, 6);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(33, 16);
            this.Label6.Text = "JPVC";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(167, 238);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(65, 24);
            this.btnExit.TabIndex = 25;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(3, 62);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(232, 167);
            this.grdData.TabIndex = 20;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(3, 43);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(77, 16);
            this.tLabel1.Text = "Dry/Break Bulk";
            // 
            // HVO121
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.Label6);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.Name = "HVO121";
            this.Text = "V/S - Top/Clean";
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TTextBox txtJPVC;
        internal Framework.Controls.UserControls.TLabel Label6;
        internal Framework.Controls.UserControls.TButton btnExit;
        private Framework.Controls.UserControls.TGrid grdData;
        internal Framework.Controls.UserControls.TLabel tLabel1;

    }
}