using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using BusinessLayer;
using BusinessLayer.Models;
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
        private readonly MailHelper _mailHelper;
        private readonly ICompositeViewEngine _viewEngine;

        public MailController(ILogger<MailController> logger, ApiClientProxy proxy, MailHelper mailHelper, ICompositeViewEngine viewEngine)
        {
            _logger = logger;
            _proxy = proxy;
            _mailHelper = mailHelper;
            _viewEngine = viewEngine;
        }

        [HttpPost]
        public async Task<ApiResponse<string>> PostMail(TemplateViewModel templateViewModel)
        {
            try
            {
                templateViewModel.Title = templateViewModel.Template.GetAttribute<DisplayAttribute>().Name;
                string mailContent = await RenderViewToString(templateViewModel.Template.ToString(), templateViewModel);
                MailMessage mail = _mailHelper.GenerateMail(templateViewModel.Recipent, templateViewModel.Template.GetAttribute<DisplayAttribute>().Name, mailContent);
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
            Login();

            ApiResponse<User> userResponse = _proxy.Get<ApiResponse<User>>("User/guid=" + recipientId);
            //new ApiResponse<User>(ApiResponseCode.OK, new User{ Id = Guid.NewGuid(), UserName = "Tonur", FirstName = "Christoffer", LastName = "Pedersen", Address = "Østerbrogade 20", Email = "chriskpedersen@hotmail.com", });
            ApiResponse<Reservation> reservationResponse =
              _proxy.Get<ApiResponse<Reservation>>("Reservation/guid=" + reservationId);
            //  new ApiResponse<Reservation>(ApiResponseCode.OK, new Reservation{UserId = userResponse.Value.Id, Timeslot = new ReserveTime{FromDate = DateTime.Today.AddHours(8), ToDate = DateTime.Today.AddHours(16)}});
            ApiResponse<Resource> resourceResponse = _proxy.Get<ApiResponse<Resource>>("Resource/guid=" + resourceId);
            // new ApiResponse<Resource>(ApiResponseCode.OK, new Resource{Name = "Hansens rengøringsservice", Reservations = new List<Reservation>{reservationResponse.Value}});
            TemplateViewModel templateViewModel = new TemplateViewModel
            {
                Template = template,
                Recipent = userResponse.Value,
                Resource = resourceResponse.Value,
                Reservation = reservationResponse.Value
            };
            return await PostMail(templateViewModel);
        }

        private void Login()
        {
            var response =  _proxy.PostAsync(@"Auth/Login", new Login{UsernameOrEmail = "Tonur", Password = "ole12345"}).Result;
            var result =  ApiClientProxy.ReadAnswerAsync<ApiResponse<string>>(response);
            var token = _proxy.Get<ApiResponse<string>>("Auth/Login");
            _proxy.httpClient.DefaultRequestHeaders.Add("Authorization", token.Value);
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
