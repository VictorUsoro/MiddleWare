using System;

namespace MW.Application
{
   public abstract class BaseEntity : IBaseEntity
   {
      public Guid Id { get; set; }
      public bool IsDeleted { get; set; }
      public DateTime CreatedDate { get; set; }
      public DateTime ModifiedDate { get; set; }
      public RecordStatus RecordStatus { get; set; }
   }
}
