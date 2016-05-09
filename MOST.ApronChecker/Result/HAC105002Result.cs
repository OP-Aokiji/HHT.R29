using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.WHChecker.Result;

namespace MOST.ApronChecker.Result
{
    public class HAC105002Result : IPopupResult
    {
        private HWC101002Result whDmgUnsetLocRes = null;    // set location Damage
        private HWC101002Result spareUnsetLocRes = null;   // set location Shut-out
        private bool autoDmgLocFlag;
        //private string dmgRhdlMode;
        //private string gatePassYn;
        private int whDmgQty;
        private double whDmgMt;
        private double whDmgM3;
        private string whDmgLocId;
        private int sprQty;
        private double sprMt;
        private double sprM3;
        private string sprLocId;

        public bool AutoDmgLocFlag
        {
            get { return autoDmgLocFlag; }
            set { autoDmgLocFlag = value; }
        }
        //public string DmgRhdlMode
        //{
        //    get { return dmgRhdlMode; }
        //    set { dmgRhdlMode = value; }
        //}
        //public string GatePassYn
        //{
        //    get { return gatePassYn; }
        //    set { gatePassYn = value; }
        //}
        public int WhDmgQty
        {
            get { return whDmgQty; }
            set { whDmgQty = value; }
        }
        public double WhDmgMt
        {
            get { return whDmgMt; }
            set { whDmgMt = value; }
        }
        public double WhDmgM3
        {
            get { return whDmgM3; }
            set { whDmgM3 = value; }
        }
        public string WhDmgLocId
        {
            get { return whDmgLocId; }
            set { whDmgLocId = value; }
        }
        public int SprQty
        {
            get { return sprQty; }
            set { sprQty = value; }
        }
        public double SprMt
        {
            get { return sprMt; }
            set { sprMt = value; }
        }
        public double SprM3
        {
            get { return sprM3; }
            set { sprM3 = value; }
        }
        public string SprLocId
        {
            get { return sprLocId; }
            set { sprLocId = value; }
        }
        public HWC101002Result WhDmgUnsetLocRes
        {
            get { return whDmgUnsetLocRes; }
            set { whDmgUnsetLocRes = value; }
        }
        public HWC101002Result SpareUnsetLocRes
        {
            get { return spareUnsetLocRes; }
            set { spareUnsetLocRes = value; }
        }
    }
}
