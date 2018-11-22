using Mapster;
using MW.Application.Domain;

namespace MW.Application
{
   public static class MapsterConfig
   {
      public static void RegisterMappings()
      {
         TypeAdapterConfig<Customer, CustomerModel>.NewConfig().IgnoreNullValues(true);
         TypeAdapterConfig<Agent, AgentModel>.NewConfig().IgnoreNullValues(true);
      }
   }
}
