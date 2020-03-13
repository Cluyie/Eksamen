using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Models;
using Models.Mail;
using ViewTemplates.Controllers;
using ViewTemplates.Models;

namespace BusinessLayer
{
    public class MailHelper
    {
        private SmtpClient _smtpClient;
        private TemplatesController _templatesController;

        public MailHelper()
        {

        }

        public MailHelper(TemplatesController templatesController)
        {
            ConfigureSmtpClient();
            _templatesController = templatesController;
        }

        private void ConfigureSmtpClient()
        {
            _smtpClient = new SmtpClient(Properties.Resources.MailService_HostName,
              int.Parse(Properties.Resources.MailService_HostPort))
            {
                Credentials = new System.Net.NetworkCredential(Properties.Resources.MailService_Username,
                Properties.Resources.MailService_Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            }; //"laraSMTP", 25);
               // smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
        }

        public void SendMail(MailMessage mail)
        {
            _smtpClient.Send(mail);
        }

        public async Task<MailMessage> GenerateMail(Template template, User user)
        {
            MailMessage mail = new MailMessage
            {
                From = new MailAddress(Properties.Resources.MailService_SenderEmail), // "booking@ucl.dk");
            };
            //Setting To and CC
            mail.To.Add(new MailAddress(user.Email));


            mail.Body = "Hej"; //_templatesController.RenderViewToString(template.ToString());

            return mail;
        }
    }
}
