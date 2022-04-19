using Microsoft.Extensions.Options;
using RabbitMq.Common.Models;
using RabbitMQ.Client;
using System;

namespace RabbitMq.Common.Services
{
    public interface IRabbitMqService
    {
        IConnection CreateConnection();
    }

    public class RabbitMqService : IRabbitMqService
    {
        private readonly RabbitMqConfiguration _configuration;
        public RabbitMqService(IOptions<RabbitMqConfiguration> options)
        {
            _configuration = options.Value;
        }
        public IConnection CreateConnection()
        {
            ConnectionFactory connection = new ConnectionFactory()
            {
                UserName = _configuration.Username,
                Password = _configuration.Password,
                HostName = _configuration.HostName
            };

           // try
            //{
                connection.DispatchConsumersAsync = true;
                var channel = connection.CreateConnection();
               // return channel;
            //}
            //catch (System.Exception ex)
            //{

                //Console.WriteLine(ex.Message);
            //}
          
            
            
            return channel;
        }
    }
}
