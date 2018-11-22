using MW.Application;
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MW.Services
{
   public class UserProfileServices : BaseServices<UserProfile, UserProfileModel>
   {
      public UserProfileServices(MiddleWareDBContext context) : base(context) { }

      public DateTime? ConvertDateTime(string data)
      {
         DateTime dateTime;
         return DateTime.TryParseExact(data, "MMddyyyy", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out dateTime) ? Convert.ToDateTime(dateTime.ToString("MMddyyyy")) : dateTime;
      }
   }
}