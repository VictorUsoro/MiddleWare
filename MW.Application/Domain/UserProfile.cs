namespace MW.Application
{
   public class UserProfile : BaseEntity
   {
      public string FullName { get; set; }
      public string Email { get; set; }
      public string Phone { get; set; }
   }

   public class UserProfileModel : BaseDataModel
   {
      public string FullName { get; set; }
      public string Email { get; set; }
      public string Phone { get; set; }
   }
}
