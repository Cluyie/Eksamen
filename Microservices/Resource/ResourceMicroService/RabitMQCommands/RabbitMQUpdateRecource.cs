using RabbitMQ.Bus.Commands;

namespace ResourceMicroService.RabitMQCommands
{
    public class RabbitMQUpdateRecource : Command
    {

        public Resource InputResource { get; set; }
        public Models.Resource ResourceToChange { get; set; }
        public RabbitMQUpdateRecource()
        {

        }
    }
}
