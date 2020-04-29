using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalR_Microservice.Hubs
{
    public class QueueService : IQueueService
    {
        private Queue<(string, Guid)> _connectionQueue;

        public int QueueCount
        {
            get => QueueCount;
            set => QueueCount = _connectionQueue.Count();
        }

        public QueueService(Queue<(string, Guid)> connectionQueue)
        {
            _connectionQueue = connectionQueue;
        }

        public async void Enqueue(string connectionId, Guid ticketId)
        {
            _connectionQueue.Enqueue((connectionId, ticketId));
            await _hubContext.GetIndex(connectionId, _connectionQueue.Count);
            await _hubContext.GetQueueCount();
        }

        public async Task<Guid> Dequeue(string groupId)
        {
            if (_connectionQueue.Any())
            {
                var connectionInfo = _connectionQueue.Dequeue();
                await _hubContext.SendGroupId(connectionInfo.Item1, groupId);


                foreach (var connection in _connectionQueue)
                {
                    var connectionId = connection.ToString();
                    await _hubContext.GetIndex(connectionId,
                        Array.IndexOf(_connectionQueue.ToArray(), connectionId) + 1);
                }

                return connectionInfo.Item2;
            }

            return Guid.Empty;
        }
    }
}