using System;

namespace SignalR_Microservice.Commands
{
    public class CreateSentMessageCommand : SentMessageCommand
    {
        public CreateSentMessageCommand(Guid id, Guid ticketId, Guid userId, string text, DateTime timestamp, bool seen)
        {
            Id = id;
            TicketId = ticketId;
            UserId = userId;
            Text = text;
            TimeStamp = timestamp;
            Seen = seen;
        }
    }
}