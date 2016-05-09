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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Xml;
using Framework.Updater.Document;
using Framework.Updater.Document.Utility;
using Framework.Updater.CommonUtility;

namespace Framework.Updater
{
	public class LibraryVersionCheck
	{
        private DocumentFactory updateList;
        private string configPath = StringUtil.LOCAL_CONFIG_FILE;
        private Hashtable localFileVersionMap;

		public LibraryVersionCheck()
		{
            updateList = new DocumentFactory(new ConfigDocument(configPath).getConnectInfo());
		}

		/// <summary>
		/// Confirm Update List from Web Server or Ftp Server
		/// </summary>
		/// <param name="strURL"></param>
		/// <returns></returns>
        public  UpdateInformationItem Check()
		{
            UpdateInformationItem updateItem = new UpdateInformationItem();
            string strXML = updateList.readDocument();

            if (strXML.Length == 0){
                return null;
            }
			
            ArrayList updateFiles = updateList.documentParse();

            if (updateFiles.Count == 0){
                return null;
            }

            ArrayList list = new ArrayList();
            foreach (UpdateItem item in updateFiles)
            {
                //if (versionCompareXP(item.Version, item.PackageName))
                //{
                //    list.Add(item);
                //}
                if (versionCompareWinCE(item.Version, item.PackageName))
                {
                    list.Add(item);
                }
            }

            //if (list.Count > 0)
            //{
            //    updateLocalVersion();
            //}

            updateItem.UpdateItemList = list;
            updateItem.TransferType = updateList.TransferType;
            updateItem.ConnectionInfo = updateList.getConnectionInfo;
            updateItem.VersionUpdate = localFileVersionMap;

            return updateItem;
		}
	
		/// <summary>
		/// It will be compare between current version and server version
        /// It was running in PC(Windows XP), but it didn't work in WinCE
		/// </summary>
		/// <param name="strVer"></param>
		/// <returns></returns>
		protected  bool versionCompareXP(string strVer, string strFilename)
		{
            Assembly asm = null;
            AssemblyName name = null;

            if (!File.Exists(StringUtil.GetCurrentDirectoryName(strFilename))){
                return true;
            }

            Version verUpdate = new Version(strVer);

            try{
                asm = Assembly.LoadFrom(StringUtil.GetCurrentDirectoryName(strFilename));
                name = asm.GetName();

                int nResult = verUpdate.CompareTo(name.Version);

                if (0 < nResult)
                    return true;
                else
                    return false;
            }catch{
                return true;
            }
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strVer"></param>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        protected bool versionCompareWinCE(string strVer, string strFileName)
        {
            //get Local file version from version.xml
            if (localFileVersionMap == null)
                getLocalVersion();
            
            Version verUpdate = new Version(strVer);

            if(localFileVersionMap.ContainsKey(strFileName)){
                if (!File.Exists(StringUtil.GetCurrentDirectoryName(strFileName)))
                {
                    return true;
                }
                UpdateItem item = (UpdateItem)localFileVersionMap[strFileName];
                if (item.PackageName.Equals(strFileName))
                {
                    Version localFileVersion = new Version(item.Version);
                    int nResult = verUpdate.CompareTo(localFileVersion);

                    if (0 < nResult)
                    {
                        item.Version = strVer;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                UpdateItem data = new UpdateItem();
                data.PackageName = strFileName;
                data.Version = strVer;
                localFileVersionMap.Add(strFileName, data);
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void getLocalVersion()
        {
            localFileVersionMap = new Hashtable();
            string filename = StringUtil.GetCurrentDirectoryName(StringUtil.LOCAL_VERSION_CONTROL_FILE);

            if (System.IO.File.Exists(filename))
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(filename);

                System.Xml.XmlElement logroot = doc.DocumentElement;

                UpdateItem item;
                foreach (System.Xml.XmlNode update in logroot.ChildNodes)
                {
                    item = new UpdateItem();
                    item.PackageName = update["packagename"].InnerXml;
                    item.Version = update["version"].InnerXml;
                    localFileVersionMap.Add(update["packagename"].InnerXml, item);
                }
            }
        }
	}
}
