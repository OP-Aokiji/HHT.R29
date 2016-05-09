using System;
using System.Collections.Generic;
using System.Text;

namespace MOST.WHChecker.Item
{
    public class WHLocationItem
    {
        private string whId;
        private string whTpCd;
        private string jobNo;
        private string locId;
        private string tolocId;
        private string totQty;
        private string totM3;
        private string totMt;
        private string bay;
        private string row;

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
        
        public string Bay
        {
            get { return bay; }
            set { bay = value; }
        }

        public string Row
        {
            get { return row; }
            set { row = value; }
        }
    }
}
