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
using Framework.Common.Helper;
using System.Runtime.InteropServices;

namespace Framework.Controls.UserControls
{
    public partial class TButton : Button, IActionControl
    {
        //#region local Variable
        //private bool bMultiline = false;
        //#endregion

        public TButton()
        {
            InitializeComponent();
        }

        //#region Multiline
        //[DefaultValue(false)]
        //public bool isMultiline
        //{
        //    get { return bMultiline; }
        //    set 
        //    {
        //        bMultiline = value;
        //    }
        //}

        //public bool ShouldSerializeisMultiline()
        //{
        //    return bMultiline != false;
        //}
        //public void ResetisMultiline()
        //{
        //    bMultiline = false;
        //}
        //#endregion
    }
}
