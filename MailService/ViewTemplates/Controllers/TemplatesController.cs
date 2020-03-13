using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewTemplates.Models;

namespace ViewTemplates.Controllers
{
  public class TemplatesController : Controller
  {
    public ActionResult BookingConfirmation()
    {
      return View();
    }

    public string RenderViewToString(string viewName)
    {
      ViewData.Model = new TemplateViewModel();
      using (var sw = new StringWriter())
      {
        var viewResult = ViewEngines.Engines.FindView(this.ControllerContext, viewName, null);
        var viewContext = new ViewContext(this.ControllerContext, viewResult.View,
          ViewData, TempData, sw);
        viewResult.View.Render(viewContext, sw);
        viewResult.ViewEngine.ReleaseView(this.ControllerContext, viewResult.View);
        return sw.GetStringBuilder().ToString();
      }
    }
  }
}