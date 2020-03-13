using AdminPanel.Client.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Client.Services
{
    public class MockResourceService : IResourceService
    {
        public List<ResourceDTO> GetAll()
        {
            return new List<ResourceDTO>
            {
                new ResourceDTO
                {
                    Id = new Guid(),
                    Name = "Køkken",
                    Timeslots = new List<TimeslotDTO>
                    {
                        new TimeslotDTO
                        {
                            Id = new Guid(),
                            Available = true,
                            FromDateTime = new DateTime(2020, 1, 1, 1, 1, 1),
                            ToDateTime = new DateTime(2020, 1, 1, 2, 2, 2),
                            Recurring = 1
                        }
                    }
                },
                new ResourceDTO
                {
                    Id = new Guid(),
                    Name = "Mødelokale 1",
                    Timeslots = new List<TimeslotDTO>
                    {
                        new TimeslotDTO
                        {
                            Id = new Guid(),
                            Available = true,
                            FromDateTime = new DateTime(2020, 1, 1, 1, 1, 1),
                            ToDateTime = new DateTime(2020, 1, 1, 2, 2, 2),
                            Recurring = 1
                        },
                        new TimeslotDTO
                        {
                            Id = new Guid(),
                            Available = true,
                            FromDateTime = new DateTime(2020, 1, 1, 3, 1, 1),
                            ToDateTime = new DateTime(2020, 1, 1, 4, 2, 2),
                            Recurring = 1
                        }
                    }
                },
                new ResourceDTO
                {
                    Id = new Guid(),
                    Name = "Biograf",
                    Timeslots = new List<TimeslotDTO>
                    {
                        new TimeslotDTO
                        {
                            Id = new Guid(),
                            Available = true,
                            FromDateTime = new DateTime(2020, 1, 1, 1, 1, 1),
                            ToDateTime = new DateTime(2020, 1, 1, 2, 2, 2),
                            Recurring = 1
                        }
                    }
                }
            };
        }
    }
}
