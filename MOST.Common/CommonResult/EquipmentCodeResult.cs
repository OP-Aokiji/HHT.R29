using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonResult
{
    public class EquipmentCodeResult : IPopupResult
    {
        private string equCd;
        private string equNm;
        private string capaCd;
        private string capaDescr;

        public string EquCd
        {
            get { return equCd; }
            set { equCd = value; }
        }

        public string EquNm
        {
            get { return equNm; }
            set { equNm = value; }
        }

        public string CapaCd
        {
            get { return capaCd; }
            set { capaCd = value; }
        }

        public string CapaDescr
        {
            get { return capaDescr; }
            set { capaDescr = value; }
        }
    }
}
