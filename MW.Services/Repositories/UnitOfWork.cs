using MW.Application;

namespace MW.Services
{
   public class UnitOfWork : IUnitOfWork
   {
      private readonly MiddleWareDBContext _context;
      public UnitOfWork(MiddleWareDBContext context)
      {
         _context = context;
      }

      public IRepository<UserProfile> UserProfileRepository => new Repository<UserProfile>(_context);
   }
}