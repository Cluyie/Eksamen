using System.Net.Mail;
using RabbitMQ.Bus.Events;
using UCLDreamTeam.Mail.Domain.Models;

namespace UCLDreamTeam.Mail.Domain.Events
{
    public class EmailSendSuccessEvent : Event
    {
        public MailMessage MailMessage { get; }

        public EmailSendSuccessEvent(MailMessage mailMessage)
        {
            MailMessage = mailMessage;
        }
    }
}