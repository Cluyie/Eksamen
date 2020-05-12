using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabitMQEasy
{
    public class RabitMQCunsumer:IDisposable
    {
        public IConnection Connection;
        public List<IModel> Models;
        public RabitMQCunsumer(ConnectionFactory factory)
        {
            Connection = factory.CreateConnection();
            Models = new List<IModel>();
        }
        public async Task SubscribeOnEvent<T,TR>(IEventHandler<T, TR> handler) where T : class, TR
        {
            string eventName = handler.events.ToString("G") + "-";
            Type obj = typeof(TR);
            if (obj.IsInterface)
            {
                string name = obj.ToString();
                int i = name.IndexOf("[");
                eventName += i > 0? name.Remove(i): name;
                
            }
            else
            {
                eventName += obj.Name;
            }

            await Subscribe<T,TR>(eventName, handler.action);
        }
        
        public async Task SubscribeOnObject<T,TR>(ILissener<T, TR> handler) where T : class, TR
        {
            string eventName = "";
            Type obj = typeof(TR);
            if (obj.IsInterface)
            {
                string name = obj.ToString();
                eventName += name.Remove(name.IndexOf("["));
            }
            else
            {
                eventName += obj.Name;
            }
            await Subscribe<T,TR>(eventName,handler.action);

        }
        public async Task Subscribe<T,TR>(string eventName, Func<TR,Task> funtion) where T : class, TR
        {
            IModel channel = Connection.CreateModel();
            Models.Add(channel);
            channel.ExchangeDeclare(eventName, ExchangeType.Fanout);

            string queueName = channel.QueueDeclare(eventName + "-" + Guid.NewGuid().ToString(), false, false, true, null).QueueName;

            channel.QueueBind(queueName, eventName, "");

            Console.WriteLine($" [*] Waiting for {eventName}.");

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                Console.WriteLine("Message resived Event: " + eventName);
                funtion(Deserlicere<T>(ea.Body.ToArray()));
            };
            channel.BasicConsume(queueName, true, consumer); 

        }
        private T Deserlicere<T>(byte[] json)where T : class
        {
            string message = Encoding.UTF8.GetString(json);
            return JsonSerializer.Deserialize<T>(message);
        }
        public void Dispose()
        {
            Models.ForEach(x => x.Dispose());
            Connection.Dispose();
        }

    }
}
