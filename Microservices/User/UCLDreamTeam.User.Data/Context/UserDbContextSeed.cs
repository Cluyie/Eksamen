using Microsoft.Extensions.Configuration;
using RabbitMQ.Bus.Bus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCLDreamTeam.User.Domain.Commands;

namespace UCLDreamTeam.User.Data.Context
{
    public class UserDbContextSeed
    {
        public static async Task SeedSampleDataAsync(IEventBus eventBus, UserDbContext context, IConfiguration configuration)
        {
            var seedingUsers = new List<Domain.Models.User>
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
                };
            bool dbContainsSeededUsers = !seedingUsers.Where(sR => context.Users.Where(r => r.FirstName == sR.FirstName).Any()).Any();
            if (!context.Users.Any() || (configuration.GetValue<bool>("SeedDatabase") && dbContainsSeededUsers)) //Configuration["Jwt:Issuer"]
            {
                seedingUsers.ForEach(u => eventBus.SendCommand(new RegisterUserCommand(u)));
                //await context.Users.AddRangeAsync(seedingUsers);
                //await context.SaveChangesAsync();
            }

        }
    }
}
