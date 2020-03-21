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

        public void SendMail(MailMessage mail)
        {
            return;
        }

        public MailMessage GenerateMail(string mailContent, User user)
        {
            MailMessage mail = new MailMessage
            {
                From = new MailAddress("booking@ucl.dk", "UCL Booking Service")
            };
            //Setting To and CC
            mail.To.Add(new MailAddress("chriskpedersen@hotmail.com", "Tonur"));


            mail.Body = mailContent;

            return mail;
        }
    }
}
