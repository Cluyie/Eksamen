using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Models;
using Moq;
using NUnit.Framework;
using SignalR_Microservice.Hubs;

namespace NUnitSignalR
{
    public class ResourceHubTests
    {
        public ResourceHub resourceHub { get; set; }
        public Mock<IHubCallerClients> mockClients { get; set; }
        public Mock<IClientProxy> mockClientProxy { get; set; }

        [SetUp]
        public void Setup()
        {
            mockClients = new Mock<IHubCallerClients>();
            mockClientProxy = new Mock<IClientProxy>();

            mockClients.Setup(clients => clients.All).Returns(mockClientProxy.Object);

            resourceHub = new ResourceHub()
            {
                Clients = mockClients.Object
            };
        }

        [Test]
        public async Task UpdateReservation_ShouldReturnSentReservationObject()
        {
            Reservation reservation = new Reservation()
            {
                Id = Guid.NewGuid()
            };
            await resourceHub.UpdateReservation(reservation);

            mockClients.Verify(clients => clients.All, Times.Once);

            mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync(
                    "UpdateReservation",
                    new object[] { reservation },
                    default(CancellationToken)),
                Times.Once);
        }

        [Test]
        public async Task UpdateResource_ShouldReturnSentResourceObject()
        {
            Resource resource = new Resource()
            {
                Id = Guid.NewGuid()
            };
            await resourceHub.UpdateResource(resource);

            mockClients.Verify(clients => clients.All, Times.Once);

            mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync(
                    "UpdateResource",
                    new object[] { resource },
                    default(CancellationToken)),
                Times.Once);
        }
    }
}