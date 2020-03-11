using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;

namespace BusinessLayer
{
  public class ViewRenderService
  {

    public async Task<string> RenderViewToString<TModel>(ControllerContext controllerContext, string viewName, TModel model)
    {
      ViewEngineResult viewEngineResult = ViewEngines.Engines.FindView(controllerContext, viewName, null);
      try
      {
        IView view = viewEngineResult.View;

        using (StringWriter sw = new StringWriter())
        {
          var viewContext = new ViewContext(controllerContext, view, new ViewDataDictionary<TModel>(model), new TempDataDictionary(), sw);
          view.Render(viewContext, sw);
          return sw.ToString();
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
    }
  }
}
