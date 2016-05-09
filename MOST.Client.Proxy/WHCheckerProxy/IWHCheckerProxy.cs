using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.Client.Proxy.WHCheckerProxy
{
    public interface IWHCheckerProxy
    {
        ResponseInfo getCargoExportList(CargoExportParm parm);
        ResponseInfo getCargoMovements(CargoMovementParm parm);
        ResponseInfo getCargoRhdlHandlingOutList(CargoHandlingOutParm parm);
        ResponseInfo getWHRecnList(WHReconciliationParm parm);
        ResponseInfo getWhViewList(WhConfigurationParm parm);
        ResponseInfo getHHTWhViewList(WhConfigurationParm parm);
        ResponseInfo getCargoLocationForHHT(CargoMasterParm parm);
        ResponseInfo getCargoHIList(CargoHandlingInParm parm);
        ResponseInfo getCargoHOList(CargoHandlingOutParm parm);
        ResponseInfo getCargoJobWhLocCombo(CargoJobParm parm);
        ResponseInfo getWHComboList(CargoMasterParm parm);
        ResponseInfo getWhConfigurationList(WhConfigurationParm parm);
        ResponseInfo getCargoHandlingInList(CargoHandlingInParm parm);
        ResponseInfo getCargoHandlingOutList(CargoHandlingOutParm parm);
        ResponseInfo processCargoHandlingInItem(DataItemCollection dataCollection);
        ResponseInfo processCargoHandlingOutItem(DataItemCollection dataCollection);
        void processWHReconciliationItems(DataItemCollection dataCollection);
        void processCargoMovementItem(DataItemCollection dataCollection);
    }
}
