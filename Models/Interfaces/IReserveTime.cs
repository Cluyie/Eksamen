using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
  public interface IReserveTime
  {
      /// <summary>
      /// Primary key
      /// </summary>
    Guid ReservationId { get; set; }
    DateTime FromDate { get; set; }
    DateTime ToDate { get; set; }
  }
}
