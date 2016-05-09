using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;

namespace MOST.WHChecker.Parm
{
    public class HWC102Parm : IPopupParm
    {
        private string vslCallId;
        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }
    }
}
