using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;

namespace MOST.VesselOperator.Parm
{
    public class HVO109Parm : IPopupParm
    {
        private string dblBankingVslCallId = null;  // Mother vessel
        private SearchJPVCResult jpvcInfo = null;   // 2nd vessel

        public string DblBankingVslCallId
        {
            get { return dblBankingVslCallId; }
            set { dblBankingVslCallId = value; }
        }
        public SearchJPVCResult JpvcInfo
        {
            get { return jpvcInfo; }
            set { jpvcInfo = value; }
        }
    }
}
