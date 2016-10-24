namespace DotNetCoreRabbitMq.Infrastructure.MessageQueue
{
    public enum ExchangeType
    {
        Headers,
        Direct,
        Topic,
        Fanout
    }
}