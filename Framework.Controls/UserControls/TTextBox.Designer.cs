namespace Framework.Controls.UserControls
{
    partial class TTextBox
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
            this.SuspendLayout();
            // 
            // TTextBox
            // 
            this.GotFocus += new System.EventHandler(this.TTextBox_GotFocus);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TTextBox_KeyUp);
            //this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TTextBox_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TTextBox_KeyPress);
            this.LostFocus += new System.EventHandler(this.TTextBox_LostFocus);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
