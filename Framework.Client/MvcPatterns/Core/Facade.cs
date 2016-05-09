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
using MvcPatterns.Patterns;

namespace MvcPatterns.Core
{
    public class Facade : IFacade
    {
        protected IController controller;
        protected IView view;
        protected static IFacade instance;

        protected Facade() 
        {
			initializeFacade();	
		}

        static Facade()
        {
            instance = new Facade();
        }

        protected virtual void initializeFacade()
        {
			initializeController();
			initializeView();
		}

		public static IFacade getInstance()
        {
            return instance;
		}

		protected virtual void initializeController()
        {
			if (controller != null) return;
			controller = Controller.getInstance();
		}

        protected virtual void initializeView()
        {
			if (view != null) return;
			view = View.getInstance();
		}

        public void registerCommand(String notificationName, Type commandType) 
        {
			controller.registerCommand(notificationName, commandType);
		}

        public void removeCommand(String notificationName)
        {
			controller.removeCommand(notificationName);
		}

        public void sendNotification(String notificationName)
        {
            notifyObservers(new Notification(notificationName));
        }

        public void sendNotification(String notificationName, Object body)
        {
            notifyObservers(new Notification(notificationName, body));
        }

        public void sendNotification(String notificationName, Object body, String type)
        {
            notifyObservers(new Notification(notificationName, body, type));
        }

        public void notifyObservers(INotification notification)
        {
			if ( view != null ) view.notifyObservers( notification );
		}

        public virtual void startup(Object app)
        { }
    }
}
