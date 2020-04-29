using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RabbitMQ.IoC;
using UCLDreamTeam.Reservation.Application.Interfaces;
using UCLDreamTeam.Reservation.Application.Services;
using UCLDreamTeam.Reservation.Data.Context;
using UCLDreamTeam.Reservation.Data.Repository;
using UCLDreamTeam.Reservation.Domain.CommandHandlers;
using UCLDreamTeam.Reservation.Domain.Commands;
using UCLDreamTeam.Reservation.Domain.Interfaces;

namespace UCLDreamTeam.Reservation.Api
{
    public class StartupDevelopment
    {
        public StartupDevelopment(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ReservationDbContext>(options => { options.UseInMemoryDatabase("Reservations"); });

            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Reservation Microservice", Version = "v1"}));

            services.AddMediatR(typeof(Startup));

            services.AddRabbitMq();

            services.AddTransient<IRequestHandler<CreateReservationCommand, bool>, CreateReservationCommandHandler>();
            services.AddTransient<IRequestHandler<CreateCancelReservationCommand, bool>, CancelReservationCommandHandler>();
            services.AddTransient<IRequestHandler<CreateUpdateReservationCommand, bool>, UpdateReservationCommandHandler>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IReservationRepository, ReservationRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            //app.UseHttpsRedirection();

            app.UsePathBase("/ReservationService");

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reservation Microservice v1"));


            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}