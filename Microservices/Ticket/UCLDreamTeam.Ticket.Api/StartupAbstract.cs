using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RabbitMQ.IoC;
using UCLDreamTeam.Ticket.Application.Services;
using UCLDreamTeam.Ticket.Data.Contexts;
using UCLDreamTeam.Ticket.Data.Repositories;
using UCLDreamTeam.Ticket.Domain.EventHandlers;
using UCLDreamTeam.Ticket.Domain.Events;
using UCLDreamTeam.Ticket.Domain.Interfaces;

namespace UCLDreamTeam.Ticket.Api
{
    public abstract class StartupAbstract
    {
        public IConfiguration Configuration { get; set; }

        protected StartupAbstract(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddRabbitMq();

            //Handler DI
            services.AddTransient<MessageSentEventHandler>();
            services.AddTransient<MessageSeenEventHandler>();
            services.AddTransient<TicketCreatedEventHandler>();
            services.AddTransient<UserCreatedEventHandler>();
            services.AddTransient<UserDeletedEventHandler>();

            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddTransient<ITicketService, TicketService>();

            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ticket MicroService", Version = "v1" }));
            services.AddControllers();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Ticket API V1");
            });

            //Subscriptions
            app.Subscribe<MessageSentEvent, MessageSentEventHandler>();
            app.Subscribe<MessageSeenEvent, MessageSeenEventHandler>();
            app.Subscribe<TicketCreatedEvent, TicketCreatedEventHandler>();
            app.Subscribe<UserCreatedEvent, UserCreatedEventHandler>();
            app.Subscribe<UserDeletedEvent, UserDeletedEventHandler>();
        }
    }
}
