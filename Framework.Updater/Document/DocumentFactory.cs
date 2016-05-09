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
using System.Collections;
using Framework.Updater.Document;
using Framework.Updater.Document.XML;
using Framework.Updater.Document.Utility;

namespace Framework.Updater.Document
{
    public class DocumentFactory
    {
        private IConnectionInfo connInfo;
        private IDocument document;
        private string strXML;
        private string transferType = Constants.FILE_TRANS_FTP;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connInfo"></param>
        public DocumentFactory(IConnectionInfo connInfo)
        {
            this.connInfo = connInfo;

            IFileTrans ft;
            if (connInfo is FtpConnectionInfo)
            {
                transferType = Constants.FILE_TRANS_FTP;
                ft = new FtpFileTrans(connInfo);
                strXML = ft.getDocument();
            }
            else
            {
                transferType = Constants.FILE_TRANS_HTTP;
                ft = new HttpFileTrans(connInfo);
                strXML = ft.getDocument();
            }

            IDocument doc = new XmlDocument();
            doc.setDocument = strXML;

            document = doc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string readDocument()
        {
            return document.readDocument();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ArrayList documentParse()
        {
            return document.documentParse();
        }

        /// <summary>
        /// 
        /// </summary>
        public string TransferType
        {
            get{return transferType;}
        }

        /// <summary>
        /// 
        /// </summary>
        public IConnectionInfo getConnectionInfo
        {
            get { return connInfo; }
        }
    }
}
