using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.Client.Proxy.VesselOperatorProxy
{
    public interface IVesselOperatorProxy
    {
        ResponseInfo getBerthInfoList(VesselScheduleParm parm);
        ResponseInfo getVORLiquidBulkComboList(VORLiquidBulkParm parm);
        ResponseInfo getVORLiquidBulkCgOprType(VORLiquidBulkParm parm);
        ResponseInfo getContainerProcessList(ContainerProcessParm parm);
        ResponseInfo getConfirmationSlipBreakBulkList(ConfirmationSlipParm parm);
        ResponseInfo getConfirmationSlipLiquidBulkList(ConfirmationSlipParm parm);
        ResponseInfo validateVORDryBreakBulkItem(VORDryBreakBulkParm parm);
        ResponseInfo getVORLiquidConfirmSlipInfo(VORLiquidBulkParm parm);
        ResponseInfo getVORLiquidBulk(VORLiquidBulkParm parm);
        ResponseInfo getVORLiquidDelay(VORLiquidBulkParm parm);
        ResponseInfo getSftDblBankingList(SftDblBankingParm parm);
        ResponseInfo getVORLiquidCargo(VORLiquidBulkParm parm);
        ResponseInfo getVORList(ListVORParm parm);
        ResponseInfo getVORDryBreakBulk(VORDryBreakBulkParm parm);
        ResponseInfo getOperationSetBBKList(OperationSetParm parm);
        ResponseInfo getOperationSetDBKList(OperationSetParm parm);
        ResponseInfo getVORDryBreakBulkCommonCd(VORDryBreakBulkParm parm);
        ResponseInfo getVesselScheduleListDetail(VesselScheduleParm parm);
        ResponseInfo getDelayPenaltyReportList(DelayPenaltyReportParm parm);
        ResponseInfo getServiceOrderList(ServiceOrderParm parm);
        ResponseInfo getDelayRecordList(VesselDelayRecordParm parm);
        ResponseInfo getDeployedResource(CheckListVSRParm parm);
        void updateVesselDetailItem(DataItemCollection dataCollection);
        void processVORDryBreakBulkCUD(DataItemCollection dataCollection);
        void processVORDryBreakBulkForStevAndTrimCUD(DataItemCollection dataCollection);
        void processShiftingBankingItem(DataItemCollection dataCollection);
        void processVORLiquidCargoCUD(DataItemCollection dataCollection);
        void processDelayRecordItems(DataItemCollection dataCollection);
        void processDelayPenaltyReportItem(DataItemCollection dataCollection);
        void processVORListItem(DataItemCollection dataCollection);
        void processgetSftDblBankingItem(DataItemCollection dataCollection);
        void processVORLiquidDelayCUD(DataItemCollection dataCollection);
        void updateVORLiquidHoseLines(DataItemCollection dataCollection);
        void processContainerProcessItems(DataItemCollection dataCollection);
        void processServiceOrderCUD(DataItemCollection dataCollection);
        ResponseInfo checkValidation4Atu(BerthPlanParm parm);
        ResponseInfo getCurrAtbAtu(SftDblBankingParm parm);
        ResponseInfo getPrevShiftInfo(SftDblBankingParm parm);
    }
}
