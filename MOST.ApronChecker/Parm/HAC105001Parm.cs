using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;
using MOST.ApronChecker.Result;

namespace MOST.ApronChecker.Parm
{
    public class HAC105001Parm : IPopupParm
    {
        HAC105001Result ldCnclResult = null;
        private string vslCallId = null;
        private string cgNo = null;
        private string docMt = null;
        private string docM3 = null;
        private string docQty = null;
        private double balMt = 0;
        private double balM3 = 0;
        private int balQty = 0;
        private string opDelvTpCd = null;   // {"D", "I"}
        private string shipgNoteNo;
        private string cgTpCd;

        public HAC105001Result LdCnclResult
        {
            get { return ldCnclResult; }
            set { ldCnclResult = value; }
        }

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
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

        public double BalMt
        {
            get { return balMt; }
            set { balMt = value; }
        }

        public double BalM3
        {
            get { return balM3; }
            set { balM3 = value; }
        }

        public int BalQty
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
    }
}
