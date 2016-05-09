namespace MOST.VesselOperator
{
    partial class HVO106001
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
            this.btnOk = new Framework.Controls.UserControls.TButton();
            this.btnCancel = new Framework.Controls.UserControls.TButton();
            this.txtHoseOnDt = new Framework.Controls.UserControls.TDateTimePicker();
            this.tLabel10 = new Framework.Controls.UserControls.TLabel();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.txtHoseOffDt = new Framework.Controls.UserControls.TDateTimePicker();
            this.tLabel2 = new Framework.Controls.UserControls.TLabel();
            this.txtCommenceDt = new Framework.Controls.UserControls.TDateTimePicker();
            this.tLabel3 = new Framework.Controls.UserControls.TLabel();
            this.txtCompleteDt = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtJPVC = new Framework.Controls.UserControls.TTextBox();
            this.tLabel9 = new Framework.Controls.UserControls.TLabel();
            this.txtOpeTpNm = new Framework.Controls.UserControls.TTextBox();
            this.tLabel4 = new Framework.Controls.UserControls.TLabel();
            this.txtPrevHoseOn = new Framework.Controls.UserControls.TTextBox();
            this.tLabel5 = new Framework.Controls.UserControls.TLabel();
            this.txtPrevCommence = new Framework.Controls.UserControls.TTextBox();
            this.tLabel6 = new Framework.Controls.UserControls.TLabel();
            this.txtPrevComplete = new Framework.Controls.UserControls.TTextBox();
            this.tLabel7 = new Framework.Controls.UserControls.TLabel();
            this.txtPrevHoseOff = new Framework.Controls.UserControls.TTextBox();
            this.tLabel8 = new Framework.Controls.UserControls.TLabel();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(89, 242);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.ActionListener);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(161, 242);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.ActionListener);
            // 
            // txtHoseOnDt
            // 
            this.txtHoseOnDt.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtHoseOnDt.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtHoseOnDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtHoseOnDt.isBusinessItemName = "Hose On";
            this.txtHoseOnDt.isMandatory = false;
            this.txtHoseOnDt.Location = new System.Drawing.Point(80, 65);
            this.txtHoseOnDt.Name = "txtHoseOnDt";
            this.txtHoseOnDt.Size = new System.Drawing.Size(130, 24);
            this.txtHoseOnDt.TabIndex = 2;
            this.txtHoseOnDt.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 815);
            // 
            // tLabel10
            // 
            this.tLabel10.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel10.Location = new System.Drawing.Point(32, 70);
            this.tLabel10.Name = "tLabel10";
            this.tLabel10.Size = new System.Drawing.Size(44, 19);
            this.tLabel10.Text = "Hose on";
            // 
            // tLabel1
            // 
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel1.Location = new System.Drawing.Point(31, 215);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(45, 19);
            this.tLabel1.Text = "Hose off";
            // 
            // txtHoseOffDt
            // 
            this.txtHoseOffDt.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtHoseOffDt.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtHoseOffDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtHoseOffDt.isBusinessItemName = "Hose off";
            this.txtHoseOffDt.isMandatory = false;
            this.txtHoseOffDt.Location = new System.Drawing.Point(81, 210);
            this.txtHoseOffDt.Name = "txtHoseOffDt";
            this.txtHoseOffDt.Size = new System.Drawing.Size(130, 24);
            this.txtHoseOffDt.TabIndex = 5;
            this.txtHoseOffDt.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 815);
            // 
            // tLabel2
            // 
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel2.Location = new System.Drawing.Point(22, 118);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(58, 19);
            this.tLabel2.Text = "Commence";
            // 
            // txtCommenceDt
            // 
            this.txtCommenceDt.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtCommenceDt.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtCommenceDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtCommenceDt.isBusinessItemName = "Commence";
            this.txtCommenceDt.isMandatory = false;
            this.txtCommenceDt.Location = new System.Drawing.Point(81, 113);
            this.txtCommenceDt.Name = "txtCommenceDt";
            this.txtCommenceDt.Size = new System.Drawing.Size(130, 24);
            this.txtCommenceDt.TabIndex = 3;
            this.txtCommenceDt.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 815);
            // 
            // tLabel3
            // 
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel3.Location = new System.Drawing.Point(30, 166);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(50, 19);
            this.tLabel3.Text = "Complete";
            // 
            // txtCompleteDt
            // 
            this.txtCompleteDt.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtCompleteDt.CustomFormat = "dd/MM/yyyy HH:mm";
            this.txtCompleteDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtCompleteDt.isBusinessItemName = "Complete";
            this.txtCompleteDt.isMandatory = false;
            this.txtCompleteDt.Location = new System.Drawing.Point(81, 161);
            this.txtCompleteDt.Name = "txtCompleteDt";
            this.txtCompleteDt.Size = new System.Drawing.Size(130, 24);
            this.txtCompleteDt.TabIndex = 4;
            this.txtCompleteDt.Value = new System.DateTime(2008, 10, 7, 14, 35, 0, 815);
            // 
            // txtJPVC
            // 
            this.txtJPVC.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtJPVC.Location = new System.Drawing.Point(80, 4);
            this.txtJPVC.Multiline = true;
            this.txtJPVC.Name = "txtJPVC";
            this.txtJPVC.ReadOnly = true;
            this.txtJPVC.Size = new System.Drawing.Size(130, 17);
            this.txtJPVC.TabIndex = 160;
            // 
            // tLabel9
            // 
            this.tLabel9.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel9.Location = new System.Drawing.Point(43, 5);
            this.tLabel9.Name = "tLabel9";
            this.tLabel9.Size = new System.Drawing.Size(31, 15);
            this.tLabel9.Text = "JPVC";
            // 
            // txtOpeTpNm
            // 
            this.txtOpeTpNm.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtOpeTpNm.Location = new System.Drawing.Point(80, 23);
            this.txtOpeTpNm.Multiline = true;
            this.txtOpeTpNm.Name = "txtOpeTpNm";
            this.txtOpeTpNm.ReadOnly = true;
            this.txtOpeTpNm.Size = new System.Drawing.Size(130, 17);
            this.txtOpeTpNm.TabIndex = 163;
            // 
            // tLabel4
            // 
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel4.Location = new System.Drawing.Point(1, 24);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(79, 15);
            this.tLabel4.Text = "Operation Type";
            // 
            // txtPrevHoseOn
            // 
            this.txtPrevHoseOn.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtPrevHoseOn.isBusinessItemName = "Prev. Hose On";
            this.txtPrevHoseOn.Location = new System.Drawing.Point(80, 46);
            this.txtPrevHoseOn.Multiline = true;
            this.txtPrevHoseOn.Name = "txtPrevHoseOn";
            this.txtPrevHoseOn.ReadOnly = true;
            this.txtPrevHoseOn.Size = new System.Drawing.Size(130, 17);
            this.txtPrevHoseOn.TabIndex = 171;
            // 
            // tLabel5
            // 
            this.tLabel5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel5.Location = new System.Drawing.Point(3, 47);
            this.tLabel5.Name = "tLabel5";
            this.tLabel5.Size = new System.Drawing.Size(68, 15);
            this.tLabel5.Text = "Prev Hose on";
            // 
            // txtPrevCommence
            // 
            this.txtPrevCommence.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtPrevCommence.isBusinessItemName = "Prev. Commence";
            this.txtPrevCommence.Location = new System.Drawing.Point(80, 94);
            this.txtPrevCommence.Multiline = true;
            this.txtPrevCommence.Name = "txtPrevCommence";
            this.txtPrevCommence.ReadOnly = true;
            this.txtPrevCommence.Size = new System.Drawing.Size(130, 17);
            this.txtPrevCommence.TabIndex = 174;
            // 
            // tLabel6
            // 
            this.tLabel6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel6.Location = new System.Drawing.Point(1, 95);
            this.tLabel6.Name = "tLabel6";
            this.tLabel6.Size = new System.Drawing.Size(83, 15);
            this.tLabel6.Text = "Prev Commence";
            // 
            // txtPrevComplete
            // 
            this.txtPrevComplete.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtPrevComplete.isBusinessItemName = "Prev. Complete";
            this.txtPrevComplete.Location = new System.Drawing.Point(81, 142);
            this.txtPrevComplete.Multiline = true;
            this.txtPrevComplete.Name = "txtPrevComplete";
            this.txtPrevComplete.ReadOnly = true;
            this.txtPrevComplete.Size = new System.Drawing.Size(130, 17);
            this.txtPrevComplete.TabIndex = 177;
            // 
            // tLabel7
            // 
            this.tLabel7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel7.Location = new System.Drawing.Point(2, 143);
            this.tLabel7.Name = "tLabel7";
            this.tLabel7.Size = new System.Drawing.Size(74, 15);
            this.tLabel7.Text = "Prev Complete";
            // 
            // txtPrevHoseOff
            // 
            this.txtPrevHoseOff.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtPrevHoseOff.isBusinessItemName = "Prev. Hose Off";
            this.txtPrevHoseOff.Location = new System.Drawing.Point(80, 191);
            this.txtPrevHoseOff.Multiline = true;
            this.txtPrevHoseOff.Name = "txtPrevHoseOff";
            this.txtPrevHoseOff.ReadOnly = true;
            this.txtPrevHoseOff.Size = new System.Drawing.Size(130, 17);
            this.txtPrevHoseOff.TabIndex = 180;
            // 
            // tLabel8
            // 
            this.tLabel8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tLabel8.Location = new System.Drawing.Point(1, 192);
            this.tLabel8.Name = "tLabel8";
            this.tLabel8.Size = new System.Drawing.Size(74, 15);
            this.tLabel8.Text = "Prev Hose off";
            // 
            // HVO106001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.txtPrevHoseOff);
            this.Controls.Add(this.tLabel8);
            this.Controls.Add(this.txtPrevComplete);
            this.Controls.Add(this.tLabel7);
            this.Controls.Add(this.txtPrevCommence);
            this.Controls.Add(this.tLabel6);
            this.Controls.Add(this.txtPrevHoseOn);
            this.Controls.Add(this.tLabel5);
            this.Controls.Add(this.txtOpeTpNm);
            this.Controls.Add(this.tLabel4);
            this.Controls.Add(this.txtJPVC);
            this.Controls.Add(this.tLabel9);
            this.Controls.Add(this.tLabel3);
            this.Controls.Add(this.txtCompleteDt);
            this.Controls.Add(this.tLabel2);
            this.Controls.Add(this.txtCommenceDt);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.txtHoseOffDt);
            this.Controls.Add(this.tLabel10);
            this.Controls.Add(this.txtHoseOnDt);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "HVO106001";
            this.Text = "V/S - Jetty Operation";
            this.ResumeLayout(false);

        }

        #endregion

        internal Framework.Controls.UserControls.TButton btnOk;
        internal Framework.Controls.UserControls.TButton btnCancel;
        private Framework.Controls.UserControls.TDateTimePicker txtHoseOnDt;
        internal Framework.Controls.UserControls.TLabel tLabel10;
        internal Framework.Controls.UserControls.TLabel tLabel1;
        private Framework.Controls.UserControls.TDateTimePicker txtHoseOffDt;
        internal Framework.Controls.UserControls.TLabel tLabel2;
        private Framework.Controls.UserControls.TDateTimePicker txtCommenceDt;
        internal Framework.Controls.UserControls.TLabel tLabel3;
        private Framework.Controls.UserControls.TDateTimePicker txtCompleteDt;
        internal Framework.Controls.UserControls.TTextBox txtJPVC;
        internal Framework.Controls.UserControls.TLabel tLabel9;
        internal Framework.Controls.UserControls.TTextBox txtOpeTpNm;
        internal Framework.Controls.UserControls.TLabel tLabel4;
        internal Framework.Controls.UserControls.TTextBox txtPrevHoseOn;
        internal Framework.Controls.UserControls.TLabel tLabel5;
        internal Framework.Controls.UserControls.TTextBox txtPrevCommence;
        internal Framework.Controls.UserControls.TLabel tLabel6;
        internal Framework.Controls.UserControls.TTextBox txtPrevComplete;
        internal Framework.Controls.UserControls.TLabel tLabel7;
        internal Framework.Controls.UserControls.TTextBox txtPrevHoseOff;
        internal Framework.Controls.UserControls.TLabel tLabel8;
    }
}