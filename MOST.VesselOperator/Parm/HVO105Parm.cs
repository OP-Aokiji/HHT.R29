using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.VesselOperator.Parm
{
    public class HVO105Parm : IPopupParm
    {
        private SearchJPVCResult jpvcInfo = null;
        private SftDblBankingItem sftDblBankingItem;

        public SearchJPVCResult JpvcInfo
        {
            get { return jpvcInfo; }
            set { jpvcInfo = value; }
        }
        public SftDblBankingItem SftDblBankingItem
        {
            get { return sftDblBankingItem; }
            set { sftDblBankingItem = value; }
        }


        private string _transferMode;

        public string transferMode
        {
            get { return _transferMode; }
            set { _transferMode = value; }
        }

    }
}
