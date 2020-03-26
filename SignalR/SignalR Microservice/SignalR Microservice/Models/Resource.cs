﻿using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR_Microservice.Models
{
    public class Resource : IResource<Reservation, ReserveTime, AvailableTime>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<AvailableTime> TimeSlots { get; set; }
    }
}