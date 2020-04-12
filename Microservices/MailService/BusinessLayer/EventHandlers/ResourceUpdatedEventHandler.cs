using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Mail.Domain.Events;
using UCLDreamTeam.Mail.Domain.Interfaces;
using UCLDreamTeam.Mail.Domain.Models;

namespace UCLDreamTeam.Mail.Domain.EventHandlers
{
    public class ResourceUpdatedEventHandler : IEventHandler<ResourceUpdatedEvent>
    {
        private readonly IGenericRepository<Resource> _resourceRepository;

        public ResourceUpdatedEventHandler(IGenericRepository<Resource> resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task Handle(ResourceUpdatedEvent @event)
        {
            await _resourceRepository.Update(@event.Resource.Id, new Resource
            {
                Name = @event.Resource.Name
            });
        }
    }
}
