﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces;
using UCLDreamTeam.Ticket.Domain.Models;

namespace UCLDreamTeam.Ticket.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Models.Ticket>> GetAsync();
        Task<Models.Ticket> GetByIdAsync(Guid id);
        Task AddAsync(Models.Message message);
        Task ChangeStatusById(Guid id, Status status);
        Task MessageSeen(Guid messageId, bool seen);
    }
}
