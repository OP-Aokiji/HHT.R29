using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.Client.Proxy.ApronCheckerProxy
{
    public interface IApronCheckerProxy
    {
        ResponseInfo getDischComboBl(ImportMfCtrlListParm parm);
        ResponseInfo getCargoExportList(CargoExportParm parm);
        ResponseInfo getCargoRhdlLoadingList(CargoLoadingParm parm);
        ResponseInfo getCGjobRootChecked(CargoJobParm parm);
        ResponseInfo getCargoJobList(CargoJobParm parm);
        ResponseInfo getOperationSetHatch(OperationSetParm parm);
        ResponseInfo getVSRList(CheckListVSRParm parm);
        ResponseInfo getListOfLoading(ExportMfCtrlListParm parm);
        ResponseInfo getListOfDischarging(ImportMfCtrlListParm parm);
        ResponseInfo getModeOfOpr(ImportMfCtrlListParm parm);
        ResponseInfo getModeOfOpr(ExportMfCtrlListParm parm);
        ResponseInfo getOperationSetHtDBKList(OperationSetParm parm);
        ResponseInfo getOperationSetHtBBKList(OperationSetParm parm);
        ResponseInfo getOperationSetShftDtList(OperationSetParm parm);
        ResponseInfo getOperationSetShftList(OperationSetParm parm);
        ResponseInfo getOperationSetBBKList(OperationSetParm parm);
        ResponseInfo getOperationSetDBKList(OperationSetParm parm);
        ResponseInfo getCargoLoadingList(CargoLoadingParm parm);
        ResponseInfo getCargoDischargingList(CargoDischargingParm parm);
        ResponseInfo getDeployedResource(CheckListVSRParm parm);
        ResponseInfo processCargoLoadingItem(DataItemCollection dataCollection);
        ResponseInfo processCargoDischargingItem(DataItemCollection dataCollection);
        void processVSRListItem(DataItemCollection dataCollection);
        void processCargoJobItem(DataItemCollection dataCollection);
        void processCargoJobLocCUD(DataItemCollection dataCollection);
        void processCargoRhdlLoadingItem(DataItemCollection dataCollection);
        ResponseInfo getCargoJobNumbPage(CargoJobParm parm);
        ResponseInfo getListOfLoadingNumbPage(ExportMfCtrlListParm parm);
        ResponseInfo getListOfLoadingSummary(ExportMfCtrlListParm parm);
        ResponseInfo getListOfDischargingNumbPage(ImportMfCtrlListParm parm);
        ResponseInfo getListOfDischargingSummary(ImportMfCtrlListParm parm);
    }
}
