using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonParm
{
    public class MegaStvTrmParm : IPopupParm
    {
        private string option;
        private string searchItem;
        private string vslCallId;
        private string shftId;
        private string workYmd;
        
        public string Option
        {
            get { return option; }
            set { option = value; }
        }
        public string SearchItem
        {
            get { return searchItem; }
            set { searchItem = value; }
        }

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

        public string ShftId
        {
            get { return shftId; }
            set { shftId = value; }
        }

        public string WorkYmd
        {
            get { return workYmd; }
            set { workYmd = value; }
        }
    }
}
