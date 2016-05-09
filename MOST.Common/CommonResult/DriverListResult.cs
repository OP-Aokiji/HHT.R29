using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonResult
{
    public class DriverListResult : IPopupResult
    {
        private string empId;
        private string empName;

        public string EmpId
        {
            get { return empId; }
            set { empId = value; }
        }

        public string EmpName
        {
            get { return empName; }
            set { empName = value; }
        }
    }
}
