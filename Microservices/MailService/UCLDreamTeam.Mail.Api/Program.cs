using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UCLDreamTeam.Mail.Data.Context;

namespace UCLDreamTeam.Mail.Api
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
                    var context = services.GetRequiredService<MailDbContext>();

                    if (context.Database.IsSqlServer()) context.Database.Migrate();

                    var configuration = services.GetRequiredService<IConfiguration>();

                    //await MailDbContextSeed.SeedSampleDataAsync(eventBus, context, configuration);
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
