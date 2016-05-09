using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace MOST.Common.Utility
{
    public class VesselHistoryInfo
    {
        private string vslCallId;
        private string vslNm;
        private string vslTpCd;
        private string vslTpNm;
        private string purpCallCd;
        private string purpCallNm;
        private string deprSaId;

        public string DeprSaId
        {
            get { return deprSaId; }
            set { deprSaId = value; }
        }
        private string arrvSaId;

        public string ArrvSaId
        {
            get { return arrvSaId; }
            set { arrvSaId = value; }
        }

        protected static VesselHistoryInfo instance;

        public static VesselHistoryInfo GetInstance()
        {
            return instance;
        }

        static VesselHistoryInfo()
        {
            instance = new VesselHistoryInfo();
        }

        public static void ClearInstance()
        {
            //System.Console.WriteLine("clearInstance start ----------------");
            if (instance != null)
            {
                //System.Console.WriteLine("clearInstance <> null");

                instance.vslCallId = string.Empty;
                instance.vslNm = string.Empty;
                instance.vslTpCd = string.Empty;
                instance.vslTpNm = string.Empty;
                instance.purpCallCd = string.Empty;
                instance.purpCallNm = string.Empty;
                instance.arrvSaId = string.Empty;
                instance.deprSaId = string.Empty;
            }
            //System.Console.WriteLine("clearInstance end ----------------");
        }

        public static void SetInstanceSAID(string DeprSaId, string ArrvSaId)
        {
            if (instance != null)
            {
                //System.Console.WriteLine("setInstance <> null");

                instance.arrvSaId = ArrvSaId;
                instance.deprSaId = DeprSaId;
            }
        }

        public static void SetInstance(string argVslCallId, string argVslNm,
                                            string argVslTpCd, string argVslTpNm,
                                            string argPurpCallCd, string argPurpCallNm)
        {
            //System.Console.WriteLine("setInstance start ----------------");
            if (instance != null)
            {
                //System.Console.WriteLine("setInstance <> null");

                instance.vslCallId = argVslCallId;
                instance.vslNm = argVslNm;
                instance.vslTpCd = argVslTpCd;
                instance.vslTpNm = argVslTpNm;
                instance.purpCallCd = argPurpCallCd;
                instance.purpCallNm = argPurpCallNm;
            }
            //System.Console.WriteLine("setInstance end ----------------");
        }

        public string VslCallId
        {
            get { return vslCallId; }
            set { vslCallId = value; }
        }

        public string VslNm
        {
            get { return vslNm; }
            set { vslNm = value; }
        }

        public string VslTpCd
        {
            get { return vslTpCd; }
            set { vslTpCd = value; }
        }

        public string VslTpNm
        {
            get { return vslTpNm; }
            set { vslTpNm = value; }
        }

        public string PurpCallCd
        {
            get { return purpCallCd; }
            set { purpCallCd = value; }
        }

        public string PurpCallNm
        {
            get { return purpCallNm; }
            set { purpCallNm = value; }
        }
    }
}
