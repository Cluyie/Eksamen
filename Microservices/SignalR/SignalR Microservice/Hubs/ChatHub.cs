using Microsoft.AspNetCore.SignalR;
using SignalR_Microservice.Helpers;
using SignalR_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Microservice.Hubs
{
    public class ChatHub : Hub
    {
        private Dictionary<string, List<User>> roomsWithUsers = new Dictionary<string, List<User>>();
        private RoomUsersHandler roomUsersHandler = new RoomUsersHandler();
        private User currentUser = new User();
        private List<User> connectedUsers = new List<User>();

        public async Task CreateRoom(string roomName)
        {
            currentUser.Id = Context.ConnectionId;

            roomsWithUsers = roomUsersHandler.AddUserToRoom(roomsWithUsers, roomName, currentUser);

            await Groups.AddToGroupAsync(currentUser.Id, roomName);
            await Clients.Caller.SendAsync("CreateRoom", $"You have now created room '{roomName}'");
        }

        public async Task SendMessageToGroup(Message message, string roomName)
        {
            await Clients.Group(roomName).SendAsync("SendMessageToGroup", $"{message.Username}: {message.Content}");
        }

        public async Task JoinRoom(string roomName)
        {
            currentUser.Id = Context.ConnectionId;
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

            roomsWithUsers = roomUsersHandler.AddUserToRoom(roomsWithUsers, roomName, currentUser);

            await Clients.OthersInGroup(roomName).SendAsync("JoinedRoom", $"{Context.ConnectionId} has entered the group {roomName}.");
            await Clients.Caller.SendAsync("NewEnteredUser", $"You are now connected to room {roomName}");
        }

        //public async Task UserTyping(bool check)
        //{
        //    var currentUser = connectedUsers.SingleOrDefault(r => r.Id == Context.ConnectionId);

        //    if (check)
        //    {
        //    }
        //}

        public async Task RemoveFromGroup(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);

            await Clients.Group(roomName).SendAsync("SendMessage", $"{Context.ConnectionId} has left the group {roomName}.");
        }
    }
}
