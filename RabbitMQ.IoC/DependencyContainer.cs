using System;
using Consumer.Domain.EventHandlers;
using Consumer.Domain.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Producer.Domain.CommandHandlers;
using Producer.Domain.Commands;
using Producer.Domain.Services;
using Producer.Domain.Services.Interfaces;
using RabbitMQ.Bus.Bus;
using RabbitMQ.Bus.Bus.Interfaces;

namespace RabbitMQ.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterRabbitMq(this IServiceCollection services)
        {
            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMqBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMqBus(sp.GetService<IMediator>(), scopeFactory);
            });

            services.AddTransient<IRequestHandler<CreateMessageCommand, bool>, MessageCommandHandler>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IEventHandler<MessageCreatedEvent>, MessageEventHandler>();
        }
    }
}