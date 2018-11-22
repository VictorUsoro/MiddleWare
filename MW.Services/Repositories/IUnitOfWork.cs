using MW.Application;

namespace MW.Services
{
   public interface IUnitOfWork
   {
      IRepository<Customer> UserProfileRepository { get; }
   }
}