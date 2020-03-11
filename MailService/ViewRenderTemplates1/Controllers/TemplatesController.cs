using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ViewRenderTemplates.Models;

namespace ViewRenderTemplates.Controllers
{
  public class TemplatesController : Controller
  {
    private readonly ILogger<TemplatesController> _logger;

    public TemplatesController(ILogger<TemplatesController> logger)
    {
      _logger = logger;
    }

    public TemplatesController()
    {
      
    }

    public IActionResult BookingConfirmation()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
