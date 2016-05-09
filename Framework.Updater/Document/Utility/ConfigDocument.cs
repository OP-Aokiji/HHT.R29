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
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using Framework.Common.Exception;

namespace Framework.Updater.Document.Utility
{
    public class ConfigDocument
    {
        private string configFile;

        public ConfigDocument(string configFile) {
            this.configFile = configFile;
        }

        public IConnectionInfo getConnectInfo()
        {
            try
            {
                IConnectionInfo connInfo = null;
                string filename = Framework.Updater.CommonUtility.StringUtil.GetCurrentDirectoryName(configFile);

                if (System.IO.File.Exists(filename))
                {
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.Load(filename);

                    System.Xml.XmlElement logroot = doc.DocumentElement;

                    foreach (System.Xml.XmlNode update in logroot.ChildNodes)
                    {
                        if (update.Attributes["serverType"].InnerText.Equals("FTP") && update.Attributes["active"].InnerText.Equals("yes"))
                        {
                            FtpConnectionInfo info = new FtpConnectionInfo();
                            info.ServerIP = update["serverIP"].InnerXml;
                            info.ServerPort = update["serverPort"].InnerXml;
                            info.UserID = update["userID"].InnerXml;
                            info.Password = update["password"].InnerXml;
                            info.ConnectionMode = update["connectionMode"].InnerXml;
                            info.FileType = update["fileType"].InnerXml;
                            info.Filename = update["filename"].InnerXml;
                            info.RemoteDir = update["remoteDir"].InnerXml;
                            connInfo = info;
                        }

                        if (update.Attributes["serverType"].InnerText.Equals("HTTP") && update.Attributes["active"].InnerText.Equals("yes"))
                        {
                            HttpConnectionInfo info = new HttpConnectionInfo();
                            info.ServiceURL = update["url"].InnerXml;
                            info.FileType = update["fileType"].InnerXml;
                            info.FileName = update["filename"].InnerXml;
                            connInfo = info;
                        }
                    }
                }
                else
                {
                    throw new ServiceException("Config File not found");
                }
                return connInfo;
            }
            catch (Exception  ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
            
        }
    }
}
