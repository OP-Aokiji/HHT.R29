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
using System.IO;
using System.Reflection;

namespace Framework.Updater.CommonUtility
{
    public class StringUtil
    {
        public static string LOCAL_CONFIG_FILE = "config.xml";
        public static string LOCAL_VERSION_CONTROL_FILE = "version.xml";
        public static string UPDATE_VERSION_CONTORL_FILE_FILE = "updatelist.xml";
        
        public static string GetCurrentDirectoryName(string fileName)
        {
            string currentPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase), fileName);
            if (System.Text.RegularExpressions.Regex.IsMatch(currentPath, "file:"))
            {
                currentPath = currentPath.Substring(6, currentPath.Length - 6);
            }
            return currentPath;
        }
    }
}
