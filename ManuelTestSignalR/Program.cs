using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;

namespace ManuelTestSignalR
{
    class Program
    {
        static Guid UserId = Guid.Parse("6ddbfdab-2fec-4240-90db-4551cba91203");
        static Guid SuportId = Guid.Parse("b6edec43-0e4c-455e-8027-49cbc8367c09");
        static void Main(string[] args)
        {
            Guid GroopId = Guid.Empty;
            Console.WriteLine("Hello World!");
            HubConnection SignelRConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5017/SuportHub")
                .Build();
            SignelRConnection.On<Guid>("ConectionCreadet", x => GroopId = x);
            SignelRConnection.On<Message>("NewMessage", x => Console.WriteLine(x.Text));
            Console.WriteLine("Conect to SuportService");
            Console.ReadLine();
            SignelRConnection.StartAsync().Wait();
            SignelRConnection.SendAsync("GetSuport","Test", "Dette er en test", Guid.NewGuid(), UserId).Wait();
            Console.WriteLine("Request Sendt");
            Console.ReadLine();
            SignelRConnection.SendAsync("NextInQue", SuportId);
            string Message = Console.ReadLine();
            while (Message != "Exit")
            {
                Message = Console.ReadLine();
                SignelRConnection.SendAsync("SendMessage", new Message() {Text = "Tester Tester Tester", UserId = UserId, GroopId = GroopId, TimeStamp = DateTime.UtcNow });
            }
        }
    }
}
