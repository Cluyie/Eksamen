using BusinessLayer;
using Models;
using Models.Mail;
using NUnit.Framework;

namespace MailTest
{
    public class Tests
    {
        private MailHelper _mailHelper { get; set; }
        [SetUp]
        public void Setup()
        {
            _mailHelper = new MailHelper();
        }

        [Test]
        public void Send()
        {
            _mailHelper.SendMail(_mailHelper
                .GenerateMail(Template.BookingConfirmation, new User() {Email = "krelle1010@gmail.com"}).Result);
        }
    }
}