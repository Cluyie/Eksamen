using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using BusinessLayer.Models;

namespace BusinessLayer
{
    public class MockMailHelper : IMailHelper
    {
        private SmtpClient _smtpClient;

        public MockMailHelper()
        {
            ConfigureSmtpClient();
        }

        public void ConfigureSmtpClient()
        {
            return;
        }

        public MailMessage GenerateMail(User user, string subject, string mailContent)
        {
            MailMessage mail = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress("booking@ucl.dk", "UCL Booking Service"),
                Subject = subject,
                Body = mailContent,
            };
            //Setting To and CC
            mail.To.Add(new MailAddress("chriskpedersen@hotmail.com", "Tonur"));



            return mail;
        }

        public void SendMail(MailMessage mail)
        {
            return;
        }
    }
}
