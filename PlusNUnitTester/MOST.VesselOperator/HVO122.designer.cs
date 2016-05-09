namespace MOST.VesselOperator
{
    partial class HVO122
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
            this.btnF1 = new Framework.Controls.UserControls.TButton();
            this.Label2 = new Framework.Controls.UserControls.TLabel();
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.txtLOA = new Framework.Controls.UserControls.TTextBox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.txtATB = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtATU = new Framework.Controls.UserControls.TDateTimePicker();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
            this.txtATW = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtATC = new Framework.Controls.UserControls.TDateTimePicker();
            this.tLabel4 = new Framework.Controls.UserControls.TLabel();
            this.tLabel6 = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
            // 
            // btnF1
            // 
            this.btnF1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnF1.Location = new System.Drawing.Point(159, 29);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(35, 20);
            this.btnF1.TabIndex = 9;
            this.btnF1.Text = "F1";
            this.btnF1.Click += new System.EventHandler(this.ActionListener);
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label2.Location = new System.Drawing.Point(25, 33);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(39, 16);
            this.Label2.Text = "JPVC3";
            // 
            // txtJPVC
            // 
            this.txtJPVC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.isBusinessItemName = "3rd JPVC";
            this.txtJPVC.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtJPVC.isMandatory = true;
            this.txtJPVC.Location = new System.Drawing.Point(61, 30);
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.Size = new System.Drawing.Size(95, 19);
            this.txtJPVC.TabIndex = 8;
            this.txtJPVC.LostFocus += new System.EventHandler(this.txtJPVC_LostFocus);
            this.txtJPVC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJPVC_KeyPress);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(171, 239);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(98, 239);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 25;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtLOA
            // 
            this.txtLOA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtLOA.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtLOA.isBusinessItemName = "3rd LOA";
            this.txtLOA.isInputType = Framework.Controls.UserControls.TTextBox.InputList.Decimal;
            this.txtLOA.Location = new System.Drawing.Point(61, 55);
            this.txtLOA.Name = "txtLOA";
            this.txtLOA.Size = new System.Drawing.Size(95, 19);
            this.txtLOA.TabIndex = 11;
            this.txtLOA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(35, 58);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(24, 16);
            this.tLabel1.Text = "LOA";
            // 
            // txtATB
            // 
            this.txtATB.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtATB.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtATB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtATB.isBusinessItemName = "3rd ATB";
            this.txtATB.isMandatory = false;
            this.txtATB.Location = new System.Drawing.Point(61, 79);
            this.txtATB.Name = "txtATB";
            this.txtATB.Size = new System.Drawing.Size(128, 24);
            this.txtATB.TabIndex = 13;
            this.txtATB.Value = new System.DateTime(2008, 10, 20, 15, 4, 25, 812);
            // 
            // txtATU
            // 
            this.txtATU.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtATU.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtATU.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtATU.isBusinessItemName = "3rd ATU";
            this.txtATU.isMandatory = false;
            this.txtATU.Location = new System.Drawing.Point(61, 169);
            this.txtATU.Name = "txtATU";
            this.txtATU.Size = new System.Drawing.Size(128, 24);
            this.txtATU.TabIndex = 14;
            this.txtATU.Value = new System.DateTime(2008, 10, 20, 15, 4, 25, 421);
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(35, 115);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(28, 18);
            this.tLabel2.Text = "ATW";
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(35, 84);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(29, 18);
            this.tLabel3.Text = "ATB";
            // 
            // txtATW
            // 
            this.txtATW.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtATW.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtATW.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtATW.isBusinessItemName = "3rd ATW";
            this.txtATW.isMandatory = false;
            this.txtATW.Location = new System.Drawing.Point(61, 109);
            this.txtATW.Name = "txtATW";
            this.txtATW.Size = new System.Drawing.Size(128, 24);
            this.txtATW.TabIndex = 21;
            this.txtATW.Value = new System.DateTime(2008, 10, 20, 15, 4, 25, 812);
            // 
            // txtATC
            // 
            this.txtATC.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtATC.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtATC.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtATC.isBusinessItemName = "3rd ATC";
            this.txtATC.isMandatory = false;
            this.txtATC.Location = new System.Drawing.Point(61, 139);
            this.txtATC.Name = "txtATC";
            this.txtATC.Size = new System.Drawing.Size(128, 24);
            this.txtATC.TabIndex = 22;
            this.txtATC.Value = new System.DateTime(2008, 10, 20, 15, 4, 25, 421);
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(35, 175);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(24, 18);
            this.tLabel4.Text = "ATU";
            // 
            // tLabel6
            // 
            this.tLabel6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel6.Location = new System.Drawing.Point(35, 145);
            this.tLabel6.Name = "tLabel6";
            this.tLabel6.Size = new System.Drawing.Size(29, 18);
            this.tLabel6.Text = "ATC";
            // 
            // HVO122
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.txtATW);
            this.Controls.Add(this.txtATC);
            this.Controls.Add(this.tLabel4);
            this.Controls.Add(this.tLabel6);
            this.Controls.Add(this.txtATB);
            this.Controls.Add(this.txtATU);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.tLabel3);
            this.Controls.Add(this.txtLOA);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.Label2);
            this.Name = "HVO122";
            this.Text = "V/S - D/Banking";
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TButton btnF1;
        internal Framework.Controls.UserControls.TLabel Label2;
        internal Framework.Controls.UserControls.TTextBox txtJPVC;
        internal Framework.Controls.UserControls.TButton btnCancel;
        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TTextBox txtLOA;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        private Framework.Controls.UserControls.TDateTimePicker txtATB;
        private Framework.Controls.UserControls.TDateTimePicker txtATU;
        internal Framework.Controls.UserControls.TLabel tLabel2;
        internal Framework.Controls.UserControls.TLabel tLabel3;
        private Framework.Controls.UserControls.TDateTimePicker txtATW;
        private Framework.Controls.UserControls.TDateTimePicker txtATC;
        internal Framework.Controls.UserControls.TLabel tLabel4;
        internal Framework.Controls.UserControls.TLabel tLabel6;
    }
}