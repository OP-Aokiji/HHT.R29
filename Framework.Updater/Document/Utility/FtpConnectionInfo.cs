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
    public class FtpConnectionInfo : IConnectionInfo
    {
        private string serverIP;
		private string serverPort = "21";
		private string userID = "";
		private string password = "";
		private string connectionMode = "passive";
		private string fileType = "XML";
        private string filename = "updatelist.xml";
		private string remoteDir = "";

        public string ServerIP
        {
            get { return serverIP; }
            set { serverIP = value; }
        }

        public string ServerPort
        {
            get { return serverPort; }
            set { serverPort = value; }
        }

        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string ConnectionMode
        {
            get { return connectionMode; }
            set { connectionMode = value; }
        }

        public string FileType
        {
            get { return fileType; }
            set { fileType = value; }
        }

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        public string RemoteDir
        {
            get { return remoteDir; }
            set { remoteDir = value; }
        }
    }
}
