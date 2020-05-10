using Microsoft.AspNetCore.SignalR;
using RabitMQEasy;
using SignalR_Microservice.Events;
using SignalR_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace SignalR_Microservice.Hubs
{
    public class SuportHub: Hub
    {
        private static object QueLog;
        public List<ServiceQue> Que { get; set; }
        public readonly RabitMQPublicer EventBus;
        public SuportHub(List<ServiceQue> que, RabitMQPublicer eventBus)
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
                IConection c = new Conection(){ GroopId = GroopId, Name = Needhelper.Name, ResourceId = Needhelper.ObjctId, SuportId = SuporderId, KundeId = Needhelper.UserId, Description = Needhelper.Description };
                EventBus.PunlicEvent(RabitMQEasy.Events.NewObject, c);
            }
        }
        public async Task SendMessage(Message Message)
        {
            await Clients.Group(Message.GroopId.ToString()).SendAsync("NewMessage", Message.Text);
            EventBus.PunlicEvent<IMessage>(RabitMQEasy.Events.NewObject, Message);
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Que.RemoveAll(x => x.SignelRId == Context.ConnectionId);
        }
    }
}
