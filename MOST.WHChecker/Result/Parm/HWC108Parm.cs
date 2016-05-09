using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;

namespace MOST.WHChecker.Parm
{
    public class HWC108Parm : IPopupParm
    {
        private string vslCallId = null;
        private string spCaCoCd = null;
        private string cgNo = null;
        //private string stat = null;
        private string cgTpCd = null;
        private string orgBlSn = null;
        private string cgCoCd = null;
        private string orgGrNo = null;
        private string blSn = null;
        private string rhdlMode = null;
        private string opeClassCd = null;
        private string rhdlNo = null;
        private String delvTpCd = null;

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

        public string SpCaCoCd
        {
            get { return spCaCoCd; }
            set { spCaCoCd = value; }
        }

        public string DelvTpCd
        {
            get { return delvTpCd; }
            set { delvTpCd = value; }
        }

        public string CgNo
        {
            get { return cgNo; }
            set { cgNo = value; }
        }

        //public string Stat
        //{
        //    get { return stat; }
        //    set { stat = value; }
        //}

        public string CgTpCd
        {
            get { return cgTpCd; }
            set { cgTpCd = value; }
        }

        public string OrgBlSn
        {
            get { return orgBlSn; }
            set { orgBlSn = value; }
        }

        public string CgCoCd
        {
            get { return cgCoCd; }
            set { cgCoCd = value; }
        }

        public string OrgGrNo
        {
            get { return orgGrNo; }
            set { orgGrNo = value; }
        }

        public string BlSn
        {
            get { return blSn; }
            set { blSn = value; }
        }

        public string RhdlMode
        {
            get { return rhdlMode; }
            set { rhdlMode = value; }
        }

        public string OpeClassCd
        {
            get { return opeClassCd; }
            set { opeClassCd = value; }
        }

        public string RhdlNo
        {
            get { return rhdlNo; }
            set { rhdlNo = value; }
        }
    }
}
