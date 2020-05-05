using RabbitMQ.Bus.Events;
using SignalR_Microservice.Models;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using Message = SignalR_Microservice.Models.Message;

namespace SignalR_Microservice.Events
{
    public class ConectionCreadetEvent : Event
    {
        public IConection Conection { get; set; }

        public ConectionCreadetEvent(Conection conection)
        {
            Conection = conection;
        }
    }
}