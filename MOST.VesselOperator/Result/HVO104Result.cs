using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.VesselOperator.Result
{
    public class HVO104Result : IPopupResult
    {
        private string equipments;

        public string Equipments
        {
            get { return equipments; }
            set { equipments = value; }
        }
    }
}
