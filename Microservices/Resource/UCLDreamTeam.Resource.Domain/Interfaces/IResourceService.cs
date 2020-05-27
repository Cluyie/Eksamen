using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UCLDreamTeam.Resource.Domain.Models;

namespace UCLDreamTeam.Resource.Domain.Interfaces
{
    public interface IResourceService
    {
        public ApiResponse<Models.Resource> Create(Models.Resource resource);

        //Get all resources
        public ApiResponse<List<Models.Resource>> Get();

        //Get a specific resource from a GUID
        public ApiResponse<Models.Resource> Get(Guid guid);

        //Update a resource
        //Should ignore reservations when being updated
        public Task<ApiResponse<Models.Resource>> Update(Models.Resource resource);

        //Delete a resource
        public Task<ApiResponse<Models.Resource>> Delete(Guid guid);
    }
}
