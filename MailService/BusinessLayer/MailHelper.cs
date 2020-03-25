using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Models;
using Models.Interfaces;
using Models.Mail;
namespace BusinessLayer
{
    public class MailHelper : IMailHelper
    {
        private SmtpClient _smtpClient;

        public MailHelper()
        {
            ConfigureSmtpClient();
        }

        public void ConfigureSmtpClient()
        {
            _smtpClient = new SmtpClient(Properties.Resources.MailService_HostName, Convert.ToInt32(Properties.Resources.MailService_HostPort))
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Properties.Resources.MailService_SenderEmail, Properties.Resources.MailService_Password),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };

        }

        public void SendMail(MailMessage mail)
        {
            _smtpClient.Send(mail);
        }

        public MailMessage GenerateMail(User user, string subject, string mailContent)
        {
            MailMessage mail = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress(Properties.Resources.MailService_SenderEmail, Properties.Resources.MailService_SenderName),
                Subject = subject,
                Body = mailContent,
            };
            //Setting To and CC
            mail.To.Add(new MailAddress(user.Email, user.FirstName));

            return mail;
        }
    }
}
