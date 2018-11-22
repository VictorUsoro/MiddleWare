using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using ch.domain;
using ch.domain.Application.Util;
using ch.domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace ch.services.ServiceHandlers.SSO
{
   public class SsoManager : ISsoManager
   {
      public async Task<JSendResponseModel<SignInStatus>> PasswordSignInAsync(string userName, string password,
         bool isPersistent, bool shouldLockout, string SSOBaseURL,
         HttpContext context)
      {
         try
         {
            var baseHttpClient = new BaseHttpClient();
            var response = await baseHttpClient.PostAsync<JSendResponseModel<TokenResponse>>(
             baseUrl: SSOBaseURL,
             postdata: new LoginModel
             {
                Email = userName,
                Password = password
             },
             url: Constant.SSOGetTokenURL);

            if (response.Status != "00")
            {
               return new JSendResponseModel<SignInStatus>
               {
                  Message = response.Message,
                  Data = SignInStatus.Failure,
                  Status = "90"
               };
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(response.Data.AccessToken) as JwtSecurityToken;

            var userDataString = jsonToken.Claims.First(claim => claim.Type == "sub").Value;
            var userModel = Utility.DeserializeJson<UserModel>(userDataString);

            var newIdentity = new ClaimsIdentity("Cookies");
            newIdentity.AddClaim(new Claim("token", $"{response.Data.AccessToken}"));
            newIdentity.AddClaim(new Claim("userfullname", $"{userModel.FirstName} {userModel.LastName}"));
            newIdentity.AddClaim(new Claim("userphone", $"{userModel.PhoneNumber}"));
            newIdentity.AddClaim(new Claim("useremail", $"{userModel.Email}"));
            newIdentity.AddClaim(new Claim("usercode", $"{userModel.UserCode}"));

            await context.SignInAsync(
               scheme: "chmAuth",
               principal: new ClaimsPrincipal(newIdentity),
               properties: new AuthenticationProperties
               {
                  AllowRefresh = true,
                  ExpiresUtc = DateTime.UtcNow.AddHours(2),
               });

            return new JSendResponseModel<SignInStatus>
            {
               Message = userModel.UserCode,
               Data = SignInStatus.Success,
               Status = "00"
            };
         }
         catch (Exception ex)
         {
            return new JSendResponseModel<SignInStatus>
            {
               Message = ex.Message,
               Data = SignInStatus.Success,
               Status = "99"
            };
         }
      }

      public async Task SignOut(HttpContext context)
      {
         await context.SignOutAsync(
            scheme: "chmAuth",
            properties: new AuthenticationProperties
            {
               AllowRefresh = false
            });
      }

      public async Task<JSendResponseModel<string>> CreateUserAsync(SsoUserModel model, string SSOBaseURL)
      {
         try
         {
            var baseHttpClient = new BaseHttpClient();
            var response = await baseHttpClient.PostAsync<JSendResponseModel<string>>(
               baseUrl: SSOBaseURL,
               postdata: model,
               url: Constant.SSOCreatUserURL);

            if (response.Status != "00")
            {
               return new JSendResponseModel<string>
               {
                  Message = response.Message,
                  Data = "",
                  Status = "90"
               };
            }

            return new JSendResponseModel<string>
            {
               Message = response.Message,
               Data = response.Data,
               Status = "00"
            };
         }
         catch (Exception ex)
         {
            return new JSendResponseModel<string>
            {
               Message = ex.Message,
               Data = ex.ToString(),
               Status = "99"
            };
         }
      }

      public async Task<JSendResponseModel<string>> PasswordRecovery(string email, string SSOBaseURL)
      {
         try
         {

            var baseHttpClient = new BaseHttpClient();
            var response = await baseHttpClient.PostAsync<JSendResponseModel<string>>(
               baseUrl: SSOBaseURL,
               postdata: new PasswordRecoveryModel { Email = email },
               url: Constant.SSOPasswordRecoveryURL);

            if (response.Status != "00")
            {
               return new JSendResponseModel<string>
               {
                  Message = response.Message,
                  Data = "",
                  Status = "90"
               };
            }

            return new JSendResponseModel<string>
            {
               Message = response.Message,
               Data = response.Data,
               Status = "00"
            };
         }
         catch (Exception ex)
         {
            return new JSendResponseModel<string>
            {
               Message = ex.Message,
               Data = ex.ToString(),
               Status = "99"
            };
         }
      }

      public async Task<JSendResponseModel<string>> ChangePasswordAsync(ChangePasswordModel model, string SSOBaseURL)
      {
         try
         {
            var baseHttpClient = new BaseHttpClient(addheaders: true, token: model.Token);
            var response = await baseHttpClient.PostAsync<JSendResponseModel<string>>(
               baseUrl: SSOBaseURL,
               postdata: model,
               url: Constant.SSOChangePasswordURL);

            if (response.Status != "00")
            {
               return new JSendResponseModel<string>
               {
                  Message = response.Message,
                  Data = "",
                  Status = "90"
               };
            }

            return new JSendResponseModel<string>
            {
               Message = response.Message,
               Data = response.Data,
               Status = "00"
            };
         }
         catch (Exception ex)
         {
            return new JSendResponseModel<string>
            {
               Message = ex.Message,
               Data = ex.ToString(),
               Status = "99"
            };
         }
      }

      public async Task<JSendResponseModel<UserModel>> SetPasswordAsync(ResetPasswordModel model, string SSOBaseURL)
      {
         try
         {
            var baseHttpClient = new BaseHttpClient();
            var response = await baseHttpClient.PostAsync<JSendResponseModel<UserModel>>(
               baseUrl: SSOBaseURL,
               postdata: model,
               url: Constant.SSOSetPasswordURL);

            if (response.Status != "00")
            {
               return new JSendResponseModel<UserModel>
               {
                  Message = response.Message,
                  Data = null,
                  Status = "90"
               };
            }

            return new JSendResponseModel<UserModel>
            {
               Message = response.Message,
               Data = response.Data,
               Status = "00"
            };
         }
         catch (Exception ex)
         {
            return new JSendResponseModel<UserModel>
            {
               Message = ex.Message,
               Data = null,
               Status = "99"
            };
         }
      }

      public async Task<JSendResponseModel<string>> CreateProfilePassword(SetPasswordModel model, string SSOBaseURL)
      {
         try
         {
            var baseHttpClient = new BaseHttpClient();
            var response = await baseHttpClient.PostAsync<JSendResponseModel<string>>(
               baseUrl: SSOBaseURL,
               postdata: model,
               url: Constant.SSOSetProfilePassword);

            if (response.Status != "00")
            {
               return new JSendResponseModel<string>
               {
                  Message = response.Message,
                  Data = "",
                  Status = "90"
               };
            }

            return new JSendResponseModel<string>
            {
               Message = response.Message,
               Data = response.Data,
               Status = "00"
            };
         }
         catch (Exception ex)
         {
            return new JSendResponseModel<string>
            {
               Message = ex.Message,
               Data = ex.ToString(),
               Status = "99"
            };
         }
      }
   }
}
