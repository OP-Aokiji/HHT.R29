using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;
using MOST.ApronChecker.Result;

namespace MOST.ApronChecker.Parm
{
    public class HAC107Parm : IPopupParm
    {
        private SearchJPVCResult jpvcInfo = null;
        private CargoExportResult grInfo = null;

        public SearchJPVCResult JpvcInfo
        {
            get { return jpvcInfo; }
            set { jpvcInfo = value; }
        }

        public CargoExportResult GrInfo
        {
            get { return grInfo; }
            set { grInfo = value; }
        }
    }
}
