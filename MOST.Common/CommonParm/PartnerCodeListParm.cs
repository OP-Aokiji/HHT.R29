using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonParm
{
    public class PartnerCodeListParm : IPopupParm
    {
        private string option;
        private string searchItem;
        private string ptnrCd;
        private string vslCallId;
        private string screenId;
        private string shippingNoteNo;
        private string doNo;
        private string lorryNo;

        //Added By Chris 2015-09-30 For WareHouse Checker
        private string grNo;
        private string blNo;
        //--------------------------------------------

        //Added By Chris 2015-09-30 For WareHouse Checker
        public string BlNo
        {
            get { return blNo; }
            set { blNo = value; }
        }

        public string GrNo
        {
            get { return grNo; }
            set { grNo = value; }
        }
        //--------------------------------------------

        //added by William (2015/07/21) Mantis issue: 49799
        private string _scr_Id;
        public string scr_Id
        {
            get { return _scr_Id; }
            set { _scr_Id = value; }
        }


        public string LorryNo
        {
            get { return lorryNo; }
            set { lorryNo = value; }
        }


        public string DoNo
        {
            get { return doNo; }
            set { doNo = value; }
        }

        public string Option
        {
            get { return option; }
            set { option = value; }
        }
        public string SearchItem
        {
            get { return searchItem; }
            set { searchItem = value; }
        }

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

        public string ScreenId
        {
            get { return screenId; }
            set { screenId = value; }
        }

        public string ShippingNoteNo
        {
            get { return shippingNoteNo; }
            set { shippingNoteNo = value; }
        }

        public string PtnrCd
        {
            get { return ptnrCd; }
            set { ptnrCd = value; }
        }
    }
}
