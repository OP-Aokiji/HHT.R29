using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonResult
{
    public class DelayCodeListResult : IPopupResult
    {
        private string code;
        private string description;
        private string acceptedDelay;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string AcceptedDelay
        {
            get { return acceptedDelay; }
            set { acceptedDelay = value; }
        }
    }
}
