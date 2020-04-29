using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Bus.Bus.Interfaces;
using SignalR_Microservice.Hubs;
using RabbitMQ.IoC;
using SignalR_Microservice.EventHandlers;
using SignalR_Microservice.Events;
using SignalR_Microservice.Services;
using MediatR;
using SignalR_Microservice.Commands;
using SignalR_Microservice.CommandHandlers;
using System.Collections.Generic;
using SignalR_Microservice.Models;

namespace SignalR_Microservice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            services.AddRabbitMq();

            services.AddMediatR(typeof(Startup));

            //Handler DI
            services.AddTransient<ReservationCreatedEventHandler>();
            services.AddTransient<ReservationCanceledEventHandler>();
            services.AddTransient<ReservationUpdatedEventHandler>();

            //SignalR Handlers
            services.AddTransient<IRequestHandler<CreateSentMessageCommand, bool>, CreateSentMessageCommandHandler>();

            //SignalR dependencies
            services.AddScoped<IChatLoggingService, ChatLoggingService>();
            services.AddScoped<IQueueService, QueueService>();
            //services.AddScoped<QueueHub>();
            services.AddSingleton<Queue<(string, Guid)>>();
            //services.AddSingleton<Dictionary<string, List<User>>>();

            services.AddCors(options =>
            {
                //Dissable at later point to block requests from other then host
                options.AddPolicy("AllowOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader());

                //Enable at later point to only allow connections from the host itself
                //options.AddPolicy("AllowOrigin",
                //    builder => builder.WithOrigins("http://localhost")
                //                      .AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            //app.UseHttpsRedirection(); //Enable Later!

            app.UseWebSockets();

            app.UseRouting();

            app.UseCors("AllowOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ResourceHub>("/ResourceHub");
                endpoints.MapHub<ReservationHub>("/ReservationHub");
                endpoints.MapHub<ChatHub>("/ChatHub");
                endpoints.MapHub<QueueHub>("/QueueHub");
            });

            //Subscriptions
            app.Subscribe<ReservationCreatedEvent, ReservationCreatedEventHandler>();
            app.Subscribe<ReservationCanceledEvent, ReservationCanceledEventHandler>();
            app.Subscribe<ReservationUpdatedEvent, ReservationUpdatedEventHandler>();
        }
    }
}