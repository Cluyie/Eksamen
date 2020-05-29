using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using UCLDreamTeam.Mail.Api.Models;
using UCLDreamTeam.Mail.Application.Interfaces;
using UCLDreamTeam.Mail.Domain.Models;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLDreamTeam.SharedInterfaces.Mail;

namespace UCLDreamTeam.Mail.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly ILogger<MailController> _logger;

        public MailController(IMailService mailService, ILogger<MailController> logger)
        {
            _mailService = mailService;
            _logger = logger;
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ApiResponse<Reservation>> SendReservationNotification([FromBody] Reservation reservation, Template template)
        {
            try
            {
                await _mailService.SendMail(reservation, template);
                return new ApiResponse<Reservation>(ApiResponseCode.OK, reservation);//Ok(reservation);
            }
            catch (Exception e)
            {
                return new ApiResponse<Reservation>(ApiResponseCode.InternalServerError) { Exception = e };
            }

        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ApiResponse<string>> SendChatLog([FromBody] TicketDTO ticketDTO)
        {
            try
            {
                await _mailService.SendChatLog(ticketDTO);
                return new ApiResponse<string>(ApiResponseCode.OK, null);//Ok();
            }
            catch (Exception e)
            {
                return new ApiResponse<string>(ApiResponseCode.InternalServerError) { Exception = e };
            }

        }

        //Skal ikke bruges, event skal indeholde alle informationer, så kun overstående post skal blive brugt
        //[HttpPost("PostRoute/")]
        //public async Task<IActionResult> PostMail([FromRoute] Template template, string recipientId, string resourceId,
        //    string reservationId)
        //{
        //    var userResponse = _mailService.GetUser(recipientId);
        //    var reservationResponse = _mailService.GetReservation(reservationId);
        //    var resourceResponse = _mailService.GetResource(resourceId);
        //    var reservation = new Reservation
        //    {
        //        Template = template,
        //        Recipent = userResponse,
        //        Resource = resourceResponse,
        //        Reservation = reservationResponse
        //    };
        //    return await PostMail(reservation);
        //}

        //private async Task<string> RenderViewToString(string viewName, object model)
        //{
        //    if (string.IsNullOrEmpty(viewName))
        //        viewName = ControllerContext.ActionDescriptor.ActionName;

        //    ViewData.Model = model;

        //    using (var writer = new StringWriter())
        //    {
        //        var viewResult =
        //            _viewEngine.FindView(ControllerContext, viewName, false);

        //        var viewContext = new ViewContext(
        //            ControllerContext,
        //            viewResult.View,
        //            ViewData,
        //            TempData,
        //            writer,
        //            new HtmlHelperOptions()
        //        );

        //        await viewResult.View.RenderAsync(viewContext);

        //        return writer.GetStringBuilder().ToString();
        //    }
        //}
    }
}