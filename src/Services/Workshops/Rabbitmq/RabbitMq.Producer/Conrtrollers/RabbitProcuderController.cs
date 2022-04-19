using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMq.Common.Services;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMq.Producer.Conrtrollers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RabbitProcuderController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMqService;

        public RabbitProcuderController(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }
        [HttpPost]
        public IActionResult SendMessage(string parQueue, string message)
        {

           
            using (var connection = _rabbitMqService.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: parQueue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey:parQueue,
                                     basicProperties: null,
                                     body: body);

                return Ok();
            }
        }
    }
}
