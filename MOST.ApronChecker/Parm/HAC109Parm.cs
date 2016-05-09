using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;

namespace MOST.ApronChecker.Parm
{
    public class HAC109Parm : IPopupParm
    {
        private SearchJPVCResult jpvcInfo = null;

        public SearchJPVCResult JpvcInfo
        {
            get { return jpvcInfo; }
            set { jpvcInfo = value; }
        }
    }
}
