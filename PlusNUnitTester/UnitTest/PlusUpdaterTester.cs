#region Explanation
//* --------------------------------------------------------------
//* CHANGE REVISION
//* --------------------------------------------------------------
//* DATE           AUTHOR      	   REVISION    	     Content
//* 2008-05-20   Mr Luis Lee	     1.0          First release.
//* --------------------------------------------------------------
//* CLASS DESCRIPTION
//* --------------------------------------------------------------
#endregion 

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Framework.Updater;
using System.Collections;
using System.Windows.Forms;
using Framework.Updater.Document;

namespace PlusNUnitTester
{
    [TestFixture]
    public class PlusUpdaterTester : AssertionHelper
    {
        [SetUp]
        protected void SetUp()
        {
         
        }

        [Test]
        public void PLUSUpdaterTest()
        {
            LibraryVersionCheck check = new LibraryVersionCheck();
            UpdateInformationItem updateList = check.Check();

            if (updateList.UpdateItemList != null && updateList.UpdateItemList.Count > 0)
            {
                if (updateList.TransferType == Constants.FILE_TRANS_HTTP)
                {
                    Application.Run(new frmPlusHttpUpdater(updateList));
                }
                else
                {
                    Application.Run(new frmPlusFtpUpdater(updateList));
                }
            }
        }
    }
}
