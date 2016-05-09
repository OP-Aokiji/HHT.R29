using MOST.Common.UserAttribute;

namespace MOST.Common
{
    partial class HCM115
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
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.btnExit = new Framework.Controls.UserControls.TButton();
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.tLabel5 = new Framework.Controls.UserControls.TLabel();
            this.btnRetrieve = new Framework.Controls.UserControls.TButton();
            this.cboPaging = new Framework.Controls.UserControls.TCombobox();
            this.btnPrev = new Framework.Controls.UserControls.TButton();
            this.btnNext = new Framework.Controls.UserControls.TButton();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(2, 44);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(234, 195);
            this.grdData.TabIndex = 6;
            // 
            // txtJPVC
            // 
            this.txtJPVC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isBusinessItemName = "JPVC";
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.Location = new System.Drawing.Point(29, 2);
            this.txtJPVC.Multiline = true;
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(92, 17);
            this.txtJPVC.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(167, 242);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(65, 24);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnF1
            // 
            this.btnF1.Location = new System.Drawing.Point(123, 2);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(36, 19);
            this.btnF1.TabIndex = 2;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // tLabel5
            // 
            this.tLabel5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel5.Location = new System.Drawing.Point(1, 3);
            this.tLabel5.Name = "tLabel5";
            this.tLabel5.Size = new System.Drawing.Size(30, 16);
            this.tLabel5.Text = "JPVC";
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Location = new System.Drawing.Point(171, 4);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(62, 34);
            this.btnRetrieve.TabIndex = 6;
            this.btnRetrieve.Text = "Retrieve";
            this.btnRetrieve.Click += new System.EventHandler(this.ActionListener);
            // 
            // cboPaging
            // 
            this.cboPaging.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboPaging.isBusinessItemName = "Paging UnclosedOpr";
            this.cboPaging.isMandatory = false;
            this.cboPaging.Location = new System.Drawing.Point(40, 22);
            this.cboPaging.Name = "cboPaging";
            this.cboPaging.Size = new System.Drawing.Size(36, 19);
            this.cboPaging.TabIndex = 4;
            this.cboPaging.SelectedIndexChanged += new System.EventHandler(this.cboPaging_SelectedIndexChanged);
            // 
            // btnPrev
            // 
            this.btnPrev.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnPrev.Location = new System.Drawing.Point(2, 22);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(35, 19);
            this.btnPrev.TabIndex = 3;
            this.btnPrev.Text = "Prev";
            this.btnPrev.Click += new System.EventHandler(this.executePaging);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnNext.Location = new System.Drawing.Point(78, 22);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(35, 19);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.executePaging);
            // 
            // HCM115
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.cboPaging);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnRetrieve);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.tLabel5);
            this.Name = "HCM115";
            this.Text = "Unclosed OPR List";
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TGrid grdData;
        private Framework.Controls.UserControls.TTextBox txtJPVC;
        private Framework.Controls.UserControls.TButton btnExit;

        // tnkytn: for test
        //[AuthAccess("000001", "G/R Search")]
        //[AuthAccess("000002", "JPVC Search")]
        public Framework.Controls.UserControls.TButton btnF1;
        internal Framework.Controls.UserControls.TLabel tLabel5;
        public Framework.Controls.UserControls.TButton btnRetrieve;
        private Framework.Controls.UserControls.TCombobox cboPaging;
        private Framework.Controls.UserControls.TButton btnPrev;
        private Framework.Controls.UserControls.TButton btnNext;
    }
}