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

namespace Framework.Service.Provider
{
    public interface IServiceProvider
    {
        ResponseInfo execute(RequestInfo info);
    }
}
