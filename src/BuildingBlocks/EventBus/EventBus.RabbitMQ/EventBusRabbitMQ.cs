using EventBus.Base;
using EventBus.Base.Events;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace EventBus.RabbitMQ
{
    public class EventBusRabbitMQ : BaseEventBus
    {
        RabbitMQPersistentConnection persistentConnection;
        private readonly IConnectionFactory connectionFactory;
        private readonly IModel consumerChannel;
        private readonly int _retryCount=5;

        public EventBusRabbitMQ(EventBusConfig config, IServiceProvider serviceProvider) : base(config, serviceProvider)
        {
            if (EventBusConfig.Connection != null)
            {
                if (EventBusConfig.Connection is ConnectionFactory)
                    connectionFactory = EventBusConfig.Connection as ConnectionFactory;
                else
                {
                    var connJson = JsonConvert.SerializeObject(EventBusConfig.Connection, new JsonSerializerSettings()
                    {
                        // Self referencing loop detected for property 
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                    connectionFactory = JsonConvert.DeserializeObject<ConnectionFactory>(connJson);
                }
            }
            else
                connectionFactory = new ConnectionFactory(); //Create with default values

            persistentConnection = new RabbitMQPersistentConnection(connectionFactory, config.ConnectionRetryCount);

            consumerChannel = CreateConsumerChannel();

            SubsManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            eventName = ProcessEventName(eventName);

            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            consumerChannel.QueueUnbind(queue: eventName,
                exchange: EventBusConfig.DefaultTopicName,
                routingKey: eventName);

            if (SubsManager.IsEmpty)
            {
                consumerChannel.Close();
            }
        }

        /*  public override void Publish(IntegrationEvent @event)
          {
              if (!persistentConnection.IsConnected)
              {
                  persistentConnection.TryConnect();
              }

              var policy = RetryPolicy.Handle<BrokerUnreachableException>()
               .Or<SocketException>()
               .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
               {
                   //_logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.Id, $"{time.TotalSeconds:n1}", ex.Message);
               });



              var eventName = @event.GetType().Name;

            //  _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, eventName);

              using (var channel = persistentConnection.CreateModel())
              {
                 // _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);

                  channel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName, type: "direct");

                  var body = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions
                  {
                      WriteIndented = true
                  });

                  //channel.QueueDeclare(queue: GetSubName(eventName),
                  //                    durable: false,
                  //                    exclusive: false,
                  //                    autoDelete: true,
                  //                    arguments: null);

                  //consumerChannel.QueueBind(queue: GetSubName(eventName),
                  //                  exchange: EventBusConfig.DefaultTopicName,
                  //                  routingKey: eventName);


                  policy.Execute(() =>
                  {
                      var properties = channel.CreateBasicProperties();
                      properties.DeliveryMode = 2; // persistent

                     // _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);

                      channel.BasicPublish(
                          exchange: EventBusConfig.DefaultTopicName,
                          routingKey: eventName,
                          mandatory:false,
                          basicProperties: properties,
                          body: body);
                  });
              }
          }*/
        public override void Publish(IntegrationEvent @event)
        {
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            var policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(EventBusConfig.ConnectionRetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    // log
                });

            var eventName = @event.GetType().Name;
            eventName = ProcessEventName(eventName);

            consumerChannel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName, type: "direct"); // Ensure exchange exists while publishing

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            policy.Execute(() =>
            {
                var properties = consumerChannel.CreateBasicProperties();
                properties.DeliveryMode = 2; // persistent

                //consumerChannel.QueueDeclare(queue: GetSubName(eventName), // Ensure queue exists while publishing
                //                     durable: true,
                //                     exclusive: false,
                //                     autoDelete: false,
                //                     arguments: null);

                //consumerChannel.QueueBind(queue: GetSubName(eventName),
                //                  exchange: EventBusConfig.DefaultTopicName,
                //                  routingKey: eventName);

                consumerChannel.BasicPublish(
                    exchange: EventBusConfig.DefaultTopicName,
                    routingKey: eventName,
                    mandatory: true,
                    basicProperties: properties,
                    body: body);
            });
        }

        public override void Subscribe<T, TH>()
        {
            var eventName = typeof(T).Name;
            eventName = ProcessEventName(eventName);

            if (!SubsManager.HasSubscriptionsForEvent(eventName))
            {
                if (!persistentConnection.IsConnected)
                {
                    persistentConnection.TryConnect();
                }

                consumerChannel.QueueDeclare(queue: GetSubName(eventName), // Ensure queue exists while consuming
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                consumerChannel.QueueBind(queue: GetSubName(eventName),
                                  exchange: EventBusConfig.DefaultTopicName,
                                  routingKey: eventName);
            }

            SubsManager.AddSubscription<T, TH>();
            StartBasicConsume(eventName);
        }

        public override void UnSubscribe<T, TH>()
        {
            SubsManager.RemoveSubscription<T, TH>();
        }


        private IModel CreateConsumerChannel()
        {
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            var channel = persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName,
                                    type: "direct");

            return channel;
        }

        private void StartBasicConsume(string eventName)
        {
            if (consumerChannel != null)
            {
                var consumer = new EventingBasicConsumer(consumerChannel);

                consumer.Received += Consumer_Received;

                consumerChannel.BasicConsume(
                    queue: GetSubName(eventName),
                    autoAck: false,
                    consumer: consumer);
            }
        }

        private async void Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            eventName = ProcessEventName(eventName);
            var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

            try
            {
                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            {
                // logging
            }

            consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }
    }
}
