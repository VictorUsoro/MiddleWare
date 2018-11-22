using System;

namespace MW.Application
{
   public static class Constant
   {
      public static string Reference
      {
         get
         {
            return $"{DateTime.Now.ToString("ddMMyy")}"+
               $"{Guid.NewGuid().ToString().Replace("-", "").Remove(12).ToUpper()}";
         }
      }
      
      public static string ErrorPage => @"/Home/Error";
           
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
           
      #region Constant Link

      public static string SSOBaseURL => "AppSettingLinks:SSOLink";

      #endregion
   }
}
