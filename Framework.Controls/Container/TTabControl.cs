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

namespace Framework.Controls.Container
{
    public partial class TTabControl : TabControl, IContainerControls
    {
        public TTabControl()
        {
            InitializeComponent();
        }

        public bool validations(System.Windows.Forms.Control.ControlCollection controls)
        {
            return false;
        }

        public void clearControlValue(System.Windows.Forms.Control.ControlCollection controls)
        {
            foreach (Control ctl in controls)
            {
                if (ctl is IContainerControls)
                {
                    this.clearControlValue(ctl.Controls);
                }

                if (ctl is IConstraint)
                {
                    IConstraint clearControl = (IConstraint)ctl;
                    clearControl.clearControlValue();
                }
            }
        }

    }
}
