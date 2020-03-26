using Microsoft.AspNetCore.SignalR;
using Moq;
using NUnit.Framework;
using SignalR_Microservice.Hubs;
using SignalR_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NUnitSignalR
{
    public class AvailableTimeHubTests
    {
        public AvailableTimeHub availableTimeHub { get; set; }
        public Mock<IHubCallerClients> mockClients { get; set; }
        public Mock<IClientProxy> mockClientProxy { get; set; }

        [SetUp]
        public void Setup()
        {
            mockClients = new Mock<IHubCallerClients>();
            mockClientProxy = new Mock<IClientProxy>();

            mockClients.Setup(clients => clients.All).Returns(mockClientProxy.Object);

            availableTimeHub = new AvailableTimeHub()
            {
                Clients = mockClients.Object
            };
        }

        [Test]
        public async Task CreateAvailableTime_ShouldReturnSentAvailableTimeObject()
        {
            AvailableTime availableTime = new AvailableTime()
            {
                Id = Guid.NewGuid()
            };
            await availableTimeHub.CreateAvailableTime(availableTime);

            mockClients.Verify(clients => clients.All, Times.Once);

            mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync(
                    "CreateAvailableTime",
                    new object[] { availableTime },
                    default(CancellationToken)),
                Times.Once);
        }

        [Test]
        public async Task UpdateAvailableTime_ShouldReturnSentAvailableTimeObject()
        {
            AvailableTime availableTime = new AvailableTime()
            {
                Id = Guid.NewGuid()
            };
            await availableTimeHub.UpdateAvailableTime(availableTime);

            mockClients.Verify(clients => clients.All, Times.Once);

            mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync(
                    "UpdateAvailableTime",
                    new object[] { availableTime },
                    default(CancellationToken)),
                Times.Once);
        }

        [Test]
        public async Task DeleteAvailableTime_ShouldReturnSentAvailableTimeObject()
        {
            AvailableTime availableTime = new AvailableTime()
            {
                Id = Guid.NewGuid()
            };
            await availableTimeHub.DeleteAvailableTime(availableTime);

            mockClients.Verify(clients => clients.All, Times.Once);

            mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync(
                    "DeleteAvailableTime",
                    new object[] { availableTime },
                    default(CancellationToken)),
                Times.Once);
        }
    }
}