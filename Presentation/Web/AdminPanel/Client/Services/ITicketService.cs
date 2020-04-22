using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminPanel.Client.Models;

namespace AdminPanel.Client.Services
{
    public interface ITicketService
    {
        Task<Ticket> GetByIdAsync(Guid id);
        Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId);
    }
}
