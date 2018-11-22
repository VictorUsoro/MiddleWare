using Newtonsoft.Json;

namespace MW.Application
{
   public static class Utility
   {
      public static string MoneyFormatter(decimal Amount, string Currency = null)
      {
         string formattedAmnt = Amount < 0 ? "0.00" : string.Format("{0:C}", Amount);
         return string.IsNullOrEmpty(Currency) ? formattedAmnt.Replace("$", "NGN ") : formattedAmnt.Replace("$", Currency);
      }

      public static T DeserializeJson<T>(string input)
      {
         return JsonConvert.DeserializeObject<T>(input);
      }

      public static string SerializeJson(object input)
      {
         return JsonConvert.SerializeObject(input);
      }

      public static int MessageUnitCost(string Message, int ProfileCount)
      {
         int msgpagecount = Message.Length / 160;
         if (Message.Length % 160 > 0) msgpagecount += 1;
         return msgpagecount * ProfileCount;
      }
   }
}
