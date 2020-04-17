using System;
using Models.Mail;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Mail.Application.Interfaces;
using UCLDreamTeam.Mail.Domain.Commands;
using UCLDreamTeam.Mail.Domain.Models;

namespace UCLDreamTeam.Mail.Application.Services
{
    public class MailService : IMailService
    {
        private readonly IEventBus _eventBus;

        public MailService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async void SendMail(Reservation reservation, Template template)
        {
            await _eventBus.SendCommand(new SendEmailCommand(reservation, template));
        }
    }
}