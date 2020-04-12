using System.Net.Mail;
using Models.Interfaces;
using Models.Mail;
using UCLDreamTeam.Mail.Domain.Models;

namespace UCLDreamTeam.Mail.Application.Interfaces
{
    public interface IMailService
    {
        public void SendMail(Reservation reservation, Template template);
    }
}