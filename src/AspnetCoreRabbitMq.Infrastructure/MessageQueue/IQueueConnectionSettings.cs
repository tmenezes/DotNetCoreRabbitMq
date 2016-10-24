namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue
{
    public interface IQueueConnectionSettings
    {
        string HostName { get; }
        string VirtualHost { get; }
        string UserName { get; }
        string Password { get; }
    }
}