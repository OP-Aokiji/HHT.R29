using System;
using System.Collections.Generic;
using System.Text;

namespace MOST.Common.CommonResult
{
    public class VsrList
    {
        public String itemStatus;   
        public String status;
        public String role;
        public String staffId;
        public String staffName;
        public String wArea;
        public String startTime;
        public String endTime;
        public String sEQ;
        public String jPVC;
        public String operatorVsr;
        public String woNo;
        public String eqNo;
        public String capacity;
        public String nos;
        public String cgType;
        public String requestor;
        public String devlMode;
        public String eqArr;
        public String stevedore;
        public String supervisor;
        public String nonTonnage;
        public String purpose;
        public String purposeNm;
        public String apFp;

        /*
         * lv.dat fix issue 28358 , add ref no
         */
        private String refNo;

        public String RefNo
        {
            get { return refNo; }
            set { refNo = value; }
        }

        public String ApFp
        {
            get { return apFp; }
            set { apFp = value; }
        }

        public String PurposeNm
        {
            get { return purposeNm; }
            set { purposeNm = value; }
        }

        public String Purpose
        {
            get { return purpose; }
            set { purpose = value; }
        }

        public String NonTonnage
        {
            get { return nonTonnage; }
            set { nonTonnage = value; }
        }

        public String Supervisor
        {
            get { return supervisor; }
            set { supervisor = value; }
        }

        public String Stevedore
        {
            get { return stevedore; }
            set { stevedore = value; }
        }

        public String EqArr
        {
            get { return eqArr; }
            set { eqArr = value; }
        }

        public String DevlMode
        {
            get { return devlMode; }
            set { devlMode = value; }
        }

        public String Requestor
        {
            get { return requestor; }
            set { requestor = value; }
        }

        public String CgType
        {
            get { return cgType; }
            set { cgType = value; }
        }
        
        public String Nos
        {
            get { return nos; }
            set { nos = value; }
        }
        
        public String Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }

        public String EqNo
        {
            get { return eqNo; }
            set { eqNo = value; }
        }

        public String WoNo
        {
            get { return woNo; }
            set { woNo = value; }
        }

        public String ItemStatus
        {
            get { return itemStatus; }
            set { itemStatus = value; }
        }
        
        public String Status
        {
            get { return status; }
            set { status = value; }
        }

        public String Role
        {
            get { return role; }
            set { role = value; }
        }

        public String StaffId
        {
            get { return staffId; }
            set { staffId = value; }
        }

        public String StaffName
        {
            get { return staffName; }
            set { staffName = value; }
        }

        public String WArea
        {
            get { return wArea; }
            set { wArea = value; }
        }

        public String StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public String EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        public String SEQ
        {
            get { return sEQ; }
            set { sEQ = value; }
        }

        public String JPVC
        {
            get { return jPVC; }
            set { jPVC = value; }
        }

        public String OperatorVsr
        {
            get { return operatorVsr; }
            set { operatorVsr = value; }
        }
    }
}
