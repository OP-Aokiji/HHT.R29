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

namespace Framework.Updater.Document.XML
{
    public class XmlDocument : IXmlDocument
    {
        private string serviceURL = string.Empty;
        private string strXML = string.Empty;

        /// <summary>
        /// Return XML
        /// </summary>
        public string isXML
        {
            get
            {
                return strXML;
            }

            set
            {
                strXML = value;
            }
        }
        public string setDocument
        {
            set
            {
                strXML = value;
            }
        }

        public XmlDocument(string URL)
        {
            this.serviceURL = URL;
        }

        public XmlDocument() { }

        /// <summary>
        /// Read Xml From Remote Server
        /// </summary>
        /// <returns></returns>
        public string readDocument()
        {
            return strXML;
        }

        /// <summary>
        /// Document Parse
        /// </summary>
        /// <returns></returns>
        public ArrayList documentParse()
        {
            ArrayList updateList = new ArrayList();

            if (strXML.Length > 0)
            {
                System.Xml.XmlDocument log = new System.Xml.XmlDocument();
                log.LoadXml(strXML);

                System.Xml.XmlElement logroot = log.DocumentElement;

                UpdateItem item;
                foreach (System.Xml.XmlNode update in logroot.ChildNodes)
                {
                    item = new UpdateItem();
                    item.PackageName = update["packagename"].InnerXml;
                    item.Version = update["version"].InnerXml;
                    item.Size =update["Size"].InnerXml;
                    item.Remark = update["remark"].InnerXml;
                    updateList.Add(item);
                }
            }
            return updateList;
        }
    }
}
