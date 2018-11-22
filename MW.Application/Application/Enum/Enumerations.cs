using System.ComponentModel.DataAnnotations;

namespace MW.Application
{
   public enum Gender
   {
      Male = 1, Female
   }

   public enum SignInStatus
   {
      Success = 0,
      LockedOut = 1,
      RequiresVerification = 2,
      Failure = 3
   }
   
   public enum LocationType
   {
      Country = 1,
      State
   }
   
   public enum RecordStatus
   {
      [Display(Name = "Active")]
      Active = 0,

      [Display(Name = "Inactive")]
      Inactive,

      [Display(Name = "Achieved")]
      Achieved,

      [Display(Name = "Deleted")]
      Deleted
   }
}
