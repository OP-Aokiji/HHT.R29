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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorId"></param>
        public ServiceException(String errorId) : base(errorId) { }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public ServiceException(String message, System.Exception ex) : base(message, ex){}
    }
}
