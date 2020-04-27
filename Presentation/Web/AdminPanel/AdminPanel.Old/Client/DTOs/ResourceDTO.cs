using System;
using System.Collections.Generic;

namespace AdminPanel.Client.DTOs
{
    public class ResourceDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<TimeslotDTO> Timeslots { get; set; }
    }
}