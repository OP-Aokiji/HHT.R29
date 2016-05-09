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

namespace MvcPatterns.Interfaces
{
    public interface IView
    {
		void registerObserver (String noteName, IObserver observer);
		void notifyObservers(INotification note);
    }
}
