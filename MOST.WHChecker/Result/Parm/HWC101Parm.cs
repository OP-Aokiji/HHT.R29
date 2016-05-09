using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;

namespace MOST.WHChecker.Parm
{
    public class HWC101Parm : IPopupParm
    {
        private CargoExportResult grInfo = null;
        private BLListResult blInfo = null;

        public CargoExportResult GrInfo
        {
            get { return grInfo; }
            set { grInfo = value; }
        }
        public BLListResult BlInfo
        {
            get { return blInfo; }
            set { blInfo = value; }
        }
    }
}
