using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalR_Microservice.Hubs
{
    internal class QueueService : IQueueService
    {
        private Queue<string> _connectionQueue;
        private readonly QueueHub _hubContext;

        public QueueService(QueueHub hubContext, Queue<string> connectionQueue)
        {
            _hubContext = hubContext;
            _connectionQueue = connectionQueue;
        }

        public async void Enqueue(string connectionId)
        {
            _connectionQueue.Enqueue(connectionId);
            await _hubContext.GetIndex(connectionId, _connectionQueue.Count);
        }

        public async Task Dequeue(string groupId)
        {
            if (_connectionQueue.Any())
            {
                var id = _connectionQueue.Dequeue();
                await _hubContext.SendGroupId(id, groupId);

                foreach (var connection in _connectionQueue)
                {
                    var connectionId = connection.ToString();
                    await _hubContext.GetIndex(connectionId,
                        Array.IndexOf(_connectionQueue.ToArray(), connectionId) + 1);
                }
            }
        }
    }
}