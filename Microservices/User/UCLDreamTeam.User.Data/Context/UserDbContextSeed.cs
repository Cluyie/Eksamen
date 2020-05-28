using Microsoft.Extensions.Configuration;
using RabbitMQ.Bus.Bus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCLDreamTeam.User.Domain.Commands;
using UCLDreamTeam.User.Domain.Models;

namespace UCLDreamTeam.User.Data.Context
{
    public class UserDbContextSeed
    {
        public static async Task SeedSampleDataAsync(IEventBus eventBus, UserDbContext context, IConfiguration configuration)
        {
            var seedingUsers = new Dictionary<Domain.Models.User, Role>
                {
                    {
                        new Domain.Models.User
                        {
                            Id = Guid.NewGuid(),
                            FirstName = "admin",
                            LastName = "Adminsen",
                            Email = "admin@ucl.dk",
                            UserName = "Admin",
                            NormalizedUserName = "ADMIN",
                            Password = "P@ssw0rd",
                            Address = "Boulevarden 25",
                            City = "Vejle",
                            ZipCode = 7100,
                            Country = "Denmark"

                        },
                        new Role{ RoleId = Guid.Empty, RoleName = "Admin" }
                    },
                    {
                        new Domain.Models.User
                        {
                            Id = Guid.NewGuid(),
                            FirstName = "Customer",
                            LastName = "Support",
                            Email = "CustomerSupport@ucl.dk",
                            UserName = "CustomerSupport",
                            NormalizedUserName = "CUSTOMERSUPPORT",
                            Password = "P@ssw0rd",
                            Address = "Boulevarden 25",
                            City = "Vejle",
                            ZipCode = 7100,
                            Country = "Denmark"
                            },
                        new Role { RoleId = Guid.Empty, RoleName = "Admin" }
                    },
                    {
                        new Domain.Models.User
                        {
                            Id = Guid.NewGuid(),
                            FirstName = "Lasse",
                            LastName = "Rasch",
                            Email = "mail@r-coding.dk",
                            UserName = "lara2",
                            NormalizedUserName = "LARA2",
                            Password = "P@ssw0rd",
                            Address = "Boulevarden 25",
                            City = "Vejle",
                            ZipCode = 7100,
                            Country = "Denmark"
                        },
                        new Role{ RoleId = Guid.Empty, RoleName = "User" }

                    }
                };
            var dbContainsSeededUsers = !seedingUsers.Any(sR => context.Users.Any(r => r.FirstName == sR.Key.FirstName));
            if (!context.Users.Any() || (configuration.GetValue<bool>("SeedDatabase") && dbContainsSeededUsers)) //Configuration["Jwt:Issuer"]
            {
                foreach (var (user, role) in seedingUsers)
                {
                      await eventBus.SendCommand(new RegisterUserCommand(user, role));
                }
                //await context.Users.AddRangeAsync(seedingUsers);
                //await context.SaveChangesAsync();
            }

        }
    }
}