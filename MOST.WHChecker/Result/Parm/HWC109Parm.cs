using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;

namespace MOST.WHChecker.Parm
{
    public class HWC109Parm : IPopupParm
    {
        private string vslCallId = null;
        private string cgNo = null;
        private string shipgNoteNo = null;
        private string caTyCd = null;

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
        public string ShipgNoteNo
        {
            get { return shipgNoteNo; }
            set { shipgNoteNo = value; }
        }
        public string CaTyCd
        {
            get { return caTyCd; }
            set { caTyCd = value; }
        }
    }
}
