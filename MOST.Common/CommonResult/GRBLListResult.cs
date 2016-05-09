using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonResult
{
    public class GRBLListResult : IPopupResult
    {
        private string vslCallId;
        private string cgNo;
        private string cgTpCd;
        private string mt;
        private string m3;
        private string qty;

        public string Mt
        {
            get { return mt; }
            set { mt = value; }
        }

        public string M3
        {
            get { return m3; }
            set { m3 = value; }
        }

        public string Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

        public string CgNo
        {
            get { return cgNo; }
            set { cgNo = value; }
        }

        public string CgTpCd
        {
            get { return cgTpCd; }
            set { cgTpCd = value; }
        }
    }
}
