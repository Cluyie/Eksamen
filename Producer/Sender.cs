using System;
using Microsoft.Extensions.DependencyInjection;
using Producer.Domain.Services.Interfaces;
using RabbitMQ.IoC;

namespace Producer
{
    internal class Sender
    {
        private static void Main(string[] args)
        {
            var serviceProvider = RegisterServices();
            var scope = serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IMessageService>();
            service.Send("Test");
        }

        private static ServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();
            DependencyContainer.RegisterServices(services);
            return services.BuildServiceProvider(true);
        }
    }
}