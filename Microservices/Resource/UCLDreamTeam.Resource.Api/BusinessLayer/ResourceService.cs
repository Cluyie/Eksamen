using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Bus.Events;
using RabitMQEasy;
using UCLDreamTeam.Resource.Api.Models;
using UCLDreamTeam.Resource.Data.Context;
using UCLDreamTeam.Resource.Domain.Models;
using UCLDreamTeam.Resource.Domain.RabbitMQEvents;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Resource.Api.BusinessLayer
{
    public class ResourceService
    {
        private ResourceContext _applicationContext;
        RabitMQPublicer _eventBus { get; set; }
        public ResourceService(ResourceContext applicationContext, RabitMQPublicer eventBus)
        {
            _applicationContext = applicationContext;
            _eventBus = eventBus;
        }

        #region Create
        //Creates a resource
        public ApiResponse<Domain.Models.Resource> Create(Domain.Models.Resource resource)
        {
            bool invalidDate = false;

            try
            {
                if (resource == null)
                {
                    return new ApiResponse<Domain.Models.Resource>(Models.ApiResponseCode.NoContent, null);
                }

                foreach (var timeslot in resource.TimeSlots)
                {
                    timeslot.Id = Guid.NewGuid();
                }

                //Checks that the timeslots do not overlap.
                resource.TimeSlots.ForEach(delegate (AvailableTime outerTime)
                {
                    resource.TimeSlots.ForEach(delegate (AvailableTime innerTime)
                    {
                        if (!(((outerTime.From <= innerTime.From) && (outerTime.To <= innerTime.From)) ||
                        ((outerTime.From >= innerTime.To) && (outerTime.To >= innerTime.To))))
                        {
                            if (outerTime.Id != innerTime.Id)
                            {
                                invalidDate = true;
                            }
                        }
                    });
                });

                if (!invalidDate)
                {
                    _applicationContext.Add(resource);
                    _applicationContext.SaveChanges();
                    _eventBus.PunlicEvent( Events.NewObject, resource);
                    _eventBus.PunlicObject(new ResourceCreatedEvent(resource));
                    return new ApiResponse<Domain.Models.Resource>(Models.ApiResponseCode.OK, resource);
                }
                else
                {
                    return new ApiResponse<Domain.Models.Resource>(Models.ApiResponseCode.NotModified, null);
                }
            }
            catch (Exception)
            {
                return new ApiResponse<Domain.Models.Resource>(Models.ApiResponseCode.InternalServerError, null);
            }
        }

        #endregion
        #region Get
        //Get all resources
        public ApiResponse<List<Domain.Models.Resource>> Get()
        {
            try
            {   //Returns a list of Resources, with all of its children
                return new ApiResponse<List<UCLDreamTeam.Resource.Domain.Models.Resource>>
                    (Models.ApiResponseCode.OK, _applicationContext.Resources
                    .Include(resource => resource.TimeSlots)
                    .ToList());
            }
            catch (Exception)
            {
                return new ApiResponse<List<Domain.Models.Resource>>(Models.ApiResponseCode.InternalServerError, null);
            }
        }

        //Get a specific resource from a GUID
        public ApiResponse<Domain.Models.Resource> Get(Guid guid)
        {
            Domain.Models.Resource resourceToReturn = new Domain.Models.Resource();

            try
            {
                //Gets a resource with all of its children
               resourceToReturn = _applicationContext.Resources
                    .Include(resource => resource.TimeSlots)
                    .SingleOrDefault(resource => resource.Id == guid);

                if (resourceToReturn == null)
                {
                    return new ApiResponse<Domain.Models.Resource>(Models.ApiResponseCode.NoContent, null);
                }
                else
                {
                    return new ApiResponse<Domain.Models.Resource>(Models.ApiResponseCode.OK, resourceToReturn);
                }
            }
            catch (Exception)
            {
                return new ApiResponse<Domain.Models.Resource>(Models.ApiResponseCode.InternalServerError, null);
            }
        }
        #endregion
        #region Update
        //Update a resource
        //Should ignore reservations when being updated
        public async Task<ApiResponse<Domain.Models.Resource>> Update(Domain.Models.Resource resource)
        {
            Domain.Models.Resource resourceToUpdate = new Domain.Models.Resource();

            bool invalidDate = false;

            try
            {
                resourceToUpdate = _applicationContext.Resources.Include(r => r.TimeSlots).SingleOrDefault(r => r.Id == resource.Id);

                if (resourceToUpdate == null)
                {
                    return new ApiResponse<Domain.Models.Resource>(Models.ApiResponseCode.NoContent, null);
                }

                //Checks that the timeslots do not overlap. Also makes sure that he id is not the same, if the times do match.
                resource.TimeSlots.ForEach(delegate (AvailableTime newTimeSlot)
                {
                    resourceToUpdate.TimeSlots.ForEach(delegate (AvailableTime existingTimeSlot)
                    {
                        if (!(((newTimeSlot.From <= existingTimeSlot.From) && (newTimeSlot.To <= existingTimeSlot.From)) ||
                        ((newTimeSlot.From >= existingTimeSlot.To) && (newTimeSlot.To >= existingTimeSlot.To))))
                        {
                            if (newTimeSlot.Id != existingTimeSlot.Id)
                            {
                                invalidDate = true;
                            }
                        }
                    });
                });

                if (!invalidDate)
                {
                    resourceToUpdate.Name = resource.Name;
                    resourceToUpdate.Description = resource.Description;

                    //Makes sure that new timeslots that is not in db has no ID because of Auto Assignment
                    resource.TimeSlots.ForEach(newTimeSlot =>
                    {
                        if(!resourceToUpdate.TimeSlots.Contains(newTimeSlot))
                            newTimeSlot.Id = Guid.Empty;
                    });
                    resourceToUpdate.TimeSlots = resource.TimeSlots;

                    Domain.Models.Resource OldResourece = await _applicationContext.Resources.FirstOrDefaultAsync(x => x.Id == resourceToUpdate.Id);
                    _applicationContext.Resources.Update(resourceToUpdate);
                    _applicationContext.SaveChanges();
                    _eventBus.PunlicEvent(Events.UpdateObject , resourceToUpdate);
                    _eventBus.PunlicObject(new ResourceUpdatedEvent(resourceToUpdate));

                    return new ApiResponse<Domain.Models.Resource>(Models.ApiResponseCode.OK, resourceToUpdate);
                }
                else
                {
                    return new ApiResponse<Domain.Models.Resource>(Models.ApiResponseCode.NotModified, null);
                }
            }
            catch (Exception)
            {
                return new ApiResponse<Domain.Models.Resource>(Models.ApiResponseCode.InternalServerError, null);
            }
        }
        #endregion
        #region Delete
        //Delete a resource
        public async Task<ApiResponse<Domain.Models.Resource>> Delete(Guid guid)
        {
            Domain.Models.Resource resourceToDelete = new Domain.Models.Resource();

            try
            {
                //Gets the resource to be deleted. All children are tracked as well, so that they are also deleted.
                resourceToDelete = _applicationContext.Resources
                    .Include(resource => resource.TimeSlots)
                    .SingleOrDefault(resource => resource.Id == guid);

                if (resourceToDelete == null)
                {
                    return new ApiResponse<Domain.Models.Resource>(Models.ApiResponseCode.NoContent, null);
                }
                else
                {
                    var resource = await _applicationContext.Resources.FirstOrDefaultAsync(x => x.Id == resourceToDelete.Id);
                    //This will cascade delete.
                    _applicationContext.Resources.Remove(resourceToDelete);
                    
                    _applicationContext.SaveChanges();

                    _eventBus.PunlicEvent(Events.DeleateObject, resource);
                    _eventBus.PunlicObject(new ResourceDeletedEvent(resource));
                    return new ApiResponse<Domain.Models.Resource>(Models.ApiResponseCode.OK, null);
                }
            }
            catch (Exception)
            {
                return new ApiResponse<Domain.Models.Resource>(Models.ApiResponseCode.InternalServerError, null);
            }
        }
        #endregion
    }
}
