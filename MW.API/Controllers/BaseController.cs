using Microsoft.AspNetCore.Mvc;
using MW.Application;
using MW.Services;

public class BaseController : Controller
{
    public readonly IServiceInit _services;
    public BaseController (IServiceInit services)
    {
        _services = services;
    }

    #region ResponseHelper

      public IActionResult Success(object data, string message = null)
      {
         return Ok(new JSendResponseModel<object> { Data = data, Message = message, Status = "00" });
      }

      public IActionResult Error(string message)
      {
         return Ok(new JSendResponseModel<object> { Data = null, Message = message, Status = "99" });
      }

      public IActionResult Critical()
      {
         return Ok(new JSendResponseModel<object> { Data = null, Status = "99", Message = "An error occured" });
      }

      #endregion

}