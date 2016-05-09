using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.ApronChecker.Parm
{
    public class HAC101Parm : IPopupParm
    {
        private String jpvc;

        public String Jpvc
        {
            get { return jpvc; }
            set { jpvc = value; }
        }
    }
}
