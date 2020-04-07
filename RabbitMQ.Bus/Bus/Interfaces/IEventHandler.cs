using System.Threading.Tasks;
using RabbitMQ.Bus.Events;

namespace RabbitMQ.Bus.Bus.Interfaces
{
    public interface IEventHandler<in TEvent> : IEventHandler where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandler
    {
    }
}