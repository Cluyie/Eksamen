using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Moq;
using NUnit.Framework;
using SignalR_Microservice.Hubs;

namespace NUnitSignalR
{
    public static class ResourceHubTests
    {
        public class UpdateReservationTests
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
                //Change object to Reservation type when it is available from DB team
                object reservation = new object();
                await resourceHub.UpdateReservation(reservation);

                mockClients.Verify(clients => clients.All, Times.Once);

                mockClientProxy.Verify(
                    clientProxy => clientProxy.SendCoreAsync(
                        "ReceiveMessage",
                        It.Is<object[]>(o => o[0].Equals(reservation)),
                        default(CancellationToken)),
                    Times.Once);
            }
        }

        public class UpdateResourceTests
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
            public async Task UpdateResource_ShouldReturnSentResourceObject()
            {
                //Change object to Resource type when it is available from DB team
                object resource = new object();
                await resourceHub.UpdateResource(resource);

                mockClients.Verify(clients => clients.All, Times.Once);

                mockClientProxy.Verify(
                    clientProxy => clientProxy.SendCoreAsync(
                        "UpdateReservation",
                        It.Is<object[]>(o => o[0].Equals(resource)),
                        default(CancellationToken)),
                    Times.Once);
            }
        }
    }
}
