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
using System.Collections;
using MvcPatterns.Interfaces;

namespace MvcPatterns.Patterns
{
    public class SimpleCommand : Notifier, ICommand
    {
		public virtual void execute(INotification notification){ }
    }
}