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
        QueSendService QueSend { get; set; }
        private Queue<(string, Guid)> _connectionQueue;

        public int QueueCount
        {
            get => QueueCount;
            set => QueueCount = _connectionQueue.Count();
        }

        public QueueService(Queue<(string, Guid)> connectionQueue, QueSendService queSend)
        {
            _connectionQueue = connectionQueue;
            QueSend = queSend;
        }

        public async void Enqueue(string connectionId, Guid ticketId)
        {
            _connectionQueue.Enqueue((connectionId, ticketId));
            await QueSend.SendToReceiveIndex(connectionId, _connectionQueue.Count);
            await QueSend.SendReceiveQueueCount(connectionId, QueueCount);
        }

        public async Task<Guid> Dequeue(string groupId)
        {
            if (_connectionQueue.Any())
            {
                var connectionInfo = _connectionQueue.Dequeue();
                await QueSend.SendReceiveGroupId(connectionInfo.Item1, groupId);


                foreach (var connection in _connectionQueue)
                {
                    var connectionId = connection.ToString();
                    await QueSend.SendToReceiveIndex(connectionId,
                        Array.IndexOf(_connectionQueue.ToArray(), connectionId) + 1);
                }

                return connectionInfo.Item2;
            }

            return Guid.Empty;
        }
    }
}