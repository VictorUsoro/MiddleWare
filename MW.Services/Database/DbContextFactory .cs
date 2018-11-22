using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MW.Services
{
   public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MiddleWareDBContext>
   {
      public MiddleWareDBContext CreateDbContext(string[] args)
      {
         var builder = new DbContextOptionsBuilder<MiddleWareDBContext>();
         builder.UseSqlServer(Constant.ConnectionString);
         return new MiddleWareDBContext(builder.Options);
      }
   }
}