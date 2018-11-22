using MW.Application;
using MW.Application.Domain;

namespace MW.Services
{
   public class UnitOfWork : IUnitOfWork
   {
      private readonly MiddleWareDBContext _context;
      public UnitOfWork(MiddleWareDBContext context)
      {
         _context = context;
      }

      public IRepository<Customer> CustomerRepository => new Repository<Customer>(_context);
      public IRepository<Agent> AgentRepository => new Repository<Agent>(_context);
   }
}