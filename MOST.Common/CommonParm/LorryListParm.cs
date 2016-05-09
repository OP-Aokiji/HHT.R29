using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonParm
{
    public class LorryListParm : IPopupParm
    {
        private string jpvc;
        private string snNo;
        private string blNo;
        private string lorryNo;
        private string grNo;

        //added by William (2015 July 10): Lorry list is missing
        private string doNo;

        public string DoNo
        {
            get { return doNo; }
            set { doNo = value; }
        }

        public string Jpvc
        {
            get { return jpvc; }
            set { jpvc = value; }
        }

        public string SnNo
        {
            get { return snNo; }
            set { snNo = value; }
        }

        public string BlNo
        {
            get { return blNo; }
            set { blNo = value; }
        }

        public string LorryNo
        {
            get { return lorryNo; }
            set { lorryNo = value; }
        }

        public string GrNo
        {
            get { return grNo; }
            set { grNo = value; }
        }
    }
}
