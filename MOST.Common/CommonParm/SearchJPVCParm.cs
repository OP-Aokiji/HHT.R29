using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonParm
{
    public class SearchJPVCParm : IPopupParm
    {
        private string jpvc;
        private string isWHChecker;
        
        public string Jpvc
        {
            get { return jpvc; }
            set { jpvc = value; }
        }

        public string IsWHChecker
        {
            get { return isWHChecker; }
            set { isWHChecker = value; }
        }
    }
}
