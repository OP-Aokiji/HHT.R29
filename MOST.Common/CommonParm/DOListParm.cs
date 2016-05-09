using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonParm
{
    public class DOListParm : IPopupParm
    {
        private String jpvc;
        private String doNo;

        //added by William (2015/07/21 - HHT) Mantis issue: 49799
        private String lorryNo;
        private String driverId;

        public String LorryNo
        {
            get { return lorryNo; }
            set { lorryNo = value; }
        }
       

        public String DriverId
        {
            get { return driverId; }
            set { driverId = value; }
        }


        public String Jpvc
        {
            get { return jpvc; }
            set { jpvc = value; }
        }

        public String DoNo
        {
            get { return doNo; }
            set { doNo = value; }
        }
    }
}
