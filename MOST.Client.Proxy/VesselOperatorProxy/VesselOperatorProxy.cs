using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Framework.Client.ServiceProxy;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.Client.Proxy.VesselOperatorProxy
{
    public class VesselOperatorProxy : BaseServiceProxy, IVesselOperatorProxy
    {
        public ResponseInfo getBerthInfoList(VesselScheduleParm parm)
        {
            return this.execute("PlanningBean", "getBerthInfoList", parm);
        }

        public ResponseInfo getVORLiquidBulkComboList(VORLiquidBulkParm parm)
        {
            return this.execute("ControllerBean", "getVORLiquidBulkComboList", parm);
        }

        public ResponseInfo getVORLiquidBulkCgOprType(VORLiquidBulkParm parm)
        {
            return this.execute("ControllerBean", "getVORLiquidBulkCgOprType", parm);
        }

        public ResponseInfo getContainerProcessList(ContainerProcessParm parm)
        {
            return this.execute("ControllerBean", "getContainerProcessList", parm);
        }

        public ResponseInfo getConfirmationSlipBreakBulkList(ConfirmationSlipParm parm)
        {
            return this.execute("PlanningBean", "getConfirmationSlipBreakBulkList", parm);
        }

        public ResponseInfo getConfirmationSlipLiquidBulkList(ConfirmationSlipParm parm)
        {
            return this.execute("PlanningBean", "getConfirmationSlipLiquidBulkList", parm);
        }

        public ResponseInfo validateVORDryBreakBulkItem(VORDryBreakBulkParm parm)
        {
            return this.execute("ControllerBean", "validateVORDryBreakBulkItem", parm);
        }

        public ResponseInfo getVORLiquidConfirmSlipInfo(VORLiquidBulkParm parm)
        {
            return this.execute("ControllerBean", "getVORLiquidConfirmSlipInfo", parm);
        }

        public ResponseInfo getVORLiquidBulk(VORLiquidBulkParm parm)
        {
            return this.execute("ControllerBean", "getVORLiquidBulk", parm);
        }

        public ResponseInfo getVORLiquidDelay(VORLiquidBulkParm parm)
        {
            return this.execute("ControllerBean", "getVORLiquidDelay", parm);
        }

        public ResponseInfo getSftDblBankingList(SftDblBankingParm parm)
        {
            return this.execute("ControllerBean", "getSftDblBankingList", parm);
        }

        public ResponseInfo getVORLiquidCargo(VORLiquidBulkParm parm)
        {
            return this.execute("ControllerBean", "getVORLiquidCargo", parm);
        }

        public ResponseInfo getVORList(ListVORParm parm)
        {
            return this.execute("ControllerBean", "getVORList", parm);
        }
        public ResponseInfo getCurrAtbAtu(SftDblBankingParm parm)
        {
            return this.execute("ControllerBean", "getCurrAtbAtu", parm);
        }
        public ResponseInfo getPrevShiftInfo(SftDblBankingParm parm)
        {
            return this.execute("ControllerBean", "getPrevShiftInfo", parm);
        }
        public ResponseInfo getVORDryBreakBulk(VORDryBreakBulkParm parm)
        {
            return this.execute("ControllerBean", "getVORDryBreakBulk", parm);
        }

        public ResponseInfo getOperationSetBBKList(OperationSetParm parm)
        {
            return this.execute("ControllerBean", "getOperationSetBBKList", parm);
        }

        public ResponseInfo getOperationSetDBKList(OperationSetParm parm)
        {
            return this.execute("ControllerBean", "getOperationSetDBKList", parm);
        }

        public ResponseInfo getVORDryBreakBulkCommonCd(VORDryBreakBulkParm parm)
        {
            return this.execute("ControllerBean", "getVORDryBreakBulkCommonCd", parm);
        }

        public ResponseInfo getVesselScheduleListDetail(VesselScheduleParm parm)
        {
            return this.execute("PlanningBean", "getVesselScheduleListDetail", parm);
        }

        public ResponseInfo getDelayPenaltyReportList(DelayPenaltyReportParm parm)
        {
            return this.execute("ControllerBean", "getDelayPenaltyReportList", parm);
        }

        public ResponseInfo getServiceOrderList(ServiceOrderParm parm)
        {
            return this.execute("AdministrationBean", "getServiceOrderItems", parm);
        }

        public ResponseInfo getDelayRecordList(VesselDelayRecordParm parm)
        {
            return this.execute("ControllerBean", "getDelayRecordList", parm);
        }

        public void updateVesselDetailItem(DataItemCollection dataCollection)
        {
            this.execute("PlanningBean", "updateVesselDetailItem", dataCollection);
        }

        public ResponseInfo getDeployedResource(CheckListVSRParm parm)
        {
            return this.execute("ControllerBean", "getDeployedResource", parm);
        }

        public void processVORDryBreakBulkCUD(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "processVORDryBreakBulkCUD", dataCollection);
        }

        public void processVORDryBreakBulkForStevAndTrimCUD(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "processVORDryBreakBulkForStevAndTrimCUD", dataCollection);
        }

        public void processShiftingBankingItem(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "processShiftingBankingItem", dataCollection);
        }

        public void processVORLiquidCargoCUD(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "processVORLiquidCargoCUD", dataCollection);
        }

        public void processDelayRecordItems(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "processDelayRecordItems", dataCollection);
        }

        public void processDelayPenaltyReportItem(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "processDelayPenaltyReportItem", dataCollection);
        }

        public void processVORListItem(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "processVORListItem", dataCollection);
        }

        public void processgetSftDblBankingItem(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "processgetSftDblBankingItem", dataCollection);
        }

        public void processVORLiquidDelayCUD(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "processVORLiquidDelayCUD", dataCollection);
        }

        public void updateVORLiquidHoseLines(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "updateVORLiquidHoseLines", dataCollection);
        }

        public void processContainerProcessItems(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "processContainerProcessItems", dataCollection);
        }

        public void processServiceOrderCUD(DataItemCollection dataCollection)
        {
            this.execute("AdministrationBean", "processServiceOrderItems", dataCollection);
        }

        public ResponseInfo checkValidation4Atu(BerthPlanParm parm)
        {
            return this.execute("PlanningBean", "checkValidation4Atu", parm);
        }
    }
}