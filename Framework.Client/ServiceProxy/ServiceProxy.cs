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

namespace Framework.Client.ServiceProxy
{
    public class ServiceProxy
    {
        public ServiceProxy() {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ResponseInfo execute(RequestInfo info)
        {
            Framework.Service.Provider.IServiceProvider service = ServiceHomeCache.getService();

            return service.execute(info);
        }
    }
}
