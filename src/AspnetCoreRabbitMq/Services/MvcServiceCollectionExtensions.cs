using DotNetCoreRabbitMq.Infrastructure.MessageQueue;
using DotNetCoreRabbitMq.Infrastructure.Serializer;
using DotNetCoreRabbitMq.Queues;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcServiceCollectionExtensions
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddSingleton<ISerializer, JsonSerializer>();
            services.AddTransient<IQueueClient, QueueClient>();
            services.AddTransient<IQueueConnectionSettings, QueueConnectionSettings>();
        }
    }
}
