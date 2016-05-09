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
using Framework.Common.Exception;

namespace Framework.Common.Exception
{
    public class NetworkException : PlusBaseException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public NetworkException(string message) : base(message) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public NetworkException(string message, System.Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errCode"></param>
        /// <param name="errMsg"></param>
        /// <param name="ex"></param>
        public NetworkException(string errCode, string errMsg, System.Exception ex) : base(errCode, errMsg, ex) { }
    }
}
