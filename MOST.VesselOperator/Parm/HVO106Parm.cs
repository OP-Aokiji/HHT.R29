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
    public class HVO106Parm : IPopupParm
    {
        private SearchJPVCResult jpvcInfo = null;
        private VORLiquidBulkItem liquidBulkItem = null;    // Current selected item (for Update process)
        private ArrayList gridArrayList = null;             // Grid Data
        private string loadPlanMt;      // Document Loading MT
        private string disPlanMt;       // Document Discharging MT
        private string loadActualMt;    // Handled Loading MT
        private string disActualMt;     // Handled Discharging MT
        private string loadBalMt;       // Loading Balance
        private string disBalMt;        // Discharging Balance
        private string vslShiftingSeq;

        public string VslShiftingSeq
        {
            get { return vslShiftingSeq; }
            set { vslShiftingSeq = value; }
        }

        //// Get the original hose time (from database)
        //private string orgHoseOnDt;
        //private string orgCommenceDt;
        //private string orgCompleteDt;
        //private string orgHoseOffDt;

        public SearchJPVCResult JpvcInfo
        {
            get { return jpvcInfo; }
            set { jpvcInfo = value; }
        }

        public VORLiquidBulkItem LiquidBulkItem
        {
            get { return liquidBulkItem; }
            set { liquidBulkItem = value; }
        }

        public ArrayList GridArrayList
        {
            get { return gridArrayList; }
            set { gridArrayList = value; }
        }

        public string LoadPlanMt
        {
            get { return loadPlanMt; }
            set { loadPlanMt = value; }
        }

        public string DisPlanMt
        {
            get { return disPlanMt; }
            set { disPlanMt = value; }
        }

        public string LoadActualMt
        {
            get { return loadActualMt; }
            set { loadActualMt = value; }
        }

        public string DisActualMt
        {
            get { return disActualMt; }
            set { disActualMt = value; }
        }

        public string LoadBalMt
        {
            get { return loadBalMt; }
            set { loadBalMt = value; }
        }

        public string DisBalMt
        {
            get { return disBalMt; }
            set { disBalMt = value; }
        }

        ///// <summary>
        ///// </summary>
        //public string OrgHoseOnDt
        //{
        //    get
        //    {
        //        return this.orgHoseOnDt;
        //    }
        //    set
        //    {
        //        this.orgHoseOnDt = value;
        //    }
        //}

        ///// <summary>
        ///// </summary>
        //public string OrgCommenceDt
        //{
        //    get
        //    {
        //        return this.orgCommenceDt;
        //    }
        //    set
        //    {
        //        this.orgCommenceDt = value;
        //    }
        //}

        ///// <summary>
        ///// </summary>
        //public string OrgCompleteDt
        //{
        //    get
        //    {
        //        return this.orgCompleteDt;
        //    }
        //    set
        //    {
        //        this.orgCompleteDt = value;
        //    }
        //}

        ///// <summary>
        ///// </summary>
        //public string OrgHoseOffDt
        //{
        //    get
        //    {
        //        return this.orgHoseOffDt;
        //    }
        //    set
        //    {
        //        this.orgHoseOffDt = value;
        //    }
        //}
    }
}
