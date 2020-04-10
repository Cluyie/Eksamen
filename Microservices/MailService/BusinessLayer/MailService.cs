using System.Net;
using System.Net.Mail;
using Models.Interfaces;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Mail.Domain.Models;
using UCLDreamTeam.Mail.Domain.Properties;

namespace BusinessLayer
{
    public class MailService
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

        public void SendMail(MailMessage mail)
        {
            _smtpClient.Send(mail);
        }

        public MailMessage GenerateMail(IUser user, string subject, string mailContent)
        {
            var mail = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress(Resources.MailService_SenderEmail, Resources.MailService_SenderName),
                Subject = subject,
                Body = mailContent
            };
            //Setting To and CC
            mail.To.Add(new MailAddress(user.Email, user.FirstName));

            return mail;
        }

        public User GetUser(string recipientId)
        {
            throw new System.NotImplementedException();
        }

        public Reservation GetReservation(string reservationId)
        {
            throw new System.NotImplementedException();
        }

        public Resource GetResource(string resourceId)
        {
            throw new System.NotImplementedException();
        }
    }
}