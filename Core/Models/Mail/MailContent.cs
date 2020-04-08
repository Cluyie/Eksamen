namespace Models.Mail
{
    public class MailContent
    {
        public Template Template { get; set; }
        public string Recipient { get; set; }
        public string[] CcRecipients { get; set; } = null;
        public string TitleContent { get; set; }
        public string BodyContent { get; set; }
    }
}