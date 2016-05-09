using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonParm
{
    public class UnclosedOperationParm : IPopupParm
    {
        private string vslCallId;
        //private string cgNo;
        //private string gpNo;

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

        //public string CgNo
        //{
        //    get { return cgNo; }
        //    set { cgNo = value; }
        //}

        //public string GpNo
        //{
        //    get { return gpNo; }
        //    set { gpNo = value; }
        //}
    }
}
