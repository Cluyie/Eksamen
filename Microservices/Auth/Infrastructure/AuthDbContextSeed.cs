using Microsoft.Extensions.Configuration;
using RabbitMQ.Bus.Bus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCLDreamTeam.Auth.Api.Infrastructure;
using UCLDreamTeam.Auth.Api.Models;

namespace UCLDreamTeam.User.Data.Context
{
    public class AuthDbContextSeed
    {
        public static async Task SeedSampleDataAsync(AuthContext context, IConfiguration configuration)
        {
            var seedingRoles = new List<Role>
                {
                    new Role
                    {
                        RoleId = Guid.NewGuid(),
                        RoleName = "CustomerService"
                    },
                    new Role
                    {
                        RoleId = Guid.NewGuid(),
                        RoleName = "User"
                    },
                    new Role
                    {
                        RoleId = Guid.NewGuid(),
                        RoleName = "Admin"
                    },
                };
            bool dbContainsSeededUsers = !seedingRoles.Any(sR => context.Roles.Any(r => r.RoleName == sR.RoleName));
            if (!context.Roles.Any() || (configuration.GetValue<bool>("SeedDatabase") && dbContainsSeededUsers)) //Configuration["Jwt:Issuer"]
            {
                seedingRoles.ForEach(r => context.Roles.Add(r));
                
                await context.SaveChangesAsync();
            }
        }
    }
}
