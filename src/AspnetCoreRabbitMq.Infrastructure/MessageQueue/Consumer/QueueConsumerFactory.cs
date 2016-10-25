using System.Linq;
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

        public QueueConsumerManager<TMessage> CreateConsumerManager<TService, TMessage>(ConsumerProperties consumerProperties)
            where TService : MessageQueueService<TMessage>
            where TMessage : class
        {
            // creates 'N' workers, on 'N' is equals consumer's quantity defined in ConsumerProperties instance
            var workers = Enumerable.Range(1, consumerProperties.ConsumersQuantity)
                                    .Select(i =>
                                    {
                                        var service = _container.Resolve<TService>();
                                        return new ConsumerWorker<TMessage>(_connectionManager, service, consumerProperties);
                                    });

            return new QueueConsumerManager<TMessage>(workers, consumerProperties);
        }
    }
}