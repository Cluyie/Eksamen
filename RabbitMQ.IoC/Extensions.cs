using Consumer.Domain.EventHandlers;
using Consumer.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Producer.Domain.CommandHandlers;
using Producer.Domain.Commands;
using Producer.Domain.Services;
using Producer.Domain.Services.Interfaces;
using RabbitMQ.Bus.Bus;
using RabbitMQ.Bus.Bus.Interfaces;
using RabbitMQ.Bus.Events;

namespace RabbitMQ.IoC
{
    public static class Extensions
    {
        public static void AddRabbitMq(this IServiceCollection services)
        {

            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMqBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMqBus(sp.GetService<IMediator>(), scopeFactory);
            });

            //Template for test consumer and producer
            services.AddTransient<IMessageService, MessageService>();
            //Message template
            services.AddTransient<IRequestHandler<CreateMessageCommand, bool>, MessageCommandHandler>();
            services.AddTransient<IEventHandler<MessageCreatedEvent>, MessageEventHandler>();
        }

        public static void Subscribe<T, TH>(this IApplicationBuilder app) where T : Event where TH : IEventHandler<T>
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<T, TH>();
        }
    }
}