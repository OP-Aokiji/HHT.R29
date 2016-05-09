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
            if (e is BusinessException)
            {
                Console.WriteLine("Error Code = " + ((BusinessException)e).ErrorCode);
                Console.WriteLine("Error Message = " + ((BusinessException)e).ErrorMessage);
            }
            else
            {
                Console.WriteLine(e.Message);
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentForm"></param>
        /// <param name="e"></param>
        public static void ErrorHandler(System.Windows.Forms.Form currentForm, System.Exception e)
        {
            //frmExceptionMsg frm = new frmExceptionMsg(currentForm, e);
            //frm.ShowDialog();
            new frmExceptionMsg(currentForm, e).ShowDialog();
        }
    }
}
