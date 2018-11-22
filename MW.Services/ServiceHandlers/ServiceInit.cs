using ch.services.OtherServices;

namespace MW.Services
{
   public class ServiceInit : IServiceInit
   {
      private readonly MiddleWareDBContext _context;
      public ServiceInit(MiddleWareDBContext context)
      {
         _context = context;
      }
      
      public UserProfileServices UserProfileServices => new UserProfileServices(_context);
      public UnitOfWork UnitOfWork => new UnitOfWork(_context);
   }
}