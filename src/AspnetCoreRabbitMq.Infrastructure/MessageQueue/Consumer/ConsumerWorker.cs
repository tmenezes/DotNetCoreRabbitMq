using System;
using System.Threading.Tasks;
using DotNetCoreRabbitMq.Infrastructure.MessageQueue.Connection;
using DotNetCoreRabbitMq.Infrastructure.Serializer;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;

namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue.Consumer
{
    internal class ConsumerWorker<TMessage>
        where TMessage : class
    {
        private readonly IConnectionManager _connectionManager;
        private readonly IMessageQueueService<TMessage> _service;
        private readonly ConsumerProperties _consumerProperties;
        private readonly ISerializer _serializer;
        private readonly Task _workerTask;
        private IModel _channel;

        public ConsumerWorker(IConnectionManager connectionManager, IMessageQueueService<TMessage> service,
                             ConsumerProperties consumerProperties, ISerializer serializer)
        {
            _connectionManager = connectionManager;
            _service = service;
            _consumerProperties = consumerProperties;
            _serializer = serializer;

            _workerTask = new Task(Consume);
        }

        internal void Start()
        {
            _channel = CreateChannel();
            _channel.BasicQos(0, GetPrefetchCount(), false);

            _workerTask.Start();
        }

        internal void Stop()
        {
            // implement cancellation token
            _channel.Close();
        }


        private IModel CreateChannel()
        {
            return _connectionManager.GetConnection().CreateModel();
        }

        private void Consume()
        {
            var subscription = new Subscription(_channel, _consumerProperties.QueueName, false);

            while (true)
            {
                var deliveryArguments = subscription.Next();

                var message = _serializer.DeSerialize<TMessage>(deliveryArguments.Body);

                try
                {
                    _service.ProcessMessage(message);

                    subscription.Ack(deliveryArguments);
                }
                catch (Exception ex)
                {
                    // make a flexibility way to handle exception
                    //Console.WriteLine($"Queue unhandled exception. Type: {ex.GetType().Name}, Message: {ex.Message}");
                    subscription.Nack(deliveryArguments, false, true);
                }
            }
        }

        private ushort GetPrefetchCount()
        {
            if (_consumerProperties.PrefetchCount <= 0)
                return 1;

            return (ushort)_consumerProperties.PrefetchCount;
        }
    }
}