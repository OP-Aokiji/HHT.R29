﻿#region Explanation
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
    public interface IController
    {
        void registerCommand(String notificationName, Type commandType);
		void executeCommand(INotification notification);
		void removeCommand(String notificationName);
    }
}
