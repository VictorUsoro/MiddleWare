using Microsoft.EntityFrameworkCore;
using MW.Application;
using MW.Application.Domain;

namespace MW.Services
{
   public partial class MiddleWareDBContext : DbContext
   {
      public MiddleWareDBContext()
      {
      }

      public MiddleWareDBContext(DbContextOptions<MiddleWareDBContext> options) : base(options) { }
            
      public virtual DbSet<Customer> Customers { get; set; }
      public virtual DbSet<Agent> Agents { get; set; }
   }
}