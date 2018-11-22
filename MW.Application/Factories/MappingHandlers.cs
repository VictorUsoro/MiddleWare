namespace MW.Application
{
   public class MappingHandlers : IMappingHandlers
   {
      public UserProfileFactory UserProfileFactory => new UserProfileFactory();
   }
}
