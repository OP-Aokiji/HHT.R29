using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;
using MOST.ApronChecker.Result;

namespace MOST.ApronChecker.Parm
{
    public class HAC106Parm : IPopupParm
    {
        //private HAC102Result cgOprSettings = null;
        //private SearchJPVCResult jpvcInfo = null;
        private BLListResult blInfo = null;
        //private string shipgNoteNo = null;
        private string delvTpCd = null;     // {"D", "I"}
        private string opDelvTpCd = null;   // {"D", "I"}
        private string catgCd = null;       // {"E", "I"}
        private string userId = null;
        private string startDt = null;
        private string endDt = null;
        private string tsptr = null;
        private string stat = null;
        private string lorryId = null;
        private string tsptTpCd = null;
        private string vslCallId; 

        //public HAC102Result CgOprSettings
        //{
        //    get { return cgOprSettings; }
        //    set { cgOprSettings = value; }
        //}

        //public SearchJPVCResult JpvcInfo
        //{
        //    get { return jpvcInfo; }
        //    set { jpvcInfo = value; }
        //}

        public BLListResult BlInfo
        {
            get { return blInfo; }
            set { blInfo = value; }
        }

        //public string ShipgNoteNo
        //{
        //    get { return shipgNoteNo; }
        //    set { shipgNoteNo = value; }
        //}

        public string DelvTpCd
        {
            get { return delvTpCd; }
            set { delvTpCd = value; }
        }

        public string OpDelvTpCd
        {
            get { return opDelvTpCd; }
            set { opDelvTpCd = value; }
        }

        public string CatgCd
        {
            get { return catgCd; }
            set { catgCd = value; }
        }

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public string StartDt
        {
            get { return startDt; }
            set { startDt = value; }
        }

        public string EndDt
        {
            get { return endDt; }
            set { endDt = value; }
        }

        public string Tsptr
        {
            get { return tsptr; }
            set { tsptr = value; }
        }

        public string Stat
        {
            get { return stat; }
            set { stat = value; }
        }

        public string LorryId
        {
            get { return lorryId; }
            set { lorryId = value; }
        }

        public string TsptTpCd
        {
            get { return tsptTpCd; }
            set { tsptTpCd = value; }
        }

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }
    }
}
