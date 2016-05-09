using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;

namespace MOST.VesselOperator.Parm
{
    public class HVO111Parm : IPopupParm
    {
        private SearchJPVCResult jpvcInfo = null;
        private ArrayList arrGridData = null;
        private int currIndex = 0;

        public SearchJPVCResult JpvcInfo
        {
            get { return jpvcInfo; }
            set { jpvcInfo = value; }
        }

        public ArrayList ArrGridData
        {
            get { return arrGridData; }
            set { arrGridData = value; }
        }

        public int CurrIndex
        {
            get { return currIndex; }
            set { currIndex = value; }
        }
    }
}
