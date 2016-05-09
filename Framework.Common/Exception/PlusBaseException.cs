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
    public class PlusBaseException : System.ApplicationException
    {
        protected string errCode;
        protected string errMessage;

        /// <summary>
        /// 
        /// </summary>
	    public PlusBaseException()
		{}	

		/// <summary>
        /// 에러코드를 인자로 가지는 생성자
		/// </summary>
		/// <param name="message">예외메시지</param>
        public PlusBaseException(string errCode) : base(errCode)
		{
            this.errCode = errCode;
        }

		/// <summary>
		/// 에러코드, 예외메시지를 인자로 가지는 생성자
		/// </summary>
		/// <param name="errMsgCode">에러코드</param>
		/// <param name="message">에러메시지</param>
        public PlusBaseException(string errCode, string errMsg) : base(errMsg)
		{
            this.errCode = errCode;
            this.errMessage = errMsg;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errMsg"></param>
        /// <param name="ex"></param>
        public PlusBaseException(String errMsg, System.Exception ex) : base(errMsg, ex)
        {
            this.errMessage = errMsg;
        }

        /// <summary>
		/// 에러코드,예외메시지 및 예외객체를 인자로 가지는 생성자
		/// </summary>
		/// <param name="errorCode">에러코드</param>
		/// <param name="message">예외메시지</param>
		/// <param name="ex">예외객체</param>
        public PlusBaseException(string errCode, string errMsg, System.Exception ex) : base(errMsg, ex)
		{
            this.errCode = errCode;
            this.errMessage = errMsg;
		}

        /// <summary>
        /// 에러코드 속성
        /// </summary>
        public string ErrorCode
        {
            get { return errCode; }
            set { errCode = value; }
        }


        public string ErrorMessage
        {
            get { return errMessage; }
            set { errMessage = value; }
        }
    }
}
