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

namespace Framework.Updater.Document.Utility
{
    public class HttpFileTrans : IFileTrans
    {
        private HttpConnectionInfo connInfo;

        public HttpFileTrans(IConnectionInfo connInfo)
        {
            this.connInfo = (HttpConnectionInfo)connInfo;
        }

        public string getDocument()
        {
            string strXML = string.Empty;
            try
            {
                WebRequest wr = WebRequest.Create(connInfo.ServiceURL + connInfo.FileName);
                WebResponse ws = wr.GetResponse();

                Stream strm = ws.GetResponseStream();
                StreamReader sr = new StreamReader(strm);
                strXML = sr.ReadToEnd();

                strm.Close();
            }
            catch (Exception ex)
            {
                throw new Framework.Common.Exception.HttpException(ex.Message, ex);
            }
            return strXML;
        }
    }
}
