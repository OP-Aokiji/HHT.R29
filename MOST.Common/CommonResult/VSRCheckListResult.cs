using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonResult
{
    public class VSRCheckListResult : IPopupResult
    {
        private string shiftDt;
        private string shiftId;
        private string hatchNo;
        private string hatchDrt;
        private string gangNo;
        private string stevedore;
        private string eqNo;
        private string facNo;
        private string top;
        private string clean;
        private string cgTpCd;

        public string ShiftDt
        {
            get { return shiftDt; }
            set { shiftDt = value; }
        }

        public string ShiftId
        {
            get { return shiftId; }
            set { shiftId = value; }
        }

        public string HatchNo
        {
            get { return hatchNo; }
            set { hatchNo = value; }
        }

        public string HatchDrt
        {
            get { return hatchDrt; }
            set { hatchDrt = value; }
        }

        public string GangNo
        {
            get { return gangNo; }
            set { gangNo = value; }
        }

        public string Stevedore
        {
            get { return stevedore; }
            set { stevedore = value; }
        }

        public string EqNo
        {
            get { return eqNo; }
            set { eqNo = value; }
        }

        public string FacNo
        {
            get { return facNo; }
            set { facNo = value; }
        }
       
        public string Top
        {
            get { return top; }
            set { top = value; }
        }

        public string Clean
        {
            get { return clean; }
            set { clean = value; }
        }

        public string CgTpCd
        {
            get { return cgTpCd; }
            set { cgTpCd = value; }
        }
    }
}
