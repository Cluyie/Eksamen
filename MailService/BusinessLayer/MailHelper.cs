using System;
using System.Net.Mail;
using System.Threading.Tasks;
using BusinessLayer.Models;
using Models;
using Models.Mail;
namespace BusinessLayer
{
    public class MailHelper: IMailHelper
    {
        private SmtpClient _smtpClient;

        public MailHelper()
        {
            ConfigureSmtpClient();
        }

        public void ConfigureSmtpClient()
        {
            _smtpClient = new SmtpClient(Properties.Resources.MailService_HostName,
              int.Parse(Properties.Resources.MailService_HostPort))
            {
                //Credentials = new System.Net.NetworkCredential(Properties.Resources.MailService_Username,
                //Properties.Resources.MailService_Password, Properties.Resources.MailService_DomainName),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = false
            }; //"laraSMTP", 25);
               // smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
        }

        public void SendMail(MailMessage mail)
        {
            _smtpClient.Send(mail);
        }

        public MailMessage GenerateMail(string mailContent, User user)
        {
            MailMessage mail = new MailMessage
            {
                From = new MailAddress(Properties.Resources.MailService_SenderEmail, "UCL Booking Service"), // "booking@ucl.dk");
            };
            //Setting To and CC
            mail.To.Add(new MailAddress(user.Email, "Tonur"));


            mail.Body = mailContent;

            return mail;
        }
    }
}
