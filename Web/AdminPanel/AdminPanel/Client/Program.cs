using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AdminPanel.Client.Services;

namespace AdminPanel.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            // Register services
            builder.Services.AddSingleton<IAuthService>(new MockAuthService());

            await builder.Build().RunAsync();
        }
    }
}
