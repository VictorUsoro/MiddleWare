namespace MW.Application
{
   public interface IMappingHandlers
   {
      CustomerFactory UserProfileFactory { get; }
      AgentFactory AgentFactory { get; }
   }
}
