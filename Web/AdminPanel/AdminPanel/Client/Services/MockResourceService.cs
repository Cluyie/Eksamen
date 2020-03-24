using AdminPanel.Client.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Client.Services
{
    public class MockResourceService : IResourceService
    {
        private List<ResourceDTO> _resources = new List<ResourceDTO>
        {
            new ResourceDTO
            {
                Id = Guid.NewGuid(),
                Name = "Køkken",
                Timeslots = new List<TimeslotDTO>
                {
                    new TimeslotDTO
                    {
                        Id = Guid.NewGuid(),
                        Available = true,
                        From = new DateTime(2020, 1, 1, 1, 1, 1),
                        To = new DateTime(2020, 1, 1, 2, 2, 2),
                        Recurring = 1
                    }
                }
            },
            new ResourceDTO
            {
                Id = Guid.NewGuid(),
                Name = "Mødelokale 1",
                Timeslots = new List<TimeslotDTO>
                {
                    new TimeslotDTO
                    {
                        Id = Guid.NewGuid(),
                        Available = true,
                        From = new DateTime(2020, 1, 1, 1, 1, 1),
                        To = new DateTime(2020, 1, 1, 2, 2, 2),
                        Recurring = 1
                    },
                    new TimeslotDTO
                    {
                        Id = Guid.NewGuid(),
                        Available = true,
                        From = new DateTime(2020, 1, 1, 3, 1, 1),
                        To = new DateTime(2020, 1, 1, 4, 2, 2),
                        Recurring = 1
                    }
                }
            },
            new ResourceDTO
            {
                Id = Guid.NewGuid(),
                Name = "Biograf",
                Timeslots = new List<TimeslotDTO>
                {
                    new TimeslotDTO
                    {
                        Id = Guid.NewGuid(),
                        Available = true,
                        From = new DateTime(2020, 1, 1, 1, 1, 1),
                        To = new DateTime(2020, 1, 1, 2, 2, 2),
                        Recurring = 1
                    }
                }
            }
        };

        public async Task<List<ResourceDTO>> GetAll()
        {
            return _resources;
        }

        public async Task<ResourceDTO> GetFromId(Guid id)
        {
            return _resources.FirstOrDefault(resource => resource.Id == id);
        }

        public async Task DeleteFromId(Guid id)
        {
            _resources.Remove(await GetFromId(id));
        }

        public async Task Add(ResourceDTO resource)
        {
            _resources.Add(resource);
        }

        public async Task Update(ResourceDTO resource)
        {
            // Don't need to do anything here as the resource that was edited is
            // already a reference to the resource in memory
        }
    }
}
