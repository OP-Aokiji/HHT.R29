using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;

namespace MOST.ApronChecker.Parm
{
    public class HAC112Parm : IPopupParm
    {
        private string vslCallId = null;
        private string grNo = null;
        private string cgNo = null;
        private string stat = null;
        private string cgTpCd = null;
        private string shipgNoteNo = null;
        private string delvTpCd = null;
        private string catgCd = null;
        private string spCaCoCd = null;
        private string jobCoCd = null;
        private string opeClassCd = null;
        private string rhdlNo = null;
        private string orgVslCallId = null;
        private string orgCgNo = null;
        private string cgCoCd = null;       // Cargo condition: General, Damage, Shut-out {"G", "D", "S"}

        // Fix issue 33939
        private string rhdlGroupNo = null;
        private string orgRefNo = null;

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

        public string GrNo
        {
            get { return grNo; }
            set { grNo = value; }
        }

        public string CgNo
        {
            get { return cgNo; }
            set { cgNo = value; }
        }

        public string Stat
        {
            get { return stat; }
            set { stat = value; }
        }

        public string CgTpCd
        {
            get { return cgTpCd; }
            set { cgTpCd = value; }
        }

        public string ShipgNoteNo
        {
            get { return shipgNoteNo; }
            set { shipgNoteNo = value; }
        }

        public string DelvTpCd
        {
            get { return delvTpCd; }
            set { delvTpCd = value; }
        }

        public string CatgCd
        {
            get { return catgCd; }
            set { catgCd = value; }
        }

        public string SpCaCoCd
        {
            get { return spCaCoCd; }
            set { spCaCoCd = value; }
        }

        public string JobCoCd
        {
            get { return jobCoCd; }
            set { jobCoCd = value; }
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

        public string OrgVslCallId
        {
            get { return orgVslCallId; }
            set { orgVslCallId = value; }
        }

        public string OrgCgNo
        {
            get { return orgCgNo; }
            set { orgCgNo = value; }
        }

        public string CgCoCd
        {
            get { return cgCoCd; }
            set { cgCoCd = value; }
        }

        // Fix issue 33939
        public string RhdlGroupNo
        {
            get { return rhdlGroupNo; }
            set { rhdlGroupNo = value; }
        }

        public string OrgRefNo
        {
            get { return orgRefNo; }
            set { orgRefNo = value; }
        }
    }
}
