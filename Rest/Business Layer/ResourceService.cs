using System;
using Data_Access_Layer.Context;
using Models;
using System.Collections.Generic;

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
        public ApiResponse<IResource> Create(IResource resource)
        {
            throw new NotImplementedException();
        }

        //Get all resources
        public ApiResponse<List<IResource>> Get()
        {
            throw new NotImplementedException();
        }

        //Get a specific resource from a GUID
        public ApiResponse<IResource> Get(Guid guid)
        {
            throw new NotImplementedException();
        }

        //Update a resource
        //Should ignore reservations when being updated
        public ApiResponse<IResource> Update(IResource resource)
        {
            throw new NotImplementedException();
        }

        //Delete a resource
        public ApiResponse<string> Delete(IResource resource)
        {
            throw new NotImplementedException();
        }
    }
}
