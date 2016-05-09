using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Service.Provider.WebService.Provider
{
    partial class FacadeService
    {
        public FacadeService(string serviceUrl)
            : this()
        {
            this.Url = serviceUrl;
        }
    }
}
