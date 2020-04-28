using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinFormsApp.Model
{
   public class ReservationMock
    {
        public Guid Id { get; set; }

        public DateTime From{ get; set; }

        public DateTime To { get; set; }
        public Guid UserId { get; set; }
        public Guid ResourceId { get; set; }
    }
}
