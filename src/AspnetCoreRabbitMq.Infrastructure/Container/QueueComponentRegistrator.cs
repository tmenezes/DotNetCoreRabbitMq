using DotNetCoreRabbitMq.Infrastructure.MessageQueue.Client;
using DotNetCoreRabbitMq.Infrastructure.MessageQueue.Connection;
using DotNetCoreRabbitMq.Infrastructure.Serializer;

namespace DotNetCoreRabbitMq.Infrastructure.Container
{
    public static class QueueComponentRegistrator
    {
        public static void RegisterComponents(IDIContainer container)
        {
            container.RegisterAsSingleton<IConnectionManager, ConnectionManager>();
            container.RegisterAsSingleton<ISerializer, JsonSerializer>();
            container.RegisterAsTransient<IQueueClient, QueueClient>();
        }
    }
}