using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;

namespace MOST.VesselOperator.Parm
{
    public class HVO117Parm : IPopupParm
    {
        //private SearchJPVCResult jpvcInfo = null;

        //public SearchJPVCResult JpvcInfo
        //{
        //    get { return jpvcInfo; }
        //    set { jpvcInfo = value; }
        //}

        private string vslCallId = null;

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

    }
}
