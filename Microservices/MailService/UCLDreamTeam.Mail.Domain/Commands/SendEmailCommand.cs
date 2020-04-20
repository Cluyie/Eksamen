using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using RabbitMQ.Bus.Bus.Interfaces;
using RabbitMQ.Bus.Commands;
using UCLDreamTeam.Mail.Domain.Models;
using UCLDreamTeam.SharedInterfaces.Mail;

namespace UCLDreamTeam.Mail.Domain.Commands
{
    public class SendEmailCommand : Command
    {
        public Template Template { get;  }
        public Reservation Reservation { get; }

        public SendEmailCommand(Reservation reservation, Template template)
        {
            Reservation = reservation;
            Template = template;
        }
    }
}
