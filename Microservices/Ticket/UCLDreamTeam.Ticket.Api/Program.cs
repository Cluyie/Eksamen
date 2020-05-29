using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Ticket.Data.Contexts;

namespace UCLDreamTeam.Ticket.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("sharedSettings.json", optional: false, reloadOnChange: true)
            .Build();

            var host = CreateHostBuilder(args, config).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var eventBus = services.GetRequiredService<IEventBus>();

                    var context = services.GetRequiredService<TicketDbContext>();

                    if (context.Database.IsSqlServer()) context.Database.Migrate();

                    var configuration = services.GetRequiredService<IConfiguration>();

                    //await TicketDbContextSeed.SeedSampleDataAsync(eventBus, context, configuration);
                }
                catch (Exception ex)
                {

                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration config)
        {
            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.FullName;

            return Host.CreateDefaultBuilder(args)
                .UseDefaultServiceProvider(options =>
                    options.ValidateScopes = false) //If this is not here MediatR will not work
                .ConfigureWebHostDefaults(webBuilder => 
                {
                    webBuilder.UseConfiguration(config);
                    webBuilder.UseStartup(assemblyName); 
                });
        }
    }
}
