using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Mail
{
  public class MailTemplate
  {
    public enum Templates : int
    {
      BookingConfirmation = 1,
      CancellationConfirmation = 2,
    }

    public Templates Template { get; set; } 
    public string Recipient { get; set; }
    public string[] CcRecipients { get; set; } = null;
    public string TitleContent { get; set; }
    public string BodyContent { get; set; }
  }
}
