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
using MvcPatterns.Patterns;

namespace MvcPatterns.Core
{
    public class Controller : IController
    {
        protected IView view;
        protected IDictionary commandMap;
        protected static IController instance;

        static Controller()
        {
            instance = new Controller();
        }

        protected Controller()
		{
            commandMap = new Hashtable();	
			initializeController();	
		}
		
        protected virtual void initializeController()
		{
			view = View.getInstance();
		}
	
		public static IController getInstance()
		{
			return instance;
		}

		public void executeCommand(INotification note)
		{
			Type commandType = (Type)commandMap[note.getName()];
            if (commandType == null) return;

            Object commandInstance = Activator.CreateInstance(commandType);
            if (commandInstance is ICommand)
            {
                ((ICommand)commandInstance).execute(note);
            }
		}

        public void registerCommand(String notificationName, Type commandType)
		{
            if (!commandMap.Contains(notificationName))
            {
                view.registerObserver(notificationName, new Observer("executeCommand", this));
            }
            commandMap[notificationName] = commandType;
		}

		public void removeCommand(String notificationName)
		{
            if (commandMap.Contains(notificationName))
            {
                commandMap.Remove(notificationName);
            }
		}
    }
}
