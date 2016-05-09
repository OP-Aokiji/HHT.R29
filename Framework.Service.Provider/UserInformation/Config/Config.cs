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
using System.Net;
using System.IO;
using System.Collections;
using Framework.Common;
using Framework.Common.Exception;

namespace Framework.Service.Provider.Config
{
    public class Config : IConfig
    {
        public Config() { }

        /// <summary>
        /// Document Parse
        /// </summary>
        /// <returns></returns>
        public string getServierProvider()
        {
            String servierProvider = string.Empty;
            String filename = StringUtil.GetCurrentDirectoryName(StringUtil.SERVICE_PROVIDER_CONFIG_FILE);

            if (System.IO.File.Exists(filename))
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(filename);

                System.Xml.XmlElement logroot = doc.DocumentElement;

                foreach (System.Xml.XmlNode update in logroot.ChildNodes)
                {
                    if (update.NodeType == System.Xml.XmlNodeType.Comment)
                        continue; 

                    if (update.Attributes["serverType"].InnerText.Equals("serviceProvider") && 
                        update.Attributes["active"].InnerText.Equals("yes"))
                    {
                        servierProvider = update["url"].InnerXml;
                    }
                }
            }
            else
            {
                throw new ServiceException("ERR_90000");
            }
            return servierProvider;
        }
    }
}
