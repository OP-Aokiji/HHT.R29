using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;

namespace MOST.WHChecker.Parm
{
    public class HWC106Parm : IPopupParm
    {
        private string vslCallId;
        private string opeClassCd;
        private string snBlNo;
        private string whTpCd;
        private string whTpNm;
        private string cgNo;
        private string wgt;
        private string msrmt;
        private string pkgQty;

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }
        public string OpeClassCd
        {
            get { return opeClassCd; }
            set { opeClassCd = value; }
        }
        public string SnBlNo
        {
            get { return snBlNo; }
            set { snBlNo = value; }
        }
        public string WhTpCd
        {
            get { return whTpCd; }
            set { whTpCd = value; }
        }

        public string WhTpNm
        {
            get { return whTpNm; }
            set { whTpNm = value; }
        }
        public string CgNo
        {
            get { return cgNo; }
            set { cgNo = value; }
        }
        public string Wgt
        {
            get { return wgt; }
            set { wgt = value; }
        }
        public string Msrmt
        {
            get { return msrmt; }
            set { msrmt = value; }
        }
        public string PkgQty
        {
            get { return pkgQty; }
            set { pkgQty = value; }
        }
    }
}
