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
using Framework.Service.Provider;
using Framework.Service.Provider.WebService.Provider;

namespace Framework.Client.ServiceProxy
{
    public class ServiceHomeCache
    {
        private static Hashtable homes = new Hashtable();
        private const String serviceKey = "service";

        public ServiceHomeCache() {}

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
        /// <returns></returns>
        public static Framework.Service.Provider.IServiceProvider getService()
        {
            if (homes.ContainsKey(serviceKey))
            {
                return (Framework.Service.Provider.IServiceProvider)homes[serviceKey];
            }
            else
            {
                Framework.Service.Provider.IServiceProvider serviceProvider = new Framework.Service.Provider.WebServiceProvider();
                homes.Add(serviceKey, serviceProvider);

                return serviceProvider;
            }
        }
    }
}
