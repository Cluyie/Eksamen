using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace UCLDreamTeam.Resource.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseDefaultServiceProvider(options =>
                    options.ValidateScopes = false) //If this is not here MediatR will not work
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}