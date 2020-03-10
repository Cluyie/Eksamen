using System;
using System.Net.Mail;
using Models.Mail;

namespace BusinessLayer
{
  public class MailHelper
  {
    public void SendMail(string title, string htmlBody, string recipient, string[] ccRecipients = null)
    {
      SmtpClient smtpClient = new SmtpClient(Properties.Resources.MailService_HostName, int.Parse(Properties.Resources.MailService_HostPort)); //"laraSMTP", 25);

      smtpClient.Credentials = new System.Net.NetworkCredential(Properties.Resources.MailService_Username, Properties.Resources.MailService_Password);
      // smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
      smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
      smtpClient.EnableSsl = true;

      //Setting From, subject and body
      MailMessage mail = new MailMessage
      {
        From = new MailAddress(Properties.Resources.MailService_SenderEmail), // "booking@ucl.dk");
        Subject = title,
        Body = htmlBody,
      };

      //Setting To and CC
      mail.To.Add(new MailAddress(recipient));
      foreach (string ccRecipient in ccRecipients)
      {
        mail.CC.Add(new MailAddress(ccRecipient));
      }

      smtpClient.Send(mail);
    }

    public void SendMail(MailContent content)
    {
      SmtpClient smtpClient = new SmtpClient(Properties.Resources.MailService_HostName,
        int.Parse(Properties.Resources.MailService_HostPort))
      {
        Credentials = new System.Net.NetworkCredential(Properties.Resources.MailService_Username,
          Properties.Resources.MailService_Password),
        DeliveryMethod = SmtpDeliveryMethod.Network,
        EnableSsl = true
      }; //"laraSMTP", 25);

      // smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials

      //Setting From, subject and body
      MailMessage mail = new MailMessage
      {
        From = new MailAddress(Properties.Resources.MailService_SenderEmail), // "booking@ucl.dk");
        Subject = content.TitleContent,
        Body = content.BodyContent,
      };

      //Setting To and CC
      mail.To.Add(new MailAddress(content.Recipient));
      foreach (string ccRecipient in content.CcRecipients)
      {
        mail.CC.Add(new MailAddress(ccRecipient));
      }


      smtpClient.Send(mail);
    }
  }
}
