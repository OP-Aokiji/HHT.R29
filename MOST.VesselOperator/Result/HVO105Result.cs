using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.VesselOperator.Result
{
    public class HVO105Result : IPopupResult
    {
        private SftDblBankingItem sftDblBankingItem;

        public HVO105Result()
        {
            //sftDblBankingItem = new SftDblBankingItem();
        }

        public SftDblBankingItem SftDblBankingItem
        {
            get { return sftDblBankingItem; }
            set { sftDblBankingItem = value; }
        }


    }
}
