using System;
using System.Collections.Generic;
using System.Text;

namespace MOST.Common.CommonResult
{
    public class LoadingList
    {
        private String snNo;
        private String grNo;
        private String delvMode;
        private String docMT;
        private String docM3;
        private String docQty;
        private String actMT;
        private String actM3;
        private String actQty;
        private String lorry;
        private String fAgent;
        private String cgType;
        private String shift;
        private String hatch;
        private String loadDate;

        public String Hatch
        {
            get { return hatch; }
            set { hatch = value; }
        }

        public String Shift
        {
            get { return shift; }
            set { shift = value; }
        }

        public String CgType
        {
            get { return cgType; }
            set { cgType = value; }
        }

        public String FAgent
        {
            get { return fAgent; }
            set { fAgent = value; }
        }

        public String Lorry
        {
            get { return lorry; }
            set { lorry = value; }
        }

        public String ActQty
        {
            get { return actQty; }
            set { actQty = value; }
        }

        public String ActM3
        {
            get { return actM3; }
            set { actM3 = value; }
        }

        public String ActMT
        {
            get { return actMT; }
            set { actMT = value; }
        }

        public String DocQty
        {
            get { return docQty; }
            set { docQty = value; }
        }

        public String DocM3
        {
            get { return docM3; }
            set { docM3 = value; }
        }

        public String DocMT
        {
            get { return docMT; }
            set { docMT = value; }
        }

        public String DelvMode
        {
            get { return delvMode; }
            set { delvMode = value; }
        }

        public String GrNo
        {
            get { return grNo; }
            set { grNo = value; }
        }

        public String SnNo
        {
            get { return snNo; }
            set { snNo = value; }
        }

        public String LoadDate
        {
            get { return loadDate; }
            set { loadDate = value; }
        }


    }
}
