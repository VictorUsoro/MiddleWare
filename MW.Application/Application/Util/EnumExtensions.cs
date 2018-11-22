using System.ComponentModel.DataAnnotations;

namespace MW.Application
{
   public static class EnumExtensions
   {
      public static string Name(this System.Enum e)
      {
         var attributes = (DisplayAttribute[])e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DisplayAttribute), false);
         return attributes.Length > 0 ? attributes[0].Name : string.Empty;
      }
   }
}
