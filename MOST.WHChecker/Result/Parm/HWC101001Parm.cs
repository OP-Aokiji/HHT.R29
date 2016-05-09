using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;

namespace MOST.WHChecker.Parm
{
    public class HWC101001Parm : IPopupParm
    {
        private string totMt;
        private string totM3;
        private string totQty;
        private string whId;
        private string locId;
        private string whTpCd;
        private string cgNo;
        private string jobNo;
        private string vslCallId;
        private string catgCd;
        private string plannedLoc;
        private string shipgNoteNo;

        public string TotMt
        {
            get { return totMt; }
            set { totMt = value; }
        }
        
        public string TotM3
        {
            get { return totM3; }
            set { totM3 = value; }
        }

        public string TotQty
        {
            get { return totQty; }
            set { totQty = value; }
        }

        public string WhId
        {
            get { return whId; }
            set { whId = value; }
        }

        public string LocId
        {
            get { return locId; }
            set { locId = value; }
        }

        public string WhTpCd
        {
            get { return whTpCd; }
            set { whTpCd = value; }
        }

        public string CgNo
        {
            get { return cgNo; }
            set { cgNo = value; }
        }

        public string JobNo
        {
            get { return jobNo; }
            set { jobNo = value; }
        }

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

        public string CatgCd
        {
            get { return catgCd; }
            set { catgCd = value; }
        }

        public string PlannedLoc
        {
            get { return plannedLoc; }
            set { plannedLoc = value; }
        }

        public string ShipgNoteNo
        {
            get { return shipgNoteNo; }
            set { shipgNoteNo = value; }
        }
    }
}
