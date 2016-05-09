using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;

namespace MOST.WHChecker.Parm
{
    public class HWC104Parm : IPopupParm
    {
        private string vslCallId = null;
        private string cgNo = null;

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }
        public string CgNo
        {
            get { return cgNo; }
            set { cgNo = value; }
        }
    }
}
