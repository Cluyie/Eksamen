using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer.Models
{
    public class ReserveTime : IReserveTime
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
