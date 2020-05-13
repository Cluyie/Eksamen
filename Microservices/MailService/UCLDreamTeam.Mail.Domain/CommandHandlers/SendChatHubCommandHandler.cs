using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Mail.Domain.Commands;
using UCLDreamTeam.Mail.Domain.Events;
using UCLDreamTeam.Mail.Domain.Interfaces;
using UCLDreamTeam.Mail.Domain.Models;
using UCLDreamTeam.Mail.Domain.Properties;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLDreamTeam.SharedInterfaces.Mail;
using UCLToolBox;

namespace UCLDreamTeam.Mail.Domain.CommandHandlers
{
    public class SendChatHubCommandHandler : IRequestHandler<SendChatLogCommand, bool>
    {
        private readonly IEventBus _eventBus;
        private readonly IGenericRepository<User> _userRepository;
        private readonly SmtpClient _smtpClient;

        public SendChatHubCommandHandler(IEventBus eventBus, IGenericRepository<User> userRepository)
        {
            _eventBus = eventBus;
            _userRepository = userRepository;
            _smtpClient = ConfigureSmtpClient();
        }

        public async Task<bool> Handle(SendChatLogCommand request, CancellationToken cancellationToken)
        {
            MailMessage mailMessage = null;
            try
            {
                mailMessage = await GenerateMail(request.TicketDTO);
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

        public async Task<MailMessage> GenerateMail(TicketDTO ticketDTO)
        {
            var mail = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress(Resources.MailService_SenderEmail, Resources.MailService_SenderName),
                Subject = "Chatlog",
                Body = GenerateMailContentFromTemplate(ticketDTO)
            };
            //Setting To and CC
            var user = await _userRepository.GetById(ticketDTO.Reservation.UserId);
            mail.To.Add(new MailAddress(user.Email, $"{user.FirstName} {user.LastName}"));

            return mail;
        }

        private string GenerateMailContentFromTemplate(TicketDTO ticketDTO)
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
            stringBuilder.AppendLine("<main role='main' class='pb-3'>");

            stringBuilder.AppendLine($"<p>Support chat omkring {ticketDTO.Resource.Name}.</p>");
            stringBuilder.AppendLine($"<p>Omkring reservation d. {ticketDTO.Reservation.Timeslot.FromDate:d} fra {ticketDTO.Reservation.Timeslot.FromDate:hh:mm} til {ticketDTO.Reservation.Timeslot.FromDate:hh:mm}.</p>");

            stringBuilder.AppendLine($"<p>Start på chat log d. {ticketDTO.Ticket.Messages.FirstOrDefault().TimeStamp:d}</p>");
            stringBuilder.AppendLine("</br>");

            foreach (var message in ticketDTO.Ticket.Messages)
            {

                stringBuilder.AppendLine($"<p>From {message.UserId}: {message.TimeStamp:hh:mm} - {message.Text}</p>");
                stringBuilder.AppendLine("</br>");

                return stringBuilder.ToString();
            }

            stringBuilder.AppendLine("<p>Slut på chat log</p>");
            stringBuilder.AppendLine("</br>");
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

        private string GenerateMailContentFromTemplate(Reservation reservation, Template template)
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
            stringBuilder.AppendLine("<main role='main' class='pb-3'>");

            switch (template)
            {
                case Template.BookingConfirmation:
                    stringBuilder.AppendLine($"<p>Hej {reservation.Recipent.FirstName}</p>");
                    stringBuilder.AppendLine("<br/>");
                    stringBuilder.AppendLine(
                        $"<p>Du har reserveret {reservation.Resource.Name} d. {reservation.Timeslot.FromDate:d} i tidsrummet " +
                        $"{reservation.Timeslot.FromDate:HH:mm} til {reservation.Timeslot.ToDate:HH:mm}.</p>");
                    stringBuilder.AppendLine("<br/>");
                    stringBuilder.AppendLine("<p>Tak fordi du bruger UCL Dream Team booking!</p>");
                    break;
                case Template.CancellationConfirmation:
                    stringBuilder.AppendLine($"<p>Hej {reservation.Recipent.FirstName}</p>");
                    stringBuilder.AppendLine("<br/>");
                    stringBuilder.AppendLine(
                        $"<p>Du har aflyst din reservation af {reservation.Resource.Name} d. {reservation.Timeslot.FromDate:d} i tidsrummet " +
                        $"{reservation.Timeslot.FromDate:HH:mm} til {reservation.Timeslot.ToDate:HH:mm}.</p>");
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