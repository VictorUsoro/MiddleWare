namespace MW.Application
{
   public class MappingHandlers : IMappingHandlers
   {
      public CustomerFactory UserProfileFactory => new CustomerFactory();
      public AgentFactory AgentFactory => new AgentFactory();
   }
}
