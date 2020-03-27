using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using BusinessLayer;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using MailService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Extensions;
using Models;
using Models.Interfaces;
using Models.Mail;
using UCLToolBox;

namespace MailService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class MailController : Controller
  {
    private readonly ILogger<MailController> _logger;
    private readonly ApiClientProxy _proxy;
    private readonly IMailHelper _mailHelper;
    private readonly ICompositeViewEngine _viewEngine;

    public MailController(ILogger<MailController> logger, ApiClientProxy proxy, IMailHelper mailHelper, ICompositeViewEngine viewEngine)
    {
      _logger = logger;
      _proxy = proxy;
      _mailHelper = mailHelper;
      _viewEngine = viewEngine;
    }

    [HttpPost]
    public async Task<ApiResponse<string>> PostMail(ITemplateViewModel templateViewModel)
    {
      try
      {
        string mailContent = await RenderViewToString(templateViewModel.Template.ToString(), templateViewModel);
        MailMessage mail = _mailHelper.GenerateMail(templateViewModel.Recipient.Value, templateViewModel.Template.GetAttribute<DisplayAttribute>().Name, mailContent);
        _mailHelper.SendMail(mail);
      }
      catch (Exception e)
      {
        return new ApiResponse<string>(ApiResponseCode.InternalServerError, e.Message);
      }
      return new ApiResponse<string>(ApiResponseCode.OK, "Email send");
    }

    [HttpPost]
    public async Task<ApiResponse<string>> PostMail(Template template, string recipientId, string resourceId,
      string reservationId)
    {
      ApiResponse<User> userResponse = _proxy.Get<ApiResponse<User>>("User/guid=" + recipientId);
      //new ApiResponse<User>(ApiResponseCode.OK, new User{ Id = Guid.NewGuid(), UserName = "Tonur", FirstName = "Christoffer", LastName = "Pedersen", Address = "Østerbrogade 20", Email = "chriskpedersen@hotmail.com", });
      ApiResponse<Reservation> reservationResponse =
        _proxy.Get<ApiResponse<Reservation>>("Reservation/guid=" + reservationId);
      //  new ApiResponse<Reservation>(ApiResponseCode.OK, new Reservation{UserId = userResponse.Value.Id, Timeslot = new ReserveTime{FromDate = DateTime.Today.AddHours(8), ToDate = DateTime.Today.AddHours(16)}});
      ApiResponse<Resource> resourceResponse = _proxy.Get<ApiResponse<Resource>>("Resource/guid=" + resourceId);
      // new ApiResponse<Resource>(ApiResponseCode.OK, new Resource{Name = "Hansens rengøringsservice", Reservations = new List<Reservation>{reservationResponse.Value}});
      TemplateViewModel templateViewModel = new TemplateViewModel
      {
        Title = template.GetAttribute<DisplayAttribute>().Name,
        User = userResponse.Value,
        Resource = resourceResponse.Value,
        Reservation = reservationResponse.Value
      };
      return PostMail(templateViewModel);
    }

    [HttpGet]
    private async Task<string> RenderViewToString(string viewName, object model)
    {
      if (string.IsNullOrEmpty(viewName))
        viewName = ControllerContext.ActionDescriptor.ActionName;

      ViewData.Model = model;

      using (var writer = new StringWriter())
      {
        ViewEngineResult viewResult =
            _viewEngine.FindView(ControllerContext, viewName, false);

        ViewContext viewContext = new ViewContext(
            ControllerContext,
            viewResult.View,
            ViewData,
            TempData,
            writer,
            new HtmlHelperOptions()
        );

        await viewResult.View.RenderAsync(viewContext);

        return writer.GetStringBuilder().ToString();
      }
    }
  }
}
