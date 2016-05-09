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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Framework.Common.Utility;
using Framework.Updater.CommonUtility;
using Framework.Updater.Container;
using Framework.Updater.Document;
using Framework.Updater.Document.Utility;
using Framework.Updater.Observer;

namespace Framework.Updater
{
    public partial class frmPlusFtpUpdater : BaseForm, IObserver
    {
        private FtpConnectionInfo updateInfo;
        private ArrayList updateFiles;
        private FtpClient ftpClient;
        private Hashtable updateVersion;
        private Hashtable librarySize;
        private long nTotalDownloadSize;
        private long nDownloadSize;
        private Thread _updateThread;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateInfo"></param>
        public frmPlusFtpUpdater(UpdateInformationItem updateInfo)
        {
            InitializeComponent();
            this.initialFormSize();
            this.updateInfo = (FtpConnectionInfo)updateInfo.ConnectionInfo;
            this.updateFiles = updateInfo.UpdateItemList;
            this.updateVersion = updateInfo.VersionUpdate;

            foreach (UpdateItem item in updateInfo.UpdateItemList)
            {
                lstUpdate.Items.Add(item.PackageName);
            }

            pbTotalTrans.Value = 0;
            pbTrans.Value = 0;

            lbTotalTransFiles.Text = "0/" + updateFiles.Count;

            //lbTotalTransFiles.Refresh();

            //this.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void sendNotify(long message)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<long>(this.sendNotify), message);
            else
            {
                if (pbTrans.Maximum < message)
                {
                    pbTrans.Value = pbTrans.Maximum;
                }
                else
                {
                    pbTrans.Value = (int)message;
                }

                nDownloadSize = pbTrans.Value;

                if (pbTotalTrans.Maximum < nDownloadSize + nTotalDownloadSize)
                {
                    pbTotalTrans.Value = pbTotalTrans.Maximum;
                }
                else
                {
                    pbTotalTrans.Value = (int)(nDownloadSize + nTotalDownloadSize);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            lbDownloadFile.Text = String.Empty;
            btnUpdate.Enabled = false;
            btnCancel.Text = "Cancel";

            _updateThread = new Thread(new ThreadStart(UpdateLocalFiles));
            _updateThread.Name = "HHT_Update_Thread";
            _updateThread.Start();
        }

        private void UpdateLocalFiles()
        {
            try
            {
                SetMessageText("Connecting... ");
                // FTP Login
                if (ftpClient != null)
                    ftpClient.Close();

                ftpClient = new FtpClient(updateInfo.ServerIP, updateInfo.UserID, updateInfo.Password);
#if DEBUG
                ftpClient.VerboseDebugging = true;
#endif
                ftpClient.registerInterest(this);
                ftpClient.Login();
                ftpClient.BinaryMode = true;

                if (updateInfo.RemoteDir.Length > 0)
                {
                    ftpClient.ChangeDir(updateInfo.RemoteDir);
                }

                long nTotalSize = 0;
                long nFileSize;
                librarySize = new Hashtable();
                foreach (UpdateItem item in updateFiles)
                {
                    nFileSize = 0;
                    nFileSize = ftpClient.GetFileSize(item.PackageName);

                    //This hashtable will use for check a size of file
                    librarySize.Add(item.PackageName, nFileSize);

                    nTotalSize += nFileSize;

                    //System.Console.WriteLine("PackageName=" + item.PackageName + " nFileSize=" + nFileSize + " nTotalSize=" + nTotalSize);
                }
                pbTotalTrans.Maximum = (int)nTotalSize;

                SetMessageText("Downloading");
                string downloadPath = string.Empty;
                int iCount = 1;
                foreach (UpdateItem item in updateFiles)
                {
                    ProcessUpdateItem(iCount, item);
                    downloadPath = StringUtil.GetCurrentDirectoryName(item.PackageName);

                    StartDownloadUpdateItem(iCount, item);
                    ftpClient.Download(item.PackageName, downloadPath, true);

                    //Checking local file when it finished download
                    VerifyDownloadedUpdateItem(iCount, item);
                    if (librarySize.ContainsKey(item.PackageName))
                    {
                        if (!File.Exists(StringUtil.GetCurrentDirectoryName(item.PackageName)))
                        {
                            DownloadedUpdateItemError(iCount, item, "Error");
                            //throw new Framework.Common.Exception.FtpException("Download Fail");
                        }
                        else
                        {
                            FileInfo info = new FileInfo(StringUtil.GetCurrentDirectoryName(item.PackageName));
                            if (info.Exists)
                            {
                                if (info.Length != (long)librarySize[item.PackageName])
                                {
                                    DownloadedUpdateItemError(iCount, item, "Failed");
                                    //throw new Framework.Common.Exception.FtpException("The download failed with network problem.");
                                }
                                else
                                    FinishDownloadingUpdateItem(iCount, item, info.Length);
                            }

                        }
                    }

                    nTotalDownloadSize += pbTrans.Maximum;
                    iCount++;
                }

                //System.Console.WriteLine("Before updateLocalVersion");

                //It will be update a file of Local Version Control
                updateLocalVersion();

                UpdateLocalFilesCompleted();
            }
            catch (ThreadAbortException)
            {
                UpdateLocalFilesAborted();
            }
            catch (Exception ex)
            {
                UpdateLocalFilesError(ex);
            }
            finally
            {
                if (ftpClient != null)
                {
                    ftpClient.Close();
                    ftpClient = null;
                }
                _updateThread = null;
            }
        }

        private void SetMessageText(string message)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<string>(SetMessageText), message);
            else
                tLabel3.Text = message;
        }

        private void UpdateLocalFilesAborted()
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(UpdateLocalFilesAborted));
            else
            {
                pbTotalTrans.Value = 0;
                pbTrans.Value = 0;
                lbDownloadFile.Text = String.Empty;

                tLabel3.Text = "Cancelled.";
                btnUpdate.Enabled = true;
                btnCancel.Text = "Close";
                MessageBox.Show("Update has been cancelled.", "HHT Update", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation, 
                    MessageBoxDefaultButton.Button1);
            }
        }

        private void UpdateLocalFilesCompleted()
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(UpdateLocalFilesCompleted));
            else
            {
                tLabel3.Text = "Completed.";
                btnCancel.Text = "Close";
                MessageBox.Show("Update completed successfully.", "HHT Update",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk,
                    MessageBoxDefaultButton.Button1);
            }
        }

        private void UpdateLocalFilesError(Exception error)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<Exception>(UpdateLocalFilesError), error);
            else
            {
                pbTotalTrans.Value = 0;
                pbTrans.Value = 0;

                tLabel3.Text = "Error.";
                btnUpdate.Enabled = true;
                btnCancel.Text = "Close";
                MessageBox.Show(
                    "An error has occurred during the update progress. \n" + error, 
                    "HHT Update",
                    MessageBoxButtons.OK, MessageBoxIcon.Hand,
                    MessageBoxDefaultButton.Button1
                );
            }
        }

        private void FinishDownloadingUpdateItem(int itemNumber, UpdateItem item, long fileSize)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<int, UpdateItem, long>(FinishDownloadingUpdateItem), itemNumber, item, fileSize);
            else
                lstUpdate.Items[itemNumber - 1] = String.Format(item.PackageName + " [Completed: {0:N0}B]", fileSize);
        }

        private void DownloadedUpdateItemError(int itemNumber, UpdateItem item, string message)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<int, UpdateItem, string>(DownloadedUpdateItemError), itemNumber, item, message);
            else
                lstUpdate.Items[itemNumber - 1] = String.Format(item.PackageName + " [{0}] ", message);
        }

        private void VerifyDownloadedUpdateItem(int itemNumber, UpdateItem item)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<int, UpdateItem>(VerifyDownloadedUpdateItem), itemNumber, item);
            else
                lstUpdate.Items[itemNumber - 1] = "Verifying " + item.PackageName + "... ";
        }

        private void StartDownloadUpdateItem(int itemNumber, UpdateItem item)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<int, UpdateItem>(StartDownloadUpdateItem), itemNumber, item);
            else
                lstUpdate.Items[itemNumber - 1] = "Downloading " + item.PackageName + "... ";
        }

        private void ProcessUpdateItem(int itemNumber, UpdateItem item)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<int, UpdateItem>(ProcessUpdateItem), itemNumber, item);
            else
            {
                lbTotalTransFiles.Text = itemNumber + "/" + updateFiles.Count;
                lbDownloadFile.Text = item.PackageName;
                lstUpdate.SelectedIndex = itemNumber - 1;
                lstUpdate.Items[itemNumber - 1] = "Processing " + item.PackageName + "... ";
                pbTrans.Maximum = (int)ftpClient.GetFileSize(item.PackageName);
                pbTrans.Value = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void updateLocalVersion()
        {
            string versionFileName = StringUtil.GetCurrentDirectoryName(StringUtil.LOCAL_VERSION_CONTROL_FILE);
            XmlTextWriter tw = new XmlTextWriter(versionFileName, null);
            try
            {
                tw.Formatting = Formatting.Indented;
                tw.WriteStartDocument();
                tw.WriteStartElement("PLUS_HHT_Version");
                foreach (System.Collections.DictionaryEntry data in updateVersion)
                {
                    UpdateItem item = (UpdateItem)data.Value;
                    tw.WriteStartElement("item");
                    tw.WriteElementString("packagename", item.PackageName);
                    tw.WriteElementString("version", item.Version);
                    tw.WriteEndElement();
                }
                tw.WriteEndElement();
                tw.WriteEndDocument();
                tw.Flush();
            }
            finally
            {
                tw.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_updateThread != null)
            {
                _updateThread.Abort();
                _updateThread = null;
            }
            else
                this.Close();
        }
    }

    internal delegate void Action();
    internal delegate void Action<T1, T2>(T1 p1, T2 p2);
    internal delegate void Action<T1, T2, T3>(T1 p1, T2 p2, T3 p3);
}