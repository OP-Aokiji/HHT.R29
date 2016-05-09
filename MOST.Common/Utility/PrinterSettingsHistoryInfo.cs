using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace MOST.Common.Utility
{
    public class PrinterSettingsHistoryInfo
    {
        private int baudRate;
        private string comPort;

        protected static PrinterSettingsHistoryInfo instance;

        public static PrinterSettingsHistoryInfo GetInstance()
        {
            return instance;
        }

        static PrinterSettingsHistoryInfo()
        {
            instance = new PrinterSettingsHistoryInfo();
        }

        public static void ClearInstance()
        {
            if (instance != null)
            {
                instance.baudRate = 0;
                instance.comPort = string.Empty;
            }
        }

        public static void SetInstance(string argCOMPort, int argBaudRate)
        {
            if (instance != null)
            {
                instance.baudRate = argBaudRate;
                instance.comPort = argCOMPort;
            }
        }

        public int BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }

        public string ComPort
        {
            get { return comPort; }
            set { comPort = value; }
        }
    }
}
