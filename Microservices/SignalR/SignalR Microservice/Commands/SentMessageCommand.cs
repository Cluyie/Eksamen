using RabbitMQ.Bus.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Microservice.Commands
{
    public class SentMessageCommand : Command
    {
        public string Username { get; set; }
        public string Content { get; set; }
    }
}
