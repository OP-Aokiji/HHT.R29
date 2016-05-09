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

namespace Framework.Common.ExceptionHandler
{
    public class ExceptionHandler
    {
        public ExceptionHandler() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public static void ErrorHandler(System.Exception e)
        {
            if (e is Framework.Common.Exception.NetworkException)
            {
                Console.WriteLine("Error Code = " + ((NetworkException)e).ErrorCode);
                Console.WriteLine("Error Message = " + ((NetworkException)e).ErrorMessage);
            }
            else
            {
                Console.WriteLine(e.Message);
            }
            new frmExceptionMsg(e).ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentForm"></param>
        /// <param name="e"></param>
        public static void ErrorHandler(System.Windows.Forms.Form currentForm, System.Exception e)
        {
            new frmExceptionMsg(currentForm, e).ShowDialog();
        }
    }
}
