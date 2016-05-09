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

namespace MvcPatterns.Core
{
    public class View : IView
    {
        protected IDictionary observerMap;
        protected static IView instance;

        static View()
        {
            instance = new View();
        }

        protected View()
		{
            observerMap = new Hashtable();
            initializeView();
		}
		
        protected virtual void initializeView()
		{ }

        public static IView getInstance() 
		{
			return instance;
		}

		public void registerObserver (String notificationName, IObserver observer)
		{
			if(!observerMap.Contains(notificationName)) 
            {
                observerMap[notificationName] = new ArrayList();
			}
            ((IList)observerMap[notificationName]).Add(observer);
		}

		public void notifyObservers(INotification notification)
		{
            if(observerMap.Contains(notification.getName())) 
            {
                IList observers = (IList)observerMap[notification.getName()];
                
                for (int i = 0; i < observers.Count; i++)
                {
                    IObserver observer = (IObserver)observers[i];
                    observer.notifyObserver(notification);
                }
            }
		}
    }
}
