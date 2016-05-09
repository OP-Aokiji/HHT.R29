using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;

namespace MOST.PortSafety.Parm
{
    public class HPS102Parm : IPopupParm
    {
        //private SearchJPVCResult jpvcInfo = null; 

        //public SearchJPVCResult JpvcInfo
        //{
        //    get { return jpvcInfo; }
        //    set { jpvcInfo = value; }
        //}


        private string vslCallId;

        //Added by Chris 2015-10-06 for List of Gate In
        private string delvOdrNo;
        private string blNo;
        private string grNo;
        private string lorryNo;

        public string LorryNo
        {
            get { return lorryNo; }
            set { lorryNo = value; }
        }

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

        public string GrNo
        {
            get { return grNo; }
            set { grNo = value; }
        }

        public string BlNo
        {
            get { return blNo; }
            set { blNo = value; }
        }        

        public string DelvOdrNo
        {
            get { return delvOdrNo; }
            set { delvOdrNo = value; }
        }        

    }
}
