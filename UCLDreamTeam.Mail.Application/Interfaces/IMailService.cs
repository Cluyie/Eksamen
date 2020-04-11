using System.Net.Mail;
using Models.Interfaces;
using UCLDreamTeam.Mail.Domain.Models;

namespace UCLDreamTeam.Mail.Application.Interfaces
{
    public interface IMailService
    {
        public void ConfigureSmtpClient();

        public void SendMail(TemplateViewModel templateViewModel);

        public MailMessage GenerateMail(TemplateViewModel templateViewModel);
    }
}