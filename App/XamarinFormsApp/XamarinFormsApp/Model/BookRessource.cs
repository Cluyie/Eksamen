using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinFormsApp.Model
{
    public class BookRessource : AutoMapper.Profile, IResource
    {
        public Guid Id { get; set; }
        public string strName { get; set; }
        public List<IReservation> Reservations { get; set; }
        public List<IAvailableTime> TimeSlot { get; set; }
    }
}
