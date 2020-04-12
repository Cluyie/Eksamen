using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using RabbitMQ.Bus.Bus.Interfaces;
using RabbitMQ.Bus.Commands;
using UCLDreamTeam.Mail.Domain.Models;

namespace UCLDreamTeam.Mail.Domain.Commands
{
    public class SendEmailCommand : Command
    {
        public MailModel MailModel { get; }

        public SendEmailCommand(MailModel mailModel)
        {
            MailModel = mailModel;
        }
    }
}
