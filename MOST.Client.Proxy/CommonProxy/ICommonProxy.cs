using System;
using System.Collections.Generic;
using System.Text;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.Client.Proxy.CommonProxy
{
    public interface ICommonProxy
    {
        ResponseInfo getValidationCode(CommonCodeParm parm);
        ResponseInfo getUnclosedOperationList(UnclosedOperationParm parm);
        ResponseInfo getUnclosedOperationListNumbPage(UnclosedOperationParm parm);
        ResponseInfo getCargoRhdlOperation(CargoRehandlingParm parm);
        ResponseInfo getCargoRhdlOperationNumbPage(CargoRehandlingParm parm);
        ResponseInfo getMegaCompList(MegaParm parm);
        ResponseInfo getCargoGatePassList(CargoGatePassParm parm);  // GP list
        ResponseInfo getGatePassImport(GatePassImportParm parm);    // GP detail
        ResponseInfo getCargoExportList(CargoExportParm parm);      // for searching BL
        ResponseInfo getCargoExportListHHT(CargoExportParm parm);      // for searching BL
        ResponseInfo getCargoListNumbPage(CargoExportParm parm); //for paging
        ResponseInfo getCargoImport(CargoImportParm parm);          // for checking if BL is valid and unique
        ResponseInfo getCargoImportList(CargoImportParm parm);
        ResponseInfo getCargoImportListNumbPage(CargoImportParm parm);
        ResponseInfo getLocationCodeList(LocationCodeParm parm);
        ResponseInfo getEquipmentCodeList(EquipmentCodeParm parm);
        ResponseInfo getSearchJPVCList(SearchJPVCParm parm);    // for searching JPVC
        ResponseInfo getSearchVslCallId(SearchJPVCParm parm);   // for checking if JPVC is valid and unique
        ResponseInfo getDealyCodeItems(DelayCodeParm parm);
        ResponseInfo getGoodsReceiptNo(GoodsReceiptParm parm);
        ResponseInfo getDeliveryOrderList(DeliveryOrderParm parm);
        ResponseInfo getInternalStaffMngList(InternalStaffMngParm parm);
        ResponseInfo getPartnerCodeList(PartnerCodeParm parm);
        ResponseInfo getPartnerCodeTypeList(PartnerCodeParm parm);
        ResponseInfo getAssignmentLorrysItems(AssignmentLorrysParm parm);
        ResponseInfo getShippingNoteComboList(ShippingNoteParm parm);
        ResponseInfo getDeliveryOrderNo(DeliveryOrderParm parm);
        ResponseInfo getCurrentServerTime(CurrentServerTimeParm parm);
        ResponseInfo getDeliveryOrderBLComboList(DeliveryOrderParm parm);
        ResponseInfo getCommonCodeList(CommonCodeParm parm);
        ResponseInfo getShift(ShiftParm parm);
        ResponseInfo getCargoGatePassNo(CargoGatePassParm parm);
        ResponseInfo getCargoGateLorryNo(CargoGatePassParm parm);
        ResponseInfo getVSRList(CheckListVSRParm parm);
        ResponseInfo getCargoMasterComboList(CargoMasterParm parm);
        void updateCargoGatePassRemark(DataItemCollection dataCollection);
        ResponseInfo getStatusList(CargoExportParm parm); //get status list
        ResponseInfo getSummary(CargoExportParm parm); // get the summary of doc , act MT M3 Qty
        ResponseInfo getForkliftDeployListForVSRHHT(VOperationDeployParm parm);
        ResponseInfo getHealthClearanceStatus_CR_OGA(SearchJPVCParm parm);


        //added by William (2015/07/21 - HHT) Mantis issue 49799
        ResponseInfo GetGoodsReceiptList(GoodsReceiptParm parm);
        ResponseInfo GetLorryList(LorryListParm parm);
        ResponseInfo GetDeliveryOrderList(DeliveryOrderParm parm);

        //added by William (2015/08/05 - HHT) Mantis issue 49799
        ResponseInfo CheckValidLorry(LorryListParm parm);
    }
}
