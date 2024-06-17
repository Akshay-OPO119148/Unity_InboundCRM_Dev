using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCRM
{
    #region StaticClass
    static class CL_AgentDetails
    {
        public static string ProcessName { get; set; }
        public static string OPOID { get; set; }
        public static string AgentName { get; set; }
        public static string AgentID { get; set; }
        public static string DN { get; set; }
        public static string Astr_DN { get; set; }
        public static string MobileNo { get; set; }
        public static string Email { get; set; }
        public static string Prefix { get; set; }
        public static string TserverIP { get; set; }
        public static string TserverPort { get; set; }
        public static string IsAsterikLogic { get; set; }
        public static string AsterikGetNextUrl { get; set; }
        public static string IsConf { get; set; }
        public static string ClientUrl { get; set; }
        public static string IsManual { get; set; }
        public static string manualprefix { get; set; }
        public static string AstrikLocalIP { get; set; }
        public static string AstrikIP { get; set; }
        public static string ApiCallCut { get; set; }
        public static string ConfChannel { get; set; }
        public static string AstrikReqPort { get; set; }
        public static string AstrikPort { get; set; }
        public static string iframesource { get; set; }
        public static string PhoneNoMaskIs { get; set; }
        public static string DialAccess { get; set; }
        public static string SingleStepTransfer { get; set; }
        public static string ThreeStepTransfer { get; set; }
        public static string Version { get; set; }
        public static string Result { get; set; }
        public static string OTPRequired { get; set; }
        public static string OTP { get; set; }
        public static string APKVersion { get; set; }
        public static string APKURL { get; set; }

        public static string Status { get; set; }
        public static string SipPort { get; set; }
        public static string TserverIP_OFFICE { get; set; }
        public static string iframesource_OFFICE { get; set; }
        public static string HistoryPage { get; set; }
        public static string Location { get; set; }
        public static string ProcessType { get; set; }
        public static string AsterikGetNextUrl_Office { get; set; }
        public static string KMS_OFFICE { get; set; }
        public static string password { get; set; }
        public static string Ishome { get; set; }
        public static string IsAutoWrap { get; set; }
        public static string AutoWrapTime { get; set; }
        public static string HoldMusic_Path { get; set; }
        public static string IdleGetNextTimer { get; set; }
        public static string AutoGetNextTimer { get; set; }
        public static string BioLoginStatus { get; set; }

        public static string IsTest_Required { get; set; }
        public static string testAttempted { get; set; }
        
    }
    #endregion

    #region NonStaticClass
    class CL_AgentDet
    {
        public string ProcessName { get; set; }
        public string OPOID { get; set; }
        public string AgentName { get; set; }
        public string AgentID { get; set; }
        public string DN { get; set; }
        public string Astr_DN { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Prefix { get; set; }
        public string TserverIP { get; set; }
        public string TserverPort { get; set; }
        public string IsAsterikLogic { get; set; }
        public string AsterikGetNextUrl { get; set; }
        public string IsConf { get; set; }
        public string ClientUrl { get; set; }
        public string IsManual { get; set; }
        public string manualprefix { get; set; }
        public string AstrikLocalIP { get; set; }
        public string AstrikIP { get; set; }
        public string ApiCallCut { get; set; }
        public string ConfChannel { get; set; }
        public string AstrikReqPort { get; set; }
        public string AstrikPort { get; set; }
        public string iframesource { get; set; }
        public string PhoneNoMaskIs { get; set; }
        public string DialAccess { get; set; }
        public string SingleStepTransfer { get; set; }
        public string ThreeStepTransfer { get; set; }
        public string Version { get; set; }
        public string Result { get; set; }
        public string OTPRequired { get; set; }
        public string OTP { get; set; }
        public string APKVersion { get; set; }
        public string APKURL { get; set; }
        public string Status { get; set; }
        public string SipPort { get; set; }
        public string TserverIP_OFFICE { get; set; }
        public string iframesource_OFFICE { get; set; }
        public string HistoryPage { get; set; }
        public string Location { get; set; }
        public string ProcessType { get; set; }
        public string AsterikGetNextUrl_Office { get; set; }
        public string KMS_OFFICE { get; set; }
        public string password { get; set; }
        public string Ishome { get; set; }
        public string IsAutoWrap { get; set; }
        public string AutoWrapTime { get; set; }
        public string HoldMusic_Path { get; set; }
        public string IdleGetNextTimer { get; set; }
        public string AutoGetNextTimer { get; set; }
        public string BioLoginStatus { get; set; }

        public string IsTest_Required { get; set; }

        public string testAttempted { get; set; }
    }
    #endregion
}
