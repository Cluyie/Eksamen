using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consumer.Domain.EventHandlers;
using Consumer.Domain.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RabbitMQ.IoC;
using MediatR;
using Producer.Domain.CommandHandlers;
using Producer.Domain.Commands;
using Producer.Domain.Services;
using Producer.Domain.Services.Interfaces;
using RabbitMQ.Bus.Bus.Interfaces;

namespace RabbitMQ.Producer
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
            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Producer Microservice", Version = "v1"}));
            services.AddMediatR(typeof(Startup));
            services.AddControllers();
            services.AddRabbitMq();

            services.AddTransient<IRequestHandler<CreateMessageCommand, bool>, MessageCommandHandler>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IEventHandler<MessageCreatedEvent>, MessageEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Producer Microservice v1"));

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}