using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace RabitMQEasy
{
    public class RabitMQPublicer
    {
        public ConnectionFactory Factory;
        public RabitMQPublicer(ConnectionFactory factory)
        {
            Factory = factory;
        }
        public void PunlicEvent<T>(Events events, T @object) where T : class
        {
            using (IConnection connection = Factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    string eventName = events.ToString("G")+"-";
                    Type obj = typeof(T);
                    if (obj.IsInterface)
                    {
                        string name = obj.ToString();
                        eventName += name.Remove(name.IndexOf("["));
                    }
                    else
                    {
                        eventName += obj.Name;
                    }
                    channel.ExchangeDeclare(eventName, ExchangeType.Fanout);
                    channel.BasicPublish(eventName, "", null, Serlicere(@object));
                }
            }
        }
        public void PunlicObject<T>(T @object) where T : class
        {
            using (IConnection connection = Factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    string eventName = "";
                    Type obj = typeof(T);
                    if (obj.IsInterface)
                    {
                        string name = obj.ToString();
                        eventName += name.Remove(name.IndexOf("["));
                    }
                    else
                    {
                        eventName = obj.Name;
                    }
                    channel.ExchangeDeclare(eventName, ExchangeType.Fanout);
                    channel.BasicPublish(eventName, "", null, Serlicere(@object));
                }
            }
        }

        public void PunlicObject<T>(string publicKey , T @object) where T : class
        {
            using (IConnection connection = Factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    Type obj = typeof(T);
                    channel.ExchangeDeclare(publicKey, ExchangeType.Fanout);
                    channel.BasicPublish(publicKey, "", null, Serlicere(@object));
                }
            }
        }

        public byte[] Serlicere<T>(T Object)
        {
            var message = JsonSerializer.Serialize(Object);
            var body = Encoding.UTF8.GetBytes(message);
            return body;
        }
    }
}
