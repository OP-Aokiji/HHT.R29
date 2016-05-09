using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Framework.Common.PopupManager;

namespace MOST.WHChecker.Result
{
    public class HWC101002Result : IPopupResult
    {
        private ArrayList arrWHLocation = null;
        private string vslCallId;
        private string whId;
        private string whTpCd;
        private string jobNo;
        private string locId;
        private string tolocId;
        private string totQty;
        private string totM3;
        private string totMt;

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

        public string WhId
        {
            get { return whId; }
            set { whId = value; }
        }

        public string WhTpCd
        {
            get { return whTpCd; }
            set { whTpCd = value; }
        }

        public string JobNo
        {
            get { return jobNo; }
            set { jobNo = value; }
        }

        public string LocId
        {
            get { return locId; }
            set { locId = value; }
        }

        public string TolocId
        {
            get { return tolocId; }
            set { tolocId = value; }
        }

        public string TotQty
        {
            get { return totQty; }
            set { totQty = value; }
        }

        public string TotM3
        {
            get { return totM3; }
            set { totM3 = value; }
        }

        public string TotMt
        {
            get { return totMt; }
            set { totMt = value; }
        }

        public HWC101002Result()
        {
            arrWHLocation = new ArrayList();
        }

        public ArrayList ArrWHLocation
        {
            get { return arrWHLocation; }
            set { arrWHLocation = value; }
        }
    }
}
