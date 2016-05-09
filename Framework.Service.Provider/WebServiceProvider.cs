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
using System.Collections;
using Framework.Service.Provider.WebService.Provider;
using Framework.Common.Exception;
using Framework.Service.Provider.Config;

namespace Framework.Service.Provider
{
    public class WebServiceProvider : IServiceProvider
    {
        private FacadeService service;
        private Hashtable serviceCache;
        private const String proxyConstant = "proxy";

        public WebServiceProvider(){}
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns>ResponseInfo</returns>
        public ResponseInfo execute(RequestInfo info)
        {
            System.Net.ServicePointManager.CertificatePolicy = new SSLPolicy();  //Add because of SSL

            ResponseInfo returnInfo = null;

            if (serviceCache == null)
            {
                serviceCache = new Hashtable();
            }

            try
            {
                if (serviceCache.ContainsKey(proxyConstant))
                {
                    service = (FacadeService)serviceCache[proxyConstant];
                }
                else
                {
                    IConfig config = new Framework.Service.Provider.Config.Config();
                    String serviceProvider = config.getServierProvider();
                    service = new FacadeService(serviceProvider);
                    serviceCache.Add(proxyConstant, service);
                }

                info.userInfoItem = setUserInfo();
                returnInfo = service.execute(info);

                if (returnInfo.list.Length > 0)
                {
                    if (returnInfo.list[0] is ExceptionItem)
                    {
                        ExceptionItem item = (ExceptionItem)returnInfo.list[0];
                        throw new BusinessException(item.errorID, item.errorMessage);
                    }
                }
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (ServiceException sex)
            {
                throw sex;
            }
            catch (Exception e)
            {
                throw new ServiceException("The HHT Client couldn't connect to the Webservice", e);
            }
            return returnInfo;
        }

        private object[] setUserInfo()
        {
            UserInfoItem item = new UserInfoItem();
            item.userId = Framework.Common.UserInformation.UserInfo.getInstance().UserId;

            object[] obj = new object[] { item };
            return obj;
        }
    }
}
