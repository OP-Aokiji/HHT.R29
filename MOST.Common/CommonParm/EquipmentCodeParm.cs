using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonParm
{
    public class EquipmentCodeParm : IPopupParm
    {
        private string roleCd;
        private string capaCd;
        private string eqIncludedList;
        
        public string RoleCd
        {
            get { return roleCd; }
            set { roleCd = value; }
        }

        public string CapaCd
        {
            get { return capaCd; }
            set { capaCd = value; }
        }
        public string EqIncludedList
        {
            get { return eqIncludedList; }
            set { eqIncludedList = value; }
        }
    }
}
