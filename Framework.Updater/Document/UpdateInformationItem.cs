#region Explanation
//* --------------------------------------------------------------
//* CHANGE REVISION
//* --------------------------------------------------------------
//* DATE           AUTHOR      	   REVISION    	     Content
//* 2008-05-20   Mr Luis Lee	     1.0          First release.
//* --------------------------------------------------------------
//* CLASS DESCRIPTION
//* --------------------------------------------------------------
#endregion 

using System;
using System.Collections.Generic;
using System.Text;
using Framework.Updater.Document;
using System.Collections;
using Framework.Updater.Document.Utility;

namespace Framework.Updater.Document
{
    public class UpdateInformationItem
    {
        private IConnectionInfo connInfo;
        private string transferType = Constants.FILE_TRANS_FTP;
        private ArrayList updateItemList;
        private Hashtable versionUpdate;

        public string TransferType
        {
            get { return transferType; }
            set { transferType = value; }
        }

        public ArrayList UpdateItemList
        {
            get { return updateItemList; }
            set { updateItemList = value; }
        }

        public IConnectionInfo ConnectionInfo
        {
            get { return connInfo; }
            set { connInfo = value; }
        }

        public Hashtable VersionUpdate
        {
            get { return versionUpdate; }
            set { versionUpdate = value; }
        }
    }
}
