using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UCLDreamTeam.Auth.Api.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using UCLDreamTeam.Auth.Api.Infrastructure;
using UCLDreamTeam.Auth.Api.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using RabbitMQ.Bus.Bus.Interfaces;
using RabbitMQ.IoC;
using MediatR;
using UCLDreamTeam.Auth.Api.IntegrationEvents.Events;
using UCLDreamTeam.Auth.Api.IntegrationEvents.EventHandlers;

namespace UCLDreamTeam.Auth.Api
{
    public class Startup
    {
        private const string xmlKey =
            "<RSAKeyValue>" +
            "<Modulus>p9ZX2CSot2aHOiIRJJz0lngezY51Z+stl/sMYGFD1rxcYZbuHDs/cZgUURDhxdlkGoLGv5VSVSyecJ15LIDsjkaKeZ5HJOT5TXVXQOtvtq8Wm/gPsOZso0qoxNIswKwEAsHclfaNOQ7zi3yvVv04Wq3AnhC6y2u/I7YhZUIZtW9oy1BWKnP+HS0PUlP+EhCSmcCro76kWNTQn0Y9lv9ouJqrlOuGmjBEobCyGXISQYfitCTMFZXTcFv9k5F8Y3Kq7FIjAakAjX90rUzl5JxY81Q+8xeOT7zzXn+CrqGuFvlQ0+QrIJLylUOf/x6OguBHlfco682RIqReVFGRwPU+db77OUlj7Yazq1s5X2aRUFn+dRIo/x7+iEin+b1OeA8JycjCrk6bqkttGpy4rKYGuZfoheRwUoJdI8KnuWwWg7D5VbxCh0TX8l9aSczQCryHNN0YZtVDbxRhU/HdOgHSzTAzKsQ8O/fJwgGcaEZs/JH3AS9BGmfurYXZbpiMnkoBEvZpe1pd64GeRenaaCnL2UYFu96Bbb/IUW62foh78+T/leuY1buTLlsiYHAu2fmZw7FBiaPa+RSJ6WXO/sPG/aFPk3AgZx6xX/9tY7Zo1UJ4BWyNw3tpxM+NTu49y9rdiaJ1hdZscPfFACpt/VFFKolgMcVauqV+OvVBZO3ZrsE=</Modulus>" +
            "<Exponent>AQAB</Exponent>" +
            "</RSAKeyValue>";


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<AuthService>();
            services.AddScoped<HashService>();
            services.AddScoped<AuthRepository>();
            services.AddDbContext<AuthContext>();
            services.AddMediatR(typeof(Startup));
            services.AddRabbitMq();
            services.AddTransient<UserCreatedEventHandler>();
            services.AddTransient<UserUpdatedEventHandler>();
            services.AddTransient<UserDeletedEventHandler>();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var key = KeyService.BuildRsaSigningKey(xmlKey);
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = key
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Auth MicroService", Version = "v1"});

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.
                    Enter 'Bearer' [space] {Token}.
                    Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("../swagger/v1/swagger.json", "Auth API V1"); });

            app.Subscribe<UserCreatedEvent, UserCreatedEventHandler>();
            app.Subscribe<UserUpdatedEvent, UserUpdatedEventHandler>();
            app.Subscribe<UserDeletedEvent, UserDeletedEventHandler>();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            CreateRoles(serviceProvider);
        }

        private void CreateRoles(IServiceProvider serviceProvider)

        {
            //adding custom roles

            var authContext = serviceProvider.GetRequiredService<AuthContext>();
            var hashService = serviceProvider.GetRequiredService<HashService>();

            var roleName = "Admin";

            //creating the role and seeding it to the database
            var roleExist = authContext.Roles.Any(r => r.RoleName == roleName);

            var roleToAdd = new Role
            {
                RoleId = Guid.NewGuid(),
                RoleName = roleName
            };

            if (!roleExist) authContext.Roles.Add(roleToAdd);

            var _user = authContext.AuthUsers.SingleOrDefault(u =>
                u.UserName == Configuration.GetSection("UserSettings")["UserName"]);

            if (_user == null)
            {
                //creating an admin
                var admin = new AuthUser
                {
                    Id = Guid.NewGuid(),
                    UserName = Configuration.GetSection("UserSettings")["UserName"],
                    Email = Configuration.GetSection("UserSettings")["UserEmail"]
                };

                admin.PasswordSalt = hashService.GenerateSalt();

                admin.PasswordHash = hashService.GenerateHash(Configuration.GetSection("UserSettings")["UserPassword"],
                    admin.PasswordSalt);

                authContext.AuthUsers.Add(admin);
                authContext.UserRoles.Add(new UserRole {AuthUserId = admin.Id, Role = roleToAdd});
                authContext.SaveChanges();
            }
        }
    }
}