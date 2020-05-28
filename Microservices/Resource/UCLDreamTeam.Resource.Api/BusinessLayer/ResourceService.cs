using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Resource.Data.Context;
using UCLDreamTeam.Resource.Domain.Interfaces;
using UCLDreamTeam.Resource.Domain.Models;
using UCLDreamTeam.Resource.Domain.RabbitMQEvents;

namespace UCLDreamTeam.Resource.Api.BusinessLayer
{
    public class ResourceService : IResourceService
    {
        private ResourceContext _applicationContext;
        private IEventBus _eventBus;
        public ResourceService(ResourceContext applicationContext, IEventBus eventBus)
        {
            _applicationContext = applicationContext;
            _eventBus = eventBus;
        }

        #region Create
        //Creates a Resource
        public ApiResponse<Domain.Models.Resource> Create(Domain.Models.Resource Resource)
        {
            bool invalidDate = false;

            try
            {
                if (Resource == null)
                {
                    return new ApiResponse<Domain.Models.Resource>(ApiResponseCode.NoContent, null);
                }

                foreach (var timeslot in Resource.TimeSlots)
                {
                    timeslot.Id = Guid.NewGuid();
                }

                //Checks that the timeslots do not overlap.
                Resource.TimeSlots.ForEach(delegate (AvailableTime outerTime)
                {
                    Resource.TimeSlots.ForEach(delegate (AvailableTime innerTime)
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
                    _applicationContext.Add(Resource);
                    _applicationContext.SaveChanges();

                    _eventBus.PublishEvent(new ResourceCreatedEvent(Resource));
                    return new ApiResponse<Domain.Models.Resource>(ApiResponseCode.OK, Resource);
                }
                else
                {
                    return new ApiResponse<Domain.Models.Resource>(ApiResponseCode.NotModified, null);
                }
            }
            catch (Exception e)
            {
                return new ApiResponse<Domain.Models.Resource>(ApiResponseCode.InternalServerError, null);
            }
        }

        #endregion
        #region Get
        //Get all Resources
        public ApiResponse<List<Domain.Models.Resource>> Get()
        {
            try
            {   //Returns a list of Resources, with all of its children
                return new ApiResponse<List<UCLDreamTeam.Resource.Domain.Models.Resource>>
                    (ApiResponseCode.OK, _applicationContext.Resources
                    .Include(Resource => Resource.TimeSlots)
                    .ToList());
            }
            catch (Exception)
            {
                return new ApiResponse<List<Domain.Models.Resource>>(ApiResponseCode.InternalServerError, null);
            }
        }

        //Get a specific Resource from a GUID
        public ApiResponse<Domain.Models.Resource> Get(Guid guid)
        {
            Domain.Models.Resource ResourceToReturn = new Domain.Models.Resource();

            try
            {
                //Gets a Resource with all of its children
               ResourceToReturn = _applicationContext.Resources
                    .Include(Resource => Resource.TimeSlots)
                    .SingleOrDefault(Resource => Resource.Id == guid);

                if (ResourceToReturn == null)
                {
                    return new ApiResponse<Domain.Models.Resource>(ApiResponseCode.NoContent, null);
                }
                else
                {
                    return new ApiResponse<Domain.Models.Resource>(ApiResponseCode.OK, ResourceToReturn);
                }
            }
            catch (Exception)
            {
                return new ApiResponse<Domain.Models.Resource>(ApiResponseCode.InternalServerError, null);
            }
        }
        #endregion
        #region Update
        //Update a Resource
        //Should ignore reservations when being updated
        public async Task<ApiResponse<Domain.Models.Resource>> Update(Domain.Models.Resource Resource)
        {
            Domain.Models.Resource ResourceToUpdate = new Domain.Models.Resource();

            bool invalidDate = false;

            try
            {
                ResourceToUpdate = _applicationContext.Resources.Include(r => r.TimeSlots).SingleOrDefault(r => r.Id == Resource.Id);

                if (ResourceToUpdate == null)
                {
                    return new ApiResponse<Domain.Models.Resource>(ApiResponseCode.NoContent, null);
                }

                //Checks that the timeslots do not overlap. Also makes sure that he id is not the same, if the times do match.
                Resource.TimeSlots.ForEach(delegate (AvailableTime newTimeSlot)
                {
                    ResourceToUpdate.TimeSlots.ForEach(delegate (AvailableTime existingTimeSlot)
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
                    ResourceToUpdate.Name = Resource.Name;
                    ResourceToUpdate.Description = Resource.Description;

                    //Makes sure that new timeslots that is not in db has no ID because of Auto Assignment
                    Resource.TimeSlots.ForEach(newTimeSlot =>
                    {
                        if(!ResourceToUpdate.TimeSlots.Contains(newTimeSlot))
                            newTimeSlot.Id = Guid.Empty;
                    });
                    ResourceToUpdate.TimeSlots = Resource.TimeSlots;

                    Domain.Models.Resource OldResourece = await _applicationContext.Resources.FirstOrDefaultAsync(x => x.Id == ResourceToUpdate.Id);
                    _applicationContext.Resources.Update(ResourceToUpdate);
                    _applicationContext.SaveChanges();
                    _eventBus.PublishEvent(new ResourceUpdatedEvent(ResourceToUpdate));

                    return new ApiResponse<Domain.Models.Resource>(ApiResponseCode.OK, ResourceToUpdate);
                }
                else
                {
                    return new ApiResponse<Domain.Models.Resource>(ApiResponseCode.NotModified, null);
                }
            }
            catch (Exception)
            {
                return new ApiResponse<Domain.Models.Resource>(ApiResponseCode.InternalServerError, null);
            }
        }
        #endregion
        #region Delete
        //Delete a Resource
        public async Task<ApiResponse<Domain.Models.Resource>> Delete(Guid guid)
        {
            Domain.Models.Resource ResourceToDelete = new Domain.Models.Resource();

            try
            {
                //Gets the Resource to be deleted. All children are tracked as well, so that they are also deleted.
                ResourceToDelete = _applicationContext.Resources
                    .Include(Resource => Resource.TimeSlots)
                    .SingleOrDefault(Resource => Resource.Id == guid);

                if (ResourceToDelete == null)
                {
                    return new ApiResponse<Domain.Models.Resource>(ApiResponseCode.NoContent, null);
                }
                else
                {
                    var Resource = await _applicationContext.Resources.FirstOrDefaultAsync(x => x.Id == ResourceToDelete.Id);
                    //This will cascade delete.
                    _applicationContext.Resources.Remove(ResourceToDelete);
                    
                    _applicationContext.SaveChanges();

                    _eventBus.PublishEvent(new ResourceDeletedEvent(Resource));
                    return new ApiResponse<Domain.Models.Resource>(ApiResponseCode.OK, null);
                }
            }
            catch (Exception)
            {
                return new ApiResponse<Domain.Models.Resource>(ApiResponseCode.InternalServerError, null);
            }
        }
        #endregion
    }
}
