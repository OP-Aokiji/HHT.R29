using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.VesselOperator.Result
{
    public class HVO103Result : IPopupResult
    {
        private string hatches;

        public string Hatches
        {
            get { return hatches; }
            set { hatches = value; }
        }
    }
}
