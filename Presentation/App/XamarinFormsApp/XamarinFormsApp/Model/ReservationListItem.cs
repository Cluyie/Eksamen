using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinFormsApp.Model
{
   public class ReservationListItem
    {
        public Guid Id { get; set; }

        public string TimeSpan { get => " Fra  " + From + "  Til  " + To; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
        public Guid UserId { get; set; }
        public Guid ResourceId { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }
    }
}
