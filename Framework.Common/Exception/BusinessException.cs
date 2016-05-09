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
    public class BusinessException : PlusBaseException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errCode"></param>
        /// <param name="message"></param>
        public BusinessException(String errCode) : base(errCode) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errCode"></param>
        /// <param name="message"></param>
        public BusinessException(String errCode, String message) : base(errCode, message) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public BusinessException(String message, System.Exception ex) : base(message, ex) { }
    }
}
