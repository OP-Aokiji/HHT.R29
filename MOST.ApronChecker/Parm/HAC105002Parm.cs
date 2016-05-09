using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;
using MOST.ApronChecker.Result;

namespace MOST.ApronChecker.Parm
{
    public class HAC105002Parm : IPopupParm
    {
        HAC105002Result whDmgSprRes = null;
        private string vslCallId = null;
        private string cgNo = null;
        private string docMt = null;
        private string docM3 = null;
        private string docQty = null;
        private string balMt = null;
        private string balM3 = null;
        private string balQty = null;
        private string opDelvTpCd = null;   // {"D", "I"}
        private string shipgNoteNo;
        private string cgTpCd;

        private double whDmgBalMt;
        private double whDmgBalM3;
        private int whDmgBalQty;
        private int locDmgCount;
        private double sprBalMt;
        private double sprBalM3;
        private int sprBalQty;
        private string spCaCoCd;

        public HAC105002Result WhDmgSprRes
        {
            get { return whDmgSprRes; }
            set { whDmgSprRes = value; }
        }

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

        public string SpCaCoCd
        {
            get { return spCaCoCd; }
            set { spCaCoCd = value; }
        }

        public string CgNo
        {
            get { return cgNo; }
            set { cgNo = value; }
        }

        public string DocMt
        {
            get { return docMt; }
            set { docMt = value; }
        }

        public string DocM3
        {
            get { return docM3; }
            set { docM3 = value; }
        }

        public string DocQty
        {
            get { return docQty; }
            set { docQty = value; }
        }

        public string BalMt
        {
            get { return balMt; }
            set { balMt = value; }
        }

        public string BalM3
        {
            get { return balM3; }
            set { balM3 = value; }
        }

        public string BalQty
        {
            get { return balQty; }
            set { balQty = value; }
        }

        public string OpDelvTpCd
        {
            get { return opDelvTpCd; }
            set { opDelvTpCd = value; }
        }

        public string ShipgNoteNo
        {
            get { return shipgNoteNo; }
            set { shipgNoteNo = value; }
        }
        public string CgTpCd
        {
            get { return cgTpCd; }
            set { cgTpCd = value; }
        }



        public double WhDmgBalMt
        {
            get { return whDmgBalMt; }
            set { whDmgBalMt = value; }
        }

        public double WhDmgBalM3
        {
            get { return whDmgBalM3; }
            set { whDmgBalM3 = value; }
        }

        public int WhDmgBalQty
        {
            get { return whDmgBalQty; }
            set { whDmgBalQty = value; }
        }

        public int LocDmgCount
        {
            get { return locDmgCount; }
            set { locDmgCount = value; }
        }

        public double SprBalMt
        {
            get { return sprBalMt; }
            set { sprBalMt = value; }
        }

        public double SprBalM3
        {
            get { return sprBalM3; }
            set { sprBalM3 = value; }
        }

        public int SprBalQty
        {
            get { return sprBalQty; }
            set { sprBalQty = value; }
        }
    }
}
