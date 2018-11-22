namespace MW.Application.Domain
{
   public class Agent : UserDetails
   {
      public string ProviderAPI { get; set; }
   }

   public class AgentModel : UserDetailsModel
   {
      public string ProviderAPI { get; set; }
   }
}
