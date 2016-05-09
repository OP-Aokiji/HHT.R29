using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonParm
{
    public class GRListParm : IPopupParm
    {
        private string jpvc;
        private string shipgNoteNo;
        private string grNo;
        private string delvTpCd;
        private string gateInOut;
        private bool excludeFnlLoading;         // Dot not display items whose status are Final Loading.
        private bool excludeFnlHI;              // Dot not display items whose status are Final Handling-In.

        //Added by Chris 2016-03-09
        private string screenid;

        //added by William (2015/07/21 - HHT) Mantis issue: 49799
        private string lorryNo;
        private string driverId;

        public string DriverId
        {
            get { return driverId; }
            set { driverId = value; }
        }
        public string LorryNo
        {
            get { return lorryNo; }
            set { lorryNo = value; }
        }


        public string Jpvc
        {
            get { return jpvc; }
            set { jpvc = value; }
        }

        public string ShipgNoteNo
        {
            get { return shipgNoteNo; }
            set { shipgNoteNo = value; }
        }

        public string GrNo
        {
            get { return grNo; }
            set { grNo = value; }
        }

        public string DelvTpCd
        {
            get { return delvTpCd; }
            set { delvTpCd = value; }
        }

        public string GateInOut
        {
            get { return gateInOut; }
            set { gateInOut = value; }
        }

        public bool ExcludeFnlLoading
        {
            get { return excludeFnlLoading; }
            set { excludeFnlLoading = value; }
        }

        public bool ExcludeFnlHI
        {
            get { return excludeFnlHI; }
            set { excludeFnlHI = value; }
        }

        public string Screenid
        {
            get { return screenid; }
            set { screenid = value; }
        }
    }
}
