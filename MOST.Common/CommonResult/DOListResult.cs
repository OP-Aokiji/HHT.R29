using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonResult
{
    public class DOListResult : IPopupResult
    {
        private String bl;
        private String doNo;
        private String mt;
        private String m3;
        private String qty;
        private string tsptr;       // Transporter Company Code
        private string tsptrCompNm; // Transporter Company Name
        private string vslCallId;
        private string dgYn;
        private string dgStatCd;
        private string delvTpCd;    // Delivery Type Cd
        private string delvTpNm;
        private string cmdtCd;

        //Added by Chris 2015/11/18
        private string driverId;
        private string driverNm;
        private string lic;
        private string expire;

        public string Lic
        {
            get { return lic; }
            set { lic = value; }
        }


        public string Expire
        {
            get { return expire; }
            set { expire = value; }
        }

        public string DriverId
        {
            get { return driverId; }
            set { driverId = value; }
        }
        

        public string DriverNm
        {
            get { return driverNm; }
            set { driverNm = value; }
        }

        public string CmdtCd
        {
            get { return cmdtCd; }
            set { cmdtCd = value; }
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

        public string Tsptr
        {
            get { return tsptr; }
            set { tsptr = value; }
        }

        public string TsptrCompNm
        {
            get { return tsptrCompNm; }
            set { tsptrCompNm = value; }
        }

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

        public string DgYn
        {
            get { return dgYn; }
            set { dgYn = value; }
        }

        public string DgStatCd
        {
            get { return dgStatCd; }
            set { dgStatCd = value; }
        }

        public string DelvTpCd
        {
            get { return delvTpCd; }
            set { delvTpCd = value; }
        }

        public string DelvTpNm
        {
            get { return delvTpNm; }
            set { delvTpNm = value; }
        }
    }
}
