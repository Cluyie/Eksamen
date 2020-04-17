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
    public class ResourceDeletedEventHandler : IEventHandler<ResourceDeletedEvent>
    {
        private readonly IGenericRepository<Resource> _resourceRepository;

        public ResourceDeletedEventHandler(IGenericRepository<Resource> resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task Handle(ResourceDeletedEvent @event)
        {
            await _resourceRepository.Delete(@event.Resource.Id);
        }
    }
}
