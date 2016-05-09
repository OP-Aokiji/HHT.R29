using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Framework.Common.Constants
{
    public class Constants
    {
        public const string STRING_NULL = "";
        public const string STRING_VALUE_NULL = "Null";
        public const string STRING_SPACE = " ";
        public const string STRING_TRUE = "true";
        public const string STRING_C = "C";
        public const string STRING_LEFT_SQUARE_BRACKETS = "[";
        public const string STRING_RIGHT_SQUARE_BRACKETS = "]";
        public const string STRING_SELECT = "Select";
        public const string STRING_WIFI = "WiFi";
        public const string STRING_WIRELESS = "Wireless";
        public const string STRING_KBPS = "kbps";
        
        public static Color DefaultColor = Color.LightBlue;
        public static Color ErrorColor = Color.Red;

        //public const int SCREEN_WIDTH  = 240;
        //public const int SCREEN_HEIGHT = 294;

        public const string MPTS_VESSEL_OPERATION = "MPHVO000";
        public const string MPTS_WAREHOUSE_CHECKER = "MPHWC000";
        public const string MPTS_PORT_SAFETY = "MPHPS000";
        public const string MPTS_APRON_CHECKER = "MPHAC000";

        public const string CMB_VESSEL_OPERATION = "Vessel Supervisor";
        public const string CMB_WAREHOUSE_CHECKER = "W/H Checker";
        public const string CMB_PORT_SAFETY = "Port Safety";
        public const string CMB_APRON_CHECKER = "Apron Checker";

        // WorkingStatus
        public const string WS_INITIAL  = "0";
        public const string WS_QUERY    = "R";
        public const string WS_INSERT   = "C";
        public const string WS_UPDATE   = "U";
        public const string WS_DELETE   = "D";

        //Transfer Mode - William

        public const string TSM_UPDATE = "TRANSFER_MODE_UPDATE";
        public const string TSM_INSERT = "TRANSFER_MODE_INSERT";

        // WorkingStatus Name
        public const string WS_NM_INITIAL   = "";
        public const string WS_NM_QUERY     = "Sel";
        public const string WS_NM_INSERT    = "New";
        public const string WS_NM_UPDATE    = "Upd";
        public const string WS_NM_DELETE    = "Del";

        // VSR Confirmed , Not Confirmed Status
        public const string ITEM_CONFIRMED = "Confirmed";
        public const string ITEM_NOTCONFIRMED = "Not Confirmed";
        public const string ITEM_MEGA = "MEGA";

        // Item Status
        public const string ITEM_OLD = "O";
        public const string ITEM_NEW = "N";

        // Mode of operation
        public const int MODE_ADD = 1;
        public const int MODE_UPDATE = 2;
        public const int MODE_DELETE = 3;

        // Non-JPVC
        public const string NONCALLID = "NonCallId";

        // JPVC type
        public const int JPVC = 1;
        public const int NONJPVC = 2;

        // Clearance
        public const string CLEARANCE_HOLD = "Hold";
        public const string CLEARANCE_RELEASE = "Release";
        public const string CLEARANCE_INSPECTION = "Inspection";

        // Mode of operation
            //TSPTTP	CV	Conveyor	
            //TSPTTP	LR	Lorry	
            //TSPTTP	WG	Wagon	
        public const string OPRMODE_CONVEYOR = "CV";
        public const string OPRMODE_LORRY = "LR";
        public const string OPRMODE_WAGON = "WG";

        // Final Operation Mode
        public const string FINAL_MODE_LDFN     = "HHT_LDFN";       // Loading final
        public const string FINAL_MODE_DSFN     = "HHT_DSFN";       // Discharging final
        public const string FINAL_MODE_HIFN     = "HHT_HIFN";       // Handle in final
        public const string FINAL_MODE_HOFN     = "HHT_HOFN";       // Handle in final
        public const string FINAL_MODE_RHLDFN   = "HHT_RHLDFN";     // Re-handle loading final
        public const string FINAL_MODE_RHHOFN   = "HHT_RHHOFN";     // Re-handle handle out final

        // Vessel type
        public const string VSLTYPECD_LIQUID = "01";

        // QuanBTL Fix issue 33939 16-07-2012 START

        // Cargo statNm
        public const string CARGO_STATNM_LOADED = "Loaded";

        //Delivery mode
        public const string DELIVERY_MODE_INDIRECT = "I";
        public const string DELIVERY_MODE_DIRECT = "D";
        public const string STRING_DELIVERY_MODE_INDIRECT = "Indirect";
        public const string STRING_DELIVERY_MODE_DIRECT = "Direct";

        //Cargo type
        public const string STRING_BBK = "BBK";
        public const string STRING_DBE = "DBE";
        public const string STRING_DBN = "DBN";

        //Item info
        public const string ITEM_REQTPCD_SPC = "SPC";
        
        //Item search Type
        public const string VIEW_PLAN_INFO = "viewPlanInfo";

        // QuanBTL Fix issue 33939 16-07-2012 END

        // HHT Controller flag
        // QuanBTL D/C screen performance START
        public const string LOAD_CODE_MATER_FLAG = "LOADCDMSTER";
        public const string LOAD_CARGO_FLAG = "LOADCARGO";
        // QuanBTL D/C screen performance END
        
        // lv.dat add paging number item per page 20130611
        public static int iNumbPerPage = 5;
    }
}
