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

   public enum ProfileType
   {
      [Display(Name = "Church Admin")]
      Admin = 1,

      [Display(Name = "Newcomer, Invitee")]
      NewCommer = 2,

      [Display(Name = "Member")]
      Member = 3,

      [Display(Name = "Worker")]
      Worker = 4,
      
      [Display(Name ="Group Member")]
      GroupMembers = 9      
   }

   public enum LocationType
   {
      Country = 1,
      State
   }

   public enum GroupType
   {
      [Display(Name = "Members")]
      Member = 1,

      [Display(Name = "Workers")]
      Worker,

      [Display(Name = "Others")]
      Others
   }

   public enum MessageType
   {
      [Display(Name = "Instant SMS")]
      InstantSMS = 1,

      [Display(Name = "Email Only")]
      EmailOnly,

      [Display(Name = "Scheduled SMS")]
      ScheduleSMS,

      [Display(Name = "Recurrent SMS")]
      RecurrentSMS
   }

   public enum SendingFrequency
   {
      [Display(Name = "Send Once")]
      Once = 1,
      [Display(Name = "Every 6 Hours")]
      SixHours,
      [Display(Name = "Every 12 Hours")]
      TwelveHours,
      [Display(Name = "Daily")]
      Daily,
      [Display(Name = "Weekly")]
      Weekly,
      [Display(Name = "Monthly")]
      Monthly
   }

   public enum BaseUrlType
   {
      HangFireLink = 1
   }

   public enum SmsTransactionType
   {
      Purchase = 1, Debit
   }

   public enum PaymentChannel
   {
      [Display(Name = "Online Payment")]
      Online = 1,
      [Display(Name = "Bank Deposit")]
      Bank,
      [Display(Name = "Unpaid Invoice")]
      Unpaid
   }

   public enum Roles
   {
      SuperAdmin = 1,
      Admin,
      ChurchAdmin,
      ChurchGroupAdmin,
      DataEntry
   }

   public enum ChurchType
   {
      [Display(Name = "Group of Churches/Fellowship")]
      GroupChurch = 1,
      [Display(Name = "District Church")]
      District,
      [Display(Name = "District Location")]
      Location,
      [Display(Name = "Single Church/Fellowship")]
      SingleChurch
   }

   public enum SMSDeliveryRoute
   {
      SMSLive247 = 0,
      BulkSMSNigeria
   }

   public enum DoNotDisturb
   {
      [Display(Name = "Normal Route")]
      NoDND = 0,
      [Display(Name = "Override DND Route")]
      OverrideDND,
      [Display(Name = "Override DND Fall-back Route")]
      ResentViaDND,

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

   public enum MenuType
   {
      [Display(Name ="Parent Menu")]
      TopMenu = 1, 
      [Display(Name ="Sub Menu")]
      SubMenu
   }
}
