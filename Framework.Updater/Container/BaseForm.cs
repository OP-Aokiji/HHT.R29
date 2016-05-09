#region Explanation
//* --------------------------------------------------------------
//* CHANGE REVISION
//* --------------------------------------------------------------
//* DATE           AUTHOR      	   REVISION    	     Content
//* 2008-07-02   Mr Luis Lee	     1.0          First release.
//* --------------------------------------------------------------
//* CLASS DESCRIPTION
//* --------------------------------------------------------------
#endregion 

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
//using System.Reflection;
//using Framework.Controls.UserControls;
//using System.Collections.Specialized;
//using Framework.Controls.ValidationHandler;
//using Framework.Common.UserInformation;
//using Framework.Service.Provider.WebService.Provider;

namespace Framework.Updater.Container
{
    public partial class BaseForm : Form
    {
        #region Initialize
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaseForm";
            this.ResumeLayout(false);
        }

        public void initialFormSize()
        {
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }
        #endregion
    }
}
