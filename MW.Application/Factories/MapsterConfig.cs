using Mapster;

namespace MW.Application
{
   public static class MapsterConfig
   {
      public static void RegisterMappings()
      {
         TypeAdapterConfig<UserProfile, UserProfileModel>.NewConfig().IgnoreNullValues(true);
      }
   }
}
