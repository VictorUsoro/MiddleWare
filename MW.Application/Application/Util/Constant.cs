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
      
      public static string ErrorPage => @"/LandingPage/FailRequest";

      public static string ConnectionString => "Host=localhost;Database=middlewaredb;Username=postgres;Password=adminp";
         
   }
}
