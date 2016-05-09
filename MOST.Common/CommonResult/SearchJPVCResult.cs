using System;
using System.Collections.Generic;
using System.Text;
using Framework.Common.PopupManager;

namespace MOST.Common.CommonResult
{
    public class SearchJPVCResult : IPopupResult
    {
        private string jpvc;
        private string vesselName;
        private string etb;
        private string berthLocation;
        private string loa;
        private string atb;
        private string etw;
        private string wharfStart;
        private string wharfEnd;
        private string atw;
        private string etc;
        private string atc;
        private string etu;
        private string atu;
        private string purpCall;
        private string purpCallCd;
        private string vslTp;
        private string vslTpNm;
        private string dblBankingVslCallId;     // Mother vessel (using in Double Banking - STS operation).
        private string curAtb;

        private string deprSaId;
        private string arrvSaId;

        // oga CR 
        private string ogaStatus;
        private string ogaStatusDate;
        private string ogaQuarantine;

        // Vessel Shifting CR - 17 Mar 2015
        private string vslShiftingSeq;
        private string currAtb;
        private string currAtu;

        private string wgtBbkDbk;
        private string wgtLq;

        public string VslShiftingSeq
        {
            get { return vslShiftingSeq; }
            set { vslShiftingSeq = value; }
        }
        public string CurrAtb
        {
            get { return currAtb; }
            set { currAtb = value; }
        }
        public string CurrAtu
        {
            get { return currAtu; }
            set { currAtu = value; }
        }

        public string WgtBbkDbk
        {
            get { return wgtBbkDbk; }
            set { wgtBbkDbk = value; }
        }
        public string WgtLq
        {
            get { return wgtLq; }
            set { wgtLq = value; }
        }


        public string ArrvSaId
        {
            get { return arrvSaId; }
            set { arrvSaId = value; }
        }

        public string DeprSaId
        {
            get { return deprSaId; }
            set { deprSaId = value; }
        }

        public string Atu
        {
            get { return atu; }
            set { atu = value; }
        }

        public string Jpvc
        {
            get { return jpvc; }
            set { jpvc = value; }
        }

        public string VesselName
        {
            get { return vesselName; }
            set { vesselName = value; }
        }

        public string Etb
        {
            get { return etb; }
            set { etb = value; }
        }

        public string BerthLocation
        {
            get { return berthLocation; }
            set { berthLocation = value; }
        }

        public string Loa
        {
            get { return loa; }
            set { loa = value; }
        }

        public string Atb
        {
            get { return atb; }
            set { atb = value; }
        }

        public string Etw
        {
            get { return etw; }
            set { etw = value; }
        }
        public string Etu
        {
            get { return etu; }
            set { etu = value; }
        }

        public string Atc
        {
            get { return atc; }
            set { atc = value; }
        }

        public string Etc
        {
            get { return etc; }
            set { etc = value; }
        }

        public string Atw
        {
            get { return atw; }
            set { atw = value; }
        }

        public string WharfEnd
        {
            get { return wharfEnd; }
            set { wharfEnd = value; }
        }

        public string WharfStart
        {
            get { return wharfStart; }
            set { wharfStart = value; }
        }

        public string PurpCall
        {
            get { return purpCall; }
            set { purpCall = value; }
        }

        public string PurpCallCd
        {
            get { return purpCallCd; }
            set { purpCallCd = value; }
        }

        public string VslTp
        {
            get { return vslTp ; }
            set { vslTp = value; }
        }

        public string VslTpNm
        {
            get { return vslTpNm; }
            set { vslTpNm = value; }
        }

        public string DblBankingVslCallId
        {
            get { return dblBankingVslCallId; }
            set { dblBankingVslCallId = value; }
        }

        public string CurAtb
        {
            get { return curAtb; }
            set { curAtb = value; }
        }

        public string OgaStatus
        {
            get { return ogaStatus; }
            set { ogaStatus = value; }
        }

        public string OgaStatusDate
        {
            get { return ogaStatusDate; }
            set { ogaStatusDate = value; }
        }

        public string OgaQuarantine
        {
            get { return ogaQuarantine; }
            set { ogaQuarantine = value; }
        }
    }
}
