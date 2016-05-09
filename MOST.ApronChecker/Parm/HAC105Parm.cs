using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;
using MOST.ApronChecker.Result;

namespace MOST.ApronChecker.Parm
{
    public class HAC105Parm : IPopupParm
    {
        //private HAC102Result cgOprSettings = null;
        //private SearchJPVCResult jpvcInfo = null;
        private CargoExportResult grInfo = null;
        //private string shipgNoteNo = null;
        private string delvTpCd = null;     // {"D", "I"}
        private string opDelvTpCd = null;   // {"D", "I"}
        private string catgCd = null;       // {"E", "I"}
        private string userId = null;
        private string startDt = null;
        private string endDt = null;
        private string tsptr = null;
        private string vslCallId;
        private string isValidated;

        //added by William (2015 July 10): Lorry list is missing
        private string doNo;

        public string DoNo
        {
            get { return doNo; }
            set { doNo = value; }
        } 


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

        public CargoExportResult GrInfo
        {
            get { return grInfo; }
            set { grInfo = value; }
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

        public string VslCallId 
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

        public string IsValidated
        {
            get { return isValidated; }
            set { isValidated = value; }
        }
    }
}
