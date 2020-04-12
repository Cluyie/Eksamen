using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Models.Mail;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Mail.Domain.Commands;
using UCLDreamTeam.Mail.Domain.Events;
using UCLDreamTeam.Mail.Domain.Models;
using UCLDreamTeam.Mail.Domain.Properties;
using UCLToolBox;

namespace UCLDreamTeam.Mail.Domain.CommandHandlers
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, bool>
    {
        private readonly IEventBus _eventBus;
        private readonly SmtpClient _smtpClient;

        public SendEmailCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _smtpClient = ConfigureSmtpClient();
        }

        public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            MailMessage mailMessage = null;
            try
            {
                mailMessage = GenerateMail(request.MailModel);
                _smtpClient.Send(mailMessage);
                _eventBus.PublishEvent(new EmailSendSuccessEvent(mailMessage));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _eventBus.PublishEvent(new SendFailedEvent(mailMessage, e));
                throw;
            }
        }

        public SmtpClient ConfigureSmtpClient()
        {
            return new SmtpClient
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

        public MailMessage GenerateMail(MailModel mailModel)
        {
            var mail = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress(Resources.MailService_SenderEmail, Resources.MailService_SenderName),
                Subject = mailModel.Template.GetAttribute<DisplayAttribute>().Name,
                Body = GenerateMailContentFromTemplate(mailModel)
            };
            //Setting To and CC
            mail.To.Add(new MailAddress(mailModel.Recipent.Email, mailModel.Recipent.FirstName));

            return mail;
        }

        private string GenerateMailContentFromTemplate(MailModel mailModel)
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

            switch (mailModel.Template)
            {
                case Template.BookingConfirmation:
                    stringBuilder.AppendLine($"<p>Hej {mailModel.Recipent.FirstName}</p>");
                    stringBuilder.AppendLine("<br/>");
                    stringBuilder.AppendLine(
                        $"<p>Du har reserveret {mailModel.Resource.Name} d. {mailModel.Reservation.Timeslot.FromDate:d} i tidsrummet " +
                        $"{mailModel.Reservation.Timeslot.FromDate:HH:mm} til {mailModel.Reservation.Timeslot.ToDate:HH:mm}.</p>");
                    stringBuilder.AppendLine("<br/>");
                    stringBuilder.AppendLine("<p>Tak fordi du bruger UCL Dream Team booking!</p>");
                    break;
                case Template.CancellationConfirmation:
                    stringBuilder.AppendLine($"<p>Hej {mailModel.Recipent.FirstName}</p>");
                    stringBuilder.AppendLine("<br/>");
                    stringBuilder.AppendLine(
                        $"<p>Du har aflyst din reservation af {mailModel.Resource.Name} d. {mailModel.Reservation.Timeslot.FromDate:d} i tidsrummet " +
                        $"{mailModel.Reservation.Timeslot.FromDate:HH:mm} til {mailModel.Reservation.Timeslot.ToDate:HH:mm}.</p>");
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