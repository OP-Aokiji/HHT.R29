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
using Framework.Service.Provider.WebService.Provider;
using System.Collections;

namespace Framework.Client.ServiceProxy
{
    public class BaseServiceProxy
    {
        public BaseServiceProxy(){}

        internal ServiceDelegate ServiceDelegate
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceID"></param>
        /// <param name="methodName"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        public ResponseInfo execute(String serviceID, String methodName, object parm)
        {
            ResponseInfo returnInfo;
            try
            {
                object[] obj = new object[] { parm };
                returnInfo = new ServiceDelegate().execute(serviceID, methodName, obj);
            }
            catch (Framework.Common.Exception.BusinessException ex)
            {
                throw ex;
            }
            return returnInfo;
        }

        //public ResponseInfo execute(String serviceID, String methodName, ArrayList items)
        //{
        //    ResponseInfo returnInfo;
        //    try
        //    {
        //        object[] obj = items.ToArray();
        //        returnInfo = new ServiceDelegate().execute(serviceID, methodName, obj);
        //    }
        //    catch (Framework.Common.Exception.BusinessException ex)
        //    {
        //        throw ex;
        //    }
        //    return returnInfo;
        //}
    }
}