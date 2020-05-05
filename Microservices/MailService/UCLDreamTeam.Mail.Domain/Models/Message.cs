using System;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Mail.Domain.Models
{
    public class Message : IMessage
    {
        public Guid Id { get; set; }
        public Guid GroopId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Seen { get; set; }
    }
}