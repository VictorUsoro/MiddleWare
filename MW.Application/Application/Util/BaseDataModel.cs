using System;

namespace MW.Application
{
   public abstract class BaseDataModel
   {
      public Guid Id { get; set; }
      public bool IsDeleted { get; }
      public DateTime CreatedDate { get; set; }
      public DateTime ModifiedDate { get; }
      public RecordStatus RecordStatus { get; }
   }
}
