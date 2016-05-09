using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;

namespace MOST.ApronChecker.Parm
{
    public class HAC110Parm : IPopupParm
    {
        //private SearchJPVCResult jpvcInfo = null;
        private string vslCallId = null;
        private string cgNo = null;

        //public SearchJPVCResult JpvcInfo
        //{
        //    get { return jpvcInfo; }
        //    set { jpvcInfo = value; }
        //}
        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }
        public string CgNo
        {
            get { return cgNo; }
            set { cgNo = value; }
        }
    }
}
