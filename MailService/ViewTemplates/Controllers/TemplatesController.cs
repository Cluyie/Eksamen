using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ViewTemplates.Controllers
{
  public class TemplatesController : Controller
  {
    public ActionResult BookingConfirmation()
    {
      return View();
    }
  }
}