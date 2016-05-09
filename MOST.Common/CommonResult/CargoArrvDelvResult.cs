using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonResult
{
    //added by William (2015/08/04 - HHT) Mantis issue 49799
    public class CargoArrvDelvResult : IPopupResult
    {
        private string cgNo;
        private string cgInOutCd;
        private string seq;
        private string wgt;
        private string pkgQty;
        private string cmdCd;
        private string gateInDt;
        private string gatePassNo;
        private string grNo;
        private string driverId;
        private string driverNm;
        private string licsNo;
        private string licsExprYmd;
        private string tsptr;

        //Added by Chris 2015-10-14
        private string vslCallId;

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
        public string CgInOutCd
        {
            get { return cgInOutCd; }
            set { cgInOutCd = value; }
        }
        public string Seq
        {
            get { return seq; }
            set { seq = value; }
        }
        public string Wgt
        {
            get { return wgt; }
            set { wgt = value; }
        }
        public string PkgQty
        {
            get { return pkgQty; }
            set { pkgQty = value; }
        }
        public string CmdCd
        {
            get { return cmdCd; }
            set { cmdCd = value; }
        }
        public string GateInDt
        {
            get { return gateInDt; }
            set { gateInDt = value; }
        }
        public string GatePassNo
        {
            get { return gatePassNo; }
            set { gatePassNo = value; }
        }
        public string GrNo
        {
            get { return grNo; }
            set { grNo = value; }
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
        public string LicsNo
        {
            get { return licsNo; }
            set { licsNo = value; }
        }
        public string LicsExprYmd
        {
            get { return licsExprYmd; }
            set { licsExprYmd = value; }
        }
        public string Tsptr
        {
            get { return tsptr; }
            set { tsptr = value; }
        }
    }
}
