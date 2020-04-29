﻿using Microsoft.AspNetCore.SignalR;
using SignalR_Microservice.Commands;
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
        private readonly IChatLoggingService _messageLogging;
        ChatSenderService ChatSender { get; set; }
        public ChatHub(IChatLoggingService messageLogging, ChatSenderService chatSender)
        {
            _messageLogging = messageLogging;
            ChatSender = chatSender;
        }

        public async Task SendMessageToGroup(Message message, string roomName)
        {
            ChatSender.SendSendMessageToGroup(message.Text, roomName);
            //await Clients.Group(roomName).SendAsync("SendMessageToGroup", message.Text);

            await _messageLogging.SendMessageAsync(message);
        }

        public async Task JoinGroup(string groupId)
        {
            var userId = Context.ConnectionId;

            await Groups.AddToGroupAsync(userId, groupId);
            await Clients.OthersInGroup(groupId).SendAsync("JoinGroup", $"{userId} has entered the room {groupId}.");
            await Clients.Caller.SendAsync("NewEnteredUser", $"You are now connected to room {groupId}");
        }

        public async Task RemoveFromRoom(string groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);

            await Clients.Group(groupId)
                .SendAsync("SendMessage", $"{Context.ConnectionId} has left the room {groupId}.");
        }
    }
}