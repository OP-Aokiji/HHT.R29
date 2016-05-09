using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonResult
{
    public class ContractorListResult : IPopupResult
    {
        private string roleCd;
        private string empId;
        private string empNm;

        public string RoleCd
        {
            get { return roleCd; }
            set { roleCd = value; }
        }

        public string EmpId
        {
            get { return empId; }
            set { empId = value; }
        }

        public string EmpNm
        {
            get { return empNm; }
            set { empNm = value; }
        }
    }
}
