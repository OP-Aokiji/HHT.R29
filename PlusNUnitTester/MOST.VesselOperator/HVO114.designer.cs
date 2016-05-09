namespace MOST.VesselOperator
{
    partial class HVO114
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
            this.lblJPVC = new Framework.Controls.UserControls.TLabel();
            this.grdData = new Framework.Controls.UserControls.TGrid();
            this.btnDelete = new Framework.Controls.UserControls.TButton();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.btnUpdate = new Framework.Controls.UserControls.TButton();
            this.btnAdd = new Framework.Controls.UserControls.TButton();
            this.SuspendLayout();
            // 
            // txtJPVC
            // 
            this.txtJPVC.Enabled = false;
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isBusinessItemName = "JPVC";
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.isMandatory = true;
            this.txtJPVC.Location = new System.Drawing.Point(36, 3);
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(128, 19);
            this.txtJPVC.TabIndex = 100;
            // 
            // lblJPVC
            // 
            this.lblJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblJPVC.Location = new System.Drawing.Point(7, 6);
            this.lblJPVC.Name = "lblJPVC";
            this.lblJPVC.Size = new System.Drawing.Size(34, 15);
            this.lblJPVC.Text = "JPVC";
            // 
            // grdData
            // 
            this.grdData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdData.IsDirty = false;
            this.grdData.Location = new System.Drawing.Point(3, 52);
            this.grdData.Name = "grdData";
            this.grdData.RowHeadersVisible = false;
            this.grdData.Size = new System.Drawing.Size(232, 185);
            this.grdData.TabIndex = 7;
            this.grdData.DoubleClick += new System.EventHandler(this.grdData_DoubleClick);
            this.grdData.CurrentCellChanged += new System.EventHandler(this.grdData_CurrentCellChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(145, 29);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(65, 20);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(170, 242);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnOk.Location = new System.Drawing.Point(99, 242);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(74, 29);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(65, 20);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 29);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(65, 20);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Input";
            this.btnAdd.Click += new System.EventHandler(this.ActionListener);
            // 
            // HVO114
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.lblJPVC);
            this.Name = "HVO114";
            this.Text = "V/S - Double Banking List";
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TTextBox txtJPVC;
        private Framework.Controls.UserControls.TLabel lblJPVC;
        private Framework.Controls.UserControls.TGrid grdData;
        internal Framework.Controls.UserControls.TButton btnDelete;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TButton btnUpdate;
        internal Framework.Controls.UserControls.TButton btnAdd;
    }
}