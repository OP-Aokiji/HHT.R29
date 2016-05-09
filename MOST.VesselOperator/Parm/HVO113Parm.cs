using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.VesselOperator.Parm
{
    public class HVO113Parm : IPopupParm
    {
        private SearchJPVCResult jpvcInfo = null;
        //private VORLiquidBulkItem liquidBulkItem = null;

        public SearchJPVCResult JpvcInfo
        {
            get { return jpvcInfo; }
            set { jpvcInfo = value; }
        }

        //public VORLiquidBulkItem LiquidBulkItem
        //{
        //    get { return liquidBulkItem; }
        //    set { liquidBulkItem = value; }
        //}
    }
}
