using System.Threading.Tasks;

namespace SignalR_Microservice.Hubs
{
    public interface IQueueService
    {
        void Enqueue(string connectionId);
        Task Dequeue(string groupId);
    }
}