using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonParm
{
    public class WorkingAreaParm : IPopupParm
    {
        private string workingArea;
        private string workingAreaType;

        /// <summary>
        /// </summary>
        public string WorkingArea
        {
            get
            {
                return this.workingArea;
            }
            set
            {
                this.workingArea = value;
            }
        }

        /// <summary>
        /// </summary>
        public string WorkingAreaType
        {
            get
            {
                return this.workingAreaType;
            }
            set
            {
                this.workingAreaType = value;
            }
        }
    }
}
