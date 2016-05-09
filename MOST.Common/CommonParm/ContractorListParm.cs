using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonParm
{
    public class ContractorListParm : IPopupParm
    {
        private string empId;
        private string roleCd;
        private string shftDt;
        private string shftId;
        
        public string EmpId
        {
            get { return empId; }
            set { empId = value; }
        }

        public string RoleCd
        {
            get { return roleCd; }
            set { roleCd = value; }
        }

        public string ShftDt
        {
            get { return shftDt; }
            set { shftDt = value; }
        }

        public string ShftId
        {
            get { return shftId; }
            set { shftId = value; }
        }
    }
}
