using System;
using System.Collections.Generic;
using System.Text;
using Framework.Client.ServiceProxy;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.Client.Proxy.PortSafetyProxy
{
    public class PortSafetyProxy : BaseServiceProxy, IPortSafetyProxy
    {
        public ResponseInfo getArrvDelvIsCheck(CargoArrvDelvParm parm)
        {
            return this.execute("ControllerBean", "getArrvDelvIsCheck", parm);
        }

        public ResponseInfo getCargoArrvDelvComboList(CargoArrvDelvParm parm)
        {
            return this.execute("ControllerBean", "getCargoArrvDelvComboList", parm);
        }

        public ResponseInfo getLorryListItems(LorryListParm parm)
        {
            return this.execute("DocumentBean", "getLorryListItems", parm);
        }

        public ResponseInfo getListOfGateIn(ExportMfCtrlListParm parm)
        {
            return this.execute("ControllerBean", "getListOfGateIn", parm);
        }

        public ResponseInfo getListOfGateOut(ImportMfCtrlListParm parm)
        {
            return this.execute("ControllerBean", "getListOfGateOut", parm);
        }

        public void processCargoArrvDelvItem(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "processCargoArrvDelvItem", dataCollection);
        }

        //added by William (2015/08/04 - HHT) Mantis issue 49799
        public ResponseInfo GetCargoArrvDelvByLorryNo(CargoArrvDelvParm parm)
        {
            return this.execute("ControllerBean", "GetCargoArrvDelvByLorryNo", parm);
        }

        public ResponseInfo checkGateOut(CargoArrvDelvParm parm)
        {
            return this.execute("ControllerBean", "checkGateOut", parm);
        }

    }
}
