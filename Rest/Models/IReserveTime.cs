using System;

namespace Data_Access_Layer.Interfaces
{
    public interface IReserveTime
    {
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
    }
}