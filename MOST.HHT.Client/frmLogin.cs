using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Service.Provider.WebService.Provider;
using Framework.Common.ExceptionHandler;
using MOST.Client.Proxy.LoginProxy;
using Framework.Controls.Container;
using MOST.VesselOperator;
using MOST.ApronChecker;
using MOST.PortSafety;
using MOST.WHChecker;
using MvcPatterns;
using Framework.Common.UserInformation;
using Framework.Common.Constants;
using Framework.Controls;
using System.Collections;
using MOST.Client.Proxy.CommonProxy;
using MOST.Common.Utility;

namespace MOST.CEClient
{
    public partial class frmLogin : TForm, ISubject
    {
        private ArrayList observers = new ArrayList();
        private ArrayList shiftList = new ArrayList();
        private int cbIndex = 0;

        //private Boolean FinishedLoad = false;

        ShiftDataItem shiftItem = null;
        public frmLogin()
        {
            InitializeComponent();
            this.initialFormSize();

            this.authorityCheck();
            
            Internationalization();
        }

        #region Internationalization( i18n )
        private void Internationalization()
        {
            label1.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0005");
            label2.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0003");
            //label3.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0004");
            btnLogIn.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0002");
            btnExit.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0001");
            this.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0009");
            btnGoto.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0011");
        }
        #endregion

        private void ActionListener(object sender, EventArgs e)
        {
            Button mybutton = (Button)sender;
            String buttonName = mybutton.Name;
            switch (buttonName) 
            {
                case "btnLogIn":
                    ExecuteLogin();
                    break;
                case "btnGoto":
                    ExecuteGoto();
                    break;
                case "btnExit":   
                    ExecuteLogout();
                    break;
            }
        }

        /// <summary>
        /// System logout Function
        /// </summary>
        private void ExecuteLogout()
        {
            if (MessageBox.Show(@"Do you want to exit?", @"Confirm", MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                //FinishedLoad = false;
                this.Dispose();
                Application.Exit();
            }
        }
        /// <summary>
        /// Login Check
        /// </summary>
        private void ExecuteLogin()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (btnLogIn.Text == Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0002"))
                {
                    if (this.validations(pnLoginCheck.Controls))
                    {
                        #region Login Validation Chcek
                        ILoginProxy proxy = new LoginProxy();
                        LoginParm parm = new LoginParm();
                        parm.userId = txtUserId.Text;
                        parm.password = txtPasswords.Text;

                        ResponseInfo info = proxy.login(parm);
                        UserInfoItem item = (UserInfoItem)info.list[0];
                        pnLogin.Top = 2;
                        pnLogin.Left = 2;
                        pnLogin.Visible = true;
                        pnLoginCheck.Visible = false;

                        UserInfo.getInstance().LoginDateTime = "";
                        UserInfo.getInstance().UserGroup = item.deptNm;
                        UserInfo.getInstance().UserId = txtUserId.Text;
                        UserInfo.getInstance().UserName = item.engNm;

                        ArrayList arrProgramTmp = new ArrayList();

                        for (int i = 1; i < info.list.Length; i++)
                        {
                            if (info.list[i] is AuthAccessItem)
                            {
                                AuthAccessItem accessItem = (AuthAccessItem)info.list[i];
                                UserInfo.getInstance().addAccessItem(accessItem);

                                switch (accessItem.pgmId)
                                {
                                    case Constants.MPTS_VESSEL_OPERATION:
                                        arrProgramTmp.Add(Constants.CMB_VESSEL_OPERATION);
                                        break;
                                    case Constants.MPTS_APRON_CHECKER:
                                        arrProgramTmp.Add(Constants.CMB_APRON_CHECKER);
                                        break;
                                    case Constants.MPTS_WAREHOUSE_CHECKER:
                                        arrProgramTmp.Add(Constants.CMB_WAREHOUSE_CHECKER);
                                        break;
                                    case Constants.MPTS_PORT_SAFETY:
                                        arrProgramTmp.Add(Constants.CMB_PORT_SAFETY);
                                        break;
                                }
                           }
                            if (info.list[i] is AuthMenuItem)
                            {
                                UserInfo.getInstance().addMenuItem((AuthMenuItem)info.list[i]);
                            }
                        }

                        if (arrProgramTmp.Contains(Constants.CMB_VESSEL_OPERATION))
                        {
                            cmbProgram.Items.Add(Constants.CMB_VESSEL_OPERATION);
                        }
                        if (arrProgramTmp.Contains(Constants.CMB_APRON_CHECKER))
                        {
                            cmbProgram.Items.Add(Constants.CMB_APRON_CHECKER);
                        }
                        if (arrProgramTmp.Contains(Constants.CMB_WAREHOUSE_CHECKER))
                        {
                            cmbProgram.Items.Add(Constants.CMB_WAREHOUSE_CHECKER);
                        }
                        if (arrProgramTmp.Contains(Constants.CMB_PORT_SAFETY))
                        {
                            cmbProgram.Items.Add(Constants.CMB_PORT_SAFETY);
                        }

                        if (cmbProgram.Items.Count > 0)
                        {
                            cmbProgram.SelectedIndex = 0;
                        }

                        btnLogIn.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0010");

                        statusPanel.Text = "Login ID : " + txtUserId.Text;

                        #endregion

                        #region Server Datetime
                        DateTime serverDt = CommonUtility.ParseStringToDate(
                            CommonUtility.GetCurrentServerTime(), CommonUtility.DDMMYYYYHHMM);
                        txtWorkDate.Value = serverDt;
                        #endregion

                        #region get Shift
                        ICommonProxy common = new CommonProxy();
                        ShiftParm shiftParm = new ShiftParm();
                        ResponseInfo shiftInfo = common.getShift(shiftParm);
                        if (shiftInfo != null)
                        {
                            for (int i = 0; i < shiftInfo.list.Length; i++)
                            {
                                if (shiftInfo.list[i] is ShiftDataItem)
                                {
                                    shiftItem = (ShiftDataItem)shiftInfo.list[i];
                                    shiftList.Add(shiftItem);

                                    cmbShift.Items.Add(shiftItem.shiftName);
                                }
                                else
                                {
                                    //CurrentServerTimeItem timeItem = (CurrentServerTimeItem)shiftInfo.list[i];
                                    //int serverTime = Convert.ToInt16(timeItem.currentServerTime.Substring(11, 5).Replace(":", ""));
                                    //for (int j = 0; j < shiftList.Count; j++)
                                    //{
                                    //    ShiftDataItem subtem = (ShiftDataItem)shiftList[j];
                                    //    if (Convert.ToInt16(subtem.fromTime) <= serverTime && serverTime <= Convert.ToInt16(subtem.toTime))
                                    //    {
                                    //        cmbShift.SelectedIndex = j;
                                    //    }
                                    //}
                                    //if (cmbShift.Text == "")
                                    //{
                                    //    cmbShift.SelectedIndex = cmbShift.Items.Count - 1;
                                    //}

                                    // Current server time is within Shift: 7:00 - 14:59, 15:00 - 22:59, 23:00 - 6:59
                                    CurrentServerTimeItem timeItem = (CurrentServerTimeItem)shiftInfo.list[i];
                                    int serverTime = Convert.ToInt16(timeItem.currentServerTime.Substring(11, 5).Replace(":", ""));
                                    int shiftIndex = 0;
                                    if (Convert.ToInt16(CommonUtility.SHIFT_1_FRM.Replace(":", "")) <= serverTime && serverTime <= Convert.ToInt16(CommonUtility.SHIFT_1_TO.Replace(":", "")))
                                    {
                                        shiftIndex = 1;
                                    }
                                    else if (Convert.ToInt16(CommonUtility.SHIFT_2_FRM.Replace(":", "")) <= serverTime && serverTime <= Convert.ToInt16(CommonUtility.SHIFT_2_TO.Replace(":", "")))
                                    {
                                        shiftIndex = 2;
                                    }
                                    else if (Convert.ToInt16(CommonUtility.SHIFT_3_FRM.Replace(":", "")) <= serverTime || serverTime <= Convert.ToInt16(CommonUtility.SHIFT_3_TO.Replace(":", "")))
                                    {
                                        shiftIndex = 3;
                                    }
                                    for (int j = 0; j < shiftList.Count; j++)
                                    {
                                        ShiftDataItem subtem = (ShiftDataItem)shiftList[j];
                                        if (shiftIndex.ToString().Equals(subtem.shiftindex))
                                        {
                                            cmbShift.SelectedIndex = j;
                                            break;
                                        }
                                    }
                                }
                            }
                            //FinishedLoad = true;
                        }
                        #endregion

                        #region Clear VesselHistoryInfo
                        VesselHistoryInfo.ClearInstance();
                        #endregion

                        #region Clear PrinterSettingsHistoryInfo
                        PrinterSettingsHistoryInfo.ClearInstance();
                        PrinterSettingsHistoryInfo.GetInstance().ComPort = "COM8:";
                        PrinterSettingsHistoryInfo.GetInstance().BaudRate = 19200;
                        #endregion
                    }
                }
                else
                {
                    #region Logout(Valiable Clear)
                    if (MessageBox.Show(@"Do you want to logout?", @"Confirm", MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {

                        //All forms close;
                        SendMessage();

                        pnLogin.Visible = false;
                        pnLoginCheck.Visible = true;
                        txtUserId.Text = "";
                        txtPasswords.Text = "";
                        txtUserId.Focus();

                        UserInfo.getInstance().LoginDateTime = "";
                        UserInfo.getInstance().UserGroup = "";
                        UserInfo.getInstance().UserId = "";
                        UserInfo.getInstance().UserName = "";
                        UserInfo.getInstance().Shift = "";
                        UserInfo.getInstance().ShiftNm = "";
                        UserInfo.getInstance().ShiftIndex = "";
                        UserInfo.getInstance().Workdate = "";
                        UserInfo.getInstance().clearAccessItem();
                        UserInfo.getInstance().clearMenuItem();
                        cmbProgram.Items.Clear();
                        cmbShift.Items.Clear();
                        shiftList.Clear();
                        observers.Clear();

                        btnLogIn.Text = Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0002");
                        statusPanel.Text = "Status : it is not connected";


                        // Clear VesselHistoryInfo
                        VesselHistoryInfo.ClearInstance();
                        // Clear PrinterSettingsHistoryInfo
                        PrinterSettingsHistoryInfo.ClearInstance();
                    }
                    #endregion
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ErrorHandler(this, e);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Navigation
        /// </summary>
        private void ExecuteGoto()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (this.validations(pnLogin.Controls))
                {
                    for (int j = 0; j < shiftList.Count; j++)
                    {
                        ShiftDataItem subItem = (ShiftDataItem)shiftList[j];
                        if (subItem.shiftName == cmbShift.Text)
                        {
                            UserInfo.getInstance().Shift = subItem.shiftId;
                            UserInfo.getInstance().ShiftNm = subItem.shiftName;
                            UserInfo.getInstance().ShiftIndex = subItem.shiftindex;
                        }
                    }
                    UserInfo.getInstance().Workdate = txtWorkDate.Text;

                    switch (cmbProgram.Text)
                    {
                        case Constants.CMB_VESSEL_OPERATION:
                            HVO101 vo = new HVO101();
                            registerView(vo);

                            ApplicationFacade.getInstance().sendNotification("view", vo);
                            break;
                        case Constants.CMB_APRON_CHECKER:
                            HAC101 ac = new HAC101();
                            registerView(ac);

                            ApplicationFacade.getInstance().sendNotification("view", ac);
                            break;
                        case Constants.CMB_PORT_SAFETY:
                            HPS101 ps = new HPS101();
                            registerView(ps);

                            ApplicationFacade.getInstance().sendNotification("view", ps);
                            break;
                        case Constants.CMB_WAREHOUSE_CHECKER:
                            HWC103 wc = new HWC103();
                            registerView(wc);

                            ApplicationFacade.getInstance().sendNotification("view", wc);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ErrorHandler(this, e);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #region Reference Code
        private void tButton2_Click(object sender, EventArgs e)
        {
            this.clearControlValue(this.Controls);
        }

        private void tButton1_Click(object sender, EventArgs e)
        {
            pnLoginCheck.clearControlValue(pnLoginCheck.Controls);
        }
        #endregion

        private void frmLogin_Closed(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void registerView(IObserver obs)
        {
            observers.Add(obs);
        }

        private void SendMessage()
        {
            NoticeMessage message = new NoticeMessage();
            for (int i = 0; i < observers.Count; i++)
            {
                IObserver obs = (IObserver)observers[i];
                obs.receiveNotify(message);
            }
        }

        private void KeypressListener(object sender, KeyPressEventArgs e)
        {
            if (btnLogIn.Text.Equals(Framework.Common.ResourceManager.ResourceManager.getInstance().getString("HCM0002"))
                && e.KeyChar.Equals((char)Keys.Enter))
            {
                ExecuteLogin();
            }
        }

        private void cmbProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbIndex = cmbShift.SelectedIndex;
            if (cmbProgram.SelectedItem != null)
            {
                if (cmbProgram.SelectedItem.Equals(Constants.CMB_PORT_SAFETY))
                {
                    if (cmbShift.Items.Count>2)
                    {
                        cmbShift.Items.RemoveAt(2);
                        if (cbIndex >= 2)
                            cmbShift.SelectedIndex = cbIndex - 1;
                        else if(cbIndex < 0)
                            cmbShift.SelectedIndex = 0;
                        else 
                            cmbShift.SelectedIndex = cbIndex;
                    }
                }
                else
                {
                    cmbShift.Items.Clear();
                    if (shiftList != null && shiftList.Count > 0)
                    {
                        for (int i = 0; i < shiftList.Count; i++)
                        {
                            cmbShift.Items.Add(((ShiftDataItem)shiftList[i]).shiftName);
                        }
                        if (cbIndex < 0)
                            cmbShift.SelectedIndex = 0;
                        else
                            cmbShift.SelectedIndex = cbIndex;
                    }
                }
            }
            
        }
    }
}