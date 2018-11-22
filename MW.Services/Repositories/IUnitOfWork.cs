using MW.Application;

namespace MW.Services
{
   public interface IUnitOfWork
   {
      IRepository<UserProfile> UserProfileRepository { get; }
   }
}