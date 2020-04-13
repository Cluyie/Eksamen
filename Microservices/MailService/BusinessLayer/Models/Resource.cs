using System;
using System.Collections.Generic;
using Models.Interfaces;
using UCLDreamTeam.Mail.Domain.Interfaces;

namespace UCLDreamTeam.Mail.Domain.Models
{
    public class Resource : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}