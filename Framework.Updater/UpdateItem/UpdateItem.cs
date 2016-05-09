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

namespace Framework.Updater
{
    public class UpdateItem
    {
        private string packageName;
        private string version;
        private string size;
        private string remark;

        public string PackageName
        {
            get { return packageName; }
            set { packageName = value; }
        }

        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        public string Size
        {
            get { return size; }
            set { size = value; }
        }

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}