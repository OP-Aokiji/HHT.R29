using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;

namespace MOST.VesselOperator.Parm
{
    public class HVO101001Parm : IPopupParm
    {
        private string dblBankingVslCallId = null;  // Mother vessel
        private SearchJPVCResult stsJPVC = null;    // 2nd vessel

        public string DblBankingVslCallId
        {
            get { return dblBankingVslCallId; }
            set { dblBankingVslCallId = value; }
        }
        public SearchJPVCResult StsJpvcInfo
        {
            get { return stsJPVC; }
            set { stsJPVC = value; }
        }
    }
}
