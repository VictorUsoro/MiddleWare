namespace MW.Application
{
   public abstract class UserDetails : BaseEntity
    {
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string OtherNames { get; set; }

      public string PhoneNumber { get; set; }
      public Gender Gender { get; set; }
      public string Email { get; set; }
   }

   public abstract class UserDetailsModel : BaseDataModel
   {
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string OtherNames { get; set; }

      public string PhoneNumber { get; set; }
      public Gender Gender { get; set; }
      public string Email { get; set; }
   }
}
