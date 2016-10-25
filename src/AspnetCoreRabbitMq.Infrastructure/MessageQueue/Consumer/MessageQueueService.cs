namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue.Consumer
{
    public interface IMessageQueueService<T> where T : class
    {
        void ProcessMessage(T message);
    }
}