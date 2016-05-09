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
using Framework.Client.ServiceProxy;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.Client.Proxy.LoginProxy
{
    public class LoginProxy : BaseServiceProxy, ILoginProxy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public ResponseInfo login(LoginParm parm)
        {
            return this.execute("AuthBean", "getHHTUserInfo", parm);
        }
    }
}
