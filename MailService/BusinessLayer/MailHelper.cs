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
    public class MailHelper : IMailHelper
    {
        private SmtpClient _smtpClient;

        public MailHelper()
        {
            ConfigureSmtpClient();
        }

        public void ConfigureSmtpClient()
        {
            _smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("DreamTeamUCL@gmail.com", "maauuhyrjpejtmvw"),
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
                From = new MailAddress("DreamTeamUCL@gmail.com", "UCL Booking Service"),
                Subject = subject,
                Body = mailContent, // "booking@ucl.dk");
            };
            //Setting To and CC
            mail.To.Add(new MailAddress(user.Email, user.FirstName));

            return mail;
        }
    }
}
