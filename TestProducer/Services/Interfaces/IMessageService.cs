using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProducer.Services.Interfaces
{
    public interface IMessageService
    {
        Task Send(string message);
    }
}