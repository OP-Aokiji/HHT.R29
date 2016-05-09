using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Framework.Client.ServiceProxy;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.Client.Proxy.WHCheckerProxy
{
    public class WHCheckerProxy : BaseServiceProxy, IWHCheckerProxy
    {
        public ResponseInfo getCargoExportList(CargoExportParm parm)
        {
            return this.execute("ControllerBean", "getCargoExportList", parm);
        }

        public ResponseInfo getCargoMovements(CargoMovementParm parm)
        {
            return this.execute("ControllerBean", "getCargoMovements", parm);
        }

        public ResponseInfo getCargoRhdlHandlingOutList(CargoHandlingOutParm parm)
        {
            return this.execute("ControllerBean", "getCargoRhdlHandlingOutList", parm);
        }

        public ResponseInfo getWHRecnList(WHReconciliationParm parm)
        {
            return this.execute("ControllerBean", "getWHRecnList", parm);
        }

        public ResponseInfo getWhViewList(WhConfigurationParm parm)
        {
            return this.execute("ConfigurationBean", "getWhViewList", parm);
        }

        public ResponseInfo getHHTWhViewList(WhConfigurationParm parm)
        {
            return this.execute("ConfigurationBean", "getHHTWhViewList", parm);
        }

        public ResponseInfo getCargoLocationForHHT(CargoMasterParm parm)
        {
            return this.execute("ControllerBean", "getCargoLocationForHHT", parm);
        }

        public ResponseInfo getCargoHIList(CargoHandlingInParm parm)
        {
            return this.execute("ControllerBean", "getCargoHIList", parm);
        }

        public ResponseInfo getCargoHOList(CargoHandlingOutParm parm)
        {
            return this.execute("ControllerBean", "getCargoHOList", parm);
        }

        public ResponseInfo getCargoJobWhLocCombo(CargoJobParm parm)
        {
            return this.execute("ControllerBean", "getCargoJobWhLocCombo", parm);
        }

        public ResponseInfo getWHComboList(CargoMasterParm parm)
        {
            return this.execute("ControllerBean", "getWHComboList", parm);
        }

        public ResponseInfo getWhConfigurationList(WhConfigurationParm parm)
        {
            return this.execute("ConfigurationBean", "getWhConfigurationList", parm);
        }

        public ResponseInfo getCargoHandlingInList(CargoHandlingInParm parm)
        {
            return this.execute("ControllerBean", "getCargoHandlingInList", parm);
        }

        public ResponseInfo getCargoHandlingOutList(CargoHandlingOutParm parm)
        {
            return this.execute("ControllerBean", "getCargoHandlingOutList", parm);
        }

        public ResponseInfo processCargoHandlingInItem(DataItemCollection dataCollection)
        {
            return this.execute("ControllerBean", "processCargoHandlingInItem", dataCollection);
        }

        public ResponseInfo processCargoHandlingOutItem(DataItemCollection dataCollection)
        {
            return this.execute("ControllerBean", "processCargoHandlingOutItem", dataCollection);
        }

        public void processWHReconciliationItems(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "processWHReconciliationItems", dataCollection);
        }

        public void processCargoMovementItem(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "processCargoMovementItem", dataCollection);
        }

    }
}