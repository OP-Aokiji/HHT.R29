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
using Framework.Service.Provider;
using Framework.Service.Provider.WebService.Provider;
using Framework.Common.Exception;
using Framework.Common.UserInformation;

namespace Framework.Client.ServiceProxy
{
    public class ServiceDelegate
    {
        private ServiceProxy proxy;

        /// <summary>
        /// 
        /// </summary>
        public ServiceDelegate()
        {
            proxy = new ServiceProxy();
        }

        internal ServiceProxy ServiceProxy
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
        /// <param name="obj"></param>
        /// <returns></returns>
        public ResponseInfo execute(String serviceID, String methodName, object[] obj)
        {
            ResponseInfo returnInfo;
            try
            {
                //if (!(serviceID.Equals("AuthBean") && methodName.Equals("getHHTUserInfo")))
                //{
                //    if (UserInfo.getInstance().getAccessItem().Count == 0 && UserInfo.getInstance().getMenuItem().Count == 0)
                //    {
                //        throw new BusinessException("HHT_000001");
                //    }
                //}

                RequestInfo info = new RequestInfo();
                info.serviceID = serviceID;
                info.methodName = methodName;
                info.list = obj;

                DateTime startTime = System.DateTime.Now;

                //###############################################
                // This code will call webservice
                returnInfo = proxy.execute(info);
                //###############################################

                DateTime endTime = System.DateTime.Now;

                //StringBuilder sb = new StringBuilder();
                //sb.Append("\n#------------------------------------------------#\n");
                //sb.Append("[Request Time : " + startTime + "]\n");
                //sb.Append("[Service ID   : " + serviceID + "]\n");
                //sb.Append("[Method Name  : " + methodName + "]\n");
                //sb.Append("[Runnig Time  : " + endTime.Subtract(startTime) + "]\n");
                //sb.Append("#------------------------------------------------#\n");
                //System.Console.WriteLine(sb.ToString());
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            return returnInfo;
        }
    }
}
