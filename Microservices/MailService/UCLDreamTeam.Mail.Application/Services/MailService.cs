using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task SendMail(Reservation reservation, Template template)
        {
            await _eventBus.SendCommand(new SendEmailCommand(reservation, template));
        }

        public async Task SendChatLog(TicketDTO ticketDTO)
        {
            await _eventBus.SendCommand(new SendChatLogCommand(ticketDTO));
        }
    }
}