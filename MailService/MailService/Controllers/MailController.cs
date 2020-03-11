using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace MailService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class MailController : ControllerBase
  {
    private readonly ILogger<MailController> _logger;

    public MailController(ILogger<MailController> logger)
    {
      _logger = logger;
    }

    [HttpPost]
    public ApiResponse<string> PostMail()
    {
      return new ApiResponse<string>(ApiResponseCode.OK, "Email send");
    }
  }
}
