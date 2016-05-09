namespace MOST.Common
{
    partial class Test
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
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.manPowerListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.portCraneListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.grdStyleManPower = new System.Windows.Forms.DataGridTableStyle();
            this.grdTextBoxColumnItemStatus = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGrid2 = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.SuspendLayout();
            // 
            // manPowerListBindingSource
            // 
            this.manPowerListBindingSource.DataSource = typeof(MOST.Common.CommonResult.VsrList);
            // 
            // portCraneListBindingSource
            // 
            this.portCraneListBindingSource.DataSource = typeof(MOST.Common.CommonResult.VsrList);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.DataSource = this.manPowerListBindingSource;
            this.dataGrid1.Location = new System.Drawing.Point(14, 3);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(586, 64);
            this.dataGrid1.TabIndex = 0;
            this.dataGrid1.TableStyles.Add(this.grdStyleManPower);
            // 
            // grdStyleManPower
            // 
            this.grdStyleManPower.GridColumnStyles.Add(this.grdTextBoxColumnItemStatus);
            this.grdStyleManPower.MappingName = "VsrList";
            // 
            // grdTextBoxColumnItemStatus
            // 
            this.grdTextBoxColumnItemStatus.HeaderText = "ItemStatus";
            this.grdTextBoxColumnItemStatus.MappingName = "ItemStatus";
            this.grdTextBoxColumnItemStatus.Width = 100;
            // 
            // dataGrid2
            // 
            this.dataGrid2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid2.DataSource = this.portCraneListBindingSource;
            this.dataGrid2.Location = new System.Drawing.Point(14, 73);
            this.dataGrid2.Name = "dataGrid2";
            this.dataGrid2.Size = new System.Drawing.Size(586, 59);
            this.dataGrid2.TabIndex = 1;
            this.dataGrid2.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.MappingName = "VsrList";
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.dataGrid2);
            this.Controls.Add(this.dataGrid1);
            this.Menu = this.mainMenu1;
            this.Name = "Test";
            this.Text = "Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.BindingSource manPowerListBindingSource;
        private System.Windows.Forms.DataGridTableStyle grdStyleManPower;
        private System.Windows.Forms.DataGridTextBoxColumn grdTextBoxColumnItemStatus;
        private System.Windows.Forms.DataGrid dataGrid2;
        private System.Windows.Forms.BindingSource portCraneListBindingSource;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
    }
}