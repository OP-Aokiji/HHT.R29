#region Explanation
//* --------------------------------------------------------------
//* CHANGE REVISION
//* --------------------------------------------------------------
//* DATE           AUTHOR      	   REVISION    	     Content
//* 2008-05-20   Mr Luis Lee	     1.0          First release.
//* --------------------------------------------------------------
//* CLASS DESCRIPTION
//* --------------------------------------------------------------
#endregion 

using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Common.Exception
{
    public class ServiceException : PlusBaseException
    {
        public ServiceException(String message) : base(message) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public ServiceException(String message, System.Exception ex) : base(message, ex){}
    }
}
