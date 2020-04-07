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
using UCLDreamTeam.UserServiceApi.Domain.Context;
using UCLDreamTeam.UserServiceApi.Domain.Models;
using UCLDreamTeam.UserServiceApi.Domain.Services;

namespace UCLDreamTeam.UserServiceApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<UserService>();
            Mapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, User>()
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            }));
            services.AddSingleton(mapper);
            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<IdentityContext>();
            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Producer Microservice", Version = "v1" }));
            services.AddMediatR(typeof(Startup));
            services.AddControllers();
            services.RegisterRabbitMq();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
        }
    }
}
