using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;

namespace MOST.WHChecker.Parm
{
    public class HWC101002Parm : IPopupParm
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
        private string shipgNoteNo;
        private string catgCd;
        private string spCaCoCd;
        private string blSn;
        private bool isGeneralCg;
        private bool isShutoutCg;
        private bool isDamageCg;
        
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
        
        public string ShipgNoteNo
        {
            get { return shipgNoteNo; }
            set { shipgNoteNo = value; }
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

        public string BlSn
        {
            get { return blSn; }
            set { blSn = value; }
        }

        public bool IsGeneralCg
        {
            get { return isGeneralCg; }
            set { isGeneralCg = value; }
        }

        public bool IsShutoutCg
        {
            get { return isShutoutCg; }
            set { isShutoutCg = value; }
        }

        public bool IsDamageCg
        {
            get { return isDamageCg; }
            set { isDamageCg = value; }
        }
    }
}
