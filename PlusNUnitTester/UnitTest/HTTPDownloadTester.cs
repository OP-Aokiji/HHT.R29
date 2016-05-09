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
using NUnit.Framework;
using System.Net;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace PlusNUnitTester
{
    [TestFixture]
    public class HTTPDownloadTester : AssertionHelper
    {
        private const string RemoteURL = "http://localhost:9081/JPBi-MPTS-Web/";
        private const string RemoteURLPath = "PlusHHT/";

        [SetUp]
        protected void SetUp()
        {
                    
        }

        [Test]
        public void HTTPDownloadTest()
        {
            Console.WriteLine("HTTP File Read");
            string strXML = string.Empty;
            WebRequest wr = WebRequest.Create(RemoteURL + RemoteURLPath + "updatelist.xml");
            WebResponse ws = wr.GetResponse();
            try
            {
                Stream strm = ws.GetResponseStream();
                StreamReader sr = new StreamReader(strm);
                strXML = sr.ReadToEnd();
                Console.WriteLine(strXML);
                strm.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                strXML = string.Empty;
            }

            XmlDocument log = new XmlDocument();
            log.LoadXml(strXML);

            XmlElement logroot = log.DocumentElement;

            //logroot.SetAttribute("schemastamp", schemastamp);
            //logroot.SetAttribute("timestamp", timestamp);
            foreach (XmlNode update in logroot.ChildNodes)
            {
                Console.WriteLine("Package Name = " + update["packagename"].InnerXml);
                Console.WriteLine("version = " + update["version"].InnerXml);
                Console.WriteLine("Size = " + update["Size"].InnerXml);
                Console.WriteLine("remark = " + update["remark"].InnerXml);
            }
        }

        //[Test]
        //public void FileLoadTest()
        //{
        //   Console.WriteLine("Local File Read");
        //   if (File.Exists("updatelist.xml"))
        //   {
        //       System.Net.WebClient wc = new System.Net.WebClient();

        //       string strLocalVersion = string.Empty;

        //       Stream strm = wc.OpenRead(Application.StartupPath + @"\updatelist.xml");
        //       StreamReader sr = new StreamReader(strm);
        //       strLocalVersion = sr.ReadToEnd();
        //       Console.WriteLine(strLocalVersion);


        //       XmlDocument log = new XmlDocument();
        //       log.LoadXml(strLocalVersion);

        //       XmlElement logroot = log.DocumentElement;

        //       //logroot.SetAttribute("schemastamp", schemastamp);
        //       //logroot.SetAttribute("timestamp", timestamp);
        //       foreach (XmlNode update in logroot.ChildNodes)
        //       {
        //           Console.WriteLine("Package Name = " + update["packagename"].InnerXml);
        //           Console.WriteLine("version = " + update["version"].InnerXml);
        //           Console.WriteLine("Size = " + update["Size"].InnerXml);
        //           Console.WriteLine("remark = " + update["remark"].InnerXml);
        //       }
        //   }
        //   else
        //   {
        //       Console.WriteLine("Not Found");
        //   }
        //}
    }
}
