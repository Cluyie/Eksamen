using RabbitMQ.Bus.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Microservice.Commands
{
    public class SentMessageCommand : Command
    {
        public Guid Id { get; set; }
        public Guid TicketId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Seen { get; set; } = false;
    }
}
