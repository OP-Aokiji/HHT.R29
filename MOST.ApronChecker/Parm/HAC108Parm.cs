using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;
using MOST.ApronChecker.Result;

namespace MOST.ApronChecker.Parm
{
    public class HAC108Parm : IPopupParm
    {
        private SearchJPVCResult jpvcInfo = null;
        private BLListResult blInfo = null;

        public SearchJPVCResult JpvcInfo
        {
            get { return jpvcInfo; }
            set { jpvcInfo = value; }
        }

        public BLListResult BlInfo
        {
            get { return blInfo; }
            set { blInfo = value; }
        }
    }
}
