using System;
using System.Collections.Generic;
using System.Text;
using Framework.Client.ServiceProxy;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.Client.Proxy.CommonProxy
{

    public class CommonProxy : BaseServiceProxy, ICommonProxy
    {
        public ResponseInfo getValidationCode(CommonCodeParm parm)
        {
            return this.execute("CommonBean", "getValidationCode", parm);
        }

        public ResponseInfo getUnclosedOperationList(UnclosedOperationParm parm)
        {
            return this.execute("ControllerBean", "getUnclosedOperationList", parm);
        }

        public ResponseInfo getUnclosedOperationListNumbPage(UnclosedOperationParm parm)
        {
            return this.execute("ControllerBean", "getUnclosedOperationListNumbPage", parm);
        }

        public ResponseInfo getCargoRhdlOperation(CargoRehandlingParm parm)
        {
            return this.execute("ControllerBean", "getCargoRhdlOperation", parm);
        }

        public ResponseInfo getCargoRhdlOperationNumbPage(CargoRehandlingParm parm)
        {
            return this.execute("ControllerBean", "getCargoRhdlOperationNumbPage", parm);
        }

        public ResponseInfo getMegaCompList(MegaParm parm)
        {
            return this.execute("PlanningBean", "getMegaCompList", parm);
        }

        public ResponseInfo getCargoGatePassList(CargoGatePassParm parm)
        {
            return this.execute("ControllerBean", "getCargoGatePassList", parm);
        }

        public ResponseInfo getGatePassImport(GatePassImportParm parm)
        {
            return this.execute("ControllerBean", "getGatePassImport", parm);
        }

        public ResponseInfo getCargoExportList(CargoExportParm parm)
        {
            return this.execute("ControllerBean", "getCargoExportList", parm);
        }

        public ResponseInfo getCargoExportListHHT(CargoExportParm parm)
        {
            return this.execute("ControllerBean", "getCargoExportListHHT", parm);
        }

        public ResponseInfo getCargoListNumbPage(CargoExportParm parm)
        {
            return this.execute("ControllerBean", "getCargoListNumbPage", parm);
        }

        public ResponseInfo getCargoImport(CargoImportParm parm)
        {
            return this.execute("ControllerBean", "getCargoImport", parm);
        }

        public ResponseInfo getCargoImportList(CargoImportParm parm)
        {
            return this.execute("ControllerBean", "getCargoImportList", parm);
        }

        public ResponseInfo getCargoImportListNumbPage(CargoImportParm parm)
        {
            return this.execute("ControllerBean", "getCargoImportNumbPage", parm);
        }

        public ResponseInfo getLocationCodeList(LocationCodeParm parm)
        {
            return this.execute("CommonBean", "getLocationCodeList", parm);
        }

        public ResponseInfo getEquipmentCodeList(EquipmentCodeParm parm)
        {
            return this.execute("CommonBean", "getEquipmentCodeList", parm); // eqDivCdType = "MC"
        }

        public ResponseInfo getSearchJPVCList(SearchJPVCParm parm)
        {
            return this.execute("CommonBean", "getSearchJPVCList", parm);
        }

        public ResponseInfo getSearchVslCallId(SearchJPVCParm parm)
        {
            return this.execute("CommonBean", "getSearchVslCallId", parm);
        }

        public ResponseInfo getDealyCodeItems(DelayCodeParm parm)
        {
            return this.execute("ConfigurationBean", "getDealyCodeItems", parm);
        }

        public ResponseInfo getGoodsReceiptNo(GoodsReceiptParm parm)
        {
            return this.execute("DocumentBean", "getGoodsReceiptNo", parm);
        }

        public ResponseInfo getDeliveryOrderList(DeliveryOrderParm parm)
        {
            return this.execute("DocumentBean", "getDeliveryOrderList", parm);
        }

        public ResponseInfo getInternalStaffMngList(InternalStaffMngParm parm)
        {
            return this.execute("PlanningBean", "getInternalStaffMngList", parm);
        }

        public ResponseInfo getPartnerCodeList(PartnerCodeParm parm)
        {
            return this.execute("CommonBean", "getPartnerCodeList", parm);
        }

        public ResponseInfo getPartnerCodeTypeList(PartnerCodeParm parm)
        {
            return this.execute("CommonBean", "getPartnerCodeTypeList", parm);
        }

        public ResponseInfo getAssignmentLorrysItems(AssignmentLorrysParm parm)
        {
            return this.execute("DocumentBean", "getAssignmentLorrysItems", parm);
        }

        public ResponseInfo getShippingNoteComboList(ShippingNoteParm parm)
        {
            return this.execute("DocumentBean", "getShippingNoteComboList", parm);
        }

        public ResponseInfo getDeliveryOrderNo(DeliveryOrderParm parm)
        {
            return this.execute("DocumentBean", "getDeliveryOrderNo", parm);
        }

        public ResponseInfo getCurrentServerTime(CurrentServerTimeParm parm)
        {
            return this.execute("CommonBean", "getServerTime", parm);
        }

        public ResponseInfo getDeliveryOrderBLComboList(DeliveryOrderParm parm)
        {
            return this.execute("DocumentBean", "getDeliveryOrderBLComboList", parm);
        }

        public ResponseInfo getCommonCodeList(CommonCodeParm parm)
        {
            return this.execute("CommonBean", "getCommonCodeList", parm);
        }

        public ResponseInfo getShift(ShiftParm parm)
        {
            return this.execute("CommonBean", "getShift", parm);
        }

        public ResponseInfo getCargoGatePassNo(CargoGatePassParm parm)
        {
            return this.execute("ControllerBean", "getCargoGatePassNo", parm);
        }

        public ResponseInfo getCargoGateLorryNo(CargoGatePassParm parm)
        {
            return this.execute("ControllerBean", "getCargoGateLorryNo", parm);
        }

        public ResponseInfo getVSRList(CheckListVSRParm parm)
        {
            return this.execute("ControllerBean", "getVSRList", parm);
        }

        public ResponseInfo getCargoMasterComboList(CargoMasterParm parm)
        {
            return this.execute("ControllerBean", "getCargoMasterComboList", parm);
        }

        public void updateCargoGatePassRemark(DataItemCollection dataCollection)
        {
            this.execute("ControllerBean", "updateCargoGatePassRemark", dataCollection);
        }

        public ResponseInfo getStatusList(CargoExportParm parm)
        {
            return this.execute("ControllerBean", "getStatusList", parm);
        }

        public ResponseInfo getSummary(CargoExportParm parm)
        {
            return this.execute("ControllerBean", "getSummary", parm);
        }

        public ResponseInfo getForkliftDeployListForVSRHHT(VOperationDeployParm parm)
        {
            return this.execute("PlanningBean", "getForkliftDeployListForVSRHHT", parm);
        }

        public ResponseInfo getHealthClearanceStatus_CR_OGA(SearchJPVCParm parm)
        {
            return this.execute("CommonBean", "getHealthClearanceStatus_CR_OGA", parm);
        }

        //added by William (2015/07/21 - HHT) Mantis issue 49799
        public ResponseInfo GetLorryList(LorryListParm parm)
        {
            return this.execute("CommonBean", "GetLorryList", parm);
        }

        //added by William (2015/07/21 - HHT) Mantis issue 49799
        public ResponseInfo GetGoodsReceiptList(GoodsReceiptParm parm)
        {
            return this.execute("DocumentBean", "GetGoodsReceiptList", parm);
        }
        //added by William (2015/07/21 - HHT) Mantis issue 49799
        public ResponseInfo GetDeliveryOrderList(DeliveryOrderParm parm)
        {
            return this.execute("DocumentBean", "GetDeliveryOrderList", parm);
        }

        //added by William (2015/08/05 - HHT) Mantis issue 49799
        public ResponseInfo CheckValidLorry(LorryListParm parm)
        {
            return this.execute("CommonBean", "CheckValidLorry", parm);
        }
    }
}