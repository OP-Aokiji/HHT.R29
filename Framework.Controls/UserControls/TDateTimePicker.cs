#region Explanation
//* --------------------------------------------------------------
//* CHANGE REVISION
//* --------------------------------------------------------------
//* DATE           AUTHOR      	   REVISION    	     Content
//* 2008-05-20   Mr Luis Lee	     1.0          First release.
//* --------------------------------------------------------------
//* CLASS DESCRIPTION
//* --------------------------------------------------------------
#endregion 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Framework.Controls;
using Framework.Controls.ValidationHandler;
//using Framework.Common.UserInformation;

namespace Framework.Controls.UserControls
{
    public partial class TDateTimePicker : DateTimePicker, ITransactionControls
    {
        private ValidationError error;
        private string m_originFormat;
        public const string FORMAT_BLANK        = " ";
        public const string FORMAT_DDMMYYYY     = "dd/MM/yyyy";
        public const string FORMAT_DDMMYYYYHHMM = "dd/MM/yyyy HH:mm";

        public TDateTimePicker()
        {
            InitializeComponent();
            m_originFormat = "";
            //this.Format = DateTimePickerFormat.Custom;
            //this.CustomFormat = TDateTimePicker.FORMAT_BLANK;
        }

        public override string Text 
        {
            get
            {
                return base.Text.Trim();
            }
            set
            {
                base.Text = value;
            }
        }

        public new string CustomFormat
        {
            get
            {
                return base.CustomFormat;
            }
            set
            {
                if (!TDateTimePicker.FORMAT_BLANK.Equals(this.CustomFormat) && TDateTimePicker.FORMAT_BLANK.Equals(value))
                {
                    m_originFormat = base.CustomFormat;
                }
                base.CustomFormat = value;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                if (!TDateTimePicker.FORMAT_BLANK.Equals(this.CustomFormat))
                {
                    m_originFormat = this.CustomFormat;
                }
                this.Format = DateTimePickerFormat.Custom;
                this.CustomFormat = TDateTimePicker.FORMAT_BLANK;
            }

            base.OnKeyUp(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            // CustomFormat: [dd/MM/yyyy] or [dd/MM/yyyy HH:mm]
            //this.Format = DateTimePickerFormat.Custom;
            //if (!TDateTimePicker.FORMAT_DDMMYYYY.Equals(this.CustomFormat))
            //{
            //    this.CustomFormat = TDateTimePicker.FORMAT_DDMMYYYYHHMM;
            //}
            if (!"".Equals(m_originFormat))
            {
                this.Format = DateTimePickerFormat.Custom;
                if (TDateTimePicker.FORMAT_DDMMYYYYHHMM.Equals(m_originFormat))
                {
                    this.CustomFormat = TDateTimePicker.FORMAT_DDMMYYYYHHMM;
                }
                else if (TDateTimePicker.FORMAT_DDMMYYYY.Equals(m_originFormat))
                {
                    this.CustomFormat = TDateTimePicker.FORMAT_DDMMYYYY;
                }
                else
                {
                    this.CustomFormat = TDateTimePicker.FORMAT_BLANK;
                }
            }

            base.OnGotFocus(e);
        }

        public new DateTime Value
        {
            get
            {
                // CustomFormat: [dd/MM/yyyy] or [dd/MM/yyyy HH:mm]
                DateTime value = base.Value;
                if (FORMAT_DDMMYYYYHHMM.Equals(this.CustomFormat))
                {
                    // Remove second
                    int second = value.Second;
                    TimeSpan ts = new TimeSpan(0, 0, second);
                    value = value.Subtract(ts);
                }
                else if (FORMAT_DDMMYYYY.Equals(this.CustomFormat))
                {
                    // Remove hour, minute, second
                    int hour = value.Hour;
                    int minute = value.Minute;
                    int second = value.Second;
                    TimeSpan ts = new TimeSpan(hour, minute, second);
                    value = value.Subtract(ts);
                }
                return value;

                //return base.Value;
            }
            set
            {
                // CustomFormat: [dd/MM/yyyy] or [dd/MM/yyyy HH:mm]
                //this.Format = DateTimePickerFormat.Custom;
                //if (!TDateTimePicker.FORMAT_DDMMYYYY.Equals(this.CustomFormat))
                //{
                //    this.CustomFormat = TDateTimePicker.FORMAT_DDMMYYYYHHMM;
                //}
                if (!"".Equals(m_originFormat))
                {
                    this.Format = DateTimePickerFormat.Custom;
                    if (TDateTimePicker.FORMAT_DDMMYYYYHHMM.Equals(m_originFormat))
                    {
                        this.CustomFormat = TDateTimePicker.FORMAT_DDMMYYYYHHMM;
                    }
                    else if (TDateTimePicker.FORMAT_DDMMYYYY.Equals(m_originFormat))
                    {
                        this.CustomFormat = TDateTimePicker.FORMAT_DDMMYYYY;
                    }
                    else
                    {
                        this.CustomFormat = TDateTimePicker.FORMAT_BLANK;
                    }
                }

                base.Value = value;
            }
        }

        private string businessItemName = string.Empty;
        public string isBusinessItemName
        {
            get { return businessItemName; }
            set { businessItemName = value; }
        }

        private bool mandatory = false;
        [DefaultValue(false)]
        public bool isMandatory
        {
            get { return mandatory; }
            set { mandatory = value; }
        }

        private Color fColor = Color.LightBlue;
        public Color focusColor
        {
            get { return fColor; }
        }

        #region Control Validation Check
        public ValidationError validation()
        {
            if (mandatory && this.Visible && this.Enabled && (this.Text == null || this.Text.Trim().Equals(string.Empty)))
            {
                error = new ValidationError();
                error.ErrorID = "ERR-00001";
                error.ErrorMessage = string.Format("{0} is mandatory item", businessItemName);
                error.RaiseControl = this;

                return error;
            }

            return null;
        }
        #endregion

        #region Clear ControlValue
        public void clearControlValue()
        {
            this.Format = DateTimePickerFormat.Custom;
            this.CustomFormat = TDateTimePicker.FORMAT_BLANK;
        }
        #endregion
    }
}
