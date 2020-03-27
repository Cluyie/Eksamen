using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BusinessLayer.Models;
using Models;
using Models.Interfaces;
using Models.Mail;
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
            _smtpClient = new SmtpClient()
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = Properties.Resources.MailService_HostName,
                Port = Properties.Resources.MailService_HostPort,
                Credentials = new NetworkCredential(Properties.Resources.MailService_SenderEmail, Properties.Resources.MailService_Password),
                Timeout = 20000,
            };

        }

        public void SendMail(MailMessage mail)
        {
            _smtpClient.Send(mail);
        }

        public MailMessage GenerateMail(IUser user, string subject, string mailContent)
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
