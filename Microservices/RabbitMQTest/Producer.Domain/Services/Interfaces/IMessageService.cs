using System.Threading.Tasks;

namespace Producer.Domain.Services.Interfaces
{
    public interface IMessageService
    {
        Task Send(string message);
    }
}