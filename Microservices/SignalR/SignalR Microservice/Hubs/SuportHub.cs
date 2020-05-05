using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Bus.Bus.Interfaces;
using RabbitMQ.Client;
using SignalR_Microservice.Events;
using SignalR_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignalR_Microservice.Hubs
{
    public class SuportHub: Hub
    {
        private static object QueLog;
        public List<ServiceQue> Que { get; set; }
        public readonly IEventBus EventBus;
        public SuportHub(List<ServiceQue> que, IEventBus eventBus)
        {
            Que = que;
            EventBus = eventBus;
        }
        public void FindService(string Description, string ObjctId)
        {
            Que.Add(new ServiceQue { SignelRId = Context.ConnectionId, Description = Description, ObjctId = ObjctId });
            Clients.Caller.SendAsync("CorrentInQue", Que.Count);
        }
        public async Task Reconect(string GroopId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, GroopId);
            await Clients.Group(GroopId).SendAsync("ConectionCreadet", GroopId);
        }
        public async Task NextInQue()
        {
            string GroopId = Guid.NewGuid().ToString();
            ServiceQue Needhelper = null;
            lock(QueLog)
            {
                if (Que.Count > 0)
                { 
                    Needhelper = Que[0];
                    Que.RemoveAt(0);
                }
            }
            if(Needhelper != null)
            {
                await Groups.AddToGroupAsync(Needhelper.SignelRId, GroopId);
                await Groups.AddToGroupAsync(Context.ConnectionId, GroopId);
                await Clients.Group(GroopId).SendAsync("ConectionCreadet", GroopId);
                await Clients.All.SendAsync("GroopPosisionDown");
                EventBus.PublishEvent(new ConectionCreadetEvent(new Conection() { GroopId = GroopId }));
            }
        }
        public async Task SendMessage(string GroopId,string Message)
        {
            await Clients.Group(GroopId).SendAsync("NewMessage", Message);
            EventBus.PublishEvent(new MessageSentEvent(new Message() {Text = Message, GroopId = GroopId, TimeStamp = DateTime.UtcNow }));
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Que.RemoveAll(x => x.SignelRId == Context.ConnectionId);
        }
    }
}
