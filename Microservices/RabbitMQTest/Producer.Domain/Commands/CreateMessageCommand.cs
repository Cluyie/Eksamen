namespace Producer.Domain.Commands
{
    public class CreateMessageCommand : MessageCommand
    {
        public CreateMessageCommand(string message)
        {
            Message = message;
        }
    }
}