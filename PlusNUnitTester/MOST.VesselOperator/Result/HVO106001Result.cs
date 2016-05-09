using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.VesselOperator.Result
{
    public class HVO106001Result : IPopupResult
    {
        private string hoseOnDt;
        private string hoseOffDt;
        private string commenceDt;
        private string completeDt;
        private string prevHoseOnDt;
        private string prevHoseOffDt;
        private string prevCommenceDt;
        private string prevCompleteDt;

        public string HoseOnDt
        {
            get { return hoseOnDt; }
            set { hoseOnDt = value; }
        }

        public string HoseOffDt
        {
            get { return hoseOffDt; }
            set { hoseOffDt = value; }
        }

        public string CommenceDt
        {
            get { return commenceDt; }
            set { commenceDt = value; }
        }

        public string CompleteDt
        {
            get { return completeDt; }
            set { completeDt = value; }
        }

        public string PrevHoseOnDt
        {
            get { return prevHoseOnDt; }
            set { prevHoseOnDt = value; }
        }

        public string PrevHoseOffDt
        {
            get { return prevHoseOffDt; }
            set { prevHoseOffDt = value; }
        }

        public string PrevCommenceDt
        {
            get { return prevCommenceDt; }
            set { prevCommenceDt = value; }
        }

        public string PrevCompleteDt
        {
            get { return prevCompleteDt; }
            set { prevCompleteDt = value; }
        }
    }
}
