using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;

namespace MOST.ApronChecker.Parm
{
    public class HAC111Parm : IPopupParm
    {   
        private string vslCallId;
        //private bool excludeRhdlLDFN;        // Dot not display items whose status are Rehandle Loading Final.

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }
        //public bool ExcludeRhdlLDFN
        //{
        //    get { return excludeRhdlLDFN; }
        //    set { excludeRhdlLDFN = value; }
        //}
    }
}
