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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
//using System.Runtime.InteropServices;
using Framework.Controls;
using Framework.Controls.ValidationHandler;

namespace Framework.Controls.UserControls
{
    public partial class TTextBox : TextBox, ITransactionControls, IConstraint
    {
        #region local Variable
        private ValidationError error;
        private string prevText = string.Empty;
        private string businessItemName = string.Empty;
        private bool mandatory = false;
        private CharacterList characterType = CharacterList.Default;
        private InputList inputType = InputList.All;
        private Color fColor = Framework.Common.Constants.Constants.DefaultColor;
        private Color orignalColor;
        private FocusColorList focusedColor = FocusColorList.LightBlue;
        private int nSmallestInputLength = 0;
        #endregion

        #region Initialize
        public TTextBox()
        {
            InitializeComponent();
        }
        #endregion

        #region Business Name
        [DefaultValue(0)]
        public int isSmallestInputLength
        {
            get { return nSmallestInputLength; }
            set { nSmallestInputLength = value; }
        }
        public bool ShouldSerializeisSmallestInputLength()
        {
            return nSmallestInputLength != 0;
        }

        public string isBusinessItemName
        {
            get { return businessItemName; }
            set { businessItemName = value; }
        }

        public void ResetisFBusinessItemName()
        {
            businessItemName = string.Empty;
        }
        public bool ShouldSerializeisBusinessItemName()
        {
            return businessItemName != string.Empty;
        }
        #endregion

        #region FocusedColor
        public enum FocusColorList
        {
            Red,
            Yellow,
            LightBlue
        }
        [DefaultValue(FocusColorList.LightBlue)]
        public FocusColorList isFocusedColor
        {
            get { return focusedColor; }
            set
            {
                focusedColor = value;
                switch (value)
                {
                    case FocusColorList.LightBlue:
                        fColor = Color.LightBlue;
                        break;
                    case FocusColorList.Red:
                        fColor = Color.Red;
                        break;
                    case FocusColorList.Yellow:
                        fColor = Color.Yellow;
                        break;
                }
            }
        }
        public void ResetisFocusedColor()
        {
            focusedColor = FocusColorList.LightBlue;
        }
        public bool ShouldSerializeisFocusedColor()
        {
            return focusedColor != FocusColorList.LightBlue;
        }

        private void TTextBox_GotFocus(object sender, EventArgs e)
        {
            orignalColor = this.BackColor;
            this.BackColor = fColor;

            Invalidate();
        }

        private void TTextBox_LostFocus(object sender, EventArgs e)
        {
            if (orignalColor != null)
            {
                this.BackColor = orignalColor;
            }
            Invalidate();

            // If decimal point is the last char, remove it.
            if (inputType == InputList.Decimal)
            {
                //if (!string.IsNullOrEmpty(this.Text) &&
                //    this.Text.LastIndexOf(".") == (this.Text.Length - 1))
                //{
                //    this.Text = this.Text.Substring(0, this.Text.Length - 1);
                //}

                // If decimal point is the first char, add 0 before it.
                if (!string.IsNullOrEmpty(this.Text) &&
                    this.Text.IndexOf(".") == 0)
                {
                    this.Text = "0" + this.Text;
                }
            }
        }
        #endregion

        #region Mandatory
        [DefaultValue(false)]
        public bool isMandatory
        {
            get { return mandatory; }
            set { mandatory = value; }
        }

        public void ResetisMandatory()
        {
            mandatory = false;
        }
        public bool ShouldSerializeisMandatory()
        {
            return mandatory != false;
        }
        #endregion

        #region Character Type
        /// <summary>
        /// Character Type
        /// </summary>
        public enum CharacterList
        {
            UpperCase,
            LowerCase,
            Default
        }
        
        [DefaultValue(CharacterList.Default)]
        public CharacterList isCharacterType
        {
            get { return characterType; }
            set { characterType = value; }
        }

        public void ResetisCharacterType()
        {
            characterType = CharacterList.Default;
        }
        public bool ShouldSerializeisCharacterType()
        {
            return characterType != CharacterList.Default;
        }

        private void TTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            string InputString = e.KeyCode.ToString();

            if (!(InputString.Equals("Delete") || InputString.Equals("Back") || InputString.Equals("Up") || InputString.Equals("Down") || InputString.Equals("Left") || InputString.Equals("Right")))
            {
                // UpperCase
                if (this.Text != null || this.Text.Equals(string.Empty))
                {
                    if (characterType == CharacterList.UpperCase)
                    {
                        this.Text = this.Text.ToUpper();
                        this.SelectionStart = Text.Length;
                    }
                    else if (characterType == CharacterList.LowerCase)
                    {
                        this.Text = this.Text.ToLower();
                        this.SelectionStart = Text.Length;
                    }
                }

                // Decimal number processing
                if (inputType == InputList.Decimal)
                {
                    // Process dot char [.]
                    if (e.KeyValue == 190)
                    {
                        // In case decimal point is the first char of text || input 2 decimal points.
                        if (this.Text.IndexOf(".") == 0 || prevText.IndexOf(".") > -1)
                        {
                            this.Text = prevText;
                            return;
                        }
                    }
                    // Validate decimal with maximum 3 digits after decimal point
                    bool bMatchText = Regex.IsMatch(this.Text, @"^\d*(\.)?(\d{1,3})?$");
                    if (!bMatchText)
                    {
                        // Substring to get the maximum 3 digits after decimal point
                        if (!string.IsNullOrEmpty(this.Text))
                        {
                            string[] arrTmp = this.Text.Split('.');
                            if (arrTmp != null && arrTmp.Length > 1)
                            {
                                string decimalNumber = arrTmp[1];
                                if (!string.IsNullOrEmpty(decimalNumber) && decimalNumber.Length > 3)
                                {
                                    decimalNumber = decimalNumber.Substring(0, 3);
                                    this.Text = arrTmp[0] + "." + decimalNumber;
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Input Type
        /// <summary>
        /// Input Type 
        /// </summary>
        public enum InputList
        {
            NumberOnly,
            Decimal,
            AlphaNumeric,
            AlphabetOnly,
            All
        }
                
        [DefaultValue(InputList.All)]
        public InputList isInputType
        {
            get { return inputType; }
            set 
            { 
                inputType = value;

                // Set right align in case of numeric data.
                if (inputType == InputList.NumberOnly || inputType == InputList.Decimal)
                {
                    this.TextAlign = HorizontalAlignment.Right;
                }
            }
        }
                
        public Color focusColor
        {
            get { return fColor; }
        }

        public void ResetisInputType()
        {
            inputType = InputList.All;
        }
        public bool ShouldSerializeisInputType()
        {
            return inputType != InputList.All;
        }

        /// <summary>
        /// Input value check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string InputString = e.KeyChar.ToString();

            char[] avoid = new char[] { (char)3, (char)22, (char)26 };

            bool bNotFound = Array.IndexOf(avoid, e.KeyChar) < 0;

            if (inputType == InputList.NumberOnly)
            {
                if (!Regex.IsMatch(InputString, "[\\d\\v\\e\\b]") && bNotFound)
                {
                    e.Handled = true;
                }
            }
            else if (inputType == InputList.Decimal)
            {
                bool bMatchInputStr = Regex.IsMatch(InputString, @"[\d\.\r\v\e\b]");
                if (!bMatchInputStr && bNotFound)
                {
                    e.Handled = true;
                }
            }
            else if (inputType == InputList.AlphaNumeric)
            {
                if (!Regex.IsMatch(InputString, "[a-zA-Z\\d\\r\\v\\e\\b]") && bNotFound)
                {
                    e.Handled = true;
                }
            }
            else if (inputType == InputList.AlphabetOnly)
            {
                if (!Regex.IsMatch(InputString, "[a-zA-Z\\r\\v\\e\\b]") && bNotFound)
                {
                    e.Handled = true;
                }
            }

            // Backup previous text
            prevText = this.Text;
        }
        #endregion

        #region Control Validation Check
        public ValidationError validation()
        {
            if (mandatory && this.Visible && this.Enabled && (this.Text == null || this.Text.Trim().Equals(string.Empty)))
            {
                makeError();

                error = new ValidationError();
                error.ErrorID = "ERR-00001";
                error.ErrorMessage = string.Format("{0} is a mandatory item", businessItemName);
                error.RaiseControl = this;

                return error;
            }

            if (mandatory && this.Visible && this.Enabled && nSmallestInputLength > 0 && (this.Text.Length < nSmallestInputLength))
            {
                makeError();

                error = new ValidationError();
                error.ErrorID = "ERR-00002";
                error.ErrorMessage = string.Format("{0} has to input at least {1} character(s)", businessItemName, nSmallestInputLength);
                error.RaiseControl = this;

                return error;
            }            

            return null;
        }
        #endregion

        #region Clear ControlValue
        public void clearControlValue()
        {
            this.Text = string.Empty;
        }
        #endregion

        #region Display Error
        private void makeError()
        {
            this.BackColor = Framework.Common.Constants.Constants.DefaultColor;
        }
        #endregion
    }
}