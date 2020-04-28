using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalR_Microservice.Hubs
{
    internal class QueueService : IQueueService
    {
        private Queue<string> connectionQueue;
        private readonly QueueHub _hubContext;

        public QueueService(QueueHub hubContext)
        {
            hubContext = _hubContext;
        }

        public async void Enqueue(string connectionId)
        {
            connectionQueue.Enqueue(connectionId);
            await _hubContext.GetIndex(connectionId, connectionQueue.Count);
        }

        public async Task Dequeue(string groupId)
        {
            if (connectionQueue.Any())
            {
                var id = connectionQueue.Dequeue();
                await _hubContext.SendGroupId(id, groupId);

                foreach (var connection in connectionQueue)
                {
                    var connectionId = connection.ToString();
                    await _hubContext.GetIndex(connectionId,
                        Array.IndexOf(connectionQueue.ToArray(), connectionId) + 1);
                }
            }
        }
    }
}