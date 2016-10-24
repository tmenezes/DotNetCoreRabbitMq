using System.Threading.Tasks;
using DotNetCoreRabbitMq.Infrastructure.MessageQueue.Connection;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;

namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue.Consumer
{
    internal class ConsumerWorker<TMessage>
        where TMessage : class
    {
        private readonly IConnectionManager _connectionManager;
        private readonly MessageQueueService<TMessage> _service;
        private readonly ConsumerProperties _consumerProperties;
        private readonly Task _workerTask;
        private IModel _channel;

        public ConsumerWorker(IConnectionManager connectionManager, MessageQueueService<TMessage> service, ConsumerProperties consumerProperties)
        {
            _connectionManager = connectionManager;
            _service = service;
            _consumerProperties = consumerProperties;

            _workerTask = new Task(Consume);
        }

        private IModel CreateChannel()
        {
            return _connectionManager.GetConnection().CreateModel();
        }

        internal void Start()
        {
            _channel = CreateChannel();
            _channel.BasicQos(0, (ushort)_consumerProperties.PreFetchCount, false);

            _workerTask.Start();
        }

        internal void Stop()
        {
            // implement cancellation token
            _channel.Close();
        }

        private void Consume()
        {
            var subscription = new Subscription(_channel, _consumerProperties.QueueName, false);

            while (true)
            {
                var deliveryArguments = subscription.Next();

                //var message = (T)  deliveryArguments.Body.DeSerialize(typeof(PurchaseOrder));
                //var routingKey = deliveryArguments.RoutingKey;

                subscription.Ack(deliveryArguments);
            }
        }
    }
}