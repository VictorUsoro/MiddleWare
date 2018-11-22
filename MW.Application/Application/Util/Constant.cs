using System;

namespace MW.Application
{
   public static class Constant
   {
      public static string SuperAdminEmail => "victorusoro@hotmail.com";

      public static string Reference
      {
         get
         {
            return $"{DateTime.Now.ToString("ddMMyy")}{Guid.NewGuid().ToString().Replace("-", "").Remove(12).ToUpper()}";
         }
      }

      public static string PayStack_Base_URL => @"https://api.paystack.co";

      public static string PayStack_Account_Type => @"";

      public static string LiveBalance => "LiveBalance";
      public static string PendingBalance => "PendingBalance";
      public static string ErrorPage => @"/Home/Error";

      public static string ListNigerianStateAddress => "/files/ListNigerianStates.txt";

      public static string ListCountriesAddress => "/files/ListCountries.txt";
            
      public static string SendInBluePassword => @"QYKhbys8P0SnN4qR";

      public static string SendGrid
      {
         get { return @"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"; }
      }

      public static string ConnectionString
      {
         get
         {
            return @"Data Source=.\SQLExpress; Database=chdb;Trusted_Connection=True;MultipleActiveResultSets=true";
         }
      }

      #region SMS

      #region SMSLive Constants

      public static string SMSLiveUrl => "http://www.smslive247.com/";

      public static string SMSLiveSessionId
      {
         get
         {
            return @"52915bce-132b-4132-af06-4ebd00e5ec9d";
         }
      }

      #endregion

      #region BulkSMSNigeria

      public static string BulkSMSNigeriaURL => "https://www.bulksmsnigeria.com/api/v1/sms/create";

      public static string BulkSMSNigeriaAPIKey
      {
         get
         {
            return @"C5amp0WfdRT6hVggWuzXLXF5EfH9VNzggayyesXdnOa8FGgZsC6EdXKPENdN";
         }
      }

      #endregion

      #endregion

      #region Hangfire

      public static string HangFireProcessURL => "api/job/process/";
      public static string HangFireProcess2URL => "api/job/process2";
      public static string HangFireScheduleURL => "api/job/schedule/";

      #endregion

      #region SSO

      public static string SSOLoginURL => "api/token/get";
      public static string SSOCreatUserURL => "api/access/create";
      public static string SSOSetProfilePassword => "api/access/newpassword";
      public static string SSOUserInfoURL => "api/access/getuserinfo";
      public static string SSOGetTokenURL => "api/token/get";
      public static string SSOPasswordRecoveryURL => "api/access/passwordrecovery";
      public static string SSOSetPasswordURL => "api/access/setpassword";
      public static string SSOChangePasswordURL => "api/access/changepassword";

      #endregion

      public static string BanksJsonAddress => "/files/ListBanks.txt";
      public static string SendGrid_API_Key
      {
         get { return @"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"; }
      }

      #region Constant Link

      public static string SSOBaseURL => "AppSettingLinks:SSOLink";
      public static string PortalBaseURL => "AppSettingLinks:PortalLink";
      public static string HangFireBaseURL => "AppSettingLinks:HangFireLink";
      public static string PaystackSecret => "Payments:PaystackSecret";
      public static string PaystackPublic => "Payments:PaystackPublic";

      #endregion
   }
}
