using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using MOST.Common.CommonResult;
using MOST.Client.Proxy.CommonProxy;
using MOST.Client.Proxy.VesselOperatorProxy;
using Framework.Common.Helper;
using Framework.Controls;
using Framework.Controls.UserControls;
using Framework.Service.Provider.WebService.Provider;
using Framework.Common.UserInformation;
using Framework.Controls.Container;
using System.Threading;


namespace MOST.Common.Utility
{
    public partial class CommonUtility
    {
        public const string SHIFT_1_FRM     = "07:00";
        public const string SHIFT_1_TO = "14:59";
        //public const string SHIFT_1_TO      = "15:00";
        public const string SHIFT_2_FRM     = "15:00";
        public const string SHIFT_2_TO = "22:59";
        //public const string SHIFT_2_TO      = "23:00";
        public const string SHIFT_3_FRM     = "23:00";
        public const string SHIFT_3_TO = "06:59";
        //public const string SHIFT_3_TO      = "07:00";
        public const string DDMMYYYY        = "dd/MM/yyyy";
        public const string DDMMYYYYHHMM    = "dd/MM/yyyy HH:mm";
        public const string DDMMYYYYHHMMSS  = "dd/MM/yyyy HH:mm:ss";

        //added by William (2015/07/21 - HHT) Mantis issue 49799
        public const string PORT_SAFETY = "PORT_SAFETY";

        public const string M_HWC101 = "HWC101";

        /// <summary>
        /// Get declared hatches
        /// </summary>
        /// <param name="argJPVC"></param>
        /// <param name="cboHatch"></param>
        public static void GetDeclaredHatches(string argJPVC, ComboBox cboHatch)
        {
            IVesselOperatorProxy proxy = new VesselOperatorProxy();
            ResponseInfo info = null;
            VORDryBreakBulkParm parm = null;

            parm = new VORDryBreakBulkParm();
            parm.searchType = "info";
            parm.rsDivCd = "EQ";
            parm.vslCallId = argJPVC;
            parm.workYmd = UserInfo.getInstance().Workdate;
            parm.shift = UserInfo.getInstance().Shift;
            info = proxy.getVORDryBreakBulk(parm);
            CommonUtility.InitializeCombobox(cboHatch);
            if (info != null)
            {
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is VORDryBreakBulkItem)
                    {
                        VORDryBreakBulkItem item = (VORDryBreakBulkItem)info.list[i];
                        cboHatch.Items.Add(new ComboboxValueDescriptionPair(item.hatchNo, item.hatchNo));
                    }
                }
            }
        }

        /// <summary>
        /// Get declared hatches and equipments
        /// </summary>
        /// <param name="argJPVC"></param>
        /// <param name="cboHatch"></param>
        /// <param name="cboEQU"></param>
        public static void GetDeclaredEqu(string argJPVC, ComboBox cboEQU)
        {
            IVesselOperatorProxy proxy = new VesselOperatorProxy();
            ResponseInfo info = null;
            VORDryBreakBulkParm parm = null;

            parm = new VORDryBreakBulkParm();
            parm.searchType = "info";
            parm.rsDivCd = "EQ";
            parm.vslCallId = argJPVC;
            parm.workYmd = UserInfo.getInstance().Workdate;
            parm.shift = UserInfo.getInstance().Shift;
            info = proxy.getVORDryBreakBulk(parm);
            CommonUtility.InitializeCombobox(cboEQU);
            if (info != null)
            {
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is VORDryBreakBulkItem)
                    {
                        VORDryBreakBulkItem item = (VORDryBreakBulkItem)info.list[i];
                        cboEQU.Items.Add(new ComboboxValueDescriptionPair(item.eqFacNo, item.eqFacNm));
                    }
                }
            }
        }

		public static void GetEuipmentNoList(ComboBox cboEQU)
        {
            IVesselOperatorProxy proxy = new VesselOperatorProxy();
            ResponseInfo info = null;
            VesselDelayRecordParm parm = null;

            parm = new VesselDelayRecordParm();
            parm.searchType = "HHT_COMBOBOX";
            info = proxy.getDelayRecordList(parm);
            CommonUtility.InitializeCombobox(cboEQU);
            if (info != null)
            {
                for (int i = 0; i < info.list.Length; i++)
                {
                    if (info.list[i] is VesselDelayRecordItem)
                    {
                        VesselDelayRecordItem item = (VesselDelayRecordItem)info.list[i];
                        cboEQU.Items.Add(new ComboboxValueDescriptionPair(item.eqNo, item.eqNm));
                    }
                }
            }
        }
		
        /// <summary>
        /// Start Time, End Time display automatically within Workdate & shift.
        /// </summary>
        /// <param name="argFromCtrl">DateTimePicker with format: dd/MM/yyyy HH:mm</param>
        /// <param name="argToCtrl">DateTimePicker with format: dd/MM/yyyy HH:mm</param>
        public static void SetDTPWithinShift(TDateTimePicker argFromCtrl, TDateTimePicker argToCtrl)
        {
            if (argFromCtrl != null && argToCtrl != null)
            {
                string strStartTime = UserInfo.getInstance().Workdate + " ";
                string strEndTime = UserInfo.getInstance().Workdate + " ";
                if ("1".Equals(UserInfo.getInstance().ShiftIndex))
                {
                    strStartTime += CommonUtility.SHIFT_1_FRM;
                    strEndTime += CommonUtility.SHIFT_1_TO;
                }
                else if ("2".Equals(UserInfo.getInstance().ShiftIndex))
                {
                    strStartTime += CommonUtility.SHIFT_2_FRM;
                    strEndTime += CommonUtility.SHIFT_2_TO;
                }
                else if ("3".Equals(UserInfo.getInstance().ShiftIndex))
                {
                    strStartTime += CommonUtility.SHIFT_3_FRM;

                    DateTime workdate = CommonUtility.ParseYMD(UserInfo.getInstance().Workdate);
                    workdate = workdate.AddDays(1);
                    strEndTime = workdate.ToString(CommonUtility.DDMMYYYY);
                    strEndTime += " " + CommonUtility.SHIFT_3_TO;
                }
                CommonUtility.SetDTPValueDMYHM(argFromCtrl, strStartTime);
                CommonUtility.SetDTPValueDMYHM(argToCtrl, strEndTime);
            }
        }

        /// <summary>
        /// Check if start date is less or equal than end date.
        /// </summary>
        /// <param name="argJPVC"></param>
        /// <param name="argJPVCResult"></param>
        /// <returns></returns>
        public static bool CheckDateStartEnd(TDateTimePicker argStartDateCtrl, TDateTimePicker argEndDateCtrl)
        {
            if (!string.IsNullOrEmpty(argStartDateCtrl.Text) && !string.IsNullOrEmpty(argEndDateCtrl.Text))
            {
                DateTime dtFrom = argStartDateCtrl.Value;
                DateTime dtTo = argEndDateCtrl.Value;
                if (dtFrom.CompareTo(dtTo) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0041"));
                    argStartDateCtrl.Focus();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check if JPVC is valid and unique.
        /// </summary>
        /// <param name="argJPVC"></param>
        /// <param name="argJPVCResult"></param>
        /// <returns></returns>
        public static bool IsValidJPVC(string argJPVC, ref SearchJPVCResult argJPVCResult)
        {
            if (string.IsNullOrEmpty(argJPVC))
            {
                return false;
            }
            argJPVCResult = null;
            ICommonProxy proxy = new CommonProxy();
            SearchJPVCParm parm = new SearchJPVCParm();
            parm.vslCallId = argJPVC;
            ResponseInfo info = proxy.getSearchVslCallId(parm);
            if ((info != null) && (info.list != null) && (info.list.Length == 1) && (info.list[0] is SearchJPVCItem))
            {
                SearchJPVCItem item = (SearchJPVCItem)info.list[0];
                argJPVCResult = new SearchJPVCResult();
                argJPVCResult.Jpvc = item.vslCallId;
                argJPVCResult.VesselName = item.vslNm;
                argJPVCResult.WharfStart = item.wharfStart;
                argJPVCResult.WharfEnd = item.wharfEnd;
                argJPVCResult.BerthLocation = item.berthLoc;
                argJPVCResult.VslTp = item.vslTp;
                argJPVCResult.VslTpNm = item.vslTpNm;
                argJPVCResult.Loa = item.loa;
                argJPVCResult.Etb = item.etb;
                argJPVCResult.Etw = item.etw;
                argJPVCResult.Etc = item.etc;
                argJPVCResult.Etu = item.etu;
                argJPVCResult.Atb = item.atb;
                argJPVCResult.CurAtb = item.curAtb;
                argJPVCResult.Atw = item.atw;
                argJPVCResult.Atc = item.atc;
                argJPVCResult.Atu = item.atu;
                argJPVCResult.PurpCall = item.purpCall;
                argJPVCResult.PurpCallCd = item.purpCall;

                //OGA CR / WILLIAM
                argJPVCResult.OgaStatus = item.ogaStatus;
                argJPVCResult.OgaQuarantine = item.ogaQuarantine;
                argJPVCResult.OgaStatusDate = item.ogaStatusDate;


                // Vessel Shifting CR - 17 Mar 2015 / WILLIAM
                argJPVCResult.VslShiftingSeq = item.vslShiftingSeq;
                //argJPVCResult.CurrAtb = item.curAtb;
                //argJPVCResult.CurrAtu = item.currAtu;
                //argJPVCResult.WgtBbkDbk = item.wgtBbkDbk;
                //argJPVCResult.WgtLq = item.wgtLq;


                return true;
            }

            return false;
        }

        /// <summary>
        /// Check G/R Number
        /// </summary>
        /// <param name="argJPVC"></param>
        /// <param name="argJPVCResult"></param>
        /// <returns></returns>
        public static bool IsValidGR(string argGRNo, ref GRListResult argGRResult)
        {
            if (string.IsNullOrEmpty(argGRNo))
            {
                return false;
            }

            ICommonProxy proxy = new CommonProxy();
            Framework.Service.Provider.WebService.Provider.GoodsReceiptParm parm = new Framework.Service.Provider.WebService.Provider.GoodsReceiptParm();
            parm.gdsRecvNo = argGRNo;
            parm.searchType = "grNo";
            ResponseInfo info = proxy.getGoodsReceiptNo(parm);
            if ((info != null) && (info.list != null) && (info.list.Length == 1) && (info.list[0] is GoodsReceiptItem))
            {
                GoodsReceiptItem item = (GoodsReceiptItem)info.list[0];

                argGRResult = new GRListResult();
                argGRResult.GrNo = item.gdsRecvNo;
                argGRResult.VslCallId = item.vslCallId;
                argGRResult.ShipgNoteNo = item.shipgNoteNo;
                argGRResult.Lorry = item.lorryId;
                argGRResult.Qty = CommonUtility.ToString(item.grQty);
                argGRResult.Mt = CommonUtility.ToString(item.grWgt);
                argGRResult.M3 = CommonUtility.ToString(item.grMsrmt);
                argGRResult.SpCargoChk = item.spCargoChk;
                argGRResult.Tsptr = item.tsptr;
                argGRResult.TsptrCompNm = item.tsptCompNm;
                argGRResult.CgTpCd = item.cgTpCd;
                argGRResult.DelvTpCd = item.delvTpCd;
                argGRResult.DelvTpNm = item.delvTpNm;
                argGRResult.DgYn = item.dgYn;
                argGRResult.DgStatCd = item.dgStatCd;

                //Added by Chris 2015-09-24 for 49779
                argGRResult.CmdtCd = item.cmdtCd;

                return true;
            }

            return false;
        }

        //Added By Chris 2015-09-28
        public static bool IsValidGR2(string argGRNo, string lorryno, ref GRListResult argGRResult)
        {
            if (string.IsNullOrEmpty(argGRNo))
            {
                return false;
            }

            ICommonProxy proxy = new CommonProxy();
            Framework.Service.Provider.WebService.Provider.GoodsReceiptParm parm = new Framework.Service.Provider.WebService.Provider.GoodsReceiptParm();
            parm.gdsRecvNo = argGRNo;
            parm.hhtFlag = "Y";
            parm.lorryNo = lorryno;
            parm.searchType = "grNo";
            ResponseInfo info = proxy.getGoodsReceiptNo(parm);
            if ((info != null) && (info.list != null) && (info.list.Length == 1) && (info.list[0] is GoodsReceiptItem))
            {
                GoodsReceiptItem item = (GoodsReceiptItem)info.list[0];

                argGRResult = new GRListResult();
                argGRResult.GrNo = item.gdsRecvNo;
                argGRResult.VslCallId = item.vslCallId;
                argGRResult.ShipgNoteNo = item.shipgNoteNo;
                argGRResult.Lorry = item.lorryId;
                argGRResult.Qty = CommonUtility.ToString(item.grQty);
                argGRResult.Mt = CommonUtility.ToString(item.grWgt);
                argGRResult.M3 = CommonUtility.ToString(item.grMsrmt);
                argGRResult.SpCargoChk = item.spCargoChk;
                argGRResult.Tsptr = item.tsptr;
                argGRResult.TsptrCompNm = item.tsptCompNm;
                argGRResult.CgTpCd = item.cgTpCd;
                argGRResult.DelvTpCd = item.delvTpCd;
                argGRResult.DelvTpNm = item.delvTpNm;
                argGRResult.DgYn = item.dgYn;
                argGRResult.DgStatCd = item.dgStatCd;

                //Added by Chris 2015-09-24 for 49779
                argGRResult.CmdtCd = item.cmdtCd;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Check G/R number of cargo export.
        /// </summary>
        /// <param name="argJPVC"></param>
        /// <param name="argJPVCResult"></param>
        /// <returns></returns>
        public static bool IsValidCargoExportGR(string argJPVC, string argGRNo, ref CargoExportResult argGRResult)
        {
            ICommonProxy proxy = new CommonProxy();
            Framework.Service.Provider.WebService.Provider.CargoExportParm cgExpParm = new Framework.Service.Provider.WebService.Provider.CargoExportParm();
            if (!string.IsNullOrEmpty(argJPVC))
            {
                cgExpParm.vslCallId = argJPVC;
            }
            cgExpParm.cgNo = argGRNo;
            ResponseInfo info = proxy.getCargoExportList(cgExpParm);

            if ((info != null) && (info.list != null) && (info.list.Length == 1) && (info.list[0] is CargoExportItem))
            {
                CargoExportItem item = (CargoExportItem)info.list[0];

                if (!item.grNo.Equals(argGRNo))
                {
                    return false;
                }

                argGRResult = new CargoExportResult();
                argGRResult.GrNo = item.grNo;
                argGRResult.VslCallId = item.vslCallId;
                argGRResult.ShipgNoteNo = item.shipgNoteNo;
                argGRResult.Lorry = item.lorryId;
                argGRResult.Qty = CommonUtility.ToString(item.docQty);
                argGRResult.Mt = CommonUtility.ToString(item.docMt);
                argGRResult.M3 = CommonUtility.ToString(item.docM3);
                argGRResult.FnlOpeYn = item.fnlOpeYn;
                argGRResult.HiFnlYn = item.hiFnlYn;
                argGRResult.RhdlMode = item.rhdlMode;
                argGRResult.DmgYn = item.dmgYn;
                argGRResult.ShuYn = item.shuYn;
                argGRResult.SpYn = item.spYn;
                argGRResult.PkgNo = item.pkgNo;
                argGRResult.CgTpCd = item.cgTpCd;
                argGRResult.DelvTpCd = item.delvTpCd;
                argGRResult.DelvTpNm = item.delvTpNm;
                argGRResult.StatCd = item.statCd;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check B/L Number.
        /// </summary>
        /// <param name="argJPVC"></param>
        /// <param name="argJPVCResult"></param>
        /// <returns></returns>
        public static bool IsValidBL(string argJPVC, string argBLNo, ref BLListResult argBLResult)
        {
            if (string.IsNullOrEmpty(argBLNo))
            {
                return false;
            }
            ICommonProxy proxy = new CommonProxy();
            Framework.Service.Provider.WebService.Provider.CargoImportParm parm = new Framework.Service.Provider.WebService.Provider.CargoImportParm();
            if (!string.IsNullOrEmpty(argJPVC))
            {
                parm.vslCallId = argJPVC;
            }
            parm.blNo = argBLNo;
            parm.searchType = "validate";

            ResponseInfo info = proxy.getCargoImportList(parm);

            if ((info != null) && (info.list != null) && (info.list.Length == 1) && (info.list[0] is CargoImportItem))
            {
                CargoImportItem item = (CargoImportItem)info.list[0];

                if (!item.blNo.Equals(argBLNo))
                {
                    return false;
                }

                argBLResult = new BLListResult();
                argBLResult.VslCallId = item.vslCallId;
                argBLResult.Bl = item.blNo;
                argBLResult.DoNo = item.doNo;
                argBLResult.FwrAgnt = item.fwrAgnt;
                argBLResult.Qty = CommonUtility.ToString(item.docQty);
                argBLResult.Mt = CommonUtility.ToString(item.docMt);
                argBLResult.M3 = CommonUtility.ToString(item.docM3);
                argBLResult.CgTpCd = item.cgTpCd;
                argBLResult.FnlDelvYn = item.fnlDelvYn;
                argBLResult.FnlOpeYn= item.fnlOpeYn;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Check D/O number.
        /// </summary>
        /// <param name="argDONo"></param>
        /// <param name="argPtnrCd"></param>
        /// <returns></returns>
        public static bool IsValidDO(string argDONo, ref DOListResult argDOResult)
        {
            if (!string.IsNullOrEmpty(argDONo))
            {
                ICommonProxy proxy = new CommonProxy();
                Framework.Service.Provider.WebService.Provider.DeliveryOrderParm parm = new Framework.Service.Provider.WebService.Provider.DeliveryOrderParm();
                parm.dono = argDONo;
                parm.searchType = "doNo";
                ResponseInfo info = proxy.getDeliveryOrderNo(parm);
                if ((info != null) && (info.list != null) && (info.list.Length > 0) && (info.list[0] is DeliveryOrderItem))
                {
                    DeliveryOrderItem item = (DeliveryOrderItem)info.list[0];
                    argDOResult = new DOListResult();
                    argDOResult.Bl = item.blno;
                    argDOResult.DoNo = item.dono;
                    argDOResult.Mt = item.wgt;
                    argDOResult.M3 = item.vol;
                    argDOResult.Qty = item.pkgqty;
                    argDOResult.Tsptr = item.tsptr;
                    argDOResult.TsptrCompNm = item.tsptCompNm;
                    argDOResult.VslCallId = item.vslCallId;
                    argDOResult.DgYn = item.dgYn;
                    argDOResult.DgStatCd = item.dgStatCd;
                    argDOResult.DelvTpCd = item.delvTpCd;
                    argDOResult.DelvTpNm = item.delvTpNm;

                    //Added by Chris 2015-10-01
                    argDOResult.CmdtCd = item.cmdtcd;

                    return true;
                }
            }

            return false;
        }

        //Addded by Chris 2015-10-01
        public static bool IsValidDO2(string argDONo, string argLorryNo, ref DOListResult argDOResult)
        {
            if (!string.IsNullOrEmpty(argDONo))
            {
                ICommonProxy proxy = new CommonProxy();
                Framework.Service.Provider.WebService.Provider.DeliveryOrderParm parm = new Framework.Service.Provider.WebService.Provider.DeliveryOrderParm();
                parm.dono = argDONo;
                parm.lorryNo = argLorryNo;
                parm.searchType = "doNo";
                ResponseInfo info = proxy.getDeliveryOrderNo(parm);
                if ((info != null) && (info.list != null) && (info.list.Length > 0) && (info.list[0] is DeliveryOrderItem))
                {
                    DeliveryOrderItem item = (DeliveryOrderItem)info.list[0];
                    argDOResult = new DOListResult();
                    argDOResult.Bl = item.blno;
                    argDOResult.DoNo = item.dono;
                    argDOResult.Mt = item.wgt;
                    argDOResult.M3 = item.vol;
                    argDOResult.Qty = item.pkgqty;
                    argDOResult.Tsptr = item.tsptr;
                    argDOResult.TsptrCompNm = item.tsptCompNm;
                    argDOResult.VslCallId = item.vslCallId;
                    argDOResult.DgYn = item.dgYn;
                    argDOResult.DgStatCd = item.dgStatCd;
                    argDOResult.DelvTpCd = item.delvTpCd;
                    argDOResult.DelvTpNm = item.delvTpNm;

                    //Added by Chris 2015-10-01
                    argDOResult.CmdtCd = item.cmdtcd;

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check lorry number.
        /// </summary>
        /// <param name="argLorryNo"></param>
        /// <param name="argPtnrCd"></param>
        /// <returns></returns>
        public static bool IsValidRegisterationLorry(string argLorryNo, string argPtnrCd)
        {
            if (!string.IsNullOrEmpty(argLorryNo))
            {
                ICommonProxy proxy = new CommonProxy();
                ResponseInfo info = null;
                AssignmentLorrysParm lorryParm = new AssignmentLorrysParm();
                lorryParm.searchType = "popUpList";
                lorryParm.divCd = "LR";
                lorryParm.tyCd = "CD";
                lorryParm.ptnrCd = CommonUtility.PreprocessPtnrCd(argPtnrCd);
                lorryParm.cd = argLorryNo;

                info = proxy.getAssignmentLorrysItems(lorryParm);
                if ((info == null) || (info.list == null) || (info.list.Length < 1) || (!(info.list[0] is AssignmentLorrysItem)))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check if lorry was assigned to specific B/L.
        /// </summary>
        /// <param name="argVslCallId"></param>
        /// <param name="argLorryNo"></param>
        /// <param name="argBLNo"></param>
        /// <param name="argPtnrCd"></param>
        /// <returns></returns>
        public static bool IsValidAssignmentLorry(string argVslCallId, string argBLNo, string argLorryNo, string argPtnrCd)
        {
            if (!string.IsNullOrEmpty(argLorryNo))
            {
                ICommonProxy proxy = new CommonProxy();
                ResponseInfo info = null;
                AssignmentLorrysParm lorryParm = new AssignmentLorrysParm();
                lorryParm.searchType = "onlyLorry";
                lorryParm.lorryNo = argLorryNo;
                if (!string.IsNullOrEmpty(argVslCallId))
                {
                    lorryParm.vslCallId = argVslCallId;
                }
                if (!string.IsNullOrEmpty(argBLNo))
                {
                    lorryParm.blNo = argBLNo;
                }
                lorryParm.tsptr = CommonUtility.PreprocessPtnrCd(argPtnrCd);
                info = proxy.getAssignmentLorrysItems(lorryParm);

                if ((info == null) || (info.list == null) || (info.list.Length < 1) || (!(info.list[0] is AssignmentLorrysItem)))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check driver number.
        /// </summary>
        /// <param name="argLorryNo"></param>
        /// <param name="argBLNo"></param>
        /// <param name="argPtnrCd"></param>
        /// <returns></returns>
        public static bool IsValidDriver(string argLorryNo, string argPtnrCd, ref PartnerCodeListResult driverResult)
        {
            if (!string.IsNullOrEmpty(argLorryNo))
            {
                ICommonProxy proxy = new CommonProxy();
                ResponseInfo info = null;

                AssignmentLorrysParm lorryParm = new AssignmentLorrysParm();
                lorryParm.searchType = "popUpList";
                lorryParm.divCd = "DV";
                lorryParm.ptnrCd = CommonUtility.PreprocessPtnrCd(argPtnrCd);
                lorryParm.tyCd = "CD";
                lorryParm.cd = argLorryNo;
                info = proxy.getAssignmentLorrysItems(lorryParm);
                if ((info != null) && (info.list != null) && (info.list.Length == 1) && (info.list[0] is AssignmentLorrysItem))
                {
                    AssignmentLorrysItem item = (AssignmentLorrysItem)info.list[0];
                    driverResult = new PartnerCodeListResult();
                    driverResult.DriverNo = item.cd;
                    driverResult.DriverId = item.cdNm;
                    driverResult.DriverCom = item.ptnrCd;
                    driverResult.LicsNo = item.licsNo;
                    driverResult.LicsExprYmd = item.licsExprYmd;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check gatepass number.
        /// </summary>
        /// <param name="argGPNo"></param>
        /// <param name="argGPResult"></param>
        /// <returns></returns>
        public static bool IsValidGP(string argGPNo, ref GatePassListResult argGPResult)
        {
            if (string.IsNullOrEmpty(argGPNo))
            {
                return false;
            }

            ICommonProxy proxy = new CommonProxy();
            Framework.Service.Provider.WebService.Provider.CargoGatePassParm parm = new Framework.Service.Provider.WebService.Provider.CargoGatePassParm();
            parm.gatePassNo = argGPNo;
            parm.hhtFlag = "Y";
            ResponseInfo info = proxy.getCargoGatePassNo(parm);
            if ((info != null) && (info.list != null) && (info.list.Length == 1) && (info.list[0] is CargoGatePassItem))
            {
                CargoGatePassItem item = (CargoGatePassItem)info.list[0];
                argGPResult = new GatePassListResult();
                argGPResult.GatePass = item.gatePassNo;
                argGPResult.VslCallId = item.vslCallId;
                argGPResult.CgNo = item.cgNo;
                argGPResult.LorryNo = item.lorryNo;
                argGPResult.Tsptr = item.tsptr;
                argGPResult.CgInOutCd = item.cgInOutCd;
                argGPResult.Wgt = CommonUtility.ToString(item.wgt);
                argGPResult.Mrsmt = CommonUtility.ToString(item.msrmt);
                argGPResult.PkgQty = CommonUtility.ToString(item.pkgQty);
                argGPResult.Catgcd = item.catgCd;
                argGPResult.Seq = CommonUtility.ToString(item.seq);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check package type.
        /// </summary>
        /// <param name="argPkgTp"></param>
        /// <returns></returns>
        public static bool IsValidPkgTp(string argPkgTp)
        {
            if (!string.IsNullOrEmpty(argPkgTp))
            {
                ICommonProxy proxy = new CommonProxy();
                CommonCodeParm commonParm = new CommonCodeParm();
                commonParm.searchType = "COMM";
                commonParm.divCd = "PKGTP";
                commonParm.lcd = "MT";
                commonParm.tyCd = "CD";
                commonParm.cd = argPkgTp;
                ResponseInfo info = proxy.getCommonCodeList(commonParm);

                if ((info == null) || (info.list == null) || (info.list.Length != 1) || 
                    !(info.list[0] is CodeMasterListItem || info.list[0] is CodeMasterListItem1))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check loose bag type.
        /// </summary>
        /// <param name="argBagTp"></param>
        /// <returns></returns>
        public static bool IsValidLooseBagTp(string argBagTp)
        {
            if (!string.IsNullOrEmpty(argBagTp))
            {
                ICommonProxy proxy = new CommonProxy();
                CommonCodeParm commonParm = new CommonCodeParm();
                commonParm.searchType = "COMM";
                commonParm.divCd = "PKGTP";
                commonParm.lcd = "MT";
                commonParm.tyCd = "CD";
                commonParm.col1 = "LB";
                commonParm.cd = argBagTp;
                ResponseInfo info = proxy.getCommonCodeList(commonParm);

                if ((info == null) || (info.list == null) || (info.list.Length != 1) || !(info.list[0] is CodeMasterListItem || info.list[0] is CodeMasterListItem1))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Pre-process company code.
        /// </summary>
        /// <returns></returns>
        public static string PreprocessPtnrCd(string argPtnrCd)
        {
            // pre-process Company Cd
            string strPtnrCd = "";
            if (!string.IsNullOrEmpty(argPtnrCd))
            {
                string[] arrTmp = argPtnrCd.Split(',');
                for (int i = 0; i < arrTmp.Length; i++)
                {
                    if (strPtnrCd.Length == 0)
                    {
                        strPtnrCd = "'" + arrTmp[i] + "'";
                    }
                    else
                    {
                        strPtnrCd += ",'" + arrTmp[i] + "'";
                    }
                }
            }

            return strPtnrCd;
        }

        /// <summary>
        /// Get current server time.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentServerTime()
        {
            string strResult = "";
            ICommonProxy proxy = new CommonProxy();
            CurrentServerTimeParm parm = new CurrentServerTimeParm();

            ResponseInfo info = proxy.getCurrentServerTime(parm);
            if (info != null && info.list.Length > 0) {
                CurrentServerTimeItem item = (CurrentServerTimeItem)info.list[0];
                strResult = item.currentServerTime;
            }

            return strResult;
        }

        /// <summary>
        /// Set list of hatch number to combobox.
        /// </summary>
        /// <returns></returns>
        public static void SetHatchInfo(ComboBox cboCtrl)
        {
            // Request Webservice
            ICommonProxy proxy = new CommonProxy();
            CommonCodeParm parm = new CommonCodeParm();
            parm.searchType = "COMM";
            parm.lcd = "MT";
            parm.divCd = "HTC";
            ResponseInfo resInfo = proxy.getCommonCodeList(parm);

            // Display Data
            CommonUtility.InitializeCombobox(cboCtrl);
            for (int i = 0; i < resInfo.list.Length; i++)
            {
                if (resInfo.list[i] is CodeMasterListItem)
                {
                    CodeMasterListItem item = (CodeMasterListItem)resInfo.list[i];
                    cboCtrl.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                }
                else if (resInfo.list[i] is CodeMasterListItem1)
                {
                    CodeMasterListItem1 item = (CodeMasterListItem1)resInfo.list[i];
                    cboCtrl.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scdNm));
                }
            }
        }

        /// <summary>
        /// argDate: DD/MM/YYYY
        /// Format date YYYYMMDD
        /// </summary>
        /// <param name="argDate"></param>
        /// <returns></returns>
        public static string FormatDateYYYYMMDD(string argDate)
        {
            if (!string.IsNullOrEmpty(argDate) && argDate.Length == 10)
            {
                string strYear = argDate.Substring(6);
                string strMonth = argDate.Substring(3, 2);
                string strDay = argDate.Substring(0, 2);
                return strYear + strMonth + strDay;
            }
            return null;
        }

        /// <summary>
        /// Set value of TDateTimePicker control.
        /// <br>The value is formatted as [dd/MM/yyyy HH:mm] or [dd/MM/yyyy HH:mm:ss] or [dd/MM/yyyy]</br>
        /// <br></br>
        /// </summary>
        /// <param name="dtpCtrl"></param>
        /// <param name="value"></param>
        /// <param name="valueFormat"></param>
        /// <returns></returns>
        private static void SetDTPValue(TDateTimePicker dtpCtrl, string value, string valueFormat)
        {
            if (dtpCtrl != null)
            {

                dtpCtrl.Format = DateTimePickerFormat.Custom;
                if (value == null || string.IsNullOrEmpty(value.Trim()))
                {
                    dtpCtrl.CustomFormat = TDateTimePicker.FORMAT_BLANK;
                }
                else
                {
                    dtpCtrl.CustomFormat = TDateTimePicker.FORMAT_DDMMYYYYHHMM;
                    if (CommonUtility.DDMMYYYYHHMM.Equals(valueFormat))
                    {
                        dtpCtrl.Value = CommonUtility.ParseYMDHM(value);
                    }
                    else if (CommonUtility.DDMMYYYYHHMMSS.Equals(valueFormat))
                    {
                        dtpCtrl.Value = CommonUtility.ParseYMDHMS(value);
                    }
                    else if (CommonUtility.DDMMYYYY.Equals(valueFormat))
                    {
                        dtpCtrl.Value = CommonUtility.ParseYMD(value);
                    }
                }
            }
        }

        /// <summary>
        /// Set value of TDateTimePicker control.
        /// <br>The value is formatted as [dd/MM/yyyy]</br>
        /// </summary>
        /// <param name="dtpCtrl"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void SetDTPValueDMY(TDateTimePicker dtpCtrl, string value)
        {
            CommonUtility.SetDTPValue(dtpCtrl, value, CommonUtility.DDMMYYYY);
        }
        
        /// <summary>
        /// Set value of TDateTimePicker control.
        /// <br>The value is formatted as [dd/MM/yyyy HH:mm]</br>
        /// </summary>
        /// <param name="dtpCtrl"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void SetDTPValueDMYHM(TDateTimePicker dtpCtrl, string value)
        {
            CommonUtility.SetDTPValue(dtpCtrl, value, CommonUtility.DDMMYYYYHHMM);
        }

        /// <summary>
        /// Set value of TDateTimePicker control.
        /// <br>The value is formatted as [dd/MM/yyyy HH:mm:ss]</br>
        /// </summary>
        /// <param name="dtpCtrl"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void SetDTPValueDMYHMS(TDateTimePicker dtpCtrl, string value)
        {
            CommonUtility.SetDTPValue(dtpCtrl, value, CommonUtility.DDMMYYYYHHMMSS);
        }

        /// <summary>
        /// Set value of TDateTimePicker control.
        /// <br>The value is formatted as [dd/MM/yyyy HH:mm:ss]</br>
        /// </summary>
        /// <param name="dtpCtrl"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void SetDTPValueBlank(TDateTimePicker dtpCtrl)
        {
            // Set TDateTimePicker value the current date time.
            dtpCtrl.Value = DateTime.Now;
            CommonUtility.SetDTPValue(dtpCtrl, "", " ");
        }

        /// <summary>
        /// Initialize combobox.
        /// </summary>
        /// <param name="cboCtrl"></param>
        /// <returns></returns>
        public static void InitializeCombobox(ComboBox cboCtrl)
        {
            CommonUtility.InitializeCombobox(cboCtrl, "");
        }

        /// <summary>
        /// Initialize combobox.
        /// </summary>
        /// <param name="cboCtrl"></param>
        /// <param name="strFirstLine"></param>
        /// <returns></returns>
        public static void InitializeCombobox(ComboBox cboCtrl, string strFirstLine)
        {
            cboCtrl.Items.Clear();
            cboCtrl.Items.Add(new ComboboxValueDescriptionPair("", strFirstLine));
            cboCtrl.SelectedIndex = 0;
        }

        /// <summary>
        /// Load AP/FP combobox.
        /// </summary>
        /// <param name="cboAPFP"></param>
        /// <returns></returns>
        public static void SetHatchDirectionAPFP(ComboBox cboAPFP)
        {
            //CommonUtility.initializeCombobox(cboAPFP);
            //cboAPFP.Items.Add(new ComboboxValueDescriptionPair("FP", "FP"));
            //cboAPFP.Items.Add(new ComboboxValueDescriptionPair("AP", "AP"));

            // Request Webservice
            ICommonProxy proxy = new CommonProxy();
            CommonCodeParm parm = new CommonCodeParm();
            parm.searchType = "COMM";
            parm.lcd = "MT";
            parm.divCd = "HCHDRT";
            ResponseInfo resInfo = proxy.getCommonCodeList(parm);

            // Display Data

            CommonUtility.InitializeCombobox(cboAPFP, "");
            for (int i = 0; i < resInfo.list.Length; i++)
            {
                if (resInfo.list[i] is CodeMasterListItem)
                {
                    CodeMasterListItem item = (CodeMasterListItem)resInfo.list[i];
                    cboAPFP.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scd));
                }
                else if (resInfo.list[i] is CodeMasterListItem1)
                {
                    CodeMasterListItem1 item = (CodeMasterListItem1)resInfo.list[i];
                    cboAPFP.Items.Add(new ComboboxValueDescriptionPair(item.scd, item.scd));
                }
            }
        }

        /// <summary>
        /// Set selected item of a combobox that corresponds to strSelectedValue.
        /// </summary>
        /// <param name="cboCtrl"></param>
        /// <param name="strSelectedValue"></param>
        /// <returns></returns>
        public static void SetComboboxSelectedItem(ComboBox cboCtrl, string strSelectedValue)
        {
            if (cboCtrl != null && cboCtrl.Items != null && cboCtrl.Items.Count > 0)
            {
                int selectedIndex = -1;
                if (!string.IsNullOrEmpty(strSelectedValue))
                {
                    for (int i = 0; i < cboCtrl.Items.Count; i++)
                    {
                        string value = CommonUtility.GetComboboxSelectedValueAt(cboCtrl, i);
                        if (strSelectedValue.Equals(value))
                        {
                            selectedIndex = i;
                            break;
                        }
                    }
                }
                cboCtrl.SelectedIndex = selectedIndex;
            }
        }

        /// <summary>
        /// Set selected item of a combobox that corresponds to strSelectedValue.
        /// </summary>
        /// <param name="cboCtrl"></param>
        /// <param name="strSelectedValue"></param>
        /// <returns></returns>
        public static void SetComboboxSelectedDescription(ComboBox cboCtrl, string strSelectedDescription)
        {
            if (cboCtrl != null && cboCtrl.Items != null && cboCtrl.Items.Count > 0)
            {
                int selectedIndex = -1;
                if (!string.IsNullOrEmpty(strSelectedDescription))
                {
                    for (int i = 0; i < cboCtrl.Items.Count; i++)
                    {
                        string desc = CommonUtility.GetComboboxSelectedDescriptionAt(cboCtrl, i);
                        if (strSelectedDescription.Equals(desc))
                        {
                            selectedIndex = i;
                            break;
                        }
                    }
                }
                cboCtrl.SelectedIndex = selectedIndex;
            }
        }

        /// <summary>
        /// Get value of selected item.
        /// </summary>
        /// <param name="cboCtrl"></param>
        /// <returns></returns>
        public static string GetComboboxSelectedValue(ComboBox cboCtrl)
        {
            string result = "";
            if (cboCtrl != null)
            {
                result = CommonUtility.GetComboboxSelectedValueAt(cboCtrl, cboCtrl.SelectedIndex);
            }
            return result;
        }

        /// <summary>
        /// Get value of selected item at corresponding index.
        /// </summary>
        /// <param name="cboCtrl"></param>
        /// <returns></returns>
        public static string GetComboboxSelectedValueAt(ComboBox cboCtrl, int index)
        {
            string result = "";
            if (cboCtrl != null && cboCtrl.Items != null && cboCtrl.Items.Count > 0 && index > -1)
            {
                object item = cboCtrl.Items[index];
                if (item != null)
                {
                    if (item is ComboboxValueDescriptionPair)
                    {
                        ComboboxValueDescriptionPair cboItem = (ComboboxValueDescriptionPair)item;
                        if (cboItem != null && cboItem.Value != null)
                        {
                            result = cboItem.Value.ToString();
                        }
                    }
                    else
                    {
                        result = item.ToString();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get description of selected item.
        /// </summary>
        /// <param name="cboCtrl"></param>
        /// <returns></returns>
        public static string GetComboboxSelectedDescription(ComboBox cboCtrl)
        {
            string result = "";
            if (cboCtrl != null)
            {
                result = CommonUtility.GetComboboxSelectedDescriptionAt(cboCtrl, cboCtrl.SelectedIndex);
            }
            return result;
        }

        /// <summary>
        /// Get value of selected description at corresponding index.
        /// </summary>
        /// <param name="cboCtrl"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetComboboxSelectedDescriptionAt(ComboBox cboCtrl, int index)
        {
            string result = "";
            if (cboCtrl != null && index > -1 && cboCtrl.Items.Count > 0)
            {
                object item = cboCtrl.Items[index];
                if (item != null)
                {
                    if (item is ComboboxValueDescriptionPair)
                    {
                        ComboboxValueDescriptionPair cboItem = (ComboboxValueDescriptionPair)item;
                        result = cboItem.Description.ToString();
                    }
                    else
                    {
                        result = item.ToString();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Get value of selected item at corresponding index.
        /// </summary>
        /// <param name="cboCtrl"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetArrayListSelectedValueAt(ArrayList cboCtrl, int index)
        {
            string result = "";
            if (cboCtrl != null && index > -1 && cboCtrl.Count > 0)
            {
                object item = cboCtrl[index];
                if (item != null)
                {
                    if (item is ComboboxValueDescriptionPair)
                    {
                        ComboboxValueDescriptionPair cboItem = (ComboboxValueDescriptionPair)item;
                        result = cboItem.Value.ToString();
                    }
                    else
                    {
                        result = item.ToString();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Sort items in combobox with alphabetical order.
        /// </summary>
        /// <param name="ctlCombobox"></param>
        /// <returns></returns>
        public static void SortComboboxItems(ComboBox ctlCombobox)
        {
            if (ctlCombobox != null && ctlCombobox.Items.Count > 0)
            {
                ArrayList lstTempItems = new ArrayList();
                Object objListItem;

                // store listitems in temp arraylist
                foreach (Object tmpItem in ctlCombobox.Items)
                {
                    objListItem = tmpItem;
                    lstTempItems.Add(objListItem);
                }

                // sort arraylist based on text value
                lstTempItems.Sort(new ListItemComparer());

                // clear control
                ctlCombobox.Items.Clear();

                // add listitems to control
                foreach (Object tmpItem in lstTempItems)
                {
                    objListItem = tmpItem;
                    ctlCombobox.Items.Add(objListItem);
                }
            }
        }

        public static int ParseInt(string argInt)
        {
            try
            {
                if (!string.IsNullOrEmpty(argInt) && !string.IsNullOrEmpty(argInt.Trim()))
                {
                    return int.Parse(argInt);
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.Message + " ParseInt.Argument=" + argInt.ToString());
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            
            return 0;
        }

        public static long ParseLong(string argLong)
        {
            try
            {
                if (!string.IsNullOrEmpty(argLong) && !string.IsNullOrEmpty(argLong.Trim()))
                {
                    return long.Parse(argLong);
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.Message + " ParseLong.Argument=" + argLong.ToString());
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }

            return 0;
        }

        public static float ParseFloat(string argFloat)
        {
            try
            {
                if (!string.IsNullOrEmpty(argFloat) && !string.IsNullOrEmpty(argFloat.Trim()))
                {
                    return float.Parse(argFloat);
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.Message + " ParseFloat.Argument=" + argFloat.ToString());
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }

            return 0;
        }

        public static double ParseDouble(string argDouble)
        {
            try
            {
                if (!string.IsNullOrEmpty(argDouble) && !string.IsNullOrEmpty(argDouble.Trim()))
                {
                    return double.Parse(argDouble);
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.Message + " ParseDouble.Argument=" + argDouble.ToString());
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            
            return 0;
        }

        public static decimal ParseDecimal(string argDecimal)
        {
            try
            {
                if (!string.IsNullOrEmpty(argDecimal) && !string.IsNullOrEmpty(argDecimal.Trim()))
                {
                    return decimal.Parse(argDecimal);
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.Message + " ParseDecimal.Argument=" + argDecimal.ToString());
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            
            return 0;
        }

        public static string ToString(object argObj)
        {
            if (argObj != null)
            {
                return argObj.ToString();
            }
            return "";
        }

        public static string ToStringNumber(object argObj)
        {
            if (argObj != null && !string.IsNullOrEmpty(argObj.ToString().Trim()))
            {
                return argObj.ToString();
            }
            return "0";
        }

        /// <summary>
        /// Parse a string(format 'dd/MM/yyyy') to DateTime.
        /// </summary>
        /// <param name="strYMDHM"></param>
        /// <returns>DateTime</returns>
        public static DateTime ParseYMD(string strYMD)
        {
            return ParseStringToDate(strYMD, "dd/MM/yyyy");
        }

        /// <summary>
        /// Parse a string(format 'dd/MM/yyyy HH:mm') to DateTime.
        /// </summary>
        /// <param name="strYMDHM"></param>
        /// <returns>DateTime</returns>
        public static DateTime ParseYMDHM(string strYMDHM)
        {
            return ParseStringToDate(strYMDHM, "dd/MM/yyyy HH:mm");
        }

        /// <summary>
        /// Parse a string(format 'dd/MM/yyyy HH:mm:ss') to DateTime.
        /// </summary>
        /// <param name="strYMDHMS"></param>
        /// <returns>DateTime</returns>
        /// 

        public static DateTime ConvertDt(string strDatetime)
        {
            return DateTime.Parse(strDatetime + ":00");
        }



        public static DateTime ParseYMDHMS(string strYMDHMS)
        {
            return ParseStringToDate(strYMDHMS, "dd/MM/yyyy HH:mm:ss");
        }

        /// <summary>
        /// Parse a string to DateTime with approciated format.
        /// </summary>
        /// <param name="strDatetime"></param>
        /// <param name="strFormat"></param>
        /// <returns>DateTime</returns>
        public static DateTime ParseStringToDate(string strDatetime, string strFormat)
        {
            try
            {
                if (!string.IsNullOrEmpty(strDatetime) && !string.IsNullOrEmpty(strDatetime.Trim()) && !string.IsNullOrEmpty(strFormat))
                {
                    IFormatProvider formatProvider = System.Globalization.CultureInfo.CreateSpecificCulture("ms-MY");
                    return DateTime.ParseExact(strDatetime, strFormat, formatProvider);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to convert '{0}'.", strDatetime);
            }
            return DateTime.Now;
        }
        
        /// <summary>
        /// Alert message
        /// </summary>
        /// <param name="text"></param>
        /// <param name="title"></param>
        /// <returns>DialogResult</returns>
        public static DialogResult AlertMessage(string text, string title)
        {
            return MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Alert message
        /// </summary>
        /// <param name="text"></param>
        /// <returns>DialogResult</returns>
        public static DialogResult AlertMessage(string text)
        {
            return AlertMessage(text, "Message");
        }

        /// <summary>
        /// Confirm message
        /// </summary>
        /// <param name="text"></param>
        /// <param name="title"></param>
        /// <returns>DialogResult</returns>
        public static DialogResult ConfirmMsgSaveChances()
        {
            return MessageBox.Show(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0061"), "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
        }

        /// <summary>
        /// Confirm message
        /// </summary>
        /// <param name="text"></param>
        /// <param name="title"></param>
        /// <returns>DialogResult</returns>
        public static DialogResult ConfirmMessage(string text, string title)
        {
            return MessageBox.Show(text, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        /// <summary>
        /// Confirm message
        /// </summary>
        /// <param name="text"></param>
        /// <returns>DialogResult</returns>
        public static DialogResult ConfirmMessage(string text)
        {
            return ConfirmMessage(text, "Confirm");
        }

        /// <summary>
        /// Get CRUD code coresponding to the given name.
        /// </summary>
        /// <param name="crudName"></param>
        /// <returns>CRUD</returns>
        public static string GetCRUDFromName(string crudName)
        {
            string result = "";
            switch (crudName)
            {
                case Framework.Common.Constants.Constants.WS_NM_INITIAL:
                    result = Framework.Common.Constants.Constants.WS_INITIAL;
                    break;
                case Framework.Common.Constants.Constants.WS_NM_QUERY:
                    result = Framework.Common.Constants.Constants.WS_QUERY;
                    break;
                case Framework.Common.Constants.Constants.WS_NM_INSERT:
                    result = Framework.Common.Constants.Constants.WS_INSERT;
                    break;
                case Framework.Common.Constants.Constants.WS_NM_UPDATE:
                    result = Framework.Common.Constants.Constants.WS_UPDATE;
                    break;
                case Framework.Common.Constants.Constants.WS_NM_DELETE:
                    result = Framework.Common.Constants.Constants.WS_DELETE;
                    break;
            }
            return result;
        }

        /// <summary>
        /// Validate date: ATB <= ATW <= ATC <= ATU or ETB <= ETW <= ETC <= ETU
        /// </summary>
        /// <param name="txtATB"></param>
        /// <param name="txtATW"></param>
        /// <param name="txtATC"></param>
        /// <param name="txtATU"></param>
        /// <returns></returns>
        public static bool ValidateDateOrder(TDateTimePicker txtATB, TDateTimePicker txtATW, 
                                        TDateTimePicker txtATC, TDateTimePicker txtATU)
        {
            // ATB <= ATW <= ATC <= ATU
            bool atbEmpty = true;
            bool atwEmpty = true;
            bool atcEmpty = true;
            bool atuEmpty = true;
            DateTime dtATB = DateTime.MinValue;
            DateTime dtATW = DateTime.MinValue;
            DateTime dtATC = DateTime.MinValue;
            DateTime dtATU = DateTime.MinValue;
            if (!string.IsNullOrEmpty(txtATB.Text))
            {
                atbEmpty = false;
                dtATB = txtATB.Value;
            }
            if (!string.IsNullOrEmpty(txtATW.Text))
            {
                atwEmpty = false;
                dtATW = CommonUtility.ParseYMDHM(txtATW.Text);
            }
            if (!string.IsNullOrEmpty(txtATC.Text))
            {
                atcEmpty = false;
                dtATC = CommonUtility.ParseYMDHM(txtATC.Text);
            }
            if (!string.IsNullOrEmpty(txtATU.Text))
            {
                atuEmpty = false;
                dtATU = txtATU.Value;
            }

            if (!atbEmpty)
            {
                if (!atwEmpty && dtATB.CompareTo(dtATW) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0042"));
                    txtATB.Focus();
                    return false;
                }

                if (!atcEmpty && dtATB.CompareTo(dtATC) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0042"));
                    txtATB.Focus();
                    return false;
                }

                if (!atuEmpty && dtATB.CompareTo(dtATU) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0042"));
                    txtATB.Focus();
                    return false;
                }
            }

            if (!atwEmpty)
            {
                if (!atcEmpty && dtATW.CompareTo(dtATC) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0042"));
                    txtATW.Focus();
                    return false;
                }

                if (!atuEmpty && dtATW.CompareTo(dtATU) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0042"));
                    txtATW.Focus();
                    return false;
                }
            }

            if (!atcEmpty)
            {
                if (!atuEmpty && dtATC.CompareTo(dtATU) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0042"));
                    txtATC.Focus();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check if start time and end time are within work date & shift.
        /// </summary>
        /// <param name="argStartDtCtrl">DateTimePicker with format: dd/MM/yyyy HH:mm</param>
        /// <param name="argEndDtCtrl">DateTimePicker with format: dd/MM/yyyy HH:mm</param>
        ///
        public static bool ValidateStartEndDtWithinShift(TDateTimePicker argStartDtCtrl, TDateTimePicker argEndDtCtrl)
        {
            // Check if start time <= end time
            if (!CommonUtility.CheckDateStartEnd(argStartDtCtrl, argEndDtCtrl))
            {
                return false;
            }

            // Check if inputted date time is within work date & shift.
            string strStartTime = UserInfo.getInstance().Workdate + " ";
            string strEndTime = UserInfo.getInstance().Workdate + " ";
            if ("1".Equals(UserInfo.getInstance().ShiftIndex))
            {
                strStartTime += CommonUtility.SHIFT_1_FRM;
                strEndTime += CommonUtility.SHIFT_1_TO;
            }
            else if ("2".Equals(UserInfo.getInstance().ShiftIndex))
            {
                strStartTime += CommonUtility.SHIFT_2_FRM;
                strEndTime += CommonUtility.SHIFT_2_TO;
            }
            else if ("3".Equals(UserInfo.getInstance().ShiftIndex))
            {
                strStartTime += CommonUtility.SHIFT_3_FRM;

                DateTime workdate = CommonUtility.ParseYMD(UserInfo.getInstance().Workdate);
                workdate = workdate.AddDays(1);
                strEndTime = workdate.ToString(CommonUtility.DDMMYYYY);
                strEndTime += " " + CommonUtility.SHIFT_3_TO;
            }

            DateTime dtFm = CommonUtility.ParseYMDHM(strStartTime);
            DateTime dtTo = CommonUtility.ParseYMDHM(strEndTime);

            if (argStartDtCtrl != null && argStartDtCtrl.Value != null)
            {
                if (dtFm.CompareTo(argStartDtCtrl.Value) > 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0059"));
                    return false;
                }
            }
            if (argEndDtCtrl != null && argEndDtCtrl.Value != null)
            {
                if (dtTo.CompareTo(argEndDtCtrl.Value) < 0)
                {
                    CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0059"));
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check if inputted time is within work date & shift.
        /// </summary>
        /// <param name="argDtCtrl">DateTimePicker with format: dd/MM/yyyy HH:mm</param>
        public static bool ValidateDatetimeWithinShift(TDateTimePicker argDtCtrl)
        {
            // Check if inputted date time is within work date & shift.
            string strStartTime = UserInfo.getInstance().Workdate + " ";
            string strEndTime = UserInfo.getInstance().Workdate + " ";
            if ("1".Equals(UserInfo.getInstance().ShiftIndex))
            {
                strStartTime += CommonUtility.SHIFT_1_FRM;
                strEndTime += CommonUtility.SHIFT_1_TO;
            }
            else if ("2".Equals(UserInfo.getInstance().ShiftIndex))
            {
                strStartTime += CommonUtility.SHIFT_2_FRM;
                strEndTime += CommonUtility.SHIFT_2_TO;
            }
            else if ("3".Equals(UserInfo.getInstance().ShiftIndex))
            {
                strStartTime += CommonUtility.SHIFT_3_FRM;

                DateTime workdate = CommonUtility.ParseYMD(UserInfo.getInstance().Workdate);
                workdate = workdate.AddDays(1);
                strEndTime = workdate.ToString(CommonUtility.DDMMYYYY);
                strEndTime += " " + CommonUtility.SHIFT_3_TO;
            }

            DateTime dtFm = CommonUtility.ParseYMDHM(strStartTime);
            DateTime dtTo = CommonUtility.ParseYMDHM(strEndTime);

            if (argDtCtrl != null && argDtCtrl.Value != null)
            {
                if (dtFm.CompareTo(argDtCtrl.Value) > 0 || dtTo.CompareTo(argDtCtrl.Value) < 0)
                {
                    //CommonUtility.AlertMessage(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0059"));
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check if inputted date is within work date.
        /// </summary>
        /// <param name="argDtCtrl">DateTimePicker with format: dd/MM/yyyy</param>
        public static bool ValidateDateWithinWorkdate(TDateTimePicker argDtCtrl)
        {
            // Check if inputted date is within work date.
            if (argDtCtrl != null && !string.IsNullOrEmpty(argDtCtrl.Text))
            {
                string workdate = UserInfo.getInstance().Workdate;
                string date = argDtCtrl.Text;
                if (!string.IsNullOrEmpty(date) && date.Length > 10)
                {
                    date = date.Substring(0, 10);
                }

                if (!date.Equals(workdate))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Get shift start time.
        /// </summary>
        public static DateTime GetShiftStartTime()
        {
            string shiftIndex = UserInfo.getInstance().ShiftIndex;
            string strShiftDt = UserInfo.getInstance().Workdate + " ";
            DateTime shiftStartDt = DateTime.MinValue;

            if ("1".Equals(shiftIndex))
            {
                strShiftDt += CommonUtility.SHIFT_1_FRM;
            }
            else if ("2".Equals(shiftIndex))
            {
                strShiftDt += CommonUtility.SHIFT_2_FRM;
            }
            else if ("3".Equals(shiftIndex))
            {
                strShiftDt += CommonUtility.SHIFT_3_FRM;
            }

            if (!string.IsNullOrEmpty(strShiftDt))
            {
                shiftStartDt = CommonUtility.ParseYMDHM(strShiftDt);
            }

            return shiftStartDt;
        }

        /// <summary>
        /// Get shift end time.
        /// In order to calculate liquid pump rate, we assumes that:
        ///     1st shift end = 15:00 (instead of 14:59)
        ///     2nd shift end = 23:00 (instead of 22:59)
        ///     3rd shift end = 7:00 (instead of 6:59)
        /// </summary>
        public static DateTime GetShiftEndTime()
        {
            string shiftIndex = UserInfo.getInstance().ShiftIndex;
            string strShiftDt = UserInfo.getInstance().Workdate + " ";
            DateTime shiftEndDt = DateTime.MinValue;

            if ("1".Equals(shiftIndex))
            {
                strShiftDt += CommonUtility.SHIFT_2_FRM;
            }
            else if ("2".Equals(shiftIndex))
            {
                strShiftDt += CommonUtility.SHIFT_3_FRM;
            }
            else if ("3".Equals(shiftIndex))
            {
                DateTime workdate = CommonUtility.ParseYMD(UserInfo.getInstance().Workdate);
                workdate = workdate.AddDays(1);
                strShiftDt = workdate.ToString(CommonUtility.DDMMYYYY);
                strShiftDt += " " + CommonUtility.SHIFT_1_FRM;
            }

            if (!string.IsNullOrEmpty(strShiftDt))
            {
                shiftEndDt = CommonUtility.ParseYMDHM(strShiftDt);
            }

            return shiftEndDt;
        }

        /// <summary>
        /// Substring from beginning of text.
        /// </summary>
        /// <param name="argText"></param>
        /// <param name="length"></param>
        public static string SubstringText(string argText, int length)
        {
            if (!string.IsNullOrEmpty(argText) && argText.Length >= length && length > 0)
            {
                return argText.Substring(0, length);
            }
            return string.Empty;
        }

        /// <summary>
        /// Substring from beginning of text.
        /// </summary>
        /// <param name="argText"></param>
        /// <param name="length"></param>
        public static string CutText(string argText, int length)
        {
            if (!string.IsNullOrEmpty(argText))
            {
                if (argText.Length <= length)
                {
                    return argText;
                }
                else if (length > 0)
                {
                    return argText.Substring(0, length);
                }
            }
            return string.Empty;
        }

        public static bool ValidateFunc(string tyCd, params string[] cols)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
               
                ICommonProxy proxy = new CommonProxy();
                CommonCodeParm parm = new CommonCodeParm();

                switch (tyCd)
                {
                    case "vesselAtbEditable":
                    case "dblBnkValid":
                    case "laterEQValid":
                    case "shftValid":
                        if (cols == null || cols.Length < 2)
                        {
                            return false;
                        }
                        parm.tyCd = tyCd;
                        parm.col1 = CommonUtility.ToString(cols[0]);
                        parm.col2 = CommonUtility.ToString(cols[1]);
                        break;
                    case "dgConfirmedCmdt":
                        if (cols == null || cols.Length < 5)
                        {
                            return false;
                        }
                        parm.tyCd = tyCd;
                        parm.col1 = CommonUtility.ToString(cols[0]);
                        parm.col2 = CommonUtility.ToString(cols[1]);
                        parm.col3 = CommonUtility.ToString(cols[2]);
                        parm.col4 = CommonUtility.ToString(cols[3]);
                        parm.col5 = CommonUtility.ToString(cols[4]);
                        break;
                    case "atbExist":
                    case "stsValid":
                        if (cols == null || cols.Length < 1)
                        {
                            return false;
                        }
                        parm.tyCd = tyCd;
                        parm.col1 = CommonUtility.ToString(cols[0]);
                	    break;
                }

                ResponseInfo info = proxy.getValidationCode(parm);
                if (info == null || 
                    info.list == null || 
                    info.list.Length < 1 ||
                    !(info.list[0] is CommonCodeItem) || 
                    "N".Equals(((CommonCodeItem)info.list[0]).isValidated))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return true;
        }

        // Fix issue 0032175
        public static CodeMasterListItem1 ToCodeMasterListItem1(CodeMasterListItem item)
        {
            CodeMasterListItem1 codeItem = new CodeMasterListItem1();
            codeItem.acptYN = item.acptYN;
            codeItem.mcd = item.mcd;
            codeItem.mcdNm = item.mcdNm;
            codeItem.no = item.no;
            codeItem.scd = item.scd;
            codeItem.scdLgv = item.scdLgv;
            codeItem.scdVal = item.scdVal;
            codeItem.scdNm = item.scdNm;
            return codeItem;
        }

        //add get work loc type for checklist vsr from mega
        public static string GetWorkAreaType(string workArea)
        {
            if (workArea.Length < 1)
                return string.Empty;

            string firstChar = workArea.Substring(0, 1);
            if (firstChar.Equals("H"))
                return HCM113.AREA_HATCH;
            else if (firstChar.Equals("W") || firstChar.Equals("B"))
                return HCM113.AREA_WHARF;
            else
                return HCM113.AREA_WHO;
        }
    }
}
