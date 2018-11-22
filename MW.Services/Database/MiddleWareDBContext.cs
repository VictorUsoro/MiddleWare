using Microsoft.EntityFrameworkCore;
using MW.Application;

namespace MW.Services
{
   public partial class MiddleWareDBContext : DbContext
   {
      public MiddleWareDBContext()
      {
      }

      public MiddleWareDBContext(DbContextOptions<MiddleWareDBContext> options) : base(options) { }
            
      public virtual DbSet<Customer> UserProfiles { get; set; }
   }
}