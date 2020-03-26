using System.Net.Mail;
using BusinessLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IMailHelper
    {
        void ConfigureSmtpClient();
        MailMessage GenerateMail(User user, string subject, string mailContent);
        void SendMail(MailMessage mail);
    }
}