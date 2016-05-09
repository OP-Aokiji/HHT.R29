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
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections;
using Framework.Updater.Document;
using Framework.Common.ExceptionHandler;
using Framework.Updater.CommonUtility;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using PlusHHTUpdater;
namespace Framework.Updater
{
	static class PlusUpdaterRunner
	{
        private const int ERROR_ALREADY_EXISTS = 183;
        private const string RUN_EXE = "PlusHHT.exe";

        [MTAThread]
		static void Main() 
		{
            bool bCreated = false;

            //string applicationName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string applicationName = "PLUS_MOST_HHT";
            if (CreateMutex(IntPtr.Zero, true, applicationName) != 0)
            {
                if (GetLastError() == ERROR_ALREADY_EXISTS)
                {
                    bCreated = true;
                }
            }

            if (!bCreated)
            {
                try
                {
                    frmPlusLogo logo = new frmPlusLogo();
                    logo.Show();
                    logo.Refresh();

                    logo.lbCheck.Text = "The PLUS HHT Updater is checking file version";
                    logo.lbCheck.Refresh();

                    logo.statusBar.Text = "connecting to the server....";
                    logo.statusBar.Refresh();

                    LibraryVersionCheck check = new LibraryVersionCheck();
                    UpdateInformationItem updateList = check.Check();

                    logo.statusBar.Text = "Library version checking....";
                    logo.statusBar.Refresh();

                    if (updateList != null && updateList.UpdateItemList.Count > 0)
                    {
                        logo.lbCheck.Text = "There are files which change";
                        logo.lbCheck.Refresh();

                        logo.statusBar.Text = "Library will be update....";
                        logo.statusBar.Refresh();

                        System.Threading.Thread.Sleep(1000);

                        logo.lbCheck.Text = "The PLUS HHT Updater will update the HHT";
                        logo.lbCheck.Refresh();

                        System.Threading.Thread.Sleep(1000);
                        logo.Close();

                        if (updateList.TransferType == Constants.FILE_TRANS_HTTP)
                        {
                            Application.Run(new frmPlusHttpUpdater(updateList));
                        }
                        else
                        {
                            Application.Run(new frmPlusFtpUpdater(updateList));
                        }
                    }

                    logo.Close();

                    if (!System.IO.File.Exists(StringUtil.GetCurrentDirectoryName(RUN_EXE)))
                    {
                        Application.Exit();
                        return;
                    }

                    System.Diagnostics.Process.Start(StringUtil.GetCurrentDirectoryName(RUN_EXE), null);
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    ExceptionHandler.ErrorHandler(ex);
                }
            }
	    }

        [DllImport("CoreDll.dll")]
        private static extern int GetLastError();

        [DllImport("CoreDll.dll", EntryPoint = "CreateMutexW")]
        private static extern int CreateMutex(IntPtr lpMutexAttributes, bool InitialOwner, string MutexName);
	}
}
