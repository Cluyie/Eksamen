using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Text;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Mail.Application.Interfaces;
using UCLDreamTeam.Mail.Application.Properties;
using UCLDreamTeam.Mail.Domain.Commands;
using UCLDreamTeam.Mail.Domain.Events;
using UCLDreamTeam.Mail.Domain.Models;
using UCLDreamTeam.SharedInterfaces.Mail;
using UCLToolBox;

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