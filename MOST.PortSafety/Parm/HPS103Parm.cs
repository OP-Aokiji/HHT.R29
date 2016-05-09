using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;

namespace MOST.PortSafety.Parm
{
    public class HPS103Parm : IPopupParm
    {
        //private SearchJPVCResult jpvcInfo = null;

        //public SearchJPVCResult JpvcInfo
        //{
        //    get { return jpvcInfo; }
        //    set { jpvcInfo = value; }
        //}

        private string vslCallId;

        //Added by Chris 2015-10-08 Gate In - Gate Out
        private string grNo;
        private string gatePassNo;

        public string GrNo
        {
            get { return grNo; }
            set { grNo = value; }
        }        

        public string GatePassNo
        {
            get { return gatePassNo; }
            set { gatePassNo = value; }
        }

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

    }
}
