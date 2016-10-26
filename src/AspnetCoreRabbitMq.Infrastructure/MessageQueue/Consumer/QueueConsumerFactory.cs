using System.Linq;
using DotNetCoreRabbitMq.Infrastructure.Container;
using DotNetCoreRabbitMq.Infrastructure.MessageQueue.Connection;
using DotNetCoreRabbitMq.Infrastructure.Serializer;

namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue.Consumer
{
    public class QueueConsumerFactory
    {
        private readonly IDIContainer _container;
        private readonly IConnectionManager _connectionManager;

        public QueueConsumerFactory(IDIContainer container)
        {
            _container = container;
            _connectionManager = container.Resolve<IConnectionManager>();
        }

        public QueueConsumerManager<TMessage> CreateConsumerManager<TService, TMessage>(ConsumerProperties consumerProperties)
            where TService : IMessageQueueService<TMessage>
            where TMessage : class
        {
            var serializer = _container.Resolve<ISerializer>();

            // creates 'N' workers, where 'N' is equals consumer's quantity defined in ConsumerProperties instance
            var workers = Enumerable.Range(1, consumerProperties.ConsumersQuantity)
                                    .Select(i =>
                                    {
                                        var service = _container.Resolve<TService>();
                                        return new ConsumerWorker<TMessage>(_connectionManager, service, consumerProperties, serializer);
                                    }).ToList();

            return new QueueConsumerManager<TMessage>(workers, consumerProperties);
        }
    }
}