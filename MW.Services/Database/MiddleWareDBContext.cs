using ch.domain;
using Microsoft.EntityFrameworkCore;

namespace MW.Services
{
   public partial class MiddleWareDBContext : DbContext
   {
      public MiddleWareDBContext()
      {
      }

      public MiddleWareDBContext(DbContextOptions<MiddleWareDBContext> options) : base(options) { }

      public virtual DbSet<Church> Churches { get; set; }
      public virtual DbSet<Group> Groups { get; set; }
      public virtual DbSet<GroupMember> GroupMembers { get; set; }
      public virtual DbSet<UserProfile> UserProfiles { get; set; }
      public virtual DbSet<Location> Locations { get; set; }
      public virtual DbSet<Token> Tokens { get; set; }
      public virtual DbSet<ChurchEvent> ChurchEvents { get; set; }
      public virtual DbSet<Attendance> Attendances { get; set; }
      public virtual DbSet<Message> Messages { get; set; }
      public virtual DbSet<Reciepient> Reciepients { get; set; }
      public virtual DbSet<SmsUnit> SmsUnits { get; set; }
      public virtual DbSet<SmsTransaction> SmsTransactions { get; set; }
      public virtual DbSet<UserRole> UserRoles { get; set; }
      public virtual DbSet<SmsSetting> SmsSettings { get; set; }
      public virtual DbSet<AppMenu> AppMenus { get; set; }
      public virtual DbSet<UserPermission> UserPermissions { get; set; }
   }
}