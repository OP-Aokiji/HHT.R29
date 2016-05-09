using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonResult
{
    public class RequesterListResult : IPopupResult
    {
        private string code;
        private string name;
        private string type;
        private string telNo;
        private string faxNo;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string TelNo
        {
            get { return telNo; }
            set { telNo = value; }
        }

        public string FaxNo
        {
            get { return faxNo; }
            set { faxNo = value; }
        }
    }
}
