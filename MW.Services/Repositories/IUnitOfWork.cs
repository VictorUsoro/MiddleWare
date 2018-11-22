using MW.Application;
using MW.Application.Domain;

namespace MW.Services
{
   public interface IUnitOfWork
   {
      IRepository<Customer> CustomerRepository { get; }
      IRepository<Agent> AgentRepository { get; }
   }
}