using System;
using System.Collections.Generic;
using System.Text;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.Client.Proxy.PortSafetyProxy
{
    public interface IPortSafetyProxy
    {
        ResponseInfo getArrvDelvIsCheck(CargoArrvDelvParm parm);
        ResponseInfo getCargoArrvDelvComboList(CargoArrvDelvParm parm);
        ResponseInfo getLorryListItems(LorryListParm parm);
        ResponseInfo getListOfGateIn(ExportMfCtrlListParm parm);
        ResponseInfo getListOfGateOut(ImportMfCtrlListParm parm);
        void processCargoArrvDelvItem(DataItemCollection dataCollection);

        //added by William (2015/08/04 - HHT) Mantis issue 49799
        ResponseInfo GetCargoArrvDelvByLorryNo(CargoArrvDelvParm parm);
        ResponseInfo checkGateOut(CargoArrvDelvParm parm);
    }
}
