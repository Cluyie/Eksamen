using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using UCLDreamTeam.Mail.Domain.Models;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLDreamTeam.SharedInterfaces.Mail;

namespace UCLDreamTeam.Mail.Application.Interfaces
{
    public interface IMailService
    {
        Task SendMail(Reservation reservation, Template template);
        Task SendChatLog(TicketDTO ticketDTO);
    }
}