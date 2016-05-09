using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using MOST.Client.Proxy.CommonProxy;
using Framework.Service.Provider.WebService.Provider;
using System.Collections;
using MOST.Client.Proxy.ApronCheckerProxy;
using MOST.Common.Utility;

namespace PlusNUnitTester.CommonModule
{
    [TestFixture]
    public class CommonCodeTester : AssertionHelper
    {
        //[Test]
        //public void testParseInteger()
        //{
        //    Console.WriteLine("======================================================");
        //    string testParm = null;
        //    int testResult = -1;

        //    testParm = null;
        //    testResult = CommonUtility.ParseInt(testParm);
        //    Console.WriteLine("Parm=null Result=" + testResult.ToString());

        //    testParm = string.Empty;
        //    testResult = CommonUtility.ParseInt(testParm);
        //    Console.WriteLine("Parm=empty Result=" + testResult.ToString());

        //    testParm = "abc";
        //    testResult = CommonUtility.ParseInt(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "abc123";
        //    testResult = CommonUtility.ParseInt(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "123abc";
        //    testResult = CommonUtility.ParseInt(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "123abc456";
        //    testResult = CommonUtility.ParseInt(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = ".123";
        //    testResult = CommonUtility.ParseInt(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "0.123";
        //    testResult = CommonUtility.ParseInt(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "-123";
        //    testResult = CommonUtility.ParseInt(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "+123";
        //    testResult = CommonUtility.ParseInt(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "25/12/2009";
        //    testResult = CommonUtility.ParseInt(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "100";
        //    testResult = CommonUtility.ParseInt(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());;

        //    testParm = "500L";
        //    testResult = CommonUtility.ParseInt(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "500l";
        //    testResult = CommonUtility.ParseInt(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());
        //    Console.WriteLine("======================================================");
        //}

        //[Test]
        //public void testParseLong()
        //{
        //    Console.WriteLine("======================================================");
        //    string testParm = null;
        //    long testResult = -1;

        //    testParm = null;
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=null Result=" + testResult.ToString());

        //    testParm = string.Empty;
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=empty Result=" + testResult.ToString());

        //    testParm = "abc";
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "abc123";
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "123abc";
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "123abc456";
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = ".123";
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "-.123";
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "0.123";
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "045.123";
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "-45.123";
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "+45.123";
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "-123";
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "+123";
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "25/12/2009";
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "100";
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "500L";
        //    testResult = CommonUtility.ParseInt(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "500l";
        //    testResult = CommonUtility.ParseInt(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "-500L";
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "+500L";
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "+500l";
        //    testResult = CommonUtility.ParseLong(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());
        //}

        //[Test]
        //public void testParseDouble()
        //{
        //    Console.WriteLine("======================================================");
        //    string testParm = null;
        //    double testResult = -1;

        //    testParm = null;
        //    testResult = CommonUtility.ParseDouble(testParm);
        //    Console.WriteLine("Parm=null Result=" + testResult.ToString());

        //    testParm = string.Empty;
        //    testResult = CommonUtility.ParseDouble(testParm);
        //    Console.WriteLine("Parm=empty Result=" + testResult.ToString());

        //    testParm = "abc";
        //    testResult = CommonUtility.ParseDouble(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "abc123";
        //    testResult = CommonUtility.ParseDouble(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "123abc";
        //    testResult = CommonUtility.ParseDouble(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "123abc456";
        //    testResult = CommonUtility.ParseDouble(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = ".123";
        //    testResult = CommonUtility.ParseDouble(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "-.123";
        //    testResult = CommonUtility.ParseDouble(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "0.123";
        //    testResult = CommonUtility.ParseDouble(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "045.123";
        //    testResult = CommonUtility.ParseDouble(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "-45.123";
        //    testResult = CommonUtility.ParseDouble(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "+45.123";
        //    testResult = CommonUtility.ParseDouble(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "-123";
        //    testResult = CommonUtility.ParseDouble(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "-123.45.6";
        //    testResult = CommonUtility.ParseDouble(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "123.456L";
        //    testResult = CommonUtility.ParseDouble(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());

        //    testParm = "25/12/2009";
        //    testResult = CommonUtility.ParseDouble(testParm);
        //    Console.WriteLine("Parm=" + testParm + " Result=" + testResult.ToString());
        //    Console.WriteLine("======================================================");
        //}

        //[Test]
        //public void getSearchJPVCList()
        //{
        //    ICommonProxy proxy = new CommonProxy();
        //    SearchJPVCParm parm = new SearchJPVCParm();
        //    parm.vslCallId = "";

        //    ResponseInfo info = proxy.getSearchJPVCList(parm);
        //    //ArrayList list = (ArrayList)info.list[0];
        //    Console.WriteLine("======================================================");
        //    foreach (SearchJPVCItem item in info.list)
        //    {
        //        Console.Write("JPVC : " + item.vslCallId);
        //        Console.WriteLine(", VesselName = " + item.vslNm);
        //    }
        //    Console.WriteLine("======================================================");
        //}

        //[Test]
        //public void getDelayCodeList()
        //{
        //    ICommonProxy proxy = new CommonProxy();
        //    DelayCodeParm parm = new DelayCodeParm();

        //    ResponseInfo info = proxy.getDealyCodeItems(parm);

        //    Console.WriteLine("======================================================");
        //    foreach(DelayCodeItem item in info.list){
        //        Console.Write("Delay Code : " + item.dlyCd);
        //        Console.WriteLine(", Desc = " + item.descr);
        //    }
        //    Console.WriteLine("======================================================");
        //}

        ////[Test]
        ////public void getGoodsReceiptList()
        ////{
        ////    ICommonProxy proxy = new CommonProxy();
        ////    GoodsReceiptParm parm = new GoodsReceiptParm();
        ////    parm.searchType = "update";

        ////    ResponseInfo info = proxy.getGoodsReceiptList(parm);

        ////    Console.WriteLine("======================================================");
        ////    for (int i = 0; i < info.list.Length; i++)
        ////    {
        ////        if (info.list[i] is CodeMasterListItem)
        ////        {
        ////            CodeMasterListItem item = (CodeMasterListItem)info.list[i];

        ////            Console.Write("CodeMasterListItem MCD : " + item.mcd);
        ////            Console.Write(",  SCD : " + item.scd);
        ////            Console.WriteLine(", CD NAME = " + item.scdNm);
        ////        }
        ////        else
        ////        {
        ////            GoodsReceiptItem item = (GoodsReceiptItem)info.list[i];
        ////            Console.Write("CBR No : " + item.cbrNo);
        ////            Console.WriteLine(", JPVC = " + item.vslCallId);
        ////        }
        ////    }
        ////    Console.WriteLine("======================================================");  
        ////}
        
        //[Test]
        //public void getDeliveryOrderList()
        //{
        //    ICommonProxy proxy = new CommonProxy();
        //    DeliveryOrderParm parm = new DeliveryOrderParm();
        //    parm.searchType = "detail";

        //    ResponseInfo info = proxy.getDeliveryOrderList(parm);

        //    Console.WriteLine("======================================================");
        //    for (int i = 0; i < info.list.Length; i++)
        //    {
        //        if (info.list[i] is CodeMasterListItem)
        //        {
        //            CodeMasterListItem item = (CodeMasterListItem)info.list[i];

        //            Console.Write("CodeMasterListItem MCD : " + item.mcd);
        //            Console.Write(",  SCD : " + item.scd);
        //            Console.WriteLine(", CD NAME = " + item.scdNm);
        //        }
        //        else
        //        {
        //            DeliveryOrderItem item = (DeliveryOrderItem)info.list[i];
        //            Console.Write("CGTPCD : " + item.cgtpcd);
        //            Console.WriteLine(", CGTPCD NAME = " + item.cgTpCdNm);
        //        }
        //    }
        //    Console.WriteLine("======================================================");  
        //}

        //[Test]
        //public void getInternalStaffMngList()
        //{
        //    ICommonProxy proxy = new CommonProxy();
        //    InternalStaffMngParm parm = new InternalStaffMngParm();
        //    parm.viewType = "staffcombo";

        //    ResponseInfo info = proxy.getInternalStaffMngList(parm);

        //    Console.WriteLine("======================================================");
        //    for(int i = 0; i < info.list.Length; i++)
        //    {
        //        if (info.list[i] is CodeMasterListItem)
        //        {
        //            CodeMasterListItem item = (CodeMasterListItem)info.list[i];

        //            Console.Write("CodeMasterListItem MCD : " + item.mcd);
        //            Console.Write(",  SCD : " + item.scd);
        //            Console.WriteLine(", CD NAME = " + item.scdNm);
        //        }
        //        else
        //        {
        //            InternalStaffMngItem item = (InternalStaffMngItem)info.list[i];
        //            Console.Write("InternalStaffMngItem GRDCD : " + item.grdCd);
        //            Console.WriteLine(", CRDCDNAME = " + item.grdCdNm);
        //        }
        //    }
        //    Console.WriteLine("======================================================");                    
        //}

        //[Test]
        //public void getPartnerCodeList()
        //{
        //    ICommonProxy proxy = new CommonProxy();
        //    PartnerCodeParm parm = new PartnerCodeParm();

        //    ResponseInfo info = proxy.getPartnerCodeList(parm);

        //    Console.WriteLine("======================================================");
        //    foreach (PartnerCodeItem item in info.list)
        //    {
        //        Console.Write("PTYCD : " + item.ptyCd);
        //        Console.WriteLine(", PTYCIDVNAMNE = " + item.ptyDivName);
        //    }
        //    Console.WriteLine("======================================================");
        //}

        //[Test]
        //public void getAssignmentLorrysItems()
        //{
        //    ICommonProxy proxy = new CommonProxy();
        //    AssignmentLorrysParm parm = new AssignmentLorrysParm();
        //    //parm.searchType = "lorryList";
        //    parm.searchType = "";
            

        //    ResponseInfo info = proxy.getAssignmentLorrysItems(parm);

        //    Console.WriteLine("======================================================");
        //    foreach (AssignmentLorrysItem item in info.list)
        //    {
        //        Console.Write("CD : " + item.cd);
        //        Console.WriteLine(",CDNM = " + item.cdNm);
        //    }
        //    Console.WriteLine("======================================================");
        //}

        //[Test]
        //public void getShippingNoteComboList()
        //{
        //    ICommonProxy proxy = new CommonProxy();
        //    ShippingNoteParm parm = new ShippingNoteParm();
        //    parm.searchType = "";
            
        //    ResponseInfo info = proxy.getShippingNoteComboList(parm);

        //    Console.WriteLine("======================================================");
        //    for (int i = 0; i < info.list.Length; i++)
        //    {
        //        if (info.list[i] is CodeMasterListItem)
        //        {
        //            CodeMasterListItem item = (CodeMasterListItem)info.list[i];

        //            Console.Write("CodeMasterListItem MCD : " + item.mcd);
        //            Console.Write(",  SCD : " + item.scd);
        //            Console.WriteLine(", CD NAME = " + item.scdNm);
        //        }
        //        else
        //        {
        //            ShippingNoteItem item = (ShippingNoteItem)info.list[i];
        //            Console.WriteLine("ShippingNoteItem shipgNoteNo : " + item.shipgNoteNo);
        //        }
        //    }
        //    Console.WriteLine("======================================================");
        //}

        //[Test]
        //public void getDeliveryOrderBLComboList()
        //{
        //    ICommonProxy proxy = new CommonProxy();
        //    DeliveryOrderParm parm = new DeliveryOrderParm();
        //    parm.searchType = "combo";

        //    ResponseInfo info = proxy.getDeliveryOrderBLComboList(parm);

        //    Console.WriteLine("======================================================");
        //    for (int i = 0; i < info.list.Length; i++)
        //    {
        //        if (info.list[i] is CodeMasterListItem)
        //        {
        //            CodeMasterListItem item = (CodeMasterListItem)info.list[i];

        //            Console.Write("CodeMasterListItem MCD : " + item.mcd);
        //            Console.Write(",  SCD : " + item.scd);
        //            Console.WriteLine(", CD NAME = " + item.scdNm);
        //        }
        //        else
        //        {
        //            DeliveryOrderItem item = (DeliveryOrderItem)info.list[i];
        //            Console.WriteLine("DeliveryOrderItem [B/L No] : " + item.blno);
        //        }
        //    }
        //    Console.WriteLine("======================================================");
        //}



        //[Test]
        //public void getCargoLoadingList()
        //{
        //    // Request Webservice
        //    IApronCheckerProxy proxy = new ApronCheckerProxy();
        //    Framework.Service.Provider.WebService.Provider.CargoLoadingParm parm = new Framework.Service.Provider.WebService.Provider.CargoLoadingParm();
        //    //parm.vslCallId = argJpvc;
        //    //if (!String.IsNullOrEmpty(argBLNo))
        //    //{
        //    //    parm.blNo = argBLNo;
        //    //}

        //    ResponseInfo info = proxy.getCargoLoadingList(parm);

        //    Console.WriteLine("======================================================");
        //    for (int i = 0; i < info.list.Length; i++)
        //    {
        //        if (info.list[i] is CodeMasterListItem)
        //        {
        //            CodeMasterListItem item = (CodeMasterListItem)info.list[i];

        //            Console.Write("CodeMasterListItem MCD : " + item.mcd);
        //            Console.Write(",  SCD : " + item.scd);
        //            Console.WriteLine(", CD NAME = " + item.scdNm);
        //        }
        //        else
        //        {
        //            CargoLoadingItem item = (CargoLoadingItem)info.list[i];
        //            Console.WriteLine("CargoLoadingItem vslCallId : " + item.vslCallId + "   MT:" + item.mt);
        //        }
        //    }
        //    Console.WriteLine("======================================================");
        //}

        //[Test]
        //public void getCargoDischargingList()
        //{
        //    // Request Webservice
        //    IApronCheckerProxy proxy = new ApronCheckerProxy();
        //    Framework.Service.Provider.WebService.Provider.CargoDischargingParm parm = new Framework.Service.Provider.WebService.Provider.CargoDischargingParm();
        //    //parm.vslCallId = argJpvc;
        //    //if (!String.IsNullOrEmpty(argBLNo))
        //    //{
        //    //    parm.blNo = argBLNo;
        //    //}
        //    parm.vslCallId = "aaa";
        //    parm.blNo = "bbb";

        //    ResponseInfo info = proxy.getCargoDischargingList(parm);

        //    Console.WriteLine("======================================================");
        //    for (int i = 0; i < info.list.Length; i++)
        //    {
        //        if (info.list[i] is CodeMasterListItem)
        //        {
        //            CodeMasterListItem item = (CodeMasterListItem)info.list[i];

        //            Console.Write("CodeMasterListItem MCD : " + item.mcd);
        //            Console.Write(",  SCD : " + item.scd);
        //            Console.WriteLine(", CD NAME = " + item.scdNm);
        //        }
        //        else
        //        {
        //            CargoDischargingItem item = (CargoDischargingItem)info.list[i];
        //            Console.WriteLine("CargoDischargingItem vslCallIdField : " + item.vslCallId + "   MT:" + item.mt);
        //        }
        //    }
        //    Console.WriteLine("======================================================");
        //}
    }
}
