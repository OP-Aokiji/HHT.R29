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
    public interface INotifier
    {
		void sendNotification(String notificationName);
		void sendNotification(String notificationName, Object body);
		void sendNotification(String notificationName, Object body, String type);
    }
}
