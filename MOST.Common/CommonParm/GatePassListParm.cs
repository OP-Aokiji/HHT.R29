using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonParm
{
    public class GatePassListParm : IPopupParm
    {
        private string gatePass;
        private string lorryNo;
        private string wordDate;
        private string shiftId;

        //added by William (2015/07/27 - HHT) Mantis issue 49799
        private string driverId;

        public string DriverId
        {
            get { return driverId; }
            set { driverId = value; }
        }

        public string WordDate
        {
            get { return wordDate; }
            set { wordDate = value; }
        }

        public string ShiftId
        {
            get { return shiftId; }
            set { shiftId = value; }
        }

        public string GatePass
        {
            get { return gatePass; }
            set { gatePass = value; }
        }

        public string LorryNo
        {
            get { return lorryNo; }
            set { lorryNo = value; }
        }
    }
}
