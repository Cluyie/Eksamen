﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces;

namespace UCLDreamTeam.Ticket.Application.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<Domain.Models.Ticket>> GetAsync();
        Task<Domain.Models.Ticket> GetByIdAsync(Guid id);
        Task AddAsync(Domain.Models.Ticket reservation);
        Task ChangeStatusById(Guid id, Status status);
    }
}
