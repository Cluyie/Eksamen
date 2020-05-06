﻿using System;
using System.Collections.Generic;
using UCLDreamTeam.SharedInterfaces;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Mail.Domain.Models
{
    public class Ticket : ITicket<Message>
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Message> Messages { get; set; }
        public Guid? ResourceId { get; set; }
    }
}