using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UCLDreamTeam.Ticket.Data.Contexts;
using Microsoft.OpenApi.Models;
using RabbitMQ.IoC;
using UCLDreamTeam.Ticket.Domain.EventHandlers;
using UCLDreamTeam.Ticket.Domain.Events;

namespace UCLDreamTeam.Ticket.Api
{
    public class Startup : StartupAbstract
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TicketDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TicketDbConnection")));
            base.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);
        }
    }
}
