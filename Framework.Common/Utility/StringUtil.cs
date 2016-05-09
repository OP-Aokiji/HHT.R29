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

namespace Framework.Common
{
    public class StringUtil
    {
        public static string SERVICE_PROVIDER_CONFIG_FILE = "Config.xml";
       
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
