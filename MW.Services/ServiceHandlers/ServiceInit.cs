namespace MW.Services
{
   public class ServiceInit : IServiceInit
   {
      private readonly MiddleWareDBContext _context;
      public ServiceInit(MiddleWareDBContext context)
      {
         _context = context;
      }
      
      public CustomerServices CustomerServices => new CustomerServices(_context);
      public AgentServices AgentServices => new AgentServices(_context);


      public UnitOfWork UnitOfWork => new UnitOfWork(_context);
   }
}