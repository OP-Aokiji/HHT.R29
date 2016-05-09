using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonParm
{
    public class DriverListParm : IPopupParm
    {
        private string empId;
        public string EmpId
        {
            get { return empId; }
            set { empId = value; }
        }
    }
}
