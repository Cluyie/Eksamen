using Microsoft.AspNetCore.SignalR;
using Moq;
using NUnit.Framework;
using SignalR_Microservice.Hubs;
using SignalR_Microservice.Models;
using SignalR_Microservice.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace NUnitSignalR
{
    public class ChatHubTest
    {
        public ChatHub chatHub { get; set; }
        public Mock<IHubCallerClients> mockClients { get; set; }
        public Mock<IClientProxy> mockClientProxy { get; set; }
        public Mock<HubCallerContext> navn { get; set; }
        public Mock<IGroupManager> mockGroupManager { get; set; }
        public IChatLoggingService loggingService { get; set; }

        [SetUp]
        public void Setup()
        {
            mockClients = new Mock<IHubCallerClients>();
            mockClientProxy = new Mock<IClientProxy>();
            navn = new Mock<HubCallerContext>();

            mockClients.Setup(clients => clients.Group("group1")).Returns(mockClientProxy.Object);

            navn.Setup(c => c.ConnectionId).Returns(Guid.NewGuid().ToString());

            chatHub = new ChatHub(loggingService)
            {
                Clients = mockClients.Object,
                Context = navn.Object
            };
            chatHub.Clients.Group("group1");
        }

        [Test]
        public async Task SendMessage_ShouldSendAMessage()
        {
            var message = new Message
            {
                Username = "Hans",
                Content = "hejsa"
            };

            await chatHub.JoinGroup("group1");
            await chatHub.SendMessageToGroup(message, "group1");


            mockClients.Verify(clients => clients.Groups("group1"), Times.Once);

            mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync(
                    "SendMessage",
                    new object[] { message },
                    default),
                Times.Once);
        }
    }
}
