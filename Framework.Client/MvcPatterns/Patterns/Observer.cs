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
using System.Reflection;
using MvcPatterns.Interfaces;

namespace MvcPatterns.Patterns
{
    public class Observer : IObserver
    {
        private String notify;
		private Object context;
	
        public Observer(String notifyMethod, Object notifyContext) 
		{
            this.notify = notifyMethod;
            this.context = notifyContext;
		}

		public void notifyObserver(INotification notification)
		{
            Type t = this.context.GetType();
            BindingFlags f = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
            MethodInfo mi = t.GetMethod(this.notify, f);
            mi.Invoke(this.context, new Object[] { notification });
		}
    }
}
