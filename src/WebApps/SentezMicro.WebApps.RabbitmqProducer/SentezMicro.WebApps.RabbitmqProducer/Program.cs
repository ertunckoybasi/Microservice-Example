using RabbitMQ.Client;
using System;
using System.Text;

namespace SentezMicro.WebApps.RabbitmqProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            string RabbitKey = "Sentez";
            string RabbitMessage = "Sentez Mesajı!";

            var factory = new ConnectionFactory() { HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: RabbitKey,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete:true,
                                     arguments: null);

                
                var body = Encoding.UTF8.GetBytes(RabbitMessage);

                channel.BasicPublish(exchange: "",
                                     routingKey: RabbitKey,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", RabbitMessage);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
    }
