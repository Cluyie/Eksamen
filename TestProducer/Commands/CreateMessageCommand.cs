using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestProducer.Commands
{
    public class CreateMessageCommand : MessageCommand
    {
        public CreateMessageCommand(string message)
        {
            Message = message;
        }
    }
}