using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
using MOST.Common.CommonParm;
using MOST.Common.CommonResult;
using Framework.Service.Provider.WebService.Provider;

namespace MOST.VesselOperator.Parm
{
    public class HVO106001Parm : IPopupParm
    {
        private SearchJPVCResult jpvcInfo = null;
        private string hoseOnDt;
        private string hoseOffDt;
        private string commenceDt;
        private string completeDt;
        private string prevHoseOnDt;
        private string prevHoseOffDt;
        private string prevCommenceDt;
        private string prevCompleteDt;

        // Original value (hose time before being modified in Datetime screen)
        private string orgHoseOnDt;
        private string orgHoseOffDt;
        private string orgCommenceDt;
        private string orgCompleteDt;

        private string seq;

        private string vslShiftingSeq;

        public string VslShiftingSeq
        {
            get { return vslShiftingSeq; }
            set { vslShiftingSeq = value; }
        }

        public SearchJPVCResult JpvcInfo
        {
            get { return jpvcInfo; }
            set { jpvcInfo = value; }
        }

        public string HoseOnDt
        {
            get { return hoseOnDt; }
            set { hoseOnDt = value; }
        }

        public string HoseOffDt
        {
            get { return hoseOffDt; }
            set { hoseOffDt = value; }
        }

        public string CommenceDt
        {
            get { return commenceDt; }
            set { commenceDt = value; }
        }

        public string CompleteDt
        {
            get { return completeDt; }
            set { completeDt = value; }
        }

        public string PrevHoseOnDt
        {
            get { return prevHoseOnDt; }
            set { prevHoseOnDt = value; }
        }

        public string PrevHoseOffDt
        {
            get { return prevHoseOffDt; }
            set { prevHoseOffDt = value; }
        }

        public string PrevCommenceDt
        {
            get { return prevCommenceDt; }
            set { prevCommenceDt = value; }
        }

        public string PrevCompleteDt
        {
            get { return prevCompleteDt; }
            set { prevCompleteDt = value; }
        }

        /// <summary>
        /// </summary>
        public string OrgHoseOnDt
        {
            get
            {
                return this.orgHoseOnDt;
            }
            set
            {
                this.orgHoseOnDt = value;
            }
        }

        /// <summary>
        /// </summary>
        public string OrgHoseOffDt
        {
            get
            {
                return this.orgHoseOffDt;
            }
            set
            {
                this.orgHoseOffDt = value;
            }
        }

        /// <summary>
        /// </summary>
        public string OrgCommenceDt
        {
            get
            {
                return this.orgCommenceDt;
            }
            set
            {
                this.orgCommenceDt = value;
            }
        }

        /// <summary>
        /// </summary>
        public string OrgCompleteDt
        {
            get
            {
                return this.orgCompleteDt;
            }
            set
            {
                this.orgCompleteDt = value;
            }
        }

        /// <summary>
        /// </summary>
        public string Seq
        {
            get
            {
                return this.seq;
            }
            set
            {
                this.seq = value;
            }
        }
    }
}
