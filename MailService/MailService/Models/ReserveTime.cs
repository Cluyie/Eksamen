using System;
using Models.Interfaces;

namespace MailService.Models
{
    public class ReserveTime : IReserveTime
    {
        public Guid Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}