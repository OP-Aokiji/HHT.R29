using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;

namespace MOST.Common.CommonParm
{
    public class VSRCheckListParm : IPopupParm
    {
        private SearchJPVCResult jpvcInfo = null;

        public SearchJPVCResult JpvcInfo
        {
            get { return jpvcInfo; }
            set { jpvcInfo = value; }
        }
    }
}
