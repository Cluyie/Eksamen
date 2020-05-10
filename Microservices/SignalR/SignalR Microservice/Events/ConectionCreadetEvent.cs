﻿using SignalR_Microservice.Models;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using Conection = SignalR_Microservice.Models.Conection;

namespace SignalR_Microservice.Events
{
    public class ConectionCreadetEvent
    {
        public IConection Conection { get; set; }

        public ConectionCreadetEvent(Conection conection)
        {
            Conection = conection;
        }
    }
}