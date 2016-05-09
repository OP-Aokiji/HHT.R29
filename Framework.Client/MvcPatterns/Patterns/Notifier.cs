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
using MvcPatterns.Interfaces;
using MvcPatterns.Core;

namespace MvcPatterns.Patterns
{
    public class Notifier : INotifier
    {
        protected IFacade facade = Facade.getInstance();

        public void sendNotification(String notificationName) 
		{
            facade.sendNotification(notificationName);
		}

        public void sendNotification(String notificationName, Object body)
		{
            facade.sendNotification(notificationName, body);
		}

        public void sendNotification(String notificationName, Object body, String type)
		{
            facade.sendNotification(notificationName, body, type);
		}
    }
}
