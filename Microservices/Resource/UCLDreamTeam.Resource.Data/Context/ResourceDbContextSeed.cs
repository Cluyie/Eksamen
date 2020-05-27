using Microsoft.Extensions.Configuration;
using RabbitMQ.Bus.Bus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCLDreamTeam.Resource.Domain.Interfaces;
using UCLDreamTeam.Resource.Domain.InternalCommands;
using UCLDreamTeam.Resource.Domain.Models;

namespace UCLDreamTeam.Resource.Data.Context
{
    public class ResourceDbContextSeed
    {
        public static async Task SeedSampleDataAsync(IResourceService resourceService, ResourceContext context, IConfiguration configuration)
        {
            var seedingResources = new List<Domain.Models.Resource>
                {
                    new Domain.Models.Resource
                    {
                        Id = Guid.NewGuid(),
                        Name = "Oslo",
                        Description = "Meeting room Oslo",
                        TimeSlots = GetTimeSlots()
                    },
                    new Domain.Models.Resource
                    {
                        Id = Guid.NewGuid(),
                        Name = "Helsinki",
                        Description = "Meeting room Helsinki",
                        TimeSlots = GetTimeSlots()
                    },
                    new Domain.Models.Resource
                    {
                        Id = Guid.NewGuid(),
                        Name = "Tokyo",
                        Description = "Meeting room Tokyo",
                        TimeSlots = GetTimeSlots()
                    },
                    new Domain.Models.Resource
                    {
                        Id = Guid.NewGuid(),
                        Name = "Berlin",
                        Description = "Meeting room Berlin",
                        TimeSlots = GetTimeSlots()
                    },
                    new Domain.Models.Resource
                    {
                        Id = Guid.NewGuid(),
                        Name = "Moscow",
                        Description = "Meeting room Moscow",
                        TimeSlots = GetTimeSlots()
                    },
                    new Domain.Models.Resource
                    {
                        Id = Guid.NewGuid(),
                        Name = "Nairobi",
                        Description = "Meeting room Nairobi",
                        TimeSlots = GetTimeSlots()
                    },
                    new Domain.Models.Resource
                    {
                        Id = Guid.NewGuid(),
                        Name = "Rio",
                        Description = "Meeting room Rio",
                        TimeSlots = GetTimeSlots()
                    },
                    new Domain.Models.Resource
                    {
                        Id = Guid.NewGuid(),
                        Name = "Denver",
                        Description = "Meeting room Denver",
                        TimeSlots = GetTimeSlots()
                    },
                    new Domain.Models.Resource
                    {
                        Id = Guid.NewGuid(),
                        Name = "Stockholm",
                        Description = "Meeting room Stockholm",
                        TimeSlots = GetTimeSlots()
                    },
                };
            bool dbContainsSeededResources = !seedingResources.Where(sR => context.Resources.Where(r => r.Name == sR.Name).Any()).Any();
            if ((!context.Resources.Any()) || (configuration.GetValue<bool>("SeedDatabase") && dbContainsSeededResources)) //Configuration["Jwt:Issuer"]
            {
                seedingResources.ForEach(r => resourceService.Create(r));
                //await context.Resources.AddRangeAsync(seedingResources);
                //await context.SaveChangesAsync();
            }

        }

        private static List<AvailableTime> GetTimeSlots()
        {
            var random = new Random();
            var list = new List<AvailableTime>();
            for (var i = 0; i < 7; i++)
            {
                var time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 08, 00, 00).AddDays(i);
                while (time <= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 00, 00).AddDays(i))
                {
                    var oldTime = time;
                    time = time.AddMinutes(random.Next(15, 481));

                    if (time >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 00, 00).AddDays(i)) break;
                    list.Add(new AvailableTime
                    {
                        Id = Guid.NewGuid(),
                        From = oldTime,
                        To = time,
                        Recurring = true
                    });

                    time = time.AddMinutes(random.Next(15, 240));
                }
            }

            return list;
        }
    }
}
