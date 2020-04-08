using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business_Layer.Models;
using Business_Layer.Properties;
using Data_Access_Layer.Context;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;

namespace Business_Layer
{
    public class ResourceService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly HubConnection _hubConnection;

        public ResourceService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{Resources.ResourceManager.GetString("SignalRBaseAddress")}ResourceHub")
                .Build();
            _ = Connect();
        }

        private async Task Connect()
        {
            await _hubConnection.StartAsync();
        }

        #region Create

        //Creates a resource
        public ApiResponse<Resource> Create(Resource resource)
        {
            var invalidDate = false;

            try
            {
                if (resource == null) return new ApiResponse<Resource>(ApiResponseCode.NoContent, null);

                foreach (var timeslot in resource.TimeSlots) timeslot.Id = Guid.NewGuid();

                //Checks that the timeslots do not overlap.
                resource.TimeSlots.ForEach(delegate(AvailableTime outerTime)
                {
                    resource.TimeSlots.ForEach(delegate(AvailableTime innerTime)
                    {
                        if (!(outerTime.From <= innerTime.From && outerTime.To <= innerTime.From ||
                              outerTime.From >= innerTime.To && outerTime.To >= innerTime.To))
                            if (outerTime.Id != innerTime.Id)
                                invalidDate = true;
                    });
                });

                if (!invalidDate)
                {
                    _applicationContext.Add(resource);
                    _applicationContext.SaveChanges();
                    _hubConnection.SendAsync("CreateResource", resource);

                    return new ApiResponse<Resource>(ApiResponseCode.OK, resource);
                }

                return new ApiResponse<Resource>(ApiResponseCode.NotModified, null);
            }
            catch (Exception)
            {
                return new ApiResponse<Resource>(ApiResponseCode.InternalServerError, null);
            }
        }

        #endregion

        #region Update

        //Update a resource
        //Should ignore reservations when being updated
        public ApiResponse<Resource> Update(Resource resource)
        {
            var resourceToUpdate = new Resource();

            var invalidDate = false;

            try
            {
                resourceToUpdate = _applicationContext.Resources.Include(r => r.TimeSlots)
                    .SingleOrDefault(r => r.Id == resource.Id);

                if (resourceToUpdate == null) return new ApiResponse<Resource>(ApiResponseCode.NoContent, null);

                //Checks that the timeslots do not overlap. Also makes sure that he id is not the same, if the times do match.
                resource.TimeSlots.ForEach(delegate(AvailableTime newTimeSlot)
                {
                    resourceToUpdate.TimeSlots.ForEach(delegate(AvailableTime existingTimeSlot)
                    {
                        if (!(newTimeSlot.From <= existingTimeSlot.From && newTimeSlot.To <= existingTimeSlot.From ||
                              newTimeSlot.From >= existingTimeSlot.To && newTimeSlot.To >= existingTimeSlot.To))
                            if (newTimeSlot.Id != existingTimeSlot.Id)
                                invalidDate = true;
                    });
                });

                if (!invalidDate)
                {
                    resourceToUpdate.Name = resource.Name;

                    resourceToUpdate.TimeSlots = resource.TimeSlots;

                    _applicationContext.Update(resourceToUpdate);
                    _applicationContext.SaveChanges();
                    _hubConnection.SendAsync("UpdateResource", resourceToUpdate);

                    return new ApiResponse<Resource>(ApiResponseCode.OK, resourceToUpdate);
                }

                return new ApiResponse<Resource>(ApiResponseCode.NotModified, null);
            }
            catch (Exception)
            {
                return new ApiResponse<Resource>(ApiResponseCode.InternalServerError, null);
            }
        }

        #endregion

        #region Delete

        //Delete a resource
        public ApiResponse<Resource> Delete(Guid guid)
        {
            var resourceToDelete = new Resource();

            try
            {
                //Gets the resource to be deleted. All children are tracked as well, so that they are also deleted.
                resourceToDelete = _applicationContext.Resources
                    .Include(resource => resource.TimeSlots)
                    .Include(resource => resource.Reservations)
                    .ThenInclude(reservation => reservation.Timeslot)
                    .SingleOrDefault(resource => resource.Id == guid);

                if (resourceToDelete == null) return new ApiResponse<Resource>(ApiResponseCode.NoContent, null);

                //This will cascade delete.
                _applicationContext.Resources.Remove(resourceToDelete);

                _applicationContext.SaveChanges();
                _hubConnection.SendAsync("DeleteResource", resourceToDelete);


                return new ApiResponse<Resource>(ApiResponseCode.OK, null);
            }
            catch (Exception)
            {
                return new ApiResponse<Resource>(ApiResponseCode.InternalServerError, null);
            }
        }

        #endregion

        #region Get

        //Get all resources
        public ApiResponse<List<Resource>> Get()
        {
            try
            {
                //Returns a list of Resources, with all of its children
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
            var resourceToReturn = new Resource();

            try
            {
                //Gets a resource with all of its children
                resourceToReturn = _applicationContext.Resources
                    .Include(resource => resource.TimeSlots)
                    .Include(resource => resource.Reservations)
                    .ThenInclude(reservation => reservation.Timeslot)
                    .SingleOrDefault(resource => resource.Id == guid);

                if (resourceToReturn == null)
                    return new ApiResponse<Resource>(ApiResponseCode.NoContent, null);
                return new ApiResponse<Resource>(ApiResponseCode.OK, resourceToReturn);
            }
            catch (Exception)
            {
                return new ApiResponse<Resource>(ApiResponseCode.InternalServerError, null);
            }
        }

        #endregion
    }
}