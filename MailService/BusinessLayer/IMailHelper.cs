using System.Net.Mail;
using BusinessLayer.Models;

namespace BusinessLayer
{
    public interface IMailHelper
    {
        void ConfigureSmtpClient();
        MailMessage GenerateMail(string mailContent, User user);
        void SendMail(MailMessage mail);
    }
}