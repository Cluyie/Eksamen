using System;
using Models.Interfaces;

namespace UCLDreamTeam.Ticket.Domain.Models
{
    public class Message : IMessage
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Seen { get; set; } = false;
    }
}