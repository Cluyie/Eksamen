using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalR_Microservice.Services;

namespace SignalR_Microservice.Hubs
{
    public class QueueService : IQueueService
    {
        private QueueSendService QueueSend { get; set; }
        private Queue<(string, Guid)> _connectionQueue;

        public int QueueCount => _connectionQueue.Count();

        public QueueService(Queue<(string, Guid)> connectionQueue, QueueSendService queueSend)
        {
            _connectionQueue = connectionQueue;
            QueueSend = queueSend;
        }

        public async void Enqueue(string connectionId, Guid ticketId)
        {
            _connectionQueue.Enqueue((connectionId, ticketId));
            await QueueSend.SendToReceiveIndex(connectionId, _connectionQueue.Count);
            await QueueSend.SendReceiveQueueCountAll(QueueCount);
        }

        public async Task<Guid> Dequeue(string groupId)
        {
            if (_connectionQueue.Any())
            {
                var connectionInfo = _connectionQueue.Dequeue();
                await QueueSend.SendReceiveGroupId(connectionInfo.Item1, groupId);


                foreach (var connection in _connectionQueue)
                {
                    var connectionId = connection.ToString();
                    await QueueSend.SendToReceiveIndex(connectionId,
                        Array.IndexOf(_connectionQueue.ToArray(), connectionId) + 1);
                }

                await QueueSend.SendReceiveQueueCountAll(QueueCount);
                return connectionInfo.Item2;
            }

            return Guid.Empty;
        }
    }
}