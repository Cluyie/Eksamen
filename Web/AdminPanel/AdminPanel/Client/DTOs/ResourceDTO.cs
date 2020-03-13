using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Client.DTOs
{
    public class ResourceDTO
    {
        public Guid Id;

        public string Name;

        public List<TimeslotDTO> Timeslots;
    }
}
