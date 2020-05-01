using System;
using System.Collections.Generic;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Mail.Application.Interfaces;
using UCLDreamTeam.Mail.Domain.Commands;
using UCLDreamTeam.Mail.Domain.Models;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLDreamTeam.SharedInterfaces.Mail;

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

        public async void SendChatLog(IEnumerable<IMessage> messages, Template template)
        {
            await _eventBus.SendCommand(new SendChatLogCommand(messages, template));
        }
    }
}