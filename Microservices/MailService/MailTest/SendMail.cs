using BusinessLayer;
using Microsoft.OpenApi.Extensions;
using Models.Mail;
using NUnit.Framework;
using UCLDreamTeam.Mail.Domain;
using UCLDreamTeam.Mail.Domain.Models;

namespace MailTest
{
    public class Tests
    {
        private MailService MailService { get; set; }

        [SetUp]
        public void Setup()
        {
            MailService = new MailService();
        }

        [Test]
        public void Send()
        {
            MailService.SendMail(MailService
                .GenerateMail(new User {Email = "krelle1010@gmail.com"}, Template.BookingConfirmation.GetDisplayName(),
                    "Hej"));
        }
    }
}