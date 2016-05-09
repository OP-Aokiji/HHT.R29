using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;

namespace MOST.VesselOperator.Parm
{
    public class HVO122Parm : IPopupParm
    {   
        private string bankingType = null;
        private SearchJPVCResult jpvcInfo = null;

        public string BankingType
        {
            get { return bankingType; }
            set { bankingType = value; }
        }

        public SearchJPVCResult JpvcInfo
        {
            get { return jpvcInfo; }
            set { jpvcInfo = value; }
        }
    }
}
