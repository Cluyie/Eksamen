using RabbitMQ.Bus.Bus.Interfaces;
using RabbitMQ.Bus.Events;
using SignalR_Microservice.Commands;
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
        private readonly IEventBus _eventBus;

        public ChatLoggingService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task SendMessageAsync(Message message)
        {
            var command = new CreateSentMessageCommand(message.Username, message.Content);
            await _eventBus.SendCommand(command);
        }
    }
}
