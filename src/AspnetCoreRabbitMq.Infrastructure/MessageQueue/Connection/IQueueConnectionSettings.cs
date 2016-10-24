namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue.Connection
{
    public interface IQueueConnectionSettings
    {
        string HostName { get; }
        string VirtualHost { get; }
        string UserName { get; }
        string Password { get; }
    }
}