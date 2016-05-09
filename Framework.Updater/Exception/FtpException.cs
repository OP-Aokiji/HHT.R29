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
    public class FtpException : NetworkException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public FtpException(string message) : base(message) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public FtpException(string message, System.Exception innerException) : base(message, innerException) { }

    }
}
