namespace MW.Services
{
   public interface IServiceInit
   {
      CustomerServices CustomerServices { get; }
      AgentServices AgentServices { get; }
   }
}