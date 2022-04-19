using Microsoft.AspNetCore.Mvc;
using RabbitMq.Common.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Consumer.Conrtrollers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMqService;

        public ConsumerController(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }
        [HttpGet]
        public string ReadMessage(string parQueue)
        {
            string msMessage = "";
            using (var connection = _rabbitMqService.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                {
                    channel.QueueDeclare(queue: parQueue,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new AsyncEventingBasicConsumer(channel);
                    consumer.Received += async (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        msMessage = message;
                        Console.WriteLine(" Received {0}", message);
                        await Task.Yield();
                    };

                    channel.BasicConsume(queue: parQueue, autoAck: true, consumer: consumer);

                }

            }
            return msMessage;
        }

      
    }
}
