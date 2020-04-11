using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Text;
using Models.Interfaces;
using Models.Mail;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Mail.Application.Interfaces;
using UCLDreamTeam.Mail.Application.Properties;
using UCLDreamTeam.Mail.Domain.Commands;
using UCLDreamTeam.Mail.Domain.Events;
using UCLDreamTeam.Mail.Domain.Models;
using UCLToolBox;

namespace UCLDreamTeam.Mail.Application.Services
{
    public class MailService : IMailService
    {
        private SmtpClient _smtpClient;
        private readonly IEventBus _eventBus;

        public MailService(IEventBus eventBus)
        {
            _eventBus = eventBus;
            ConfigureSmtpClient();
        }


        public void ConfigureSmtpClient()
        {
            _smtpClient = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = Resources.MailService_HostName,
                Port = Resources.MailService_HostPort,
                Credentials = new NetworkCredential(Resources.MailService_SenderEmail, Resources.MailService_Password),
                Timeout = 20000
            };
        }

        public async void SendMail(TemplateViewModel templateViewModel)
        {
            await _eventBus.SendCommand(new SendEmailCommand(GenerateMail(templateViewModel), _smtpClient));
        }

        public MailMessage GenerateMail(TemplateViewModel templateViewModel)
        {
            var mail = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress(Resources.MailService_SenderEmail, Resources.MailService_SenderName),
                Subject = templateViewModel.Template.GetAttribute<DisplayAttribute>().Name,
                Body = GenerateMailFromTemplate(templateViewModel)
            };
            //Setting To and CC
            mail.To.Add(new MailAddress(templateViewModel.Recipent.Email, templateViewModel.Recipent.FirstName));

            return mail;
        }

        private string GenerateMailFromTemplate(TemplateViewModel templateViewModel)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<!DOCTYPE html>");
            stringBuilder.AppendLine("<html lang='en'>");
            stringBuilder.AppendLine("<head>");
            stringBuilder.AppendLine("    <meta charset='utf-8'/>");
            stringBuilder.AppendLine("    <meta name='viewport' content='width=device-width, initial-scale=1.0'/>");
            stringBuilder.AppendLine("    <link rel='stylesheet' href='~/lib/bootstrap/dist/css/bootstrap.min.css'/>");
            stringBuilder.AppendLine("    <link rel='stylesheet' href='~/css/site.css'/>");
            stringBuilder.AppendLine("    <img height='200' src='https://scontent-arn2-1.xx.fbcdn.net/v/t1.0-9/69509664_10157971554196189_8594223189859500032_o.jpg?_nc_cat=104&_nc_sid=ca434c&_nc_ohc=YrzrfoI3WOIAX835Gu0&_nc_ht=scontent-arn2-1.xx&oh=14aa2918b9f7537c0a1b1cef07481b8d&oe=5E9FBF5C'/>");
            stringBuilder.AppendLine("</head>");
            stringBuilder.AppendLine("<body>");
            stringBuilder.AppendLine("<header>");
            stringBuilder.AppendLine("</header>");
            stringBuilder.AppendLine("<div class='container'>");
            stringBuilder.AppendLine("<main role='main' class='pb-3>");

            switch (templateViewModel.Template)
            {
                case Template.BookingConfirmation:
                    stringBuilder.AppendLine($"<p>Hej {templateViewModel.Recipent.FirstName}</p>");
                    stringBuilder.AppendLine("<br/>");
                    stringBuilder.AppendLine($"<p>Du har reserveret {templateViewModel.Resource.Name} d. {templateViewModel.Reservation.Timeslot.FromDate:d} i tidsrummet " +
                                             $"{templateViewModel.Reservation.Timeslot.FromDate:HH:mm} til {templateViewModel.Reservation.Timeslot.ToDate:HH:mm}.</p>");
                    stringBuilder.AppendLine("<br/>");
                    stringBuilder.AppendLine("<p>Tak fordi du bruger UCL Dream Team booking!</p>");
                    break;
                case Template.CancellationConfirmation:
                    stringBuilder.AppendLine($"<p>Hej {templateViewModel.Recipent.FirstName}</p>");
                    stringBuilder.AppendLine("<br/>");
                       stringBuilder.AppendLine($"<p>Du har aflyst din reservation af {templateViewModel.Resource.Name} d. {templateViewModel.Reservation.Timeslot.FromDate:d} i tidsrummet " +
                                             $"{templateViewModel.Reservation.Timeslot.FromDate:HH:mm} til {templateViewModel.Reservation.Timeslot.ToDate:HH:mm}.</p>");
                    stringBuilder.AppendLine("<br/>");
                    stringBuilder.AppendLine("<p>Du kan frit reserverer en anden gang.</p>");
                    break;
            }
            stringBuilder.AppendLine("</main>");
            stringBuilder.AppendLine("</div>");
            stringBuilder.AppendLine("<footer class='border-top footer text-muted'>");
            stringBuilder.AppendLine("    <div class='container'>");
            stringBuilder.AppendLine("        &copy; 2020 - UCL Booking - Dette er en automatisk besked.");
            stringBuilder.AppendLine("    </div>");
            stringBuilder.AppendLine("</footer>");
            stringBuilder.AppendLine("<script src='~/lib/jquery/dist/jquery.min.js'></script>");
            stringBuilder.AppendLine("<script src='~/lib/bootstrap/dist/js/bootstrap.bundle.min.js'></script>");
            stringBuilder.AppendLine("<script src='~/js/site.js' asp-append-version='true'></script>");
            stringBuilder.AppendLine("</body>");
            stringBuilder.AppendLine("</html>");



            return stringBuilder.ToString();
        }
    }
}