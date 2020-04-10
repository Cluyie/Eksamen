using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Models.Interfaces;
using Models.Mail;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Mail.Domain;
using UCLDreamTeam.Mail.Domain.Models;
using UCLToolBox;

namespace UCLDreamTeam.Mail.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : Controller
    {
        private readonly MailService _mailService;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly ILogger<MailController> _logger;

        public MailController(MailService mailService, ICompositeViewEngine viewEngine, ILogger<MailController> logger)
        {
            _mailService = mailService;
            _viewEngine = viewEngine;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ApiResponse<string>> PostMail([FromBody] TemplateViewModel templateViewModel)
        {
            try
            {
                templateViewModel.Title = templateViewModel.Template.GetAttribute<DisplayAttribute>().Name;
                var mailContent = await RenderViewToString(templateViewModel.Template.ToString(), templateViewModel);
                var mail = _mailService.GenerateMail(templateViewModel.Recipent,
                    templateViewModel.Template.GetAttribute<DisplayAttribute>().Name, mailContent);
                _mailService.SendMail(mail);
            }
            catch (Exception e)
            {
                return new ApiResponse<string>(ApiResponseCode.InternalServerError, e.Message);
            }

            return new ApiResponse<string>(ApiResponseCode.OK, "Email send");
        }

        [HttpPut]
        public async Task<ApiResponse<string>> PostMail(Template template, string recipientId, string resourceId,
            string reservationId)
        {
            var userResponse = _mailService.GetUser(recipientId);
            var reservationResponse = _mailService.GetReservation(reservationId);
            var resourceResponse = _mailService.GetResource(resourceId);
            var templateViewModel = new TemplateViewModel
            {
                Template = template,
                Recipent = userResponse,
                Resource = resourceResponse,
                Reservation = reservationResponse
            };
            return await PostMail(templateViewModel);
        }

        [HttpGet]
        private async Task<string> RenderViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.ActionDescriptor.ActionName;

            ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                var viewResult =
                    _viewEngine.FindView(ControllerContext, viewName, false);

                var viewContext = new ViewContext(
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