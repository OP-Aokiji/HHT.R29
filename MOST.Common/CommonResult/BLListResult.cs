using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonResult
{
    public class BLListResult : IPopupResult
    {
        private String vslCallId;
        private String bl;
        private String doNo;
        private String mt;
        private String m3;
        private String qty;
        private String cgTpCd;      // Cargo Type
        private String fwrAgnt;     // Forwarding Agent
        private String fnlOpeYn;    // Discharging Final
        private String fnlDelvYn;   // H/O Final
        private String cnsneeCd;    // Consignee Code
        private String cnsneeNm;    // Consignee Name
        
        public String VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

        public String Bl
        {
            get { return bl; }
            set { bl = value; }
        }

        public String DoNo
        {
            get { return doNo; }
            set { doNo = value; }
        }

        public String Mt
        {
            get { return mt; }
            set { mt = value; }
        }

        public String M3
        {
            get { return m3; }
            set { m3 = value; }
        }

        public String Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        public string CgTpCd
        {
            get { return cgTpCd; }
            set { cgTpCd = value; }
        }

        public String FwrAgnt
        {
            get { return fwrAgnt; }
            set { fwrAgnt = value; }
        }

        public String FnlOpeYn
        {
            get { return fnlOpeYn; }
            set { fnlOpeYn = value; }
        }

        public String FnlDelvYn
        {
            get { return fnlDelvYn; }
            set { fnlDelvYn = value; }
        }

        public string CnsneeCd
        {
            get { return cnsneeCd; }
            set { cnsneeCd = value; }
        }

        public string CnsneeNm
        {
            get { return cnsneeNm; }
            set { cnsneeNm = value; }
        }
    }
}
