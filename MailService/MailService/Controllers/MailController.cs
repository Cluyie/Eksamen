using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.Mail;
using UCLToolBox;

namespace MailService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class MailController : ControllerBase
  {
    private readonly ILogger<MailController> _logger;
    private ApiClientProxy _proxy;
    private MailHelper _mailHelper;

    public MailController(ILogger<MailController> logger, ApiClientProxy proxy, MailHelper mailHelper)
    {
      _logger = logger;
      _proxy = proxy;
      _mailHelper = mailHelper;
    }

    [HttpPost]
    public ApiResponse<string> PostMail(int recipientId, [FromBody] MailContent content)
    {
      try
      {
        var user = _proxy.Get<User>("User/GetProfileById/" + recipientId);
        content.Recipient = user.Email;
        _mailHelper.SendMail(content);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
        return new ApiResponse<string>(ApiResponseCode.InternalServerError, e.Message);
      }
      return new ApiResponse<string>(ApiResponseCode.OK, "Email send");
    }
  }
}
