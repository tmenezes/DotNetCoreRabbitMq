using System;
using System.Threading;
using System.Threading.Tasks;
using DotNetCoreRabbitMq.Infrastructure.MessageQueue.Connection;
using DotNetCoreRabbitMq.Infrastructure.Serializer;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;

namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue.Consumer
{
    internal class ConsumerWorker<TMessage>
        where TMessage : class
    {
        private const int WAIT_TIME = 3000; // 3 seconds

        private readonly IConnectionManager _connectionManager;
        private readonly IMessageQueueService<TMessage> _service;
        private readonly ConsumerProperties _consumerProperties;
        private readonly ISerializer _serializer;
        private readonly Task _workerTask;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private IModel _channel;
        string _guid;

        public ConsumerWorker(IConnectionManager connectionManager, IMessageQueueService<TMessage> service,
                             ConsumerProperties consumerProperties, ISerializer serializer)
        {
            _connectionManager = connectionManager;
            _service = service;
            _consumerProperties = consumerProperties;
            _serializer = serializer;

            _cancellationTokenSource = new CancellationTokenSource();
            _workerTask = new Task(Consume);

            _guid = Guid.NewGuid().ToString();
        }

        internal void Start()
        {
            _channel = CreateChannel();
            _channel.BasicQos(0, GetPrefetchCount(), false);

            _workerTask.Start();
        }

        internal void Stop()
        {
            _cancellationTokenSource.Cancel();
        }


        private IModel CreateChannel()
        {
            var channel = _connectionManager.GetConnection().CreateModel();
            _connectionManager.GetConnection().AutoClose = true;

            return channel;
        }

        private void Consume()
        {
            var subscription = new Subscription(_channel, _consumerProperties.QueueName, false);

            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                BasicDeliverEventArgs deliveryArguments;
                if (!subscription.Next(WAIT_TIME, out deliveryArguments))
                    continue;

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

            subscription.Close();
            _channel.Close();
        }

        private ushort GetPrefetchCount()
        {
            if (_consumerProperties.PrefetchCount <= 0)
                return 1;

            return (ushort)_consumerProperties.PrefetchCount;
        }
    }
}