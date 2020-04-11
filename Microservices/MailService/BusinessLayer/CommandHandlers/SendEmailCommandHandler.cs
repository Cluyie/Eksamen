using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Mail.Domain.Commands;
using UCLDreamTeam.Mail.Domain.Events;

namespace UCLDreamTeam.Mail.Domain.CommandHandlers
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, bool>
    {
        private readonly IEventBus _eventBus;

        public SendEmailCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.SmtpClient.Send(request.MailMessage);
                _eventBus.PublishEvent(new EmailSendSuccessEvent(request.MailMessage));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _eventBus.PublishEvent(new SendFailedEvent(request.MailMessage, e));
                throw;
            }
        }
    }
}
