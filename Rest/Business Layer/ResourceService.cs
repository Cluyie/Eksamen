using System;
using Data_Access_Layer.Context;
using Models;
using System.Collections.Generic;
using System.Linq;
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

            //If the _applicationContext does not make any changes, it returns 0.
            if (_applicationContext.SaveChanges() != 0)
            {
                return new ApiResponse<Resource>(ApiResponseCode.Created, resource);
            }
            else
            {
                return new ApiResponse<Resource>(ApiResponseCode.NotModified, resource);
            }
        }

        //Get all resources
        public ApiResponse<List<Resource>> Get()
        {
            return new ApiResponse<List<Resource>>(ApiResponseCode.OK, _applicationContext.Resources.ToList());
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
