using System;
using Data_Access_Layer.Context;
using Models;
using System.Collections.Generic;
using System.Linq;
using Data_Access_Layer.Models;
using Business_Layer.Models;
using Microsoft.EntityFrameworkCore;

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
            try
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
            catch (Exception)
            {
                return new ApiResponse<Resource>(ApiResponseCode.InternalServerError, resource);
            }
        }

        //Get all resources
        public ApiResponse<List<Resource>> Get()
        {
            try
            {
                return new ApiResponse<List<Resource>>
                    (ApiResponseCode.OK, _applicationContext.Resources
                    .Include(resource => resource.TimeSlots)
                    .Include(resource => resource.Reservations)
                    .ThenInclude(reservation => reservation.Timeslot)
                    .ToList());
            }
            catch (Exception)
            {
                return new ApiResponse<List<Resource>>(ApiResponseCode.InternalServerError, null);
            }
        }

        //Get a specific resource from a GUID
        public ApiResponse<Resource> Get(Guid guid)
        {
            Resource resourceToReturn = new Resource();

            try
            {
               resourceToReturn = _applicationContext.Resources
                    .Include(resource => resource.TimeSlots)
                    .Include(resource => resource.Reservations)
                    .ThenInclude(reservation => reservation.Timeslot)
                    .SingleOrDefault(resource => resource.Id == guid);
            }
            catch (Exception)
            {
                return new ApiResponse<Resource>(ApiResponseCode.InternalServerError, null);
            }

            if (resourceToReturn == null)
            {
                return new ApiResponse<Resource>(ApiResponseCode.NoContent, null);
            }
            else
            {
                return new ApiResponse<Resource>(ApiResponseCode.OK, resourceToReturn);
            }
        }

        //Update a resource
        //Should ignore reservations when being updated
        public ApiResponse<Resource> Update(Resource resource)
        {
            Resource resourceToUpdate = new Resource();

            try
            {
                resourceToUpdate = _applicationContext.Resources.Find(resource.Id);

                if (resourceToUpdate == null)
                {
                    return new ApiResponse<Resource>(ApiResponseCode.NoContent, null);
                }
                else
                {
                    resourceToUpdate.Name = resource.Name;
                    resourceToUpdate.TimeSlots = resource.TimeSlots;

                    //_applicationContext.Entry(resourceToUpdate).Property(resource => resource.Reservations).IsModified = false;

                    _applicationContext.SaveChanges();

                    return new ApiResponse<Resource>(ApiResponseCode.OK, resourceToUpdate);
                }
            }
            catch (Exception)
            {
                return new ApiResponse<Resource>(ApiResponseCode.InternalServerError, null);
            }
        }

        //Delete a resource
        public ApiResponse<Resource> Delete(Guid guid)
        {
            Resource resourceToDelete = new Resource();

            try
            {
                resourceToDelete = _applicationContext.Resources
                    .Include(resource => resource.TimeSlots)
                    .Include(resource => resource.Reservations)
                    .ThenInclude(reservation => reservation.Timeslot)
                    .SingleOrDefault(resource => resource.Id == guid);

                if (resourceToDelete == null)
                {
                    return new ApiResponse<Resource>(ApiResponseCode.NoContent, null);
                }
                else
                {
                    //Needs to CASCADE delete, so that TimeSlots are removed as well.
                    _applicationContext.Resources.Remove(resourceToDelete);

                    _applicationContext.SaveChanges();

                    return new ApiResponse<Resource>(ApiResponseCode.OK, null);
                }
            }
            catch (Exception)
            {
                return new ApiResponse<Resource>(ApiResponseCode.InternalServerError, null);
            }
        }
    }
}
