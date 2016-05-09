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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Collections;
using Framework.Updater.Document;
using Framework.Updater.Document.Utility;
using System.Threading;
using System.Xml;
using Framework.Updater.Observer;
using Framework.Updater.CommonUtility;
using Framework.Updater.Container;

namespace Framework.Updater
{
    public partial class frmPlusHttpUpdater : BaseForm
    {
        private HttpConnectionInfo updateInfo;
        private ArrayList updateFiles;
        private Hashtable updateVersion;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateInfo"></param>
        public frmPlusHttpUpdater(UpdateInformationItem updateInfo)
        {
            InitializeComponent();
            this.initialFormSize();
            this.updateInfo = (HttpConnectionInfo)updateInfo.ConnectionInfo;
            this.updateFiles = updateInfo.UpdateItemList;
            this.updateVersion = updateInfo.VersionUpdate;

            foreach (UpdateItem item in updateInfo.UpdateItemList)
            {
                lstUpdate.Items.Add(item.PackageName);
            }

            pbTotalTrans.Minimum = 0;
            pbTotalTrans.Maximum = updateInfo.UpdateItemList.Count;
            pbTotalTrans.Value = 0;

            this.Refresh();
        }

        private void getFiles()
        {
            #region Download Files 
            try
            {
                int iCount = 0;
                foreach (UpdateItem item in updateFiles)
                {
                    iCount++;
                    lbTotalTransFiles.Text = iCount + "/" + updateFiles.Count;
                    lbTotalTransFiles.Refresh();

                    HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(updateInfo.ServiceURL + item.PackageName);
                    HttpWebResponse webres = (HttpWebResponse)webreq.GetResponse();

                    BinaryWriter bw = new BinaryWriter(new FileStream(item.PackageName,
                                                                        FileMode.Create,
                                                                        FileAccess.Write,
                                                                        FileShare.None),
                                                                        Encoding.Default);

                    BinaryReader br = new BinaryReader(webres.GetResponseStream(), Encoding.Default);

                    long FileSize = webres.ContentLength;
                    long Stack = 0;

                    pbTrans.Minimum = 0;
                    pbTrans.Maximum = Convert.ToInt32(FileSize);
                    pbTrans.Value = 0;

                    lblTransFile.Text = item.PackageName;
                    lblTransFile.Refresh();

                    int Max = 1024;
                    byte[] Buffer = new byte[Max];
                    int BytesRead = 0;
                    while ((BytesRead = br.Read(Buffer, 0, Max)) > 0)
                    {
                        Stack += BytesRead;
                        pbTrans.Value = pbTrans.Value + BytesRead;
                        bw.Write(Buffer, 0, BytesRead);
                    }
                    pbTotalTrans.Value = pbTotalTrans.Value + 1;
                    bw.Close();
                }
                //It will be update a file of Local Version Control
                updateLocalVersion();
            }
            finally
            {
                Application.Exit();
            }
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        private void updateLocalVersion()
        {
            string versionFileName = StringUtil.GetCurrentDirectoryName(StringUtil.LOCAL_VERSION_CONTROL_FILE);
            XmlTextWriter tw = new XmlTextWriter(versionFileName, null);

            tw.Formatting = Formatting.Indented;
            tw.WriteStartDocument();

            tw.WriteStartElement("PLUS_HHT_Version");
            foreach (System.Collections.DictionaryEntry data in updateVersion)
            {
                UpdateItem item = (UpdateItem)data.Value;

                for (int i = 0; i < lstUpdate.Items.Count; i++)
                {
                    if (lstUpdate.Items[i].ToString() == item.PackageName)
                    {
                        lstUpdate.SelectedIndex = i;
                    }
                }
                
                tw.WriteStartElement("item");
                tw.WriteElementString("packagename", item.PackageName);
                tw.WriteElementString("version", item.Version);
                tw.WriteEndElement();
            }
            tw.WriteEndElement();
            tw.WriteEndDocument();

            tw.Flush();
            tw.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            btnCancel.Enabled = false;
            getFiles();
            //ThreadStart myThreadDelegate = new ThreadStart(getFiles);
            //myThread = new Thread(myThreadDelegate);
            //myThread.Start();
        }
    }
}