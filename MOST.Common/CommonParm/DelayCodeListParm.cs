using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonParm
{
    public class DelayCodeListParm : IPopupParm
    {
        private string delayCode;
        public string DelayCode
        {
            get { return delayCode; }
            set { delayCode = value; }
        }
    }
}
