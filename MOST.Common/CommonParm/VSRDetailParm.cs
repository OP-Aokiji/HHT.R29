using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonResult;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.Common.CommonParm
{
    public class VSRDetailParm : IPopupParm
    {
        private string vslCallId = null;
        private List<CheckListVSRItem> arrGrdDataMP = null;
        private List<CheckListVSRItem> arrGrdDataPC = null;
        private List<CheckListVSRItem> arrGrdDataST = null;
        private List<CheckListVSRItem> arrGrdDataFL = null;
        private List<CheckListVSRItem> arrGrdDataTR = null;
        private List<CheckListVSRItem> arrGrdDataEQ = null;
        private List<CheckListVSRItem> arrUpdGrdData = null;
        private int currIndex;

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

        public List<CheckListVSRItem> ArrGrdDataMP
        {
            get { return arrGrdDataMP; }
            set { arrGrdDataMP = value; }
        }
        
        public List<CheckListVSRItem> ArrGrdDataPC
        {
            get { return arrGrdDataPC; }
            set { arrGrdDataPC = value; }
        }

        public List<CheckListVSRItem> ArrGrdDataST
        {
            get { return arrGrdDataST; }
            set { arrGrdDataST = value; }
        }
        
        public List<CheckListVSRItem> ArrGrdDataFL
        {
            get { return arrGrdDataFL; }
            set { arrGrdDataFL = value; }
        }
        
        public List<CheckListVSRItem> ArrGrdDataTR
        {
            get { return arrGrdDataTR; }
            set { arrGrdDataTR = value; }
        }
        
        public List<CheckListVSRItem> ArrGrdDataEQ
        {
            get { return arrGrdDataEQ; }
            set { arrGrdDataEQ = value; }
        }
        
        public List<CheckListVSRItem> ArrUpdGrdData
        {
            get { return arrUpdGrdData; }
            set { arrUpdGrdData = value; }
        }

        public int CurrIndex
        {
            get { return currIndex; }
            set { currIndex = value; }
        }
    }
}
