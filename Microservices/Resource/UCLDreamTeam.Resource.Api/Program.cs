using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Bus.Bus.Interfaces;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using UCLDreamTeam.Resource.Data.Context;
using UCLDreamTeam.Resource.Domain.Interfaces;

namespace UCLDreamTeam.Resource.Api
{
    public class Program
    {
        public async static Task Main(string[] args)
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
                    var eventBus = services.GetRequiredService<IResourceService>();

                    var context = services.GetRequiredService<ResourceContext>();

                    if (context.Database.IsSqlServer()) context.Database.Migrate();

                    var configuration = services.GetRequiredService<IConfiguration>();

                    await ResourceDbContextSeed.SeedSampleDataAsync(eventBus, context, configuration);
                }
                catch (Exception ex)
                {

                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration config)
        {
            return Host.CreateDefaultBuilder(args)
                .UseDefaultServiceProvider(options =>
                    options.ValidateScopes = false) //If this is not here MediatR will not work
                .ConfigureWebHostDefaults(webBuilder => 
                {
                    webBuilder.UseConfiguration(config);
                    webBuilder.UseStartup<Startup>(); 
                });
        }
    }
}
