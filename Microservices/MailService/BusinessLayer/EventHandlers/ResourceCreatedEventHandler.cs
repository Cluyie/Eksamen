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
    public class ResourceCreatedEventHandler : IEventHandler<ResourceCreatedEvent>
    {
        private readonly IGenericRepository<Resource> _resourceRepository;

        public ResourceCreatedEventHandler(IGenericRepository<Resource> resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task Handle(ResourceCreatedEvent @event)
        {
            await _resourceRepository.Create(new Resource
            {
                Id = @event.Resource.Id,
                Name = @event.Resource.Name,
            });
        }
    }
}
