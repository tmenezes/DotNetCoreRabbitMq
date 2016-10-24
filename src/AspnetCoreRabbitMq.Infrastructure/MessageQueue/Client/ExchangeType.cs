namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue.Client
{
    public enum ExchangeType
    {
        Headers,
        Direct,
        Topic,
        Fanout
    }
}