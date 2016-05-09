using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonParm
{
    public class RequesterListParm : IPopupParm
    {
        private string option;
        private string searchItem;

        /// <summary>
        /// </summary>
        public string Option
        {
            get
            {
                return this.option;
            }
            set
            {
                this.option = value;
            }
        }

        /// <summary>
        /// </summary>
        public string SearchItem
        {
            get
            {
                return this.searchItem;
            }
            set
            {
                this.searchItem = value;
            }
        }
    }
}
