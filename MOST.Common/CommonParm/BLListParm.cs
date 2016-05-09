using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonParm
{
    public class BLListParm : IPopupParm
    {
        private String jpvc;
        private String blNo;
        private bool excludeFnlDischarging;        // Dot not display items whose status are Final Discharging.
        private bool excludeFnlHandlingOut;        // Dot not display items whose status are Final Handling Out.

        public String Jpvc
        {
            get { return jpvc; }
            set { jpvc = value; }
        }

        public String BlNo
        {
            get { return blNo; }
            set { blNo = value; }
        }

        public bool ExcludeFnlDischarging
        {
            get { return excludeFnlDischarging; }
            set { excludeFnlDischarging = value; }
        }

        public bool ExcludeFnlHandlingOut
        {
            get { return excludeFnlHandlingOut; }
            set { excludeFnlHandlingOut = value; }
        }
    }
}
