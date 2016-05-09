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
using System.Text;
using System.Windows.Forms;

namespace Framework.Controls
{
    interface IContainerControls : IControls
    {
        bool validations(System.Windows.Forms.Control.ControlCollection controls);
        void clearControlValue(System.Windows.Forms.Control.ControlCollection controls);
    }
}
