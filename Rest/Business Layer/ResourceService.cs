using System;
using Data_Access_Layer.Context;
using Models;
using System.Collections.Generic;
using Data_Access_Layer.Models;
using Business_Layer.Models;

namespace Business_Layer
{
    public class ResourceService
    {
        
        private ApplicationContext _applicationContext;

        public ResourceService(ApplicationContext applicationContext)
        {
            
            _applicationContext = applicationContext;
            
        }

        //Create resource
        public ApiResponse<Resource> Create(Resource resource)
        {
            _applicationContext.Add(resource);
            _applicationContext.SaveChangesAsync();

            return new ApiResponse<Resource>(ApiResponseCode.Created, resource);

        }

        //Get all resources
        public ApiResponse<List<Resource>> Get()
        {
            throw new NotImplementedException();
        }

        //Get a specific resource from a GUID
        public ApiResponse<Resource> Get(Guid guid)
        {
            throw new NotImplementedException();
        }

        //Update a resource
        //Should ignore reservations when being updated
        public ApiResponse<Resource> Update(Resource resource)
        {
            throw new NotImplementedException();
        }

        //Delete a resource
        public ApiResponse<string> Delete(Resource resource)
        {
            throw new NotImplementedException();
        }
    }
}
