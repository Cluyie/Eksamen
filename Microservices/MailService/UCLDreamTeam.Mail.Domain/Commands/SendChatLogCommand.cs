using RabbitMQ.Bus.Commands;
using System.Collections.Generic;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLDreamTeam.SharedInterfaces.Mail;

namespace UCLDreamTeam.Mail.Domain.Commands
{
    public class SendChatLogCommand : Command
    {
        public IEnumerable<IMessage> Messages { get; }
        public Template Template { get; }

        public SendChatLogCommand(IEnumerable<IMessage> messages, Template template)
        {
            Messages = messages;
            Template = template;
        }
    }
}