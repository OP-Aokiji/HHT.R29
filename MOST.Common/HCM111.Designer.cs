namespace MOST.Common
{
    partial class HCM111
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
            this.grdDataList = new Framework.Controls.UserControls.TGrid();
            this.btnSearch = new Framework.Controls.UserControls.TButton();
            this.txtSearchItem = new Framework.Controls.UserControls.TTextBox();
            this.lbGP = new Framework.Controls.UserControls.TLabel();
            this.txtLorryNo = new Framework.Controls.UserControls.TTextBox();
            this.tLabel1 = new Framework.Controls.UserControls.TLabel();
            this.txtEndDt = new Framework.Controls.UserControls.TDateTimePicker();
            this.txtStartDt = new Framework.Controls.UserControls.TDateTimePicker();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(95, 240);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 24);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grdDataList
            // 
            this.grdDataList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdDataList.IsDirty = false;
            this.grdDataList.Location = new System.Drawing.Point(7, 89);
            this.grdDataList.Name = "grdDataList";
            this.grdDataList.RowHeadersVisible = false;
            this.grdDataList.Size = new System.Drawing.Size(225, 139);
            this.grdDataList.TabIndex = 3;
            this.grdDataList.DoubleClick += new System.EventHandler(this.grdGPList_DoubleClick);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(166, 7);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(65, 24);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearchItem
            // 
            this.txtSearchItem.isBusinessItemName = "JPVC";
            this.txtSearchItem.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtSearchItem.Location = new System.Drawing.Point(60, 8);
            this.txtSearchItem.Name = "txtSearchItem";
            this.txtSearchItem.Size = new System.Drawing.Size(102, 23);
            this.txtSearchItem.TabIndex = 1;
            // 
            // lbGP
            // 
            this.lbGP.Location = new System.Drawing.Point(2, 10);
            this.lbGP.Name = "lbGP";
            this.lbGP.Size = new System.Drawing.Size(60, 17);
            this.lbGP.Text = "GatePass";
            // 
            // txtLorryNo
            // 
            this.txtLorryNo.isBusinessItemName = "JPVC";
            this.txtLorryNo.isCharacterType = Framework.Controls.UserControls.TTextBox.CharacterList.UpperCase;
            this.txtLorryNo.Location = new System.Drawing.Point(60, 34);
            this.txtLorryNo.Name = "txtLorryNo";
            this.txtLorryNo.Size = new System.Drawing.Size(102, 23);
            this.txtLorryNo.TabIndex = 7;
            // 
            // tLabel1
            // 
            this.tLabel1.Location = new System.Drawing.Point(2, 36);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(60, 17);
            this.tLabel1.Text = "LorryNo";
            // 
            // txtEndDt
            // 
            this.txtEndDt.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtEndDt.CustomFormat = "dd/MM/yyyy";
            this.txtEndDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtEndDt.isBusinessItemName = "G/O Time";
            this.txtEndDt.isMandatory = true;
            this.txtEndDt.Location = new System.Drawing.Point(122, 62);
            this.txtEndDt.Name = "txtEndDt";
            this.txtEndDt.Size = new System.Drawing.Size(110, 24);
            this.txtEndDt.TabIndex = 55;
            this.txtEndDt.Value = new System.DateTime(2008, 10, 15, 0, 0, 0, 0);
            
            // 
            // txtStartDt
            // 
            this.txtStartDt.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtStartDt.CustomFormat = "dd/MM/yyyy";
            this.txtStartDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtStartDt.isBusinessItemName = "G/O Time";
            this.txtStartDt.isMandatory = true;
            this.txtStartDt.Location = new System.Drawing.Point(9, 61);
            this.txtStartDt.Name = "txtStartDt";
            this.txtStartDt.Size = new System.Drawing.Size(110, 24);
            this.txtStartDt.TabIndex = 56;
            this.txtStartDt.Value = new System.DateTime(2008, 10, 15, 0, 0, 0, 62);
            // 
            // HCM111
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 269);
            this.Controls.Add(this.txtStartDt);
            this.Controls.Add(this.txtEndDt);
            this.Controls.Add(this.txtLorryNo);
            this.Controls.Add(this.tLabel1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearchItem);
            this.Controls.Add(this.lbGP);
            this.Controls.Add(this.grdDataList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.Menu = null;
            this.MinimizeBox = false;
            this.Name = "HCM111";
            this.Text = "JPB";
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Controls.UserControls.TButton btnOk;
        private Framework.Controls.UserControls.TButton btnCancel;
        private Framework.Controls.UserControls.TGrid grdDataList;
        private Framework.Controls.UserControls.TButton btnSearch;
        private Framework.Controls.UserControls.TTextBox txtSearchItem;
        private Framework.Controls.UserControls.TLabel lbGP;
        private Framework.Controls.UserControls.TTextBox txtLorryNo;
        private Framework.Controls.UserControls.TLabel tLabel1;
        private Framework.Controls.UserControls.TDateTimePicker txtEndDt;
        private Framework.Controls.UserControls.TDateTimePicker txtStartDt;
    }
}