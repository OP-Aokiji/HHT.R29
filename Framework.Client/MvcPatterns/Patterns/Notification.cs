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

namespace MvcPatterns.Patterns
{
    public class Notification : INotification
    {
        private String name;
        private String type;
        private Object body;

        public Notification(String name)
            : this(name, null, null)
		{ }

        public Notification(String name, Object body)
            : this(name, body, null)
		{ }

        public Notification(String name, Object body, String type)
		{
			this.name = name;
			this.body = body;
			this.type = type;
		}
		
		public String getName()
		{
			return name;
		}
		
		public void setBody(Object body)
		{
			this.body = body;
		}
		
		public Object getBody()
		{
			return body;
		}

		public void setType(String type)
        {
			this.type = type;
		}
		
		public String getType()
        {
			return type;
		}

		public String toString()
		{
			String msg = "Notification Name: "+ getName();
			msg += "\nBody:"+ (( body == null )? "null" : body.ToString());
			msg += "\nType:"+ (( type == null )? "null" : type);
			return msg;
		}
    }
}
