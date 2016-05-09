using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.WHChecker.Result;

namespace MOST.ApronChecker.Result
{
    public class HAC105001Result : IPopupResult
    {
        private HWC101001Result dmgSetLocRes = null;    // set location Damage
        private HWC101001Result shutSetLocRes = null;   // set location Shut-out
        private string shutRhdlMode;
        private string dmgRhdlMode;
        private string gatePassYn;
        private string shuQty;
        private string shuMt;
        private string shuM3;
        private string shuLocId;
        private string dmgQty;
        private string dmgMt;
        private string dmgM3;
        private string dmgLocId;
        private string remark;

        public string ShutRhdlMode
        {
            get { return shutRhdlMode; }
            set { shutRhdlMode = value; }
        }
        public string DmgRhdlMode
        {
            get { return dmgRhdlMode; }
            set { dmgRhdlMode = value; }
        }
        public string GatePassYn
        {
            get { return gatePassYn; }
            set { gatePassYn = value; }
        }
        public string ShuQty
        {
            get { return shuQty; }
            set { shuQty = value; }
        }
        public string ShuMt
        {
            get { return shuMt; }
            set { shuMt = value; }
        }
        public string ShuM3
        {
            get { return shuM3; }
            set { shuM3 = value; }
        }
        public string ShuLocId
        {
            get { return shuLocId; }
            set { shuLocId = value; }
        }
        public string DmgQty
        {
            get { return dmgQty; }
            set { dmgQty = value; }
        }
        public string DmgMt
        {
            get { return dmgMt; }
            set { dmgMt = value; }
        }
        public string DmgM3
        {
            get { return dmgM3; }
            set { dmgM3 = value; }
        }
        public string DmgLocId
        {
            get { return dmgLocId; }
            set { dmgLocId = value; }
        }
        public HWC101001Result DmgSetLocRes
        {
            get { return dmgSetLocRes; }
            set { dmgSetLocRes = value; }
        }
        public HWC101001Result ShutSetLocRes
        {
            get { return shutSetLocRes; }
            set { shutSetLocRes = value; }
        }

        public string Remark
        {
            get
            {
                return this.remark;
            }
            set
            {
                this.remark = value;
            }
        }
    }
}
