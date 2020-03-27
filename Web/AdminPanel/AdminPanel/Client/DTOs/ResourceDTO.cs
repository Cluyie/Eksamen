using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Client.DTOs
{
    public class ResourceDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<TimeslotDTO> Timeslots { get; set; }
    }
}
