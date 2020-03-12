using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalR_Microservice.Hubs;

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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection(); //Enable Later!

            app.UseWebSockets();

            app.UseRouting();

            app.UseCors("AllowOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<DemoHub>("/DemoHub");
                endpoints.MapHub<ResourceHub>("/ResourceHub");
            });
        }
    }
}