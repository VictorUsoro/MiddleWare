using System;

namespace MW.Application
{
   public interface IBaseEntity
   {
      Guid Id { get; set; }
      bool IsDeleted { get; set; }
      DateTime CreatedDate { get; set; }
      DateTime ModifiedDate { get; set; }
      RecordStatus RecordStatus { get; set; }
   }
}
