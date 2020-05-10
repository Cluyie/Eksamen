using RabitMQEasy;
using SignalR_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Message = SignalR_Microservice.Models.Message;


namespace SignalR_Microservice.Services
{
    public class ChatLoggingService : IChatLoggingService
    {
        private readonly RabitMQPublicer _eventBus;

        public ChatLoggingService(RabitMQPublicer eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task SendMessageAsync(Message message)
        {
            //var command = new CreateSentMessageCommand(message.Id, message.GroopId, message.UserId, message.Text,
            //    message.TimeStamp, message.Seen);
            //await _eventBus.SendCommand(command);
        }
    }
}