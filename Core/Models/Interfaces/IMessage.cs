using System;

namespace UCLDreamTeam.SharedInterfaces.Interfaces
{
    public interface IMessage
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GroopId { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}