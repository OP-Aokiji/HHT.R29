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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Framework.Controls.Container
{
    public partial class TDialog : Framework.Controls.Container.BaseForm, IForm, IContainerControls
    {
        public TDialog()
        {
            InitializeComponent();
        }

        public void focusControl(Control control)
        {

        }

        public void formShow()
        {
            this.ShowDialog();
        }
        public string formName()
        {
            return this.Name;
        }
    }
}