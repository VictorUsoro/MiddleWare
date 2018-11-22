using PagedList.Core;
using System.Collections.Generic;

namespace MW.Application
{
   public class PaginationExtension<T>
   {
      public IEnumerable<T> ReturnedList { get; set; }
      public int TotalCount { get; set; }
      public string Summary { get; set; }

      public StaticPagedList<T> StaticPagedList { get; set; }
   }
}
