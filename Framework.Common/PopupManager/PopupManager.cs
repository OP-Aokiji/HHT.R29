using System;
using System.Collections.Generic;
using System.Text;
using MOST.Common.UserAttribute;
using System.Reflection;
using System.Windows.Forms;

namespace Framework.Common.PopupManager
{
    public class PopupManager
    {
        public static PopupManager instance = new PopupManager();

        private PopupManager() { }

        public IPopupResult ShowPopup(IPopupWindow popupWindow, IPopupParm parm)
        {
            return popupWindow.ShowPopup(parm);
        }
    }
}
