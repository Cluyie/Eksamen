using Microsoft.AspNetCore.SignalR;
using SignalR_Microservice.Commands;
using SignalR_Microservice.Helpers;
using SignalR_Microservice.Models;
using SignalR_Microservice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Microservice.Hubs
{
    public class ChatHub : Hub
    {
        private Dictionary<string, List<User>> _roomsWithUsers;
        private User currentUser = new User();
        private List<User> connectedUsers = new List<User>();

        private IRoomUsersHandler _roomUsersHandler;
        private IChatLoggingService _messageLogging;

        public ChatHub(IRoomUsersHandler roomUsersHandler, IChatLoggingService messageLogging, Dictionary<string,List<User>> roomsWithUsers)
        {
            _messageLogging = messageLogging;
            _roomUsersHandler = roomUsersHandler;
            _roomsWithUsers = roomsWithUsers;
        }

        public async Task CreateRoom(string roomName)
        {
            currentUser.Id = Context.ConnectionId;

            //var response = await _roomUsersHandler.AddUserToRoom(roomsWithUsers, roomName, currentUser);

            _roomsWithUsers.Add(roomName, new List<User>());
            _roomsWithUsers[roomName].Add(currentUser);

            await Groups.AddToGroupAsync(currentUser.Id, roomName);
            await Clients.Caller.SendAsync("CreateRoom", $"You have now created room '{roomName}'");
        }

        public async Task SendMessageToRoom(Message message, string roomName)
        {
            await Clients.Group(roomName).SendAsync("SendMessageToRoom", message.Content);

            //await _messageLogging.SendMessageAsync(message);
        }

        public async Task JoinGroup(string roomName)
        {
            var userId = Context.ConnectionId;

            var response = await _roomUsersHandler.AddUserToRoom(_roomsWithUsers, roomName, currentUser);

            if(response.Item3 != null)
            {
                userId = response.Item3.Id;
            }

            _roomsWithUsers = response.Item1;
            if( response.Item2 == true) 
            {
                await Groups.AddToGroupAsync(userId, roomName);
                await Clients.OthersInGroup(roomName).SendAsync("JoinGroup", $"{userId} has entered the room {roomName}.");
                await Clients.Caller.SendAsync("NewEnteredUser", $"You are now connected to room {roomName}");
            }         

            

        }

        //public async Task UserTyping(bool check)
        //{
        //    var currentUser = connectedUsers.SingleOrDefault(r => r.Id == Context.ConnectionId);

        //    if (check)
        //    {
        //    }
        //}

        public async Task RemoveFromRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);

            await Clients.Group(roomName).SendAsync("SendMessage", $"{Context.ConnectionId} has left the room {roomName}.");
        }
    }
}
