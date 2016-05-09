using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;

namespace MOST.VesselOperator.Parm
{
    public class HVO121Parm : IPopupParm
    {
        private string vslCallId = null;

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }
    }
}
