using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonResult
{
    public class GatePassListResult : IPopupResult
    {
        private string gatePass;
        private string vslCallId;
        private string cgNo;
        private string lorryNo;
        private string cgInOutCd;
        private string wgt;
        private string mrsmt;
        private string pkgQty;
        private string catgcd;
        private string tsptr;
        private string seq;
        private string cmdtCd;

        public string CmdtCd
        {
            get { return cmdtCd; }
            set { cmdtCd = value; }
        }

        public string Wgt
        {
            get { return wgt; }
            set { wgt = value; }
        }

        public string Mrsmt
        {
            get { return mrsmt; }
            set { mrsmt = value; }
        }

        public string PkgQty
        {
            get { return pkgQty; }
            set { pkgQty = value; }
        }

        public string GatePass
        {
            get { return gatePass; }
            set { gatePass = value; }
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

        public string LorryNo
        {
            get { return lorryNo; }
            set { lorryNo = value; }
        }

        public string CgInOutCd
        {
            get { return cgInOutCd; }
            set { cgInOutCd = value; }
        }

        public string Catgcd
        {
            get { return catgcd; }
            set { catgcd = value; }
        }

        public string Tsptr
        {
            get { return tsptr; }
            set { tsptr = value; }
        }

        public string Seq
        {
            get { return seq; }
            set { seq = value; }
        }
    }
}
