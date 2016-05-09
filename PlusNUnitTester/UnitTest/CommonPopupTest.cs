using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using MOST.Client.Proxy.CommonProxy;
using Framework.Service.Provider.WebService.Provider;
using System.Collections;
using MOST.ApronChecker;
using MvcPatterns;
using System.Windows.Forms;
using MOST.CEClient;
using MOST.PortSafety;
using MOST.Client.Proxy.WHCheckerProxy;
using MOST.Client.Proxy.VesselOperatorProxy;
using MOST.Common;
using Framework.Common.PopupManager;

namespace PlusNUnitTester.CommonModule
{
    [TestFixture]
    public class CommonPopupTest : AssertionHelper
    {
        [Test]
        public void showLogin()
        {
            //ApplicationFacade facade = ApplicationFacade.getInstance();
            //Application.Run(new frmLogin());

            new frmLogin().ShowDialog();
            //new MOST.VesselOperator.HVO101().ShowDialog();
        }

        //[Test]
        //public void formatLocation()
        //{
        //    string argArrLoc = "3A-A1,3A-A2,5B-C1,3A-D2,MILA-E1,5B-E5,MILA-E2";
        //    System.Console.WriteLine(argArrLoc);

        //    string strLocation = "";
        //    if (!string.IsNullOrEmpty(argArrLoc))
        //    {
        //        string strWHId = "";
        //        string strWHIdTemp = "";
        //        string strCellId = "";
        //        string[] arrLocId = argArrLoc.Split(',');
                

        //        // Sort list
        //        List<string> lstLocId = new List<string>(arrLocId);
        //        lstLocId.Sort();

        //        for (int i = 0; i < lstLocId.Count; i++)
        //        {
        //            System.Console.Write(lstLocId[i] + ",");
        //        }

        //        for (int i = 0; i < lstLocId.Count; i++)
        //        {
        //            int index = lstLocId[i].IndexOf("-");
        //            strWHIdTemp = lstLocId[i].Substring(0, index);
        //            strCellId = lstLocId[i].Substring(index + 1);

        //            if (!strWHId.Equals(strWHIdTemp))
        //            {
        //                if (i == 0)
        //                {
        //                    strLocation += strWHIdTemp + "(";
        //                }
        //                else
        //                {
        //                    strLocation += ")," + strWHIdTemp + "(";
        //                }
        //                strLocation += strCellId;
        //            }
        //            else
        //            {
        //                strLocation += "," + strCellId;
        //            }
        //            strWHId = strWHIdTemp;
        //        }

        //        if (!string.IsNullOrEmpty(strLocation))
        //        {
        //            strLocation += ")";
        //        }
        //    }
        //    //return strLocation;

        //    System.Console.WriteLine(" ");
        //    System.Console.WriteLine("\n" + strLocation);
        //}

        //[Test]
        //public void HVO106_Bunkering()
        //{
        //    //MOST.VesselOperator.Parm.HVO106Parm parm = new MOST.VesselOperator.Parm.HVO106Parm();
        //    //parm.JpvcInfo = new MOST.Common.CommonResult.SearchJPVCResult();
        //    //parm.JpvcInfo.PurpCall = "BK";
        //    //MOST.VesselOperator.HVO106 ac = new MOST.VesselOperator.HVO106(MOST.VesselOperator.HVO106.WS_ADD);
        //    //ac.ShowDialog();

        //    MOST.VesselOperator.Parm.HVO106Parm parm = new MOST.VesselOperator.Parm.HVO106Parm();
        //    parm.JpvcInfo = new MOST.Common.CommonResult.SearchJPVCResult();
        //    parm.JpvcInfo.PurpCall = "BK";
        //    PopupManager.instance.ShowPopup(new MOST.VesselOperator.HVO106(MOST.VesselOperator.HVO106.WS_ADD), parm);
        //}

        //[Test]
        //public void HVO106_CargoOPR()
        //{
        //    //MOST.VesselOperator.Parm.HVO106Parm parm = new MOST.VesselOperator.Parm.HVO106Parm();
        //    //parm.JpvcInfo = new MOST.Common.CommonResult.SearchJPVCResult();
        //    //parm.JpvcInfo.PurpCall = "BK";
        //    //MOST.VesselOperator.HVO106 ac = new MOST.VesselOperator.HVO106(MOST.VesselOperator.HVO106.WS_ADD);
        //    //ac.ShowDialog();

        //    MOST.VesselOperator.Parm.HVO106Parm parm = new MOST.VesselOperator.Parm.HVO106Parm();
        //    parm.JpvcInfo = new MOST.Common.CommonResult.SearchJPVCResult();
        //    parm.JpvcInfo.PurpCall = "CO";
        //    PopupManager.instance.ShowPopup(new MOST.VesselOperator.HVO106(MOST.VesselOperator.HVO106.WS_ADD), parm);
        //}

        //[Test]
        //public void getCodeMasterList_Test()
        //{
        //    //MOST.VesselOperator.Parm.HVO106Parm parm = new MOST.VesselOperator.Parm.HVO106Parm();
        //    //parm.JpvcInfo = new MOST.Common.CommonResult.SearchJPVCResult();
        //    //parm.JpvcInfo.PurpCall = "BK";
        //    //MOST.VesselOperator.HVO106 ac = new MOST.VesselOperator.HVO106(MOST.VesselOperator.HVO106.WS_ADD);
        //    //ac.ShowDialog();

        //    //MOST.VesselOperator.Parm.HVO106Parm parm = new MOST.VesselOperator.Parm.HVO106Parm();
        //    //parm.JpvcInfo = new MOST.Common.CommonResult.SearchJPVCResult();
        //    //parm.JpvcInfo.PurpCall = "BK";
        //    //PopupManager.instance.ShowPopup(new MOST.VesselOperator.HVO106(MOST.VesselOperator.HVO106.WS_ADD), parm);

        //    //Framework.Common.PopupManager.PopupManager.instance.ShowPopup(new HCM113(), null);

        //    //MOST.Common.HCM113 ac = new MOST.Common.HCM113();
        //    //ac.ShowDialog();

        //    //IVesselOperatorProxy acProxy = new VesselOperatorProxy();
        //    //ResponseInfo info = null;
        //    //VORDryBreakBulkParm parm = null;

        //    //parm = new VORDryBreakBulkParm();
        //    //parm.vslCallId = "08MBVS-AAAAB";
        //    //info = acProxy.getVORDryBreakBulkCommonCd(parm);
        //    Console.WriteLine("======================================================");
        //    //for (int i = 0; i < info.list.Length; i++)
        //    //{
        //    //    if (info.list[i] is VORDryBreakBulkItem)
        //    //    {
        //    //        VORDryBreakBulkItem item = (VORDryBreakBulkItem)info.list[i];

        //    //        Console.Write("VORDryBreakBulkItem facNo : " + item.facNo);
        //    //        Console.Write(",  stevedore : " + item.stevedore);
        //    //        Console.WriteLine(", eqNo = " + item.eqNo);
        //    //    }
        //    //}
        //    //Console.WriteLine("======================================================");

        //    //string strDate1 = "18/09/2008 15:15:15";
        //    //string strDate2 = "18/09/2008 18:18";
        //    //string strDate3 = "18/09/2008 10:10";
        //    //string strDate4 = "18/09/2008 11:11:11";
        //    //try
        //    //{
        //    //    DateTime dateValue1 = DateTime.ParseExact(strDate2, "dd/MM/yyyy HH:mm", null);
        //    //    Console.WriteLine("'{0}' converted to {1}.", strDate2, dateValue1);
        //    //}
        //    //catch (FormatException)
        //    //{
        //    //    Console.WriteLine("Unable to convert '{0}'.", strDate2);
        //    //}

        //    //try
        //    //{
        //    //    DateTime dateValue2 = DateTime.Parse(strDate2);
        //    //    Console.WriteLine("'{0}' converted to {1}.", strDate2, dateValue2);
        //    //}
        //    //catch (FormatException)
        //    //{
        //    //    Console.WriteLine("Unable to convert '{0}'.", strDate2);
        //    //}

        //    //try
        //    //{
        //    //    DateTime dateValue3 = DateTime.Parse(strDate4);
        //    //    Console.WriteLine("'{0}' converted to {1}.", strDate4, dateValue3);
        //    //}
        //    //catch (FormatException)
        //    //{
        //    //    Console.WriteLine("Unable to convert '{0}'.", strDate4);
        //    //}

        //    //// Assume the current culture is en-US. 
        //    //// The date is February 16, 2008, 12 hours, 15 minutes and 12 seconds.

        //    //// Use standard en-US date and time value
        //    //DateTime dateValue;
        //    //string dateString = "2/16/2008 12:15:12 PM";
        //    //try
        //    //{
        //    //    dateValue = DateTime.Parse(dateString, IFormatProvider.);
        //    //    Console.WriteLine("'{0}' converted to {1}.", dateString, dateValue);
        //    //}
        //    //catch (FormatException)
        //    //{
        //    //    Console.WriteLine("Unable to convert '{0}'.", dateString);
        //    //}

        //    //// Reverse month and day to conform to the fr-FR culture.
        //    //// The date is February 16, 2008, 12 hours, 15 minutes and 12 seconds.
        //    //dateString = "16/02/2008 12:15:12";
        //    //try
        //    //{
        //    //    dateValue = DateTime.Parse(dateString);
        //    //    Console.WriteLine("'{0}' converted to {1}.", dateString, dateValue);
        //    //}
        //    //catch (FormatException)
        //    //{
        //    //    Console.WriteLine("Unable to convert '{0}'.", dateString);
        //    //}

        //    //// Call another overload of Parse to successfully convert string
        //    //// formatted according to conventions of fr-FR culture.      
        //    ////try
        //    ////{
        //    ////    dateValue = DateTime.Parse(dateString, new CultureInfo("fr-FR", false));
        //    ////    Console.WriteLine("'{0}' converted to {1}.", dateString, dateValue);
        //    ////}
        //    ////catch (FormatException)
        //    ////{
        //    ////    Console.WriteLine("Unable to convert '{0}'.", dateString);
        //    ////}

        //    //// Parse string with date but no time component.
        //    //dateString = "2/16/2008";
        //    //try
        //    //{
        //    //    dateValue = DateTime.Parse(dateString);
        //    //    Console.WriteLine("'{0}' converted to {1}.", dateString, dateValue);
        //    //}
        //    //catch (FormatException)
        //    //{
        //    //    Console.WriteLine("Unable to convert '{0}'.", dateString);
        //    //}


        //}

        //[Test]
        //public void getVORList()
        //{
        //    IVesselOperatorProxy acProxy = new VesselOperatorProxy();
        //    ResponseInfo info = null;
        //    ListVORParm parm = null;

        //    parm = new ListVORParm();
        //    parm.searchType = "info";
        //    parm.vslCallID = "08MBVS-AAAAB";
        //    info = acProxy.getVORList(parm);
        //    Console.WriteLine("======================================================");
        //    //for (int i = 0; i < info.list.Length; i++)
        //    //{
        //    //    if (info.list[i] is VORDryBreakBulkItem)
        //    //    {
        //    //        VORDryBreakBulkItem item = (VORDryBreakBulkItem)info.list[i];

        //    //        Console.Write("VORDryBreakBulkItem facNo : " + item.facNo);
        //    //        Console.Write(",  stevedore : " + item.stevedore);
        //    //        Console.WriteLine(", eqNo = " + item.eqNo);
        //    //    }
        //    //}
        //    Console.WriteLine("======================================================");
        //}

        //[Test]
        //public void showApronCheckerMain()
        //{
        //    // tnkytn: for test
        //    Framework.Common.UserInformation.UserInfo.getInstance().UserId = "MPTSHHTVO";
        //    Framework.Common.UserInformation.UserInfo.getInstance().Workdate = "06/10/2008";
        //    Framework.Common.UserInformation.UserInfo.getInstance().Shift = "SF0014";

        //    MOST.ApronChecker.HAC101 ac = new MOST.ApronChecker.HAC101();
        //    ac.ShowDialog();
        //}

        //[Test]
        //public void showVesselOperator()
        //{
        //    // tnkytn: for test
        //    Framework.Common.UserInformation.UserInfo.getInstance().UserId = "MPTSHHTVO";
        //    Framework.Common.UserInformation.UserInfo.getInstance().Workdate = "16/07/2008";
        //    Framework.Common.UserInformation.UserInfo.getInstance().Shift = "SF0014";

        //    MOST.VesselOperator.HVO101 vo = new MOST.VesselOperator.HVO101();
        //    vo.ShowDialog();
        //}

        ////[Test]
        ////public void showWarehouseChecker()
        ////{
        ////    // tnkytn: for test
        ////    Framework.Common.UserInformation.UserInfo.getInstance().UserId = "MPTSHHTVO";
        ////    Framework.Common.UserInformation.UserInfo.getInstance().Workdate = "25/08/2008";
        ////    Framework.Common.UserInformation.UserInfo.getInstance().Shift = "SF0012";

        ////    MOST.WHChecker.HWC101 wc = new MOST.WHChecker.HWC101();
        ////    wc.ShowDialog();
        ////}

        //[Test]
        //public void showPortSafetyMain()
        //{
        //    // tnkytn: for test
        //    Framework.Common.UserInformation.UserInfo.getInstance().UserId = "MPTSHHTVO";
        //    Framework.Common.UserInformation.UserInfo.getInstance().Workdate = "25/08/2008";
        //    Framework.Common.UserInformation.UserInfo.getInstance().Shift = "SF0012";

        //    MOST.PortSafety.HPS101 ac = new MOST.PortSafety.HPS101();
        //    ac.ShowDialog();
        //}

        //[Test]
        //public void showVSRCheckList_Driver_Contractor()
        //{
        //    MOST.ApronChecker.HAC103 ac = new MOST.ApronChecker.HAC103();
        //    ac.ShowDialog();
        //}

        //[Test]
        //public void showVesselDelay_Delaycode()
        //{
        //    MOST.VesselOperator.HVO107 ac = new MOST.VesselOperator.HVO107();
        //    ac.ShowDialog();
        //}

        //[Test]
        //public void showApronChecker_LoadingConfirm_Lorry()
        //{
        //    MOST.ApronChecker.HAC105 ac = new MOST.ApronChecker.HAC105();
        //    ac.ShowDialog();
        //}

        //[Test]
        //public void showApronChecker_DischargingConfirm()
        //{
        //    MOST.ApronChecker.HAC106 ac = new MOST.ApronChecker.HAC106();
        //    ac.ShowDialog();
        //}

        //[Test]
        //public void showPortSafety()
        //{
        //    HPS101 ac = new HPS101();
        //    ac.ShowDialog();
        //}
    }
}
