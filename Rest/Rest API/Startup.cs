using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business_Layer;
using Data_Access_Layer.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rest_API.Middleware;
using AutoMapper;
using Data_Access_Layer.Models;


namespace Rest_API
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
      services.AddControllers();
      services.AddScoped<IdentityContext>();
      services.AddScoped<ApplicationContext>();
      services.AddScoped<AuthService>();
      services.AddScoped<UserService>();
      services.AddScoped<ResourceService>();
      services.AddScoped<ReservationService>();

      //AutoMapper setup

      Mapper mapper = new Mapper(new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<User, User>()
     .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
      }));
      services.AddSingleton(mapper);
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

      app.UseTokenValidation();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
