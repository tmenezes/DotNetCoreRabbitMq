using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCoreRabbitMq.Core;
using DotNetCoreRabbitMq.Infrastructure.Container;
using DotNetCoreRabbitMq.Infrastructure.MessageQueue.Connection;
using DotNetCoreRabbitMq.Infrastructure.MessageQueue.Consumer;
using DotNetCoreRabbitMq.Infrastructure.Serializer;
using SimpleInjector;

namespace DotnetCoreRabbitMq.ConsumersConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = SetupAndGetContainer();
            var consumerFactory = new QueueConsumerFactory(container);

            var queue1Properties = ConsumerProperties.ForSingleConsumer(QueueConstants.Queue1);
            var queue1Manager = consumerFactory.CreateConsumerManager<ServiceQueue1, Message>(queue1Properties);

            var queue1Properties = ConsumerProperties.ForSingleConsumer(QueueConstants.Queue1, 10);
            var queue1Manager = consumerFactory.CreateConsumerManager<ServiceQueue1, Message>(queue1Properties);

            queue1Manager.Start();
            Console.ReadLine();
        }

        private static IDIContainer SetupAndGetContainer()
        {
            var container = new Container();
            var containerWrapper = new DIContainer(container);

            containerWrapper.RegisterAsSingleton<IQueueConnectionSettings, QueueConnectionSettings>();
            QueueComponentRegistrator.RegisterComponents(containerWrapper);

            return containerWrapper;
        }
    }
}
