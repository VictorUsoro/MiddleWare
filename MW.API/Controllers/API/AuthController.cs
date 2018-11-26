using Microsoft.AspNetCore.Mvc;
using MW.Services;

public class AuthController : BaseController
{
    public AuthController(IServiceInit services) : base(services)
    {
    }

    [Route("/token"), HttpPost]
    public IActionResult Token(LoginModel model)
    {
        if(!ModelState.IsValid)
        {
            return Error(validationErrorMessage(ModelState));
        }
        return Success("sometokenhere");
    }

    [Route("/resetpassword/{email}"), HttpPost]
    public IActionResult ResetPassword(string email)
    {
        if(string.IsNullOrEmpty(email))
        {
            return Error($"Invalid email address {email}");
        }
        return Success("email reset token sent to that email");
    }

    [Route("/changepassword"), HttpPost]
    public IActionResult ChangePassword(ChangePasswordModel model)
    {
        if(!ModelState.IsValid)
        {
            return Error(validationErrorMessage(ModelState));
        }
        return Success("email reset token sent to that email");
    }
}