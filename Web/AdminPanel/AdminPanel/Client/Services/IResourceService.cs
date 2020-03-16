using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Client.DTOs;

namespace AdminPanel.Client.Services
{
    interface IResourceService
    {
        Task<List<ResourceDTO>> GetAll();

        Task<ResourceDTO> GetFromId(Guid id);

        Task DeleteFromId(Guid id);
    }
}
