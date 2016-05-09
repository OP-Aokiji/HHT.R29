#region Explanation
//* --------------------------------------------------------------
//* CHANGE REVISION
//* --------------------------------------------------------------
//* DATE           AUTHOR      	   REVISION    	     Content
//* 2008-05-28   Mr Luis Lee	     1.0          First release.
//* --------------------------------------------------------------
//* CLASS DESCRIPTION
//* --------------------------------------------------------------
#endregion 

using System;
using System.Collections.Generic;
using System.Text;
using MvcPatterns.Patterns;
using MvcPatterns.Interfaces;
using Framework.Controls;
using Framework.Controls.Container;
using System.Reflection;
using MOST.Common.UserAttribute;

namespace MvcPatterns.Command
{
    public class FormCommand : SimpleCommand
    {
        public FormCommand() : base()
		{ }
		
        override public void execute(INotification note)
		{
            if (note.getBody() is IForm)
            {
                IForm appForm = (IForm)note.getBody();
                appForm.formShow();
            }
		}
    }
}