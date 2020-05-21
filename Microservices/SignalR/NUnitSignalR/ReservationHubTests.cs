using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Moq;
using NUnit.Framework;
using SignalR_Microservice.Hubs;
using SignalR_Microservice.Models;

namespace NUnitSignalR
{
    public class ReservationHubTests
    {
        public ReservationHub reservationHub { get; set; }
        public Mock<IHubCallerClients> mockClients { get; set; }
        public Mock<IClientProxy> mockClientProxy { get; set; }

        [SetUp]
        public void Setup()
        {
            mockClients = new Mock<IHubCallerClients>();
            mockClientProxy = new Mock<IClientProxy>();

            mockClients.Setup(clients => clients.All).Returns(mockClientProxy.Object);

            reservationHub = new ReservationHub
            {
                Clients = mockClients.Object
            };
        }

        [Test]
        public async Task CreateReservation_ShouldReturnSentReservationObject()
        {
            var reservation = new Reservation
            {
                Id = Guid.NewGuid()
            };
            await reservationHub.CreateReservation(reservation);

            mockClients.Verify(clients => clients.All, Times.Once);

            mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync(
                    "CreateReservation",
                    new object[] {reservation},
                    default),
                Times.Once);
        }

        [Test]
        public async Task UpdateReservation_ShouldReturnSentReservationObject()
        {
            var reservation = new Reservation
            {
                Id = Guid.NewGuid()
            };
            await reservationHub.UpdateReservation(reservation);

            mockClients.Verify(clients => clients.All, Times.Once);

            mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync(
                    "UpdateReservation",
                    new object[] {reservation},
                    default),
                Times.Once);
        }

        [Test]
        public async Task DeleteReservation_ShouldReturnSentReservationObject()
        {
            var reservation = new Reservation
            {
                Id = Guid.NewGuid()
            };
            await reservationHub.DeleteReservation(reservation);

            mockClients.Verify(clients => clients.All, Times.Once);

            mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync(
                    "DeleteReservation",
                    new object[] {reservation},
                    default),
                Times.Once);
        }
    }
}