using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonParm
{
    public class GRBLListParm : IPopupParm
    {
        private string cgNo;
        private string vslCallId;

        public string CgNo
        {
            get { return cgNo; }
            set { cgNo = value; }
        }
        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }
    }
}
