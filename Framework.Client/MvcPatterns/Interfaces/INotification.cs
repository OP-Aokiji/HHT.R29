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
    public interface INotification
    {
		String getName();
        void setBody(Object body);
		Object getBody();
		void setType(String type);
		String getType();
        String toString();
    }
}
