namespace MW.Application
{
    public class JSendResponseModel<T>
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public T Data { get; set; }
    }

   public class JSendServiceModel
   {
      public string Message { get; set; }
      public bool Status { get; set; }
      public object Data { get; set; }
   }

   public class JSendServiceResponseModel<T>
   {
      public string Message { get; set; }
      public bool Status { get; set; }
      public T Data { get; set; }
   }
}