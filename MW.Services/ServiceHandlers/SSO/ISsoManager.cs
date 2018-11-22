using ch.domain;
using ch.domain.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ch.services.ServiceHandlers.SSO
{
   public interface ISsoManager
   {
      Task<JSendResponseModel<SignInStatus>> PasswordSignInAsync(string userName, string password,
         bool isPersistent, bool shouldLockout, string SSOBaseURL, HttpContext context);
      Task SignOut(HttpContext context);
      Task<JSendResponseModel<string>> CreateUserAsync(SsoUserModel model, string SSOBaseURL);
      Task<JSendResponseModel<string>> PasswordRecovery(string email, string SSOBaseURL);
      Task<JSendResponseModel<string>> ChangePasswordAsync(ChangePasswordModel model, string SSOBaseURL);
      Task<JSendResponseModel<UserModel>> SetPasswordAsync(ResetPasswordModel model, string SSOBaseURL);
      Task<JSendResponseModel<string>> CreateProfilePassword(SetPasswordModel model, string SSOBaseURL);
   }
}
