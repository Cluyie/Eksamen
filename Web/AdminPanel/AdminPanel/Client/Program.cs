using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AdminPanel.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

namespace AdminPanel.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Register auth
            builder.Services.AddSingleton<AuthenticationStateProvider,
                CustomAuthStateProvider>();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore(options => { }); // The options needs to be there for auth to work on some machines

            // Register root component
            builder.RootComponents.Add<App>("app");

            // Add local storage
            builder.Services.AddBlazoredLocalStorage();

            // Register services
            builder.Services.AddSingleton<AuthCredentialsKeeper>();
            builder.Services.AddSingleton<IAuthService, MockAuthService>();
            builder.Services.AddSingleton<IResourceService, MockResourceService>();

            await builder.Build().RunAsync();
        }
    }
}
