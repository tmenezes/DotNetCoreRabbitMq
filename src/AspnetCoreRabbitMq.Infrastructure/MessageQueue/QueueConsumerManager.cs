namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue
{
    public class QueueConsumerManager
    {
        private readonly IConnectionManager _connectionManager;

        public QueueConsumerManager(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void CreateConsumer<T>(string queueName) where T : IMessageQueueService
        {
        }
    }

    public interface IMessageQueueService<in T> : IMessageQueueService where T : class
    {
        void ProcessMessage(T message);
    }

    public interface IMessageQueueService
    {
    }
}