using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Client.DTOs;

namespace AdminPanel.Client.Services
{
    interface IResourceService
    {
        public List<ResourceDTO> GetAll();
    }
}
