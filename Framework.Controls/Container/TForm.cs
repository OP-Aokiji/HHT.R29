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
using Framework.Common.Constants;

namespace Framework.Controls.Container
{
    public partial class TForm : Framework.Controls.Container.BaseForm, IForm, IContainerControls
    {
       

        #region Local Valiable
        //private bool _isDirty;
        #endregion

        #region Initialize
        public TForm()
        {
            //_isDirty = false;
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        //public bool IsDirty
        //{
        //    get { return _isDirty; }
        //    set { _isDirty = value; }
        //}
        #endregion

        #region focus Control
        public void focusControl(Control control)
        {

        }
        #endregion
        
        public void formShow()
        {
            this.Show();
        }

        public string formName()
        {
            return this.Name;
        }

        #region To Do
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnClosed(EventArgs e)
        //{
        //    this.Visible = false;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        //{
        //        this.Visible = false;
        //        e.Cancel = true;
        //}

        #endregion


        ///// <summary>
        ///// Recursive function which traverses the Control tree.
        ///// Whenever it locates a control that can be classified as an input control, 
        ///// it attaches an event handler to know when the form is dirty.
        ///// </summary>
        ///// <param name="ctrlCollection"></param>
        //protected void AddOnChangeHandlerToInputControls(ControlCollection ctrlCollection)
        //{
        //    foreach (Control subctrl in ctrlCollection)
        //    {
        //        if (subctrl is TextBox)
        //            ((TextBox)subctrl).TextChanged +=
        //            new EventHandler(InputControls_OnChange);
        //        else if (subctrl is CheckBox)
        //            ((CheckBox)subctrl).CheckStateChanged +=
        //            new EventHandler(InputControls_OnChange);
        //        else if (subctrl is RadioButton)
        //            ((RadioButton)subctrl).CheckedChanged +=
        //            new EventHandler(InputControls_OnChange);
        //        else if (subctrl is ListBox)
        //            ((ListBox)subctrl).SelectedIndexChanged +=
        //            new EventHandler(InputControls_OnChange);
        //        else if (subctrl is ComboBox)
        //            ((ComboBox)subctrl).SelectedIndexChanged +=
        //            new EventHandler(InputControls_OnChange);
        //        else if (subctrl is DateTimePicker)
        //            ((DateTimePicker)subctrl).ValueChanged +=
        //            new EventHandler(InputControls_OnChange);
        //        //else
        //        //{
        //        //    if (subctrl.Controls.Count > 0)
        //        //        this.AddOnChangeHandlerToInputControls(subctrl.Controls);
        //        //}
        //    }
        //}
        
        ///// <summary>
        ///// Do something to indicate the form is dirty
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void InputControls_OnChange(object sender, EventArgs e)
        //{
        //    // Do something to indicate the form is dirty
        //    this.IsDirty = true;
        //}

        //public DialogResult ShowDialog()
        //{
        //    this._isDirty = false;
        //    return base.ShowDialog();
        //}
    }
}
