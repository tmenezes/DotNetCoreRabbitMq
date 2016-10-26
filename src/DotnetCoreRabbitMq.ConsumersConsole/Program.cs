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

            var queue1Properties = ConsumerProperties.ForMultipleConsumers(QueueConstants.Queue1, consumersQuantity: 2);
            var queue1Manager = consumerFactory.CreateConsumerManager<Queue1Service, Message>(queue1Properties);

            var queue2Properties = ConsumerProperties.ForSingleConsumer(QueueConstants.Queue2);
            var queue2Manager = consumerFactory.CreateConsumerManager<Queue2Service, Message>(queue2Properties);

            var queue3Properties = ConsumerProperties.ForSingleConsumer(QueueConstants.Queue3);
            var queue3Manager = consumerFactory.CreateConsumerManager<Queue3Service, Message>(queue3Properties);

            queue1Manager.Start();
            queue2Manager.Start();
            queue3Manager.Start();

            Console.WriteLine("press any key to exit...");
            Console.ReadLine();
            Console.WriteLine("stoping...");

            queue1Manager.Stop();
            queue2Manager.Stop();
            queue3Manager.Stop();
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
