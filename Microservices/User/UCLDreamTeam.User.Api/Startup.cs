using System;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RabbitMQ.IoC;
using UCLDreamTeam.User.Application.Interfaces;
using UCLDreamTeam.User.Application.Services;
using UCLDreamTeam.User.Data.Context;
using UCLDreamTeam.User.Domain.CommandHandlers;
using UCLDreamTeam.User.Domain.Commands;

namespace UCLDreamTeam.User.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IdentityContext>();
            services.AddIdentity<Domain.Models.User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<IdentityContext>();
            services.AddAuthentication();

            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InputUser Microservice", Version = "v1" }));
            services.AddMediatR(typeof(Startup));
            services.AddRabbitMq();

            //Register user
            services.AddTransient<IRequestHandler<RegisterUserCommand, bool>, RegisterUserCommandHandler>();
            //Update user
            services.AddTransient<IRequestHandler<UpdateUserCommand, bool>, UpdateUserCommandHandler>();
            //Delete user
            services.AddTransient<IRequestHandler<DeleteUserCommand, bool>, DeleteUserCommandHandler>();
            //No user found
            services.AddTransient<IRequestHandler<NoUserFoundCommand, bool>, NoUserFoundCommandHandler>();
            services.AddTransient<IUserService, UserService>();

            Mapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Models.User, Domain.Models.User>()
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            }));
            services.AddSingleton(mapper);
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseAuthentication();
            //app.UseAuthorization();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
