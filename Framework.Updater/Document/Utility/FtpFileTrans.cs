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
using Framework.Common.Utility;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Framework.Updater.CommonUtility;

namespace Framework.Updater.Document.Utility
{
    public class FtpFileTrans : IFileTrans
    {
        private FtpConnectionInfo connInfo;
        private FtpClient   ftpClient;

        public FtpFileTrans(IConnectionInfo connInfo) {
            this.connInfo = (FtpConnectionInfo)connInfo;
        }

        public string getDocument(){

            try
            {
                ftpClient = new FtpClient(connInfo.ServerIP, connInfo.UserID, connInfo.Password);
                ftpClient.Login();

                if (connInfo.RemoteDir.Length > 0)
                {
                    ftpClient.ChangeDir(connInfo.RemoteDir);
                }

                string downloadPath = Framework.Updater.CommonUtility.StringUtil.GetCurrentDirectoryName("updatelist.xml");

                ftpClient.Download("updatelist.xml", downloadPath, true);

                if (ftpClient != null)
                    ftpClient.Close();
                ftpClient = null;

                if (System.IO.File.Exists(downloadPath))
                {
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.Load(downloadPath);
                    return doc.OuterXml;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Framework.Common.Exception.FtpException ftpEx)
            {
                throw ftpEx;
            }
            catch (Exception ex)
            {
                throw new Framework.Common.Exception.ServiceException(ex.Message, ex);
            }
        }
    }
}
