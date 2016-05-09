namespace Framework.Controls.UserControls
{
    partial class TDualListbox
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
            this.lstAvailable = new System.Windows.Forms.ListBox();
            this.cmdRemoveAll = new System.Windows.Forms.Button();
            this.cmdRemove = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdAddAll = new System.Windows.Forms.Button();
            this.lstAssigned = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstAvailable
            // 
            this.lstAvailable.Location = new System.Drawing.Point(0, 0);
            this.lstAvailable.Name = "lstAvailable";
            this.lstAvailable.Size = new System.Drawing.Size(77, 50);
            this.lstAvailable.TabIndex = 1;
            // 
            // cmdRemoveAll
            // 
            this.cmdRemoveAll.Location = new System.Drawing.Point(82, 48);
            this.cmdRemoveAll.Name = "cmdRemoveAll";
            this.cmdRemoveAll.Size = new System.Drawing.Size(34, 15);
            this.cmdRemoveAll.TabIndex = 5;
            this.cmdRemoveAll.Text = "<<";
            this.cmdRemoveAll.Click += new System.EventHandler(this.cmdRemoveAll_Click);
            // 
            // cmdRemove
            // 
            this.cmdRemove.Location = new System.Drawing.Point(82, 32);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(34, 15);
            this.cmdRemove.TabIndex = 4;
            this.cmdRemove.Text = "<";
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(82, 16);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(34, 15);
            this.cmdAdd.TabIndex = 3;
            this.cmdAdd.Text = ">";
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdAddAll
            // 
            this.cmdAddAll.Location = new System.Drawing.Point(82, 0);
            this.cmdAddAll.Name = "cmdAddAll";
            this.cmdAddAll.Size = new System.Drawing.Size(34, 15);
            this.cmdAddAll.TabIndex = 2;
            this.cmdAddAll.Text = ">>";
            this.cmdAddAll.Click += new System.EventHandler(this.cmdAddAll_Click);
            // 
            // lstAssigned
            // 
            this.lstAssigned.Location = new System.Drawing.Point(122, 0);
            this.lstAssigned.Name = "lstAssigned";
            this.lstAssigned.Size = new System.Drawing.Size(77, 50);
            this.lstAssigned.TabIndex = 6;
            // 
            // TDualListbox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.cmdRemoveAll);
            this.Controls.Add(this.cmdRemove);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.cmdAddAll);
            this.Controls.Add(this.lstAssigned);
            this.Controls.Add(this.lstAvailable);
            this.Name = "TDualListbox";
            this.Size = new System.Drawing.Size(200, 64);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstAvailable;
        private System.Windows.Forms.Button cmdRemoveAll;
        private System.Windows.Forms.Button cmdRemove;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Button cmdAddAll;
        private System.Windows.Forms.ListBox lstAssigned;
    }
}
