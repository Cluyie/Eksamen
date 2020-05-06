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
            QueLog = new object();
            EventBus = eventBus;
        }
        public void GetSuport(string name, string description, Guid objctId, Guid userId)
        {
            Que.Add(new ServiceQue { Name = name, SignelRId = Context.ConnectionId, Description = description, ObjctId = objctId, UserId = userId });
            Clients.Caller.SendAsync("CorrentInQue", Que.Count);
        }
        public async Task Reconect(string GroopId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, GroopId);
            await Clients.Group(GroopId).SendAsync("ConectionCreadet", GroopId);
        }
        public async Task NextInQue(Guid SuporderId)
        {
            Guid GroopId = Guid.NewGuid();
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
                await Groups.AddToGroupAsync(Needhelper.SignelRId, GroopId.ToString());
                await Groups.AddToGroupAsync(Context.ConnectionId, GroopId.ToString());
                await Clients.Group(GroopId.ToString()).SendAsync("ConectionCreadet", GroopId);
                await Clients.All.SendAsync("GroopPosisionDown");
                EventBus.PublishEvent(new ConectionCreadetEvent(new Conection() { GroopId = GroopId,Name = Needhelper.Name, ResourceId = Needhelper.ObjctId,SuportId = SuporderId, KundeId = Needhelper.UserId, Description = Needhelper.Description }));
            }
        }
        public async Task SendMessage(Guid GroopId, Message Message)
        {
            await Clients.Group(GroopId.ToString()).SendAsync("NewMessage", Message.Text);
            EventBus.PublishEvent(new MessageSentEvent(Message));
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Que.RemoveAll(x => x.SignelRId == Context.ConnectionId);
        }
    }
}
