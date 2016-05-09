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
using System.Collections;

namespace Framework.Updater.Document.Text
{
    public class TextDocument : ITextDocument
    {
        private string path = string.Empty;
        private string strTxt = string.Empty;

        public TextDocument(string path)
        {
            this.path = path;
        }

        public string readDocument()
        {
            return string.Empty;
        }

        public ArrayList documentParse()
        {
            return null;
        }

        public string setDocument
        {
            set
            {
                strTxt = value;
            }
        }

    }
}