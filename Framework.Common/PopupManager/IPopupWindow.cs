using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;
namespace Framework.Common.PopupManager
{
    public interface IPopupWindow
    {
        IPopupResult ShowPopup(IPopupParm parm);
    }
}
