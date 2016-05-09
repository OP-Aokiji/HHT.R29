#region Explanation
//* --------------------------------------------------------------
//* CHANGE REVISION
//* --------------------------------------------------------------
//* DATE           AUTHOR      	   REVISION    	     Content
//* 2008-08-22   Truong Ngoc Ky	     1.0          First release.
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
using System.Collections;
using Framework.Common.Helper;
using Framework.Controls;
using Framework.Controls.ValidationHandler;

namespace Framework.Controls.UserControls
{
    public partial class TDualListbox : UserControl, ITransactionControls
    {
        private ArrayList _Assigned;
        private ArrayList _Available;
        private bool _Enabled = true;
        private int _ListBoxHeight = 82;
        private int _ListBoxWidth = 200;
        private ValidationError error;

        #region getter/setter
        public ArrayList Assigned
        {
            get
            {
                if (lstAssigned != null && lstAssigned.Items.Count > 0)
                {
                    this._Assigned = new ArrayList();
                    for (int i = 0; i < lstAssigned.Items.Count; i++)
                    {
                        this._Assigned.Add(lstAssigned.Items[i]);
                    }
                }
                return this._Assigned;
            }
            set
            {
                this._Assigned = value;
                if (_Assigned != null && _Assigned.Count > 0)
                {
                    for (int i = 0; i < _Assigned.Count; i++)
                    {
                        lstAssigned.Items.Add(_Assigned[i]);
                    }
                }
                Sort(lstAssigned);
                ValidateButton();
            }
        }

        public ArrayList Available
        {
            get
            {
                if (lstAvailable != null && lstAvailable.Items.Count > 0)
                {
                    this._Available = new ArrayList();
                    for (int i = 0; i < lstAvailable.Items.Count; i++)
                    {
                        this._Available.Add(lstAvailable.Items[i]);
                    }
                }
                return this._Available;
            }
            set
            {
                this._Available = value;
                if (_Available != null && _Available.Count > 0)
                {
                    for (int i = 0; i < _Available.Count; i++)
                    {
                        lstAvailable.Items.Add(_Available[i]);
                    }
                }
                Sort(lstAvailable);
                ValidateButton();
            }
        }

        public bool EnabledControl
        {
            set
            {
                this._Enabled = value;
                if (this._Enabled)
                {
                    lstAvailable.Enabled = true;
                    lstAssigned.Enabled = true;
                    cmdAdd.Enabled = true;
                    cmdAddAll.Enabled = true;
                    cmdRemove.Enabled = true;
                    cmdRemoveAll.Enabled = true;
                }
                else
                {
                    lstAvailable.Enabled = false;
                    lstAssigned.Enabled = false;
                    cmdAdd.Enabled = false;
                    cmdAddAll.Enabled = false;
                    cmdRemove.Enabled = false;
                    cmdRemoveAll.Enabled = false;
                }
            }
        }

        public int ListBoxHeight
        {
            get
            {
                return this._ListBoxHeight;
            }
            set
            {
                this._ListBoxHeight = value;

                // set dimensions of control
                this.Height = _ListBoxHeight;
                lstAvailable.Height = _ListBoxHeight;
                lstAssigned.Height = _ListBoxHeight;

                // relocate buttons
            }
        }

        public int ListBoxWidth
        {
            get
            {
                return this._ListBoxWidth;
            }
            set
            {
                this._ListBoxWidth = value;

                // set dimensions of control
                this.Width = _ListBoxWidth * 2 + 48;
                lstAvailable.Width = _ListBoxWidth;
                lstAssigned.Width = _ListBoxWidth;

                // relocate buttons
            }
        }
        #endregion

        public TDualListbox()
        {
            InitializeComponent();
            this._Available = new ArrayList();
            this._Assigned = new ArrayList();
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
            if (mandatory && this.Visible && this._Enabled && this._Assigned == null && this._Assigned.Count <= 0)
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

        }
        #endregion

        #region Event listener
        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            SwitchItem(lstAvailable, lstAssigned);
            Sort(lstAssigned);
            ValidateButton();
        }

        protected void cmdAddAll_Click(object sender, EventArgs e)
        {
            Object objListItem;

            foreach (Object tempLoopVar_objListItem in lstAvailable.Items)
            {
                objListItem = tempLoopVar_objListItem;
                lstAssigned.Items.Add(objListItem);
            }

            lstAvailable.Items.Clear();
            Sort(lstAssigned);
            if (lstAssigned.Items.Count > 0)
            {
                lstAssigned.SelectedIndex = 0;
            }
            ValidateButton();
        }

        protected void cmdRemove_Click(object sender, EventArgs e)
        {
            SwitchItem(lstAssigned, lstAvailable);
            Sort(lstAvailable);
            ValidateButton();
        }

        protected void cmdRemoveAll_Click(object sender, EventArgs e)
        {
            foreach (Object tempLoopVar_objListItem in lstAssigned.Items)
            {
                Object objListItem = tempLoopVar_objListItem;
                lstAvailable.Items.Add(objListItem);
            }

            lstAssigned.Items.Clear();
            Sort(lstAvailable);
            if (lstAvailable.Items.Count > 0)
            {
                lstAvailable.SelectedIndex = 0;
            }
            ValidateButton();
        }

        private void lstAvailable_DoubleClick(object sender, EventArgs e)
        {
            SwitchItem(lstAvailable, lstAssigned);
            Sort(lstAssigned);
            ValidateButton();
        }

        private void lstAssigned_DoubleClick(object sender, EventArgs e)
        {
            SwitchItem(lstAssigned, lstAvailable);
            Sort(lstAvailable);
            ValidateButton();
        }
        #endregion

        #region Helper functions
        /// <summary>
        /// Sort items in listbox with alphabetical order.
        /// </summary>
        /// <param name="ctlListBox"></param>
        /// <returns></returns>
        private void Sort(ListBox ctlListBox)
        {
            if (ctlListBox != null && ctlListBox.Items.Count > 0)
            {
                ArrayList lstTempItems = new ArrayList();
                Object objListItem;

                // store listitems in temp arraylist
                foreach (Object tmpItem in ctlListBox.Items)
                {
                    objListItem = tmpItem;
                    lstTempItems.Add(objListItem);
                }

                // sort arraylist based on text value
                lstTempItems.Sort(new ListItemComparer());

                // clear control
                ctlListBox.Items.Clear();

                // add listitems to control
                foreach (Object tmpItem in lstTempItems)
                {
                    objListItem = tmpItem;
                    ctlListBox.Items.Add(objListItem);
                }
            }
        }

        /// <summary>
        /// Switch item form source to destination.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        private void SwitchItem(ListBox source, ListBox destination)
        {
            int index = source.SelectedIndex;
            if (index > -1)
            {
                destination.SelectedIndex = destination.Items.Add(source.SelectedItem);
                source.Items.RemoveAt(index);
                source.SelectedIndex = (index > source.Items.Count - 1) ? index - 1 : index;
            }
        }

        /// <summary>
        /// Validate buttons.
        /// </summary>
        /// <returns></returns>
        private void ValidateButton()
        {
            if (lstAssigned.Items.Count > 0)
            {
                cmdRemove.Enabled = true;
                cmdRemoveAll.Enabled = true;
                lstAssigned.Focus();
            }
            else
            {
                cmdRemove.Enabled = false;
                cmdRemoveAll.Enabled = false;
            }

            if (lstAvailable.Items.Count > 0)
            {
                cmdAdd.Enabled = true;
                cmdAddAll.Enabled = true;
                lstAvailable.Focus();
            }
            else
            {
                cmdAdd.Enabled = false;
                cmdAddAll.Enabled = false;
            }
        }
        #endregion
    }
}
