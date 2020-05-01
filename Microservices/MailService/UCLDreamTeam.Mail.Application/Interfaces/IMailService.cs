using System.Collections.Generic;
using System.Net.Mail;
using UCLDreamTeam.Mail.Domain.Models;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLDreamTeam.SharedInterfaces.Mail;

namespace UCLDreamTeam.Mail.Application.Interfaces
{
    public interface IMailService
    {
        public void SendMail(Reservation reservation, Template template);
        void SendChatLog(IEnumerable<IMessage> messages, Template template);
    }
}