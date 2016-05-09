using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonResult
{
    public class PartnerCodeListResult : IPopupResult
    {
        private string code;
        private string name;
        private string licsNo;      // license number
        private string licsExprYmd; // license expired date
        private string lorryNo;
        private string lorryId;
        private string lorryCom;
        private string driverNo;
        private string driverId;
        private string driverCom;

        //Added By Chris 2015-10-16
        private string ptnrCD;

        public string PtnrCD
        {
            get { return ptnrCD; }
            set { ptnrCD = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
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

        public string LorryNo
        {
            get { return lorryNo; }
            set { lorryNo = value; }
        }

        public string LorryId
        {
            get { return lorryId; }
            set { lorryId = value; }
        }

        public string LorryCom
        {
            get { return lorryCom; }
            set { lorryCom = value; }
        }

        public string DriverNo
        {
            get { return driverNo; }
            set { driverNo = value; }
        }

        public string DriverId
        {
            get { return driverId; }
            set { driverId = value; }
        }

        public string DriverCom
        {
            get { return driverCom; }
            set { driverCom = value; }
        }
    }
}
