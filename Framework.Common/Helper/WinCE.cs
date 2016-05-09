//#define DESKTOP
// tnkytn: for testing on PC

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Framework.Common.Helper
{

    /// <summary>
    /// Contains managed wrappers or implementations of Win32 structs, delegates,
    /// constants and PInvokes that are useful for this sample.
    ///
    /// See the documentation on MSDN for more information on the elements provided
    /// in this file.
    /// </summary>
    public sealed class WinCE
    {
        public const int GWL_WNDPROC = -4;
        public const uint WM_CLOSE = 0x0010;


        ///////////////////////////////////// Make button text multiline ///////////////////////////////////
        public const int BS_MULTILINE = 0x00002000;
        public const int GWL_STYLE = -16;

        #if DESKTOP
            [DllImport("user32.dll")]
        #else
            [DllImport("coredll.dll")]
        #endif
        public extern static int GetWindowLong(IntPtr hWnd, int nIndex);

        #if DESKTOP
            [DllImport("user32.dll")]
        #else
            [DllImport("coredll.dll")]
        #endif
        public extern static int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        ///////////////////////////////////// Make button text multiline ///////////////////////////////////





        /// <summary>
        /// A callback to a Win32 window procedure (wndproc)
        /// </summary>
        /// <param name="hwnd">The handle of the window receiving a message</param>
        /// <param name="msg">The message</param>
        /// <param name="wParam">The message's parameters (part 1)</param>
        /// <param name="lParam">The message's parameters (part 2)</param>
        /// <returns>A integer as described for the given message in MSDN</returns>
        public delegate int WndProc(IntPtr hwnd, uint msg, uint wParam, int lParam);

        #if DESKTOP
            [DllImport("user32.dll")]
        #else
            [DllImport("coredll.dll")]
        #endif
        public extern static int DefWindowProc(
                IntPtr hwnd, uint msg, uint wParam, int lParam);

        #if DESKTOP
            [DllImport("user32.dll")]
        #else
            [DllImport("coredll.dll")]
        #endif
        public extern static IntPtr SetWindowLong(
                IntPtr hwnd, int nIndex, IntPtr dwNewLong);

        #if DESKTOP
            [DllImport("user32.dll")]
        #else
            [DllImport("coredll.dll")]
        #endif
        public extern static int CallWindowProc(
                IntPtr lpPrevWndFunc, IntPtr hwnd, uint msg, uint wParam, int lParam);
    }
}
