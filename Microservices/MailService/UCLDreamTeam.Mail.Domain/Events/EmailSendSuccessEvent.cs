using System.Net.Mail;
using RabbitMQ.Bus.Events;

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