using RabbitMQ.Bus.Commands;

namespace Producer.Domain.Commands
{
    public class MessageCommand : Command
    {
        public string Message { get; set; }
    }
}