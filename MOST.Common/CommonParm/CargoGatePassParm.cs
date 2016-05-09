using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonParm
{
    public class CargoGatePassParm : IPopupParm
    {
        private string vslCallId;
        private string cgNo;
        private string gpNo;
        private List<string> arrInitGPNos;

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

        public string CgNo
        {
            get { return cgNo; }
            set { cgNo = value; }
        }

        public string GpNo
        {
            get { return gpNo; }
            set { gpNo = value; }
        }

        /// <summary>
        /// </summary>
        public System.Collections.Generic.List<string> ArrInitGPNos
        {
            get
            {
                return this.arrInitGPNos;
            }
            set
            {
                this.arrInitGPNos = value;
            }
        }
    }
}
