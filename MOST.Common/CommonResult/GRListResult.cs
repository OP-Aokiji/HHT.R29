using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonResult
{
    public class GRListResult : IPopupResult
    {
        private string grNo;
        private string lorry;
        private string qty;
        private string mt;
        private string m3;
        private string shipgNoteNo;
        private string tsptr;       // Transporter Company Code
        private string tsptrCompNm; // Transporter Company Name
        private string cgTpCd;      // Cargo Type
        private string delvTpCd;    // Delivery Type Cd
        private string vslCallId;
        private string delvTpNm;
        private string dgYn;
        private string dgStatCd;
        private string cmdtCd;
        private string blNo;

        //Added by Chris 2015/11/18
        private string driverId;
        private string lic;
        private string expire;

        public string DriverId
        {
            get { return driverId; }
            set { driverId = value; }
        }
        
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

        private string cgInOutCd;

        public string CgInOutCd
        {
            get { return cgInOutCd; }
            set { cgInOutCd = value; }
        }

        //Added by Chris 2015-10-14
        private string seq;

        public string Seq
        {
            get { return seq; }
            set { seq = value; }
        }

        public string BlNo
        {
            get { return blNo; }
            set { blNo = value; }
        }

        public string CmdtCd
        {
            get { return cmdtCd; }
            set { cmdtCd = value; }
        }

        //private string spYn;
        //private string fnlOpeYn;
        //private string hiFnlYn;
        //private string rhdlMode;
        //private string dmgYn;
        //private string shuYn;

        private string spCargoChk;   //check spare cargo (for Port Safety)  
        
        public string GrNo
        {
            get { return grNo; }
            set { grNo = value; }
        }        

        public string Lorry
        {
            get { return lorry; }
            set { lorry = value; }
        }

        public string Qty
        {
            get { return qty; }
            set { qty = value; }
        }

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

        public string ShipgNoteNo
        {
            get { return shipgNoteNo; }
            set { shipgNoteNo = value; }
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

        public string CgTpCd
        {
            get { return cgTpCd; }
            set { cgTpCd = value; }
        }

        public string DelvTpCd
        {
            get { return delvTpCd; }
            set { delvTpCd = value; }
        }

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

        public string DelvTpNm
        {
            get { return delvTpNm; }
            set { delvTpNm = value; }
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

        //public string SpYn
        //{
        //    get { return spYn; }
        //    set { spYn = value; }
        //}
        //public string FnlOpeYn
        //{
        //    get { return fnlOpeYn; }
        //    set { fnlOpeYn = value; }
        //}
        //public string HiFnlYn
        //{
        //    get { return hiFnlYn; }
        //    set { hiFnlYn = value; }
        //}
        //public string RhdlMode
        //{
        //    get { return rhdlMode; }
        //    set { rhdlMode = value; }
        //}
        //public string DmgYn
        //{
        //    get { return dmgYn; }
        //    set { dmgYn = value; }
        //}
        //public string ShuYn
        //{
        //    get { return shuYn; }
        //    set { shuYn = value; }
        //}
        public string SpCargoChk
        {
            get { return spCargoChk; }
            set { spCargoChk = value; }
        }
    }
}
