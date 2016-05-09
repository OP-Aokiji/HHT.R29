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

namespace Framework.Updater.Document.Utility
{
    public class HttpConnectionInfo : IConnectionInfo
    {
        private string serviceURL;
        private string fileType="XML";
        private String fileName = "updatelist.xml";

        public string ServiceURL{
            get { return serviceURL; }
            set { serviceURL = value; }
        }

        public string FileType
        {
            get { return fileType; }
            set { fileType = value; }
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
    }
}
