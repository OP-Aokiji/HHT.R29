using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using MvcPatterns;

namespace MOST.CEClient
{
    static class HHTClient
    {
        private const int ERROR_ALREADY_EXISTS = 183;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            bool bCreated = false;

            string applicationName = "PLUS_MOST_HHT_MAIN";
            if (CreateMutex(IntPtr.Zero, true, applicationName) != 0)
            {
                if (GetLastError() == ERROR_ALREADY_EXISTS)
                {
                    bCreated = true;
                }
            }

            if (!bCreated)
            {
                ApplicationFacade facade = ApplicationFacade.getInstance();
                //facade.startup(new frmLogin());
                Application.Run(new frmLogin());
            }
        }

        [DllImport("CoreDll.dll")]
        private static extern int GetLastError();

        [DllImport("CoreDll.dll", EntryPoint = "CreateMutexW")]
        private static extern int CreateMutex(IntPtr lpMutexAttributes, bool InitialOwner, string MutexName);
    }
}