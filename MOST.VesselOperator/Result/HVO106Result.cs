using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.VesselOperator.Result
{
    public class HVO106Result : IPopupResult
    {
        private VORLiquidBulkItem liquidBulkItem = null;

        public VORLiquidBulkItem LiquidBulkItem
        {
            get { return liquidBulkItem; }
            set { liquidBulkItem = value; }
        }
    }
}
