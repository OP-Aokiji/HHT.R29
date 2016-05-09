using System;
using System.Collections.Generic;
using System.Text;
using Framework.Service.Provider.WebService.Provider;
using System.Collections;

namespace Framework.Common.UserInformation
{
    public class UserInfo
    {
        private string userId;
        private string userName;
        private string userGroup;
        private bool authority;
        private string loginDateTime;
        private string shift;
        private string shiftNm;
        private string shiftIndex;
        private string workdate;
        private Hashtable AccessItem = new Hashtable();
        private Hashtable MenuItem = new Hashtable();

        //private string vslCallId;
        //private string vslNm;
        //private string vslTpCd;
        //private string vslTpNm;
        //private string purpCallCd;
        //private string purpCallNm;

        protected static UserInfo instance;

        public static UserInfo getInstance()
        {
            return instance;
        }

        static UserInfo()
        {
            instance = new UserInfo();
        }

        public void addAccessItem(AuthAccessItem item)
        {
            AccessItem.Add(item.pgmId,item);
        }
        public void clearAccessItem()
        {
            AccessItem = new Hashtable();
        }

        public void addMenuItem(AuthMenuItem item)
        {
            MenuItem.Add(item.pgmId, item);
        }
        public void clearMenuItem()
        {
            MenuItem = new Hashtable();
        }

        public Hashtable getAccessItem()
        {
            return AccessItem;
        }

        public AuthAccessItem getAccessItem(string key)
        {
            return (AuthAccessItem)AccessItem[key];
        }

        public AuthMenuItem getMenuItem(string key)
        {
            return (AuthMenuItem)MenuItem[key];
        }

        public Hashtable getMenuItem()
        {
            return MenuItem;
        }

        public string Shift
        {
            get { return shift; }
            set { shift = value; }
        }

        public string ShiftNm
        {
            get { return shiftNm; }
            set { shiftNm = value; }
        }

        public string ShiftIndex
        {
            get { return shiftIndex; }
            set { shiftIndex = value; }
        }

        public string Workdate
        {
            get { return workdate; }
            set { workdate = value; }
        }

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string UserGroup
        {
            get { return userGroup; }
            set { userGroup = value; }
        }

        public bool Authority
        {
            get { return authority; }
            set { authority = value; }
        }

        public string LoginDateTime
        {
            get { return loginDateTime; }
            set { loginDateTime = value; }
        }



        //public string VslCallId
        //{
        //    get { return vslCallId; }
        //    set { vslCallId = value; }
        //}

        //public string VslNm
        //{
        //    get { return vslNm; }
        //    set { vslNm = value; }
        //}

        //public string VslTpCd
        //{
        //    get { return vslTpCd; }
        //    set { vslTpCd = value; }
        //}

        //public string VslTpNm
        //{
        //    get { return vslTpNm; }
        //    set { vslTpNm = value; }
        //}

        //public string PurpCallCd
        //{
        //    get { return purpCallCd; }
        //    set { purpCallCd = value; }
        //}

        //public string PurpCallNm
        //{
        //    get { return purpCallNm; }
        //    set { purpCallNm = value; }
        //}
    }
}
