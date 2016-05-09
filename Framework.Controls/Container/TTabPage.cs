using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Framework.Controls.Container
{
    public partial class TTabPage : TabPage, IContainerControls
    {
        public TTabPage()
        {
            InitializeComponent();
        }

        public bool validations(System.Windows.Forms.Control.ControlCollection controls)
        {
            return false;
        }

        public void clearControlValue(ControlCollection controls)
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
