using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RabbitMQ.IoC;
using UCLDreamTeam.Mail.Application.Interfaces;
using UCLDreamTeam.Mail.Application.Services;
using UCLDreamTeam.Mail.Data.Context;
using UCLDreamTeam.Mail.Data.Repository;
using UCLDreamTeam.Mail.Domain.CommandHandlers;
using UCLDreamTeam.Mail.Domain.Commands;
using UCLDreamTeam.Mail.Domain.EventHandlers;
using UCLDreamTeam.Mail.Domain.Events;
using UCLDreamTeam.Mail.Domain.Interfaces;
using UCLDreamTeam.Mail.Domain.Models;

namespace UCLDreamTeam.Mail.Api
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
            services.AddRabbitMq();
            services.AddMediatR(typeof(Startup));
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
            services.AddTransient<IGenericRepository<Resource>, GenericRepository<Resource>>();

            services.AddDbContext<MailDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MailDbConnection"));
            });

            //Command setup
            services.AddTransient<IRequestHandler<SendEmailCommand, bool>, SendEmailCommandHandler>();

            //Event setup
            services.AddTransient<ReservationCreatedEventHandler>();

            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Mail Microservice", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            //app.UseHttpsRedirection();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("../swagger/v1/swagger.json", "Mail API V1"); });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            //Reservation
            app.Subscribe<ReservationCreatedEvent, ReservationCreatedEventHandler>();
            //User
            app.Subscribe<UserRegisteredEvent, UserRegisteredEventHandler>();
            app.Subscribe<UserUpdatedEvent, UserUpdatedEventHandler>();
            app.Subscribe<UserDeletedEvent, UserDeletedEventHandler>();
            //Resource
            app.Subscribe<ResourceCreatedEvent, ResourceCreatedEventHandler>();
            app.Subscribe<ResourceUpdatedEvent, ResourceUpdatedEventHandler>();
            app.Subscribe<ResourceDeletedEvent, ResourceDeletedEventHandler>();
        }
    }
}