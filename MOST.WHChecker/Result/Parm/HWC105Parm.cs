using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;

namespace MOST.WHChecker.Parm
{
    public class HWC105Parm : IPopupParm
    {
        private string vslCallId = null;
        private string blNo = null;
        private string snNo = null;
        private string grNo = null;

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }
        public string BlNo
        {
            get { return blNo; }
            set { blNo = value; }
        }
        public string SnNo
        {
            get { return snNo; }
            set { snNo = value; }
        }
        public string GrNo
        {
            get { return grNo; }
            set { grNo = value; }
        }
    }
}
