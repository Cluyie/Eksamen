using System;

namespace Models
{
    public interface IReserveTime
    {
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
    }
}