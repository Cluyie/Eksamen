using System;
using System.ComponentModel.DataAnnotations.Schema;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Ticket.Domain.Models.Event
{
    public class MessageEvent : RabbitMQ.Bus.Events.Event, IMessage
    {
        public Guid Id { get; set; }
        public Guid GroopId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Seen { get; set; }
    }
}