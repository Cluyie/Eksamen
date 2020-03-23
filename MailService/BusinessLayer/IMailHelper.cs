using System;
using System.Net.Mail;
using BusinessLayer.Models;
using Models.Interfaces;

namespace BusinessLayer
{
    public interface IMailHelper
    {
        void ConfigureSmtpClient();
        MailMessage GenerateMail(User user, string subject, string mailContent);
        void SendMail(MailMessage mail);
    }
}