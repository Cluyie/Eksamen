using System.Net;
using System.Net.Mail;
using BusinessLayer.Properties;
using Models.Interfaces;

namespace BusinessLayer
{
    public class MailHelper
    {
        private SmtpClient _smtpClient;

        public MailHelper()
        {
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
    }
}