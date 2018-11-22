using Mapster;

namespace MW.Application
{
   public static class MapsterConfig
   {
      public static void RegisterMappings()
      {
         TypeAdapterConfig<Customer, CustomerModel>.NewConfig().IgnoreNullValues(true);
      }
   }
}
