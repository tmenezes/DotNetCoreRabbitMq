namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue.Consumer
{
    public abstract class MessageQueueService<T> where T : class
    {
        public abstract void ProcessMessage(T message);
    }
}