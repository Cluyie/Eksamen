using RabbitMQ.Bus.Commands;
using System.Collections.Generic;
using UCLDreamTeam.Mail.Domain.Models;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLDreamTeam.SharedInterfaces.Mail;

namespace UCLDreamTeam.Mail.Domain.Commands
{
    public class SendChatLogCommand : Command
    {
        public TicketDTO TicketDTO { get; set; }

        public SendChatLogCommand(TicketDTO ticketDTO)
        {
            TicketDTO = ticketDTO;
        }
    }
}