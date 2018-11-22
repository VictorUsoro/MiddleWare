namespace MW.Application
{
   public class Customer : UserDetails
   {
      public string PhoneNumber2 { get; set; }
      public string ShopLine { get; set; }
      public string ShopNumber { get; set; }
      public string ShopBlock { get; set; }
      public string MeterId { get; set; }
      public string PictureURL { get; set; }
      public string Tier { get; set; }
   }

   public class CustomerModel : UserDetailsModel
   {
      public string PhoneNumber2 { get; set; }
      public string ShopLine { get; set; }
      public string ShopNumber { get; set; }
      public string ShopBlock { get; set; }
      public string MeterId { get; set; }
      public string PictureURL { get; set; }
      public string Tier { get; set; }
   }
}
