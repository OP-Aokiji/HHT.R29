using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.VesselOperator.Result
{
    public class HVO102Result : IPopupResult
    {
        private string eqNo;

        public string EqNo
        {
            get { return eqNo; }
            set { eqNo = value; }
        }
    }
}
