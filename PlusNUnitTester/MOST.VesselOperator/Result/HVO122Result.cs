using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;

namespace MOST.VesselOperator.Result
{
    public class HVO122Result : IPopupResult
    {
        private SearchJPVCResult jpvcInfo = null;

        public SearchJPVCResult JpvcInfo
        {
            get { return jpvcInfo; }
            set { jpvcInfo = value; }
        }
    }
}
