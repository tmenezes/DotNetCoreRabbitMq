using DotNetCoreRabbitMq.Infrastructure.Container;
using DotNetCoreRabbitMq.Infrastructure.MessageQueue.Connection;

namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue.Consumer
{
    public class QueueConsumerFactory
    {
        private readonly IConnectionManager _connectionManager;
        private readonly IDIContainer _container;

        public QueueConsumerFactory(IConnectionManager connectionManager, IDIContainer container)
        {
            _connectionManager = connectionManager;
            _container = container;
        }

        public QueueConsumerManager<TMessage> CreateConsumer<TMessage>(MessageQueueService<TMessage> service, ConsumerProperties consumerProperties)
            where TMessage : class
        {
            //return new QueueConsumerManager<TMessage>(_connectionManager, service, consumerProperties);
            return null;
        }

        public QueueConsumerManager<TMessage> CreateConsumer<TService, TMessage>(ConsumerProperties consumerProperties)
            where TService : MessageQueueService<TMessage>
            where TMessage : class
        {
            var service = _container.Resolve<TService>();

            return CreateConsumer(service, consumerProperties);
        }


        private ConsumerWorker<TMessage> CreateConsumerWorker<TMessage>(MessageQueueService<TMessage> service, ConsumerProperties consumerProperties)
            where TMessage : class
        {
            return new ConsumerWorker<TMessage>(_connectionManager, service, consumerProperties);
        }
    }
}