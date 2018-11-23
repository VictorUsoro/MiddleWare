using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MW.Application;

namespace MW.Services
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MiddleWareDBContext>
   {
      public MiddleWareDBContext CreateDbContext(string[] args)
      {
         var builder = new DbContextOptionsBuilder<MiddleWareDBContext>();
         builder.UseNpgsql(Constant.ConnectionString);
         return new MiddleWareDBContext(builder.Options);
      }
   }
}