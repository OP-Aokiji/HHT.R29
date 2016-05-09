using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonResult
{
    public class LorryListResult : IPopupResult
    {
        private string contractor;
        private string snblNo;
        private string lorryNo;
        private string driver;
        private SearchJPVCResult jpvcInfo = null;

        public string Contractor
        {
            get { return contractor; }
            set { contractor = value; }
        }

        public string SnblNo
        {
            get { return snblNo; }
            set { snblNo = value; }
        }

        public string LorryNo
        {
            get { return lorryNo; }
            set { lorryNo = value; }
        }

        public string Driver
        {
            get { return driver; }
            set { driver = value; }
        }

        public SearchJPVCResult JpvcInfo
        {
            get { return jpvcInfo; }
            set { jpvcInfo = value; }
        }
    }
}
