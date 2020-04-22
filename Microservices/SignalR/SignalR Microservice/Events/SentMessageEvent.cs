using RabbitMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Microservice.Events
{
    public class SentMessageEvent : Event
    {
        public string Username { get; set; }
        public string Content { get; set; }

        public SentMessageEvent(string username, string content)
        {
            Username = username;
            Content = content;
        }
    }
}
