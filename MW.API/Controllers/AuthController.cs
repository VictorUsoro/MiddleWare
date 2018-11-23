using Microsoft.AspNetCore.Mvc;
using MW.Services;

[Route("api/auth")]
public class AuthController : BaseController
{
    public AuthController(IServiceInit services) : base(services)
    {
    }


}