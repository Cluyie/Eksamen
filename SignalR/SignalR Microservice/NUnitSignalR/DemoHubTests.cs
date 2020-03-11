using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Moq;
using NUnit.Framework;
using SignalR_Microservice.Hubs;

namespace NUnitSignalR
{
    public class Tests
    {
        public DemoHub demohub { get; set; }
        public Mock<IHubCallerClients> mockClients { get; set; }
        public Mock<IClientProxy> mockClientProxy { get; set; }

        [SetUp]
        public void Setup()
        {
            mockClients = new Mock<IHubCallerClients>();
            mockClientProxy = new Mock<IClientProxy>();

            mockClients.Setup(clients => clients.All).Returns(mockClientProxy.Object);


            demohub = new DemoHub()
            {
                Clients = mockClients.Object
            };
        }

        [Test]
        public async Task ReceiveMessage_ShouldReturnTestUserAndTestMessage()
        {
            await demohub.SendMessage("TestUser", "TestMessage");

            mockClients.Verify(clients => clients.All, Times.Once);

            mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync(
                    "ReceiveMessage",
                    It.Is<object[]>(o => o[0].Equals("TestUser") && o[1].Equals("TestMessage")),
                    default(CancellationToken)),
                Times.Once);
        }
    }
}