using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Framework.Client.ServiceProxy;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.Client.Proxy.ApronCheckerProxy
{
    public class ApronCheckerProxy : BaseServiceProxy, IApronCheckerProxy
    {
        public ResponseInfo getDischComboBl(ImportMfCtrlListParm parm)
        {
            return this.execute("ControllerBean", "getDischComboBl", parm);
        }

        public ResponseInfo getCargoExportList(CargoExportParm parm)
        {
            return this.execute("ControllerBean", "getCargoExportList", parm);
        }

        public ResponseInfo getCargoRhdlLoadingList(CargoLoadingParm parm)
        {
            return this.execute("ControllerBean", "getCargoRhdlLoadingList", parm);
        }

        public ResponseInfo getCGjobRootChecked(CargoJobParm parm)
        {
            return this.execute("ControllerBean", "getCGjobRootChecked", parm);
        }

        public ResponseInfo getCargoJobList(CargoJobParm parm)
        {
            return this.execute("ControllerBean", "getCargoJobList", parm);
        }

        public ResponseInfo getOperationSetHatch(OperationSetParm parm)
        {
            return this.execute("ControllerBean", "getOperationSetHatch", parm);
        }

        public ResponseInfo getVSRList(CheckListVSRParm parm)
        {
            return this.execute("ControllerBean", "getVSRList", parm);
        }

        public ResponseInfo getListOfLoading(ExportMfCtrlListParm parm)
        {
            return this.execute("ControllerBean", "getListOfLoadingForHHT", parm);
        }

        public ResponseInfo getListOfDischarging(ImportMfCtrlListParm parm)
        {
            return this.execute("ControllerBean", "getListOfDischarging", parm);
        }

        public ResponseInfo getModeOfOpr(ImportMfCtrlListParm parm)
        {
            return this.execute("ControllerBean", "getModeOfOpr", parm);
        }

        public ResponseInfo getModeOfOpr(ExportMfCtrlListParm parm)
        {
            return this.execute("ControllerBean", "getModeOfOpr", parm);
        }

        public ResponseInfo getOperationSetHtDBKList(OperationSetParm parm)
        {
            return this.execute("ControllerBean", "getOperationSetHtDBKList", parm);
        }

        public ResponseInfo getOperationSetHtBBKList(OperationSetParm parm)
        {
            return this.execute("ControllerBean", "getOperationSetHtBBKList", parm);
        }

        public ResponseInfo getOperationSetShftDtList(OperationSetParm parm)
        {
            return this.execute("ControllerBean", "getOperationSetShftDtList", parm);
        }

        public ResponseInfo getOperationSetShftList(OperationSetParm parm)
        {
            return this.execute("ControllerBean", "getOperationSetShftList", parm);
        }

        public ResponseInfo getOperationSetBBKList(OperationSetParm parm)
        {
            return this.execute("ControllerBean", "getOperationSetBBKList", parm);
        }

        public ResponseInfo getOperationSetDBKList(OperationSetParm parm)
        {
            return this.execute("ControllerBean", "getOperationSetDBKList", parm);
        }

        public ResponseInfo getCargoLoadingList(CargoLoadingParm parm)
        {
            return this.execute("ControllerBean", "getCargoLoadingList", parm);
        }

        public ResponseInfo getCargoDischargingList(CargoDischargingParm parm)
        {
            return this.execute("ControllerBean", "getCargoDischargingList", parm);
        }

        public ResponseInfo getDeployedResource(CheckListVSRParm parm)
        {
            return this.execute("ControllerBean", "getDeployedResource", parm);
        }

        public ResponseInfo processCargoLoadingItem(DataItemCollection dataCollection)
        {
            return this.execute("ControllerBean", "processCargoLoadingItem", dataCollection);
        }

        public ResponseInfo processCargoDischargingItem(DataItemCollection dataCollection)
        {
            return this.execute("ControllerBean", "processCargoDischargingItem", dataCollection);
        }

        public void processVSRListItem(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "processVSRListItem", dataCollection);
        }

        public void processCargoJobItem(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "processCargoJobItem", dataCollection);
        }

        public void processCargoJobLocCUD(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "processCargoJobLocCUD", dataCollection);
        }

        public void processCargoRhdlLoadingItem(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "processCargoRhdlLoadingItem", dataCollection);
        }

        public ResponseInfo getCargoJobNumbPage(CargoJobParm parm)
        {
            return this.execute("ControllerBean", "getCargoJobNumbPage", parm);
        }

        public ResponseInfo getListOfLoadingNumbPage(ExportMfCtrlListParm parm)
        {
            return this.execute("ControllerBean", "getListOfLoadingNumbPage", parm);
        }

        public ResponseInfo getListOfLoadingSummary(ExportMfCtrlListParm parm)
        {
            return this.execute("ControllerBean", "getListOfLoadingSummary", parm);
        }

        public ResponseInfo getListOfDischargingNumbPage(ImportMfCtrlListParm parm)
        {
            return this.execute("ControllerBean", "getListOfDischargingNumbPage", parm);
        }

        public ResponseInfo getListOfDischargingSummary(ImportMfCtrlListParm parm)
        {
            return this.execute("ControllerBean", "getListOfDischargingSummary", parm);
        }
    }
}