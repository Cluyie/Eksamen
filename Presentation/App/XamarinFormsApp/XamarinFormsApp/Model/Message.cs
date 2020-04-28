using System;

namespace XamarinFormsApp.Model
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }


        //public string Content { get; set; }
        //public string Username { get; set; }
        //public Guid Id { get; set; }
        //public Guid TicketId { get; set; }
        //public string Text { get; set; }
        //public DateTime TimeStamp { get; set; }
        //public bool Seen { get; set; }
    }
}