using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.User.Domain.Commands;
using UCLDreamTeam.User.Domain.Events;

namespace UCLDreamTeam.User.Domain.CommandHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IEventBus _eventBus;
        private readonly DbContext _identityContext;
        private readonly Mapper _mapper;

        public UpdateUserCommandHandler(IEventBus eventBus, DbContext identityContext, Mapper mapper)
        {
            _eventBus = eventBus;
            _identityContext = identityContext;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Finds the user
                var dbUser = _identityContext.Find<Models.User>(request.UserToChange.Id);

                //Prevent changing the ID
                request.UserToChange.Id = Guid.Empty;
                
                // Can only update an existing user
                if (request.UserToChange == null)
                {
                    _eventBus.PublishEvent(new NoUserFoundEvent(request.UserToChange));
                    return false;
                }

                // Update the user
                if (!string.IsNullOrWhiteSpace(request.UserToChange.PasswordHash) &&
                    request.InputUser.PasswordHash != request.UserToChange.PasswordHash)
                {
                    //If the password is unchanged or empty, this does not update the password
                    request.UserToChange.PasswordHash = request.UserToChange.PasswordHash;
                }
                // Automapper is configured to only overwrite the fields that are not null
                _mapper.Map(request.UserToChange, dbUser);

                _identityContext.Update(dbUser);
                _identityContext.SaveChanges();

                _eventBus.PublishEvent(new UserUpdatedEvent(dbUser));
                return true;
            }
            catch (Exception e)
            {
                _eventBus.PublishEvent(new UserUpdateFailedEvent(request.UserToChange, e));
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
